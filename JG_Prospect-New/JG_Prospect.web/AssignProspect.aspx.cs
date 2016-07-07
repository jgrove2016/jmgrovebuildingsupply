using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.Configuration;
using JG_Prospect.Common;

namespace JG_Prospect
{
    public partial class AssignProspect : System.Web.UI.Page
    {
        private static int UserID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                ds = UserBLL.Instance.getUser(Session["loginid"].ToString().Trim());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Id"]!="")
                        UserID = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                show(DateTime.MinValue, DateTime.MinValue, "All");
            }
        }

        private DataSet bindSruser()
        {
            DataSet DS = new DataSet();
            DS = UserBLL.Instance.getSrusers();
            return DS;
        }


        public static DataTable dt;
        protected void btnshow_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DateTime? fromdate = (txtfrmdate.Text != "") ? Convert.ToDateTime(txtfrmdate.Text, JGConstant.CULTURE) : DateTime.MinValue;
            DateTime? todate = (txtTodate.Text != "") ? Convert.ToDateTime(txtTodate.Text, JGConstant.CULTURE) : DateTime.MinValue;
            string username = "All";
            show(fromdate, todate, username);
        }

        protected void show(DateTime? fromdate, DateTime? todate, string username)
        {
            if (txtfrmdate.Text != "" || txtTodate.Text != "")
            {
                if (txtfrmdate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter from date');", true);
                    return;

                }
                if (txtTodate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter to date');", true);
                    return;

                }
            }

            DataSet ds = new DataSet();
            ds = UserBLL.Instance.FetchProspectstoassign(fromdate, todate, username);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                grddata.DataSource = ds.Tables[0];
                grddata.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                grddata.DataSource = null;
                grddata.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No Records Found');", true);
                btnSave.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow r in grddata.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkpros = (CheckBox)r.FindControl("chkprospect");

                        if (chkpros.Checked)
                        {
                            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                            int CustomerId = Convert.ToInt32(grddata.DataKeys[r.RowIndex]["id"]);
                            DropDownList ddlassignto = (DropDownList)r.FindControl("ddlassignuser");
                            int assignuserid = Convert.ToInt32(ddlassignto.SelectedValue);
                            string datetime = null;
                            DateTime meetingDate = DateTime.Now;
                            if (r.Cells[6].Text != "&nbsp;")
                            {
                                string date = Convert.ToDateTime(r.Cells[6].Text).ToShortDateString();
                                
                                // if (date != "1/1/1753")
                                //{
                                datetime = Convert.ToDateTime(date + " " + r.Cells[7].Text).ToString("MM/dd/yy hh:mm tt");
                                //}
                                //DataSet ds = new DataSet();
                                //ds = existing_customerBLL.Instance.GetExistingCustomerDetailById(CustomerId);
                                meetingDate = Convert.ToDateTime(datetime);
                            }
                            DataSet ds1 = new DataSet();
                            ds1 = existing_customerBLL.Instance.UpdateCustomerAssignId(CustomerId, assignuserid);
                            new_customerBLL.Instance.AddCustomerFollowUp(CustomerId, meetingDate, JG_Prospect.Common.JGConstant.CUSTOMER_STATUS_ASSIGNED, UserID, false, assignuserid,"");
                        }
                    }

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Customers assigned successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
            }
        }

        protected void grddata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnassignid = (HiddenField)e.Row.FindControl("hdnassign");
                string assignid = hdnassignid.Value;
                DropDownList ddlassign = (DropDownList)e.Row.FindControl("ddlassignuser");
                ddlassign.DataSource = bindSruser();
                ddlassign.DataValueField = bindSruser().Tables[0].Columns["Id"].ToString();
                ddlassign.DataTextField = bindSruser().Tables[0].Columns["Username"].ToString();
                ddlassign.DataBind();
                ddlassign.SelectedValue = assignid;
            }
        }


    }
}