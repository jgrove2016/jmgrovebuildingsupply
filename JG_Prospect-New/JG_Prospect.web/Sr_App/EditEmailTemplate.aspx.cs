using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class EditEmailTemplate : System.Web.UI.Page
    {
        public int HTMLTemplateID
        {
            get { return ViewState["HTMLTemplateID"] != null ? Convert.ToInt32(ViewState["HTMLTemplateID"].ToString()) : 0; }
            set { ViewState["HTMLTemplateID"] = value; }
        }
        public int SubHTMLTemplateID
        {
            get { return ViewState["SubHTMLTemplateID"] != null ? Convert.ToInt32(ViewState["SubHTMLTemplateID"].ToString()) : 0; }
            set { ViewState["SubHTMLTemplateID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HTMLTemplateID = Convert.ToInt32(Request.QueryString["htempID"].ToString());
                InitialDataBind(0);
            }
        }
        protected void lnkVendorCategory_Click(object sender, EventArgs e)
        {
        }
        protected void lnkVendor_Click(object sender, EventArgs e)
        {
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditor.Content;
            string Editor_contentFooter = FooterEditor.Content;
            string Editor_contentBody = BodyEditor.Content;
            List<CustomerDocument> custDocs = new List<CustomerDocument>();
            int intFileSize = flVendCat.PostedFile.ContentLength;

            if (flVendCat.HasFile)
            {
                if (flVendCat.PostedFile.FileName != "")
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollection attachments = Request.Files;
                        for (int i = 0; i < attachments.Count; i++)
                        {

                            HttpPostedFile attachment = attachments[i];
                            if (attachment.ContentLength > 0 && !String.IsNullOrEmpty(attachment.FileName))
                            {
                                CustomerDocument cbc = new CustomerDocument();
                                if (File.Exists(Server.MapPath("../CustomerDocs/VendorEmailDocument/") + attachment.FileName) == true)
                                {
                                    File.Delete(Server.MapPath("../CustomerDocs/VendorEmailDocument/") + attachment.FileName);
                                    flVendCat.PostedFile.SaveAs(Server.MapPath("../CustomerDocs/VendorEmailDocument/") + attachment.FileName);
                                }
                                else
                                {
                                    flVendCat.PostedFile.SaveAs(Server.MapPath("../CustomerDocs/VendorEmailDocument/") + attachment.FileName);
                                }
                                string fPath;
                                fPath = ("../CustomerDocs/VendorEmailDocument/") + attachment.FileName;
                                cbc.DocumentName = attachment.FileName;
                                cbc.DocumentPath = fPath;
                                custDocs.Add(cbc);
                            }
                        }
                    }
                }
            }
            bool result = AdminBLL.Instance.UpdateHTMLTemplate(Editor_contentHeader, Editor_contentBody, Editor_contentFooter, txtSubject.Text, SubHTMLTemplateID, custDocs);
            if (result)
            {
                InitialDataBind(SubHTMLTemplateID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Auto Email Template Updated Successfully');", true);
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            Int32 lAttachmentID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            DataSet lDsAttachment = AdminBLL.Instance.DeleteEmailAttachment(lAttachmentID);
            string fileName = (Server.MapPath(lDsAttachment.Tables[0].Rows[0]["DocumentPath"].ToString()));
            //bool res = AdminBLL.Instance.DeleteCustomerAttachment(fileName);
            if (fileName != "")
            {
                File.Delete(fileName);
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void btnUpdateVendor_Click(object sender, EventArgs e)
        {
        }

        private void InitialDataBind(int pSubHTMLTemplateID)
        {
            DataSet ds = AdminBLL.Instance.GetAutoEmailTemplate(HTMLTemplateID, pSubHTMLTemplateID);
            if (ds != null)
            {
                if (SubHTMLTemplateID == 0)
                    SubHTMLTemplateID = Convert.ToInt32(ds.Tables[1].Rows[0]["Id"].ToString());

                drpChooseCategory.DataSource = ds.Tables[0];
                drpChooseCategory.DataTextField = "SubHtmlName";
                drpChooseCategory.DataValueField = "Id";
                drpChooseCategory.DataBind();
                drpChooseCategory.SelectedValue = SubHTMLTemplateID.ToString();

                lblPageTitle.Text = ds.Tables[1].Rows[0]["SubHTMLName"].ToString().Replace("_", " ");
                txtSubject.Text = ds.Tables[1].Rows[0]["htmlsubject"].ToString();

                HeaderEditor.Content = ds.Tables[1].Rows[0]["HTMLHeader"].ToString();
                BodyEditor.Content = ds.Tables[1].Rows[0]["HTMLBody"].ToString();
                FooterEditor.Content = ds.Tables[1].Rows[0]["HTMLFooter"].ToString();

                grdVendCatAtc.DataSource = ds.Tables[2];
                grdVendCatAtc.DataBind();
            }
        }

        protected void drpChooseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubHTMLTemplateID = Convert.ToInt32(drpChooseCategory.SelectedValue);
            InitialDataBind(SubHTMLTemplateID);
        }
    }
}