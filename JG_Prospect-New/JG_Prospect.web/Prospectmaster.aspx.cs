using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using JG_Prospect.Common.modal;

using System.Configuration;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common;

namespace JG_Prospect
{
    public partial class Prospectmaster : System.Web.UI.Page
    {
        private static int UserId = 0;
        private static int ColorFlag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindstatus();
                bindProducts();
                if (Request.QueryString["title"] != null)
                {
                    string[] title = Request.QueryString["title"].Split('-');
                    Session["CustomerId"] = title[0].ToString();

                    update();
                    hdntype.Value = "old";
                    bindGrid();
                }
                else
                {
                    hdntype.Value = "new";
                    hideTouchPointLogDetails();
                }
                if (Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()] != null)
                {
                    UserId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                //string previousPage = Request.UrlReferrer.ToString();
                //string url = ConfigurationManager.AppSettings["URL"].ToString();
                //string pageName=url+ @"/StaticReport.aspx";
                if (Session[SessionKey.Key.PreviousPage.ToString()] !=null)
                {
                    if(Session[SessionKey.Key.PreviousPage.ToString()].ToString() == JGConstant.PAGE_STATIC_REPORT)
                        txtEst_date.Focus();
                }
            }
            greyout();
        }
        DataSet ds = new DataSet();
        public static int Customer_id;
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
        private void hideTouchPointLogDetails()
        {
            //lblIdHeading.Visible = false;
            //lblTPLHeading.Visible = false;
            //tblAddNotes.Visible = false;
            btnAddNotes.Enabled = false;
            DataSet DS=new DataSet();
            DS.Tables.Add("category");
            DS.Tables["category"].Columns.Add("Email");
            DS.Tables["category"].Columns.Add("SoldJobId");
            DS.Tables["category"].Columns.Add("Date");
            DS.Tables["category"].Columns.Add("Status");
            DS.Tables["category"].Rows.Add("", "", "");

            grdTouchPointLog.DataSource = DS.Tables[0];
            grdTouchPointLog.DataBind();
            
        }
        private void bindProducts()
        {
            DataSet ds = UserBLL.Instance.GetAllProducts();
            drpProductOfInterest1.DataSource = ds;
            drpProductOfInterest1.DataTextField = "ProductName";
            drpProductOfInterest1.DataValueField = "ProductId";
            drpProductOfInterest1.DataBind();
            drpProductOfInterest1.Items.Insert(0, new ListItem("Select", "Select"));
            drpProductOfInterest2.DataSource = ds;
            drpProductOfInterest2.DataTextField = "ProductName";
            drpProductOfInterest2.DataValueField = "ProductId";
            drpProductOfInterest2.DataBind();
            drpProductOfInterest2.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void bindstatus()
        {
            ddlstatus.Items.Clear();
            ddlstatus.Items.Add("Select");
            ddlstatus.Items.Add("Set");
            ddlstatus.Items.Add("Prospect");
            //ddlstatus.Items.Add("PTW est");
            ddlstatus.Items.Add("est>$1000");
            ddlstatus.Items.Add("est<$1000");
            //ddlstatus.Items.Add("EST-one legger");
            ddlstatus.Items.Add("sold>$1000");
            ddlstatus.Items.Add("sold<$1000");
            ddlstatus.Items.Add("Closed (not sold)");
            ddlstatus.Items.Add("Closed (sold)");
            ddlstatus.Items.Add("Rehash");
            ddlstatus.Items.Add("cancelation-no rehash");
            ddlstatus.Items.Add("Follow up");

        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue == "Follow up")
            {
                txtfollowupdate.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
                txtEst_date.Text = "";
                txtEst_date.Enabled = false;
                txtestimatetime.Text = "";
                txtestimatetime.Enabled = false;
                txtfollowupdate.ReadOnly = false;
                txtfollowupdate.Enabled = true;
            }
            else
            {
                txtfollowupdate.Text = "";
                txtEst_date.Enabled = true;
                txtfollowupdate.Enabled = false;
                txtfollowupdate.ReadOnly = true;
                txtestimatetime.Enabled = true;
            }
        }
        private void greyout()
        {
            foreach (ListItem item in ddlstatus.Items)
            {
                ListItem i = ddlstatus.Items.FindByText(item.Text);
                if (i.Text == "Closed (not sold)" || i.Text == "est>$1000" || i.Text == "est<$1000" || i.Text == "Closed (sold)" || i.Text == "sold>$1000" || i.Text == "sold<$1000")
                {
                    i.Attributes.Add("style", "color:gray;");
                    i.Attributes.Add("disabled", "true");
                    i.Value = "-1";
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

        //Save Praspect .......
        protected void Save_Click(object sender, EventArgs e)
        {
            int result = 0;
            string primarycontact = "";
            Customer objcust = new Customer();
            objcust.missingcontacts = 0;
            if ((txtEst_date.Text == "" || txtestimatetime.Text == "") && (txtfollowupdate.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one Estimate Date & time or follow up date');", true);
                return;
            }
            if ((txtEst_date.Text != "" || txtestimatetime.Text != "") && (txtfollowupdate.Text != ""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill only one : either Estimate Date & Time or Follow Up Date');", true);
                return;
            }
            if ((txtfollowupdate.Text == "") && (ddlstatus.SelectedValue == "Follow up"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill follow up date');", true);
                return;
            }
            if (txtzip.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter zip code');", true);
                return;
            }

            if (chbemail.Checked && (txtemail.Text == "" && txtEmail2.Text=="" && txtEmail3.Text==""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter at least one Email');", true);
                return;
            }

            if (txtcellphone.Text == "" || txtcellphone.Text == null)
            {
                objcust.missingcontacts++;
            }
            if (txt_altphone.Text == "" || txt_altphone.Text == null)
            {
                objcust.missingcontacts++;
            }
            if (txt_homephone.Text == "" || txt_homephone.Text == null)
            {
                objcust.missingcontacts++;
            }
            if (objcust.missingcontacts > 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one contact(Cell Phone, Home Phone or Alt. Phone');", true);
                return;
            }
            if (ddlprimarycontact.SelectedValue == "Cell Phone")
            {
                if (txtcellphone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Cell Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txtcellphone.Text;
            }
            else if (ddlprimarycontact.SelectedValue == "House Phone")
            {
                if (txt_homephone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter House Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txt_homephone.Text;
            }
            else if (ddlprimarycontact.SelectedValue == "Alt Phone")
            {
                if (txt_altphone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Alternate Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txt_altphone.Text;
            }

            int primarycont = new_customerBLL.Instance.Searchprimarycontact(txt_homephone.Text, txtcellphone.Text, txt_altphone.Text, 0);
            if (primarycont == 1)
            {
                objcust.Isrepeated = false;

                if (chbemail.Checked == true)
                {
                    objcust.ContactPreference = chbemail.Text;
                }
                if (chbmail.Checked == true)
                {
                    objcust.ContactPreference = chbmail.Text;
                }

                objcust.Addedby = Convert.ToString(Session["loginid"]);
                objcust.CallTakenby = Convert.ToString(Session["loginid"]);
                objcust.Leadtype = Convert.ToString(Session["loginid"]);
                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                objcust.EstDate = string.IsNullOrEmpty(txtEst_date.Text) ? "1/1/1753" : Convert.ToDateTime(txtEst_date.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
               // objcust.EstDate =  txtEst_date.Text;
                objcust.EstTime = txtestimatetime.Text;

                objcust.firstName = txt_fname.Text.Trim();
                objcust.lastName = txtlastname.Text.Trim();
                objcust.customerNm = txt_fname.Text.Trim() + ' ' + txtlastname.Text.Trim();
                objcust.CellPh = txtcellphone.Text;
                objcust.AltPh = txt_altphone.Text;
                objcust.HousePh = txt_homephone.Text;
                objcust.Email = txtemail.Text;
                objcust.Email2 = txtEmail2.Text;
                objcust.Email3 = txtEmail3.Text;
                objcust.CustomerAddress = txtaddress.Text;
                objcust.City = txtcity.Text;
                objcust.state = txtstate.Text;
                objcust.Zipcode = txtzip.Text;
                objcust.followupdate = string.IsNullOrEmpty(txtfollowupdate.Text) ? "1/1/1753" : Convert.ToDateTime(txtfollowupdate.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
                //objcust.followupdate =  txtfollowupdate.Text;
                if (drpProductOfInterest1.SelectedIndex != 0)
                {
                    objcust.Productofinterest = Convert.ToInt16(drpProductOfInterest1.SelectedItem.Value); //txtproductofinterest.Text;
                }
                else
                {
                    objcust.Productofinterest = 0;
                }
                if (drpProductOfInterest2.SelectedIndex != 0)
                {
                    objcust.SecondaryProductofinterest = Convert.ToInt16(drpProductOfInterest2.SelectedItem.Value); //txtproductofinterest.Text;
                }
                else
                {
                    objcust.SecondaryProductofinterest = 0;
                }
                objcust.BestTimetocontact = ddlbesttimetocontact.SelectedValue.ToString();
               // objcust.Notes = txtnotes.Text;
                objcust.BillingAddress = txtbillingaddress.Text;
                objcust.PrimaryContact = ddlprimarycontact.SelectedValue;

                //if (txtEst_date.Text != "" && txtestimatetime.Text != "" && txtfollowupdate.Text != "")
                //{
                //    objcust.status = ddlstatus.SelectedItem.Text;
                //}
                //else 
                if (txtEst_date.Text != "" && txtestimatetime.Text != "")
                {
                    objcust.status = "Set";
                }
                else if (txtfollowupdate.Text != "" && txtfollowupdate.Text != "1/1/1753")
                {
                    objcust.status = "Follow up";
                }
                else
                {
                    objcust.status = ddlstatus.SelectedItem.Text;
                }


                objcust.Map1 = objcust.customerNm + "-" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                objcust.Map2 = objcust.customerNm + "-" + "Direction" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
                new_customerBLL.Instance.SaveMapImage(objcust.Map1, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode, DestinationPath);
                new_customerBLL.Instance.SaveMapImageDirection(objcust.Map2, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode, DestinationPath);


                result = new_customerBLL.Instance.AddCustomer(objcust);
                
                if (result > 0)
                {
                    string note = txtAddNotes.Text.Trim();
                    if (note != "")
                    {
                        new_customerBLL.Instance.AddCustomerFollowUp(result, DateTime.Now, note, UserId, true, 0,"");
                    }

                    string date = Convert.ToDateTime(objcust.EstDate).ToShortDateString();
                    string datetime = "1/1/1753";

                    if (date != "1/1/1753")
                    {
                        datetime = Convert.ToDateTime(date + " " + objcust.EstTime).ToString("MM/dd/yy hh:mm tt");
                    }
                    //else
                    //{
                    //    datetime = Convert.ToDateTime(objcust.followupdate).ToString("MM/dd/yy hh:mm tt");
                    //}
                    string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                    string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                    string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                    if (objcust.status == "Set")
                    {

                        string gtitle = txtestimatetime.Text + " -" + primarycontact + " -" + objcust.CallTakenby;
                        string gcontent = "Name: " + objcust.customerNm + " ,Product of Interest: " + objcust.Productofinterest + ", Phone: " + objcust.CellPh + ", Alt. phone: " + objcust.AltPh + ", Email: " + objcust.Email + ",Notes: " + objcust.Notes + ",Status: " + objcust.status;
                        string gaddress = objcust.CustomerAddress + " " + objcust.City + "," + objcust.state + "," + objcust.Zipcode;

                       if (AdminId != Convert.ToString(Session["loginid"]))
                           GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), Convert.ToString(Session["loginid"]));
                       GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);


                        if (objcust.status == "Set")
                        {
                            gtitle = objcust.EstTime + " -" + primarycontact + " -" + Convert.ToString(Session["loginid"]);
                            gcontent = "Name: " + objcust.customerNm + " , Cell Phone: " + objcust.CellPh + ", Alt. phone: " + objcust.AltPh + ", Email: " + objcust.Email + ",Service: " + objcust.Notes + ",Status: " + objcust.status;
                            gaddress = txtaddress.Text + " " + txtcity.Text + "," + txtstate.Text + " -" + txtzip.Text;
                            GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);
                        }
                    }

                    ResetFormControlValues(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prospect Added successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in adding the Prospect');", true);
                }
            }
            else
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Duplicate contact cannot add the Prospect');", true);
                objcust.Isrepeated = true;
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            ResetFormControlValues(this);
            // trfollowup.Visible = true;
            lblmsg.Visible = false;
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
                            ((TextBox)c).Text = "";
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((CheckBox)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.RadioButton":
                            ((RadioButton)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.DropDownList":
                            ((DropDownList)c).SelectedValue = "Select";
                            break;
                    }
                }
            }
            txtestimatetime.Text = "";
            lblId.Text = "";
        }

        protected void update()
        {
            btnsave.Visible = false;
            btnupdate.Visible = true;

            string title = Request.QueryString["title"].ToString();
            string[] titles = title.Split('-');

            try
            {
                Customer_id = Convert.ToInt32(titles[0]);
                ds = new DataSet();

                ds = UserBLL.Instance.getprospectdetails(Customer_id);
                if (ds != null)
                {
                    lblId.Text = Session["CustomerId"].ToString();
                    ViewState["userid"] = ds.Tables[0].Rows[0]["Login_Id"].ToString();
                    if (ds.Tables[0].Rows[0]["EstDateSchdule"].ToString() != "")
                    {
                        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["EstDateSchdule"]).ToShortDateString() != "1/1/1753")
                        {
                            txtEst_date.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EstDateSchdule"]).ToString("MM/dd/yyyy");
                            ViewState["PreviousEstimateDate"] = txtEst_date.Text;
                        }
                        else
                        {
                            txtEst_date.Text = null;
                            ViewState["PreviousEstimateDate"] = "1/1/1753";
                        }
                    }
                    else
                    {
                        ViewState["PreviousEstimateDate"] = "1/1/1753";
                    }

                    
                    string[] citystatezip = ds.Tables[0].Rows[0]["city_state_zip"].ToString().Split(',');
                    //Match zip = Regex.Match(citystatezip, @"(\d+)");
                    //string zp = zip.ToString();                   
                    //string city = ds.Tables[0].Rows[0]["city_state_zip"].ToString().Split(',').First<string>();
                    txtestimatetime.Text = ds.Tables[0].Rows[0]["EstTime"].ToString();
                    if (txtestimatetime.Text != "")
                        ViewState["PreviousEstimateTime"] = txtestimatetime.Text;
                    else
                        ViewState["PreviousEstimateTime"] = 0;
                    string[] customerNm = ds.Tables[0].Rows[0]["CustomerName"].ToString().Split(' ');
                    int count = customerNm.Count<string>();
                    for (int i = 0; i < count - 1; i++)
                    {
                        txt_fname.Text = txt_fname.Text + " " + customerNm[i];
                    }
                    txtlastname.Text = customerNm[count - 1];
                    txtcellphone.Text = ds.Tables[0].Rows[0]["CellPh"].ToString();
                    txt_altphone.Text = ds.Tables[0].Rows[0]["AlternatePh"].ToString();
                    txt_homephone.Text = ds.Tables[0].Rows[0]["HousePh"].ToString();
                    txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtEmail2.Text = ds.Tables[0].Rows[0]["Email2"].ToString();
                    txtEmail3.Text = ds.Tables[0].Rows[0]["Email3"].ToString();
                    txtaddress.Text = ds.Tables[0].Rows[0]["CustomerAddress"].ToString();
                    txtzip.Text = citystatezip[2];
                    txtstate.Text = citystatezip[1];
                    txtcity.Text = citystatezip[0];
                    if (ds.Tables[0].Rows[0]["Followup_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToShortDateString() != "1/1/1753" && Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToShortDateString() != "01/01/1753")
                        {
                            txtfollowupdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToString("MM/dd/yyyy");
                            ViewState["PreviousFollowupDate"] = txtfollowupdate.Text;
                        }
                        else
                        {
                            ViewState["PreviousFollowupDate"] = "1/1/1753";
                        }
                    }
                    else
                    {
                        ViewState["PreviousFollowupDate"] = "1/1/1753";
                    }

                    if (ds.Tables[0].Rows[0]["ProductOfInterest"].ToString() != "")
                        drpProductOfInterest1.SelectedIndex = Convert.ToInt16(ds.Tables[0].Rows[0]["ProductOfInterest"].ToString());
                    else
                        drpProductOfInterest1.SelectedIndex = 0;

                    if (ds.Tables[0].Rows[0]["SecondaryProductOfInterest"].ToString() != "")
                        drpProductOfInterest2.SelectedIndex = Convert.ToInt16(ds.Tables[0].Rows[0]["SecondaryProductOfInterest"].ToString());
                    else
                        drpProductOfInterest2.SelectedIndex = 0;

                    //txtproductofinterest.Text = ds.Tables[0].Rows[0]["Service"].ToString();
                    ddlbesttimetocontact.SelectedValue = ds.Tables[0].Rows[0]["BestTimetocontact"].ToString();
                    ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    ViewState["PreviousStatus"] = ddlstatus.SelectedValue;
                    HiddenStatus.Value = ds.Tables[0].Rows[0]["Status"].ToString();
                    txtbillingaddress.Text = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
                    ddlprimarycontact.SelectedValue = ds.Tables[0].Rows[0]["PrimaryContact"].ToString();
                   // txtnotes.Text = ds.Tables[0].Rows[0]["Service"].ToString();
                    string contactpreference = ds.Tables[0].Rows[0]["ContactPreference"].ToString();
                   
                   // ViewState["CallTakenBy"] = ds.Tables[0].Rows[0]["AddedBy"].ToString();
                    ViewState[JG_Prospect.Common.SessionKey.Key.UserId.ToString ()] = ds.Tables[0].Rows[0]["UserId"].ToString();
                    if (contactpreference == "Email")
                    {
                        chbemail.Checked = true;
                        chbmail.Checked = false;
                    }
                    else
                    {
                        chbmail.Checked = true;
                        chbemail.Checked = false;
                    }
                   // btnTouchPointLog.Visible = true;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Record Does not exists');", true);
            }
        }

        //protected void btnTouchPointLog_Click(object sender, EventArgs e)
        //{
        //   // Response.Redirect("TouchPointLogJr.aspx?CustomerId=" + Customer_id.ToString());
        //    bindGrid();
        //    mpeTouchPointLog.Show();
        //}
        protected void bindGrid()
        {
            DataSet ds = new_customerBLL.Instance.GetTouchPointLogData((Convert.ToInt32(Session["CustomerId"].ToString())), (Convert.ToInt32(Session["CustomerId"].ToString())));
            grdTouchPointLog.DataSource = ds;
            grdTouchPointLog.DataBind();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            string primarycontact = "";
            Customer objcust = new Customer();
            objcust.id = Customer_id;
            objcust.missingcontacts = 0;
            string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
            string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
            string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
            bool IsStatusSame = false;
            if ((txtEst_date.Text == "" || txtestimatetime.Text == "") && (txtfollowupdate.Text == ""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one Estimate Date & time or follow up date');", true);
                return;
            }
            if ((txtEst_date.Text != "" || txtestimatetime.Text != "") && (txtfollowupdate.Text != ""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill only one : either Estimate Date & Time or Follow Up Date');", true);
                return;
            }
            if ((txtEst_date.Text == "" || txtestimatetime.Text == "") && (ddlstatus.SelectedValue == "Set"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill Estimate Date & time while status is Set ');", true);
                return;
            }
            if ((txtfollowupdate.Text == "") && (ddlstatus.SelectedValue == "Follow up"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill follow up date');", true);
                return;
            }
            if (txtzip.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter zip code');", true);
                return;
            }

            if (chbemail.Checked && txtemail.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Email');", true);
                return;
            }

            if (txtcellphone.Text == "" || txtcellphone.Text == null)
            {
                objcust.missingcontacts++;
            }
            if (txt_altphone.Text == "" || txt_altphone.Text == null)
            {
                objcust.missingcontacts++;
            }
            if (txt_homephone.Text == "" || txt_homephone.Text == null)
            {
                objcust.missingcontacts++;
            }
            if (objcust.missingcontacts > 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one contact(Cell Phone, Home Phone or Alt. Phone');", true);
                return;
            }

            if (ddlprimarycontact.SelectedValue == "Cell Phone")
            {
                if (txtcellphone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Cell Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txtcellphone.Text;
            }
            else if (ddlprimarycontact.SelectedValue == "House Phone")
            {
                if (txt_homephone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter House Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txt_homephone.Text;
            }
            else if (ddlprimarycontact.SelectedValue == "Alt Phone")
            {
                if (txt_altphone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Alternate Phone, as it is primary contact');", true);
                    return;
                }
                primarycontact = txt_altphone.Text;
            }

            int primarycont = new_customerBLL.Instance.Searchprimarycontact(txt_homephone.Text, txtcellphone.Text, txt_altphone.Text, Customer_id);
            if (primarycont == 1)
            {
                objcust.Isrepeated = false;

                if (chbemail.Checked == true)
                {
                    objcust.ContactPreference = chbemail.Text;
                }
                if (chbmail.Checked == true)
                {
                    objcust.ContactPreference = chbmail.Text;
                }

                objcust.EstDate = string.IsNullOrEmpty(txtEst_date.Text) ? "1/1/1753" : txtEst_date.Text;
                ViewState["CurrentEstimateDate"]=objcust.EstDate ;
                objcust.EstTime = txtestimatetime.Text;
                if (txtestimatetime.Text != "")
                    ViewState["CurrentEstimateTime"] = objcust.EstTime;
                else
                    ViewState["CurrentEstimateTime"] = 0;

                objcust.firstName = txt_fname.Text.Trim();
                objcust.lastName = txtlastname.Text.Trim();
                objcust.customerNm = txt_fname.Text.Trim() + ' ' + txtlastname.Text.Trim();

                objcust.CellPh = txtcellphone.Text;
                objcust.AltPh = txt_altphone.Text;
                objcust.HousePh = txt_homephone.Text;
                objcust.Email = txtemail.Text;
                objcust.Email2 = txtEmail2.Text;
                objcust.Email3 = txtEmail3.Text;
                objcust.CustomerAddress = txtaddress.Text;
                objcust.City = txtcity.Text;
                objcust.state = txtstate.Text;
                objcust.Zipcode = txtzip.Text;
                objcust.followupdate = string.IsNullOrEmpty(txtfollowupdate.Text) ? "1/1/1753" : Convert.ToDateTime(txtfollowupdate.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
                ViewState["CurrentFollowupDate"]=objcust.followupdate;
                //objprospect.followup_status = txtfollowupstatus.Text;
                if (drpProductOfInterest1.SelectedIndex != 0)
                {
                    objcust.Productofinterest = Convert.ToInt16(drpProductOfInterest1.SelectedItem.Value); //txtproductofinterest.Text;
                }
                else
                {
                    objcust.Productofinterest = 0;
                }

                if (drpProductOfInterest2.SelectedIndex != 0)
                {
                    objcust.SecondaryProductofinterest = Convert.ToInt16(drpProductOfInterest2.SelectedItem.Value); //txtproductofinterest.Text;
                }
                else
                {
                    objcust.SecondaryProductofinterest = 0;
                }

                objcust.BestTimetocontact = ddlbesttimetocontact.SelectedValue.ToString();
                //if (HiddenStatus.Value != ddlstatus.SelectedItem.Text && ddlstatus.SelectedItem.Text != "Select")
                //    objcust.status = ddlstatus.SelectedItem.Text;
                //else
                //    objcust.status = HiddenStatus.Value;
                if (ddlstatus.SelectedIndex != 0)
                    ViewState["CurrentStatus"] = ddlstatus.SelectedItem.Text;
                else
                {
                    if (ddlstatus.Text.Trim() == "Select")
                    {
                        ViewState["CurrentStatus"] = ViewState["PreviousStatus"];
                    }
                    else
                    {
                        ViewState["CurrentStatus"] = ddlstatus.Text;
                    }
                }

                //if all are same, then status will remain as previous
                if (ViewState["CurrentStatus"].ToString() == ViewState["PreviousStatus"].ToString() && ViewState["CurrentFollowupDate"].ToString() == ViewState["PreviousFollowupDate"].ToString() && ViewState["CurrentEstimateDate"].ToString() == ViewState["PreviousEstimateDate"].ToString() && ViewState["CurrentEstimateTime"].ToString() == ViewState["PreviousEstimateTime"].ToString())
                {
                    objcust.status = ViewState["PreviousStatus"].ToString();
                    IsStatusSame = true;
                } 
                //if only status gets changed then new status will be stored
                else if (ViewState["CurrentStatus"].ToString() != ViewState["PreviousStatus"].ToString() && ViewState["CurrentFollowupDate"].ToString() == ViewState["PreviousFollowupDate"].ToString() && ViewState["CurrentEstimateDate"].ToString() == ViewState["PreviousEstimateDate"].ToString() && ViewState["CurrentEstimateTime"].ToString() == ViewState["PreviousEstimateTime"].ToString())
                {
                    objcust.status = ViewState["CurrentStatus"].ToString();
                }
                //if followup date & status gets changes then status=Followup
                else if (ViewState["CurrentStatus"].ToString() != ViewState["PreviousStatus"].ToString() && ViewState["CurrentFollowupDate"].ToString() != ViewState["PreviousFollowupDate"].ToString() && ViewState["CurrentEstimateDate"].ToString() == ViewState["PreviousEstimateDate"].ToString() && ViewState["CurrentEstimateTime"].ToString() == ViewState["PreviousEstimateTime"].ToString())
                {
                    objcust.status = JGConstant.CUSTOMER_STATUS_FOLLOWUP;
                }
                //if status & estimate date or time gets changed then status=set
                else if (ViewState["CurrentStatus"].ToString() != ViewState["PreviousStatus"].ToString() && ViewState["CurrentFollowupDate"].ToString() == ViewState["PreviousFollowupDate"].ToString() && (ViewState["CurrentEstimateDate"].ToString() != ViewState["PreviousEstimateDate"].ToString() || ViewState["CurrentEstimateTime"].ToString() != ViewState["PreviousEstimateTime"].ToString()))
                {
                    if (ViewState["CurrentEstimateTime"].ToString() != "" && ViewState["CurrentEstimateDate"].ToString() != "")
                    {
                        objcust.status = JGConstant.CUSTOMER_STATUS_SET;
                    }
                }
                // if only followup date is changed then status will be Followup
                else if (ViewState["CurrentFollowupDate"].ToString() != ViewState["PreviousFollowupDate"].ToString() && ViewState["CurrentEstimateDate"].ToString() == ViewState["PreviousEstimateDate"].ToString() && ViewState["CurrentEstimateTime"].ToString() == ViewState["PreviousEstimateTime"].ToString())
                {
                    objcust.status = JGConstant.CUSTOMER_STATUS_FOLLOWUP;
                }
                    //if only estimate date or time gets changed then status=set
                else if (ViewState["CurrentFollowupDate"].ToString() == ViewState["PreviousFollowupDate"].ToString() && (ViewState["CurrentEstimateDate"].ToString() != ViewState["PreviousEstimateDate"].ToString() || ViewState["CurrentEstimateTime"].ToString() != ViewState["PreviousEstimateTime"].ToString()))
                {
                    if (ViewState["CurrentEstimateTime"].ToString() != "" && ViewState["CurrentEstimateDate"].ToString() != "")
                    {
                        objcust.status = JGConstant.CUSTOMER_STATUS_SET;
                    }
                }
                else
                {
                    objcust.status = ViewState["CurrentStatus"].ToString();
                }

               // objcust.Notes = txtnotes.Text;
                objcust.BillingAddress = txtbillingaddress.Text;
                objcust.PrimaryContact = ddlprimarycontact.SelectedValue;
                objcust.Addedby = ViewState["CallTakenBy"].ToString();
              //  objcust.CallTakenby = Convert.ToString(Session["loginid"]);
                objcust.CallTakenby = Convert.ToString(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
              //  objcust.CallTakenby = ViewState["CallTakenBy"].ToString();
                string date = Convert.ToDateTime(objcust.EstDate).ToShortDateString();
                string datetime = null;
                if (date != "1/1/1753")
                {
                    datetime = Convert.ToDateTime(date + " " + objcust.EstTime).ToString("MM/dd/yy hh:mm tt");
                }
                //else
                //{
                //    datetime = Convert.ToDateTime(objcust.followupdate).ToString("MM/dd/yy hh:mm tt");
                //}

                objcust.Map1 = objcust.customerNm + "-" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                objcust.Map2 = objcust.customerNm + "-" + "Direction" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
                new_customerBLL.Instance.SaveMapImage(objcust.Map1, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode, DestinationPath);
                new_customerBLL.Instance.SaveMapImageDirection(objcust.Map2, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode, DestinationPath);

                result = new_customerBLL.Instance.UpdateCustomer(objcust);
                if (!IsStatusSame)
                {
                    if (objcust.status == JGConstant.CUSTOMER_STATUS_SET)
                    {
                        new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(result.ToString()), DateTime.Parse(datetime, JGConstant.CULTURE), objcust.status, Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString()), false, 0);
                    }
                    else
                    {
                        new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(result.ToString()), DateTime.Parse(objcust.followupdate,JGConstant.CULTURE), objcust.status, Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString()), false,0);
                    }
                }
                try
                {
                    if (result > 0)
                    {
                        string gtitle = txtestimatetime.Text + " -" + primarycontact + " -" + objcust.Addedby;
                        string gcontent = "Name: " + objcust.customerNm + " , Product of Interest: " + objcust.Productofinterest + ", Phone: " + objcust.CellPh + ", Alt. phone: " + objcust.AltPh + ", Email: " + objcust.Email + ",Notes: " + objcust.Notes + ",Status: " + objcust.status;
                        string gaddress = objcust.CustomerAddress + "," + objcust.City;
                        string newstatus = objcust.status;
                        if (newstatus == "Set" || newstatus == "est<$1000" || newstatus == "est>$1000" || newstatus == "sold<$1000" || newstatus == "sold>$1000" || newstatus == "Closed(sold)")
                        {
                            if (GoogleCalendarEvent.DeleteEvent(result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId))
                            {
                                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting the prospect');", true);
                            }
                            if (AdminId != objcust.Addedby)
                            {
                                if (GoogleCalendarEvent.DeleteEvent(result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), objcust.Addedby))
                                {
                                    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), objcust.Addedby);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting the prospect');", true);
                                }
                            }

                            if (GoogleCalendarEvent.DeleteEvent(result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar))
                            {
                                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting the prospect');", true);
                            }
                        }

                        else
                        {
                            GoogleCalendarEvent.DeleteEvent(result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), objcust.Addedby);

                            GoogleCalendarEvent.DeleteEvent(result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);
                            GoogleCalendarEvent.DeleteEvent(result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);
                        }

                        ResetFormControlValues(this);
                        hideTouchPointLogDetails();
                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prospect Updated successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in updating the prospect');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in updating the prospect');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Duplicate contact cannot Update the Prospect');", true);
                objcust.Isrepeated = true;
            }

        }

        protected void txtestimatetime_TextChanged(object sender, EventArgs e)
        {
            //if (txtEst_date.Text != "" && txtestimatetime.Text != "")
            //{
            //    trfollowup.Visible = false;
            //    ddlstatus.SelectedValue = "Set";
            //}
            //else
            //{
            //    trfollowup.Visible = true;
            //}
        }
        protected void txtfollowupdate_TextChanged(object sender, EventArgs e)
        {
            if (txtfollowupdate.Text != "")
            {
                txtEst_date.Enabled = false;
                txtestimatetime.Enabled = false;
                txtEst_date.Text = "";
                txtestimatetime.Text = "";
            }
            else
            {
                txtEst_date.Enabled = true;
                txtestimatetime.Enabled = true;
                ddlstatus.SelectedIndex = -1;
            }
        }
        protected void txtzip_TextChanged(object sender, EventArgs e)
        {
            if (txtzip.Text == "")
            {
                //
            }
            else
            {
                ds = new DataSet();
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
            greyout();
        }

        protected void chkbillingaddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbillingaddress.Checked)
            {
                txtbillingaddress.Text = txtaddress.Text + " " + txtcity.Text + " " + txtstate.Text + " " + txtzip.Text;
            }
            //else
            //{
            //    txtbillingaddress.Text = "";
            //}
        }

        protected void txtcellphone_TextChanged(object sender, EventArgs e)
        {
            int primarycont = 0;
            primarycont = new_customerBLL.Instance.Searchprimarycontact(txt_homephone.Text, txtcellphone.Text, txt_altphone.Text, 0);
            if (primarycont == 1)
            {
                hdnisduplicate.Value = "0";
                hdnCustId.Value = "0";
            }
            else
            {
                hdnisduplicate.Value = "1";
                hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txtcellphone.Text).ToString();
            }
        }

        protected void txt_altphone_TextChanged(object sender, EventArgs e)
        {
            int primarycont = 0;
            primarycont = new_customerBLL.Instance.Searchprimarycontact(txt_homephone.Text, txtcellphone.Text, txt_altphone.Text, 0);
            if (primarycont == 1)
            {
                hdnisduplicate.Value = "0";
                hdnCustId.Value = "0";
            }
            else
            {
                hdnisduplicate.Value = "1";
                hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txt_altphone.Text).ToString();
            }
        }

        protected void txt_homephone_TextChanged(object sender, EventArgs e)
        {
            int primarycont = 0;
            primarycont = new_customerBLL.Instance.Searchprimarycontact(txt_homephone.Text, txtcellphone.Text, txt_altphone.Text, 0);
            if (primarycont == 1)
            {
                hdnisduplicate.Value = "0";
                hdnCustId.Value = "0";
            }
            else
            {
                hdnisduplicate.Value = "1";
                hdnCustId.Value = new_customerBLL.Instance.SearchCustomerId(txt_homephone.Text).ToString();
            }
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            string note = txtAddNotes.Text.Trim();
            new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(Session["CustomerId"].ToString()), DateTime.Now, note, UserId, true,0);
            txtAddNotes.Text = string.Empty;
            bindGrid();
        }
    }
}