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

            if (Session["loginid"] != null)
            {
                if ((string)Session["usertype"] == "MM" || (string)Session["usertype"] == "SSE")
                {
                    // li_addresources.Visible = false;
                    li_pricecontrol.Visible = false;
                    li_statusoverride.Visible = true;
                }
                if ((string)Session["usertype"] != "Admin")
                {
                    btnSubmitScript.Visible = false;
                    btnDeleteScript.Visible = false;
                    btnNewScript.Visible = false;
                    ScriptEditor.Enabled = false;
                    ScriptEditor.Attributes.Add("readonly", "readonly");
                }
            }
            else
            {
                Response.Redirect("~/login.aspx");
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