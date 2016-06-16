using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using System.Configuration;
using System.Web.Services;

namespace JG_Prospect.Sr_App
{
    public partial class Sold_popup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }     

        [WebMethod]
        public static string IsExists(string value)
        {
            if (value == AdminBLL.Instance.GetAdminCode())
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}