using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using JG_Prospect.BLL;

namespace JG_Prospect.Sr_App
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetInventoryCategoryList();
            }
        }

        public void GetInventoryCategoryList()
        {
            StringBuilder str = new StringBuilder();
            try
            {
                DataSet ds = VendorBLL.Instance.GETInvetoryCatogriesList();
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

                                    str.Append("<li>");
                                    str.AppendFormat("<a href=\"javascript:;\">{0}</a>", VendorSubCategoryName);
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
    }
}