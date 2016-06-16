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
using System.Web.Services;
using JG_Prospect.Common.Logger;
using JG_Prospect.Common.modal;
using System.Diagnostics;
using NReco.PdfGenerator;


namespace JG_Prospect.Sr_App
{
    public partial class shutterproposal : System.Web.UI.Page
    {
        StringBuilder sb = new StringBuilder();
        DataSet DS = new DataSet();
        static string[] arr;
        private Boolean IsPageRefresh = false;
        ErrorLog logManager = new ErrorLog();
        int customerId = 0, productType = 0, productId = 0;
        static int ProductTypeID;
        List<Tuple<int, string, int>> proposalOptionList = null;
        static int[] productIdList = new int[50];
        static int[] estimateIdList = new int[50];
        static string QuoteNumber = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["success"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alsert('Your Transaction is successfull')", true);
                return;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["UserId"] != null && Request.QueryString["CustomerId"] != null && Request.QueryString["EstId"] != null && Request.QueryString["paymentMode"] != null && Request.QueryString["amount"] != null && Request.QueryString["checkNo"] != null && Request.QueryString["creditCardDetails"] != null && Request.QueryString["IsEmail"] != null && Request.QueryString["TransactionId"] != null)
                {
                    TransactionComplete(true, Request.QueryString["UserId"], Request.QueryString["CustomerId"], Request.QueryString["EstId"], Request.QueryString["paymentMode"], Request.QueryString["amount"], Request.QueryString["checkNo"], Request.QueryString["creditCardDetails"], Request.QueryString["TransactionId"]);
                }
                Session["clicks"] = 0;
                Session["NotSoldclicks"] = 0;
                Session["AddedEmails"] = "";
                proposalOptionList = new List<Tuple<int, string, int>>();
                RefreshData();
                Uri uri = HttpContext.Current.Request.Url;
                String host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
                Session["FormDataObjects"] = null;
                if (Session[SessionKey.Key.CustomerId.ToString()] != null)
                {
                    customerId = Convert.ToInt32(Session[SessionKey.Key.CustomerId.ToString()]);
                }
                if (Session[SessionKey.Key.ProductType.ToString()] != null)
                {
                    productType = Convert.ToInt32(Session[SessionKey.Key.ProductType.ToString()]);
                }


                DataTable dtShutters = new DataTable();
                dtShutters.Columns.Add("id");
                dtShutters.Columns.Add("productId");
                DataTable dtGeneral = new DataTable();
                dtGeneral.Columns.Add("id");
                dtGeneral.Columns.Add("productId");


                if (Session["loginid"] == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('firstly you have to login');", true);
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    if (Session["EstID"] != null)
                    {
                        arr = Session["EstID"].ToString().Split(',');
                        //productId = Convert.ToInt32(arr[0].ToString());

                        int counter = 0;
                        foreach (string item in arr)
                        {
                            string[] s = item.Split('-');
                            productIdList[counter] = Convert.ToInt32(s[1].ToString());
                            if (productIdList[counter] == 1)
                                dtShutters.Rows.Add(s[0].ToString(), s[1].ToString());
                            else
                                dtGeneral.Rows.Add(s[0].ToString(), s[1].ToString());

                            estimateIdList[counter] = Convert.ToInt32(s[0].ToString());
                            counter += 1;
                        }

                        if (Session["CustomerId"] != null)
                        {
                            // LiteralHeader.Text = createHeaderEstimate("", int.Parse(arr[0].ToString()), productType);
                            //LiteralHeader.Text = createHeaderEstimate("", estimateIdList, productIdList);
                            string Header = createHeaderEstimate("", estimateIdList, productIdList);
                            LiteralHeader.Text = Header.Replace(ConfigurationManager.AppSettings["UrlToReplaceForTemplates"].ToString(), host);
                        }
                        string s1 = Session["EstID"].ToString();
                        string[] EstID = s1.Split(',');
                        int count = EstID.Count();




                        //for (int i = 0; i < count; i++)
                        //{
                        //    string id = EstID[i].ToString().Trim();

                        //    dt.Rows.Add(id);
                        //}
                        // dsestimate = shuttersBLL.Instance.FetchContractdetails(int.Parse(Est));
                    }

                    //grdproductlines.Style.Add(HtmlTextWriterStyle.Display, "block");
                    int flag = 0;
                    decimal totalAmount = 0;
                    int counterShutters = 0, counterOthers = 0;

                    foreach (int prodId in productIdList)
                    {
                        if (prodId == 0)
                            break;
                        if (prodId == 1)
                        {
                            if (counterShutters == 0)
                            {
                                ProductTypeID = prodId;
                                grdproductlines.Visible = true;
                                grdproductlines.DataSource = dtShutters;
                                grdproductlines.DataBind();
                                CalculateAmount();
                                if (flag == 0)
                                {
                                    flag = 1;
                                    totalAmount = Convert.ToDecimal(HiddenFieldtotalAmount.Value);
                                }
                                else
                                {
                                    HiddenFieldtotalAmount.Value = (totalAmount + Convert.ToDecimal(HiddenFieldtotalAmount.Value)).ToString();
                                    totalAmount = Convert.ToDecimal(HiddenFieldtotalAmount.Value);
                                }
                            }
                            counterShutters = 1;
                        }
                        else
                        {
                            // Original code
                            if (counterOthers == 0)
                            {
                                ProductTypeID = prodId;
                                grdCustom.Visible = true;
                                grdCustom.DataSource = dtGeneral;
                                grdCustom.DataBind();
                                CalculateAmountForCustom();
                                if (flag == 0)
                                {
                                    flag = 1;
                                    totalAmount = Convert.ToDecimal(HiddenFieldtotalAmount.Value);
                                }
                                else
                                {
                                    HiddenFieldtotalAmount.Value = (totalAmount + Convert.ToDecimal(HiddenFieldtotalAmount.Value)).ToString();
                                    totalAmount = Convert.ToDecimal(HiddenFieldtotalAmount.Value);
                                }
                            }
                            counterOthers = 1;

                        }


                    }
                    DataSet ds = new DataSet();
                    ds = AdminBLL.Instance.FetchContractTemplate(productType);
                    if (ds != null)
                    {
                        // LiteralFooter.Text = createFooterEstimate("", int.Parse(arr[0].ToString()), productType);

                        int productid = productIdList[0];
                        if (productid != 0 && productid != 1)
                        {
                            productid = 2;
                        }
                        string footer = createFooterEstimate("", estimateIdList[0], productid);
                        LiteralFooter.Text = footer.Replace(ConfigurationManager.AppSettings["UrlToReplaceForTemplates"].ToString(), host);

                        // LiteralFooter.Text = createFooterEstimate("", estimateIdList[0], productIdList[0]);
                    }
                }
            }
            else
            {
                if (ViewState["postids"] != null && Session["postid"] != null)
                {
                    if (ViewState["postids"].ToString() != Session["postid"].ToString())
                    {
                        IsPageRefresh = true;
                    }
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"];
                if (Session[SessionKey.Key.ProductType.ToString()] != null)
                {
                    productType = Convert.ToInt32(Session[SessionKey.Key.ProductType.ToString()]);
                }
                if (Session[SessionKey.Key.CustomerId.ToString()] != null)
                {
                    customerId = Convert.ToInt32(Session[SessionKey.Key.CustomerId.ToString()]);
                }
                if (Session["EstID"] != null)
                {
                    arr = Session["EstID"].ToString().Split(',');
                    productId = 1;//Convert.ToInt32(arr[0].ToString());
                }
            }
        }

        private void TransactionComplete(bool IsEmail, string UserId, string CustomerId, string EstId, string paymentModeNew, string amountNew, string checkNoNew, string creditCardDetailsNew, string TransactionIdNew)
        {
            bool stat = true;
            try
            {
                if (stat)
                {
                    int userId = Convert.ToInt16(UserId);
                    string soldJobID = string.Empty;
                    string s1 = EstId;
                    string[] EstID = s1.Split(',');
                    int count = EstID.Count();

                    string paymentMode = string.Empty, checkNo = string.Empty, creditCardDetails = string.Empty;
                    decimal amount = 0;
                    paymentMode = paymentModeNew;
                    amount = Convert.ToDecimal(amountNew);
                    checkNo = checkNoNew;
                    creditCardDetails = creditCardDetailsNew;
                    soldJobID = shuttersBLL.Instance.UpdateShutterEstimate(s1, "Sold", Convert.ToInt32(CustomerId), userId, paymentMode, amount, checkNo, creditCardDetails, "");
                    if (soldJobID != string.Empty)
                    {
                        string mailid = ViewState["customeremail"].ToString();

                        string path = Server.MapPath("~/CustomerDocs/Pdfs/");

                        // Create and display the value of two GUIDs.
                        string g = Guid.NewGuid().ToString().Substring(0, 5);

                        string tempInvoiceFileName = "Invoice" + DateTime.Now.Ticks + ".pdf";

                        string originalInvoiceFileName = "Invoice" + ".pdf";


                        // Create Invoice with Pdf for Transaction.....
                        GeneratePDF(path, tempInvoiceFileName, false, createEstimate("InvoiceNumber-" + Session["CustomerId"].ToString(), Convert.ToInt32(Session["CustomerId"].ToString())), true);

                        if ((Session["FormDataObjects"] != null) || (productId > 0))
                        {
                            List<Tuple<int, string, int>> proposalOptionList = (List<Tuple<int, string, int>>)Session["FormDataObjects"];

                            foreach (var prop in proposalOptionList)
                            {
                                new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(Session["CustomerId"].ToString()), prop.Item1, originalInvoiceFileName, "Contract", tempInvoiceFileName, prop.Item3, 0);
                            }
                        }

                        GenerateWorkOrder(soldJobID);

                        if (IsEmail)
                        {
                            SendEmailToCustomer(tempInvoiceFileName);
                        }
                        RefreshData();
                        //GeneratePDF(path, tempWorkOrderFilename , false, createWorkOrder("Work Order-" + Session["CustomerId"].ToString(), int.Parse(ViewState["EstimateId"].ToString())));
                        //new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(Session["CustomerId"].ToString()), 1, originalWorkOrderFilename , "WorkOrder",tempWorkOrderFilename );
                        string email = mailid;
                        string AttachedPdfFile = path + tempInvoiceFileName;
                        //  sendmail(email, AttachedPdfFile);
                        string url = ConfigurationManager.AppSettings["URL"].ToString();
                        //ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + tempInvoiceFileName  + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");

                        // string completeFileName = url + "/CustomerDocs/Pdfs/" + tempInvoiceFileName;
                        string completeFileName = "http://50.191.13.206/JGP/CustomerDocs/Pdfs/" + tempInvoiceFileName;
                        HidCV.Value = string.Empty;
                        Response.Redirect("~/Sr_App/Procurement.aspx?FileToOpen=" + completeFileName);
                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Terms & Conditions');", true);
                    //RequiredAmount.IsValid = false;
                    //CV.IsValid = false;
                    //CV.ErrorMessage = "Invalid Password";
                    //CV.ForeColor = System.Drawing.Color.Red;
                    //mp_sold.Show();
                }
            }
            catch (Exception ex)
            {
                HidCV.Value = string.Empty;
                logManager.writeToLog(ex, "Custom", Request.ServerVariables["remote_addr"].ToString());
            }
        }
        /*
         for sending mail...# Updated as on 26 Feb 2013 #...
         */
        private void sendmail(string email, string invoice)
        {
            try
            {
                string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                // SmtpClient smtpClient = new SmtpClient();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                MailMessage message = new MailMessage();
                message.From = new MailAddress(AdminId, "Admin");
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
                            " <tr> <td>&nbsp;</td></tr><tr> <td>Estimate Amount is attached with this Email. <br></td></tr><tr><td><p>Please feel free to contact at: '" + AdminId + "' for any queries. </p></td>" +
                            "</tr><tr><td>&nbsp;</td></tr> <tr><td><b>Thanks</b>,</td> </tr> <tr><td style='height: 16px'>Admin</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

                message.Body = strBody;
                smtpClient.Credentials = new System.Net.NetworkCredential(Adminuser, AdminPwd);
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

        private string createEstimate(string InvoiceNo, int CustomerId)
        {
            string proposalOption = string.Empty;
            if ((Session["FormDataObjects"] != null) || (productId > 0))
            {
                List<Tuple<int, string, int>> proposalOptionList = (List<Tuple<int, string, int>>)Session["FormDataObjects"];
                string Praposal = Convert.ToString(Session["Proposal"]);

                proposalOption = pdf_BLL.Instance.CreateEstimate(InvoiceNo, CustomerId, productType, HiddenFieldtotalAmount.Value,
                                    proposalOptionList, hidDownPayment.Value, Praposal);

                Uri uri = HttpContext.Current.Request.Url;
                String host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                proposalOption = proposalOption.Replace(ConfigurationManager.AppSettings["UrlToReplaceForTemplates"].ToString(), host);
            }
            return proposalOption;

        }

        private void literalbody2(string CustomerName)
        {
            string amountpart1 = "";
            string amountpart2 = "";
            string amountpart3 = "";
            decimal amt2 = 0, amt3 = 0, amt1 = 0;
            //            string body2text = @"<table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 10pt;'> 
            //<tbody> 
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td align='justify' colspan='13'><br />
            //</td>        
            //<td valign='top'>&nbsp;</td>      </tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top' colspan='13'>We propose hereby to furnish material and labor - complete in accordance with above specifications, for the sum of:</td>        
            //<td valign='top'>&nbsp;</td>      </tr>
            //<tr>  
            //<td valign='top'>&nbsp;</td>  
            //<td valign='top' colspan='13'>&nbsp;</td>  
            //<td valign='top'>&nbsp;</td></tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top' style='width:5%;'>&nbsp;</td>        
            //<td valign='top' colspan='5'>Any building permits, additional wood, metal, <br />
            //  mandated EPA lead containment requirements or <br />
            //  additional un-forseen materials &amp; labor needed <br />
            //  to complete a job will be at an additional <br />
            //  charge. Review reverse side for terms and<br />
            //  conditions..</td>                 
            //<td valign='top' colspan='7'>
            //<table width='100%' cellspacing='0' cellpadding='0' border='0'>          
            //<tbody>
            //<tr>            
            //<td width='50%'>&nbsp;</td>            
            //<td width='50%'>$ lblTotalAmount</td>            </tr>          
            //<tr>            
            //<td></td>            
            //<td>
            //<hr style='color: #000000;' /></td>            </tr>          
            //<tr>            
            //<td align='right' colspan='2'><strong>Payment to be made as follows</strong></td>            </tr>          
            //<tr>            
            //<td align='right'>1/3 Down Payment:&nbsp; </td>            
            //<td>$ lblamountpart1</td>            </tr>          
            //<tr>            
            //<td></td>            
            //<td>
            //<hr style='color: #000000;' /></td>            </tr>          
            //<tr>            
            //<td align='right'> 1/3 Due upon scheduling:&nbsp; </td>            
            //<td>$&nbsp;lblamountpart2</td>            </tr>          
            //<tr>            
            //<td></td>            
            //<td>
            //<hr style='color: #000000;' /></td>            </tr>          
            //<tr>            
            //<td align='right'>1/3 Due upon majority completion:&nbsp; </td>            
            //<td>$&nbsp;lblamountpart3</td>            </tr>          
            //<tr>            
            //<td></td>            
            //<td>
            //<hr style='color: #000000;' /></td>            </tr>          </tbody></table></td>        
            //<td valign='top'>&nbsp;</td>      </tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top' colspan='13'><strong>The Attorney General 717-787-3391</strong></td>        
            //<td valign='top'>&nbsp;</td>      </tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>      </tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top' colspan='13'>Registration #:PA092750</td>        
            //<td valign='top'>&nbsp;</td>      </tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top' colspan='13'>&nbsp;</td>        
            //<td valign='top'>&nbsp;</td>      </tr>      
            //<tr>        
            //<td valign='top'>&nbsp;</td>        
            //<td valign='top' colspan='13'>Acceptance of Proposal</td>        
            //<td valign='top'>&nbsp;</td>      </tr>     
            //
            //</tbody></table>";
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            string body2text = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (productType != 4 && productType != 7 && productType != 0 && productType != 100 && productType != 20)
                {
                    body2text = ds.Tables[0].Rows[0][3].ToString();
                }
            }
            //            string body2text = @"<table align='center' width='100%' bordercolor='#666666' bgcolor='#FFFFFF' border='0' class='no_line' cellspacing='0' cellpadding='0' style='font-family:Verdana, Geneva, sans-serif; font-size:8pt;'>  <tr>    <td colspan='2'><table width='100%' border='0' cellspacing='10' cellpadding='0'>  <tr>    <td width='50%' height='30' valign='top' style='text-align:justify'>*Standard JM Grove projects start approximately: 2-8 weeks (depending on job type, size) from finalized material list and/or deposits. And are normally substantially completed by: (undeterminable)  Act of god, Weather, labor shortages, supplies availability, Custom orders, un-forseen labor/material and but not limited to change orders can all cause delays and cause undeterminable extended turn-around time. All following material & labor is furnished & installed by J.M. Grove Construction unless otherwise specified. J.M. Grove strongly recommends supplying and installing ALL material to prevent costly delays, “owner” is responsible forall delays caused by supplying incorrect material at Time & Material basis. The above prices, specifications and conditions are satisfactory and are hereby accepted. You are authorized to do the work as specified. Any equipment, material, or labor designated as “OWNERS” responsibility must be on the job site, uncrated and inspected and approved on the day the project begins. Client supplied material used will not be warrantied by J.M. Grove Construction. Payment will be made as outlined below. Please sign and return white copy if proposal is accepted. Review reverse side for terms and conditions</td>    <td width='50%' valign='top' style='text-align:justify'>Any removal or correction of any concealed wall, ceiling or floor obstruction ,decay, pipe, ducting, additional wood or metal, additional un-forseen materials &labor needed to complete a job, Any required state or local permit ordinances,  Testing or Analysis or Remediation Removal or Repair of – Radon – Lead – Asbestos – Mold or any other like substances etc. revealed during construction, is not included in this contract. If this type of situation occurs, it will be reviewed with the client, and a separate price will be quoted on that part of the project or billed at a Time & Material rate of: (Mechanic Rate=$90/hr and/or Helper/Painter Rate =$70/hr). Payment for the extra work will be paid in full prior to the start of work or the original specifications will be performed. All changes will generally increase the time it takes to complete the project. Change Order Forms must be signed by both J.M. Grove Construction and client.      <p align='justify'>&nbsp;</p>      <p align='right' style='font-weight:bold'>Customer:X_____________________________</p></td>  </tr>  <tr>    <td height='30' colspan='2' valign='top'>    	<p><strong>Acceptance of Proposal:</strong></p>        <p>Registration #:PA092750      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            Attorney General 717-787-3391<br />        
            //We  propose hereby to furnish material and labor – complete in accordance with abovespecifications, for the sum of:$ lblTotalAmount</p>        <p><strong>Payment to be made as follows:</strong></p>        <p>1/3 Down Payment:$ lblamountpart1 ,1/3 Due upon scheduling:$&nbsp;lblamountpart2 ,1/3 Due upon majority completion:$&nbsp;lblamountpart3</p><p>Date:&nbsp;lblFooterDate &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Authorized Signature:__________________________</p>		<p>Customer Name (Printed):&nbsp; lblCustomerName &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     Customer Signature:__________________________</p></td>
            //    </tr>
            //</table></td></tr></table>";
            if (HiddenFieldtotalAmount.Value != "")
            {
                amt1 = amt2 = amt3 = Convert.ToDecimal(HiddenFieldtotalAmount.Value) / 3;
                body2text = body2text.Replace("lblTotalAmount", HiddenFieldtotalAmount.Value);
                amountpart1 = Math.Round(amt1, 2).ToString();
                amountpart2 = Math.Round(amt2, 2).ToString();
                amountpart3 = Math.Round(amt3, 2).ToString();
                //txtAmount.Text = amountpart1;
                //txtAmount.ReadOnly = true;
            }
            body2text = body2text.Replace("lblamountpart1", amountpart1);
            body2text = body2text.Replace("lblamountpart2", amountpart2);
            body2text = body2text.Replace("lblamountpart3", amountpart3);

            body2text = body2text.Replace("lblCustomerName", CustomerName);
            body2text = body2text.Replace("lblFooterDate", DateTime.Now.Date.ToShortDateString());

            LiteralBody2.Text = body2text;

            hidDownPayment.Value = amountpart1;
        }

        private string createHeaderEstimate(string InvoiceNo, int[] estimateIdList, int[] productIdList)
        {
            string Customername = "";
            string customercellphone = "";
            string customeraddress = "";
            string citystatezip = "";
            string customerhousePh = "";
            string customeremail = "";
            string Joblocation = "";

            string bodycontent = "";
            string TotalAmount = "";
            string Amounttpart1 = "";
            string AmountPart2 = "";
            string Amountpart3 = "";
            string customerId = "";
            string quoteNo = "";
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productIdList[0]);
            string result = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0][0].ToString();
            }
            DataSet DS1 = new DataSet();
            DS1 = shuttersBLL.Instance.FetchContractdetails(estimateIdList[0], productIdList[0]);

            if (DS1 != null)
            {
                Customername = DS1.Tables[0].Rows[0]["CustomerName"].ToString();
                customercellphone = DS1.Tables[0].Rows[0]["CellPh"].ToString();
                customerhousePh = DS1.Tables[0].Rows[0]["HousePh"].ToString();
                customeraddress = DS1.Tables[0].Rows[0]["CustomerAddress"].ToString();
                citystatezip = DS1.Tables[0].Rows[0]["citystatezip"].ToString();
                customeremail = DS1.Tables[0].Rows[0]["Email"].ToString();
                Joblocation = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                customerId = "C" + DS1.Tables[0].Rows[0]["CustomerId"].ToString();
                // quoteNo = DS1.Tables[0].Rows[0]["QuoteNumber"].ToString();
                ViewState["customeremail"] = customeremail;
                //TotalAmount = txtTotal.Text;
                //bodycontent = "Proposal " + RadioButtonList1.SelectedValue + ": To supply and install (" + DS1.Tables[0].Rows[0]["Quantity"].ToString() + ") pair(s) of " + DS1.Tables[0].Rows[0]["ShutterName"].ToString() + DS1.Tables[0].Rows[0]["SpecialInstruction"].ToString() + " Job location:( " + Joblocation + ") Total price : $" + TotalAmount;
                //decimal amt = Convert.ToDecimal(txtTotal.Text) / 3;
                //Amounttpart1 = amt.ToString();
                //AmountPart2 = amt.ToString();
                //Amountpart3 = amt.ToString();

            }

            DataSet dsContract = new DataSet();
            for (int i = 0; i <= estimateIdList.Length - 1; i++)
            {
                if (estimateIdList[i] == 0)
                    break;

                dsContract = shuttersBLL.Instance.FetchContractdetails(estimateIdList[i], productIdList[i]);
                if (i == 0)
                {
                    string s = dsContract.Tables[0].Rows[0]["QuoteNumber"].ToString();
                    string[] qno = s.Split('-');
                    quoteNo = qno[1];
                }
                else
                {
                    string s = dsContract.Tables[0].Rows[0]["QuoteNumber"].ToString();
                    string[] qno = s.Split('-');
                    quoteNo = quoteNo + "," + qno[1];
                }
            }

            QuoteNumber = quoteNo;
            result = result.Replace("lblsubmittedto", Customername);
            result = result.Replace("lblDate", DateTime.Now.Date.ToShortDateString());
            result = result.Replace("lblhousePhone", customerhousePh);
            result = result.Replace("lblAddress", customeraddress);
            result = result.Replace("lblCell", customercellphone);
            result = result.Replace("lblcitystatezip", citystatezip);
            result = result.Replace("lblemail", customeremail);
            result = result.Replace("lblJobLocation", Joblocation);
            result = result.Replace("txtBodyContent", bodycontent);
            result = result.Replace("lblTotalAmount", TotalAmount);
            result = result.Replace("lblamountpart1", Amounttpart1);
            result = result.Replace("lblamountpart2", AmountPart2);
            result = result.Replace("lblamountpart3", Amountpart3);
            result = result.Replace("lblCustomerName", Customername);
            result = result.Replace("lblCustomerId", customerId);
            result = result.Replace("lblJobNo", quoteNo);

            return result;
        }

        private string createBodyEstimate(string InvoiceNo, int estimateId, int productType)
        {
            string Quantity = "";
            string Style = "";
            string Color = "";
            string Joblocation = "";

            string ProposalAmtA = "";
            string ProposalAmtB = "";
            string SpecialInstructions = "";
            string WorkArea = "";
            string ShutterTops = "";
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            string result = ds.Tables[0].Rows[0][1].ToString();

            DataSet DS1 = new DataSet();
            DS1 = shuttersBLL.Instance.FetchContractdetails(estimateId, productType);

            if (DS1 != null)
            {
                Quantity = DS1.Tables[0].Rows[0]["Quantity"].ToString();
                Style = DS1.Tables[0].Rows[0]["Productname"].ToString();
                Color = DS1.Tables[0].Rows[0]["ColorName"].ToString();
                Joblocation = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                SpecialInstructions = DS1.Tables[0].Rows[0]["SpecialInstruction"].ToString();
                WorkArea = DS1.Tables[0].Rows[0]["WorkArea"].ToString();
                ShutterTops = DS1.Tables[0].Rows[0]["ShutterTop"].ToString();
                ProposalAmtA = DS1.Tables[0].Rows[0]["AmountA"].ToString();
                ProposalAmtB = DS1.Tables[0].Rows[0]["AmountB"].ToString();
            }
            result = result.Replace("lblQuantity", Quantity);
            result = result.Replace("lblStyle", Style);
            result = result.Replace("lblColor", Color);
            result = result.Replace("lblJobLocation", Joblocation);
            result = result.Replace("lblSpecialInstructions", SpecialInstructions);
            result = result.Replace("lblWorkArea", WorkArea);
            result = result.Replace("lblShutterTops", ShutterTops);
            result = result.Replace("lblProposalAmtA", ProposalAmtA);
            result = result.Replace("lblProposalAmtB", ProposalAmtB);
            result = result + "<br /><hr color='#000000' width='450' />";
            return result;
        }

        private string createBodyEstimateCustom(string InvoiceNo, int estimateId, int productType)
        {
            string ProposalAmtA = "", SpecialInstruction = string.Empty, WorkArea = string.Empty;
            string proposalTerm = "";
            string CustomerSuppliedMaterial = "";
            string MaterialDumpsterStorage = "";
            string PermitRequired = "";
            string HabitatForHumanityPickUp = "";

            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            string result = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0][1].ToString();
            }
            DataSet DS1 = new DataSet();
            DS1 = shuttersBLL.Instance.FetchContractdetails(estimateId, productType);

            if (DS1 != null)
            {
                proposalTerm = DS1.Tables[0].Rows[0]["ProposalTerms"].ToString();
                ProposalAmtA = DS1.Tables[0].Rows[0]["ProposalCost"].ToString();
                SpecialInstruction = DS1.Tables[0].Rows[0]["SpecialInstruction"].ToString();
                WorkArea = DS1.Tables[0].Rows[0]["WorkArea"].ToString();
                if (DS1.Tables[0].Rows[0]["IsCustSupMatApplicable"] != null && DS1.Tables[0].Rows[0]["IsCustSupMatApplicable"].ToString() != "")
                {
                    if (Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsCustSupMatApplicable"].ToString()) == false)
                    {
                        CustomerSuppliedMaterial = "<strong>Customer Supplied Material : </strong>" + DS1.Tables[0].Rows[0]["CustSuppliedMaterial"].ToString();
                    }
                }
                if (DS1.Tables[0].Rows[0]["IsMatStorageApplicable"] != null && DS1.Tables[0].Rows[0]["IsMatStorageApplicable"].ToString() != "")
                {
                    if (Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsMatStorageApplicable"].ToString()) == false)
                    {
                        MaterialDumpsterStorage = "<strong>Material/Dumpster Storage : </strong>" + DS1.Tables[0].Rows[0]["MaterialStorage"].ToString();
                    }
                }
                if (DS1.Tables[0].Rows[0]["IsPermitRequired"] != null && DS1.Tables[0].Rows[0]["IsPermitRequired"].ToString() != "")
                {
                    if (Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsPermitRequired"].ToString()) == true)
                    {
                        PermitRequired = "Permit Required";
                    }
                }
                if (DS1.Tables[0].Rows[0]["IsHabitat"] != null && DS1.Tables[0].Rows[0]["IsHabitat"].ToString() != "")
                {
                    if (Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsHabitat"].ToString()) == true)
                    {
                        HabitatForHumanityPickUp = "Habitat For Humanity Pick Up";
                    }
                }
            }

            result = result.Replace("lblSpecialInstructions", SpecialInstruction);
            result = result.Replace("lblProposalAmtA", ProposalAmtA);
            result = result.Replace("lblProposalTerms", proposalTerm);
            result = result.Replace("lblWorkArea", WorkArea);
            result = result.Replace("lblCustomerSuppliedMaterial", CustomerSuppliedMaterial);
            result = result.Replace("lblMaterialDumpsterStorage", MaterialDumpsterStorage);
            result = result.Replace("lblPermitRequired", PermitRequired);
            result = result.Replace("lblHabitatForHumanityPickUp", HabitatForHumanityPickUp);

            result = result + "<br /><hr color='#000000' width='450' />";
            return result;
        }

        private string createFooterEstimate(string InvoiceNo, int estimateId, int productType)
        {
            // literalbody2();

            string CustomerName = "";


            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            string result = @"<table align=""center"" width=""100%"" bordercolor=""#666666"" bgcolor=""#FFFFFF"" border=""0"" class=""no_line"" cellspacing=""0"" cellpadding=""0"" style=""font-family: verdana, geneva, sans-serif;     font-size: 8pt;"">        <tbody>    <tr>            <td>            <img src=""../img/bar3.png"" width=""100%"" height=""40"" />        </td>    </tr></tbody></table>";
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = result + ds.Tables[0].Rows[0][2].ToString();
            }
            DS = new DataSet();
            DS = shuttersBLL.Instance.FetchContractdetails(estimateId, productType);

            if (DS != null)
            {
                CustomerName = DS.Tables[0].Rows[0]["CustomerName"].ToString();
            }
            literalbody2(CustomerName);
            //result = result.Replace("lblCustomerName", CustomerName);
            //result = result.Replace("lblFooterDate", DateTime.Now.Date.ToShortDateString());

            return result;
        }

        private string createWorkOrder(string InvoiceNo, int estimateId)
        {
            return pdf_BLL.Instance.CreateWorkOrder(InvoiceNo, estimateId, productType, customerId, "", 1);
        }

        private void GeneratePDF(string path, string fileName, bool download, string text, bool IsFooter)//download set to false in calling method
        {
            #region sandeep patil logic
            /*
            var document = new Document();
            //text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar2.png'", "src='~/img/Bar2.png'");
            //text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/LogoJG_PDF.png'", "src='~/img/LogoJG_PDF.png'");
            //text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar1.png'", "src='~/img/Bar1.png'");
            //text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png'", "src='~/img/ma.png'");

            //text = text.Replace("background:url(http://176.31.133.194:205/img/logo_bg.png)", "src='~/img/logo_bg.png'");
            //text = text.Replace("background: url(http://176.31.133.194:205/img/logo_bg.png)", "src='~/img/logo_bg.png'");




            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar2.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/LogoJG_PDF.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar1.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png'", "");

            text = text.Replace("background:url(http://176.31.133.194:205/img/logo_bg.png)", "");
            text = text.Replace("background: url(http://176.31.133.194:205/img/logo_bg.png)", "");

            ////////////

            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar2.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/LogoJG_PDF.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar1.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png'", "");
            text = text.Replace("src=\"jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png\"", "");
            text = text.Replace("background:url(http://176.31.133.194:205/img/logo_bg.png)", "");
            text = text.Replace("background: url(http://176.31.133.194:205/img/logo_bg.png)", "");

            text = text.Replace("src=jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/JG-Logo.gif", "");
            // FileStream FS = new FileStream(path + fileName, FileMode.Create);
            try
            {
                //if (download)
                //{
                //    Response.Clear();
                //    Response.ContentType = "application/pdf";
                //    PdfWriter.GetInstance(document, Response.OutputStream);
                //}
                //else
                //{
                //    //PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));
                //    PdfWriter.GetInstance(document, FS);
                //}
                // generates the grid first
                StringBuilder strB = new StringBuilder();
                strB.Append(text);
                document.Open();
                // now read the Grid html one by one and add into the document object

                //using (TextReader sReader = new StringReader(strB.ToString()))
                //{
                //    document.Open();
                //    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                //    foreach (IElement elm in list)
                //    {
                //        document.Add(elm);
                //    }
                //}

                //var htmlContent = strB.ToString();
                //var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                //var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
                //byte[] byteData = pdfBytes;
                string filePath = Server.MapPath("~/CustomerDocs/Pdfs/wkhtmltopdf.exe");
                // string filePath = Server.MapPath("/CustomerDocs/Pdfs/wkhtmltox-0.12.2.4_msvc2013-win32.exe");
                byte[] byteData = ConvertHtmlToByte(strB.ToString(), "", "", filePath, IsFooter);
                //string destination = Server.MapPath("~/CustomerDocs/Pdfs/") + fileName;
                //byte[] byteData = HTMLToPdf(text, destination);
                ////HTMLToPdf(text, destination);
                if (byteData != null)
                {
                    StreamByteToPDF(byteData, Server.MapPath("~/CustomerDocs/Pdfs/") + fileName);
                }
            }
            catch (Exception ex)
            {
                //throw ee;
                logManager.writeToLog(ex, "ShutterProposal", Request.ServerVariables["remote_addr"].ToString());
            }
            //finally
            //{
            //    if (document.IsOpen())
            //        document.Close();

            //}
            document.Close();
            */
            #endregion

            #region PDF generation through Exe

            //var document = new Document();
            //StringBuilder strnew = new StringBuilder();
            //string filePath = string.Empty;
            //strnew.Append("In Genrate PDF");
            //// FileStream FS = new FileStream(path + fileName, FileMode.Create);
            //try
            //{
            //    //if (download)
            //    //{
            //    //    Response.Clear();
            //    //    Response.ContentType = "application/pdf";
            //    //    PdfWriter.GetInstance(document, Response.OutputStream);
            //    //}
            //    //else
            //    //{
            //    //    //PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));
            //    //    PdfWriter.GetInstance(document, FS);
            //    //}
            //    // generates the grid first
            //    StringBuilder strB = new StringBuilder();
            //    strB.Append(text);
            //    strnew.Append(text);
            //    // now read the Grid html one by one and add into the document object

            //    //using (TextReader sReader = new StringReader(strB.ToString()))
            //    //{
            //    //    document.Open();
            //    //    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
            //    //    foreach (IElement elm in list)
            //    //    {
            //    //        document.Add(elm);
            //    //    }
            //    //}

            //    // filePath = Server.MapPath("/CustomerDocs/Pdfs/wkhtmltopdf.exe");
            //    filePath = "C:\\inetpub\\wwwroot\\TestPublishNew\\CustomerDocs\\Pdfs\\wkhtmltopdf.exe";

            //    string filepathnew = "C:\\inetpub\\wwwroot\\TestPublishNew\\CustomerDocs\\Pdfs\\";
            //   // string filepathnew = "E:\\JGPTest\\JG_Prospect.root\\JG_Prospect\\JG_Prospect.web\\CustomerDocs\\Pdfs\\";

            //    byte[] byteData = ConvertHtmlToByte(Convert.ToString(strB), "", "", filePath, IsFooter);
            //    if (byteData != null)
            //    {//E:\JGPTest\JG_Prospect.root\JG_Prospect\JG_Prospect.web\CustomerDocs\Pdfs\wkhtmltopdf.exe
            //        strnew.Append("before convert");
            //        StreamByteToPDF(byteData, filepathnew + fileName);
            //        //StreamByteToPDF(byteData, Server.MapPath("/CustomerDocs/Pdfs/") + fileName);
            //        strnew.Append("after convert");
            //    }

            //    // lblpath.Text = filePath;

            //}
            //catch (Exception ex)
            //{
            //    //throw ee;
            //    lblerrornew.Text = strnew + ex.Message + ex.StackTrace;
            //    logManager.writeToLog(ex, "ShutterProposal", Request.ServerVariables["remote_addr"].ToString());
            //    // lblpath.Text = filePath;
            //}
            ////finally
            ////{
            ////    if (document.IsOpen())
            ////        document.Close();

            ////}

            #endregion

            var document = new Document();

            // FileStream FS = new FileStream(path + fileName, FileMode.Create);
            try
            {
                //if (download)
                //{
                //    Response.Clear();
                //    Response.ContentType = "application/pdf";
                //    PdfWriter.GetInstance(document, Response.OutputStream);
                //}
                //else
                //{
                //    //PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));
                //    PdfWriter.GetInstance(document, FS);
                //}
                // generates the grid first
                StringBuilder strB = new StringBuilder();
                strB.Append(text);
                // now read the Grid html one by one and add into the document object

                //using (TextReader sReader = new StringReader(strB.ToString()))
                //{
                //    document.Open();
                //    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                //    foreach (IElement elm in list)
                //    {
                //        document.Add(elm);
                //    }
                //}

                string filePath = Server.MapPath("~/CustomerDocs/Pdfs/wkhtmltopdf.exe");
                byte[] byteData = ConvertHtmlToByte(strB.ToString(), "", "", filePath);
                if (byteData != null)
                {
                    StreamByteToPDF(byteData, Server.MapPath("/CustomerDocs/Pdfs/") + fileName);
                }
            }
            catch (Exception ex)
            {
                //throw ee;
                logManager.writeToLog(ex, "Custom", Request.ServerVariables["remote_addr"].ToString());
            }
            //finally
            //{
            //    if (document.IsOpen())
            //        document.Close();

            //}

        }

        public static byte[] ConvertHtmlToByte(string HtmlData, string headerPath, string footerPath, string filePath)
        {
            string url = ConfigurationManager.AppSettings["URL"].ToString();
            //ContractFooter cf=new ContractFooter();
            footerPath = url + @"/Sr_App/ContractFooter.aspx";

            //headerPath = headerPath.Replace(@"src=""../img", @"src=""" + url + @"/img");

            Process p;
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = filePath;
            psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);

            // run the conversion utility
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            // note: that we tell wkhtmltopdf to be quiet and not run scripts
            string args = "-q -n ";
            args += "--disable-smart-shrinking ";
            args += "--orientation Portrait ";
            args += "--outline-depth 0 ";
            //args += "--header-spacing 140 ";
            //args += "--default-header ";

            if (footerPath != string.Empty)
            {
                args += "--footer-html " + footerPath + " ";

            }
            if (headerPath != string.Empty)
            {
                args += "--header-spacing 2 ";
                args += "--header-html " + headerPath + " ";

            }
            //args += "--header-font-size  20 ";
            args += "--page-size A4 --encoding windows-1250";
            args += " - -";

            psi.Arguments = args;

            p = Process.Start(psi);

            try
            {
                using (StreamWriter stdin = new StreamWriter(p.StandardInput.BaseStream, Encoding.UTF8))
                {
                    stdin.AutoFlush = true;
                    stdin.Write(HtmlData);
                }

                //read output
                byte[] buffer = new byte[32768];
                byte[] file;
                using (var ms = new MemoryStream())
                {
                    while (true)
                    {
                        int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                            break;
                        ms.Write(buffer, 0, read);
                    }
                    file = ms.ToArray();
                }

                p.StandardOutput.Close();
                // wait or exit
                p.WaitForExit(60000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                if (returnCode == 0)
                    return file;
                //else
                //    log.Error("Could not create PDF, returnCode:" + returnCode);
            }
            catch (Exception ex)
            {
                // log.Error("Could not create PDF", ex);
            }
            finally
            {
                p.Close();
                p.Dispose();
            }
            return null;
        }
        private void GenerateWorkOrderPDF(string path, string fileName, bool download, string text, bool IsFooter)//download set to false in calling method
        {
            #region Generate WorkOrderPdf
            /*
            var document = new Document();

            text = text.Replace("<strong>Terms &amp; Conditions</strong>", "<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><strong>Terms &amp; Conditions</strong>");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar2.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/LogoJG_PDF.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar1.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png'", "");

            //text = text.Replace("background:url(http://176.31.133.194:205/img/logo_bg.png)", "");
            //text = text.Replace("background: url(http://176.31.133.194:205/img/logo_bg.png)", "");


            ////////////

            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar2.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/LogoJG_PDF.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/Bar1.png'", "");
            text = text.Replace("src='jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png'", "");
            text = text.Replace("src=\"jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/ma.png\"", "");
            text = text.Replace("background:url(http://176.31.133.194:205/img/logo_bg.png)", "");
            text = text.Replace("background: url(http://176.31.133.194:205/img/logo_bg.png)", "");

            text = text.Replace("src=jgp.jmgroveconstruction.com.192-185-6-42.secure23.win.hostgator.com/img/JG-Logo.gif", "");
            // FileStream FS = new FileStream(path + fileName, FileMode.Create);
            try
            {
                //if (download)
                //{
                //    Response.Clear();
                //    Response.ContentType = "application/pdf";
                //    PdfWriter.GetInstance(document, Response.OutputStream);
                //}
                //else
                //{
                //    //PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));
                //    PdfWriter.GetInstance(document, FS);
                //}
                // generates the grid first
                StringBuilder strB = new StringBuilder();
                strB.Append(text);
                document.Open();
                // now read the Grid html one by one and add into the document object

                //using (TextReader sReader = new StringReader(strB.ToString()))
                //{
                //    document.Open();
                //    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                //    foreach (IElement elm in list)
                //    {
                //        document.Add(elm);
                //    }
                //}

                //var htmlContent = strB.ToString();
                //var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                //var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
                //byte[] byteData = pdfBytes;
                //string filePath = Server.MapPath("~/CustomerDocs/Pdfs/wkhtmltopdf.exe");
                // string filePath = Server.MapPath("/CustomerDocs/Pdfs/wkhtmltox-0.12.2.4_msvc2013-win32.exe");
                // byte[] byteData = ConvertHtmlToByte(strB.ToString(), "", "", filePath, IsFooter);
                string destination = Server.MapPath("~/CustomerDocs/Pdfs/") + fileName;
                byte[] byteData = WorkOrderPdf(text, destination);
                //HTMLToPdf(text, destination);
                if (byteData != null)
                {
                    StreamByteToPDF(byteData, Server.MapPath("~/CustomerDocs/Pdfs/") + fileName);
                }
            }
            catch (Exception ex)
            {
                //throw ee;
                logManager.writeToLog(ex, "ShutterProposal", Request.ServerVariables["remote_addr"].ToString());
            }
        */
            #endregion

            #region new code
            var document = new Document();
            StringBuilder strnew = new StringBuilder();
            string filePath = string.Empty;

            StringBuilder strB = new StringBuilder();
            strB.Append(text);
            document.Open();

            filePath = "C:\\inetpub\\wwwroot\\TestPublishNew\\CustomerDocs\\Pdfs\\wkhtmltopdf.exe";
            string filepathnew = "C:\\inetpub\\wwwroot\\TestPublishNew\\CustomerDocs\\Pdfs\\";
            string fullPath = filepathnew + fileName;
            byte[] byteData = WorkOrderPdf(text, fullPath);
            //HTMLToPdf(text, destination);
            if (byteData != null)
            {
                StreamByteToPDF(byteData, fullPath);
            }

            #endregion
            //finally
            //{
            //    if (document.IsOpen())
            //        document.Close();

            //}
            document.Close();
        }



        public byte[] HTMLToPdf(string HTML, string FilePath)
        {



            //StringReader sr = new StringReader(HTML);

            //Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //    pdfDoc.Open();
            //    htmlparser.Parse(sr);
            //    pdfDoc.Close();
            //    byte[] bytes = memoryStream.ToArray();
            //    memoryStream.Close();
            //    return bytes;
            //}

            //HtmlToPdfConverter nRecohtmltoPdfObj = new HtmlToPdfConverter();
            //nRecohtmltoPdfObj.Orientation = PageOrientation.Portrait;
            ////nRecohtmltoPdfObj.PageFooterHtml = CreatePDFFooter();
            //nRecohtmltoPdfObj.CustomWkHtmlArgs =
            //"--margin-top 35 --header-spacing 0 --margin-left 0 --margin-right 0";
            //return nRecohtmltoPdfObj.GeneratePdf(HTML);
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(HTML);
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        return bytes;
                    }
                }
            }
        }



        public byte[] WorkOrderPdf(string HTML, string FilePath)
        {


            //HtmlToPdfConverter nRecohtmltoPdfObj = new HtmlToPdfConverter();
            //nRecohtmltoPdfObj.Orientation = PageOrientation.Portrait;
            //nRecohtmltoPdfObj.PageFooterHtml = CreatePDFFooter();
            //nRecohtmltoPdfObj.CustomWkHtmlArgs =
            //"--margin-top 35 --header-spacing 0 --margin-left 0 --margin-right 0";
            //return nRecohtmltoPdfObj.GeneratePdf(HTML);

            using (StringWriter ss = new StringWriter())
            {
                using (HtmlTextWriter hh = new HtmlTextWriter(ss))
                {
                    StringBuilder sbs = new StringBuilder();
                    sbs.Append(HTML);
                    StringReader srn = new StringReader(sbs.ToString());
                    Document pdfDocn = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparsern = new HTMLWorker(pdfDocn);
                    using (MemoryStream memoryStreamNew = new MemoryStream())
                    {
                        PdfWriter writern = PdfWriter.GetInstance(pdfDocn, memoryStreamNew);
                        pdfDocn.Open();
                        htmlparsern.Parse(srn);
                        pdfDocn.Close();
                        byte[] bytes = memoryStreamNew.ToArray();
                        memoryStreamNew.Close();
                        return bytes;
                    }
                }
            }
        }


        public static void StreamByteToPDF(byte[] byteData, string filePathPhysical)
        {
            try
            {
                if (byteData != null)
                {
                    if (File.Exists(filePathPhysical))
                    {
                        File.Delete(filePathPhysical);

                    }
                    // string filename = "C:\\Reports\\Newsamplecif.pdf";
                    FileStream fs = new FileStream(filePathPhysical, FileMode.Create, FileAccess.ReadWrite);
                    //Read block of bytes from stream into the byte array
                    fs.Write(byteData, 0, byteData.Length);

                    //Close the File Stream
                    fs.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        public static byte[] ConvertHtmlToByte(string HtmlData, string headerPath, string footerPath, string filePath, bool Isfooter)
        {
            if (Isfooter)
            {

                string url = ConfigurationManager.AppSettings["URL"].ToString();
                ContractFooter cf = new ContractFooter();
                footerPath = url + @"/Sr_App/ContractFooter.aspx";



            }

            Process p;
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = filePath;
            psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);

            // run the conversion utility
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            // note: that we tell wkhtmltopdf to be quiet and not run scripts
            string args = "-q -n ";
            args += "--disable-smart-shrinking ";
            args += "--orientation Portrait ";
            args += "--outline-depth 0 ";
            //args += "--header-spacing 140 ";
            //args += "--default-header ";

            if (footerPath != string.Empty)
            {
                args += "--footer-html " + footerPath + " ";

            }
            if (headerPath != string.Empty)
            {
                args += "--header-spacing 2 ";
                args += "--header-html " + headerPath + " ";

            }
            //args += "--header-font-size  20 ";
            args += "--page-size A4 --encoding windows-1250";
            args += " - -";

            psi.Arguments = args;

            p = Process.Start(psi);

            try
            {
                using (StreamWriter stdin = new StreamWriter(p.StandardInput.BaseStream, Encoding.UTF8))
                {
                    stdin.AutoFlush = true;
                    stdin.Write(HtmlData);
                }

                //read output
                byte[] buffer = new byte[32768];
                byte[] file;
                using (var ms = new MemoryStream())
                {
                    while (true)
                    {
                        int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                            break;
                        ms.Write(buffer, 0, read);
                    }
                    file = ms.ToArray();
                }

                p.StandardOutput.Close();
                // wait or exit
                p.WaitForExit(60000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                if (returnCode == 0)
                    return file;
                //else
                //    log.Error("Could not create PDF, returnCode:" + returnCode);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                p.Close();
                p.Dispose();
            }
            return null;
        }
        protected bool CheckCustomerEmail()
        {
            string finalEmail = GetCustomerEmail();
            if (finalEmail == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private string GetCustomerEmail()
        {
            string finalEmail = string.Empty;
            DataSet ds = new DataSet();
            if (Session["CustomerId"].ToString() != null)
                ds = new_customerBLL.Instance.GetCustomerDetails(Convert.ToInt32(Session["CustomerId"].ToString()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                string email1 = ds.Tables[0].Rows[0]["Email"].ToString();
                string email2 = ds.Tables[0].Rows[0]["Email2"].ToString();
                string email3 = ds.Tables[0].Rows[0]["Email3"].ToString();

                if (email1 != "")
                {
                    finalEmail = email1;
                }
                else if (email2 != "")
                {
                    finalEmail = email2;
                }
                else if (email3 != "")
                {
                    finalEmail = email3;
                }
            }
            return finalEmail;
        }

        public DataSet fetchCustomerEmailTemplate()
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(20);
            return ds;
        }

        protected void SendEmailToCustomer(string contractName)
        {
            string finalEmail = GetCustomerEmail();
            if (finalEmail != string.Empty)
            {
                //bool emailStatus = true;
                string htmlBody = string.Empty;
                try
                {

                    string mailId = finalEmail;
                    // string vendorName = dr["VendorName"].ToString();

                    MailMessage m = new MailMessage();
                    SmtpClient sc = new SmtpClient();

                    string userName = ConfigurationManager.AppSettings["CustomerEmailUsername"].ToString();
                    string password = ConfigurationManager.AppSettings["CustomerEmailPassword"].ToString();

                    m.From = new MailAddress(userName, "JMGROVECONSTRUCTION");
                    m.To.Add(new MailAddress(mailId, ""));
                    m.Subject = "JMGrove proposal " + "C" + customerId.ToString() + "-" + QuoteNumber;
                    m.IsBodyHtml = true;
                    DataSet dsEmailTemplate = fetchCustomerEmailTemplate();

                    if (dsEmailTemplate.Tables[0].Rows.Count > 0)
                    {
                        string templateHeader = dsEmailTemplate.Tables[0].Rows[0][0].ToString();
                        StringBuilder tHeader = new StringBuilder();
                        tHeader.Append(templateHeader);
                        var replacedHeader = tHeader.Replace("src=\"../img/Email art header.png\"", "src=cid:myImageHeader")
                                                    .Replace("lblJobId", "C" + customerId.ToString() + "-" + QuoteNumber)
                                                    .Replace("lblCustomerId", "C" + customerId.ToString());
                        htmlBody = replacedHeader.ToString();
                        htmlBody += "</br></br></br>";
                        string templateBody = dsEmailTemplate.Tables[0].Rows[0][1].ToString();

                        StringBuilder tbody = new StringBuilder();
                        tbody.Append(templateBody);

                        htmlBody += templateBody.ToString();

                        htmlBody += "</br></br></br>";

                        string templateFooter = dsEmailTemplate.Tables[0].Rows[0][2].ToString();
                        StringBuilder tFooter = new StringBuilder();
                        tFooter.Append(templateFooter);
                        var replacedFooter = tFooter.Replace("src=\"../img/JG-Logo.gif\"", "src=cid:myImageLogo")
                                                      .Replace("src=\"../img/Email footer.png\"", "src=cid:myImageFooter")
                                                   .Replace("src=\"../img/facebook.jpg\"", "src=cid:myImageFooterF")
                                                   .Replace("src=\"../img/twitter.jpg\"", "src=cid:myImageFooterT")
                                                  .Replace("src=\"../img/g+.png\"", "src=cid:myImageFooterG");
                        htmlBody += replacedFooter.ToString();
                    }
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");

                    string imageSourceHeader = Server.MapPath(@"~\img") + @"\Email art header.png";
                    LinkedResource theEmailImageHeader = new LinkedResource(imageSourceHeader);
                    theEmailImageHeader.ContentId = "myImageHeader";

                    string imageSourceLogo = Server.MapPath(@"~\img") + @"\JG-Logo.gif";
                    LinkedResource theEmailImageLogo = new LinkedResource(imageSourceLogo);
                    theEmailImageLogo.ContentId = "myImageLogo";

                    string imageFooter = Server.MapPath(@"~\img") + @"\Email footer.png";
                    LinkedResource theImageFooter = new LinkedResource(imageFooter);
                    theImageFooter.ContentId = "myImageFooter";

                    string imageFooterF = Server.MapPath(@"~\img") + @"\facebook.jpg";
                    LinkedResource theImageFooterF = new LinkedResource(imageFooterF);
                    theImageFooterF.ContentId = "myImageFooterF";

                    string imageFooterT = Server.MapPath(@"~\img") + @"\twitter.jpg";
                    LinkedResource theImageFooterT = new LinkedResource(imageFooterT);
                    theImageFooterT.ContentId = "myImageFooterT";

                    string imageFooterG = Server.MapPath(@"~\img") + @"\g+.png";
                    LinkedResource theImageFooterG = new LinkedResource(imageFooterG);
                    theImageFooterG.ContentId = "myImageFooterG";

                    //Add the Image to the Alternate view
                    htmlView.LinkedResources.Add(theEmailImageHeader);
                    htmlView.LinkedResources.Add(theEmailImageLogo);
                    htmlView.LinkedResources.Add(theImageFooterF);
                    htmlView.LinkedResources.Add(theImageFooter);
                    htmlView.LinkedResources.Add(theImageFooterT);
                    htmlView.LinkedResources.Add(theImageFooterG);

                    m.AlternateViews.Add(htmlView);

                    m.Body = htmlBody;

                    string sourceDirContract = Server.MapPath("~/CustomerDocs/Pdfs/");
                    if (contractName != string.Empty)
                    {
                        Attachment attachment = new Attachment(sourceDirContract + "\\" + contractName);
                        attachment.Name = contractName;
                        m.Attachments.Add(attachment);
                    }
                    DataSet ds = AdminBLL.Instance.FetchCustomerAttachments();
                    string sourceDirDocs = Server.MapPath("~/CustomerDocs/CustomerEmailDocument/");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string filename = ds.Tables[0].Rows[i][i].ToString();
                            Attachment attachment1 = new Attachment(sourceDirDocs + "\\" + filename);
                            attachment1.Name = filename;
                            m.Attachments.Add(attachment1);
                        }
                    }

                    sc.UseDefaultCredentials = false;
                    sc.Host = "jmgrove.fatcow.com";
                    //sc.Host = "smtp.gmail.com";
                    sc.Port = 25;


                    sc.Credentials = new System.Net.NetworkCredential(userName, password);
                    sc.EnableSsl = false; // runtime encrypt the SMTP communications using SSL
                    //sc.EnableSsl = true; // runtime encrypt the SMTP communications using SSL
                    try
                    {
                        sc.Send(m);

                    }
                    catch (Exception ex)
                    {


                    }

                }



                catch (Exception ex)
                {
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('" + ex.Message + "');", true);
                }
            }
        }
        protected void btnSaveEmailSold_Click(object sender, EventArgs e)
        {
            if (txtEmailSold.Text.Trim() != "")
            {
                bool result = new_customerBLL.Instance.UpdateEmailOfCustomer(Convert.ToInt32(Session["CustomerId"].ToString()), txtEmailSold.Text.Trim());
                SoldTasks(true);
            }
        }
        protected void btnSold_Click(object sender, EventArgs e)
        {
            #region Original code....

            if (chkSendEmailSold.Checked == true)
            {
                bool result = CheckCustomerEmail();
                if (!result)
                {
                    mpeCustomerEmail.Show();
                    return;
                }
                else
                {
                    SoldTasks(true);
                }
            }
            else
            {
                SoldTasks(false);
            }
            Session["Proposal"] = null;

            #endregion


        }

        protected void SoldTasks(bool IsEmail)
        {
            bool stat = true;
            string str_Emails = string.Empty;
            HidCV.Value = "12";
            try
            {
                if (!IsPageRefresh)
                {
                    if (chkedit.Checked && txtauthpass.Text != "")
                    {
                        string adminCode = AdminBLL.Instance.GetAdminCode();
                        if (adminCode != txtauthpass.Text.Trim())
                        {
                            CV.ErrorMessage = "Invalid Admin Code";
                            CV.ForeColor = System.Drawing.Color.Red;
                            CV.IsValid = false;
                            CV.Visible = true;
                            chkedit.Checked = true;
                            mp_sold.Show();
                            return;
                        }
                    }

                    if (stat)
                    {
                        int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                        string soldJobID = string.Empty;
                        string s1 = Session["EstID"].ToString();
                        string[] EstID = s1.Split(',');
                        int count = EstID.Count();

                        string paymentMode = string.Empty, checkNo = string.Empty, creditCardDetails = string.Empty;
                        decimal amount = 0;
                        if (ddlpaymode.SelectedIndex != -1)
                        {
                            paymentMode = ddlpaymode.SelectedItem.Text;
                            Session["paymentMode"] = ddlpaymode.SelectedItem.Text;
                        }
                        if (txtAmount.Text.Trim() != "")
                        {
                            amount = Convert.ToDecimal(txtAmount.Text);
                            Session["PaymentAmount"] = amount;
                        }
                        else
                        {
                            if (hidDownPayment.Value != "")
                            {
                                amount = Convert.ToDecimal(hidDownPayment.Value);
                                Session["PaymentAmount"] = amount;
                            }
                        }
                        checkNo = txtchequeno.Text.Trim();
                        Session["checkNo"] = checkNo;
                        creditCardDetails = txtcardholderNm.Text.Trim();
                        Session["creditCardDetails"] = creditCardDetails;
                        foreach (TextBox textBox in pnlControls.Controls.OfType<TextBox>())
                        {
                            str_Emails += textBox.Text + ",";
                        }
                        shuttersBLL.Instance.UpdateEmails(str_Emails, Convert.ToInt32(Session["CustomerId"]));
                        string tempInvoiceFileName = "Invoice" + DateTime.Now.Ticks + ".pdf";
                        soldJobID = shuttersBLL.Instance.UpdateShutterEstimate(s1, "Sold-in Progress", (Convert.ToInt32(Session["CustomerId"].ToString())), userId, paymentMode, amount, checkNo, creditCardDetails, tempInvoiceFileName);

                        if (soldJobID != string.Empty)
                        {
                            string mailid = ViewState["customeremail"].ToString();

                            string path = Server.MapPath("~/CustomerDocs/Pdfs/");

                            // Create and display the value of two GUIDs.
                            string g = Guid.NewGuid().ToString().Substring(0, 5);

                            // string tempInvoiceFileName = "Invoice" + DateTime.Now.Ticks + ".pdf";
                            string originalInvoiceFileName = "Invoice" + ".pdf";


                            // Create Invoice with Pdf for Transaction..... Initially it False....altered to true...
                            GeneratePDF(path, tempInvoiceFileName, false, createEstimate("InvoiceNumber-" + Session["CustomerId"].ToString(), Convert.ToInt32(Session["CustomerId"].ToString())), true);

                            if ((Session["FormDataObjects"] != null) || (productId > 0))
                            {
                                List<Tuple<int, string, int>> proposalOptionList = (List<Tuple<int, string, int>>)Session["FormDataObjects"];

                                foreach (var prop in proposalOptionList)
                                {
                                    new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(Session["CustomerId"].ToString()), prop.Item1, originalInvoiceFileName, "Contract", tempInvoiceFileName, prop.Item3, 0);
                                }
                            }

                            GenerateWorkOrder(soldJobID);

                            if (IsEmail)
                            {
                                // SendEmailToCustomer(tempInvoiceFileName);
                            }
                            RefreshData();
                            //GeneratePDF(path, tempWorkOrderFilename , false, createWorkOrder("Work Order-" + Session["CustomerId"].ToString(), int.Parse(ViewState["EstimateId"].ToString())));
                            //new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(Session["CustomerId"].ToString()), 1, originalWorkOrderFilename , "WorkOrder",tempWorkOrderFilename );
                            string email = mailid;
                            string AttachedPdfFile = path + tempInvoiceFileName;
                            //  sendmail(email, AttachedPdfFile);
                            string url = ConfigurationManager.AppSettings["URL"].ToString();
                            string CheckSave = "Checking";
                            if (rdoChecking.Checked)
                            {
                                CheckSave = "Checking";
                            }
                            else if (rdoSaving.Checked)
                            {
                                CheckSave = "Saving";
                            }
                            //ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + tempInvoiceFileName  + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");

                            string completeFileName = "../CustomerDocs/Pdfs/" + tempInvoiceFileName;
                            Session["FilePath"] = completeFileName;
                            //string filename = tempInvoiceFileName.Replace(".pdf", "");
                            HidCV.Value = string.Empty;
                            // Response.Redirect("~/Sr_App/Procurement.aspx?FileToOpen=" + completeFileName,false);
                            //string SynapsePay = System.Configuration.ConfigurationManager.AppSettings["SynapsePay"].ToString();
                            //SynapsePay = SynapsePay + // "?Email=" + txtSynapseEmail.Text.Trim()
                            // + "&Password=" + txtPassword.Text.Trim()
                            // + "&UserId=" + userId
                            //SynapsePay = SynapsePay + "?UserId=" + userId
                            //                           + "&CustomerId=" + Convert.ToString(Session["CustomerId"].ToString())
                            //                         + "&EstId=" + s1
                            //                         + "&paymentMode=" + paymentMode
                            //                         + "&amount=" + amount
                            //                         + "&checkNo=" + checkNo
                            //                         + "&creditCardDetails" + creditCardDetails
                            //                         + "&completeFileName=" + filename
                            //                         + "&loginid=" + Convert.ToString(Session["loginid"])
                            //                         + "&IsEmail=" + IsEmail
                            //                         + "&Bank=" + txtBank.Text
                            //                         + "&RoutingNo=" + txtRoutingNo.Text
                            //                         + "&AccountNo=" + txtMFAATRT.Text
                            //                         + "&SSN=" + txtLASTSSN.Text
                            //                         + "&DOB=" + txtDOB.Text
                            //                         + "&CheckSave=" + CheckSave
                            //                         + "&PerBus=" + ddlperbus.SelectedValue
                            //+ "&en=" + txtPassword.Text
                            ;
                            //Response.Redirect(SynapsePay);


                            completeFileName = url + "/CustomerDocs/Pdfs/" + tempInvoiceFileName;

                            HidCV.Value = string.Empty;
                            string urlll = url + "/Sr_App/Procurement.aspx?FileToOpen=" + completeFileName;
                            // Response.Redirect(url+"/Sr_App/Procurement.aspx?FileToOpen=" + completeFileName);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect", "location.href = '" + urlll + "'", true);
                            //Response.Redirect("~/Sr_App/Procurement.aspx?FileToOpen=" + completeFileName);

                            string script = "window.location='" + urlll + "';";

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);

                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Terms & Conditions');", true);
                        //RequiredAmount.IsValid = false;
                        //CV.IsValid = false;
                        //CV.ErrorMessage = "Invalid Password";
                        //CV.ForeColor = System.Drawing.Color.Red;
                        //mp_sold.Show();


                    }
                }
            }
            catch (Exception ex)
            {
                HidCV.Value = string.Empty;
                logManager.writeToLog(ex, "Custom", Request.ServerVariables["remote_addr"].ToString());
            }
        }

        protected void GenerateWorkOrder(string soldJobID)
        {
            try
            {
                string path = Server.MapPath("~/CustomerDocs/Pdfs/");
                //string path = "C:\\inetpub\\wwwroot\\TestPublishNew\\CustomerDocs\\Pdfs\\wkhtmltopdf.exe";
                string tempWorkOrderFilename = string.Empty;
                string originalWorkOrderFilename = "WorkOrder" + ".pdf";

                if ((Session["FormDataObjects"] != null) || (productId > 0))
                {
                    List<Tuple<int, string, int>> proposalOptionList = (List<Tuple<int, string, int>>)Session["FormDataObjects"];

                    foreach (var prop in proposalOptionList)
                    {
                        tempWorkOrderFilename = "WorkOrder" + DateTime.Now.Ticks + ".pdf";

                        // tempWorkOrderFilename = "WorkOrder123.pdf";
                        // GenerateWorkOrderPDF(path, tempWorkOrderFilename, false, createWorkOrder("Work Order-" + customerId.ToString(), prop.Item1, prop.Item3, soldJobID), false);
                        GeneratePDF(path, tempWorkOrderFilename, false, createWorkOrder("Work Order-" + customerId.ToString(), prop.Item1, prop.Item3, soldJobID), false);
                        new_customerBLL.Instance.AddCustomerDocs(Convert.ToInt32(customerId.ToString()), prop.Item1, originalWorkOrderFilename, "WorkOrder", tempWorkOrderFilename, prop.Item3, 0);

                        string url = ConfigurationManager.AppSettings["URL"].ToString();
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + url + "/CustomerDocs/Pdfs/" + tempWorkOrderFilename + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string createWorkOrder(string InvoiceNo, int estimateId, int productTypeId, string soldJobId)
        {
            return pdf_BLL.Instance.CreateWorkOrder(InvoiceNo, estimateId, productTypeId, customerId, soldJobId, 1);
        }

        //private void GeneratePDF(string path, string fileName, bool download, string text)//download set to false in calling method
        //{
        //    var document = new Document();
        //    FileStream FS = new FileStream(path + fileName, FileMode.Create);
        //    try
        //    {
        //        if (download)
        //        {
        //            Response.Clear();
        //            Response.ContentType = "application/pdf";
        //            PdfWriter.GetInstance(document, Response.OutputStream);
        //        }
        //        else
        //        {
        //            PdfWriter.GetInstance(document, FS);
        //        }
        //        StringBuilder strB = new StringBuilder();
        //        strB.Append(text);
        //        //string filePath = Server.MapPath("/CustomerDocs/Pdfs/wkhtmltopdf.exe");
        //        //byte[] byteData = ConvertHtmlToByte(strB.ToString(), "", "", filePath);
        //        //if (byteData != null)
        //        //{
        //        //    StreamByteToPDF(byteData, Server.MapPath("/CustomerDocs/Pdfs/") + fileName);
        //        //}

        //        using (TextReader sReader = new StringReader(strB.ToString()))
        //        {
        //            document.Open();
        //            List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
        //            foreach (IElement elm in list)
        //            {
        //                document.Add(elm);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.Instance.writeToLog(ex, "Custom", "");
        //        //LogManager.Instance.WriteToFlatFile(ex.Message, "Custom",1);// Request.ServerVariables["remote_addr"].ToString());

        //    }
        //    finally
        //    {
        //        if (document.IsOpen())
        //            document.Close();
        //    }
        //}
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() != "")
            {
                bool result = new_customerBLL.Instance.UpdateEmailOfCustomer(Convert.ToInt32(Session["CustomerId"].ToString()), txtEmail.Text.Trim());
                NotSoldTasks(true);
            }
        }
        protected void btnNotSold_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            try
            {
                // filePath = "C:\\inetpub\\wwwroot\\TestPublishNew\\CustomerDocs\\Pdfs\\wkhtmltopdf.exe";
                filePath = Server.MapPath("~/CustomerDocs/Pdfs/wkhtmltopdf.exe");

                if (ddlstatus.SelectedValue == "est>$1000" || ddlstatus.SelectedValue == "est<$1000" || ddlstatus.SelectedValue == "Follow up")
                {
                    if (txtfollowupdate.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please enter FollowupDate.');", true);
                        mp_notsold.Show();
                    }
                }
                if (chkSendMailNotSold.Checked == true)
                {
                    bool result = CheckCustomerEmail();
                    if (!result)
                    {
                        mpeCustomerEmail.Show();
                        return;
                    }
                    else
                    {
                        NotSoldTasks(true);
                    }
                }
                else
                {
                    NotSoldTasks(false);
                }
            }
            catch (Exception ex)
            {
                lblerrornew.Text = filePath + lblerrornew.Text + ex.Message + ex.StackTrace;
            }
        }

        protected void NotSoldTasks(bool IsEmail)
        {
            DateTime followupdate = (txtfollowupdate.Text != "") ? Convert.ToDateTime(txtfollowupdate.Text, JGConstant.CULTURE) : Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"), JGConstant.CULTURE);
            string tempInvoiceFileName = "Invoice" + DateTime.Now.Ticks + ".pdf";
            string status = ddlstatus.SelectedValue;
            int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);

            //# Changes by Shabbir, to add follow up entries in customer follow up.
            if (productIdList.Count() >= 1)
            {
                for (int i = 0; i < estimateIdList.Length; i++)
                {
                    int outEstID = 0;
                    if (int.TryParse(estimateIdList[i].ToString(), out outEstID))
                    {
                        if (outEstID > 0)
                        {
                            new_customerBLL.Instance.AddCustomerFollowUp(Convert.ToInt32(Session["CustomerId"].ToString()), followupdate, status, userId, false, 0, tempInvoiceFileName, outEstID);
                        }
                    }
                }
            }
            
            
            if (txtfollowupdate.Text.Trim() != "")
            {
                new_customerBLL.Instance.UpdateCustomerFollowUpDate(followupdate, Convert.ToInt32(Session["CustomerId"].ToString()));
            }
            string mailid = ViewState["customeremail"].ToString();
            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string g = Guid.NewGuid().ToString().Substring(0, 5);
            

            GeneratePDF(path, tempInvoiceFileName, false, createEstimate("InvoiceNumber-" + Session["CustomerId"].ToString(), Convert.ToInt32(Session["CustomerId"].ToString())), true);

            string url = ConfigurationManager.AppSettings["URL"].ToString();

            string completeFileName = url + "/CustomerDocs/Pdfs/" + tempInvoiceFileName;

            if (IsEmail)
            {
                SendEmailToCustomer(tempInvoiceFileName);
            }

            RefreshData();
            HidCV.Value = string.Empty;

            Response.Redirect("~/Sr_App/CallSheet.aspx?FileToOpen=" + completeFileName);
        }

        protected void RefreshData()
        {
            proposalOptionList = new List<Tuple<int, string, int>>();
            productIdList = new int[50];
            estimateIdList = new int[50];
            Session["FormDataObjects"] = null;
            QuoteNumber = string.Empty;
        }
        protected void grdproductlines_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //int productType = Convert.ToInt32(Session[SessionKey.Key.ProductType.ToString()]);
            int productType = ProductTypeID;

            //proposalOptionList = new List<Tuple<int, string>>();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButtonList RadioButtonList1 = (RadioButtonList)e.Row.FindControl("RadioButtonList1");

                HiddenField hidstId = (HiddenField)e.Row.FindControl("HiddenFieldEstimate");
                HiddenField HDAmountA = (HiddenField)e.Row.FindControl("HDAmountA");
                HiddenField HDAmountB = (HiddenField)e.Row.FindControl("HDAmountB");
                Literal LiteralBody = (Literal)e.Row.FindControl("LiteralBody");
                DataSet DS1 = new DataSet();
                DS1 = shuttersBLL.Instance.FetchContractdetails(int.Parse(hidstId.Value), productType);

                if (DS1.Tables[0].Rows.Count > 0)
                {
                    HDAmountA.Value = DS1.Tables[0].Rows[0]["AmountA"].ToString();
                    HDAmountB.Value = DS1.Tables[0].Rows[0]["AmountB"].ToString();
                }
                if (Session["Proposal"] != null)
                    LiteralBody.Text = Session["Proposal"].ToString();
                else
                    LiteralBody.Text = createBodyEstimate("", int.Parse(hidstId.Value), productType);
            }
        }

        protected void grdCustom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //int productType = Convert.ToInt32(Session[SessionKey.Key.ProductType.ToString()]);
            //int productType = ProductTypeID;

            int productType = ProductTypeID;
            if (ProductTypeID != 1 && ProductTypeID != 0)
            {
                ProductTypeID = 7;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hidstId = (HiddenField)e.Row.FindControl("HiddenFieldEstimateCustom");
                HiddenField HDAmountA = (HiddenField)e.Row.FindControl("HDAmountACustom");

                Literal LiteralBody = (Literal)e.Row.FindControl("LiteralBodyCustom");
                DataSet DS1 = new DataSet();
                DS1 = shuttersBLL.Instance.FetchContractdetails(int.Parse(hidstId.Value), productType);

                if (DS1.Tables[0].Rows.Count > 0)
                {
                    HDAmountA.Value = DS1.Tables[0].Rows[0]["ProposalCost"].ToString();
                }
                //if (Session["Proposal"] != null)
                //{
                //    LiteralBody.Text = Session["Proposal"].ToString();
                //}
                //else
                LiteralBody.Text = createBodyEstimateCustom("", int.Parse(hidstId.Value), productType);
            }
        }

        protected void CalculateAmount()
        {
            if (proposalOptionList == null)
                proposalOptionList = new List<Tuple<int, string, int>>();

            //if (Session["FormDataObjects"] != null)
            //{
            //    proposalOptionList = (List<Tuple<int, string, int>>)Session["FormDataObjects"];
            //}

            int productType = ProductTypeID;
            //int productType = Convert.ToInt32(Session[SessionKey.Key.ProductType.ToString()]);
            decimal total = 0; decimal totalAmount = 0;

            foreach (GridViewRow grow in grdproductlines.Rows)
            {
                HiddenField HiddenFieldEstimate = (HiddenField)grow.FindControl("HiddenFieldEstimate");
                HiddenField HiddenFieldProduct = (HiddenField)grow.FindControl("HiddenFieldProduct");
                ViewState["EstimateId"] = HiddenFieldEstimate.Value;
                RadioButtonList RadioButtonList1 = (RadioButtonList)grow.FindControl("RadioButtonList1");
                HiddenField HDAmountA = (HiddenField)grow.FindControl("HDAmountA");
                HiddenField HDAmountB = (HiddenField)grow.FindControl("HDAmountB");
                if (RadioButtonList1.SelectedValue != null)
                {
                    if (RadioButtonList1.SelectedValue == "A")
                    {
                        total += Math.Round(Convert.ToDecimal(HDAmountA.Value), 2);
                        //HidShutterProposal.Value = RadioButtonList1.SelectedValue;
                        // proposalOptionList.Add(Tuple.Create(Convert.ToInt32(HiddenFieldEstimate.Value), RadioButtonList1.SelectedValue, ProductTypeID));
                        proposalOptionList.Add(Tuple.Create(Convert.ToInt32(HiddenFieldEstimate.Value), RadioButtonList1.SelectedValue, Convert.ToInt32(HiddenFieldProduct.Value)));
                    }
                    else if (RadioButtonList1.SelectedValue == "B")
                    {
                        total += Math.Round(Convert.ToDecimal(HDAmountB.Value), 2);
                        //HidShutterProposal.Value = RadioButtonList1.SelectedValue;
                        proposalOptionList.Add(Tuple.Create(Convert.ToInt32(HiddenFieldEstimate.Value), RadioButtonList1.SelectedValue, Convert.ToInt32(HiddenFieldProduct.Value)));
                    }
                    else
                    {
                        total = 0;
                    }
                }
            }

            //HidShutterProposal.Value = proposalOptionList.ToString();
            Session["FormDataObjects"] = proposalOptionList;

            totalAmount = total;
            HiddenFieldtotalAmount.Value = totalAmount.ToString();
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            if (ds != null)
            {
                //LiteralFooter.Text = createFooterEstimate("", int.Parse(arr[0].ToString()), productType);
                LiteralFooter.Text = createFooterEstimate("", estimateIdList[0], productIdList[0]);
            }
        }

        protected void CalculateAmountForCustom()
        {
            if (proposalOptionList == null)
                proposalOptionList = new List<Tuple<int, string, int>>();

            //if (Session["FormDataObjects"] != null)
            //{
            //    proposalOptionList = (List<Tuple<int, string, int>>)Session["FormDataObjects"];
            //}

            int productType = ProductTypeID;
            //int productType = Convert.ToInt32(Session[SessionKey.Key.ProductType.ToString()]);
            decimal total = 0; decimal totalAmount = 0;
            foreach (GridViewRow grow in grdCustom.Rows)
            {
                HiddenField HiddenFieldEstimate = (HiddenField)grow.FindControl("HiddenFieldEstimateCustom");
                HiddenField HiddenFieldProduct = (HiddenField)grow.FindControl("HiddenFieldProduct");
                ViewState["EstimateId"] = HiddenFieldEstimate.Value;

                HiddenField HDAmount = (HiddenField)grow.FindControl("HDAmountACustom");

                total += Math.Round(Convert.ToDecimal(HDAmount.Value), 2);

                //proposalOptionList.Add(Tuple.Create(Convert.ToInt32(HiddenFieldEstimate.Value), "A", ProductTypeID));
                proposalOptionList.Add(Tuple.Create(Convert.ToInt32(HiddenFieldEstimate.Value), "A", Convert.ToInt32(HiddenFieldProduct.Value)));
                proposalOptionList.Select(a => a.Item1 == Convert.ToInt32(HiddenFieldEstimate.Value) && a.Item3 == Convert.ToInt32(HiddenFieldProduct.Value)).FirstOrDefault();//.Item2 = "A";
            }
            Session["FormDataObjects"] = proposalOptionList;

            totalAmount = total;
            HiddenFieldtotalAmount.Value = totalAmount.ToString();
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            if (ds != null)
            {
                LiteralFooter.Text = createFooterEstimate("", estimateIdList[0], productIdList[0]);
            }
        }

        
        //protected void createcontract()
        //{
        //    string fileName = string.Empty;
        //    string path = Server.MapPath("/CustomerDocs/Pdfs/");
        //    string workorder = string.Empty;
        //    // Create and display the value of two GUIDs.
        //    string g = Guid.NewGuid().ToString().Substring(0, 5);
        //    fileName = "Contract" + g.ToString() + ".pdf";
        //    workorder = "WorkOrder" + g.ToString() + ".pdf";
        //    // Create Invoice with Pdf for Transaction.....
        //    StringBuilder sbWork = new StringBuilder();
        //    sbWork.Append(createWorkOrder("InvoiceNumber", int.Parse(ViewState["EstimateId"].ToString())));
        //    string FileWorkText = sbWork.ToString();
        //    GeneratePDF(path, workorder, false, FileWorkText);

        //    StringBuilder sbEstimate = new StringBuilder();
        //    sbEstimate.Append(createHeaderEstimate("InvoiceNumber", int.Parse(arr[0].ToString())));
        //    sbEstimate.Append(createBodyEstimate("InvoiceNumber", int.Parse(arr[0].ToString())));
        //    sbEstimate.Append(createFooterEstimate("InvoiceNumber", int.Parse(arr[0].ToString())));
        //    string FileText = sbEstimate.ToString();
        //    GeneratePDF(path, fileName, false, FileText);
        //}
        

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateAmount();

            decimal totalAmount = Convert.ToDecimal(HiddenFieldtotalAmount.Value);

            CalculateAmountForCustom();

            HiddenFieldtotalAmount.Value = (totalAmount + Convert.ToDecimal(HiddenFieldtotalAmount.Value)).ToString();
            LiteralFooter.Text = createFooterEstimate("", estimateIdList[0], productIdList[0]);
        }

        //[WebMethod]
        //public static string Exists(string value)
        //{
        //    if (value == AdminBLL.Instance.GetAdminCode())
        //    {
        //       return "true";
        //    }
        //    else
        //    {
        //        return "false";
        //    }
        //}

        protected void btnSold_Click1(object sender, EventArgs e)
        {

        }


        protected void btnNotSold_Click1(object sender, EventArgs e)
        {
            decimal totalAmount = Convert.ToDecimal(HiddenFieldtotalAmount.Value.ToString());
            if (totalAmount > 1000)
            {
                ddlstatus.SelectedIndex = 0;
                txtfollowupdate.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
            }
            else
            {
                ddlstatus.SelectedIndex = 1;
                txtfollowupdate.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
            }
            string[] Emails;
            int count = 0;
            DataSet ds = shuttersBLL.Instance.GetEmails(Convert.ToInt32(Session["CustomerId"].ToString()));
            //if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
            //{
            //    txtNotSoldEmail.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
            //}
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                {
                    Emails = Convert.ToString(ds.Tables[0].Rows[0][0]).Split(',');
                    count = Emails.Count();
                    for (int i = 0; i < count; i++)
                    {
                        TextBox NewTextBox = new TextBox();
                        NewTextBox.ID = "TextBoxE" + i.ToString();
                        NewTextBox.Text = Emails[i];
                        //form1 is a form in my .aspx file with runat=server attribute
                        NotSoldEmails.Controls.Add(NewTextBox);
                    }
                }
            }



            mp_notsold.Show();
        }
        protected void chkedit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkedit.Checked == true)
            {
                txtAmount.ReadOnly = false;
                trauthpass.Visible = true;
            }
            else
            {
                txtAmount.ReadOnly = true;
                trauthpass.Visible = false;

            }
            if (txtAmount.Text == "")
            {
                txtAmount.Text = hidDownPayment.Value;
            }
            if (ddlpaymode.SelectedIndex==1)
            {
                txtPromotionalcode.Visible = true;
                txtPwd.Visible = false;
            }
            else
            {
                txtPromotionalcode.Visible = false;
                txtPwd.Visible = true;
            }

            string[] Emails;
            int count = 0;
            DataSet ds = shuttersBLL.Instance.GetEmails(Convert.ToInt32(Session["CustomerId"].ToString()));
            //if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
            //{
            //    txtEmailId.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
            //}
            if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
            {
                txtDOB.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                {
                    Emails = Convert.ToString(ds.Tables[0].Rows[0][0]).Split(',');
                    count = Emails.Count();
                    for (int i = 0; i < count; i++)
                    {
                        TextBox NewTextBox = new TextBox();
                        NewTextBox.ID = "TextBoxE" + i.ToString();
                        NewTextBox.Text = Emails[i];
                        //form1 is a form in my .aspx file with runat=server attribute
                        pnlControls.Controls.Add(NewTextBox);
                    }
                }
            }
            //txtPromotionalcode.Visible = false;
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlstatus = sender as DropDownList;
            if (ddlstatus.SelectedValue == "est>$1000" || ddlstatus.SelectedValue == "est<$1000" || ddlstatus.SelectedValue == "Follow up")
            {
                txtfollowupdate.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
                mp_notsold.Show();
            }
            else
            {
                txtfollowupdate.Text = "";
                mp_notsold.Show();
            }
            string[] Emails;
            int count = 0;
            DataSet ds = shuttersBLL.Instance.GetEmails(Convert.ToInt32(Session["CustomerId"].ToString()));
            //if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
            //{
            //    txtNotSoldEmail.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
            //}
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                {
                    Emails = Convert.ToString(ds.Tables[0].Rows[0][0]).Split(',');
                    count = Emails.Count();
                    for (int i = 0; i < count; i++)
                    {
                        TextBox NewTextBox = new TextBox();
                        NewTextBox.ID = "TextBoxE" + i.ToString();
                        NewTextBox.Text = Emails[i];
                        //form1 is a form in my .aspx file with runat=server attribute
                        NotSoldEmails.Controls.Add(NewTextBox);
                    }
                }
            }
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            //initialize a session.
            rowCount = Convert.ToInt32(Session["clicks"]);
            rowCount++;
            //In each button clic save the numbers into the session.
            Session["clicks"] = rowCount;
            //Create the textboxes and labels each time the button is clicked.
            for (int i = 0; i < rowCount; i++)
            {
                TextBox TxtBoxE = new TextBox();
                TxtBoxE.ID = "TextBoxE" + i.ToString();
                //Add the labels and textboxes to the Panel.
                pnlControls.Controls.Add(TxtBoxE);
            }
            mp_sold.Show();
        }

        [System.Web.Services.WebMethod]
        public static List<string> GetCompletionList(string prefixText)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.AutoFill(prefixText);
            List<string> CountryNames = new List<string>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        CountryNames.Add(Convert.ToString(ds.Tables[0].Rows[k][0]));
                    }
                }
            }
            return CountryNames;
        }

        protected void btnSold_Click2(object sender, EventArgs e)
        {
            string[] Emails;
            int count = 0;
            DataSet ds = shuttersBLL.Instance.GetEmails(Convert.ToInt32(Session["CustomerId"].ToString()));
            //if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
            //{
            //    txtEmailId.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
            //}
            if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
            {
                txtDOB.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                {
                    Emails = Convert.ToString(ds.Tables[0].Rows[0][0]).Split(',');
                    count = Emails.Count();
                    for (int i = 0; i < count; i++)
                    {
                        TextBox NewTextBox = new TextBox();
                        NewTextBox.ID = "TextBoxE" + i.ToString();
                        NewTextBox.Text = Emails[i];
                        //form1 is a form in my .aspx file with runat=server attribute
                        pnlControls.Controls.Add(NewTextBox);
                    }
                }
            }
            mp_sold.Show();
        }

        protected void lnkAddNotSoldEmail_Click(object sender, EventArgs e)
        {
            int rowCount = 0;
            //initialize a session.
            rowCount = Convert.ToInt32(Session["NotSoldclicks"]);
            rowCount++;
            //In each button clic save the numbers into the session.
            Session["NotSoldclicks"] = rowCount;
            //Create the textboxes and labels each time the button is clicked.
            for (int i = 0; i < rowCount; i++)
            {
                TextBox TxtBoxE = new TextBox();
                TxtBoxE.ID = "TextBoxE" + i.ToString();
                //Add the labels and textboxes to the Panel.
                NotSoldEmails.Controls.Add(TxtBoxE);
                // txtNotSoldEmail.Visible = false;
            }
            mp_notsold.Show();
        }

        protected void ddlpaymode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymode.SelectedIndex == 0)
            {
                lblPwd.Visible = false;
                txtPwd.Visible = false;
                btnsavesold.Visible = true;
                btnSaveSold2.Visible = false;
                btnSaveSold2.Style.Add("display", "none");
                txtPwd.Style.Add("display", "none");
                btnsavesold.Style.Add("display", "block");
                PanelHide.Visible = true;
                lblPro.Visible = true;
                txtPromotionalcode.Visible = true;
            }
            else if (ddlpaymode.SelectedIndex == 1)
            {
                lblPwd.Visible = false;
                txtPwd.Visible = false;
                btnsavesold.Visible = true;
                btnSaveSold2.Visible = false;
                btnSaveSold2.Style.Add("display", "none");
                txtPwd.Style.Add("display", "none");
                btnsavesold.Style.Add("display", "block");
                PanelHide.Visible = true;
                lblPro.Visible = true;
                txtPromotionalcode.Visible = true;
                rdoChecking.Visible = true;
                rdoSaving.Visible = true;

            }
            else
            {
                lblPwd.Visible = true;
                txtPwd.Visible = true;
                btnsavesold.Visible = false;
                btnSaveSold2.Visible = true;
                btnSaveSold2.Style.Add("display", "block");
                btnsavesold.Style.Add("display", "none");
                txtPwd.Style.Add("display", "block");
                PanelHide.Visible = false;
                lblPro.Visible = false;
                txtPromotionalcode.Visible = false;
                rdoChecking.Visible = false;
                rdoSaving.Visible = false;

                /*
                lblPerBus.Visible = false;
                Label5.Visible = false;
                lblDOB.Visible = false;
                Label4.Visible = false;
                lblSSN4.Visible = false;
                Label6.Visible = false;
                lblRouting.Visible = false;
                Label3.Visible = false;
                lblAccNo.Visible = false;
                Label2.Visible = false;
                lblBank.Visible = false;
                Label1.Visible = false;
                rdoChecking.Visible = false;
                rdoSaving.Visible = false;
                lblAmount.Visible = false;
                chkedit.Visible = false;
                lblA.Visible = false;
                lblReqAmt.Visible = false;

                txtAmount.Visible = false;
                txtBank.Visible = false;
                txtMFAATRT.Visible = false;
                txtRoutingNo.Visible = false;
                txtLASTSSN.Visible = false;
                txtDOB.Visible = false;
                ddlperbus.Visible = false;
                txtAmount.Visible = false;
                txtAmount.Visible = false;
                 * */
            }

           
            trauthpass.Visible = false;
            //if (!string.IsNullOrEmpty(Session["PaymentAmount"] as string))
            //{
            // Session["PaymentAmount"]= hidDownPayment.Value;
            if (txtAmount.Text == "")
            {
                txtAmount.Text = hidDownPayment.Value;
            }

            //txtAmount.Text = Session["PaymentAmount"].ToString();

            //}

            string[] Emails;
            int count = 0;
            DataSet ds = shuttersBLL.Instance.GetEmails(Convert.ToInt32(Session["CustomerId"].ToString()));
            //if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
            //{
            //    txtEmailId.Text = Convert.ToString(ds.Tables[0].Rows[0][1]);
            //}
            if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
            {
                txtDOB.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                {
                    Emails = Convert.ToString(ds.Tables[0].Rows[0][0]).Split(',');
                    count = Emails.Count();
                    for (int i = 0; i < count; i++)
                    {
                        TextBox NewTextBox = new TextBox();
                        NewTextBox.ID = "TextBoxE" + i.ToString();
                        NewTextBox.Text = Emails[i];
                        //form1 is a form in my .aspx file with runat=server attribute
                        pnlControls.Controls.Add(NewTextBox);
                    }
                }
            }

        }

        protected void btnCancelAdminDetails_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelsold_Click(object sender, EventArgs e)
        {
            this.mp_sold.Hide();
            //ddlpaymode.SelectedIndex = 0;
            Response.Redirect("shutterproposal.aspx");
            //txtPromotionalcode.Visible = false;
            //txtPwd.Visible = false;
        }
        protected void btnSaveAdminDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sr_App/CallSheet.aspx");
        }

        protected void btnSaveSold2_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text != "")
            {
                //Verify Password...
                int isvaliduser = 0;
                isvaliduser = UserBLL.Instance.chklogin("jgrove@jmgroveconstruction.com", txtPwd.Text);
                //isvaliduser = UserBLL.Instance.chklogin("nitintold@custom-soft.com", txtPwd.Text);
                if (isvaliduser == 1)
                {
                    Session["Sols"] = "Sold";
                    Session["SaveEID"] = "SaveEmail";
                    //NotSoldTasks(true);
                    SoldTasks(true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Enter correct password .');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter password .');", true);
            }

        }

        private void EntryOnProcurement()
        {

        }

        //protected void txtauthpass_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtauthpass.Text == AdminBLL.Instance.GetAdminCode())
        //    {
        //        txtAmount.Enabled = true;
        //    }
        //    else
        //    {
        //        txtAmount.Enabled = false;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Invalid code!');", true);
        //    }
        //}
    }
}