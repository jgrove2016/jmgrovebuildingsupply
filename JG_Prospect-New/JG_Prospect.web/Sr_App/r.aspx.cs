using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using JG_Prospect.Common;
using System.IO;
using System.Net;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;
using System.Configuration;


namespace JG_Prospect.Sr_App
{
    public partial class r : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void GeneratePDF(string path, string fileName, bool download, string text)//download set to false in calling method
        {
            var document = new Document();

            FileStream FS = new FileStream(path + fileName, FileMode.Create);
            try
            {
                if (download)
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    PdfWriter.GetInstance(document, Response.OutputStream);
                }
                else
                {
                    PdfWriter.GetInstance(document, FS);
                }
                // generates the grid first
                StringBuilder strB = new StringBuilder();
                strB.Append(text);
                // now read the Grid html one by one and add into the document object

                using (TextReader sReader = new StringReader(strB.ToString()))
                {
                    document.Open();
                    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                    foreach (IElement elm in list)
                    {
                        document.Add(elm);
                    }
                }
            }
            catch (Exception ee)
            {
                throw ee;
                //  lblMessage.Text = ee.ToString();
            }
            finally
            {
                if (document.IsOpen())
                    document.Close();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string workorder = string.Empty;
            // Create and display the value of two GUIDs.
            string g = Guid.NewGuid().ToString().Substring(0, 5);
            fileName = "InstallInvoice" + g.ToString() + ".pdf";
            workorder = "InstallWorkOrder" + g.ToString() + ".pdf";

            string FileText = pdf_BLL.Instance.InstallerInvoice();
            GeneratePDF(path, fileName, false, FileText);
            string url = ConfigurationManager.AppSettings["URL"].ToString();
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + fileName + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
        }
    }
}