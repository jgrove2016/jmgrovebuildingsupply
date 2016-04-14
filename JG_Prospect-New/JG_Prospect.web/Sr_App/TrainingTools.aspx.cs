using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using JG_Prospect.BLL;
using System.Data;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class TrainingTools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTraingtools();
            }
        }
        private void BindTraingtools()
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.GetResources("TrainingTools");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdTrainingtool.DataSource = ds.Tables[0];
                GrdTrainingtool.DataBind();
            }
            else
            {
                GrdTrainingtool.DataSource = null;
                GrdTrainingtool.DataBind();
            }
        }
        protected void lnktraingtool_Click(object sender, EventArgs e)
        {
            LinkButton lnktraingtool = sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnktraingtool.Parent.Parent;
            Label lbldescription = (Label)gr.FindControl("lbldescription");
            Response.Redirect(lnktraingtool.Text);
        }
    }
}