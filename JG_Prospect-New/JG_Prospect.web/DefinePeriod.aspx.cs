using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
namespace JG_Prospect
{
    public partial class DefinePeriod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblmsg.Visible = false;
            if (!IsPostBack)
            {
                bindlist();
            }
        }
        private void bindlist()
        {
            selectlist.Items.Clear();
            DataSet DS = new DataSet();
            DS = UserBLL.Instance.getallperiod();

            selectlist.DataSource = DS;
            selectlist.DataTextField = DS.Tables[0].Columns[1].ToString();
            selectlist.DataValueField = DS.Tables[0].Columns[0].ToString();
            selectlist.DataBind();
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            period objperiod = new period();
            if (selectlist.SelectedIndex >= 0)
            {
                objperiod.id = Convert.ToInt32(selectlist.SelectedValue.ToString());
            }
            else
            {
                objperiod.id = 0;
            }
            objperiod.periodname = txtperiod.Text;
            objperiod.fromdate = txtfrmdate.Text;
            objperiod.todate = txtTodate.Text;
            bool result = UserBLL.Instance.SavePeriod(objperiod);
            bindlist();
            clearcontrols();
            if (result)
            {
                lblmsg.Visible = true;
                lblmsg.CssClass = "success";
                lblmsg.Text = "Pay Schedule Added/Updated successfully";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.CssClass = "error";
                lblmsg.Text = "there is some error in Add/Update pay Schedule";
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectlist.SelectedIndex >= 0)
            {
                bool result = UserBLL.Instance.deleteperiod(Convert.ToInt32(selectlist.SelectedValue));
                bindlist();
                clearcontrols();

                if (result)
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "success";
                    lblmsg.Text = "Pay Schedule deleted successfully";
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "error";
                    lblmsg.Text = "delete operation can't performed beacuse period already used in prospect";

                }
            }
        }
        protected void reset_Click(object sender, EventArgs e)
        {
            clearcontrols();
            lblmsg.Visible = false;

        }
        protected void selectlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
           // ds = UserBLL.Instance.getperioddetails(selectlist.SelectedValue.ToString());
            txtperiod.Text = ds.Tables[0].Rows[0][2].ToString();
            txtfrmdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToShortDateString();
            txtTodate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0][1]).ToShortDateString();
            Submit.Text = "Update";           
        }
        protected void clearcontrols()
        {
            txtperiod.Text = null;
            txtfrmdate.Text = null;
            txtTodate.Text = null;
            selectlist.ClearSelection();
            Submit.Text = "Save";           
        }       
    }
}