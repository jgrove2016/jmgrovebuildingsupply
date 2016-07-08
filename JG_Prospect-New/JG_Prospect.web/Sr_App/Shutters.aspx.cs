using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.IO;
using JG_Prospect.Common;
using JG_Prospect.Common.Logger;
using AjaxControlToolkit;

namespace JG_Prospect.Sr_App
{
    public partial class Shutters : System.Web.UI.Page
    {
        ErrorLog logManager = new ErrorLog();
        int userId = 0;
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
            int ProductTypeId = 0, CustomerId = 0, ProductId = 0;
            userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            if (!IsPostBack)
            {
                bindshutters();
                bindshutter_subfields();
                if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null)
                {
                    lblmsg.Text = Request.QueryString[QueryStringKey.Key.CustomerId.ToString()].ToString();
                }
                DataSet dsCustomer = new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt16(Request.QueryString[1]));
                if (dsCustomer.Tables[0].Rows.Count > 0)
                {
                    txtCustomer.Text = dsCustomer.Tables[0].Rows[0]["CustomerName"].ToString();
                    txtCustomer.Enabled = false;
                }
                //if (Session[SessionKey.Key.CustomerName.ToString()] != null)
                //{
                //    txtCustomer.Text = Session[SessionKey.Key.CustomerName.ToString().Trim()].ToString();
                //    txtCustomer.Enabled = false;
                //}
                //else
                //{ txtCustomer.Text = Request.QueryString[0]; }
                if (Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null)
                {
                    hidProdType.Value = Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()];
                }
              
                if (ViewState[QueryStringKey.Key.Edit.ToString()] == null)
                {
                    if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null &&
                        Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null &&
                        Request.QueryString[QueryStringKey.Key.ProductId.ToString()] != null)
                    {
                        CustomerId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
                        ProductTypeId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
                        ProductId = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);

                        hidProdType.Value = ProductTypeId.ToString();
                        btnsave.Text = JGConstant.UPDATE;

                        fillestimate(CustomerId, ProductId, ProductTypeId);
                        ViewState[QueryStringKey.Key.Edit.ToString()] = JGConstant.TRUE;
                    }
                    else { btnsave.Text = JGConstant.SAVE; }
                }

                DataSet ds = new DataSet();
                ds = shuttersBLL.Instance.fetchShutterMount();
                ddlSurface_mount.DataSource = ds;
                ddlSurface_mount.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSurface_mount.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSurface_mount.DataBind();
                ddlSurface_mount.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        protected void bindshutters()
        {
            DataSet ds = new DataSet();
            ds = shuttersBLL.Instance.GetAllShutters();

            ddlshuttername.DataSource = ds.Tables[0];
            ddlshuttername.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlshuttername.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlshuttername.DataBind();
            ddlshuttername.Items.Insert(0, "Select");
            ddlShutterTop.DataSource = ds.Tables[1];
            ddlShutterTop.DataValueField = ds.Tables[1].Columns[0].ToString();
            ddlShutterTop.DataTextField = ds.Tables[1].Columns[1].ToString();
            ddlShutterTop.DataBind();
            ddlShutterTop.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void bindshutter_subfields()
        {
            if (ddlshuttername.SelectedValue != "Select")
            {
                DataSet ds = new DataSet();
                ds = shuttersBLL.Instance.GetShutter_subfields(Convert.ToInt32(ddlshuttername.SelectedValue));
                ddlColor.DataSource = ds.Tables[0];
                ddlColor.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlColor.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new ListItem("Select", ""));
                ddlWidth.DataSource = ds.Tables[1];
                ddlWidth.DataValueField = ds.Tables[1].Columns[0].ToString();
                ddlWidth.DataTextField = ds.Tables[1].Columns[1].ToString();
                ddlWidth.DataBind();
                ddlWidth.Items.Insert(0, new ListItem("Select", ""));
                lblHeight.Text = ds.Tables[1].Rows[0][2].ToString();
            }
        }
        //Save on Shutter...

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string filename;
                //List<string> locationpics = new List<string>();
                shutters objshutter = new shutters();



                if (ddlColor.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Color.')", true);
                    return;
                }

                if (Request.QueryString[QueryStringKey.Key.ProductId.ToString()] != null)
                    objshutter.estimateID = Convert.ToInt32(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);
                else
                    objshutter.estimateID = 0;
                objshutter.ProductType = Convert.ToInt32(hidProdType.Value);
                objshutter.CustomerNm = txtCustomer.Text;
                //objshutter.CustomerId = Convert.ToInt32(Session["CustomerId"]);
               // if (Session[SessionKey.Key.CustomerId.ToString()] != null)
                //{
                objshutter.CustomerId = Convert.ToInt16(Request.QueryString[1]);//Convert.ToInt32(Session[SessionKey.Key.CustomerId.ToString()]);
                //}
                objshutter.ShutterId = Convert.ToInt32(ddlshuttername.SelectedValue);
                objshutter.ShutterName = ddlshuttername.Text;
                objshutter.ShutterTopId = Convert.ToInt32(ddlShutterTop.SelectedValue);
                objshutter.Shutters_top = ddlShutterTop.Text;
                objshutter.workarea = txtWork_area.Text;

                List<CustomerLocationPic> pics = (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];

                var image = pics.AsEnumerable().Take(1);
                string mainImage = image.FirstOrDefault().LocationPicture;

                objshutter.CustomerLocationPics = pics;
                objshutter.MainImage = mainImage;

                objshutter.ColorCode = ddlColor.SelectedValue.ToString();
                objshutter.Color = ddlColor.Text;
                List<Shutter_accessories> lstaccessorie = new List<Shutter_accessories>();
                Shutter_accessories accessories = new Shutter_accessories();
                accessories.accessories = ddlaccessories.Text;
                accessories.quantity = Convert.ToInt32(txtqty.Text);
                lstaccessorie.Add(accessories);
                int value = Convert.ToInt32(hdncount.Value);
                for (int i = 1; i <= value; i++)
                {
                    Shutter_accessories item = new Shutter_accessories();
                    string accessorie = "accessories" + i.ToString();
                    string qty = "qty" + i.ToString();
                    if (Request.Form[accessorie] != null)
                    {
                        item.accessories = Request.Form[accessorie].ToString();
                        item.quantity = Convert.ToInt32(Request.Form[qty].ToString());
                        lstaccessorie.Add(item);
                    }
                }
                objshutter.ShutterAccessories = lstaccessorie;
                objshutter.surfaceofmount = Convert.ToInt32(ddlSurface_mount.SelectedValue);
                objshutter.specialinstructions = txtSpcl_inst.Text;
                objshutter.ShutterWidthId = Convert.ToInt32(ddlWidth.SelectedValue);
                objshutter.width = ddlWidth.Text;
                objshutter.height = txtHeight.Text;
                objshutter.quantity = Convert.ToInt32(txtQuantity.Text);

                objshutter.UserId = userId;
                bool result = shuttersBLL.Instance.AddShutterEstimate(objshutter);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('New Shutter estimate has been added successfully');", true);
                    clearcontrols();
                    //Response.Redirect("~/Sr_App/ProductEstimate.aspx?CustomerId=" + objshutter.CustomerId);
                    Response.Redirect("~/Sr_App/ProductEstimate.aspx");
                }
            }
            catch (Exception ex)
            {
                logManager.writeToLog(ex, "Custom", Request.ServerVariables["remote_addr"].ToString());
            }
        }

        protected void ddlshuttername_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindshutter_subfields();
        }

        protected void clearcontrols()
        {
            txtCustomer.Text = txtHeight.Text = txtSpcl_inst.Text = txtQuantity.Text = txtWork_area.Text = null;
            ddlaccessories.SelectedIndex = ddlColor.SelectedIndex = ddlshuttername.SelectedIndex = ddlShutterTop.SelectedIndex = ddlSurface_mount.SelectedIndex = ddlWidth.SelectedIndex = 0;
            ViewState[SessionKey.Key.PagedataTable.ToString()] = null;
            grvShutter.DataSource = null;
            grvShutter.DataBind();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCustomerlist(string prefixText)
        {
            List<string> Customer = new List<string>();
            DataSet dds = new DataSet();
            dds = new_customerBLL.Instance.GetAutoSuggestiveCustomers(prefixText);
            foreach (DataRow dr in dds.Tables[0].Rows)
            {
                Customer.Add(dr[0].ToString());
            }
            return Customer.ToArray();
        }

        private void fillestimate(int CustomerId,int ProductId,int  ProductTypeId)
        {
            try
            {
                List<CustomerLocationPic> customerLocationPicList = new List<CustomerLocationPic>();

                DataSet ds = new DataSet();
                ds = shuttersBLL.Instance.FetchShutterDetails(CustomerId, ProductId, ProductTypeId);
                ViewState[QueryStringKey.Key.ProductId.ToString()] = ProductId;
                btnsave.Text = "Update";
                btndelete.Visible = true;
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtCustomer.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                        ddlshuttername.SelectedValue = ds.Tables[0].Rows[0]["ShutterId"].ToString();
                        bindshutter_subfields();
                        ddlShutterTop.SelectedValue = ds.Tables[0].Rows[0]["ShutterTopId"].ToString();
                        txtWork_area.Text = ds.Tables[0].Rows[0]["WorkArea"].ToString();
                        ddlColor.SelectedValue = ds.Tables[0].Rows[0]["ShutterColorId"].ToString();
                        ddlSurface_mount.SelectedValue = ds.Tables[0].Rows[0]["SurfaceMountId"].ToString();
                        txtHeight.Text = ds.Tables[0].Rows[0]["ShutterLength"].ToString();
                        ddlWidth.SelectedValue = ds.Tables[0].Rows[0]["ShutterWidthId"].ToString();
                        txtQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                        txtSpcl_inst.Text = ds.Tables[0].Rows[0]["SpecialInstruction"].ToString();
                        //LocImg = ds.Tables[0].Rows[0]["LocationPicture"].ToString().Split(',');
                        //imgplc_Loc.ImageUrl = LocImg[0].ToString();
                    }
                }

                if (ds.Tables[1] != null)
                {
                    int count = ds.Tables[1].Rows.Count;
                    hdncount.Value = "1";
                    if (count > 0)
                    {
                        hdncount.Value = count.ToString();
                        ddlaccessories.SelectedValue = ds.Tables[1].Rows[0]["AccessorieName"].ToString();
                        txtqty.Text = ds.Tables[1].Rows[0]["Quantity"].ToString();
                        for (int i = 1; i < count; i++)
                        {
                            hdnddlaccessories.Value = hdnddlaccessories.Value + "#" + ds.Tables[1].Rows[i]["AccessorieName"].ToString();
                            hdntxtqty.Value = hdntxtqty.Value + "#" + ds.Tables[1].Rows[i]["Quantity"].ToString();
                        }
                    }
                }
                if (ds.Tables[2] != null)
                {
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        CustomerLocationPic customerLocPic = new CustomerLocationPic();
                        customerLocPic.LocationPicture = ds.Tables[2].Rows[i]["LocationPicture"].ToString();
                        customerLocPic.RowSerialNo = Convert.ToInt32(ds.Tables[2].Rows[i]["RowSerialNo"]);

                        customerLocationPicList.Add(customerLocPic);
                    }
                    ViewState[SessionKey.Key.PagedataTable.ToString()] = customerLocationPicList;
                    hidCount.Value = customerLocationPicList.Count.ToString();
                    grvShutter.DataSource = ViewState[SessionKey.Key.PagedataTable.ToString()];
                    grvShutter.DataBind();
                }
            }
            catch (Exception ex)
            {
                logManager.writeToLog(ex, "Custom", Request.ServerVariables["remote_addr"].ToString());
            }
        }

        protected void ddlaccessories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlShutterTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShutterTop.SelectedValue == "2" || ddlShutterTop.SelectedValue == "6")
            {
                foreach (ListItem a in ddlWidth.Items)
                {
                    if(a.Text.Contains("12"))
                    {
                        ddlWidth.SelectedValue = a.Value;
                        return;
                    }
                }
            }
            else if (ddlShutterTop.SelectedValue != "Select" && ddlShutterTop.SelectedValue != "1")
            {
                foreach (ListItem a in ddlWidth.Items)
                {
                    if (a.Text.Contains("14"))
                    {
                        ddlWidth.SelectedValue = a.Value;
                        return;
                    }
                }
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            bool result = shuttersBLL.Instance.DeleteShutterDetails(Convert.ToInt32(ViewState[QueryStringKey.Key.ProductId.ToString()]));
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Shutter estimate record deleted sucessfully ');", true);
                clearcontrols();
                Response.Redirect("~/Sr_App/ProductEstimate.aspx");
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in deleting records ');", true);

                return;
            }
        }

        //protected void bntAdd_Click(object sender, EventArgs e)
        //{
        //    int srNo = 1;
        //    List<CustomerLocationPic> pics = null;
        //    //string filename = Guid.NewGuid() + "-" + Path.GetFileName(FileUpload1.FileName);

        //    //FileUpload1.SaveAs(Server.MapPath("~/CustomerDocs/LocationPics/") + filename);

        //    if (ViewState[SessionKey.Key.PagedataTable.ToString()] != null)
        //    {
        //        pics = (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];
        //    }
        //    else
        //    {
        //        pics = new List<CustomerLocationPic>();
        //    }
        //    if (pics.Count > 0)
        //    {
        //        //srNo = pics.Max(p => p.RowSerialNo) + 1;                
        //        srNo = pics.Count + 1;
        //    }

        //    pics.Add(new CustomerLocationPic { RowSerialNo = srNo, LocationPicture = filename });

        //    ViewState[SessionKey.Key.PagedataTable.ToString()] = pics;
        //    hidCount.Value = srNo.ToString();
        //    grvShutter.DataSource = pics;
        //    grvShutter.DataBind();
        //}

        protected void grvShutter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRec")
            {
                int Id = Convert.ToInt32(e.CommandArgument.ToString());
                List<CustomerLocationPic> pics = (List<CustomerLocationPic>)ViewState[SessionKey.Key.PagedataTable.ToString()];
                pics.Remove(pics.FirstOrDefault(id => id.RowSerialNo == Id));
                ViewState[SessionKey.Key.PagedataTable.ToString()] = pics;
                hidCount.Value = "";
                hidCount.Value = pics.Count.ToString();
                grvShutter.DataSource = pics;
                grvShutter.DataBind();
            }
        }
        protected void btnImageUploadClick_Click(object sender, EventArgs e)
        {
            
        }
        protected void ajaxFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            if (ValidateImageUpload(Path.GetFileName(ajaxFileUpload.FileName)))
            {
                int srNo = 1;
                List<CustomerLocationPic> pics = null;
                string imageName = Path.GetFileName(ajaxFileUpload.FileName);
                /*Initially...* */
                string tempImageName = Guid.NewGuid() + "-" + imageName;
                ajaxFileUpload.SaveAs(Server.MapPath("~/CustomerDocs/LocationPics/" + tempImageName));
                tempImageName = "../CustomerDocs/LocationPics/" + tempImageName;
                imageName = tempImageName;

                //Altered By Neeta....
                //ajaxFileUpload.SaveAs(Server.MapPath("~/CustomerDocs/LocationPics/" + imageName));
               // string tempImageName = imageName;

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
                    //srNo = pics.Max(p => p.RowSerialNo) + 1;                
                    srNo = pics.Count + 1;
                }
                pics.Add(new CustomerLocationPic { RowSerialNo = srNo, LocationPicture = imageName });

                CustomerLocationPicturesList = pics;
                hidCount.Value = pics.Count == 0 ? string.Empty : pics.Count.ToString();
                grvShutter.DataSource = pics;
                grvShutter.DataBind();
            }
        }

        private bool ValidateImageUpload(string fileName)
        {
            string[] extensions = { ".gif", ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
            bool flag = false;
            for (int counter = 0; counter < extensions.Length; counter++)
            {
                if (fileName.Contains(extensions[counter]))
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
        protected void grvShutter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CustomerLocationPic dr = (CustomerLocationPic)e.Row.DataItem;
                string strImage = dr.LocationPicture;
                //((Image)(e.Row.FindControl("imglocation"))).ImageUrl = "~/CustomerDocs/LocationPics/" + strImage;
            }
        }

        protected void grvShutter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvShutter.PageIndex = e.NewPageIndex;
            grvShutter.DataSource = ViewState[SessionKey.Key.PagedataTable.ToString()];
            grvShutter.DataBind();
        }
    }
}