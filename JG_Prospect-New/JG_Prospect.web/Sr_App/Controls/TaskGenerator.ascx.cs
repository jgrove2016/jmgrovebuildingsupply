#region "-- using -"

using JG_Prospect.BLL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using Saplin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.Configuration;

#endregion

namespace JG_Prospect.Sr_App.Controls
{
    public partial class TaskGenerator : System.Web.UI.UserControl
    {
        #region "--Page methods--"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Add mode.
                controlMode.Value = "0";
                LoadFilters();
                SearchTasks(null);
                LoadPopupDropdown();
            }
        }



        #endregion

        #region "-Control Events-"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTasks(null);
        }

        protected void btnLoadMore_Click(object sender, EventArgs e)
        {
            SearchTasks(50);
            ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "expand", "SetHeaderSectionHeight();", true);
        }

        protected void gvTasks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTaskStatus = (Label)e.Row.FindControl("lblTaskStatus");

                if (lblTaskStatus != null)
                {
                    lblTaskStatus.Text = ((JGConstant.TaskStatus)Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Status"))).ToString();

                }

            }
        }

        protected void btnSaveTask_Click(object sender, EventArgs e)
        {
            SaveTask();
        }

        /// <summary>
        /// Will bind users based on designation changed in dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUsersByDesgination();
        }

        private void LoadUsersByDesgination()
         {
            DataSet dsUsers;

            // DropDownCheckBoxes ddlAssign = (FindControl("ddcbAssigned") as DropDownCheckBoxes);
            // DropDownList ddlDesignation = (DropDownList)sender;
            string designation = ddlUserDesignation.SelectedValue;

            dsUsers = TaskGeneratorBLL.Instance.GetInstallUsers(2, designation);

            ddcbAssigned.DataSource = dsUsers;
            ddcbAssigned.DataTextField = "FristName";
            ddcbAssigned.DataValueField = "Id";
            ddcbAssigned.DataBind();
        }

        /// <summary>
        /// To bind users on change designation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddcbAssigned_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controlMode.Value == "0")
            {
                DataSet dsUsers = new DataSet();
                DataSet tempDs;
                List<string> SelectedUsersID = new List<string>();
                List<string> SelectedUsers = new List<string>();
                foreach (System.Web.UI.WebControls.ListItem item in ddcbAssigned.Items)
                {
                    if (item.Selected)
                    {
                        SelectedUsersID.Add(item.Value);
                        SelectedUsers.Add(item.Text);
                        tempDs = TaskGeneratorBLL.Instance.GetInstallUserDetails(Convert.ToInt32(item.Value));
                        dsUsers.Merge(tempDs);
                    }
                }
                if (dsUsers.Tables.Count != 0)
                {
                    gdTaskUsers.DataSource = dsUsers;
                    gdTaskUsers.DataBind();
                }
                else
                {
                    gdTaskUsers.DataSource = null;
                    gdTaskUsers.DataBind();
                } 
            }

        }

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditTask")
            {
                controlMode.Value = "1";
                hdnTaskId.Value = e.CommandArgument.ToString();
                LoadTaskData(e.CommandArgument.ToString());
            }
        }
        #endregion

        #region "--Private Methods--"

        /// <summary>
        /// Load filter dropdowns for task
        /// </summary>
        private void LoadFilters()
        {

            DataSet dsFilters = TaskGeneratorBLL.Instance.GetAllUsersNDesignationsForFilter();

            DataTable dtUsers = dsFilters.Tables[0];
            DataTable dtDesignations = dsFilters.Tables[1];

            ddlUsers.DataSource = dtUsers;
            ddlDesignation.DataSource = dtDesignations;

            ddlUsers.DataTextField = "FirstName";
            ddlUsers.DataValueField = "Id";
            ddlUsers.DataBind();

            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "Designation";
            ddlDesignation.DataBind();

            ddlUsers.Items.Insert(0, new ListItem("--Users--", "0"));
            ddlDesignation.Items.Insert(0, new ListItem("--Designation--", "0"));
        }

        /// <summary>
        /// Search tasks with parameters choosen by user.
        /// </summary>
        private void SearchTasks(int? RecordstoPull)
        {

            int? UserID = null;
            string Title = String.Empty, Designation = String.Empty;
            Int16? Status = null;
            DateTime? CreatedOn = null;

            // this is for paging based data fetch, in header view case it will be always page numnber 0 and page size 5
            int Start = 0, PageLimit = 5;

            if (RecordstoPull != null)
            {
                PageLimit = Convert.ToInt32(RecordstoPull);
            }

            PrepareSearchFilerts(ref UserID, ref Title, ref Designation, ref Status, ref CreatedOn);

            DataSet dsFilters = TaskGeneratorBLL.Instance.GetTasksList(UserID, Title, Designation, Status, CreatedOn, Start, PageLimit);

            gvTasks.DataSource = dsFilters;
            gvTasks.DataBind();

        }

        /// <summary>
        /// Prepare search filters choosen by users before performing search
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Title"></param>
        /// <param name="Designation"></param>
        /// <param name="Status"></param>
        /// <param name="CreatedOn"></param>
        private void PrepareSearchFilerts(ref int? UserID, ref string Title, ref string Designation, ref short? Status, ref DateTime? CreatedOn)
        {
            if (ddlUsers.SelectedIndex > 0)
            {
                UserID = Convert.ToInt32(ddlUsers.SelectedItem.Value);
            }

            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                Title = txtSearch.Text;
            }
            if (ddlDesignation.SelectedIndex > 0)
            {
                Designation = ddlDesignation.SelectedItem.Value;
            }

            if (ddlTaskStatus.SelectedIndex > 0)
            {
                Status = Convert.ToInt16(ddlTaskStatus.SelectedItem.Value);
            }

            if (!String.IsNullOrEmpty(txtCreatedDate.Text))
            {
                CreatedOn = Convert.ToDateTime(txtCreatedDate.Text);
            }
        }

        /// <summary>
        /// To load Designation to popup dropdown
        /// </summary>
        private void LoadPopupDropdown()
        {
            DataSet dsdesign = TaskGeneratorBLL.Instance.GetInstallUsers(1, "");
            DataSet ds = TaskGeneratorBLL.Instance.GetTaskUserDetails(1);
            ddlUserDesignation.DataSource = dsdesign;
            ddlUserDesignation.DataTextField = "Designation";
            ddlUserDesignation.DataValueField = "Designation";
            ddlUserDesignation.DataBind();

        }

        /// <summary>
        /// To clear the popup details after save
        /// </summary>
        private void clearAllFormData()
        {
            txtTaskTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlUserDesignation.ClearSelection();
            ddcbAssigned.Items.Clear();
            cmbStatus.ClearSelection();
            ddlUserAcceptance.ClearSelection();
            txtDueDate.Text = string.Empty;
            txtHours.Text = string.Empty;
            gdTaskUsers.DataSource = null;
            gdTaskUsers.DataBind();
            txtLog.Text = string.Empty;
            fuUpload.Dispose();
            hdnTaskId.Value = "0";
            controlMode.Value = "0";
        }

        /// <summary>
        /// Save task master details, user information and user attachments.
        /// Created By: Yogesh Keraliya
        /// </summary>
        private void SaveTask()
        {
            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            Task task = new Task();
            task.TaskId = Convert.ToInt32( hdnTaskId.Value);
            task.Title = txtTaskTitle.Text;
            task.Description = txtDescription.Text;
            task.Status = Convert.ToUInt16(cmbStatus.SelectedItem.Value);
            task.DueDate = txtDueDate.Text;
            task.Hours = Convert.ToInt16(txtHours.Text);
            task.Notes = txtLog.Text;
            task.CreatedBy = userId;
            task.Attachment = null;
            task.Mode = Convert.ToInt32( controlMode.Value);

            string Designame = ddlUserDesignation.SelectedItem.Value;
            Int64 ItaskId = TaskGeneratorBLL.Instance.SaveOrDeleteTask(task);    // save task master details

            // Save task notes and user information, returns TaskUpdateId for reference to add in user attachments.
            Int32 TaskUpdateId = SaveTaskNotes(Designame, ItaskId);

            // Save task related user's attachment.
            UploadUserAttachements(userId, ItaskId, TaskUpdateId);

            clearAllFormData();

            // Refresh task list on top header.
            SearchTasks(null);

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Task created successfully');", true);

           
        }

        private void UploadUserAttachements(int userId, long ItaskId, int taskUpdateId)
        {
            TaskUser taskUserFiles = new TaskUser();
            HttpFileCollection uploads = Request.Files;

            for (int fileCount = 0; fileCount < uploads.Count; fileCount++)
            {
                HttpPostedFile uploadedFile = uploads[fileCount];

                string fileName = Path.GetFileName(uploadedFile.FileName);
                if (uploadedFile.ContentLength > 0)
                {
                    if (!Request.Files.AllKeys[fileCount].Contains("flpUplaodPicture"))
                    {
                        string filename = uploadedFile.FileName;//flpUploadFiles.FileName;
                        filename = DateTime.Now.ToString() + filename;
                        filename = filename.Replace("/", "");
                        filename = filename.Replace(":", "");
                        filename = filename.Replace(" ", "");
                        Server.MapPath("~/Sr_App/UploadedFile/" + filename);
                        uploadedFile.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));

                        //string attach = Session["attachments"] as string;
                        taskUserFiles.Attachment = filename;
                        taskUserFiles.Mode = 0;
                        taskUserFiles.TaskId = ItaskId;
                        taskUserFiles.UserId = userId;
                        taskUserFiles.TaskUpdateId = taskUpdateId;
                        TaskGeneratorBLL.Instance.SaveOrDeleteTaskUserFiles(taskUserFiles);  // save task files
                    }
                }
            }
        }

        /// <summary>
        /// Save task user information.
        /// </summary>
        /// <param name="Designame"></param>
        /// <param name="ItaskId"></param>
        private Int32 SaveTaskNotes(string Designame, long ItaskId)
        {
            Int32 TaskUpdateId = 0;

            for (int i = 0; i < gdTaskUsers.Rows.Count; i++)
            {

                TaskUser taskUser = new TaskUser();
                Label userID = (Label)gdTaskUsers.Rows[i].Cells[1].FindControl("lbluserId");
                Label userType = (Label)gdTaskUsers.Rows[i].Cells[1].FindControl("lbluserType");
                Label notes = (Label)gdTaskUsers.Rows[i].Cells[1].FindControl("lblNotes");
                taskUser.UserId = Convert.ToInt32(userID.Text);
                //taskUser.UserType = userType.Text;
                taskUser.Notes = notes.Text;
                taskUser.TaskId = ItaskId;

                taskUser.Status = Convert.ToInt16(cmbStatus.SelectedItem.Value);
                int userAcceptance = Convert.ToInt32(ddlUserAcceptance.SelectedItem.Value);
                taskUser.UserAcceptance = Convert.ToBoolean(userAcceptance);
                TaskGeneratorBLL.Instance.SaveOrDeleteTaskUser(ref taskUser);

                TaskUpdateId = taskUser.TaskUpdateId;

                //Inform user by email about task assgignment.
//SendEmail(Designame, taskUser.UserId); // send auto email to selected users

            }

            return TaskUpdateId;
        }

        /// <summary>
        /// send Email
        /// </summary>
        /// <param name="userID"></param>
        private void SendEmail(string Designation, int userID)
        {
            try
            {
                DataSet dsUser = TaskGeneratorBLL.Instance.GetInstallUserDetails(Convert.ToInt32(userID));

                string HTML_TAG_PATTERN = "<.*?>";
                DataSet ds = new DataSet(); //AdminBLL.Instance.GetEmailTemplate("Sales Auto Email");// AdminBLL.Instance.FetchContractTemplate(104);

                ds = AdminBLL.Instance.GetEmailTemplate(Designation);

                if (ds == null)
                {
                    if (Designation.Contains("Install"))
                    {
                        ds = AdminBLL.Instance.GetEmailTemplate("Installer - Helper");// AdminBLL.Instance.FetchContractTemplate(104);
                    }
                    else
                    {
                        ds = AdminBLL.Instance.GetEmailTemplate("SubContractor");// AdminBLL.Instance.FetchContractTemplate(104);
                    }
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    if (Designation.Contains("Install"))
                    {
                        ds = AdminBLL.Instance.GetEmailTemplate("Installer - Helper");// AdminBLL.Instance.FetchContractTemplate(104);
                    }
                    else
                    {
                        ds = AdminBLL.Instance.GetEmailTemplate("SubContractor");// AdminBLL.Instance.FetchContractTemplate(104);
                    }
                }

                string FName = dsUser.Tables[0].Rows[0]["FristName"].ToString();
                string emailId = dsUser.Tables[0].Rows[0]["Email"].ToString();
                string LName = dsUser.Tables[0].Rows[0]["LastName"].ToString();
                string fullname = FName + " " + LName;

                string strHeader = ds.Tables[0].Rows[0]["HTMLHeader"].ToString(); //GetEmailHeader(status);
                string strBody = ds.Tables[0].Rows[0]["HTMLBody"].ToString(); //GetEmailBody(status);
                string strFooter = ds.Tables[0].Rows[0]["HTMLFooter"].ToString(); // GetFooter(status);
                string strsubject = ds.Tables[0].Rows[0]["HTMLSubject"].ToString();

                string userName = ConfigurationManager.AppSettings["VendorCategoryUserName"].ToString();
                string password = ConfigurationManager.AppSettings["VendorCategoryPassword"].ToString();




                strBody = strBody.Replace("#Name#", FName).Replace("#name#", FName);
                /*
                 strBody = strBody.Replace("#Date#", dtInterviewDate.Text).Replace("#date#", dtInterviewDate.Text);
                 strBody = strBody.Replace("#Time#", ddlInsteviewtime.SelectedValue).Replace("#time#", ddlInsteviewtime.SelectedValue);
                  */
                strBody = strBody.Replace("#Designation#", Designation).Replace("#designation#", Designation);


                strFooter = strFooter.Replace("#Name#", FName).Replace("#name#", FName);
                /*
                strFooter = strFooter.Replace("#Date#", dtInterviewDate.Text).Replace("#date#", dtInterviewDate.Text);
                strFooter = strFooter.Replace("#Time#", ddlInsteviewtime.SelectedValue).Replace("#time#", ddlInsteviewtime.SelectedValue);
                 */
                strFooter = strFooter.Replace("#Designation#", Designation).Replace("#designation#", Designation);

                strBody = strBody.Replace("Lbl Full name", fullname);
                strBody = strBody.Replace("LBL position", Designation);
                //strBody = strBody.Replace("lbl: start date", txtHireDate.Text);
                //strBody = strBody.Replace("($ rate","$"+ txtHireDate.Text);
                /*
                strBody = strBody.Replace("Reason", Reason);
                */
                //Hi #lblFName#, <br/><br/>You are requested to appear for an interview on #lblDate# - #lblTime#.<br/><br/>Regards,<br/>
                StringBuilder Body = new StringBuilder();
                MailMessage Msg = new MailMessage();
                //Sender e-mail address.
                Msg.From = new MailAddress(userName, "JGrove Construction");
                //ds = AdminBLL.Instance.GetEmailTemplate('');
                Msg.To.Add(emailId);
                Msg.Bcc.Add(new MailAddress("shabbir.kanchwala@straitapps.com", "Shabbir Kanchwala"));
                Msg.CC.Add(new MailAddress("jgrove.georgegrove@gmail.com", "Justin Grove"));

                Msg.Subject = strsubject;// "JG Prospect Notification";
                Body.Append(strHeader);
                Body.Append(strBody);
                Body.Append(strFooter);
                /*
                if (status == "OfferMade")
                {
                    createForeMenForJobAcceptance(Convert.ToString(Body), FName, LName, Designition, emailId, HireDate, EmpType, PayRates);
                }
                if (status == "Deactive")
                {
                    CreateDeactivationAttachment(Convert.ToString(Body), FName, LName, Designition, emailId, HireDate, EmpType, PayRates);
                }
                 */
                Msg.Body = Convert.ToString(Body);
                Msg.IsBodyHtml = true;
                // your remote SMTP server IP.
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    string sourceDir = Server.MapPath(ds.Tables[1].Rows[i]["DocumentPath"].ToString());
                    if (File.Exists(sourceDir))
                    {
                        Attachment attachment = new Attachment(sourceDir);
                        attachment.Name = Path.GetFileName(sourceDir);
                        Msg.Attachments.Add(attachment);
                    }
                }
                SmtpClient sc = new SmtpClient(ConfigurationManager.AppSettings["smtpHost"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].ToString()));


                NetworkCredential ntw = new System.Net.NetworkCredential(userName, password);
                sc.UseDefaultCredentials = false;
                sc.Credentials = ntw;

                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"].ToString()); // runtime encrypt the SMTP communications using SSL
                try
                {
                    sc.Send(Msg);
                }
                catch (Exception ex)
                {
                }

                Msg = null;
                sc.Dispose();
                sc = null;

                //  Page.RegisterStartupScript("UserMsg", "<script>alert('An email notification has sent on " + emailId + ".');}</script>");

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }

        private void LoadTaskData(string TaskId)
        {
            DataSet dsTaskDetails = TaskGeneratorBLL.Instance.GetTaskDetails(Convert.ToInt32(TaskId));
            DataTable dtTaskMasterDetails = dsTaskDetails.Tables[0];
            DataTable dtTaskUserDetails = dsTaskDetails.Tables[1];

            SetMasterTaskDetails(dtTaskMasterDetails);
            SetTaskUserNNotesDetails(dtTaskUserDetails);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "open popup", "EditTask(0);", true);

        }

        private void SetTaskUserNNotesDetails(DataTable dtTaskUserDetails)
        {
            gdTaskUsers.DataSource = dtTaskUserDetails;
            gdTaskUsers.DataBind();

            foreach (ListItem item in ddcbAssigned.Items)
            {


            }
            
        }

        private void SetMasterTaskDetails(DataTable dtTaskMasterDetails)
        {

            txtTaskTitle.Text = dtTaskMasterDetails.Rows[0]["Title"].ToString();
            txtDescription.Text = dtTaskMasterDetails.Rows[0]["Description"].ToString();

            //Get selected index of task status
            ListItem item = ddlTaskStatus.Items.FindByValue(dtTaskMasterDetails.Rows[0]["Status"].ToString());

            if (item != null)
            {
                ddlTaskStatus.SelectedIndex = ddlTaskStatus.Items.IndexOf(item);
            }

            txtDueDate.Text = dtTaskMasterDetails.Rows[0]["DueDate"].ToString();
            txtHours.Text = dtTaskMasterDetails.Rows[0]["Hours"].ToString();
             ddlUserDesignation.SelectedValue = dtTaskMasterDetails.Rows[0]["Designation"].ToString();
            LoadUsersByDesgination();
        }




        #endregion

    }
}