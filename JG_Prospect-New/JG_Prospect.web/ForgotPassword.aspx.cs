using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common.Logger;
using JG_Prospect.BLL;
using System.Net.Mail;
using System.Net;

namespace JG_Prospect
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtloginid.Text = "";
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string password = "";
            password = InstallUserBLL.Instance.GetPassword(txtloginid.Text);
            string strEmailId = System.Configuration.ConfigurationManager.AppSettings["ForgotPassEmail"].ToString();
            string strPass = System.Configuration.ConfigurationManager.AppSettings["ForgotPass"].ToString();
            if (password != "")
            {
                string str_Body = "<table><tr><td>Hello,<span style=\"background-color: orange;\">User</span></td></tr><tr><td>your password for the GM Grove Construction is:" + password;
                str_Body = str_Body + "</td></tr>";
                str_Body = str_Body + "<tr><td></td></tr>";
                str_Body = str_Body + "<tr><td>Thanks & Regards.</td></tr>";
                str_Body = str_Body + "<tr><td><span style=\"background-color: orange;\">JM Grove Constructions</span></td></tr></table>";
                //using (MailMessage mm = new MailMessage("support@jmgroveconstruction.com", txtloginid.Text))
                using (MailMessage mm = new MailMessage(strEmailId, txtloginid.Text))
                {
                    mm.Subject = "JM Grove Construction:Forgot Password";
                    mm.Body = str_Body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "mail.jmgroveconstruction.com";
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                   // NetworkCredential NetworkCred = new NetworkCredential(strEmailId, strPass);
                    
                    smtp.UseDefaultCredentials = true;
                   // smtp.Credentials = NetworkCred;
                    smtp.Credentials = new System.Net.NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                    //smtp.Port = 25;
                    smtp.Port = 25;
                    smtp.Send(mm);
                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password send to your registered email id.');window.location ='login.aspx';", true);
                //return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Id does not exists.')", true);
                return;
            }
        }
    }
}