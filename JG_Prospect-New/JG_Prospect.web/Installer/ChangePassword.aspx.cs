using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common.Logger;
using JG_Prospect.BLL;

namespace JG_Prospect.Installer
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        ErrorLog logErr = new ErrorLog();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            
            bool result = false;
            result = InstallUserBLL.Instance.ChangeInstallerPassword(id, txtUser_Password.Text);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Password Changed Successfully!');", true);
                //Response.Redirect("~/Installer/InstallerHome.aspx");
            }
        }
    }
}