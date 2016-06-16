using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using JG_Prospect.BLL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;

namespace JG_Prospect.Sr_App
{
    public partial class video : System.Web.UI.Page
    {
        public string VideoURL = "", VideoDescription = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bindVideoDetails();
        }

        private void bindVideoDetails()
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.GetResources("Videos");
                      
            if (ds.Tables[0].Rows.Count > 0)
            {
                VideoURL = ds.Tables[0].Rows[0][1].ToString();
                VideoDescription = ds.Tables[0].Rows[0][2].ToString();          
                grdvideo.DataSource = ds.Tables[0];
                grdvideo.DataBind();                    
            }
            else
            {
                VideoURL = string.Empty;
                VideoDescription = string.Empty;
                grdvideo.DataSource = null;
                grdvideo.DataBind();
            }
        }

        protected void lnkvideourl_Click(object sender, EventArgs e)
        {
            LinkButton lnkurl = sender as LinkButton;
            GridViewRow gr = (GridViewRow)lnkurl.Parent.Parent;
            Label lbldescription = (Label)gr.FindControl("lbldescription");
            
             VideoURL = lnkurl.Text;
             VideoDescription = lbldescription.Text;
        }

    }
}