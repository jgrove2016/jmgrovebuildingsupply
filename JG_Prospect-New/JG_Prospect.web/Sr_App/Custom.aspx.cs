using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common.modal;
using JG_Prospect.BLL;
using System.IO;
using JG_Prospect.Common.Logger;
using System.Data;
using JG_Prospect.Common;
using System.Web.Services;
using System.Web.Script.Services;
using AjaxControlToolkit;
using System.Text.RegularExpressions;

namespace JG_Prospect.Sr_App.Product_Line
{
    public partial class Custom : System.Web.UI.Page
    {
        ErrorLog logManager = new ErrorLog();
        string previousPage = string.Empty;
        private static string OtherText = string.Empty;
        int userId = 0;
        protected int ProductTypeId
        {
            get
            {
                return ViewState[QueryStringKey.Key.ProductTypeId.ToString()] == null ? 0 : Convert.ToInt32(ViewState[QueryStringKey.Key.ProductTypeId.ToString()]);
            }
            set { ViewState[QueryStringKey.Key.ProductTypeId.ToString()] = value; }
        }

        public List<CustomerLocationPic> CustomerLocationPicturesList
        {
            get
            {
                return ViewState[SessionKey.Key.PagedataTable.ToString()] == null ? null : (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];
            }
            set
            {
                ViewState[SessionKey.Key.PagedataTable.ToString()] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            int ProductId = 0, CustomerId = 0, CustomId = 0;
            if (Request.QueryString[QueryStringKey.Key.Other.ToString()] != null)
            {
                OtherText = Request.QueryString[QueryStringKey.Key.Other.ToString()].ToString();
            }
            ViewState["PreviousPage"] = Request.UrlReferrer;
            userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            if (ViewState["PreviousPage"] != null)
            {
                previousPage = ViewState["PreviousPage"].ToString();
            }

            //if (Request.QueryString[QueryStringKey.Key.ProductTypeIdFrom.ToString()] != null)
            //{
            //    ProductTypeIdFrom = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductTypeIdFrom.ToString()]);
            //}
            if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null)
            {
                lblmsg.Text = Request.QueryString[QueryStringKey.Key.CustomerId.ToString()].ToString();
            }

            //DataSet dsCustomer=new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt16(Request.QueryString[1]));
            DataSet dsCustomer = new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt16(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()].ToString()));
            if (dsCustomer.Tables[0].Rows.Count > 0)
            {
                txtCustomer.Text = dsCustomer.Tables[0].Rows[0]["CustomerName"].ToString();
                txtCustomer.Enabled = false;
            }
            DataSet dsTerms;
            if (Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null)
            {
                ProductTypeId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);

                if (ProductTypeId != 0)
                {
                    string productName = UserBLL.Instance.GetProductNameByProductId(ProductTypeId);
                    string ProposalTerm = string.Empty;
                    dsTerms = new_customerBLL.Instance.GetProposalTerm(Convert.ToInt32(ProductTypeId));
                    if (dsTerms.Tables.Count > 0)
                    {
                        if (dsTerms.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsTerms.Tables[0].Rows[0][1]) != "")
                            {
                                ProposalTerm = Convert.ToString(dsTerms.Tables[0].Rows[0][1]);
                                ProposalTerm = Regex.Replace(ProposalTerm, "<.*?>|&.*?;", string.Empty);
                                if (!IsPostBack)
                                {
                                    txtProposalTerm.Text = ProposalTerm;
                                }
                            }
                        }
                    }
                    h1Heading.InnerText = "Details:" + productName;
                }
            }
            if (ViewState[QueryStringKey.Key.Edit.ToString()] == null)
            {
                if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null &&
                    Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null &&
                    Request.QueryString[QueryStringKey.Key.ProductId.ToString()] != null)
                {
                    if (previousPage.Contains("Procurement.aspx"))
                    {
                        CustomerId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
                        ProductId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
                        CustomId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);
                        fillCusromDetails(CustomerId, CustomId, ProductId);
                        lnkDownload.Visible = true;
                        DisableControls();
                        btnexit.Text = "Go Back";
                    }
                    else
                    {
                        CustomerId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
                        ProductId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
                        CustomId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);

                        hidProdType.Value = ProductId.ToString();
                        btnsave.Text = JGConstant.UPDATE;
                        lnkDownload.Visible = true;

                        fillCusromDetails(CustomerId, CustomId, ProductId);
                        ViewState[QueryStringKey.Key.Edit.ToString()] = JGConstant.TRUE;
                    }

                }
                else { btnsave.Text = JGConstant.SAVE; }

            }
        }

        private void DisableControls()
        {
            txtProposalTerm.Enabled = false;
            txtProposalCost.Enabled = false;
            txtCustSupMaterial.Enabled = false;
            chkCustSupMaterial.Enabled = false;

            ddlMaterialStorage.Enabled = false;
            chkMaterialStorage.Enabled = false;

            ddlDumpStorageLocation.Enabled = false;
            chkDumpStorageLocation.Enabled = false;

            txtspecialIns.Enabled = false;
            chkPermit.Enabled = false;
            chkHabitat.Enabled = false;
        }
        [WebMethod]
        public static string Exists(string value)
        {
            if (value == AdminBLL.Instance.GetAdminCode())
            {
                return "True";
            }
            else
            {
                return "false";
            }
        }

        private void fillCusromDetails(int CustomerId, int CustomId, int ProductTypeId)
        {
            Customs custom = new Customs();
            string locPics = "";
            custom.ProductTypeId = ProductTypeId;
            custom.CustomerId = CustomerId;
            custom.Id = CustomId;


            custom = CustomBLL.Instance.GetCustomDetail(custom);
            if (custom != null)
            {
                locPics = Convert.ToString(custom.CustomerLocationPics.Count);

                if (locPics != "")
                {
                    hidCount.Value = Convert.ToString(custom.CustomerLocationPics.Count);
                }
                hidProdId.Value = custom.Id.ToString();
                txtCustomer.Text = custom.Customer;
                txtProposalCost.Text = custom.ProposalCost.ToString();
                txtProposalTerm.Text = custom.ProposalTerms;
                txtspecialIns.Text = custom.SpecialInstruction;
                txtworkarea.Text = custom.WorkArea;
                txtCustSupMaterial.Text = custom.CustSuppliedMaterial;
                chkCustSupMaterial.Checked = custom.IsCustSupMatApplicable;

                chkMaterialStorage.Checked = !custom.IsMatStorageApplicable;

                if (custom.IsMatStorageApplicable)
                {
                    ddlMaterialStorage.Enabled = true; 
                    var customMaterialStorageItem = ddlMaterialStorage.Items.FindByText(custom.MaterialStorage);
                    if (customMaterialStorageItem != null)
                    {
                        customMaterialStorageItem.Selected = true;
                    }
                }

                chkDumpStorageLocation.Checked = !custom.IsDumpStorageApplicable;
                if (custom.IsDumpStorageApplicable)
                {
                    ddlDumpStorageLocation.Enabled = true;
                    var customDumpStorageItem = ddlDumpStorageLocation.Items.FindByText(custom.DumpStorage);
                    if (customDumpStorageItem != null)
                    {
                        customDumpStorageItem.Selected = true;
                    }
                }

                chkPermit.Checked = custom.IsPermitRequired;
                chkHabitat.Checked = custom.IsHabitat;
                lnkDownload.Text = custom.Attachment;
                ViewState[SessionKey.Key.PagedataTable.ToString()] = custom.CustomerLocationPics;
                gvCategory.DataSource = custom.CustomerLocationPics;
                gvCategory.DataBind();
            }
        }

        private void ClearCustomData()
        {
            hidProdId.Value = null;
            hidProdType.Value = null;
            hidCount.Value = null;
            txtCustomer.Text = string.Empty;
            txtProposalCost.Text = string.Empty;
            txtProposalTerm.Text = string.Empty;
            txtspecialIns.Text = string.Empty;
            txtworkarea.Text = string.Empty;
            txtCustSupMaterial.Text = string.Empty;
            chkCustSupMaterial.Checked = false;
            
            ddlMaterialStorage.SelectedIndex = 0;
            chkMaterialStorage.Checked = false;

            ddlDumpStorageLocation.SelectedIndex = 0;
            chkDumpStorageLocation.Checked = false;

            chkPermit.Checked = false;
            chkHabitat.Checked = false;
            lnkDownload.Visible = false;
            ViewState[SessionKey.Key.PagedataTable.ToString()] = null;
            ViewState[QueryStringKey.Key.ProductTypeId.ToString()] = null;
            gvCategory.DataSource = null;
            gvCategory.DataBind();
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            if (btnexit.Text == "Go Back")
            {
                Response.Redirect("~/Sr_App/Procurement.aspx");
            }
            else
            {
                Response.RedirectPermanent("~/Sr_App/home.aspx");
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    Customs custom = new Customs();
                    if (hidProdId.Value != "")
                    { custom.Id = Convert.ToInt32(hidProdId.Value); }
                    else { custom.Id = 0; }

                    // custom.CustomerId = Convert.ToInt16(Request.QueryString[2]);// Convert.ToInt32(Session[SessionKey.Key.CustomerId.ToString()]);
                    custom.CustomerId = Convert.ToInt32(Request.QueryString[SessionKey.Key.CustomerId.ToString()]);
                    custom.Customer = txtCustomer.Text.Trim();
                    custom.ProposalCost = decimal.Parse(txtProposalCost.Text);
                    custom.ProposalTerms = txtProposalTerm.Text.Trim();
                    custom.SpecialInstruction = txtspecialIns.Text;
                    custom.WorkArea = txtworkarea.Text;
                    custom.UserId = userId;
                    custom.ProductTypeId = this.ProductTypeId;

                    string xml = "<root>";

                    List<CustomerLocationPic> pics = (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];

                    var image = pics.AsEnumerable().Take(1);
                    string mainImage = image.FirstOrDefault().LocationPicture;

                    for (int i = 0; i < pics.Count; i++)
                    {
                        xml += "<pics><pic>" + pics[i].LocationPicture + "</pic></pics>";
                    }
                    xml += "</root>";
                    custom.LocationImage = xml;
                    custom.MainImage = mainImage;
                    //if (fileAttachment.HasFile)
                    //{
                    //    if (fileAttachment.PostedFile.FileName != "")
                    //    {
                    //        custom.Attachment = fileAttachment.PostedFile.FileName;

                    //        string strFileNameWithPath = fileAttachment.PostedFile.FileName;
                    //        string strExtensionName = System.IO.Path.GetExtension(strFileNameWithPath);
                    //        string strFileName = System.IO.Path.GetFileName(strFileNameWithPath);
                    //        custom.Attachment = strFileName;
                    //        int intFileSize = fileAttachment.PostedFile.ContentLength;

                    //        if (intFileSize > 0)
                    //        {
                    //            if (File.Exists(Server.MapPath("~/UploadedFiles/") + strFileName) == true)
                    //            {
                    //                File.Delete(Server.MapPath("~/UploadedFiles/") + strFileName);
                    //                fileAttachment.PostedFile.SaveAs(Server.MapPath("~/UploadedFiles/") + strFileName);
                    //            }
                    //            else
                    //            {
                    //                fileAttachment.PostedFile.SaveAs(Server.MapPath("~/UploadedFiles/") + strFileName);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (lnkDownload.Visible)
                    //    {
                    //        custom.Attachment = lnkDownload.Text;
                    //    }
                    //    else { custom.Attachment = string.Empty; }
                    //}

                    if (lnkDownload.Text != "")
                    {
                        custom.Attachment = lnkDownload.Text;
                    }
                    else { custom.Attachment = string.Empty; }
                    custom.CustSuppliedMaterial = txtCustSupMaterial.Text.Trim();
                    custom.IsCustSupMatApplicable = chkCustSupMaterial.Checked;
                    
                    custom.MaterialStorage = ddlMaterialStorage.SelectedItem.Text;
                    custom.DumpStorage = ddlDumpStorageLocation.SelectedItem.Text;

                    custom.IsMatStorageApplicable = !chkMaterialStorage.Checked;
                    custom.IsDumpStorageApplicable = !chkDumpStorageLocation.Checked;

                    custom.IsPermitRequired = chkPermit.Checked;
                    custom.IsHabitat = chkHabitat.Checked;
                    custom.Others = OtherText;
                    bool result = CustomBLL.Instance.AddCustom(custom);

                    Session["Proposal"] = txtProposalTerm.Text;

                    if (result && custom.Id == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Product has been added successfully.');", true);
                        ClearCustomData();
                        // Response.Redirect("~/Sr_App/ProductEstimate.aspx?CustomerId=" + custom.CustomerId, false);
                        Response.Redirect("~/Sr_App/ProductEstimate.aspx", false);
                    }
                    else if (result && custom.Id > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Product has been updated successfully.');", true);
                        ClearCustomData();
                        // Response.Redirect("~/Sr_App/ProductEstimate.aspx?CustomerId=" + custom.CustomerId, false);
                        Response.Redirect("~/Sr_App/ProductEstimate.aspx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to add Product.');", true);
                        ClearCustomData();
                    }

                }
            }
            catch (Exception ex)
            {
                logManager.writeToLog(ex, "Custom", Request.ServerVariables["remote_addr"].ToString());
            }
        }

        protected void btnImageUploadClick_Click(object sender, EventArgs e)
        {
            var ImageList = CustomerLocationPicturesList;
        }
        protected void AsyncFileUploadCustomerAttachment_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string imageName = Path.GetFileName(AsyncFileUploadCustomerAttachment.FileName);

            if (File.Exists(Server.MapPath("~/UploadedFiles/") + imageName) == true)
            {
                File.Delete(Server.MapPath("~/UploadedFiles/") + imageName);
            }
            AsyncFileUploadCustomerAttachment.SaveAs(Server.MapPath("~/UploadedFiles/" + imageName));
            lnkDownload.Visible = true;
            lnkDownload.Text = imageName;

        }
        protected void ajaxFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            if (ValidateImageUpload(Path.GetFileName(ajaxFileUpload.FileName)))
            {
                int srNo = 1;
                List<CustomerLocationPic> pics = null;
                string imageName = Path.GetFileName(ajaxFileUpload.FileName);
                string tempImageName = Guid.NewGuid() + "-" + imageName;
                ajaxFileUpload.SaveAs(Server.MapPath("~/CustomerDocs/LocationPics/" + tempImageName));
                tempImageName = "../CustomerDocs/LocationPics/" + tempImageName;
                imageName = tempImageName;
                //ajaxFileUpload.SaveAs(Server.MapPath("~/CustomerDocs/LocationPics/" + tempImageName));

                if (ViewState[SessionKey.Key.PagedataTable.ToString()] != null)
                {
                    pics = (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];
                }
                else
                {
                    pics = new List<CustomerLocationPic>();
                }
                if (pics.Count > 0)
                {
                    srNo = pics.Count + 1;
                }
                pics.Add(new CustomerLocationPic { RowSerialNo = srNo, LocationPicture = tempImageName });

                CustomerLocationPicturesList = pics;
                hidCount.Value = pics.Count == 0 ? string.Empty : pics.Count.ToString();
                gvCategory.DataSource = pics;
                gvCategory.DataBind();
            }
        }
        private bool ValidateImageUpload(string fileName)
        {
            string[] extensions = { ".gif", ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
            bool flag = false;
            for (int counter = 0; counter < extensions.Length; counter++)
            {
                if (fileName.ToLower().Contains(extensions[counter]))
                {
                    flag = true;
                    break;
                }
            }
            List<CustomerLocationPic> pics = CustomerLocationPicturesList;
            if (pics != null && Convert.ToInt32(pics.Count) >= 5)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + "You can not upload image more than five." + "');", true);
                return false;
            }
            if (!flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + "Invalid formats are not allowed." + "');", true);
                return false;
            }
            return flag;
        }
        protected void bntAdd_Click(object sender, EventArgs e)
        {
            //LoadImage();
        }

        protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRec")
            {
                int Id = Convert.ToInt32(e.CommandArgument.ToString());
                List<CustomerLocationPic> pics = (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];
                pics.Remove(pics.FirstOrDefault(id => id.RowSerialNo == Id));
                ViewState[SessionKey.Key.PagedataTable.ToString()] = pics;
                hidCount.Value = "";
                hidCount.Value = pics.Count.ToString();
                gvCategory.DataSource = pics;
                gvCategory.DataBind();
            }
        }

        protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CustomerLocationPic dr = (CustomerLocationPic)e.Row.DataItem;
                string strImage = dr.LocationPicture;
                //((Image)(e.Row.FindControl("imglocation"))).ImageUrl = "~/CustomerDocs/LocationPics/" + strImage;
            }
        }

        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategory.PageIndex = e.NewPageIndex;
            gvCategory.DataSource = ViewState[SessionKey.Key.PagedataTable.ToString()];
            gvCategory.DataBind();
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string fileName = lnkDownload.Text.Trim();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
            Response.TransmitFile("~/UploadedFiles/" + fileName);
            Response.End();
        }

        protected void chkCustSupMaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustSupMaterial.Checked == true)
            {
                txtCustSupMaterial.Enabled = false;
                txtCustSupMaterial.Text = "";
            }
            else
            {
                txtCustSupMaterial.Enabled = true;
                txtCustSupMaterial.Text = "";
            }
        }

        protected void chkDumpStorageLocation_CheckedChanged(object sender, EventArgs e)
        {
            ddlDumpStorageLocation.SelectedIndex = 0;
            if (chkDumpStorageLocation.Checked == true)
                ddlDumpStorageLocation.Enabled = false;
            else
                ddlDumpStorageLocation.Enabled = true;
        }

        protected void chkMaterialStorage_CheckedChanged(object sender, EventArgs e)
        {
            ddlMaterialStorage.SelectedIndex = 0;
            if (chkMaterialStorage.Checked == true)
                ddlMaterialStorage.Enabled = false;
            else
                ddlMaterialStorage.Enabled = true;
        }

    }
}
