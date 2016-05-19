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
        #region "Page Vairable"
        //#- Protected Vairables
        protected string ForemanPwdVisibility
        {
            get { return ViewState["ForemanPwdVisibility"] != null ? Convert.ToString(ViewState["ForemanPwdVisibility"]) : ""; }
            set { ViewState["ForemanPwdVisibility"] = value; }
        }
        protected string ForemanMessage = "";
        protected string SalesmanPwdVisibility
        {
            get { return ViewState["SalesmanPwdVisibility"] != null ? Convert.ToString(ViewState["SalesmanPwdVisibility"]) : ""; }
            set { ViewState["SalesmanPwdVisibility"] = value; }
        }
        protected string SalesmanMessage = "";
        protected string AdminPwdVisibility
        {
            get { return ViewState["AdminPwdVisibility"] != null ? Convert.ToString(ViewState["AdminPwdVisibility"]) : ""; }
            set { ViewState["AdminPwdVisibility"] = value; }
        }
        protected string AdminMessage = "";
        protected string SrSalesmanPwdVisibility
        {
            get { return ViewState["SrSalesmanPwdVisibility"] != null ? Convert.ToString(ViewState["SrSalesmanPwdVisibility"]) : ""; }
            set { ViewState["SrSalesmanPwdVisibility"] = value; }
        }
        protected string SrSalesManMessage = "";
        protected static string jobId = string.Empty;
        protected static string salesmanCode = "";
        protected static int LoggedinUserID = 0;
        protected int customerId = 0;
        
       
        //#- Private Variables
        private Boolean IsPageRefresh = false;
        private int estimateId = 0, productTypeId = 0;

        #region "Custom Material List"
        protected string ElabJobID
        {
            get { return ViewState["ElabJobID"] != null ? Convert.ToString(ViewState["ElabJobID"]) : ""; }
            set { ViewState["ElabJobID"] = value; }
        }
        protected string CustomerName
        {
            get { return ViewState["CustomerName"] != null ? Convert.ToString(ViewState["CustomerName"]) : ""; }
            set { ViewState["CustomerName"] = value; }
        }
        protected int StaffID
        {
            get { return ViewState["StaffID"] != null ? Convert.ToInt32(ViewState["StaffID"]) : 0; }
            set { ViewState["StaffID"] = value; }
        }
        protected string StaffName
        {
            get { return ViewState["StaffName"] != null ? Convert.ToString(ViewState["StaffName"]) : ""; }
            set { ViewState["StaffName"] = value; }
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

        protected DataTable VendorCategoryList
        {
            get { return ViewState["VendorCategoryList"] != null ? ((DataTable)ViewState["VendorCategoryList"]) : new DataTable(); }
            set { ViewState["VendorCategoryList"] = value; }
        }

        protected DataTable VendorList
        {
            get { return ViewState["VendorList"] != null ? ((DataTable)ViewState["VendorList"]) : new DataTable(); }
            set { ViewState["VendorList"] = value; }
        }
        protected DataTable MaterialListAttachment
        {
            get { return ViewState["MaterialListAttachment"] != null ? ((DataTable)ViewState["MaterialListAttachment"]) : new DataTable(); }
            set { ViewState["MaterialListAttachment"] = value; }
        }
        #endregion

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
          
            jobId = Session[SessionKey.Key.JobId.ToString()].ToString();
            LoggedinUserID = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            salesmanCode = Session["loginpassword"].ToString();
            if (Request.QueryString[QueryStringKey.Key.ProductId.ToString()] != null)
            {
                estimateId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);
            }
            if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null)
            {
                customerId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
            }
            if (Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null)
            {
                productTypeId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
            }

            //setPermissions();
            if (!IsPostBack)
            {
                ForemanPwdVisibility = "none";
                SalesmanPwdVisibility = "none";
                AdminPwdVisibility = "none";
                SrSalesmanPwdVisibility = "none";
                InitialDataBind();
                //bindMaterialList();
                SetButtonText();
            }
            btnSendMail.Text = "Send Mail To Vendors";
          
        }

        DataSet DS = new DataSet();

        public void SetButtonText()
        {
            string EmailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
            int result = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (result == 0) //if list doesn't exists
            {
                btnSendMail.Text = "Save";
                showVendorCategoriesPermissions();
            }
            else  //if list exists
            {
                if (EmailStatus == JGConstant.EMAIL_STATUS_NONE || EmailStatus == string.Empty)       //if no email was sent
                {
                    int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
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
                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(jobId);//, productTypeId, estimateId);
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
        private void EnableVendorNameAndAmount()
        {
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
                ddlVendorName.Enabled = true;
                TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                txtAmount.Enabled = true;
            }
        }

        private void DisableVendorNameAndAmount()
        {
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
                ddlVendorName.Enabled = false;
                TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                txtAmount.Enabled = false;
            }
        }
        public void setPermissions()
        {
            DataSet ds = CustomBLL.Instance.GetAllPermissionOfCustomMaterialList(jobId);//, productTypeId, estimateId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<CustomMaterialList> lCustomMaterial = (List<CustomMaterialList>)ViewState["CustomMaterialList"];
                if (lnkForemanPermission.Visible == true)
                {
                    //#- Since we have text boxes, we don't need this controls.
                    lnkForemanPermission.Visible = false;
                    lnkSrSalesmanPermissionF.Visible = false;
                   // pnlAdmin.Visible = false;
                   // pnlSrSalesman.Visible = false;

                    if (Convert.ToChar(ds.Tables[0].Rows[0]["IsForemanPermission"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                    {
                        lnkForemanPermission.Enabled = false;
                        ForemanPwdVisibility = "";
                        ForemanMessage = "<a href='EditUser.aspx?id=" + lCustomMaterial[0].ForemaneID + "'>" + lCustomMaterial[0].ForemaneID + "</a> - " + (lCustomMaterial[0].ForemanFirstName.Trim() != "" ? lCustomMaterial[0].ForemanFirstName + " " + lCustomMaterial[0].ForemanLastName : lCustomMaterial[0].ForemanUserName) + " has approved the material list.";
                        txtForemanManPwd.Visible = false;
                        lnkForemanPermission.ForeColor = System.Drawing.Color.DarkGray;
                        popupForeman_permission.TargetControlID = "hdnForeman";
                    }
                    if (Convert.ToChar(ds.Tables[0].Rows[0]["IsSrSalemanPermissionF"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                    {
                        lnkSrSalesmanPermissionF.Enabled = false;
                        SalesmanPwdVisibility = "";
                        SalesmanMessage = "<a href='EditUser.aspx?id=" + lCustomMaterial[0].SrSaleManFID + "'>" + lCustomMaterial[0].SrSaleManFID + "</a> - " + (lCustomMaterial[0].SrSaleManFFirstName.Trim() != "" ? lCustomMaterial[0].SrSaleManFFirstName + " " + lCustomMaterial[0].SrSaleManFLastName : lCustomMaterial[0].SrSaleManFUserName) + " has approved the material list.";
                        txtSrSalesManPwd.Visible = false;
                        lnkSrSalesmanPermissionF.ForeColor = System.Drawing.Color.DarkGray;
                        popupSrSalesmanPermissionF.TargetControlID = "hdnSrF";
                    }
                }
                if (lnkAdminPermission.Visible == true)
                {
                    //pnlForeman.Visible = false;
                    //pnlSalesF.Visible = false;
                    lnkAdminPermission.Visible = false;
                    lnkSrSalesmanPermissionA.Visible = false;
                    if (Convert.ToChar(ds.Tables[0].Rows[0]["IsAdminPermission"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                    {
                        lnkAdminPermission.Enabled = false;
                        AdminPwdVisibility = "";
                        AdminMessage = "<a href='EditUser.aspx?id=" + lCustomMaterial[0].AdminID + "'>" + lCustomMaterial[0].AdminID + "</a> - " + (lCustomMaterial[0].AdminFirstName.Trim() != "" ? lCustomMaterial[0].AdminFirstName + " " + lCustomMaterial[0].AdminLastName : lCustomMaterial[0].AdminUserName) + " has approved the material list.";
                        txtAdminPwd.Visible = false;
                        lnkAdminPermission.ForeColor = System.Drawing.Color.DarkGray;
                        popupAdmin_permission.TargetControlID = "hdnAdmin";
                    }
                    if (Convert.ToChar(ds.Tables[0].Rows[0]["IsSrSalemanPermissionA"].ToString().Trim()) == JGConstant.PERMISSION_STATUS_GRANTED)
                    {
                        lnkSrSalesmanPermissionA.Enabled = false;
                        SrSalesmanPwdVisibility = "";
                        SrSalesManMessage = "<a href='EditUser.aspx?id=" + lCustomMaterial[0].SrSaleManAID + "'>" + lCustomMaterial[0].SrSaleManAID + "</a> - " + (lCustomMaterial[0].SrSaleManAFirstName.Trim() != "" ? lCustomMaterial[0].SrSaleManAFirstName + " " + lCustomMaterial[0].SrSaleManALastName : lCustomMaterial[0].SrSaleManAUserName) + " has approved the material list.";
                        txtSrSales1Pwd.Visible = false;
                        lnkSrSalesmanPermissionA.ForeColor = System.Drawing.Color.DarkGray;
                        popupSrSalesmanPermissionA.TargetControlID = "hdnSrA";
                    }
                }
            }
            else
            { //#- Since we have text boxes, we don't need this controls.
                lnkForemanPermission.Visible = false;
                lnkSrSalesmanPermissionF.Visible = false;
                lnkAdminPermission.Visible = false;
                lnkSrSalesmanPermissionA.Visible = false;
                pnlAdmin.Visible = false;
                pnlSrSalesman.Visible = false;
            }
        }
        public void showVendorCategoriesPermissions()
        {
            lnkForemanPermission.Visible = true;
            lnkSrSalesmanPermissionF.Visible = true;
            lnkAdminPermission.Visible = false;
            lnkSrSalesmanPermissionA.Visible = false;
            setPermissions();
        }
        public void showVendorPermissions()
        {
            lnkForemanPermission.Visible = false;
            lnkSrSalesmanPermissionF.Visible = false;
            lnkAdminPermission.Visible = true;
            lnkSrSalesmanPermissionA.Visible = true;

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

                DropDownList ddlVendorName = (DropDownList)gr.FindControl("ddlVendorName");
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

                DropDownList ddlVendorName = (DropDownList)gr.FindControl("ddlVendorName");
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

        private void bindMaterialList2()
        {
            DataSet ds = CustomBLL.Instance.GetCustom_MaterialList(jobId.ToString(), customerId);//,productTypeId,estimateId);
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

                    cm.ForemaneID = Convert.ToInt32(dr["foremanID"].ToString());
                    cm.ForemanFirstName = dr["foremanFirstName"].ToString();
                    cm.ForemanLastName = dr["foremanLastName"].ToString();
                    cm.ForemanUserName = dr["foremanUserName"].ToString();

                    cm.SrSaleManFID = Convert.ToInt32(dr["SLFID"].ToString());
                    cm.SrSaleManFFirstName = dr["SLFFirstName"].ToString();
                    cm.SrSaleManFLastName = dr["SLFLastName"].ToString();
                    cm.SrSaleManFUserName = dr["SLFUserName"].ToString();

                    cm.SrSaleManAID = Convert.ToInt32(dr["SLAID"].ToString());
                    cm.SrSaleManAFirstName = dr["SLAFirstName"].ToString();
                    cm.SrSaleManALastName = dr["SLALastName"].ToString();
                    cm.SrSaleManAUserName = dr["SLAUserName"].ToString();

                    cm.AdminID = Convert.ToInt32(dr["ADID"].ToString());
                    cm.AdminFirstName = dr["ADFirstName"].ToString();
                    cm.AdminLastName = dr["ADLastName"].ToString();
                    cm.AdminUserName = dr["ADUserName"].ToString();
                    cmList.Add(cm);
                    StaffID = Convert.ToInt32(dr["lastUpdatedByID"].ToString());
                    StaffName = dr["lastUpdatedByfirstname"].ToString().Trim() != "" ? (dr["lastUpdatedByfirstName"] + " " + dr["lastUpdatedBylastname"]) : dr["lastUpdatedByusername"].ToString();
                }
                CustomMaterialList cm1 = new CustomMaterialList();
                cm1.Id = 0;
                cm1.MaterialList = "";
                cm1.VendorCategoryId = 0;
                cm1.VendorCategoryName = "";
                cm1.VendorId = 0;
                cm1.VendorName = "";
                cm1.Amount = 0;
                cm1.ProductCatId = productTypeId;
                cm1.DocName = "";
                cm1.TempName = "";
                cm1.IsForemanPermission = "";
                cm1.IsSrSalemanPermissionF = "";
                cm1.IsAdminPermission = "";
                cm1.IsSrSalemanPermissionA = "";
                cm1.Status = JGConstant.CustomMaterialListStatus.Unchanged;
                cm1.ForemaneID = 0;
                cm1.ForemanFirstName = "";
                cm1.ForemanLastName = "";
                cm1.ForemanUserName = "";

                cm1.SrSaleManFID = 0;
                cm1.ForemanFirstName = "";
                cm1.ForemanLastName = "";
                cm1.ForemanUserName = "";

                cm1.SrSaleManAID = 0;
                cm1.ForemanFirstName = "";
                cm1.ForemanLastName = "";
                cm1.ForemanUserName = "";

                cm1.AdminID = 0;
                cm1.ForemanFirstName = "";
                cm1.ForemanLastName = "";
                cm1.ForemanUserName = "";
                cmList.Add(cm1);
                ViewState["CustomMaterialList"] = cmList;

                BindCustomMaterialList(cmList);
            }
            else
            {
                List<CustomMaterialList> cmList1 = BindEmptyRowToMaterialList();
                ViewState["CustomMaterialList"] = cmList1;
                BindCustomMaterialList(cmList1);
            }

            //#- Added By Shabbir Kanchwala
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    //Response.Redirect("Custom.aspx?ProductTypeId=" + Convert.ToInt16(hdnProductTypeId.Value) + "&ProductId=" + productId + "&CustomerId=" + customerId);
                    ElabJobID = jobId.Substring(0, 1) + "<a href='Customer_Profile.aspx?CustomerId=" + customerId + "'>" + jobId.Substring(1, jobId.IndexOf("-") - 1) + "</a>-<a href='Custom.aspx?ProductTypeId=" + ds.Tables[1].Rows[0]["producttypeid"].ToString() + "&ProductId=" + ds.Tables[1].Rows[0]["productid"].ToString() + "&CustomerId=" + customerId + "'>" + jobId.Substring(jobId.IndexOf("-") + 1) + "</a>";
                    CustomerName = ds.Tables[1].Rows[0]["CustomerName"].ToString() + " " + ds.Tables[1].Rows[0]["LastName"].ToString();
                }
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
            cm1.ProductCatId = productTypeId;
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
                cm.ProductCatId = productTypeId;
                HiddenField hdnEmailStatus = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnEmailStatus");
                if (hdnEmailStatus.Value.ToString() != "")
                    cm.EmailStatus = hdnEmailStatus.Value;

                HiddenField hdnForemanPermission = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnForemanPermission");
                if (hdnForemanPermission.Value.ToString() != "")
                    cm.IsForemanPermission = hdnForemanPermission.Value;

                HiddenField hdnSrSalesmanPermissionF = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnSrSalesmanPermissionF");
                if (hdnSrSalesmanPermissionF.Value.ToString() != "")
                    cm.IsSrSalemanPermissionF = hdnSrSalesmanPermissionF.Value;

                HiddenField hdnAdminPermission = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnAdminPermission");
                if (hdnAdminPermission.Value.ToString() != "")
                    cm.IsAdminPermission = hdnAdminPermission.Value;

                HiddenField hdnSrSalesmanPermissionA = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnSrSalesmanPermissionA");
                if (hdnSrSalesmanPermissionA.Value.ToString() != "")
                    cm.IsSrSalemanPermissionA = hdnSrSalesmanPermissionA.Value;

                HiddenField hdnMaterialListId = (HiddenField)grdcustom_material_list.Rows[i].FindControl("hdnMaterialListId");
                if (hdnMaterialListId.Value.ToString() != "")
                    cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
                TextBox txtMateriallist = (TextBox)grdcustom_material_list.Rows[i].FindControl("txtMateriallist");
                cm.MaterialList = txtMateriallist.Text;

                DropDownList ddlVendorCategory = (DropDownList)grdcustom_material_list.Rows[i].FindControl("ddlVendorCategory");
                if (ddlVendorCategory.SelectedIndex != -1)
                {
                    cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
                    cm.VendorCategoryName = ddlVendorCategory.SelectedItem.Text;
                }
                DropDownList ddlVendorName = (DropDownList)grdcustom_material_list.Rows[i].FindControl("ddlVendorName");
                if (ddlVendorName.SelectedIndex != -1)
                {
                    cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);
                    cm.VendorName = ddlVendorName.SelectedItem.Text;
                }

                LinkButton lnkQuote = (LinkButton)grdcustom_material_list.Rows[i].FindControl("lnkQuote");
                if (lnkQuote.Text != "")
                {
                    cm.DocName = lnkQuote.Text;
                    cm.TempName = lnkQuote.CommandArgument;
                }
                TextBox txtAmount = (TextBox)grdcustom_material_list.Rows[i].FindControl("txtAmount");
                if (txtAmount.Text != "")
                    cm.Amount = Convert.ToDecimal(txtAmount.Text);

                itemList.Add(cm);
            }
            return itemList;
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
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);

            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                CustomMaterialList cml = cmList[j];
                if (cml.Status != JGConstant.CustomMaterialListStatus.Deleted)
                {
                    Label lblsrno = (Label)r.FindControl("lblsrno");

                    DropDownList ddlVendorCategory1 = (DropDownList)r.FindControl("ddlVendorCategory");
                    DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
                    TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                    LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                    HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
                    HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
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
                    if (Convert.ToInt16(cml.Id.ToString()) != 0)
                    {
                        hdnMaterialListId.Value = cml.Id.ToString();
                    }
                    else
                    {
                        hdnMaterialListId.Value = "0";
                    }
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
                    if (cml.EmailStatus != "")
                    {
                        hdnEmailStatus.Value = cml.EmailStatus;
                    }
                    else
                    {
                        hdnEmailStatus.Value = "";
                    }
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
            DataSet ds = VendorBLL.Instance.GetVendorQuoteByVendorId(jobId, vendorId);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                ddlVendorName.SelectedIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First attach quote for this vendor');", true);

            }

            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                DropDownList ddlVendorName1 = (DropDownList)r.FindControl("ddlVendorName");
                {
                    DataSet dsVendorQuoute = VendorBLL.Instance.GetVendorQuoteByVendorId(jobId, Convert.ToInt16(ddlVendorName1.SelectedValue));
                    LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                    if (dsVendorQuoute.Tables[0].Rows.Count > 0)
                    {
                        lnkQuote.Text = dsVendorQuoute.Tables[0].Rows[0]["DocName"].ToString();
                        lnkQuote.CommandArgument = dsVendorQuoute.Tables[0].Rows[0]["TempName"].ToString();
                        lnkQuote.CommandName = "View";
                    }

                }
            }
            Add_Click(sender, e);
        }
        protected void ddlVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlVendorCategory = (DropDownList)sender;

            string selectedCategory = ddlVendorCategory.SelectedItem.Text;
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
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
                    DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
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
            Add_Click(sender, e);
        }
        protected void grdcustom_material_list_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<CustomMaterialList> cmList = GetMaterialListFromGrid();
            if (cmList.Count > 1)
            {
                CustomMaterialList cm = cmList[e.RowIndex];
                if (cm.Id > 0)
                {
                    cm.Status = JGConstant.CustomMaterialListStatus.Deleted;
                    UpdateMaterialList(cm, e.RowIndex);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('This row cannot be deleted. Deleting this row will block all options to add new custom material.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Atleast one row must be there in Custom- Material List');", true);
            }
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

        protected void grdcustom_material_list_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblsrno = (Label)e.Row.FindControl("lblsrno");
                lblsrno.Text = Convert.ToString(Convert.ToInt16(lblsrno.Text) + 1);
                DropDownList ddlVendorCategory = (DropDownList)e.Row.FindControl("ddlVendorCategory");
                DataSet dsVendorCategory = GetVendorCategories();
                ddlVendorCategory.DataSource = GetVendorCategories();
                ddlVendorCategory.DataSource = dsVendorCategory;
                ddlVendorCategory.DataTextField = "VendorCategoryNm";
                ddlVendorCategory.DataValueField = "VendorCategpryId";
                ddlVendorCategory.DataBind();
                ddlVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                ddlVendorCategory.SelectedIndex = 0;
            }
            if (btnSendMail.Text == "Send Mail To Vendor(s)")
            {
                grdcustom_material_list.Columns[6].Visible = false;
            }

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procurement.aspx");
        }

        protected void btnXForeman_Click(object sender, EventArgs e)
        {
            popupForeman_permission.Hide();
        }
        protected void btnXSrSalesmanF_Click(object sender, EventArgs e)
        {
            popupSrSalesmanPermissionF.Hide();
        }

        protected void btnXSrSalesmanA_Click(object sender, EventArgs e)
        {
            popupSrSalesmanPermissionA.Hide();
        }

        protected void btnXAdmin_Click(object sender, EventArgs e)
        {
            popupAdmin_permission.Hide();
        }

        protected void VerifyForemanPermission(object sender, EventArgs e)
        {

            int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtForemanPassword.Text))
                {
                    string adminCode = AdminBLL.Instance.GetForemanCode();
                    if (adminCode != txtForemanPassword.Text.Trim())
                    {
                        CVForeman.ErrorMessage = "Invalid Foreman Code";
                        CVForeman.ForeColor = System.Drawing.Color.Red;
                        CVForeman.IsValid = false;
                        CVForeman.Visible = true;
                        popupForeman_permission.Show();
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateForemanPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            lnkForemanPermission.Enabled = false;
                            lnkForemanPermission.ForeColor = System.Drawing.Color.DarkGray;
                            popupForeman_permission.TargetControlID = "hdnForeman";
                            SetButtonText();
                        }
                    }
                }
                else
                {
                    CVForeman.ErrorMessage = "Please Enter Foreman Code";
                    CVForeman.ForeColor = System.Drawing.Color.Red;
                    CVForeman.IsValid = false;
                    CVForeman.Visible = true;
                    popupForeman_permission.Show();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);

            }

        }

        protected void VerifySrSalesmanPermissionF(object sender, EventArgs e)
        {
            int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtSrSalesmanPasswordF.Text))
                {
                    salesmanCode = Session["loginpassword"].ToString();
                    if (salesmanCode != txtSrSalesmanPasswordF.Text.Trim())
                    {
                        CVSrSalesmanF.ErrorMessage = "Invalid Sr. Salesman Code";
                        CVSrSalesmanF.ForeColor = System.Drawing.Color.Red;
                        CVSrSalesmanF.IsValid = false;
                        CVSrSalesmanF.Visible = true;
                        popupSrSalesmanPermissionF.Show();
                        return;
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialListF(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            lnkSrSalesmanPermissionF.Enabled = false;
                            lnkSrSalesmanPermissionF.ForeColor = System.Drawing.Color.DarkGray;
                            popupSrSalesmanPermissionF.TargetControlID = "hdnSrF";
                            SetButtonText();
                        }
                    }
                }
                else
                {
                    CVSrSalesmanF.ErrorMessage = "Please Enter Sr. Salesman Code";
                    CVSrSalesmanF.ForeColor = System.Drawing.Color.Red;
                    CVSrSalesmanF.IsValid = false;
                    CVSrSalesmanF.Visible = true;
                    popupSrSalesmanPermissionF.Show();
                    return;

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
            }
        }

        protected void VerifyAdminPermission(object sender, EventArgs e)
        {
            int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(jobId);//,productTypeId,estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtAdminPassword.Text))
                {
                    string adminCode = AdminBLL.Instance.GetAdminCode();
                    if (adminCode != txtAdminPassword.Text.Trim())
                    {
                        CVAdmin.ErrorMessage = "Invalid Admin Code";
                        CVAdmin.ForeColor = System.Drawing.Color.Red;
                        CVAdmin.IsValid = false;
                        CVAdmin.Visible = true;
                        popupAdmin_permission.Show();
                        return;
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateAdminPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            lnkAdminPermission.Enabled = false;
                            lnkAdminPermission.ForeColor = System.Drawing.Color.DarkGray;
                            popupAdmin_permission.TargetControlID = "hdnAdmin";
                            SetButtonText();
                            DisableVendorNameAndAmount();
                        }
                    }
                }
                else
                {
                    CVAdmin.ErrorMessage = "Please Enter Admin Code";
                    CVAdmin.ForeColor = System.Drawing.Color.Red;
                    CVAdmin.IsValid = false;
                    CVAdmin.Visible = true;
                    popupAdmin_permission.Show();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List and enter all vendor names');", true);
            }
            //message mail is not sent to categories
        }

        protected void VerifySrSalesmanPermissionA(object sender, EventArgs e)
        {
            int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(txtSrSalesmanPasswordA.Text))
                {
                    salesmanCode = Session["loginpassword"].ToString();
                    if (salesmanCode != txtSrSalesmanPasswordA.Text.Trim())
                    {
                        CVSrSalesmanA.ErrorMessage = "Invalid Sr. Salesman Code";
                        CVSrSalesmanA.ForeColor = System.Drawing.Color.Red;
                        CVSrSalesmanA.IsValid = false;
                        CVSrSalesmanA.Visible = true;
                        popupSrSalesmanPermissionA.Show();
                        return;
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                        }
                        else
                        {
                            lnkSrSalesmanPermissionA.Enabled = false;
                            lnkSrSalesmanPermissionA.ForeColor = System.Drawing.Color.DarkGray;
                            popupSrSalesmanPermissionA.TargetControlID = "hdnSrA";
                            SetButtonText();
                            DisableVendorNameAndAmount();
                        }
                    }
                }
                else
                {
                    CVSrSalesmanA.ErrorMessage = "Please Enter Sr. Salesman Code";
                    CVSrSalesmanA.ForeColor = System.Drawing.Color.Red;
                    CVSrSalesmanA.IsValid = false;
                    CVSrSalesmanA.Visible = true;
                    popupSrSalesmanPermissionA.Show();
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List and enter all vendor names');", true);
            }
        }

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

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
                List<CustomMaterialList> cmList = new List<CustomMaterialList>();
                foreach (GridViewRow r in grdcustom_material_list.Rows)
                {
                    CustomMaterialList cm = new CustomMaterialList();
                    DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
                    cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
                    TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
                    HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
                    HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
                    HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
                    HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
                    HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
                    HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");
                    cm.ProductCatId = productTypeId;
                    if (txtMateriallist.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill Material List(s).');", true);
                    }
                    else
                    {
                        cm.MaterialList = txtMateriallist.Text;
                    }

                    if (hdnMaterialListId.Value != "")
                    {
                        cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
                    }
                    else
                    {
                        cm.Id = 0;
                    }
                    DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
                    TextBox txtAmount = (TextBox)r.FindControl("txtAmount");

                    if (status == "C") //mail was already sent to vendor categories
                    {
                        if (ddlVendorName.SelectedItem.Text == "Select")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select vendor name.');", true);
                            //return;
                        }
                        else
                        {
                            cm.VendorName = ddlVendorName.SelectedItem.Text;
                            cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);

                            DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
                            cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
                        }

                        if (txtAmount.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter amount.');", true);
                            return;
                        }
                        else
                        {
                            cm.Amount = Convert.ToDecimal(txtAmount.Text);
                        }
                        if (lnkAdminPermission.Enabled == true)
                        {
                            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }
                        if (lnkSrSalesmanPermissionA.Enabled == true)
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
                    }
                    else // mail was not sent to vendor categories
                    {
                        cm.VendorName = "";
                        cm.VendorEmail = "";
                        cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        if (lnkForemanPermission.Enabled == true)
                        {
                            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }
                        if (lnkSrSalesmanPermissionF.Enabled == true)
                        {
                            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }

                        cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
                    }
                    cmList.Add(cm);
                }
                if (btnSendMail.Text == "Save") //#- Commented By Shabbir because we have added code on Add Button. Though the permission issue is remaining.
                {
                    /* int existsList = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
                     if (existsList == 0)
                     {
                         saveCustom_MaterialList(cmList);
                     }
                     else
                     {
                         EnableVendorNameAndAmount();
                         int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
                         if (permissionStatusCategories == 0)
                         {
                             saveCustom_MaterialList(cmList);
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
                             return;
                         }
                         else
                         {
                             int permissionStatusVendors = CustomBLL.Instance.CheckPermissionsForVendors(jobId);//, productTypeId, estimateId);
                             if (permissionStatusVendors == 0)
                             {
                                 saveCustom_MaterialList(cmList);
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
                                 return;
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('After giving permissions lists cann't be changed');", true);
                                 return;
                             }
                         }
                     }
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);*/
                }

                else if (btnSendMail.Text == "Send Mail To Vendor Category(s)")
                {

                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        bool emailStatusVendorCategory = sendEmailToVendorCategories(cmList);

                        if (emailStatusVendorCategory == true)
                        {
                            bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
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
                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(jobId);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        int statusQuotes = CustomBLL.Instance.WhetherVendorQuotesExists(jobId);
                        if (statusQuotes == 1)
                        {

                            bool emailStatusVendor = sendEmailToVendors(cmList);
                            if (emailStatusVendor == true)
                            {
                                bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDOR);//, productTypeId, estimateId);
                                UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDOR.ToString());
                                btnSendMail.Text = "Save";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Email is sent to all vendors');", true);
                                setControlsAfterSendingBothMails();
                                string fileName = GenerateWorkOrder();
                                string url = ConfigurationManager.AppSettings["URL"].ToString();
                                ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + fileName + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
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
        protected string GenerateWorkOrder()
        {
            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string tempWorkOrderFilename = "WorkOrder" + DateTime.Now.Ticks + ".pdf";
            string originalWorkOrderFilename = "WorkOrder" + ".pdf";
            string soldjobId = Session["jobId"].ToString();
            // DataSet dssoldJobs = new_customerBLL.Instance.GetProductAndEstimateIdOfSoldJob(soldjobId);
            int productId = estimateId;// Convert.ToInt16(dssoldJobs.Tables[0].Rows[0]["EstimateId"].ToString());
            GeneratePDF(path, tempWorkOrderFilename, false, createWorkOrder("Work Order-" + customerId.ToString(), productId, soldjobId));

            new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(customerId.ToString()), productId, originalWorkOrderFilename, "WorkOrder", tempWorkOrderFilename, productTypeId, 0);
            return tempWorkOrderFilename;
        }

        private string createWorkOrder(string InvoiceNo, int estimateId, string soldJobId)
        {
            return pdf_BLL.Instance.CreateWorkOrder(InvoiceNo, estimateId, productTypeId, customerId, soldJobId);
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
            lnkAdminPermission.Enabled = false;
            lnkForemanPermission.Enabled = false;
            lnkSrSalesmanPermissionA.Enabled = false;
            lnkSrSalesmanPermissionF.Enabled = false;
            foreach (GridViewRow r in grdcustom_material_list.Rows)
            {
                TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
                txtMateriallist.Enabled = false;
                DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
                ddlVendorCategory.Enabled = false;
                DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
                ddlVendorName.Enabled = false;
                TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                txtAmount.Enabled = false;
                LinkButton lnkQuote = (LinkButton)r.FindControl("lnkQuote");
                lnkQuote.Enabled = true;
            }
        }
        protected void setControlsForVendors()
        {
            DataSet ds1 = CustomBLL.Instance.GetCustom_MaterialList(jobId);//,productTypeId,estimateId);
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
                DropDownList ddlVendorName = (DropDownList)gr.FindControl("ddlVendorName");
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
            lnkAdminPermission.Visible = true;
            lnkSrSalesmanPermissionA.Visible = true;
            lnkForemanPermission.Visible = false;
            lnkSrSalesmanPermissionF.Visible = false;
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
                DropDownList ddlVendorName = (DropDownList)gr.FindControl("ddlVendorName");
                ddlVendorName.Enabled = true;
            }
            lnkAdminPermission.Visible = true;
            lnkSrSalesmanPermissionA.Visible = true;
            lnkForemanPermission.Visible = false;
            lnkSrSalesmanPermissionF.Visible = false;
            grdcustom_material_list.Columns[6].Visible = false;
        }

        protected void saveCustom_MaterialList(List<CustomMaterialList> cmList)
        {
            bool result = false;
            CustomBLL.Instance.DeleteCustomMaterialList(jobId);//, productTypeId, estimateId);
            foreach (CustomMaterialList cm in cmList)
            {

                result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);//,productTypeId,estimateId);
            }

            ViewState["CustomMaterialList"] = cmList;
        }

        public DataSet fetchVendorCategoryEmailTemplate()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.GetEmailTemplate("Manufacturer");
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
                            SmtpClient sc = new SmtpClient("jmgroveconstruction.com", 26);

                            string userName = ConfigurationManager.AppSettings["VendorCategoryUserName"].ToString();
                            string password = ConfigurationManager.AppSettings["VendorCategoryPassword"].ToString();

                            m.From = new MailAddress(userName, "JMGROVECONSTRUCTION");
                            m.To.Add(new MailAddress(mailId, vendorName));
                            m.Subject = "J.M. Grove " + jobId + " quote request ";
                            m.IsBodyHtml = true;
                            DataSet dsEmailTemplate = fetchVendorCategoryEmailTemplate();

                            if (dsEmailTemplate != null)
                            {
                                string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                                StringBuilder tHeader = new StringBuilder();
                                tHeader.Append(templateHeader);
                                var replacedHeader = tHeader//.Replace("imgHeader", "<img src=cid:myImageHeader height=10% width=80%>")
                                                               .Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                            .Replace("lblJobId", jobId.ToString())
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
                            sc.Credentials = new System.Net.NetworkCredential(userName, password);
                            sc.UseDefaultCredentials = true;
                            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                            sc.EnableSsl = false; // runtime encrypt the SMTP communications using SSL
                            try
                            {
                                sc.Send(m);
                                emailCounter += 1;
                            }
                            catch (Exception ex)
                            {
                                mailNotSendIds += mailId + " , ";
                                CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_NONE);//, productTypeId, estimateId);
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
            ds = AdminBLL.Instance.GetEmailTemplate("VendorEmail_Template"); ;
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
                foreach (CustomMaterialList cm in cmList)
                {
                    DataSet dsVendorQuoute = VendorBLL.Instance.GetVendorQuoteByVendorId(jobId, cm.VendorId);
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
                    m.Subject = "J.M. Grove " + jobId + " quote acceptance ";
                    m.IsBodyHtml = true;
                    DataSet dsEmailTemplate = fetchVendorEmailTemplate();

                    if (dsEmailTemplate != null)
                    {
                        string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                        StringBuilder tHeader = new StringBuilder();
                        tHeader.Append(templateHeader);

                        var replacedHeader = tHeader//.Replace("imgHeader", "<img src=cid:myImageHeader height=10% width=80%>")
                                                   .Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                   .Replace("lblJobId", jobId.ToString())
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
                    sc.Host = "mail.jmgroveconstruction.com";
                    sc.Port = 587;
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
                        CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
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

        protected void DeleteFile(object sender, EventArgs e)
        {
            Int32 lAttachmentID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            DataSet lDsAttachment = AdminBLL.Instance.DeleteEmailAttachment(lAttachmentID);
            string fileName = (Server.MapPath(lDsAttachment.Tables[0].Rows[0]["DocumentPath"].ToString()));
            //bool res = AdminBLL.Instance.DeleteCustomerAttachment(fileName);
            if (fileName != "")
            {
                File.Delete(fileName);
            }
            Response.Redirect(Request.Url.AbsoluteUri);
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
                else if (e.CommandName == "AttachQuote")
                {
                    string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, Convert.ToInt16(lblProductType.Text),  Convert.ToInt16(hdnproductid.Value));
                    if (emailStatus == JGConstant.EMAIL_STATUS_VENDORCATEGORIES)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "WindowOpen", "window.open('AttachQuotes.aspx','','')", true);
                    }
                    else if (emailStatus == JGConstant.EMAIL_STATUS_VENDOR)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "WindowOpen", "window.open('AttachQuotes.aspx?EmailStatus=" + emailStatus + "', '', '')", true);
                        Response.Redirect("~/Sr_App/AttachQuotes.aspx?EmailStatus=" + emailStatus);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First send email to all vendor categories');", true);
                    }
                }
                else if (e.CommandName == "Delete")
                {
                    int lMaterialID = Convert.ToInt16(e.CommandArgument.ToString() == "" ? "0" : e.CommandArgument.ToString());
                    if (lMaterialID > 0)
                    {
                        CustomBLL.Instance.DeleteCustomMaterialList(lMaterialID);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region "Shabbir's Methods"

        /// <summary>
        /// This method will verify Foreman permission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string VerifyForemanPermissionWB(String password)
        {
            String lReturnValue = "success";
            int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    string adminCode = AdminBLL.Instance.GetForemanCode();
                    if (adminCode != password.Trim())
                    {
                        lReturnValue = "Invalid Foreman Code";
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateForemanPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            lReturnValue = "First save Material List";
                        }
                    }
                }
                else
                {
                    lReturnValue = "Please Enter Foreman Code";
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First save Material List');", true);
                lReturnValue = "Please save material list first.";
            }
            return lReturnValue;
        }

        /// <summary>
        /// This method will verify Sr. Salesman permission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string VerifySrSalesmanPermissionFWB(string password)
        {
            string lResult = "success";
            int cResult = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    if (salesmanCode != password.Trim())
                    {
                        lResult = "Invalid Sr. Salesman Code";
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialListF(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            lResult = "First save Material List";
                        }
                        else
                        {
                            //SetButtonText();
                        }
                    }
                }
                else
                {
                    lResult = "Please Enter Sr. Salesman Code";
                }
            }
            else
            {
                lResult = "First save Material List";
            }
            return lResult;
        }

        /// <summary>
        /// This method will verify Admin permission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string VerifyAdminPermissionWB(string password)
        {
            String lResult = "";
            int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(jobId);//,productTypeId,estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    string adminCode = AdminBLL.Instance.GetAdminCode();
                    if (adminCode != password.Trim())
                    {
                        lResult = "Invalid Admin Code";
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateAdminPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            lResult = "First save Material List";
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    lResult = "Please Enter Admin Code";
                }
            }
            else
            {
                lResult = "First save Material List and enter all vendor names";
            }
            return lResult;
        }

        [WebMethod]
        public static Int32 AllowPermissionToInstaller(Int32 pInstallerID, String pPassword) 
        {
           return CustomBLL.Instance.UpdateInstallerPrmToMaterialList(jobId, pInstallerID, pPassword);
        }

        /// <summary>
        /// This method will verify Sr. Salesman permission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string VerifySrSalesmanPermissionAWB(string password)
        {
            string lResult = "";
            int cResult = CustomBLL.Instance.WhetherVendorInCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (cResult == 1)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    if (salesmanCode != password.Trim())
                    {
                        lResult = "Invalid Sr. Salesman Code";
                    }
                    else
                    {
                        int result = CustomBLL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialList(jobId.ToString(), JGConstant.PERMISSION_STATUS_GRANTED, LoggedinUserID);//, productTypeId, estimateId);
                        if (result == 0)
                        {
                            lResult = "First save Material List";
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    lResult = "Please Enter Sr. Salesman Code";

                }
            }
            else
            {
                lResult = "First save Material List and enter all vendor names";
            }
            return lResult;
        }

        /// <summary>
        /// This method will save vendor ids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string UpdateSpecificProductLine(string pFieldName, String pFieldValue, Int32 pID)
        {
            string lResult = "1";
            CustomBLL.Instance.UpdateSpecificProductLine(pFieldName, HttpUtility.UrlDecode( pFieldValue), pID, jobId);
            return lResult;
        }

        /// <summary>
        /// This method will save payment details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string UpdatePaymentDetails(string pFieldName, String pFieldValue, Int32 pProdCatID, Int32 pVendorID)
        {
            string lResult = "1";
            CustomBLL.Instance.UpdateSpecificPaymentDetails(pFieldName, HttpUtility.UrlDecode(pFieldValue), pProdCatID, pVendorID, jobId);
            return lResult;
        }


        /// <summary>
        /// This method will save vendor ids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string SaveVendorIds(string pExcMaterialListId, Int32 pProductCatID, String pVendorIds)
        {
            string lResult = "1";
            CustomBLL.Instance.UpdateVendorIDs(pVendorIds, pProductCatID, pExcMaterialListId, jobId);
            return lResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pExcMaterialListId"></param>
        /// <param name="pProductCatID"></param>
        /// <param name="pVendorIds"></param>
        /// <returns></returns>
        [WebMethod]
        public static string AssociateVendorToCat(Int32 pProductCatID, String pVendorIds, Int32 pProductLineID)
        {
            string lResult = "1";
            CustomBLL.Instance.AssociateVendorToCat(pVendorIds, pProductCatID, pProductLineID, jobId);
            return lResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pProductCatID"></param>
        /// <param name="pVendorIds"></param>
        /// <param name="pProductLineID"></param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateDefaultVendor(Int32 pProductCatID, Boolean pDefaultVendor, Int32 pProductLineID)
        {
            string lResult = "1";
            CustomBLL.Instance.UpdateDefaultVendorsInProdLine(pDefaultVendor, pProductCatID, pProductLineID, jobId);
            return lResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pProductCatID"></param>
        /// <param name="pVendorIds"></param>
        /// <param name="pProductLineID"></param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateVendorPO(Int32 pProductCatID, Int32 pVendorID, Int32 pProductLineID)
        {
            string lResult = "1";
            CustomBLL.Instance.UpdateVendorForPO(pVendorID, pProductCatID, pProductLineID, jobId);
            return lResult;
        }
        /// <summary>
        /// This method will save vendor ids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod]
        public static string SaveVendorIdsForSpecificMaterial(Int32 pMaterialListID, String pVendorIds)
        {
            string lResult = "1";
            CustomBLL.Instance.UpdateVendorIDForSpecMaterial(pVendorIds, pMaterialListID);
            return lResult;
        }

        /// <summary>
        /// This method will auto save the custom material list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Add_Click(object sender, EventArgs e)
        {
            string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
            List<CustomMaterialList> cmList = new List<CustomMaterialList>();


            GridViewRow r = grdcustom_material_list.Rows[grdcustom_material_list.Rows.Count - 1];
            if (sender.GetType().Equals(typeof(LinkButton)))
            {
                r = ((GridViewRow)((LinkButton)sender).Parent.Parent);
            }
            else if (sender.GetType().Equals(typeof(TextBox)))
            {
                r = ((GridViewRow)((TextBox)sender).Parent.Parent);
            }
            else if (sender.GetType().Equals(typeof(DropDownList)))
            {
                r = ((GridViewRow)((DropDownList)sender).Parent.Parent);
            }

            CustomMaterialList cm = new CustomMaterialList();
            DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
            cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
            TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
            HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
            HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
            HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
            HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
            HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
            HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");
            cm.ProductCatId = productTypeId;
            if (txtMateriallist.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill Material List(s).');", true);
            }
            else
            {
                cm.MaterialList = txtMateriallist.Text;
            }

            if (hdnMaterialListId.Value != "")
            {
                cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
            }
            else
            {
                cm.Id = 0;
            }
            DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
            TextBox txtAmount = (TextBox)r.FindControl("txtAmount");

            if (status == "C") //mail was already sent to vendor categories
            {
                if (ddlVendorName.SelectedItem.Text == "Select")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select vendor name.');", true);
                    //return;
                }
                else
                {
                    cm.VendorName = ddlVendorName.SelectedItem.Text;
                    cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);

                    DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
                    cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
                }

                if (txtAmount.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter amount.');", true);
                    return;
                }
                else
                {
                    cm.Amount = Convert.ToDecimal(txtAmount.Text);
                }
                if (lnkAdminPermission.Enabled == true)
                {
                    cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                }
                else
                {
                    cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                }
                if (lnkSrSalesmanPermissionA.Enabled == true)
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
            }
            else // mail was not sent to vendor categories
            {
                cm.VendorName = "";
                cm.VendorEmail = "";
                cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                if (lnkForemanPermission.Enabled == true)
                {
                    cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                }
                else
                {
                    cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                }
                if (lnkSrSalesmanPermissionF.Enabled == true)
                {
                    cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                }
                else
                {
                    cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                }

                cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            }
            cmList.Add(cm);
            if (cm.VendorCategoryId > 0)
            {
                bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);//,productTypeId,estimateId);
            }
            //List<CustomMaterialList> cmList2 = BindEmptyRowToMaterialList();
            //ViewState["CustomMaterialList"] = cmList2;
            //bindMaterialList();
        }

        /// <summary>
        /// This method will auto save the custom material list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            Add_Click(sender, e);
        }

        /// <summary>
        /// This method will auto save the custom material list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtMateriallist_TextChanged(object sender, EventArgs e)
        {
            Add_Click(sender, e);
        }

        //protected void txtExtended_TextChanged(object sender, EventArgs e)
        //{
        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
        //}

        //protected void txtLine_TextChanged(object sender, EventArgs e)
        //{
        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
        //}

        //protected void txtSkuPartNo_TextChanged(object sender, EventArgs e)
        //{
        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
        //}

        //protected void txtDescription_TextChanged(object sender, EventArgs e)
        //{
        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
        //}

        //protected void txtQTY_TextChanged(object sender, EventArgs e)
        //{
        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
        //}

        //protected void txtUOM_TextChanged(object sender, EventArgs e)
        //{
        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
        //}

        protected void ddlVendorName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            SaveMaterialList(sender, e);
        }

        private void SaveMaterialListOld(object sender, EventArgs e)
        {
            //#-This line is not required. 
            /*  string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);

              List<CustomMaterialList> cmList = new List<CustomMaterialList>();


              GridViewRow r=new GridViewRow(0,0, DataControlRowType.DataRow, DataControlRowState.Normal);
              if (sender.GetType().Equals(typeof(LinkButton)))
              {
                  r = ((GridViewRow)((LinkButton)sender).Parent.Parent);
              }
              else if (sender.GetType().Equals(typeof(TextBox)))
              {
                  r = ((GridViewRow)((TextBox)sender).Parent.Parent);
              }
              else if (sender.GetType().Equals(typeof(DropDownList)))
              {
                  r = ((GridViewRow)((DropDownList)sender).Parent.Parent);
              }

              CustomMaterialList cm = new CustomMaterialList();
              //DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
              //cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
              TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
              TextBox txtLine = (TextBox)r.FindControl("txtLine");
              TextBox txtSkuPartNo = (TextBox)r.FindControl("txtSkuPartNo");
              TextBox txtDescription = (TextBox)r.FindControl("txtDescription");
              TextBox txtQTY = (TextBox)r.FindControl("txtQTY");
              TextBox txtUOM = (TextBox)r.FindControl("txtUOM");
              DropDownCheckBoxes ddlVendorName = (DropDownCheckBoxes)r.FindControl("ddlVendorName");
              TextBox txtMaterialCost = (TextBox)r.FindControl("txtMaterialCost");
              DropDownList ddlExtent = (DropDownList)r.FindControl("ddlExtent");
              TextBox txtAmount = (TextBox)r.FindControl("txtAmount");

              HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
              HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
              HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
              HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
              HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
              HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");
              cm.ProductCatId = productTypeId;
              if (txtDescription.Text == "")
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill Material List(s).');", true);
              }
              else
              {
                  cm.MaterialList = txtDescription.Text;
              }

              if (hdnMaterialListId.Value != "")
              {
                  cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
              }
              else
              {
                  cm.Id = 0;
              }
            
            

              if (status == "C") //mail was already sent to vendor categories
              {
                  if (ddlVendorName.SelectedItem.Text == "Select")
                  {
                      //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select vendor name.');", true);
                      //return;
                  }
                  else
                  {
                      cm.VendorName = ddlVendorName.SelectedItem.Text;
                      cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);

                      DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
                      cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
                  }

                  if (txtAmount.Text == "")
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter amount.');", true);
                      return;
                  }
                  else
                  {
                      cm.Amount = Convert.ToDecimal(txtAmount.Text);
                  }
                  if (lnkAdminPermission.Enabled == true)
                  {
                      cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                  }
                  else
                  {
                      cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                  }
                  if (lnkSrSalesmanPermissionA.Enabled == true)
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
              }
              else // mail was not sent to vendor categories
              {
                  cm.VendorName = "";
                  cm.VendorEmail = "";
                  cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                  cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                  if (lnkForemanPermission.Enabled == true)
                  {
                      cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                  }
                  else
                  {
                      cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                  }
                  if (lnkSrSalesmanPermissionF.Enabled == true)
                  {
                      cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                  }
                  else
                  {
                      cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                  }

                  cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
              }
              cmList.Add(cm);
              if (cm.VendorCategoryId > 0)
              {
                  bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);//,productTypeId,estimateId);
              }
              //List<CustomMaterialList> cmList2 = BindEmptyRowToMaterialList();
              //ViewState["CustomMaterialList"] = cmList2;
              bindMaterialList();
              */

            //try
            //{
            //    StringBuilder strerr = new StringBuilder();

            //    //  lblException.Text = Convert.ToString(strerr.Append("Status start"));
            //    string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(SSNo.Text);//, productTypeId, estimateId);
            //    //   lblException.Text = Convert.ToString(strerr.Append("End Status "));
            //    List<CustomMaterialList> cmList = new List<CustomMaterialList>();
            //    foreach (GridViewRow r in grdcustom_material_list.Rows)
            //    {
            //        //  lblException.Text = Convert.ToString(strerr.Append("Customer List found "));
            //        HiddenField HiddenVisible = (HiddenField)r.FindControl("HiddenField1");

            //        CustomMaterialList cm = new CustomMaterialList();
            //        HiddenField hidCategory = (HiddenField)r.FindControl("hidCategory");
            //        TextBox txtLine = (TextBox)r.FindControl("txtLine");
            //        TextBox txtSkuPartNo = (TextBox)r.FindControl("txtSkuPartNo");
            //        TextBox txtDescription = (TextBox)r.FindControl("txtDescription");
            //        TextBox txtQTY = (TextBox)r.FindControl("txtQTY");
            //        TextBox txtUOM = (TextBox)r.FindControl("txtUOM");
            //        TextBox txtMaterialCost = (TextBox)r.FindControl("txtMaterialCost");
            //        LinkButton lblTotal = (LinkButton)r.FindControl("lblTotal");
            //        DropDownList ddlExtent = (DropDownList)r.FindControl("ddlExtent");
            //        //DropDownCheckBoxes ddlVendorName = (DropDownCheckBoxes)r.FindControl("ddlVendorName");

            //        //DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
            //        //cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
            //        //TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
            //        //HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
            //        //database.AddInParameter(command, "@VendorQuotesPath"	varchar(MAX) = '',
            //        //HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
            //        //HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
            //        //HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
            //        //HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
            //        //HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");

            //        if (txtLine.Text == "")
            //        {
            //            if (flag == "")
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill Line Item;", true);
            //            }
            //        }
            //        else
            //        {
            //            cm.Line = txtLine.Text;
            //        }

            //        //if (hdnMaterialListId.Value != "")
            //        //{
            //        //    cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
            //        //}
            //        //else
            //        //{
            //        //    cm.Id = 0;
            //        //}
            //        Saplin.Controls.DropDownCheckBoxes ddlVendorName = (Saplin.Controls.DropDownCheckBoxes)r.FindControl("ddlVendorName");
            //        //TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
            //        if (txtMaterialCost.Text == "")
            //        {
            //            if (flag == "")
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter amount.');", true);
            //                return;
            //            }
            //            else
            //            {
            //                cm.Amount = 0;
            //            }
            //        }
            //        else
            //        {
            //            cm.Amount = Convert.ToDecimal(txtMaterialCost.Text);
            //        }
            //        // if (lnkAdminPermission.Enabled == true)
            //        if (txtAdminPasswordNew.Visible == true)
            //        {
            //            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            //        }
            //        else
            //        {
            //            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
            //        }
            //        //if (lnkSrSalesmanPermissionA.Enabled == true)
            //        if (txtSrSalesPassword.Visible == true)
            //        {
            //            cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            //        }
            //        else
            //        {
            //            cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
            //        }
            //        cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
            //        cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();

            //        cm.EmailStatus = JGConstant.EMAIL_STATUS_VENDORCATEGORIES;
            //        if (hidCategory.Value != "")
            //        {
            //            cm.ProductCatId = Convert.ToInt32(hidCategory.Value);
            //        }
            //        else
            //        {
            //            cm.ProductCatId = Convert.ToInt32(Convert.ToInt32(Session["MaterialListProductId"]));
            //        }
            //        cm.JGSkuPartNo = txtSkuPartNo.Text;
            //        cm.Description = txtDescription.Text;
            //        cm.MaterialList = txtDescription.Text;
            //        cm.Quantity = txtQTY.Text;
            //        cm.UOM = txtUOM.Text;
            //        cm.extend = ddlExtent.SelectedValue;
            //        if (lblTotal.Text != "")
            //        {
            //            cm.Total = Convert.ToDecimal(lblTotal.Text);
            //        }
            //        else
            //        {
            //            cm.Total = 0;
            //        }
            //        if (status == "C") //mail was already sent to vendor categories
            //        {
            //            //  lblException.Text = Convert.ToString(strerr.Append("mail was already sent to vendor categories"));
            //            string VendorId = string.Empty;
            //            for (int i = 0; i < ddlVendorName.Items.Count; i++)
            //            {
            //                if (ddlVendorName.Items[i].Selected)
            //                {
            //                    if (VendorId == string.Empty)
            //                    {
            //                        VendorId = ddlVendorName.Items[i].Value;
            //                    }
            //                    else
            //                    {
            //                        VendorId = VendorId + "," + ddlVendorName.Items[i].Value;
            //                    }
            //                }
            //            }
            //            cm.VendorIds = VendorId;
            //            string VendorNames = string.Empty;
            //            string VendorEmailIds = string.Empty;
            //            DataSet ds = VendorBLL.Instance.getVendorDetails(VendorId);

            //            if (ds.Tables.Count > 0)
            //            {
            //                if (ds.Tables[0].Rows.Count > 0)
            //                {
            //                    lblException.Text = Convert.ToString(strerr.Append("VendorId found"));
            //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //                    {
            //                        if (VendorNames == string.Empty)
            //                        {
            //                            VendorNames = Convert.ToString(ds.Tables[0].Rows[i][1]);
            //                        }
            //                        else
            //                        {
            //                            VendorNames = VendorNames + "," + Convert.ToString(ds.Tables[0].Rows[i][1]);
            //                        }
            //                        if (VendorEmailIds == string.Empty)
            //                        {
            //                            VendorEmailIds = Convert.ToString(ds.Tables[0].Rows[i][6]);
            //                        }
            //                        else
            //                        {
            //                            VendorEmailIds = VendorEmailIds + "," + Convert.ToString(ds.Tables[0].Rows[i][6]);
            //                        }
            //                    }
            //                }
            //            }
            //            cm.VendorNames = VendorNames;
            //            cm.VendorEmails = VendorEmailIds;
            //            //cm.VendorName = ddlVendorName.SelectedItem.Text;
            //            //cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);
            //            //DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
            //            //cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
            //            //if (ddlVendorName.SelectedItem.Text == "Select")
            //            //{
            //            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select vendor name.');", true);
            //            //    //return;
            //            //}
            //            //else
            //            //{
            //            //    cm.VendorName = ddlVendorName.SelectedItem.Text;
            //            //    cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);

            //            //    DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
            //            //    cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
            //            //}


            //        }
            //        else // mail was not sent to vendor categories
            //        {
            //            cm.VendorName = "";
            //            cm.VendorEmail = "";
            //            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            //            cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            //            // if (lnkForemanPermission.Enabled == true)
            //            if (txtForemanPasswordNew.Visible == true)
            //            {
            //                cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            //            }
            //            else
            //            {
            //                cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
            //            }
            //            //if (lnkSrSalesmanPermissionF.Enabled == true)
            //            if (txtSrSalesManPermition.Visible == true)
            //            {
            //                cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
            //            }
            //            else
            //            {
            //                cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
            //            }

            //            cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            //            //if(Convert.ToString(Session["Visible"])=="Visible")
            //            if (Convert.ToString(HiddenVisible.Value) != "")
            //            {
            //                cm.DisplaDLL = Convert.ToString(HiddenVisible.Value);
            //            }
            //            else
            //            {
            //                if (Convert.ToString(Session["Visible"]) == "Visible")
            //                {
            //                    cm.DisplaDLL = "Visible";
            //                }
            //                else
            //                {
            //                    cm.DisplaDLL = "NotVisible";
            //                }
            //            }

            //        }
            //        cmList.Add(cm);
            //    }
            //    if (btnSendMail.Text == "Save")
            //    {
            //        // lblException.Text = Convert.ToString(strerr.Append("Start existsList "));
            //        int existsList = CustomBLL.Instance.WhetherCustomMaterialListExists(SSNo.Text);//, productTypeId, estimateId);
            //        // lblException.Text = Convert.ToString(strerr.Append("End existsList"));
            //        if (existsList == 0)
            //        {
            //            lblException.Text = Convert.ToString(strerr.Append(" existsList = 0"));
            //            saveCustom_MaterialList(cmList);
            //        }
            //        else
            //        {
            //            // lblException.Text = Convert.ToString(strerr.Append(" existsList != 0"));
            //            EnableVendorNameAndAmount();
            //            int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(SSNo.Text);//, productTypeId, estimateId);
            //            if (permissionStatusCategories == 0)
            //            {
            //                saveCustom_MaterialList(cmList);

            //                if (flag == "")
            //                {
            //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                int permissionStatusVendors = CustomBLL.Instance.CheckPermissionsForVendors(SSNo.Text);//, productTypeId, estimateId);
            //                if (permissionStatusVendors == 0)
            //                {
            //                    saveCustom_MaterialList(cmList);
            //                    if (flag == "")
            //                    {
            //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('After giving permissions lists cann't be changed');", true);
            //                    return;
            //                }
            //            }
            //        }
            //        if (flag == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);

            //        }
            //    }
            //    else if (btnSendMail.Text == "Send Mail To Vendor Category(s)")
            //    {

            //        int permissionStatus = CustomBLL.Instance.CheckPermissionsForCategories(SSNo.Text);//, productTypeId, estimateId);
            //        if (permissionStatus == 1)
            //        {
            //            bool emailStatusVendorCategory = sendEmailToVendorCategories(cmList);

            //            if (emailStatusVendorCategory == true)
            //            {
            //                bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(SSNo.Text, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
            //                UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDORCATEGORIES.ToString());
            //                btnSendMail.Text = "Save";
            //                setControlsForVendors();
            //                grdcustom_material_list.Columns[6].Visible = true;
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Email is sent to all vendor categories');", true);

            //            }
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Foreman and Sr. Salesman permission');", true);
            //        }
            //    }
            //    else
            //    {
            //        int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(SSNo.Text);//, productTypeId, estimateId);
            //        if (permissionStatus == 1)
            //        {
            //            int statusQuotes = CustomBLL.Instance.WhetherVendorQuotesExists(SSNo.Text);
            //            if (statusQuotes == 1)
            //            {

            //                bool emailStatusVendor = sendEmailToVendors(cmList);
            //                if (emailStatusVendor == true)
            //                {
            //                    bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(SSNo.Text, JGConstant.EMAIL_STATUS_VENDOR);//, productTypeId, estimateId);
            //                    UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDOR.ToString());
            //                    btnSendMail.Text = "Save";
            //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Email is sent to all vendors');", true);
            //                    setControlsAfterSendingBothMails();

            //                    DeleteExistingWorkorders();
            //                    GenerateWorkOrder();

            //                }
            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First attach quotes.');", true);
            //            }
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Admin and Sr. Salesman permission');", true);
            //        }

            //    }
            //}
            //catch (Exception)
            //{

            //}
        }

        private void DeleteExistingWorkorders()
        {
            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string soldjobId = Session["jobId"].ToString();
            bool result = CustomBLL.Instance.DeleteWorkorders(soldjobId);
        }
        //protected void txtMaterialCost_TextChanged(object sender, EventArgs e)
        //{
        //    //GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent;
        //    //TextBox txt = (TextBox)currentRow.FindControl("txtQTY");
        //    //TextBox txtCost = (TextBox)currentRow.FindControl("txtMaterialCost");
        //    //Label lblCost = (Label)currentRow.FindControl("lblCost");
        //    //int a = 0;
        //    //if (txt.Text == "")
        //    //{
        //    //    txt.Text = Convert.ToString(a);
        //    //}
        //    //if (txtCost.Text == "")
        //    //{
        //    //    txtCost.Text = Convert.ToString(a);
        //    //}
        //    //lblCost.Text = Convert.ToString(Convert.ToDecimal(txt.Text) * Convert.ToDecimal(txtCost.Text));

        //    flag = "Autosave";
        //    SaveMaterialList(sender, e);
      
        //}

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
            SaveMaterialList(sender, e);
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
            // ResetControl();
        }
        #endregion

        protected void grdProdLines_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView lDr = (DataRowView)e.Row.DataItem;
                int lProdCatID = Convert.ToInt32(lDr["ProductCatID"].ToString());
                Boolean lDefVendorCat = Convert.ToBoolean(lDr["DefaultVendorForCategory"].ToString());
                Label lblVendorNames = (Label)e.Row.FindControl("lblVendorNames");
                LinkButton lnkAddVendors = (LinkButton)e.Row.FindControl("lnkAddVendors");
                CheckBox chkDefault = (CheckBox)e.Row.FindControl("chkDefault");
                chkDefault.Checked = false; //lDefVendorCat;

                if (e.Row.RowIndex == 0)
                {
                    chkDefault.Style.Add("display", "none");
                }
                else
                {
                    if (chkDefault.Checked)
                    {
                        lblVendorNames.Style.Add("display", "none");
                        lnkAddVendors.Style.Add("display", "none");
                        //lstVendorName.Style.Add("display", "none");
                    }
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadTheList" + lProdCatID, "SaveVendor(document.getElementById('" + lstVendorName.ClientID + "'));", true);
                }
            
                DataView lDvVendor = new DataView(VendorList, " ProductCategoryId=" + lProdCatID, "VendorName asc", DataViewRowState.CurrentRows);
                
                
                String lVendorIds = lDr["VendorIds"].ToString();

               /* if (lVendorIds.Split(',').Count() >2)
                {
                    lblVendorNames.Text = lVendorIds.Split(',').Count() + " vendors selected";
                }
                else
                {*/
                foreach (DataRow lRow in VendorList.Select("ProductCategoryId=" + lProdCatID))
                {
                    foreach (string lVendorId in lVendorIds.Split(','))
                    {
                        if (lRow["VendorID"].ToString() == lVendorId)
                        {
                            lblVendorNames.Text += "<input type='radio' " + ((lDr["VendorID"].ToString() == lVendorId.ToString()) ? "checked='checked'" : "") + " onclick='updateVendor(" + lDr["id"] + "," + lProdCatID + "," + lVendorId + ", this)'  id='rdo" + lDr["id"] + "' name='grp" + lDr["id"] + "' /> <label for='rdo" + lDr["id"] + "'> <a href='Procurement.aspx?vid="+lRow["VendorID"]+"' target='_blank'>" + lRow["VendorName"].ToString() + "</a></label> <a href='javascript:void(0);' onclick='ShowAttachQuotes(" + lProdCatID + "," + lVendorId + ")'>Attach Quote</a><br/>";
                        }
                    }
                }
                    lblVendorNames.Text = lblVendorNames.Text.TrimEnd(',');
               // }

            }
        }


        #region "Custom Material List Grid Functionality"

        /// <summary>
        /// This method creates a Skeleton record which enables website user to add more material.
        /// </summary>
        /// <returns></returns>
        private CustomMaterialList CreateSkeletonRecord()
        {
            CustomMaterialList cm1 = new CustomMaterialList();
            cm1.Id = 0;
            cm1.MaterialList = "";
            cm1.VendorCategoryId = 0;
            cm1.VendorCategoryName = "";
            cm1.VendorId = 0;
            cm1.VendorName = "";
            cm1.Amount = 0;
            cm1.ProductCatId = productTypeId;
            cm1.DocName = "";
            cm1.TempName = "";
            cm1.IsForemanPermission = "";
            cm1.IsSrSalemanPermissionF = "";
            cm1.IsAdminPermission = "";
            cm1.IsSrSalemanPermissionA = "";
            cm1.Status = JGConstant.CustomMaterialListStatus.Unchanged;
            cm1.ForemaneID = 0;
            cm1.ForemanFirstName = "";
            cm1.ForemanLastName = "";
            cm1.ForemanUserName = "";

            cm1.SrSaleManFID = 0;
            cm1.ForemanFirstName = "";
            cm1.ForemanLastName = "";
            cm1.ForemanUserName = "";

            cm1.SrSaleManAID = 0;
            cm1.ForemanFirstName = "";
            cm1.ForemanLastName = "";
            cm1.ForemanUserName = "";

            cm1.AdminID = 0;
            cm1.ForemanFirstName = "";
            cm1.ForemanLastName = "";
            cm1.ForemanUserName = "";

            return cm1;
        }

        /// <summary>
        /// This method enable access level for quote and purchasing request
        /// </summary>
        public void SetAccessLevel()
        {
            string EmailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
            int result = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
            if (result == 0) //if list doesn't exists
            {
                //btnSendEmailToVendors.Enabled = false;
            }
            else  //if list exists
            {
                if (EmailStatus == JGConstant.EMAIL_STATUS_NONE || EmailStatus == string.Empty)       //if no email was sent
                {
                    int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
                    if (permissionStatusCategories == 0)        //if no permissions were granted for categories
                    {
                       // btnSendEmailToVendors.Enabled = false;
                    }
                    else                //if permissions were granted for categories
                    {
                        btnSendEmailToVendors.Enabled = true;
                        btnSendEmailToVendors.Text = "Send Mail to All Vendors";
                    }
                }
                else if (EmailStatus == JGConstant.EMAIL_STATUS_VENDOR)    //if both mails are sent
                {
                    btnSendEmailToVendors.Enabled = true;
                    btnSendEmailToVendors.Text = "Resend Mail to All Vendors";
                }
                else        //if mails were sent to categories
                {
                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(jobId);//, productTypeId, estimateId);
                    btnSendEmailToVendors.Enabled = true;
                    btnSendEmailToVendors.Text = "Resend Mail to Vendors";
                    
                    
                    if (permissionStatus == 0)  //if permissions were not granted for vendors
                    {
                      //  btnSendEmailToVendors.Enabled = false;
                    }
                    else         //if permissions were granted for vendors
                    {
                       // btnSendEmailToVendors.Enabled = true;
                       // btnSendEmailToVendors.Text = "Resend Mail to Vendors";
                    }
                }
            }
        }

        private void InitialDataBind()
        {

            ProductDataset = UserBLL.Instance.GetAllProducts();
            ddlCategoryH.DataSource = ProductDataset;
            ddlCategoryH.DataTextField = "ProductName";
            ddlCategoryH.DataValueField = "ProductId";
            ddlCategoryH.DataBind();

            PageDataset = CustomBLL.Instance.GetCustomMaterialList(jobId.ToString(), customerId);
          
            ddlInstaller.DataSource = PageDataset.Tables[3];
            ddlInstaller.DataTextField = "QualifiedName";
            ddlInstaller.DataValueField = "Id";
            ddlInstaller.DataBind();

            rptInstaller.DataSource = PageDataset.Tables[4];
            rptInstaller.DataBind();

            VendorCategoryList = PageDataset.Tables[6];
            VendorList = PageDataset.Tables[7];
            MaterialListAttachment = PageDataset.Tables[8];

            rptVendorTotals.DataSource = PageDataset.Tables[9];
            rptVendorTotals.DataBind();

            if (PageDataset.Tables[1].Rows.Count <= 0) //#-Shabbir: If this is the new record.
            {
                CustomMaterialList cm = new CustomMaterialList();
                cm.ProductCatId = 1;
                cm.MaterialList = "";
                cm.Id = 0;
                cm.VendorName = "";
                cm.VendorEmail = "";
                cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
                bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);
                InitialDataBind();
                return;
            }
            List<CustomMaterialList> cmList = new List<CustomMaterialList>();
            if (PageDataset.Tables[1].Rows.Count > 0)
            {
                for (int j = 0; j < PageDataset.Tables[1].Rows.Count; j++)
                {
                    DataRow dr = PageDataset.Tables[1].Rows[j];
                    if (dr["RequestStatus"].ToString() != "1")
                    {
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

                        cm.ForemaneID = Convert.ToInt32(dr["foremanID"].ToString());
                        cm.ForemanFirstName = dr["foremanFirstName"].ToString();
                        cm.ForemanLastName = dr["foremanLastName"].ToString();
                        cm.ForemanUserName = dr["foremanUserName"].ToString();

                        cm.SrSaleManFID = Convert.ToInt32(dr["SLFID"].ToString());
                        cm.SrSaleManFFirstName = dr["SLFFirstName"].ToString();
                        cm.SrSaleManFLastName = dr["SLFLastName"].ToString();
                        cm.SrSaleManFUserName = dr["SLFUserName"].ToString();

                        cm.SrSaleManAID = Convert.ToInt32(dr["SLAID"].ToString());
                        cm.SrSaleManAFirstName = dr["SLAFirstName"].ToString();
                        cm.SrSaleManALastName = dr["SLALastName"].ToString();
                        cm.SrSaleManAUserName = dr["SLAUserName"].ToString();

                        cm.AdminID = Convert.ToInt32(dr["ADID"].ToString());
                        cm.AdminFirstName = dr["ADFirstName"].ToString();
                        cm.AdminLastName = dr["ADLastName"].ToString();
                        cm.AdminUserName = dr["ADUserName"].ToString();
                        cmList.Add(cm);
                        StaffID = Convert.ToInt32(dr["lastUpdatedByID"].ToString());
                        StaffName = dr["lastUpdatedByfirstname"].ToString().Trim() != "" ? (dr["lastUpdatedByfirstName"] + " " + dr["lastUpdatedBylastname"]) : dr["lastUpdatedByusername"].ToString();
                    }
                }
            }

            //#- Added By Shabbir Kanchwala
            if (PageDataset.Tables.Count > 2)
            {
                if (PageDataset.Tables[2].Rows.Count > 0)
                {
                    //Response.Redirect("Custom.aspx?ProductTypeId=" + Convert.ToInt16(hdnProductTypeId.Value) + "&ProductId=" + productId + "&CustomerId=" + customerId);
                    ElabJobID = jobId.Substring(0, 1) + "<a href='Customer_Profile.aspx?CustomerId=" + customerId + "'>" + jobId.Substring(1, jobId.IndexOf("-") - 1) + "</a>-<a href='Custom.aspx?ProductTypeId=" + PageDataset.Tables[2].Rows[0]["producttypeid"].ToString() + "&ProductId=" + PageDataset.Tables[2].Rows[0]["productid"].ToString() + "&CustomerId=" + customerId + "'>" + jobId.Substring(jobId.IndexOf("-") + 1) + "</a>";
                    CustomerName = PageDataset.Tables[2].Rows[0]["CustomerName"].ToString() + " " + PageDataset.Tables[2].Rows[0]["LastName"].ToString();
                }
            }
            for (int j = 0; j < PageDataset.Tables[0].Rows.Count; j++)
            {
                DataView lDvFilter = new DataView(PageDataset.Tables[1], "ProductCatID=" + PageDataset.Tables[0].Rows[j]["ProductCatID"].ToString() + " and RequestStatus=1", "id desc", DataViewRowState.None);
                if (lDvFilter.Table.Rows.Count > 0)
                {
                    PageDataset.Tables[0].Rows[j]["RequestStatus"] = "1";
                }
            }

            //cmList.Add(CreateSkeletonRecord());

            ViewState["CustomMaterialList"] = cmList;
            BindCustomMaterialList(cmList);
            SetAccessLevel();
            lstCustomMaterialList.DataSource = PageDataset.Tables[0];//PageDataset.Tables[0].Select("RequestStatus<>'1'");
            lstCustomMaterialList.DataBind();

            lstRequestedMaterial.DataSource = PageDataset.Tables[0].Select("RequestStatus='1'");
            lstRequestedMaterial.DataBind();

        }


        private void SaveMaterialList(object sender, EventArgs e)
        {
            //#-This line is not required. 
            string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);

            


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
                CustomBLL.Instance.UpdateProductTypeInMaterialList(lProdCat, Convert.ToInt32(lhdnCurrentProdCat.Value), jobId);
                InitialDataBind();
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
            DropDownCheckBoxes ddlVendorList = (DropDownCheckBoxes)r.FindControl("ddlVendorName");
            TextBox txtMaterialCost = (TextBox)r.FindControl("txtMaterialCost");
            DropDownList ddlExtent = (DropDownList)r.FindControl("ddlExtent");
            TextBox txtAmount = (TextBox)r.FindControl("txtAmount");

            HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
            HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
            HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
            HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
            HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
            HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");

            cm.ProductCatId = productTypeId;
            cm.Id = hdnMaterialListId.Value != "" ? Convert.ToInt16(hdnMaterialListId.Value) : 0;

            cm.Line = txtLine.Text;
            cm.JGSkuPartNo = txtSkuPartNo.Text;
            cm.MaterialList = txtDescription.Text;
            cm.Quantity = txtQTY.Text != "" ? Convert.ToInt32(txtQTY.Text) : 0;
            cm.UOM = txtUOM.Text;
            cm.MaterialCost = txtMaterialCost.Text != "" ? Convert.ToInt32(txtMaterialCost.Text) : 0;
            cm.extend = txtExtended.Text;

            string lVendorIDs = "";
            foreach (System.Web.UI.WebControls.ListItem item in ddlVendorList.Items)
            {
                if(item.Selected ==true ){
                    lVendorIDs += item.Value+",";
                }
            }
            cm.VendorIds = lVendorIDs.TrimEnd(',');

            if (status == "C") //mail was already sent to vendor categories
            {
                cm.IsAdminPermission = lnkAdminPermission.Enabled == true ? JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString() : JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                cm.IsSrSalemanPermissionA = lnkSrSalesmanPermissionA.Enabled == true ? JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString() : JGConstant.PERMISSION_STATUS_GRANTED.ToString();
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
                cm.IsForemanPermission = lnkForemanPermission.Enabled ? JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString() : JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                cm.IsSrSalemanPermissionF = lnkSrSalesmanPermissionF.Enabled ? JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString() : JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
            }
            
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);//,productTypeId,estimateId);
            
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

        protected void lstCustomMaterialList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList dldVendorCategory = (DropDownList)e.Item.FindControl("dldVendorCategory");
                RadioButton rdoManufacturer = (RadioButton)e.Item.FindControl("rdoManufacturer");
                RadioButton rdoWholeSaler = (RadioButton)e.Item.FindControl("rdoWholeSaler");

                Button btnSendEmailToVendorsForProd = (Button)e.Item.FindControl("btnSendEmailToVendorsForProd");
                Button btnSendPurchaseOrder = (Button)e.Item.FindControl("btnSendPurchaseOrder");

                DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
                ddlCategory.DataSource = ProductDataset;
                ddlCategory.DataTextField = "ProductName";
                ddlCategory.DataValueField = "ProductId";
                ddlCategory.DataBind();
                
                DataRowView lDrView = (DataRowView)e.Item.DataItem;
                int lProdCatID = Convert.ToInt32(lDrView["ProductCatID"]);
                if (lDrView["EmailStatus"].ToString() == "C")
                {
                    btnSendEmailToVendorsForProd.Text = "Resend Quote Request";
                   // btnSendPurchaseOrder.Visible = true;
                }
                else
                {
                    btnSendEmailToVendorsForProd.Text = "Request Quotes";
                    //btnSendPurchaseOrder.Visible = false;
                }

                DataList grdAttachment = (DataList)e.Item.FindControl("grdAttachment");
                grdAttachment.DataSource = new DataView(MaterialListAttachment, "ProductCatId=" + lProdCatID + " and AttachmentType=1", "Id desc", DataViewRowState.CurrentRows);
                grdAttachment.DataBind();

                DataList grdPurchaseOrder = (DataList)e.Item.FindControl("grdPurchaseOrder");
                grdPurchaseOrder.DataSource = new DataView(MaterialListAttachment, "ProductCatId=" + lProdCatID + " and AttachmentType=2", "Id desc", DataViewRowState.CurrentRows);
                grdPurchaseOrder.DataBind();

                Repeater rptVendorTotalsByProdCat = (Repeater)e.Item.FindControl("rptVendorTotalsByProdCat");
                rptVendorTotalsByProdCat.DataSource = new DataView(PageDataset.Tables[9], "ProductCatId=" + lProdCatID , "VendorName asc", DataViewRowState.CurrentRows);
                rptVendorTotalsByProdCat.DataBind();
               
                ddlCategory.SelectedValue = lProdCatID.ToString();
                GridView grdProdLines = (GridView)e.Item.FindControl("grdProdLines");
                DataView lDvMaterialList = new DataView(PageDataset.Tables[1], "ProductCatID=" + lProdCatID, "id asc", DataViewRowState.OriginalRows);

                DataView lDVVendorCatList = new DataView(VendorCategoryList, "ProductCategoryId=" + lProdCatID, "VendorCategoryNm asc", DataViewRowState.CurrentRows);
                dldVendorCategory.DataSource = lDVVendorCatList;
                dldVendorCategory.DataTextField = "VendorCategoryNm";
                dldVendorCategory.DataValueField = "VendorCategpryID";
                dldVendorCategory.DataBind();
                dldVendorCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "-1"));

                if (e.Item.DataItemIndex > 0)
                {
                    grdProdLines.ShowHeader = false;
                }
                grdProdLines.DataSource = lDvMaterialList;
                grdProdLines.DataBind();

                if (e.Item.DataItemIndex > 0) {
                    grdProdLines.ShowHeader = false;
                }
            }
        }
        protected void lnkAddLines_Click(object sender, EventArgs e)
        {

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
                bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);
                InitialDataBind();
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
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);
            InitialDataBind();
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
            cm.InstallerID = 0;
            cm.RequestStatus = 0;
            cm.extend = "0";
            cm.Quantity = 0;
            cm.MaterialCost = 0;
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);
            InitialDataBind();
        }
        protected void lnkDeleteLineItems_Click(object sender, EventArgs e)
        {
            LinkButton lnkDeleteLine = ((LinkButton)sender);
            GridView grdProdLines = ((GridView)((LinkButton)sender).Parent.Parent.Parent.Parent);
            if (grdProdLines.Rows.Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Deleting this record will delete the product category. This record cannot be deleted.');", true);
                InitialDataBind();
                return;
            }
            int lMaterialID = Convert.ToInt16(lnkDeleteLine.CommandArgument.ToString() == "" ? "0" : lnkDeleteLine.CommandArgument.ToString());
            if (lMaterialID > 0)
            {
                CustomBLL.Instance.DeleteCustomMaterialList(lMaterialID);
            }
            InitialDataBind();
            //bindMaterialList();
            
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
            bool result = CustomBLL.Instance.AddCustomMaterialList(cm, jobId);
            InitialDataBind();
        }
        protected void lnkDeleteProdCat_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = ((LinkButton)sender);
            if (PageDataset.Tables[0].Rows.Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ValidationMsg", "alert('Product category cannot be deleted. There should be at least one product category in the material list.')", true);
                InitialDataBind();
            }
            else
            {
                int lProdCatID = Convert.ToInt32(btnDelete.CommandArgument);
                CustomBLL.Instance.DeleteCustomMaterialListByProductCatID(lProdCatID);
                InitialDataBind();
            }
        }
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

        public bool SendEmailToVendors(string pVendorCategoryIds, StringBuilder pStrMaterialListTable)
        {
            bool emailStatus = true;
            string mailNotSendIds = string.Empty;
            string htmlBody = string.Empty;
            int emailCounter = 0;
            //to fetch all vendors within a category
            DataSet dsVendorsListByCategory = VendorBLL.Instance.GetVendors(pVendorCategoryIds);

            if (dsVendorsListByCategory != null)
            {
                //loop for all vendors within a category
                for (int counter = 0; counter < dsVendorsListByCategory.Tables[0].Rows.Count; counter++)
                {
                    DataRow dr = dsVendorsListByCategory.Tables[0].Rows[counter];
                    string mailId = dr["Email"].ToString();
                    string vendorName = dr["VendorName"].ToString();

                    MailMessage m = new MailMessage();
                    SmtpClient sc = new SmtpClient(ConfigurationManager.AppSettings["smtpHost"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].ToString()));

                    string userName = ConfigurationManager.AppSettings["VendorCategoryUserName"].ToString();
                    string password = ConfigurationManager.AppSettings["VendorCategoryPassword"].ToString();

                    m.From = new MailAddress(userName, "JGrove Construction");
                    m.To.Add(new MailAddress(mailId, vendorName));
                    m.Bcc.Add(new MailAddress("shabbir.kanchwala@straitapps.com", "Shabbir Kanchwala"));
                 
                    m.CC.Add(new MailAddress("jgrove.georgegrove@gmail.com", "Justin Grove"));
                    m.Subject = "J.M. Grove " + jobId + " quote request ";
                    m.IsBodyHtml = true;
                    DataSet dsEmailTemplate = fetchVendorCategoryEmailTemplate();

                    if (dsEmailTemplate != null)
                    {
                        m.Subject = dsEmailTemplate.Tables[0].Rows[0]["HTMLSubject"].ToString().Replace("c-xxxx", jobId);
                        string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                        StringBuilder tHeader = new StringBuilder();
                        tHeader.Append(templateHeader);
                        var replacedHeader = tHeader//.Replace("imgHeader", "<img src=cid:myImageHeader height=10% width=80%>")
                                                       .Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                    .Replace("lblJobId", jobId.ToString())
                                                    .Replace("lblCustomerId", "C" + customerId.ToString());
                        htmlBody = replacedHeader.ToString();
                        htmlBody += "</br></br></br>";
                        string templateBody = dsEmailTemplate.Tables[0].Rows[0][1].ToString();

                       // string materialList = cm.MaterialList;


                        StringBuilder tbody = new StringBuilder();
                        tbody.Append(templateBody);

                        var replacedBody = tbody.Replace("lblMaterialList",  pStrMaterialListTable.ToString());

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
                    m.Body = htmlBody; //"Email To: "+ mailId + htmlBody;

                    try
                    {
                       // DataSet lDSAttachedFiles = AdminBLL.Instance.GetHTMLTemplateAttachedFile(14); //Vendor Categories
                        for (int i = 0; i < dsEmailTemplate.Tables[1].Rows.Count; i++)
                        {
                            string sourceDir = Server.MapPath(dsEmailTemplate.Tables[1].Rows[i]["DocumentPath"].ToString());
                            if (File.Exists(sourceDir))
                            {
                                Attachment attachment = new Attachment(sourceDir);
                                attachment.Name = Path.GetFileName(sourceDir);
                                m.Attachments.Add(attachment);
                            }
                        }

                        for (int i = 0; i < MaterialListAttachment.Rows.Count; i++)
                        {
                            string sourceDir = Server.MapPath(MaterialListAttachment.Rows[i]["DocumentPath"].ToString());
                            if (File.Exists(sourceDir))
                            {
                                Attachment attachment = new Attachment(sourceDir);
                                attachment.Name = Path.GetFileName(sourceDir);
                                m.Attachments.Add(attachment);
                            }
                        }
                    }
                    catch (Exception ex) { }


                    NetworkCredential ntw = new System.Net.NetworkCredential(userName, password);
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = ntw;
                    
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"].ToString()); // runtime encrypt the SMTP communications using SSL
                    try
                    {
                        sc.Send(m);
                        emailCounter += 1;
                    }
                    catch (Exception ex)
                    {
                        emailStatus = false;
                        mailNotSendIds += mailId + " , ";
                        CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_NONE);//, productTypeId, estimateId);
                    }

                }
            }
            return emailStatus;
        }

        public bool SendOrderonEmails(string pVendorCategoryIds, StringBuilder pStrMaterialListTable, Int32 pProductCatID)
        {
            bool emailStatus = true;
            string mailNotSendIds = string.Empty;
            string htmlBody = string.Empty;
            int emailCounter = 0;
            //to fetch all vendors within a category
            DataSet dsVendorsListByCategory = VendorBLL.Instance.GetVendors(pVendorCategoryIds);

            if (dsVendorsListByCategory != null)
            {
                //loop for all vendors within a category
                for (int counter = 0; counter < dsVendorsListByCategory.Tables[0].Rows.Count; counter++)
                {
                    DataRow dr = dsVendorsListByCategory.Tables[0].Rows[counter];
                    string mailId = dr["Email"].ToString();
                    string vendorName = dr["VendorName"].ToString();

                    MailMessage m = new MailMessage();
                    SmtpClient sc = new SmtpClient(ConfigurationManager.AppSettings["smtpHost"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].ToString()));

                    string userName = ConfigurationManager.AppSettings["VendorCategoryUserName"].ToString();
                    string password = ConfigurationManager.AppSettings["VendorCategoryPassword"].ToString();

                    m.From = new MailAddress(userName, "JGrove Construction");
                    //m.To.Add(new MailAddress(mailId, vendorName));
                    m.To.Add(new MailAddress("shabbir.kanchwala@straitapps.com", "Shabbir Kanchwala"));

                    //m.To.Add(new MailAddress("skanchwala@4qlearning.com", "Shabbir Kanchwala"));
                    //m.To.Add(new MailAddress("skanchwala@mosaic-network.com", "Shabbir Kanchwala"));
                    //m.To.Add(new MailAddress("shabbirk@live.com", "Shabbir Kanchwala"));

                    m.CC.Add(new MailAddress("jgrove.georgegrove@gmail.com", "Justin Grove"));
                    m.Subject = "J.M. Grove " + jobId + " quote request ";
                    m.IsBodyHtml = true;
                    DataSet dsEmailTemplate = fetchVendorEmailTemplate();
                    DataSet lDsJobInformation = AdminBLL.Instance.GetJobInformation(jobId, pProductCatID, Convert.ToInt32(dr["VendorID"].ToString()));
                    if (dsEmailTemplate != null)
                    {
                        m.Subject = dsEmailTemplate.Tables[0].Rows[0]["HTMLSubject"].ToString().Replace("c-xxxx", jobId);
                        string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                        StringBuilder tHeader = new StringBuilder();
                        tHeader.Append(templateHeader);
                        var replacedHeader = tHeader//.Replace("imgHeader", "<img src=cid:myImageHeader height=10% width=80%>")
                                                       .Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                    .Replace("lblJobId", jobId.ToString())
                                                    .Replace("lblCustomerId", "C" + customerId.ToString());
                        htmlBody = replacedHeader.ToString();
                        htmlBody += "</br></br></br>";
                        string templateBody = dsEmailTemplate.Tables[0].Rows[0][1].ToString();

                        // string materialList = cm.MaterialList;


                        StringBuilder tbody = new StringBuilder();
                        tbody.Append(templateBody);

                        var replacedBody = tbody.Replace("lblMaterialList", pStrMaterialListTable.ToString());

                        htmlBody += replacedBody.ToString();

                        htmlBody += "<br/>";
                        htmlBody += "<b>Delivery Type:</b> " + lDsJobInformation.Tables[0].Rows[0]["DeliveryMethod"].ToString() + "<br/>";
                        htmlBody += "<b>Delivery Address:</b> " + lDsJobInformation.Tables[0].Rows[0]["DeliverySite"].ToString() + ",<br/>" + lDsJobInformation.Tables[0].Rows[0]["City"].ToString() + ", " + lDsJobInformation.Tables[0].Rows[0]["State"].ToString()  + (lDsJobInformation.Tables[0].Rows[0]["ZipCode"].ToString() == "" ? "" : "(" + lDsJobInformation.Tables[0].Rows[0]["ZipCode"].ToString() + ")");
                        htmlBody += "</br></br>";
                        htmlBody += "<b>Material Storage:</b> " + lDsJobInformation.Tables[2].Rows[0]["MaterialStorage"].ToString();
                        htmlBody += "<br/><br/>";
                        htmlBody += "<b>Primary Delivery Contact:</b> " + lDsJobInformation.Tables[1].Rows[0]["CustomerName"].ToString() + " (" + lDsJobInformation.Tables[1].Rows[0]["CellPh"].ToString()+")";
                        htmlBody += "<br/><br/><br/>";


                        string templateFooter = dsEmailTemplate.Tables[0].Rows[0][2].ToString();
                        StringBuilder tFooter = new StringBuilder();
                        tFooter.Append(templateFooter);
                        var replacedFooter = tFooter.Replace("src=\"../img/JG-Logo-white.gif\"", "src=cid:myImageLogo")
                                                   .Replace("src=\"../img/Email footer.png\"", "src=cid:myImageFooter");
                        htmlBody += replacedFooter.ToString();
                    }
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
                    m.Body = "TO: "+mailId +"<br/>"+ htmlBody; //"Email To: "+ mailId + htmlBody;

                    try
                    {
                        DataSet lDSAttachedFiles = AdminBLL.Instance.GetHTMLTemplateAttachedFile(14); //Vendor Categories
                        for (int i = 0; i < lDSAttachedFiles.Tables[0].Rows.Count; i++)
                        {
                            string sourceDir = Server.MapPath(lDSAttachedFiles.Tables[0].Rows[i]["DocumentPath"].ToString());
                            if (File.Exists(sourceDir))
                            {
                                Attachment attachment = new Attachment(sourceDir);
                                attachment.Name = Path.GetFileName(sourceDir);
                                m.Attachments.Add(attachment);
                            }
                        }

                        for (int i = 0; i < MaterialListAttachment.Rows.Count; i++)
                        {
                            string sourceDir = Server.MapPath(MaterialListAttachment.Rows[i]["DocumentPath"].ToString());
                            if (File.Exists(sourceDir))
                            {
                                Attachment attachment = new Attachment(sourceDir);
                                attachment.Name = Path.GetFileName(sourceDir);
                                m.Attachments.Add(attachment);
                            }
                        }
                    }
                    catch (Exception ex) { }


                    NetworkCredential ntw = new System.Net.NetworkCredential(userName, password);
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = ntw;

                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"].ToString()); // runtime encrypt the SMTP communications using SSL
                    try
                    {
                        sc.Send(m);
                        emailCounter += 1;
                    }
                    catch (Exception ex)
                    {
                        emailStatus = false;
                        mailNotSendIds += mailId + " , ";
                        CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_NONE);//, productTypeId, estimateId);
                    }

                }
            }
            return emailStatus;
        }


        protected void btnSendEmailToVendors_Click(object sender, EventArgs e)
        {
            try
            {
                PageDataset = CustomBLL.Instance.GetCustomMaterialList(jobId.ToString(), customerId);
                string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);

                for (int i = 0; i < PageDataset.Tables[0].Rows.Count; i++)
                {
                    StringBuilder lStrbHTMLTable = new StringBuilder();
                    lStrbHTMLTable.Append("<table  rules='all' style='width:80%;margin:auto auto;border:solid 1px;border-collapse:collapse;' cellpadding='0' cellspacing='0'>");
                    lStrbHTMLTable.Append("<tr>");
                    lStrbHTMLTable.Append("<th>#</th>");
                   // lStrbHTMLTable.Append("<th>JG SKU - Vendor Part #</th>");
                    lStrbHTMLTable.Append("<th>Material</th>");
                    lStrbHTMLTable.Append("<th>Quantity</th>");
                    lStrbHTMLTable.Append("<th>UOM</th>");
                    lStrbHTMLTable.Append("<th>Cost</th>");
                    lStrbHTMLTable.Append("<th>Extended</th>");
                    lStrbHTMLTable.Append("</tr>");

                    DataView lDvMaterialList = new DataView(PageDataset.Tables[1], "ProductCatID=" + PageDataset.Tables[0].Rows[i]["ProductCatID"].ToString(), "id asc", DataViewRowState.OriginalRows);
                    String lVendorIds = "";
                    for (int x = 0; x < lDvMaterialList.Table.Rows.Count; x++)
                    {
                        lStrbHTMLTable.Append("<tr>");
                        lStrbHTMLTable.Append("<td>" + (x + 1) + "</td>");
                       // lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["MaterialList"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["MaterialList"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["Quantity"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["Quantity"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["UOM"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["UOM"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["MaterialCost"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["MaterialCost"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["extend"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["extend"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("</tr>");
                        lVendorIds = lDvMaterialList.Table.Rows[x]["vendorids"].ToString().Trim() + ",";
                    }

                    lStrbHTMLTable.Append("</table>");
                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        bool emailStatusVendorCategory = SendEmailToVendors(lVendorIds.TrimEnd(','), lStrbHTMLTable); //sendEmailToVendorCategories(cmList);
                        
                        if (emailStatusVendorCategory == true)
                        {
                            bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
                            UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDORCATEGORIES.ToString());
                            btnSendEmailToVendors.Text = "Resend Mail to Vendors";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote request send to selected vendors');window.location = window.location.href;", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Foreman and Sr. Salesman permission');", true);
                    }
                    Response.Redirect(Request.Url.AbsoluteUri);
                    return;
                }

                //string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
                List<CustomMaterialList> cmList = new List<CustomMaterialList>();
                foreach (GridViewRow r in grdcustom_material_list.Rows)
                {
                    CustomMaterialList cm = new CustomMaterialList();
                    DropDownList ddlVendorCategory = (DropDownList)r.FindControl("ddlVendorCategory");
                    cm.VendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
                    TextBox txtMateriallist = (TextBox)r.FindControl("txtMateriallist");
                    HiddenField hdnMaterialListId = (HiddenField)r.FindControl("hdnMaterialListId");
                    HiddenField hdnEmailStatus = (HiddenField)r.FindControl("hdnEmailStatus");
                    HiddenField hdnForemanPermission = (HiddenField)r.FindControl("hdnForemanPermission");
                    HiddenField hdnSrSalesmanPermissionF = (HiddenField)r.FindControl("hdnSrSalesmanPermissionF");
                    HiddenField hdnAdminPermission = (HiddenField)r.FindControl("hdnAdminPermission");
                    HiddenField hdnSrSalesmanPermissionA = (HiddenField)r.FindControl("hdnSrSalesmanPermissionA");
                    cm.ProductCatId = productTypeId;
                    if (txtMateriallist.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill Material List(s).');", true);
                    }
                    else
                    {
                        cm.MaterialList = txtMateriallist.Text;
                    }

                    if (hdnMaterialListId.Value != "")
                    {
                        cm.Id = Convert.ToInt16(hdnMaterialListId.Value);
                    }
                    else
                    {
                        cm.Id = 0;
                    }
                    DropDownList ddlVendorName = (DropDownList)r.FindControl("ddlVendorName");
                    TextBox txtAmount = (TextBox)r.FindControl("txtAmount");

                    if (status == "C") //mail was already sent to vendor categories
                    {
                        if (ddlVendorName.SelectedItem.Text == "Select")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select vendor name.');", true);
                            //return;
                        }
                        else
                        {
                            cm.VendorName = ddlVendorName.SelectedItem.Text;
                            cm.VendorId = Convert.ToInt16(ddlVendorName.SelectedValue);

                            DataSet ds = VendorBLL.Instance.getVendorEmailId(ddlVendorName.SelectedItem.Text);
                            cm.VendorEmail = ds.Tables[0].Rows[0][0].ToString();
                        }

                        if (txtAmount.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter amount.');", true);
                            return;
                        }
                        else
                        {
                            cm.Amount = Convert.ToDecimal(txtAmount.Text);
                        }
                        if (lnkAdminPermission.Enabled == true)
                        {
                            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }
                        if (lnkSrSalesmanPermissionA.Enabled == true)
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
                    }
                    else // mail was not sent to vendor categories
                    {
                        cm.VendorName = "";
                        cm.VendorEmail = "";
                        cm.IsAdminPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        cm.IsSrSalemanPermissionA = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        if (lnkForemanPermission.Enabled == true)
                        {
                            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsForemanPermission = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }
                        if (lnkSrSalesmanPermissionF.Enabled == true)
                        {
                            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_NOTGRANTED.ToString();
                        }
                        else
                        {
                            cm.IsSrSalemanPermissionF = JGConstant.PERMISSION_STATUS_GRANTED.ToString();
                        }

                        cm.EmailStatus = JGConstant.EMAIL_STATUS_NONE;
                    }
                    cmList.Add(cm);
                }
                if (btnSendMail.Text == "Save") //#- Commented By Shabbir because we have added code on Add Button. Though the permission issue is remaining.
                {
                    /* int existsList = CustomBLL.Instance.WhetherCustomMaterialListExists(jobId);//, productTypeId, estimateId);
                     if (existsList == 0)
                     {
                         saveCustom_MaterialList(cmList);
                     }
                     else
                     {
                         EnableVendorNameAndAmount();
                         int permissionStatusCategories = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
                         if (permissionStatusCategories == 0)
                         {
                             saveCustom_MaterialList(cmList);
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
                             return;
                         }
                         else
                         {
                             int permissionStatusVendors = CustomBLL.Instance.CheckPermissionsForVendors(jobId);//, productTypeId, estimateId);
                             if (permissionStatusVendors == 0)
                             {
                                 saveCustom_MaterialList(cmList);
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);
                                 return;
                             }
                             else
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('After giving permissions lists cann't be changed');", true);
                                 return;
                             }
                         }
                     }
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('All lists are saved.');", true);*/
                }

                else if (btnSendMail.Text == "Send Mail To Vendor Category(s)")
                {

                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForCategories(jobId);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        bool emailStatusVendorCategory = sendEmailToVendorCategories(cmList);

                        if (emailStatusVendorCategory == true)
                        {
                            bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
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
                    int permissionStatus = CustomBLL.Instance.CheckPermissionsForVendors(jobId);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        int statusQuotes = CustomBLL.Instance.WhetherVendorQuotesExists(jobId);
                        if (statusQuotes == 1)
                        {

                            bool emailStatusVendor = sendEmailToVendors(cmList);
                            if (emailStatusVendor == true)
                            {
                                bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDOR);//, productTypeId, estimateId);
                                UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDOR.ToString());
                                btnSendMail.Text = "Save";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Email is sent to all vendors');", true);
                                setControlsAfterSendingBothMails();
                                string fileName = GenerateWorkOrder();
                                string url = ConfigurationManager.AppSettings["URL"].ToString();
                                ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + fileName + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
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
        protected void btnAddInstaller_Click(object sender, EventArgs e)
        {
            CustomBLL.Instance.AddInstallerToMaterialList(jobId, Convert.ToInt32(ddlInstaller.SelectedValue));
            InitialDataBind();
        }
        protected void rptInstaller_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrStatus = (Literal)e.Item.FindControl("ltrStatus");
                DataRowView lDrInstallerRow = (DataRowView)e.Item.DataItem;
                if (lDrInstallerRow["UpdatedDateTime"] != DBNull.Value && lDrInstallerRow["UpdatedDateTime"] != null)
                {
                    ltrStatus.Text = Convert.ToDateTime(lDrInstallerRow["UpdatedDateTime"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
                }
                else
                {
                    ltrStatus.Text = "<input type='password' id='txtInstPwd" + lDrInstallerRow["InstallerID"].ToString() + "' onblur='AllowInstaller(\"" + lDrInstallerRow["InstallerID"].ToString() + "\", this)' />";
                }
            }
        }
        protected void rptInstaller_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteInstaller")
            {
                CustomBLL.Instance.RemoveInstallerFromMaterialList(Convert.ToInt32(e.CommandArgument));
                InitialDataBind();
            }
        }

        private void FilterVendors(object sender, EventArgs e)
        {



            GridViewRow r = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            if (sender.GetType().Equals(typeof(DropDownList)))
            {
                r = ((GridViewRow)((DropDownList)sender).Parent.Parent);
                return;
            }
            else if (sender.GetType().Equals(typeof(RadioButton)))
            {
              //  r = ((GridViewRow)((RadioButton)sender).Parent.Parent.Parent.Parent);
                return;
            }
            else if (sender.GetType().Equals(typeof(CheckBox)))
            {
                r = ((GridViewRow)((CheckBox)sender).Parent.Parent);
            }
            else
            {
                return;
            }

            CustomMaterialList cm = new CustomMaterialList();
            CheckBox chkApplyFilter = (CheckBox)r.FindControl("chkApplyFilter");
            RadioButton rdoManufacturer = (RadioButton)r.Parent.Parent.Parent.FindControl("rdoManufacturer");
            RadioButton rdoWholeSaler = (RadioButton)r.Parent.Parent.Parent.FindControl("rdoWholeSaler");
            DropDownList ddlVendorCategory = (DropDownList)r.Parent.Parent.Parent.FindControl("dldVendorCategory");
            DropDownCheckBoxes ddlVendorName = (DropDownCheckBoxes)r.FindControl("ddlVendorName");
            ListBox lstVendor = (ListBox)r.FindControl("lstVendor");
            UpdatePanel updVend = (UpdatePanel)r.FindControl("updVend");
            HiddenField hdnProductCatID = (HiddenField)r.FindControl("hdnProductCatID");

            int lProdCatID = Convert.ToInt32(hdnProductCatID.Value.ToString());
            
            //DataView lDVVendorCatList = new DataView(VendorCategoryList, "ProductCategoryId=" + lProdCatID, "VendorCategoryNm asc", DataViewRowState.CurrentRows);
            //dldVendorCategory.DataSource = lDVVendorCatList;
            //dldVendorCategory.DataTextField = "VendorCategoryNm";
            //dldVendorCategory.DataValueField = "VendorCategpryID";
            //dldVendorCategory.DataBind();

            if (chkApplyFilter.Checked)
            {
                DataView lDvVendor = new DataView(VendorList, "IsManufacturer=" + (rdoManufacturer.Checked ? 1 : 0) + " and IsRetail_Wholesale=" + (rdoWholeSaler.Checked ? 1 : 0) + (ddlVendorCategory.SelectedValue != "-1" ? " and VendorCategpryId=" + ddlVendorCategory.SelectedValue : "") + " and ProductCategoryId=" + lProdCatID, "VendorName asc", DataViewRowState.CurrentRows);
                lstVendor.DataSource = lDvVendor;
                lstVendor.DataTextField = "VendorName";
                lstVendor.DataValueField = "VendorId";
                lstVendor.DataBind();
            }
            else
            {
                DataView lDvVendor = new DataView(VendorList, "ProductCategoryId=" + lProdCatID, "VendorName asc", DataViewRowState.CurrentRows);
                lstVendor.DataSource = lDvVendor;
                lstVendor.DataTextField = "VendorName";
                lstVendor.DataValueField = "VendorId";
                lstVendor.DataBind();
            }
            //ScriptManager.RegisterStartupScript(updVend, updVend.GetType(), "DropDownInit", "window." + ddlVendorName.ClientID + " = new DropDownScript('" + ddlVendorName.ClientID + "','_dv','_sl'," + ddlVendorName.ClientID + ", false, true, true);window."+ddlVendorName.ClientID+".init(); alert('hi');alert(window." + ddlVendorName.ClientID + ");", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "DropDownInit", "window." + ddlVendorName.ClientID + " = new DropDownScript('" + ddlVendorName.ClientID + "','_dv','_sl'," + ddlVendorName.ClientID + ", false, true, true);window." + ddlVendorName.ClientID + ".init(); alert('hi');alert(window." + ddlVendorName.ClientID + ");", true);
        }
        protected void dldVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVendors(sender, e);
        }

        protected void rdoManufacturer_CheckedChanged(object sender, EventArgs e)
        {
            FilterVendors(sender, e);
        }

        protected void rdoWholeSaler_CheckedChanged(object sender, EventArgs e)
        {
            FilterVendors(sender, e);
        }
        protected void chkApplyFilter_CheckedChanged(object sender, EventArgs e)
        {
            FilterVendors(sender, e);
        }
     

        protected void btnSendEmailToVendorsForProd_Click(object sender, EventArgs e)
        {
            try
            {
                //PageDataset = CustomBLL.Instance.GetCustomMaterialList(jobId.ToString(), customerId);
                //string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
                Button btnSendVendorEmails = (Button )sender;
                Int32 lProductCatID = Convert.ToInt32(Convert.ToInt32(btnSendVendorEmails.CommandArgument));
                   

                    DataView lDvMaterialList = new DataView(PageDataset.Tables[1]);
                    lDvMaterialList = new DataView(PageDataset.Tables[1], "ProductCatID=" + lProductCatID, "id asc", DataViewRowState.ModifiedCurrent);
                    lDvMaterialList.Table = PageDataset.Tables[1];
                    lDvMaterialList.RowFilter = "ProductCatID=" + lProductCatID;
                    lDvMaterialList.RowStateFilter = DataViewRowState.ModifiedCurrent;
                    String lVendorIds = "";
                  
                    foreach (DataRow lRow in  PageDataset.Tables[1].Select("ProductCatID=" + lProductCatID))
                    {
                        foreach (string lVend in lRow["vendorids"].ToString().Trim().Split(','))
                        {
                            if (!lVendorIds.Contains(lVend + ","))
                            {
                                lVendorIds += lVend + ",";
                            }
                        }
                    }
                lVendorIds = lVendorIds.TrimEnd(',');
                    foreach (string vendor in lVendorIds.Split(','))
                    {
                        int y = 1;
                        StringBuilder lStrbHTMLTable = new StringBuilder();
                        lStrbHTMLTable.Append("<table  rules='all' style='width:80%;margin:auto auto;border:solid 1px;border-collapse:collapse;' cellpadding='0' cellspacing='0'>");
                        lStrbHTMLTable.Append("<tr>");
                        lStrbHTMLTable.Append("<th>#</th>");
                        // lStrbHTMLTable.Append("<th>JG SKU - Vendor Part #</th>");
                        lStrbHTMLTable.Append("<th>Material</th>");
                        lStrbHTMLTable.Append("<th>Quantity</th>");
                        lStrbHTMLTable.Append("<th>UOM</th>");
                        lStrbHTMLTable.Append("<th>Cost</th>");
                        lStrbHTMLTable.Append("<th>Extended</th>");
                        lStrbHTMLTable.Append("</tr>");
                        foreach (DataRow lRowC in PageDataset.Tables[1].Select("ProductCatID=" + lProductCatID + " AND VendorIds like '%" + vendor + "%'"))
                        {
                            lStrbHTMLTable.Append("<tr>");
                            lStrbHTMLTable.Append("<td>" + (y) + "</td>");
                            // lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim()) + "</td>");
                            lStrbHTMLTable.Append("<td>" + (lRowC["MaterialList"].ToString().Trim() == "" ? "-" : lRowC["MaterialList"].ToString().Trim()) + "</td>");
                            lStrbHTMLTable.Append("<td>" + (lRowC["Quantity"].ToString().Trim() == "" ? "-" : lRowC["Quantity"].ToString().Trim()) + "</td>");
                            lStrbHTMLTable.Append("<td>" + (lRowC["UOM"].ToString().Trim() == "" ? "-" : lRowC["UOM"].ToString().Trim()) + "</td>");
                            lStrbHTMLTable.Append("<td>" + (lRowC["MaterialCost"].ToString().Trim() == "" ? "-" : lRowC["MaterialCost"].ToString().Trim()) + "</td>");
                            lStrbHTMLTable.Append("<td>" + (lRowC["extend"].ToString().Trim() == "" ? "-" : lRowC["extend"].ToString().Trim()) + "</td>");
                            lStrbHTMLTable.Append("</tr>");
                            y += 1;
                        }
                        lStrbHTMLTable.Append("</table>");
                        if (y == 1)
                        {
                            continue;
                        }
                        int permissionStatus = 1; //CustomBLL.Instance.CheckPermissionsForCategories(jobId, lProductCatID);//, productTypeId, estimateId);
                        if (permissionStatus == 1)
                        {
                            bool emailStatusVendorCategory = SendEmailToVendors(vendor.TrimEnd(','), lStrbHTMLTable); //sendEmailToVendorCategories(cmList);

                            if (emailStatusVendorCategory == true)
                            {
                                CustomBLL.Instance.UpdateStatusOfJobProdCat(jobId, lProductCatID, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);
                               // bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, JGConstant.EMAIL_STATUS_VENDORCATEGORIES);//, productTypeId, estimateId);
                                UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDORCATEGORIES.ToString());
                                btnSendEmailToVendors.Text = "Resend Mail to Vendors";
                                

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Foreman and Sr. Salesman permission');", true);
                            return;
                        }
                    }
                        //for (int x = 0; x < lDvMaterialList.ToTable().Rows.Count; x++)
                        //{
                        //    lStrbHTMLTable.Append("<tr>");
                        //    lStrbHTMLTable.Append("<td>" + (x + 1) + "</td>");
                        //    // lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim()) + "</td>");
                        //    lStrbHTMLTable.Append("<td>" + (lDvMaterialList.ToTable().Rows[x]["MaterialList"].ToString().Trim() == "" ? "-" : lDvMaterialList.ToTable().Rows[x]["MaterialList"].ToString().Trim()) + "</td>");
                        //    lStrbHTMLTable.Append("<td>" + (lDvMaterialList.ToTable().Rows[x]["Quantity"].ToString().Trim() == "" ? "-" : lDvMaterialList.ToTable().Rows[x]["Quantity"].ToString().Trim()) + "</td>");
                        //    lStrbHTMLTable.Append("<td>" + (lDvMaterialList.ToTable().Rows[x]["UOM"].ToString().Trim() == "" ? "-" : lDvMaterialList.ToTable().Rows[x]["UOM"].ToString().Trim()) + "</td>");
                        //    lStrbHTMLTable.Append("<td>" + (lDvMaterialList.ToTable().Rows[x]["MaterialCost"].ToString().Trim() == "" ? "-" : lDvMaterialList.ToTable().Rows[x]["MaterialCost"].ToString().Trim()) + "</td>");
                        //    lStrbHTMLTable.Append("<td>" + (lDvMaterialList.ToTable().Rows[x]["extend"].ToString().Trim() == "" ? "-" : lDvMaterialList.ToTable().Rows[x]["extend"].ToString().Trim()) + "</td>");
                        //    lStrbHTMLTable.Append("</tr>");
                        //    lVendorIds = lDvMaterialList.ToTable().Rows[x]["vendorids"].ToString().Trim() + ",";
                        //}


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote request send to selected vendors');window.location = window.location.href;", true);
                    Response.Redirect(Request.Url.AbsoluteUri);
                    return;
                
            }
            catch (Exception)
            {

            }
        }

        protected void btnAttachFile_Click(object sender, EventArgs e)
        {
            Button btnAttachMaterialList = (Button)sender;
            FileUpload flMaterialList = (FileUpload)btnAttachMaterialList.Parent.Parent.FindControl("flMaterialList");
            DropDownList ddlCategory = (DropDownList)btnAttachMaterialList.Parent.Parent.FindControl("ddlCategory");

            List<CustomerDocument> custDocs = new List<CustomerDocument>();
            int intFileSize = flMaterialList.PostedFile.ContentLength;

            if (flMaterialList.HasFile)
            {
                if (flMaterialList.PostedFile.FileName != "")
                {
                    if (Request.Files.Count > 0)
                    {
                        int i = 0;
                        //for (int i = 0; i < attachments.Count; i++)
                        //{

                            
                            if (flMaterialList.PostedFile.ContentLength > 0 && !String.IsNullOrEmpty(flMaterialList.PostedFile.FileName))
                            {
                                CustomerDocument cbc = new CustomerDocument();
                                if (File.Exists(Server.MapPath("../CustomerDocs/MaterialListAttachment/") + flMaterialList.PostedFile.FileName) == true)
                                {
                                    File.Delete(Server.MapPath("../CustomerDocs/MaterialListAttachment/") + flMaterialList.PostedFile.FileName);
                                    flMaterialList.PostedFile.SaveAs(Server.MapPath("../CustomerDocs/MaterialListAttachment/") + flMaterialList.PostedFile.FileName);
                                }
                                else
                                {
                                    flMaterialList.PostedFile.SaveAs(Server.MapPath("../CustomerDocs/MaterialListAttachment/") + flMaterialList.PostedFile.FileName);
                                }
                                string fPath;
                                fPath = ("../CustomerDocs/MaterialListAttachment/") + flMaterialList.PostedFile.FileName;
                                cbc.DocumentName = flMaterialList.PostedFile.FileName;
                                cbc.DocumentPath = fPath;
                                custDocs.Add(cbc);
                            }
                        //}
                    }
                }
            }
            string result = AdminBLL.Instance.AddMaterialListAttachment(jobId, Convert.ToInt32(ddlCategory.SelectedValue), custDocs,1,0); //#- 1 means Email Attachment
            InitialDataBind();
            if (result!="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Material List attachment added successfully');", true);
            }
            else if (result == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('File with the same name already exists in the system. Please rename the file and upload again.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Internal Error Occurred');", true);
            }
        }

        protected void lnkDeleteMatLisAttc_Click(object sender, EventArgs e)
        {
            LinkButton lnkDeleteMatLisAttc = (LinkButton)sender;
            int lAttachmentID = Convert.ToInt32(lnkDeleteMatLisAttc.CommandArgument);
            DataSet lDsAttachment = AdminBLL.Instance.DeleteMaterialListlAttachment(lAttachmentID);
            string fileName = (Server.MapPath(lDsAttachment.Tables[0].Rows[0]["DocumentPath"].ToString()));
            //bool res = AdminBLL.Instance.DeleteCustomerAttachment(fileName);
            if (fileName != "")
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);

        }
       

        protected void drpVendorCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpVendorSubCat.SelectedIndex = -1;
            BindVendorSubCatByVendorCat(drpVendorCat.SelectedValue.ToString());
            string ManufacturerType = GetManufacturerType();

            if (drpVendorCat.SelectedValue.ToString() != "Select")
            {
                FilterVendors(drpVendorCat.SelectedValue.ToString(), "VendorCategory", ManufacturerType, "");
            }
            else
            {
                FilterVendorByProductCategory();
            }
            mpVendorCat.Show();
        }

        protected void drpVendorSubCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ManufacturerType = GetManufacturerType();
            if (drpVendorSubCat.SelectedValue.ToString() != "Select")
                FilterVendors(drpVendorSubCat.SelectedValue.ToString(), "VendorSubCategory", ManufacturerType, drpVendorCat.SelectedValue.ToString());
            else if (drpVendorCat.SelectedValue.ToString() != "Select")
                FilterVendors(drpVendorCat.SelectedValue.ToString(), "VendorCategory", ManufacturerType, "");
            else
                FilterVendorByProductCategory();
            mpVendorCat.Show();
        }

        protected void optManf_CheckedChanged(object sender, EventArgs e)
        {
            BindVendorByProdCat(ProductCategoryID.ToString());
            drpVendorCat.SelectedIndex = -1;
            mpVendorCat.Show();
        }

        protected void optWholeSaler_CheckedChanged(object sender, EventArgs e)
        {
            BindVendorByProdCat(ProductCategoryID.ToString());
            drpVendorCat.SelectedIndex = -1;
            mpVendorCat.Show();
        }
        protected void lnkAddVendors_Click(object sender, EventArgs e)
        {
            LinkButton lnkAddVendor = (LinkButton)sender;
            HiddenField hdnProductCat = (HiddenField)lnkAddVendor.Parent.Parent.FindControl("hdnProductCatID");
            HiddenField hdnVendorIDs = (HiddenField)lnkAddVendor.Parent.Parent.FindControl("hdnVendorIDs");
            
            ProductCategoryID = Convert.ToInt32(hdnProductCat.Value);
            ProductLineID = Convert.ToInt32(lnkAddVendor.CommandArgument);
            VendorIDs = hdnVendorIDs.Value;
            drpVendorCat.SelectedIndex = -1;
            drpVendorSubCat.SelectedIndex = -1;
            BindVendorByProdCat(ProductCategoryID.ToString());
            FilterVendorByProductCategory();
            mpVendorCat.Show();
        }

        protected int ProductCategoryID
        {
            get { return ViewState["ProductCategoryID"] != null ? Convert.ToInt32(ViewState["ProductCategoryID"].ToString()) : 0; }
            set { ViewState["ProductCategoryID"] = value; }
        }
        protected int ProductLineID
        {
            get { return ViewState["ProductLineID"] != null ? Convert.ToInt32(ViewState["ProductLineID"].ToString()) : 0; }
            set { ViewState["ProductLineID"] = value; }
        }
        protected string VendorIDs
        {
            get { return ViewState["VendorIDs"] != null ? Convert.ToString(ViewState["VendorIDs"].ToString()) : ""; }
            set { ViewState["VendorIDs"] = value; }
        }
        #region "Private Methods"
        public string GetManufacturerType()
        {
            string MType = "";
            if (optWholeSaler.Checked)
                MType = optWholeSaler.Text;
            else if (optManf.Checked)
                MType = optManf.Text;
            return MType;
        }
        public void BindVendorSubCatByVendorCat(string VendorCatId)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.GetVendorSubCategory(VendorCatId, optWholeSaler.Checked, optManf.Checked);
            drpVendorSubCat.DataSource = ds;
            drpVendorSubCat.DataTextField = "VendorSubCategoryName";
            drpVendorSubCat.DataValueField = "VendorSubCategoryId";
            drpVendorSubCat.DataBind();
            drpVendorSubCat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }
        public void FilterVendors(string FilterParams, string FilterBy, string ManufacturerType, string VendorCategoryId)
        {
            lstVendors.DataSource = null;
            lstVendors.DataBind();
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.GetVendorList(FilterParams, FilterBy, ManufacturerType, VendorCategoryId);
            if (ds != null)
            {
                lstVendors.DataSource = ds;
                lstVendors.DataTextField = "VendorName";
                lstVendors.DataValueField = "VendorID";
                lstVendors.DataBind();

                foreach(string Vendor in VendorIDs.Split(',')){
                    foreach (System.Web.UI.WebControls.ListItem item in lstVendors.Items)
                    {
                        if (item.Value == Vendor)
                        {
                            item.Selected = true;
                            lblGotQuotesFrom.Text += "<a href='Procurement.aspx?vid="+item.Value+"' target='_blank'>" +item.Text + "</a>, ";
                        }
                    }
                }
                lblGotQuotesFrom.Text = lblGotQuotesFrom.Text.TrimEnd(',');
                //foreach (DataRow lRow in ds.Tables[0].Rows)
                //{
                //    System.Web.UI.WebControls.ListItem lstVendor = new System.Web.UI.WebControls.ListItem(lRow["VendorName"].ToString(), lRow["VendorID"].ToString());
                //    //lstVendor.Attributes.Add("OptionGroup", VendorCategoryList.Select("VendorCategpryId=" + lRow["VendorCategpryId"].ToString())[0]["VendorCategoryNm"].ToString());
                //    lstVendors.Items.Add(lstVendor);
                //}

            }
        }
        public void FilterVendorByProductCategory()
        {
            StringBuilder strVendorCategory = new StringBuilder();
            string FilterParams = "";
            if (drpVendorCat.Items.Count > 1)
            {
                for (int i = 1; i < drpVendorCat.Items.Count; i++)
                {
                    strVendorCategory.Append(drpVendorCat.Items[i].Value.ToString()).Append(",");
                }

                FilterParams = strVendorCategory.Remove(strVendorCategory.Length - 1, 1).ToString();
                string ManufacturerType = GetManufacturerType();
                FilterVendors(FilterParams, "ProductCategory", ManufacturerType, "");
            }
            else
            {
                lstVendors.DataSource = null;
                lstVendors.DataBind();
            }
        }
        public void BindVendorByProdCat(string ProductId)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.GetVendorCategory(ProductId, optWholeSaler.Checked, optManf.Checked);
            drpVendorCat.DataSource = ds;
            drpVendorCat.DataTextField = "VendorCategoryName";
            drpVendorCat.DataValueField = "VendorCategoryId";
            drpVendorCat.DataBind();
            drpVendorCat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            InitialDataBind();
            mpVendorCat.Hide();
        }





        protected void lnkaddvendorquotes_Click(object sender, EventArgs e)
        {
            LinkButton lnkquotes = sender as LinkButton;

            GridViewRow gr = (GridViewRow)lnkquotes.Parent.Parent;
            //LinkButton lblcustid = (LinkButton)gr.FindControl("lnkcustomerid");
            //LinkButton lnksoldjobid = (LinkButton)gr.FindControl("lnksoldjobid");
            HiddenField hdnproductid = (HiddenField)gr.FindControl("hdnMaterialListId");
            //LinkButton lnkmateriallist = (LinkButton)gr.FindControl("lnkmateriallist");
            //Label lblProductType = (Label)gr.FindControl("lblProductType");

            string soldjobId = jobId;
            ViewState[ViewStateKey.Key.CustomerId.ToString()] = customerId;
            int custId = customerId;
            Session[SessionKey.Key.JobId.ToString()] = soldjobId;
            // ViewState[ViewStateKey.Key.SoldJobNo.ToString()] = soldjobId;
            //ViewState[ViewStateKey.Key.ProductId.ToString()] = hdnproductid.Value;
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(soldjobId);//, Convert.ToInt16(lblProductType.Text),  Convert.ToInt16(hdnproductid.Value));
            if (emailStatus == JGConstant.EMAIL_STATUS_VENDORCATEGORIES)
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(productTypeId);
                //Response.Redirect("~/Sr_App/AttachQuotes.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Attach_Quote", "window.open('AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(productTypeId) + "','AttachQuote','height=550px,width=1150px,top=50px,left=50px');", true);
                 //Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(productTypeId));
            }
            else if (emailStatus == JGConstant.EMAIL_STATUS_VENDOR)
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(productTypeId);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Attach_Quote", "window.open('AttachQuotes.aspx?EmailStatus=" + emailStatus + "','AttachQuote','height=550px,width=950px,top=50px,left=50px');", true);
                Response.Redirect("~/Sr_App/AttachQuotes.aspx?EmailStatus=" + emailStatus);
                // Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(lblProductType.Text) + "&EmailStatus=" + emailStatus);
            }
         
            else
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(productTypeId);
                //Response.Redirect("~/Sr_App/AttachQuotes.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Attach_Quote", "window.open('AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(productTypeId) + "','AttachQuote','height=550px,width=1150px,top=50px,left=50px');", true);
 
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First send email to all vendor categories');", true);
            }
        }

        protected void btnUploadPO_Click(object sender, EventArgs e)
        {
            List<CustomerDocument> custDocs = new List<CustomerDocument>();
            int intFileSize = flUploadPurchaseOrder.PostedFile.ContentLength;
            string pFileName = "";
            if (flUploadPurchaseOrder.HasFile)
            {
                if (flUploadPurchaseOrder.PostedFile.FileName != "")
                {
                    if (flUploadPurchaseOrder.PostedFile.ContentLength > 0 && !String.IsNullOrEmpty(flUploadPurchaseOrder.PostedFile.FileName))
                    {
                         pFileName = "PO_" + DateTime.Now.ToString("yyyyMMdd") + "_" + jobId.Replace("-", "") + "_#" + Path.GetExtension(flUploadPurchaseOrder.PostedFile.FileName);//"PO" + jobId.Replace("-", "") + "_" + hdnProdCatID.Value + "_" + hdnVendorID.Value + Path.GetExtension(flUploadPurchaseOrder.PostedFile.FileName);
                        CustomerDocument cbc = new CustomerDocument();
                        if (File.Exists(Server.MapPath("../CustomerDocs/PurchaseOrder/") + pFileName) == true)
                        {
                            File.Delete(Server.MapPath("../CustomerDocs/PurchaseOrder/") + pFileName);
                            flUploadPurchaseOrder.PostedFile.SaveAs(Server.MapPath("../CustomerDocs/PurchaseOrder/") + pFileName);
                        }
                        else
                        {
                            flUploadPurchaseOrder.PostedFile.SaveAs(Server.MapPath("../CustomerDocs/PurchaseOrder/") + pFileName);
                        }
                        string fPath;
                        fPath = ("../CustomerDocs/PurchaseOrder/") + pFileName;
                        cbc.DocumentName = pFileName;
                        cbc.DocumentPath = fPath;
                        custDocs.Add(cbc);
                    }
                }
            }
            string result = AdminBLL.Instance.AddMaterialListAttachment(jobId, Convert.ToInt32(hdnProdCatID.Value), custDocs, 2, Convert.ToInt32(hdnVendorID.Value)); //#- 2 means Quote Attachment
            File.Move(Server.MapPath("../CustomerDocs/PurchaseOrder/") + pFileName, Server.MapPath("../CustomerDocs/PurchaseOrder/") + result);
            InitialDataBind();
            if (result != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quotes attached successfully');", true);
            }
            else if (result == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('File with the same name already exists in the system. Please rename the file and upload again.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Internal Error Occurred');", true);
            }
        }
        protected void btnSendPurchaseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //PageDataset = CustomBLL.Instance.GetCustomMaterialList(jobId.ToString(), customerId);
                //string status = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
                Button btnSendVendorEmails = (Button)sender;
                Int32 lProductCatID = Convert.ToInt32(Convert.ToInt32(btnSendVendorEmails.CommandArgument));


                DataView lDvMaterialList = new DataView(PageDataset.Tables[1]);
                lDvMaterialList = new DataView(PageDataset.Tables[1], "ProductCatID=" + lProductCatID, "id asc", DataViewRowState.ModifiedCurrent);
                lDvMaterialList.Table = PageDataset.Tables[1];
                lDvMaterialList.RowFilter = "ProductCatID=" + lProductCatID;
                lDvMaterialList.RowStateFilter = DataViewRowState.ModifiedCurrent;
                String lVendorIds = "";

                foreach (DataRow lRow in PageDataset.Tables[1].Select("ProductCatID=" + lProductCatID))
                {
                    foreach (string lVend in lRow["vendorid"].ToString().Trim().Split(','))
                    {
                        if (!lVendorIds.Contains(lVend + ","))
                        {
                            lVendorIds += lVend + ",";
                        }
                    }
                }
                lVendorIds = lVendorIds.TrimEnd(',');
                foreach (string vendor in lVendorIds.Split(','))
                {
                    int y = 1;
                    StringBuilder lStrbHTMLTable = new StringBuilder();
                    lStrbHTMLTable.Append("<table  rules='all' style='width:80%;margin:auto auto;border:solid 1px;border-collapse:collapse;' cellpadding='0' cellspacing='0'>");
                    lStrbHTMLTable.Append("<tr>");
                    lStrbHTMLTable.Append("<th>#</th>");
                    // lStrbHTMLTable.Append("<th>JG SKU - Vendor Part #</th>");
                    lStrbHTMLTable.Append("<th>Material</th>");
                    lStrbHTMLTable.Append("<th>Quantity</th>");
                    lStrbHTMLTable.Append("<th>UOM</th>");
                    lStrbHTMLTable.Append("<th>Cost</th>");
                    lStrbHTMLTable.Append("<th>Extended</th>");
                    lStrbHTMLTable.Append("</tr>");
                    foreach (DataRow lRowC in PageDataset.Tables[1].Select("ProductCatID=" + lProductCatID + " AND VendorIds like '%" + vendor + "%'"))
                    {
                        lStrbHTMLTable.Append("<tr>");
                        lStrbHTMLTable.Append("<td>" + (y) + "</td>");
                        // lStrbHTMLTable.Append("<td>" + (lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim() == "" ? "-" : lDvMaterialList.Table.Rows[x]["JGSkuPartNo"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lRowC["MaterialList"].ToString().Trim() == "" ? "-" : lRowC["MaterialList"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lRowC["Quantity"].ToString().Trim() == "" ? "-" : lRowC["Quantity"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lRowC["UOM"].ToString().Trim() == "" ? "-" : lRowC["UOM"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lRowC["MaterialCost"].ToString().Trim() == "" ? "-" : lRowC["MaterialCost"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("<td>" + (lRowC["extend"].ToString().Trim() == "" ? "-" : lRowC["extend"].ToString().Trim()) + "</td>");
                        lStrbHTMLTable.Append("</tr>");
                        y += 1;
                    }
                    lStrbHTMLTable.Append("</table>");
                    if (y == 1)
                    {
                        continue;
                    }
                    int permissionStatus = 1;// CustomBLL.Instance.CheckPermissionsForVendorsByProdCat(jobId, lProductCatID);//, productTypeId, estimateId);
                    if (permissionStatus == 1)
                    {
                        bool emailStatusVendorCategory = SendOrderonEmails(vendor.TrimEnd(','), lStrbHTMLTable, lProductCatID); //sendEmailToVendorCategories(cmList);

                        if (emailStatusVendorCategory == true)
                        {
                            bool result = CustomBLL.Instance.UpdateEmailStatusOfCustomMaterialList(jobId, lProductCatID, JGConstant.EMAIL_STATUS_VENDOR);//, productTypeId, estimateId);
                            UpdateEmailStatus(JGConstant.EMAIL_STATUS_VENDOR.ToString());
                            btnSendMail.Text = "Save";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Foreman and Sr. Salesman permission');", true);
                        return;
                    }
                }
          


                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Purchase Order send to selected vendors');window.location = window.location.href;", true);
                return;

            }
            catch (Exception)
            {

            }




            
            //if (permissionStatus == 1)
            //{
            //    int statusQuotes = CustomBLL.Instance.WhetherVendorQuotesExists(jobId);
            //    if (statusQuotes == 1)
            //    {

            //        bool emailStatusVendor = sendEmailToVendors(cmList);
            //        if (emailStatusVendor == true)
            //        {
            //                               }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First attach quotes.');", true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First grant Admin and Sr. Salesman permission');", true);
            //}
        }
        #endregion

      


    }

}