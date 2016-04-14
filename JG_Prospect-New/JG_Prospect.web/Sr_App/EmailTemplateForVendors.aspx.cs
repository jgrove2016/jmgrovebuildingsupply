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
    public partial class EmailTemplateForVendors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }
        protected void bind()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(10);
            if (ds != null)
            {
                HeaderEditor.Content = ds.Tables[0].Rows[0][0].ToString();
                lblMaterials.Text = ds.Tables[0].Rows[0][1].ToString();
                FooterEditor.Content = ds.Tables[0].Rows[0][2].ToString();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditor.Content;
            string Editor_contentFooter = FooterEditor.Content;

            bool result = AdminBLL.Instance.UpdateEmailVendorTemplate2(Editor_contentHeader, Editor_contentFooter,"");

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('EmailVendor Template Updated Successfully');", true);
            }

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Custom_MaterialList.aspx");
        }
    }
}