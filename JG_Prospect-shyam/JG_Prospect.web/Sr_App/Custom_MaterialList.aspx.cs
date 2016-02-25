using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using JG_Prospect.BLL;

using System.Net.Mail;
using System.Text;
using System.Web.Services;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Configuration;
using System.Net.Configuration;
using System.Net;
using JG_Prospect.Common.Logger;
using Saplin.Controls;
//using System.Diagnostics;

namespace JG_Prospect.Sr_App
{
    public partial class Custom_MaterialList : System.Web.UI.Page
    {
        string flag = "";

        List<AttachedQuotes> aqList = new List<AttachedQuotes>();
        int productId = 0;
        static int selectedVendorID = 0;
        static string selectedRowIndex = "-1";
        static DataTable dtAddLine = new DataTable();
        string emailStatus = string.Empty, SoldJobID = string.Empty;

        string jobId = string.Empty;
        private Boolean IsPageRefresh = false;
        int estimateId = 0, customerId = 0, productTypeId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //jobId = Convert.ToString(Session[SessionKey.Key.JobId.ToString()]);
            //GetCustomerId(Convert.ToInt32(jobId));
            if (Request.QueryString[QueryStringKey.Key.ProductId.ToString()] != null)
            {
                estimateId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);
            }
            
            if (Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null)
            {
                productTypeId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
            }
            if (Request.QueryString[QueryStringKey.Key.SoldJobId.ToString()] != null)
            {
                SSNo.Text = Convert.ToString(Request.QueryString[QueryStringKey.Key.SoldJobId.ToString()]);
            }
            setPermissions();
            if (!IsPostBack)
            {
                BindDropDown();
                Session["MaterialListProductId"] = "";
                Session["AddFlag"] = "";
                Session["RowIndex"] = 0;
                Session["VendoeAttachmentPathNew"] = "";
                Session["VendoeCategoryAttachmentPath"] = "";
                ViewState["Id"] = 0;
                BindDropDowns();
                BindGridView();
                selectedRowIndex = "-1";
                DisableControlsBasedOnEmailStatus(emailStatus);

                //bindMaterialList();
                SetButtonText();
                //bind();
                lnkVendorCategory.ForeColor = System.Drawing.Color.DarkGray;
                lnkVendorCategory.Enabled = false;
                lnkVendor.Enabled = true;
                lnkVendor.ForeColor = System.Drawing.Color.Blue;
                //if (Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null)
                //{
                //   // productTypeId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
                //    string prodName = UserBLL.Instance.GetProductNameByProductId(productTypeId);
                //    h1Heading.InnerText = "Material List - " + prodName;
                //}
              //  DataSet ds = SetInitialRow();
                
                if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null)
                {
                    customerId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
                    CusId.Text = Convert.ToString(customerId);
                    GetCustomerName(customerId);
                }
                BindNewGrid(SSNo.Text, Convert.ToInt32(CusId.Text));
            }
            else
            {
              
               IsPageRefresh = true;
            }
            btnFileUpload.Attributes.Add("onclick", "document.getElementById('" + uploadvendorquotes.ClientID + "').click();");

        }
        private void BindDropDown()
        {
            DataSet ds = VendorBLL.Instance.fetchAllVendorCategoryHavingVendors();
            drpVendorCategory1.DataSource = ds;
            drpVendorCategory1.DataTextField = "VendorCategoryNm";
            drpVendorCategory1.DataValueField = "VendorCategpryId";
            drpVendorCategory1.DataBind();
            drpVendorCategory1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            drpVendorName1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        private DataSet SetInitialRow()
        {
            DataSet ds = new DataSet();
            DataRow dr = null;
            ds.Tables[0].Columns.Add(new DataColumn("ProductId", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("Line", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("JGSkuPartNo", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("Description", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("Qty", typeof(int)));
            ds.Tables[0].Columns.Add(new DataColumn("VendorQuotes", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("MaterialCost", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("Extent", typeof(string)));
            ds.Tables[0].Columns.Add(new DataColumn("Total", typeof(string)));
            dr = ds.Tables[0].NewRow();

            dr["ProductId"] = 0;
            dr["Line"] = string.Empty;
            dr["JGSkuPartNo"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Qty"] = 1;
            dr["VendorQuotes"] = string.Empty;
            dr["MaterialCost"] = string.Empty;
            dr["Extent"] = string.Empty;
            dr["Total"] = string.Empty;
            ds.Tables[0].Rows.Add(dr);
            //dr = dt.NewRow();
            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = ds.Tables[0];
            return ds;
        }

        protected void grdcustom_material_list_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkAdd = (LinkButton)(e.Row.FindControl("lnkAdd"));
                HiddenField hfMyRow = (HiddenField)(e.Row.FindControl("hfMyRow"));
                
                HiddenField HiddenField1 = (HiddenField)(e.Row.FindControl("HiddenField1"));
                HiddenField hdnCategorynew = (HiddenField)(e.Row.FindControl("hdnCategorynew"));
                HiddenField txtLine = (HiddenField)(e.Row.FindControl("hidCategory"));
                TextBox txtSkuPartNo = (TextBox)(e.Row.FindControl("txtSkuPartNo"));
                TextBox txtDescription = (TextBox)(e.Row.FindControl("txtDescription"));
                TextBox txtQTY = (TextBox)(e.Row.FindControl("txtQTY"));
                TextBox txtUOM = (TextBox)(e.Row.FindControl("txtUOM"));
                Label lblCost = (Label)(e.Row.FindControl("lblCost"));
                TextBox txtMaterialCost = (TextBox)(e.Row.FindControl("txtMaterialCost"));
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");
                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                DataSet ds = UserBLL.Instance.GetAllProducts();
                ddlCategory.DataSource = ds;
                ddlCategory.DataTextField = "ProductName";
                ddlCategory.DataValueField = "ProductId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0)"));
                ddlCategory.SelectedValue = lblCategory.Text;

                DropDownCheckBoxes ddlVendorCategory = (DropDownCheckBoxes)e.Row.FindControl("ddlVendorName");
                DataSet dsVendorCategory = GetVendorCategories();
                ddlVendorCategory.DataSource = GetVendorCategories();
                ddlVendorCategory.DataSource = dsVendorCategory;
                ddlVendorCategory.DataTextField = "VendorCategoryNm";
                ddlVendorCategory.DataValueField = "VendorCategpryId";
                ddlVendorCategory.DataBind();
                ddlVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                ddlVendorCategory.SelectedIndex = 0;
                if (txtMaterialCost.Text != "" && txtQTY.Text!="") 
                {
                    lblCost.Text = Convert.ToString(Convert.ToInt32(txtMaterialCost.Text) * Convert.ToInt32(txtQTY.Text));
                }
                //if (Convert.ToString(Session["AddFlag"]) != "" && txtLine.Text == "" && txtSkuPartNo.Text == "" && txtDescription.Text == "" && txtQTY.Text == "" && txtUOM.Text == "")
               /*
                if (Convert.ToString(Session["AddFlag"]) != "" && hdnCategorynew.Value == "")
                {
                    ddlCategory.Style.Add("display", "none");
                }
                #region Added by Neeta...

                else if (dtAddLine.Rows.Count>0)
                {
                   // DataTable d=(DataTable) ViewState["Visible"];
                    if (Convert.ToString(dtAddLine.Rows[0][1]) != "Visible")
                    {
                        ddlCategory.Style.Add("display", "none");
                    }
                }*/
              //  if (Convert.ToString(hfMyRow.Value) != "Visible")
                    if (Convert.ToString(HiddenField1.Value) != "Visible") 
                //if (Convert.ToString(hdnCategorynew.Value) != "NotVisible")
                
                // if (Convert.ToString(Session["AddFlag"]) != "" && hdnCategorynew.Value == "" )
                {
                   // ddlCategory.Style.Add("display", "none");
                     ddlCategory.Visible = false;
                     lnkAdd.Visible = false;
                }
                else
                {
                    ddlCategory.Visible = true;
                    lnkAdd.Visible = true;
                   // ddlCategory.Style.Add("display", "block");
                }
                
                
               // #endregion
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblsrno = (Label)e.Row.FindControl("lblsrno");
            //    lblsrno.Text = Convert.ToString(Convert.ToInt16(lblsrno.Text) + 1);
            //    DropDownList ddlVendorCategory = (DropDownList)e.Row.FindControl("ddlVendorCategory");
            //    DataSet dsVendorCategory = GetVendorCategories();
            //    ddlVendorCategory.DataSource = GetVendorCategories();
            //    ddlVendorCategory.DataSource = dsVendorCategory;
            //    ddlVendorCategory.DataTextField = "VendorCategoryNm";
            //    ddlVendorCategory.DataValueField = "VendorCategpryId";
            //    ddlVendorCategory.DataBind();
            //    ddlVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            //    ddlVendorCategory.SelectedIndex = 0;
            //}
            //if (btnSendMail.Text == "Send Mail To Vendor(s)")
            //{
            //    grdcustom_material_list.Columns[6].Visible = false;
            //}

        }

        private void BindNewGrid(string SoldId,int CustomerId)
        {
            try
            {

            
            DataSet ds = VendorBLL.Instance.GetMaterialListData(SoldId,CustomerId);
            DataTable dtNew = (DataTable)ds.Tables[0];

            ds.Tables[0].Columns.Add("MyRow", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //need to set value to MyRow column
                dr["MyRow"] ="Visible";   // or set it to some other value
            }
           
            Session["MaterialList"] = ds;
            //if(ds.Tables.Count>0)
            //{
            //    if (Convert.ToString(ds.Tables[0].Rows[0][0])!="")
            //    {
            //        Session["MaterialListProductId"] = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            //    }
            //}
            grdcustom_material_list.DataSource = ds;
            grdcustom_material_list.DataBind();
            //List<AttachedQuotes> fileList = new List<AttachedQuotes>();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        DataRow dr = ds.Tables[0].Rows[i];

            //        AttachedQuotes a = new AttachedQuotes();
            //        a.id = Convert.ToInt16(dr["Id"]);
            //        a.DocName = dr["DocName"].ToString();
            //        a.TempName = dr["TempName"].ToString();
            //        a.VendorName = dr["VendorName"].ToString();
            //        a.VendorCategoryNm = dr["VendorCategoryNm"].ToString();
            //        a.action = "Add";

            //        if (Convert.IsDBNull(dr["VendorCategpryId"]))
            //        {
            //            a.VendorCategoryId = 0;
            //        }
            //        else
            //        {
            //            a.VendorCategoryId = Convert.ToInt16(dr["VendorCategpryId"]);
            //        }

            //        if (Convert.IsDBNull(dr["VendorId"]))
            //        {
            //            a.VendorId = 0;
            //        }
            //        else
            //        {
            //            a.VendorId = Convert.ToInt16(dr["VendorId"]);
            //        }
            //        fileList.Add(a);
            //    }
            //}
            //ViewState["FileList"] = fileList;
            }
            catch (Exception ex )
            {
                lblException.Text = ex.Message;
            }
        }

        private void GetCustomerName(int Id)
        {
            DataSet ds = new_customerBLL.Instance.GetCustName(Id);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
                    {
                        CusName.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    }
                }
            }
        }

        
        //public void GetCustomerId(string SoldJobId)
        //{
        //    DataSet ds = new_customerBLL.Instance.GetCustIdAndName(SoldJobId);
        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
        //            {
        //                lblCustomerId.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
        //            }
        //            if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
        //            {
        //                lblCustName.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
        //            }
        //        }
        //    }
        //}

        private void DisableControlsBasedOnEmailStatus(string emailStatus)
        {
            if (emailStatus == "V")
            {
                btnSaveQuotes.Visible = false;
                btnResetQuotes.Visible = false;
                drpVendorCategory.Enabled = false;
                drpVendorName.Enabled = false;
                btnFileUpload.Disabled = true;
                txtFileName.Enabled = false;
                txtFileContent.Enabled = false;
                btnCancelQuotes.Text = "Close";
                foreach (GridViewRow r in grdAttachQuotes.Rows)
                {
                    LinkButton lnkdelete = (LinkButton)r.FindControl("lnkdelete");
                    lnkdelete.Enabled = false;
                    lnkdelete.ForeColor = System.Drawing.Color.DarkGray;
                    lnkdelete.Attributes.Remove("onclick");
                    LinkButton lnkSelect = (LinkButton)r.FindControl("lnkSelect");
                    lnkSelect.Enabled = false;
                }
            }
        }

        private void BindDropDowns()
        {
            DataSet ds = VendorBLL.Instance.fetchAllVendorCategoryHavingVendors();
            drpVendorCategory.DataSource = ds;
            drpVendorCategory.DataTextField = "VendorCategoryNm";
            drpVendorCategory.DataValueField = "VendorCategpryId";
            drpVendorCategory.DataBind();
            drpVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }

        private void BindGridView()
        {
            DataSet ds = VendorBLL.Instance.GetAllAttachedQuotes(SoldJobID);
            grdAttachQuotes.DataSource = ds;
            grdAttachQuotes.DataBind();
            List<AttachedQuotes> fileList = new List<AttachedQuotes>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    AttachedQuotes a = new AttachedQuotes();
                    a.id = Convert.ToInt16(dr["Id"]);
                    a.DocName = dr["DocName"].ToString();
                    a.TempName = dr["TempName"].ToString();
                    a.VendorName = dr["VendorName"].ToString();
                    a.VendorCategoryNm = dr["VendorCategoryNm"].ToString();
                    a.action = "Add";

                    if (Convert.IsDBNull(dr["VendorCategpryId"]))
                    {
                        a.VendorCategoryId = 0;
                    }
                    else
                    {
                        a.VendorCategoryId = Convert.ToInt16(dr["VendorCategpryId"]);
                    }

                    if (Convert.IsDBNull(dr["VendorId"]))
                    {
                        a.VendorId = 0;
                    }
                    else
                    {
                        a.VendorId = Convert.ToInt16(dr["VendorId"]);
                    }
                    fileList.Add(a);
                }
            }
            ViewState["FileList"] = fileList;
        }

        DataSet DS = new DataSet();

        public void SetButtonText()
        {
            try
            {

            
                string EmailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);
                int result = CustomBLL.Instance.WhetherCustomMaterialListExists(SSNo.Text);//, productTypeId, estimateId);
                if (result == 0) //if list doesn't exists
                {
                    btnSendMail.Text = "Save";
                    showVendorCategoriesPermissions();
                }
                else  //if list exists
                {
                    if (EmailStatus == JGConstant.EMAIL_STATUS_NONE || EmailStatus == string.Empty)       //if no email was sent
                    {
                        int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(SSNo.Text);//, productTypeId, estimateId);
                        if (permissionStatusCategories == 0)        //if no permissions were granted for categories
                        {
                            btnSendMail.Text = "Save";
                        }
                        else                //if permissions were granted for categories
                        {
                            btnSendMail.Text = "Send Mail To Vendor Category(s)";
                            grdcustom_material_list.Columns[6].Visible = false;
                        }
                        showVendorCategoriesPermissions();
                    }

                    else if (EmailStatus == JGConstant.EMAIL_STATUS_VENDOR)    //if both mails are sent
                    {
                        setControlsAfterSendingBothMails();
                        showVendorPermissions();
                    }
                    else        //if mails were sent to categories
                    {
                        int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(SSNo.Text);//, productTypeId, estimateId);
                        if (permissionStatus == 0)  //if permissions were not granted for vendors
                        {
                            btnSendMail.Text = "Save";
                            showVendorPermissions();
                            EnableVendorNameAndAmount();
                            grdcustom_material_list.Columns[6].Visible = true;
                        }
                        else         //if permissions were granted for vendors
                        {
                            btnSendMail.Text = "Send Mail To Vendor(s)";
                            setControlsForVendorsAfterSave();
                            showVendorPermissions();
                            EnableVendorNameAndAmount();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblException.Text = ex.Message;
            }
        }
        private void EnableVendorNameAndAmount()
        {
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                try
                {
                    Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                    ddlVendorName.Enabled = true;
                  //  TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
               
                   // txtAmount.Enabled = true;
                }
                catch (Exception)
                {

                   // throw;
                }
            }
        }

        private void DisableVendorNameAndAmount()
        {
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                ddlVendorName.Enabled = false;
                TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                txtAmount.Enabled = false;
            }
        }
        public void setPermissions()
        {
            try
            {
                DataSet ds = CustomBLL.Instance.GetAllPermissionOfCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //if (lnkForemanPermission.Visible == true)
                    if(txtForemanPasswordNew.Visible == true)
                    {
                        if (Convert.ToChar(ds.Tables[0].Rows[0]["IsForemanPermission"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                        {
                            txtForemanPasswordNew.Visible = false;
                            if (Convert.ToString(ds.Tables[0].Rows[0]["FormanEmail"].ToString().Trim()) != "")
                            lblFormanEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["FormanEmail"].ToString().Trim());
                            lblFormanEmail.ForeColor = System.Drawing.Color.Green;
                            //txtForemanPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkForemanPermission.Enabled = false;
                            //lnkForemanPermission.ForeColor = System.Drawing.Color.DarkGray;
                            //popupForeman_permission.TargetControlID = "hdnForeman";
                        }
                        if (Convert.ToChar(ds.Tables[0].Rows[0]["IsSrSalemanPermissionF"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                        {
                            txtSrSalesManPermition.Visible   = false;
                            if (Convert.ToString(ds.Tables[0].Rows[0]["SrSalesEmail"].ToString().Trim()) != "")
                                lblSrSales.Text = Convert.ToString(ds.Tables[0].Rows[0]["SrSalesEmail"].ToString().Trim());
                            //txtSrSalesManPermition.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkSrSalesmanPermissionF.Enabled = false;
                           // lnkSrSalesmanPermissionF.ForeColor = System.Drawing.Color.DarkGray;
                            //popupSrSalesmanPermissionF.TargetControlID = "hdnSrF";
                        }
                        # region For removing Password
                       // txtForemanPasswordNew.Text = "";
                        #endregion
                    }
                    //if (lnkAdminPermission.Visible == true)
                    if (txtAdminPasswordNew.Visible==true)
                    {
                        if (Convert.ToChar(ds.Tables[0].Rows[0]["IsAdminPermission"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                        {
                            txtAdminPasswordNew.Visible = false;
                            if (Convert.ToString(ds.Tables[0].Rows[0]["AdminEmail"].ToString().Trim()) != "")
                                lblAdmin.Text = Convert.ToString(ds.Tables[0].Rows[0]["AdminEmail"].ToString().Trim());
                            //txtAdminPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkAdminPermission.Enabled = false;
                            //lnkAdminPermission.ForeColor = System.Drawing.Color.DarkGray;
                           // popupAdmin_permission.TargetControlID = "hdnAdmin";
                        }
                        if (Convert.ToChar(ds.Tables[0].Rows[0]["IsSrSalemanPermissionA"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                        {
                            txtSrSalesPassword.Visible = false;
                            if (Convert.ToString(ds.Tables[0].Rows[0]["SrSalemanAEmail"].ToString().Trim()) != "")
                                lblSrSalesManPermition.Text = Convert.ToString(ds.Tables[0].Rows[0]["SrSalemanAEmail"].ToString().Trim());
                            //txtSrSalesPassword.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkSrSalesmanPermissionA.Enabled = false;
                            //lnkSrSalesmanPermissionA.ForeColor = System.Drawing.Color.DarkGray;
                            //popupSrSalesmanPermissionA.TargetControlID = "hdnSrA";
                        }
                        # region For removing Password
                      //  txtAdminPasswordNew.Text = "";
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                lblException.Text = ex.Message;
            }
        }
        public void showVendorCategoriesPermissions()
        {
            txtForemanPasswordNew.Visible = true;
            #region forremoving Autofill option
          //  txtForemanPasswordNew.Text = "";
            #endregion
            //lnkForemanPermission.Visible = true;
            txtSrSalesManPermition.Visible = true;
            #region forremoving Autofill option
           // txtSrSalesManPermition.Text = "";
            #endregion
            //lnkSrSalesmanPermissionF.Visible = true;
            txtAdminPasswordNew.Visible = false;
            //lnkAdminPermission.Visible = false;
            txtSrSalesPassword.Visible = false;
            //lnkSrSalesmanPermissionA.Visible = false;
            setPermissions();
        }
        public void showVendorPermissions()
        {
            txtForemanPasswordNew.Visible = false;
            //lnkForemanPermission.Visible = false;
            txtSrSalesManPermition.Visible = false;
            txtSrSalesManPermition.Visible = false;
            //lnkSrSalesmanPermissionF.Visible = false;
            txtAdminPasswordNew.Visible = true;
            //lnkAdminPermission.Visible = true;
            txtSrSalesPassword.Visible = true;
            //lnkSrSalesmanPermissionA.Visible = true;

            setPermissions();
        }

        protected void setControlsForVendorCategoriesAfterSave()
        {
            foreach (GridViewRow gr in grdcustom_material_list.Rows)
            {
                TextBox txtMateriallist = (TextBox)gr.FindControl("txtMateriallist");
                txtMateriallist.Enabled = false;

                TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                txtAmount.Enabled = false;
                DropDownList ddlVendorCategory = (DropDownList)gr.FindControl("ddlVendorCategory");
                ddlVendorCategory.Enabled = false;

                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)gr.FindControl("ddlVendorName");
                ddlVendorName.Enabled = false;
            }
        }

        protected void setControlsForVendorsAfterSave()
        {
            foreach (GridViewRow gr in grdcustom_material_list.Rows)
            {
                TextBox txtMateriallist = (TextBox)gr.FindControl("txtMateriallist");
                txtMateriallist.Enabled = false;

                TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                txtAmount.Enabled = false;
                DropDownList ddlVendorCategory = (DropDownList)gr.FindControl("ddlVendorCategory");
                ddlVendorCategory.Enabled = false;
                int selectedCategoryID = Convert.ToInt16(ddlVendorCategory.SelectedItem.Value);

                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)gr.FindControl("ddlVendorName");
                ddlVendorName.Enabled = false;
            }
            grdcustom_material_list.Columns[6].Visible = false;
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

        public DataSet GetVendorCategories()
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchAllVendorCategoryHavingVendors();
            return ds;
        }

        public DataSet GetVendorNames(int vendorcategoryId)
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchVendorNamesByVendorCategory(vendorcategoryId);
            return ds;
        }

        private void bindMaterialList()
        {
            DataSet ds = CustomBLL.Instance.GetCustom_MaterialList(SSNo.Text.ToString());//,productTypeId,estimateId);
            List<CustomMaterialList> cmList = new List<CustomMaterialList>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    DataRow dr = ds.Tables[0].Rows[j];
                    CustomMaterialList cm = new CustomMaterialList();
                    cm.Id = Convert.ToInt16(dr["Id"]);
                    cm.MaterialList = dr["MaterialList"].ToString();
                    cm.VendorCategoryId = Convert.ToInt16(dr["VendorCategoryId"]);
                    cm.VendorCategoryName = dr["VendorCategoryNm"].ToString();
                    if (dr["VendorId"].ToString() != "")
                        cm.VendorId = Convert.ToInt16(dr["VendorId"]);
                    cm.VendorName = dr["VendorName"].ToString();
                    if (dr["Amount"].ToString() != "")
                        cm.Amount = Convert.ToDecimal(dr["Amount"]);
                    cm.DocName = dr["DocName"].ToString();
                    cm.TempName = dr["TempName"].ToString();
                    cm.IsForemanPermission = dr["IsForemanPermission"].ToString();
                    cm.IsSrSalemanPermissionF = dr["IsSrSalemanPermissionF"].ToString();
                    cm.IsAdminPermission = dr["IsAdminPermission"].ToString();
                    cm.IsSrSalemanPermissionA = dr["IsSrSalemanPermissionA"].ToString();
                    cm.Status = JGConstant.CustomMaterialListStatus.Unchanged;
                    cmList.Add(cm);
                }

                ViewState["CustomMaterialList"] = cmList;

                BindCustomMaterialList(cmList);
            }
            else
            {
                //List<CustomMaterialList> cmList1 = new List<CustomMaterialList>();

                //CustomMaterialList cm1 = new CustomMaterialList();
                //cm1.Id = 0;
                //cm1.MaterialList = "";
                //cm1.VendorCategoryId = 0;
                //cm1.VendorCategoryName = "";
                //cm1.VendorId = 0;
                //cm1.VendorName = "";
                //cm1.Amount = 0;
                //cm1.DocName = "";
                //cm1.TempName = "";
                //cm1.IsForemanPermission = "";
                //cm1.IsSrSalemanPermissionF = "";
                //cm1.IsAdminPermission = "";
                //cm1.IsSrSalemanPermissionA = "";
                //cm1.Status = JGConstant.CustomMaterialListStatus.Unchanged;
                //cmList1.Add(cm1);
                List<CustomMaterialList> cmList1 = BindEmptyRowToMaterialList();

                ViewState["CustomMaterialList"] = cmList1;
                BindCustomMaterialList(cmList1);
            }

        }

        private List<CustomMaterialList> BindEmptyRowToMaterialList()
        {
            List<CustomMaterialList> cmList1 = new List<CustomMaterialList>();
            cmList1 = GetMaterialListFromGrid();
            CustomMaterialList cm1 = new CustomMaterialList();
            cm1.Id = 0;
            cm1.MaterialList = "";
            cm1.VendorCategoryId = 0;
            cm1.VendorCategoryName = "";
            cm1.VendorId = 0;
            cm1.VendorName = "";
            cm1.Amount = 0;
            cm1.DocName = "";
            cm1.TempName = "";
            cm1.IsForemanPermission = "";
            cm1.IsSrSalemanPermissionF = "";
            cm1.IsAdminPermission = "";
            cm1.IsSrSalemanPermissionA = "";
            cm1.Status = JGConstant.CustomMaterialListStatus.Unchanged;
            cmList1.Add(cm1);

            return cmList1;
        }
        private List<CustomMaterialList> GetMaterialListFromGrid()
        {
            List<CustomMaterialList> itemList = new List<CustomMaterialList>();

            for (int i = 0; i < grdcustom_material_list.Rows.Count; i++)
            {
                CustomMaterialList cm = new CustomMaterialList();
                HiddenField hdnEmailStatus = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnEmailStatus");
               // string a = Convert.ToString(hdnEmailStatus.Value);

                    // Original code....
             //   if (Convert.ToString(hdnEmailStatus.Value) != "" && Convert.ToString(hdnEmailStatus.Value) !=null)
                if (hdnEmailStatus != null )
                    cm.EmailStatus = hdnEmailStatus.Value;

                HiddenField hdnForemanPermission = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnForemanPermission");
               // if (hdnForemanPermission.Value.ToString() != "")
                if (hdnForemanPermission != null)
                    cm.IsForemanPermission = hdnForemanPermission.Value;

                HiddenField hdnSrSalesmanPermissionF = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnSrSalesmanPermissionF");
               // if (hdnSrSalesmanPermissionF.Value.ToString() != "" )
                if (hdnSrSalesmanPermissionF != null)
                    cm.IsSrSalemanPermissionF = hdnSrSalesmanPermissionF.Value;

                HiddenField hdnAdminPermission = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnAdminPermission");
              //  if (hdnAdminPermission.Value.ToString() != "")
                if (hdnAdminPermission != null)
                    cm.IsAdminPermission = hdnAdminPermission.Value;

                HiddenField hdnSrSalesmanPermissionA = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnSrSalesmanPermissionA");
               // if (hdnSrSalesmanPermissionA.Value.ToString() != "")
                if (hdnSrSalesmanPermissionA != null)
                    cm.IsSrSalemanPermissionA = hdnSrSalesmanPermissionA.Value;

                HiddenField hdnMaterialListId = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnMaterialListId");
                //if (hdnMaterialListId.Value.ToString() != "")
                if (hdnMaterialListId != null)
                    cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
                TextBox txtMateriallist = (TextBox)grdcustom_material_list.Rows[i].FindControl("txtMateriallist");
                if (txtMateriallist != null)
                cm.MaterialList = txtMateriallist.Text;

                DropDownList ddlVendorCategory = (DropDownList)grdcustom_material_list.Rows[i].FindControl("ddlVendorCategory");
              //  if (ddlVendorCategory.SelectedIndex != -1)
                if (ddlVendorCategory !=null)
                {
                    cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
                    cm.VendorCategoryName = ddlVendorCategory.SelectedItem.Text;
                }
                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)grdcustom_material_list.Rows[i].FindControl("ddlVendorName");
               // if (ddlVendorName.SelectedIndex != -1)
                if (ddlVendorName != null)
                {
                    cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);
                    cm.VendorName = ddlVendorName.SelectedItem.Text;
                }

                LinkButton lnkQuote = (LinkButton)grdcustom_material_list.Rows[i].FindControl("lnkQuote");
                if (lnkQuote != null)
                {
                    if (lnkQuote.Text != "")
                    {
                        cm.DocName = lnkQuote.Text;
                        cm.TempName = lnkQuote.CommandArgument;
                    }
                }
                TextBox txtAmount = (TextBox)grdcustom_material_list.Rows[i].FindControl("txtAmount");
                if (txtAmount != null)
                {
                    if (txtAmount.Text != "")
                        cm.Amount = Convert.ToDecimal(txtAmount.Text);
                }
                itemList.Add(cm);
            }
            return itemList;
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            try
            {

                //For checking...
               // BindNewGrid(SSNo.Text, Convert.ToInt32(CusId.Text));


                DataSet dsNew = (DataSet)(Session["MaterialList"]);
                Session["AddFlag"] = "Yes";
                //Session["MaterialListProductId"] = ProductId.Value;
                dtAddLine = (DataTable)dsNew.Tables[0];
                DataTable DtTemp = dsNew.Tables[0].Clone();
                //DataTable dtCustomers = new DataTable("Customers");

                int rowIndex = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;
                 #region For finding whether data is fill or not
                TextBox txtLine1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtLine"));
                TextBox txtSkuPartNo1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtSkuPartNo"));
                TextBox txtDescription1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtDescription"));
                TextBox txtQTY1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtQTY"));
                TextBox txtUOM1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtUOM"));
                DropDownCheckBoxes ddlVendorName1 = (DropDownCheckBoxes)(grdcustom_material_list.Rows[rowIndex].FindControl("ddlVendorName"));
                TextBox txtMaterialCost1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtMaterialCost"));
                Label lblCost1 = (Label)(grdcustom_material_list.Rows[rowIndex].FindControl("lblCost"));
                DropDownList ddlExtent1 = (DropDownList)(grdcustom_material_list.Rows[rowIndex].FindControl("ddlExtent"));
                LinkButton lblTotal1 = (LinkButton)(grdcustom_material_list.Rows[rowIndex].FindControl("lblTotal"));
                #endregion

                if (txtLine1.Text == "" && txtSkuPartNo1.Text == "" && txtDescription1.Text == "" && txtQTY1.Text == "" && txtUOM1.Text == "" && txtMaterialCost1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill all the details');", true);
                }
                else
                {

                    //int rowIndex;
                    for (int i = 0; i < grdcustom_material_list.Rows.Count; i++)
                    {
                        HiddenField HiddenField1 = (HiddenField)(grdcustom_material_list.Rows[i].FindControl("HiddenField1"));

                        HiddenField hdnCategorynew = (HiddenField)(grdcustom_material_list.Rows[i].FindControl("hdnCategorynew"));
                        HiddenField ProductId = (HiddenField)(grdcustom_material_list.Rows[i].FindControl("hidCategory"));
                        TextBox txtLine = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtLine"));
                        TextBox txtSkuPartNo = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtSkuPartNo"));
                        TextBox txtDescription = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtDescription"));
                        TextBox txtQTY = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtQTY"));
                        TextBox txtUOM = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtUOM"));
                        DropDownCheckBoxes ddlVendorName = (DropDownCheckBoxes)(grdcustom_material_list.Rows[i].FindControl("ddlVendorName"));
                        TextBox txtMaterialCost = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtMaterialCost"));
                        Label lblCost = (Label)(grdcustom_material_list.Rows[i].FindControl("lblCost"));
                        DropDownList ddlExtent = (DropDownList)(grdcustom_material_list.Rows[i].FindControl("ddlExtent"));
                        LinkButton lblTotal = (LinkButton)(grdcustom_material_list.Rows[i].FindControl("lblTotal"));
                        
                        DataRow drAddLine = dtAddLine.NewRow();
                        DataRow dr = DtTemp.NewRow();
                        if (ProductId.Value != "")
                        {
                            Session["MaterialListProductId"] = ProductId.Value;
                        }
                        if (ProductId.Value != "")
                        {
                            dr["ProductId"] = ProductId.Value;
                            drAddLine["ProductId"] = ProductId.Value;
                        }
                        else
                        {
                            dr["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                            drAddLine["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                        }
                        //dr["ProductName"] ="";
                        dr["MyRow"] = "NotVisible";
                        dr["Line"] = txtLine.Text;
                        dr["JGSkuPartNo"] = txtSkuPartNo.Text;
                        dr["Description"] = txtDescription.Text;

                        drAddLine["MyRow"] = "NotVisible";
                        drAddLine["Line"] = txtLine.Text;
                        drAddLine["JGSkuPartNo"] = txtSkuPartNo.Text;
                        drAddLine["Description"] = txtDescription.Text;
                        if (txtQTY.Text != "")
                        {
                            dr["Qty"] = txtQTY.Text;
                            drAddLine["Qty"] = txtQTY.Text;
                        }
                        else
                        {
                            dr["Qty"] = 0;
                            drAddLine["Qty"] = 0;
                        }
                        //dr["VendorQuotes"] = ddlVendorName;
                        dr["UOM"] = txtUOM.Text;
                        dr["VendorQuotes"] = "";

                        drAddLine["UOM"] = txtUOM.Text;
                        drAddLine["VendorQuotes"] = "";
                        if (txtMaterialCost.Text != "")
                        {
                            dr["MaterialCost"] = txtMaterialCost.Text;
                            drAddLine["MaterialCost"] = txtMaterialCost.Text;
                        }
                        else
                        {
                            dr["MaterialCost"] = 0;
                            drAddLine["MaterialCost"] = 0;
                        }
                        if (lblCost.Text != "")
                        {
                            dr["SubTotal"] = lblCost.Text;
                            drAddLine["SubTotal"] = lblCost.Text;
                        }
                        else
                        {
                            dr["SubTotal"] = 0;
                            drAddLine["SubTotal"] = lblCost.Text;
                        }
                        if (ddlExtent.SelectedValue != "")
                        {
                            dr["Extent"] = ddlExtent.SelectedValue;
                            drAddLine["Extent"] = ddlExtent.SelectedValue;
                        }
                        else
                        {
                            dr["Extent"] = "Select";
                            drAddLine["Extent"] = ddlExtent.SelectedValue;
                        }
                        if (lblTotal.Text != "")
                        {
                            dr["Total"] = lblTotal.Text;
                            drAddLine["Total"] = lblTotal.Text;
                        }
                        else
                        {
                            dr["Total"] = 0;
                            drAddLine["Total"] = 0;
                        }
                        //  dr["Display"] = hdnCategorynew.Value;
                        //    drAddLine["Display"] = hdnCategorynew.Value;
                        dr["Display"] = "NotVisible";
                        dr["Visible"] = "NotVisible";
                        drAddLine["Display"] = "NotVisible";
                        drAddLine["Visible"] = "NotVisible";
                        //dsNew.Tables[0].Rows.InsertAt(dr, i);
                        DtTemp.Rows.InsertAt(dr, i);
                        // dtAddLine.Rows.InsertAt(drAddLine, i);
                    }

                    //if (Session["RowIndex"] != null)
                    //{
                    //    rowIndex = Convert.ToInt32(Session["RowIndex"]);
                    //}
                    //else
                    //{
                    //    rowIndex = 0;
                    //}
                    DataRow drNew = DtTemp.NewRow();
                    drNew["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                    drNew["Extent"] = "Select";
                    DtTemp.Rows.InsertAt(drNew, rowIndex + 1);

                    DataRow drAddNew = dtAddLine.NewRow();
                    drAddNew["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                    drAddNew["Extent"] = "Select";

                    dtAddLine.Rows.InsertAt(drAddNew, rowIndex + 1);

                    dtAddLine.Rows[rowIndex + 1]["MyRow"] = "NotVisible";
                    dtAddLine.Rows[rowIndex + 1]["Visible"] = "NotVisible";
                    Session["Visible"] = "NotVisible";
                    grdcustom_material_list.DataSource = dtAddLine;
                    grdcustom_material_list.DataBind();
                    // grdcustom_material_list.DataSource = DtTemp;
                    // grdcustom_material_list.DataBind();
                    Session["MaterialListProductId"] = 0;

                    Session["MaterialList"] = dsNew;
                   
                    // ViewState["newDT"] = DtTemp;
                    ViewState["newDT"] = dtAddLine;
                    //GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent.Parent.Parent;
                    //TextBox txt = (TextBox)currentRow.FindControl("txtQTY");
                    //TextBox txtCost = (TextBox)currentRow.FindControl("txtMaterialCost");
                    //Label lblCost = (Label)currentRow.FindControl("lblCost");
                    //DropDownList ddl = (DropDownList)currentRow.FindControl("ddlExtent");
                    //Label lblTotal = (Label)currentRow.FindControl("lblTotal");
                    //if (Session["RowIndex"] != null)
                    //{
                    //    rowIndex = Convert.ToInt32(Session["RowIndex"]);
                    //}
                    //else
                    //{
                    //    rowIndex = 0;
                    //}

                    ////Loop through the GridView and copy rows.
                    //foreach (GridViewRow row in grdcustom_material_list.Rows)
                    //{

                    //    for (int i = 0; i < row.Cells.Count; i++)
                    //    {
                    //        dtCustomers.Rows[row.RowIndex][i] = row.Cells[i].Text;
                    //    }
                    //}

                    ////if (dsNew.Tables.Count > 0)
                    ////{
                    ////    DataRow dr = dsNew.Tables[0].NewRow();
                    ////    dsNew.Tables[0].Rows.Add(dr);
                    ////}

                    //if (dtCustomers.Rows.Count > 0)
                    //{
                    //    DataRow dr = dtCustomers.NewRow();
                    //    dtCustomers.Rows.InsertAt(dr, rowIndex);
                    //}
                    //grdcustom_material_list.DataSource = dtCustomers;
                    //grdcustom_material_list.DataBind();
                    //List<CustomMaterialList> cmList = BindEmptyRowToMaterialList();
                    //ViewState["CustomMaterialList"] = cmList;
                    //BindCustomMaterialList(cmList);
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //For checking...
              //  BindNewGrid(SSNo.Text, Convert.ToInt32(CusId.Text));


                DataSet dsNew = (DataSet)(Session["MaterialList"]);
                Session["AddFlag"] = null;
                //Session["MaterialListProductId"] = ProductId.Value;
                dtAddLine = (DataTable)dsNew.Tables[0];
                DataTable DtTemp = dsNew.Tables[0].Clone();
                //DataTable dtCustomers = new DataTable("Customers");

                int rowIndex = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;
                #region For finding whether data is fill or not
                TextBox txtLine1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtLine"));
                TextBox txtSkuPartNo1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtSkuPartNo"));
                TextBox txtDescription1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtDescription"));
                TextBox txtQTY1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtQTY"));
                TextBox txtUOM1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtUOM"));
                DropDownCheckBoxes ddlVendorName1 = (DropDownCheckBoxes)(grdcustom_material_list.Rows[rowIndex].FindControl("ddlVendorName"));
                TextBox txtMaterialCost1 = (TextBox)(grdcustom_material_list.Rows[rowIndex].FindControl("txtMaterialCost"));
                Label lblCost1 = (Label)(grdcustom_material_list.Rows[rowIndex].FindControl("lblCost"));
                DropDownList ddlExtent1 = (DropDownList)(grdcustom_material_list.Rows[rowIndex].FindControl("ddlExtent"));
                LinkButton lblTotal1 = (LinkButton)(grdcustom_material_list.Rows[rowIndex].FindControl("lblTotal"));
                #endregion

                if (txtLine1.Text == "" && txtSkuPartNo1.Text == "" && txtDescription1.Text == "" && txtQTY1.Text == "" && txtUOM1.Text == "" && txtMaterialCost1.Text == "")
                {
                  //  ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill all the details;", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill all the details');", true);
                }
                else
                {
                //int rowIndex;
                for (int i = 0; i < grdcustom_material_list.Rows.Count; i++)
                {
                    HiddenField hdnCategorynew = (HiddenField)(grdcustom_material_list.Rows[i].FindControl("hdnCategorynew"));
                    HiddenField ProductId = (HiddenField)(grdcustom_material_list.Rows[i].FindControl("hidCategory"));
                    HiddenField HiddenField1 = (HiddenField)(grdcustom_material_list.Rows[i].FindControl("HiddenField1"));
                    #region New Dropdown Added Of Product Category..
                    DropDownList ddlCategory = (DropDownList)(grdcustom_material_list.Rows[i].FindControl("ddlCategory"));
                    #endregion

                    TextBox txtLine = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtLine"));
                    TextBox txtSkuPartNo = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtSkuPartNo"));
                    TextBox txtDescription = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtDescription"));
                    TextBox txtQTY = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtQTY"));
                    TextBox txtUOM = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtUOM"));
                    DropDownCheckBoxes ddlVendorName = (DropDownCheckBoxes)(grdcustom_material_list.Rows[i].FindControl("ddlVendorName"));
                    TextBox txtMaterialCost = (TextBox)(grdcustom_material_list.Rows[i].FindControl("txtMaterialCost"));
                    Label lblCost = (Label)(grdcustom_material_list.Rows[i].FindControl("lblCost"));
                    DropDownList ddlExtent = (DropDownList)(grdcustom_material_list.Rows[i].FindControl("ddlExtent"));
                    //Label lblTotal = (Label)(grdcustom_material_list.Rows[i].FindControl("lblTotal"));
                    LinkButton lblTotal = (LinkButton)(grdcustom_material_list.Rows[i].FindControl("lblTotal"));
                    
                    DataRow drAddLine = dtAddLine.NewRow();
                    DataRow dr = DtTemp.NewRow();
                    if (ProductId.Value != "")
                    {
                        Session["MaterialListProductId"] = ProductId.Value;
                    }
                    if (ProductId.Value != "")
                    {
                        dr["ProductId"] = ProductId.Value;
                        drAddLine["ProductId"] = ProductId.Value;
                    }
                    else
                    {
                        dr["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                        drAddLine["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                    }
                    //dr["ProductName"] = ProductId.Value;
                  //  dr["ProductName"] ="";  //Origional code commented....
                    dr["MyRow"] = "Visible";
                    dr["Visible"] = "Visible";
                    dr["Line"] = txtLine.Text;
                    dr["JGSkuPartNo"] = txtSkuPartNo.Text;
                    dr["Description"] = txtDescription.Text;

                    drAddLine["MyRow"] ="Visible";
                    drAddLine["Visible"] = "Visible";
                    drAddLine["Line"] = txtLine.Text;
                    drAddLine["JGSkuPartNo"] = txtSkuPartNo.Text;
                    drAddLine["Description"] = txtDescription.Text;

                    if (txtQTY.Text != "")
                    {
                        dr["Qty"] = txtQTY.Text;
                        drAddLine["Qty"] = txtQTY.Text;
                    }
                    else
                    {
                        dr["Qty"] = 0;
                        drAddLine["Qty"] =0;
                    }
                    //dr["VendorQuotes"] = ddlVendorName;
                    dr["UOM"] = txtUOM.Text;
                    dr["VendorQuotes"] = "";

                    drAddLine["UOM"] = txtUOM.Text;
                    drAddLine["VendorQuotes"] = "";
                    if (txtMaterialCost.Text != "")
                    {
                        dr["MaterialCost"] = txtMaterialCost.Text;
                        drAddLine["MaterialCost"] = txtMaterialCost.Text;
                    }
                    else
                    {
                        dr["MaterialCost"] = 0;
                        drAddLine["MaterialCost"] = 0;
                    }
                    if (lblCost.Text != "")
                    {
                        dr["SubTotal"] = lblCost.Text;
                        drAddLine["SubTotal"] = lblCost.Text;
                    }
                    else
                    {
                        dr["SubTotal"] = 0;
                        drAddLine["SubTotal"] = 0;
                    }
                    if (ddlExtent.SelectedValue != "")
                    {
                        dr["Extent"] = ddlExtent.SelectedValue;
                        drAddLine["Extent"] = ddlExtent.SelectedValue;
                    }
                    else
                    {
                        dr["Extent"] = "Select";
                        drAddLine["Extent"] = "Select";
                    }
                    if (lblTotal.Text != "")
                    {
                        dr["Total"] = lblTotal.Text;
                        drAddLine["Total"] = lblTotal.Text;
                    }
                    else
                    {
                        dr["Total"] = 0;
                        drAddLine["Total"] = 0;
                    }
                    dr["Display"] = hdnCategorynew.Value;

                    drAddLine["Display"] = hdnCategorynew.Value;
                    //dsNew.Tables[0].Rows.InsertAt(dr, i);
                    DtTemp.Rows.InsertAt(dr, i);
                  //  dtAddLine.Rows.InsertAt(drAddLine, i);
                }

                //if (Session["RowIndex"] != null)
                //{
                //    rowIndex = Convert.ToInt32(Session["RowIndex"]);
                //}
                //else
                //{
                //    rowIndex = 0;
                //}
                
                DataRow drAddNew = dtAddLine.NewRow();
                drAddNew["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                drAddNew["Extent"] = "Select";
                dtAddLine.Rows.InsertAt(drAddNew, rowIndex + 1);
                int a = grdcustom_material_list.Rows.Count;
                dtAddLine.Rows[rowIndex + 1]["MyRow"] = "Visible";
                dtAddLine.Rows[rowIndex + 1]["Visible"] = "Visible";
                Session["Visible"] = "Visible";

                DataRow drNew = DtTemp.NewRow();
                drNew["ProductId"] = Convert.ToInt32(Session["MaterialListProductId"]);
                drNew["Extent"] = "Select";
                DtTemp.Rows.InsertAt(drNew, rowIndex + 1);

                grdcustom_material_list.DataSource = dtAddLine;
                grdcustom_material_list.DataBind();
                
                //grdcustom_material_list.DataSource = DtTemp;
                //grdcustom_material_list.DataBind();
                Session["MaterialListProductId"] = 0;

                Session["MaterialList"] = dsNew;
              
               // ViewState["newDT"] = DtTemp;
                ViewState["newDT"] = dtAddLine;
                //GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent.Parent.Parent;
                //TextBox txt = (TextBox)currentRow.FindControl("txtQTY");
                //TextBox txtCost = (TextBox)currentRow.FindControl("txtMaterialCost");
                //Label lblCost = (Label)currentRow.FindControl("lblCost");
                //DropDownList ddl = (DropDownList)currentRow.FindControl("ddlExtent");
                //Label lblTotal = (Label)currentRow.FindControl("lblTotal");
                //if (Session["RowIndex"] != null)
                //{
                //    rowIndex = Convert.ToInt32(Session["RowIndex"]);
                //}
                //else
                //{
                //    rowIndex = 0;
                //}

                ////Loop through the GridView and copy rows.
                //foreach (GridViewRow row in grdcustom_material_list.Rows)
                //{

                //    for (int i = 0; i < row.Cells.Count; i++)
                //    {
                //        dtCustomers.Rows[row.RowIndex][i] = row.Cells[i].Text;
                //    }
                //}

                ////if (dsNew.Tables.Count > 0)
                ////{
                ////    DataRow dr = dsNew.Tables[0].NewRow();
                ////    dsNew.Tables[0].Rows.Add(dr);
                ////}

                //if (dtCustomers.Rows.Count > 0)
                //{
                //    DataRow dr = dtCustomers.NewRow();
                //    dtCustomers.Rows.InsertAt(dr, rowIndex);
                //}
                //grdcustom_material_list.DataSource = dtCustomers;
                //grdcustom_material_list.DataBind();
                //List<CustomMaterialList> cmList = BindEmptyRowToMaterialList();
                //ViewState["CustomMaterialList"] = cmList;
                //BindCustomMaterialList(cmList);
                }
            }
            catch (Exception ex)
            {

            }
      
        }

        private List<CustomMaterialList> GetMaterialListFromViewState()
        {
            List<CustomMaterialList> itemList = null;

            if (ViewState["CustomMaterialList"] == null)
            {
                itemList = new List<CustomMaterialList>();
            }
            else
            {
                itemList = ViewState["CustomMaterialList"] as List<CustomMaterialList>;
            }
            return itemList;
        }

        protected void UpdateMaterialList(CustomMaterialList item, int rowIndex = 0)
        {
            List<CustomMaterialList> itemList = GetMaterialListFromGrid();

            switch (item.Status)
            {
                case JGConstant.CustomMaterialListStatus.Unchanged:
                    break;
                case JGConstant.CustomMaterialListStatus.Added:
                    itemList.Add(item);
                    break;
                case JGConstant.CustomMaterialListStatus.Deleted:
                    itemList[rowIndex].Status = JGConstant.CustomMaterialListStatus.Deleted;
                    break;
                case JGConstant.CustomMaterialListStatus.Modified:
                    itemList[rowIndex] = item;
                    break;
                default:
                    break;
            }

            ViewState["CustomMaterialList"] = itemList;
            BindCustomMaterialList(itemList);
        }

        protected void BindCustomMaterialList(List<CustomMaterialList> itemList = null)
        {
            if (itemList == null)
            {
                itemList = GetMaterialListFromViewState();
            }
            List<CustomMaterialList> cmList = itemList.Where(c => c.Status != JGConstant.CustomMaterialListStatus.Deleted).ToList();
            grdcustom_material_list.DataSource = cmList;
            grdcustom_material_list.DataBind();
            int j = 0;
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);

            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                CustomMaterialList cml = cmList[j];
                if (cml.Status != JGConstant.CustomMaterialListStatus.Deleted)
                {
                    Label lblsrno = (Label)r.FindControl("lblsrno");

                    DropDownList ddlVendorCategory1 = (DropDownList)r.FindControl("ddlVendorCategory");
                    Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                    TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                    LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                    //HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
                    //HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
                    HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
                    HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
                    HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
                    HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");

                    lblsrno.Text = (j + 1).ToString();
                    if (cml.VendorCategoryId.ToString() != "")
                    {
                        ddlVendorCategory1.SelectedValue = cml.VendorCategoryId.ToString();
                    }
                    else
                    {
                        ddlVendorCategory1.SelectedIndex = -1;
                    }
                    if (cml.VendorId.ToString() != "")
                    {
                        int selectedCategoryID = Convert.ToInt16(ddlVendorCategory1.SelectedItem.Value);
                        DataSet ds = GetVendorNames(selectedCategoryID);
                        ddlVendorName.DataSource = ds;
                        ddlVendorName.SelectedIndex = -1;
                        ddlVendorName.DataTextField = "VendorName";
                        ddlVendorName.DataValueField = "VendorId";
                        ddlVendorName.DataBind();
                        ddlVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

                        ddlVendorName.SelectedValue = cml.VendorId.ToString();

                    }
                    else
                    {
                        ddlVendorName.SelectedIndex = -1;
                    }

                    if (cml.Amount.ToString() != "")
                    {
                        txtAmount.Text = cml.Amount.ToString();

                    }
                    else
                    {
                        txtAmount.Text = string.Empty;
                    }
                    //if (Convert.ToInt16(cml.Id.ToString()) != 0)
                    //{
                    //    hdnMaterialListId.Value = cml.Id.ToString();
                    //}
                    //else
                    //{
                    //    hdnMaterialListId.Value = "0";
                    //}
                    if (cml.IsForemanPermission != "")
                    {
                        hdnForemanPermission.Value = cml.IsForemanPermission;
                    }
                    else
                    {
                        hdnForemanPermission.Value = "";
                    }
                    if (cml.IsSrSalemanPermissionF != "")
                    {
                        hdnSrSalesmanPermissionF.Value = cml.IsSrSalemanPermissionF;
                    }
                    else
                    {
                        hdnSrSalesmanPermissionF.Value = "";
                    }
                    if (cml.IsAdminPermission != "")
                    {
                        hdnAdminPermission.Value = cml.IsAdminPermission;
                    }
                    else
                    {
                        hdnAdminPermission.Value = "";
                    }
                    if (cml.IsSrSalemanPermissionA != "")
                    {
                        hdnSrSalesmanPermissionA.Value = cml.IsSrSalemanPermissionA;
                    }
                    else
                    {
                        hdnSrSalesmanPermissionA.Value = "";
                    }
                    //if (cml.EmailStatus != "")
                    //{
                    //    hdnEmailStatus.Value = cml.EmailStatus;
                    //}
                    //else
                    //{
                    //    hdnEmailStatus.Value = "";
                    //}
                }
                if (emailStatus == JGConstant.EMAIL_STATUS_VENDORCATEGORIES)
                {
                    EnableVendorNameAndAmount();
                }
                j++;
            }
        }

        protected void ddlVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlVendorName = (DropDownList)sender;
            string selectedName = ddlVendorName.SelectedItem.Text;
            int vendorId = Convert.ToInt16(ddlVendorName.SelectedValue.ToString());

            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                if (selectedName == "Select")
                {
                    ddlVendorName.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select a vendor name');", true);

                }
            }
            DataSet ds = VendorBLL.Instance.GetVendorQuoteByVendorId(SSNo.Text, vendorId);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                ddlVendorName.SelectedIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First attach quote for this vendor');", true);

            }

            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                Saplin.Controls.DropDownCheckBoxes ddlVendorName1 = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                {
                    DataSet dsVendorQuoute = VendorBLL.Instance.GetVendorQuoteByVendorId(SSNo.Text, Convert.ToInt16(ddlVendorName1.SelectedValue));
                    LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                    if (dsVendorQuoute.Tables[0].Rows.Count > 0)
                    {
                        lnkQuote.Text = dsVendorQuoute.Tables[0].Rows[0]["DocName"].ToString();
                        lnkQuote.CommandArgument = dsVendorQuoute.Tables[0].Rows[0]["TempName"].ToString();
                    }
                    else
                    {
                        lnkQuote.Text = "";
                        lnkQuote.CommandArgument = "";
                    }
                }
            }
        }
        protected void ddlVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlVendorCategory = (DropDownList)sender;

            string selectedCategory = ddlVendorCategory.SelectedItem.Text;
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);
            int counter = 1;
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                if (selectedCategory == "Select")
                {
                    ddlVendorCategory.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select a vendor category');", true);

                }
                else if (((DropDownList)r.FindControl("ddlVendorCategory")).SelectedItem.Text == selectedCategory)
                {
                    if (counter == 2)
                    {
                        ddlVendorCategory.SelectedIndex = -1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('This Vendor Category is already selected');", true);

                    }
                    counter++;

                }
                if (emailStatus == JGConstant.EMAIL_STATUS_VENDORCATEGORIES)
                {
                    Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                    DropDownList ddlVendorCategorySelected = (DropDownList)r.FindControl("ddlVendorCategory");
                    LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                    if (ddlVendorCategory == ddlVendorCategorySelected)
                    {
                        int selectedCategoryID = Convert.ToInt16(ddlVendorCategory.SelectedItem.Value);
                        DataSet ds = GetVendorNames(selectedCategoryID);
                        ddlVendorName.DataSource = ds;
                        ddlVendorName.SelectedIndex = -1;
                        ddlVendorName.DataTextField = "VendorName";
                        ddlVendorName.DataValueField = "VendorId";
                        ddlVendorName.DataBind();
                        ddlVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                        ddlVendorName.SelectedIndex = 0;

                        lnkQuote.Text = "";
                    }
                }
            }
        }
            
       // Original code commented.....

        protected void grdcustom_material_list_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //List<CustomMaterialList> cmList = GetMaterialListFromGrid();
            //if (cmList.Count > 1)
            //{
            //    CustomMaterialList cm = cmList[e.RowIndex];
            //    cm.Status = JGConstant.CustomMaterialListStatus.Deleted;
            //    UpdateMaterialList(cm, e.RowIndex);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Atleast one row must be there in Custom- Material List');", true);
            //}
            /*
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["StudentList"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["dt"] = dt;
            BindGrid();*/
            try
            {
                  int index = Convert.ToInt32(e.RowIndex);
              //  string SoldId = Convert.ToString(ViewState["SSNo"]);
              //  int CustomerId = Convert.ToInt32(ViewState["CustId"]);
              //  DataSet ds = VendorBLL.Instance.GetMaterialListData(SoldId, CustomerId);
              //  DataTable dtDelete = ds.Tables[0];

                  if (index != 0)
                  {
                    //  DataTable dtDelete = (DataTable)ViewState["newDT"];
                      DataSet dsSe = new DataSet();
                      dsSe = (DataSet)Session["MaterialList"];
                      DataTable dtDelete = dsSe.Tables[0];// dtDelete.Rows.RemoveAt(e.RowIndex);
                      dtDelete.Rows[index].Delete();

                      grdcustom_material_list.DataSource = dtDelete;
                      grdcustom_material_list.DataBind();
                      DataSet d = new DataSet();
                      d.Tables.Add(dtDelete);
                      Session["MaterialList"] = d;
                  }
                  else
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Row cannot be deleted.');", true);
                  }
            }
            catch (Exception)
            {

              //  throw;
            }
        }



        //protected void grdcustom_material_list_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    List<CustomMaterialList> cmList = GetMaterialListFromGrid();
        //    if (cmList.Count > 1)
        //    {
        //        CustomMaterialList cm = cmList[e.RowIndex];
        //        cm.Status = JGConstant.CustomMaterialListStatus.Deleted;
        //        UpdateMaterialList(cm, e.RowIndex);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Atleast one row must be there in Custom- Material List');", true);
        //    }
        //}
        protected void lnkVendorCategory_Click(object sender, EventArgs e)
        {
            pnlEmailTemplateForVendorCategories.Visible = true;
            pnlEmailTemplateForVendors.Visible = false;
            lnkVendorCategory.ForeColor = System.Drawing.Color.DarkGray;
            lnkVendorCategory.Enabled = false;
            lnkVendor.Enabled = true;
            lnkVendor.ForeColor = System.Drawing.Color.Blue;
            bind();
        }
        protected void bindVendorTemplate()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(100);
            if (ds != null)
            {
                HeaderEditorVendor.Content = ds.Tables[0].Rows[0][0].ToString();
                lblMaterialsVendor.Text = ds.Tables[0].Rows[0][1].ToString();
                FooterEditorVendor.Content = ds.Tables[0].Rows[0][2].ToString();
            }
        }
        protected void lnkVendor_Click(object sender, EventArgs e)
        {
            pnlEmailTemplateForVendors.Visible = true;
            pnlEmailTemplateForVendorCategories.Visible = false;
            lnkVendor.ForeColor = System.Drawing.Color.DarkGray;
            lnkVendor.Enabled = false;
            lnkVendorCategory.Enabled = true;
            lnkVendorCategory.ForeColor = System.Drawing.Color.Blue;
            bindVendorTemplate();
        }
        protected void lnkdelete_Click(object sender, EventArgs e)
        {
            if (grdcustom_material_list.Rows.Count > 1)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Atleast one row must be there in Custom- Material List');", true);
            }

        }

        
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procurement.aspx");
        }

        protected void btnXForeman_Click(object sender, EventArgs e)
        {
            //popupForeman_permission.Hide();
        }
        protected void btnXSrSalesmanF_Click(object sender, EventArgs e)
        {
            //popupSrSalesmanPermissionF.Hide();
        }

        protected void btnXSrSalesmanA_Click(object sender, EventArgs e)
        {
            //popupSrSalesmanPermissionA.Hide();
        }

        protected void btnXAdmin_Click(object sender, EventArgs e)
        {
            //popupAdmin_permission.Hide();
        }

        //protected void VerifyForemanPermission(object sender, EventArgs e)
        //{
        //    int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
        //    if (cResult == 1)
        //    {
        //        if (!string.IsNullOrEmpty(txtForemanPasswordNew.Text))
        //        {
        //            string adminCode = AdminBLL.Instance.GetForemanCode();
        //            if (adminCode != txtForemanPasswordNew.Text.Trim())
        //            {
        //                CVForeman.ErrorMessage = "Invalid Foreman Code";
        //                CVForeman.ForeColor = System.Drawing.Color.Red;
        //                CVForeman.IsValid = false;
        //                CVForeman.Visible = true;
        //                //popupForeman_permission.Show();
        //                return;
        //            }
        //            else
        //            {
        //                int result = CustomBLL.Instance.UpdateForemanPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED);//, productTypeId, estimateId);
        //                if (result == 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
        //                }
        //                else
        //                {
        //                    txtForemanPasswordNew.Enabled = false;
        //                    txtForemanPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
        //                    //lnkForemanPermission.Enabled = false;
        //                    //lnkForemanPermission.ForeColor = System.Drawing.Color.DarkGray;
        //                    //popupForeman_permission.TargetControlID = "hdnForeman";
        //                    SetButtonText();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CVForeman.ErrorMessage = "Please Enter Foreman Code";
        //            CVForeman.ForeColor = System.Drawing.Color.Red;
        //            CVForeman.IsValid = false;
        //            CVForeman.Visible = true;
        //            //popupForeman_permission.Show();
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
        //    }
        //}

        //protected void VerifySrSalesmanPermissionF(object sender, EventArgs e)
        //{
        //    int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
        //    if (cResult == 1)
        //    {
        //        if (!string.IsNullOrEmpty(txtSrSalesManPermition.Text))
        //        {
        //            string salesmanCode = Session["loginpassword"].ToString();
        //            if (salesmanCode != txtSrSalesManPermition.Text.Trim())
        //            {
        //                CVSrSalesmanF.ErrorMessage = "Invalid Sr. Salesman Code";
        //                CVSrSalesmanF.ForeColor = System.Drawing.Color.Red;
        //                CVSrSalesmanF.IsValid = false;
        //                CVSrSalesmanF.Visible = true;
        //                //popupSrSalesmanPermissionF.Show();
        //                return;
        //            }
        //            else
        //            {
        //                int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialListF(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED);//, productTypeId, estimateId);
        //                if (result == 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
        //                }
        //                else
        //                {
        //                    txtSrSalesManPermition.Enabled = false;
        //                    txtSrSalesManPermition.ForeColor = System.Drawing.Color.DarkGray;

        //                    //lnkSrSalesmanPermissionF.Enabled = false;
        //                    //lnkSrSalesmanPermissionF.ForeColor = System.Drawing.Color.DarkGray;
        //                    //popupSrSalesmanPermissionF.TargetControlID = "hdnSrF";
        //                    SetButtonText();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CVSrSalesmanF.ErrorMessage = "Please Enter Sr. Salesman Code";
        //            CVSrSalesmanF.ForeColor = System.Drawing.Color.Red;
        //            CVSrSalesmanF.IsValid = false;
        //            CVSrSalesmanF.Visible = true;
        //            //popupSrSalesmanPermissionF.Show();
        //            return;

        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
        //    }
        //}

        //protected void VerifyAdminPermission(object sender, EventArgs e)
        //{
        //    int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(jobId);//,productTypeId,estimateId);
        //    if (cResult == 1)
        //    {
        //        if (!string.IsNullOrEmpty(txtAdminPasswordNew.Text))
        //        {
        //            string adminCode = AdminBLL.Instance.GetAdminCode();
        //            if (adminCode != txtAdminPasswordNew.Text.Trim())
        //            {
        //                CVAdmin.ErrorMessage = "Invalid Admin Code";
        //                CVAdmin.ForeColor = System.Drawing.Color.Red;
        //                CVAdmin.IsValid = false;
        //                CVAdmin.Visible = true;
        //                //popupAdmin_permission.Show();
        //                return;
        //            }
        //            else
        //            {
        //                int result = CustomBLL.Instance.UpdateAdminPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED);//, productTypeId, estimateId);
        //                if (result == 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
        //                }
        //                else
        //                {
        //                    txtAdminPasswordNew.Enabled = false;
        //                    txtAdminPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
        //                    //lnkAdminPermission.Enabled = false;
        //                    //lnkAdminPermission.ForeColor = System.Drawing.Color.DarkGray;
        //                    //popupAdmin_permission.TargetControlID = "hdnAdmin";
        //                    SetButtonText();
        //                    DisableVendorNameAndAmount();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CVAdmin.ErrorMessage = "Please Enter Admin Code";
        //            CVAdmin.ForeColor = System.Drawing.Color.Red;
        //            CVAdmin.IsValid = false;
        //            CVAdmin.Visible = true;
        //            //popupAdmin_permission.Show();
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List and enter all vendor names');", true);
        //    }
        //    //message mail is not sent to categories
        //}

        //protected void VerifySrSalesmanPermissionA(object sender, EventArgs e)
        //{
        //    int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(jobId);//, productTypeId, estimateId);
        //    if (cResult == 1)
        //    {
        //        if (!string.IsNullOrEmpty(txtSrSalesPassword.Text))
        //        {
        //            string salesmanCode = Session["loginpassword"].ToString();
        //            if (salesmanCode != txtSrSalesPassword.Text.Trim())
        //            {
        //                CVSrSalesmanA.ErrorMessage = "Invalid Sr. Salesman Code";
        //                CVSrSalesmanA.ForeColor = System.Drawing.Color.Red;
        //                CVSrSalesmanA.IsValid = false;
        //                CVSrSalesmanA.Visible = true;
        //                //popupSrSalesmanPermissionA.Show();
        //                return;
        //            }
        //            else
        //            {
        //                int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED);//, productTypeId, estimateId);
        //                if (result == 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
        //                }
        //                else
        //                {
        //                    txtSrSalesPassword.Enabled = false;
        //                    txtSrSalesPassword.ForeColor = System.Drawing.Color.DarkGray;
        //                    //lnkSrSalesmanPermissionA.Enabled = false;
        //                    //lnkSrSalesmanPermissionA.ForeColor = System.Drawing.Color.DarkGray;
        //                    //popupSrSalesmanPermissionA.TargetControlID = "hdnSrA";
        //                    SetButtonText();
        //                    DisableVendorNameAndAmount();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CVSrSalesmanA.ErrorMessage = "Please Enter Sr. Salesman Code";
        //            CVSrSalesmanA.ForeColor = System.Drawing.Color.Red;
        //            CVSrSalesmanA.IsValid = false;
        //            CVSrSalesmanA.Visible = true;
        //            //popupSrSalesmanPermissionA.Show();
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List and enter all vendor names');", true);
        //    }
        //}

        protected void disableAddDeleteLinks()
        {
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                LinkButton lnkAdd = (LinkButton)r.FindControl("lnkAdd");
                lnkAdd.Enabled = false;
                lnkAdd.ForeColor = System.Drawing.Color.DarkGray;
                LinkButton lnkdelete = (LinkButton)r.FindControl("lnkdelete");
                lnkdelete.Enabled = false;
                lnkdelete.ForeColor = System.Drawing.Color.DarkGray;
            }

        }

        //Save functionality.......
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            flag = "";
            SaveMaterialList();
        }

        private void SaveMaterialList()
        {
            try
            {
                StringBuilder strerr = new StringBuilder();

              //  lblException.Text = Convert.ToString(strerr.Append("Status start"));
                string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);
             //   lblException.Text = Convert.ToString(strerr.Append("End Status "));
                List<CustomMaterialList> cmList = new List<CustomMaterialList>();
                foreach (GridViewRow r in grdcustom_material_list.Rows)
                {
                  //  lblException.Text = Convert.ToString(strerr.Append("Customer List found "));
                     HiddenField HiddenVisible = (HiddenField)r.FindControl("HiddenField1");

                    CustomMaterialList cm = new CustomMaterialList();
                    HiddenField hidCategory = (HiddenField)r.FindControl("hidCategory");
                    TextBox txtLine = (TextBox)r.FindControl("txtLine");
                    TextBox txtSkuPartNo = (TextBox)r.FindControl("txtSkuPartNo");
                    TextBox txtDescription = (TextBox)r.FindControl("txtDescription");
                    TextBox txtQTY = (TextBox)r.FindControl("txtQTY");
                    TextBox txtUOM = (TextBox)r.FindControl("txtUOM");
                    TextBox txtMaterialCost = (TextBox)r.FindControl("txtMaterialCost");
                    LinkButton lblTotal = (LinkButton)r.FindControl("lblTotal");
                    DropDownList ddlExtent = (DropDownList)r.FindControl("ddlExtent");
                    //DropDownCheckBoxes ddlVendorName = (DropDownCheckBoxes)r.FindControl("ddlVendorName");

                    //DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
                    //cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
                    //TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
                    //HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
                    //database.AddInParameter(command, "@VendorQuotesPath"	varchar(MAX) = '',
                    //HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
                    //HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
                    //HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
                    //HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
                    //HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");
            
                    if (txtLine.Text == "")
                    {
                        if (flag == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill Line Item;", true);
                        }
                    }
                    else
                    {
                        cm.Line = txtLine.Text;
                    }

                    //if (hdnMaterialListId.Value != "")
                    //{
                    //    cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
                    //}
                    //else
                    //{
                    //    cm.Id = 0;
                    //}
                    Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                    //TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                    if (txtMaterialCost.Text == "")
                    {
                        if (flag == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter amount.');", true);
                            return;
                        }
                        else
                        {
                            cm.Amount = 0;
                        }
                    }
                    else
                    {
                        cm.Amount = Convert.ToDecimal(txtMaterialCost.Text);
                    }
                    // if (lnkAdminPermission.Enabled == true)
                    if (txtAdminPasswordNew.Visible == true)
                    {
                        cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                    }
                    else
                    {
                        cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                    }
                    //if (lnkSrSalesmanPermissionA.Enabled == true)
                    if (txtSrSalesPassword.Visible == true)
                    {
                        cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                    }
                    else
                    {
                        cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                    }
                    cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                    cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();

                    cm.EmailStatus = JGConstant.EMAIL_STATUS_VENDORCATEGORIES;
                    if (hidCategory.Value != "")
                    {
                        cm.ProductCatId = Convert.ToInt32(hidCategory.Value);
                    }
                    else
                    {
                        cm.ProductCatId = Convert.ToInt32(Convert.ToInt32(Session["MaterialListProductId"]));
                    }
                    cm.JGSkuPartNo = txtSkuPartNo.Text;
                    cm.Description = txtDescription.Text;
                    cm.MaterialList = txtDescription.Text;
                    cm.Quantity = txtQTY.Text;
                    cm.UOM = txtUOM.Text;
                    cm.extend = ddlExtent.SelectedValue;
                    if (lblTotal.Text != "")
                    {
                        cm.Total = Convert.ToDecimal(lblTotal.Text);
                    }
                    else
                    {
                        cm.Total = 0;
                    }
                    if (status == "C") //mail was already sent to vendor categories
                    {
                      //  lblException.Text = Convert.ToString(strerr.Append("mail was already sent to vendor categories"));
                        string VendorId = string.Empty;
                        for (int i = 0; i < ddlVendorName.Items.Count; i++)
                        {
                            if (ddlVendorName.Items[i].Selected)
                            {
                                if (VendorId == string.Empty)
                                {
                                    VendorId = ddlVendorName.Items[i].Value;
                                }
                                else
                                {
                                    VendorId = VendorId + "," + ddlVendorName.Items[i].Value;
                                }
                            }
                        }
                        cm.VendorIds = VendorId;
                        string VendorNames = string.Empty;
                        string VendorEmailIds = string.Empty;
                        DataSet ds = VendorBLL.Instance.getVendorDetails(VendorId);

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblException.Text = Convert.ToString(strerr.Append("VendorId found"));
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (VendorNames == string.Empty)
                                    {
                                        VendorNames = Convert.ToString(ds.Tables[0].Rows[i][1]);
                                    }
                                    else
                                    {
                                        VendorNames = VendorNames + "," + Convert.ToString(ds.Tables[0].Rows[i][1]);
                                    }
                                    if (VendorEmailIds == string.Empty)
                                    {
                                        VendorEmailIds = Convert.ToString(ds.Tables[0].Rows[i][6]);
                                    }
                                    else
                                    {
                                        VendorEmailIds = VendorEmailIds + "," + Convert.ToString(ds.Tables[0].Rows[i][6]);
                                    }
                                }
                            }
                        }
                        cm.VendorNames = VendorNames;
                        cm.VendorEmails = VendorEmailIds;
                        //cm.VendorName = ddlVendorName.SelectedItem.Text;
                        //cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);
                        //DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
                        //cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
                        //if (ddlVendorName.SelectedItem.Text == "Select")
                        //{
                        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select vendor name.');", true);
                        //    //return;
                        //}
                        //else
                        //{
                        //    cm.VendorName = ddlVendorName.SelectedItem.Text;
                        //    cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);

                        //    DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
                        //    cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
                        //}


                    }
                    else // mail was not sent to vendor categories
                    {
                        cm.VendorName = "";
                        cm.VendorEmail = "";
                        cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        // if (lnkForemanPermission.Enabled == true)
                        if (txtForemanPasswordNew.Visible == true)
                        {
                            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }
                        //if (lnkSrSalesmanPermissionF.Enabled == true)
                        if (txtSrSalesManPermition.Visible == true)
                        {
                            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }

                        cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
                        //if(Convert.ToString(Session["Visible"])=="Visible")
                        if (Convert.ToString(HiddenVisible.Value)!="")
                        {
                            cm.DisplaDLL = Convert.ToString(HiddenVisible.Value);
                        }
                        else
                        {
                            if (Convert.ToString(Session["Visible"]) == "Visible")
                            {
                                cm.DisplaDLL = "Visible";
                            }
                            else
                            {
                                 cm.DisplaDLL = "NotVisible";
                            }
                        }
                       
                    }
                    cmList.Add(cm);
                }
                if (btnSendMail.Text == "Save")
                {
                    // lblException.Text = Convert.ToString(strerr.Append("Start existsList "));
                    int existsList = CustomBLL.Instance.WhetherCustomMaterialListExists(SSNo.Text);//, productTypeId, estimateId);
                    // lblException.Text = Convert.ToString(strerr.Append("End existsList"));
                    if (existsList == 0)
                    {
                        lblException.Text = Convert.ToString(strerr.Append(" existsList = 0"));
                        saveCustom_MaterialList(cmList);
                    }
                    else
                    {
                        // lblException.Text = Convert.ToString(strerr.Append(" existsList != 0"));
                        EnableVendorNameAndAmount();
                        int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(SSNo.Text);//, productTypeId, estimateId);
                        if (permissionStatusCategories == 0)
                        {
                            saveCustom_MaterialList(cmList);

                            if (flag == "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
                                return;
                            }
                        }
                        else
                        {
                            int permissionStatusVendors = CustomBLL.Instance.CheckPermissionsForVendors(SSNo.Text);//, productTypeId, estimateId);
                            if (permissionStatusVendors == 0)
                            {
                                saveCustom_MaterialList(cmList);
                                if (flag == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('After giving permissions lists cann't be changed');", true);
                                return;
                            }
                        }
                    }
                    if (flag == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);

                    }
                }
                else if (btnSendMail.Text == "Send Mail To Vendor Category(s)")
                {

                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForCategories(SSNo.Text);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        bool emailStatusVendorCategory = sendEmailToVendorCategories(cmList);

                        if (emailStatusVendorCategory == true)
                        {
                            bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(SSNo.Text, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
                            UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDORCATEGORIES.ToString());
                            btnSendMail.Text = "Save";
                            setControlsForVendors();
                            grdcustom_material_list.Columns[6].Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Email is sent to all vendor categories');", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Foreman and Sr. Salesman permission');", true);
                    }
                }
                else
                {
                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(SSNo.Text);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        int statusQuotes = CustomBLL.Instance.WhetherVendorQuotesExists(SSNo.Text);
                        if (statusQuotes == 1)
                        {

                            bool emailStatusVendor = sendEmailToVendors(cmList);
                            if (emailStatusVendor == true)
                            {
                                bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(SSNo.Text, JGConstant.EMAIL_STATUS_VENDOR);//, productTypeId, estimateId);
                                UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDOR.ToString());
                                btnSendMail.Text = "Save";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Email is sent to all vendors');", true);
                                setControlsAfterSendingBothMails();

                                DeleteExistingWorkorders();
                                GenerateWorkOrder();

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First attach quotes.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Admin and Sr. Salesman permission');", true);
                    }

                }
            }
            catch (Exception)
            {

            }
        }
        protected void UpdateEmailStatus(string status)
        {
            List<CustomMaterialList> cmList = GetMaterialListFromGrid();
            foreach (CustomMaterialList cm in cmList)
            {
                cm.EmailStatus = status;
            }
            ViewState["CustomMaterialList"] = cmList;
        }

        private void DeleteExistingWorkorders()
        {
            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string soldjobId = Session["jobId"].ToString();
            bool result = CustomBLL.Instance.DeleteWorkorders(soldjobId);
        }

        protected void GenerateWorkOrder()
        {
            string path = Server.MapPath("/CustomerDocs/Pdfs/");

            string originalWorkOrderFilename = "WorkOrder" + ".pdf";
            string soldjobId = Session["jobId"].ToString();
            // DataSet dssoldJobs = new_customerBLL.Instance.GetProductAndEstimateIdOfSoldJob(soldjobId);
            int productId = estimateId;// Convert.ToInt16(dssoldJobs.Tables[0].Rows[0]["EstimateId"].ToString());
            DataSet ds = new_customerBLL.Instance.GetProductAndEstimateIdOfSoldJob(soldjobId);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string tempWorkOrderFilename = "WorkOrder" + DateTime.Now.Ticks + ".pdf";
                        DataRow dr = ds.Tables[0].Rows[i];
                        GeneratePDF(path, tempWorkOrderFilename, false, createWorkOrder("Work Order-" + dr["CustomerId"].ToString(), Convert.ToInt16(dr["CustomerId"].ToString()), Convert.ToInt16(dr["EstimateId"].ToString()), Convert.ToInt16(dr["ProductId"].ToString()), soldjobId));

                        new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(dr["CustomerId"].ToString()), Convert.ToInt16(dr["EstimateId"].ToString()), originalWorkOrderFilename, "WorkOrder", tempWorkOrderFilename, Convert.ToInt16(dr["ProductId"].ToString()), 0);
                        Session["CustomerIdForML"] = Convert.ToInt32(dr["CustomerId"].ToString());
                        string url = ConfigurationManager.AppSettings["URL"].ToString();
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + tempWorkOrderFilename + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                    }
                }
            }
        }

        private string createWorkOrder(string InvoiceNo, int customerId, int estimateId, int productTypeId, string soldJobId)
        {
            return pdf_BLL.Instance.CreateWorkOrder(InvoiceNo, estimateId, productTypeId, customerId, soldJobId, 3);
        }

        private void GeneratePDF(string path, string fileName, bool download, string text)//download set to false in calling method
        {
            var document = new Document();
            FileStream FS = new FileStream(path + fileName, FileMode.Create);
            try
            {
                if (download)
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    PdfWriter.GetInstance(document, Response.OutputStream);
                }
                else
                {
                    PdfWriter.GetInstance(document, FS);
                }
                StringBuilder strB = new StringBuilder();
                strB.Append(text);
                //string filePath = Server.MapPath("/CustomerDocs/Pdfs/wkhtmltopdf.exe");
                //byte[] byteData = ConvertHtmlToByte(strB.ToString(), "", "", filePath);
                //if (byteData != null)
                //{
                //    StreamByteToPDF(byteData, Server.MapPath("/CustomerDocs/Pdfs/") + fileName);
                //}

                using (TextReader sReader = new StringReader(strB.ToString()))
                {
                    document.Open();
                    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                    foreach (IElement elm in list)
                    {
                        document.Add(elm);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "Custom", "");
                //LogManager.Instance.WriteToFlatFile(ex.Message, "Custom",1);// Request.ServerVariables["remote_addr"].ToString());

            }
            finally
            {
                if (document.IsOpen())
                    document.Close();
            }
        }
        //public static void StreamByteToPDF(byte[] byteData, string filePathPhysical)
        //{

        //    if (byteData != null)
        //    {
        //        if (File.Exists(filePathPhysical))
        //        {
        //            File.Delete(filePathPhysical);

        //        }
        //        // string filename = "C:\\Reports\\Newsamplecif.pdf";
        //        FileStream fs = new FileStream(filePathPhysical, FileMode.Create, FileAccess.ReadWrite);
        //        //Read block of bytes from stream into the byte array
        //        fs.Write(byteData, 0, byteData.Length);

        //        //Close the File Stream
        //        fs.Close();
        //    }
        //}
        //public static byte[] ConvertHtmlToByte(string HtmlData, string headerPath, string footerPath, string filePath)
        //{
        //    Process p;
        //    ProcessStartInfo psi = new ProcessStartInfo();

        //    psi.FileName = filePath;
        //    psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);

        //    // run the conversion utility
        //    psi.UseShellExecute = false;
        //    psi.CreateNoWindow = true;
        //    psi.RedirectStandardInput = true;
        //    psi.RedirectStandardOutput = true;
        //    psi.RedirectStandardError = true;
        //    // note: that we tell wkhtmltopdf to be quiet and not run scripts
        //    string args = "-q -n ";
        //    args += "--disable-smart-shrinking ";
        //    args += "--orientation Portrait ";
        //    args += "--outline-depth 0 ";
        //    //args += "--header-spacing 140 ";
        //    //args += "--default-header ";

        //    if (footerPath != string.Empty)
        //    {
        //        args += "--footer-html " + footerPath + " ";

        //    }
        //    if (headerPath != string.Empty)
        //    {
        //        args += "--header-spacing 2 ";
        //        args += "--header-html " + headerPath + " ";

        //    }
        //    //args += "--header-font-size  20 ";
        //    args += "--page-size A4 --encoding windows-1250";
        //    args += " - -";

        //    psi.Arguments = args;

        //    p = Process.Start(psi);

        //    try
        //    {
        //        using (StreamWriter stdin = new StreamWriter(p.StandardInput.BaseStream, Encoding.UTF8))
        //        {
        //            stdin.AutoFlush = true;
        //            stdin.Write(HtmlData);
        //        }

        //        //read output
        //        byte[] buffer = new byte[32768];
        //        byte[] file;
        //        using (var ms = new MemoryStream())
        //        {
        //            while (true)
        //            {
        //                int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);
        //                if (read <= 0)
        //                    break;
        //                ms.Write(buffer, 0, read);
        //            }
        //            file = ms.ToArray();
        //        }

        //        p.StandardOutput.Close();
        //        // wait or exit
        //        p.WaitForExit(60000);

        //        // read the exit code, close process
        //        int returnCode = p.ExitCode;
        //        p.Close();

        //        if (returnCode == 0)
        //            return file;
        //        //else
        //        //    log.Error("Could not create PDF, returnCode:" + returnCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        // log.Error("Could not create PDF", ex);
        //    }
        //    finally
        //    {
        //        p.Close();
        //        p.Dispose();
        //    }
        //    return null;
        //}
        protected void setControlsAfterSendingBothMails()
        {
            btnSendMail.Visible = false;
            grdcustom_material_list.Columns[6].Visible = false;
            txtAdminPasswordNew.Enabled = false;
            //lnkAdminPermission.Enabled = false;
            txtForemanPasswordNew.Enabled = false;
            //lnkForemanPermission.Enabled = false;
            txtSrSalesPassword.Enabled = false;
            //lnkSrSalesmanPermissionA.Enabled = false;
            txtSrSalesManPermition.Enabled = false;
            //lnkSrSalesmanPermissionF.Enabled = false;
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
                txtMateriallist.Enabled = false;
                DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
                ddlVendorCategory.Enabled = false;
                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
                ddlVendorName.Enabled = false;
                TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                txtAmount.Enabled = false;
                LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                lnkQuote.Enabled = true;
            }
        }
        protected void setControlsForVendors()
        {
            DataSet ds1 = CustomBLL.Instance.GetCustom_MaterialList(SSNo.Text);//,productTypeId,estimateId);
            decimal amount = 0;
            int vendorId = 0, i = 0;
            foreach (GridViewRow gr in grdcustom_material_list.Rows)
            {
                TextBox txtMateriallist = (TextBox)gr.FindControl("txtMateriallist");
                txtMateriallist.Enabled = false;
                TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                txtAmount.Enabled = true;
                DropDownList ddlVendorCategory = (DropDownList)gr.FindControl("ddlVendorCategory");
                ddlVendorCategory.Enabled = false;
                int selectedCategoryID = Convert.ToInt16(ddlVendorCategory.SelectedItem.Value);
                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)gr.FindControl("ddlVendorName");
                ddlVendorName.Enabled = true;
                DataSet ds = GetVendorNames(selectedCategoryID);
                ddlVendorName.DataSource = ds;
                ddlVendorName.SelectedIndex = -1;
                ddlVendorName.DataTextField = "VendorName";
                ddlVendorName.DataValueField = "VendorId";
                ddlVendorName.DataBind();
                ddlVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                ddlVendorName.SelectedIndex = 0;
                if (ds1.Tables[0].Rows[i]["Amount"].ToString() != "")
                {
                    amount = Convert.ToDecimal(ds1.Tables[0].Rows[i]["Amount"].ToString());
                    txtAmount.Text = amount.ToString();
                }
                if (ds1.Tables[0].Rows[i]["VendorId"].ToString() != "")
                {
                    ddlVendorName.SelectedIndex = -1;
                    vendorId = Convert.ToInt16(ds1.Tables[0].Rows[i]["VendorId"].ToString());
                    ddlVendorName.SelectedValue = vendorId.ToString();
                }
                i++;
            }
            txtAdminPasswordNew.Visible = true;
            //lnkAdminPermission.Visible = true;
            txtSrSalesPassword.Visible = true;
            //lnkSrSalesmanPermissionA.Visible = true;
            txtForemanPasswordNew.Visible = false;
            //lnkForemanPermission.Visible = false;
            txtSrSalesManPermition.Visible = false;
            //lnkSrSalesmanPermissionF.Visible = false;
        }

        protected void setControlsForVendorsOnPageLoad()
        {
            foreach (GridViewRow gr in grdcustom_material_list.Rows)
            {
                TextBox txtMateriallist = (TextBox)gr.FindControl("txtMateriallist");
                txtMateriallist.Enabled = false;
                TextBox txtAmount = (TextBox)gr.FindControl("txtAmount");
                txtAmount.Enabled = true;
                DropDownList ddlVendorCategory = (DropDownList)gr.FindControl("ddlVendorCategory");
                ddlVendorCategory.Enabled = false;
                int selectedCategoryID = Convert.ToInt16(ddlVendorCategory.SelectedItem.Value);
                Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)gr.FindControl("ddlVendorName");
                ddlVendorName.Enabled = true;
            }
            txtAdminPasswordNew.Visible = true;
            //lnkAdminPermission.Visible = true;
            txtSrSalesPassword.Visible = true;
            //lnkSrSalesmanPermissionA.Visible = true;
            txtForemanPasswordNew.Visible = false;
            //lnkForemanPermission.Visible = false;
            txtSrSalesManPermition.Visible = false;
            //lnkSrSalesmanPermissionF.Visible = false;
            grdcustom_material_list.Columns[6].Visible = false;
        }

        protected void saveCustom_MaterialList(List<CustomMaterialList> cmList)
        {
            try
            {
                bool result = false;
                CustomBLL.Instance.DeleteCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);
                foreach (CustomMaterialList cm in cmList)
                {

                    result = CustomBLL.Instance.AddCustomMaterialList(cm, SSNo.Text);//,productTypeId,estimateId);
                }

                ViewState["CustomMaterialList"] = cmList;
            }
            catch (Exception err)
            {

                lblException.Text = Convert.ToString(lblException.Text+ " " +(err.ToString()));
            }
            
        }

        public DataSet fetchVendorCategoryEmailTemplate()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(0);
            return ds;
        }
        protected bool sendEmailToVendorCategories(List<CustomMaterialList> cmList)
        {
            bool emailStatus = true;
            string mailNotSendIds = string.Empty;
            string htmlBody = string.Empty;
            int emailCounter = 0;
            try
            {
                if (cmList != null && SSNo.Text != "")
                {
                    //loop for each vendor category on procurement page
                    foreach (CustomMaterialList cm in cmList)
                    {
                        //to fetch all vendors within a category
                        DataSet dsVendorsListByCategory = VendorBLL.Instance.fetchVendorListByCategoryForEmail(cm.VendorCategoryId);

                        if (dsVendorsListByCategory != null)
                        {
                            //loop for all vendors within a category
                            for (int counter = 0; counter < dsVendorsListByCategory.Tables[0].Rows.Count; counter++)
                            {
                                DataRow dr = dsVendorsListByCategory.Tables[0].Rows[counter];
                                string mailId = dr["Email"].ToString();
                                string vendorName = dr["VendorName"].ToString();

                                MailMessage m = new MailMessage();
                                SmtpClient sc = new SmtpClient();

                                string userName = ConfigurationManager.AppSettings["VendorCategoryUserName"].ToString();
                                string password = ConfigurationManager.AppSettings["VendorCategoryPassword"].ToString();

                                m.From = new MailAddress(userName, "JMGROVECONSTRUCTION");
                                m.To.Add(new MailAddress(mailId, vendorName));
                                m.Subject = "J.M. Grove " + SSNo.Text + " quote request ";
                                m.IsBodyHtml = true;
                                DataSet dsEmailTemplate = fetchVendorCategoryEmailTemplate();

                                if (dsEmailTemplate != null)
                                {
                                    string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                                    StringBuilder tHeader = new StringBuilder();
                                    tHeader.Append(templateHeader);
                                    var replacedHeader = tHeader//.Replace("imgHeader", "<img src=cid:myImageHeader height=10% width=80%>")
                                                                   .Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                                .Replace("lblJobId", SSNo.Text.ToString())
                                                                .Replace("lblCustomerId", "C" + customerId.ToString());
                                    htmlBody = replacedHeader.ToString();
                                    htmlBody += "</br></br></br>";
                                    string templateBody = dsEmailTemplate.Tables[0].Rows[0][1].ToString();

                                    string materialList = cm.MaterialList;


                                    StringBuilder tbody = new StringBuilder();
                                    tbody.Append(templateBody);

                                    var replacedBody = tbody.Replace("lblMaterialList", materialList);

                                    htmlBody += replacedBody.ToString();

                                    htmlBody += "</br></br></br>";

                                    string templateFooter = dsEmailTemplate.Tables[0].Rows[0][2].ToString();
                                    StringBuilder tFooter = new StringBuilder();
                                    tFooter.Append(templateFooter);
                                    var replacedFooter = tFooter.Replace("src=\"../img/JG-Logo-white.gif\"", "src=cid:myImageLogo")
                                                               .Replace("src=\"../img/Email footer.png\"", "src=cid:myImageFooter");
                                    htmlBody += replacedFooter.ToString();
                                }
                                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");

                                string imageSourceHeader = Server.MapPath(@"~\img") + @"\Email art header.png";
                                LinkedResource theEmailImageHeader = new LinkedResource(imageSourceHeader);
                                theEmailImageHeader.ContentId = "myImageHeader";

                                string imageSourceLogo = Server.MapPath(@"~\img") + @"\JG-Logo-white.gif";
                                LinkedResource theEmailImageLogo = new LinkedResource(imageSourceLogo);
                                theEmailImageLogo.ContentId = "myImageLogo";

                                string imageSourceFooter = Server.MapPath(@"~\img") + @"\Email footer.png";
                                LinkedResource theEmailImageFooter = new LinkedResource(imageSourceFooter);
                                theEmailImageFooter.ContentId = "myImageFooter";

                                //Add the Image to the Alternate view
                                htmlView.LinkedResources.Add(theEmailImageHeader);
                                htmlView.LinkedResources.Add(theEmailImageLogo);
                                htmlView.LinkedResources.Add(theEmailImageFooter);

                                m.AlternateViews.Add(htmlView);
                                m.Body = htmlBody;
                                sc.UseDefaultCredentials = false;
                                sc.Host = "jmgrove.fatcow.com";
                                sc.Port = 25;


                                sc.Credentials = new System.Net.NetworkCredential(userName, password);
                                sc.EnableSsl = false; // runtime encrypt the SMTP communications using SSL
                                try
                                {
                                    sc.Send(m);
                                    emailCounter += 1;
                                }
                                catch (Exception ex)
                                {
                                    mailNotSendIds += mailId + " , ";
                                    CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(SSNo.Text, JGConstant.EMAIL_STATUS_NONE);//, productTypeId, estimateId);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
            if (mailNotSendIds != string.Empty)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Failed to send email to : " + mailNotSendIds + "');", true);
            if (emailCounter == 0)
                emailStatus = false;
            else
                emailStatus = true;

            return emailStatus;
        }

        public DataSet fetchVendorEmailTemplate()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(100);
            return ds;
        }

        protected bool sendEmailToVendors(List<CustomMaterialList> cmList)
        {
            bool emailstatus = true;
            string htmlBody = string.Empty;
            string mailNotSendIds = string.Empty;
            int emailCounter = 0;
            try
            {
                //loop for each vendor
                if (cmList != null && SSNo.Text != "")
                {
                    foreach (CustomMaterialList cm in cmList)
                    {
                        DataSet dsVendorQuoute = VendorBLL.Instance.GetVendorQuoteByVendorId(SSNo.Text, cm.VendorId);
                        string quoteTempName = "", quoteOriginalName = "";
                        if (dsVendorQuoute.Tables[0].Rows.Count > 0)
                        {
                            quoteTempName = dsVendorQuoute.Tables[0].Rows[0]["TempName"].ToString();
                            quoteOriginalName = dsVendorQuoute.Tables[0].Rows[0]["DocName"].ToString();
                        }
                        MailMessage m = new MailMessage();
                        SmtpClient sc = new SmtpClient();

                        string userName = ConfigurationManager.AppSettings["VendorUserName"].ToString();
                        string password = ConfigurationManager.AppSettings["VendorPassword"].ToString();

                        m.From = new MailAddress(userName, "JMGROVECONSTRUCTION");
                        string mailId = cm.VendorEmail;
                        m.To.Add(new MailAddress(mailId, cm.VendorName));
                        m.Subject = "J.M. Grove " + SSNo.Text + " quote acceptance ";
                        m.IsBodyHtml = true;
                        DataSet dsEmailTemplate = fetchVendorEmailTemplate();

                        if (dsEmailTemplate != null)
                        {
                            string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                            StringBuilder tHeader = new StringBuilder();
                            tHeader.Append(templateHeader);

                            var replacedHeader = tHeader//.Replace("imgHeader", "<img src=cid:myImageHeader height=10% width=80%>")
                                                       .Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                       .Replace("lblJobId", SSNo.Text.ToString())
                                                       .Replace("lblCustomerId", "C" + customerId.ToString());
                            htmlBody = replacedHeader.ToString();

                            string templateBody = dsEmailTemplate.Tables[0].Rows[0][1].ToString();

                            StringBuilder tbody = new StringBuilder();
                            tbody.Append(templateBody);

                            var replacedBody = tbody.Replace("lblMaterialList", cm.MaterialList)
                                                    .Replace("lblAmount", cm.Amount.ToString());

                            htmlBody += replacedBody.ToString();

                            string templateFooter = dsEmailTemplate.Tables[0].Rows[0][2].ToString();
                            StringBuilder tFooter = new StringBuilder();
                            tFooter.Append(templateFooter);

                            var replacedFooter = tFooter.Replace("src=\"../img/JG-Logo-white.gif\"", "src=cid:myImageLogo")
                                                               .Replace("src=\"../img/Email footer.png\"", "src=cid:myImageFooter");
                            htmlBody += replacedFooter.ToString();
                        }
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");

                        if (quoteTempName != "")
                        {
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");
                            Attachment attachment = new Attachment(sourceDir + "\\" + quoteTempName);
                            attachment.Name = quoteOriginalName;
                            m.Attachments.Add(attachment);
                        }
                        string imageSourceHeader = Server.MapPath(@"~\img") + @"\Email art header.png";
                        LinkedResource theEmailImageHeader = new LinkedResource(imageSourceHeader);
                        theEmailImageHeader.ContentId = "myImageHeader";

                        string imageSourceLogo = Server.MapPath(@"~\img") + @"\JG-Logo-white.gif";
                        LinkedResource theEmailImageLogo = new LinkedResource(imageSourceLogo);
                        theEmailImageLogo.ContentId = "myImageLogo";

                        string imageSourceFooter = Server.MapPath(@"~\img") + @"\Email footer.png";
                        LinkedResource theEmailImageFooter = new LinkedResource(imageSourceFooter);
                        theEmailImageFooter.ContentId = "myImageFooter";

                        //Add the Image to the Alternate view
                        htmlView.LinkedResources.Add(theEmailImageHeader);
                        htmlView.LinkedResources.Add(theEmailImageLogo);
                        htmlView.LinkedResources.Add(theEmailImageFooter);

                        m.AlternateViews.Add(htmlView);
                        m.Body = htmlBody;

                        sc.UseDefaultCredentials = false;
                        sc.Host = "jmgrove.fatcow.com";
                        sc.Port = 25;
                        sc.Credentials = new System.Net.NetworkCredential(userName, password);
                        sc.EnableSsl = false; // runtime encrypt the SMTP communications using SSL
                        try
                        {
                            sc.Send(m);
                            emailCounter += 1;
                        }
                        catch (Exception ex)
                        {
                            mailNotSendIds += mailId + " , ";
                            CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(SSNo.Text, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (emailCounter == 0)
                emailstatus = false;
            else
                emailstatus = true;

            if (mailNotSendIds != string.Empty)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Failed to send email to : " + mailNotSendIds + "');", true);

            return emailstatus;
        }

        protected void bind()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(0);
            if (ds != null)
            {
                HeaderEditor.Content = ds.Tables[0].Rows[0][0].ToString();
                lblMaterials.Text = ds.Tables[0].Rows[0][1].ToString();
                FooterEditor.Content = ds.Tables[0].Rows[0][2].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditor.Content;
            string Editor_contentFooter = FooterEditor.Content;
            bool result = AdminBLL.Instance.UpdateEmailVendorCategoryTemplate(Editor_contentHeader, Editor_contentFooter, Convert.ToString(Session["VendoeCategoryAttachmentPath"]));
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('EmailVendor Template Updated Successfully');", true);
            }

        }

        protected void bindVendor()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(100);
            if (ds != null)
            {
                HeaderEditorVendor.Content = ds.Tables[0].Rows[0][0].ToString();
                lblMaterialsVendor.Text = ds.Tables[0].Rows[0][1].ToString();
                FooterEditorVendor.Content = ds.Tables[0].Rows[0][2].ToString();
            }
        }
        protected void btnUpdateVendor_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditorVendor.Content;
            string Editor_contentFooter = FooterEditorVendor.Content;
            bool result = AdminBLL.Instance.UpdateEmailVendorTemplate(Editor_contentHeader, Editor_contentFooter, Convert.ToString(Session["VendoeAttachmentPathNew"]));
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('EmailVendor Template Updated Successfully');", true);
            }
        }
        protected void grdcustom_material_list_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string fileName = Convert.ToString(e.CommandArgument);
                if (e.CommandName.Equals("View", StringComparison.InvariantCultureIgnoreCase))
                {
                    string domainName = Request.Url.GetLeftPart(UriPartial.Authority);

                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + domainName + "/CustomerDocs/VendorQuotes/" + fileName + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                }
                else
                {
                    Session["RowIndex"] = fileName; 
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void txtForemanPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(SSNo.Text);//, productTypeId, estimateId);
                if (cResult == 1)
                {
                    if (!string.IsNullOrEmpty(txtForemanPasswordNew.Text))
                    {
                         #region  Password chacked fron tblInstallUsers table
                         // string adminCode = AdminBLL.Instance.GetForemanCode();
                         DataSet dsPassword = AdminBLL.Instance.GetForemanPassword(txtForemanPasswordNew.Text);
                         #endregion

                        if (dsPassword.Tables[0].Rows.Count > 0)
                        {
                            string adminCode = Convert.ToString(dsPassword.Tables[0].Rows[0][1]);
                            int id = Convert.ToInt32(dsPassword.Tables[0].Rows[0][0]);




                            if (adminCode != txtForemanPasswordNew.Text.Trim())
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Invalid Foreman Code');", true);
                                //CVForeman.ErrorMessage = "Invalid Foreman Code";
                                //CVForeman.ForeColor = System.Drawing.Color.Red;
                                //CVForeman.IsValid = false;
                                //CVForeman.Visible = true;
                                //popupForeman_permission.Show();
                                return;
                            }
                            else
                            {
                                StringBuilder s = new StringBuilder();
                                int result = CustomBLL.Instance.UpdateForemanPermissionOfCustomMaterialList(SSNo.Text, JGConstant.PERMISSION_STATUS_GRANTED, Convert.ToString(Session["loginid"]));//, productTypeId, estimateId);
                                if (result == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                                }
                                else
                                {

                                    try
                                    {
                                        txtForemanPasswordNew.Visible = false;
                                        // DataSet ds = CustomBLL.Instance.GetFormanEmail(SSNo.Text);
                                        DataSet ds = CustomBLL.Instance.GetFormanNameAndID(id);
                                        if (ds.Tables.Count > 0)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {/*
                                                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                                                {
                                                   //Original code....
                                                    //lblFormanEmail.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                                    string eml = Convert.ToString(ds.Tables[0].Rows[0][0]);

                                                    #region Hide Email in green After enering password 
                                                    lblFormanEmail.Text = eml;
                                                    lblFormanEmail.ForeColor = System.Drawing.Color.Green;


                                                   // lnkEmployeeId.Text=
                                                   // lnkEmployeeName.Text=
                                                    #endregion



                                                }*/
                                                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                                                {
                                                    //lblFormanEmail.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
                                                    lblForemanName.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
                                                    lnkForemanId.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);

                                                    #region For entering ProductId  CustomerId into tblInstallUsertable
                                                   // string qAdd="Insert into"
                                                    AddForemanName a = new AddForemanName();
                                                    a.ProductId = estimateId;
                                                    a.CustomerId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
                                                    a.ID = Convert.ToInt32(lnkForemanId.Text);
                                                    a.SoldJob = Convert.ToString(Request.QueryString[QueryStringKey.Key.SoldJobId.ToString()]);
                                                    //For checking duplicate event....
                                                    DataSet dsAddFore = new DataSet();
                                                    new_customerBLL.Instance.AddForeManName(a);
                                                    #endregion
                                                }
                                            }
                                            //txtForemanPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
                                            //lnkForemanPermission.Enabled = false;
                                            //lnkForemanPermission.ForeColor = System.Drawing.Color.DarkGray;
                                            //popupForeman_permission.TargetControlID = "hdnForeman";
                                            SetButtonText();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        lblException.Text = ex.Message;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Invalid Foreman Code');", true);                       
                            return;
                        }
                    }
                    else
                    {
                        //CVForeman.ErrorMessage = "Please Enter Foreman Code";
                        //CVForeman.ForeColor = System.Drawing.Color.Red;
                        //CVForeman.IsValid = false;
                        //CVForeman.Visible = true;
                        //popupForeman_permission.Show();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                }
                txtSrSalesPassword.Text = "";
                txtSrSalesManPermition.Text = "";
                txtAdminPasswordNew.Text = "";
                txtForemanPasswordNew.Text = "";

            }
            catch (Exception)
            {

              //  throw;
            }
        }

        protected void txtAdminPassword_TextChanged(object sender, EventArgs e)
        {
            int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(SSNo.Text);//,productTypeId,estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtAdminPasswordNew.Text))
                {
                    string adminCode = AdminBLL.Instance.GetAdminCode();
                    if (adminCode != txtAdminPasswordNew.Text.Trim())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Invalid Admin Code');", true);
                        //CVAdmin.ErrorMessage = "Invalid Admin Code";
                        //CVAdmin.ForeColor = System.Drawing.Color.Red;
                        //CVAdmin.IsValid = false;
                        //CVAdmin.Visible = true;
                        //popupAdmin_permission.Show();
                        return;
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateAdminPermissionOfCustomMaterialList(SSNo.Text.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, Convert.ToString(Session["loginid"]));//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            txtAdminPasswordNew.Visible = false;
                            DataSet ds = CustomBLL.Instance.GetAdminEmail(SSNo.Text);
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                                    {
                                        lblAdmin.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                    }
                                }
                                //txtForemanPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
                                //lnkForemanPermission.Enabled = false;
                                //lnkForemanPermission.ForeColor = System.Drawing.Color.DarkGray;
                                //popupForeman_permission.TargetControlID = "hdnForeman";
                            }
                            //lnkAdminPermission.Enabled = false;
                            //txtAdminPasswordNew.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkAdminPermission.ForeColor = System.Drawing.Color.DarkGray;
                            //popupAdmin_permission.TargetControlID = "hdnAdmin";
                            SetButtonText();
                            DisableVendorNameAndAmount();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                    //CVAdmin.ErrorMessage = "Please Enter Admin Code";
                    //CVAdmin.ForeColor = System.Drawing.Color.Red;
                    //CVAdmin.IsValid = false;
                    //CVAdmin.Visible = true;
                    //popupAdmin_permission.Show();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List and enter all vendor names');", true);
            }
            txtSrSalesPassword.Text = "";
            txtSrSalesManPermition.Text = "";
            txtAdminPasswordNew.Text = "";
            txtForemanPasswordNew.Text = "";
        }

        protected void txtSrSalesPassword_TextChanged(object sender, EventArgs e)
        {
            int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(SSNo.Text);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtSrSalesPassword.Text))
                {
                    string salesmanCode = Session["loginpassword"].ToString();
                    if (salesmanCode != txtSrSalesPassword.Text.Trim())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Invalid Sr. Salesman Code');", true);
                        //CVSrSalesmanA.ErrorMessage = "Invalid Sr. Salesman Code";
                        //CVSrSalesmanA.ForeColor = System.Drawing.Color.Red;
                        //CVSrSalesmanA.IsValid = false;
                        //CVSrSalesmanA.Visible = true;
                        //popupSrSalesmanPermissionA.Show();
                        return;
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialList(SSNo.Text.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, Convert.ToString(Session["loginid"]));//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            txtSrSalesPassword.Enabled = false;
                            DataSet ds = CustomBLL.Instance.GetSrSalesFEmail(SSNo.Text);
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                                    {
                                        lblSrSales.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                    }
                                }
                            }
                            //txtSrSalesPassword.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkSrSalesmanPermissionA.Enabled = false;
                            //lnkSrSalesmanPermissionA.ForeColor = System.Drawing.Color.DarkGray;
                            //popupSrSalesmanPermissionA.TargetControlID = "hdnSrA";
                            SetButtonText();
                            DisableVendorNameAndAmount();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Sr. Salesman Code');", true);
                    //CVSrSalesmanA.ErrorMessage = "Please Enter Sr. Salesman Code";
                    //CVSrSalesmanA.ForeColor = System.Drawing.Color.Red;
                    //CVSrSalesmanA.IsValid = false;
                    //CVSrSalesmanA.Visible = true;
                    //popupSrSalesmanPermissionA.Show();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List and enter all vendor names');", true);
            }
            txtSrSalesPassword.Text = "";
            txtSrSalesManPermition.Text = "";
            txtAdminPasswordNew.Text = "";
            txtForemanPasswordNew.Text = "";
        }

        protected void txtSrSalesManPermition_TextChanged(object sender, EventArgs e)
        {
            int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(SSNo.Text);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtSrSalesManPermition.Text))
                {
                     string salesmanCode="";
                    if (Session["loginpassword"] != null)
                    {
                         salesmanCode = Convert.ToString(Session["loginpassword"]);
                    }
                    if (salesmanCode != txtSrSalesManPermition.Text.Trim())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Invalid Sr. Salesman Code');", true);
                        //CVSrSalesmanF.ErrorMessage = "Invalid Sr. Salesman Code";
                        //CVSrSalesmanF.ForeColor = System.Drawing.Color.Red;
                        //CVSrSalesmanF.IsValid = false;
                        //CVSrSalesmanF.Visible = true;
                        //popupSrSalesmanPermissionF.Show();
                        return;
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialListF(SSNo.Text.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, Convert.ToString(Session["loginid"]));//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            txtSrSalesManPermition.Visible = false;
                            DataSet ds = CustomBLL.Instance.GetSrSalesAEmail(SSNo.Text);
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                                    {
                                        lblSrSalesManPermition.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                        lblSrSalesManPermition.Visible = true;
                                    }
                                }
                            }
                            //txtSrSalesManPermition.ForeColor = System.Drawing.Color.DarkGray;
                            //lnkSrSalesmanPermissionF.Enabled = false;
                            //lnkSrSalesmanPermissionF.ForeColor = System.Drawing.Color.DarkGray;
                            //popupSrSalesmanPermissionF.TargetControlID = "hdnSrF";
                            SetButtonText();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Sr. Salesman Code');", true);
                    //CVSrSalesmanF.ErrorMessage = "Please Enter Sr. Salesman Code";
                    //CVSrSalesmanF.ForeColor = System.Drawing.Color.Red;
                    //CVSrSalesmanF.IsValid = false;
                    //CVSrSalesmanF.Visible = true;
                    //popupSrSalesmanPermissionF.Show();
                    return;

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
            }
            txtSrSalesPassword.Text = "";
            txtSrSalesManPermition.Text = "";
            
        }


        protected void lnkaddvendorquotes_Click(object sender, EventArgs e)
        {
            LinkButton lnkquotes = sender as LinkButton;

            GridViewRow gr = (GridViewRow)lnkquotes.Parent.Parent;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "overlay", "overlay()", true);
                //Response.Redirect("~/Sr_App/AttachQuotes.aspx");
                // Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(lblProductType.Text));
            //}
            //else if (emailStatus == JGConstant.EMAIL_STATUS_VENDOR)
            //{
            //    //ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(lblProductType.Text);
            //    Response.Redirect("~/Sr_App/AttachQuotes.aspx?EmailStatus=" + emailStatus);
            //    // Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(lblProductType.Text) + "&EmailStatus=" + emailStatus);
            //}
            ////else if (lblProductType.Text == JGConstant.PRODUCT_SHUTTER)
            ////{
            ////    ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = (int)JGConstant.ProductType.shutter;

            ////    Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + (int)JGConstant.ProductType.shutter);
            ////}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First send email to all vendor categories');", true);
            //}

        }


        protected void BindFiles(List<AttachedQuotes> fileList = null)
        {
            if (fileList == null)
            {
                fileList = GetFilesFromViewState();
            }
            grdAttachQuotes.DataSource = null;
            grdAttachQuotes.DataSource = fileList.Where(c => c.action != "Delete").ToList();
            grdAttachQuotes.DataBind();

        }
        protected void btnCancelQuotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procurement.aspx");
        }
        private List<AttachedQuotes> GetFilesFromViewState()
        {
            List<AttachedQuotes> fileList = null;

            if (ViewState["FileList"] == null)
            {
                fileList = new List<AttachedQuotes>();
            }
            else
            {
                fileList = ViewState["FileList"] as List<AttachedQuotes>;
            }
            return fileList;
        }

        protected void UpdateFileList(AttachedQuotes file, int rowIndex = 0)
        {
            List<AttachedQuotes> fileList = GetFilesFromViewState();

            switch (file.action)
            {
                case "Add":
                    fileList.Add(file);
                    break;
                case "Delete":
                    fileList.Remove(file);
                    //file.id
                    break;
                case "Update":
                    fileList[rowIndex] = file;
                    break;
                default:
                    break;
            }

            ViewState["FileList"] = fileList;
            Session["FileList"] = fileList.ToList();
            BindFiles(fileList);

        }

        protected void grdAttachQuotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string rowIndex = Convert.ToString(e.CommandArgument);
                var fileList = ViewState["FileList"] as List<AttachedQuotes>;
                string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");
                if (e.CommandName.Equals("Delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    var file = fileList.Where(a => a.TempName == rowIndex).FirstOrDefault();

                    if (!File.Exists(sourceDir + file.TempName))
                    {
                        File.Delete(sourceDir + file.TempName);
                    }

                    file.action = "Delete";
                    VendorBLL.Instance.RemoveAttachedQuote(file.TempName);
                    UpdateFileList(file);
                }
                if (e.CommandName.Equals("Select", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedRowIndex = rowIndex;

                    var file = fileList.Where(a => a.TempName == rowIndex).FirstOrDefault();
                    selectedVendorID = file.VendorId;

                    if (file.VendorCategoryNm != "")
                    {
                        drpVendorCategory.SelectedValue = file.VendorCategoryId.ToString();
                    }
                    else
                    {
                        drpVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    }
                    if (file.VendorName != "")
                    {
                        drpVendorName.SelectedItem.Value = file.VendorName;
                        drpVendorName.SelectedItem.Text = file.VendorName;
                    }
                    else
                    {
                        drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    }
                    if (file.DocName.Contains(".txt"))
                    {
                        txtFileName.Text = file.DocName;
                        string fileContent = File.ReadAllText(sourceDir + file.TempName);
                        txtFileContent.Text = fileContent;
                        txtFileUpload.Text = "";
                    }
                    else
                    {
                        txtFileUpload.Text = file.DocName;
                        txtFileName.Text = "";
                        txtFileContent.Text = "";
                    }
                    disableDropdowns();
                }
                if (e.CommandName.Equals("View", StringComparison.InvariantCultureIgnoreCase))
                {
                    string domainName = Request.Url.GetLeftPart(UriPartial.Authority);

                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + domainName + "/CustomerDocs/VendorQuotes/" + rowIndex + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void grdAttachQuotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnSaveQuotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex == "-1")
                {
                    bool res = false;
                    bool fileUploaded = false, fileCreated = false;

                    if (drpVendorCategory.SelectedValue != "Select" && drpVendorName.SelectedValue != "Select")
                    {
                        int vendorId = Convert.ToInt16(drpVendorName.SelectedValue); // need check of blank
                        List<AttachedQuotes> fileList = GetFilesFromViewState();
                        foreach (var item in fileList)
                        {
                            if (vendorId == item.VendorId)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have alreay attached a quote with this vendor.');", true);
                                return;
                            }
                        }
                        if (uploadvendorquotes.HasFile)
                            fileUploaded = true;
                        if (txtFileName.Text != "" && txtFileContent.Text != "")
                            fileCreated = true;

                        if (fileCreated == true && fileUploaded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please either upload file or write file. Both operations in one go is not possible.');", true);
                            return;
                        }
                        else if (fileUploaded == true)
                        {
                            HttpPostedFile uploadfile = uploadvendorquotes.PostedFile;

                            string tempFileName = DateTime.Now.Ticks + Path.GetFileName(uploadfile.FileName);
                            string originalFileName = Path.GetFileName(uploadfile.FileName);

                            if (uploadfile.ContentLength > 0)
                            {
                                uploadfile.SaveAs(Server.MapPath("~/CustomerDocs/VendorQuotes/") + tempFileName);
                            }

                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                            AttachedQuotes aq = new AttachedQuotes();
                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Add";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
                            aqList.Add(aq);
                            UpdateFileList(aq);

                            res = VendorBLL.Instance.AddVendorQuotes(SoldJobID, originalFileName, tempFileName, vendorId);

                            txtFileUpload.Text = "";
                            drpVendorCategory.SelectedIndex = -1;
                            drpVendorName.Items.Clear();
                            drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        else if (fileCreated == true)
                        {
                            AttachedQuotes aq = new AttachedQuotes();
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                            string filename = "";
                            if (!txtFileName.Text.Contains(".txt"))
                                filename = txtFileName.Text + ".txt";
                            else
                                filename = txtFileName.Text;

                            string tempFileName = DateTime.Now.Ticks + filename;
                            string originalFileName = filename;

                            File.WriteAllText(sourceDir + tempFileName, txtFileContent.Text);
                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Add";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);

                            aqList.Add(aq);

                            UpdateFileList(aq);

                            ResetControls();

                            res = VendorBLL.Instance.AddVendorQuotes(SoldJobID, originalFileName, tempFileName, vendorId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please upload document or specify file name and content.');", true);
                        }

                        if (res)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote attached successfully');ClosePopup();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Vendor Category and Vendor Name');", true);
                    }
                }
                else //if record gets updated
                {
                    bool res = false;
                    int vendorId = selectedVendorID;

                    if (drpVendorCategory.SelectedValue != "Select" && drpVendorName.SelectedValue != "Select")
                    {
                        List<AttachedQuotes> fileListViewState = GetFilesFromViewState();
                        int rowIndex = 0;
                        foreach (var item in fileListViewState)
                        {
                            if (selectedRowIndex == item.TempName)
                            {
                                break;
                            }
                            rowIndex++;
                        }

                        var fileList = ViewState["FileList"] as List<AttachedQuotes>;
                        var selectedRecord = fileList.Where(a => a.TempName == selectedRowIndex).FirstOrDefault();
                        if (vendorId != selectedRecord.VendorId)
                        {
                            List<AttachedQuotes> file = fileList.Where(a => a.TempName != selectedRowIndex).ToList();

                            foreach (var item in file)
                            {
                                if (vendorId == item.VendorId)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have alreay attached a quote with this vendor.');", true);
                                    return;
                                }
                            }
                        }
                        string uploadedFilename = "", createdFilename = "";
                        bool fileUploaded = false, fileCreated = false;
                        if (uploadvendorquotes.HasFile)
                        {
                            uploadedFilename = txtFileUpload.Text;
                            fileUploaded = true;
                        }
                        if (txtFileName.Text != "" && txtFileContent.Text != "")
                        {
                            createdFilename = txtFileName.Text;
                            fileCreated = true;
                        }

                        if (fileCreated == true && fileUploaded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please either upload file or write file. Both operations in one go is not possible.');", true);
                        }
                        else if (fileUploaded == true)
                        {
                            if (uploadvendorquotes.HasFile)
                            {

                                HttpPostedFile uploadfile = uploadvendorquotes.PostedFile;

                                string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                                if (File.Exists(sourceDir + selectedRecord.TempName))   // to delete previous file
                                {
                                    File.Delete(sourceDir + selectedRecord.TempName);
                                }

                                string tempFileName = DateTime.Now.Ticks + Path.GetFileName(uploadfile.FileName);
                                string originalFileName = Path.GetFileName(uploadfile.FileName);

                                if (uploadfile.ContentLength > 0)
                                {
                                    uploadfile.SaveAs(Server.MapPath("~/CustomerDocs/VendorQuotes/") + tempFileName);
                                }

                                DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                                AttachedQuotes aq = new AttachedQuotes();
                                aq.TempName = tempFileName;
                                aq.DocName = originalFileName;
                                aq.action = "Update";
                                aq.VendorId = vendorId;
                                aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                                aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                                aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
                                aqList.Add(aq);
                                UpdateFileList(aq, rowIndex);

                                res = VendorBLL.Instance.UpdateAttachedQuote(SoldJobID, originalFileName, tempFileName, vendorId);

                                txtFileUpload.Text = "";
                                drpVendorCategory.SelectedIndex = -1;
                                drpVendorName.Items.Clear();
                                drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                            }

                            selectedRowIndex = "-1";   //means now no row is selected
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote updated successfully');", true);
                        }
                        else if (fileCreated == true)
                        {
                            AttachedQuotes aq = new AttachedQuotes();
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                            string filename = "";
                            if (!txtFileName.Text.Contains(".txt"))
                                filename = txtFileName.Text + ".txt";
                            else
                                filename = txtFileName.Text;

                            string tempFileName = DateTime.Now.Ticks + filename;
                            string originalFileName = filename;

                            File.WriteAllText(sourceDir + tempFileName, txtFileContent.Text);
                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);
                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Update";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);

                            aqList.Add(aq);
                            UpdateFileList(aq, rowIndex);

                            if (File.Exists(sourceDir + selectedRecord.TempName))   // to delete previous file
                            {
                                File.Delete(sourceDir + selectedRecord.TempName);
                            }

                            res = VendorBLL.Instance.UpdateAttachedQuote(SoldJobID, originalFileName, tempFileName, vendorId);

                            ResetControls();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote updated successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please upload document or specify file name and content.');", true);
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Vendor Category and Vendor Name');", true);
                    }
                    enableDropdowns();


                }

            }
            catch (Exception ex)
            {
            }
        }

        private void disableDropdowns()
        {
            drpVendorCategory.Enabled = false;
            drpVendorName.Enabled = false;
        }

        private void enableDropdowns()
        {
            drpVendorCategory.Enabled = true;
            drpVendorName.Enabled = true;
        }

        protected void ResetControls()
        {
            txtFileName.Text = "";
            txtFileContent.Text = "";
            txtFileUpload.Text = "";
            drpVendorCategory.SelectedIndex = -1;
            drpVendorName.Items.Clear();
            drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            selectedRowIndex = "-1";
            enableDropdowns();
        }

        protected void drpVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpVendorCategory.SelectedValue != "Select")
            {
                DataSet ds = VendorBLL.Instance.fetchVendorNamesByVendorCategory(Convert.ToInt16(drpVendorCategory.SelectedValue));
                drpVendorName.DataSource = ds;
                drpVendorName.DataTextField = "VendorName";
                drpVendorName.DataValueField = "VendorId";
                drpVendorName.DataBind();
            }
            else
            {
                drpVendorName.Items.Clear();
                drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }

        }

        protected void btnResetQuotes_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void grdAttachQuotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this document?');");
            }
        }

        protected void btnAttach_Click(object sender, EventArgs e)
        {
            if (flpEmailAttachment.HasFile)
            {
                string filename = Path.GetFileName(flpEmailAttachment.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpEmailAttachment.SaveAs(Server.MapPath("~/Sr_App/MailDocument/" + filename));
                Session["VendoeCategoryAttachmentPath"] = "~/Sr_App/MailDocument/" + filename;
            }
        }

        protected void btnUploadVendorAttach_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(flpVendorEmailAttach.PostedFile.FileName);
            filename = DateTime.Now.ToString() + filename;
            filename = filename.Replace("/", "");
            filename = filename.Replace(":", "");
            filename = filename.Replace(" ", "");
            flpVendorEmailAttach.SaveAs(Server.MapPath("~/Sr_App/MailDocument/" + filename));
            Session["VendoeAttachmentPathNew"] = "";
            Session["VendoeAttachmentPathNew"] = "~/Sr_App/MailDocument/" + filename;
        }

        protected void CusId_Click(object sender, EventArgs e)
        {
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Yet to develop.');", true);
            Response.Redirect("~/Sr_App/Customer_Profile.aspx?CustomerId=" + CusId.Text);
        }

        protected void SSNo_Click(object sender, EventArgs e)
        {
           
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Yet to develop.');", true);
            Response.Redirect("Custom.aspx?ProductTypeId=" + Convert.ToInt16(productTypeId) + "&ProductId=" + estimateId + "&CustomerId=" + CusId.Text);
        }

        protected void CusName_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Yet to develop.');", true);
        }

        protected void txtMaterialCost_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
            TextBox txt = (TextBox)currentRow.FindControl("txtQTY");
            TextBox txtCost = (TextBox)currentRow.FindControl("txtMaterialCost");
            Label lblCost = (Label)currentRow.FindControl("lblCost");
            int a = 0;
                if(txt.Text=="")
                {
                    txt.Text =Convert.ToString(a);
                }
                if (txtCost.Text == "")
                {
                    txtCost.Text = Convert.ToString(a);
                }
                lblCost.Text = Convert.ToString(Convert.ToDecimal(txt.Text) * Convert.ToDecimal(txtCost.Text));
        
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void ddlExtent_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent.Parent.Parent;
            DropDownList ddl = (DropDownList)currentRow.FindControl("ddlExtent");
            LinkButton lblTotal = (LinkButton)currentRow.FindControl("lblTotal");
            Label lblCost = (Label)currentRow.FindControl("lblCost");
            if (ddl.SelectedValue == "Jobsitedelivery" || ddl.SelectedValue == "OfficeDelivery")
            {
                lblTotal.Text = Convert.ToString(Convert.ToDecimal(lblCost.Text) + 20);
            }
            else
            {
                lblTotal.Text = Convert.ToString(lblCost.Text);
            }
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void lnkAttachQuotes_Click(object sender, EventArgs e)
        {
            #region Originonal Code...
            /*
            string Qt=Convert.ToString(Request.QueryString[QueryStringKey.Key.SoldJobId.ToString()]);
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(Qt);//, Convert.ToInt16(lblProductType.Text),  Convert.ToInt16(hdnproductid.Value));
            if (emailStatus == JGConstant.EMAIL_STATUS_VENDORCATEGORIES)
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(productTypeId);
                Response.Redirect("~/Sr_App/AttachQuotes.aspx");
                // Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(lblProductType.Text));
            }
            else if (emailStatus == JGConstant.EMAIL_STATUS_VENDOR)
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(productTypeId);
                Response.Redirect("~/Sr_App/AttachQuotes.aspx?EmailStatus=" + emailStatus);
                // Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(lblProductType.Text) + "&EmailStatus=" + emailStatus);
            }
            //else if (lblProductType.Text == JGConstant.PRODUCT_SHUTTER)
            //{
            //    ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = (int)JGConstant.ProductType.shutter;

            //    Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + (int)JGConstant.ProductType.shutter);
            //}
            else
            {
                Response.Redirect("~/Sr_App/AttachQuotes.aspx");
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First send email to all vendor categories');", true);
            }

            */
        #endregion

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
            return;
        }
        protected void btnCancelQuotes1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procurement.aspx");
        }
        protected void btnResetQuotes1_Click(object sender, EventArgs e)
        {
            ResetControl();
        }
        protected void ResetControl()
        {
            txtFileName.Text = "";
            txtFileContent.Text = "";
            txtFileUpload.Text = "";
            drpVendorCategory.SelectedIndex = -1;
            drpVendorName.Items.Clear();
            drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            selectedRowIndex = "-1";
            enableDropdowns();
        }
        protected void btnSaveQuotes1_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex == "-1")
                {
                    bool res = false;
                    bool fileUploaded = false, fileCreated = false;

                    if (drpVendorCategory1.SelectedValue != "Select" && drpVendorName1.SelectedValue != "Select")
                    {
                        int vendorId = Convert.ToInt16(drpVendorName1.SelectedValue); // need check of blank
                        List<AttachedQuotes> fileList = GetFilesFromViewState();
                        foreach (var item in fileList)
                        {
                            if (vendorId == item.VendorId)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have alreay attached a quote with this vendor.');", true);
                                return;
                            }
                        }
                        if (uploadvendorquotes.HasFile)
                            fileUploaded = true;
                        if (txtFileName.Text != "" && txtFileContent.Text != "")
                            fileCreated = true;

                        if (fileCreated == true && fileUploaded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please either upload file or write file. Both operations in one go is not possible.');", true);
                            return;
                        }
                        else if (fileUploaded == true)
                        {
                            HttpPostedFile uploadfile = uploadvendorquotes.PostedFile;

                            string tempFileName = DateTime.Now.Ticks + Path.GetFileName(uploadfile.FileName);
                            string originalFileName = Path.GetFileName(uploadfile.FileName);

                            if (uploadfile.ContentLength > 0)
                            {
                                uploadfile.SaveAs(Server.MapPath("~/CustomerDocs/VendorQuotes/") + tempFileName);
                            }

                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                            AttachedQuotes aq = new AttachedQuotes();
                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Add";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
                            aqList.Add(aq);
                            UpdateFileList(aq);

                            res = VendorBLL.Instance.AddVendorQuotes(SoldJobID, originalFileName, tempFileName, vendorId);

                            txtFileUpload.Text = "";
                            drpVendorCategory1.SelectedIndex = -1;
                            drpVendorName1.Items.Clear();
                            drpVendorName1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                        }
                        else if (fileCreated == true)
                        {
                            AttachedQuotes aq = new AttachedQuotes();
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                            string filename = "";
                            if (!txtFileName.Text.Contains(".txt"))
                                filename = txtFileName.Text + ".txt";
                            else
                                filename = txtFileName.Text;

                            string tempFileName = DateTime.Now.Ticks + filename;
                            string originalFileName = filename;

                            File.WriteAllText(sourceDir + tempFileName, txtFileContent.Text);
                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Add";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);

                            aqList.Add(aq);

                            UpdateFileList(aq);

                            ResetControls();

                            res = VendorBLL.Instance.AddVendorQuotes(SoldJobID, originalFileName, tempFileName, vendorId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please upload document or specify file name and content.');", true);
                        }

                        if (res)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote attached successfully');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Vendor Category and Vendor Name');", true);
                    }
                }
                else //if record gets updated
                {
                    bool res = false;
                    int vendorId = selectedVendorID;

                    if (drpVendorCategory1.SelectedValue != "Select" && drpVendorName1.SelectedValue != "Select")
                    {
                        List<AttachedQuotes> fileListViewState = GetFilesFromViewState();
                        int rowIndex = 0;
                        foreach (var item in fileListViewState)
                        {
                            if (selectedRowIndex == item.TempName)
                            {
                                break;
                            }
                            rowIndex++;
                        }

                        var fileList = ViewState["FileList"] as List<AttachedQuotes>;
                        var selectedRecord = fileList.Where(a => a.TempName == selectedRowIndex).FirstOrDefault();
                        if (vendorId != selectedRecord.VendorId)
                        {
                            List<AttachedQuotes> file = fileList.Where(a => a.TempName != selectedRowIndex).ToList();

                            foreach (var item in file)
                            {
                                if (vendorId == item.VendorId)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have alreay attached a quote with this vendor.');", true);
                                    return;
                                }
                            }
                        }
                        string uploadedFilename = "", createdFilename = "";
                        bool fileUploaded = false, fileCreated = false;
                        if (uploadvendorquotes.HasFile)
                        {
                            uploadedFilename = txtFileUpload.Text;
                            fileUploaded = true;
                        }
                        if (txtFileName.Text != "" && txtFileContent.Text != "")
                        {
                            createdFilename = txtFileName.Text;
                            fileCreated = true;
                        }

                        if (fileCreated == true && fileUploaded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please either upload file or write file. Both operations in one go is not possible.');", true);
                        }
                        else if (fileUploaded == true)
                        {
                            if (uploadvendorquotes.HasFile)
                            {

                                HttpPostedFile uploadfile = uploadvendorquotes.PostedFile;

                                string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                                if (File.Exists(sourceDir + selectedRecord.TempName))   // to delete previous file
                                {
                                    File.Delete(sourceDir + selectedRecord.TempName);
                                }

                                string tempFileName = DateTime.Now.Ticks + Path.GetFileName(uploadfile.FileName);
                                string originalFileName = Path.GetFileName(uploadfile.FileName);

                                if (uploadfile.ContentLength > 0)
                                {
                                    uploadfile.SaveAs(Server.MapPath("~/CustomerDocs/VendorQuotes/") + tempFileName);
                                }

                                DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                                AttachedQuotes aq = new AttachedQuotes();
                                aq.TempName = tempFileName;
                                aq.DocName = originalFileName;
                                aq.action = "Update";
                                aq.VendorId = vendorId;
                                aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                                aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                                aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
                                aqList.Add(aq);
                                UpdateFileList(aq, rowIndex);

                                res = VendorBLL.Instance.UpdateAttachedQuote(SoldJobID, originalFileName, tempFileName, vendorId);

                                txtFileUpload.Text = "";
                                drpVendorCategory1.SelectedIndex = -1;
                                drpVendorName1.Items.Clear();
                                drpVendorName1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                            }

                            selectedRowIndex = "-1";   //means now no row is selected
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote updated successfully');", true);
                        }
                        else if (fileCreated == true)
                        {
                            AttachedQuotes aq = new AttachedQuotes();
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                            string filename = "";
                            if (!txtFileName.Text.Contains(".txt"))
                                filename = txtFileName.Text + ".txt";
                            else
                                filename = txtFileName.Text;

                            string tempFileName = DateTime.Now.Ticks + filename;
                            string originalFileName = filename;

                            File.WriteAllText(sourceDir + tempFileName, txtFileContent.Text);
                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);
                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Update";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);

                            aqList.Add(aq);
                            UpdateFileList(aq, rowIndex);

                            if (File.Exists(sourceDir + selectedRecord.TempName))   // to delete previous file
                            {
                                File.Delete(sourceDir + selectedRecord.TempName);
                            }

                            res = VendorBLL.Instance.UpdateAttachedQuote(SoldJobID, originalFileName, tempFileName, vendorId);

                            ResetControls();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote updated successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please upload document or specify file name and content.');", true);
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Vendor Category and Vendor Name');", true);
                    }
                    enableDropdown();


                }

            }
            catch (Exception ex)
            {
            }
        }

        private void enableDropdown()
        {
            drpVendorCategory1.Enabled = true;
            drpVendorName1.Enabled = true;
        }
        protected void drpVendorCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpVendorCategory1.SelectedValue != "Select")
            {
                DataSet ds = VendorBLL.Instance.fetchVendorNamesByVendorCategory(Convert.ToInt16(drpVendorCategory1.SelectedValue));
                drpVendorName1.DataSource = ds;
                drpVendorName1.DataTextField = "VendorName";
                drpVendorName1.DataValueField = "VendorId";
                drpVendorName1.DataBind();
            }
            else
            {
                drpVendorName1.Items.Clear();
                drpVendorName1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void txtLine_TextChanged(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void txtSkuPartNo_TextChanged(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void txtQTY_TextChanged(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void txtUOM_TextChanged(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void ddlVendorName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            flag = "Autosave";
            SaveMaterialList();
        }

        protected void gvAttachQuotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string rowIndex = Convert.ToString(e.CommandArgument);
                var fileList = ViewState["FileList"] as List<AttachedQuotes>;
                string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");
                if (e.CommandName.Equals("Delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    var file = fileList.Where(a => a.TempName == rowIndex).FirstOrDefault();

                    if (!File.Exists(sourceDir + file.TempName))
                    {
                        File.Delete(sourceDir + file.TempName);
                    }

                    file.action = "Delete";
                    VendorBLL.Instance.RemoveAttachedQuote(file.TempName);
                    UpdateFileList(file);
                }
                if (e.CommandName.Equals("Select", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedRowIndex = rowIndex;

                    var file = fileList.Where(a => a.TempName == rowIndex).FirstOrDefault();
                    selectedVendorID = file.VendorId;

                    if (file.VendorCategoryNm != "")
                    {
                        drpVendorCategory.SelectedValue = file.VendorCategoryId.ToString();
                    }
                    else
                    {
                        drpVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    }
                    if (file.VendorName != "")
                    {
                        drpVendorName.SelectedItem.Value = file.VendorName;
                        drpVendorName.SelectedItem.Text = file.VendorName;
                    }
                    else
                    {
                        drpVendorName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    }
                    if (file.DocName.Contains(".txt"))
                    {
                        txtFileName.Text = file.DocName;
                        string fileContent = File.ReadAllText(sourceDir + file.TempName);
                        txtFileContent.Text = fileContent;
                        txtFileUpload.Text = "";
                    }
                    else
                    {
                        txtFileUpload.Text = file.DocName;
                        txtFileName.Text = "";
                        txtFileContent.Text = "";
                    }
                    disableDropdowns();
                }
                if (e.CommandName.Equals("View", StringComparison.InvariantCultureIgnoreCase))
                {
                    string domainName = Request.Url.GetLeftPart(UriPartial.Authority);

                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + domainName + "/CustomerDocs/VendorQuotes/" + rowIndex + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvAttachQuotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvAttachQuotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this document?');");
            }
        }

        protected void lnkForemanId_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sr_App/InstallCreateUser.aspx?id=" + lnkForemanId.Text);
        }
    }
}