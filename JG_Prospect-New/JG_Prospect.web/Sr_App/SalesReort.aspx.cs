using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.Text;
using System.IO;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;

namespace JG_Prospect.Sr_App
{
    public partial class SalesReort : System.Web.UI.Page
    {

        public string GridViewSortExpression
        {
            get
            {
                return ViewState[SessionKey.SortExpression] == null ? JGConstant.Sorting_UserName : ViewState[SessionKey.SortExpression] as string;
            }
            set
            {
                ViewState[SessionKey.SortExpression] = value;
            }
        }

        public String GridViewSortDirection
        {
            get
            {
                if (ViewState[SessionKey.SortDirection] == null)
                    ViewState[SessionKey.SortDirection] = JGConstant.Sorting_SortDirection_DESC;

                return (string)ViewState[SessionKey.SortDirection];
            }
            set { ViewState[SessionKey.SortDirection] = value; }
        }
     

        private static string UserType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string loginId = string.Empty;
            if (!IsPostBack)
            {
                UserType =Session [JG_Prospect .Common.SessionKey.Key.usertype.ToString ()].ToString ();
                if (UserType == JG_Prospect.Common.JGConstant.USER_TYPE_ADMIN)
                {
                    bindAllUsers();
                }
                else
                {
                    loginId = Session[JG_Prospect.Common.SessionKey.Key.loginid.ToString()].ToString();
                    DataSet ds = ds = UserBLL.Instance.getUser(loginId.Trim ());
                   // string userName = ds.Tables[0].Rows[0]["Username"].ToString();
                    //int userId = Convert.ToInt16 (ds.Tables[0].Rows[0]["Id"].ToString());
                    drpUser.DataSource = ds;
                    drpUser.DataTextField = "Username";
                    drpUser.DataValueField = "Id";
                    drpUser.DataBind();
                    drpUser.SelectedIndex = 1;

                }
                HideControls();
                DisplayControls();
                //DateTime fromDate = Convert.ToDateTime("01-01-1753");
                //DateTime toDate = DateTime .Now ;
                DataSet dsCurrentPeriod = UserBLL.Instance.Getcurrentperioddates();
                DateTime fromDate = Convert.ToDateTime(dsCurrentPeriod.Tables[0].Rows[0]["FromDate"].ToString());
                DateTime toDate = Convert.ToDateTime(dsCurrentPeriod.Tables[0].Rows[0]["ToDate"].ToString());
                bindDataOnPageLoad(fromDate, toDate,UserType );
                bindPayPeriod(dsCurrentPeriod);
            }
        }

        private void bindPayPeriod(DataSet dsCurrentPeriod)
        {
            DataSet ds = UserBLL.Instance.getallperiod();
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpPayPeriod.Items.Insert(0, new ListItem("Select", "0"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    drpPayPeriod.Items.Add(new ListItem(dr["Periodname"].ToString(),dr["Id"].ToString()));
                }
                drpPayPeriod.SelectedValue = dsCurrentPeriod.Tables[0].Rows[0]["Id"].ToString();
                txtfrmdate.Text = Convert.ToDateTime(dsCurrentPeriod.Tables[0].Rows[0]["FromDate"].ToString()).ToString("MM/dd/yyyy"); 
                txtTodate.Text = Convert.ToDateTime(dsCurrentPeriod.Tables[0].Rows[0]["ToDate"].ToString()).ToString("MM/dd/yyyy");
            }
            else
            {
                drpPayPeriod.DataSource = null;
                drpPayPeriod.DataBind();
            }

        }
        private void bindDataOnPageLoad(DateTime fromDate, DateTime toDate, string usertype)
        {
           
            DataSet ds = null;
            if (usertype == JG_Prospect.Common.JGConstant.USER_TYPE_ADMIN)
            {
                ds = UserBLL.Instance.FetchSalesReport(0, fromDate, toDate);
                int leadsIssued = UserBLL.Instance.CalculateLeadsIssued(0, fromDate, toDate);
                lblLeadsIssued.Text = leadsIssued.ToString();
                decimal grossSales = UserBLL.Instance.CalculateGrossSalesOfUser(0, fromDate, toDate);
                lblGrossSales.Text = "$" + grossSales.ToString();
                decimal overAllClosingPercent = UserBLL.Instance.CalculateOverAllClosingPercent(0, fromDate, toDate);
                lblOverallClosing.Text = overAllClosingPercent.ToString();
                int leadsSeen = UserBLL.Instance.CalculateLeadsSeen(0, fromDate, toDate);
                lblLeadsSeen.Text = leadsSeen.ToString();
                decimal rehashPercent=0;
                if(leadsSeen!=0)
                    rehashPercent = leadsIssued / leadsSeen;
                lblRehash.Text = rehashPercent.ToString();
            }
            else
            {
                ds = UserBLL.Instance.FetchSalesReport(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                int leadsIssued = UserBLL.Instance.CalculateLeadsIssued(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblLeadsIssued.Text = leadsIssued.ToString();
                decimal grossSales = UserBLL.Instance.CalculateGrossSalesOfUser(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblGrossSales.Text = "$" + grossSales.ToString();
                decimal overAllClosingPercent = UserBLL.Instance.CalculateOverAllClosingPercent(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblOverallClosing.Text = overAllClosingPercent.ToString();
                int leadsSeen = UserBLL.Instance.CalculateLeadsSeen(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblLeadsSeen.Text = leadsSeen.ToString();
                decimal rehashPercent = 0;
                if (leadsSeen != 0)
                    rehashPercent = leadsIssued / leadsSeen;
                lblRehash.Text = rehashPercent.ToString();
            }
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                DataView sortedView = new DataView(dt);
                sortedView.Sort = GridViewSortExpression + " " + GridViewSortDirection;
                grddata.DataSource = sortedView;
                grddata.DataBind();
                btnExporttoExcel.Visible = true;
            }
            else
            {
                grddata.DataSource = null;
                grddata.DataBind();
                btnExporttoExcel.Visible = false;
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Records Found');", true);
            }
        }

        protected void grddata_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            if (GridViewSortDirection == JGConstant.Sorting_SortDirection_ASC)
            {
                GridViewSortDirection = JGConstant.Sorting_SortDirection_DESC;
            }
            else
            {
                GridViewSortDirection = JGConstant.Sorting_SortDirection_ASC;
            };
             DateTime fromDate = Convert.ToDateTime(txtfrmdate.Text,JG_Prospect.Common.JGConstant.CULTURE);
            DateTime toDate = Convert.ToDateTime(txtTodate.Text, JG_Prospect.Common.JGConstant.CULTURE);
            if (fromDate < toDate)
            {
                bindDataOnPageLoad(fromDate, toDate, UserType);
            }
        }
        public static DataTable dt;
        protected void btnshow_Click(object sender, EventArgs e)
        {
            DateTime fromDate = Convert.ToDateTime(txtfrmdate.Text,JG_Prospect.Common.JGConstant.CULTURE);
            DateTime toDate = Convert.ToDateTime(txtTodate.Text, JG_Prospect.Common.JGConstant.CULTURE);
            if (fromDate < toDate)
            {
                int leadsIssued = UserBLL.Instance.CalculateLeadsIssued(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                // DisplayControls();
                lblLeadsIssued.Text = leadsIssued.ToString();
                decimal grossSales = UserBLL.Instance.CalculateGrossSalesOfUser(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblGrossSales.Text = "$" + grossSales.ToString();
                decimal overAllClosingPercent = UserBLL.Instance.CalculateOverAllClosingPercent(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblOverallClosing.Text = overAllClosingPercent.ToString();
                int leadsSeen = UserBLL.Instance.CalculateLeadsSeen(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
                lblLeadsSeen.Text = leadsSeen.ToString();
                decimal rehashPercent = 0;
                if (leadsSeen != 0)
                    rehashPercent = leadsIssued / leadsSeen;
                lblRehash.Text = rehashPercent.ToString();

                bindGrid(fromDate, toDate);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('ToDate must be greater than FromDate');", true);
            }
        }

        private void bindGrid(DateTime fromDate, DateTime toDate)
        {
            DataSet ds = null;
            ds = UserBLL.Instance.FetchSalesReport(Convert.ToInt16(drpUser.SelectedValue), fromDate, toDate);
            //if (ds.Tables[0].Rows.Count <= 0)
            //{
            //    ds = null;
            //}
            //grddata.DataSource = ds;
            //grddata.DataBind();
   
            if (ds.Tables[0].Rows.Count > 0)
            {
              
                grddata.DataSource = ds;
                grddata.DataBind();
                btnExporttoExcel.Visible = true;
            }
            else
            {
                grddata.DataSource = null;
                grddata.DataBind();
                btnExporttoExcel.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Records Found');", true);
            }
        }
        protected void bindAllUsers()
        {
            DataSet ds = UserBLL.Instance.getSrusers();
            drpUser.DataSource = ds;
            drpUser.DataTextField = "Username";
            drpUser.DataValueField = "Id";
            drpUser.DataBind();
            drpUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            drpUser.SelectedIndex = 0;
        }

        protected void HideControls()
        {
            lblLeads.Visible = false;
            lblLeadsIssued.Visible = false;
            lblOClosing.Visible = false;
            lblOverallClosing.Visible = false;
            lblGrossSales.Visible = false;
            lblGrSales.Visible = false;
            lblLdSeen.Visible = false;
            lblLeadsSeen.Visible = false;
            lblTMProfit.Visible = false;
            lblTotalMeanProfit.Visible = false;
            lblre.Visible = false;
            lblRehash.Visible = false;
        }

        protected void DisplayControls()
        {
            lblLeads.Visible = true;
            lblLeadsIssued.Visible = true;
            lblOClosing.Visible = true;
            lblOverallClosing.Visible = true;
            lblGrossSales.Visible = true;
            lblGrSales.Visible = true;
            lblLdSeen.Visible = true;
            lblLeadsSeen.Visible = true;
            //lblTMProfit.Visible = true;
            //lblTotalMeanProfit.Visible = true;
            lblre.Visible = true;
            lblRehash.Visible = true;
        }
       

        //protected void btnExporttoExcel_Click(object sender, EventArgs e)
        //{
        //    DataGrid dg = new DataGrid();
        //    dg.DataSource = dt;
        //    dg.DataBind();
        //    ExportToExcel("SalesReport.xls", dg);
        //    dg = null;
        //    dg.Dispose();
        //}
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + "SalesReport.xls");
            Response.ContentType = "application/excel";
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //dg.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();

            StringBuilder sb = new StringBuilder();

            StringWriter sw = new StringWriter(sb);

            HtmlTextWriter htw = new HtmlTextWriter(sw);

            System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();

            for (int i = 0; i < grddata.Rows.Count; i++)
            {
                //Apply text style to each Row
                grddata.Rows[i].Cells[8].Attributes.Add("class", "textmode");
            }

            Controls.Add(form);
            form.Controls.Add(grddata);
            form.RenderControl(htw);

            string sTemp = sw.ToString();
            sTemp = sTemp.Replace("href=", "");
            string style = @"<style>.textmode{mso-number-format:'\@'}</style>";
            Response.Write(style);

            sw.Close();
            //Response.Write(sw.ToString());            
            Response.Write(sTemp);
            Response.Flush();
            Response.End();
        }

        protected void grddata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkJobPacket = (LinkButton)e.Row.FindControl("lnkJobPacket");

                if (lnkJobPacket.Text.StartsWith("C"))
                {
                    // grddata.Rows[e.Row.RowIndex-1].ForeColor =
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lnkJobPacket.Enabled = false;
                }

             }
        }
        public static class ResponseHelper
        {
            public static void Redirect(string url, string target, string windowFeatures)
            {
                HttpContext context = HttpContext.Current;

                if ((String.IsNullOrEmpty(target) ||
                    target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
                    String.IsNullOrEmpty(windowFeatures))
                {

                    context.Response.Redirect(url);
                }
                else
                {
                    Page page = (Page)context.Handler;
                    if (page == null)
                    {
                        throw new InvalidOperationException(
                            "Cannot redirect to new window outside Page context.");
                    }
                    url = page.ResolveClientUrl(url);

                    string script;
                    if (!String.IsNullOrEmpty(windowFeatures))
                    {
                        script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                    }
                    else
                    {
                        script = @"window.open(""{0}"", ""{1}"");";
                    }

                    script = String.Format(script, url, target, windowFeatures);
                    ScriptManager.RegisterStartupScript(page,
                        typeof(Page),
                        "Redirect",
                        script,
                        true);
                }
            }
        }
        protected void lnkJobPacket_Click(object sender, EventArgs e)
        {
            LinkButton lnkJobPacket = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnkJobPacket.Parent.Parent;
            HiddenField hdnProductTypeId = (HiddenField)row.FindControl("hdnProductTypeId");
            int productId = Convert.ToInt16 (hdnProductTypeId.Value);
            
            Label lblProductType = (Label)row.FindControl("lblProductType");
            string product = lblProductType.Text;
            int productTypeId = 0;
            if (product == JG_Prospect.Common.JGConstant.PRODUCT_CUSTOM)
            {
                productTypeId = (int)JG_Prospect.Common.JGConstant.ProductType.custom;
            }
            else
            {
                productTypeId = (int)JG_Prospect.Common.JGConstant.ProductType.shutter;
            }
            int customerId = Convert.ToInt16(lnkJobPacket.Text.Substring(1).Split ('-')[0]);
            ResponseHelper.Redirect("ZipFile.aspx?customerId=" + customerId +"&productId=" + productId +"&productTypeId=" + productTypeId , "_blank", "menubar=0,width=605px,height=630px");
               
        }
        protected void lnkCustomerId_Click(object sender, EventArgs e)
        {
            LinkButton lnkCustomerId = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnkCustomerId.Parent.Parent;

            LinkButton lnkJobPacket = (LinkButton)row.FindControl("lnkJobPacket");

           
            int customerId = Convert.ToInt16(lnkJobPacket.Text.Substring(1).Split('-')[0]);
            Response.Redirect("~/Sr_App/Customer_Profile.aspx?CustomerId=" + customerId);

        }

        protected void drpPayPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpPayPeriod.SelectedIndex != -1)
            {
                DataSet ds = UserBLL.Instance.getperioddetails(Convert.ToInt16(drpPayPeriod.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtfrmdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"].ToString()).ToString("MM/dd/yyyy");
                    txtTodate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"].ToString()).ToString("MM/dd/yyyy");
                }
            }
        }

        protected void txtfrmdate_TextChanged(object sender, EventArgs e)
        {
            drpPayPeriod.SelectedIndex = -1;
        }

        protected void txtTodate_TextChanged(object sender, EventArgs e)
        {
            drpPayPeriod.SelectedIndex = -1;
        }
    }
}