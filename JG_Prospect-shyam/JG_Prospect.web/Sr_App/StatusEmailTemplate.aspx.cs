using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data;

namespace JG_Prospect.Sr_App
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditor.Content;
            string Editor_contentBody = BodyEditor.Content;
            string Editor_contentFooter = FooterEditor.Content;
            string StrStatus = ddlstatus.SelectedValue;
            List<CustomerDocument> custDocs = new List<CustomerDocument>();
            bool result = AdminBLL.Instance.UpdateCustomerEmailTemplate(Editor_contentHeader, Editor_contentBody, Editor_contentFooter, Convert.ToInt32(Session["StatusTemplateId"]), custDocs);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Template updated successfully')", true);
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue == "InterviewDate")
            {
                bind("InterviewDate");
            }
            else if (ddlstatus.SelectedValue == "OfferMade")
            {
                bind("OfferMade");
            }
            else if (ddlstatus.SelectedValue == "Deactive")
            {
                bind("Deactive");
            }
        }

        protected void bind(string HTML_Name)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchStatusEmailTemplate(HTML_Name);
            if (ds != null)
            {
                Session["StatusTemplateId"] = ds.Tables[0].Rows[0][0].ToString();
                HeaderEditor.Content = ds.Tables[0].Rows[0][1].ToString();
                BodyEditor.Content = ds.Tables[0].Rows[0][2].ToString();
                FooterEditor.Content = ds.Tables[0].Rows[0][3].ToString();
            }
            //BodyEditor2.Content = ds.Tables[0].Rows[0][3].ToString();


            //DataSet ds2 = new DataSet();
            //ds2 = AdminBLL.Instance.FetchCustomerAttachmentTemplate();
            //if (ds != null)
            //{
            //    //ds2.Tables[0]
            //}
        }

    }
}