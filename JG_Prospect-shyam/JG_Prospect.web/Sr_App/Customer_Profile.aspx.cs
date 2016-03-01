using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;

using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;


namespace JG_Prospect.Sr_App
{
    public partial class Customer_Profile : System.Web.UI.Page
    {
        private static int UserId = 0;
        private static int ColorFlag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["loginid"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have to login first');", true);
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    bindProducts();
                    txtcall_taken.Text = Session["loginid"].ToString();
                    //txtProjectManager.Text = Session["loginid"].ToString();
                    if (Request.QueryString["title"] != null)
                    {
                        string[] title = Request.QueryString["title"].Split('-');
                        Session["CustomerId"] = title[0].ToString();
                    }

                    if (Request.QueryString["CustomerId"] != null)
                    {
                        Session["CustomerId"] = Request.QueryString["CustomerId"];
                    }

                    if (Session["CustomerId"] != null)
                    {
                        DataSet ds = new DataSet();

                        int CustID = Convert.ToInt32(Session["CustomerId"].ToString());
                        ds = new_customerBLL.Instance.GetCustomerDetails(CustID);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            imgmap.ImageUrl = "~/CustomerDocs/Maps/" + ds.Tables[0].Rows[0]["Map1"].ToString();
                        }
                        bindstatus();
                        FillCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
                        //FillFollowUpDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
                        FillsoldJobs(Convert.ToInt32(Session["CustomerId"].ToString()));
                       
                        if (Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()] != null)
                        {
                            UserId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                        }
                    }
                    bindGrid();
                }
            }
        }
        static string oldtitle;
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
        private void FillsoldJobs(int customerId)
        {
            DataSet ds = new DataSet();
            string Status = "Sold";
            //ds = shuttersBLL.Instance.GetProductLineItems(customerId, Status);
            ds = new_customerBLL.Instance.GetCustomerProfileProducts(customerId);
            ViewState["soldjobs"] = ds;
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridViewSoldJobs.DataSource = ds;
                    GridViewSoldJobs.DataBind();
                }
                else
                {
                    GridViewSoldJobs.DataSource = null;
                }
            }
        }
        //private void FillFollowUpDetails(int CustomerId)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = new_customerBLL.Instance.GetCustomerFollowUpDetails(CustomerId);

        //        if (ds != null)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                HiddenFieldassignid.Value = ds.Tables[0].Rows[0][4].ToString();
        //                if (ds.Tables[1].Rows.Count > 1)
        //                {
        //                    if (ds.Tables[1].Rows[1]["MeetingDate"] != DBNull.Value)
        //                    {
        //                        txtfollowup1.Text = ds.Tables[1].Rows[1]["MeetingDate"].ToString() == "01-01-1753" ? " " : ds.Tables[1].Rows[1]["MeetingDate"].ToString();
        //                    }
        //                    if (ds.Tables[1].Rows[1]["MeetingStatus"] != DBNull.Value)
        //                    {
        //                        LblStatus1.Text = ds.Tables[1].Rows[1]["MeetingStatus"].ToString();
        //                    }
        //                    if (ds.Tables[1].Rows[0]["MeetingDate"] != DBNull.Value)
        //                    {
        //                        txtfollowup2.Text = ds.Tables[1].Rows[0]["MeetingDate"].ToString() == "01-01-1753" ? " " : ds.Tables[1].Rows[0]["MeetingDate"].ToString();
        //                    }
        //                    if (ds.Tables[1].Rows[0]["MeetingStatus"] != DBNull.Value)
        //                    {
        //                        LblStatus2.Text = ds.Tables[1].Rows[0]["MeetingStatus"].ToString();
        //                    }
        //                }
        //                else
        //                {
        //                    if (ds.Tables[0].Rows[0][2] != DBNull.Value)
        //                    {
        //                        txtfollowup1.Text = ds.Tables[0].Rows[0][2].ToString() == "01-01-1753" ? " " : ds.Tables[0].Rows[0][2].ToString();
        //                    }
        //                    if (ds.Tables[0].Rows[0][3] != DBNull.Value)
        //                    {
        //                        LblStatus1.Text = ds.Tables[0].Rows[0][3].ToString();
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //
        //    }
        //}
        private void bindstatus()
        {
            ddlfollowup3.Items.Clear();
            ddlfollowup3.Items.Insert(0, new ListItem("Select", "0"));
            ddlfollowup3.Items.Add("Set");
            ddlfollowup3.Items.Add("Prospect");
            ddlfollowup3.Items.Add("est>$1000");
            ddlfollowup3.Items.Add("est<$1000");
            ddlfollowup3.Items.Add("sold>$1000");
            ddlfollowup3.Items.Add("sold<$1000");
            ddlfollowup3.Items.Add("Closed (not sold)");
            ddlfollowup3.Items.Add("Closed (sold)");
            ddlfollowup3.Items.Add("Rehash");
            ddlfollowup3.Items.Add("cancelation-no rehash");
            ddlfollowup3.Items.Add("Assigned");
            //ddlfollowup3.Items.Add("Follow up");
            // greyout();
        }

        private void greyout()
        {
            foreach (ListItem item in ddlfollowup3.Items)
            {
                ListItem i = ddlfollowup3.Items.FindByText(item.Text);
                if (i.Text == "est>$1000" || i.Text == "est<$1000" || i.Text == "sold>$1000" || i.Text == "sold<$1000")
                {
                    i.Attributes.Add("style", "color:gray;");
                    i.Attributes.Add("disabled", "true");
                    i.Value = "-1";
                }
            }
        }
        //protected void btnTouchPointLog_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("TouchPointLogSr.aspx?CustomerId=" + Session["CustomerId"].ToString());
            
        //   // mpeTouchPointLog.Show();
        //}
        protected void bindGrid()
        {
            //DataSet ds = new_customerBLL.Instance.GetTouchPointLogData(Convert.ToInt32(Session["CustomerId"].ToString()));
            //grdTouchPointLog.DataSource = ds;
            //grdTouchPointLog.DataBind();
            int CustomerId = Convert.ToInt32(HttpContext.Current.Session["CustomerId"].ToString());
            DataSet ds = new_customerBLL.Instance.GetTouchPointLogData(CustomerId, UserId);
            grdTouchPointLog.DataSource = ds;
            grdTouchPointLog.DataBind();
            txtAddNotes.Text = "";
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

        private void FillCustomerDetails(int CustomerId)
        {
            try
            {
                string customername, leadtype;
                DataSet ds = new DataSet();
                DataSet dsLocation = new DataSet();
                bool flag = false;
                ds = new_customerBLL.Instance.GetCustomerDetails(CustomerId);
                var LocationPic = ds.Tables[1];
                ViewState["dt"] = ds.Tables[1];
                GridViewLocationPic.DataSource = LocationPic;
                GridViewLocationPic.DataBind();
                if (ds != null)
                {
                    lblmsg.Text = CustomerId.ToString();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        customername = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                        String[] name = customername.Split(' ');
                        int count = name.Count<string>();
                        for (int i = 0; i < count - 1; i++)
                        {
                            txtfirstname.Text = txtfirstname.Text + " " + name[i];
                        }
                        
                        txtlast_name.Text = name[count - 1];
                        txtaddress.Text = ds.Tables[0].Rows[0]["CustomerAddress"].ToString();
                        txtstate.Text = ds.Tables[0].Rows[0]["State"].ToString();
                        txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        txtzip.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                        if (ds.Tables[0].Rows[0]["EstDate"].ToString() != "")
                        {
                            txtestimate_date.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EstDate"].ToString()).ToString("MM/dd/yyyy");
                        }
                        txtestimate_time.Text = ds.Tables[0].Rows[0]["EstTime"].ToString();
                        txtcell_ph.Text = ds.Tables[0].Rows[0]["CellPh"].ToString();
                        txthome_phone.Text = ds.Tables[0].Rows[0]["HousePh"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                        txtEmail2.Text = ds.Tables[0].Rows[0]["Email2"].ToString();
                        txtEmail3.Text = ds.Tables[0].Rows[0]["Email3"].ToString();
                        txtcall_taken.Text = ds.Tables[0].Rows[0]["Login_Id"].ToString();
                        txtService.Text = ds.Tables[0].Rows[0]["Service"].ToString();
                        leadtype = ds.Tables[0].Rows[0]["LeadType"].ToString();
                        txtalt_phone.Text = ds.Tables[0].Rows[0]["AlternatePh"].ToString();
                        txtbill_address.Text = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
                        ddlbesttime.SelectedValue = ds.Tables[0].Rows[0]["BestTimetocontact"].ToString();
                        ddlprimarycontact.SelectedValue = ds.Tables[0].Rows[0]["PrimaryContact"].ToString();
                        string contactpreference = ds.Tables[0].Rows[0]["ContactPreference"].ToString();
                        Hiddenfieldstatus.Value = ds.Tables[0].Rows[0]["Status"].ToString();
                        if (ds.Tables[0].Rows[0]["Followup_date"] != null)
                        {
                            if (ds.Tables[0].Rows[0]["Followup_date"].ToString() != "")
                                txtfollowup3.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToString("MM/dd/yyyy");
                        }
                       
                        ddlfollowup3.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                        for (int i = 0; i < ddlleadtype.Items.Count; i++)
                        {
                            if (leadtype == ddlleadtype.Items[i].Value)
                            {
                                spanother.Visible = false;
                                ddlleadtype.SelectedValue = ddlleadtype.Items[i].Value;
                                flag = true;
                            }
                            else if (leadtype == "Friendly/Family")
                            {
                                spanother.Visible = false;
                                ddlleadtype.SelectedValue = "Referal Family/Friend";
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            spanother.Visible = true;
                            txtleadtype.Text = leadtype;
                            ddlleadtype.SelectedValue = "Other";
                        }
                        if (contactpreference.Trim() == "Email")
                        {
                            chbemail.Checked = true;

                        }
                        if (contactpreference.Trim() == "Mail")
                        {
                            chbmail.Checked = true;
                        }
                        oldtitle = Session["CustomerId"] + "-" + txtestimate_time.Text + " -" + txtcell_ph.Text + " -" + Session["loginid"].ToString(); // added by id to replace
                        ViewState["oldtitle"] = oldtitle;
                        if(ds.Tables[0].Rows[0]["ProjectManager"]!=null)
                        {
                            txtProjectManager.Text = ds.Tables[0].Rows[0]["ProjectManager"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["ProductOfInterest"].ToString() != "")
                        {
                            drpProductOfInterest1.SelectedIndex = Convert.ToInt16(ds.Tables[0].Rows[0]["ProductOfInterest"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["SecondaryProductOfInterest"].ToString() != "")
                        {
                            drpProductOfInterest2.SelectedIndex = Convert.ToInt16(ds.Tables[0].Rows[0]["SecondaryProductOfInterest"].ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }

        }
        protected void lnkestimateid_Click(object sender, EventArgs e)
        {
            LinkButton btnProduct = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnProduct.Parent.Parent;
            HiddenField HiddenFieldEstimate = (HiddenField)row.FindControl("HiddenFieldEstimate");
            HiddenField HidCustomerId = row.FindControl("HidCustomerId") as HiddenField;
            HiddenField HidProductTypeId = row.FindControl("HidProductTypeId") as HiddenField;
            HiddenField HidProductTypeIdFrom = row.FindControl("HidProductTypeIdFrom") as HiddenField;
            string file = "~/UploadedFiles/" + btnProduct.Text;

                ResponseHelper.Redirect(file, "_blank", "menubar=0,width=600,height=600");

        }
        public static class ResponseHelper
        {
            public static void Redirect(string url, string target, string windowFeatures)
            {
                HttpContext context = HttpContext.Current;

                if ((String.IsNullOrEmpty(target) ||
                    target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
                    String.IsNullOrEmpty(windowFeatures))
                {

                    context.Response.Redirect(url);
                }
                else
                {
                    Page page = (Page)context.Handler;
                    if (page == null)
                    {
                        throw new InvalidOperationException(
                            "Cannot redirect to new window outside Page context.");
                    }
                    url = page.ResolveClientUrl(url);

                    string script;
                    if (!String.IsNullOrEmpty(windowFeatures))
                    {
                        script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                    }
                    else
                    {
                        script = @"window.open(""{0}"", ""{1}"");";
                    }

                    script = String.Format(script, url, target, windowFeatures);
                    ScriptManager.RegisterStartupScript(page,
                        typeof(Page),
                        "Redirect",
                        script,
                        true);
                }
            }
        }
        protected void lnkwrkordfer_Click(object sender, EventArgs e)
        {
            LinkButton wrkorder = sender as LinkButton;
            GridViewRow row = (GridViewRow)wrkorder.Parent.Parent;
            HiddenField HiddenFieldEstimate = (HiddenField)row.FindControl("HiddenFieldEstimate");
            HiddenField HiddenFieldWorkOrder = (HiddenField)row.FindControl("HiddenFieldWorkOrder");
            if (HiddenFieldEstimate.Value != null)
            {
                string file = "~/CustomerDocs/Pdfs/" + HiddenFieldWorkOrder.Value.ToString();

                ResponseHelper.Redirect(file, "_blank", "menubar=0,width=600,height=600");
                // wrkordfer.PostBackUrl = "~/CustomerDocs/Pdfs/" + wrkordfer.Text;
            }
        }
        protected void lnkwrkzip_Click(object sender, EventArgs e)
        {
            LinkButton lnkwrkzip = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnkwrkzip.Parent.Parent;
            HiddenField HiddenFieldEstimate = (HiddenField)row.FindControl("HiddenFieldEstimate");
            HiddenField HidProductTypeId = (HiddenField)row.FindControl("HidProductTypeId");
            int productId = Convert.ToInt16(HiddenFieldEstimate.Value);
            int productTypeId = Convert.ToInt16(HidProductTypeId.Value);
            if (HiddenFieldEstimate.Value != null)
            {
                ResponseHelper.Redirect("ZipFile.aspx?productId=" + productId + "&productTypeId=" + productTypeId, "_blank", "menubar=0,width=605px,height=630px");
                // wrkordfer.PostBackUrl = "~/CustomerDocs/Pdfs/" + wrkordfer.Text;
            }
        }
        protected void lnkContract_Click(object sender, EventArgs e)
        {
            LinkButton Contract = sender as LinkButton;
            GridViewRow row = (GridViewRow)Contract.Parent.Parent;
            HiddenField HiddenFieldEstimate = (HiddenField)row.FindControl("HiddenFieldEstimate");
            HiddenField HiddenFieldContract = (HiddenField)row.FindControl("HiddenFieldContract");

            if (HiddenFieldEstimate.Value != null)
            {
                string file = "~/CustomerDocs/Pdfs/" + HiddenFieldContract.Value.ToString();
                ResponseHelper.Redirect(file, "_blank", "menubar=0,width=600,height=600");
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
        protected void ddlfollowup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfollowup3.SelectedValue == "est>$1000" || ddlfollowup3.SelectedValue == "est<$1000")
            {
                txtfollowup3.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
            }
            else
            {
                txtfollowup3.Text = "";
            }
        }
        protected void ddlleadtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlleadtype.SelectedValue.ToString() == "Other")
            //{
            //    spanother.Visible = true;
            //}
            //else
            //{
            //    spanother.Visible = false;
            //}

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


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Customer objcust = new Customer();
                objcust.id = Convert.ToInt32(Session["CustomerId"].ToString());
                int primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, objcust.id);
                string primarycontact = "";
                objcust.ContactPreference = string.Empty;
                if ((txtestimate_date.Text != "" || txtestimate_time.Text != "") && (txtfollowup3.Text != ""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill only one : either Estimate Date & Time or Follow Up Date');", true);
                    return;
                }
                if ((ddlfollowup3.SelectedItem.Text == "est<$1000" || ddlfollowup3.SelectedItem.Text == "est>$1000") && txtfollowup3.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select FollowUp Date!');", true);
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


                if (chbemail.Checked)
                {
                    if (txtEmail.Text == "" && txtEmail2.Text == "" && txtEmail3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter at least one Email');", true);
                        return;
                    }
                    objcust.ContactPreference = chbemail.Text;
                }
                if (chbmail.Checked)
                {
                    objcust.ContactPreference = chbmail.Text;
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                objcust.firstName = txtfirstname.Text.Trim();
                objcust.lastName = txtlast_name.Text.Trim();
                objcust.customerNm = txtfirstname.Text.Trim() + ' ' + txtlast_name.Text.Trim();
                objcust.CustomerAddress = txtaddress.Text;
                objcust.state = txtstate.Text;
                objcust.City = txtcity.Text;
                objcust.Zipcode = txtzip.Text;
                objcust.BillingAddress = txtbill_address.Text;
                DateTime EstDate = new DateTime();
                EstDate = string.IsNullOrEmpty(txtestimate_date.Text) ? Convert.ToDateTime("1/1/1753",JGConstant.CULTURE) : Convert.ToDateTime(txtestimate_date.Text, JGConstant.CULTURE);
                objcust.EstDate = EstDate.ToString("MM/dd/yyyy");
                objcust.EstTime = txtestimate_time.Text;
                objcust.CellPh = txtcell_ph.Text;
                objcust.HousePh = txthome_phone.Text;
                objcust.AltPh = txtalt_phone.Text;
                objcust.Email = txtEmail.Text;
                objcust.Email2 = txtEmail2.Text;
                objcust.Email3 = txtEmail3.Text;
                objcust.CallTakenby = txtcall_taken.Text;
                objcust.Addedby = txtcall_taken.Text;
                objcust.Notes = txtService.Text;
                objcust.BestTimetocontact = ddlbesttime.SelectedValue;
                objcust.PrimaryContact = ddlprimarycontact.SelectedValue;
                objcust.followupdate = string.IsNullOrEmpty(txtfollowup3.Text) ? "1/1/1753" : Convert.ToDateTime(txtfollowup3.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
                if (ddlfollowup3.SelectedValue == "0")
                    objcust.status = Hiddenfieldstatus.Value;
                else
                    objcust.status = ddlfollowup3.SelectedItem.Text;
                //objcust.Service = txtService.Text;
                if (ddlleadtype.SelectedValue.ToString() == "Other")
                {
                    objcust.Leadtype = txtleadtype.Text;
                }
                else
                {
                    objcust.Leadtype = ddlleadtype.SelectedValue.ToString();
                }
                    
                objcust.ProjectManager = txtProjectManager.Text;

                if (drpProductOfInterest1.SelectedIndex != 0)
                {
                    objcust.Productofinterest =Convert.ToInt16( drpProductOfInterest1.SelectedItem.Value);
                }
                else
                {
                    objcust.Productofinterest = 0;
                }
                if (drpProductOfInterest2.SelectedIndex != 0)
                {
                    objcust.SecondaryProductofinterest = Convert.ToInt16(drpProductOfInterest2.SelectedItem.Value);
                }
                else
                {
                    objcust.SecondaryProductofinterest = 0;
                }
                objcust.Map1 = objcust.customerNm + "-" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                objcust.Map2 = objcust.customerNm + "-" + "Direct" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";

                SaveMapImage(objcust.Map1, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode);
                SaveMapImageDirection(objcust.Map2, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode);

                int res = new_customerBLL.Instance.UpdateCustomer(objcust);

                try
                {
                    // UserId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                    if (ddlfollowup3.SelectedValue != "0")
                    {
                        int res1;
                        if (ddlfollowup3.SelectedItem.Text == "est<$1000" || ddlfollowup3.SelectedItem.Text == "est>$1000")
                        {
                            if (txtfollowup3.Text != "")
                            {
                                res1 = new_customerBLL.Instance.AddCustomerFollowUp(objcust.id, DateTime.Parse(objcust.followupdate, JGConstant.CULTURE), objcust.status, UserId, false, 0);
                                lblmsg.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter Follow Up Date');", true);
                                return;
                            }
                        }
                        else
                        {
                            res1 = new_customerBLL.Instance.AddCustomerFollowUp(objcust.id, DateTime.Parse(objcust.followupdate, JGConstant.CULTURE), objcust.status, UserId, false, 0);
                        }
                    }
                    string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                    string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                    string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                    if (res > 0)
                    {
                        string newstatus;
                        if (ddlfollowup3.SelectedValue != "0")
                            newstatus = ddlfollowup3.SelectedItem.Text;
                        else
                            newstatus = Hiddenfieldstatus.Value;
                        if (newstatus == "Set" || newstatus == "est<$1000" || newstatus == "est>$1000" || newstatus == "sold<$1000" || newstatus == "sold>$1000" || newstatus == "Closed(sold)")
                        {

                            DateTime datetime;
                            string t = string.Empty;
                            TimeSpan time;

                            datetime = string.IsNullOrEmpty(txtestimate_date.Text) ? Convert.ToDateTime("1/1/1753") : Convert.ToDateTime(txtestimate_date.Text, JGConstant.CULTURE);

                            if (txtestimate_time.Text != "")
                            {
                                t = txtestimate_time.Text;
                                time = Convert.ToDateTime(t).TimeOfDay;
                                datetime += time;
                            }
                            string gtitle = t + " -" + primarycontact + " -" + objcust.Addedby;
                            string gcontent = "Name: " + objcust.customerNm + " , Cell Phone: " + txtcell_ph.Text + ", Alt. phone: " + txtalt_phone.Text + ", Email: " + txtEmail.Text + ",Service: " + txtService.Text + ",Status: " + newstatus;
                            string gaddress = txtaddress.Text + " " + txtcity.Text + "," + txtstate.Text + " -" + txtzip.Text;
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Customer Updated successfully');window.location='home.aspx'", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in Updating the Customer');", true);
                    }
                }
                catch(Exception ex)
                {
 
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Customer Updated successfully.');window.location='home.aspx'", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Try again');", true);
            }
        }
        private void SaveMapImage(string Map1, string CustomerStreet, string Cityname, string Statename, string ZipCode)
        {
            StringBuilder queryAddress = new StringBuilder();
            //Appending the Basic format of the Staticmap from Google maps Here
            queryAddress.Append("http://maps.google.com/maps/api/staticmap?size=600x500&zoom=14&maptype=roadmap&markers=size:mid|color:red|");
            string location = CustomerStreet + " " + Cityname + " , " + Statename + " " + ZipCode;
            queryAddress.Append(location + ',' + '+');
            queryAddress.Append("&sensor=false");
            string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
            string filename = Map1;
            string path = DestinationPath + "/" + filename;
            string url = queryAddress.ToString();
            //String Url to Bytes
            byte[] bytes = GetBytesFromUrl(url);

            //Bytes Saved to Specified File
            WriteBytesToFile(DestinationPath + "/" + filename, bytes);
        }
        private void SaveMapImageDirection(string Map2, string CustomerStreet, string Cityname, string Statename, string ZipCode)
        {
            StringBuilder queryAddress = new StringBuilder();
            //Appending the Basic format of the Staticmap from Google maps Here
            queryAddress.Append("http://maps.google.com/maps/api/staticmap?size=600x500&zoom=14&maptype=roadmap&markers=size:mid|color:red|");
            string location = CustomerStreet + " " + Cityname + " , " + Statename + " " + ZipCode;
            queryAddress.Append(location + ',' + '+');
            queryAddress.Append('|');
            queryAddress.Append("220 krams Ave Manayunk, PA 19127");
            queryAddress.Append("&sensor=false");
            string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
            string filename = Map2;
            string path = DestinationPath + "/" + filename;
            string url = queryAddress.ToString();
            //String Url to Bytes
            byte[] bytes = GetBytesFromUrl(url);

            //Bytes Saved to Specified File
            WriteBytesToFile(DestinationPath + "/" + filename, bytes);
        }
        //Writing the bytes in to a Specified File to Save

        public void WriteBytesToFile(string fileName, byte[] content)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
            }

        }
        public byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();
            Stream stream = myResp.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }
        protected void GridViewLocationPic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (sender != null)
            {
                GridView grdlocationpics = sender as GridView;
                grdlocationpics.PageIndex = e.NewPageIndex;
                grdlocationpics.DataSource = (DataTable)ViewState["dt"];
                grdlocationpics.DataBind();
            }
        }
        protected void LinkButtonmap1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
            imgmap.ImageUrl = "~/CustomerDocs/Maps/" + ds.Tables[0].Rows[0]["Map1"].ToString();

        }
        private void ResetFormControlValues(Control parent)
        {
            imgmap.ImageUrl = "";
            GridViewLocationPic.DataSource = null;
            GridViewLocationPic.DataBind();
            GridViewSoldJobs.DataSource = null;
            GridViewSoldJobs.DataBind();
            LinkButtonmap1.Text = "";
            LinkButtonmap2.Text = "";
            lblmsg.Text = "";
            //LblStatus1.Text = "";

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
                            ((DropDownList)c).SelectedValue = "0";
                            break;

                        case "System.Web.UI.WebControls.Image":
                            //((Image)c).ImageUrl = "";
                            break;

                    }
                }

            }

        }
        protected void LinkButtonmap2_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
            imgmap.ImageUrl = "~/CustomerDocs/Maps/" + ds.Tables[0].Rows[0]["Map2"].ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFormControlValues(this);
            chbmail.Checked = true;
            lblmsg.Visible = false;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            if (Session["usertype"].ToString() != "Admin")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You don't have permission to delete customer record');", true);
                return;
            }
            else
            {
                int custid = Convert.ToInt32(Session["CustomerId"].ToString());
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                bool result = new_customerBLL.Instance.DeleteCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
                if (result)
                {
                    GoogleCalendarEvent.DeleteEvent(custid.ToString(), "", "", "", DateTime.Now, DateTime.Now, Session["loginid"].ToString());

                    GoogleCalendarEvent.DeleteEvent(custid.ToString(), "", "", "", DateTime.Now, DateTime.Now, AdminId);
                    GoogleCalendarEvent.DeleteEvent(custid.ToString(), "", "", "", DateTime.Now, DateTime.Now, JGConstant.CustomerCalendar);

                    ResetFormControlValues(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Customer Record Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting customer');", true);
                    return;
                }
            }
        }

        protected void GridViewSoldJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (sender != null)
            {
                GridView grdoldjobs = sender as GridView;
                grdoldjobs.PageIndex = e.NewPageIndex;
                grdoldjobs.DataSource = (DataSet)ViewState["soldjobs"];
                grdoldjobs.DataBind();
            }
        }

        protected void GridViewSoldJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton lnkwrkContract = (LinkButton)e.Row.FindControl("lnkwrkContract");
                //LinkButton lnkwrkordfer = (LinkButton)e.Row.FindControl("lnkwrkordfer");
                //HiddenField HiddenFieldWorkOrder = (HiddenField)e.Row.FindControl("HiddenFieldWorkOrder");
                //HiddenField HiddenFieldContract = (HiddenField)e.Row.FindControl("HiddenFieldContract");
                //if (HiddenFieldContract.Value != "")//|| HiddenFieldContract.Value != null)
                //{
                //    lnkwrkContract.Text = "Contract.pdf";
                //}

                //if (HiddenFieldWorkOrder.Value != "")//|| HiddenFieldWorkOrder.Value != null)
                //{
                //    lnkwrkordfer.Text = "WorkOrder.pdf";
                //}

            }
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            string note = txtAddNotes.Text.Trim();
            new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(Session["CustomerId"].ToString()), DateTime.Now, note, UserId,true,0);
            txtAddNotes.Text = string.Empty;
            bindGrid();
        }
    }
}
