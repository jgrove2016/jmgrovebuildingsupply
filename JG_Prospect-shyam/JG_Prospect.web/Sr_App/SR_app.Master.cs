using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common;

namespace JG_Prospect.Sr_App
{
    public partial class SR_app : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.Form.DefaultButton = searchbutton.UniqueID;
            int timeout = 20 * 1000 * 60;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
            if (Request.QueryString["loginid"] != null)
            {
                Session["loginid"] = Convert.ToString(Request.QueryString["loginid"]);
            }
            if (Session["loginid"] != null)
            {
                ////Original code....
                //if ((string)Session["usertype"] == "MM" || (string)Session["usertype"] == "SSE")
                //{
                //    // li_addresources.Visible = false;
                //    li_pricecontrol.Visible = false;
                //    li_statusoverride.Visible = false;
                //}

                //Code added by Neeta for SR Sales....
                if ((string)Session["usertype"] == "SSE")
                {
                    li_pricecontrol.Visible = false;

                }
                if ((string)Session["usertype"] == "MM")
                {
                    li_pricecontrol.Visible = false;
                    li_statusoverride.Visible = false;
                }             
            }
            else if (Session["loginid"] == null)
            {
                Session["PopUpOnSessionExpire"] = "Expire";
                ScriptManager.RegisterStartupScript(this, GetType(), "alsert", "alert('Your session has expired,login to continue');window.location='../login.aspx;')", true);
            }
        }
      

        protected void searchbutton_Click(object sender, EventArgs e)
        {            
            Response.Redirect("http://www.google.com/search");
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process objP = new System.Diagnostics.Process();
                objP.StartInfo.UseShellExecute = false;
                objP.StartInfo.UserName = "en12";
                objP.StartInfo.FileName = @"D:\FileZilla FTP Client\filezilla.exe";               
                objP.Start();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Process started successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }

    }
}