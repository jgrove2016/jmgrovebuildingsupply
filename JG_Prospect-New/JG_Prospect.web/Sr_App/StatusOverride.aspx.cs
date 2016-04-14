using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.Configuration;
using JG_Prospect.Common;
using JG_Prospect.Common.Logger;
using System.Drawing;

namespace JG_Prospect.Sr_App
{
    public partial class StatusOverride : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alsert('Your session has expired,login to contineu');window.location='../login.aspx'", true);
            }
            if (!IsPostBack)
            {
                Session["dsToExportSrSales"] = "";
                bindgrid("","Select All","","");
                txtEstDate.Attributes.Add("readonly", "readonly");
                txtJrSalesReason.Attributes.Add("readonly", "readonly");
            }
            if (Convert.ToString(Session["usertype"]).Contains("Admin"))
            {
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = true;
            }
        }

        protected void bindgrid(string str_Search, string str_Criateria,string from,string to)
        {
            try
            {
                DataSet ds = new DataSet();
                //ds = AdminBLL.Instance.FetchALLcustomer();
                ds = AdminBLL.Instance.BindGridForSrSales(str_Search, str_Criateria,from,to);
                Session["dsToExportSrSales"] = ds;
                if(ds.Tables.Count>0)
                {
                    Session["DataTableSrSales"] = ds.Tables[0];
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdoverridestatus.DataSource = ds.Tables[0];
                        grdoverridestatus.DataBind();
                    }
                    else
                    {
                        grdoverridestatus.DataSource = null;
                        grdoverridestatus.DataBind();
                        grdoverridestatus.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "Status Override", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grdoverridestatus_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ddlSearchCustomer.SelectedIndex == 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Customer Name')", true);
                bindgrid(txtSearch.Text, "Select All", txtDurationFrom.Text, txtdurationto.Text);
            }
            else
            {
                bindgrid(txtSearch.Text, ddlSearchCustomer.SelectedValue, txtDurationFrom.Text, txtdurationto.Text);
            }
            DataTable dt = new DataTable();
            dt = (DataTable)(Session["DataTableSrSales"]);
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                grdoverridestatus.DataSource = sortedView;
                grdoverridestatus.DataBind();
            }
        }

        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        protected void grdoverridestatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //   // Find the DropDownList in the Row
            //    DropDownList ddlStatus = (e.Row.FindControl("ddlStatus") as DropDownList);
            //    //Select the status in DropDownList
            //    string Status = (e.Row.FindControl("lblStatus") as Label).Text;
            //    if (Status != "")
            //    {
            //        if (Status == "Set" || Status == "Prospect" || Status == "est>$1000" || Status == "est<$1000" || Status == "sold>$1000" || Status == "sold<$1000" || Status == "Rehash" || Status == "Cancelation-no rehash" || Status == "Material Confirmation(1)" || Status == "Procurring Quotes(2)" || Status == "Ordered(3)")
            //        {
            //            ddlStatus.Items.FindByValue(Status).Selected = true;
            //        }
            //        else
            //        {
            //            ddlStatus.Items.FindByValue("Set").Selected = true;
            //        }
            //    }
            //    else
            //    {
            //        ddlStatus.Items.FindByValue("Set").Selected = true;
            //    }
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkLatestStatus = (e.Row.FindControl("lnkLatestStatus") as LinkButton);
                string Status = lnkLatestStatus.Text;
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (Status == "Sold-in Progress")
                    {
                        cell.BackColor = Color.Red;
                    }
                    else if (Status == "Sold")
                    {
                        cell.BackColor = Color.Red;
                    }
                    else if (Status.Contains("est"))
                    {
                        cell.BackColor = Color.Black;
                        cell.ForeColor = Color.White;
                    }
                    else if (Status == "Prospect")
                    {
                        cell.BackColor = Color.Orange;
                        cell.ForeColor = Color.White;
                    }
                    else if (Status == "cancelation-no rehash")
                    {
                        cell.BackColor = Color.Gray;
                        cell.ForeColor = Color.White;
                    }
                    else if (Status == " Cancelation-no rehash")
                    {
                        cell.BackColor = Color.Gray;
                        cell.ForeColor = Color.White;
                    }
                    else if (Status == "Cancelation-no rehash")
                    {
                        cell.BackColor = Color.Gray;
                        cell.ForeColor = Color.White;
                    }
                    else if (Status == "sold>$1000")
                    {
                        cell.BackColor = Color.Red;
                    }
                    else if (Status == "sold<$1000")
                    {
                        cell.BackColor = Color.Red;
                    }
                    else
                    {
                        cell.BackColor = Color.SkyBlue;
                    }
                }
                   
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
                //DropDownList ddlmeetingstatus = (DropDownList)e.Row.FindControl("ddlmeetingstatus");
                //HiddenField hdfmeetingstatus = (HiddenField)e.Row.FindControl("hdfmeetingstatus");
               // TextBox txtfollowup = (TextBox)e.Row.FindControl("txtfollowup");

                //if (txtfollowup.Text == "01/01/1753")
                //{
                //    txtfollowup.Text = null;
                //}

                //ddlmeetingstatus.Items.Clear();
                //ddlmeetingstatus.Items.Add("Set");
                //ddlmeetingstatus.Items.Add("Prospect");
                //ddlmeetingstatus.Items.Add("est>$1000");
                //ddlmeetingstatus.Items.Add("est<$1000");
                //ddlmeetingstatus.Items.Add("sold>$1000");
                //ddlmeetingstatus.Items.Add("sold<$1000");
                //ddlmeetingstatus.Items.Add("Follow up");
                //ddlmeetingstatus.Items.Add("Closed (not sold)");
                //ddlmeetingstatus.Items.Add("Closed (sold)");
                //ddlmeetingstatus.Items.Add("Rehash");
                //ddlmeetingstatus.Items.Add("cancelation-no rehash");
                //ddlmeetingstatus.SelectedValue = hdfmeetingstatus.Value;

                //if (ddlmeetingstatus.SelectedValue == "PTW est" || ddlmeetingstatus.SelectedValue == "est>$1000" || ddlmeetingstatus.SelectedValue == "est<$1000" || ddlmeetingstatus.SelectedValue == "Follow up" || ddlmeetingstatus.SelectedValue == "EST-one legger")
                //{
                //    txtfollowup.Style.Add("display", "block");
                //}
                //else
                //{
                //    txtfollowup.Style.Add("display", "none");
                //}

            //}
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddl = (DropDownList)grow.FindControl("ddlStatus");
            Label Id = (Label)grow.FindControl("lblIdPopUpStatus");
            int QuoteId = Convert.ToInt32(Id.Text);
            string Status = ddl.SelectedValue;
            if (Status != "Set" && Status != "Closed (not sold)" && Status != "Cancelation-no rehash" && Status != "Jr sales follow up" && Status != "est>$1000" && Status != "est<$1000" && Status != "sold>$1000" && Status != "sold<$1000")
            {
                AdminBLL.Instance.UpdateStatusFromGrid(QuoteId, Status);
                bindgrid("", "Select All", "", "");
            }
            else if (Status == "Set")
            {
                Session["QuoteIdSrSalesApp"] = QuoteId;
                ddlEsttime.DataSource = GetTimeIntervals();
                ddlEsttime.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlaySetStatus();", true);
            }
            else if (Status == "Closed (not sold)")
            {
                Session["QuoteIdSrSalesApp"] = QuoteId;
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayStatusClosedNotSold();", true);
            }
            else if (Status == "Jr sales follow up")
            {
                Session["QuoteIdSrSalesApp"] = QuoteId;
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayStatusJrSales();", true);
            }
            else if (Status == "Cancelation-no rehash")
            {
                Session["QuoteIdSrSalesApp"] = QuoteId;
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayCancelationnorehash();", true);
            }
            else if (Status == "est>$1000" || Status == "est<$1000")
            {
                Session["estStatus"] = Status;
                Session["QuoteIdSrSalesApp"] = QuoteId;
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayEstDate();", true);
            }
            else if (Status == "sold>$1000" || Status == "sold<$1000")
            {
                Session["estStatus"] = Status;
                Session["QuoteIdSrSalesApp"] = QuoteId;
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlaysoldDate();", true);
            }

        }


        public List<string> GetTimeIntervals()
        {
            List<string> timeIntervals = new List<string>();
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
            for (int i = 0; i < 48; i++)
            {
                int minutesToBeAdded = 30 * i;      // Increasing minutes by 30 minutes interval
                TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
                TimeSpan t = startTime.Add(timeToBeAdded);
                DateTime result = startDate + t;
                timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
            }
            return timeIntervals;
        }

        protected void lnkcustomerid_Click(object sender, EventArgs e)
        {
            LinkButton btnid = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnid.Parent.Parent;
            string CustID = btnid.Text;
            string type = btnid.Text.Substring(0, 1);
            string id = btnid.Text.Substring(1);

            // Session["EstimateHome"] = btnEdit.Text.ToString();

            //Initially code is.....Commented by Neeta A..
            /*
            if (type == "C")
            {
                Response.Redirect("~/Sr_App/Customer_Profile.aspx?title=" + id);
            }
            else if (type == "P")
            {
                Response.Redirect("~/Prospectmaster.aspx?title=" + id);
            }*/
            //End commenting....

            Response.Redirect("~/Sr_App/Customer_Profile.aspx?CustomerId=" + CustID);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            Button btnsave = sender as Button;
            GridViewRow gr = (GridViewRow)btnsave.Parent.Parent;
            LinkButton lnkid = (LinkButton)gr.FindControl("lnkcustomerid");
            DropDownList ddlstatus = (DropDownList)gr.FindControl("ddlmeetingstatus");
            TextBox txtfollowup = (TextBox)gr.FindControl("txtfollowup");
            HiddenField hdffollowupdate = (HiddenField)gr.FindControl("hdffollowupdate");
            HiddenField hdfmeetingstatus = (HiddenField)gr.FindControl("hdfmeetingstatus");
            string followupdate = string.IsNullOrEmpty(txtfollowup.Text) ? "1/1/1753" : Convert.ToDateTime(txtfollowup.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
            string type = lnkid.Text.Substring(0, 1);
            string id = lnkid.Text.Substring(1);
            int custid = Convert.ToInt32(id);
            string oldstatus = hdfmeetingstatus.Value;
            string newstatus = ddlstatus.SelectedValue;
            bool updateresult = false;
            string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
            string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
            string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();

            Customer c = new Customer();
            c = new_customerBLL.Instance.fetchcustomer(custid);
            string productName = UserBLL.Instance.GetProductNameByProductId(c.Productofinterest);
            if (newstatus == "Set")
            {
                if (type == "C")
                {
                    Response.Redirect("~/Sr_App/Customer_Profile.aspx?title=" + id);
                }
                else if (type == "P")
                {

                    Response.Redirect("~/Prospectmaster.aspx?title=" + id);

                }
            }

            //commented for phase I, Applicable in phase II

            //else if ((oldstatus == "Prospect" || oldstatus == "Follow up" || oldstatus == "Rehash" || oldstatus == "cancelation-no rehash") && (newstatus == "PTW est" || newstatus == "est>$1000" || newstatus == "est<$1000" || newstatus == "EST-one legger" || newstatus == "sold>$1000" || newstatus == "sold<$1000" || newstatus == "Closed (not sold)" || newstatus == "Closed (sold)"))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Cannot change prospect to estimate or sold');", true);
            //    return;
            //}
            else if (newstatus == "Prospect" || newstatus == "Follow up" || newstatus == "Rehash" || newstatus == "cancelation-no rehash")
            {
                if (AdminId != c.Addedby)
                {
                    GoogleCalendarEvent.DeleteEvent(id, "", "", "", DateTime.Now, DateTime.Now, c.Addedby);
                }
                GoogleCalendarEvent.DeleteEvent(id, "", "", "", DateTime.Now, DateTime.Now, AdminId);
                GoogleCalendarEvent.DeleteEvent(id, "", "", "", DateTime.Now, DateTime.Now, JGConstant.CustomerCalendar);
            }
            else if (newstatus == "PTW est" || newstatus == "est>$1000" || newstatus == "est<$1000" || newstatus == "EST-one legger" || newstatus == "Closed (not sold)" || newstatus == "sold>$1000" || newstatus == "sold<$1000")
            {
                string gtitle = c.EstTime + " -" + c.PrimaryContact + " -" + c.Addedby;
                string gcontent = "Name: " + c.customerNm + " , Product of Interest: " + productName + ", Phone: " + c.CellPh + ", Alt. phone: " + c.AltPh + ", Email: " + c.Email + ",Notes: " + c.Notes + ",Status: " + newstatus;
                string gaddress = c.CustomerAddress + "," + c.City;
                string datetime = null;
                if (c.EstDate != "1/1/1753")
                {
                    datetime = Convert.ToDateTime(c.EstDate + " " + c.EstTime).ToString("MM/dd/yy hh:mm tt");
                }
                GoogleCalendarEvent.DeleteEvent(custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);
                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);
                if (AdminId != c.Addedby)
                {
                    GoogleCalendarEvent.DeleteEvent(custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), c.Addedby);
                    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), c.Addedby);
                }
                GoogleCalendarEvent.DeleteEvent(custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);
                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);

            }

            //commented for phase I, Applicable in phase II

            //else if (newstatus == "sold>$1000" || newstatus == "sold<$1000")
            //{
            //    Session["CustomerId"] = custid;
            //    Session["CustomerName"] = c.customerNm;
            //    Response.Redirect("~/Sr_App/ProductEstimate.aspx");
            //}          

            try
            {
                updateresult = AdminBLL.Instance.UpdateStatus(custid, newstatus, followupdate);
                int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                new_customerBLL.Instance.AddCustomerFollowUp(custid, Convert.ToDateTime(followupdate), newstatus, userId, false,0,"");
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
                //bindgrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string str_Search = string.Empty;
            if (ddlSearchCustomer.SelectedIndex == 0)
            {
                str_Search = txtSearch.Text;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Customer Name')", true);
                bindgrid(txtSearch.Text, "Select All", txtDurationFrom.Text, txtdurationto.Text);
            }
            //else 
            //{
            //    bindgrid(txtSearch.Text, ddlSearchCustomer.SelectedValue, txtDurationFrom.Text, txtdurationto.Text);
            //}
            else
            {
                bindgrid(txtSearch.Text, ddlSearchCustomer.SelectedValue, txtDurationFrom.Text, txtdurationto.Text);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sr_App/StatusOverride.aspx");
        }
        // For Export........
        protected void btnExport_Click(object sender, EventArgs e)
        {/*
            //For Export Multiple columns are shown....

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SrSales.xls"));
            Response.ContentType = "application/ms-excel";
            
             DataSet ds = InstallUserBLL.Instance.getalluserdetails();
            DataTable dt = (DataTable)(Session["DataTableSrSales"]);
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
           
            Response.End();*/
            DataTable dt = (DataTable)(Session["DataTableSrSales"]);
            
            string filename = "SrSales.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Write(tw.ToString());
            Response.End();
        }


        protected void btnSaveEst_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            int QuoteId = Convert.ToInt32(Session["QuoteIdSrSalesApp"]);
            string Date = txtEstDate.Text;
            string Time = ddlEsttime.SelectedValue;
            AdminBLL.Instance.MakeAppointments(Id, QuoteId, Date, Time,"SalesCal");
            bindgrid("", "Select All", "", "");
        }

        protected void grdoverridestatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int status = 0;
            if (e.CommandName == "Quotes")
            {
                status = bindgridQuotes(Convert.ToInt32(e.CommandArgument));
                if (status == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlay();", true);
                }
            }
            if (e.CommandName == "Jobs")
            {
                status = bindgridJobs(Convert.ToInt32(e.CommandArgument));
                if (status == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayJobs();", true);
                }
            }
            if (e.CommandName == "Status")
            {
                status = bindgridStatus(Convert.ToInt32(e.CommandArgument));
                if (status == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayStatus();", true);
                }
            }
            if (e.CommandName == "ProductCategory")
            {
                status = bindgridJobs(Convert.ToInt32(e.CommandArgument));
                if (status == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlayCategory();", true);
                }
            }

        }

        protected int bindgridStatus(int CustId)
        {
            try
            {
                DataSet ds = new DataSet();

                //ds = AdminBLL.Instance.FetchALLcustomer();
                ds = AdminBLL.Instance.BindGridForStatus(CustId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdStatus.DataSource = ds.Tables[0];
                        grdStatus.DataBind();
                        grdStatus.Visible = true;
                        return 1;
                    }
                    else
                    {
                        grdStatus.DataSource = null;
                        grdStatus.DataBind();
                        grdStatus.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                        return 0;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "Quotes Error", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
                return 0;
            }
        }


        protected int bindgridQuotes(int CustId)
        {
            try
            {
                DataSet ds = new DataSet();

                //ds = AdminBLL.Instance.FetchALLcustomer();
                ds = AdminBLL.Instance.BindGridForQuotes(CustId);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdQuotes.DataSource = ds.Tables[0];
                        grdQuotes.DataBind();
                        grdQuotes.Visible = true;
                        return 1;
                    }
                    else
                    {
                        grdQuotes.DataSource = null;
                        grdQuotes.DataBind();
                        grdQuotes.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                        return 0;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "Quotes Error", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
                return 0;
            }
        }


        protected int bindgridJobs(int CustId)
        {
            try
            {
                DataSet ds = new DataSet();

                //ds = AdminBLL.Instance.FetchALLcustomer();
                ds = AdminBLL.Instance.BindGridForJobs(CustId);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdJobsId.DataSource = ds.Tables[0];
                        grdJobsId.DataBind();
                        grdJobsId.Visible = true;
                        return 1;
                    }
                    else
                    {
                        grdJobsId.DataSource = null;
                        grdJobsId.DataBind();
                        grdJobsId.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                        return 0;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "Quotes Error", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
                return 0;
            }
        }

        protected void grdStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the DropDownList in the Row
                DropDownList ddlStatus = (e.Row.FindControl("ddlStatus") as DropDownList);
                //Select the status in DropDownList
                string Status = (e.Row.FindControl("lblStatus") as Label).Text;
                if (Status != "")
                {
                    if (Status == "Set" || Status == "Prospect" || Status == "est>$1000" || Status == "est<$1000" || Status == "sold>$1000" || Status == "sold<$1000" || Status == "Rehash" || Status == "Cancelation-no rehash" || Status == "Material Confirmation(1)" || Status == "Procurring Quotes(2)" || Status == "Ordered(3)" || Status == "Closed (not sold)" || Status == "Jr sales follow up")
                    {
                        ddlStatus.Items.FindByValue(Status).Selected = true;
                        if (Status == "sold>$1000" || Status == "sold<$1000")
                        {
                            ddlStatus.Enabled = false;
                        }
                    }
                    else
                    {
                        ddlStatus.Items.FindByValue("Set").Selected = true;
                    }
                }
                else
                {
                    ddlStatus.Items.FindByValue("Set").Selected = true;
                }
            }
        }

        protected void btnClosedSubmit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            int QuoteId = Convert.ToInt32(Session["QuoteIdSrSalesApp"]);
            string Reason = txtReason.Text;
            AdminBLL.Instance.UpdateCloseReason(QuoteId, Reason);
            bindgrid("", "Select All", "", "");
        }

        protected void btnJrFade_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            int QuoteId = Convert.ToInt32(Session["QuoteIdSrSalesApp"]);
            string Reason = txtJrSalesReason.Text;
            AdminBLL.Instance.UpdateCloseReason(QuoteId, Reason);
            bindgrid("", "Select All", "", "");
        }

        protected void btnCancelationnorehash_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            int QuoteId = Convert.ToInt32(Session["QuoteIdSrSalesApp"]);
            string Reason = txtCancelationnorehash.Text;
            AdminBLL.Instance.UpdateCloseReason(QuoteId, Reason);
            bindgrid("", "Select All", "", "");
        }

        protected void btnSoldDate_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            int QuoteId = Convert.ToInt32(Session["QuoteIdSrSalesApp"]);
            string Reason = txtJrSalesReason.Text;
            string Status = Convert.ToString(Session["estStatus"]);
            AdminBLL.Instance.UpdateSoldStatus(QuoteId, Reason,Status);
            bindgrid("", "Select All", "", "");
        }

        protected void btnEstDateSet_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
            int QuoteId = Convert.ToInt32(Session["QuoteIdSrSalesApp"]);
            string Reason = txtSetEstDate.Text;
            string Status = Convert.ToString(Session["estStatus"]);
            AdminBLL.Instance.UpdateEstDate(QuoteId, Reason,Status);
            bindgrid("", "Select All", "", "");
        }

        //protected void grdJobsId_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string Status = Convert.ToString(e.Row.Cells[3].Text);
        //        foreach (TableCell cell in e.Row.Cells)
        //        {
        //            if (Status == "Sold-in Progress")
        //            {
        //                cell.BackColor = Color.Red;
        //            }
        //            else if (Status == "Sold")
        //            {
        //                cell.BackColor = Color.Red;
        //            }
        //            else if (Status.Contains("est"))
        //            {
        //                cell.BackColor = Color.Black;
        //            }
        //            else if (Status == "Prospect")
        //            {
        //                cell.BackColor = Color.Orange;
        //            }
        //            else if (Status == "Cancelation-no rehash")
        //            {
        //                cell.BackColor = Color.Gray;
        //            }
        //            else
        //            {
        //                cell.BackColor = Color.SkyBlue;
        //            }


        //        }
        //    }
        //}


    }
}