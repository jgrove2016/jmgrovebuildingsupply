using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.Configuration;


namespace JG_Prospect.Controls
{
    public partial class left : System.Web.UI.UserControl
    {
        int flag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = ImageButton1.UniqueID;
            if (!IsPostBack)
            {
                Session["NoMessage"] = "PageLoad";
                fillprospect(0);
            }
        }

        DataSet DS;
        static string loginby;

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["NoMessage"] = "";
            if (ddlsearchtype.Text != "Select")
                fillprospect(0);
            else
                fillprospect(1);
            //if (ddlsearchtype.Text == "Select")
            //{
            //    fillAllprospect();
               
            //}
            
        }

        protected void rptcutomerlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkprospect = (LinkButton)e.Item.FindControl("lnkprospect");
            HiddenField hdnCustomerColor = (HiddenField)e.Item.FindControl("hdnCustomerColor");

            if (hdnCustomerColor.Value.ToString() == "grey")
                lnkprospect.ForeColor = System.Drawing.Color.Gray;
            else if (hdnCustomerColor.Value.ToString() == "red")
                lnkprospect.ForeColor = System.Drawing.Color.Red;
            else
                lnkprospect.ForeColor = System.Drawing.Color.Black;
        }
        protected void rptcutomerlist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void prospect_click(object sender, EventArgs e)
        {
            LinkButton lnkprospect = sender as LinkButton;
            string ProspectId = lnkprospect.CommandArgument;
            Session["ProspectId"] = ProspectId;
            Session["ProspectName"] = lnkprospect.Text;
            Response.Redirect("~/Prospectmaster.aspx?title=" + ProspectId + "-" + lnkprospect.Text);
        }
        protected void txtProspectsearch_TextChanged(object sender, EventArgs e)
        {
            fillprospect(1);
        }

        private void fillprospect(int flag)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = null;
                string user;
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString() == "SM" || Session["usertype"].ToString() == "MM")  //(string)Session["loginid"] == AdminId 
                {
                    user = "All";
                }
                else
                {
                    user = Convert.ToString(Session["loginid"]);
                }
                if (flag == 0)
                    ds = new_customerBLL.Instance.SearchCustomers(ddlsearchtype.SelectedValue, txtProspectsearch.Text, user);
                //else if (flag == 1)
                //    ds = new_customerBLL.Instance.SearchCustomers("", txtProspectsearch.Text, user);
                else
                    ds = new_customerBLL.Instance.SearchCustomers("CustomerName", txtProspectsearch.Text, user);

                if (ds.Tables.Count>0)
                {
                    DataView dv = new DataView(ds.Tables[0]);

                    // ds = new_customerBLL.Instance.SearchProspect(ddlsearchtype.SelectedValue, txtProspectsearch.Text, user);
                    Session["LeftUser"] = dv;
                    rptcutomerlist.DataSource = ds;
                    rptcutomerlist.DataBind();
                    rptcutomerlist.Visible = true;
                }
                else
                {
                    rptcutomerlist.DataSource = null;
                    rptcutomerlist.DataBind();
                    rptcutomerlist.Visible = false;
                    if (Convert.ToString(Session["NoMessage"]) != "PageLoad")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void fillAllprospect()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = null;
                string user;
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString() == "SM" || Session["usertype"].ToString() == "MM")  //(string)Session["loginid"] == AdminId 
                {
                    user = "All";
                }
                else
                {
                    user = Convert.ToString(Session["loginid"]);
                }
                string str = "";
                ds = new_customerBLL.Instance.SearchCustomers(str, txtProspectsearch.Text, user);
                //else if (flag == 1)
                //    ds = new_customerBLL.Instance.SearchCustomers("", txtProspectsearch.Text, user);
             
                if (ds.Tables.Count > 0)
                {
                    DataView dv = new DataView(ds.Tables[0]);

                    // ds = new_customerBLL.Instance.SearchProspect(ddlsearchtype.SelectedValue, txtProspectsearch.Text, user);
                    Session["LeftUser"] = dv;
                    rptcutomerlist.DataSource = ds;
                    rptcutomerlist.DataBind();
                    rptcutomerlist.Visible = true;
                }
                else
                {
                    rptcutomerlist.DataSource = null;
                    rptcutomerlist.DataBind();
                    rptcutomerlist.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

    }
}