using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect
{
    public partial class Employment_Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void ddlPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = ddlPositions.SelectedItem.Value;
            
            if (choice == "2" || choice == "3" || choice == "4" || choice == "7")
            {
                Response.Redirect("~/Sr_App/EditInstallUser.aspx", true);
            }
            else if (choice != "1")
            {
                Response.Redirect("~/Sr_App/EditUser.aspx", true);
            }
        }
    }
}