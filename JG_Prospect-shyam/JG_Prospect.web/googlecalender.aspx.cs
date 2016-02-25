using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using System.Configuration;



namespace JG_Prospect
{
    public partial class googlecalender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                TextBox1.Text = GetCID(Session["UserId"].ToString());
            }
        }

        public string GetCID(string calendarName)
        {
            string id = null;
            try
            {
                string userName = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                string userPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                GoogleCalendar calendar = new GoogleCalendar
                       (calendarName, userName, userPwd);
                id = calendar.GetCalendarId();
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile("stepPage" + ex.Message);
            }
            return id;
        }
    }
}