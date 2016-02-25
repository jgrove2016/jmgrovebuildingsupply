using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using JG_Prospect.BLL;
using Telerik.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace JG_Prospect
{
    public partial class home : System.Web.UI.Page
    {

        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = null;
        static DataSet dsDDL = null, dsAll = null;
        static string usertType = "";
        static int count = 0, rowsCount = 0, c = 0, r = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(strcon);
            DateTime d = System.DateTime.Now;
            int a = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            con.Open();
            string query = "Select * from new_customer Where AssignedToId='" + a + "' AND EstDateSchdule='" + d + "' AND EstTime>='" + System.DateTime.Now.ToLongTimeString() + "'";
            da = new SqlDataAdapter(query, con);

            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string custAddr = Convert.ToString(dt.Rows[0]["CustomerAddress"]);
                txtDestinationId.Value = custAddr;
            }
            if (!IsPostBack)
            {

                usertType = Convert.ToString(Session["usertype"]);
                
                //Hide Insert,Edit Delete.....
                rsAppointments.AllowInsert = false;
                rsAppointments.AllowEdit = false;
                rsAppointments.AllowDelete = false;
                BindCalendar();
                
                BindGoogleMap();
                if (Session["usertype"] != null)
                {
                    if (usertType == "Admin")
                    {
                        //  Response.Redirect("/home.aspx");
                    }

                    else if (Session["loginid"] != null)
                    {

                    }
                }
                Session["AppType"] = "JrApp";

               
            }
       
        }

        public void BindGoogleMap()
        {
            //For Google map  showing markers Set Default Address...
            // txtDefaultAddr.Text = "Swarget";GetJuniorsalesAppointmentsById
            DataSet ds = AdminBLL.Instance.GetTodaysSalesAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));

            if (ds.Tables[0].Rows.Count > 0)
            { //Create Datatble..
                dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] 
                        { 
                                new DataColumn("Title", typeof(string)),
                                new DataColumn("Latitude", typeof(string)),
                                new DataColumn("Longitude",typeof(string)),
                                new DataColumn("Address",typeof(string)) 
                        });
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) //ds.Tables[0].Rows.Count
                {

                    string Addr = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    if(Addr !="")
                    {
                        try
                        {

                            //Code for Finding Latitude And Langitude of Current location.
                            string Lattitude1 = "", Langitude1 = "";
                            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + Addr + "&sensor=false";
                            WebRequest request = WebRequest.Create(url);
                            using (WebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                                {
                                    DataSet dsResult = new DataSet();
                                    dsResult.ReadXml(reader);
                                    DataTable dtCoordinates = new DataTable();
                                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                                        new DataColumn("Address", typeof(string)),
                                        new DataColumn("Latitude",typeof(string)),
                                        new DataColumn("Longitude",typeof(string)) });
                                    DataRow location = null;
                                    string geometry_id = null;
                                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                                    {
                                        geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                                        location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);

                                    }
                                    // cmd.Parameters.AddWithValue("@Latitude", location["lat"].ToString());
                                    // cmd.Parameters.AddWithValue("@Longitude", location["lng"].ToString());
                                    Lattitude1 = Convert.ToString(location["lat"]);//Lattitude
                                    Langitude1 = Convert.ToString(location["lng"]);//langitude

                                }
                            }


                            dt.Rows.Add(Addr, Lattitude1, Langitude1, Addr);

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                }

                }
                rptMarkers.DataSource = dt;
                rptMarkers.DataBind();
            }


        }
        public void BindCalendar()
        {
            //DataSet ds = AdminBLL.Instance.GetJuniorsalesAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        rsAppointments.DataSource = ds.Tables[0];
            //        rsAppointments.DataBind();
            //    }
            //    //CreateDataTableForMap(ds.Tables[0]);
            //}
            //Bind Marker....




            if (usertType == "Admin")
            {
                dsAll = AdminBLL.Instance.GetAllsalesAppointments();
                if (dsAll.Tables[0].Rows.Count > 0)
                {
                    //foreach (DataRow dr in dsAll.Tables[0].Rows)
                    //{
                    //    strDate = Convert.ToString(dr["EventDateTime"]);
                    //}
                    rsAppointments.DataSource = dsAll.Tables[0];
                    rsAppointments.DataBind();
                }
            }
            else
            {
                //dsAll = AdminBLL.Instance.GetsalesAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                //if (dsAll.Tables[0].Rows.Count > 0)
                //{
                //    rsAppointments.DataSource = dsAll.Tables[0];
                //    rsAppointments.DataBind();
                //}

                //All Data displays to all customers.....

                dsAll = AdminBLL.Instance.GetAllsalesAppointments();
                if (dsAll.Tables[0].Rows.Count > 0)
                {
                    //foreach (DataRow dr in dsAll.Tables[0].Rows)
                    //{
                    //    strDate = Convert.ToString(dr["EventDateTime"]);
                    //}
                    rsAppointments.DataSource = dsAll.Tables[0];
                    rsAppointments.DataBind();
                }
            }


        }
        protected void rsAppointments_AppointmentClick(object sender, SchedulerEventArgs e)
        {
            con = new SqlConnection(strcon);
            int ID = Convert.ToInt32(e.Appointment.ID);
            ViewState["ID"] = ID;
            string query = "Select * from new_customer where id='" + ID + "'";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lbtCustomerID.Text = Convert.ToString(dt.Rows[0]["id"]);
                lblName.Text = Convert.ToString(dt.Rows[0]["CustomerName"]);
                lblPhone.Text = Convert.ToString(dt.Rows[0]["CellPh"]);
                lblZip.Text = Convert.ToString(dt.Rows[0]["ZipCode"]);
                lblCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                lblAddress.Text = Convert.ToString(dt.Rows[0]["CustomerAddress"]);
                lblProductLine.Text = Convert.ToString(dt.Rows[0]["ProductOfInterest"]);

                RadWindow2.VisibleOnPageLoad = true;
            }

        }
        protected void lbtCustomerID_Click(object sender, EventArgs e)
        {            
            //Redirect to customer profile page....
            //ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "YetToDeveloped();", true);
            Response.Redirect("Sr_App/Customer_Profile.aspx?CustomerId=" + lbtCustomerID.Text);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(strcon);

            cmd = new SqlCommand("Update new_customer set Status=@Status Where id=@ID", con);

            cmd.Parameters.AddWithValue("@ID", ViewState["ID"]);
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "Save();", true);

            RadWindow2.VisibleOnPageLoad = false;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            RadWindow2.VisibleOnPageLoad = false;
        }

        protected void rsAppointments_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            //  DropDownList StatusDDl = (DropDownList)e.Container.FindControl("ddlstatus1");
            //string Even = e.Appointment.Subject;
            // DropDownList dd = (DropDownList)e.Appointment.AppointmentControls[0].FindControl("ddlstatus1");
            int I = Convert.ToInt32(e.Appointment.ID);
            // string[] EvenSplit = Even.Split(' ');
            //string strResult = EvenSplit[0];
            //int I = Convert.ToInt32(strResult); 

            DataSet ds = AdminBLL.Instance.GetStatusForColorCode(I);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string status = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                    if (status == "est>$1000" || status == "est<$1000")
                    {
                        e.Appointment.BackColor = System.Drawing.Color.Black;
                        e.Appointment.ForeColor = System.Drawing.Color.White;
                        e.Appointment.BorderColor = System.Drawing.Color.Black;
                        e.Appointment.BorderStyle = BorderStyle.Dotted;
                        e.Appointment.BorderWidth = Unit.Pixel(2);
                    }
                    else if (status == "sold>$1000" || status == "sold<$1000")
                    {
                        e.Appointment.BackColor = System.Drawing.Color.Red;
                        e.Appointment.BorderColor = System.Drawing.Color.Red;
                        e.Appointment.BorderStyle = BorderStyle.Dotted;
                        e.Appointment.BorderWidth = Unit.Pixel(2);
                    }
                    else
                    {
                        e.Appointment.BackColor = System.Drawing.Color.Gray;
                        e.Appointment.ForeColor = System.Drawing.Color.White;
                        e.Appointment.BorderColor = System.Drawing.Color.Gray;
                        e.Appointment.BorderStyle = BorderStyle.Dotted;
                        e.Appointment.BorderWidth = Unit.Pixel(2);
                    }
                }
            }


            //if (usertType == Admin)
            //{

            //     if (c <= r)
            //        {
            //            DateTime a = Convert.ToDateTime(dsAll.Tables[0].Rows[c]["EventDateTime"]);
            //        }

            //}
            //else
            //{

            //    if (c <= r)
            //    {
            //        DateTime a = Convert.ToDateTime(dsAll.Tables[0].Rows[c]["EventDateTime"]);
            //    }
            //}
        }

        protected void lbtCustID_Click(object sender, EventArgs e)
        {
            LinkButton CustomerId = (LinkButton)sender;
            Response.Redirect("Sr_App/Customer_Profile.aspx?CustomerId=" + CustomerId.Text);
        }

        protected void ddlstatus1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            SchedulerAppointmentContainer appContainer = (SchedulerAppointmentContainer)ddl1.Parent;
            Appointment appointment = appContainer.Appointment;
            int i = Convert.ToInt32(appointment.ID);
            RadWindow2.VisibleOnPageLoad = false;
            con = new SqlConnection(strcon);

            cmd = new SqlCommand("Update new_customer set Status=@Status Where id=@ID", con);

            cmd.Parameters.AddWithValue("@ID", i);
            cmd.Parameters.AddWithValue("@Status", ddl1.SelectedValue);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            rsAppointments.Rebind();
            //rsAppointments_AppointmentCreated(rsAppointments,AppointmentCreatedEventArgs e);
            // rsAppointments.AppointmentCreated += new EventHandler(rsAppointments_AppointmentCreated);
            ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "Update();", true);

            RadWindow2.VisibleOnPageLoad = false;
        }

        protected void rsAppointments_TimeSlotCreated(object sender, TimeSlotCreatedEventArgs e)
        {
            if (e.TimeSlot.Appointments.Count > 0)
            {
                e.TimeSlot.CssClass = "Disabled";
            }

            ////DropDownList d = (DropDownList)sender;
            //SchedulerAppointmentContainer appContainer = (SchedulerAppointmentContainer)d.Parent;
            // Appointment appointment = appContainer.Appointment;
        }

        protected void rsAppointments_FormCreated(object sender, SchedulerFormCreatedEventArgs e)
        {

            DropDownList StatusDDl = (DropDownList)e.Container.FindControl("ddlE");
            string Admin = "Admin";
            if (usertType == Admin)
            {
                DataSet ds = AdminBLL.Instance.GetAllsalesAppointments();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StatusDDl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                    /*
                    StatusDDl.DataSource = ds.Tables[0];
                    StatusDDl.DataTextField = "Status";
                    StatusDDl.DataValueField = "Status";
                    StatusDDl.DataBind();*/
                }
            }
            else
            {
                DataSet ds = AdminBLL.Instance.GetsalesAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    StatusDDl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                }
            }
        }

        protected void rsAppointments_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            DropDownList d = (DropDownList)e.Container.FindControl("ddlstatus1");
            if (count <= rowsCount)
            {
                string a = Convert.ToString(dsAll.Tables[0].Rows[count]["Status"]);
                if (a == "Set")
                {
                    d.SelectedIndex = 0;
                }
                else if (a == "Prospect")
                {
                    d.SelectedIndex = 1;
                }
                else if (a == "est>$1000")
                {
                    d.SelectedIndex = 2;
                }
                else if (a == "est<$1000")
                {
                    d.SelectedIndex = 3;
                }
                else if (a == "sold>$1000")
                {
                    d.SelectedIndex = 4;
                }
                else if (a == "sold<$1000")
                {
                    d.SelectedIndex = 5;
                }
                else if (a == "Rehash")
                {
                    d.SelectedIndex = 6;
                }
                else if (a == "cancelation-no rehash")
                {
                    d.SelectedIndex = 7;
                }
                else if (a == "Material Confirmation(1)")
                {
                    d.SelectedIndex = 8;
                }
                else if (a == "Procurring Quotes(2)")
                {
                    d.SelectedIndex = 9;
                }
                else if (a == "Ordered(3)")
                {
                    d.SelectedIndex = 10;
                }
                else if (a == "Follow up")
                {
                    d.SelectedIndex = 11;
                }
                else if (a == "Estimate Given")
                {
                    d.SelectedIndex = 12;
                }
                else if (a == "Sold-in Progress")
                {
                    d.SelectedIndex = 13;
                }
                else if (a == "Sold")
                {
                    d.SelectedIndex = 14;
                }
                else if (a == "Assigned")
                {
                    d.SelectedIndex = 15;
                }
                else if (a == "Paid Final")
                {
                    d.SelectedIndex = 16;
                }
                else if (a == "Received “storage location?”")
                {
                    d.SelectedIndex = 17;
                }
                else if (a == "On Standby @ vendor link to vendor profile")
                {
                    d.SelectedIndex = 18;
                }
                else if (a == "Being delivered to job site")
                {
                    d.SelectedIndex = 19;
                }
            }
            count++;
        }

    }
}