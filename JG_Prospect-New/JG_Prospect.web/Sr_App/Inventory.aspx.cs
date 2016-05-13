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
            if (Session["loginid"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have to login first');", true);
                Response.Redirect("~/login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string Mtype = GetManufacturerType();
                    GetInventoryCategoryList(Mtype);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "BindEvent", "bindClickEvent();", true);
            }
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
                List<InventoryProduct> lstVendorProducts = new List<InventoryProduct>();
                List<InventoryVendorCat> lstVendorCat = new List<InventoryVendorCat>();
                List<InventoryVendorSubCat> lstVendorSubCat = new List<InventoryVendorSubCat>();
                List<InventoryVendor> lstVendor = new List<InventoryVendor>();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        InventoryProduct obj = new InventoryProduct();
                        DataRow dr = ds.Tables[0].Rows[i];
                        int PrdouctID = Convert.ToInt32(dr["ProductId"] == DBNull.Value ? "0" : dr["ProductId"].ToString());
                        string ProductName = dr["ProductName"].ToString().Trim();
                        ProductName = ProductName.Replace(System.Environment.NewLine, string.Empty);
                        obj.ProductId = PrdouctID;
                        obj.ProductName = ProductName;
                        lstVendorProducts.Add(obj);
                    }
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        InventoryVendorCat obj = new InventoryVendorCat();
                        DataRow dr = ds.Tables[1].Rows[i];
                        int ProductCategoryId = Convert.ToInt32(dr["ProductCategoryId"] == DBNull.Value ? "0" : dr["ProductCategoryId"].ToString());
                        string VendorCategoryName = dr["VendorCategoryNm"].ToString().Trim();
                        VendorCategoryName = VendorCategoryName.Replace(System.Environment.NewLine, string.Empty);
                        int VendorCategoryId = Convert.ToInt32(dr["VendorCategpryId"] == DBNull.Value ? "0" : dr["VendorCategpryId"].ToString());
                        Boolean IsRetail_Wholesale = Convert.ToBoolean(dr["IsRetail_Wholesale"] == DBNull.Value ? "false" : dr["IsRetail_Wholesale"].ToString());
                        Boolean IsManufacturer = Convert.ToBoolean(dr["IsManufacturer"] == DBNull.Value ? "false" : dr["IsManufacturer"].ToString());
                        obj.ProductCategoryId = ProductCategoryId;
                        obj.VendorCategoryId = VendorCategoryId;
                        obj.VendorCategoryName = VendorCategoryName;
                        obj.IsRetail_Wholesale = IsRetail_Wholesale;
                        obj.IsManufacturer = IsManufacturer;
                        lstVendorCat.Add(obj);
                    }
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        InventoryVendorSubCat obj = new InventoryVendorSubCat();
                        DataRow dr = ds.Tables[2].Rows[i];
                        int VendorCategoryId = Convert.ToInt32(dr["VendorCategoryId"] == DBNull.Value ? "0" : dr["VendorCategoryId"].ToString());
                        string VendorSubCategoryName = dr["VendorSubCategoryName"].ToString().Trim();
                        VendorSubCategoryName = VendorSubCategoryName.Replace(System.Environment.NewLine, string.Empty);
                        int VendorSubCategoryId = Convert.ToInt32(dr["VendorSubCategoryId"] == DBNull.Value ? "0" : dr["VendorSubCategoryId"].ToString());
                        Boolean IsRetail_Wholesale = Convert.ToBoolean(dr["IsRetail_Wholesale"] == DBNull.Value ? "false" : dr["IsRetail_Wholesale"].ToString());
                        Boolean IsManufacturer = Convert.ToBoolean(dr["IsManufacturer"] == DBNull.Value ? "false" : dr["IsManufacturer"].ToString());
                        obj.VendorCategoryId = VendorCategoryId;
                        obj.VendorSubCategoryId = VendorSubCategoryId;
                        obj.VendorSubCategoryName = VendorSubCategoryName;
                        obj.IsRetail_Wholesale = IsRetail_Wholesale;
                        obj.IsManufacturer = IsManufacturer;
                        lstVendorSubCat.Add(obj);
                    }

                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        InventoryVendor obj = new InventoryVendor();
                        DataRow dr = ds.Tables[3].Rows[i];
                        int VendorId = Convert.ToInt32(dr["VendorId"] == DBNull.Value ? "0" : dr["VendorId"].ToString());
                        string VendorName = dr["VendorName"].ToString().Trim();
                        VendorName = VendorName.Replace(System.Environment.NewLine, string.Empty);
                        int VendorSubCatId = Convert.ToInt32(dr["VendorSubCatId"] == DBNull.Value ? "0" : dr["VendorSubCatId"].ToString());
                        obj.VendorId = VendorId;
                        obj.VendorName = VendorName;
                        obj.VendorSubCatId = VendorSubCatId;
                        lstVendor.Add(obj);
                    }

                }

                if (lstVendorProducts.Count > 0)
                {
                    str.Append("<ul class=\"clearfix inventory_main\">");
                    foreach (var item in lstVendorProducts)
                    {
                        int isCate = lstVendorCat.Where(a => a.ProductCategoryId == item.ProductId).Count();
                       
                        str.Append("<li>");
                        str.AppendFormat("<a href=\"javascript:;\"><span class=\"text\" onclick=\"productClick(this,'{0}','{1}')\">{1} ({2})</span><span class=\"buttons\"><i class=\"\" onclick=\"AddVenodrCat(this,'{0}','{1}')\">Add</i></span></a>", item.ProductId, item.ProductName, isCate);

                        if (isCate > 0)
                        {
                            string productClass = "";
                            if (ActiveProductID == item.ProductId)
                            {
                                productClass = "active";
                            }
                            str.AppendFormat("<ul class=\"clearfix inventory_cat {0}\">", productClass);
                            foreach (var cat in lstVendorCat.Where(a => a.ProductCategoryId == item.ProductId))
                            {
                                int isSubCate = lstVendorSubCat.Where(a => a.VendorCategoryId == cat.VendorCategoryId).Count();
                                str.Append("<li>");
                                str.AppendFormat("<a href=\"javascript:;\"><span class=\"text\" onclick=\"vendorClick(this,'{0}','{1}','{2}','{3}','{4}','{5}')\">{3} ({6})</span><span class=\"buttons\"><i class=\"\" onclick=\"AddSubCat(this,'{0}','{1}','{2}','{3}','{4}','{5}')\">Add</i><i class=\"\" onclick=\"EditVendorCat(this,'{0}','{1}','{2}','{3}','{4}','{5}')\">Edit</i><i class=\"\" onclick=\"DeleteVendorCat(this,'{0}','{1}','{2}','{3}','{4}','{5}')\">Delete</i></span></a>", cat.ProductCategoryId, item.ProductName, cat.VendorCategoryId, cat.VendorCategoryName, cat.IsRetail_Wholesale, cat.IsManufacturer, isSubCate);
                                                                
                                if (isSubCate > 0)
                                {
                                    string catClass = "";
                                    if (ActiveCategoryID == cat.VendorCategoryId)
                                    {
                                        catClass = "active";
                                    }
                                    str.AppendFormat("<ul class=\"clearfix inventory_subcat {0}\">", catClass);
                                    foreach (var subcat in lstVendorSubCat.Where(a => a.VendorCategoryId == cat.VendorCategoryId))
                                    {
                                        int VendorCount = lstVendor.Where(a => a.VendorSubCatId == subcat.VendorSubCategoryId).Count();
                                        str.Append("<li>");
                                        str.AppendFormat("<a href=\"javascript:;\"><span class=\"text\" onclick=\"vendorSubClick(this,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">{1} ({8})</span><span class=\"buttons\"><i class=\"\" onclick=\"EditSubCat(this,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">Edit</i><i class=\"\" onclick=\"DeleteSubCat(this,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">Delete</i></span></a>", subcat.VendorSubCategoryId, subcat.VendorSubCategoryName, subcat.VendorCategoryId, cat.VendorCategoryName, subcat.IsRetail_Wholesale, subcat.IsManufacturer, item.ProductId, item.ProductName, VendorCount);
                                        str.Append("</li>");
                                    }
                                    str.Append("</ul>");
                                }
                                str.Append("</li>");
                            }
                            str.Append("</ul>");
                        }
                        str.Append("</li>");
                    }
                    str.Append("</ul>");
                }
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

        protected void btnNewVendorSubCat_Click(object sender, EventArgs e)
        {
            VendorSubCategory objVendorSubCat = new VendorSubCategory();
            objVendorSubCat.VendorCatId = hdnVendorCatID.Value.ToString();
            objVendorSubCat.IsRetail_Wholesale = chkVSCRetail_WholesaleEdit.Checked;
            objVendorSubCat.IsManufacturer = chkVSCManufacturerEdit.Checked;
            objVendorSubCat.Name = txtVendorSubCatEdit.Text;

            ActiveProductID = Convert.ToInt32(hdnProductID.Value.ToString());
            ActiveCategoryID = Convert.ToInt32(hdnVendorCatID.Value.ToString());

            bool res = VendorBLL.Instance.SaveNewVendorSubCat(objVendorSubCat);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Sub Category Added Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to add Vendor Sub Category');", true);
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

            ActiveProductID = Convert.ToInt32(hdnProductID.Value.ToString());
            ActiveCategoryID = Convert.ToInt32(hdnVendorCatID.Value.ToString());

            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Sub Category Updated Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to update Vendor Sub Category');", true);
            }
        }

        protected void btnDeleteVendorSubCat_Click(object sender, EventArgs e)
        {
            ActiveProductID = Convert.ToInt32(hdnProductID.Value.ToString());
            ActiveCategoryID = Convert.ToInt32(hdnVendorCatID.Value.ToString());

            VendorSubCategory objVendorSubCat = new VendorSubCategory();
            objVendorSubCat.Id = hdnSubCategoryId.Value.ToString();
            //objVendorSubCat.IsRetail_Wholesale = chkVSCRetail_WholesaleEdit.Checked;
            //objVendorSubCat.IsManufacturer = chkVSCManufacturerEdit.Checked;
            //objVendorSubCat.Name = txtVendorSubCatEdit.Text;
            bool res = VendorBLL.Instance.DeleteVendorSubCat(objVendorSubCat);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Sub Category Deleted Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to delete Vendor Sub Category');", true);
            }
        }

        protected void btnAddVendorCat_Click(object sender, EventArgs e)
        {
            string ProductID = hdnProductID.Value.ToString();
            string VendorName = txtVendorCateogryName.Text;
            Boolean IsRetail_Wholesale = chkVendorCRetail_WholesaleEdit.Checked; ;
            Boolean IsManufacturer = chkVendorCManufacturerEdit.Checked;


            ActiveProductID = Convert.ToInt32(hdnProductID.Value.ToString());
            ActiveCategoryID = 0;

            NewVendorCategory objNewVendor = new NewVendorCategory();

            objNewVendor.VendorName = VendorName;
            objNewVendor.IsRetail_Wholesale = IsRetail_Wholesale;
            objNewVendor.IsManufacturer = IsManufacturer;
            string vendorCatId = VendorBLL.Instance.SaveNewVendorCategory(objNewVendor);
            objNewVendor.VendorId = vendorCatId;
            objNewVendor.ProductId = ProductID;
            bool res = VendorBLL.Instance.SaveNewVendorProduct(objNewVendor);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category Added Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to Add Vendor Category');", true);
            }
        }

        protected void btnUpdateVendorCat_Click(object sender, EventArgs e)
        {
            string ProductID = hdnProductID.Value.ToString();
            string VendorCatId = hdnVendorID.Value.ToString();
            string VendorName = txtVendorCateogryName.Text;
            Boolean IsRetail_Wholesale = chkVendorCRetail_WholesaleEdit.Checked; ;
            Boolean IsManufacturer = chkVendorCManufacturerEdit.Checked;


            ActiveProductID = Convert.ToInt32(hdnProductID.Value.ToString());
            ActiveCategoryID =0;

            NewVendorCategory objNewVendor = new NewVendorCategory();

            objNewVendor.VendorName = VendorName;
            objNewVendor.IsRetail_Wholesale = IsRetail_Wholesale;
            objNewVendor.IsManufacturer = IsManufacturer;
            objNewVendor.VendorId = VendorCatId;
            objNewVendor.ProductId = ProductID;

            bool res = VendorBLL.Instance.UpdateVendorCategory(objNewVendor);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category Updated Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to Update Vendor Category');", true);
            }
        }

        protected void btnDeleteVendorCat_Click(object sender, EventArgs e)
        {
            ActiveProductID = Convert.ToInt32(hdnProductID.Value.ToString());
            ActiveCategoryID =0;

            int vendorcategogyid = Convert.ToInt32(hdnVendorID.Value.ToString());
            bool res = VendorBLL.Instance.deletevendorcategory(vendorcategogyid);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category Deleted Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Unable to Delete Vendor Category. Vendor Cateogry contains Vendors.');", true);
            }
        }


        public int ActiveProductID
        {
            get
            {
                if (ViewState["ActiveProductID"] == null)
                {
                    return 0;
                }

                return (int)ViewState["ActiveProductID"];
            }
            set
            {
                ViewState["ActiveProductID"] = value;
            }
        }

        public int ActiveCategoryID
        {
            get
            {
                if (ViewState["ActiveCategoryID"] == null)
                {
                    return 0;
                }

                return (int)ViewState["ActiveCategoryID"];
            }
            set
            {
                ViewState["ActiveCategoryID"] = value;
            }
        }

    }

    public class InventoryVendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int VendorSubCatId { get; set; }
    }

    public class InventoryProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class InventoryVendorCat
    {
        public int ProductCategoryId { get; set; }
        public int VendorCategoryId { get; set; }
        public string VendorCategoryName { get; set; }
        public bool IsRetail_Wholesale { get; set; }
        public bool IsManufacturer { get; set; }
    }
    public class InventoryVendorSubCat
    {
        public int VendorCategoryId { get; set; }
        public int VendorSubCategoryId { get; set; }
        public string VendorSubCategoryName { get; set; }
        public bool IsRetail_Wholesale { get; set; }
        public bool IsManufacturer { get; set; }
    }
}