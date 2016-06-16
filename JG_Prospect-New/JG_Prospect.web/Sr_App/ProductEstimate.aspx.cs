using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.Text;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;

namespace JG_Prospect.Sr_App
{
    public partial class ProductEstimate : System.Web.UI.Page
    {
        private static int CustomerID = 0;
        static string LoginSession = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginSession = Convert.ToString(Session["loginid"]);
                FillCustomerDropDown();
                if (Request.QueryString["title"] != null)
                {
                    string[] title = Request.QueryString["title"].ToString().Split('-');
                    Session["CustomerId"] = title[0].ToString();
                }
                //if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null)
                if (Session["CustomerId"] != null)
                {
                    CustomerID = Convert.ToInt16(Session["CustomerId"].ToString());//Convert.ToInt16(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
                    Customer c = new_customerBLL.Instance.fetchcustomer(CustomerID);
                    if (c != null)
                    {
                        txtProspectsearch.Text = c.customerNm + "-" + CustomerID;
                        //ddlCustomer.SelectedValue = Convert.ToString(c.id);
                        bindproductlines(CustomerID);
                    }
                }
                //if (Session["CustomerName"] != null)
                //{
                //    txtCustomer.Text = Session["CustomerName"].ToString();
                //    txtCustomer.Enabled = false;
                //}
                BindProducts();

            }
            ChangeColours();
        }

        private void FillCustomerDropDown()
        {
            DataSet dds = new DataSet();
            dds = new_customerBLL.Instance.GetCustomerForDropDown();
            DataRow dr = dds.Tables[0].NewRow();
            dr["id"] = "0";
            dr["CustomerName"] = "--Select--";
            dds.Tables[0].Rows.InsertAt(dr, 0);
            if (dds.Tables[0].Rows.Count > 0)
            {
                //ddlCustomer.DataSource = dds.Tables[0];
                //ddlCustomer.DataValueField = "id";
                //ddlCustomer.DataTextField = "CustomerName";
                //ddlCustomer.DataBind();
            }

        }
        private void BindProducts()
        {
            DataSet ds = UserBLL.Instance.GetAllProducts();
            ddlproductlines.DataSource = ds;
            ddlproductlines.DataTextField = "ProductName";
            ddlproductlines.DataValueField = "ProductId";
            ddlproductlines.DataBind();
            ddlproductlines.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0)"));
        }

        private void ChangeColours()
        {
            foreach (System.Web.UI.WebControls.ListItem item in ddlproductlines.Items)
            {
                System.Web.UI.WebControls.ListItem i = ddlproductlines.Items.FindByText(item.Text);
                if (i.Text == "Shutters" || i.Text == "Custom" || i.Text == "Custom - Other *" || i.Text == "Siding -Masonry" || i.Text == "Roofing -Terracotta-Shake-Slate" || i.Text == "Roofing - Metal" || i.Text == "Awnings" || i.Text == "Flooring - Hardwood-Laminate-Vinyl" || i.Text == "Flooring - Marble - Porcelain, Ceramic" || i.Text == "Windows & Doors" || i.Text == "Masonry -  Flat - Retaining walls" || i.Text == "Bathrooms" || i.Text == "Kitchens" || i.Text == "Basements" || i.Text == "Additions" || i.Text == "Electric" || i.Text == "Plumbing")
                {
                    i.Attributes.Add("style", "color:red;");
                }
            }
        }
        StringBuilder sb = new StringBuilder();
        DataSet DS = new DataSet();
        protected void btnAddlineitem_Click(object sender, EventArgs e)
        {
            if (CustomerID == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Customer to Proceed');", true);
                return;
            }
            switch (ddlproductlines.SelectedValue)
            {
                    //New code added
                case "1":
                    Response.Redirect("Shutters.aspx?ProductTypeId=" + ddlproductlines.SelectedValue.Trim() + "&CustomerId=" + CustomerID);
                    break;

                //case "2":
                //case "3":
                //case "4":
                //case "5":
                //case "6":
                //case "7":
                //case "8":
                //case "9":
                //case "10":
                //case "11":
                //case "12":
                //case "13":
                //case "14":
                //case "15":
                //case "16":
                //case "17":
                //case "18":
                //case "19":
                //case "20":
                //case "21":
                //case "22":
                //case "23":
                //case "24":
                //case "25":
                //case "26":
                //case "27":
                //case "28":
                //case "29":
                //    Response.Redirect("Custom.aspx?ProductTypeId=" + ddlproductlines.SelectedValue.Trim() + "&CustomerId=" + CustomerID + "&Other=" + txtOther.Text.Trim());
                //    break;

                default:
                    Response.Redirect("Custom.aspx?ProductTypeId=" + ddlproductlines.SelectedValue.Trim() + "&CustomerId=" + CustomerID + "&Other=" + txtOther.Text.Trim());
                    break;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCustomerlist(string prefixText)
        {
            List<string> Customer = new List<string>();
            DataSet dds = new DataSet();
            dds = new_customerBLL.Instance.GetAutoSuggestiveCustomers(prefixText);
            foreach (DataRow dr in dds.Tables[0].Rows)
            {
                Customer.Add(dr[0].ToString());
            }
            return Customer.ToArray();
        }


        private void bindproductlines(int CustId)
        {
            if (CustId != 0)
            {
                DataSet ds = new DataSet();
                string Status = "UnSold";
                ds = shuttersBLL.Instance.GetProductLineItems(CustId, Status);
                if (ds != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                        Session["CustomerEmail"] = ds.Tables[1].Rows[0]["Email"].ToString();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdproductlines.DataSource = ds;
                        grdproductlines.DataBind();
                    }
                    else
                    {
                        grdproductlines.DataSource = null;
                        grdproductlines.DataBind();
                    }
                }
                else
                {
                    grdproductlines.DataSource = null;
                    grdproductlines.DataBind();
                }
            }
            else
            {
                grdproductlines.DataSource = null;
                grdproductlines.DataBind();
            }
        }

        protected void lnkestimateid_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            HiddenField HiddenFieldEstimate = (HiddenField)lnk.FindControl("HiddenFieldEstimate");
            HiddenField HidCustomerId = lnk.FindControl("HidCustomerId") as HiddenField;
            HiddenField HidProductTypeId = lnk.FindControl("HidProductTypeId") as HiddenField;
            HiddenField HidProductTypeIdFrom = lnk.FindControl("HidProductTypeIdFrom") as HiddenField;

            if (Convert.ToInt32(HidProductTypeId.Value) == 1)
            {
                Response.Redirect("Shutters.aspx?ProductId=" + HiddenFieldEstimate.Value + "&CustomerId=" + HidCustomerId.Value + "&ProductTypeId=" + HidProductTypeId.Value);
            }
            else
            {
                Response.RedirectPermanent("Custom.aspx?ProductId=" + HiddenFieldEstimate.Value + "&CustomerId=" + HidCustomerId.Value + "&ProductTypeId=" + HidProductTypeId.Value + "&ProductTypeIdFrom=" + HidProductTypeIdFrom.Value);
            }

        }

        private string createEstimate(string InvoiceNo, int estimateId)
        {

            DataSet ds = new DataSet();
            ds = shuttersBLL.Instance.FetchShutterDetails(estimateId);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append(@"<br/> <br/>
          <table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Arial, Helvetica, sans-serif; font-size:9pt; '>  
          <tr>
            <td><u><strong>Proposal A:</strong></u> To supply and install (" + ds.Tables[0].Rows[0]["Quantity"].ToString() + @")pair(s) of custom made Mid America ( " + ds.Tables[0].Rows[0]["Style"].ToString() + ") (" + ds.Tables[0].Rows[0]["ColorName"].ToString() + ")shutters. The shutters are to consist of a heavy duty vinyl.  Remove and haul away old shutters and debris. Job location:( " + ds.Tables[0].Rows[0]["WorkArea"].ToString() + ")</td></tr>");
                    sb.Append(@"<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Arial, Helvetica, sans-serif; font-size:9pt'><tr><td>&nbsp;</td>        <td>&nbsp;</td>
                <td scope='col'>&nbsp;</td>
              </tr>
              <tr>
                <td><img src=" + Server.MapPath("~/img/ma.png") + @" width='150'></td>
                <td colspan='2'>
                <table width='100%' border='0' cellspacing='0' 
        
        cellpadding='0'  style='font-family:Arial, Helvetica, sans-serif; font-size:9pt'>
                  <tr>
                    <td><strong>Lifetime manufacturer’s warranty</strong></td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                  </tr>
                  <tr>
                    <td><strong>Two year labor warranty</strong></td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                  </tr>
                <tr>
                <td>
                
                  <strong>
                
                Total price :   $ " + (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"].ToString()) * 125).ToString() + @"      </strong></td></tr>
            </table></td>
          </tr>
          </table>
          </td>
          </tr>
        <tr>
                    <td><br /><br /><br /></td>
                  </tr>
        <tr>
                    <td>&nbsp;</td>
                  </tr>
          <tr>
            <td><u><strong>Proposal B::</strong></u> To supply and install (" + ds.Tables[0].Rows[0]["Quantity"].ToString() + @") 
        
        pair(s) of generic plastic (" + ds.Tables[0].Rows[0]["Style"].ToString() + @") (" + ds.Tables[0].Rows[0]["ColorName"].ToString() + @")shutters.  Remove and haul away old 
        
        shutters and debris. Job location:( " + ds.Tables[0].Rows[0]["WorkArea"].ToString() + @")</td>
          </tr>
          <tr>
            <td><table width='100%' border='0' cellspacing='0' cellpadding='0'  
        
        style='font-family:Arial, Helvetica, sans-serif; font-size:9pt'>
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td colspan='2'><strong>
                    
                    Total price : $ " + (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"].ToString()) * 100).ToString() + @"  </strong> </td>   
              </tr>
            </table></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
        </table><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br /><br/><br/><br/><br />");
                }
            }
            return (sb.ToString());

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

                // now read the Grid html one by one and add into the document object

                using (TextReader sReader = new StringReader(text))
                {
                    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                    document.Open();

                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/Header1.jpg"));
                    logo.SetAbsolutePosition(0f, 650f);
                    PdfPTable table = new PdfPTable(1);
                    table.AddCell(logo);

                    table.WidthPercentage = 110;
                    document.Add(table);

                    foreach (IElement elm in list)
                    {
                        document.Add(elm);
                    }

                    iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/ma_footer.jpg"));//footer
                    logo1.SetAbsolutePosition(0f, 0f);
                    PdfPTable table1 = new PdfPTable(1);
                    table1.AddCell(logo1);

                    table1.WidthPercentage = 110;

                    document.Add(table1);


                }

            }
            catch (Exception ee)
            {
                //  lblMessage.Text = ee.ToString();
            }
            finally
            {
                if (document.IsOpen())
                    document.Close();
            }
        }

        private void sendmail(string email, string invoice)
        {
            try
            {
                string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("jgrove.georgegrove@gmail.com", "Admin");
                message.To.Add(email);
                message.Subject = "Email From Admin via JG";
                Attachment attach = new Attachment(invoice);
                message.Attachments.Add(attach);
                message.IsBodyHtml = true;

                string strBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Untitled Document</title><style type='text/css'>body{font-family:Arial, Helvetica, sans-serif; font-size:13px; font-weight:normal; color:#000000; }" +
                             "p{line-height:18px; margin-left:5px}td,tr{margin-left:5px;}</style></head><body><table width='70%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td width='13%'></td>" +
                            "<td width='87%'>&nbsp;</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td><b>Hi</b></td></tr><tr><td>&nbsp;</td></tr><tr><td><p>Thank You, please find the estimates below: \n User ID: 'admin' \n Password: 'admin' </p></td>" +
                            "</tr><tr><td>&nbsp; </td></tr><tr><td><p><b>Your EmailId</b> :" + email + "</p></td></tr> <tr> <td><p> is registered with us for further communications. </p></td></tr>" +
                            " <tr> <td>&nbsp;</td></tr><tr> <td>Estimate Amount is attached with this Email. <br></td></tr><tr><td><p>Please feel free to contact at: 'jgrove.georgegrove@gmail.com' for any queries. </p></td>" +
                            "</tr><tr><td>&nbsp;</td></tr> <tr><td><b>Thanks</b>,</td> </tr> <tr><td style='height: 16px'>Admin</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

                message.Body = strBody;
                smtpClient.Credentials = new System.Net.NetworkCredential("jgrove.georgegrove@gmail.com", AdminPwd);
                try
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }

                catch (Exception exm)
                {
                    Response.Write(exm.Message);
                }
            }
            catch { }
        }

        protected void lnkcreatecontract_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            int index = ((GridViewRow)lnk.NamingContainer).RowIndex;
            LinkButton estimate = (LinkButton)grdproductlines.Rows[index].FindControl("lnkestimateid");
            string[] est = estimate.Text.Split('-');

            Response.Redirect("Shutterproposal.aspx?EstimateId=" + est[1].ToString() + "&custmail=" + Session["CustomerEmail"].ToString());

            string fileName = string.Empty;
            string path = Server.MapPath("/CustomerDocs/Pdfs/");

            // Create and display the value of two GUIDs.
            string g = Guid.NewGuid().ToString().Substring(0, 5);
            fileName = "Invoice" + g.ToString() + ".pdf";
            // Create Invoice with Pdf for Transaction.....
            GeneratePDF(path, fileName, false, createEstimate("InvoiceNumber-1", Convert.ToInt32(est[1].ToString())));
            string email = Session["CustomerEmail"].ToString();
            string AttachedPdfFile = path + fileName;
            sendmail(email, AttachedPdfFile);

            Response.Redirect(AttachedPdfFile);

        }

        protected void btncreatecontract_Click(object sender, EventArgs e)
        {
            string strEstimateId = "";
            HiddenField HidCustomerId = null;
            HiddenField HidProductTypeId = null;
            int rowcount = 0, flag = 0;
            CheckBox chk = new CheckBox();
            HiddenField HiddenEstimate = new HiddenField();
            foreach (GridViewRow GVPLines in grdproductlines.Rows)
            {
                chk = (CheckBox)GVPLines.FindControl("CheckBoxContract");
                HiddenEstimate = (HiddenField)GVPLines.FindControl("HiddenFieldEstimate");
                if (chk.Checked == true)
                {
                    HidCustomerId = GVPLines.FindControl("HidCustomerId") as HiddenField;
                    HidProductTypeId = GVPLines.FindControl("HidProductTypeId") as HiddenField;
                    // if (HidProductTypeId.Value == "1" || HidProductTypeId.Value == "4")
                    // {
                    rowcount++;
                    int EstId = int.Parse(HiddenEstimate.Value);
                    // strEstimateId += EstId.ToString() + " , ";
                    strEstimateId += EstId.ToString() + "-" + HidProductTypeId.Value + ",";
                    string a = strEstimateId.ToString().Substring(0, strEstimateId.Length - 1);
                    Session["EstID"] = a;
                    // }
                    // else
                    // {
                    //   flag = 1;
                    //   break;
                    //}
                }
            }
            // if (flag == 0)
            // {


            if (rowcount > 0)
            {
                Session[SessionKey.Key.CustomerId.ToString()] = HidCustomerId.Value;
                Session[SessionKey.Key.ProductType.ToString()] = HidProductTypeId.Value;
                Response.Redirect("Shutterproposal.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select at least product');", true);
            }
            // }
            // else
            // {
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select only Custom or Shutter products');", true);
            // }
        }

        protected void ddlproductlines_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlproductlines.SelectedValue)
            {
                case "2":
                case "3":
                    txtOther.Visible = true;
                    break;
                default:
                    txtOther.Visible = false;
                    break;
            }
        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            //if (hdCustID.Value != "")
            //{
            //    //if (hdCustID.Value == txtCustomer.Text)
            //    if (hdCustID.Value == ddlCustomer.SelectedValue)
            //    {
            //        string[] customer = hdCustID.Value.ToString().Split('-');
            //        string customerName = customer[0];
            //        int id = Convert.ToInt16(customer[1]);
            //        Session["CustomerName"] = customerName;
            //        Session["CustomerId"] = id;
            //        CustomerID = id;
            //        bindproductlines(id);
            //    }
            //    else
            //    {
            //        Session["CustomerName"] = "";
            //        Session["CustomerId"] = "";
            //        CustomerID = 0;
            //        bindproductlines(0);
            //    }
            //}
            //else
            //{
            //    Session["CustomerName"] = "";
            //    Session["CustomerId"] = "";
            //    CustomerID = 0;
            //    bindproductlines(0);
            //}
        }

        protected void rptcutomerlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkprospect = (LinkButton)e.Item.FindControl("lnkprospect");
            HiddenField hdnCustomerColor = (HiddenField)e.Item.FindControl("hdnCustomerColor");

            if (hdnCustomerColor.Value.ToString() == "grey")
                lnkprospect.ForeColor = System.Drawing.Color.Gray;
            else if (hdnCustomerColor.Value.ToString() == "red")
                lnkprospect.ForeColor = System.Drawing.Color.Red;
            else
                lnkprospect.ForeColor = System.Drawing.Color.Black;
        }

        protected void prospect_click(object sender, EventArgs e)
        {
            LinkButton lnkprospect = sender as LinkButton;
            string ProspectId = lnkprospect.CommandArgument;
            Session["CustomerId"] = ProspectId;
            string customerName = lnkprospect.Text;
            txtProspectsearch.Text = lnkprospect.Text;
            int id = Convert.ToInt32(Session["CustomerId"]);
            CustomerID = id;
            bindproductlines(id);
            rptcutomerlist.Visible = false;
            //Session["ProspectName"] = lnkprospect.Text;
            //Response.Redirect("~/Prospectmaster.aspx?title=" + ProspectId + "-" + lnkprospect.Text);
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (hdCustID.Value != "")
            //{
            //if (hdCustID.Value == txtCustomer.Text)
            //if (hdCustID.Value == ddlCustomer.SelectedValue)
            //{
            //string[] customer = hdCustID.Value.ToString().Split('-');
            //string customerName = ddlCustomer.SelectedItem.Text;
            //int id = Convert.ToInt16(ddlCustomer.SelectedValue);

            //}
            //else
            //{
            //    Session["CustomerName"] = "";
            //    Session["CustomerId"] = "";
            //    CustomerID = 0;
            //    bindproductlines(0);
            //}
            //}
            //else
            //{
            //    Session["CustomerName"] = "";
            //    Session["CustomerId"] = "";
            //    CustomerID = 0;
            //    bindproductlines(0);
            //}
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //if (ddlsearchtype.Text != "Select")
            //    fillprospect(0);
            //else
            //    fillprospect(1);
        }

        protected void txtProspectsearch_TextChanged(object sender, EventArgs e)
        {
            fillprospect(1);
        }

        private void fillprospect(int flag)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = null;
                string user;
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                if (Convert.ToString(Session["usertype"]) == "Admin" || Convert.ToString(Session["usertype"]) == "SM" || Convert.ToString(Session["usertype"]) == "MM")  //(string)Session["loginid"] == AdminId 
                {
                    user = "All";
                }
                else
                {
                    user = LoginSession;
                }
                //if (flag == 0)
                //    ds = new_customerBLL.Instance.SearchCustomers(ddlsearchtype.SelectedValue, txtProspectsearch.Text, user);
                ////else if (flag == 1)
                ////    ds = new_customerBLL.Instance.SearchCustomers("", txtProspectsearch.Text, user);
                //else
                ds = new_customerBLL.Instance.SearchCustomers("CustomerName", txtProspectsearch.Text, user);

                if (ds.Tables.Count > 0)
                {
                    DataView dv = new DataView(ds.Tables[0]);

                    // ds = new_customerBLL.Instance.SearchProspect(ddlsearchtype.SelectedValue, txtProspectsearch.Text, user);
                    Session["LeftUser"] = dv;
                    rptcutomerlist.DataSource = ds;
                    rptcutomerlist.DataBind();
                    rptcutomerlist.Visible = true;
                }
                else
                {
                    rptcutomerlist.DataSource = null;
                    rptcutomerlist.DataBind();
                    rptcutomerlist.Visible = false;
                    if (Convert.ToString(Session["NoMessage"]) != "PageLoad")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('No record found');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        protected void grdproductlines_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.Image img = (e.Row.FindControl("imglocation") as System.Web.UI.WebControls.Image);
                    string imgUrl = Convert.ToString((e.Row.FindControl("hdnlocation") as HiddenField).Value);
                    if (imgUrl != "")
                    {
                        if (imgUrl.Contains('~') && imgUrl.Contains(".."))
                        {
                            string[] imgUri = imgUrl.Split('.');

                            string a = imgUri[0];
                            string b = ".." + imgUri[2] + "." + imgUri[3];
                            img.ImageUrl = b;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}