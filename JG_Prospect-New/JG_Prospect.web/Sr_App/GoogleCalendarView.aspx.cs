using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using Microsoft.Office.Interop.Excel;
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

namespace JG_Prospect.Sr_App
{
    public partial class GoogleCalendarView : System.Web.UI.Page
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
            RadWindow2.VisibleOnPageLoad = false;
            if (!IsPostBack)
            {
                //Hide Insert,Edit Delete.....
                rsAppointments.AllowInsert = false;
                rsAppointments.AllowEdit = false;
                rsAppointments.AllowDelete = false;
                

                // BindGoogleMap();
                if (Session["usertype"] != null)
                {
                    usertType = Convert.ToString(Session["usertype"]);
                  
                    if (usertType == Admin)
                    {
                        btnAddEvent.Visible = true;
                        A4.Visible = false;
                        //  Response.Redirect("/home.aspx");
                    }
                    else if (Session["loginid"] != null)
                    {

                    }
                }
                Session["AppType"] = "SrApp";
                BindCalendar();
            }
            
        }

        public void BindCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetAllAnnualEvent();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
            }
            RadWindow2.VisibleOnPageLoad = false;
        }
        public void BindHRCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetHRCalendar();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
            }
            RadWindow2.VisibleOnPageLoad = false;
        }
        public void BindCompanyCalendar()
        {
            if (usertType == Admin)
            {
                DataSet ds = AdminBLL.Instance.GetCompanyCalendar();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rsAppointments.DataSource = ds.Tables[0];
                    rsAppointments.DataBind();
                }
            }
            RadWindow2.VisibleOnPageLoad = false;
        }
        public void BindEventCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetEventCalendar();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
            }
            RadWindow2.VisibleOnPageLoad = false;
        }
        public void BindHRCompanyCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetHRCompanyCalendar();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
            }
            RadWindow2.VisibleOnPageLoad = false;
        }

        public void BindHREventCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetHRCompanyEventCalendar();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
            }
            RadWindow2.VisibleOnPageLoad = false;
        }
        public void BindCompanyEventCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetEventCompanyCalendar();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
            }
            RadWindow2.VisibleOnPageLoad = false;
        }
        public void BindHRCompanyEventCalendar()
        {
            DataSet ds = AdminBLL.Instance.GetHRCompanyEventCalendar();
            if (ds.Tables[0].Rows.Count > 0)
            {
                rsAppointments.DataSource = ds.Tables[0];
                rsAppointments.DataBind();
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
            a.id =Convert.ToInt32(lbtCustomerID.Text);
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
            if (usertType == Admin)
            {
                con = new SqlConnection(strcon);
                int ID = Convert.ToInt32(e.Appointment.ID);
                string Even = e.Appointment.Subject;
                string[] str = Even.Split(' ');
                string strResult = str[0];
                ViewState["ID"] = ID;
                string year = Convert.ToString(System.DateTime.Now.Year);

                if (strResult != "InterViewDetails")
                {
                    lblDesigna.Visible = false;
                    lblApplicant.Visible = false;
                    lblPhone.Visible = false;
                    lblDesigna.Visible = false;
                    lblAdded.Visible = false;
                    lblAplicantfirstName.Visible = false;
                    lblPhoneNo.Visible = false;
                    lblPhoneNo.Visible = false;
                    lblDesignation.Visible = false;
                    lblAddedBy.Visible = false;
                    lbtLastName.Visible = false;
                    Label2.Visible = true;
                    txtEventName.Visible = true;
                    Label2.Visible = true;
                    txtEventName.Visible = true;
                    Label3.Visible = true;
                    txtHolidayDate.Visible = true;
                    btnsave.Visible = true;
                    btnDelete.Visible = true;

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
                else
                {
                    lblAplicantfirstName.Visible = true;
                    lblPhoneNo.Visible = true;
                    lblPhoneNo.Visible = true;
                    lblDesignation.Visible = true;
                    lblAddedBy.Visible = true;
                    lbtLastName.Visible = true;
                    Label2.Visible = false;
                    txtEventName.Visible = false;
                    Label2.Visible = false;
                    txtEventName.Visible = false;
                    Label3.Visible = false;
                    txtHolidayDate.Visible = false;
                    lblDesigna.Visible = true;
                    lblApplicant.Visible = true;
                    lblPhone.Visible = true;
                    lblDesigna.Visible = true;
                    lblAdded.Visible = true;
                    btnsave.Visible = false;
                    btnDelete.Visible = false;

                    int id = Convert.ToInt32(ViewState["ID"]);
                    DataSet ds = AdminBLL.Instance.GetInterviewDetails(id);
                    int applicantId = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        applicantId = Convert.ToInt32(ds.Tables[0].Rows[0]["ApplicantId"]);
                        ViewState["ApplicantId"] = applicantId;
                        lbtCustomerID.Text = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);                      
                        lblAplicantfirstName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FristName"]);
                        lblPhoneNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Phone"]);
                        lblDesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["Designation"]);
                        int a = Convert.ToInt32(ds.Tables[0].Rows[0]["EventAddedBy"]);
                        string query = "select * from tblUsers where id='" + a + "'";
                        da = new SqlDataAdapter(query, con);
                        DataSet dsid = new DataSet();
                        da.Fill(dsid);

                        if (dsid.Tables[0].Rows.Count > 0)
                        {
                            lblAddedBy.Text = Convert.ToString(dsid.Tables[0].Rows[0]["Username"]);
                        }
                        lbtLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                        RadWindow2.VisibleOnPageLoad = true;
                    }
                }
            }
          
           else
           {
               RadWindow2.VisibleOnPageLoad = false;
           }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AnnualEvent a = new AnnualEvent();            
            a.id = Convert.ToInt32(lbtCustomerID.Text);
            new_customerBLL.Instance.DeleteAnnualEvent(a);
            BindCalendar();
           
            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
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
            Response.Redirect("AddEvents.aspx");
        }

        protected void lbtLastName_Click(object sender, EventArgs e)
        {
            Response.Redirect("InstallCreateUser.aspx?ID=" + ViewState["ApplicantId"]);
        }

        protected void chkHR_CheckedChanged(object sender, EventArgs e)
        {

            //For HR Calendar.....
            if (chkHR.Checked == true && chkCompany.Checked == false && chkEvents.Checked == false)
            {
                BindHRCalendar();
            }
            //For Company Calendar.....
            else if (chkCompany.Checked == true && chkHR.Checked == false && chkEvents.Checked == false)
            {
                BindCompanyCalendar();
            }
            //For Event Calendar.....
            else if (chkEvents.Checked == true && chkHR.Checked == false && chkCompany.Checked == false)
            {
                BindEventCalendar();
            }
            //For HR Calendar AND Company Calendar.....
            else if (chkHR.Checked == true && chkCompany.Checked == true && chkEvents.Checked == false)
            {
                BindHRCompanyCalendar();
            }
            //For HR Calendar AND Event Calendar.....
            else if (chkHR.Checked == true && chkEvents.Checked == true && chkCompany.Checked == false)
            {
                BindHREventCalendar();
            }
            //For Company Calendar AND Event Calendar.....
            else if (chkCompany.Checked == true && chkEvents.Checked == true && chkHR.Checked == false)
            {
                BindCompanyEventCalendar();
            }
            //For Company Calendar AND Event Calendar AND HR Calendar.....
            else if (chkHR.Checked == true && chkCompany.Checked == true && chkEvents.Checked == true)
            {
                BindHRCompanyEventCalendar();
            }
            else if (chkHR.Checked == false && chkCompany.Checked == false && chkEvents.Checked == false)
            {
                BindCalendar();
            }
        }

        protected void chkCompany_CheckedChanged(object sender, EventArgs e)
        {

            //For HR Calendar.....
            if (chkHR.Checked == true && chkCompany.Checked == false && chkEvents.Checked == false)
            {
                BindHRCalendar();
            }
            //For Company Calendar.....
            else if (chkCompany.Checked == true && chkHR.Checked == false && chkEvents.Checked == false)
            {
                BindCompanyCalendar();
            }
            //For Event Calendar.....
            else if (chkEvents.Checked == true && chkHR.Checked == false && chkCompany.Checked == false)
            {
                BindEventCalendar();
            }
            //For HR Calendar AND Company Calendar.....
            else if (chkHR.Checked == true && chkCompany.Checked == true && chkEvents.Checked == false)
            {
                BindHRCompanyCalendar();
            }
            //For HR Calendar AND Event Calendar.....
            else if (chkHR.Checked == true && chkEvents.Checked == true && chkCompany.Checked == false)
            {
                BindHREventCalendar();
            }
            //For Company Calendar AND Event Calendar.....
            else if (chkCompany.Checked == true && chkEvents.Checked == true && chkHR.Checked == false)
            {
                BindCompanyEventCalendar();
            }
            //For Company Calendar AND Event Calendar AND HR Calendar.....
            else if (chkHR.Checked == true && chkCompany.Checked == true && chkEvents.Checked == true)
            {
                BindHRCompanyEventCalendar();
            }
            else if (chkHR.Checked == false && chkCompany.Checked == false && chkEvents.Checked == false)
            {
                BindCalendar();
            }
        }

        protected void chkEvents_CheckedChanged(object sender, EventArgs e)
        {

            //For HR Calendar.....
            if (chkHR.Checked == true && chkCompany.Checked == false && chkEvents.Checked == false)
            {
                BindHRCalendar();
            }
            //For Company Calendar.....
            else if (chkCompany.Checked == true && chkHR.Checked == false && chkEvents.Checked == false)
            {
                BindCompanyCalendar();
            }
            //For Event Calendar.....
            else if (chkEvents.Checked == true && chkHR.Checked == false && chkCompany.Checked == false)
            {
                BindEventCalendar();
            }
            //For HR Calendar AND Company Calendar.....
            else if (chkHR.Checked == true && chkCompany.Checked == true && chkEvents.Checked == false)
            {
                BindHRCompanyCalendar();
            }
            //For HR Calendar AND Event Calendar.....
            else if (chkHR.Checked == true && chkEvents.Checked == true && chkCompany.Checked == false)
            {
                BindHREventCalendar();
            }
            //For Company Calendar AND Event Calendar.....
            else if (chkCompany.Checked == true && chkEvents.Checked == true && chkHR.Checked == false)
            {
                BindCompanyEventCalendar();
            }
            //For Company Calendar AND Event Calendar AND HR Calendar.....
            else if (chkHR.Checked == true && chkCompany.Checked == true && chkEvents.Checked == true)
            {
                BindHRCompanyEventCalendar();
            }
            else if (chkHR.Checked == false && chkCompany.Checked == false && chkEvents.Checked == false)
            {
                BindCalendar();
            }
        }

        protected void lbtCustID_Click(object sender, EventArgs e)
        {
            LinkButton CustomerId = (LinkButton)sender;

            SchedulerAppointmentContainer appContainer = (SchedulerAppointmentContainer)CustomerId.Parent;
            Appointment appointment = appContainer.Appointment;
            int i = Convert.ToInt32(appointment.ID);
            DataSet ds = AdminBLL.Instance.GetInterviewDetails(i);
            int applicantId = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                applicantId = Convert.ToInt32(ds.Tables[0].Rows[0]["ApplicantId"]);
                ViewState["ApplicantId"] = applicantId;
            }

            Response.Redirect("InstallCreateUser.aspx?ID=" + ViewState["ApplicantId"]);
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rsAppointments_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            string status = e.Appointment.Attributes["Status"];

            DropDownList ddlStatus = (DropDownList)e.Container.FindControl("ddlStatus");
            if (ddlStatus != null)
            {
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(status.ToString()));
            }
        }
    }
}