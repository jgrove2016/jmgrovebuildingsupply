using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

using JG_Prospect.BLL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;

namespace JG_Prospect.Sr_App
{
    public partial class SharedDocuments : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSharedDocsDetail();
            }
        }
        private void BindSharedDocsDetail()
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.GetResources("SharedDocument");

            if (ds.Tables[0].Rows.Count > 0)
            {              
                GrdSharedDocument.DataSource = ds.Tables[0];
                GrdSharedDocument.DataBind();
            }
            else
            {
               GrdSharedDocument.DataSource = null;
               GrdSharedDocument.DataBind();
            }
        }

        protected void lnksahredDocs_Click(object sender, EventArgs e)
        {
            LinkButton lnkShareDocs =sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnkShareDocs.Parent.Parent;
            Label lbldescription = (Label)gr.FindControl("lbldescription");
            Response.Redirect(lnkShareDocs.Text);
         
        }
    }
   
}