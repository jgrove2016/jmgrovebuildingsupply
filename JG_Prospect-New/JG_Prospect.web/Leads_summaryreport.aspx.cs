using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;

namespace JG_Prospect
{
    public partial class Leads_summaryreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               binduserlist();
               DataSet ds = new DataSet();
               ds = UserBLL.Instance.getUser(Session["loginid"].ToString().Trim());
               ddlusername.SelectedValue = ds.Tables[0].Rows[0]["Username"].ToString();
               if ((string)Session["usertype"] != "Admin" && (string)Session["usertype"] != "SSE")
               {                  
                   ddlusername.Enabled = false;
               }
               else
               {
                   ddlusername.Enabled = true;
               }

               show(DateTime.MinValue, DateTime.MinValue, ddlusername.SelectedValue.ToString());  
            }
        }
        
        private void binduserlist()
        {
            DataSet DS = new DataSet();
            ddlusername.Items.Add("All");
            DS = UserBLL.Instance.getallusers(Session["usertype"].ToString());
            foreach (DataRow dr in DS.Tables[1].Rows)
            {
             ddlusername.Items.Add(dr.ItemArray[0].ToString());               

            }
            ddlusername.DataBind();
        }


        public static DataTable dt;
        protected void btnshow_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime? fromdate = Convert.ToDateTime(txtfrmdate.Text);
            DateTime? todate = Convert.ToDateTime(txtTodate.Text);
            string username = "";
            if (ddlusername.SelectedIndex >= 0)
            {
                username = ddlusername.SelectedValue.ToString();
            }
            show(fromdate, todate, username);                        
        }

        protected void show(DateTime? fromdate, DateTime? todate, string username)
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.Fetchleadssummary(fromdate, todate, username);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                grddata.DataSource = ds.Tables[0];
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Records Found');", true);
            }
        }

        protected void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            DataGrid dg = new DataGrid();
            dg.DataSource = dt;
            dg.DataBind();            
            ExportToExcel("SummaryReport.xls", dg);
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

        protected void lnkprospectid_Click(object sender, EventArgs e)
        {
            LinkButton lnkid = sender as LinkButton;
            Response.Redirect("~/Prospectmaster.aspx?title=" + lnkid.Text);
        }
    }
}