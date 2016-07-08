using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common.Logger;

using JG_Prospect.BLL;

namespace JG_Prospect
{
    public partial class changepassword : System.Web.UI.Page
    {
        ErrorLog logErr = new ErrorLog();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
          
                //int loginid = (int)Session["loginid"];
                int id = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
               // string UserType = (string)Session["usertype"];
                bool result = false;
                result = UserBLL.Instance.changepassword(id, txtUser_Password.Text);//, UserType
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Password Changed Successfully!');", true);
                }
           
           

        }
    }
}