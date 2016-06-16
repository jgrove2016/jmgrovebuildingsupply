using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common;
using System.IO;
using System.Text;
using JG_Prospect.Common.modal;
using System.Configuration;

namespace JG_Prospect
{
    public partial class StaticReport : System.Web.UI.Page
    {
        static string LoginSession = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindgrid("onload");
                ViewState["check"] = true;
               LoginSession=Convert.ToString(Session["loginid"]);
            }
        }

        protected void bindgrid(string st)
        {
            prospect objprospect = new prospect();
            string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
            //objprospect.firstname = txt_fname.Text;
            //objprospect.lastname = txtlastname.Text;
            //objprospect.cellphone = txtphone.Text;
            //objprospect.email = txtemail.Text;
            //objprospect.followup_date = txtfollowupdate.Text;
            objprospect.user = Convert.ToString(Session["loginid"]);
            objprospect.usertyp = Convert.ToString(Session["usertype"]);
            //if ((string)Session["usertype"] == "SSE")
            //{
            //    objprospect.user = "";
            //    objprospect.usertyp = "SSE";
            //}
            //else if ((string)Session["usertype"] == "Admin")
            //{
            //    objprospect.user = "";
            //    objprospect.usertyp = "Admin";
            //}
            //else 
            //{
            //    objprospect.user = Session["loginid"].ToString();
            //    objprospect.usertyp = Session["usertype"].ToString();
            //}

            objprospect.status = "onload";
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.Fetchstaticreport(objprospect);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdstaticdata.DataSource = ds.Tables[0];
                grdstaticdata.DataBind();
            }
            else
            {
                grdstaticdata.DataSource = null;
                grdstaticdata.DataBind();
                divcallsheet.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
            }
            string log = Convert.ToString(Session["loginid"]);
            if (log == AdminId)
            {
                Btnexcel.Visible = true;
            }
            else
            {
                Btnexcel.Visible = false;
            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid("");
            ViewState["check"] = false;
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            //txt_fname.Text = txtemail.Text = txtfollowupdate.Text =  txtlastname.Text = txtphone.Text = null;
            bindgrid("onload");
            ViewState["check"] = true;
        }

        protected void lnkestimateid_Click(object sender, EventArgs e)
        {
            LinkButton btnEdit = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnEdit.Parent.Parent;
            // Session["EstimateHome"] = btnEdit.Text.ToString();
            Response.Redirect("~/Prospectmaster.aspx?title=" + btnEdit.Text);
        }
        private void bindstatus(int row)
        {
            GridViewRow gr = grdstaticdata.Rows[row];
            DropDownList ddlmeetingstatus = (DropDownList)gr.FindControl("ddlmeetingstatus");
            ddlmeetingstatus.Enabled = true;
            string status = ddlmeetingstatus.SelectedValue;
            ddlmeetingstatus.Items.Clear();
            if ((string)Session["usertype"] == "SSE" || (string)Session["usertype"] == "Admin")
            {
                ddlmeetingstatus.Items.Add("Set");
                ddlmeetingstatus.Items.Add("Prospect");
                ddlmeetingstatus.Items.Add("Rehash");
                ddlmeetingstatus.Items.Add("cancelation-no rehash");
                ddlmeetingstatus.Items.Add("Follow up");
                ddlmeetingstatus.SelectedValue = status;
            }
            else
            {

                ddlmeetingstatus.Items.Add("Set");
                ddlmeetingstatus.Items.Add("Prospect");
                ddlmeetingstatus.Items.Add("Rehash");
                ddlmeetingstatus.Items.Add("cancelation-no rehash");
                ddlmeetingstatus.Items.Add("Follow up");
                ddlmeetingstatus.SelectedValue = status;
                foreach (ListItem item in ddlmeetingstatus.Items)
                {
                    ListItem i = ddlmeetingstatus.Items.FindByValue(item.Value);
                    if (i.Text == "Rehash" || i.Text == "cancelation-no rehash")
                    {
                        i.Attributes.Add("style", "color:gray;");
                        i.Attributes.Add("disabled", "true");
                        i.Value = "-1";
                    }
                }
            }
        }
        protected void grdstaticdata_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gr = grdstaticdata.Rows[e.RowIndex];
            LinkButton lbestimateid = (LinkButton)gr.FindControl("lnkestimateid");
            DropDownList ddlmeetingstatus = (DropDownList)gr.FindControl("ddlmeetingstatus");
            TextBox txtfollowupdate = (TextBox)gr.FindControl("txtfollowup");
            bool result = UserBLL.Instance.updateProspectstatus(Convert.ToInt32(lbestimateid.Text), ddlmeetingstatus.SelectedValue, Convert.ToDateTime(txtfollowupdate.Text));
            grdstaticdata.EditIndex = -1;
            if (Convert.ToBoolean(ViewState["check"]) == true)
                bindgrid("onload");
            else
                bindgrid("");
            Upgrdstaticdata.Update();
            if (ddlmeetingstatus.SelectedValue == "")
            {
                Response.Redirect("~/Prospectmaster.aspx?title=" + lbestimateid.Text);
            }

        }
        protected void grdstaticdata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdstaticdata.EditIndex = -1;
            if (Convert.ToBoolean(ViewState["check"]) == true)
                bindgrid("onload");
            else
                bindgrid("");

        }
        protected void grdstaticdata_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdstaticdata.EditIndex = e.NewEditIndex;

            int rowindex = grdstaticdata.EditIndex;

            if (Convert.ToBoolean(ViewState["check"]) == true)
                bindgrid("onload");
            else
                bindgrid("");
            bindstatus(rowindex);
        }
        protected void hdffollowupdate_OnDataBinding(object sender, System.EventArgs e)
        {
            HiddenField hdffollowupdate = (HiddenField)(sender);
            if(Eval("Followup_date").ToString()!="")
                if(Eval("Followup_date")!=null)
                    hdffollowupdate.Value = Convert.ToDateTime(Eval("Followup_date")).ToString("MM/dd/yyyy");
        }
        protected void grdstaticdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlmeetingstatus = (DropDownList)e.Row.FindControl("ddlmeetingstatus");
                HiddenField hdfmeetingstatus = (HiddenField)e.Row.FindControl("hdfmeetingstatus");
                TextBox txtfollowup = (TextBox)e.Row.FindControl("txtfollowup");
                HiddenField hdffollowupdate = (HiddenField)e.Row.FindControl("hdffollowupdate");
                //hdffollowupdate.Value = hdffollowupdate.Value.ToString("MM/dd/yyyy");
                DateTime followupdate = (hdffollowupdate.Value != "1/1/1753" && hdffollowupdate.Value != "") ? Convert.ToDateTime(hdffollowupdate.Value,JGConstant.CULTURE) : DateTime.Now.AddDays(7);
                hdffollowupdate.Value = followupdate.ToString("MM/dd/yyyy");
                HiddenField hdnCustomerColor = (HiddenField)e.Row.FindControl("hdnCustomerColor");
                Label lblCustomerName = (Label)e.Row.FindControl("lblCustomerName");
                ddlmeetingstatus.Items.Clear();
                if ((string)Session["usertype"] == "SSE" || (string)Session["usertype"] == "Admin")
                {

                    ddlmeetingstatus.Items.Add("Set");
                    ddlmeetingstatus.Items.Add("Prospect");
                    ddlmeetingstatus.Items.Add("Rehash");
                    ddlmeetingstatus.Items.Add("cancelation-no rehash");
                    ddlmeetingstatus.Items.Add("Follow up");
                    ddlmeetingstatus.SelectedValue = hdfmeetingstatus.Value;
                }
                else
                {
                    ddlmeetingstatus.Items.Add("Set");
                    ddlmeetingstatus.Items.Add("Prospect");
                    ddlmeetingstatus.Items.Add("Rehash");
                    ddlmeetingstatus.Items.Add("cancelation-no rehash");
                    ddlmeetingstatus.Items.Add("Follow up");
                    ddlmeetingstatus.SelectedValue = hdfmeetingstatus.Value;
                }
                ddlmeetingstatus.SelectedValue = hdfmeetingstatus.Value;

                if (ddlmeetingstatus.SelectedValue == "Follow up")
                {
                    txtfollowup.Visible = true;
                    if (hdffollowupdate.Value != "")
                    {
                        followupdate = Convert.ToDateTime(hdffollowupdate.Value,JGConstant.CULTURE);
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
                    e.Row.ForeColor = System.Drawing.Color.Gray;
                    txtfollowup.ForeColor = System.Drawing.Color.Gray;
                    ddlmeetingstatus.ForeColor = System.Drawing.Color.Gray;
                }
                //lblCustomerName.ForeColor = System.Drawing.Color.Gray;
                else if (hdnCustomerColor.Value.ToString() == "red")
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    txtfollowup.ForeColor = System.Drawing.Color.Red;
                    ddlmeetingstatus.ForeColor = System.Drawing.Color.Red;
                }
                //lblCustomerName.ForeColor = System.Drawing.Color.Red;
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    txtfollowup.ForeColor = System.Drawing.Color.Black;
                    ddlmeetingstatus.ForeColor = System.Drawing.Color.Black;
                }
                    //lblCustomerName.ForeColor = System.Drawing.Color.Black;
                // ddlmeetingstatus.Items.Add(hdfmeetingstatus.Value);
            }
        }

        protected void ddlmeetingstatus_selectedindexchanged(object sender, EventArgs e)
        {
            DropDownList ddlstatus = sender as DropDownList;
            GridViewRow gr = (GridViewRow)ddlstatus.Parent.Parent;
            LinkButton lnkid = (LinkButton)gr.FindControl("lnkestimateid");
            TextBox txtfollowup = (TextBox)gr.FindControl("txtfollowup");
            HiddenField hdffollowupdate = (HiddenField)gr.FindControl("hdffollowupdate");

            if (ddlstatus.SelectedValue == "Follow up")
            {
                //txtfollowup.Text = hdffollowupdate.Value;
                txtfollowup.Text = Convert.ToString(DateTime.Now.AddDays(7).ToString("MM/dd/yyyy"));
                txtfollowup.Visible = true;
            }
            else
            {
                txtfollowup.Visible = false;
            }
            AutoSave(sender, e, JGConstant.DROPDOWNLIST);
        }

       // protected void btnsave_Click(object sender, EventArgs e)
         protected void AutoSave(object sender, EventArgs e,string control)
        {
            try
            {
                //Button btnsave = sender as Button;
                //GridViewRow gr = (GridViewRow)btnsave.Parent.Parent;
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
                LinkButton lnkid = (LinkButton)gr.FindControl("lnkestimateid");
                DropDownList ddlstatus = (DropDownList)gr.FindControl("ddlmeetingstatus");
                TextBox txtfollowup = (TextBox)gr.FindControl("txtfollowup");
                HiddenField hdffollowupdate = (HiddenField)gr.FindControl("hdffollowupdate");
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                DateTime followupdate = (txtfollowup.Text != "") ? Convert.ToDateTime(txtfollowup.Text,JGConstant.CULTURE) : DateTime.MinValue;
                string followDate = string.IsNullOrEmpty(txtfollowup.Text) ? "1/1/1753" : Convert.ToDateTime(txtfollowup.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
                string newstatus = ddlstatus.SelectedItem.Text;
                if (newstatus == "Set")
                {
                    followDate = "1/1/1753";
                    AdminBLL.Instance.UpdateStatus(Convert.ToInt32(lnkid.Text), newstatus, followDate);
                    //Server.Transfer("~/Prospectmaster.aspx?title=" + lnkid.Text);
                    Session[SessionKey.Key.PreviousPage.ToString()] = JGConstant.PAGE_STATIC_REPORT;
                    Response.Redirect("~/Prospectmaster.aspx?title=" + lnkid.Text);
                }
                else if (newstatus == "Follow up")
                {
                    txtfollowup.Visible = true;
                    //if (hdffollowupdate.Value != "")
                    //{
                    //    followupdate = Convert.ToDateTime(hdffollowupdate.Value, JGConstant.CULTURE);
                    //    txtfollowup.Text = hdffollowupdate.Value;

                    //}
                    //else
                    //{
                    //    txtfollowup.Text = "";
                    //    followupdate = DateTime.MinValue;
                    //}
                    if (txtfollowup.Text == "")
                    {
                        followupdate = Convert.ToDateTime(hdffollowupdate.Value, JGConstant.CULTURE);
                        txtfollowup.Text = hdffollowupdate.Value;
                        followDate = string.IsNullOrEmpty(txtfollowup.Text) ? "1/1/1753" : Convert.ToDateTime(txtfollowup.Text, JGConstant.CULTURE).ToString("MM/dd/yyyy");
                    }
                    UserBLL.Instance.updateProspectstatus(Convert.ToInt32(lnkid.Text), newstatus, followupdate);
                    GoogleCalendarEvent.DeleteEvent(lnkid.Text, "", "", "", DateTime.Now, DateTime.Now, AdminId);
                    GoogleCalendarEvent.DeleteEvent(lnkid.Text, "", "", "", DateTime.Now, DateTime.Now, JGConstant.CustomerCalendar);
                    if (LoginSession != AdminId)
                    {
                        GoogleCalendarEvent.DeleteEvent(lnkid.Text, "", "", "", DateTime.Now, DateTime.Now, LoginSession);
                    }
                }
                else if (newstatus == "Prospect" || newstatus == "Rehash" || newstatus == "cancelation-no rehash")
                {
                    GoogleCalendarEvent.DeleteEvent(lnkid.Text, "", "", "", DateTime.Now, DateTime.Now, AdminId);
                    GoogleCalendarEvent.DeleteEvent(lnkid.Text, "", "", "", DateTime.Now, DateTime.Now, JGConstant.CustomerCalendar);
                    if (LoginSession != AdminId)
                    {
                        GoogleCalendarEvent.DeleteEvent(lnkid.Text, "", "", "", DateTime.Now, DateTime.Now, LoginSession);
                    }
                }
                else
                {
                    txtfollowup.Visible = false;
                }
                AdminBLL.Instance.UpdateStatus(Convert.ToInt32(lnkid.Text), newstatus, followDate);
                int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString());
                new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(lnkid.Text), Convert.ToDateTime(followupdate), newstatus, userId, false,0);
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Saved Successfully');", true);
                bindgrid("onload");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error in Saving');", true);
            }
        }

         protected void txtfollowup_TextChanged(object sender, EventArgs e)
         {
             AutoSave(sender, e, JGConstant.TEXTBOX);
            
         }

        protected void Btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();

                Response.ContentType = "application/excel";
                string filenam = "Static_Report" + Guid.NewGuid().ToString().Substring(0, 5) + ".xls";
                Response.AddHeader("content-disposition", "attachment; filename=" + filenam);

                StringBuilder sb = new StringBuilder();
                string style = @"<style type='text/css'>
        .CellText
        {
        width:100%;
        text-align:left !important;
        padding-right:4px !important; 
        padding-left:6px !important; 
        border-bottom:1px solid White;
        background-color:#1779CD; 
        color:White;
        }
         .CellText1
        {
        width:100%;
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
            width:auto;         
        }</style> ";

                HttpContext.Current.Response.Write(style);
                StringWriter sw = new StringWriter(sb.Append("<table cellpadding='0' cellspacing='0' width='3000px border='0'><tr><td style='text-transform:uppercase; width:auto;  min-width:300px;'><h2>Static Report</h2>"));

                HtmlTextWriter htw = new HtmlTextWriter(sw);

                System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();

                Controls.Add(form);
                form.Controls.Add(grdstaticdata);
                //grdstaticdata.Columns[0].Visible = false;
                //grdstaticdata.Columns[7].Visible = false;
                grdstaticdata.Columns[8].Visible = false;
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

    }
}