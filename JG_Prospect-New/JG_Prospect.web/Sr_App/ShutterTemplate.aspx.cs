using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using JG_Prospect.BLL;
using System.Data;

namespace JG_Prospect.Sr_App
{
    public partial class ShutterTemplate : System.Web.UI.Page
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
            ds = AdminBLL.Instance.FetchWorkOrderTemplate();
            if (ds != null)
                Editor1.Content = ds.Tables[0].Rows[0][0].ToString();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string Editor_content = Editor1.Content;

            bool result = AdminBLL.Instance.UpdateWorkOrderTemplate(Editor_content);

            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Installer Work Order Template Updated Successfully');", true);
            }
        }
    }
}