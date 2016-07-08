using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using JG_Prospect.Common;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using Google.GData.Calendar;
using Google.GData.AccessControl;
using System.Configuration;
using System.Data;
using System.Net;
using JG_Prospect.App_Code;
using JG_Prospect.Common.Logger;

namespace JG_Prospect
{
    public partial class CreateUser : System.Web.UI.Page
    {
        List<string> newAttachments = new List<string>();
        string fn;
        user objuser = new user();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDay.Text = System.DateTime.Now.ToShortDateString();
            if (!IsPostBack)
            {
                 btnUpdate.Visible = false;
                btn_UploadFiles.Visible = false;
                if (Request.QueryString["ID"] != null)
                {
                    btnUpdate.Visible = true;
                    btnreset.Visible = false;
                    btncreate.Visible = false;
                    btn_UploadFiles.Visible = true;
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    Session["ID"] = id;
                    DataSet ds;
                    ds = UserBLL.Instance.GetuserData(id);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        txtusername.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtloginid.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtemail.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtpassword.Text = ds.Tables[0].Rows[0][4].ToString();
                        txtpassword1.Text = ds.Tables[0].Rows[0][4].ToString();
                        ddlusertype.SelectedValue = ds.Tables[0].Rows[0][5].ToString();
                        //ddldesignation.Items.FindByValue(ds.Tables[0].Rows[0][6].ToString()).Selected = true;
                        // ddldesignation.Items.FindByText(ds.Tables[0].Rows[0][6].ToString()).Selected=true;  
                        ddlstatus.SelectedValue = ds.Tables[0].Rows[0][7].ToString();
                        txtphone.Text = ds.Tables[0].Rows[0][8].ToString();
                        txtaddress.Text = ds.Tables[0].Rows[0][9].ToString();
                        ViewState["address"] = ds.Tables[0].Rows[0][9].ToString();
                        if (ds.Tables[0].Rows[0][10].ToString() != "")
                        {
                            FillListBoxImage(ds.Tables[0].Rows[0][10].ToString());
                            string curItem = lstboxUploadedImages.SelectedItem.ToString();
                            Image2.ImageUrl = Server.MapPath("~/UserImages") + "\\" + curItem;
                            Image2.ImageUrl = "~/UserImages" + "\\" + curItem;
                        }
                        else
                        {
                            lstboxUploadedImages.SelectedIndex = -1;
                        }
                        Session["attachments"] = ds.Tables[0].Rows[0][11].ToString();
                        FillDocument();
                        txtfristname.Text = ds.Tables[0].Rows[0][12].ToString();
                        ViewState["FristName"] = ds.Tables[0].Rows[0][12].ToString();
                        txtlastname.Text = ds.Tables[0].Rows[0][13].ToString();
                        ViewState["LastName"] = ds.Tables[0].Rows[0][13].ToString();
                        txtZip.Text = ds.Tables[0].Rows[0][14].ToString();
                        ViewState["Zipi9"] = ds.Tables[0].Rows[0][14].ToString();
                        txtState.Text = ds.Tables[0].Rows[0][15].ToString();
                        ViewState["State"] = ds.Tables[0].Rows[0][15].ToString();
                        txtCity.Text = ds.Tables[0].Rows[0][16].ToString();
                        ViewState["City"] = ds.Tables[0].Rows[0][16].ToString();
                        txtbusinessname.Text = ds.Tables[0].Rows[0][17].ToString();
                        ViewState["BusinessName"] = ds.Tables[0].Rows[0][17].ToString();
                        txtssn.Text = ds.Tables[0].Rows[0][18].ToString();
                        ViewState["SSN"] = ds.Tables[0].Rows[0][18].ToString();
                        txtSignature.Text = ds.Tables[0].Rows[0][19].ToString();
                        ViewState["Signature"] = ds.Tables[0].Rows[0][19].ToString();
                        DOBdatepicker.Text = ds.Tables[0].Rows[0][20].ToString();
                        ViewState["DOB"] = ds.Tables[0].Rows[0][20].ToString();
                        txtssn0.Text = ds.Tables[0].Rows[0][21].ToString();
                        ViewState["SSN1"]=ds.Tables[0].Rows[0][21].ToString();
                        txtssn1.Text = ds.Tables[0].Rows[0][22].ToString();
                        ViewState["SSN2"] = ds.Tables[0].Rows[0][22].ToString();
                        ddlcitizen.SelectedValue = ds.Tables[0].Rows[0][23].ToString();
                        ViewState["citizen"] = ds.Tables[0].Rows[0][23].ToString();
                        txtTIN.Text = ds.Tables[0].Rows[0][24].ToString();
                         ViewState["TIN"]=ds.Tables[0].Rows[0][24].ToString();
                        txtEIN.Text = ds.Tables[0].Rows[0][25].ToString();
                         ViewState["EIN1"]=ds.Tables[0].Rows[0][25].ToString();
                        txtEIN2.Text = ds.Tables[0].Rows[0][26].ToString();
                         ViewState["EIN2"]=ds.Tables[0].Rows[0][26].ToString();
                        string strssn = ((ds.Tables[0].Rows[0][18].ToString() != "") ? ds.Tables[0].Rows[0][18].ToString() : "") + ((ds.Tables[0].Rows[0][21].ToString() != "") ? "- " + ds.Tables[0].Rows[0][21].ToString() : string.Empty) + ((ds.Tables[0].Rows[0][22].ToString() != "") ? "-" + ds.Tables[0].Rows[0][22].ToString() : string.Empty);
                        ViewState["ssn"] = strssn;
                        string str = ((ds.Tables[0].Rows[0][16].ToString() != "") ? ds.Tables[0].Rows[0][16].ToString() : "") + ((ds.Tables[0].Rows[0][15].ToString() != "") ? ", " + ds.Tables[0].Rows[0][15].ToString() : string.Empty) + ((ds.Tables[0].Rows[0][14].ToString() != "") ? ", " + ds.Tables[0].Rows[0][14].ToString() : string.Empty);
                        ViewState["Zip"] = str;
                        ddlMaritalstatus.SelectedValue = ds.Tables[0].Rows[0][27].ToString();
                        txtA.Text = ds.Tables[0].Rows[0][28].ToString();
                        txtB.Text = ds.Tables[0].Rows[0][29].ToString();
                        txtC.Text = ds.Tables[0].Rows[0][30].ToString();
                        txtD.Text = ds.Tables[0].Rows[0][31].ToString();
                        txtE.Text = ds.Tables[0].Rows[0][32].ToString();
                        txtF.Text = ds.Tables[0].Rows[0][33].ToString();
                        txtG.Text = ds.Tables[0].Rows[0][34].ToString();
                        txtH.Text = ds.Tables[0].Rows[0][35].ToString();
                        txt5.Text = ds.Tables[0].Rows[0][36].ToString();
                        txt6.Text = ds.Tables[0].Rows[0][37].ToString();
                        txt7.Text = ds.Tables[0].Rows[0][38].ToString();
                        chkboxcondition.Checked = true;
                        chkboxcondition.Enabled = false;
                    }
                }
                
            }
     
        }
        private void FillDocument()
        {
            string attach = Session["attachments"] as string;
            if (attach != null)
            {
                string[] att = attach.Split(',');
                var data = att.Select(s => new { FileName = s }).ToList();
                if (string.IsNullOrWhiteSpace(attach))
                {
                    data.Clear();
                }
                gvUploadedFiles.DataSource = data;
                gvUploadedFiles.DataBind();
            }
        }
        private void FillListBoxImage(string imageName)
        {
            string[] arr = imageName.Split(',');
            if (flpUplaodPicture != null)
            {
                foreach (string img in arr)
                {
                    lstboxUploadedImages.Items.Add(img);
                }
                lstboxUploadedImages.SelectedIndex = 0;
            }
            else
            {
                foreach (string img in arr)
                {
                    lstboxUploadedImages.SelectedIndex = -2;
                }

            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            txtpassword.Attributes["value"] = txtpassword.Text;
            txtpassword1.Attributes["value"] = txtpassword1.Text;
            base.OnPreRender(e);
        }
        protected void btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                btn_UploadFiles_Click(sender, e);

                if (ddlstatus.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Status');", true);
                    return;
                }
                else
                {
                    objuser.status = ddlstatus.SelectedValue;
                }

                if (ddlusertype.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select User Type');", true);
                    return;
                }
                else
                {
                    objuser.usertype = ddlusertype.SelectedValue;
                }
                objuser.username = txtusername.Text;
                objuser.loginid = txtloginid.Text.Trim();
                objuser.email = txtemail.Text.Trim();
                objuser.password = txtpassword.Text;
                objuser.designation = null;
                objuser.phone = txtphone.Text;
                objuser.address = txtaddress.Text;
                ViewState["address"] = txtaddress.Text;
                objuser.fristname = txtfristname.Text;
                objuser.lastname = txtlastname.Text;
                objuser.zip = txtZip.Text;
                objuser.state = txtState.Text;
                objuser.city = txtCity.Text;
                objuser.businessname = txtbusinessname.Text;
                objuser.ssn = txtssn.Text;
                objuser.ssn1 = txtssn0.Text;
                objuser.ssn2 = txtssn1.Text;
                string ssn = txtssn.Text + "-" + txtssn0.Text + "-" + txtssn1.Text;
                ViewState[ssn] = ssn;
                objuser.signature = txtSignature.Text;
                objuser.dob = DOBdatepicker.Text;
                objuser.citizenship = ddlcitizen.SelectedValue;
                objuser.tin = txtTIN.Text;
                objuser.ein1 = txtEIN.Text;
                objuser.ein2 = txtEIN2.Text;
                objuser.maritalstatus = ddlMaritalstatus.SelectedValue;
                objuser.a = txtA.Text;
                objuser.b = txtB.Text;
                objuser.c = txtC.Text;
                objuser.d = txtD.Text;
                objuser.e = txtE.Text;
                objuser.f = txtF.Text;
                objuser.g = txtG.Text;
                objuser.h = txtH.Text;
                objuser.i = txt5.Text;
                objuser.j = txt6.Text;
                objuser.k = txt7.Text;
                // lblName.Text = ViewState["FristName"].ToString() + ViewState["LastName"].ToString();
                if (flpUplaodPicture.FileName != string.Empty)
                {
                    foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                    {
                        fn = fn + "," + img.Text;
                    }
                    Server.MapPath("~/UserImages" + "\\" + flpUplaodPicture.FileName);
                    objuser.picture = fn.TrimStart(',');
                }
                else if (lstboxUploadedImages.Items.Count > 0)
                {
                    foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                    {
                        fn = fn + "," + img.Text;
                    }
                    objuser.picture = fn.TrimStart(',');
                }
                else
                {
                    objuser.picture = string.Empty;
                }
                string strFileName = string.Empty;
                if (ViewState["FileName"] != null)
                {
                    strFileName = ViewState["FileName"].ToString();
                    strFileName = strFileName.TrimStart(',');
                    objuser.attachements = strFileName;
                }
                else
                {
                    objuser.attachements = strFileName;
                }
                bool result = UserBLL.Instance.AddUser(objuser);
                if (result)
                {
                    lblmsg.CssClass = "success";
                    lblmsg.Text = "User has been created successfully";
                    lblmsg.Visible = true;
                    clearcontrols();
                    GoogleCalendarEvent.CreateCalendar(txtemail.Text, txtaddress.Text);
                    shareCalender(txtemail.Text);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User has been created successfully');", true);
                    clearcontrols();
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "error";
                    lblmsg.Text = "User with this email id already exist";

                }
            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "CreateUser", "");
                //LogManager.Instance.WriteToFlatFile(ex.Message, "Custom",1);// Request.ServerVariables["remote_addr"].ToString());
                
            }
        }

        protected void shareCalender(string emailId)
        {
            CalendarService service = new CalendarService("CalendarSampleApp");

            string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
            string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();

            service.setUserCredentials(Adminuser, AdminPwd);

            AclEntry entry = new AclEntry();

            entry.Scope = new AclScope();
            entry.Scope.Type = AclScope.SCOPE_USER;
            entry.Scope.Value =  emailId;

            entry.Role = AclRole.ACL_CALENDAR_READ;

            Uri aclUri = new Uri(string.Format("https://www.google.com/calendar/feeds/{0}/acl/full", Adminuser));

         AclEntry insertedEntry = service.Insert(aclUri, entry) as AclEntry;
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            clearcontrols();
            lblmsg.Visible = false;
        }
        private void clearcontrols()
        {
           ddlstatus.SelectedValue =ddlusertype.SelectedValue=ddlcitizen.SelectedValue=ddlMaritalstatus.SelectedValue= "0";
            txtusername.Text = txtloginid.Text = txtemail.Text  = txtphone.Text = txtaddress.Text = null;
            lstboxUploadedImages.Items.Clear(); Image2.Visible = false;
            gvUploadedFiles.DataSource = null;
            gvUploadedFiles.DataBind();
            gvUploadedFiles.Visible = false;
            txtpassword.Text = txtpassword1.Text =txtfristname.Text=txtlastname.Text=txtZip.Text=txtState.Text=txtCity.Text = null;
            txtssn.Text = txtssn0.Text = txtssn1.Text = txtSignature.Text = DOBdatepicker.Text =txtbusinessname.Text= null;
            txtEIN.Text = txtEIN2.Text = txtTIN.Text = null;
        }
        protected void btn_UploadPicture_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string save = string.Empty;
            if (flpUplaodPicture.FileName != string.Empty)
            {
                fileName = Guid.NewGuid().ToString() + "_" + flpUplaodPicture.FileName;
                save = Server.MapPath("~/UserImages") + "\\" + fileName;
                flpUplaodPicture.SaveAs(save);
               int cnt = lstboxUploadedImages.Items.Count;
                if (cnt == 0)
                {
                    lstboxUploadedImages.Items.Add(fileName);
                    lstboxUploadedImages.SelectedIndex = 0;
                    string curItem = lstboxUploadedImages.SelectedItem.ToString();
                    Image2.Visible = true;
                    Image2.ImageUrl = "~/UserImages" + "\\" + fileName;
                    return;

                }
                if (fileName != "")
                {
                    lstboxUploadedImages.Items.Add(fileName);
                    lstboxUploadedImages.SelectedIndex = 0;
                }
            }
           
        }
        protected void lstbxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = lstboxUploadedImages.SelectedItem.ToString();
            Image2.ImageUrl = "~/UserImages" + "\\" + curItem;
        }
        protected void btn_UploadFiles_Click(object sender, EventArgs e)
        {
            HttpFileCollection uploads = Request.Files;
            string attach = "";
            if (Session["attachments"] != null)
            {
                attach = Session["attachments"] as string;
            }
            for (int fileCount = 0; fileCount < uploads.Count; fileCount++)
            {
                HttpPostedFile uploadedFile = uploads[fileCount];

                string fileName = Path.GetFileName(uploadedFile.FileName);
                newAttachments.Add(uploadedFile.FileName);
                if (uploadedFile.ContentLength > 0)
                {
                    if (!Request.Files.AllKeys[fileCount].Contains("flpUplaodPicture"))
                    {
                        string filename = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;//flpUploadFiles.FileName;
                        Server.MapPath("/UploadedFiles/" + filename);
                        uploadedFile.SaveAs(Server.MapPath("/UploadedFiles/" + filename));

                        //string attach = Session["attachments"] as string;
                        if (string.IsNullOrWhiteSpace(attach))
                        {
                            attach = filename;

                        }
                        else
                        {
                            attach = attach + "," + filename;
                        }
                        Session["attachments"] = attach;
                        ViewState["FileName"] = attach;
                    }
                    if (attach != null)
                    {
                        string[] att = attach.Split(',');
                        var data = att.Select(s => new { FileName = s }).ToList();
                        gvUploadedFiles.DataSource = data;
                        gvUploadedFiles.DataBind();
                    }
                }
            }

        }
        protected void btndelete_Click1(object sender, EventArgs e)
        {
            if (lstboxUploadedImages.SelectedIndex >= 0)
            {
                int Index = lstboxUploadedImages.SelectedIndex;
                string strDelete = lstboxUploadedImages.SelectedItem.Text;
                lstboxUploadedImages.Items.Remove(strDelete);
                var path = Server.MapPath("~/UserImages\\" + strDelete);
                System.IO.File.Delete(path);

                if (lstboxUploadedImages.Items.Count > 0)
                {
                    //lstboxUploadedImages.SelectedIndex = Index - 1;
                    lstboxUploadedImages.SelectedIndex = 0;
                    string Remainfile = lstboxUploadedImages.SelectedItem.Text;
                    Image2.ImageUrl = "~/UserImages" + "\\" + Remainfile;
                }

                if (lstboxUploadedImages.SelectedIndex != -1)
                {
                    lstboxUploadedImages.SelectedIndex = 0;
                    string Remainfile = lstboxUploadedImages.SelectedItem.Text;
                    Image2.ImageUrl = "~/UserImages" + "\\" + Remainfile;
                }
                else
                {
                   /// Image2.ImageUrl = "";
                }
            }
        }
        private void DeleteAttachment()
        {
            if (ViewState["FilesToDelete"] != null)
            {
                string filesTodeleteString = Convert.ToString(ViewState["FilesToDelete"]);
                List<string> filesTodelete = new List<string> { filesTodeleteString };
                try
                {
                    var path = Server.MapPath("UploadedFiles\\" + filesTodeleteString);
                    System.IO.File.Delete(path);
                    string attachmentsString = Convert.ToString(Session["attachments"]);
                    var updatedAttachments = attachmentsString.Split(',').Except(filesTodelete).ToArray();
                    Session["attachments"] = string.Join(",", updatedAttachments);
                }
                catch (Exception)
                {


                }
            }
        }
        private string GetUpdateAttachments()
        {
            string CsvAttachments = string.Empty;
            var filesTodelete = new List<string>();
            if (ViewState["FilesToDelete"] != null)
            {
                filesTodelete = ViewState["FilesToDelete"] as List<string>;
            }
            try
            {
                string attachments = Session["attachments"] as string;
                string attach = Session["attachments"] as string;
                attachments = attachments.Trim(',').TrimEnd(',');
                 ViewState["FileName"] = attach;
                if (attach != null)
                {
                    string[] att = attachments.Split(',');
                    var data = att.Select(s => new { FileName = s }).ToList();
                    gvUploadedFiles.DataSource = data;
                    gvUploadedFiles.DataBind();
                }
                var updatedAttachments = attachments.Split(',');
                CsvAttachments = string.Join(",", updatedAttachments);
                CsvAttachments = CsvAttachments.TrimStart(',');
            }
            catch (Exception)
            {
            }
            return CsvAttachments;
        }
        protected void gvUploadedFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteRecord")
            {
                string file = e.CommandArgument.ToString();
                ViewState["FilesToDelete"] = file;
                DeleteAttachment();
                FillDocument();
            }
            if (e.CommandName == "DownloadRecord")
            {
                string file = e.CommandArgument.ToString();
                string filePath = ("~\\UploadedFiles" + "\\" + file);
                string strUrl = filePath;
                WebClient req = new System.Net.WebClient();
                HttpResponse response = HttpContext.Current.Response;
                Response.Clear();
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filePath);
                byte[] data = req.DownloadData(Server.MapPath(strUrl));
                Response.ContentType = "application/doc";
                response.BinaryWrite(data);
                response.End();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            btn_UploadFiles_Click(sender, e);
            objuser.username = txtusername.Text;
            objuser.loginid = txtloginid.Text;
            objuser.email = txtemail.Text.Trim();
            objuser.address = txtaddress.Text;
            objuser.password = txtpassword.Text;
            objuser.usertype = ddlusertype.SelectedValue;
            //objuser.designation = ddldesignation.SelectedValue;
            objuser.designation = null;
            objuser.status = ddlstatus.SelectedValue;
            objuser.phone = txtphone.Text;
            objuser.fristname = txtfristname.Text;
            objuser.lastname = txtlastname.Text;
            objuser.zip = txtZip.Text;
            objuser.state = txtState.Text;
            objuser.city = txtCity.Text;
            objuser.businessname = txtbusinessname.Text;
            objuser.ssn = txtssn.Text;
            objuser.ssn1 = txtssn0.Text;
            objuser.ssn2 = txtssn1.Text;
            objuser.signature = txtSignature.Text;
            objuser.dob = DOBdatepicker.Text;
            objuser.citizenship = ddlcitizen.SelectedValue;
            objuser.tin = txtTIN.Text;
            objuser.ein1 = txtEIN.Text;
            objuser.ein2 = txtEIN2.Text;
            objuser.maritalstatus = ddlMaritalstatus.SelectedValue;
            objuser.a = txtA.Text;
            objuser.b = txtB.Text;
            objuser.c = txtC.Text;
            objuser.d = txtD.Text;
            objuser.e = txtE.Text;
            objuser.f = txtF.Text;
            objuser.g = txtG.Text;
            objuser.h = txtH.Text;
            objuser.i = txt5.Text;
            objuser.j = txt6.Text;
            objuser.k = txt7.Text;
            //lblName.Text = ViewState["FristName"].ToString() + ViewState["LastName"].ToString();
            if (lstboxUploadedImages.Items.Count != 0)
            {
                foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                {
                    fn = fn + "," + img.Text;
                }
                objuser.picture = fn.TrimStart(',');
            }
            else if (lstboxUploadedImages.Items.Count > 0)
            {
                foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                {
                    fn = fn + "," + img.Text;
                }
                objuser.picture = fn.TrimStart(',');
            }
            else
            {
                objuser.picture = "";
            }

            objuser.attachements = GetUpdateAttachments();
            int id = Convert.ToInt32(Session["ID"]);
            bool result = UserBLL.Instance.UpdatingUser(objuser, id);
            GoogleCalendarEvent.CreateCalendar(txtemail.Text, txtaddress.Text);
            lblmsg.Visible = true;
            lblmsg.CssClass = "success";
            lblmsg.Text = "User Details Updated  successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User  Update successfully');", true);
            clearcontrols();
            Server.Transfer("EditUser.aspx");
           
        }

        protected void linkbuttonW9_Click(object sender, EventArgs e)
        {
            try
            {
                var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/w9.pdf"));
                var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
                var response = HttpContext.Current.Response;
                formFieldMap["topmostSubform[0].Page1[0].f1_01_0_[0]"] = Session["FirstName"] == null ? "" : Session["FirstName"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].f1_02_0_[0]"] = Session["LastName"] == null ? "" : Session["LastName"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].f1_04_0_[0]"] = Session["Address"] == null ? "" : Session["Address"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].f1_05_0_[0]"] = Session["Zip"] == null ? "" : Session["Zip"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].f1_02_0_[0]"] = ViewState["BusinessName"] == null ? "" : ViewState["BusinessName"].ToString();
                formFieldMap["Text1"] = ViewState["Signature"] == null ? "" : ViewState["Signature"].ToString();
                formFieldMap["Text2"] = System.DateTime.Now == null ? "" : System.DateTime.Now.ToShortDateString();
                formFieldMap["topmostSubform[0].Page1[0].social[0].TextField1[0]"] = ViewState["SSN"] == null ? "" : ViewState["SSN"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[0]"] = ViewState["SSN1"] == null ? "" : ViewState["SSN1"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[1]"] = ViewState["SSN2"] == null ? "" : ViewState["SSN2"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[2]"] = ViewState["ein1"] == null ? "" : ViewState["ein1"].ToString();
                formFieldMap["topmostSubform[0].Page1[0].social[0].TextField2[3]"] = ViewState["ein2"] == null ? "" : ViewState["ein2"].ToString();
                var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
                string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";
                //string PdflDirectory = Server.MapPath("/PDFS");
                string Filename = "W9_" + txtfristname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
                //string path = PdflDirectory + "\\" + Filename;
                PDFHelper.ReturnPDF(pdfContents, Filename);
               
            }
            catch (Exception ex)
            {
                //ex.Message;
            }
            finally
            {

            }

        }

        protected void txtusername_TextChanged(object sender, EventArgs e)
        {
            ViewState["Name"] = txtusername.Text;
            txtloginid.Focus();
        }

      protected void txtaddress_TextChanged1(object sender, EventArgs e)
        {
            ViewState["address"] = txtaddress.Text;
            flpUplaodPicture.Focus();
        }

      protected void linkbuttonI9_Click(object sender, EventArgs e)
      {
          var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/I-9.pdf"));
          var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
          formFieldMap["Text2"] = ViewState["FristName"] == null ? "" : ViewState["FristName"].ToString();
          formFieldMap["Text1"] = ViewState["LastName"] == null ? "" : ViewState["LastName"].ToString();
          formFieldMap["Text4"] = ViewState["address"] == null ? "" : ViewState["address"].ToString();
          //formFieldMap["Text8"] = ViewState["Zip"] == null ? "" : ViewState["Zip"].ToString();
          formFieldMap["Text37"] = ViewState["ssn"] == null ? "" : ViewState["ssn"].ToString();
          formFieldMap["Text6"] = ViewState["DOB"] == null ? "" : ViewState["DOB"].ToString();
          formFieldMap["Text40"] = ViewState["Signature"] == null ? "" : ViewState["Signature"].ToString();
          formFieldMap["Text41"] = System.DateTime.Now.ToShortDateString();
          formFieldMap["Text37"] = ViewState["Zipi9"] == null ? "" : ViewState["Zipi9"].ToString();
          formFieldMap["Text8"] = ViewState["City"] == null ? "" : ViewState["City"].ToString();
          formFieldMap["Text42"] = ViewState["State"] == null ? "" : ViewState["State"].ToString();
          if (ViewState["citizen"] != null)
          {
              if (ddlcitizen.SelectedValue == "USCitizenship")
              {
                  formFieldMap["Check Box13"] = "Yes";
              }
              else if (ddlcitizen.SelectedValue == "NonUSCitizenship")
              {
                  formFieldMap["Check Box14"] = "Yes";
              }
              else if (ddlcitizen.SelectedValue == "permanentresident")
              {
                  formFieldMap["Check Box15"] = "Yes";
              }
              else
              {
                  formFieldMap["Check Box16"] = "Yes";
              }
          }
          var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
          string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";
          string Filename = "I9_" + txtusername.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
          PDFHelper.ReturnPDF(pdfContents,Filename);
      }

      [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
      public static string[] GetZipcodes(string prefixText)
      {
          List<string> ZipCodes = new List<string>();
          DataSet dds = new DataSet();
          dds = UserBLL.Instance.fetchzipcode(prefixText);
          foreach (DataRow dr in dds.Tables[0].Rows)
          {
              ZipCodes.Add(dr[0].ToString());
          }
          return ZipCodes.ToArray();
      }

      protected void txtZip_TextChanged(object sender, EventArgs e)
      {
          if (txtZip.Text == "")
          {
              //
          }
          else
          {

              DataSet ds = new DataSet();
              ds = UserBLL.Instance.fetchcitystate(txtZip.Text);
              if (ds != null)
              {
                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                      txtState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                  }
                  else
                  {
                      txtCity.Text = txtState.Text = "";
                  }
              }
              string str = ((txtCity.Text != "") ? txtCity.Text : "") + ((txtState.Text != "") ? ", " + txtState.Text : string.Empty) + ((txtZip.Text != "") ? ", " + txtZip.Text : string.Empty);
              ViewState["Zip"] = str.ToString();
              ViewState["Zipi9"] = txtZip.Text;
          }
      }

      protected void txtfristname_TextChanged(object sender, EventArgs e)
      {
          ViewState["FristName"] = txtfristname.Text;
          txtlastname.Focus();
      }

      protected void txtlastname_TextChanged(object sender, EventArgs e)
      {
          ViewState["LastName"] = txtlastname.Text;
          txtusername.Focus();
      }

      protected void txtSignature_TextChanged(object sender, EventArgs e)
      {
          ViewState["Signature"] = txtSignature.Text;
      }

    

      protected void txtbusinessname_TextChanged(object sender, EventArgs e)
      {
          ViewState["BusinessName"] = txtbusinessname.Text;
      }

      protected void txtssn1_TextChanged(object sender, EventArgs e)
      {
          ViewState["SSN2"] = txtssn1.Text;
          string strssn = ((txtssn.Text != "") ? txtssn.Text : "") + ((txtssn0.Text != "") ? "- " + txtssn0.Text : string.Empty) + ((txtssn1.Text != "") ? "-" + txtssn1.Text : string.Empty);
          ViewState["ssn"] = strssn;
      }

      protected void txtssn_TextChanged(object sender, EventArgs e)
      {
          ViewState["SSN"] = txtssn.Text;
         
      }

      protected void txtssn0_TextChanged(object sender, EventArgs e)
      {
          ViewState["SSN1"] = txtssn0.Text;
          
      }

      protected void DOBdatepicker_TextChanged(object sender, EventArgs e)
      {
          ViewState["DOB"]= DOBdatepicker.Text;
      }
      protected void txtTIN_TextChanged(object sender, EventArgs e)
      {

          ViewState["tin"] = txtTIN.Text;
      }

      protected void ddlcitizen_SelectedIndexChanged(object sender, EventArgs e)
      {
          ViewState["citizen"] = ddlcitizen.SelectedValue;
      }
      protected void txtEIN_TextChanged(object sender, EventArgs e)
      {
          ViewState["ein1"] = txtEIN.Text;
      }

      protected void txtEIN2_TextChanged(object sender, EventArgs e)
      {
          ViewState["ein2"] = txtEIN2.Text;
      }

      protected void ddlMaritalstatus_SelectedIndexChanged(object sender, EventArgs e)
      {
          ViewState["Maritalstatus"] = ddlMaritalstatus.SelectedValue;
         
      }

      protected void lnkbuttonW4_Click(object sender, EventArgs e)
      {
          var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/w4.pdf"));
          var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
          formFieldMap["topmostSubform[0].Page1[0].f1_09_0_[0]"] = Session["FirstName"] == null ? "" : Session["FirstName"].ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_10_0_[0]"] = Session["LastName"] == null ? "" : Session["LastName"].ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_14_0_[0]"] = Session["Address"] == null ? "" : Session["Address"].ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_15_0_[0]"] = Session["Zip"] == null ? "" : Session["Zip"].ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_13_0_[0]"] = ViewState["ssn"] == null ? "" : ViewState["ssn"].ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_22_0_[0]"] = ViewState["ein"] == null ? "" : ViewState["ein"].ToString();
          formFieldMap["Text2"] = System.DateTime.Now.ToShortDateString();
          formFieldMap["Text1"] = ViewState["Signature"] == null ? "" : ViewState["Signature"].ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_01_0_[0]"] = txtA.Text == null ? "" : txtA.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_02_0_[0]"] = txtB.Text == null ? "" : txtB.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_03_0_[0]"] = txtC.Text == null ? "" : txtC.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_04_0_[0]"] = txtD.Text == null ? "" : txtD.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_05_0_[0]"] = txtE.Text == null ? "" : txtE.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_06_0_[0]"] = txtF.Text == null ? "" : txtF.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_07_0_[0]"] = txtG.Text == null ? "" : txtG.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_08_0_[0]"] = txtH.Text == null ? "" : txtH.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_16_0_[0]"] = txt5.Text == null ? "" : txt5.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_17_0_[0]"] = txt6.Text == null ? "" : txt6.Text.ToString();
          formFieldMap["topmostSubform[0].Page1[0].f1_18_0_[0]"] = txt7.Text == null ? "" : txt7.Text.ToString();
          if (ViewState["Maritalstatus"] != null)
          {
              if (ddlMaritalstatus.SelectedValue == "Single")
              {
                  formFieldMap["topmostSubform[0].Page1[0].p1-cb1[0]"] = "1";
              }
              else
              {
                  formFieldMap["Check Box2"] = "Yes";
              }
          }
          var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
          string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";
          string PdflDirectory = Server.MapPath("/PDFS");
          string Filename = "W4_" + txtfristname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
          string path = PdflDirectory + "\\" + Filename;
          PDFHelper.ReturnPDF(pdfContents, Filename);
      }

      protected void lnkw4Details_Click(object sender, EventArgs e)
      {
          ModalPopupExtender1.Show();
      }

      protected void txtCity_TextChanged(object sender, EventArgs e)
      {
          ViewState["City"] = txtCity.Text;
      }

      protected void txtState_TextChanged(object sender, EventArgs e)
      {
          ViewState["State"] = txtState.Text;
      }
      
    }
}