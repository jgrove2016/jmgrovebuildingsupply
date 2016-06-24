using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common;
using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using System.Data;
using System.Configuration;
using System.IO;
using JG_Prospect.App_Code;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using System.Net;
namespace JG_Prospect.Sr_App
{
    public partial class InstallCreateProspect : System.Web.UI.Page
    {
        user objuser = new user();
        //ProspectClass objprospect = new ProspectClass();
        string fn;
        List<string> newAttachments = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Session["Username"].ToString()))
            //{
            //    txtSource.Text = Session["Username"].ToString();
            //}
            //else
            //{
            //    Response.Redirect("/login.aspx");
            //}

            if (Session["Username"] != null)
            {
                txtSource.Text = Session["Username"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alsert('Your session has expired,login to contineu');window.location='../login.aspx'", true);
            }

<<<<<<< HEAD
            
=======

>>>>>>> refs/remotes/origin/master
            if (!IsPostBack)
            {
                //if (Request.QueryString["ImageUpload"] == null)
                //{
                txtSecTradeOthers.Visible = false;
                txtOtherTrade.Visible = false;
                Session["UploadFileCountProspect"] = 0;
                Session["ProspectAttachment"] = null;
                //Session["ProspectAttachment"] = "";
                Session["IdGenerated"] = "";
                BindDDLs();
<<<<<<< HEAD
=======
                BindDesignation();
>>>>>>> refs/remotes/origin/master
                DateSourced.Text = DateTime.Today.ToString("MM/dd/yyyy");
                //btnUpdate.Visible = false;
                //btn_UploadFiles.Visible = false;
                //gvUploadedFiles.Visible = false;
                DataSet ds;

                if (Request.QueryString["ID"] != null)
                {
                    btnUpdate.Visible = true;
                    btncreate.Visible = false;
                    btnreset.Visible = false;
                    AsyncFileUploadCustomerAttachment.Visible = true;
                    gvUploadedFiles.Visible = true;
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    Session["ID"] = id;
                    //ds = InstallProspectBLL.Instance.getProspectdetails(id);
                    //if (ds.Tables[0].Rows.Count != 0)
                    //{
                    //    txtfirstname.Text = ds.Tables[0].Rows[0][1].ToString();
                    //    Session["FirstName"] = ds.Tables[0].Rows[0][1].ToString();
                    //    txtlastname.Text = ds.Tables[0].Rows[0][2].ToString();
                    //    Session["LastName"] = ds.Tables[0].Rows[0][2].ToString();
                    //    txtemail.Text = ds.Tables[0].Rows[0][3].ToString();
                    //    ViewState["Email"] = ds.Tables[0].Rows[0][3].ToString();
                    //    txtCompanyName.Text = ds.Tables[0].Rows[0][4].ToString();
                    //    Session["CompanyName"] = ds.Tables[0].Rows[0][4].ToString();
                    //    txtemail2.Text = ds.Tables[0].Rows[0][5].ToString();
                    //    ViewState["Email2"] = ds.Tables[0].Rows[0][5].ToString();
                    //    txtSource.Text = Session["Username"].ToString();
                    //    ViewState["Source"] = ds.Tables[0].Rows[0][7].ToString();
                    //    txtPhone.Text = ds.Tables[0].Rows[0][8].ToString();
                    //    ViewState["Phone"] = ds.Tables[0].Rows[0][8].ToString();
                    //    txtPhone2.Text = ds.Tables[0].Rows[0][9].ToString();
                    //    ViewState["Phone2"] = ds.Tables[0].Rows[0][9].ToString();
                    //    txtnotes.Text = ds.Tables[0].Rows[0][10].ToString();
                    //    ViewState["Notes"] = ds.Tables[0].Rows[0][10].ToString();
                    //    DateSourced.Text = ds.Tables[0].Rows[0][11].ToString();
                    //    ViewState["DateSourced"] = ds.Tables[0].Rows[0][11].ToString();
                    //    ddlPrimaryTrade.SelectedValue = ds.Tables[0].Rows[0][12].ToString();
                    //    ddlSecondaryTrade.SelectedValue = ds.Tables[0].Rows[0][13].ToString();
                    //    Session["ProspectAttachment"] = ds.Tables[0].Rows[0][14].ToString();
                    //    FillDocument();

                    //}
                    //}
                }
                else
                {
                    btnUpdate.Visible = false;
                    DateSourced.Text = DateTime.Today.ToString("MM/dd/yyyy");
                }
                if (Convert.ToString(Session["ProspectAttachment"]) != "")
                {
                    displayfiles(Convert.ToString(Session["ProspectAttachment"]));
                }
            }

        }

        private void FillDocument()
        {
            string attach = Session["ProspectAttachment"] as string;
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


        protected void btnreset_Click(object sender, EventArgs e)
        {
            clearcontrols();
            //lblmsg.Visible = false;
        }

        private void clearcontrols()
        {
            //ddlPrimaryTrade.SelectedValue = ddlSecondaryTrade.SelectedValue = "0";
            txtfirstname.Text = txtlastname.Text = txtemail.Text = txtPhone.Text = txtCompanyName.Text = txtemail2.Text = txtPhone2.Text = DateSourced.Text = txtnotes.Text = null;
            gvUploadedFiles.Visible = false;
<<<<<<< HEAD

=======
            BindDesignation();
            isInstallUser = false;
>>>>>>> refs/remotes/origin/master
        }

        private void BindDDLs()
        {

            DataSet DS = new DataSet();
            DS = InstallUserBLL.Instance.getTrades();
            ddlPrimaryTrade.DataSource = DS.Tables[0];
            ddlPrimaryTrade.DataTextField = "TradeName";
            ddlPrimaryTrade.DataValueField = "Id";
            ddlPrimaryTrade.DataBind();
            ddlPrimaryTrade.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            ddlSecondaryTrade.DataSource = DS.Tables[0];
            ddlSecondaryTrade.DataTextField = "TradeName";
            ddlSecondaryTrade.DataValueField = "Id";
            ddlSecondaryTrade.DataBind();
            ddlSecondaryTrade.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }

        private string GetId()
        {
            DataTable dtId;
            string LastInt = string.Empty;
            string installId = string.Empty;
            dtId = InstallUserBLL.Instance.getMaxId("Installer", "Applicant");
            if (dtId.Rows.Count > 0)
            {
                installId = Convert.ToString(dtId.Rows[0][0]);
            }
            if (installId != "")
            {

                LastInt = installId.Substring(installId.Length - 4);
                installId = "INS000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);
            }
            else
            {
                installId = "INS0001";
            }
            Session["installId"] = installId;
            return installId;
        }

        protected void btncreate_Click(object sender, EventArgs e)
        {
            StringBuilder err = new StringBuilder();
<<<<<<< HEAD
          
=======

>>>>>>> refs/remotes/origin/master
            try
            {
                lblException.Text = Convert.ToString(err.Append("Before GetId"));
                string InstallId = string.Empty;
                Session["IdGenerated"] = GetId();
                // btn_UploadFiles_Click(sender, e);
<<<<<<< HEAD
                if (ddlPrimaryTrade.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Primary Trade');", true);
                    return;
                }
                else if (ddlPrimaryTrade.SelectedValue != "17")
                {
                    objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                }
                else if (ddlPrimaryTrade.SelectedValue == "17")
                {
                    objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                    objuser.PTradeOthers = txtOtherTrade.Text;
                }

                if (ddlSecondaryTrade.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Secondary Trade');", true);
                    return;
                }
                else if (ddlPrimaryTrade.SelectedValue != "17")
                {
                    objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                }
                else if (ddlPrimaryTrade.SelectedValue == "17")
                {
                    objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                    objuser.PTradeOthers = txtSecTradeOthers.Text;
=======
                if (isInstallUser)
                {
                    if (ddlPrimaryTrade.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Primary Trade');", true);
                        return;
                    }
                    else if (ddlPrimaryTrade.SelectedValue != "17")
                    {
                        objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                    }
                    else if (ddlPrimaryTrade.SelectedValue == "17")
                    {
                        objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                        objuser.PTradeOthers = txtOtherTrade.Text;
                    }

                    if (ddlSecondaryTrade.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Secondary Trade');", true);
                        return;
                    }
                    else if (ddlPrimaryTrade.SelectedValue != "17")
                    {
                        objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                    }
                    else if (ddlPrimaryTrade.SelectedValue == "17")
                    {
                        objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                        objuser.PTradeOthers = txtSecTradeOthers.Text;
                    }
>>>>>>> refs/remotes/origin/master
                }
                objuser.fristname = txtfirstname.Text;
                objuser.lastname = txtlastname.Text;
                objuser.email = txtemail.Text.Trim();
                objuser.Email2 = txtemail2.Text.Trim();
                objuser.CompanyName = txtCompanyName.Text;
                objuser.phone = txtPhone.Text;
                objuser.Phone2 = txtPhone2.Text;
                objuser.Notes = txtnotes.Text;
                InstallId = Convert.ToString(Session["IdGenerated"]);
                objuser.InstallId = InstallId;
                objuser.SourceUser = Convert.ToString(Session["userid"]);
                objuser.Source = Convert.ToString(Session["Username"]);

                lblException.Text = Convert.ToString(err.Append("Before CheckSource"));
                DataSet ds = InstallUserBLL.Instance.CheckSource(Convert.ToString(Session["Username"]));
                lblException.Text = Convert.ToString(err.Append("After CheckSource"));
<<<<<<< HEAD
               // if (ds.Tables[0].Rows.Count > 0)
=======
                // if (ds.Tables[0].Rows.Count > 0)
>>>>>>> refs/remotes/origin/master
                if (ds.Tables.Count > 0)
                {
                    lblException.Text = Convert.ToString(err.Append("Checksource datasource"));
                    //do nothing
                }
                else
                {
                    lblException.Text = Convert.ToString(err.Append(" No Checksource datasource before"));
                    DataSet dsadd = InstallUserBLL.Instance.AddSource(Convert.ToString(Session["Username"]));
                    lblException.Text = Convert.ToString(err.Append(" No Checksource datasource After"));
                }
                //objprospect.str_Source = txtSource.Text;
                if (DateSourced.Text != "")
                {
                    objuser.DateSourced = DateSourced.Text;
                }
                else
                {
                    lblException.Text = Convert.ToString(err.Append(" before Date "));
                    objuser.DateSourced = DateTime.Today.ToString("MM/dd/yyyy");
                    lblException.Text = Convert.ToString(err.Append("After Date"));
                }
                lblException.Text = Convert.ToString(err.Append(" before Attachmant "));
                objuser.attachements = Convert.ToString(Session["ProspectAttachment"]);
                lblException.Text = Convert.ToString(err.Append(" After Attachmant "));
<<<<<<< HEAD
                objuser.UserType = "Prospect";
                objuser.designation = "Installer";
=======
                if (isInstallUser)
                {
                    objuser.UserType = "Prospect";
                }
                else
                {
                    objuser.UserType = "sales";
                }
                objuser.designation = ddldesignation.SelectedValue;
>>>>>>> refs/remotes/origin/master
                string strFileName = string.Empty;

                //if (ViewState["FileName"] != null)
                //{
                //    strFileName = ViewState["FileName"].ToString();
                //    strFileName = strFileName.TrimStart(',');
                //    objprospect.attachements = strFileName;
                //}
                //else
                //{
                //    objprospect.attachements = strFileName;
                //}
                objuser.status = "Install Prospect";

                lblException.Text = Convert.ToString(err.Append(" Before  CheckInstallUser "));
                DataSet dsCheckDuplicate = InstallUserBLL.Instance.CheckInstallUser(txtemail.Text, txtPhone.Text);
                lblException.Text = Convert.ToString(err.Append(" After  CheckInstallUser "));
<<<<<<< HEAD
               // if (dsCheckDuplicate.Tables[0].Rows.Count > 0)
=======
                // if (dsCheckDuplicate.Tables[0].Rows.Count > 0)
>>>>>>> refs/remotes/origin/master
                if (dsCheckDuplicate.Tables.Count > 0)
                {
                    lblException.Text = Convert.ToString(err.Append("Before Session[EmailEdiId]"));
                    Session["EmailEdiId"] = Convert.ToInt32(dsCheckDuplicate.Tables[0].Rows[0][0]);
                    lblException.Text = Convert.ToString(err.Append("After Session[EmailEdiId]"));
                    ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlay();", true);
                    //ModalPopupExtender2.Show();
                    return;
                    //string confirmValue = Request.Form["confirm_value"];
                    //if (confirmValue == "Yes")
                    //{
                    //    bool result = InstallUserBLL.Instance.UpdateInstallUser(objuser, Convert.ToInt32(dsCheckDuplicate.Tables[0].Rows[0][0]));
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User has been updated successfully');", true);
                    //    clearcontrols();
                    //    Server.Transfer("EditInstallUser.aspx");
                    //    return;
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Use diffrent Email & phone number.');", true);
                    //    return;
                    //}
                }
                else
                {
                    lblException.Text = Convert.ToString(err.Append("Before AddUser(objuser)"));
                    bool result = InstallUserBLL.Instance.AddUser(objuser);
                    lblException.Text = Convert.ToString(err.Append("After AddUser(objuser)"));
                    //lblmsg.Visible = true;
                    //lblmsg.CssClass = "success";
                    //lblmsg.Text = "Prospect has been created successfully";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prospect has been created successfully');", true);
<<<<<<< HEAD
                    clearcontrols();
                    Session["UploadFileCountProspect"] = 0;
                    Session["ProspectAttachment"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Prospect has been created successfully');window.location ='EditInstallUser.aspx';", true);
                    //Response.Redirect("EditInstallUser.aspx");
=======
                    
                    Session["UploadFileCountProspect"] = 0;
                    Session["ProspectAttachment"] = null;
                    CheckUserType();
                  
                    if(isInstallUser)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Prospect has been created successfully');window.location ='EditInstallUser.aspx';", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sales has been created successfully');window.location ='EditUser.aspx';", true);
                    //Response.Redirect("EditInstallUser.aspx");
                    clearcontrols();
>>>>>>> refs/remotes/origin/master
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
               // lblException.Text = ex.Message + ex.StackTrace;
                lblException.Text = Convert.ToString(err.Append(ex.Message));
               // throw;
=======
                // lblException.Text = ex.Message + ex.StackTrace;
                lblException.Text = Convert.ToString(err.Append(ex.Message));
                // throw;
>>>>>>> refs/remotes/origin/master
            }
        }

        protected void No_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender2.Hide();
            ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "ClosePopup();", true);
            //RadWindow_ExisatingRecord.VisibleOnPageLoad = false;
            txtPhone.Text = "";
            txtemail.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Use diffrent Email & phone number.');", true);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "ClosePopup();", true);
                //ModalPopupExtender2.Hide();
                //RadWindow_ExisatingRecord.VisibleOnPageLoad = false;
                btn_UploadFiles_Click(sender, e);
                if (ddlPrimaryTrade.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Primary Trade');", true);
                    return;
                }
                else if (ddlPrimaryTrade.SelectedValue != "16")
                {
                    objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                }
                else if (ddlPrimaryTrade.SelectedValue == "16")
                {
                    objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                    objuser.PTradeOthers = txtOtherTrade.Text;
                }

                if (ddlSecondaryTrade.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Secondary Trade');", true);
                    return;
                }
                else if (ddlPrimaryTrade.SelectedValue != "16")
                {
                    objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                }
                else if (ddlPrimaryTrade.SelectedValue == "16")
                {
                    objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                    objuser.PTradeOthers = txtSecTradeOthers.Text;
                }
                objuser.fristname = txtfirstname.Text;
                objuser.lastname = txtlastname.Text;
                objuser.email = txtemail.Text.Trim();
                objuser.Email2 = txtemail2.Text.Trim();
                objuser.CompanyName = txtCompanyName.Text;
                objuser.phone = txtPhone.Text;
                objuser.Phone2 = txtPhone2.Text;
                objuser.Notes = txtnotes.Text;
                objuser.SourceUser = Convert.ToString(Session["userid"]);
                objuser.Source = Convert.ToString(Session["Username"]);
                DataSet ds = InstallUserBLL.Instance.CheckSource(Convert.ToString(Session["Username"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //do nothing
                }
                else
                {
                    DataSet dsadd = InstallUserBLL.Instance.AddSource(Convert.ToString(Session["Username"]));
                }
                //objprospect.str_Source = txtSource.Text;
                if (DateSourced.Text != "")
                {
                    objuser.DateSourced = DateSourced.Text;
                }
                else
                {
                    objuser.DateSourced = DateTime.Today.ToString("MM/dd/yyyy");
                }
                objuser.attachements = Convert.ToString(Session["ProspectAttachment"]);
                objuser.UserType = "Prospect";
                objuser.id = Convert.ToInt32(Session["EmailEdiId"]);
                //bool result = InstallUserBLL.Instance.UpdateInstallUser(objuser, Convert.ToInt32(Session["EmailEdiId"]));
                InstallUserBLL.Instance.UpdateProspect(objuser);

                //lblmsg.Visible = true;
                //lblmsg.CssClass = "success";
                //lblmsg.Text = "Prospect has been created successfully";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prospect has been created successfully');", true);
                clearcontrols();
                Session["UploadFileCountProspect"] = 0;
                Session["ProspectAttachment"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Prospect has been updated successfully');window.location ='EditInstallUser.aspx';", true);
            }
            catch (Exception ex)
            {

            }
        }



        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        //btn_UploadFiles_Click(sender, e);
        //        if (ddlPrimaryTrade.SelectedIndex <= 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Primary Trade');", true);
        //            return;
        //        }
        //        else
        //        {
        //            objprospect.int_PrimaryTrade = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
        //        }
        //        if (ddlSecondaryTrade.SelectedIndex <= 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Secondary Trade');", true);
        //            return;
        //        }
        //        else
        //        {
        //            objprospect.int_SecondaryTrade = Convert.ToInt16(ddlSecondaryTrade.SelectedValue);
        //        }
        //        objprospect.FirstName = txtfirstname.Text;
        //        objprospect.LastName = txtlastname.Text;
        //        objprospect.Email = txtemail.Text.Trim();
        //        objprospect.Email2 = txtemail2.Text.Trim();
        //        objprospect.CompanyName = txtCompanyName.Text;
        //        objprospect.PhoneNo = txtPhone.Text;
        //        objprospect.PhoneNo2 = txtPhone2.Text;
        //        objprospect.Notes = txtnotes.Text;
        //        objprospect.str_Source = Convert.ToString(Session["userid"]);
        //        //objprospect.str_Source = txtSource.Text;
        //        if (DateSourced.Text != "")
        //        {
        //            objprospect.DateSourced = Convert.ToDateTime(DateSourced.Text);
        //        }
        //        else
        //        {
        //            objprospect.DateSourced = Convert.ToDateTime(DateTime.Today.ToString("MM/dd/yyyy"));
        //        }
        //        objprospect.str_Attachments = Convert.ToString(Session["ProspectAttachment"]);
        //        int id = Convert.ToInt32(Session["ID"]);
        //        bool result = InstallProspectBLL.Instance.UpdateInstallUser(objprospect, id);
        //        lblmsg.Visible = true;
        //        lblmsg.CssClass = "success";
        //        lblmsg.Text = "User has been created successfully";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('InstallUser  Update successfully');", true);
        //        clearcontrols();
        //        Response.Redirect("EditInstallProspect.aspx");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        protected void btn_UploadFiles_Click(object sender, EventArgs e)
        {
            //HttpFileCollection uploads = Request.Files;
            string attach = "";
            Session["UploadFileCountProspect"] = 1;
            attach = Session["ProspectAttachment"] as string;
            //if (Session["ProspectAttachment"] != null)
            //{
            //    attach = Session["ProspectAttachment"] as string;
            //}
            //for (int fileCount = 0; fileCount < uploads.Count; fileCount++)
            //{
            //    HttpPostedFile uploadedFile = uploads[fileCount];

            //    string fileName = Path.GetFileName(uploadedFile.FileName);
            //    newAttachments.Add(uploadedFile.FileName);
            //    if (uploadedFile.ContentLength > 0)
            //    {
            //        if (!Request.Files.AllKeys[fileCount].Contains("flpUplaodPicture"))
            //        {
            //            string filename = uploadedFile.FileName;//flpUploadFiles.FileName;
            //            Server.MapPath("/UploadedFiles/" + filename);
            //            uploadedFile.SaveAs(Server.MapPath("/UploadedFiles/" + filename));

            //            //string attach = Session["attachments"] as string;
            //            if (string.IsNullOrWhiteSpace(attach))
            //            {
            //                attach = filename;

            //            }
            //            else
            //            {
            //                attach = attach + "," + filename;
            //            }
            //            Session["ProspectAttachment"] = attach;
            //            ViewState["FileName"] = attach;
            //        }
            if (attach != null)
            {
                string[] att = attach.Split(',');
                var data = att.Select(s => new { FileName = s }).ToList();
                gvUploadedFiles.DataSource = data;
                gvUploadedFiles.DataBind();
                gvUploadedFiles.Visible = true;
            }
            else
            {
                gvUploadedFiles.Visible = false;
            }
        }
        //    }

        //}

        public void displayfiles(string files)
        {
            string attach = "";
            attach = files;
            if (attach != null)
            {
                string[] att = attach.Split(',');
                var data = att.Select(s => new { FileName = s }).ToList();
                gvUploadedFiles.DataSource = data;
                gvUploadedFiles.DataBind();
                gvUploadedFiles.Visible = true;
            }
            else
            {
                gvUploadedFiles.Visible = false;
            }
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //}

        protected void gvUploadedFiles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
        private void DeleteAttachment()
        {
            if (ViewState["FilesToDelete"] != null)
            {
                string filesTodeleteString = Convert.ToString(ViewState["FilesToDelete"]);
                List<string> filesTodelete = new List<string> { filesTodeleteString };
                try
                {
                    var path = Server.MapPath("UploadedFiles\\" + filesTodeleteString);
                    //System.IO.File.Delete(path);
                    string attachmentsString = Convert.ToString(Session["ProspectAttachment"]);
                    var updatedAttachments = attachmentsString.Split(',').Except(filesTodelete).ToArray();
                    Session["ProspectAttachment"] = string.Join(",", updatedAttachments);
                }
                catch (Exception)
                {


                }
            }
        }
        private string GetUpdateAttachments()
        {
            //string attach = string.Empty;
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


        protected void txtfirstname_TextChanged(object sender, EventArgs e)
        {
            Session["FirstName"] = txtfirstname.Text;
            txtlastname.Focus();
        }
        protected void txtlastname_TextChanged(object sender, EventArgs e)
        {
            Session["LastName"] = txtlastname.Text;
            txtCompanyName.Focus();
        }
        protected void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            ViewState["CompanyName"] = txtCompanyName.Text;
            txtPhone.Focus();
        }
        protected void txtPhone_TextChanged(object sender, EventArgs e)
        {
            ViewState["Phone"] = txtPhone.Text;
            txtPhone2.Focus();
        }
        protected void txtPhone2_TextChanged(object sender, EventArgs e)
        {
            ViewState["Phone2"] = txtPhone2.Text;
            txtemail.Focus();
        }
        protected void txtemail_TextChanged(object sender, EventArgs e)
        {
            ViewState["Email"] = txtemail.Text;
            txtemail2.Focus();

        }
        protected void txtemail2_TextChanged(object sender, EventArgs e)
        {
            ViewState["Email2"] = txtemail2.Text;
            ddlPrimaryTrade.Focus();

        }
        protected void ddlPrimaryTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PrimaryTrade"] = ddlPrimaryTrade.Text;
            if (ddlPrimaryTrade.SelectedValue == "17")
            {
                txtOtherTrade.Visible = true;
            }
            else
            {
                txtOtherTrade.Visible = false;
            }
            //ddlSecondaryTrade.Focus();
        }
        protected void ddlSecondaryTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["PrimaryTrade"] = ddlPrimaryTrade.Text;
            if (ddlSecondaryTrade.SelectedValue == "17")
            {
                txtSecTradeOthers.Visible = true;
            }
            else
            {
                txtSecTradeOthers.Visible = false;
            }
            //DateSourced.Focus();
        }

        protected void AsyncFileUploadCustomerAttachment_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (Convert.ToInt32(Session["UploadFileCountProspect"]) == 0)
            {
                string fileNameall = "";
                if (Convert.ToString(Session["ProspectAttachment"]) != null)
                {
                    fileNameall = Convert.ToString(Session["ProspectAttachment"]);
                }
                string fileName = Path.GetFileName(AsyncFileUploadCustomerAttachment.FileName);
                fileName = Convert.ToString(DateTime.Now) + fileName;
                fileName = fileName.Replace("/", "");
                fileName = fileName.Replace(":", "");
                fileName = fileName.Replace(" ", "");
                AsyncFileUploadCustomerAttachment.SaveAs(Server.MapPath("~/UploadedFiles/" + fileName));
                if (Session["ProspectAttachment"] != null)
                {
                    Session["ProspectAttachment"] = null;
                    fileName = fileNameall + "," + fileName;
                    Session["ProspectAttachment"] = fileName;
                }
                else
                {
                    Session["ProspectAttachment"] = fileName;
                }
            }
            else
            {
                Session["UploadFileCountProspect"] = 0;
            }
        }
<<<<<<< HEAD
    }
=======

        //List<Designation> lstSaleuser = new List<Designation>();
        //List<Designation> lstInstalluser = new List<Designation>();
        public static List<KeyValuePair<string, string>> salesUserDesignationList = new List<KeyValuePair<string, string>>();
        public static List<KeyValuePair<string, string>> InstallUserDesignationList = new List<KeyValuePair<string, string>>();

        public void BindDesignation()
        {
            ddldesignation.DataSource = null;
            ddldesignation.DataBind();
            salesUserDesignationList = new List<KeyValuePair<string, string>>();
            InstallUserDesignationList = new List<KeyValuePair<string, string>>();

            salesUserDesignationList.Add(new KeyValuePair<string, string>("Admin", "Admin"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("Jr. Sales", "Jr. Sales"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("Jr Project Manager", "Jr Project Manager"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("Office Manager", "Office Manager"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("Recruiter", "Recruiter"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("Sales Manager", "Sales Manager"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("Sr. Sales", "Sr. Sales"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("IT - Network Admin", "ITNetworkAdmin"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("IT - Jr .Net Developer", "ITJr.NetDeveloper"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("IT - Sr .Net Developer", "ITSr.NetDeveloper"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("IT - Android Developer", "ITAndroidDeveloper"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("IT - PHP Developer", "ITPHPDeveloper"));
            salesUserDesignationList.Add(new KeyValuePair<string, string>("IT - SEO / BackLinking", "ITSEOBackLinking"));


            InstallUserDesignationList.Add(new KeyValuePair<string, string>("Installer - Helper", "InstallerHelper"));
            InstallUserDesignationList.Add(new KeyValuePair<string, string>("Installer - Journeyman", "InstallerJourneyman"));
            InstallUserDesignationList.Add(new KeyValuePair<string, string>("Installer - Mechanic", "InstallerMechanic"));
            InstallUserDesignationList.Add(new KeyValuePair<string, string>("Installer - Lead mechanic", "InstallerLeadMechanic"));
            InstallUserDesignationList.Add(new KeyValuePair<string, string>("Installer - Foreman", "InstallerForeman"));
            InstallUserDesignationList.Add(new KeyValuePair<string, string>("Commercial Only", "CommercialOnly"));
            InstallUserDesignationList.Add(new KeyValuePair<string, string>("SubContractor", "SubContractor"));

            var DesignationList = new List<KeyValuePair<string, string>>();

            foreach (var item in salesUserDesignationList)
            {
                DesignationList.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            }
            foreach (var item in InstallUserDesignationList)
            {
                DesignationList.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            }

            ddldesignation.DataSource = DesignationList;
            ddldesignation.DataTextField = "Key";
            ddldesignation.DataValueField = "Value";
            ddldesignation.DataBind();
            CheckUserType();
        }
        public static Boolean isInstallUser = false;

        protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckUserType();
        }

        public void CheckUserType()
        {
            string designation = ddldesignation.SelectedValue;
            foreach (KeyValuePair<string, string> salesItem in salesUserDesignationList)
            {
                if (salesItem.Value == designation)
                {
                    RfvPrimaryTrade.ValidationGroup = "";
                    RfvSecondaryTrade.ValidationGroup = "";
                    isInstallUser = false;
                    return;
                }
            }
            foreach (KeyValuePair<string, string> installItem in InstallUserDesignationList)
            {
                if (installItem.Value == designation)
                {
                    RfvPrimaryTrade.ValidationGroup = "submit";
                    RfvSecondaryTrade.ValidationGroup = "submit";
                    isInstallUser = true;
                    return;
                }
            }
        }

    }

    //public class Designation
    //{
    //    public string name { get; set; }
    //    public string value { get; set; }
    //}
>>>>>>> refs/remotes/origin/master
}
