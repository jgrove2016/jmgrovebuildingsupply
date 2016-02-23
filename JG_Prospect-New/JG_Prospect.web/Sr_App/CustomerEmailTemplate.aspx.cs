using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.IO;
using System.Xml;
using System.Collections;

namespace JG_Prospect.Sr_App
{
    public partial class CustomerEmailTemplate : System.Web.UI.Page
    {
        private static int ProductID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["ProductId"].ToString() == "1")
                //{
                ProductID = 20;
                bind(ProductID);
                BindUploadedFile();
                //}
                //else
                //{
                //    ProductID = 4;
                //    bind(4);
                //}
            }            
        }

        protected void bind(int id)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchCustomerEmailTemplate(id);
            if (ds != null)
                HeaderEditor.Content = ds.Tables[0].Rows[0][0].ToString();
            BodyEditor.Content = ds.Tables[0].Rows[0][1].ToString();
            FooterEditor.Content = ds.Tables[0].Rows[0][2].ToString();
            //BodyEditor2.Content = ds.Tables[0].Rows[0][3].ToString();


            DataSet ds2 = new DataSet();
            ds2 = AdminBLL.Instance.FetchCustomerAttachmentTemplate();
            if (ds != null)
            {
                //ds2.Tables[0]
            }
        }

        private void BindUploadedFile()
        {
            //DirectoryInfo dir = new DirectoryInfo(MapPath("~/CustomerDocs/CustomerEmailDocument/"));
            //FileInfo[] files = dir.GetFiles();
            //ArrayList listItems = new ArrayList();
            //foreach (FileInfo info in files)
            //{
            //    listItems.Add(info);
            //}
            //GridView1.DataSource = listItems;
            //GridView1.DataBind();
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/CustomerDocs/CustomerEmailDocument/"));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            GridView1.DataSource = files;
            GridView1.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditor.Content;
            string Editor_contentBody = BodyEditor.Content;
            string Editor_contentFooter = FooterEditor.Content;

            List<CustomerDocument> custDocs = new List<CustomerDocument>();

            //string strFileNameWithPath = fileAttachment.PostedFile.FileName;
            //string strExtensionName = System.IO.Path.GetExtension(strFileNameWithPath);
            //string strFileName = System.IO.Path.GetFileName(strFileNameWithPath);

            int intFileSize = fileAttachment.PostedFile.ContentLength;

            if (fileAttachment.HasFile)
            {
                if (fileAttachment.PostedFile.FileName != "")
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
                                if (File.Exists(Server.MapPath("~/CustomerDocs/CustomerEmailDocument/") + attachment.FileName) == true)
                                {
                                    File.Delete(Server.MapPath("~/CustomerDocs/CustomerEmailDocument/") + attachment.FileName);
                                    fileAttachment.PostedFile.SaveAs(Server.MapPath("~/CustomerDocs/CustomerEmailDocument/") + attachment.FileName);
                                }
                                else
                                {
                                    fileAttachment.PostedFile.SaveAs(Server.MapPath("~/CustomerDocs/CustomerEmailDocument/") + attachment.FileName);
                                }
                                string fPath;
                                fPath = Path.GetFullPath(attachment.FileName);
                                cbc.DocumentName = attachment.FileName;
                                cbc.DocumentPath = fPath;
                                custDocs.Add(cbc);
                            }
                        }
                    }
                }
                else
                {
                    //if (lnkDownload.Visible)
                    //{
                    //    custDocs. = lnkDownload.Text;
                    //}
                    //else { custDocs.DocumentName = string.Empty; }
                }


                bool result = AdminBLL.Instance.UpdateCustomerEmailTemplate(Editor_contentHeader, Editor_contentBody, Editor_contentFooter, ProductID, custDocs);

                if (result)
                {
                    BindUploadedFile();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Customer Email Template Updated Successfully');", true);
                }
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string fileName = lnkDownload.Text.Trim();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
            Response.TransmitFile("~/UploadedFiles/CustomerEmailDocument/" + fileName);
            Response.End();
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            string fileName = Path.GetFileName(filePath);
            bool res = AdminBLL.Instance.DeleteCustomerAttachment(fileName);
            if (res == true)
            {
                File.Delete(filePath);
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}