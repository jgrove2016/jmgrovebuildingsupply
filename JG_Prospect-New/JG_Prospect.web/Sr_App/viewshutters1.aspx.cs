using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using JG_Prospect.Common;
using System.Data;

namespace JG_Prospect.Sr_App
{
    public partial class viewshutters1 : System.Web.UI.Page
    {
        DataSet DS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string email = Request.QueryString["aa"].ToString();
                string addedby = Session["loginid"].ToString();

                DS = shuttersBLL.Instance.GetShutters(email, addedby);

                grdshutters.DataSource = DS.Tables[0];
                grdshutters.DataBind();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string email = Request.QueryString["aa"].ToString();
            string addedby = Session["loginid"].ToString();

            LinkButton lnk = (LinkButton)sender;
            int index = ((GridViewRow)lnk.NamingContainer).RowIndex;
            string orderno = grdshutters.DataKeys[index].Value.ToString();

            Response.Redirect("estimate.aspx?addedby=" + addedby + "&custmail=" + email + "&orderno=" + orderno);

        }

        protected void btncreatecontract_Click(object sender, EventArgs e)
        {
            string email = Request.QueryString["aa"].ToString();
            string addedby = Session["loginid"].ToString();


            //  var argument = ((Button)sender).CommandArgument;
            string orderno = ((Button)sender).CommandArgument;

            Response.Redirect("shutterproposal.aspx?addedby=" + addedby + "&custmail=" + email + "&orderno=" + orderno);
        }

        protected void btncreatecontract_Click1(object sender, EventArgs e)
        {

        }
    }
}