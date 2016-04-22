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
using JG_Prospect.UserControl;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;

namespace JG_Prospect.Sr_App
{
    public partial class Customer_Profile : System.Web.UI.Page
    {
        private static int UserId = 0;
        private static int ColorFlag = 0;
        static int myCount = 0;
        public void Page_Load(object sender, EventArgs e)
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
                    Session["ButtonCount"] = 0;
                    bindProducts();
                    //txtcall_taken.Text = Session["loginid"].ToString();
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
                        /****** Commented By TCT ******/
                        //DataSet ds = new DataSet();
                        //ds = new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //imgmap.ImageUrl = "~/CustomerDocs/Maps/" + ds.Tables[0].Rows[0]["Map1"].ToString();

                        FillCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));
                        FillsoldJobs(Convert.ToInt32(Session["CustomerId"].ToString()));
                        if (Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()] != null)
                        {
                            UserId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                        }
                        bindGrid();
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Selected Customer Id not exists');", true);
                        //}
                    }

                }
            }

            
            ChangeColours();

           
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
            //if (ds != null)
            //{
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            GridViewSoldJobs.DataSource = ds;
            GridViewSoldJobs.DataBind();
            //}
            //else
            //{
            //GridViewSoldJobs.DataSource = null;
            //GridViewSoldJobs.DataBind();
            // }
            //}
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
        //private void bindstatus()
        //{
        //    ddlfollowup3.Items.Clear();
        //    ddlfollowup3.Items.Insert(0, new ListItem("Select", "0"));
        //    ddlfollowup3.Items.Add("Set");
        //    ddlfollowup3.Items.Add("Prospect");
        //    ddlfollowup3.Items.Add("est>$1000");
        //    ddlfollowup3.Items.Add("est<$1000");
        //    ddlfollowup3.Items.Add("sold>$1000");
        //    ddlfollowup3.Items.Add("sold<$1000");
        //    ddlfollowup3.Items.Add("Closed (not sold)");
        //    ddlfollowup3.Items.Add("Closed (sold)");
        //    ddlfollowup3.Items.Add("Rehash");
        //    ddlfollowup3.Items.Add("cancelation-no rehash");
        //    //ddlfollowup3.Items.Add("Follow up");
        //    // greyout();
        //}

        //private void greyout()
        //{
        //    DropDownList ddlfollowup3 = (DropDownList)e.Row.FindControl("ddlfollowup3");
        //    foreach (ListItem item in ddlfollowup3.Items)
        //    {
        //        ListItem i = ddlfollowup3.Items.FindByText(item.Text);
        //        if (i.Text == "est>$1000" || i.Text == "est<$1000" || i.Text == "sold>$1000" || i.Text == "sold<$1000")
        //        {
        //            i.Attributes.Add("style", "color:gray;");
        //            i.Attributes.Add("disabled", "true");
        //            i.Value = "-1";
        //        }
        //    }
        //}

        //protected void btnTouchPointLog_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("TouchPointLogSr.aspx?CustomerId=" + Session["CustomerId"].ToString());

        //   // mpeTouchPointLog.Show();
        //}
        protected void bindGrid()
        {
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
                //GridViewLocationPic.DataSource = LocationPic;
                //GridViewLocationPic.DataBind();
                if (ds != null)
                {
                    lblmsg.Text = CustomerId.ToString();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        customername = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                        String[] name = customername.Split(' ');
                        int count = name.Count<string>();

                        //TextBox txtAddress = (TextBox)UCAddress.FindControl("txtaddress");
                        //TextBox txtstate = (TextBox)UCAddress.FindControl("txtstate");
                        //TextBox txtcity = (TextBox)UCAddress.FindControl("txtcity");
                        //TextBox txtzip = (TextBox)UCAddress.FindControl("txtzip");
                        //DropDownList ddlAddressType = (DropDownList)UCAddress.FindControl("DropDownList1");
                        //txtAddress.Text = ds.Tables[0].Rows[0]["CustomerAddress"].ToString();
                        //txtstate.Text = ds.Tables[0].Rows[0]["State"].ToString();
                        //txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                        //txtzip.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                        //ddlAddressType.Items.FindByValue(ds.Tables[0].Rows[0]["strAddressType"].ToString()).Selected = true;

                        selAddressType.Items.FindByValue(ds.Tables[0].Rows[0]["strAddressType"].ToString()).Selected = true;
                        if (ds.Tables[0].Rows[0]["EstDate"].ToString() != "")
                        {
                            txtestimate_date.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EstDate"].ToString()).ToString("MM/dd/yyyy");
                        }
                        txtestimate_time.Text = ds.Tables[0].Rows[0]["EstTime"].ToString();
                        //txtbill_address.InnerText = ds.Tables[0].Rows[0]["strBillingAddress"].ToString();
                        txtCompetitorBids.Text = ds.Tables[0].Rows[0]["strCompetitorBids"].ToString();
                        leadtype = ds.Tables[0].Rows[0]["LeadType"].ToString();
                        string contactpreference = ds.Tables[0].Rows[0]["ContactPreference"].ToString();
                        Hiddenfieldstatus.Value = ds.Tables[0].Rows[0]["Status"].ToString();

                        /****** Commented By TCT ******/
                        //if (ds.Tables[0].Rows[0]["Followup_date"] != null)
                        //{
                        //    if (ds.Tables[0].Rows[0]["Followup_date"].ToString() != "")
                        //        txtfollowup3.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToString("MM/dd/yyyy");
                        //}
                        /****** Commented By TCT ******/

                        //   ddlfollowup3.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                        //for (int i = 0; i < ddlleadtype.Items.Count; i++)
                        //{
                        //    if (leadtype == ddlleadtype.Items[i].Value)
                        //    {
                        //        spanother.Visible = false;
                        //        ddlleadtype.SelectedValue = ddlleadtype.Items[i].Value;
                        //        flag = true;
                        //    }
                        //    else if (leadtype == "Friendly/Family")
                        //    {
                        //        spanother.Visible = false;
                        //        ddlleadtype.SelectedValue = "Referal Family/Friend";
                        //        flag = true;
                        //    }
                        //}
                        //if (flag == false)
                        //{
                        //    spanother.Visible = true;
                        //    txtleadtype.Text = leadtype;
                        //    ddlleadtype.SelectedValue = "Other";
                        //}
                        if (contactpreference.Trim() == "Email")
                        {
                            chbemail.Checked = true;

                        }
                        if (contactpreference.Trim() == "Mail")
                        {
                            chbmail.Checked = true;
                        }
                        //oldtitle = Session["CustomerId"] + "-" + txtestimate_time.Text + " -" + txtcell_ph.Text + " -" + Session["loginid"].ToString(); // added by id to replace
                        ViewState["oldtitle"] = oldtitle;
                        //if(ds.Tables[0].Rows[0]["ProjectManager"]!=null)
                        //{
                        //    txtProjectManager.Text = ds.Tables[0].Rows[0]["ProjectManager"].ToString();
                        //}
                        if (ds.Tables[0].Rows[0]["ProductOfInterest"].ToString() != "")
                        {
                            drpProductOfInterest1.SelectedIndex = Convert.ToInt16(ds.Tables[0].Rows[0]["ProductOfInterest"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["SecondaryProductOfInterest"].ToString() != "")
                        {
                            drpProductOfInterest2.SelectedIndex = Convert.ToInt16(ds.Tables[0].Rows[0]["SecondaryProductOfInterest"].ToString());
                        }

                        lblLastStatus.Text = ds.Tables[0].Rows[0]["LastStatus"].ToString();
                        ddlleadtype.SelectedValue = ds.Tables[0].Rows[0]["LeadSource"].ToString();

                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        txtStartAddress.Value = ds.Tables[3].Rows[0]["CompanyAddress"].ToString();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        txtEndAddress.Text = ds.Tables[2].Rows[0]["ContactAddress"].ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "calcRoute", "calcRoute('" + txtStartAddress.Value + "','" + txtEndAddress.Text + "');", true);
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        ddlSoldJobId.DataSource = ds.Tables[5];
                        ddlSoldJobId.DataTextField = "SoldJobId";
                        ddlSoldJobId.DataValueField = "SoldJobId";
                        ddlSoldJobId.DataBind();
                        ddlSoldJobId.Items.Insert(0, new ListItem("Default All", ""));
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        Session["ButtonCount"] = ds.Tables[4].Rows.Count - 1;
                        for (int row = 0; row < ds.Tables[4].Rows.Count; row++)
                        {
                            if (row == 0)
                            {
                                TextBox txtAddress = (TextBox)UCAddress.FindControl("txtaddress");
                                TextBox txtstate = (TextBox)UCAddress.FindControl("txtstate");
                                TextBox txtcity = (TextBox)UCAddress.FindControl("txtcity");
                                TextBox txtzip = (TextBox)UCAddress.FindControl("txtzip");
                                DropDownList ddlAddressType = (DropDownList)UCAddress.FindControl("DropDownList1");
                                txtAddress.Text = ds.Tables[4].Rows[row]["strAddress"].ToString();
                                txtstate.Text = ds.Tables[4].Rows[row]["strState"].ToString();
                                txtcity.Text = ds.Tables[4].Rows[row]["strCity"].ToString();
                                txtzip.Text = ds.Tables[4].Rows[row]["strZipCode"].ToString();
                                ddlAddressType.Items.FindByValue(ds.Tables[4].Rows[row]["strAddressType"].ToString()).Selected = true;
                            }
                            else
                            {
                                UCAddress objUCAddress = LoadControl("../UserControl/UCAddress.ascx") as UCAddress;
                                objUCAddress.ID = "ucAddress" + row;
                                Label lblAddressUC = (Label)objUCAddress.FindControl("lblAddress");
                                Label lblAddressTypeUC = (Label)objUCAddress.FindControl("lblAddressType");
                                Label lblZipUC = (Label)objUCAddress.FindControl("lblZip");
                                Label lblCityUC = (Label)objUCAddress.FindControl("lblCity");
                                Label lblStateUC = (Label)objUCAddress.FindControl("lblState");
                                lblAddressUC.Text = "Address " + row;
                                lblAddressTypeUC.Text = "Address " + +row + " Type";
                                lblZipUC.Text = "Zip " + row;
                                lblCityUC.Text = "City " + row;
                                lblStateUC.Text = "State " + row;
                                TextBox txtAddressUC = (TextBox)objUCAddress.FindControl("txtaddress");
                                TextBox txtstateUC = (TextBox)objUCAddress.FindControl("txtstate");
                                TextBox txtcityUC = (TextBox)objUCAddress.FindControl("txtcity");
                                TextBox txtzipUC = (TextBox)objUCAddress.FindControl("txtzip");
                                DropDownList ddlAddressTypeUC = (DropDownList)objUCAddress.FindControl("DropDownList1");

                                ddlAddressTypeUC.Items.Add(new ListItem("Select", "Select"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Primary Residence", "Primary Residence"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Business", "Business"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Vacation House", "Vacation House"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Rental", "Rental"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Condo", "Condo"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Apartment", "Apartment"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Mobile Home", "Mobile Home"));
                                ddlAddressTypeUC.Items.Add(new ListItem("Other", "Other"));

                                txtAddressUC.Text = ds.Tables[4].Rows[row]["strAddress"].ToString();
                                txtstateUC.Text = ds.Tables[4].Rows[row]["strState"].ToString();
                                txtcityUC.Text = ds.Tables[4].Rows[row]["strCity"].ToString();
                                txtzipUC.Text = ds.Tables[4].Rows[row]["strZipCode"].ToString();
                                ddlAddressTypeUC.Items.FindByValue(ds.Tables[4].Rows[row]["strAddressType"].ToString()).Selected = true;
                                PlaceHolder myPlaceHolder = (PlaceHolder)panel4.FindControl("myPlaceHolder");
                                myPlaceHolder.Controls.Add(objUCAddress);
                            }
                        }
                    }

                    //if (ds.Tables[5].Rows.Count > 0)
                    //{
                    //    for (int row = 0; row < ds.Tables[4].Rows.Count; row++)
                    //    {
                    //        if(row == 0)
                    //        {
                    //            txtbill_address.InnerText = ds.Tables[5].Rows[0]["strBillingAddress"].ToString();
                    //            selAddressType.Items.FindByValue(ds.Tables[5].Rows[row]["strBillingAddressType"].ToString()).Selected = true;
                    //        }
                    //        else 
                    //        {
                    //            HtmlTextArea ctrlBillingAddressTextArea = new HtmlTextArea();
                    //            ctrlBillingAddressTextArea.ID = "txtbill_address" + (row + 1);
                    //            ctrlBillingAddressTextArea.InnerText = ds.Tables[5].Rows[0]["strBillingAddress"].ToString();

                    //            HtmlSelect ctrlBillingAddressTypeSelect = new HtmlSelect();
                    //            ctrlBillingAddressTypeSelect.ID = "selAddressType" + (row + 1);

                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("", "Select"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Primary Residence", "Primary Residence"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Business", "Business"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Vacation House", "Vacation House"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Rental", "Rental"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Condo", "Condo"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Apartment", "Apartment"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Mobile Home", "Mobile Home"));
                    //            ctrlBillingAddressTypeSelect.Items.Add(new ListItem("Other", "Other"));

                    //            ctrlBillingAddressTypeSelect.Items.FindByValue(ds.Tables[5].Rows[row]["strBillingAddressType"].ToString()).Selected = true;

                    //            CheckBox ctrlBillingAddressCheckBox = new CheckBox();
                    //            ctrlBillingAddressCheckBox.ID = "chkbillingaddress" + row;


                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert(\"" + ex.Message + "\");", true);
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
                    else if (formVars[i].key.Contains("PrimaryRadio"))
                    {
                        dRowNew["Type"] = formVars[i].value;
                        //dtProduct.Rows.Add(dRowNew);
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
        //protected void ddlfollowup3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlfollowup3.SelectedValue == "est>$1000" || ddlfollowup3.SelectedValue == "est<$1000")
        //    {
        //      //  txtfollowup3.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
        //    }
        //    else
        //    {
        //        //txtfollowup3.Text = "";
        //    }
        //}
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
            //            //  dtOtherDEtails.Rows.Add(dRowNew);
            //            //  dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        else if (formVars[i].key.Contains("txtPhone"))
            //        {
            //            dRowNew["PhoneNumber"] = formVars[i].value;
            //            // dtOtherDEtails.Rows.Add(dRowNew);
            //            // dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        else if (formVars[i].key.Contains("selPhoneType"))
            //        {
            //            dRowNew["PhoneType"] = formVars[i].value;
            //            //dtOtherDEtails.Rows.Add(dRowNew);
            //            //dRowNew = dtOtherDEtails.NewRow();
            //        }
            //        else if (formVars[i].key.Contains("txtEMail"))
            //        {
            //            dRowNew["Email"] = formVars[i].value;
            //            dtOtherDEtails.Rows.Add(dRowNew);
            //            dRowNew = dtOtherDEtails.NewRow();
            //        }
            //    }
            //}
            return dtOtherDEtails;
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

        public class NameValue
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string CheckForDuplication(List<NameValue> formVars)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string deserializedJson = jsSerializer.Serialize(formVars);

            //var PropertyInfos = formVars.GetType().GetProperties();
            //foreach (PropertyInfo pInfo in PropertyInfos)
            //{
            //    string propertyName = pInfo.Name; //gets the name of the property
            //    //doSomething(pInfo.GetValue(formVars, null));
            //}

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
            dtFirstName = GetAllValues(formVars, "FirstName", "Text");

            foreach (DataRow drow in dtFirstName.Rows)
            {
                if (drow["FirstName"] == "")
                {

                    strResult = "EmptyName";
                    return strResult;

                }
            }

            int CustomerId = Convert.ToInt32(HttpContext.Current.Session["CustomerId"].ToString());

            DataSet dsCustomerDuplication = new_customerBLL.Instance.CheckCustomerDuplication(dtCusAddress, dtDetails, CustomerId);
            if (dsCustomerDuplication != null)
            {
                strResult = dsCustomerDuplication.Tables[0].Rows[0][0].ToString();
                return strResult;
            }
            else
                return strResult;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Customer objcust = new Customer();
                objcust.id = Convert.ToInt32(Session["CustomerId"].ToString());
                //int primarycont = new_customerBLL.Instance.Searchprimarycontact(txthome_phone.Text, txtcell_ph.Text, txtalt_phone.Text, objcust.id);
                string primarycontact = "";
                objcust.ContactPreference = string.Empty;

                Boolean bitYesNo;
                if (hdnStatus.Value.ToString() == "1")
                    bitYesNo = true;
                else
                    bitYesNo = false;

                objcust.BestTimetocontact = hdnBestTimeToContact.Value;
                objcust.CompetitorsBids = txtCompetitorBids.Text;
                //objcust.BestTimetocontact = txtBestDayToContact.Text;
                objcust.EstTime = txtestimate_time.Text;
                objcust.EstDate = txtestimate_date.Text;

                if (chbemail.Checked)
                {
                    objcust.ContactPreference = chbemail.Text;
                }
                else if (chbcall.Checked)
                {
                    objcust.ContactPreference = chbcall.Text;
                }

                else if (chbtext.Checked)
                {
                    objcust.ContactPreference = chbtext.Text;
                }
                else if (chbmail.Checked)
                {
                    objcust.ContactPreference = chbmail.Text;
                }
                
                if (chbmail.Checked)
                {
                    objcust.ContactPreference = chbmail.Text;
                }
               
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
               
                DateTime EstDate = new DateTime();
                EstDate = string.IsNullOrEmpty(txtestimate_date.Text) ? Convert.ToDateTime("1/1/1753", JGConstant.CULTURE) : Convert.ToDateTime(txtestimate_date.Text, JGConstant.CULTURE);
                objcust.EstDate = EstDate.ToString("MM/dd/yyyy");
                objcust.EstTime = txtestimate_time.Text;
                if (ddlleadtype.SelectedValue == "Other")
                {
                    objcust.Leadtype = txtleadtype.Text;
                }
                else
                {
                    objcust.Leadtype = ddlleadtype.SelectedValue.ToString();
                }
                
                if (drpProductOfInterest1.SelectedIndex != 0)
                {
                    objcust.Productofinterest = Convert.ToInt16(drpProductOfInterest1.SelectedItem.Value);
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

                DataTable dtbleAddress = Session["dtAddress"] as DataTable;
                DataTable dtbleBillingAddress = Session["dtBillingAddress"] as DataTable;
                DataTable dtblePrimary = Session["dtDetails"] as DataTable;
                DataTable dtbleProduct = Session["dtPrimarySecondary"] as DataTable;



                int res = new_customerBLL.Instance.UpdateSrCustomer(objcust, dtbleAddress, dtbleBillingAddress, dtblePrimary, dtbleProduct, bitYesNo);

               

                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                if (res > 0)
                {
                    string newstatus;
                    /* Commented by Tct*/
                    //if (ddlfollowup3.SelectedValue != "0")
                    //    newstatus = ddlfollowup3.SelectedItem.Text;
                    //else
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
                        //string gcontent = "Name: " + objcust.customerNm + " , Cell Phone: " + txtcell_ph.Text + ", Alt. phone: " + txtalt_phone.Text + ", Email: " + txtEmail.Text + ",Service: " + txtService.Text + ",Status: " + newstatus;
                        string gcontent = "Name: " + objcust.customerNm;//TCT
                        //string gaddress = txtaddress.Text + " " + txtcity.Text + "," + txtstate.Text + " -" + txtzip.Text;
                        string gaddress = ""; //TCT


                        new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(Session["CustomerId"].ToString()), DateTime.Now, "", UserId, true, 0, "", 0, Convert.ToInt32(drpProductOfInterest1.SelectedValue));
                       

                        //if (GoogleCalendarEvent.DeleteEvent(objcust.id.ToString(), gtitle, gcontent, gaddress, datetime, datetime.AddHours(1), AdminId))
                        //{
                        //    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), objcust.id.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting the Customer');", true);
                        //}
                        //if (AdminId != objcust.Addedby)
                        //{
                        //    if (GoogleCalendarEvent.DeleteEvent(objcust.id.ToString(), gtitle, gcontent, gaddress, datetime, datetime.AddHours(1), objcust.Addedby))
                        //    {
                        //        GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), objcust.id.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), objcust.Addedby);
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting the Customer');", true);
                        //    }
                        //}
                        //if (GoogleCalendarEvent.DeleteEvent(objcust.id.ToString(), gtitle, gcontent, gaddress, datetime, datetime.AddHours(1), JGConstant.CustomerCalendar))
                        //{
                        //    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), objcust.id.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting the Customer');", true);
                        //}
                    }
                    else
                    {
                        //GoogleCalendarEvent.DeleteEvent(objcust.id.ToString(), "", "", "", DateTime.Now, DateTime.Now, AdminId);
                        //if (Session["AdminUserId"] == null)
                        //{
                        //    GoogleCalendarEvent.DeleteEvent(objcust.id.ToString(), "", "", "", DateTime.Now, DateTime.Now, objcust.Addedby);
                        //}
                        //GoogleCalendarEvent.DeleteEvent(objcust.id.ToString(), "", "", "", DateTime.Now, DateTime.Now, JGConstant.CustomerCalendar);
                    }


                    ResetFormControlValues(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Customer Updated successfully');", true);
                    Response.Redirect("home.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in Updating the Customer');", true);
                    RecreateControls("ContentPlaceHolder1%24ucAddress", (int)Session["ButtonCount"]);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Try Again');", true);
                RecreateControls("ContentPlaceHolder1%24ucAddress", (int)Session["ButtonCount"]);
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

        private void ResetFormControlValues(Control parent)
        {
            try
            {
                //imgmap.ImageUrl = "";
                //GridViewLocationPic.DataSource = null;
                //GridViewLocationPic.DataBind();
                GridViewSoldJobs.DataSource = null;
                GridViewSoldJobs.DataBind();
                //LinkButtonmap1.Text = "";
                //LinkButtonmap2.Text = "";
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
                                ((Image)c).ImageUrl = "";
                                break;

                        }
                    }

                }
            }
            catch { }

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
                //GridView grdoldjobs = sender as GridView;
                //grdoldjobs.PageIndex = e.NewPageIndex;
                //grdoldjobs.DataSource = (DataSet)ViewState["soldjobs"];
                //grdoldjobs.DataBind();
                GridViewSoldJobs.PageIndex = e.NewPageIndex;
                GridViewSoldJobs.DataSource = (DataSet)ViewState["soldjobs"];
                GridViewSoldJobs.DataBind();
            }
        }

        protected void GridViewSoldJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSoldJobDate = (Label)e.Row.FindControl("lblSoldJobDate");
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
                
                ///#- Commented By Shabbir - For coloring Sold Job Quote Date Red
                /*if (ColorFlag == JGConstant.ZERO)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    ColorFlag = JGConstant.ONE;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    ColorFlag = JGConstant.ZERO;
                }*/
                ///#- Comment End By Shabbir - For coloring Sold Job Quote Date Red

                DataRowView lDrRowData = (DataRowView)e.Row.DataItem;
                lblSoldJobDate.Text = Convert.ToDateTime( lDrRowData["DateSold"].ToString()).ToString("dd-MM-yyyy");
                if (lDrRowData["SoldJobID"].ToString().Contains("SJ"))
                {
                    lblSoldJobDate.Text = Convert.ToDateTime(lDrRowData["DateSold"].ToString()).ToString("dd-MM-yyyy") + "-<br/>" + "<span style='color:#ff0000'>"+ Convert.ToDateTime(lDrRowData["DateSold"].ToString()).ToString("dd-MM-yyyy");
                }

                DropDownList ddlfollowup3 = (DropDownList)e.Row.FindControl("ddlfollowup3");
                // * DEc18 test            

                //ddlfollowup3.Items.Clear();
                //ddlfollowup3.Items.Insert(0, new ListItem("Select", "0"));

                //ddlfollowup3.Items.Insert(0, new ListItem("Set", "1"));

                //ddlfollowup3.Items.Insert(0, new ListItem("Prospect", "2"));

                //ddlfollowup3.Items.Insert(0, new ListItem("est>$1000", "3"));

                //ddlfollowup3.Items.Insert(0, new ListItem("est<$1000", "4"));

                //ddlfollowup3.Items.Insert(0, new ListItem("sold>$1000", "5"));

                //ddlfollowup3.Items.Insert(0, new ListItem("Closed (not sold)", "6"));

                //ddlfollowup3.Items.Insert(0, new ListItem("Closed (sold)", "7"));

                //ddlfollowup3.Items.Insert(0, new ListItem("Rehash", "8"));
                //ddlfollowup3.Items.Insert(0, new ListItem("cancelation-no rehash", "9"));
                //ddlfollowup3.Items.Insert(0, new ListItem("Follow up", "10"));

                // * DEc18 test
                DataSet dsStatus = new DataSet();

                //ds = shuttersBLL.Instance.GetProductLineItems(customerId, Status);
                dsStatus = new_customerBLL.Instance.GetAllStatus();

                if (dsStatus != null)
                {
                    if (dsStatus.Tables[0].Rows.Count > 0)
                    {
                        //GridViewSoldJobs.DataSource = dsStatus;
                        //GridViewSoldJobs.DataBind();
                        ddlfollowup3.DataSource = dsStatus;
                        ddlfollowup3.DataTextField = "StatusName";
                        ddlfollowup3.DataValueField = "StatusId";
                        ddlfollowup3.DataBind();
                        Label StatusId = (Label)e.Row.FindControl("lblStatusId");
                        ddlfollowup3.SelectedValue = StatusId.Text;

                    }
                    else
                    {
                        GridViewSoldJobs.DataSource = null;
                    }
                }
                LinkButton lnkSoldJobId = (LinkButton)e.Row.FindControl("lnkSoldJobDetails");
                string strDiv = "";
                DataSet dsTeamMembers = new DataSet();
                dsTeamMembers = new_customerBLL.Instance.GetJobTeamMembers(Convert.ToInt32(Session["CustomerId"]), lDrRowData["SoldJobId"].ToString());
                if (dsTeamMembers != null)
                {
                    for (int i = 0; i < dsTeamMembers.Tables[0].Rows.Count; i++)
                    {
                        strDiv += "<li><span class='clsCustomerIdLink'><a href='Customer_Profile.aspx?CustomerId=" + dsTeamMembers.Tables[0].Rows[i]["userid"] + "'>" + dsTeamMembers.Tables[0].Rows[i]["userid"] + "</a></span><span> - " + dsTeamMembers.Tables[0].Rows[i]["UserType"]+ " - " + dsTeamMembers.Tables[0].Rows[i]["Username"] + " </span></li>";
                      //  strDiv += "<option value='"+ dsTeamMembers.Tables[0].Rows[i]["userid"] + "'>" + dsTeamMembers.Tables[0].Rows[i]["userid"] + " - " + dsTeamMembers.Tables[0].Rows[i]["Username"] + " </option>";
                    }
                }

                strDiv = "<div class='popover drop_popover' data-content=\"<ul>" + strDiv + "</ul>\">Team Members</div>";

                HtmlGenericControl div = e.Row.FindControl("ddlDropDown") as HtmlGenericControl;
                div.InnerHtml = strDiv + "<script type='text/javascript'>$('.popover').webuiPopover({ constrains: 'horizontal', multi: false, placement: 'bottom', width: 200 });</script>";
                //this.Page.RegisterStartupScript("ShowPopup", "<script type='text/javascript'>$('.popover').webuiPopover({ constrains: 'horizontal', multi: false, placement: 'bottom', width: 200 });</script>");
                ClientScript.RegisterStartupScript(this.GetType(),"ShowPopup", "<script type='text/javascript'>$('.popover').webuiPopover({ constrains: 'horizontal', multi: false, placement: 'bottom', width: 200 });</script>");
                //string strDiv = "";
                //for(int i = 0; i < dsStatus.Tables[0].Rows.Count - 1; i++)
                //{
                //    strDiv += "<li><span class='clsCustomerIdLink'><a href='Customer_Profile.aspx?CustomerId=195'>" + dsStatus.Tables[0].Rows[i][0] + "</a></span><span> - " + dsStatus.Tables[0].Rows[i][1] + " </span></li>";
                //}

                //HtmlGenericControl div = e.Row.FindControl("ddlDropDown") as HtmlGenericControl;
                //div.InnerHtml = strDiv;

                // greyout();
            }
            //if (e.Row.RowType == DataControlRowType.DataRow && GridViewSoldJobs.EditIndex == e.Row.RowIndex)
            //{
            //    DropDownList ddlfollowup3 = (DropDownList)e.Row.FindControl("ddlfollowup3");
            //    ddlfollowup3.Items.Clear();
            //    ddlfollowup3.Items.Insert(0, new ListItem("Select", "0"));
            //    ddlfollowup3.Items.Add("Set");
            //    ddlfollowup3.Items.Add("Prospect");
            //    ddlfollowup3.Items.Add("est>$1000");
            //    ddlfollowup3.Items.Add("est<$1000");
            //    ddlfollowup3.Items.Add("sold>$1000");
            //    ddlfollowup3.Items.Add("sold<$1000");
            //    ddlfollowup3.Items.Add("Closed (not sold)");
            //    ddlfollowup3.Items.Add("Closed (sold)");
            //    ddlfollowup3.Items.Add("Rehash");
            //    ddlfollowup3.Items.Add("cancelation-no rehash");
            //    //ddlfollowup3.Items.Add("Follow up");
            //    // greyout();
            //}
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            string note = txtAddNotes.Text.Trim();
            new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(Session["CustomerId"].ToString()), DateTime.Now, note, UserId, true, 0);
            txtAddNotes.Text = string.Empty;
            bindGrid();
        }

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {

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

        protected void lnkSoldJobDetails_Click(object sender, EventArgs e)
        {
            LinkButton lnksoldjobid = sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnksoldjobid.Parent.Parent;
            //Newly Added .....
            HiddenField hdnProductTypeId = (HiddenField)gr.FindControl("hdnProductTypeId");
            HiddenField hdnproductid = (HiddenField)gr.FindControl("hdnproductid");
            int customerId = Convert.ToInt16(lnksoldjobid.CommandArgument);
            int productId = Convert.ToInt16(hdnproductid.Value); //Convert.ToInt16(dssoldJobs.Tables[0].Rows[0]["EstimateId"].ToString());

            Response.Redirect("Custom.aspx?ProductTypeId=" + Convert.ToInt16(hdnProductTypeId.Value) + "&ProductId=" + productId + "&CustomerId=" + customerId);

        }

        [System.Web.Services.WebMethod]
        public static string GetPrimaryContact()
        {
            string JsonResult = string.Empty;
            DataSet dds = new DataSet();
            dds = UserBLL.Instance.fetchPrimaryContactDetails(Convert.ToInt32(HttpContext.Current.Session["CustomerId"].ToString()));
            //dds = UserBLL.Instance.fetchPrimaryContactDetails(1);
            DataTable mainTable = new DataTable();
            DataTable childTable = new DataTable();

            mainTable = dds.Tables[0];
            childTable = dds.Tables[1];

            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("CustomerId");
            dtTable.Columns.Add("IsPrimaryContact");
            dtTable.Columns.Add("strContactType");
            dtTable.Columns.Add("FName");
            dtTable.Columns.Add("LName");
            dtTable.Columns.Add("PhoneNumber");
            dtTable.Columns.Add("PhoneType");
            dtTable.Columns.Add("EMail");

            for (int i = 0; i < mainTable.Rows.Count; i++)
            {
                bool boolFirstRow = true;

                for (int j = 0; j < childTable.Rows.Count; j++)
                {
                    DataRow dRow = dtTable.NewRow();
                    if (mainTable.Rows[i]["strContactType"].ToString() == childTable.Rows[j]["strContactType"].ToString()
                        && mainTable.Rows[i]["FName"].ToString() == childTable.Rows[j]["FName"].ToString() && mainTable.Rows[i]["LName"].ToString() == childTable.Rows[j]["LName"].ToString())
                    {
                        if (boolFirstRow == true)
                        {
                            dRow["IsPrimaryContact"] = childTable.Rows[i]["IsPrimaryContact"];
                            dRow["strContactType"] = childTable.Rows[i]["strContactType"];
                            dRow["FName"] = childTable.Rows[i]["FName"];
                            dRow["LName"] = childTable.Rows[i]["LName"];
                        }
                        else
                        {
                            dRow["IsPrimaryContact"] = "";
                            dRow["strContactType"] = "";
                            dRow["FName"] = "";
                            dRow["LName"] = "";
                        }
                        dRow["CustomerId"] = childTable.Rows[i]["CustomerId"];
                        dRow["PhoneNumber"] = childTable.Rows[i]["PhoneNumber"];
                        dRow["PhoneType"] = childTable.Rows[i]["PhoneType"];
                        dRow["EMail"] = childTable.Rows[i]["EMail"];
                        dtTable.Rows.Add(dRow);
                        boolFirstRow = false;
                    }
                }
            }
            JsonResult = JsonConvert.SerializeObject(dtTable) + "^@" + JsonConvert.SerializeObject(dds.Tables[2]) + "^@" + JsonConvert.SerializeObject(dds.Tables[3]) + "^@" + JsonConvert.SerializeObject(dds.Tables[4]) + "^@" + JsonConvert.SerializeObject(dds.Tables[5]);
            return JsonResult;
        }
        [System.Web.Services.WebMethod]
        public static string BindImage()
        {
            string JsonResult = string.Empty;
            DataSet dds = new DataSet();
            dds = UserBLL.Instance.BindJobImage();
            JsonResult = JsonConvert.SerializeObject(dds.Tables[0]);
            return JsonResult;
        }
        //get Location Image
        [System.Web.Services.WebMethod]
        public static string GetLocationImage(string strJobSoldId)
        {
            string result = string.Empty;
            int CustomerId = Convert.ToInt32(HttpContext.Current.Session["CustomerId"]);
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.FetchLocationImage(CustomerId, strJobSoldId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["LocationPicture"].ToString();
            }
            return result;

        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> LoadAddress(string prefixText)
        {
            int CustomerId = int.Parse(HttpContext.Current.Session["CustomerId"].ToString());
            List<string> EmpNames = new List<string>();
            //string query = "UDP_BindEndAddress";
            //SqlParameter[] param = new SqlParameter[1];
            //param[0] = new SqlParameter("@EndAddress", prefixText + "%");
            DataSet ds = UserBLL.Instance.BindEndAddress("%" + prefixText + "%", CustomerId);
            //DataTable dt = Classes.Common.GetDBTable(query, param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string selectedItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["ContactAddress"].ToString(), dr["intAddressId"].ToString());
                EmpNames.Add(selectedItem);
            }
            EmpNames.Add("{\"First\":\"Street View\",\"Second\":\"-1\"}");
            return EmpNames;
        }
    }
}
