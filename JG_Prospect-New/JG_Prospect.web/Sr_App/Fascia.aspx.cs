using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class Fascia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (ddlAdditionalCapping.SelectedIndex > 0 && txtQuantity.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Quantity');", true);
                return;
            }
        }
        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Sr_App/home.aspx");
           
        }
    }
}