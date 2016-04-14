using JG_Prospect.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.UserControl
{
    public partial class UCAddress : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void txtzip_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtzip.Text == "")
        //    {
        //        txtcity.Text = txtstate.Text = "";
        //    }
        //    else
        //    {

        //        DataSet ds = new DataSet();
        //        ds = UserBLL.Instance.fetchcitystate(txtzip.Text);
        //        if (ds != null)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                txtcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
        //                txtstate.Text = ds.Tables[0].Rows[0]["State"].ToString();
        //            }
        //            else
        //            {
        //                txtcity.Text = txtstate.Text = "";
        //            }
        //        }
        //    }
        //}
    }
}