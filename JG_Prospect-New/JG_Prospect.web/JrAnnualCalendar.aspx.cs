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
using Telerik.Web.UI;

namespace JG_Prospect
{
    public partial class JrAnnualCalendar : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        static string query = "";
        static string Admin = "Admin", usertType = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Hide Insert,Edit Delete.....
                rsAppointments.AllowInsert = false;
                rsAppointments.AllowEdit = false;
                rsAppointments.AllowDelete = false;
                BindCalendar();

                // BindGoogleMap();
                if (Session["usertype"] != null)
                {
                    usertType = Convert.ToString(Session["usertype"]);

                    if (usertType == Admin)
                    {
                        btnAddEvent.Visible = true;
                        //  Response.Redirect("/home.aspx");
                    }
                    else if (Session["loginid"] != null)
                    {

                    }
                }
                Session["AppType"] = "SrApp";
            }
        }

        public void BindCalendar()
        {
            DateTime d = Convert.ToDateTime(System.DateTime.Now.ToLongDateString());
            string year = Convert.ToString(System.DateTime.Now.Year);
            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            DataSet ds = AdminBLL.Instance.GetAnnualEventByID(userId, year);            
            
            if(ds != null)
            {             
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rsAppointments.DataSource = ds.Tables[0];
                    rsAppointments.DataBind();
                }
            }

            RadWindow2.VisibleOnPageLoad = false;
        }
        protected void lbtCustomerID_Click(object sender, EventArgs e)
        {
            //Redirect to customer profile page....
            ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "YetToDeveloped();", true);
            // Response.Redirect(lbtCustomerID.Text);
        }
        //Update Annual Event...............
        protected void btnsave_Click(object sender, EventArgs e)
        {
            AnnualEvent a = new AnnualEvent();
            a.EventName = txtEventName.Text;
            a.Eventdate = txtHolidayDate.Text;
            a.id = Convert.ToInt32(lbtCustomerID.Text);
            new_customerBLL.Instance.UpdateAnnualEvent(a);
            BindCalendar();

            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Updated Successfully');", true);
            RadWindow2.VisibleOnPageLoad = false;
            /*
                        try
                        { 
                            //Adding Record to Database through Stored Procedure
                            con = new SqlConnection(strcon);
                            cmd = new SqlCommand("UpdateAnnualEvent", con);
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.AddWithValue("@Eventname", txtEventName.Text);
                            cmd.Parameters.AddWithValue("@EventDate", txtHolidayDate.Text);
                            cmd.Parameters.AddWithValue("@ID", lbtCustomerID.Text);
                
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            BindCalendar();
                            //Clear All Data after submitting.....               
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Updated Successfully');", true);
                            RadWindow2.VisibleOnPageLoad = false;
                        }

                        catch
                        {
                            //return 0;
                            //LogManager.Instance.WriteToFlatFile(ex);
                        }
                       */

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            RadWindow2.VisibleOnPageLoad = false;
        }

        protected void rsAppointments_AppointmentClick(object sender, SchedulerEventArgs e)
        {
            con = new SqlConnection(strcon);
            int ID = Convert.ToInt32(e.Appointment.ID);
            ViewState["ID"] = ID;
            string year = Convert.ToString(System.DateTime.Now.Year);
            //string query = "select * from tbl_AnnualEvents where DATEPART(yyyy,EventDate)='" + year + "'";// where id='" + ID + "'";
            string query = "select * from tbl_AnnualEvents where id='" + ID + "'";
            da = new SqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbtCustomerID.Text = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                txtEventName.Text = Convert.ToString(ds.Tables[0].Rows[0]["EventName"]);
                string EventDate = Convert.ToString(ds.Tables[0].Rows[0]["EventDate"]);
                DateTime dat = Convert.ToDateTime(EventDate);
                txtHolidayDate.Text = dat.ToString("MM/dd/yyyy"); ;
                RadWindow2.VisibleOnPageLoad = true;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AnnualEvent a = new AnnualEvent();
            a.id = Convert.ToInt32(lbtCustomerID.Text);
            new_customerBLL.Instance.DeleteAnnualEvent(a);
            

            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            BindCalendar();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Deleted Successfully');", true);
            RadWindow2.VisibleOnPageLoad = false;
            /*
            try
            {
                //Adding Record to Database through Stored Procedure
                con = new SqlConnection(strcon);
                cmd = new SqlCommand("DeleteAnnualEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", lbtCustomerID.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                BindCalendar();

                //Clear All Data after submitting.....
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Deleted Successfully');", true);
                RadWindow2.VisibleOnPageLoad = false;
            }

            catch
            {
                //return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
           */
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "AddEventPopup();", true);
        }

        protected void btnSaveEvent_Click(object sender, EventArgs e)
        {
            AnnualEvent a = new AnnualEvent();
            a.EventName = txtEventNameSR.Text;
            a.Eventdate = txtEventDate.Text;
            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            a.EventAddedBy = userId;

            new_customerBLL.Instance.AddAnnualEvent(a);
            BindCalendar();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "al", "alert('Event Added Successfully');", true);

        }
    }
}