using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace JG_Prospect.Sr_App
{
    public partial class ContractFooter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["URL"].ToString();
            img1.ImageUrl = url + "/img/bar3.png";
        }
    }
}