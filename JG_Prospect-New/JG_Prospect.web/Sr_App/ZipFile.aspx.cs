using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using Ionic.Zip;
using System.IO;
using System.Text;

namespace JG_Prospect.Sr_App
{
    public partial class CreateZip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int customerId = 0,productId=0,productTypeId=0;
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["productId"] != null)
                    {
                        productId = Convert.ToInt16(Request.QueryString["productId"]);
                    }
                    if (Request.QueryString["productTypeId"] != null)
                    {
                        productTypeId = Convert.ToInt16(Request.QueryString["productTypeId"]);
                    }
                    if (Request.QueryString["customerId"] != null)
                    {
                        customerId =Convert.ToInt16 ( Request.QueryString["customerId"]);
                        
                        DataSet ds = new DataSet();
                        ds = new_customerBLL.Instance.GetCustomerDocsDetails(customerId,productId ,productTypeId );
                        Gridviewdocs.DataSource = ds;
                        Gridviewdocs.DataBind();
                    }
                    else if (Session["CustomerId"] != null)
                    {
                        DataSet ds = new DataSet();
                        ds = new_customerBLL.Instance.GetCustomerDocsDetails(Convert.ToInt32(Session["CustomerId"].ToString()), productId, productTypeId);                        
                        Gridviewdocs.DataSource = ds;
                        Gridviewdocs.DataBind();
                    }
                }

            }
            catch (Exception)
            {
                // Ignored
            }

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            string UserName = "Salabh";
            ErrorMessage.InnerHtml = "";   // debugging only
            var filesToInclude = new System.Collections.Generic.List<String>();
            String sMappedPath = Server.MapPath("~/CustomerDocs");
            foreach (GridViewRow gvr in Gridviewdocs.Rows)
            {
                CheckBox chkbox = gvr.FindControl("checkbox1") as CheckBox;
                Label lbl = gvr.FindControl("labelfile") as Label;
                if (chkbox != null && lbl != null)
                {
                    if (chkbox.Checked)
                    {
                        ErrorMessage.InnerHtml += String.Format("adding file: {0}<br/>\n", lbl.Text);
                        filesToInclude.Add(System.IO.Path.Combine(sMappedPath, lbl.Text));
                    }
                }
            }

            if (filesToInclude.Count == 0)
            {
                ErrorMessage.InnerHtml += "You did not select any files?<br/>\n";
            }
            else
            {
                string pass = "0";
                Response.Clear();
                Response.BufferOutput = false;

                System.Web.HttpContext c = System.Web.HttpContext.Current;
                String ReadmeText = String.Format("README.TXT\n\nHello!\n\n" +
                                                 "This is a zip file that was dynamically generated at {0}\n" +
                                                 "by an ASP.NET Page running on the machine named '{1}'.\n" +
                                                 "The server type is: {2}\n" +
                                                 "The password used: '{3}'\n" +
                                                 "Encryption: {4}\n",
                                                 System.DateTime.Now.ToString("G"),
                                                 System.Environment.MachineName,
                                                 c.Request.ServerVariables["SERVER_SOFTWARE"],
                                                 "Nome",
                                                 (pass == "" || pass == string.Empty) ? EncryptionAlgorithm.WinZipAes256.ToString() : "None"
                                                 );
                string archiveName = String.Format("archive-{0}.zip", UserName + " " + DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "inline; filename=\"" + archiveName + "\"");

                using (ZipFile zip = new ZipFile())
                {
                    // the Readme.txt file will not be password-protected.
                    zip.AddEntry("Readme.txt", ReadmeText, Encoding.Default);
                    //if (!String.IsNullOrEmpty(tbPassword.Text))
                    //{
                    //    zip.Password = tbPassword.Text;
                    //    if (chkUseAes.Checked)
                    //        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    //}

                    // filesToInclude is a string[] or List<String>
                    zip.AddFiles(filesToInclude, "files");

                    zip.Save(Response.OutputStream);
                }
                Response.Close();
            }
        }

        protected void Gridviewdocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Image img = (Image)e.Row.FindControl("Image1");
                string s = DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Contains(".pdf") == true)
                {
                    //img.ImageUrl = "~/CustomerDocs/Pdfs/pdf.jpg";
                    //img.Height = 90;
                    //img.Width = 60;
                }
            }
        }
    }
}