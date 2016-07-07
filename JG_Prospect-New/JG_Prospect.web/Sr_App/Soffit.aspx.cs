using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class Soffit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindcolor();
                greyout();
            }
        }
        protected void bindcolor()
        {
            ddlsoffitcolor.Items.Clear();
            ddlsoffitcolor.Items.Add("Select");
            ddlsoffitcolor.Items.Add("Designer");
            ddlsoffitcolor.Items.Add("Burnt Orange");
            ddlsoffitcolor.Items.Add("Olive Green");
            ddlsoffitcolor.Items.Add("Eggplant");
            ddlsoffitcolor.Items.Add("Annapolis Blue");
            ddlsoffitcolor.Items.Add("Aviator Green");
            ddlsoffitcolor.Items.Add("Richmond Red");
            ddlsoffitcolor.Items.Add("Coffie Bean");
            ddlsoffitcolor.Items.Add("Amber");
            ddlsoffitcolor.Items.Add("Premium");
            ddlsoffitcolor.Items.Add("Sahara Brown");
            ddlsoffitcolor.Items.Add("Arizona Tan");
            ddlsoffitcolor.Items.Add("Khaki Brown");
            ddlsoffitcolor.Items.Add("Rockaway Grey");
            ddlsoffitcolor.Items.Add("Grenadier Green");
            ddlsoffitcolor.Items.Add("Muskoka Green");
            ddlsoffitcolor.Items.Add("Rain Forest");
            ddlsoffitcolor.Items.Add("Spring Moss");
            ddlsoffitcolor.Items.Add("Venetian Gold");
            ddlsoffitcolor.Items.Add("Caribou Brown");

            ddlsoffitcolor.Items.Add("Chestnut Brown");
            ddlsoffitcolor.Items.Add("Standard");
            ddlsoffitcolor.Items.Add("Frost");

            ddlsoffitcolor.Items.Add("Bone");
            ddlsoffitcolor.Items.Add("Ivory");

            ddlsoffitcolor.Items.Add("Prairie Gold");
            ddlsoffitcolor.Items.Add("Cypress");
            ddlsoffitcolor.Items.Add("Stratus");
            ddlsoffitcolor.Items.Add("Flagstone");
            ddlsoffitcolor.Items.Add("Brownstone");
            ddlsoffitcolor.Items.Add("Hearthstone");
            ddlsoffitcolor.Items.Add("Sandalwood");
            ddlsoffitcolor.Items.Add("Sandcastle");
            ddlsoffitcolor.Items.Add("Lite Maple");
            ddlsoffitcolor.Items.Add("Saffron");
            ddlsoffitcolor.Items.Add("Satin Grey");
            ddlsoffitcolor.Items.Add("Sage");

        
        }
        private void greyout()
        {
            foreach (ListItem item in ddlsoffitcolor.Items)
            {
                ListItem i = ddlsoffitcolor.Items.FindByValue(item.Value);
                if (i.Value == "Designer" || i.Value == "Premium" || i.Value == "Standard")
                {
                    i.Attributes.Add("style", "font-weight:bold;");
                    
                    i.Attributes.Add("disabled", "true");
                    i.Value = "-1";
                }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Sr_App/home.aspx");
        }
    }
}