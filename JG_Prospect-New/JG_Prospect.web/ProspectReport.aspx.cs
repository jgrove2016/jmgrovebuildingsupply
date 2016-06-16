using JG_Prospect.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsUsers = new DataSet();
                dsUsers = InstallUserBLL.Instance.getUserList();
                ddlUsersList.DataSource = dsUsers.Tables[0];
                ddlUsersList.DataTextField = "Username";
                ddlUsersList.DataValueField = "Id";
                ddlUsersList.DataBind();
                ddlUsersList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                txtFrom.Text = Convert.ToString(DateTime.Now.Date);
                txtTo.Text = Convert.ToString(DateTime.Now.Date);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            DataSet dsCount = new DataSet();
            if (ddlUsersList.SelectedIndex == 0)
            {
                dsCount = InstallUserBLL.Instance.GetProspectCount(0, txtFrom.Text, txtTo.Text);
            }
            else
            {
                dsCount = InstallUserBLL.Instance.GetProspectCount(Convert.ToInt32(ddlUsersList.SelectedValue), txtFrom.Text, txtTo.Text);
            }
            if (dsCount != null)
            {
                if (dsCount.Tables[0].Rows.Count > 0)
                {
                    lblCount.Visible = true;
                    lblTotCount.Text = Convert.ToString(dsCount.Tables[0].Rows[0][0]);
                }
            }
        }
    }
}