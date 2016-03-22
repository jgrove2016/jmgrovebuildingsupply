using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;


namespace JG_Prospect.Sr_App
{
    public partial class LeftPanel : System.Web.UI.UserControl
    {
        int flag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = ImageButton1.UniqueID;
            if (!IsPostBack)
            {               
                fillcustomer(0);
                Session["NoMessage"] = "PageLoad";
            }
        }


        DataSet DS;
        static string loginby;
        //   public event SendMessageToThePageHandler sendMessageToThePage;     

        protected void ddlsearchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtcustomersearch.Text != "")
            {

            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["NoMessage"] = "";
            if (ddlsearchtype.Text != "Select")
                fillcustomer(0);
            else
                fillcustomer(1);
        }

        private void fillcustomer(int flag)
        {
            try
            {
                string user = "";
                if ((string)Session["usertype"] == "Admin" || (string)Session["usertype"] == "SM")
                {
                    user = "All";
                }
                else
                {
                   // user = Session["loginid"].ToString();
                }
                DataSet ds = new DataSet();
                ds = null;
                if(flag==0)
                    ds = new_customerBLL.Instance.SearchCustomers(ddlsearchtype.SelectedValue, txtcustomersearch.Text, user);
                //else if(flag==1)
                  //  ds = new_customerBLL.Instance.SearchCustomers("", txtcustomersearch.Text, user);
                else
                    ds = new_customerBLL.Instance.SearchCustomers("", txtcustomersearch.Text, user);

                if (ds.Tables.Count > 0)
                {
                    DataView dv = new DataView(ds.Tables[0]);
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
                }
            }
            catch (Exception ex)
            {
                //
            }
        }


        protected void txtcustomersearch_TextChanged(object sender, EventArgs e)
        {
            fillcustomer(1);
        }

        protected void rptcutomerlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkcustomer = (LinkButton)e.Item.FindControl("lnkcustomer");
            HiddenField hdnCustomerColor = (HiddenField)e.Item.FindControl("hdnCustomerColor");

            if (hdnCustomerColor.Value.ToString() == "grey")
                lnkcustomer.ForeColor = System.Drawing.Color.Gray;
            else if (hdnCustomerColor.Value.ToString() == "red")
                lnkcustomer.ForeColor = System.Drawing.Color.Red;
            else
                lnkcustomer.ForeColor = System.Drawing.Color.Black;
        }

        protected void rptcutomerlist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }
        protected DataView SortingAlphabet()
        {
            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString() == "SM")
            {
                DS = new DataSet();
                DS = existing_customerBLL.Instance.AllCustomer();
                DataView dv = new DataView(DS.Tables[0]);
                return dv;
            }
            else
            {
                loginby = Session["loginid"].ToString();
                DS = new DataSet();
                DS = existing_customerBLL.Instance.GetExistingCustomer(loginby);

                DataView dv = new DataView(DS.Tables[0]);

                return dv;
            }

        }

        protected void customername_click(object sender, EventArgs e)
        {
            LinkButton lnkcustomer = sender as LinkButton;
            string CustomerId = lnkcustomer.CommandArgument;
            Session["CustomerId"] = CustomerId;
            Session["CustomerName"] = lnkcustomer.Text;
            Response.Redirect("~/Sr_App/Customer_Profile.aspx?CustomerId=" + CustomerId);
        }

        protected void A_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'A*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;

        }

        protected void B_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'B*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void C_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'C*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;

        }

        protected void D_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'D*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void E_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'E*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void F_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'F*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void G_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'G*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;

        }

        protected void H_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'H*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void I_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'I*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void J_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'J*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void K_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'K*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void L_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'L*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void M_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'M*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void N_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'N*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void O_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'O*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void P_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'P*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void Q_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'Q*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void R_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'R*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void S_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'S*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void T_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'T*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void U_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'U*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void V_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'V*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void W_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'W*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void X_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'X*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void Y_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'Y*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

        protected void Z_Click(object sender, EventArgs e)
        {
            DataView DvA = SortingAlphabet();
            DvA.RowFilter = " CustomerName LIKE  'Z*'";
            Session["LeftUser"] = DvA;
            rptcutomerlist.DataSource = DvA;
            rptcutomerlist.DataBind();

            rptcutomerlist.Visible = true;
        }

       
         
        

    }
}