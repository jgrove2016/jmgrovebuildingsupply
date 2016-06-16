using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common;

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

        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session["LogOut"] = 1;
            Response.Redirect("~/login.aspx");   
        }

        protected void lbtWeather_Click(object sender, EventArgs e)
        {


           
            //RadWindow2.VisibleOnPageLoad = true;
           
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
               // return;
        }
    }
}