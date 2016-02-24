using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.IO;
using JG_Prospect.Common;

namespace JG_Prospect.Sr_App
{
    public partial class Procurement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindSoldJobs();
                bindVendors();
                bindfordeletevender();
                if (Request.QueryString["FileToOpen"] != null)
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + Request.QueryString["FileToOpen"].ToString() + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                }
            }
        }

        private void bindSoldJobs()
        {
            DataSet ds = new_customerBLL.Instance.GetSoldjobsforprocurement();
            //DataSet ds1 = VendorBLL.Instance.GetAllvendorDetails();
            
            if (ds != null)
            {
                grdsoldjobs.DataSource = ds;
                grdsoldjobs.Columns[7].Visible = false;
                grdsoldjobs.DataBind();
            }
        }
        private void bindVendors()
        {
            DataSet ds = VendorBLL.Instance.fetchAllVendorCategoryHavingVendors();
            if (ds != null)
            {
                grdvendors.DataSource = ds;
                grdvendors.DataBind();
                grdvendors.Columns[1].Visible = false;
            }
        }
        protected void ddlstatus_selectedindexchanged(object sender, EventArgs e)
        {
            DropDownList ddlstatus = sender as DropDownList;
            GridViewRow gr = (GridViewRow)ddlstatus.Parent.Parent;
            LinkButton lblcustid = (LinkButton)gr.FindControl("lnkcustomerid");
            LinkButton lnkmateriallist = (LinkButton)gr.FindControl("lnkmateriallist");
            HiddenField hdnproductid = (HiddenField)gr.FindControl("hdnproductid");
            Label lblProductType = (Label)gr.FindControl("lblProductType");

            string soldjobId = lnkmateriallist.Text.Trim().Split('M')[0].Trim();
            int custId = Convert.ToInt16(lblcustid.Text.ToString().Substring(1));
            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);

            if (ddlstatus.SelectedValue != JGConstant.ZERO.ToString())
            {
                new_customerBLL.Instance.AddCustomerFollowUp(custId, DateTime.Now, ddlstatus.SelectedItem.Text, userId, false, 0);
                new_customerBLL.Instance.UpdateStatusOfCustomer(soldjobId, Convert.ToInt16(ddlstatus.SelectedValue));//, Convert.ToInt16(lblProductType.Text), Convert.ToInt16(hdnproductid.Value));
            }
            else if (ddlstatus.SelectedValue == JGConstant.ZERO.ToString())
            {
                new_customerBLL.Instance.AddCustomerFollowUp(custId, DateTime.Now, JGConstant.CUSTOMER_STATUS_ORDERED, userId, false, 0);
                new_customerBLL.Instance.UpdateStatusOfCustomer(soldjobId, 13);//, Convert.ToInt16(lblProductType.Text), Convert.ToInt16(hdnproductid.Value));
            }
            bindSoldJobs();
        }
        protected void grdvendors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string vendorCategoryId = e.Row.Cells[1].Text;

                    DataSet dsVendorNames = VendorBLL.Instance.fetchVendorNamesByVendorCategory(Convert.ToInt16(vendorCategoryId));
                    DropDownList drpVendorName = (DropDownList)e.Row.FindControl("drpVendorName");
                    drpVendorName.DataSource = dsVendorNames;
                    drpVendorName.DataTextField = "VendorName";
                    drpVendorName.DataValueField = "VendorId";
                    drpVendorName.DataBind();
                    drpVendorName.SelectedIndex = 1;
                    DataSet dsVendorDetails = new DataSet();
                    if (drpVendorName.SelectedValue != "")
                    {
                        dsVendorDetails = VendorBLL.Instance.fetchVendorDetailsByVendorId(Convert.ToInt16(drpVendorName.SelectedValue));
                    }
                    else
                    {
                        dsVendorDetails = null;
                    }
                    if (dsVendorDetails != null)
                    {
                        Label lblContactPerson = (Label)e.Row.FindControl("lblContactPerson");
                        lblContactPerson.Text = dsVendorDetails.Tables[0].Rows[0]["ContactPerson"].ToString();
                        Label lblContactNumber = (Label)e.Row.FindControl("lblContactNumber");
                        lblContactNumber.Text = dsVendorDetails.Tables[0].Rows[0]["ContactNumber"].ToString();
                        Label lblFax = (Label)e.Row.FindControl("lblFax");
                        lblFax.Text = dsVendorDetails.Tables[0].Rows[0]["Fax"].ToString();
                        Label lblEmail = (Label)e.Row.FindControl("lblEmail");
                        lblEmail.Text = dsVendorDetails.Tables[0].Rows[0]["Email"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
        protected void bindfordeletevender()
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchallvendorcategory();
            ddlvendercategoryname.DataSource = ds;
            ddlvendercategoryname.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlvendercategoryname.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlvendercategoryname.DataBind();


        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            Vendor_Catalog objcatalog = new Vendor_Catalog();

            objcatalog.catalog_name = txtname.Text;
            bool res = VendorBLL.Instance.savevendorcatalogdetails(objcatalog);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been inserted Successfully');", true);
                bindfordeletevender();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }

        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            bool result = VendorBLL.Instance.deletevendorcategory(Convert.ToInt32(ddlvendercategoryname.SelectedValue));
            if (result)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category has been deleted Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category cannot be deleted, delete all vendors of this category');", true);

            bindfordeletevender();

        }

        protected void btnaddvendors_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vendors.aspx");
        }

        protected void lnkaddvendorquotes_Click(object sender, EventArgs e)
        {
            LinkButton lnkquotes = sender as LinkButton;
         
            GridViewRow gr = (GridViewRow)lnkquotes.Parent.Parent;
            LinkButton lblcustid = (LinkButton)gr.FindControl("lnkcustomerid");
            LinkButton lnksoldjobid = (LinkButton)gr.FindControl("lnksoldjobid");
            HiddenField hdnproductid = (HiddenField)gr.FindControl("hdnproductid");
            LinkButton lnkmateriallist = (LinkButton)gr.FindControl("lnkmateriallist");
            Label lblProductType = (Label)gr.FindControl("lblProductType");
            string soldjobId = lnkmateriallist.Text.Trim().Split('M')[0].Trim();
            ViewState[ViewStateKey.Key.CustomerId.ToString()] = lblcustid.Text;
            int custId = Convert.ToInt16(ViewState[ViewStateKey.Key.CustomerId.ToString()].ToString().Substring(1));
            Session[SessionKey.Key.JobId.ToString()] = soldjobId;
           // ViewState[ViewStateKey.Key.SoldJobNo.ToString()] = soldjobId;
            //ViewState[ViewStateKey.Key.ProductId.ToString()] = hdnproductid.Value;
            string emailStatus = CustomBLL.Instance.GetEmailStatusOfCustomMaterialList(soldjobId);//, Convert.ToInt16(lblProductType.Text),  Convert.ToInt16(hdnproductid.Value));
            if (emailStatus == JGConstant.EMAIL_STATUS_VENDORCATEGORIES)
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(lblProductType.Text);
                Response.Redirect("~/Sr_App/AttachQuotes.aspx");
               // Response.Redirect("~/Sr_App/AttachQuotes.aspx?CustomerId=" + custId + "&ProductId=" + hdnproductid.Value + "&ProductTypeId=" + Convert.ToInt16(lblProductType.Text));
            }
            else if (emailStatus == JGConstant.EMAIL_STATUS_VENDOR)
            {
                ViewState[ViewStateKey.Key.ProductTypeId.ToString()] = Convert.ToInt16(lblProductType.Text);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('First send email to all vendor categories');", true);
            }

        }

        //protected void grdAttachQuotes_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.ToLower() == "viewfile")
        //    {
        //        string file = Convert.ToString(e.CommandArgument);
        //        string domainName = Request.Url.GetLeftPart(UriPartial.Authority);

        //        ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + domainName + "/CustomerDocs/VendorQuotes/" + file + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
        //    }
        //    else if (e.CommandName.ToLower() == "removefile")
        //    {
        //        string file = Convert.ToString(e.CommandArgument);
        //        CustomBLL.Instance.RemoveAttachedQuote(file);
        //        binddata();
        //    }
        //}
        protected void lnkcustomerid_Click(object sender, EventArgs e)
        {
            LinkButton lnkcustid = sender as LinkButton;
            Response.Redirect("~/Sr_App/Customer_Profile.aspx?CustomerId=" + lnkcustid.Text.Substring(1));
        }

        protected void lnksoldjobid_Click(object sender, EventArgs e)
        {
            LinkButton lnksoldjobid = sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnksoldjobid.Parent.Parent;
            Label lblProductType = (Label)gr.FindControl("lblProductType");
            LinkButton lnkcustomerid = (LinkButton)gr.FindControl("lnkcustomerid");
            LinkButton lnkmateriallist = (LinkButton)gr.FindControl("lnkmateriallist");
            HiddenField hdnproductid = (HiddenField)gr.FindControl("hdnproductid");
            int customerId = Convert.ToInt16(lnkcustomerid.Text.Trim().Substring(1));
            DataSet ds = existing_customerBLL.Instance.GetExistingCustomerDetailById(customerId);
            DataRow dr = ds.Tables[0].Rows[0];
            Session[SessionKey.Key.CustomerName.ToString()] = dr["CustomerName"].ToString();
            string soldjobId = lnkmateriallist.Text.Trim().Split('M')[0].Trim();
           // DataSet dssoldJobs = new_customerBLL.Instance.GetProductAndEstimateIdOfSoldJob(soldjobId);
            int productId = Convert.ToInt16(hdnproductid.Value); //Convert.ToInt16(dssoldJobs.Tables[0].Rows[0]["EstimateId"].ToString());
            //int productId = Convert.ToInt16(lnksoldjobid.Text.Trim().Substring(2));
            if (lblProductType.Text != JGConstant.ONE.ToString())
            {
                Response.Redirect("Custom.aspx?ProductTypeId="+ Convert.ToInt16(lblProductType.Text) + "&ProductId=" + productId + "&CustomerId=" + customerId);

            }
        }
        protected void lnkmateriallist_Click(object sender, EventArgs e)
        {
            LinkButton lnkmateriallist = sender as LinkButton;

            GridViewRow gr = (GridViewRow)lnkmateriallist.Parent.Parent;

            LinkButton lnksoldjobid = (LinkButton)gr.FindControl("lnksoldjobid");
            Label lblProductType = (Label)gr.FindControl("lblProductType");
            string soldjobId = lnkmateriallist.Text.Trim().Split('M')[0].Trim ();
            //int productId = Convert.ToInt16(lnksoldjobid.Text.Trim().Substring(2));
            DataSet dssoldJobs = new_customerBLL.Instance.GetProductAndEstimateIdOfSoldJob(soldjobId);
            int productId = Convert.ToInt16(dssoldJobs.Tables[0].Rows[0]["EstimateId"].ToString());
            LinkButton lnkcustomerid = (LinkButton)gr.FindControl("lnkcustomerid");
            int customerId = Convert.ToInt16(lnkcustomerid.Text.Trim().Substring(1));

        
                Session[SessionKey.Key.JobId.ToString()] = soldjobId;
                //Response.Redirect("Custom_MaterialList.aspx");
                Response.Redirect("Custom_MaterialList.aspx?" + QueryStringKey.Key.ProductId.ToString() + "=" + productId + "&" + QueryStringKey.Key.CustomerId.ToString() + "=" + customerId + "&" + QueryStringKey.Key.ProductTypeId.ToString() + "=" + Convert.ToInt16(lblProductType.Text.ToString()));
                   // }
                   // else
                   // {
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Quotes are attached. Please attach quotes.');", true);
                   // }
               // }
               // else
               // {
                    //Response.Redirect("/Sr_App/Custom_MaterialList.aspx?" + QueryStringKey.Key.ProductId.ToString() + "=" + productId + "&" + QueryStringKey.Key.CustomerId.ToString() + "=" + customerId + "&" + QueryStringKey.Key.ProductTypeId.ToString() + "=" + (int)JGConstant.ProductType.custom);
                //}

            }
       // }

      

        protected void grdsoldjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkmateriallist = (LinkButton)e.Row.FindControl("lnkmateriallist");
                LinkButton lnksoldjobid = (LinkButton)e.Row.FindControl("lnksoldjobid");
                Label lblProductType = (Label)e.Row.FindControl("lblProductType");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlstatus");
                LinkButton lnkcustomerid = (LinkButton)e.Row.FindControl("lnkcustomerid");
                HiddenField hdnStatusId = (HiddenField)e.Row.FindControl("hdnStatusId");
                HiddenField hdnJobSeqId = (HiddenField)e.Row.FindControl("hdnJobSeqId");
               // GridView grdAttachQuotes = (GridView)e.Row.FindControl("grdAttachQuotes");

                //if (lblProductType.Text == JGConstant.PRODUCT_CUSTOM)
                //{
                //    lnkmateriallist.Enabled = true;
                //    lnksoldjobid.Enabled = true;
                //}
                //else
                //{
                //    lnkmateriallist.Enabled = false;
                //    lnksoldjobid.Enabled = false;
                //}
                if (lblStatus.Text.ToLower().Contains("ordered") || lblStatus.Text.ToLower().Contains("received “storage location?") || lblStatus.Text.ToLower().Contains("on standby @ vendor link to vendor profile") || lblStatus.Text.ToLower().Contains("being delivered to job site"))
                {
                    ddlstatus.Visible = true;
                    DataSet ds = new_customerBLL.Instance.FetchAllStatus();
                    string filter = " StatusId in(18,19,20)";
                    ds.Tables[0].DefaultView.RowFilter = filter;
                    ddlstatus.DataSource = ds.Tables[0].DefaultView;
                    ddlstatus.DataTextField = "StatusName";
                    ddlstatus.DataValueField = "StatusId";
                    ddlstatus.DataBind();
                    ddlstatus.Items.Insert(0, new ListItem(JGConstant.SELECT, "0"));
                    if (Convert.ToInt16(hdnStatusId.Value) == JGConstant.STATUS_ID_RECEIVED_STORAGE_LOCATION || Convert.ToInt16(hdnStatusId.Value) == JGConstant.STATUS_ID_ON_STANDBY_VENDOR_LINK_TO_VENDOR_PROFILE || Convert.ToInt16(hdnStatusId.Value) == JGConstant.STATUS_ID_BEING_DELEIVERED_TO_JOBSITE)
                    {
                        ddlstatus.SelectedValue = hdnStatusId.Value;
                    }
                }
                int customerId = Convert.ToInt16(lnkcustomerid.Text.Trim().Substring(1));
                //int soldJobId = Convert.ToInt16(lnksoldjobid.Text.Trim().Substring(2));
                
                string soldjobId = lnkmateriallist.Text.Trim().Split('M')[0].Trim();
                //bindgrid(customerId, soldjobId, grdAttachQuotes, lblProductType.Text);
            }
        }

        //public void bindgrid(int customerId, string soldJobId, GridView grdAttachQuotes,string productType)
        //public void bindgrid(int customerId, string soldJobId, string productType)
        //{
        //    DataSet ds = null;
        //    DataSet ds1 = new_customerBLL.Instance.GetProductAndEstimateIdOfSoldJob(soldJobId);
        //    int estimateId = Convert.ToInt16(ds1.Tables[0].Rows[0]["EstimateId"].ToString());
        //    int productTypeId=0;
        //    if(productType == JGConstant.PRODUCT_CUSTOM)
        //    {
        //        productTypeId =(int)JGConstant.ProductType.custom;
        //    }
        //    else
        //    {
        //        productTypeId =(int)JGConstant.ProductType.shutter ;
        //    }

        //    ds = CustomBLL.Instance.GetAllAttachedQuotes(customerId, estimateId, productTypeId);
        //    //grdAttachQuotes.DataSource = ds;
        //    //grdAttachQuotes.DataBind();
        //}
        //protected void grdAttachQuotes_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        LinkButton lnkQuote = (LinkButton)e.Row.FindControl("lnkQuote");
        //    }
        //}

        protected void drpVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpVendorName = sender as DropDownList;

            GridViewRow gr = (GridViewRow)drpVendorName.Parent.Parent;

            DataSet dsVendorDetails = VendorBLL.Instance.fetchVendorDetailsByVendorId(Convert.ToInt16(drpVendorName.SelectedValue));
            Label lblContactPerson = (Label)gr.FindControl("lblContactPerson");
            lblContactPerson.Text = dsVendorDetails.Tables[0].Rows[0]["ContactPerson"].ToString();
            Label lblContactNumber = (Label)gr.FindControl("lblContactNumber");
            lblContactNumber.Text = dsVendorDetails.Tables[0].Rows[0]["ContactNumber"].ToString();
            Label lblFax = (Label)gr.FindControl("lblFax");
            lblFax.Text = dsVendorDetails.Tables[0].Rows[0]["Fax"].ToString();
            Label lblEmail = (Label)gr.FindControl("lblEmail");
            lblEmail.Text = dsVendorDetails.Tables[0].Rows[0]["Email"].ToString();
        }

    }
}