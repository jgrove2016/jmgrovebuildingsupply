using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPSnippets.SmsAPI;
using JG_Prospect.BLL;
namespace JG_Prospect
{
    public partial class ForgotuserId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPhoneNumber.Text = "";
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string Name = "";
            SMS.APIType = SMSGateway.Site2SMS;
           // SMS.MashapeKey = "mfQOlQAbjimsh7kCIgJPiBnMMUcPp1iv5ohjsnx7lbHVFkwP3O";
            SMS.MashapeKey = "<ROidyx1DfKmshTYCQxYLMVQHRpE7p1Dnjv0jsnQFzX3jMaj4X1>";
            SMS.Username = "7040519640";
            SMS.Password = "1688439";
           // SMS.Username = "qat2015team@gmail.com";
          //  SMS.Password = "q$7@wt%j*65ba#3M@9P6";
            Name = InstallUserBLL.Instance.GetUserName(txtPhoneNumber.Text);
            string message = "Your username is " + Name;
            if (Name != "")
            {
                if (txtPhoneNumber.Text.Trim().IndexOf(",") == -1)
                {
                    SMS.SendSms(txtPhoneNumber.Text.Trim(),"message");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username send to you on your registered mobile number.');window.location='login.aspx'", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Phone number does not exists.')", true);
                return;
            }




            //string Name = "";
            //SMS.APIType = SMSGateway.Site2SMS;
            //SMS.MashapeKey = "6WVx1Q0ZTnmsh30j52qJIytfFOBjp1uIZU7jsnPwHVzJFjeGBv";
            //SMS.Username = "7709919312";
            //SMS.Password = "T3623D";
            //Name = InstallUserBLL.Instance.GetUserName(txtPhoneNumber.Text);
            //string message = "Your username is " + Name;
            //if (Name != "")
            //{
            //    if (txtPhoneNumber.Text.Trim().IndexOf(",") == -1)
            //    {
            //        SMS.SendSms(txtPhoneNumber.Text.Trim(), message);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username send to you on your registered mobile number.');window.location='login.aspx'", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Phone number does not exists.')", true);
            //    return;
            //}
        }
    }
}