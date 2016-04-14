﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.Text.RegularExpressions;
using JG_Prospect.Common;
using System.Configuration;
using JG_Prospect.Common.modal;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Ionic.Zip;
using Saplin.Controls;
namespace JG_Prospect.Installer
{
    public partial class InstallerHome : System.Web.UI.Page
    {
        public string GridViewSortExpression
        {
            get
            {
                return ViewState[JGConstant.SortExpression] == null ? JGConstant.Sorting_ReferenceId : ViewState[JGConstant.SortExpression] as string;
            }
            set
            {
                ViewState[JGConstant.SortExpression] = value;
            }
        }
        protected DataSet PageDataset
        {
            get { return ViewState["PageDataSet"] != null ? ((DataSet)ViewState["PageDataSet"]) : new DataSet(); }
            set { ViewState["PageDataSet"] = value; }
        }
        protected DataSet ProductDataset
        {
            get { return ViewState["ProductDataset"] != null ? ((DataSet)ViewState["ProductDataset"]) : new DataSet(); }
            set { ViewState["ProductDataset"] = value; }
        }
        protected DataSet RequestMaterialDataSet
        {
            get { return ViewState["RequestMaterialDataSet"] != null ? ((DataSet)ViewState["RequestMaterialDataSet"]) : new DataSet(); }
            set { ViewState["RequestMaterialDataSet"] = value; }
        }
        protected String JobID {
            get { return ViewState["JobID"] != null ? ViewState["JobID"].ToString() : ""; }
            set { ViewState["JobID"] = value; }
        }
        protected Int32 CustomerID
        {
            get { return ViewState["CustomerID"] != null ? Convert.ToInt32(ViewState["CustomerID"].ToString()) : 0; }
            set { ViewState["CustomerID"] = value; }
        }
        protected Int32 InstallerID;
        public String GridViewSortDirection
        {
            get
            {
                if (ViewState[JGConstant.SortDirection] == null)
                    ViewState[JGConstant.SortDirection] = JGConstant.Sorting_SortDirection_DESC;

                return (string)ViewState[JGConstant.SortDirection];
            }
            set { ViewState[JGConstant.SortDirection] = value; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            InstallerID = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            DataSet ds = InstallUserBLL.Instance.GetJobsForInstaller();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                DataView sortedView = new DataView(dt);
                sortedView.Sort = GridViewSortExpression + " " + GridViewSortDirection;
                grdInstaller.DataSource = sortedView;
                grdInstaller.DataBind();
            }
        }
        protected void lblSignOffDocument_Click(object sender, EventArgs e)
        {

            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string fileName = "InstallationCompletionForm.pdf";

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
            Response.TransmitFile("~/CustomerDocs/Pdfs/" + fileName);
            Response.End();
        }
        protected void lnkAvailability_Click(object sender, EventArgs e)
        {
            LinkButton lnkAvailability = (LinkButton)sender;
            GridViewRow grdInstallerRow = (GridViewRow)lnkAvailability.Parent.Parent;
            Label lblReferenceId = (Label)grdInstallerRow.FindControl("lblReferenceId");
            HiddenField hdnJobSequenceId = (HiddenField)grdInstallerRow.FindControl("hdnJobSequenceId");
            ViewState[ViewStateKey.Key.ReferenceId.ToString()] = lblReferenceId.Text;
            ViewState[ViewStateKey.Key.JobSequenceId.ToString()] = Convert.ToInt16(hdnJobSequenceId.Value);

            
            DataSet ds = InstallUserBLL.Instance.GetInstallerAvailability(lblReferenceId.Text.Trim(), InstallerID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPrimary.Text = ds.Tables[0].Rows[0]["Primary"].ToString();
                lblSecondary1.Text = ds.Tables[0].Rows[0]["Secondary1"].ToString();
                lblSecondary2.Text = ds.Tables[0].Rows[0]["Secondary2"].ToString();
            }
            else
            {
                txtPrimary.Text = "";
                txtSecondary1.Text = "";
                txtSecondary2.Text = "";
                lblPrimary.Text = "";
                lblSecondary1.Text = "";
                lblSecondary2.Text = "";
            }

            mpe.Show();
        }


        protected void btnSet_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)btnSet.Parent.Parent;
           // GridView grdInstaller = (GridView)ContentPlaceHolder1.FindControl("grdInstaller");
            string primary = string.Empty, secondary1 = string.Empty, secondary2 = string.Empty;
            if (txtPrimary.Text.Trim() != string.Empty)
            {
                primary = InstallUserBLL.Instance.AddHoursToAvailability(Convert.ToDateTime(txtPrimary.Text.Trim(), JGConstant.CULTURE));
            }
            if (txtSecondary1.Text.Trim() != string.Empty)
            {
                secondary1 = InstallUserBLL.Instance.AddHoursToAvailability(Convert.ToDateTime(txtSecondary1.Text.Trim(), JGConstant.CULTURE));
            }
            if (txtSecondary2.Text.Trim() != string.Empty)
            {
                secondary2 = InstallUserBLL.Instance.AddHoursToAvailability(Convert.ToDateTime(txtSecondary2.Text.Trim(), JGConstant.CULTURE));
            }

            Availability a = new Availability();
            a.ReferenceId = ViewState[ViewStateKey.Key.ReferenceId.ToString()].ToString();
            //a.JobSequenceId = Convert.ToInt16(ViewState[ViewStateKey.Key.JobSequenceId.ToString()].ToString());
            a.InstallerId = InstallerID;
            a.Primary = primary;
            a.Secondary1 = secondary1;
            a.Secondary2 = secondary2;

            InstallUserBLL.Instance.AddEditInstallerAvailability(a);
            BindGrid();
        }

        protected void lnkJobPackets_Click(object sender, EventArgs e)
        {
            string path = "";
            string Extention = "";
            LinkButton lnkJobPackets = sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnkJobPackets.Parent.Parent;

            //#- Shabbir Kanchwala. Added below 4 lines because Availability column functionality is moved out of grid.
            Label lblReferenceID = (Label)gr.FindControl("lblReferenceId");
            HiddenField hdnJobSequenceID = (HiddenField)gr.FindControl("hdnJobSequenceId");
            hdnJobSeqID.Value = hdnJobSequenceID.Value;
            hdnReferenceID.Value = lblReferenceID.Text;
            
            //#- Shabbir Kanchwala: This code will bind the custom material list on Job Packet Popup
            JobID = ((Label)gr.FindControl("lblCustomerIdJobId")).Text.ToString().Replace("&", "-").Replace(" ", "");
            CustomerID = Convert.ToInt32(JobID.Replace("C", "").Split('-')[0].ToString());
            BindCustomMaterialList();

            //Label lblCustomerIdJobId = (Label)gr.FindControl("lblCustomerIdJobId");
            //string[] Id = lblCustomerIdJobId.Text.Trim().Split('&');
            //string customerString = Id[0].Trim();
            //string resultString = Regex.Match(customerString, @"\d+").Value;
            //int customerId = Int32.Parse(resultString);
            //int productId = Convert.ToInt16(hdnproductid.Value);

            //int productTypeId = Convert.ToInt16(hdnProductTypeId.Value);
           // Label lblCategory = (Label)gr.FindControl("lblCategory");
           string []s=lnkJobPackets.Text.Trim().Split('-');
           string soldJobId = s[0] + "-" + s[1];
           //soldJobId = ViewStateKey.Key.SoldJobNo.ToString();
           DataSet ds = new DataSet();
           ds = new_customerBLL.Instance.GetCustomerJobPackets(soldJobId);
           //if (ds.Tables.Count > 0)
           //{
           //    if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
           //    {
           //        Extention = Path.GetExtension(Convert.ToString(ds.Tables[0].Rows[0][1]));
           //        if (Extention == ".pdf")
           //        {
           //            ds.Tables[0].Rows[0][1] = "Pdfs/pdf.jpg";
           //        }
           //    }
           //}
            
           Gridviewdocs.DataSource = ds;
           Gridviewdocs.DataBind();
           ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlay();", true);
            //ResponseHelper.Redirect("ZipJobPackets.aspx?productId=" + productId + "&productTypeId=" + productTypeId + "&customerId=" + customerId, "_blank", "menubar=0,width=605px,height=630px");
           //ResponseHelper.Redirect("ZipJobPackets.aspx?" + ViewStateKey.Key.SoldJobNo.ToString() +"=" + soldJobId, "_blank", "menubar=0,width=605px,height=630px");
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string UserName = "JG";
            ErrorMessage.InnerHtml = "";   // debugging only
            var filesToInclude = new System.Collections.Generic.List<String>();
            String sMappedPath = Server.MapPath("~/CustomerDocs");
            foreach (GridViewRow gvr in Gridviewdocs.Rows)
            {
                CheckBox chkbox = gvr.FindControl("checkbox1") as CheckBox;
                Label lbl = gvr.FindControl("labelfile") as Label;
                if (chkbox != null && lbl != null)
                {
                    if (chkbox.Checked)
                    {
                        ErrorMessage.InnerHtml += String.Format("adding file: {0}<br/>\n", lbl.Text);
                        filesToInclude.Add(System.IO.Path.Combine(sMappedPath, lbl.Text));
                    }
                }
            }

            if (filesToInclude.Count == 0)
            {
                ErrorMessage.InnerHtml += "You did not select any files?<br/>\n";
            }
            else
            {
                string pass = "0";
                Response.Clear();
                Response.BufferOutput = false;

                System.Web.HttpContext c = System.Web.HttpContext.Current;
                String ReadmeText = String.Format("README.TXT\n\nHello!\n\n" +
                                                 "This is a zip file that was dynamically generated at {0}\n" +
                                                 "by an ASP.NET Page running on the machine named '{1}'.\n" +
                                                 "The server type is: {2}\n" +
                                                 "The password used: '{3}'\n" +
                                                 "Encryption: {4}\n",
                                                 System.DateTime.Now.ToString("G"),
                                                 System.Environment.MachineName,
                                                 c.Request.ServerVariables["SERVER_SOFTWARE"],
                                                 "Nome",
                                                 (pass == "" || pass == string.Empty) ? EncryptionAlgorithm.WinZipAes256.ToString() : "None"
                                                 );
                string archiveName = String.Format("archive-{0}.zip", UserName + " " + DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "inline; filename=\"" + archiveName + "\"");

                using (ZipFile zip = new ZipFile())
                {
                    // the Readme.txt file will not be password-protected.
                    zip.AddEntry("Readme.txt", ReadmeText, Encoding.Default);
                    //if (!String.IsNullOrEmpty(tbPassword.Text))
                    //{
                    //    zip.Password = tbPassword.Text;
                    //    if (chkUseAes.Checked)
                    //        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    //}

                    // filesToInclude is a string[] or List<String>
                    zip.AddFiles(filesToInclude, "files");

                    zip.Save(Response.OutputStream);
                }
                Response.Close();
            }
        }

        protected void Gridviewdocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");
                string s = DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Contains(".pdf") == true)
                {
                    img.ImageUrl = "~/CustomerDocs/Pdfs/pdf.jpg";
                    img.Height = 90;
                    img.Width = 60;
                }
                else
                {
                    img.ImageUrl = DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Replace("LocationPics/../CustomerDocs/CustomerDocs", "/CustomerDocs");
                }
                if (DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Contains("VendorQuotes") == true)
                {
                    //img.ImageUrl = "~/CustomerDocs/VendorQuotes/Quote.jpg";
                    //img.Height = 90;
                    //img.Width = 60;
                }
            }
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

        protected void grdInstaller_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkAvailability = (LinkButton)e.Row.FindControl("lnkAvailability");
                Label lblReferenceId = (Label)e.Row.FindControl("lblReferenceId");
                HiddenField hdnColour = (HiddenField)e.Row.FindControl("hdnColour");
                HiddenField hdnJobSequenceId = (HiddenField)e.Row.FindControl("hdnJobSequenceId");
                
                DataSet ds = InstallUserBLL.Instance.GetInstallerAvailability(lblReferenceId.Text.Trim(), InstallerID);
              
                string availability = string.Empty;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    availability = "Primary: " + ds.Tables[0].Rows[0]["Primary"].ToString() + Environment.NewLine + "Secondary1: " + ds.Tables[0].Rows[0]["Secondary1"].ToString() + Environment.NewLine + "Secondary2: " + ds.Tables[0].Rows[0]["Secondary2"].ToString();
                    lnkAvailability.ToolTip = availability;
                }
                if (hdnColour.Value.ToString() == JGConstant.COLOR_RED)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void grdInstaller_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            if (GridViewSortDirection == JGConstant.Sorting_SortDirection_ASC)
            {
                GridViewSortDirection = JGConstant.Sorting_SortDirection_DESC;
            }
            else
            {
                GridViewSortDirection = JGConstant.Sorting_SortDirection_ASC;
            };
            BindGrid();
        }

        #region "Shabbir Code"
        protected void lnkAvailJobPckt_Click(object sender, EventArgs e)
        {
            ViewState[ViewStateKey.Key.ReferenceId.ToString()] = hdnReferenceID.Value;
            ViewState[ViewStateKey.Key.JobSequenceId.ToString()] = Convert.ToInt16(hdnJobSeqID.Value);

            DataSet ds = InstallUserBLL.Instance.GetInstallerAvailability(hdnReferenceID.Value.Trim(), InstallerID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPrimary.Text = ds.Tables[0].Rows[0]["Primary"].ToString();
                lblSecondary1.Text = ds.Tables[0].Rows[0]["Secondary1"].ToString();
                lblSecondary2.Text = ds.Tables[0].Rows[0]["Secondary2"].ToString();
            }
            else
            {
                txtPrimary.Text = "";
                txtSecondary1.Text = "";
                txtSecondary2.Text = "";
                lblPrimary.Text = "";
                lblSecondary1.Text = "";
                lblSecondary2.Text = "";
            }

            mpe.Show();
        }
        private void BindCustomMaterialList()
        {
            ProductDataset = UserBLL.Instance.GetAllProducts();
            ddlCategoryH.DataSource = ProductDataset;
            ddlCategoryH.DataTextField = "ProductName";
            ddlCategoryH.DataValueField = "ProductId";
            ddlCategoryH.DataBind();

            PageDataset = CustomBLL.Instance.GetCustomMaterialList(JobID, CustomerID);
            lstCustomMaterialList.DataSource = PageDataset.Tables[0];
            lstCustomMaterialList.DataBind();

            RequestMaterialDataSet = CustomBLL.Instance.GetRequestMaterialList(JobID, CustomerID, InstallerID);
            lstRequestMaterial.DataSource = RequestMaterialDataSet.Tables[0];
            lstRequestMaterial.DataBind();
        }

        protected void lstCustomMaterialList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                //DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
                //ddlCategory.DataSource = ProductDataset;
                //ddlCategory.DataTextField = "ProductName";
                //ddlCategory.DataValueField = "ProductId";
                //ddlCategory.DataBind();
                ////ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0)"));



                DataRowView lDrView = (DataRowView)e.Item.DataItem;

                int lProdCatID = Convert.ToInt32(lDrView["ProductCatID"]);
                //ddlCategory.SelectedValue = lProdCatID.ToString();
                GridView grdProdLines = (GridView)e.Item.FindControl("grdProdLines");
                DataView lDvMaterialList = new DataView(PageDataset.Tables[1], "ProductCatID=" + lProdCatID, "id asc", DataViewRowState.OriginalRows);

                grdProdLines.DataSource = lDvMaterialList;
                grdProdLines.DataBind();
            }
        }
        protected void btnAddProdLines_Click(object sender, EventArgs e)
        {
            CustomMaterialList cm = new CustomMaterialList();
            cm.ProductCatId = Convert.ToInt32(ddlCategoryH.SelectedValue);
            cm.MaterialList = "";
            cm.Id = 0;
            cm.VendorName = "";
            cm.VendorEmail = "";
            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            cm.RequestStatus = 0;
            cm.InstallerID = InstallerID;
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, JobID);
            BindCustomMaterialList();
        }

        #region "Material List"
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCategory = ((DropDownList)sender);
            if (!IsDuplicateProdCat(Convert.ToInt32(ddlCategory.SelectedValue)))
            {
                SaveMaterialList(sender, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Selected Product Category already exists');", true);
            }

        }

        protected void grdProdLines_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                //DataSet ds = UserBLL.Instance.GetAllProducts();
                //ddlCategory.DataSource = ds;
                //ddlCategory.DataTextField = "ProductName";
                //ddlCategory.DataValueField = "ProductId";
                //ddlCategory.DataBind();
                //ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0)"));


                //DropDownCheckBoxes ddlVendorCategory = (DropDownCheckBoxes)e.Row.FindControl("ddlVendorName");
                //DataSet dsVendorCategory = GetVendorCategories();
                //ddlVendorCategory.DataSource = GetVendorCategories();
                //ddlVendorCategory.DataSource = dsVendorCategory;
                //ddlVendorCategory.DataTextField = "VendorCategoryNm";
                //ddlVendorCategory.DataValueField = "VendorCategpryId";
                //ddlVendorCategory.DataBind();
                ////ddlVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                ////ddlVendorCategory.SelectedIndex = 0;

                //DataRowView lDr = (DataRowView)e.Row.DataItem;
                //String lVendorIds = lDr["VendorIds"].ToString();
                //foreach (System.Web.UI.WebControls.ListItem lItem in ddlVendorCategory.Items)
                //{
                //    foreach (string lVendorId in lVendorIds.Split(','))
                //    {
                //        if (lItem.Value == lVendorId)
                //        {
                //            lItem.Selected = true;
                //        }
                //    }
                //}


            }
        }
        protected void txtSkuPartNo_TextChanged(object sender, EventArgs e)
        {
            SaveMaterialList(sender, e);
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            SaveMaterialList(sender, e);
        }

        protected void txtQTY_TextChanged(object sender, EventArgs e)
        {
            SaveMaterialList(sender, e);
        }

        protected void txtUOM_TextChanged(object sender, EventArgs e)
        {
            SaveMaterialList(sender, e);
        }

        private void SaveMaterialList(object sender, EventArgs e)
        {
            //#-This line is not required. 
            string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(JobID);//, productTypeId, estimateId);




            GridViewRow r = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            if (sender.GetType().Equals(typeof(LinkButton)))
            {
                r = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            }
            else if (sender.GetType().Equals(typeof(TextBox)))
            {
                r = ((GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent);
            }
            else if (sender.GetType().Equals(typeof(DropDownCheckBoxes)))
            {
                r = ((GridViewRow)((DropDownCheckBoxes)sender).Parent.Parent.Parent.Parent);
            }
            else if (sender.GetType().Equals(typeof(DropDownList)))
            {
                DropDownList ddlProductCat = ((DropDownList)sender);
                HiddenField lhdnCurrentProdCat = (HiddenField)((DropDownList)sender).Parent.FindControl("hdnProductCatID");
                int lProdCat = Convert.ToInt32(ddlProductCat.SelectedValue);
                CustomBLL.Instance.UpdateProductTypeInMaterialList(lProdCat, Convert.ToInt32(lhdnCurrentProdCat.Value), JobID);
                BindCustomMaterialList();
                return;

            }
            else
            {
                return;
            }

            CustomMaterialList cm = new CustomMaterialList();

            TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
            TextBox txtLine = (TextBox)r.FindControl("txtLine");
            TextBox txtSkuPartNo = (TextBox)r.FindControl("txtSkuPartNo");
            TextBox txtDescription = (TextBox)r.FindControl("txtDescription");
            TextBox txtQTY = (TextBox)r.FindControl("txtQTY");
            TextBox txtUOM = (TextBox)r.FindControl("txtUOM");
            TextBox txtExtended = (TextBox)r.FindControl("txtExtended");
            
            //TextBox txtMaterialCost = (TextBox)r.FindControl("txtMaterialCost");
            
            

            HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
            HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
            HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
            HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
            HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
            HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");

            //cm.ProductCatId = productTypeId;
            cm.Id = hdnMaterialListId.Value != "" ? Convert.ToInt16(hdnMaterialListId.Value) : 0;

            cm.Line = txtLine.Text;
            cm.JGSkuPartNo = txtSkuPartNo.Text;
            cm.MaterialList = txtDescription.Text;
            cm.Quantity = txtQTY.Text != "" ? Convert.ToInt32(txtQTY.Text) : 0;
            cm.UOM = txtUOM.Text;
            //cm.MaterialCost = txtMaterialCost.Text != "" ? Convert.ToInt32(txtMaterialCost.Text) : 0;
            //cm.extend = txtExtended.Text;

            if (status == "C") //mail was already sent to vendor categories
            {
                cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                cm.EmailStatus = JGConstant.EMAIL_STATUS_VENDORCATEGORIES;
            }
            else // mail was not sent to vendor categories
            {
                cm.VendorName = "";
                cm.VendorEmail = "";
                cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            }
            cm.RequestStatus = 1;

            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, JobID);//,productTypeId,estimateId);

        }


        private bool IsDuplicateProdCat(Int32 pProdCatID)
        {
            bool lRetVal = false;
            for (int i = 0; i < PageDataset.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(PageDataset.Tables[0].Rows[i]["ProductCatID"].ToString()) == pProdCatID)
                {
                    lRetVal = true;
                }
            }
            return lRetVal;
        }

        protected void lnkAddProdCat_Click(object sender, EventArgs e)
        {
            CustomMaterialList cm = new CustomMaterialList();
            cm.ProductCatId = -1;
            cm.MaterialList = "";
            cm.Id = 0;
            cm.VendorName = "";
            cm.VendorEmail = "";
            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, JobID);
            BindCustomMaterialList();
        }
        protected void lnkDeleteProdCat_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = ((LinkButton)sender);
            if (PageDataset.Tables[0].Rows.Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ValidationMsg", "alert('Product category cannot be deleted. There should be at least one product category in the material list.')", true);
                BindCustomMaterialList();
            }
            else
            {
                int lProdCatID = Convert.ToInt32(btnDelete.CommandArgument);
                CustomBLL.Instance.DeleteCustomMaterialListByProductCatID(lProdCatID);
                BindCustomMaterialList();
            }
        }
        protected void txtMaterialCost_TextChanged(object sender, EventArgs e)
        {
            SaveMaterialList(sender, e);
        }
        protected void txtExtended_TextChanged(object sender, EventArgs e)
        {
            
            SaveMaterialList(sender, e);
        }
        protected void lnkDeleteLineItems_Click(object sender, EventArgs e)
        {
            LinkButton lnkDeleteLine = ((LinkButton)sender);
            GridView grdProdLines = ((GridView)((LinkButton)sender).Parent.Parent.Parent.Parent);
            if (grdProdLines.Rows.Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Deleting this record will delete the product category. This record cannot be deleted.');", true);
                BindCustomMaterialList();
                return;
            }
            int lMaterialID = Convert.ToInt16(lnkDeleteLine.CommandArgument.ToString() == "" ? "0" : lnkDeleteLine.CommandArgument.ToString());
            if (lMaterialID > 0)
            {
                CustomBLL.Instance.DeleteCustomMaterialList(lMaterialID);
            }
            BindCustomMaterialList();
        }
        protected void lnkAddLines_Click1(object sender, EventArgs e)
        {
            LinkButton lnkAddLines = ((LinkButton)sender);
            CustomMaterialList cm = new CustomMaterialList();
            cm.ProductCatId = Convert.ToInt32(lnkAddLines.CommandArgument);
            cm.MaterialList = "";
            cm.Id = 0;
            cm.VendorName = "";
            cm.VendorEmail = "";
            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            cm.InstallerID = InstallerID;
            cm.RequestStatus = 1;
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, JobID);
            BindCustomMaterialList();
        }
        protected void lstCustomMaterialList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AddLine")
            {
                CustomMaterialList cm = new CustomMaterialList();
                cm.ProductCatId = Convert.ToInt32(e.CommandArgument);
                cm.MaterialList = "";
                cm.Id = 0;
                cm.VendorName = "";
                cm.VendorEmail = "";
                cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
                bool result = CustomBLL.Instance.AddCustomMaterialList(cm, JobID);
                BindCustomMaterialList();
            }
        }

        protected void lstRequestMaterial_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                GridView grdProdLinesReq = (GridView)e.Item.FindControl("grdProdLinesReq");
                DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
                DataRowView lDrView = (DataRowView)e.Item.DataItem;
                int lProdCatID = Convert.ToInt32(lDrView["ProductCatID"]);
                DataView lDvMaterialList = new DataView(RequestMaterialDataSet.Tables[1], "ProductCatID=" + lProdCatID, "id asc", DataViewRowState.OriginalRows);
               
                ddlCategory.DataSource = ProductDataset;
                ddlCategory.DataTextField = "ProductName";
                ddlCategory.DataValueField = "ProductId";
                ddlCategory.DataBind();
                ddlCategory.SelectedValue = lProdCatID.ToString();
                

                grdProdLinesReq.DataSource = lDvMaterialList;
                grdProdLinesReq.DataBind();
            }
        }

        protected void lstRequestMaterial_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AddLine")
            {
                CustomMaterialList cm = new CustomMaterialList();
                cm.ProductCatId = Convert.ToInt32(e.CommandArgument);
                cm.MaterialList = "";
                cm.Id = 0;
                cm.VendorName = "";
                cm.VendorEmail = "";
                cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
                bool result = CustomBLL.Instance.AddCustomMaterialList(cm, JobID);
                BindCustomMaterialList();
            }
        }
        protected void grdProdLinesReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView lDrProdLine = (DataRowView)e.Row.DataItem;
                Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                TextBox txtDescription = (TextBox)e.Row.FindControl("txtDescription");
                LinkButton lnkDeleteLineItems = (LinkButton)e.Row.FindControl("lnkDeleteLineItems");
                if (lDrProdLine["RequestStatus"].ToString() == "1")
                {
                    txtDescription.Visible = true;
                    lblDescription.Visible = false;
                    lnkDeleteLineItems.Enabled = true;
                    if (Convert.ToString(lDrProdLine["InstallerID"]) != InstallerID.ToString())
                    {
                        lnkDeleteLineItems.OnClientClick = "alert('You cannot delete this item, it was not requested by you.');return false;";
                    }
                }
                else
                {
                    txtDescription.Visible = false;
                    lblDescription.Visible = true;
                    if (Convert.ToString(lDrProdLine["InstallerID"]) != InstallerID.ToString())
                    {
                        lnkDeleteLineItems.OnClientClick = "alert('You cannot delete this item, it was not requested by you.');return false;";
                    }
                    else
                    {
                        lnkDeleteLineItems.OnClientClick = "return confirm('This item is approved by admin. Do you wish to delete it?');";
                    }
                }
                
            }
        }
        #endregion

      
        #endregion

    }
}