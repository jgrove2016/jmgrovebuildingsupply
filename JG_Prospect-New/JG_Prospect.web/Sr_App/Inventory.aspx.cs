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
                str.Append("<ul class=\"clearfix inventory_main\">");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    int PrdouctID = Convert.ToInt32(dr["ProductId"].ToString());
                    string ProductName = dr["ProductName"].ToString();


                    str.Append("<li>");
                    str.AppendFormat("<a href=\"javascript:;\"><span class=\"text\" onclick=\"productClick(this,'{0}','{1}')\">{1}</span><span class=\"buttons\"><i class=\"\" onclick=\"AddVenodrCat('{0}','{1}')\">Add</i></span></a>", PrdouctID, ProductName);

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

                            Boolean IsVRetail_Wholesale = Convert.ToBoolean(dr1["IsRetail_Wholesale"].ToString());
                            Boolean IsVManufacturer = Convert.ToBoolean(dr1["IsManufacturer"].ToString());


                            str.Append("<li>");
                            str.AppendFormat("<a href=\"javascript:;\"><span class=\"text\" onclick=\"vendorClick(this,'{0}','{1}','{2}','{3}','{4}','{5}')\">{3}</span><span class=\"buttons\"><i class=\"\" onclick=\"AddSubCat('{2}','{3}')\">Add</i><i class=\"\" onclick=\"EditVendorCat({0},'{1}','{2}','{3}','{4}','{5}')\">Edit</i><i class=\"\" onclick=\"DeleteVendorCat({0},'{1}','{2}','{3}','{4}','{5}')\">Delete</i></span></a>", ProductCategoryId, ProductName, VendorCategoryId, VendorCategoryName, IsVRetail_Wholesale, IsVManufacturer);
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
                                    str.AppendFormat("<a href=\"javascript:;\"><span class=\"text\" onclick=\"vendorSubClick(this,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')\">{1}</span><span class=\"buttons\"><i class=\"\" onclick=\"EditSubCat({0},'{1}','{2}','{3}','{4}','{5}')\">Edit</i><i class=\"\" onclick=\"DeleteSubCat({0},'{1}','{2}','{3}','{4}','{5}')\">Delete</i></span></a>", VendorSubCategoryId, VendorSubCategoryName, VendorCategoryId, VendorCategoryName, IsRetail_Wholesale, IsManufacturer,ProductCategoryId,ProductName);
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

        protected void btnNewVendorSubCat_Click(object sender, EventArgs e)
        {
            VendorSubCategory objVendorSubCat = new VendorSubCategory();
            objVendorSubCat.VendorCatId = hdnVendorCatID.Value.ToString();
            objVendorSubCat.IsRetail_Wholesale = chkVSCRetail_WholesaleEdit.Checked;
            objVendorSubCat.IsManufacturer = chkVSCManufacturerEdit.Checked;
            objVendorSubCat.Name = txtVendorSubCatEdit.Text;
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

        protected void btnDeleteVendorSubCat_Click(object sender, EventArgs e)
        {
            VendorSubCategory objVendorSubCat = new VendorSubCategory();
            objVendorSubCat.Id = hdnSubCategoryId.Value.ToString();
            //objVendorSubCat.IsRetail_Wholesale = chkVSCRetail_WholesaleEdit.Checked;
            //objVendorSubCat.IsManufacturer = chkVSCManufacturerEdit.Checked;
            //objVendorSubCat.Name = txtVendorSubCatEdit.Text;
            bool res = VendorBLL.Instance.DeleteVendorSubCat(objVendorSubCat);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been Deleted Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }
        }

        protected void btnAddVendorCat_Click(object sender, EventArgs e)
        {
            string ProductID = hdnProductID.Value.ToString();
            string VendorName = txtVendorCateogryName.Text;
            Boolean IsRetail_Wholesale = chkVendorCRetail_WholesaleEdit.Checked; ;
            Boolean IsManufacturer = chkVendorCManufacturerEdit.Checked;


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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been inserted Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }
        }

        protected void btnUpdateVendorCat_Click(object sender, EventArgs e)
        {
            string ProductID = hdnProductID.Value.ToString();
            string VendorCatId = hdnVendorID.Value.ToString();
            string VendorName = txtVendorCateogryName.Text;
            Boolean IsRetail_Wholesale = chkVendorCRetail_WholesaleEdit.Checked; ;
            Boolean IsManufacturer = chkVendorCManufacturerEdit.Checked;


            NewVendorCategory objNewVendor = new NewVendorCategory();

            objNewVendor.VendorName = VendorName;
            objNewVendor.IsRetail_Wholesale = IsRetail_Wholesale;
            objNewVendor.IsManufacturer = IsManufacturer;
            objNewVendor.VendorId = VendorCatId;
            objNewVendor.ProductId = ProductID;

            bool res = VendorBLL.Instance.UpdateVendorCategory(objNewVendor);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been updated Successfully');", true);
                string Mtype = GetManufacturerType();
                GetInventoryCategoryList(Mtype);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }
        }

        protected void btnDeleteVendorCat_Click(object sender, EventArgs e)
        {
            int vendorcategogyid = Convert.ToInt32(hdnVendorID.Value.ToString());
            bool res = VendorBLL.Instance.deletevendorcategory(vendorcategogyid);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been Deleted Successfully');", true);
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