using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common;

namespace JG_Prospect
{
    public partial class ProgressReport : System.Web.UI.Page
    {
        static DateTime frmdate = new DateTime();
        static DateTime todate = new DateTime();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label3.Visible = false;
                lblCount.Visible = false;
                binduserlist();
                
                if ((string)Session["usertype"] != "Admin")
                {
                    ddlusername.SelectedValue = Session["Username"].ToString();
                    ddlusername.Enabled = false;
                }
                try
                {                 
                    DataSet ds = new DataSet();
                    ds = UserBLL.Instance.Getcurrentperioddates();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        frmdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]);
                        todate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]);
                        string username = "";
                        if (ddlusername.SelectedIndex > 0)
                        {
                            username = ddlusername.SelectedValue.ToString() == "0" ? "" : ddlusername.SelectedValue.ToString();
                        }
                        binddata(frmdate, todate, username);
                        GetProgressReport(frmdate.ToShortDateString(), todate.ToShortDateString());
                        bindPayPeriod(ds);
                        DisplayControls();
                    }
                    else
                    {
                        todate = DateTime.Now;
                        frmdate = DateTime.Now.AddDays(-14);
                    }

                }
                    
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Records Found for current period');", true);
                }
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
            DateTime fromDate = Convert.ToDateTime(txtfrmdate.Text, JG_Prospect.Common.JGConstant.CULTURE);
            DateTime toDate = Convert.ToDateTime(txtTodate.Text, JG_Prospect.Common.JGConstant.CULTURE);
            if (fromDate < toDate)
            {
                binddata(fromDate, toDate, ddlusername.SelectedValue);
            }
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
        private void bindPayPeriod(DataSet dsCurrentPeriod)
        {
            DataSet ds = UserBLL.Instance.getallperiod();

            if (ds.Tables[0].Rows.Count > 0)
            {
                drpPayPeriod.Items.Insert(0, new ListItem("Select", "0"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    drpPayPeriod.Items.Add(new ListItem(dr["Periodname"].ToString(), dr["Id"].ToString()));
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
        private void binduserlist()
        {
            DataSet DS = new DataSet();

            DS = UserBLL.Instance.getallusers(Session["usertype"].ToString());
            //foreach (DataRow dr in DS.Tables[1].Rows)
            //{
            //    ddlusername.Items.Add(dr.ItemArray[0].ToString());
            //}
            ddlusername.DataSource = DS;
            ddlusername.DataTextField = "Username";
            ddlusername.DataValueField = "Id";
            ddlusername.DataBind();
            ddlusername.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void DisplayControls()
        {
            lblTotalSeen.Visible = true;
            lblTotalSeenEST.Visible = true;
            lblProspects.Visible = true;
            lblTotalProspects.Visible = true;
            lblGrossSales.Visible = true;
            lblGrSales.Visible = true;
            lblTotalSet.Visible = true;
            lblTotalSetEST.Visible = true;
            lblre.Visible = true;
            lblRehash.Visible = true;
            lblTPDC.Visible = true;
            lblTotalProspectDataCompletion.Visible = true;
        }
       
        public static DataTable dt;
        protected void btnshow_Click(object sender, EventArgs e)
        {
         //   System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (txtfrmdate.Text != "" && txtTodate.Text!="")
            {
                frmdate = Convert.ToDateTime(txtfrmdate.Text, JG_Prospect.Common.JGConstant.CULTURE);
                todate = Convert.ToDateTime(txtTodate.Text, JG_Prospect.Common.JGConstant.CULTURE);
            }           
            string username = "";
            if (ddlusername.SelectedIndex > 0)
            {
                username = ddlusername.SelectedValue.ToString() == "0" ? "" : ddlusername.SelectedValue.ToString();
            }
            GetProgressReport(frmdate.ToShortDateString(), todate.ToShortDateString());
            binddata(frmdate, todate, username);
        }

        public int GetProgressReport(string From,string To)
        {
            DataSet dsCount = new DataSet();
            if (ddlusername.SelectedIndex == 0)
            {
                dsCount = InstallUserBLL.Instance.GetProspectCount(0, From, To);
                //dsCount = InstallUserBLL.Instance.GetProspectCount(0, txtfrmdate.Text, txtTodate.Text);
            }
            else
            {
                dsCount = InstallUserBLL.Instance.GetProspectCount(Convert.ToInt32(ddlusername.SelectedValue), txtfrmdate.Text, txtTodate.Text);
            }
            if (dsCount != null)
            {
                if (dsCount.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsCount.Tables[0].Rows[0][0]) != "")
                    {
                        Label3.Visible = true;
                        lblCount.Visible = true;
                        lblCount.Text = Convert.ToString(dsCount.Tables[0].Rows[0][0]);
                        return 1;
                    }
                    else
                    {
                        Label3.Visible = true;
                        lblCount.Visible = true;
                        lblCount.Text = "0";
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        protected void binddata(DateTime fromdate, DateTime todate, string username)
        {
            int totalSeenEST = UserBLL.Instance.CalculateTotalSeenEST(Convert.ToInt16(ddlusername.SelectedValue.ToString()), fromdate, todate);
            lblTotalSeenEST.Text = totalSeenEST.ToString();

            int totalProspect = UserBLL.Instance.CalculateTotalProspects(Convert.ToInt16(ddlusername.SelectedValue.ToString()), fromdate, todate);
            lblTotalProspects.Text = totalProspect.ToString();

            int totalSetEST = UserBLL.Instance.CalculateTotalSetEST(Convert.ToInt16(ddlusername.SelectedValue.ToString()), fromdate, todate);
            lblTotalSetEST.Text = totalSetEST.ToString();

            decimal totalRehash = UserBLL.Instance.CalculateJrRehashPercent(Convert.ToInt16(ddlusername.SelectedValue.ToString()), fromdate, todate);
            lblRehash.Text = totalRehash.ToString();

            decimal totalSale = UserBLL.Instance.CalculateJrTotalGrossSale(Convert.ToInt16(ddlusername.SelectedValue.ToString()), fromdate, todate);
            lblGrossSales.Text = totalSale.ToString();

            decimal totalData = UserBLL.Instance.CalculateJrTotalProspectDataCompletion(Convert.ToInt16(ddlusername.SelectedValue.ToString()), fromdate, todate);
            lblTotalProspectDataCompletion.Text = totalData.ToString();
            int i = 0;
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.Fetchprogressreport(fromdate, todate, username);
            i = GetProgressReport(fromdate.ToShortDateString(), todate.ToShortDateString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                DataView sortedView = new DataView(dt);
                sortedView.Sort = GridViewSortExpression + " " + GridViewSortDirection;
                grddata.DataSource = sortedView;
                grddata.DataBind();
                if ((string)Session["usertype"] == "Admin")
                {
                    btnExporttoExcel.Visible = true;
                }
                else
                {
                    btnExporttoExcel.Visible = false;
                }
            }
            else
            {
                grddata.DataSource = null;
                grddata.DataBind();
                btnExporttoExcel.Visible = false;
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Records Found');", true);
                }
            }
        }

        protected void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            DataGrid dg = new DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            ExportToExcel("ProgressReport.xls", dg);
            dg = null;
            dg.Dispose();
        }

        private void ExportToExcel(string strFileName, DataGrid dg)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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