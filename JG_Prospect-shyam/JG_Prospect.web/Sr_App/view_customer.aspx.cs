using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using JG_Prospect.BLL;

namespace JG_Prospect.Sr_App
{
    public partial class view_customer : System.Web.UI.Page
    {
        DataSet DS;
        string loginby;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["loginid"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('firstly you have to login');", true);
                Response.Redirect("~/login.aspx");
            }
            else
            {
                //Repeater rpt = LeftPanelViewCustomer1.FindControl("rptcutomerlist") as Repeater;

                //if (Session["LeftUser"] != null)
                //{
                //    DataView DvA = (DataView)Session["LeftUser"];
                //    rpt.DataSource = DvA;
                //    rpt.DataBind();

                //    rpt.Visible = true;

                //}
               
                    string[] Title;
                    if (Request.QueryString["title"] != null)
                    {
                        Title = Request.QueryString["title"].ToString().Split('-');
                        Session["CustomerId"] = Title[0].ToString().Substring(1);
                    }
                        viewcustomer();                
            }

            //LeftPanelViewCustomer1.lnkclick += new EventHandler(LeftPanelViewCustomer1_lnkclick);
            //if (IsPostBack == false)
            //{
            //    LeftPanelViewCustomer obj = (LeftPanelViewCustomer)Page.FindControl("LeftPanelViewCustomer1");
            //    Repeater reA = (Repeater)obj.FindControl("rptcutomerlist");
            //    reA.Visible = false;

            //}
        }

        protected void LeftPanelViewCustomer1_lnkclick(object sender, EventArgs e)
        {
            viewcustomer();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["CustomerId"] != null)
            {
                int customerid = Convert.ToInt32(Session["CustomerId"].ToString());
                DataSet ds = existing_customerBLL.Instance.GetExistingCustomerDetail(customerid, "", Session["loginid"].ToString());
                int Id = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                string name = txtCust_name.Text;
                string strno = txtCust_strt.Text;
                string jobloc = txtJob_loc.Text;
                DateTime estdate = Convert.ToDateTime(txtEst_date.Text);
                DateTime todaydate = Convert.ToDateTime(txtToday_date.Text);
                string ph1 = txtPri_ph.Text;
                string secph = txtSec_ph.Text;
                string mail = txtEmail.Text;
                string calltakenby = txtCall_takenby.Text;
                string service = txtService.Text;
                string leadtype;

                if (ddlleadtype.SelectedValue.ToString() == "Other")
                {
                    leadtype = txtleadtype.Text;
                }
                else
                {
                    leadtype = ddlleadtype.SelectedValue.ToString();
                }


                int a = existing_customerBLL.Instance.UpdateCustomer(Id, name, strno, jobloc, estdate, todaydate, ph1, secph, mail, calltakenby, service,leadtype);
                if (a > 0)
                {
                    string userName = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                    string userPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                    if (Session["AdminUserId"] == null)
                        GoogleCalendarEvent.EditEvent(Request.QueryString["title"].ToString(), ("C" + Id.ToString() + "-" + name), name, service, estdate, estdate, Session["loginid"].ToString());

                    GoogleCalendarEvent.EditEvent(Request.QueryString["title"].ToString(), ("C" + Id.ToString() + "-" + name), name, service, estdate, estdate, userName);
                }
            }

        }

        private void viewcustomer()
        {
            int customerid;
            if (Session["CustomerId"] != null)
            {
                customerid = Convert.ToInt32(Session["CustomerId"].ToString());

                DS = new DataSet();
                DS = existing_customerBLL.Instance.GetExistingCustomerDetail(customerid,"", Session["loginid"].ToString());


                //CustomerName,CustomerStreet,JobLocation,EstDateSchdule,TodayDate,PrimaryPh,SecondryPh,Email,CallTakenBy,Service
                txtCust_name.Text = DS.Tables[0].Rows[0][1].ToString();
                txtCust_strt.Text = DS.Tables[0].Rows[0][2].ToString();
                txtJob_loc.Text = DS.Tables[0].Rows[0][3].ToString();
                txtEst_date.Text = DS.Tables[0].Rows[0][4].ToString();
                txtToday_date.Text = DS.Tables[0].Rows[0][5].ToString();

                txtPri_ph.Text = DS.Tables[0].Rows[0][6].ToString();
                txtSec_ph.Text = DS.Tables[0].Rows[0][7].ToString();
                txtEmail.Text = DS.Tables[0].Rows[0][8].ToString();
                txtCall_takenby.Text = DS.Tables[0].Rows[0][9].ToString();
                txtService.Text = DS.Tables[0].Rows[0][10].ToString();
                ddlleadtype.SelectedValue = DS.Tables[0].Rows[0][11].ToString();
            }
        }

        protected void ddlleadtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlleadtype.SelectedValue.ToString() == "Other")
            {
                tr_leadtype.Visible = true;
            }
            else
            {
                tr_leadtype.Visible = false;
            }
        }
    }
}