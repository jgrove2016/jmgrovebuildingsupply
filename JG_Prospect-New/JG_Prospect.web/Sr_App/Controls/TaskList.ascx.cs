using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using System.Data;
using JG_Prospect.Common;
using Saplin.Controls;
namespace TestingTask
{
    public partial class TaskList : System.Web.UI.UserControl
    {
        DataTable dtDesignation;
        DataTable dtAssignedUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDesignation();
                FillTaskStatus();
                SearchTask();
            }

        }


        //Fill Designation
        private void FillDesignation()
        {
            if (ddlDesignation.Items.Count > 0)
            {
                ddlDesignation.Items.Clear();
            }
            DataTable dt = TaskBLL.Instance.GetAllDesignation();
            if (dt != null)
            {
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = dt.Columns[0].ToString();
                ddlDesignation.DataValueField = dt.Columns[0].ToString();
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, new ListItem("Select Designation", ""));

        }

        protected void DdlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedIndex > 0)
            {
                FillAsignedUserByDesignation(ddlDesignation.SelectedValue.ToString());
            }
        }

        //Fill AsignedUsers
        private void FillAsignedUserByDesignation(string designation)
        {

            if (ddlAsignedUser.Items.Count > 0)
            {
                ddlAsignedUser.Items.Clear();
            }
            DataTable dt = TaskBLL.Instance.GetUsersByDesignation(designation);
            if (dt != null)
            {
                ddlAsignedUser.DataSource = dt;
                ddlAsignedUser.DataValueField = dt.Columns["Id"].ToString();
                ddlAsignedUser.DataTextField = dt.Columns["UserName"].ToString();

                ddlAsignedUser.DataBind();
            }
        }

        //Fill Task Status
        private void FillTaskStatus()
        {
            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.Items.Clear();
            }
            Array itemValues = System.Enum.GetValues(typeof(JGConstant.TaskStatus));
            Array itemNames = System.Enum.GetNames(typeof(JGConstant.TaskStatus));

            foreach (int value in itemValues)
            {
                string name = Enum.GetName(typeof(JGConstant.TaskStatus), value);
                ListItem item = new ListItem(name, value.ToString());
                ddlStatus.Items.Add(item);
            }
            ddlStatus.Items.Insert(0, new ListItem("Select Status", ""));

        }

        //Search
        private void SearchTask()
        {
            List<TaskEntity> lstTaskEntity = new List<TaskEntity>();
            List<TaskAssignedUserEntity> lstTaskAssignedUserEntity = new List<TaskAssignedUserEntity>();

            GetDropdownsData();

            string title = txtTitle.Text != string.Empty ? txtTitle.Text.Trim() : null;
            string designation = ddlDesignation.SelectedIndex > 0 ? ddlDesignation.SelectedValue.ToString() : null;
            string asignedUser = null;
            int itemCount = ddlAsignedUser.Items.Count;
            int counter = 1;
            foreach (ListItem item in ddlAsignedUser.Items)
            {
                if (item.Selected)
                {
                    if (itemCount == counter)
                    {
                        asignedUser += item.Value;
                    }
                    else
                    {
                        asignedUser += item.Value + ",";
                    }
                }
                counter++;
            }


            int? status = ddlStatus.SelectedIndex > 0 ? (int?)Convert.ToInt16(ddlStatus.SelectedValue) : null;
            DateTime? date = txtDatePicker.Text != string.Empty ? (DateTime?)DateTime.ParseExact(txtDatePicker.Text, "dd/MM/yyyy", null) : null;
            DataSet ds = TaskBLL.Instance.GetTaskList(title, designation, asignedUser, status, date);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TaskEntity taskEntity = new TaskEntity();
                            taskEntity.ID = Convert.ToInt32(dr["ID"]);
                            taskEntity.Title = dr["Title"].ToString();
                            taskEntity.Designation = dr["Designation"].ToString();
                            taskEntity.Status = Convert.ToInt16(dr["Status"]);
                            taskEntity.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            taskEntity.CustomID = Convert.ToDateTime(taskEntity.CreatedDate).ToShortDateString() + "_" + taskEntity.ID;
                            lstTaskEntity.Add(taskEntity);
                        }
                        if (ds.Tables[1] != null)
                        {
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                TaskAssignedUserEntity taskAssignedUserEntity = new TaskAssignedUserEntity();
                                taskAssignedUserEntity.TaskID = Convert.ToInt32(dr["TaskID"]);
                                taskAssignedUserEntity.UserID = Convert.ToInt32(dr["UserID"]);
                                lstTaskAssignedUserEntity.Add(taskAssignedUserEntity);
                            }
                        }

                        foreach (TaskEntity taskEntity in lstTaskEntity)
                        {
                            taskEntity.LstTaskAssignedUser = lstTaskAssignedUserEntity.Where(x => x.TaskID == taskEntity.ID).ToList();
                        }
                        rptTaskList.DataSource = lstTaskEntity;
                        rptTaskList.DataBind();
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "No Record Found";
                        rptTaskList.DataSource = lstTaskEntity;
                        rptTaskList.DataBind();

                    }
                }

            }
        }

        //Search 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTask();
        }

        private void GetDropdownsData()
        {
            dtDesignation = TaskBLL.Instance.GetAllDesignation();
            dtAssignedUser = TaskBLL.Instance.GetUsersByDesignation(null);
        }

        protected void RptTaskList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlDesignation = e.Item.FindControl("ddlDesignation") as DropDownList;
                DropDownList ddlStatus = e.Item.FindControl("ddlStatus") as DropDownList;
                DropDownCheckBoxes ddlAsignedUser = e.Item.FindControl("ddlAsignedUser") as DropDownCheckBoxes;
                string designation = DataBinder.Eval(e.Item.DataItem, "Designation").ToString();
                string status = DataBinder.Eval(e.Item.DataItem, "Status").ToString();
                List<TaskAssignedUserEntity> lstAssignedUser = DataBinder.Eval(e.Item.DataItem, "LstTaskAssignedUser") as List<TaskAssignedUserEntity>;

                if (ddlDesignation != null)
                {
                    DataTable dt = dtDesignation;
                    if (dt != null)
                    {
                        ddlDesignation.DataSource = dt;
                        ddlDesignation.DataTextField = dt.Columns[0].ToString();
                        ddlDesignation.DataValueField = dt.Columns[0].ToString();
                        ddlDesignation.DataBind();
                    }
                    ddlDesignation.SelectedValue = designation;
                }

                if (ddlAsignedUser != null)
                {
                    DataTable dt = dtAssignedUser;
                    if (dt != null)
                    {
                        ddlAsignedUser.DataSource = dt;
                        ddlAsignedUser.DataValueField = dt.Columns["Id"].ToString();
                        ddlAsignedUser.DataTextField = dt.Columns["UserName"].ToString();

                        ddlAsignedUser.DataBind();
                    }
                    foreach (TaskAssignedUserEntity assignedEntity in lstAssignedUser)
                    {
                        ddlAsignedUser.SelectedValue = assignedEntity.UserID.ToString();
                    }

                }

                if (ddlStatus != null)
                {
                    Array itemValues = System.Enum.GetValues(typeof(JGConstant.TaskStatus));
                    Array itemNames = System.Enum.GetNames(typeof(JGConstant.TaskStatus));

                    foreach (int value in itemValues)
                    {
                        string name = Enum.GetName(typeof(JGConstant.TaskStatus), value);
                        ListItem item = new ListItem(name, value.ToString());
                        ddlStatus.Items.Add(item);
                    }

                    ddlStatus.SelectedValue = status;
                }
            }
        }
    }
    internal class TaskAssignedUserEntity
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
    }
    internal class TaskEntity
    {
        public int ID { get; set; }
        public string CustomID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Designation { get; set; }
        public int Status { get; set; }
        public int UserAcceptenceID { get; set; }
        public int DueDate { get; set; }
        public string Hours { get; set; }
        public string Notes { get; set; }
        public string Attachment { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<TaskAssignedUserEntity> LstTaskAssignedUser { get; set; }

    }
}