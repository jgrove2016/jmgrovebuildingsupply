using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using JG_Prospect.BLL;
using Telerik.Web;
using Telerik.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using GoogleMaps.LocationServices;

namespace JG_Prospect.Sr_App
{
    public partial class home : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = null;
        static string Admin = "Admin", usertType = "";
        static int count = 0, rowsCount = 0, c = 0, r = 0, statVar = 0;
        static DataSet dsDDL = null, dsAll = null;
        DataSet dsNextAppointment;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFromLocation.Text = "3502 Scotts Ln Philadelphia, PA 19129";
            //Check Session to findout user Type...
            RadWindow2.VisibleOnPageLoad = false;

            if (!IsPostBack)
            {
                usertType = Convert.ToString(Session["usertype"]);
                if (usertType == "Admin")
                {
                    dsAll = AdminBLL.Instance.GetAllsalesAppointments();
                    r = dsAll.Tables[0].Rows.Count;
                    dsNextAppointment = AdminBLL.Instance.GetNextAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                    if (dsNextAppointment.Tables.Count > 0)
                    {
                        if (dsNextAppointment.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsNextAppointment.Tables[0].Rows[0][1]) != "" && Convert.ToString(dsNextAppointment.Tables[0].Rows[0][2]) != "" && Convert.ToString(dsNextAppointment.Tables[0].Rows[0][3]) != "" && Convert.ToString(dsNextAppointment.Tables[0].Rows[0][4]) != "")
                            {
                                var address = Convert.ToString(dsNextAppointment.Tables[0].Rows[0][1]) + " " + Convert.ToString(dsNextAppointment.Tables[0].Rows[0][2]) + " " + Convert.ToString(dsNextAppointment.Tables[0].Rows[0][3] + " " + Convert.ToString(dsNextAppointment.Tables[0].Rows[0][4]));
                                var locationService = new GoogleLocationService();
                                var point = locationService.GetLatLongFromAddress(address);
                                var latitude = point.Latitude;
                                var longitude = point.Longitude;
                                txtToLocation.Text = Convert.ToString(address);
                                lblToDirection.Text = Convert.ToString(dsNextAppointment.Tables[0].Rows[0][1]);
                                txtFromLocation.Text = "3502 Scotts Ln Philadelphia, PA 19129";
                                LocationInfo destinationLocation = new LocationInfo() { Latitude = Convert.ToString(latitude), Longitude = Convert.ToString(longitude) };
                                //Destiation Location - 27.175114, 78.042154 – Taj Mahal, Agra
                                LocationInfo sourceLocation = new LocationInfo() { Latitude = "40.00794", Longitude = "-75.18606" };
                                string jsonSourceResponse = Helper.ToJson(sourceLocation, 100);
                                string jsonDestinationResponse = Helper.ToJson(destinationLocation, 100);
                                string script = "PushLocationData(" + jsonSourceResponse + "," + jsonDestinationResponse + ");";
                                Helper.BindClientScript(script, this);
                            }
                        }
                    }
                }
                else
                {
                    dsAll = AdminBLL.Instance.GetsalesAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                    r = dsAll.Tables[0].Rows.Count;
                    dsNextAppointment = AdminBLL.Instance.GetNextAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                    if (dsNextAppointment.Tables.Count > 0)
                    {
                        if (dsNextAppointment.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsNextAppointment.Tables[0].Rows[0][1]) != "" && Convert.ToString(dsNextAppointment.Tables[0].Rows[0][2]) != "" && Convert.ToString(dsNextAppointment.Tables[0].Rows[0][3]) != "" && Convert.ToString(dsNextAppointment.Tables[0].Rows[0][4]) != "")
                            {
                                var address = Convert.ToString(dsNextAppointment.Tables[0].Rows[0][1]) + " " + Convert.ToString(dsNextAppointment.Tables[0].Rows[0][2]) + " " + Convert.ToString(dsNextAppointment.Tables[0].Rows[0][3] + " " + Convert.ToString(dsNextAppointment.Tables[0].Rows[0][4]));
                                var locationService = new GoogleLocationService();
                                var point = locationService.GetLatLongFromAddress(address);
                                var latitude = point.Latitude;
                                var longitude = point.Longitude;
                                txtToLocation.Text = Convert.ToString(address);
                                lblToDirection.Text = Convert.ToString(dsNextAppointment.Tables[0].Rows[0][1]);
                                txtFromLocation.Text = "3502 Scotts Ln Philadelphia, PA 19129";
                                LocationInfo destinationLocation = new LocationInfo() { Latitude = Convert.ToString(latitude), Longitude = Convert.ToString(longitude) };
                                //Destiation Location - 27.175114, 78.042154 – Taj Mahal, Agra
                                LocationInfo sourceLocation = new LocationInfo() { Latitude = "40.00794", Longitude = "-75.18606" };
                                string jsonSourceResponse = Helper.ToJson(sourceLocation, 100);
                                string jsonDestinationResponse = Helper.ToJson(destinationLocation, 100);
                                string script = "PushLocationData(" + jsonSourceResponse + "," + jsonDestinationResponse + ");";
                                Helper.BindClientScript(script, this);
                            }
                        }
                    }
                }


                //Hide Insert,Edit Delete.....
                rsAppointments.AllowInsert = false;
                rsAppointments.AllowEdit = false;
                rsAppointments.AllowDelete = false;


                BindGoogleMap();
                if (Session["usertype"] != null)
                {
                    if (usertType == Admin)
                    {
                        //Calendar for Senior Sales Executive to add its Annual Events...... 
                       // A4.Visible = false;
                    }
                    else if (Session["loginid"] != null)
                    {

                    }
                }
                Session["AppType"] = "SrApp";
            }
            BindCalendar();
            //ConvertDataTabletoString();
        }

        public string ConvertDataTabletoString()
        {
            DataSet DataSetMarkers = new DataSet();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            DataSetMarkers = AdminBLL.Instance.GetToDaysAppointments(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
            if (DataSetMarkers.Tables.Count > 0)
            {
                DataSetMarkers.Tables[0].Columns.Add("latitude", typeof(string));
                DataSetMarkers.Tables[0].Columns.Add("longitude", typeof(string));
                for (int i = 0; i < DataSetMarkers.Tables[0].Rows.Count; i++)
                {
                    var address = Convert.ToString(DataSetMarkers.Tables[0].Rows[i][0]);
                    var locationService = new GoogleLocationService();
                    var point = locationService.GetLatLongFromAddress(address);
                    if (point != null)
                    {
                        var latitude = point.Latitude;
                        var longitude = point.Longitude;
                        DataSetMarkers.Tables[0].Rows[i][1] = Convert.ToString(latitude);
                        DataSetMarkers.Tables[0].Rows[i][2] = Convert.ToString(longitude);
                    }
                }
                foreach (DataRow dr in DataSetMarkers.Tables[0].Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in DataSetMarkers.Tables[0].Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }
            return serializer.Serialize(rows);
        }


        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            var address = txtToLocation.Text;
            if (address != "")
            {
                var locationService = new GoogleLocationService();
                var point = locationService.GetLatLongFromAddress(address);
                var latitude = point.Latitude;
                var longitude = point.Longitude;
                lblToDirection.Text = Convert.ToString(address);
                LocationInfo sourceLocation = new LocationInfo() { Latitude = "40.00794", Longitude = "-75.18606" };
                //Destiation Location - 27.175114, 78.042154 – Taj Mahal, Agra
                LocationInfo destinationLocation = new LocationInfo() { Latitude = Convert.ToString(latitude), Longitude = Convert.ToString(longitude) };
                string jsonSourceResponse = Helper.ToJson(sourceLocation, 100);
                string jsonDestinationResponse = Helper.ToJson(destinationLocation, 100);
                string script = "PushLocationData(" + jsonSourceResponse + "," + jsonDestinationResponse + ");";
                Helper.BindClientScript(script, this);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "EnterLocation();", true);
            }
        }
        public void BindStatusDDl()
        {/*
            string com = "Select * from UserDetail";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
          * */
        }
        //public void BindGoogleMap()
        //{
        //    //For Google map  showing markers Set Default Address...
        //   // txtDefaultAddr.Text = "Swarget";
        //    DataSet ds = AdminBLL.Instance.GetsalesAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));

        //    if (ds.Tables[0].Rows.Count > 0)
        //    { //Create Datatble..
        //        dt = new DataTable();
        //        dt.Columns.AddRange(new DataColumn[4] 
        //                { 
        //                        new DataColumn("Title", typeof(string)),
        //                        new DataColumn("Latitude", typeof(string)),
        //                        new DataColumn("Longitude",typeof(string)),
        //                        new DataColumn("Address",typeof(string)) 
        //                });
        //        for (int i = 0; i < 10; i++) //ds.Tables[0].Rows.Count
        //        {
        //            string Addr = Convert.ToString(ds.Tables[0].Rows[i]["CustomerAddress"]);

        //           try
        //            {

        //                //Code for Finding Latitude And Langitude of Current location.
        //                string Lattitude1 = "", Langitude1 = "";
        //                string url = "http://maps.google.com/maps/api/geocode/xml?address=" + Addr + "&sensor=false";
        //                WebRequest request = WebRequest.Create(url);
        //                using (WebResponse response = (HttpWebResponse)request.GetResponse())
        //                {
        //                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //                    {
        //                        DataSet dsResult = new DataSet();
        //                        dsResult.ReadXml(reader);
        //                        DataTable dtCoordinates = new DataTable();
        //                        dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
        //                            new DataColumn("Address", typeof(string)),
        //                            new DataColumn("Latitude",typeof(string)),
        //                            new DataColumn("Longitude",typeof(string)) });
        //                        DataRow location = null;
        //                        string geometry_id = null;
        //                        foreach (DataRow row in dsResult.Tables["result"].Rows)
        //                        {
        //                            geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
        //                            location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
        //                            dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);

        //                        }
        //                        // cmd.Parameters.AddWithValue("@Latitude", location["lat"].ToString());
        //                        // cmd.Parameters.AddWithValue("@Longitude", location["lng"].ToString());
        //                        Lattitude1 = Convert.ToString(location["lat"]);//Lattitude
        //                        Langitude1 = Convert.ToString(location["lng"]);//langitude

        //                    }
        //                }


        //                dt.Rows.Add(Addr, Lattitude1, Langitude1, Addr);

        //        }
        //            catch(Exception ex)
        //           {
        //               throw;
        //            }

        //    }
        //        rptMarker.DataSource = dt;
        //        rptMarker.DataBind();
        //    }


        //}
        public void BindGoogleMap()
        {
            //For Google map  showing markers Set Default Address...
            // txtDefaultAddr.Text = "Swarget";GetJuniorsalesAppointmentsById
            try
            {
                DataSet ds = AdminBLL.Instance.GetTodaysSalesAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));

                if (ds.Tables.Count > 0)
                {
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
                            if (Addr != "")
                            {
                                try
                                {

                                    //Code for Finding Latitude And Langitude of Current location.
                                    string Lattitude1 = "", Langitude1 = "";
                                    string url = "http://maps.google.com/maps/api/geocode/xml?address=" + Addr + "&sensor=false";
                                    WebRequest request = WebRequest.Create(url);
                                    DataSet dsResult = new DataSet();

                                    using (WebResponse response = (HttpWebResponse)request.GetResponse())
                                    {
                                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                                        {


                                            dsResult.ReadXml(reader);
                                            DataTable dtCoordinates = new DataTable();
                                            dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                                        new DataColumn("Address", typeof(string)),
                                        new DataColumn("Latitude",typeof(string)),
                                        new DataColumn("Longitude",typeof(string)) });
                                            DataRow location = null;
                                            string geometry_id = null;
                                            DataTableCollection dtCol = dsResult.Tables;
                                            if (dtCol.Contains("result"))
                                            {
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
                                                dt.Rows.Add(Addr, Lattitude1, Langitude1, Addr);
                                            }
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                }
                            }

                        }
                        rptMarkers.DataSource = dt;
                        rptMarkers.DataBind();
                    }
                }
                DataSet dsDefaultAddress = AdminBLL.Instance.GetTodaysSalesAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                //if (dsDefaultAddress.Tables[0].Rows.Count > 0)
                //{
                //    //txtDestinationId.Value = Convert.ToString(dsDefaultAddress.Tables[0].Rows[0]["CustomerAddress"]);
                //}
            }
            catch (Exception ex)
            {
              //  Response.Write(ex.Message);
            }
        }
        public void BindCalendar()
        {
            //string strDate = "";
            if (usertType == Admin)
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
                // dsAll = AdminBLL.Instance.GetsalesAppointmentsById(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()])); //AdminBLL.Instance.GetSrAppointment(Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
                //if (dsAll.Tables[0].Rows.Count > 0)
                //{
                //    rsAppointments.DataSource = dsAll.Tables[0];
                //    rsAppointments.DataBind();
                //}

                dsAll = AdminBLL.Instance.GetAllsalesAppointments();
                if (dsAll.Tables[0].Rows.Count > 0)
                {
                    rsAppointments.DataSource = dsAll.Tables[0];
                    rsAppointments.DataBind();
                }
            }
        }

        public void BindDDL()
        {

        }
        protected void rsAppointments_AppointmentClick(object sender, SchedulerEventArgs e)
        {
            // if (usertType == Admin)
            // {


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
                string test = Convert.ToString(dt.Rows[0]["ProductOfInterest"]);
                if (test != "")
                {
                    int productOfInterest = Convert.ToInt32(dt.Rows[0]["ProductOfInterest"]);

                    DataSet ds = UserBLL.Instance.GetAllProducts();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int p = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"]);
                        if (p == productOfInterest)
                        {
                            lblProductLine.Text = Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]);
                            break;
                        }
                    }
                }


                RadWindow2.VisibleOnPageLoad = true;
            }
            /* }
             else
             {
                 RadWindow2.VisibleOnPageLoad = false;
             }*/
        }

        protected void lbtCustomerID_Click(object sender, EventArgs e)
        {
            //Redirect to customer profile page....
            // ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "YetToDeveloped();", true);
            Response.Redirect("Customer_Profile.aspx?CustomerId=" + lbtCustomerID.Text);
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
            rsAppointments.Rebind();
            ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "UpdateCustomer();", true);

            RadWindow2.VisibleOnPageLoad = false;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            RadWindow2.VisibleOnPageLoad = false;
        }

        protected void rsAppointments_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            //RadComboBox rcbSubject = (RadComboBox)e.container.findControl("ddlstatus1");

            //string Even = e.Appointment.Subject;
            // RadComboBox dd = (RadComboBox)e.Appointment.AppointmentControls[0].FindControl("ddlstatus1");
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
                        //dd.SelectedValue = status;
                        e.Appointment.BackColor = System.Drawing.Color.Black;
                        e.Appointment.ForeColor = System.Drawing.Color.White;
                        e.Appointment.BorderColor = System.Drawing.Color.Black;
                        e.Appointment.BorderStyle = BorderStyle.Dotted;
                        e.Appointment.BorderWidth = Unit.Pixel(2);
                    }
                    else if (status == "sold>$1000" || status == "sold<$1000")
                    {
                        //dd.SelectedValue = status;
                        e.Appointment.BackColor = System.Drawing.Color.Red;
                        e.Appointment.BorderColor = System.Drawing.Color.Red;
                        e.Appointment.BorderStyle = BorderStyle.Dotted;
                        e.Appointment.BorderWidth = Unit.Pixel(2);
                    }
                    else
                    {
                        //dd.SelectedValue = status;
                        e.Appointment.BackColor = System.Drawing.Color.Gray;
                        e.Appointment.ForeColor = System.Drawing.Color.White;
                        e.Appointment.BorderColor = System.Drawing.Color.Gray;
                        e.Appointment.BorderStyle = BorderStyle.Dotted;
                        e.Appointment.BorderWidth = Unit.Pixel(2);
                    }
                }
            }
            DropDownList d= e.Appointment.AppointmentControls as DropDownList;
           // DropDownList StatusDDl = (DropDownList)e.Container.FindControl("ddlE");
           

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
            Response.Redirect("Customer_Profile.aspx?CustomerId=" + CustomerId.Text);
        }

        protected void ddlstatus1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            DropDownList d = (DropDownList)sender;
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
            ScriptManager.RegisterStartupScript(Page, GetType(), "script1", "Update();", true);
            RadWindow2.VisibleOnPageLoad = false;
            rsAppointments.Rebind();



          //  rsAppointments.Rebind();
            //rsAppointments_AppointmentCreated(rsAppointments,AppointmentCreatedEventArgs e);
            // rsAppointments.AppointmentCreated += new EventHandler(rsAppointments_AppointmentCreated);
/*
            //for updation...
            string a = d.SelectedValue;
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

*/
            
            //Find the appointment object to directly interact with it
           

            Appointment appointmentToUpdate = rsAppointments.PrepareToEdit(appointment, rsAppointments.EditingRecurringSeries);
            appointmentToUpdate.Attributes["Completed"] = ddl1.SelectedValue;
            rsAppointments.UpdateAppointment(appointmentToUpdate);

          
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
            //if (statVar == 0)
            //{
                if (count <= dsAll.Tables[0].Rows.Count)
                // if ()
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
                    count++;
               // }
               // statVar++;
            }
            else
            {
                string a = d.SelectedValue;
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
            /*
            if (count >= rowsCount)
           // if ()
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
            count++;*/
        }
    }


    public class LocationInfo
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public static class Helper
    {
        /// <summary>
        /// Generates Json for specific object instance
        /// </summary>
        /// <param name="instance">Instance to be converted to Json </param>
        /// <param name="recursionDepth">Recursion depth optional paramter</param>
        /// <returns>Json for specific object instance</returns>
        public static string ToJson(this object instance, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(instance);
        }

        /// <summary>
        /// Register java script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="control"></param>
        public static void BindClientScript(string script, System.Web.UI.Control control)
        {
            ScriptManager.RegisterStartupScript(control, typeof(Page), Guid.NewGuid().ToString(), script, true);
        }
    }



}