using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.Drawing;
using System.Text;


namespace JG_Prospect.Sr_App
{
    public partial class Price_control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindshutterstyle();
                bindshuttercolor();
                Bindsurfaceofmount();
                bindshutterwidth();
                bindaccessories();
                bindshuttertop();
                Session["ProductContractId"] = "";
                Session["ProductLineName"] = "";

                ////////New grid filling logic inserted by Sandeep
                BindProductLineGrid();
            }
        }

        private void BindProductLineGrid()
        {
            try
            {
                StringBuilder strerr = new StringBuilder();

                strerr.Append("DS start");
                DataSet newDs = new DataSet();
                newDs = new DataSet();
                newDs = AdminBLL.Instance.GetProductLineForGrid();
                strerr.Append("End DS ");
                if (newDs.Tables.Count > 0)
                {
                    strerr.Append("DS Found");
                    if (newDs.Tables[0].Rows.Count > 0)
                    {
                        DataTable d = newDs.Tables[0];

                     
                        //DataRow row = d.NewRow();
                        //row["ProductId"] = "331";
                        //row["ProductName"] = "Dump";

                        //d.Rows.Add(row);

                        //DataRow row1 = d.NewRow();
                        //row1["ProductId"] = "332";
                        //row1["ProductName"] = "Overhead";

                        //d.Rows.Add(row1);

                        gvProductLine.DataSource = d;
                        //   gvProductLine.DataSource = newDs.Tables[0];
                        gvProductLine.DataBind();
                        gvProductLine.Attributes.Add("style", "border='1'");
                    }
                }
                else
                {
                    strerr.Append("DS not Found");
                    lblerrorNew.Text = Convert.ToString(strerr);
                }
            }
            catch (Exception ex)
            {
                lblerrorNew.Text = ex.Message;
               // throw;
               // Response.Write(ex.Message);
            }
        }

        DataSet ds = new DataSet();
        public void bindshutterstyle()
        {
            ds = new DataSet();
            ds = AdminBLL.Instance.GetShutterStyle();
            //grdShutterStyle.DataSource = ds.Tables[0];
            //grdShutterStyle.DataBind();

        }

        public void bindshuttercolor()
        {
            ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshuttercolordetails();
            //grdShuttercolor.DataSource = ds.Tables[0];
            //grdShuttercolor.DataBind();            
        }

        public void Bindsurfaceofmount()
        {
            ds = new DataSet();
            ds = AdminBLL.Instance.GetSurfaceofmount();
            //grdsurfaceofmount.DataSource = ds.Tables[0];
            //grdsurfaceofmount.DataBind();

        }

        public void bindshutterwidth()
        {
            ds = new DataSet();
            ds = AdminBLL.Instance.GetShutterwidth();

            //grdshutterwidth.DataSource = ds.Tables[0];
            //grdshutterwidth.DataBind();

        }

        public void bindaccessories()
        {
            ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshutteraccessoriesdetails();
            //grdaccessories.DataSource = ds.Tables[0];
            //grdaccessories.DataBind();

        }

        public void bindshuttertop()
        {
            ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchtopshutterdetails();
            //grdshuttertop.DataSource = ds.Tables[0];
            //grdshuttertop.DataBind();

        }

        protected void grdShutterStyle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdShutterStyle.EditIndex = e.NewEditIndex;
            bindshutterstyle();
        }

        protected void grdShutterStyle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string key = grdShutterStyle.DataKeys[e.RowIndex].Values[0].ToString();
            //GridViewRow gr = grdShutterStyle.Rows[e.RowIndex];

            //string price = ((TextBox)(gr.FindControl("txtprice"))).Text;

            //bool result = false;

            //result = ShutterPriceControlBLL.Instance.updateshutterprice(Convert.ToInt32(key), Convert.ToDecimal(price));

            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Shutter Style Updated Successfully');", true);
            //}

            //grdShutterStyle.EditIndex = -1;
            //bindshutterstyle();
            //updateshutterstyle.Update();
        }

        //protected void grdShutterStyle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        string key = grdShutterStyle.DataKeys[e.RowIndex].Values[0].ToString();
        //      //  AdminBLL.Instance.DeleteShutterStyle(Convert.ToInt32(key));
        //        bindshutterstyle();

        //    }
        //    catch (Exception ex)
        //    {
        //        //return ex
        //    }
        //}

        protected void grdShutterStyle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdShutterStyle.EditIndex = -1;
            bindshutterstyle();
        }

        protected void grdShuttercolor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdShuttercolor.EditIndex = e.NewEditIndex;
            bindshuttercolor();
        }

        protected void grdShuttercolor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string key = grdShuttercolor.DataKeys[e.RowIndex].Values[0].ToString();
            //GridViewRow gr = grdShuttercolor.Rows[e.RowIndex];

            //string price = ((TextBox)(gr.FindControl("txtprice"))).Text;

            //bool result = false;

            //result = ShutterPriceControlBLL.Instance.updateshuttercolorprice(key, Convert.ToDecimal(price));

            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Shutter Color Updated Successfully');", true);
            //}

            //grdShuttercolor.EditIndex = -1;
            bindshuttercolor();
            //Updateshuttercolor.Update();
        }

        protected void grdShuttercolor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdShuttercolor.EditIndex = -1;
            bindshuttercolor();
        }

        protected void grdsurfaceofmount_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdsurfaceofmount.EditIndex = e.NewEditIndex;
            Bindsurfaceofmount();
        }

        protected void grdsurfaceofmount_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string key = grdsurfaceofmount.DataKeys[e.RowIndex].Values[0].ToString();
            //GridViewRow gr = grdsurfaceofmount.Rows[e.RowIndex];

            //string price = ((TextBox)(gr.FindControl("txtprice"))).Text;

            //bool result = false;

            //result = AdminBLL.Instance.updatesurfacemount(Convert.ToInt32(key), Convert.ToDecimal(price));

            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Surface of Mount Updated Successfully');", true);
            //}

            //grdsurfaceofmount.EditIndex = -1;
            Bindsurfaceofmount();
            //Updatesurfacemount.Update();
        }

        protected void grdsurfaceofmount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdsurfaceofmount.EditIndex = -1;
            Bindsurfaceofmount();
        }

        protected void grdshutterwidth_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdshutterwidth.EditIndex = e.NewEditIndex;
            bindshutterwidth();
        }

        protected void grdshutterwidth_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string key = grdshutterwidth.DataKeys[e.RowIndex].Values[0].ToString();
            //GridViewRow gr = grdshutterwidth.Rows[e.RowIndex];

            //string price = ((TextBox)(gr.FindControl("txtprice"))).Text;

            //bool result = false;

            //result = AdminBLL.Instance.updateshutterwidth(Convert.ToInt32(key), Convert.ToDecimal(price));

            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Shutter width Updated Successfully');", true);
            //}

            //grdshutterwidth.EditIndex = -1;
            bindshutterwidth();
            //Updateshutterwidth.Update();
        }

        protected void grdshutterwidth_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdshutterwidth.EditIndex = -1;
            bindshutterwidth();
        }

        protected void grdaccessories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdaccessories.EditIndex = e.NewEditIndex;
            bindaccessories();
        }

        protected void grdaccessories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string key = grdaccessories.DataKeys[e.RowIndex].Values[0].ToString();
            //GridViewRow gr = grdaccessories.Rows[e.RowIndex];

            //string price = ((TextBox)(gr.FindControl("txtprice"))).Text;

            //bool result = false;

            //result = ShutterPriceControlBLL.Instance.updateshutteraccessoriesprice(Convert.ToInt32(key), Convert.ToDecimal(price));

            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Shutter Accessories Updated Successfully');", true);
            //}

            //grdaccessories.EditIndex = -1;
            //bindaccessories();
            //Updateaccessories.Update();
        }

        protected void grdaccessories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdaccessories.EditIndex = -1;
            bindaccessories();
        }

        protected void btnMinus_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Decimal percentage = Convert.ToDecimal(txtpercentage.Text) / 100;
                string oper = "minus";
                bool result = false;
                result = AdminBLL.Instance.Updatepricebypercentage(percentage, oper);
                bindshutterstyle();
                bindshuttercolor();
                Bindsurfaceofmount();
                bindshutterwidth();
                bindaccessories();
                bindshuttertop();
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prices Updated Successfully');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnPlus_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                Decimal percentage = Convert.ToDecimal(txtpercentage.Text) / 100;
                string oper = "plus";
                bool result = false;
                result = AdminBLL.Instance.Updatepricebypercentage(percentage, oper);
                bindshutterstyle();
                bindshuttercolor();
                Bindsurfaceofmount();
                bindshutterwidth();
                bindaccessories();
                bindshuttertop();
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prices Updated Successfully');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grdshuttertop_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdshuttertop.EditIndex = -1;
            bindshuttertop();
        }

        protected void grdshuttertop_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdshuttertop.EditIndex = e.NewEditIndex;
            bindshuttertop();
        }

        protected void grdshuttertop_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string key = grdshuttertop.DataKeys[e.RowIndex].Values[0].ToString();
            //GridViewRow gr = grdshuttertop.Rows[e.RowIndex];

            //string price = ((TextBox)(gr.FindControl("txtprice"))).Text;

            //bool result = false;

            //result = ShutterPriceControlBLL.Instance.updatetopshutterprice(Convert.ToInt32(key), Convert.ToDecimal(price));

            //if (result)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Shutter Accessories Updated Successfully');", true);
            //}

            //grdshuttertop.EditIndex = -1;
            //bindshuttertop();
            //UpdateShutterTop.Update();
        }

        protected void gvProductLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditProduct")
            {
                txtProductName.Focus();
                btnSave1.Text = "Update";
                pnlAddProduct.Visible = true;
                int productId = Convert.ToInt32(e.CommandArgument);
                ViewState["ProductId_Price"] = productId;

                DataSet newDs = new DataSet();
                newDs = AdminBLL.Instance.SelectProduct_PriceControl(productId);
                if (newDs.Tables[0].Rows.Count > 0)
                {
                    txtProductName.Text = Convert.ToString(newDs.Tables[0].Rows[0]["ProductName"]);
                }

                return;
            }

            if (e.CommandName == "DeleteProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                DataSet newDs = new DataSet();
                newDs = AdminBLL.Instance.DeleteProduct_PriceControl(productId);
                BindProductLineGrid();
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "sv", "alert('Product Removed Successfully.');", true);
                return;
            }

            StringBuilder strerr = new StringBuilder();
            try
            {
                DataSet dsContract = new DataSet();
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                Label lblProductLine = (Label)(gvProductLine.Rows[index].Cells[0].FindControl("lblInstallid"));
                string ProductName = lblProductLine.Text;
                Session["ProductLineName"] = ProductName;
                string header = "";
                string Body1 = "";
                string Body2 = "";
                string Footer = "";
                dsContract = new DataSet();
               
                strerr.Append("DS Contract start");
                dsContract = AdminBLL.Instance.GetContractTemplate(ProductName);
                if (dsContract.Tables.Count > 0)
                {
                    strerr.Append("DS Contract in start");
                    if (dsContract.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsContract.Tables[0].Rows[0][0]) != "")
                        {
                            Session["ProductLineName"] = ProductName;
                            Session["ProductContractId"] = Convert.ToString(dsContract.Tables[0].Rows[0][0]);
                        }
                        if (Convert.ToString(dsContract.Tables[0].Rows[0][2]) != "")
                        {
                            header = Convert.ToString(dsContract.Tables[0].Rows[0][2]);
                            header = header.Replace(@"width=""100%""", @"width=""1000""");
                           // HeaderEditor.Content = header;
                            btnsave.Text = "Update";
                        }
                        if (Convert.ToString(dsContract.Tables[0].Rows[0][3]) != "")
                        {
                            Body1 = Convert.ToString(dsContract.Tables[0].Rows[0][3]);

                            #region Commented For Making blank body 1
                            BodyEditor.Content = Body1;
                           // BodyEditor.Content = "";
                            #endregion
                        }
                        if (Convert.ToString(dsContract.Tables[0].Rows[0][4]) != "")
                        {
                            Footer = Convert.ToString(dsContract.Tables[0].Rows[0][4]);
                            Footer = header.Replace(@"width=""100%""", @"width=""1000""");
                          //  FooterEditor.Content = header;
                        }
                        if (Convert.ToString(dsContract.Tables[0].Rows[0][6]) != "")
                        {
                            Body2 = Convert.ToString(dsContract.Tables[0].Rows[0][6]);
                           

                            #region Commented For Making blank body 2
                            BodyEditor2.Content = Body2;
                           //  BodyEditor2.Content  = "";
                            #endregion
                        }

                    }
                    else
                    {
                        Session["ProductContractId"] = "";
                        btnsave.Text = "Save";

                        #region Commented For Making blank body 1 & 2
                        BodyEditor2.Content = "";
                        BodyEditor.Content = "";
                        #endregion
                    }
                }
                else
                {
                    strerr.Append("DS Contract not found ");
                    Session["ProductContractId"] = "";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlay();", true);
                //mp_sold.Show();
                lblerrorNew.Text = Convert.ToString(strerr);
            }
            catch (Exception ex)
            {
                lblerrorNew.Text = Convert.ToString(strerr);
               // throw;
            }
        }

        protected void gvProductLine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblProductLine = (e.Row.FindControl("lblInstallid") as Label);
                    LinkButton lblContractTemplate = (e.Row.FindControl("lbtnContractTemplate") as LinkButton);

                    Label lblInstallsale = (e.Row.FindControl("lblInstallsale") as Label);
                    Label lblLaborPrice = (e.Row.FindControl("lblLaborPrice") as Label);
                    //Label lblToolChecklist = (e.Row.FindControl("lblToolChecklist") as Label);


                    if (lblProductLine.Text == "Dump" || lblProductLine.Text == "Overhead" )
                    {
                        lblContractTemplate.Text = "";
                    }
                    else
                    {
                        lblContractTemplate.Text = lblProductLine.Text + " " + "Contract Template";
                    }

                    //if (lblProductLine.Text == "Dumb" || lblProductLine.Text == "Overhead")
                    //{
                    //    e.Row.Cells[0].BackColor = Color.Yellow;

                    //}
                    //if (lblProductLine.Text == "Soffit")
                    //{
                    //    e.Row.Cells[0].BackColor = Color.Red;  
                    //}
                    if (lblProductLine.Text == "Custom - Other *" || lblProductLine.Text == "Basements" || lblProductLine.Text == "Additions" || lblProductLine.Text == "Kitchens" || lblProductLine.Text == "Bathrooms" || lblProductLine.Text == "Masonry -  Flat - Retaining walls" || lblProductLine.Text == "Windows & Doors" || lblProductLine.Text == "Flooring - Marble - Porcelain, Ceramic" || lblProductLine.Text == "Flooring - Hardwood-Laminate-Vinyl" || lblProductLine.Text == "Awnings" || lblProductLine.Text == "Roofing - Metal" || lblProductLine.Text == "Roofing -Terracotta-Shake-Slate" || lblProductLine.Text == "Siding -Masonry")
                    {
                        e.Row.Cells[0].ForeColor = Color.Red;
                        lblProductLine.ForeColor = Color.Yellow;
                        lblProductLine.Attributes.Add("style", "color:Red !important");
                        // lblProductLine.ForeColor = Color.Red;
                    }
                    lblInstallsale.Text = lblProductLine.Text + " " + "Install Selling Pricing";
                    lblLaborPrice.Text = lblProductLine.Text + " " + "Labor Pricing";
                }
               
            }
            catch (Exception ex)
            {
               Response.Write("" + ex.Message);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
           // string Editor_contentHeader = HeaderEditor.Content;
           // Editor_contentHeader = Editor_contentHeader.Replace(@"width=""1000""", @"width=""100%""");
           // string Editor_contentFooter = FooterEditor.Content;
            // Editor_contentFooter = Editor_contentFooter.Replace(@"width=""1000""", @"width=""100%""");
            #region Hide Header and Footer Template
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(1);
           // ds= AdminBLL.Instance.FetchContractTemplate(id);
            string header="";
            string footer = "";
            if (ds != null)
            {
                header = ds.Tables[0].Rows[0][0].ToString();
                header = header.Replace(@"width=""100%""", @"width=""1000""");
               // HeaderEditor.Content = header;

                footer = ds.Tables[0].Rows[0][2].ToString();
                footer = footer.Replace(@"width=""100%""", @"width=""1000""");
                //FooterEditor.Content = footer;
            }
            string Editor_contentHeader = header;
            string Editor_contentFooter = footer;
            #endregion
            string Editor_contentBody = BodyEditor.Content;
            string Editor_contentBody2 = BodyEditor2.Content;
            if (Convert.ToString(Session["ProductContractId"]) == "")
            {
                AdminBLL.Instance.InsertProductContract(Convert.ToString(Session["ProductLineName"]), Editor_contentHeader, Editor_contentBody, Editor_contentFooter, Editor_contentBody2);
              //  HeaderEditor.Content = string.Empty;
              //  FooterEditor.Content = string.Empty;
                BodyEditor.Content = string.Empty;
                BodyEditor2.Content = string.Empty;
                Session["ProductLineName"] = "";
                Session["ProductContractId"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup", "ClosePopup();", true);
            }
            else if (Convert.ToString(Session["ProductContractId"]) != "")
            {
                AdminBLL.Instance.UpdateProductContract(Convert.ToInt32(Session["ProductContractId"]), Convert.ToString(Session["ProductLineName"]), Editor_contentHeader, Editor_contentBody, Editor_contentFooter, Editor_contentBody2);
               // HeaderEditor.Content = string.Empty;
               // FooterEditor.Content = string.Empty;
                BodyEditor.Content = string.Empty;
                BodyEditor2.Content = string.Empty;
                Session["ProductLineName"] = "";
                Session["ProductContractId"] = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup", "ClosePopup();", true);
            }
        }

        protected void lnkAddProduct_Click(object sender, EventArgs e)
        {
            pnlAddProduct.Visible = true;
            txtProductName.Focus();
        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet newDs = new DataSet();

                if (ViewState["ProductId_Price"] == null)
                {

                    // check for duplicate
                    DataSet newDs1 = new DataSet();
                    newDs1 = AdminBLL.Instance.CheckDuplicateProduct_PriceControl(txtProductName.Text.Trim());
                    if (newDs1.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "sv1", "alert('Product Line Item already exist.');", true);
                        return;
                    }

                    newDs = AdminBLL.Instance.InsertProduct_PriceControl(txtProductName.Text);

                    BindProductLineGrid();
                    // ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "Product Added Successfully;", true);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "sv", "alert('Product Line Item Added Successfully.');", true);
                }
                else if (ViewState["ProductId_Price"] != null)
                {

                    // check for duplicate
                    DataSet newDs1 = new DataSet();
                    newDs1 = AdminBLL.Instance.CheckDuplicateProduct_Update_PriceControl(txtProductName.Text.Trim(), Convert.ToInt32(ViewState["ProductId_Price"]));
                    if (newDs1.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "sv1", "alert('Product Line Item already exist.');", true);
                        return;
                    }


                    newDs = AdminBLL.Instance.UpdateProduct_PriceControl(txtProductName.Text, Convert.ToInt32(ViewState["ProductId_Price"]));

                    BindProductLineGrid();
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "Product Updated Successfully;", true);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "sv", "alert('Product Line Item Updated Successfully.');", true);
                    ViewState["ProductId_Price"] = null;
                    btnSave1.Text = "Save";
                }

                txtProductName.Text = string.Empty;
                pnlAddProduct.Visible = false;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void btncancel1_Click(object sender, EventArgs e)
        {
            ViewState["ProductId_Price"] = null;
            btnSave1.Text = "Save";
            txtProductName.Text = string.Empty;
            pnlAddProduct.Visible = false;
        }


    }
}