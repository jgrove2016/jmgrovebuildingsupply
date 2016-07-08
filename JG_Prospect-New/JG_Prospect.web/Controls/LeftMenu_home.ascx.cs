using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.Controls
{
    public partial class LeftMenu_home : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkcalendar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void lnkstaticreport_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaticReport.aspx");
        }

        protected void lnkClendarView_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoogleCalendarView.aspx");
        }
    }
}