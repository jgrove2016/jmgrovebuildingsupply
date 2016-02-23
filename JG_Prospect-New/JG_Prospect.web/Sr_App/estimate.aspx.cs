using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using JG_Prospect.BLL;
using System.IO;
using System.Net.Mail;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace JG_Prospect.Sr_App
{
    public partial class estimate : System.Web.UI.Page
    {
        DataSet DS;

        StringBuilder sb = new StringBuilder();

        GoogleDetail googleDetail = new GoogleDetail();
        private string _GoogleInputID = "";
        public string GoogleInputID
        {
            get { return _GoogleInputID; }
            set { _GoogleInputID = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["loginid"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('firstly you have to login');", true);
                Response.Redirect("~/login.aspx");
            }
            else
            {

                //Repeater rpt = leftpanel5.FindControl("rptcutomerlist") as Repeater;

                //if (Session["LeftUser"] != null)
                //{
                //    DataView DvA = (DataView)Session["LeftUser"];
                //    rpt.DataSource = DvA;
                //    rpt.DataBind();

                //    rpt.Visible = true;

                //}
            }


            if (!Page.IsPostBack)
            {

                //////***** code to show data for editing to admin ******////             
                try
                {
                    string addby = Session["loginid"].ToString();// Request.QueryString["addedby"].ToString();

                    //DS = new DataSet();
                    //DS = userBLL.Instance.GetUserType(addby);
                    string usrtype = Session["usertype"].ToString();

                    if (usrtype == "Admin" || usrtype == "SSE")
                    {
                        btncreateEsti.Visible = false;
                        btnAdd.Visible = false;
                        btnedit.Visible = true;


                        string mailid = Request.QueryString["custmail"].ToString();
                        string ordernum = Request.QueryString["orderno"].ToString();


                        DataSet dds = new DataSet();
                        dds = shuttersBLL.Instance.GetShutterDetail(ordernum, mailid, addby);


                        if (dds.Tables.Count > 0)
                        {
                            txtshuttertop.Text = dds.Tables[0].Rows[0][0].ToString();
                            txtStyle.Text = dds.Tables[0].Rows[0][1].ToString();
                            //ddlColor.SelectedItem.Text = dds.Tables[0].Rows[0][2].ToString();
                            //ddlSurface_mount.SelectedItem.Text = dds.Tables[0].Rows[0][3].ToString();
                            //ddlWidth.SelectedItem.Text = dds.Tables[0].Rows[0][4].ToString();
                            //ddlHeight.SelectedItem.Text = dds.Tables[0].Rows[0][5].ToString();
                            //ddlQnty.SelectedItem.Text = dds.Tables[0].Rows[0][6].ToString();
                            txtWork_area.Text = dds.Tables[0].Rows[0][7].ToString();
                            txtSpcl_inst.Text = dds.Tables[0].Rows[0][8].ToString();

                            imgplc_Loc.ImageUrl = Server.MapPath(dds.Tables[0].Rows[0][9].ToString());
                            txtDate_frstcontc.Text = dds.Tables[0].Rows[0][10].ToString();
                            txtEmail.Text = dds.Tables[0].Rows[0][11].ToString();
                            txtFolowup_1.Text = dds.Tables[0].Rows[0][12].ToString();
                            txtFolowup_2.Text = dds.Tables[0].Rows[0][13].ToString();
                            txtFolowup_3.Text = dds.Tables[0].Rows[0][14].ToString();
                            txtCust_name.Text = dds.Tables[0].Rows[0][15].ToString();
                            txtCust_strt.Text = dds.Tables[0].Rows[0][16].ToString();

                            txtJob_loc.Text = dds.Tables[0].Rows[0][20].ToString();
                            txtEsti_date.Text = dds.Tables[0].Rows[0][21].ToString();
                            txtPri_ph.Text = dds.Tables[0].Rows[0][22].ToString();
                            txtSec_ph.Text = dds.Tables[0].Rows[0][23].ToString();
                            txtCall_takenby.Text = dds.Tables[0].Rows[0][24].ToString();

                        }

                    }
                    else
                    {
                        btncreateEsti.Visible = true;
                        btnAdd.Visible = true;
                        btnedit.Visible = false;
                    }
                }
                catch
                {
                    btncreateEsti.Visible = true;
                    btnAdd.Visible = true;
                    btnedit.Visible = false;
                }
            }

            //////***** end code to show data for editing to admin ******////

           // leftpanel5.lnkclick += new EventHandler(LeftPanelViewCustomer1_lnkclick);
        }


        protected void LeftPanelViewCustomer1_lnkclick(object sender, EventArgs e)
        {

            int customerid = Convert.ToInt32(Session["CustomerId"].ToString());
            DS = new DataSet();
            DS = existing_customerBLL.Instance.GetExistingCustomerDetail(customerid, "", Session["loginid"].ToString());

            txtCust_name.Text = DS.Tables[0].Rows[0][1].ToString();
            txtCust_strt.Text = DS.Tables[0].Rows[0][2].ToString();
            txtJob_loc.Text = DS.Tables[0].Rows[0][3].ToString();
            txtEsti_date.Text = DS.Tables[0].Rows[0][4].ToString();
            //txtToday_date.Text = DS.Tables[0].Rows[0][4].ToString();

            txtPri_ph.Text = DS.Tables[0].Rows[0][6].ToString();
            txtSec_ph.Text = DS.Tables[0].Rows[0][7].ToString();
            txtEmail.Text = DS.Tables[0].Rows[0][8].ToString();
            txtCall_takenby.Text = DS.Tables[0].Rows[0][9].ToString();
            //txtService.Text = DS.Tables[0].Rows[0][9].ToString();
            // txtDate_frstcontc.Text = DS.Tables[0].Rows[0][11].ToString();

        }

        protected void btncreateEsti_Click(object sender, EventArgs e)
        {
            string utype = Session["usertype"].ToString();

            if ((utype == "Admin") || (utype == "SSE"))
            {
                DateTime dt = new DateTime();
                if (txtDate_frstcontc.Text != string.Empty)
                {
                    dt = Convert.ToDateTime(txtDate_frstcontc.Text);
                }

                string Email = txtEmail.Text;
                string Followup1 = txtFolowup_1.Text;
                string Followup2 = txtFolowup_2.Text;
                string Followup3 = txtFolowup_3.Text;
                string shuttertop = txtshuttertop.Text;
                string Style = txtStyle.Text;
                string Color = ddlColor.SelectedItem.Text;
                string SurfaceMount = ddlSurface_mount.SelectedItem.Text;
                string Workarea = txtWork_area.Text;
                Double width = Convert.ToDouble(ddlWidth.SelectedItem.Text);
                Double height = Convert.ToDouble(ddlHeight.SelectedItem.Text);
                int qnty = Convert.ToInt32(ddlQnty.SelectedItem.Text);

                //Double Workarea = 0.00;
                //if (txtWork_area.Text != string.Empty)
                //{
                //    Workarea = Convert.ToDouble(txtWork_area.Text);
                //}
                string spcl_instr = txtSpcl_inst.Text;

                string Loc_Img = "";
                if (flpUploadLoc_img.HasFile)
                {

                    ///*********** code to show location image in imageplaceholder
                    //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("tempImg"));
                    //int count = dir.GetFiles().Length;

                    //if (count > 0)
                    //{
                    //    foreach (System.IO.FileInfo file in dir.GetFiles())
                    //        file.Delete();
                    //}

                    string filename = Path.GetFileName(flpUploadLoc_img.FileName);
                    //flpUploadLoc_img.SaveAs(Server.MapPath("~/tempImg/") + filename);
                    //imgplc_Loc.ImageUrl = Server.MapPath(@"~/tempImg/Tulips.jpg");// +Tulips.jpg;
                    ///********** end code to show location image in imageplaceholder

                    Loc_Img = "~/Location_img/" + filename;


                }
                int res = 0;
                if (Session["loginid"] != null)
                {
                    string AddedBy = Session["loginid"].ToString();

                    res = existing_customerBLL.Instance.AddNewEstimate(dt, Email, Followup1, Followup2, Followup3, shuttertop, Style, Color, SurfaceMount, width, height, qnty, Workarea, spcl_instr, Loc_Img, AddedBy);

                }
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('New estimate has been added successfully  and estimate report for your order has been send on your mailid');", true);

                    flpUploadLoc_img.SaveAs(Server.MapPath("~/Location_img/") + Path.GetFileName(flpUploadLoc_img.FileName));



                    //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("InvoicePdf"));
                    //int count = dir.GetFiles().Length;

                    //if (count > 0)
                    //{
                    //    foreach (System.IO.FileInfo file in dir.GetFiles())
                    //    {
                    //        if (File.Exists(Server.MapPath("/InvoicePdf/"+file)))
                    //        {
                    //            file.Delete();
                    //        }
                    //    }
                    //}



                    string fileName = string.Empty;
                    string path = Server.MapPath("/InvoicePdf/");

                    // Create and display the value of two GUIDs.
                    string g = Guid.NewGuid().ToString().Substring(0, 5);
                    fileName = "Invoice" + g.ToString() + ".pdf";
                    // Create Invoice with Pdf for Transaction.....
                    GeneratePDF(path, fileName, false, createEstimate("InvoiceNumber-1"));
                    string email = txtEmail.Text;
                    string AttachedPdfFile = path + fileName;
                    sendmail(email, AttachedPdfFile);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Sorry! please try again');", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Sorry! You are not authorised to create estimate');", true);
            }
        }





        private string createEstimate(string InvoiceNo)
        {

            DataSet ds = new DataSet();
            //sb.Append(@"<table><tr><td>YourInvoice</td></tr></table>");
            sb.Append(@"<br/> <br/>
  <table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Arial, Helvetica, sans-serif; font-size:9pt; '>  
  <tr>
    <td><u><strong>Proposal A:</strong></u> To supply and install (" + ddlQnty.SelectedItem.Text.ToString() + @")pair(s) of custom made Mid America ( " + txtStyle.Text.ToString() + ") (" + ddlColor.Text.ToString() + ")shutters. The shutters are to consist of a heavy duty vinyl.  Remove and haul away old shutters and debris. Job location:( " + txtJob_loc.Text.ToString() + ")</td></tr>");
            sb.Append(@"<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Arial, Helvetica, sans-serif; font-size:9pt'><tr><td>&nbsp;</td>        <td>&nbsp;</td>
        <td scope='col'>&nbsp;</td>
      </tr>
      <tr>
        <td><img src=" + Server.MapPath("~/image/ma.png") + @" width='150'></td>
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
        
        Total price :   $ " + (Convert.ToInt32(ddlQnty.SelectedItem.Text) * 100).ToString() + @"      </strong></td></tr>
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
    <td><u><strong>Proposal B::</strong></u> To supply and install (" + ddlQnty.SelectedItem.Text.ToString() + @") 

pair(s) of generic plastic (" + txtStyle.Text.ToString() + @") (" + ddlColor.Text.ToString() + @")shutters.  Remove and haul away old 

shutters and debris. Job location:( " + txtJob_loc.Text.ToString() + @")</td>
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
            
            Total price : $ " + (Convert.ToInt32

(ddlQnty.SelectedItem.Text) * 300).ToString() + @"  </strong> </td>   
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br /><br/><br/><br/><br />");

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
                    //PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));
                    PdfWriter.GetInstance(document, FS);
                }

                // generates the grid first
                StringBuilder strB = new StringBuilder();

                // now read the Grid html one by one and add into the document object

                using (TextReader sReader = new StringReader(text))
                {
                    List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                    document.Open();

                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/Header1.jpg"));
                    logo.SetAbsolutePosition(0f, 650f);
                    PdfPTable table = new PdfPTable(1);
                    table.AddCell(logo);

                    table.WidthPercentage = 110;
                    document.Add(table);



                    foreach (IElement elm in list)
                    {
                        document.Add(elm);
                    }



                    iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/footer.jpg"));//footer
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
                if(document.IsOpen())
                document.Close();
                //int i= FS.WriteTimeout;
                // //FS.Flush();
                // //FS.Dispose();
                // FS.Close();   

            }
        }

        private void sendmail(string email, string invoice)
        {
            try
            {
                // SmtpClient smtpClient = new SmtpClient();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("biswaranjan.chevron@gmail.com", "Admin");
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
                            " <tr> <td>&nbsp;</td></tr><tr> <td>Estimate Amount is attached with this Email. <br></td></tr><tr><td><p>Please feel free to contact at: 'poonam@ennomail.com' for any queries. </p></td>" +
                            "</tr><tr><td>&nbsp;</td></tr> <tr><td><b>Thanks</b>,</td> </tr> <tr><td style='height: 16px'>Admin</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";



                message.Body = strBody;
                smtpClient.Credentials = new System.Net.NetworkCredential("biswaranjan.chevron@gmail.com", "9238913579");
                try
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);


                }


                catch (Exception exm)
                {
                    Response.Write(exm.Message);
                }

                //smtpClient.Host = "stmp.gmail.com";               
                //smtpClient.Port = 587;


            }
            catch { }
        }


        protected void getLangLat(string address)
        {
            string google_address = HttpUtility.UrlEncode(address);
            string geocode1 = "http://maps.google.com/maps/geo?q=" + google_address + "&output=csv&key=ABQIAAAAVcJQrx7VsumiP2heFwp6URQLaiSrhXTkLq3mA9rOmHpVsHwBjxTg7C5-XXHl634dCROpHwKMO9BzmQ";

            WebRequest request = HttpWebRequest.Create(geocode1);

            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string urlText = reader.ReadToEnd();

            string[] LangLat = urlText.Split(',');

            string lat = LangLat[2].ToString();
            string lang = LangLat[3].ToString();

            GetGoogleMap(lang, lat);

        }

        private void GetGoogleMap(string lat, string lang)
        {
            string js_load, js;
            googleDetail.Put_Markers(_GoogleInputID, out js_load, out js, 5, lat, lang);
            if (js == "")
            {
                ImgNoGps.Visible = true;
                ImgNoGps.Src = "images/gpsnotfound.gif";
            }
            else
            {
                try
                {
                    ImgNoGps.Visible = false;

                    if (!this.Page.ClientScript.IsStartupScriptRegistered("js_Load"))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js_Load", js_load, false);
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "js", js, true);
                }
                catch (Exception ee)
                {
                    //
                }
            }
        }


        protected void txtEsti_date_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            string utype = Session["usertype"].ToString();

            if ((utype == "Admin") || (utype == "SSE"))
            {
                string addby = Request.QueryString["addedby"].ToString();
                string mailid = Request.QueryString["custmail"].ToString();
                string ordernum = Request.QueryString["orderno"].ToString();

                string Followup1 = txtFolowup_1.Text;
                string Followup2 = txtFolowup_2.Text;
                string Followup3 = txtFolowup_3.Text;
                string shuttertop = txtshuttertop.Text;
                string Style = txtStyle.Text;
                string Color = "";
                string SurfaceMount = "";

                Double width = Convert.ToDouble(ddlWidth.SelectedItem.Text);
                Double height = Convert.ToDouble(ddlHeight.SelectedItem.Text);
                int qnty = Convert.ToInt32(ddlQnty.SelectedItem.Text);
                string Workarea = txtWork_area.Text;
                string spcl_instr = "";// txtStatus.Text;

                string Loc_Img = "";
                if (flpUploadLoc_img.HasFile)
                {

                    ///*********** code to show location image in imageplaceholder
                    //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("tempImg"));
                    //int count = dir.GetFiles().Length;

                    //if (count > 0)
                    //{
                    //    foreach (System.IO.FileInfo file in dir.GetFiles())
                    //       file.Delete();
                    //}
                    string filename = Path.GetFileName(flpUploadLoc_img.FileName);
                    //flpUploadLoc_img.SaveAs(Server.MapPath("~/tempImg/") + filename);
                    //imgplc_Loc.ImageUrl = Server.MapPath(@"~/tempImg/Tulips.jpg");// +Tulips.jpg;
                    ///********** end code to show location image in imageplaceholder

                    Loc_Img = "~/Location_img/" + filename;

                }

                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(txtDate_frstcontc.Text);

                int res = shuttersBLL.Instance.UpdateShutterOrder(ordernum, mailid, addby, shuttertop, Style, Color, SurfaceMount, width, height, qnty, Workarea, spcl_instr, Loc_Img, dt, Followup1, Followup2, Followup3);

                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Estimate has been updated successfully and estimate report for your order has been send on your mailid');", true);

                    flpUploadLoc_img.SaveAs(Server.MapPath("~/Location_img/") + Path.GetFileName(flpUploadLoc_img.FileName));

                    //System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(Server.MapPath("InvoicePdf"));
                    //int count = dir1.GetFiles().Length;

                    //if (count > 0)
                    //{
                    //    foreach (System.IO.FileInfo file in dir1.GetFiles())
                    //    {
                    //        if (File.Exists(Server.MapPath("/InvoicePdf/" + file)))
                    //        {
                    //            file.Delete();

                    //        }
                    //    }
                    //}


                    string fileName = string.Empty;
                    string path = Server.MapPath("/InvoicePdf/");
                    Guid g;
                    // Create and display the value of two GUIDs.
                    g = Guid.NewGuid();
                    fileName = "Estimate" + g.ToString() + ".pdf";
                    GeneratePDF(path, fileName, false, createEstimate("Invoice"));
                    string email = txtEmail.Text;
                    string AttachedPdfFile = path + fileName;
                    sendmail(email, AttachedPdfFile);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Sorry! please try again');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Sorry! You are not authorised to edit estimate');", true);
            }
        }

        protected void ddlZipcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string address = ddlState.SelectedItem.Text.ToString() + ", " + ddlCity.SelectedItem.Text.ToString() + ", " + ddlZipcode.SelectedItem.Text.ToString();
            getLangLat(address.ToString());
        }
    }
}