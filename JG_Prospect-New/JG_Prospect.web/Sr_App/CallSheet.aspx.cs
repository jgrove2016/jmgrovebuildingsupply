using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.Configuration;
using JG_Prospect.Common;

namespace JG_Prospect.Sr_App
{
    public partial class CallSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindgrid("onload");
                ViewState["check"] = true;
                if (Request.QueryString["FileToOpen"] != null)
                {
                    string FileToOpen = Request.QueryString["FileToOpen"].Replace("jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com~", "..");
                    //ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + Request.QueryString["FileToOpen"].ToString() + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + FileToOpen + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                }
            }
        }

        protected void bindgrid(string st)
        {
            prospect objprospect = new prospect();
            string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
            objprospect.user = Convert.ToString(Session["loginid"]);
            objprospect.usertyp = Session["usertype"].ToString();
            objprospect.status = "onload";
            DataSet ds = new DataSet();
            ds = new_customerBLL.Instance.FetchCustomerCallSheet(objprospect.status, objprospect.user, objprospect.usertyp);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdcallsheet.DataSource = ds.Tables[0];
                    grdcallsheet.DataBind();
                }
                else
                {
                    grdcallsheet.DataSource = null;
                    grdcallsheet.DataBind();
                    divcallsheet.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                }
            }
            string log = Convert.ToString(Session["loginid"]);
            //if (Session["loginid"].ToString() == AdminId)
            if (log == AdminId)
            {
                btnexporttoexcel.Visible = true;
            }
            else
            {
                btnexporttoexcel.Visible = false;
            }
        }

        protected void ddlmeetingstatus_selectedindexchanged(object sender, EventArgs e)
        {
            DropDownList ddlstatus = sender as DropDownList;
            GridViewRow gr = (GridViewRow)ddlstatus.Parent.Parent;
            LinkButton lnkid = (LinkButton)gr.FindControl("lnkcustomerid");
            TextBox txtfollowup = (TextBox)gr.FindControl("txtfollowup");
            HiddenField hdffollowupdate = (HiddenField)gr.FindControl("hdffollowupdate");
            string newstatus = ddlstatus.SelectedItem.Text;
            if (newstatus == "est<$1000" || newstatus == "est>$1000" )
            {
               // txtfollowup.Text = hdffollowupdate.Value;
                txtfollowup.Text = Convert.ToString(DateTime.Now.AddDays(7).ToShortDateString ());
                txtfollowup.Visible = true;
            }
            else
            {
                txtfollowup.Visible = false;
            }
            AutoSave(sender, e,JGConstant.DROPDOWNLIST);
            bindgrid("onload");
        }

       // protected void btnsave_Click(object sender, EventArgs e)
        protected void AutoSave(object sender, EventArgs e,string control)
        {
           // Button btnsave = sender as Button;
            GridViewRow gr = null;
            if (control == JGConstant.DROPDOWNLIST)
            {
                DropDownList ddlmeetingstatus = sender as DropDownList;
                gr = (GridViewRow)ddlmeetingstatus.Parent.Parent;
            }
            else if (control == JGConstant.TEXTBOX)
            {
                TextBox txtfollow = sender as TextBox;
                gr = (GridViewRow)txtfollow.Parent.Parent;
            }
            //GridViewRow gr = (GridViewRow)btnsave.Parent.Parent;
            LinkButton lnkid = (LinkButton)gr.FindControl("lnkcustomerid");
            TextBox txtfollowup = (TextBox)gr.FindControl("txtfollowup");
            HiddenField hdffollowupdate = (HiddenField)gr.FindControl("hdffollowupdate");
            DropDownList ddlstatus = (DropDownList)gr.FindControl("ddlmeetingstatus");
            DateTime followupdate = (txtfollowup.Text != "") ? Convert.ToDateTime(txtfollowup.Text,JGConstant.CULTURE) : DateTime.Now.AddDays(7);
            string followDate = string.IsNullOrEmpty(txtfollowup.Text) ? DateTime.Now.AddDays(7).ToString("MM/dd/yyyy") : Convert.ToDateTime(txtfollowup.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
            int custid = Convert.ToInt32(lnkid.Text);
            string newstatus = ddlstatus.SelectedItem.Text;
            string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
            string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
            string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
            Customer c = new Customer();
            c = new_customerBLL.Instance.fetchcustomer(custid);
            string productName = UserBLL.Instance.GetProductNameByProductId(c.Productofinterest);
            if (ddlstatus.SelectedValue == "Set")
            {
                Response.Redirect("~/Sr_App/Customer_Profile.aspx?title=" + lnkid.Text);
            }
            else if (newstatus == "Rehash" || newstatus == "cancelation-no rehash")
            {
                GoogleCalendarEvent.DeleteEvent(custid.ToString(), "", "", "", DateTime.Now, DateTime.Now, AdminId);
                if (AdminId != c.Addedby)
                    GoogleCalendarEvent.DeleteEvent(custid.ToString(), "", "", "", DateTime.Now, DateTime.Now, c.Addedby);
                GoogleCalendarEvent.DeleteEvent(custid.ToString(), "", "", "", DateTime.Now, DateTime.Now, JGConstant.CustomerCalendar);
            }
            if (newstatus == "est>$1000" || newstatus == "est<$1000")
            {
                string gtitle = c.EstTime + " -" + c.PrimaryContact + " -" + c.Addedby;
                string gcontent = "Name: " + c.customerNm + " , Product of Interest: " + productName + ", Phone: " + c.CellPh + ", Alt. phone: " + c.AltPh + ", Email: " + c.Email + ",Notes: " + c.Notes + ",Status: " + newstatus;
                string gaddress = c.CustomerAddress + "," + c.City;
                string datetime = null;
                if (c.EstDate != "1/1/1753")
                    if (c.EstDate != "")
                        if (c.EstDate != null)
                {
                    datetime = Convert.ToDateTime(c.EstDate + " " + c.EstTime).ToString("MM/dd/yy hh:mm tt");
                }
                GoogleCalendarEvent.DeleteEvent(custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), AdminId);
                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), AdminId);
                if (AdminId != c.Addedby)
                {
                    GoogleCalendarEvent.DeleteEvent(custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), c.Addedby);
                    GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), c.Addedby);
                }
                GoogleCalendarEvent.DeleteEvent(custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), JGConstant.CustomerCalendar);
                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), custid.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime, JGConstant.CULTURE), Convert.ToDateTime(datetime, JGConstant.CULTURE).AddHours(1), JGConstant.CustomerCalendar);

            }

            else if (newstatus == "sold>$1000" || newstatus == "sold<$1000")
            {
                Session["CustomerId"] = custid;
                Session["CustomerName"] = c.customerNm;
                Response.Redirect("~/Sr_App/ProductEstimate.aspx");
            }
            else if (newstatus == "Closed (not sold)")
            {
                // save reason of closed
            }

            //foreach (ListItem item in ddlstatus.Items)
            //{
            //    ListItem i = ddlstatus.Items.FindByValue(item.Value);
            //    if (i.Text == "est>$1000" || i.Text == "est<$1000" || i.Text == "sold>$1000" || i.Text == "sold<$1000")
            //    {
            //        i.Attributes.Add("style", "color:gray;");
            //        i.Attributes.Add("disabled", "true");
            //        i.Value = "-1";
            //    }
            //}
            int userId= Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString ()].ToString());
            AdminBLL.Instance.UpdateStatus(custid, ddlstatus.SelectedItem.Text, followDate);
            new_customerBLL.Instance.AddCustomerFollowUp(custid, Convert.ToDateTime(followupdate), ddlstatus.SelectedItem.Text, userId, false,0);
            bindgrid("onload");
        }
        protected void txtfollowup_TextChanged(object sender, EventArgs e)
        {
            AutoSave(sender, e,JGConstant.TEXTBOX);
            bindgrid("onload");
        }
        protected void btnexporttoexcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();

                Response.ContentType = "application/excel";
                string filenam = "CallSheet" + Guid.NewGuid().ToString().Substring(0, 5) + ".xls";
                Response.AddHeader("content-disposition", "attachment; filename=" + filenam);

                StringBuilder sb = new StringBuilder();
                string style = @"<style type='text/css'>
        .CellText
        {
        width:30%;
        text-align:left !important;
        padding-right:4px !important; 
        padding-left:6px !important; 
        border-bottom:1px solid White;
        background-color:#1779CD; 
        color:White;
        }
         .CellText1
        {
        width:50%;
         border-bottom:1px solid black;
        text-align:left;
        padding-left:10px !important; 
        background-color:White; 
        color:Black;
        }
        .tabel_scroll td
        {
            padding:0;
            text-align:left;
        }</style> ";

                HttpContext.Current.Response.Write(style);
                StringWriter sw = new StringWriter(sb.Append("<table cellpadding='0' cellspacing='0' width='450px border='0'><tr><td style='text-transform:uppercase;'><h2>Call Sheet</h2>"));

                HtmlTextWriter htw = new HtmlTextWriter(sw);

                System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();

                Controls.Add(form);
                form.Controls.Add(grdcallsheet);
                grdcallsheet.Columns[0].Visible = false;
                grdcallsheet.Columns[6].Visible = false;
                form.RenderControl(htw);
                sw.Close();
                Response.Write(sw.ToString() + "</td></tr></table>");
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //
            }
        }

        protected void hdffollowupdate_OnDataBinding(object sender, System.EventArgs e)
        {
            HiddenField hdffollowupdate = (HiddenField)(sender);
            if (Eval("MeetingDate").ToString() != "")
            {
                if (Eval("MeetingDate") != null)
                {
                    hdffollowupdate.Value = Convert.ToDateTime(Eval("MeetingDate")).ToString("MM/dd/yyyy");
                }
                else
                {
                    hdffollowupdate.Value = "";
                }
            }
            else
            {
                hdffollowupdate.Value = "";
            }

        }
        protected void lnkcustomerid_Click(object sender, EventArgs e)
        {
            LinkButton btnid = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnid.Parent.Parent;
            // Session["EstimateHome"] = btnEdit.Text.ToString();
          //  Response.Redirect("~/Sr_App/Customer_Profile.aspx?title=" + btnid.Text);
            Response.Redirect("~/Sr_App/Customer_Profile.aspx?CustomerId=" + btnid.Text);
        }

        protected void grdcallsheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlmeetingstatus = (DropDownList)e.Row.FindControl("ddlmeetingstatus");
                HiddenField hdfmeetingstatus = (HiddenField)e.Row.FindControl("hdfmeetingstatus");
                GridViewRow gr = (GridViewRow)ddlmeetingstatus.Parent.Parent;
                LinkButton lnkid = (LinkButton)gr.FindControl("lnkcustomerid");
                TextBox txtfollowup = (TextBox)gr.FindControl("txtfollowup");
                HiddenField hdffollowupdate = (HiddenField)gr.FindControl("hdffollowupdate");
                HiddenField hdnCustomerColor = (HiddenField)gr.FindControl("hdnCustomerColor");
                Label lblCustomerNm = (Label)gr.FindControl("lblCustomerNm");
                DateTime followupdate = (hdffollowupdate.Value != "1753-01-01" && hdffollowupdate.Value != "") ? Convert.ToDateTime(hdffollowupdate.Value, JGConstant.CULTURE) : DateTime.Now.AddDays(7);
                hdffollowupdate.Value = followupdate.ToString("MM/dd/yyyy");

                ddlmeetingstatus.Items.Clear();
                ddlmeetingstatus.Items.Add("Set");
                ddlmeetingstatus.Items.Add("est>$1000");
                ddlmeetingstatus.Items.Add("est<$1000");
                ddlmeetingstatus.Items.Add("sold>$1000");
                ddlmeetingstatus.Items.Add("sold<$1000");
                ddlmeetingstatus.Items.Add("Closed (not sold)");
                //ddlmeetingstatus.Items.Add("Closed (sold)");
                ddlmeetingstatus.Items.Add("Rehash");
                ddlmeetingstatus.Items.Add("cancelation-no rehash");
                ddlmeetingstatus.SelectedValue = hdfmeetingstatus.Value;

                if (ddlmeetingstatus.SelectedValue == "est>$1000" || ddlmeetingstatus.SelectedValue == "est<$1000")
                {
                    txtfollowup.Visible = true;
                    if (hdffollowupdate.Value != "")
                    {
                        followupdate = Convert.ToDateTime(hdffollowupdate.Value, JGConstant.CULTURE);
                        txtfollowup.Text = hdffollowupdate.Value;
                    }
                    else
                    {
                        txtfollowup.Text = "";
                        followupdate = DateTime.Now.AddDays(7);
                    }
                }

                if (hdnCustomerColor.Value.ToString() == "grey")
                {
                    gr.ForeColor = System.Drawing.Color.Gray;
                    ddlmeetingstatus.ForeColor = System.Drawing.Color.Gray;
                    txtfollowup.ForeColor = System.Drawing.Color.Gray;
                }
                 //   lblCustomerNm.ForeColor = System.Drawing.Color.Gray;
                else if (hdnCustomerColor.Value.ToString() == "red")
                {
                    gr.ForeColor = System.Drawing.Color.Red;
                    ddlmeetingstatus.ForeColor = System.Drawing.Color.Red;
                    txtfollowup.ForeColor = System.Drawing.Color.Red;
                }
                //   lblCustomerNm.ForeColor = System.Drawing.Color.Red;
                else
                {
                    gr.ForeColor = System.Drawing.Color.Black;
                    ddlmeetingstatus.ForeColor = System.Drawing.Color.Black;
                    txtfollowup.ForeColor = System.Drawing.Color.Black;
                }
                 //   lblCustomerNm.ForeColor = System.Drawing.Color.Black;
            }
        }

    }
}