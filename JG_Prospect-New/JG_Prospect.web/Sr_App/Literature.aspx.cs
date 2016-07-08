using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using JG_Prospect.BLL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class Literature : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLiterature();
            }
        }
        private void BindLiterature()
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.GetResources("Literature");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdLiterature.DataSource = ds.Tables[0];
                GrdLiterature.DataBind();
            }
            else
            {
                GrdLiterature.DataSource = null;
                GrdLiterature.DataBind();
            }
        }

        protected void lnkliterature_Click(object sender, EventArgs e)
        {
            LinkButton lnkLiterature = sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnkLiterature.Parent.Parent;
            Label lbldescription = (Label)gr.FindControl("lbldescription");
            Response.Redirect(lnkLiterature.Text);
        }
    }
}