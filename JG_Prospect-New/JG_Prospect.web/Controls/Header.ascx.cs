using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace JG_Prospect.Controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginid"] != null)
            {
               
                lbluser.Text = Session["Username"].ToString().Trim();
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();

                if ((string)Session["AdminUserId"] == AdminId)
                {
                    Lidashboard.Visible = true;
                    Lihome.Visible = true;
                    Liprogress.Visible = true;
                    Li_sr_app.Visible = true;
                    //Licreateuser.Visible = true;
                    //Liedituser.Visible = true;
                    Lidefineperiod.Visible = true;
                    LiUploadprospect.Visible = true;
                    //Li_AssignProspect.Visible = true;
                }
                else
                {
                    if ((string)Session["usertype"] == "Admin" || (string)Session["usertype"] == "SM")
                    {
                        Lidashboard.Visible = true;
                        Lihome.Visible = true;
                        Liprogress.Visible = true;
                        Li_sr_app.Visible = true;
                    }
                    else if ((string)Session["usertype"] == "MM")
                    {
                        LiUploadprospect.Visible = true;
                        //Li_AssignProspect.Visible = true;
                        Li_sr_app.Visible = true;
                        //Licreateuser.Visible = true;
                        //Liedituser.Visible = true;
                    }
                    else
                    {
                        Lidefineperiod.Visible = false;
                        LiUploadprospect.Visible = false;
                        //Li_AssignProspect.Visible = false;
                    }
                }

            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/login.aspx");            
        }
       
    }
}