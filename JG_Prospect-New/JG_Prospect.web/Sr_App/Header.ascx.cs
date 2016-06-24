using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common;
using System.Data;
using JG_Prospect.BLL;

namespace JG_Prospect.Sr_App
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["loginid"] != null)
            {
                lbluser.Text = Session["Username"].ToString();
                if ((string)Session["usertype"] == "SSE")
                {
                    Li_Jr_app.Visible = false;
                }
                if ((string)Session["loginid"] == JGConstant.JUSTIN_LOGIN_ID)
                {
                    // Li_Installer.Visible = true;
                }
                else
                {
                    // Li_Installer.Visible = false;
                }
            }
            else
            {
                Session["PopUpOnSessionExpire"] = "Expire";
                // Response.Redirect("/login.aspx");
                ScriptManager.RegisterStartupScript(this, GetType(), "alsert", "alert('Your session has expired,login to continue');window.location='../login.aspx;')", true);
            }

            if (!IsPostBack)
            {
                BindData();
            }
        
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session["LogOut"] = 1;
            Response.Redirect("~/login.aspx");   
        }

        private void BindData()
        {
            DataSet DS = new DataSet();
          
            DS = InstallUserBLL.Instance.GetAllSalesInstallUsers();          

            Session["UserData"] = DS.Tables[0];
           
            ddlDesignationSearch.DataSource = ddlDesignation.DataSource=(from ptrade in DS.Tables[0].AsEnumerable()
                                         where !string.IsNullOrEmpty(ptrade.Field<string>("Designation"))
                                         select Convert.ToString(ptrade["Designation"])).Distinct().ToList();

            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "--Select--");

            ddlDesignationSearch.DataBind();
            ddlDesignationSearch.Items.Insert(0, "--Select--");
           
        }

        private void BindGrid()
        {
            DataSet dsTaskData = new DataSet();
            dsTaskData = TaskBLL.Instance.GetTaskDetails(0);
            Session["TaskData"] = dsTaskData.Tables[0];

            EnumerableRowCollection<DataRow> query = null;

            DataTable dtTask = new DataTable();
            dtTask= dsTaskData.Tables[0];

            query = (from taskdata in dtTask.AsEnumerable()

                    select taskdata);

            if (query.Count() > 0)
            {
                dtTask = query.Take(4).CopyToDataTable();
            }
            else
                dtTask = null;

            gvTaskList.DataSource = dtTask;
            gvTaskList.DataBind();


        }
        protected void lbtWeather_Click(object sender, EventArgs e)
        {


           
            //RadWindow2.VisibleOnPageLoad = true;
           
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
               // return;
        }

        protected void ddlDesignationSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)(Session["UserData"]);

            ddlAssignedToSearch.DataSource = (from fname in dt.AsEnumerable()
                                       where !string.IsNullOrEmpty(fname.Field<string>("FristName")) && (fname.Field<string>("Designation"))==ddlDesignationSearch.SelectedItem.Text 
                                              orderby fname.Field<string>("FristName") ascending
                                       select Convert.ToString(fname["FristName"])).Distinct().ToList();
            ddlAssignedToSearch.DataBind();
            ddlAssignedToSearch.Items.Insert(0, "--Select--");

            BindGrid();
        }

        protected void ddlAssignedToSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void txtCreatedOn_TextChanged(object sender, EventArgs e)
        {

        }
    }
}