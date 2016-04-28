using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;

namespace JG_Prospect.Sr_App
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
                BindProductCategory();
                bindvendorcategory();
            }
        }

        protected void BindProductCategory()
        {
            ddlProductCatgoryPopup.Items.Clear();
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.GetProductCategory();
            ddlProductCatgoryPopup.DataSource = ds;
            ddlProductCatgoryPopup.DataTextField = "ProductName";
            ddlProductCatgoryPopup.DataValueField = "ProductId";
            ddlProductCatgoryPopup.DataBind();
            ddlProductCatgoryPopup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        protected void bindvendorcategory()
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchvendorcategory(rdoRetailWholesale.Checked, rdoManufacturer.Checked);
            ddlVendorCatPopup.DataSource = ds;
            ddlVendorCatPopup.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlVendorCatPopup.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlVendorCatPopup.DataBind();
            ddlVendorCatPopup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }
        public string GetManufacturerType()
        {
            string MType = "";
            if (rdoRetailWholesale.Checked)
                MType = rdoRetailWholesale.Text;
            else if (rdoManufacturer.Checked)
                MType = rdoManufacturer.Text;
            return MType;
        }

        public void GetInventoryCategoryList(string ManufacturerType)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                DataSet ds = VendorBLL.Instance.GETInvetoryCatogriesList(ManufacturerType);
                str.Append("<ul class=\"clearfix inventory_main\">");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    int PrdouctID = Convert.ToInt32(dr["ProductId"].ToString());
                    string ProductName = dr["ProductName"].ToString();


                    str.Append("<li>");
                    str.AppendFormat("<a href=\"javascript:;\">{0}</a>", ProductName);
                                        
                    Boolean isCate = true;
                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {
                        DataRow dr1 = ds.Tables[1].Rows[j];
                        int ProductCategoryId = Convert.ToInt32(dr1["ProductCategoryId"] == DBNull.Value ? "0" : dr1["ProductCategoryId"].ToString());
                        if (PrdouctID == ProductCategoryId)
                        {
                            if (isCate)
                            {
                                str.Append("<ul class=\"clearfix inventory_cat\">");
                            }
                            isCate = false;
                            string VendorCategoryName = dr1["VendorCategoryNm"].ToString();
                            int VendorCategoryId = Convert.ToInt32(dr1["VendorCategpryId"] == DBNull.Value ? "0" : dr1["VendorCategpryId"].ToString());

                            str.Append("<li>");
                            str.AppendFormat("<a href=\"javascript:;\">{0}</a>", VendorCategoryName);
                            //str.AppendFormat("")
                            Boolean isSubCate = true;
                            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                            {
                                DataRow dr2 = ds.Tables[2].Rows[k];
                                int VendorCategoryId2 = Convert.ToInt32(dr2["VendorCategoryId"] == DBNull.Value ? "0" : dr2["VendorCategoryId"].ToString());
                                if (VendorCategoryId == VendorCategoryId2)
                                {
                                    if (isSubCate)
                                    {
                                        str.Append("<ul class=\"clearfix inventory_subcat\">");
                                    }
                                    isSubCate = false;
                                    int VendorSubCategoryId = Convert.ToInt32(dr2["VendorSubCategoryId"] == DBNull.Value ? "0" : dr2["VendorSubCategoryId"].ToString());
                                    string VendorSubCategoryName = dr2["VendorSubCategoryName"].ToString();
                                    Boolean IsRetail_Wholesale = Convert.ToBoolean(dr2["IsRetail_Wholesale"].ToString());
                                    Boolean IsManufacturer = Convert.ToBoolean(dr2["IsManufacturer"].ToString());

                                    str.Append("<li>");
                                    str.AppendFormat("<a href=\"javascript:;\" onclick=\"EditSubCat({0},'{1}','{2}','{3}')\">{1}</a>", VendorSubCategoryId, VendorSubCategoryName, IsRetail_Wholesale, IsManufacturer);
                                    str.Append("</li>");
                                }
                            }
                            if (!isSubCate)
                            {
                                str.Append("</ul>");
                            }
                            str.Append("</li>");
                        }
                    }

                    if (!isCate)
                    {
                        str.Append("</ul>");
                    }
                }

                str.Append("</li>");
                str.Append("</ul>");
            }
            catch (Exception ex)
            {
            }
            ltrInventoryCategoryList.Text = str.ToString();
        }

        protected void rdoRetailWholesale_CheckedChanged(object sender, EventArgs e)
        {
            string Mtype = GetManufacturerType();
            GetInventoryCategoryList(Mtype);
        }

        protected void rdoManufacturer_CheckedChanged(object sender, EventArgs e)
        {
            string Mtype = GetManufacturerType();
            GetInventoryCategoryList(Mtype);
        }

        protected void btnNewVendor_Click(object sender, EventArgs e)
        {
            NewVendorCategory objNewVendor = new NewVendorCategory();

            objNewVendor.VendorName = txtnewVendorCat.Text;
            objNewVendor.IsRetail_Wholesale = chkVCRetail_Wholesale.Checked;
            objNewVendor.IsManufacturer = chkVCManufacturer.Checked;
            string vendorCatId = VendorBLL.Instance.SaveNewVendorCategory(objNewVendor);
            objNewVendor.VendorId = vendorCatId;
            objNewVendor.ProductId = ddlProductCatgoryPopup.SelectedValue.ToString();
            objNewVendor.ProductName = ddlProductCatgoryPopup.SelectedItem.Text;
            bool res = VendorBLL.Instance.SaveNewVendorProduct(objNewVendor);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been inserted Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }
        }

        protected void btnNewVendorSubCat_Click(object sender, EventArgs e)
        {
            VendorSubCategory objVendorSubCat = new VendorSubCategory();
            objVendorSubCat.VendorCatId = ddlVendorCatPopup.SelectedValue.ToString();
            objVendorSubCat.IsRetail_Wholesale = chkVSCRetail_Wholesale.Checked;
            objVendorSubCat.IsManufacturer = chkVSCManufacturer.Checked;
            objVendorSubCat.Name = txtVendorSubCat.Text;
            bool res = VendorBLL.Instance.SaveNewVendorSubCat(objVendorSubCat);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been inserted Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }
        }

        protected void btnUdpateVendorSubCat_Click(object sender, EventArgs e)
        {
            VendorSubCategory objVendorSubCat = new VendorSubCategory();
            objVendorSubCat.Id = hdnSubCategoryId.Value.ToString();
            objVendorSubCat.IsRetail_Wholesale = chkVSCRetail_WholesaleEdit.Checked;
            objVendorSubCat.IsManufacturer = chkVSCManufacturerEdit.Checked;
            objVendorSubCat.Name = txtVendorSubCatEdit.Text;
            bool res = VendorBLL.Instance.UpdateVendorSubCat(objVendorSubCat);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been Updated Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }
        }

    }
}