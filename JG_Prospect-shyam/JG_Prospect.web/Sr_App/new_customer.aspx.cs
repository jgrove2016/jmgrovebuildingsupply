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

using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;


namespace JG_Prospect.Sr_App
{
    public partial class new_customer : System.Web.UI.Page
    {
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
                    txtcall_taken.Text = Convert.ToString(Session["loginid"]);
                    txtProjectManager.Text = Session[JG_Prospect.Common.SessionKey.Key.Username.ToString()].ToString();
                    hideTouchPointLogDetails();
                }
            }
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
            btnAddNotes.Enabled = false;
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
            c.missingcontacts = 0;
            string primarycontact = "";
            if (ddlleadtype.SelectedValue.ToString() == "Other")
            {
                if (txtleadtype.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Lead Type');", true);
                    return;
                }
            }
            if (chbemail.Checked && (txtEmail.Text == "" && txtEmail2.Text=="" && txtEmail3.Text==""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter at least one Email');", true);
                return;
            }

            if (txtzip.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter zip code');", true);
                return;
            }

            if (txtcell_ph.Text == "" || txtcell_ph.Text == null)
            {
                c.missingcontacts++;
            }
            if (txtalt_phone.Text == "" || txtalt_phone.Text == null)
            {
                c.missingcontacts++;
            }
            if (txthome_phone.Text == "" || txthome_phone.Text == null)
            {
                c.missingcontacts++;
            }
            if (c.missingcontacts > 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one contact(Cell Phone, Home or Alt. Phone');", true);
                return;
            }

            if (ddlprimarycontact.SelectedValue == "Cell Phone")
            {
                if (txtcell_ph.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Cell Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txtcell_ph.Text;
            }
            else if (ddlprimarycontact.SelectedValue == "House Phone")
            {
                if (txthome_phone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter House Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txthome_phone.Text;
            }
            else if (ddlprimarycontact.SelectedValue == "Alt Phone")
            {
                if (txtalt_phone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Alternate Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txtalt_phone.Text;
            }

            try
            {
                //  int emailid = new_customerBLL.Instance.SearchEmailId(txtEmail.Text.Trim(), Session["loginid"].ToString());
                int primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
                if (primarycont == 1)
                {
                    c.Isrepeated = false;

                    c.ContactPreference = string.Empty;
                    if (chbemail.Checked == true)
                    {
                        c.ContactPreference = chbemail.Text;
                    }
                    if (chbmail.Checked == true)
                    {
                        c.ContactPreference = chbmail.Text;
                    }

                    c.firstName = txtfirstname.Text.Trim();
                    c.lastName = txtlast_name.Text.Trim();
                    c.customerNm = txtfirstname.Text.Trim() + ' ' + txtlast_name.Text.Trim();
                    c.CustomerAddress = txtaddress.Text;
                    c.state = txtstate.Text;
                    c.City = txtcity.Text;
                    c.Zipcode = txtzip.Text;
                    c.BillingAddress = txtbill_address.Text;
                    c.EstDate = txtestimate_date.Text;

                    DateTime EstDate = new DateTime();
                    EstDate = string.IsNullOrEmpty(c.EstDate) ? Convert.ToDateTime("1/1/1753",JGConstant.CULTURE) : Convert.ToDateTime(c.EstDate,JGConstant.CULTURE);

                    c.EstTime = txtestimate_time.Text;

                   // DateTime EstTimeDate = Convert.ToDateTime(txtestimate_date.Text + " " + txtestimate_time.Text);
                    c.followupdate = "1/1/1753";
                   // c.followupdate = "";
                    c.CellPh = txtcell_ph.Text;
                    c.HousePh = txthome_phone.Text;
                    c.AltPh = txtalt_phone.Text;
                    c.Email = txtEmail.Text;
                    c.Email2 = txtEmail2.Text;
                    c.Email3 = txtEmail3.Text;
                    if (txtcall_taken.Text != "")
                    {
                        c.CallTakenby = txtcall_taken.Text; // change to id
                    }
                    else
                    {
                        c.CallTakenby = Convert.ToString(Session["loginid"]);
                    }
                    c.Addedby = txtcall_taken.Text;
                    //c.Notes = txtService.Text;
                    c.BestTimetocontact = ddlbesttime.SelectedValue;
                    if (drpProductOfInterest1.SelectedIndex != 0)
                    {
                        c.Productofinterest = Convert.ToInt16(drpProductOfInterest1.SelectedItem.Value); //txtproductofinterest.Text;
                    }
                    else
                    {
                        c.Productofinterest = 0;
                    }
                    c.PrimaryContact = ddlprimarycontact.SelectedValue;
                    c.ProjectManager = txtProjectManager.Text;
                    if (drpProductOfInterest2.SelectedIndex != 0)
                    {
                        c.SecondaryProductofinterest = Convert.ToInt16(drpProductOfInterest2.SelectedItem.Value); 
                    }
                    else
                    {
                        c.SecondaryProductofinterest = 0;
                    }

                    if (ddlleadtype.SelectedValue.ToString() == "Other")
                    {
                        c.Leadtype = txtleadtype.Text;
                    }
                    else
                    {
                        c.Leadtype = ddlleadtype.SelectedValue.ToString();
                    }
                    c.Map1 = c.customerNm + "-" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                    c.Map2 = c.customerNm + "-" + "Direction" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";

                    c.status = "Set";


                    int res = new_customerBLL.Instance.AddCustomer(c);
                    int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                    //new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(res.ToString()), DateTime.Parse(c.followupdate), c.status, userId);
                    if (res > 0)
                    {
                        string note = txtAddNotes.Text.Trim();
                        if (note != "")
                        {
                            new_customerBLL.Instance.AddCustomerFollowUp(res, DateTime.Now, note, userId, true, 0);
                        }

                        string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
                        new_customerBLL.Instance.SaveMapImage(c.Map1, c.CustomerAddress, c.City, c.state, c.Zipcode, DestinationPath);
                        new_customerBLL.Instance.SaveMapImageDirection(c.Map2, c.CustomerAddress, c.City, c.state, c.Zipcode, DestinationPath);

                        DateTime datetime;
                        string t;
                        TimeSpan time;

                        t = txtestimate_time.Text;
                        time = Convert.ToDateTime(t).TimeOfDay;

                        datetime = Convert.ToDateTime(txtestimate_date.Text,JGConstant.CULTURE) + time;


                        string gtitle = t + " -" + primarycontact + " -" + Convert.ToString(Session["loginid"]);
                        string gcontent = "Name: " + c.customerNm + " , Cell Phone: " + txtcell_ph.Text + ", Alt. phone: " + txtalt_phone.Text + ", Email: " + txtEmail.Text + ",Service: " + txtAddNotes.Text + ",Status: " + "Set";
                        string gaddress = txtaddress.Text + " " + txtcity.Text + "," + txtstate.Text + " -" + txtzip.Text;
                        string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                        string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                        string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                        //if (Session["loginid"].ToString() != AdminId || Session["usertype"].ToString() != "Admin")
                        //    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), Session["loginid"].ToString());

                        GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, datetime, datetime.AddHours(1), JGConstant.CustomerCalendar);


                        gcontent = "Name: " + c.customerNm + " ,Product of Interest: " + c.Productofinterest + ", Phone: " + c.CellPh + ", Alt. phone: " + c.AltPh + ", Email: " + c.Email + ",Notes: " + c.Notes + ",Status: " + c.status;
                        gaddress = c.CustomerAddress + " " + c.City + "," + c.state + "-" + c.Zipcode;

                        if (Session["AdminUserId"] == null)
                            GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), c.CallTakenby);
                        GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), res.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), AdminId);

                                              
                        ResetFormControlValues(this);
                        //chbemail.Checked = true;
                        lblmsg.Visible = true;
                        lblmsg.CssClass = "success";
                        lblmsg.Text = "Customer Added successfully";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in adding the Customer');", true);
                    }
                }
                else
                {                    
                    //btnSubmit.Attributes.Add("onclick", "if(!confirm('Duplicate contact, Do you want to add the another appointment?')) return false;");                       
                    c.Isrepeated = true;
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Try Again');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFormControlValues(this);
            chbemail.Checked = true;
            lblmsg.Visible = false;
        }

        protected void ddlleadtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlleadtype.SelectedValue.ToString() == "Other")
            {
                spanother.Visible = true;
            }
            else
            {
                spanother.Visible = false;
            }

        }
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
                            if(((TextBox)c).ID!="txtcall_taken")
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

        protected void chkbillingaddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbillingaddress.Checked == true)
            {
                txtbill_address.Text = txtaddress.Text + " " + txtcity.Text + " " + txtstate.Text + " " + txtzip.Text;
            }
            else
                txtbill_address.Text = null;
        }

        protected void txtzip_TextChanged(object sender, EventArgs e)
        {
            if (txtzip.Text == "")
            {
                //
            }
            else
            {

                DataSet ds = new DataSet();
                ds = UserBLL.Instance.fetchcitystate(txtzip.Text);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        txtstate.Text = ds.Tables[0].Rows[0]["State"].ToString();
                    }
                    else
                    {
                        txtcity.Text = txtstate.Text = "";
                    }
                }
            }

        }

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

        protected void txtcell_ph_TextChanged(object sender, EventArgs e)
        {
            int primarycont = 0;
            primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
            if (primarycont == 1)
            {
                hdnisduplicate.Value = "0";
                hdnCustId.Value = "0";
            }
            else
            {
                hdnisduplicate.Value = "1";
                hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txtcell_ph.Text).ToString();
            }
        }

        protected void txthome_phone_TextChanged(object sender, EventArgs e)
        {
            int primarycont = 0;
            primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
            if (primarycont == 1)
            {
                hdnisduplicate.Value = "0";
                hdnCustId.Value = "0";
            }
            else
            {
                hdnisduplicate.Value = "1";
                hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txthome_phone.Text).ToString();
            }
        }

        protected void txtalt_phone_TextChanged(object sender, EventArgs e)
        {
            int primarycont = 0;
            primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, 0);
            if (primarycont == 1)
            {
                hdnisduplicate.Value = "0";
                hdnCustId.Value = "0";
            }
            else
            {
                hdnisduplicate.Value = "1";
                hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txtalt_phone.Text).ToString();
            }
        }
    }
}