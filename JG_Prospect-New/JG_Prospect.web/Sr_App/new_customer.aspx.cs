using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;
using System.Web.Script.Serialization;


namespace JG_Prospect.Sr_App
{
    
    public partial class new_customer : System.Web.UI.Page
    {
        private static int UserId = 0;
        private static int ColorFlag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (!IsPostBack)
            {
                bindProducts();
                if (Session["loginid"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have to login first');", true);
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    Session["ButtonCount"] = 0;
                    //txtcall_taken.Text = Session["loginid"].ToString();
                    //txtProjectManager.Text = Session[JG_Prospect.Common.SessionKey.Key.Username.ToString()].ToString();
                    if (Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()] != null)
                    {
                        UserId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                    }
                    hideTouchPointLogDetails();
                }
            }
            ChangeColours();
        }


        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("DefinePeriod.aspx");
        }




        private void bindProducts()
        {
            DataSet ds = UserBLL.Instance.GetAllProducts();
            drpProductOfInterest1.DataSource = ds;
            drpProductOfInterest1.DataTextField = "ProductName";
            drpProductOfInterest1.DataValueField = "ProductId";
            drpProductOfInterest1.DataBind();
            drpProductOfInterest1.Items.Insert(0, new ListItem("Select", "0"));

            drpProductOfInterest2.DataSource = ds;
            drpProductOfInterest2.DataTextField = "ProductName";
            drpProductOfInterest2.DataValueField = "ProductId";
            drpProductOfInterest2.DataBind();
            drpProductOfInterest2.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void hideTouchPointLogDetails()
        {
            //lblIdHeading.Visible = false;
            //lblTPLHeading.Visible = false;
            //tblAddNotes.Visible = false;
            btnAddNotes.Enabled = true;
            DataSet DS = new DataSet();
            DS.Tables.Add("category");
            DS.Tables["category"].Columns.Add("Email");
            DS.Tables["category"].Columns.Add("SoldJobId");
            DS.Tables["category"].Columns.Add("Date");
            DS.Tables["category"].Columns.Add("Status");
            DS.Tables["category"].Rows.Add("", "", "");

            grdTouchPointLog.DataSource = DS.Tables[0];
            grdTouchPointLog.DataBind();

        }
        protected void grdTouchPointLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ColorFlag == JGConstant.ZERO)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    ColorFlag = JGConstant.ONE;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    ColorFlag = JGConstant.ZERO;
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            Boolean bitYesNo;
            if (hdnStatus.Value.ToString() == "1")
                bitYesNo = true;
            else
                bitYesNo = false;
            c.missingcontacts = 0;
            c.ContactType = "";
            c.PhoneType = "";
            c.AddressType = "";
            c.BillingAddressType = "";
            //    string primarycontact = "";
            //if (ddlleadtype.SelectedValue.ToString() == "Other")
            //{
            //    if (txtleadtype.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Lead Type');", true);
            //        return;
            //    }
            //}

            //DataTable dtAddress = new DataTable();
            ////dtAddress = GetAllAddressControls("ContentPlaceHolder1%24ucAddress", Convert.ToInt32(HttpContext.Current.Session["ButtonCount"].ToString()));
            //dtAddress = GetAllAddressControls(formVars);

            //DataTable dtDetails = new DataTable();
            //dtDetails = GetAllOtherDetails(formVars);

            //DataTable dtBillingAddress = new DataTable();
            //dtBillingAddress = GetBillingAddress(formVars);
            //TextBox txtAddress = (TextBox)UCAddress.FindControl("txtAddress");
            //txtAddress.Text

            c.BestTimetocontact = hdnBestTimeToContact.Value;
            c.CompetitorsBids = txtCompetitorBids.Text;
            //c.BestTimetocontact = txtBestDayToContact.Text;
            c.EstTime = txtestimate_time.Text;
            c.EstDate = txtestimate_date.Text;

            if (chbemail.Checked)
            {
                c.ContactPreference = chbemail.Text;
            }
            else if (chbcall.Checked)
            {
                c.ContactPreference = chbcall.Text;
            }

            else if (chbtext.Checked)
            {
                c.ContactPreference = chbtext.Text;
            }
            else if (chbmail.Checked)
            {
                c.ContactPreference = chbmail.Text;
            }

            DataTable dtbleAddress = Session["dtAddress"] as DataTable;
            DataTable dtbleBillingAddress = Session["dtBillingAddress"] as DataTable;
            DataTable dtblePrimary = Session["dtDetails"] as DataTable;
            DataTable dtbleProduct = Session["dtPrimarySecondary"] as DataTable;

            //if (chbemail.Checked && (txtEMail.Text == "" && txtEMail2.Value == "" && txtEMail3.Text == ""))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter at least one Email');", true);
            //    return;
            //}

            //if (txtzip.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter zip code');", true);
            //    return;
            //}

            //if (txtcell_ph.Text == "" || txtcell_ph.Text == null)
            //{
            //    c.missingcontacts++;
            //}
            //if (txtalt_phone.Text == "" || txtalt_phone.Text == null)
            //{
            //    c.missingcontacts++;
            //}
            //if (txthome_phone.Text == "" || txthome_phone.Text == null)
            //{
            //    c.missingcontacts++;
            //}
            //if (c.missingcontacts > 2)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one contact(Cell Phone, Home or Alt. Phone');", true);
            //    return;
            //}

            //if (ddlprimarycontact.SelectedValue == "Cell Phone")
            //{
            //    if (txtcell_ph.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Cell Phone, as it is primary contact');", true);
            //        return;
            //    }
            //    primarycontact = txtcell_ph.Text;
            //}
            //else if (ddlprimarycontact.SelectedValue == "House Phone")
            //{
            //    if (txthome_phone.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter House Phone, as it is primary contact');", true);
            //        return;
            //    }
            //    primarycontact = txthome_phone.Text;
            //}
            //else if (ddlprimarycontact.SelectedValue == "Alt Phone")
            //{
            //    if (txtalt_phone.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Alternate Phone, as it is primary contact');", true);
            //        return;
            //    }
            //    primarycontact = txtalt_phone.Text;
            //}

            //try
            //{
            //    //  int emailid = new_customerBLL.Instance.SearchEmailId(txtEmail.Text.Trim(), Session["loginid"].ToString());
            //  int primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
            //    if (primarycont == 1)
            //    {
            //        c.Isrepeated = false;

            //        c.ContactPreference = string.Empty;
            //        if (chbemail.Checked == true)
            //        {
            //            c.ContactPreference = chbemail.Text;
            //        }
            //        if (chbmail.Checked == true)
            //        {
            //            c.ContactPreference = chbmail.Text;
            //        }

            //        c.firstName = txtfirstname.Text.Trim();
            //        c.lastName = txtlast_name.Text.Trim();
            //        c.customerNm = txtfirstname.Text.Trim() + ' ' + txtlast_name.Text.Trim();
            //        c.CustomerAddress = txtaddress.Text;
            //        c.state = txtstate.Text;
            //        c.City = txtcity.Text;
            //        c.Zipcode = txtzip.Text;
            //        c.BillingAddress = txtbill_address.Text;
            //        c.EstDate = txtestimate_date.Text;

            //        DateTime EstDate = new DateTime();
            //        EstDate = string.IsNullOrEmpty(c.EstDate) ? Convert.ToDateTime("1/1/1753", JGConstant.CULTURE) : Convert.ToDateTime(c.EstDate, JGConstant.CULTURE);

            //        c.EstTime = txtestimate_time.Text;
            //        c.followupdate = "1/1/1753";
            //        // c.followupdate = "";
            //        c.CellPh = txtcell_ph.Text;
            //        c.HousePh = txthome_phone.Text;
            //        c.AltPh = txtalt_phone.Text;
            //        c.Email = txtEmail.Text;
            //        c.Email2 = txtEmail2.Text;
            //        c.Email3 = txtEmail3.Text;
            //        if (txtcall_taken.Text != "")
            //        {
            //            c.CallTakenby = txtcall_taken.Text; // change to id
            //        }
            //        else
            //        {
            //            c.CallTakenby = Session["loginid"].ToString();
            //        }
            //        c.Addedby = txtcall_taken.Text;
            //        //c.Notes = txtService.Text;
            //        c.BestTimetocontact = ddlbesttime.SelectedValue;
            //        if (drpProductOfInterest1.SelectedIndex != 0)
            //        {
            //            c.Productofinterest = Convert.ToInt16(drpProductOfInterest1.SelectedItem.Value); //txtproductofinterest.Text;
            //        }
            //        else
            //        {
            //            c.Productofinterest = 0;
            //        }
            //        c.PrimaryContact = ddlprimarycontact.SelectedValue;
            //        c.ProjectManager = txtProjectManager.Text;
            //        if (drpProductOfInterest2.SelectedIndex != 0)
            //        {
            //            c.SecondaryProductofinterest = Convert.ToInt16(drpProductOfInterest2.SelectedItem.Value);
            //        }
            //        else
            //        {
            //            c.SecondaryProductofinterest = 0;
            //        }

            //        if (ddlleadtype.SelectedValue.ToString() == "Other")
            //        {
            //            c.Leadtype = txtleadtype.Text;
            //        }
            //        else
            //        {
            //            c.Leadtype = ddlleadtype.SelectedValue.ToString();
            //        }
            //        c.Map1 = c.customerNm + "-" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
            //        c.Map2 = c.customerNm + "-" + "Direction" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";

            //        c.status = "Set";


            int res = new_customerBLL.Instance.AddSrCustomer(c, dtbleAddress, dtbleBillingAddress, dtblePrimary, dtbleProduct, bitYesNo);
            if (res > 0)
            {
                //Response.Redirect("/Sr_App/home.aspx");
                Response.Redirect("home.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in adding the Customer');", true);
            }
            //        int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            //        //new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(res.ToString()), DateTime.Parse(c.followupdate), c.status, userId);
            //        if (res > 0)
            //        {
            //            string note = txtAddNotes.Text.Trim();
            //            new_customerBLL.Instance.AddCustomerFollowUp(res, DateTime.Now, note, userId, true, 0);

            //            string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
            //            new_customerBLL.Instance.SaveMapImage(c.Map1, c.CustomerAddress, c.City, c.state, c.Zipcode, DestinationPath);
            //            new_customerBLL.Instance.SaveMapImageDirection(c.Map2, c.CustomerAddress, c.City, c.state, c.Zipcode, DestinationPath);

            //            DateTime datetime;
            //            string t;
            //            TimeSpan time;

            //            t = txtestimate_time.Text;
            //            time = Convert.ToDateTime(t).TimeOfDay;

            //            datetime = Convert.ToDateTime(txtestimate_date.Text, JGConstant.CULTURE) + time;


            //            string gtitle = t + " -" + primarycontact + " -" + Session["loginid"].ToString();
            //            string gcontent = "Name: " + c.customerNm + " , Cell Phone: " + txtcell_ph.Text + ", Alt. phone: " + txtalt_phone.Text + ", Email: " + txtEmail.Text + ",Service: " + txtAddNotes.Text + ",Status: " + "Set";
            //            //string gaddress = txtaddress.Text + " " + txtcity.Text + "," + txtstate.Text + " -" + txtzip.Text;
            //            //string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
            //            //string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
            //            //string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();

            //            //GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, datetime, datetime.AddHours(1), JGConstant.CustomerCalendar);


            //            //gcontent = "Name: " + c.customerNm + " ,Product of Interest: " + c.Productofinterest + ", Phone: " + c.CellPh + ", Alt. phone: " + c.AltPh + ", Email: " + c.Email + ",Notes: " + c.Notes + ",Status: " + c.status;
            //            //gaddress = c.CustomerAddress + " " + c.City + "," + c.state + "-" + c.Zipcode;

            //            //if (Session["AdminUserId"] == null)
            //            //    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), c.CallTakenby);
            //            //GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), AdminId);

            //            List<string> ls = new List<string>();
            //            ls.Add("ennomail.com_mo3kmgflqc9b6mtn6pfd1747pk@group.calendar.google.com");
            //            ls.Add("vikas@ennomail.com");
            //            CalendarEvent1 ce = new CalendarEvent1();
            //            ce.Attendees = ls;
            //            ce.CalendarId = "vikas calendar";
            //            ce.ColorId = 1;
            //            ce.Description = gcontent;
            //            ce.StartDate = String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now);
            //            ce.EndDate = String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now.Add(TimeSpan.FromHours(1)));
            //            ce.Id = "vikas@ennomail.com";
            //            ce.Location = "";
            //            ce.Title = gtitle;

            //            List<string> ids = new List<string>();
            //            ids.Add("vikas calendar");
            //            ids.Add("vikas@ennomail.com");
            //            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //            string sJSON = oSerializer.Serialize(ce);
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "InsertEvent('" + sJSON + "');", true);



            //            ResetFormControlValues(this);
            //            //chbemail.Checked = true;
            //            lblmsg.Visible = true;
            //            lblmsg.CssClass = "success";
            //            lblmsg.Text = "Customer Added successfully";
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in adding the Customer');", true);
            //        }
            //    }
            //    else
            //    {
            //        //btnSubmit.Attributes.Add("onclick", "if(!confirm('Duplicate contact, Do you want to add the another appointment?')) return false;");                       
            //        c.Isrepeated = true;
            //    }

            //}
            //catch
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Try Again');", true);
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFormControlValues(this);
            chbemail.Checked = true;
            lblmsg.Visible = false;
        }

        //protected void ddlleadtype_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlleadtype.SelectedValue.ToString() == "Other")
        //    {
        //        spanother.Visible = true;
        //    }
        //    else
        //    {
        //        spanother.Visible = false;
        //    }

        //}
        private void ResetFormControlValues(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Controls.Count > 0)
                {
                    ResetFormControlValues(c);
                }
                else
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            if (((TextBox)c).ID != "txtcall_taken")
                                ((TextBox)c).Text = "";
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((CheckBox)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.RadioButton":
                            ((RadioButton)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.DropDownList":
                            ((DropDownList)c).SelectedValue = "0";
                            break;
                    }
                }

            }
        }

        //protected void chkbillingaddress_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkbillingaddress.Checked == true)
        //    {
        //        txtbill_address.Text = txtaddress.Text + " " + txtcity.Text + " " + txtstate.Text + " " + txtzip.Text;
        //    }
        //    else
        //        txtbill_address.Text = null;
        //}

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetZipcodes(string prefixText)
        {
            List<string> ZipCodes = new List<string>();
            DataSet dds = new DataSet();
            dds = UserBLL.Instance.fetchzipcode(prefixText);
            foreach (DataRow dr in dds.Tables[0].Rows)
            {
                ZipCodes.Add(dr[0].ToString());
            }
            return ZipCodes.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string CheckForDuplication(List<NameValue> formVars)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string deserializedJson = jsSerializer.Serialize(formVars);

            string strResult = "";

            DataTable dtCusAddress = new DataTable();
            dtCusAddress = GetCustomerAddressControls(formVars);

            DataTable dtAddress = new DataTable();
            //dtAddress = GetAllAddressControls("ContentPlaceHolder1%24ucAddress", Convert.ToInt32(HttpContext.Current.Session["ButtonCount"].ToString()));
            dtAddress = GetAllAddressControls(formVars);

            DataTable dtDetails = new DataTable();
            dtDetails = GetAllOtherDetails(formVars);

            DataTable dtBillingAddress = new DataTable();
            dtBillingAddress = GetBillingAddress(formVars);
            //TextBox txtAddress = (TextBox)UCAddress.FindControl("txtAddress");
            //txtAddress.Text
            DataTable dtPrimarySecondary = new DataTable();
            dtPrimarySecondary = GetPrimarySecondaryControls(formVars);

            HttpContext.Current.Session["dtAddress"] = dtAddress as DataTable;
            HttpContext.Current.Session["dtDetails"] = dtDetails as DataTable;
            HttpContext.Current.Session["dtBillingAddress"] = dtBillingAddress as DataTable;
            HttpContext.Current.Session["dtPrimarySecondary"] = dtPrimarySecondary as DataTable;

            //test
            DataTable dtPrimaryContactId = new DataTable();
            dtPrimaryContactId = GetAllValuesWithId(formVars, "ISPrimaryContact", "chkContactType");

            DataTable dtContactTypeId = new DataTable();
            dtContactTypeId = GetAllValuesWithId(formVars, "ContactType", "selContactType");

            DataTable dtFirstNameId = new DataTable();
            dtFirstNameId = GetAllValuesWithId(formVars, "FirstName", "txtFName");

            DataTable dtLastNameId = new DataTable();
            dtLastNameId = GetAllValuesWithId(formVars, "LastName", "txtLName");

            DataTable dtPhoneNumberId = new DataTable();
            dtPhoneNumberId = GetAllValuesWithId(formVars, "Phone", "txtPhone");

            DataTable dtPhoneTypeId = new DataTable();
            dtPhoneTypeId = GetAllValuesWithId(formVars, "PhoneType", "selPhoneType");

            DataTable dtEMailId = new DataTable();
            dtEMailId = GetAllValuesWithId(formVars, "Mail", "txtEMail");

            string strFName = "", strLName = "", strIsPrimaryContactType = "", strContactType = "", strPhoneNumber = "", strPhoneType = "", strEMail = "";
            for (int i = 0; i <= dtPrimaryContactId.Rows.Count - 1; i++)
            {
                DataRow dRowNew = dtDetails.NewRow();

                if (dtFirstNameId.Rows[i]["RowId"].ToString() == dtPrimaryContactId.Rows[i]["RowId"].ToString())
                {
                    strFName = dtFirstNameId.Rows[i]["FirstName"].ToString();
                    strIsPrimaryContactType = dtPrimaryContactId.Rows[i]["ISPrimaryContact"].ToString();
                }

                if (dtLastNameId.Rows[i]["RowId"].ToString() == dtPrimaryContactId.Rows[i]["RowId"].ToString())
                {
                    strLName = dtLastNameId.Rows[i]["LastName"].ToString();
                }


                if (dtContactTypeId.Rows[i]["RowId"].ToString() == dtPrimaryContactId.Rows[i]["RowId"].ToString())
                {
                    strContactType = dtContactTypeId.Rows[i]["ContactType"].ToString();
                }

                DataView dvPhoneNumberId = dtPhoneNumberId.DefaultView;
                dvPhoneNumberId.RowFilter = "RowId = '" + dtPrimaryContactId.Rows[i]["RowId"].ToString() + "'";

                DataView dvEMailId = dtEMailId.DefaultView;
                dvEMailId.RowFilter = "RowId = '" + dtPrimaryContactId.Rows[i]["RowId"].ToString() + "'";

                DataView dvPhoneTypeId = dtPhoneTypeId.DefaultView;
                dvPhoneTypeId.RowFilter = "RowId = '" + dtPrimaryContactId.Rows[i]["RowId"].ToString() + "'";

                for (int j = 0; j <= dvPhoneNumberId.Count - 1 || j <= dvEMailId.Count - 1; j++)
                {
                    if (j <= dvPhoneNumberId.Count - 1)
                    {
                        if (dtPrimaryContactId.Rows[i]["RowId"].ToString() == dvPhoneNumberId[j]["RowId"].ToString())
                        {
                            strPhoneNumber = dvPhoneNumberId[j]["Phone"].ToString().Replace("-", "");
                            strPhoneType = dvPhoneTypeId[j]["PhoneType"].ToString();
                        }
                        else
                        {
                            strPhoneNumber = "";
                            strPhoneType = "";
                        }
                    }
                    else
                    {
                        strPhoneNumber = "";
                        strPhoneType = "";
                    }

                    if (j <= dvEMailId.Count - 1)
                    {
                        if (dtPrimaryContactId.Rows[i]["RowId"].ToString() == dvEMailId[j]["RowId"].ToString())
                        {
                            strEMail = dvEMailId[j]["Mail"].ToString();
                        }
                        else
                        {
                            strEMail = "";
                        }
                    }
                    else
                    {
                        strEMail = "";
                    }


                    if (j < dvPhoneNumberId.Count || j < dvEMailId.Count)
                    {
                        dRowNew["FirstName"] = strFName;
                        dRowNew["PhoneNumber"] = strPhoneNumber;
                        dRowNew["PhoneType"] = strPhoneType;
                        dRowNew["Email"] = strEMail;
                        dRowNew["LastName"] = strLName;
                        dRowNew["IsPrimaryContact"] = strIsPrimaryContactType;
                        dRowNew["ContactType"] = strContactType;

                        dtDetails.Rows.Add(dRowNew);
                        dRowNew = dtDetails.NewRow();
                    }
                }

            }

            DataTable dtPhoneNumber = new DataTable();
            dtPhoneNumber = GetAllValues(formVars, "Phone", "txtPhone");
            DataView dvPhoneNumber = new DataView();
            dvPhoneNumber = dtPhoneNumber.DefaultView;

            dvPhoneNumber.RowFilter = "Phone <> ''";
            if (dvPhoneNumber.Count == 0)
            {
                strResult = "PhoneNumberEmpty";
                return strResult;
            }
            //foreach (DataRow drow in dtPhoneNumber.Rows)
            //{
            //    if (drow["Phone"] == "")
            //    {
            //        strResult = "PhoneNumberEmpty";
            //        return strResult;
            //    }

            //}
            DataTable dtEMail = new DataTable();
            dtEMail = GetAllValues(formVars, "Mail", "txtEMail");
            //foreach (DataRow drow in dtEMail.Rows)
            //{
            //    if (drow["Mail"] == "")
            //    {

            //        strResult = "EmptyMail";
            //        return strResult;

            //    }
            //}


            DataTable dtFirstName = new DataTable();
            dtFirstName = GetAllValues(formVars, "FirstName", "txtFName");

            foreach (DataRow drow in dtFirstName.Rows)
            {
                if (drow["FirstName"] == "")
                {

                    strResult = "EmptyName";
                    return strResult;
                }
            }

            int CustomerId = 0;
            DataSet dsCustomerDuplication = new_customerBLL.Instance.CheckCustomerDuplication(dtCusAddress, dtDetails, CustomerId);
            if (dsCustomerDuplication != null)
            {
                strResult = dsCustomerDuplication.Tables[0].Rows[0][0].ToString();
                return strResult;
            }
            else
                return strResult;

        }

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            //int index = pnlAddress.Controls.OfType<UserControl.UCAddress>().ToList().Count + 1;

            //if (ViewState["NumControls"] == null || ViewState["NumControls"].ToString() == "")
            //{
            //    ViewState["NumControls"] = myPlaceHolder.Controls.OfType<UserControl.UCAddress>().ToList().Count + 1;
            //}
            //else
            //{
            //    ViewState["NumControls"] = Convert.ToInt32(ViewState["NumControls"]) + 1;
            //}
            //CreateAddressControls("ucAddress" + ViewState["NumControls"].ToString());

            int count = 0;
            if (Session["ButtonCount"] != null)
            {
                myPlaceHolder.Controls.Clear();

                count = (int)Session["ButtonCount"];
                //for (int intCount = 1; intCount <= count; intCount++)
                RecreateControls("ContentPlaceHolder1%24ucAddress", count);
            }

            count++;
            Session["ButtonCount"] = count;
            //for (int i = 0; i < count; i++)
            //{
            CreateAddressControls("ucAddress" + count, count);
            //}   
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            string note = txtAddNotes.Text.Trim();
            int CustomerId = 0;
            new_customerBLL.Instance.AddCustomerFollowUp(CustomerId, DateTime.Now, note, UserId, true, 0);
            txtAddNotes.Text = string.Empty;
            bindGrid();
        }
        protected void bindGrid()
        {

            DataSet ds = new_customerBLL.Instance.GetTouchPointLogData(0, UserId);
            grdTouchPointLog.DataSource = ds;
            grdTouchPointLog.DataBind();
            txtAddNotes.Text = string.Empty;

        }
        private void CreateAddressControls(string strId, int intCount)
        {
            UserControl.UCAddress objUCAddress = LoadControl("~/UserControl/UCAddress.ascx") as UserControl.UCAddress;
            objUCAddress.ID = strId;

            ((Label)objUCAddress.FindControl("lblAddress")).Text = "Address" + intCount.ToString();
            ((Label)objUCAddress.FindControl("lblAddressType")).Text = "Address" + intCount.ToString() + " Type";
            ((Label)objUCAddress.FindControl("lblZip")).Text = "Zip" + intCount.ToString();
            ((Label)objUCAddress.FindControl("lblCity")).Text = "City" + intCount.ToString();
            ((Label)objUCAddress.FindControl("lblState")).Text = "State" + intCount.ToString();

            myPlaceHolder.Controls.Add(objUCAddress);
        }

        private int FindOccurence(string substr)
        {
            string reqstr = Request.Form.ToString();
            return ((reqstr.Length - reqstr.Replace(substr, "").Length) / substr.Length);
        }

        private void ChangeColours()
        {
            foreach (System.Web.UI.WebControls.ListItem item in drpProductOfInterest1.Items)
            {
                System.Web.UI.WebControls.ListItem i = drpProductOfInterest1.Items.FindByText(item.Text);
                if (i.Text == "Custom -Other -*T&M*" || i.Text == "Masonry-Siding " || i.Text == "Masonry -  Flat work & Retaining walls" || i.Text == "Roofing -Metal-Shake-Slate-Terracotta" || i.Text == "Flooring - Hardwood-Laminate-Vinyl" || i.Text == "Flooring - Marble - Porcelain, Ceramic" || i.Text == "Windows & Doors" || i.Text == "Bathrooms" || i.Text == "Kitchens" || i.Text == "Basements" || i.Text == "Additions" || i.Text == "Electric" || i.Text == "Plumbing")
                {
                    i.Attributes.Add("style", "color:red;");
                }
            }

            foreach (System.Web.UI.WebControls.ListItem item in drpProductOfInterest2.Items)
            {
                System.Web.UI.WebControls.ListItem i = drpProductOfInterest2.Items.FindByText(item.Text);
                if (i.Text == "Custom -Other -*T&M*" || i.Text == "Masonry-Siding " || i.Text == "Masonry -  Flat work & Retaining walls" || i.Text == "Roofing -Metal-Shake-Slate-Terracotta" || i.Text == "Flooring - Hardwood-Laminate-Vinyl" || i.Text == "Flooring - Marble - Porcelain, Ceramic" || i.Text == "Windows & Doors" || i.Text == "Bathrooms" || i.Text == "Kitchens" || i.Text == "Basements" || i.Text == "Additions" || i.Text == "Electric" || i.Text == "Plumbing")
                {
                    i.Attributes.Add("style", "color:red;");
                }
            }
        }

        private void RecreateControls(string ctrlPrefix, int cnt)
        {
            string ctrlName = string.Empty;
            string ctrlValue = string.Empty;
            string[] ctrls = Request.Form.ToString().Split('&');
            //int cnt = FindOccurence(ctrlPrefix + "%24txtaddress");
            if (cnt > 0)
            {
                for (int k = 1; k <= cnt; k++)
                {
                    UserControl.UCAddress objUCAddress = LoadControl("~/UserControl/UCAddress.ascx") as UserControl.UCAddress;
                    objUCAddress.ID = "ucAddress" + k;
                    for (int i = 0; i < ctrls.Length; i++)
                    {
                        if (ctrls[i].Contains(ctrlPrefix + k + "%24txtaddress"))
                        {
                            ctrlName = ctrls[i].Split('=')[0];
                            ctrlValue = ctrls[i].Split('=')[1];
                            //Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);
                            ((TextBox)objUCAddress.FindControl("txtaddress")).Text = ctrlValue;
                            ((Label)objUCAddress.FindControl("lblAddress")).Text = "Address" + k.ToString();
                        }
                        else if (ctrls[i].Contains(ctrlPrefix + k + "%24DropDownList1"))
                        {
                            ctrlName = ctrls[i].Split('=')[0];
                            ctrlValue = ctrls[i].Split('=')[1];
                            //Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);
                            int myInt;
                            bool isNumerical = int.TryParse(ctrlValue, out myInt);
                            if (isNumerical)
                                ((DropDownList)objUCAddress.FindControl("DropDownList1")).SelectedIndex = myInt;
                            ((Label)objUCAddress.FindControl("lblAddressType")).Text = "Address" + k.ToString() + " Type";
                        }
                        else if (ctrls[i].Contains(ctrlPrefix + k + "%24txtzip"))
                        {
                            ctrlName = ctrls[i].Split('=')[0];
                            ctrlValue = ctrls[i].Split('=')[1];
                            //Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);
                            ((TextBox)objUCAddress.FindControl("txtzip")).Text = ctrlValue;
                            ((Label)objUCAddress.FindControl("lblZip")).Text = "Zip" + k.ToString();
                        }
                        else if (ctrls[i].Contains(ctrlPrefix + k + "%24txtcity"))
                        {
                            ctrlName = ctrls[i].Split('=')[0];
                            ctrlValue = ctrls[i].Split('=')[1];
                            //Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);
                            ((TextBox)objUCAddress.FindControl("txtcity")).Text = ctrlValue;
                            ((Label)objUCAddress.FindControl("lblCity")).Text = "City" + k.ToString();
                        }
                        else if (ctrls[i].Contains(ctrlPrefix + k + "%24txtstate"))
                        {
                            ctrlName = ctrls[i].Split('=')[0];
                            ctrlValue = ctrls[i].Split('=')[1];
                            //Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);
                            ((TextBox)objUCAddress.FindControl("txtstate")).Text = ctrlValue;
                            ((Label)objUCAddress.FindControl("lblState")).Text = "State" + k.ToString();
                        }
                    }
                    myPlaceHolder.Controls.Add(objUCAddress);
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetCityState(string strZip)
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.fetchcitystate(strZip);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strResult = ds.Tables[0].Rows[0]["City"].ToString() + "@^" + ds.Tables[0].Rows[0]["State"].ToString();
                    return strResult;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
                return string.Empty;

        }

        public class NameValue
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetAllValues(List<NameValue> formVars, string strColumnName, string strControlName)
        {
            DataTable dtPhoneEMail = new DataTable();
            dtPhoneEMail.Columns.Add(strColumnName);

            int intListLength = formVars.Count;

            if (intListLength > 0)
            {
                DataRow dRowNew = dtPhoneEMail.NewRow();
                for (int i = 0; i < intListLength - 1; i++)
                {
                    if (formVars[i].key.Contains(strControlName))
                    {
                        dRowNew[strColumnName] = formVars[i].value;
                        dtPhoneEMail.Rows.Add(dRowNew);
                        dRowNew = dtPhoneEMail.NewRow();
                    }
                }
            }

            //string[] ctrls = HttpContext.Current.Request.Form.ToString().Split('&');

            //DataRow dRowParent = dtPhoneEMail.NewRow();
            //DataRow dRowChild = dtPhoneEMail.NewRow();

            //for (int intMainContacti = 1; intMainContacti < ctrls.Length; intMainContacti++)
            //{
            //    for (int intMainContactj = 1; intMainContactj < ctrls.Length; intMainContactj++)
            //    {
            //        if (ctrls[intMainContacti].Contains(strControlName + intMainContactj))
            //        {
            //            dRowParent[strColumnName] = HttpContext.Current.Server.UrlDecode(ctrls[intMainContacti].Split('=')[1].ToString());
            //            dtPhoneEMail.Rows.Add(dRowParent);
            //            dRowParent = dtPhoneEMail.NewRow();
            //        }

            //        for (int intSubContact = 1; intSubContact < ctrls.Length; intSubContact++)
            //        {
            //            if (ctrls[intMainContacti].Contains(strControlName + intMainContactj + intSubContact))
            //            {
            //                dRowChild[strColumnName] = HttpContext.Current.Server.UrlDecode(ctrls[intMainContacti].Split('=')[1].ToString());
            //                dtPhoneEMail.Rows.Add(dRowChild);
            //                dRowChild = dtPhoneEMail.NewRow();
            //            }
            //        }
            //    }
            //}
            return dtPhoneEMail;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetAllAddressControls(List<NameValue> formVars)
        {
            int intListLength = formVars.Count;
            DataTable dtAddress = new DataTable();
            dtAddress.Columns.Add("Address");
            dtAddress.Columns.Add("Zip");
            dtAddress.Columns.Add("AddressType");
            dtAddress.Columns.Add("City");
            dtAddress.Columns.Add("State");

            if (intListLength > 0)
            {
                DataRow dRowNew = dtAddress.NewRow();
                for (int i = 0; i < intListLength; i++)
                {
                    if (formVars[i].key.Contains("txtaddress"))
                    {
                        dRowNew["Address"] = formVars[i].value;
                    }
                    else if (formVars[i].key.Contains("DropDownList1"))
                    {
                        dRowNew["AddressType"] = formVars[i].value;
                    }
                    else if (formVars[i].key.Contains("txtzip"))
                    {
                        dRowNew["Zip"] = formVars[i].value;

                    }
                    else if (formVars[i].key.Contains("txtcity"))
                    {
                        dRowNew["City"] = formVars[i].value;
                        //dtAddress.Rows.Add(dRowNew);
                        //dRowNew = dtAddress.NewRow();
                    }
                    else if (formVars[i].key.Contains("txtstate"))
                    {
                        dRowNew["State"] = formVars[i].value;
                        dtAddress.Rows.Add(dRowNew);
                        dRowNew = dtAddress.NewRow();
                    }
                }
            }
            return dtAddress;
            //string[] ctrls = HttpContext.Current.Request.Form.ToString().Split('&');
            //if (cnt > 0)
            //{
            //    for (int k = 1; k <= cnt; k++)
            //    {
            //        DataRow dRowNew = dtAddress.NewRow();
            //        for (int i = 0; i < ctrls.Length; i++)
            //        {
            //            if (ctrls[i].Contains(ctrlPrefix + k + "%24txtaddress"))
            //            {
            //                dRowNew["Address"] = HttpContext.Current.Server.UrlDecode(ctrls[i].Split('=')[1].ToString());
            //            }
            //            else if (ctrls[i].Contains(ctrlPrefix + k + "%24txtzip"))
            //            {
            //                dRowNew["Zip"] = HttpContext.Current.Server.UrlDecode(ctrls[i].Split('=')[1].ToString());
            //            }  
            //        }
            //        dtAddress.Rows.Add(dRowNew);
            //    }
            //}
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetCustomerAddressControls(List<NameValue> formVars)
        {
            int intListLength = formVars.Count;
            DataTable dtCustAddress = new DataTable();
            dtCustAddress.Columns.Add("Address");
            dtCustAddress.Columns.Add("Zip");

            if (intListLength > 0)
            {
                DataRow dRowNew = dtCustAddress.NewRow();
                for (int i = 0; i < intListLength; i++)
                {
                    if (formVars[i].key.Contains("txtaddress"))
                    {
                        dRowNew["Address"] = formVars[i].value;
                    }
                    else if (formVars[i].key.Contains("txtzip"))
                    {
                        dRowNew["Zip"] = formVars[i].value;
                        dtCustAddress.Rows.Add(dRowNew);
                        dRowNew = dtCustAddress.NewRow();
                    }
                }
            }
            return dtCustAddress;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetPrimarySecondaryControls(List<NameValue> formVars)
        {
            int intListLength = formVars.Count;
            DataTable dtProduct = new DataTable();
            string PrimaryDEtails = string.Empty;
            string[] PrimaryContents;

            dtProduct.Columns.Add("ProductId");
            dtProduct.Columns.Add("Type");
            dtProduct.Columns.Add("ProductType");


            if (intListLength > 0)
            {
                DataRow dRowNew = dtProduct.NewRow();
                for (int i = 0; i < intListLength; i++)
                {

                    if (formVars[i].key.Contains("hdnPrimaryId"))
                    {
                        dRowNew["ProductId"] = formVars[i].value;
                        //dtProduct.Rows.Add(dRowNew);
                        //dRowNew = dtProduct.NewRow();
                    }
                    else if (formVars[i].key.Contains("primaryRadio"))
                    {
                        dRowNew["Type"] = formVars[i].value;
                        // dtProduct.Rows.Add(dRowNew);
                        //dRowNew = dtProduct.NewRow();
                    }
                    else if (formVars[i].key.Contains("hdnPrimaryType"))
                    {
                        dRowNew["ProductType"] = formVars[i].value;
                        dtProduct.Rows.Add(dRowNew);
                        dRowNew = dtProduct.NewRow();
                    }


                    else if (formVars[i].key.Contains("hdnSecondaryId"))
                    {
                        dRowNew["ProductId"] = formVars[i].value;
                        //dtProduct.Rows.Add(dRowNew);
                        //dRowNew = dtProduct.NewRow();
                    }
                    else if (formVars[i].key.Contains("SecondaryRadio"))
                    {
                        dRowNew["Type"] = formVars[i].value;
                        //dtProduct.Rows.Add(dRowNew);
                        //dRowNew = dtProduct.NewRow();
                    }
                    else if (formVars[i].key.Contains("hdnSecondaryType"))
                    {
                        dRowNew["ProductType"] = formVars[i].value;
                        dtProduct.Rows.Add(dRowNew);
                        dRowNew = dtProduct.NewRow();
                    }
                }

                PrimaryContents = PrimaryDEtails.Split('_');
            }
            return dtProduct;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetAllOtherDetails(List<NameValue> formVars)
        {
            int intListLength = formVars.Count;
            DataTable dtOtherDEtails = new DataTable();
            dtOtherDEtails.Columns.Add("IsPrimaryContact");
            dtOtherDEtails.Columns.Add("ContactType");
            dtOtherDEtails.Columns.Add("FirstName");
            dtOtherDEtails.Columns.Add("LastName");
            dtOtherDEtails.Columns.Add("PhoneType");
            dtOtherDEtails.Columns.Add("Email");
            dtOtherDEtails.Columns.Add("PhoneNumber");


            //if (intListLength > 0)
            //{
            //    DataRow dRowNew = dtOtherDEtails.NewRow();
            //    for (int i = 0; i < intListLength; i++)
            //    {
            //        if (formVars[i].key.Contains("chkContactType"))
            //        {
            //            dRowNew["IsPrimaryContact"] = formVars[i].value;
            //        }
            //        else if (formVars[i].key.Contains("Select"))
            //        {
            //            dRowNew["ContactType"] = formVars[i].value;
            //            // dtOtherDEtails.Rows.Add(dRowNew);
            //            // dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        else if (formVars[i].key.Contains("txtFName"))
            //        {
            //            dRowNew["FirstName"] = formVars[i].value;
            //            // dtOtherDEtails.Rows.Add(dRowNew);
            //            // dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        else if (formVars[i].key.Contains("txtLName"))
            //        {
            //            dRowNew["LastName"] = formVars[i].value;
            //            //dtOtherDEtails.Rows.Add(dRowNew);
            //            //dRowNew = dtOtherDEtails.NewRow();
            //        }

            //        else if (formVars[i].key.Contains("selPhoneType"))
            //        {
            //            dRowNew["PhoneType"] = formVars[i].value;
            //            dtOtherDEtails.Rows.Add(dRowNew);
            //            dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        if (formVars[i].key.Contains("txtEMail"))
            //        {
            //            dRowNew["Email"] = formVars[i].value;
            //            dtOtherDEtails.Rows.Add(dRowNew);
            //            dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        else if (formVars[i].key.Contains("txtPhone"))
            //        {
            //            dRowNew["PhoneNumber"] = formVars[i].value;
            //            //dtOtherDEtails.Rows.Add(dRowNew);
            //            //dRowNew = dtOtherDEtails.NewRow();
            //        }
            //    }
            //}
            return dtOtherDEtails;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetBillingAddress(List<NameValue> formVars)
        {
            int intListLength = formVars.Count;
            DataTable dtBillingAddress = new DataTable();
            dtBillingAddress.Columns.Add("AddressType");
            dtBillingAddress.Columns.Add("BillingAddress");


            if (intListLength > 0)
            {
                DataRow dRowNew = dtBillingAddress.NewRow();
                for (int i = 0; i < intListLength; i++)
                {
                    if (formVars[i].key.Contains("BillAddress"))
                    {
                        dRowNew["BillingAddress"] = formVars[i].value;
                        //dtBillingAddress.Rows.Add(dRowNew);
                        //dRowNew = dtBillingAddress.NewRow();
                    }
                    else if (formVars[i].key.Contains("AddressType"))
                    {
                        dRowNew["AddressType"] = formVars[i].value;
                        dtBillingAddress.Rows.Add(dRowNew);
                        dRowNew = dtBillingAddress.NewRow();
                    }
                }
            }
            return dtBillingAddress;
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        private static DataTable GetAllValuesWithId(List<NameValue> formVars, string strColumnName, string strControlName)
        {
            DataTable dtPhoneEMail = new DataTable();
            dtPhoneEMail.Columns.Add("RowId");
            dtPhoneEMail.Columns.Add(strColumnName);

            int intListLength = formVars.Count;
            int id = 0;

            if (intListLength > 0)
            {
                DataRow dRowNew = dtPhoneEMail.NewRow();
                for (int i = 0; i < intListLength - 1; i++)
                {
                    if (formVars[i].key.Contains("chkContactType"))
                    {
                        id = id + 1;
                    }
                    if (formVars[i].key.Contains(strControlName))
                    {
                        dRowNew["RowId"] = id;
                        dRowNew[strColumnName] = formVars[i].value;
                        dtPhoneEMail.Rows.Add(dRowNew);
                        dRowNew = dtPhoneEMail.NewRow();
                    }
                }
            }
            return dtPhoneEMail;
        }
        //protected void txtcell_ph_TextChanged(object sender, EventArgs e)
        //{
        //    int primarycont = 0;
        //    primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
        //    if (primarycont == 1)
        //    {
        //        hdnisduplicate.Value = "0";
        //        hdnCustId.Value = "0";
        //    }
        //    else
        //    {
        //        hdnisduplicate.Value = "1";
        //        hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txtcell_ph.Text).ToString();
        //    }
        //}

        //protected void txthome_phone_TextChanged(object sender, EventArgs e)
        //{
        //    int primarycont = 0;
        //    primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
        //    if (primarycont == 1)
        //    {
        //        hdnisduplicate.Value = "0";
        //        hdnCustId.Value = "0";
        //    }
        //    else
        //    {
        //        hdnisduplicate.Value = "1";
        //        hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txthome_phone.Text).ToString();
        //    }
        //}

        //protected void txtalt_phone_TextChanged(object sender, EventArgs e)
        //{
        //    int primarycont = 0;
        //    primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
        //    if (primarycont == 1)
        //    {
        //        hdnisduplicate.Value = "0";
        //        hdnCustId.Value = "0";
        //    }
        //    else
        //    {
        //        hdnisduplicate.Value = "1";
        //        hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txtalt_phone.Text).ToString();
        //    }
        //}
    }
    [Serializable]
    public class CalendarEvent1
    {


        public IEnumerable<string> Attendees { get; set; }
        public string CalendarId { get; set; }
        public int ColorId { get; set; }
        public string Description { get; set; }
        public string EndDate { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string Title { get; set; }
        // public IEnumerable<string> CalendarIds { get; set; }
    }

     
}