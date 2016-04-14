
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace JG_Prospect.Sr_App
{
    public partial class AddEvents : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clearAllFormData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AnnualEvent a = new AnnualEvent();
            a.EventName = txtEventName.Text;
            a.Eventdate = txtHolidayDate.Text;
            //For checking duplicate event....
            DataSet ds = new DataSet();
            ds=new_customerBLL.Instance.CheckDuplicateAnnualEvent(a);
            if (ds.Tables[0].Rows.Count == 0)
            {
                int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                a.EventAddedBy = userId;
                new_customerBLL.Instance.AddAnnualEvent(a);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Added Successfully');", true);
            }
            else
            {
                //If duplicate Event......
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Name for paricular date is already exist');", true);
            }
           
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            clearAllFormData();
        }

        public void btnadd_Click(object sender, EventArgs e)
        {

        }
        private void clearAllFormData()
        {
            txtEventName.Text = string.Empty;
            txtHolidayDate.Text = string.Empty;
            lblmsg.Visible = false;
        }
    }
}