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
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace JG_Prospect.Sr_App
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        user objuser = new user();
        string fn;
        List<string> newAttachments = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alsert('Your session has expired,login to contineu');window.location='../login.aspx'", true);
            }
            ////////////Previous page//////////////////
            
            /////////////////////Previous page////////////////////
            if (!IsPostBack)
            {
                chkMaddAdd.Checked = true;
                if (Request.QueryString["Toggle"] == null)
                {
                    Session["PageData"] = "";
                }
                Session["PreviousStatusNew"] = "";
                ddldesignation.SelectedValue = "SubContractor";
                CalendarExtender1.StartDate = DateTime.Today;
                Session["ddlStatus"] = "";
                grdNew.ShowFooter = true;
                txtDateSource.Attributes.Add("readonly", "readonly");
                //pnlGrid2.Visible = false;
                //pnlGrid.Visible = false;
                lblSectionHeading.Visible = false;
                btnMinusNew.Visible = false;
                btnPlusNew.Visible = true;
                Panel3.Visible = false;
                Panel4.Visible = false;
                pnl4.Visible = false;
                pnlNew.Visible = false;
                DataTable dtPersonType = GetTableForPersonType();
                Session["PersonTypeData"] = dtPersonType;
                txtOtherTrade.Visible = false;
                txtSecTradeOthers.Visible = false;
                rqDesignition.Enabled = true;
                rqPrimaryTrade.Enabled = true;
                rqSecondaryTrade.Enabled = true;
                rqFirstName.Enabled = true;
                rqEmail.Enabled = true;
                //rvEmail.Enabled = true;
                rqZip.Enabled = true;
                rqState.Enabled = true;
                lblStateReq.Visible = true;
                rqCity.Enabled = true;
                lblCityReq.Visible = true;
                password.Enabled = true;
                rqPass.Enabled = false;
                //rqMaritalStatus.Enabled = false;
                rqNotes.Enabled = false;
                lblNotesReq.Visible = false;
               // rqSource.Enabled = true;
                //lblSource.Visible = true;
                //rfLastName.Enabled = true;
                rqAddress.Enabled = true;
                lblAddressReq.Visible = true;
                rqPhone.Enabled = true;
                lblPhoneReq.Visible = true;
                rqPass.Enabled = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                rqDOB.Enabled = false;
                Session["attachments"] = "";
                Session["SkillAttachment"] = "";
                Session["ResumePath"] = "";
                Session["ResumeName"] = "";
                Session["CirtificationName"] = "";
                Session["CirtificationPath"] = "";
                Session["PersonName"] = "";
                Session["PersonType"] = "";
                rqPenalty.Enabled = false;
                //pnlAll.Visible = false;
                Session["ExtraIncomeName"] = null;
                Session["ExtraIncomeAmt"] = null;
                Session["UploadedPictureName"] = "";
                Session["PqLicenseFileName"] = "";
                Session["flpGeneralLiabilityName"] = "";
                Session["WorkersCompFileName"] = "";
                Session["attachments"] = "";
                Session["PrevDesig"] = "";
                //gvYtd.DataSource = null;
                //gvYtd.DataBind();
                //pnlFngPrint.Visible = false;
                //pnlGrid.Visible = false;
                //pnlnewHire.Visible = false;
                //pnlNew2.Visible = false;
                //btnNewPluse.Visible = true;
                //btnNewMinus.Visible = false;
                txtReson.Visible = false;
                dtInterviewDate.Visible = false;
                btnPluse.Visible = true;
                btnMinus.Visible = false;
                btnUpdate.Visible = false;
                //btn_UploadFiles.Visible = false;
                gvUploadedFiles.Visible = false;
                ddlInsteviewtime.DataSource = GetTimeIntervals();
                ddlInsteviewtime.DataBind();
                ddlInsteviewtime.Visible = false;
                btnMinusNew.Visible = true;
                btnPlusNew.Visible = false;
                Panel3.Visible = true;
                Panel4.Visible = true;
                pnlNew.Visible = true;
                //ddlInsteviewtime.DataMember = "";
                DataSet ds;
                DataSet dsTrade;
                DataSet dsSource;
                dsTrade = InstallUserBLL.Instance.getTrades();
                dsSource = InstallUserBLL.Instance.GetSource();
                lnkW9.Visible = true;
                #region Default Required Fields
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                //lblReqDesig.Visible = false;
                lblReqPtrade.Visible = true;
                rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                rqLastName.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
               // rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                rqEmail.Enabled = false;
                reEmail.Enabled = true;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                rqPass.Enabled = false;
                lblReqSig.Visible = false;
                //rqSign.Enabled = false;
                //lblReqMarSt.Visible = false;
                //rqMaritalStatus.Enabled = false;
                lblReqPicture.Visible = false;
                lblReqDL.Visible = false;
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                //lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
             //   rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
                RequiredFieldValidator7.Enabled = false;
                #endregion



                //lnkI9.Visible = true;
                //lnkEscrow.Visible = true;
                //lnkFacePage.Visible = true;
                //lnkI9.Visible = true;
                //lnkW4.Visible = false;
                if (dsSource.Tables[0].Rows.Count > 0)
                {
                    ddlSource.DataSource = dsSource.Tables[0];
                    ddlSource.DataTextField = "Source";
                    ddlSource.DataValueField = "Source";
                    ddlSource.DataBind();
                    ddlSource.Items.Insert(0, "Select Source");
                    ddlSource.SelectedIndex = 0;
                }
                else
                {
                    ddlSource.Items.Add("Select Source");
                    ddlSource.SelectedIndex = 0;
                }
                if (dsTrade.Tables.Count > 0)
                {
                    DataRow dr = dsTrade.Tables[0].NewRow();
                    dr["Id"] = "0";
                    dr["TradeName"] = "Select";
                    dsTrade.Tables[0].Rows.InsertAt(dr, 0);
                    if (dsTrade.Tables[0].Rows.Count > 0)
                    {
                        ddlPrimaryTrade.DataSource = dsTrade.Tables[0];
                        ddlPrimaryTrade.DataValueField = "Id";
                        ddlPrimaryTrade.DataTextField = "TradeName";
                        ddlPrimaryTrade.DataBind();
                        ddlPrimaryTrade.SelectedValue = "0";
                    }
                    if (dsTrade.Tables[0].Rows.Count > 0)
                    {
                        ddlSecondaryTrade.DataSource = dsTrade.Tables[0];
                        ddlSecondaryTrade.DataValueField = "Id";
                        ddlSecondaryTrade.DataTextField = "TradeName";
                        ddlSecondaryTrade.DataBind();
                        ddlSecondaryTrade.SelectedValue = "0";
                    }
                }
                if (Request.QueryString["ID"] != null)
                {
                    //LoadNewSkillUser(Convert.ToInt32(Request.QueryString["ID"]));
                    btnUpdate.Visible = true;
                    btncreate.Visible = false;
                    btnreset.Visible = false;
                    btn_UploadFiles.Visible = true;
                    gvUploadedFiles.Visible = true;
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    Session["ID"] = id;
                    ds = InstallUserBLL.Instance.getuserdetails(id);
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        ddlSource.Enabled = false;
                        txtSource.Enabled = false;
                        btnAddSource.Enabled = false;
                        btnDeleteSource.Enabled = false;
                        lblICardName.Text = ds.Tables[0].Rows[0][1].ToString() + " " + ds.Tables[0].Rows[0][2].ToString();
                        lblICardPosition.Text = ds.Tables[0].Rows[0][5].ToString();
                        lblICardIDNo.Text = ds.Tables[0].Rows[0][62].ToString();
                        Session["installId"] = ds.Tables[0].Rows[0][62].ToString();
                        Session["IdGenerated"] = ds.Tables[0].Rows[0][62].ToString();
                        GenerateBarCode(Convert.ToString(Session["installId"]));
                        txtfirstname.Text = ds.Tables[0].Rows[0][1].ToString();
                        Session["FirstName"] = ds.Tables[0].Rows[0][1].ToString();
                        txtlastname.Text = ds.Tables[0].Rows[0][2].ToString();
                        Session["LastName"] = ds.Tables[0].Rows[0][2].ToString();
                        txtemail.Text = ds.Tables[0].Rows[0][3].ToString();
                        ViewState["Email"] = ds.Tables[0].Rows[0][3].ToString();
                        txtaddress.Text = ds.Tables[0].Rows[0][4].ToString();
                        Session["Address"] = ds.Tables[0].Rows[0][4].ToString();
                        txtZip.Text = ds.Tables[0].Rows[0][11].ToString();
                        ViewState["zipEsrow"] = ds.Tables[0].Rows[0][11].ToString();
                        txtCity.Text = ds.Tables[0].Rows[0][13].ToString();
                        ViewState["City"] = ds.Tables[0].Rows[0][13].ToString();
                        txtState.Text = ds.Tables[0].Rows[0][12].ToString();
                        ViewState["State"] = ds.Tables[0].Rows[0][12].ToString();
                        string str = ((ds.Tables[0].Rows[0][13].ToString() != "") ? ds.Tables[0].Rows[0][13].ToString() : "") + ((ds.Tables[0].Rows[0][12].ToString() != "") ? ", " + ds.Tables[0].Rows[0][12].ToString() : string.Empty) + ((ds.Tables[0].Rows[0][11].ToString() != "") ? ", " + ds.Tables[0].Rows[0][11].ToString() : string.Empty);
                        Session["Zip"] = str;
                        txtpassword.Text = ds.Tables[0].Rows[0][7].ToString();
                        txtpassword1.Text = ds.Tables[0].Rows[0][7].ToString();

                        ddldesignation.SelectedValue = "SubContractor";

                       // ddldesignation.SelectedValue = ds.Tables[0].Rows[0][5].ToString();
                        if (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer")
                        {
                            lnkW9.Visible = false;
                            //lnkI9.Visible = true;
                            //lnkW4.Visible = true;
                            //lnkEscrow.Visible = false;
                            //lnkFacePage.Visible = false;
                        }
                        else
                        {
                            lnkW9.Visible = true;
                            //lnkI9.Visible = true;
                            //lnkEscrow.Visible = true;
                            //lnkFacePage.Visible = true;
                            //lnkI9.Visible = true;
                            //lnkW4.Visible = false;
                        }
                        if (ds.Tables[0].Rows[0][6].ToString() != "")
                        {
                            
                            string status = ds.Tables[0].Rows[0][6].ToString();
                            if (status == "Interview Date")
                            {
                                status = "InterviewDate";
                            }
                            if (status == "Offer Made")
                            {
                                status = "OfferMade";
                            }
                            if (status == "Phone Screened")
                            {
                                status = "PhoneScreened";
                            }
                            Session["ddlStatus"] = status;
                            ddlstatus.SelectedValue = status;
                            Session["PreviousStatusNew"] = ds.Tables[0].Rows[0][6].ToString();
                            if (ddlstatus.SelectedValue == "Applicant")
                            {

                                Label2.Visible = false;
                                rqDesignition.Enabled = false;
                                RequiredFieldValidator3.Enabled = false;
                                //lblReqDesig.Visible = false;
                                lblReqPtrade.Visible = true;
                                rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                rqLastName.Enabled = true;
                                RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                              //  rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                rqEmail.Enabled = false;
                                reEmail.Enabled = true;
                                lblReqZip.Visible = false;
                                rqZip.Enabled = false;
                                lblStateReq.Visible = false;
                                rqState.Enabled = false;
                                lblCityReq.Visible = false;
                                rqCity.Enabled = false;
                                lblPassReq.Visible = false;
                                rqPass.Enabled = false;
                                lblReqSig.Visible = false;
                                //rqSign.Enabled = false;
                                //lblReqMarSt.Visible = false;
                                //rqMaritalStatus.Enabled = false;
                                lblReqPicture.Visible = false;
                                lblReqDL.Visible = false;
                                lblAddressReq.Visible = false;
                                rqAddress.Enabled = false;
                                Label1.Visible = false;
                                RequiredFieldValidator6.Enabled = false;
                                lblConfirmPass.Visible = true;
                                rqConPass.Enabled = false;
                                lblReqSSN.Visible = false;
                                rqSSN1.Enabled = false;
                                rqSSN2.Enabled = false;
                                rqSSN3.Enabled = false;
                                //lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = false;
                                rqPenalty.Enabled = false;
                              //  rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                                lblReqDL.Visible = false;
                                lblReqPicture.Visible = false;
                                lblReqEmail.Visible = false;
                                rqEmail.Enabled = false;
                                reEmail.Enabled = false;
                                lblConfirmPass.Visible = false;
                                rqConPass.Enabled = false;
                                password.Enabled = false;
                                lblPassReq.Visible = false;
                                rqPass.Enabled = false;
                            }
                            else if (ddlstatus.SelectedValue == "OfferMade")
                            {
                                Label2.Visible = false;
                                rqDesignition.Enabled = true;
                                RequiredFieldValidator3.Enabled = true;
                                //lblReqDesig.Visible = true;
                                lblReqPtrade.Visible = true;
                                rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                rqLastName.Enabled = true;
                                RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                              //  rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                rqEmail.Enabled = false;
                                reEmail.Enabled = true;
                                lblReqZip.Visible = true;
                                rqZip.Enabled = true;
                                lblStateReq.Visible = true;
                                rqState.Enabled = true;
                                lblCityReq.Visible = true;
                                rqCity.Enabled = true;
                                lblPassReq.Visible = false;
                                rqPass.Enabled = false;
                                lblReqSig.Visible = false;
                                //rqSign.Enabled = false;
                                //lblReqMarSt.Visible = false;
                                //rqMaritalStatus.Enabled = false;
                                lblReqPicture.Visible = false;
                                lblReqDL.Visible = false;
                                Label2.Visible = false;
                                #region Req from NewHire
                                lblAddressReq.Visible = true;
                                rqAddress.Enabled = true;
                                Label1.Visible = true;
                                RequiredFieldValidator6.Enabled = true;
                                lblConfirmPass.Visible = true;
                                rqConPass.Enabled = false;
                                lblReqSSN.Visible = false;
                                rqSSN1.Enabled = false;
                                rqSSN2.Enabled = false;
                                rqSSN3.Enabled = false;
                                //lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = false;
                                rqPenalty.Enabled = false;
                             //   rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                                //lblReqHireDate.Visible = true;
                                //rqHireDate.Enabled = true;
                                //lblReqWWC.Visible = true;
                                //rqWorkCompCode.Enabled = true;
                                //lblReqPayRates.Visible = true;
                                //rqPayRate.Enabled = true;
                                //lblReqEmpType.Visible = true;
                                //rqEmpType.Enabled = true;
                                lblReqDL.Visible = false;
                                lblReqPicture.Visible = false;
                                lblReqEmail.Visible = true;
                                rqEmail.Enabled = true;
                                reEmail.Enabled = true;
                                lblConfirmPass.Visible = true;
                                rqConPass.Enabled = true;
                                password.Enabled = true;
                                lblPassReq.Visible = true;
                                rqPass.Enabled = true;
                                #endregion
                            }
                            else if (ddlstatus.SelectedValue == "Active")
                            {
                                Label2.Visible = true;
                                rqDesignition.Enabled = true;
                                RequiredFieldValidator3.Enabled = true;
                                //lblReqDesig.Visible = true;
                                lblReqPtrade.Visible = true;
                                rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                rqLastName.Enabled = true;
                                RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                           //     rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                rqEmail.Enabled = false;
                                reEmail.Enabled = true;
                                lblReqZip.Visible = true;
                                rqZip.Enabled = true;
                                lblStateReq.Visible = true;
                                rqState.Enabled = true;
                                lblCityReq.Visible = true;
                                rqCity.Enabled = true;
                                lblPassReq.Visible = true;
                                rqPass.Enabled = true;
                                lblReqSig.Visible = false;
                                lblConfirmPass.Visible = true;
                                password.Enabled = true;
                                rqConPass.Enabled = true;
                                //rqSign.Enabled = false;
                                //lblReqMarSt.Visible = true;
                                //rqMaritalStatus.Enabled = true;
                                lblReqPicture.Visible = false;
                                lblReqDL.Visible = false;

                                #region Req from NewHire
                                Label2.Visible = true;
                                lblAddressReq.Visible = true;
                                rqAddress.Enabled = true;
                                Label1.Visible = true;
                                RequiredFieldValidator6.Enabled = true;
                                lblConfirmPass.Visible = true;
                                rqConPass.Enabled = false;
                                lblReqSSN.Visible = true;
                                rqSSN1.Enabled = true;
                                rqSSN2.Enabled = true;
                                rqSSN3.Enabled = true;
                                //lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = true;
                                rqPenalty.Enabled = true;
                               // rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                                //lblReqHireDate.Visible = true;
                                //rqHireDate.Enabled = true;
                                //lblReqWWC.Visible = true;
                                //rqWorkCompCode.Enabled = true;
                                //lblReqPayRates.Visible = true;
                                //rqPayRate.Enabled = true;
                                //lblReqEmpType.Visible = true;
                                //rqEmpType.Enabled = true;
                                lblReqDL.Visible = true;
                                lblReqPicture.Visible = true;
                                lblReqEmail.Visible = true;
                                rqEmail.Enabled = true;
                                reEmail.Enabled = true;
                                lblConfirmPass.Visible = true;
                                rqConPass.Enabled = true;
                                password.Enabled = true;
                                lblPassReq.Visible = true;
                                rqPass.Enabled = true;
                                #endregion
                            }
                        }
                        if (ds.Tables[0].Rows[0][92].ToString() != "")
                        {
                            txtDateSource.Text = ds.Tables[0].Rows[0][92].ToString();
                        }
                        if (ds.Tables[0].Rows[0][38].ToString() != "")
                        {
                            ddlSource.SelectedValue = ds.Tables[0].Rows[0][38].ToString();
                        }
                        if (ds.Tables[0].Rows[0][39].ToString() != "")
                        {
                            txtNotes.Text = ds.Tables[0].Rows[0][39].ToString();
                        }
                        if (ds.Tables[0].Rows[0][40].ToString() != "" && (ds.Tables[0].Rows[0][6].ToString() != "InterviewDate" && ds.Tables[0].Rows[0][6].ToString() != "Interview Date"))
                        {
                            dtInterviewDate.Visible = false;
                            ddlInsteviewtime.Visible = false;
                            ddlInsteviewtime.Visible = false;
                            txtReson.Visible = true;
                            txtReson.Text = ds.Tables[0].Rows[0][40].ToString();
                        }
                        else if ((ds.Tables[0].Rows[0][6].ToString() == "InterviewDate" || ds.Tables[0].Rows[0][6].ToString() == "Interview Date"))
                        {
                            txtReson.Visible = false;
                            dtInterviewDate.Visible = true;
                            ddlInsteviewtime.Visible = true;
                            ddlInsteviewtime.Visible = true;
                            if (Convert.ToString(ds.Tables[0].Rows[0][105]) != "")
                            {
                                ddlInsteviewtime.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][105]);
                            }
                            dtInterviewDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0][135].ToString()).ToShortDateString();
                        }
                        else
                        {
                            txtReson.Visible = false;
                            dtInterviewDate.Visible = false;
                            ddlInsteviewtime.Visible = false;
                        }
                        txtPhone.Text = ds.Tables[0].Rows[0][8].ToString();
                        string mailAddr=Convert.ToString(ds.Tables[0].Rows[0][100]);
                        if (mailAddr == "")
                        {
                            chkMaddAdd.Checked = false;
                           
                        }
                        else
                        {
                            chkMaddAdd.Checked = true;
                            txtMailingAddress.Text = Convert.ToString(ds.Tables[0].Rows[0][100]);
                        }
                        ViewState["Phone"] = ds.Tables[0].Rows[0][8].ToString();
                        if (ds.Tables[0].Rows[0][9].ToString() != "")
                        {
                            //FillListBoxImage(ds.Tables[0].Rows[0][9].ToString());

                            // string curItem = lstboxUploadedImages.SelectedItem.ToString();
                            Image2.ImageUrl = ds.Tables[0].Rows[0][9].ToString();
                            Session["UplaodPicture"] = ds.Tables[0].Rows[0][9].ToString();
                            Session["UploadedPictureName"] = Path.GetFileName(ds.Tables[0].Rows[0][9].ToString());
                            
                        }
                        else
                        {
                            //lstboxUploadedImages.SelectedIndex = -1;
                        }
                        Session["PrevDesig"] = ds.Tables[0].Rows[0][5].ToString();
                        Session["attachments"] = ds.Tables[0].Rows[0][10].ToString();
                        lblUF.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][10].ToString());
                        
                        Session["UploadFiles"] = ds.Tables[0].Rows[0][10].ToString();
                        //FillDocument();
                        txtBusinessName.Text = ds.Tables[0].Rows[0][14].ToString();
                        ViewState["BusinessName"] = ds.Tables[0].Rows[0][14].ToString();
                        txtssn.Text = ds.Tables[0].Rows[0][15].ToString();
                        ViewState["SSN"] = ds.Tables[0].Rows[0][15].ToString();
                        txtSignature.Text = ds.Tables[0].Rows[0][18].ToString();
                        ViewState["Signature"] = ds.Tables[0].Rows[0][18].ToString();
                        DOBdatepicker.Text = ds.Tables[0].Rows[0][19].ToString();
                        ViewState["DOB"] = ds.Tables[0].Rows[0][19].ToString();
                        txtssn0.Text = ds.Tables[0].Rows[0][16].ToString();
                        ViewState["SSN1"] = ds.Tables[0].Rows[0][16].ToString();
                        txtssn1.Text = ds.Tables[0].Rows[0][17].ToString();
                        ViewState["SSN2"] = ds.Tables[0].Rows[0][17].ToString();
                        string strssn = ((ds.Tables[0].Rows[0][15].ToString() != "") ? ds.Tables[0].Rows[0][15].ToString() : "") + ((ds.Tables[0].Rows[0][16].ToString() != "") ? "- " + ds.Tables[0].Rows[0][16].ToString() : string.Empty) + ((ds.Tables[0].Rows[0][17].ToString() != "") ? "-" + ds.Tables[0].Rows[0][17].ToString() : string.Empty);
                        ViewState["ssn"] = strssn;
                        ddlcitizen.SelectedValue = ds.Tables[0].Rows[0][20].ToString();
                        ViewState["citizen"] = ds.Tables[0].Rows[0][20].ToString();
                        //txtTIN.Text = ds.Tables[0].Rows[0][21].ToString();
                        ViewState["tin"] = ds.Tables[0].Rows[0][21].ToString();
                        txtEIN.Text = ds.Tables[0].Rows[0][22].ToString();
                        ViewState["ein1"] = ds.Tables[0].Rows[0][22].ToString();
                        txtEIN2.Text = ds.Tables[0].Rows[0][23].ToString();
                        ViewState["ein2"] = ds.Tables[0].Rows[0][23].ToString();
                        string strein = ((ds.Tables[0].Rows[0][22].ToString() != "") ? ds.Tables[0].Rows[0][22].ToString() : "") + ((ds.Tables[0].Rows[0][23].ToString() != "") ? "- " + ds.Tables[0].Rows[0][23].ToString() : string.Empty); ViewState["ein"] =
                        ViewState["ein"] = strein;
                        //txtA.Text = ds.Tables[0].Rows[0][24].ToString();
                        //txtB.Text = ds.Tables[0].Rows[0][25].ToString();
                        //txtC.Text = ds.Tables[0].Rows[0][26].ToString();
                        //txtD.Text = ds.Tables[0].Rows[0][27].ToString();
                        //txtE.Text = ds.Tables[0].Rows[0][28].ToString();
                        //txtF.Text = ds.Tables[0].Rows[0][29].ToString();
                        //txtG.Text = ds.Tables[0].Rows[0][30].ToString();
                        //txtH.Text = ds.Tables[0].Rows[0][31].ToString();
                        //txt5.Text = ds.Tables[0].Rows[0][32].ToString();
                        //txt6.Text = ds.Tables[0].Rows[0][33].ToString();
                        //txt7.Text = ds.Tables[0].Rows[0][34].ToString();
                        //ddlmaritalstatus.SelectedValue = ds.Tables[0].Rows[0][35].ToString();
                        ddlPrimaryTrade.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][36]);
                        ddlSecondaryTrade.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][37]);
                        if (ds.Tables[0].Rows[0][41].ToString() != "")
                        {
                            Session["flpGeneralLiability"] = ds.Tables[0].Rows[0][41].ToString();
                            Session["flpGeneralLiabilityName"] = Path.GetFileName(ds.Tables[0].Rows[0][41].ToString());
                            //lblGL.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][41].ToString());
                        }
                        if (ds.Tables[0].Rows[0][42].ToString() != "")
                        {
                            Session["PqLicense"] = ds.Tables[0].Rows[0][42].ToString();
                            Session["PqLicenseFileName"] = Path.GetFileName(ds.Tables[0].Rows[0][42].ToString());
                            //lblPL.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][42].ToString());
                        }
                        if (ds.Tables[0].Rows[0][43].ToString() != "")
                        {
                            Session["WorkersComp"] = ds.Tables[0].Rows[0][43].ToString();
                            Session["WorkersCompFileName"] = Path.GetFileName(ds.Tables[0].Rows[0][43].ToString());
                            lblWC.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][43].ToString());
                        }
                        txtSuiteAptRoom.Text = ds.Tables[0].Rows[0][63].ToString();
                        txtFullTimePos.Text = Convert.ToString(ds.Tables[0].Rows[0][64]);
                        if (Convert.ToString(ds.Tables[0].Rows[0][77]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][77]))
                            {
                                rdoAttchmentYes.Checked = true;
                            }
                            else
                            {
                                rdoAttchmentNo.Checked = true;
                            }
                        }
                        Session["SkillAttachment"] = Path.GetFileName(Convert.ToString(ds.Tables[0].Rows[0][78]));
                        Session["SkillAttachmentName"] = Convert.ToString(ds.Tables[0].Rows[0][78]);
                        string str_ContractorBuilderOwner = Convert.ToString(ds.Tables[0].Rows[0][65]);
                        string[] str_ContractorBuilderOwnernew = str_ContractorBuilderOwner.Split('#');
                        if (str_ContractorBuilderOwnernew.Length >= 3)
                        {
                            string str_Contractor1 = str_ContractorBuilderOwnernew[0];
                            string str_Contractor2 = str_ContractorBuilderOwnernew[1];
                            string str_Contractor3 = str_ContractorBuilderOwnernew[2];
                            txtContractor1.Text = str_Contractor1;
                            txtContractor2.Text = str_Contractor2;
                            txtContractor3.Text = str_Contractor3;
                        }
                        txtMajorTools.Text = Convert.ToString(ds.Tables[0].Rows[0][66]);
                        if (Convert.ToString(ds.Tables[0].Rows[0][67]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][67]))
                            {
                                rdoDrugtestYes.Checked = true;
                            }
                            else
                            {
                                rdoDrugtestNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][68]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][68]))
                            {
                                rdoDriveLicenseYes.Checked = true;
                            }
                            else
                            {
                                rdoDriveLicenseNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][69]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][69]))
                            {
                                rdoTruckToolsYes.Checked = true;
                            }
                            else
                            {
                                rdoTruckToolsNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][70]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][70]))
                            {
                                rdoJMApplyYes.Checked = true;
                            }
                            else
                            {
                                rdoJMApplyNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][71]) != "")
                        {
                            //if (Convert.ToBoolean(ds.Tables[0].Rows[0][71]))
                            //{
                            //    rdoLicenseYes.Checked = true;
                            //}
                            //else
                            //{
                            //    rdoLicenseNo.Checked = true;
                            //}
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][72]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][72]))
                            {
                                rdoGuiltyYes.Checked = true;
                            }
                            else
                            {
                                rdoGuiltyNo.Checked = true;
                            }
                        }
                        txtStartDateNew.Text = Convert.ToString(ds.Tables[0].Rows[0][73]);
                        txtSalRequirement.Text = Convert.ToString(ds.Tables[0].Rows[0][74]);
                        txtAvailability.Text = Convert.ToString(ds.Tables[0].Rows[0][75]);
                        Session["ResumeName"] = Path.GetFileName(Convert.ToString(ds.Tables[0].Rows[0][76]));
                        Session["ResumePath"] = Convert.ToString(ds.Tables[0].Rows[0][76]);
                        //txtWarrantyPolicy.Text = Convert.ToString(ds.Tables[0].Rows[0][79]);
                        txtYrs.Text = Convert.ToString(ds.Tables[0].Rows[0][81]);
                        txtCurrentComp.Text = Convert.ToString(ds.Tables[0].Rows[0][82]);
                        txtWebsiteUrl.Text = Convert.ToString(ds.Tables[0].Rows[0][83]);
                        txtPrinciple.Text = Convert.ToString(ds.Tables[0].Rows[0][86]);
                        Session["PersonName"] = Convert.ToString(ds.Tables[0].Rows[0][84]);
                        Session["PersonType"] = Convert.ToString(ds.Tables[0].Rows[0][85]);
                        if (Convert.ToString(ds.Tables[0].Rows[0][94]) != "")
                        {
                            ddlBusinessType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][94]);
                        }
                        else
                        {
                            ddlBusinessType.SelectedValue = "0";
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][95]) != "")
                        {
                            txtCEO.Text = Convert.ToString(ds.Tables[0].Rows[0][95]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][96]) != "")
                        {
                            txtLeagalOfficer.Text = Convert.ToString(ds.Tables[0].Rows[0][96]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][97]) != "")
                        {
                            txtPresident.Text = Convert.ToString(ds.Tables[0].Rows[0][97]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][98]) != "")
                        {
                            txtSoleProprietorShip.Text = Convert.ToString(ds.Tables[0].Rows[0][98]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][99]) != "")
                        {
                            txtPartnetsName.Text = Convert.ToString(ds.Tables[0].Rows[0][99]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][100]) != "")
                        {
                            //txtMailingAddress.Text = Convert.ToString(ds.Tables[0].Rows[0][100]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][101]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][101]))
                            {
                                rdoWarrantyYes.Checked = true;
                            }
                            else 
                            {
                                rdoWarrantyNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][102]) != "")
                        {
                            txtWarentyTimeYrs.Text = Convert.ToString(ds.Tables[0].Rows[0][102]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][103]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][103]))
                            {
                                rdoBusinessMinorityYes.Checked = true;
                            }
                            else
                            {
                                rdoBusinessMinorityNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][104]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][104]))
                            {
                                rdoWomenYes.Checked = true;
                            }
                            else
                            {
                                rdoWomenNo.Checked = true;
                            }
                        }
                        if (Convert.ToString(Session["PersonName"]) != "" && Convert.ToString(Session["PersonType"]) != "")
                        {
                            GetPersonToDisplay((DataTable)(Session["PersonTypeData"]), (string)(Session["PersonName"]), (string)(Session["PersonType"]));
                        }
                        
                        //txtHireDate.Text = ds.Tables[0].Rows[0][44].ToString();
                        //dtResignation.Text = ds.Tables[0].Rows[0][45].ToString();
                        //ddlWorkerCompCode.SelectedValue = ds.Tables[0].Rows[0][46].ToString();
                        //dtReviewDate.Text = ds.Tables[0].Rows[0][47].ToString();
                        //ddlEmpType.SelectedValue = ds.Tables[0].Rows[0][48].ToString();
                        //dtLastDate.Text = ds.Tables[0].Rows[0][49].ToString();
                        //txtPayRates.Text = ds.Tables[0].Rows[0][50].ToString();
                        ////ddlExtraEarning.SelectedValue = ;
                        //Session["ExtraIncomeName"] = ds.Tables[0].Rows[0][51].ToString();
                        //Session["ExtraIncomeAmt"] = ds.Tables[0].Rows[0][52].ToString();
                        //lblExtra.Text = Convert.ToString(Session["ExtraIncomeName"]);
                        //lblDoller.Text = "$" + Convert.ToString(Session["ExtraIncomeAmt"]);
                        ////txtExtraIncome.Text =  
                        //if (ds.Tables[0].Rows[0][53].ToString() == "Check")
                        //{
                        //    rdoCheque.Checked = true;
                        //}
                        //else
                        //{
                        //    rdoDeposite.Checked = true;
                        //}
                        //txtDeduction.Text = ds.Tables[0].Rows[0][54].ToString();
                        //if (ds.Tables[0].Rows[0][55].ToString() == "One Time")
                        //{
                        //    rdoOneTime.Checked = true;
                        //}
                        //else
                        //{
                        //    rdoReoccurance.Checked = true;
                        //}
                        if (Convert.ToString(ds.Tables[0].Rows[0][133]) != "")
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0][133]))
                            {
                                chkboxcondition.Checked = true;
                                chkboxcondition.Enabled = true;
                            }
                            else
                            {
                                chkboxcondition.Checked = false;
                                chkboxcondition.Enabled = true;
                            }
                        }
                        FillDocument();
                    }
                }
                else
                {
                    //LoadNewSkillUser(0);
                    txtDateSource.Text = DateTime.Today.ToShortDateString();
                    clearcontrols();
                    lnkW9.Visible = true;
                    //lnkI9.Visible = true;
                    //lnkEscrow.Visible = true;
                    //lnkFacePage.Visible = true;
                    //lnkI9.Visible = true;
                    //lnkW4.Visible = false;
                }

                if (Request.QueryString["ID"] == null && Request.QueryString["Toggle"] != null)
                {
                    string str_Data = Convert.ToString(Session["PageData"]);
                    string[] split = str_Data.Split(',');
                    if (split[0] == "AttchmentYes")
                    {
                        rdoAttchmentYes.Checked = true;
                    }
                    else if (split[0] == "AttchmentNo")
                    {
                        rdoAttchmentNo.Checked = true;
                    }
                    txtContractor1.Text = split[1];
                    txtContractor2.Text = split[2];
                    txtContractor3.Text = split[3];
                    if (split[4] == "rdoDrugtestYes")
                    {
                        rdoDrugtestYes.Checked = true;
                    }
                    else if (split[4] == "rdoDrugtestNo")
                    {
                        rdoDrugtestNo.Checked = true;
                    }
                    if (split[5] == "rdoTruckToolsYes")
                    {
                        rdoTruckToolsYes.Checked = true;
                    }
                    else if (split[5] == "rdoTruckToolsNo")
                    {
                        rdoTruckToolsNo.Checked = true;
                    }
                    txtStartDateNew.Text = split[6];
                    txtAvailability.Text = split[7];
                    //txtPrinciple.Text = split[8];
                    txtFullTimePos.Text = split[8];
                    txtMajorTools.Text = split[9];
                    if (split[10] == "DriveLicenseYes")
                    {
                        rdoDriveLicenseYes.Checked = true;
                    }
                    else if (split[10] == "rdoDriveLicenseNo")
                    {
                        rdoDriveLicenseNo.Checked = true;
                    }
                    if (split[11] == "JMApplyYes")
                    {
                        rdoJMApplyYes.Checked = true;
                    }
                    else if (split[11] == "JMApplyNo")
                    {
                        rdoJMApplyNo.Checked = true;
                    }

                    if (split[12] == "GuiltyYes")
                    {
                        rdoGuiltyYes.Checked = true;
                    }
                    else if (split[12] == "rdoGuiltyNo")
                    {
                        rdoGuiltyNo.Checked = true;
                    }
                    txtSalRequirement.Text = split[13];
                    //ddlType.SelectedValue = split[14];
                    //txtName.Text = split[15];
                    if (split[14] == "rdoWarrantyYes")
                    {
                        rdoGuiltyYes.Checked = true;
                    }
                    else if (split[14] == "rdoWarrantyNo")
                    {
                        rdoGuiltyNo.Checked = true;
                    }
                    txtCurrentComp.Text = split[15];
                    txtWarentyTimeYrs.Text = split[16];
                    txtYrs.Text = split[17];
                    //txtCEO.Text = split[21];
                    //txtLeagalOfficer.Text = split[22];
                    //txtSoleProprietorShip.Text = split[23];
                    //txtPartnetsName.Text = split[24];
                    ddlBusinessType.SelectedValue = split[18];
                    if (split[19] == "rdoBusinessMinorityYes")
                    {
                        rdoBusinessMinorityYes.Checked = true;
                    }
                    else if (split[19] == "rdoBusinessMinorityNo")
                    {
                        rdoBusinessMinorityNo.Checked = true;
                    }
                    if (split[20] == "rdoWomenYes")
                    {
                        rdoWomenYes.Checked = true;
                    }
                    else if (split[20] == "rdoWomenNo")
                    {
                        rdoWomenNo.Checked = true;
                    }
                    txtWebsiteUrl.Text = split[21];
                    ddldesignation.SelectedValue = split[22];
                    ddlPrimaryTrade.SelectedValue = split[23];
                    txtOtherTrade.Text = split[24];
                    ddlSecondaryTrade.SelectedValue = split[25];
                    txtSecTradeOthers.Text = split[26];
                    txtfirstname.Text = split[27];
                    txtemail.Text = split[28];
                    txtZip.Text = split[29];
                    txtState.Text = split[30];
                    txtCity.Text = split[31];
                    txtpassword.Text = split[32];
                    txtSignature.Text = split[33];
                    ddlstatus.SelectedValue = split[34];
                    txtReson.Text = split[35];
                    dtInterviewDate.Text = split[36];
                    txtDateSource.Text = split[37];
                    txtNotes.Text = split[38];
                    txtlastname.Text = split[39];
                    ddlSource.SelectedValue = split[40];
                    txtSource.Text = split[41];
                    txtaddress.Text = split[42];
                    txtMailingAddress.Text = split[43];
                    txtSuiteAptRoom.Text = split[44];
                    txtpassword1.Text = split[45];
                    txtPhone.Text = split[46];
                    txtssn.Text = split[47];
                    txtssn0.Text = split[48];
                    txtssn1.Text = split[49];
                    DOBdatepicker.Text = split[50];
                    ddlcitizen.SelectedValue = split[51];
                }
            }
        }

        private void LoadNewSkillUser(int i)
        {
            DataSet dsTemp = new DataSet();
            dsTemp = InstallUserBLL.Instance.GetSkillUser(Convert.ToString(Session["Username"]),"0");
            grdNew.DataSource = dsTemp.Tables[0];
            grdNew.DataBind();
        }

        protected void AddNewCustomer(object sender, EventArgs e)
        {
            string Name = ((TextBox)grdNew.FooterRow.FindControl("txtEnterName")).Text;
            string Title = ((TextBox)grdNew.FooterRow.FindControl("txtEnterType")).Text;
            string PerOwnership = ((TextBox)grdNew.FooterRow.FindControl("txtEnterOwnerPer")).Text;
            string Phone = ((TextBox)grdNew.FooterRow.FindControl("txtEnterPhone")).Text;
            string EMail = ((TextBox)grdNew.FooterRow.FindControl("txtEnterEmail")).Text;
            string Address = ((TextBox)grdNew.FooterRow.FindControl("txtEnterAddress")).Text;
            string userId = Convert.ToString(Session["Username"]);
            DataSet dsTemp = new DataSet();
            dsTemp = InstallUserBLL.Instance.AddSkillUser(Name, Title, PerOwnership, Phone, EMail, Address, userId);
            grdNew.DataSource = dsTemp.Tables[0];
            grdNew.DataBind();
        }

        protected void DeleteCustomer(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
        }
        protected void EditCustomer(object sender, GridViewEditEventArgs e)
        {
            grdNew.EditIndex = e.NewEditIndex;
            //LoadNewSkillUser();
        }
        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdNew.EditIndex = -1;
            //LoadNewSkillUser();
        }
        protected void UpdateCustomer(object sender, GridViewUpdateEventArgs e)
        {
            //string CustomerID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblCustomerID")).Text;
            //string Name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtContactName")).Text;
            //string Company = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCompany")).Text;
            //SqlConnection con = new SqlConnection(strConnString);
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "update customers set ContactName=@ContactName,CompanyName=@CompanyName " +
            // "where CustomerID=@CustomerID;" +
            // "select CustomerID,ContactName,CompanyName from customers";
            //cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar).Value = CustomerID;
            //cmd.Parameters.Add("@ContactName", SqlDbType.VarChar).Value = Name;
            //cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Company;
            //GridView1.EditIndex = -1;
            //GridView1.DataSource = GetData(cmd);
            //GridView1.DataBind();
        }


        public List<string> GetTimeIntervals()
        {
            List<string> timeIntervals = new List<string>();
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
            for (int i = 0; i < 48; i++)
            {
                int minutesToBeAdded = 30 * i;      // Increasing minutes by 30 minutes interval
                TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
                TimeSpan t = startTime.Add(timeToBeAdded);
                DateTime result = startDate + t;
                timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
            }
            return timeIntervals;
        }



        private void GetPersonToDisplay(DataTable dt, string PersonName, string PersonType)
        {
            DataRow drNew = dt.NewRow();
            Session["loop4"] = "";
            Session["loop5"] = "";
            string[] str_PersonName = PersonName.Split(',');
            string[] str_PersonType = PersonType.Split(',');
        label:
            drNew = dt.NewRow();
            for (int i = 0; i < str_PersonName.Length; i++)
            {
                if (Convert.ToString(Session["loop4"]) != "")
                {
                    i = Convert.ToInt32(Session["loop4"]) + 1;
                }
                if (str_PersonName[i] != "")
                {
                    drNew["PersonName"] = str_PersonName[i];
                    Session["loop4"] = i;
                    break;
                }
            }
            for (int k = 0; k < str_PersonType.Length; k++)
            {
                if (Convert.ToString(Session["loop5"]) != "")
                {
                    k = Convert.ToInt32(Session["loop5"]) + 1;
                }
                if (str_PersonType[k] != "")
                {
                    drNew["PersonType"] = str_PersonType[k];
                    Session["loop5"] = k;
                    if (k == str_PersonType.Length - 1)
                    {
                        dt.Rows.Add(drNew);
                        goto label1;
                    }
                    break;
                }
            }
            dt.Rows.Add(drNew);
            goto label;
        label1:
            Session["PersonTypeData"] = null;
            Session["PersonTypeData"] = dt;
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }

        private DataTable GetTableForPersonType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PersonName", typeof(String));
            table.Columns.Add("PersonType", typeof(string));
            return table;
        }

        private void FillDocument()
        {
            string attach = Session["attachments"] as string;
            if (Convert.ToString(Session["ResumeName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["ResumeName"]);
            }
            else if (Convert.ToString(Session["ResumeName"]) != "")
            {
                attach = Convert.ToString(Session["ResumeName"]);
            }
            if (Convert.ToString(Session["CirtificationName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["CirtificationName"]);
            }
            else if (Convert.ToString(Session["CirtificationName"]) != "")
            {
                attach = Convert.ToString(Session["CirtificationName"]);
            }
            if (Convert.ToString(Session["SkillAttachmentName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["SkillAttachmentName"]);
            }
            else if (Convert.ToString(Session["SkillAttachmentName"]) != "")
            {
                attach = Convert.ToString(Session["SkillAttachmentName"]);
            }
            if (Convert.ToString(Session["UploadedPictureName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["UploadedPictureName"]);
            }
            else if (Convert.ToString(Session["UploadedPictureName"]) != "")
            {
                attach = Convert.ToString(Session["UploadedPictureName"]);
            }
            if (Convert.ToString(Session["PqLicenseFileName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["PqLicenseFileName"]);
            }
            else if (Convert.ToString(Session["PqLicenseFileName"]) != "")
            {
                attach = Convert.ToString(Session["PqLicenseFileName"]);
            }
            if (Convert.ToString(Session["flpGeneralLiabilityName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["flpGeneralLiabilityName"]);
            }
            else if (Convert.ToString(Session["flpGeneralLiabilityName"]) != "")
            {
                attach = Convert.ToString(Session["flpGeneralLiabilityName"]);
            }
            if (Convert.ToString(Session["WorkersCompFileName"]) != "" && attach != null && attach != "")
            {
                attach = attach + "," + Convert.ToString(Session["WorkersCompFileName"]);
            }
            else if (Convert.ToString(Session["WorkersCompFileName"]) != "")
            {
                attach = Convert.ToString(Session["WorkersCompFileName"]);
            }
            if (attach != "")
            {
                string[] att = attach.Split(',');
                var data = att.Select(s => new { FileName = s }).ToList();
                if (string.IsNullOrWhiteSpace(attach))
                {
                    data.Clear();
                }
                gvUploadedFiles.DataSource = data;
                gvUploadedFiles.DataBind();
                gvUploadedFiles.Visible = true;
            }
            else
            {
                //DataTable data = null;
                //gvUploadedFiles.DataSource = data;
                //gvUploadedFiles.DataBind();
                gvUploadedFiles.Visible = false;
            }
        }
        private void FillListBoxImage(string imageName)
        {
            string[] arr = imageName.Split(',');
            if (flpUplaodPicture != null)
            {
                foreach (string img in arr)
                {
                    //  lstboxUploadedImages.Items.Add(img);
                }
                //lstboxUploadedImages.SelectedIndex = 0;
            }
            else
            {
                foreach (string img in arr)
                {
                    //  lstboxUploadedImages.SelectedIndex =-2;
                }

            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            clearcontrols();
            //lblmsg.Visible = false;
            Response.Redirect("../Sr_App/InstallCreateUser2.aspx");
        }

        private void clearcontrols()
        {
            ddlcitizen.SelectedValue = "0";
            ddldesignation.SelectedValue = "SubContractor";
            ddlstatus.SelectedValue = "Applicant";
            txtfirstname.Text = txtlastname.Text = txtemail.Text = txtpassword.Text = txtpassword1.Text = txtPhone.Text = txtZip.Text = txtState.Text = txtCity.Text = txtaddress.Text = null;
            gvUploadedFiles.Visible = false;
            // lstboxUploadedImages.Items.Clear();
            Image2.Visible = false;
            //lnkEscrow.Visible = lnkFacePage.Visible = lnkI9.Visible = lnkW4.Visible = lnkW9.Visible = false;
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
                Session["Zip"] = str.ToString();
                ViewState["zipEsrow"] = txtZip.Text;
            }
        }

        protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer")
            {
                lnkW9.Visible = false;
                //lnkI9.Visible = true;
                //lnkW4.Visible = true;
                //lnkEscrow.Visible = false;
                //lnkFacePage.Visible = false;
            }
            else
            {
                lnkW9.Visible = true;
                //lnkI9.Visible = true;
                //lnkEscrow.Visible = true;
                //lnkFacePage.Visible = true;
                //lnkI9.Visible = true;
                //lnkW4.Visible = false;
            }
            ViewState["pass"] = txtpassword.Text;
            //if (ddlstatus.SelectedValue == "SubContractor")
            //{
            //    pnl4.Visible = true;
            //}
            //else
            //{
            //    pnl4.Visible = false;
            //}
        }
        //Save User......
        protected void btncreate_Click(object sender, EventArgs e)
        {
            string InstallId = string.Empty;
            string str_Status = string.Empty;
            string str_Reason = string.Empty;
            if (txtNotes.Text =="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Notes');", true);
                return;
            }
            //if (ddldesignation.SelectedValue == "Applicant")
            //{
            //    if (ddlSource.SelectedIndex == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Source');", true);
            //        return;
            //    }
            //}
            
            if ((Convert.ToString(Session["UploadedPictureName"]) == "" || Convert.ToString(Session["UploadedPictureName"]) == null) && ddlstatus.SelectedValue == "Active")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select image file');", true);
                return;
            }
            if ((Convert.ToString(Session["PqLicense"]) == "" || Convert.ToString(Session["PqLicense"]) == null) && ddlstatus.SelectedValue == "Active")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select DL License file');", true);
                return;
            }
            btn_UploadFiles_Click(sender, e);
            if (chkboxcondition.Checked != true && ddldesignation.SelectedValue == "Active")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Term and Conditions');", true);
                return;
            }
            else 
            {
                if (Convert.ToString(Session["IdGenerated"]) == "")
                {
                    GetId(ddldesignation.SelectedValue, ddlstatus.SelectedValue);
                }
                InstallId = Convert.ToString(Session["IdGenerated"]);
                objuser.SourceUser = Convert.ToString(Session["userid"]);
                objuser.DateSourced = DateTime.Today.ToShortDateString();
                //if (ddlstatus.SelectedIndex <= 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Select Status');", true);
                //    return;
                //}
                //else
                //{
                    objuser.status = ddlstatus.SelectedValue;
                //}
                objuser.InstallId = InstallId;
                objuser.fristname = txtfirstname.Text;
                objuser.lastname = txtlastname.Text;
                objuser.email = txtemail.Text.Trim();
                objuser.address = txtaddress.Text;
                objuser.zip = txtZip.Text;
                objuser.state = txtState.Text;
                objuser.city = txtCity.Text;
                objuser.password = txtpassword.Text;
                ViewState["pass"] = txtpassword.Text;
                objuser.designation = ddldesignation.SelectedItem.Text;
                objuser.phone = txtPhone.Text;
                //if (flpUplaodPicture.FileName != string.Empty)
                //{
                // foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                //     {
                //         fn = fn + "," + img.Text;
                //     }
                //Server.MapPath("~/CustomerDocs/LocationPics/" + flpUplaodPicture.FileName);
                //objuser.picture = fn.TrimStart(',');
                objuser.picture = Convert.ToString(Session["UplaodPicture"]);
                //}
                //  else if (lstboxUploadedImages.Items.Count > 0)
                // {
                //  foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                //    {
                //         fn = fn + "," + img.Text;
                //     }
                //     objuser.picture = fn.TrimStart(',');
                // }
                //else
                //{
                //    objuser.picture = string.Empty;
                //}

                string strFileName = string.Empty;

                //if (ViewState["FileName"] != null)
                //{
                //    strFileName = ViewState["FileName"].ToString();
                //    strFileName = strFileName.TrimStart(',');
                objuser.attachements = Convert.ToString(Session["attachments"]);
                //}
                //else
                //{
                //    objuser.attachements = strFileName;
                //}
                objuser.businessname = txtBusinessName.Text;
                objuser.ssn = txtssn.Text;
                objuser.ssn1 = txtssn0.Text;
                objuser.ssn2 = txtssn1.Text;
                string ssn = txtssn.Text + "-" + txtssn0.Text + "-" + txtssn1.Text;
                ViewState[ssn] = ssn;
                objuser.signature = txtSignature.Text;
                objuser.dob = DOBdatepicker.Text;
                objuser.citizenship = ddlcitizen.SelectedValue;
                //objuser.tin = txtTIN.Text;
                objuser.ein1 = txtEIN.Text;
                objuser.ein2 = txtEIN2.Text;
                objuser.DateSourced = DateTime.Today.ToShortDateString();
                //objuser.a = txtA.Text;
                //objuser.b = txtB.Text;
                //objuser.c = txtC.Text;
                //objuser.d = txtD.Text;
                //objuser.e = txtE.Text;
                //objuser.f = txtF.Text;
                //objuser.g = txtG.Text;
                //objuser.h = txtH.Text;
                //objuser.i = txt5.Text;
                //objuser.j = txt6.Text;
                //objuser.k = txt7.Text;
                //objuser.maritalstatus = ddlmaritalstatus.SelectedValue;
                str_Status = ddlstatus.SelectedItem.Text;
                objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                objuser.Source = ddlSource.SelectedValue;
                objuser.Notes = txtNotes.Text;

                //For Event Added by.....ID
                objuser.AddedBy = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);

                if (txtReson.Visible == true && ddlstatus.SelectedValue == "Rejected")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = txtReson.Text;
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else if (dtInterviewDate.Visible == true && ddlstatus.SelectedValue == "InterviewDate")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = Convert.ToString(dtInterviewDate.Text);
                }
                else if (ddlstatus.SelectedValue == "Active")
                {
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else if (txtReson.Visible == true && ddlstatus.SelectedValue == "Deactive")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = txtReson.Text;
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else if (txtReson.Visible == true && ddlstatus.SelectedValue == "OfferMade")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = txtReson.Text;
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else
                {
                    str_Reason = "";
                    objuser.Reason = "";
                }
                objuser.PqLicense = Convert.ToString(Session["PqLicense"]);
                objuser.WorkersComp = Convert.ToString(Session["WorkersComp"]);
                objuser.GeneralLiability = Convert.ToString(Session["flpGeneralLiability"]);
                objuser.HireDate = "";
                objuser.TerminitionDate = "";
                objuser.WorkersCompCode = Convert.ToString("");
                objuser.NextReviewDate = "";
                objuser.EmpType = "";
                objuser.LastReviewDate = "";
                objuser.PayRates = "";
                objuser.ExtraEarning = Convert.ToString(0);
               // objuser.ExtraEarningAmt = Convert.ToString(0);
                //if (rdoCheque.Checked)
                //{
                //    objuser.PayMethod = "Check";
                //}
                //else
                //{
                    objuser.PayMethod = "";
                //}
                objuser.Deduction = "0";
                //if (rdoOneTime.Checked)
                //{
                //    objuser.DeductionType = "One Time";
                //}
                //else
                //{
                    objuser.DeductionType = "";
                //}
                objuser.AbaAccountNo = "";
                objuser.AccountNo = "";
                objuser.AccountType = "";
                objuser.PTradeOthers = txtOtherTrade.Text;
                objuser.STradeOthers = txtSecTradeOthers.Text;
                objuser.str_SuiteAptRoom = txtSuiteAptRoom.Text;
                if (rdoAttchmentYes.Checked)
                {
                    objuser.skillassessmentstatus = true;
                }
                else if (rdoAttchmentNo.Checked)
                {
                    objuser.skillassessmentstatus = false;
                }
                objuser.assessmentPath = Convert.ToString(Session["SkillAttachment"]);
                if (txtFullTimePos.Text != "")
                {
                    objuser.FullTimePosition = Convert.ToInt32(txtFullTimePos.Text);
                }
                else
                {
                    objuser.FullTimePosition = 0;
                }
                objuser.ContractorsBuilderOwner = txtContractor1.Text + "#" + txtContractor2.Text + "#" + txtContractor3.Text;
                objuser.MajorTools = txtMajorTools.Text;
                if (rdoDrugtestYes.Checked)
                {
                    objuser.DrugTest = true;
                }
                else if (rdoDrugtestNo.Checked)
                {
                    objuser.DrugTest = false;
                }
                if (rdoDriveLicenseYes.Checked)
                {
                    objuser.ValidLicense = true;
                }
                else if (rdoDriveLicenseNo.Checked)
                {
                    objuser.ValidLicense = false;
                }
                if (rdoTruckToolsYes.Checked)
                {
                    objuser.TruckTools = true;
                }
                else if (rdoTruckToolsNo.Checked)
                {
                    objuser.TruckTools = false;
                }
                if (rdoJMApplyYes.Checked)
                {
                    objuser.PrevApply = true;
                }
                else if (rdoJMApplyYes.Checked)
                {
                    objuser.PrevApply = false;
                }
                //if (rdoLicenseYes.Checked)
                //{
                //    objuser.LicenseStatus = true;
                //}
                //else if (rdoLicenseNo.Checked)
                //{
                //    objuser.LicenseStatus = false;
                //}
                if (rdoGuiltyYes.Checked)
                {
                    objuser.CrimeStatus = true;
                }
                else if (rdoGuiltyNo.Checked)
                {
                    objuser.CrimeStatus = false;
                }
                objuser.StartDate = Convert.ToString(txtStartDateNew.Text);
                objuser.SalaryReq = txtSalRequirement.Text;
                objuser.Avialability = txtAvailability.Text;
                //objuser.WarrentyPolicy = txtWarrantyPolicy.Text;
                if (txtYrs.Text != "")
                {
                    objuser.businessYrs = Convert.ToDouble(txtYrs.Text);
                }
                if (txtCurrentComp.Text != "")
                {
                    objuser.underPresentComp = Convert.ToDouble(txtCurrentComp.Text);
                }
                objuser.websiteaddress = txtWebsiteUrl.Text;
                objuser.ResumePath = Convert.ToString(Session["ResumePath"]);
                objuser.CirtificationTraining = Convert.ToString(Session["CirtificationPath"]);
                if (Convert.ToString(Session["PersonName"]) != "")
                {
                    objuser.PersonName = Convert.ToString(Session["PersonName"]);
                }
                else
                {
                    objuser.PersonName = txtName.Text;
                }
                if (Convert.ToString(Session["PersonType"]) != "")
                {
                    objuser.PersonType = Convert.ToString(Session["PersonType"]);
                }
                else if (txtName.Text != "")
                {
                    if (ddlType.SelectedIndex != 0)
                    {
                        objuser.PersonType = ddlType.SelectedValue;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select person type')", true);
                        ddlType.Focus();
                        return;
                    }
                }
                objuser.CompanyPrinciple = txtPrinciple.Text;
                objuser.BusinessType = ddlBusinessType.SelectedValue;
                objuser.CEO = txtCEO.Text;
                objuser.LegalOfficer = txtLeagalOfficer.Text;
                objuser.President = txtPresident.Text;
                objuser.Owner =txtSoleProprietorShip.Text;
                objuser.AllParteners = txtPartnetsName.Text;
                //objuser.MailingAddress = txtMailingAddress.Text;
                if (rdoWarrantyYes.Checked)
                    objuser.Warrantyguarantee = true;
                else if (rdoWarrantyNo.Checked)
                    objuser.Warrantyguarantee = false;
                if (txtWarentyTimeYrs.Text != "")
                {
                    objuser.WarrantyYrs = Convert.ToInt32(txtWarentyTimeYrs.Text);
                }
                if (rdoBusinessMinorityYes.Checked)
                    objuser.MinorityBussiness = true;
                else if (rdoBusinessMinorityNo.Checked)
                    objuser.MinorityBussiness = false;
                if (rdoWomenYes.Checked)
                    objuser.WomensEnterprise = true;
                else if (rdoWomenNo.Checked)
                    objuser.WomensEnterprise = false;
                if (ddlstatus.SelectedValue == "InterviewDate")
                {
                    objuser.InterviewTime = ddlInsteviewtime.SelectedItem.Text;
                }
                else
                {
                    objuser.InterviewTime = "";
                }
                if (ddlstatus.SelectedValue == "Active")
                {
                    objuser.ActivationDate = DateTime.Today.ToShortDateString();
                    objuser.UserActivated = Convert.ToString(Session["Username"]);
                }
                else
                {
                    objuser.ActivationDate = "";
                    objuser.UserActivated = "";
                }
                objuser.SourceUser = Convert.ToString(Session["userid"]);
                objuser.MailingAddress = txtMailingAddress.Text;
                if (chkboxcondition.Checked)
                {
                    objuser.TC = true;
                }
                else
                {
                    objuser.TC = false;
                }
                DataSet dsCheckDuplicate = InstallUserBLL.Instance.CheckInstallUser(txtemail.Text, txtPhone.Text);
                if (dsCheckDuplicate.Tables[0].Rows.Count > 0)
                {
                    Session["EmailEdiId"] = Convert.ToInt32(dsCheckDuplicate.Tables[0].Rows[0][0]);
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
                    bool result = InstallUserBLL.Instance.AddUser(objuser);
                    GoogleCalendarEvent.CreateCalendar(txtemail.Text, txtaddress.Text);
                    //if (ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "OfferMade")
                    //{
                    //    SendEmail(txtemail.Text, txtfirstname.Text, txtlastname.Text, ddlstatus.SelectedValue, str_Reason);
                    //}
                    //lblmsg.Visible = true;
                    //lblmsg.CssClass = "success";
                    //lblmsg.Text = "User has been created successfully";
                    if (ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "OfferMade" || ddlstatus.SelectedValue == "Deactive")
                    {
                        SendEmail(txtemail.Text, txtfirstname.Text, txtlastname.Text, ddlstatus.SelectedValue, str_Reason);
                    }
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User has been created successfully');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User has been created successfully');window.location ='EditInstallUser.aspx';", true);
                    clearcontrols();
                    //Server.Transfer("EditInstallUser.aspx");
                    Session["installId"] = "";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User has been created successfully.');window.location ='EditInstallUser.aspx';", true);
                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Term and Conditions');", true);
            //}
            
        }

        private string GetUpdatedId(string PrevId)
        {
            string LastInt = "";
            DataTable dtId;
            int IndexA = 0;
            int IndexS = 0;
            string previouPrefix = string.Empty;
            string PrefixId = string.Empty;
            string SalesId = PrevId;
            string PrevDesig = string.Empty;
            string newId = string.Empty;
            if (ddldesignation.SelectedValue == "ForeMan" && ddlstatus.SelectedValue != "Deactive")
            {
                if (SalesId != "")
                {
                    if (SalesId.Contains("FRM"))
                    {
                        IndexS = SalesId.IndexOf("FRM");
                        PrefixId = SalesId.Substring(IndexS, 1);
                        previouPrefix = SalesId.Substring(0, IndexS);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexS + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "FRM000" + newId;
                    }
                    else if (SalesId.Contains("SUB"))
                    {
                        IndexA = SalesId.IndexOf("SUB");
                        PrefixId = SalesId.Substring(IndexA, 1);
                        previouPrefix = SalesId.Substring(0, IndexA);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexA + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "FRM000" + newId;
                    }
                    else if (SalesId.Contains("INS"))
                    {
                        IndexA = SalesId.IndexOf("INS");
                        PrefixId = SalesId.Substring(IndexA, 1);
                        previouPrefix = SalesId.Substring(0, IndexA);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexA + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "FRM000" + newId;
                    }
                }
                else
                {
                    SalesId = "FRM0001";
                }
            }
            else if ((ddldesignation.SelectedValue == "ForeMan") && (ddlstatus.SelectedValue == "Deactive") && (SalesId != ""))
            {
                SalesId = SalesId + "-X";
            }
            else if (ddldesignation.SelectedValue == "Installer" && ddlstatus.SelectedValue != "Deactive")
            {
                if (SalesId != "")
                {
                    if (SalesId.Contains("INS"))
                    {
                        IndexS = SalesId.IndexOf("INS");
                        PrefixId = SalesId.Substring(IndexS, 1);
                        previouPrefix = SalesId.Substring(0, IndexS);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexS + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "INS000" + newId;
                    }
                    else if (SalesId.Contains("FRM"))
                    {
                        IndexA = SalesId.IndexOf("FRM");
                        PrefixId = SalesId.Substring(IndexA, 1);
                        previouPrefix = SalesId.Substring(0, IndexA);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexA + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "INS000" + newId;
                    }
                    else if (SalesId.Contains("SUB"))
                    {
                        IndexA = SalesId.IndexOf("SUB");
                        PrefixId = SalesId.Substring(IndexA, 1);
                        previouPrefix = SalesId.Substring(0, IndexA);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexA + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "INS000" + newId;
                    }
                }
                else
                {
                    SalesId = "INS0001";
                }
            }
            else if ((ddldesignation.SelectedValue == "Installer" && ddlstatus.SelectedValue == "Deactive") && (SalesId != ""))
            {
                SalesId = SalesId + "-X";
            }
            else if (ddldesignation.SelectedValue == "SubContractor" && ddlstatus.SelectedValue != "Deactive")
            {
                if (SalesId != "")
                {
                    if (SalesId.Contains("INS"))
                    {
                        IndexS = SalesId.IndexOf("INS");
                        PrefixId = SalesId.Substring(IndexS, 1);
                        previouPrefix = SalesId.Substring(0, IndexS);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexS + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "SUB000" + newId;
                    }
                    else if (SalesId.Contains("FRM"))
                    {
                        IndexA = SalesId.IndexOf("FRM");
                        PrefixId = SalesId.Substring(IndexA, 1);
                        previouPrefix = SalesId.Substring(0, IndexA);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexA + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "SUB000" + newId;
                    }
                    else if (SalesId.Contains("SUB"))
                    {
                        IndexA = SalesId.IndexOf("SUB");
                        PrefixId = SalesId.Substring(IndexA, 1);
                        previouPrefix = SalesId.Substring(0, IndexA);
                        PrefixId = previouPrefix + PrefixId;
                        newId = SalesId.Substring(IndexA + (SalesId.Length - 4));
                        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                        PrefixId = PrefixId + "SUB000" + newId;
                    }
                }
                else
                {
                    SalesId = "SUB0001";
                }
            }
            else if ((ddldesignation.SelectedValue == "Installer" && ddlstatus.SelectedValue == "Deactive") && (SalesId != ""))
            {
                SalesId = SalesId + "-X";
            }


            return SalesId;
        }


        private string GetId(string UserType, string UserStatus)
        {
            DataTable dtId;
            string LastInt = string.Empty;
            string installId = string.Empty;

            dtId = InstallUserBLL.Instance.getMaxId(UserType, UserStatus);
            if (dtId.Rows.Count > 0)
            {
                installId = Convert.ToString(dtId.Rows[0][0]);
            }
            if ((UserType == "ForeMan") && (UserStatus != "Deactive"))
            {
                if (installId != "")
                {
                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "FRM000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);
                }
                else
                {
                    installId = "FRM0001";
                }
            }
            else if (UserType == "ForeMan" && UserStatus == "Deactive")
            {
                installId = installId + "-X";
            }
            if (UserType == "Installer" && UserStatus != "Deactive")
            {
                if (installId != "")
                {

                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "INS000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);
                }
                else
                {
                    installId = "INS0001";
                }
            }
            else if (UserType == "Installer" && UserStatus == "Deactive")
            {
                installId = installId + "-X";
            }


            if ((UserType == "SubContractor") && (UserStatus != "Deactive"))
            {
                if (installId != "")
                {
                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "SUB000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);
                }
                else
                {
                    installId = "SUB0001";
                }
            }
            else if (UserType == "SubContractor" && UserStatus == "Deactive")
            {
                installId = installId + "-X";
            }
            Session["installId"] = installId;
            Session["IdGenerated"] = installId;
            return installId;
        }

        //private void SendEmail(string emailId, string FName, string LName, string status, string Reason)
        //{
        //    try
        //    {
        //        MailMessage Msg = new MailMessage();
        //        // Sender e-mail address.
        //        Msg.From = new MailAddress("qat2015team@gmail.com");
        //        // Recipient e-mail address.
        //        Msg.To.Add(emailId);
        //        Msg.Subject = "JG Prospect Notification";
        //        StringBuilder Body = new StringBuilder();
        //        Body.Append("Hello " + FName + " " + LName + ",");
        //        Body.Append("<br>");
        //        Body.Append("Your stattus for the JG Prospect is :" + status);
        //        Body.Append("<br>");
        //        //if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
        //        //{
        //        Body.Append(Reason);
        //        //}
        //        Body.Append("<br>");
        //        Body.Append("Tanking you");
        //        Msg.Body = Convert.ToString(Body);
        //        // your remote SMTP server IP.
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.Credentials = new System.Net.NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        Msg = null;
        //        //Page.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='SendMail.aspx';}</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("{0} Exception caught.", ex);
        //    }
        //    // //SmtpClient smtp = new SmtpClient();
        //    // //MailMessage email_msg = new MailMessage();
        //    // //email_msg.To.Add(emailId);
        //    // //email_msg.From = new MailAddress("customsoft.test@gmail.com", "Credit Chex");
        //    // StringBuilder Body = new StringBuilder();
        //    // Body.Append("Hello " + FName + " " + LName + ",");
        //    // Body.Append("<br>");
        //    // Body.Append("Your stattus for the JG Prospect is :" + status);
        //    // Body.Append("<br>");
        //    // //if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
        //    // //{
        //    //     Body.Append(Reason);
        //    // //}
        //    // Body.Append("<br>");
        //    // Body.Append("Tanking you");
        //    //// AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
        //    //// LinkedResource imagelink3 = new LinkedResource(HttpContext.Current.Server.MapPath("~/images/logo.png"), "image/png");
        //    // //imagelink3.ContentId = "imageId1";
        //    // //imagelink3.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
        //    // //htmlView.LinkedResources.Add(imagelink3);
        //    // var smtp = new System.Net.Mail.SmtpClient();
        //    // {
        //    //     smtp.Host = "smtp.gmail.com";
        //    //     smtp.Port = 587;
        //    //     smtp.EnableSsl = true;
        //    //     smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //    //     smtp.Credentials = new NetworkCredential("","q$7@wt%j*j*65ba#3M@9P6");
        //    //     smtp.Timeout = 20000;
        //    // }
        //    // // Passing values to smtp object
        //    // smtp.Send("", emailId, "JG Prospect Notification", Convert.ToString(Body));

        //}

        private void SendEmail(string emailId, string FName, string LName, string status, string Reason)
        {
            try
            {
                string HTML_TAG_PATTERN = "<.*?>";
                string strHeader = GetEmailHeader(status);
                string strBody = GetEmailBody(status);
                strBody = strBody.Replace("FirstName", FName);
                strBody = strBody.Replace("LastName", LName);
                strBody = strBody.Replace("Reason", Reason);
                string strFooter = GetFooter(status);
                //strBody = Regex.Replace(strBody, HTML_TAG_PATTERN, string.Empty);
                //strHeader = Regex.Replace(strHeader, HTML_TAG_PATTERN, string.Empty);
                //strFooter = Regex.Replace(strFooter, HTML_TAG_PATTERN, string.Empty);
                MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                //Msg.From = new MailAddress("qat2015team@gmail.com");
                // Recipient e-mail address.
                //Msg.To.Add(emailId);
                //Msg.Subject = "JG Prospect Notification";
                StringBuilder Body = new StringBuilder();
                //Body.Append("Hello " + FName + " " + LName + ",");
                //Body.Append("<br>");
                //Body.Append("Your stattus for the JG Prospect is :" + status);
                //Body.Append("<br>");
                ////if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
                ////{
                //Body.Append(Reason);
                ////}
                //Body.Append("<br>");
                //Body.Append("Tanking you");
                Body.Append(strHeader);
                Body.Append("<br>");
                Body.Append(strBody);
                Body.Append("<br>");
                //if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
                //{
                //Body.Append(Reason);
                //}
                Body.Append("<br>");
                Body.Append(strFooter);
                if (ddlstatus.SelectedValue == "OfferMade")
                {
                    createForeMenForJobAcceptance(Convert.ToString(Body));
                }
                if (ddlstatus.SelectedValue == "Deactive")
                {
                    CreateDeactivationAttachment(Convert.ToString(Body));
                }
                //Msg.Body = Convert.ToString(Body);
                //// your remote SMTP server IP.
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.Credentials = new System.Net.NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                //smtp.EnableSsl = true;
                //smtp.Send(Msg);
                //Msg = null;
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            // //SmtpClient smtp = new SmtpClient();
            // //MailMessage email_msg = new MailMessage();
            // //email_msg.To.Add(emailId);
            // //email_msg.From = new MailAddress("customsoft.test@gmail.com", "Credit Chex");
            // StringBuilder Body = new StringBuilder();
            // Body.Append("Hello " + FName + " " + LName + ",");
            // Body.Append("<br>");
            // Body.Append("Your stattus for the JG Prospect is :" + status);
            // Body.Append("<br>");
            // //if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
            // //{
            //     Body.Append(Reason);
            // //}
            // Body.Append("<br>");
            // Body.Append("Tanking you");
            //// AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
            //// LinkedResource imagelink3 = new LinkedResource(HttpContext.Current.Server.MapPath("~/images/logo.png"), "image/png");
            // //imagelink3.ContentId = "imageId1";
            // //imagelink3.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            // //htmlView.LinkedResources.Add(imagelink3);
            // var smtp = new System.Net.Mail.SmtpClient();
            // {
            //     smtp.Host = "smtp.gmail.com";
            //     smtp.Port = 587;
            //     smtp.EnableSsl = true;
            //     smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //     smtp.Credentials = new NetworkCredential("","q$7@wt%j*j*65ba#3M@9P6");
            //     smtp.Timeout = 20000;
            // }
            // // Passing values to smtp object
            // smtp.Send("", emailId, "JG Prospect Notification", Convert.ToString(Body));

        }

        private void CreateDeactivationAttachment(string MailBody)
        {
            string str_date = DateTime.Now.ToString().Replace("/", "");
            str_date = str_date.Replace(":", "");
            str_date = str_date.Replace("-", "");
            str_date = str_date.Replace(" ", "");
            string SourcePath = @"~/Sr_App/MailDocSample/DeactivationMail.doc";
            string TargetPath = @"~/Sr_App/MailDocument/" + str_date + txtfirstname.Text + "DeactivationMail.doc";
            System.IO.File.Copy(Server.MapPath(SourcePath), Server.MapPath(TargetPath), true);
            //modify word document
            object missing = System.Reflection.Missing.Value;
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = null;
            object Target = Server.MapPath(TargetPath);
            if (File.Exists(Server.MapPath(TargetPath)))
            {
                DateTime today = DateTime.Now;
                object readonlyNew = false;
                object isVisible = false;
                wordApp.Visible = false;
                FileInfo objFInfo = new FileInfo(Server.MapPath(TargetPath));
                objFInfo.IsReadOnly = false;
                aDoc = wordApp.Documents.Open(ref Target, ref missing, ref readonlyNew, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
                aDoc.Activate();
                this.FindAndReplace(wordApp, "name", txtfirstname.Text + " " + txtlastname.Text);
                //this.FindAndReplace(wordApp, "HireDate", txtHireDate.Text);
                //this.FindAndReplace(wordApp, "full time or part  time", ddlEmpType.SelectedValue);
                //this.FindAndReplace(wordApp, "HourlyRate", txtPayRates);
                //if (dtResignation.Text != "")
                //{
                //    this.FindAndReplace(wordApp, "WorkingStatus", "No");
                //    this.FindAndReplace(wordApp, "LastWorkingDay", dtResignation.Text);
                //}
                //else
                //{
                //    this.FindAndReplace(wordApp, "WorkingStatus", "No");
                //    this.FindAndReplace(wordApp, "LastWorkingDay", dtResignation.Text);
                //}
                //this.FindAndReplace(wordApp, "$ rate", txtPayRates.Text);
                //this.FindAndReplace(wordApp, "lbl: next pay period", "");
                //this.FindAndReplace(wordApp, "lbl: paycheck date", "");
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
            }
            using (MailMessage mm = new MailMessage("hr@jmgrove.com", txtemail.Text))
            {
                mm.Subject = "Deactivation";
                mm.Body = MailBody;
                mm.Attachments.Add(new Attachment(Server.MapPath(TargetPath)));
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("Customsofttest@gmail.com", "customsoft567");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        public void createForeMenForJobAcceptance(string str_Body)
        {
            //copy sample file for Foreman Job Acceptance letter template
            string str_date = DateTime.Now.ToString().Replace("/", "");
            str_date = str_date.Replace(":", "");
            str_date = str_date.Replace("-", "");
            str_date = str_date.Replace(" ", "");
            string SourcePath = @"~/Sr_App/MailDocSample/ForemanJobAcceptancelettertemplate.docx";
            string TargetPath = @"~/Sr_App/MailDocument/" + str_date + txtfirstname.Text + "ForemanJobAcceptanceletter.docx";
            System.IO.File.Copy(Server.MapPath(SourcePath), Server.MapPath(TargetPath), true);
            //modify word document
            object missing = System.Reflection.Missing.Value;
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = null;
            object Target = Server.MapPath(TargetPath);
            if (File.Exists(Server.MapPath(TargetPath)))
            {
                DateTime today = DateTime.Now;
                object readonlyNew = false;
                object isVisible = false;
                wordApp.Visible = false;
                FileInfo objFInfo = new FileInfo(Server.MapPath(TargetPath));
                objFInfo.IsReadOnly = false;
                aDoc = wordApp.Documents.Open(ref Target, ref missing, ref readonlyNew, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
                aDoc.Activate();
                this.FindAndReplace(wordApp, "LBL Date", DateTime.Now.ToShortDateString());
                this.FindAndReplace(wordApp, "Lbl Full name", txtfirstname.Text + " " + txtlastname.Text);
                this.FindAndReplace(wordApp, "LBL name", txtfirstname.Text + " " + txtlastname.Text);
                this.FindAndReplace(wordApp, "LBL position", ddldesignation.SelectedValue);
                //this.FindAndReplace(wordApp, "lbl fulltime", ddlEmpType.SelectedValue);
                //this.FindAndReplace(wordApp, "lbl: start date", txtHireDate.Text);
                //this.FindAndReplace(wordApp, "$ rate", txtPayRates.Text);
                this.FindAndReplace(wordApp, "lbl: next pay period", "");
                this.FindAndReplace(wordApp, "lbl: paycheck date", "");
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
            }
            using (MailMessage mm = new MailMessage("hr@jmgrove.com", txtemail.Text))
            {
                mm.Subject = "Foreman Job Acceptance";
                mm.Body = str_Body;
                mm.Attachments.Add(new Attachment(Server.MapPath(TargetPath)));
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("Customsofttest@gmail.com", "customsoft567");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
            }
        }

        private void FindAndReplace(Word.Application wordApp, object findText, object replaceText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref findText, ref matchCase,
                ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format,
                ref replaceText, ref replace, ref matchKashida,
                        ref matchDiacritics,
                ref matchAlefHamza, ref matchControl);
        }
        private string GetFooter(string status)
        {
            string Footer = string.Empty;
            DataTable DtFooter;
            DtFooter = InstallUserBLL.Instance.getTemplate(status, "footer");
            if (DtFooter.Rows.Count > 0)
            {
                Footer = DtFooter.Rows[0][0].ToString();
            }
            return Footer;
        }

        private string GetEmailBody(string status)
        {
            string Body = string.Empty;
            DataTable DtBody;
            DtBody = InstallUserBLL.Instance.getTemplate(status, "Body");
            if (DtBody.Rows.Count > 0)
            {
                Body = DtBody.Rows[0][0].ToString();
            }
            return Body;
        }

        private string GetEmailHeader(string status)
        {
            string Header = string.Empty;
            DataTable DtHeader;
            DtHeader = InstallUserBLL.Instance.getTemplate(status, "Header");
            if (DtHeader.Rows.Count > 0)
            {
                Header = DtHeader.Rows[0][0].ToString();
            }
            return Header;
        }

        protected void lnkFacePage_Click(object sender, EventArgs e)
        {
            var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/face page.pdf"));
            var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
            formFieldMap["Service Providers Full Business Name"] = Session["FirstName"] == null ? "" : Session["FirstName"].ToString();
            formFieldMap["Service Providers Notice ddress"] = Session["Address"] == null ? "" : Session["Address"].ToString();
            formFieldMap["City"] = Session["Zip"] == null ? "" : Session["Zip"].ToString();
            formFieldMap["Service Providers Primary Email ddress Please Provide"] = ViewState["Email"] == null ? "" : ViewState["Email"].ToString();
            formFieldMap["Service Provider Tel No"] = ViewState["Phone"] == null ? "" : ViewState["Phone"].ToString();
            var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
            string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";
            //string PdflDirectory = Server.MapPath("/PDFS");
            string Filename = "FacePage_" + txtfirstname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
            //string path = PdflDirectory + "\\" + Filename;
            PDFHelper.ReturnPDF(pdfContents, Filename);
        }
        protected void lnkEscrow_Click(object sender, EventArgs e)
        {
            var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/Escrow Signature page.pdf"));
            var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
            formFieldMap["Text5"] = Session["FirstName"] == null ? "" : Session["FirstName"].ToString();
            formFieldMap["Text6"] = Session["Address"] == null ? "" : Session["Address"].ToString();
            formFieldMap["Text2"] = ViewState["zipEsrow"] == null ? "" : ViewState["zipEsrow"].ToString();
            formFieldMap["Text7"] = ViewState["City"] == null ? "" : ViewState["City"].ToString();
            formFieldMap["Text1"] = ViewState["State"] == null ? "" : ViewState["State"].ToString();
            formFieldMap["undefined_4"] = "JMGrove LLC";
            formFieldMap["undefined_6"] = System.DateTime.Now.ToShortDateString();
            formFieldMap["X"] = ViewState["Signature"] == null ? "" : ViewState["Signature"].ToString();
            formFieldMap["Property Address"] = ViewState["tin"] == null ? "" : ViewState["tin"].ToString();
            var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
            string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";
            //string PdflDirectory = Server.MapPath("/PDFS");
            string Filename = "Escrow_" + txtfirstname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
            //string path = PdflDirectory + "\\" + Filename;
            PDFHelper.ReturnPDF(pdfContents, Filename);
        }
        protected void lnkW9_Click(object sender, EventArgs e)
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
            string Filename = "W9_" + txtfirstname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
            //string path = PdflDirectory + "\\" + Filename;
            PDFHelper.ReturnPDF(pdfContents, Filename);
        }

        protected void txtfirstname_TextChanged(object sender, EventArgs e)
        {
            Session["FirstName"] = txtfirstname.Text;
            txtlastname.Focus();
        }
        protected void txtlastname_TextChanged(object sender, EventArgs e)
        {
            Session["LastName"] = txtlastname.Text;
            txtemail.Focus();
        }
        protected void txtaddress_TextChanged(object sender, EventArgs e)
        {
            Session["Address"] = txtaddress.Text;
            if (txtaddress.Text != "" && chkMaddAdd.Checked == true)
            {
                txtMailingAddress.Text = txtaddress.Text;
            }
            txtZip.Focus();
        }


        protected void btn_UploadPicture_Click(object sender, EventArgs e)
        {
            string ext = "";
            string str_IdNo;
            string fileName = string.Empty;
            string save = string.Empty;
            Int32 cnt;
            Int32 i;
            if (txtfirstname.Text != "" && txtlastname.Text != "" && ddldesignation.SelectedIndex != 0)
            {
                if (flpUplaodPicture.FileName != string.Empty)
                {
                    ext = Path.GetExtension(flpUplaodPicture.FileName);
                    if (ext == ".jpeg" || ext == ".png" || ext == ".jpg" || ext == ".tif" || ext == ".gif")
                    {
                        fileName = flpUplaodPicture.FileName;
                        fileName = DateTime.Now.ToString() + fileName;
                        fileName = fileName.Replace("/", "");
                        fileName = fileName.Replace(":", "");
                        fileName = fileName.Replace(" ", "");
                        //fileName = Path.GetFileName(flpUplaodPicture.FileName);
                        save = Server.MapPath("~/Sr_App/UploadedFile/") + fileName;
                        flpUplaodPicture.SaveAs(save);
                        Session["UplaodPicture"] = null;
                        Session["UploadedPictureName"] = null;
                        Session["UplaodPicture"] = "~/Sr_App/UploadedFile/" + fileName;
                        Session["UploadedPictureName"] = fileName;
                        // cnt = lstboxUploadedImages.Items.Count;
                        // if (cnt == 0)
                        // {
                        // lstboxUploadedImages.Items.Add(fileName);
                        //   lstboxUploadedImages.SelectedIndex = 0;
                        //   string curItem = lstboxUploadedImages.SelectedItem.ToString();
                        Image2.Visible = true;
                        Image2.ImageUrl = "~/Sr_App/UploadedFile/" + fileName;
                        lblICardName.Text = txtfirstname.Text + " " + txtlastname.Text;
                        lblICardPosition.Text = ddldesignation.SelectedItem.Text;
                        if (Convert.ToString(Session["installId"]) == "")
                        {
                            if (Request.QueryString["ID"] == null)
                            {
                                Session["IdGenerated"] = GetId(ddldesignation.SelectedItem.Text, ddlstatus.SelectedValue);
                                lblICardIDNo.Text = Convert.ToString(Session["IdGenerated"]);
                                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
                            }
                        }
                        FillDocument();
                        return;

                        // }
                        // if (fileName != "")
                        // {
                        //  lstboxUploadedImages.Items.Add(fileName);
                        //   lstboxUploadedImages.SelectedIndex = 0;
                        // }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "AlertBox", "alert('Please select jpeg,png,jpg,tif or gif');", true);
                        return;
                    }
                }
                else
                {
                    Session["UploadedPictureName"] = "";
                    Session["UplaodPicture"] = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "AlertBox", "alert('Please Upload Image');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(Please Select Designition,Enter First Name & last Name)", true);
                return;
            }
            //lblDtae.Text = DateTime.Now.ToString();
            //UpdatePanel4.Update();

        }

        private void GenerateBarCode(string Id)
        {
            if (Id != "")
            {
                string barCode = Id;
                string strImageURL = "GenerateBarcodeImage.aspx?d=" + Id + "&h=" + 40 + "&w=" + 195 + "&bc=" + "" + "&fc=" + "" + "&t=" + "Code 39 Extended" + "&il=" + "" + "&if=" + "PNG" + "&align=" + "C";
                this.BarcodeImage.ImageUrl = strImageURL;
                this.BarcodeImage.Width = Convert.ToInt32(195);
                this.BarcodeImage.Height = Convert.ToInt32(40);
                this.BarcodeImage.Visible = true;
                //lblBarcode.Text=Id;
                //barcode.InnerHtml = Id;
                //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                //using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                //{
                //    using (Graphics graphics = Graphics.FromImage(bitMap))
                //    {
                //        System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 16);
                //        PointF point = new PointF(2f, 2f);
                //        SolidBrush blackBrush = new SolidBrush(Color.Black);
                //        SolidBrush whiteBrush = new SolidBrush(Color.White);
                //        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                //        graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                //    }
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //        byte[] byteImage = ms.ToArray();

                //        Convert.ToBase64String(byteImage);
                //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                //    }
                //    plBarCode.Controls.Add(imgBarCode);
                //}
            }
        }
        protected void lstbxImages_SelectedIndexChanged1(object sender, EventArgs e)
        {
            // string curItem = lstboxUploadedImages.SelectedItem.ToString();
            //  Image2.ImageUrl = "~/CustomerDocs/LocationPics/" + curItem;
        }
        //Update.....
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Convert.ToString(Session["UploadedPictureName"]) == "" || Convert.ToString(Session["UploadedPictureName"]) == null) && ddlstatus.SelectedValue == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select image file');", true);
                    return;
                }
                if ((Convert.ToString(Session["PqLicense"]) == "" || Convert.ToString(Session["PqLicense"]) == null) && ddlstatus.SelectedValue == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select DL License file');", true);
                    return;
                }
                if (chkboxcondition.Checked != true && ddldesignation.SelectedValue == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Term and Conditions');", true);
                    return;
                }
                btn_UploadFiles_Click(sender, e);
                objuser.fristname = txtfirstname.Text;
                objuser.lastname = txtlastname.Text;
                objuser.email = txtemail.Text.Trim();
                objuser.address = txtaddress.Text;
                objuser.zip = txtZip.Text;
                objuser.state = txtState.Text;
                objuser.city = txtCity.Text;
                objuser.password = txtpassword.Text;
                objuser.designation = ddldesignation.SelectedItem.Text;
                objuser.status = ddlstatus.SelectedItem.Text;
                objuser.phone = txtPhone.Text;
                //For Event Added by.....ID
                objuser.AddedBy = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                // if (lstboxUploadedImages.Items.Count != 0)
                // {
                //     foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                //    {
                //       fn = fn + "," + img.Text;
                ////   }
                //   objuser.picture = fn.TrimStart(',');
                //  }
                // else if(lstboxUploadedImages.Items.Count>0)
                //  {
                //       foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                //       {
                //           fn = fn + "," + img.Text;
                //       }
                //       objuser.picture = fn.TrimStart(',');
                //  }            
                //else
                //  {
                objuser.picture = Convert.ToString(Session["UplaodPicture"]);
                //  }
                objuser.attachements = GetUpdateAttachments();
                objuser.businessname = txtBusinessName.Text;
                objuser.ssn = txtssn.Text;
                objuser.ssn1 = txtssn0.Text;
                objuser.ssn2 = txtssn1.Text;
                objuser.signature = txtSignature.Text;
                objuser.dob = DOBdatepicker.Text;
                objuser.DateSourced = txtDateSource.Text;
                objuser.citizenship = ddlcitizen.SelectedValue;
                // objuser.tin = txtTIN.Text;
                objuser.ein1 = txtEIN.Text;
                objuser.ein2 = txtEIN2.Text;
                //objuser.a = txtA.Text;
                //objuser.b = txtB.Text;
                //objuser.c = txtC.Text;
                //objuser.d = txtD.Text;
                //objuser.e = txtE.Text;
                //objuser.f = txtF.Text;
                //objuser.g = txtG.Text;
                //objuser.h = txtH.Text;
                //objuser.i = txt5.Text;
                //objuser.j = txt6.Text;
                //objuser.k = txt7.Text;
                //objuser.maritalstatus = ddlmaritalstatus.SelectedValue;
                objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                objuser.Source = ddlSource.SelectedValue;
                objuser.SourceUser = Convert.ToString(Session["userid"]);
                objuser.Notes = txtNotes.Text;
                string str_Reason = "";
                if (txtReson.Visible == true && ddlstatus.SelectedValue == "Rejected")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = txtReson.Text;
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else if (dtInterviewDate.Visible == true && ddlstatus.SelectedValue == "InterviewDate")
                {
                    objuser.Reason = Convert.ToString(dtInterviewDate.Text);
                }
                else if (ddlstatus.SelectedValue == "Active")
                {
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else if (txtReson.Visible == true && ddlstatus.SelectedValue == "Deactive")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = txtReson.Text;
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else if (txtReson.Visible == true && ddlstatus.SelectedValue == "OfferMade")
                {
                    str_Reason = txtReson.Text;
                    objuser.Reason = txtReson.Text;
                    objuser.RejectionDate = DateTime.Today.ToShortDateString();
                    objuser.RejectionTime = DateTime.Now.ToShortTimeString();
                    objuser.RejectedUserId = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);
                }
                else
                {
                    objuser.Reason = "";
                }
                objuser.PqLicense = Convert.ToString(Session["PqLicense"]);
                objuser.WorkersComp = Convert.ToString(Session["WorkersComp"]);
                objuser.GeneralLiability = Convert.ToString(Session["flpGeneralLiability"]);
                //objuser.HireDate = txtHireDate.Text;
                //objuser.TerminitionDate = dtResignation.Text;
                //objuser.WorkersCompCode = Convert.ToString(ddlWorkerCompCode.Text);
                //objuser.NextReviewDate = dtReviewDate.Text;
                //objuser.EmpType = ddlEmpType.SelectedValue;
                //objuser.LastReviewDate = dtLastDate.Text;
                //objuser.PayRates = txtPayRates.Text;
                objuser.ExtraEarning = Convert.ToString(Session["ExtraIncomeName"]);
               // objuser.ExtraEarningAmt = Convert.ToString(Session["ExtraIncomeAmt"]);
                //if (rdoCheque.Checked)
                //{
                //    objuser.PayMethod = "Check";
                //}
                //else
                //{
                //    objuser.PayMethod = "Direct Deposite";
                //}
                //objuser.Deduction = Convert.ToDouble(txtDeduction.Text);
                //if (rdoOneTime.Checked)
                //{
                //    objuser.DeductionType = "One Time";
                //}
                //else
                //{
                //    objuser.DeductionType = "Re-Occurance";
                //}
                objuser.AbaAccountNo = "";
                objuser.AccountNo = "";
                objuser.AccountType = "";
                objuser.PTradeOthers = txtOtherTrade.Text;
                objuser.STradeOthers = txtSecTradeOthers.Text;
                objuser.str_SuiteAptRoom = txtSuiteAptRoom.Text;

                if (rdoAttchmentYes.Checked)
                {
                    objuser.skillassessmentstatus = true;
                }
                else if (rdoAttchmentNo.Checked)
                {
                    objuser.skillassessmentstatus = false;
                }
                objuser.assessmentPath = Convert.ToString(Session["SkillAttachment"]);
                objuser.FullTimePosition = Convert.ToInt32(txtFullTimePos.Text);
                objuser.ContractorsBuilderOwner = txtContractor1.Text + "#" + txtContractor2.Text + "#" + txtContractor3.Text;
                objuser.MajorTools = txtMajorTools.Text;
                if (rdoDrugtestYes.Checked)
                {
                    objuser.DrugTest = true;
                }
                else if (rdoDrugtestNo.Checked)
                {
                    objuser.DrugTest = false;
                }
                if (rdoDriveLicenseYes.Checked)
                {
                    objuser.ValidLicense = true;
                }
                else if (rdoDriveLicenseNo.Checked)
                {
                    objuser.ValidLicense = false;
                }
                if (rdoTruckToolsYes.Checked)
                {
                    objuser.TruckTools = true;
                }
                else if (rdoTruckToolsNo.Checked)
                {
                    objuser.TruckTools = false;
                }
                if (rdoJMApplyYes.Checked)
                {
                    objuser.PrevApply = true;
                }
                else if (rdoJMApplyYes.Checked)
                {
                    objuser.PrevApply = false;
                }

                if (Convert.ToString(Session["ddlStatus"]) != ddlstatus.SelectedValue)
                {
                    objuser.Flag = 1;
                }
                else
                {
                    objuser.Flag = 0;
                }

                //if (rdoLicenseYes.Checked)
                //{
                //    objuser.LicenseStatus = true;
                //}
                //else if (rdoLicenseNo.Checked)
                //{
                //    objuser.LicenseStatus = false;
                //}
                if (rdoGuiltyYes.Checked)
                {
                    objuser.CrimeStatus = true;
                }
                else if (rdoGuiltyNo.Checked)
                {
                    objuser.CrimeStatus = false;
                }
                objuser.StartDate = Convert.ToString(txtStartDateNew.Text);
                objuser.SalaryReq = txtSalRequirement.Text;
                objuser.Avialability = txtAvailability.Text;
                //objuser.WarrentyPolicy = txtWarrantyPolicy.Text;
                objuser.businessYrs = Convert.ToDouble(txtYrs.Text);
                objuser.underPresentComp = Convert.ToDouble(txtCurrentComp.Text);
                objuser.websiteaddress = txtWebsiteUrl.Text;
                objuser.ResumePath = Convert.ToString(Session["ResumePath"]);
                objuser.CirtificationTraining = Convert.ToString(Session["CirtificationPath"]);
                objuser.PersonName = Convert.ToString(Session["PersonName"]);
                objuser.PersonType = Convert.ToString(Session["PersonType"]);
                objuser.CompanyPrinciple = txtPrinciple.Text;
                objuser.BusinessType = ddlBusinessType.SelectedValue;
                objuser.CEO = txtCEO.Text;
                objuser.LegalOfficer = txtLeagalOfficer.Text;
                objuser.President = txtPresident.Text;
                objuser.Owner = txtSoleProprietorShip.Text;
                objuser.AllParteners = txtPartnetsName.Text;
                //objuser.MailingAddress = txtMailingAddress.Text;
                if (rdoWarrantyYes.Checked)
                    objuser.Warrantyguarantee = true;
                else if (rdoWarrantyNo.Checked)
                    objuser.Warrantyguarantee = false;
                objuser.WarrantyYrs = Convert.ToInt32(txtWarentyTimeYrs.Text);
                if (rdoBusinessMinorityYes.Checked)
                    objuser.MinorityBussiness = true;
                else if (rdoBusinessMinorityNo.Checked)
                    objuser.MinorityBussiness = false;
                if (rdoWomenYes.Checked)
                    objuser.WomensEnterprise = true;
                else if (rdoWomenNo.Checked)
                    objuser.WomensEnterprise = false;
                if (ddlstatus.SelectedValue == "InterviewDate")
                {
                    str_Reason = ddlInsteviewtime.SelectedItem.Text;
                    objuser.InterviewTime = ddlInsteviewtime.SelectedItem.Text;
                }
                else
                {
                    objuser.InterviewTime = "";
                }
                if (ddlstatus.SelectedValue == "Active")
                {
                    objuser.ActivationDate = DateTime.Today.ToShortDateString();
                    objuser.UserActivated = Convert.ToString(Session["Username"]);
                }
                else
                {
                    objuser.ActivationDate = "";
                    objuser.UserActivated = "";
                }
                objuser.MailingAddress = txtMailingAddress.Text;
                if (chkboxcondition.Checked)
                {
                    objuser.TC = true;
                }
                else
                {
                    objuser.TC = false;
                }
                if (Convert.ToString(Session["PrevDesig"]) != ddldesignation.SelectedValue || ddlstatus.SelectedValue == "Deactive")
                {
                    Session["installId"] = GetUpdatedId(Convert.ToString(Session["installId"]));
                    objuser.InstallId = Convert.ToString(Session["installId"]);
                }
                int id = Convert.ToInt32(Session["ID"]);
                DataSet dsCheckDuplicate = InstallUserBLL.Instance.CheckInstallUserOnEdit(txtemail.Text, txtPhone.Text, Convert.ToInt32(Session["ID"]));
                if (dsCheckDuplicate.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "alert('Record with same email or phone number already exists.')", true);
                    //ModalPopupExtender2.Show();
                    return;
                }
                else
                {
                    bool result = InstallUserBLL.Instance.UpdateInstallUser(objuser, id);
                    if (ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "OfferMade" || ddlstatus.SelectedValue == "Deactive")
                    {
                        SendEmail(txtemail.Text, txtfirstname.Text, txtlastname.Text, ddlstatus.SelectedValue, str_Reason);
                    }
                    GoogleCalendarEvent.CreateCalendar(txtemail.Text, txtaddress.Text);
                    //lblmsg.Visible = true;
                    //lblmsg.CssClass = "success";
                    //lblmsg.Text = "User has been created successfully";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('InstallUser  Update successfully');", true);
                    clearcontrols();
                    //Server.Transfer("EditInstallUser.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('InstallUser  Update successfully.');window.location ='EditInstallUser.aspx';", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btn_UploadFiles_Click(object sender, EventArgs e)
        {
            HttpFileCollection uploads = Request.Files;
            string attach = "";
            if (Session["attachments"] != null && Convert.ToString(Session["attachments"])!="")
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
                        string filename = uploadedFile.FileName;//flpUploadFiles.FileName;
                        filename = DateTime.Now.ToString() + filename;
                        filename = filename.Replace("/", "");
                        filename = filename.Replace(":", "");
                        filename = filename.Replace(" ", "");
                        Server.MapPath("~/Sr_App/UploadedFile/" + filename);
                        uploadedFile.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));

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
                        if (Convert.ToString(Session["ResumeName"]) != "" && attach != null)
                        {
                            attach = attach + "," + Convert.ToString(Session["ResumeName"]);
                        }
                        else if (Convert.ToString(Session["ResumeName"]) != "")
                        {
                            attach = Convert.ToString(Session["ResumeName"]);
                        }
                        if (Convert.ToString(Session["CirtificationName"]) != "" && attach != null)
                        {
                            attach = attach + "," + Convert.ToString(Session["CirtificationName"]);
                        }
                        else if (Convert.ToString(Session["CirtificationName"]) != "")
                        {
                            attach = Convert.ToString(Session["CirtificationName"]);
                        }
                        if (Convert.ToString(Session["SkillAttachmentName"]) != "" && attach != null)
                        {
                            attach = attach + "," + Convert.ToString(Session["SkillAttachmentName"]);
                        }
                        else if (Convert.ToString(Session["SkillAttachmentName"]) != "")
                        {
                            attach = Convert.ToString(Session["SkillAttachmentName"]);
                        }
                        if (Convert.ToString(Session["UploadedPictureName"]) != "")
                        {
                            attach = attach + "," + Convert.ToString(Session["UploadedPictureName"]);
                        }
                        if (Convert.ToString(Session["PqLicenseFileName"]) != "")
                        {
                            attach = attach + "," + Convert.ToString(Session["PqLicenseFileName"]);
                        }
                        if (Convert.ToString(Session["flpGeneralLiabilityName"]) != "")
                        {
                            attach = attach + "," + Convert.ToString(Session["flpGeneralLiabilityName"]);
                        }
                        if (Convert.ToString(Session["WorkersCompFileName"]) != "")
                        {
                            attach = attach + "," + Convert.ToString(Session["WorkersCompFileName"]);
                        }
                    }
                    if (attach != null)
                    {
                        string[] att = attach.Split(',');
                        var data = att.Select(s => new { FileName = s }).ToList();
                        gvUploadedFiles.DataSource = data;
                        gvUploadedFiles.DataBind();
                        gvUploadedFiles.Visible = true;
                    }
                }
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            txtpassword.Attributes["value"] = txtpassword.Text;
            txtpassword1.Attributes["value"] = txtpassword1.Text;
            base.OnPreRender(e);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            //  for (int i = lstboxUploadedImages.Items.Count - 1; i >= 0; i--)
            //   {
            //      lstboxUploadedImages.Items.Remove(lstboxUploadedImages.Items[i]);
            //  }
        }
        protected void lstbxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string curItem = lstboxUploadedImages.SelectedItem.ToString();
            //  Image2.ImageUrl = "~/CustomerDocs/LocationPics/" + curItem;
            // Image2.ImageUrl = "Images" + "\\" + curItem;
        }
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
                if (Convert.ToString(Session["ResumePath"]).Contains(file))
                {
                    if (Convert.ToString(Session["ResumePath"]).Contains("http"))
                    {
                        string url = Convert.ToString(Session["ResumePath"]);
                        Session["ResumePathTemp"] = Session["ResumePath"];
                        string str_fileName = Path.GetFileName(Convert.ToString(Session["ResumePath"]));
                        string file_name = Server.MapPath("~/Sr_App/UploadedFile/") + str_fileName;
                        Session["ResumePath"] = file_name;
                        save_file_from_url(file_name, url);
                        UpdateDocPath(Convert.ToString(Session["ResumePath"]), Convert.ToString(Session["ResumePathTemp"]));
                        string filePath = ("~/Sr_App/UploadedFile/" + file);
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
                    else
                    {
                        file = Path.GetFileName(file);
                        string filePath = ("~/Sr_App/UploadedFile/" + file);
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
                else
                {
                    file = Path.GetFileName(file);
                    string filePath = ("~/Sr_App/UploadedFile/" + file);
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

        }

        private void UpdateDocPath(string NewPath, string OldPath)
        {
            InstallUserBLL.Instance.UpdateDocPath(NewPath, OldPath);
        }

        public void save_file_from_url(string file_name, string url)
        {
            byte[] content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                content = br.ReadBytes(500000);
                br.Close();
            }
            response.Close();

            FileStream fs = new FileStream(file_name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(content);
            }
            finally
            {
                fs.Close();
                bw.Close();
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
                    if (Convert.ToString(Session["ResumePath"]).Contains(filesTodeleteString))
                    {
                        if (Convert.ToString(Session["ResumePath"]).Contains("http"))
                        {
                            string url = Convert.ToString(Session["ResumePath"]);
                            string str_fileName = Path.GetFileName(Convert.ToString(Session["ResumePath"]));
                            string file_name = Server.MapPath("~/Sr_App/UploadedFile/") + str_fileName;
                            Session["ResumePath"] = file_name;
                            save_file_from_url(file_name, url);
                            var path = Server.MapPath("~/Sr_App/UploadedFile/" + filesTodeleteString);
                            // System.IO.File.Delete(path);
                            string attachmentsString = Convert.ToString(Session["attachments"]);
                            var updatedAttachments = attachmentsString.Split(',').Except(filesTodelete).ToArray();
                            Session["attachments"] = string.Join(",", updatedAttachments);
                            if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["UploadedPictureName"])))
                            {
                                DeleteImageFile(path);
                                Session["UploadedPictureName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["PqLicenseFileName"])))
                            {
                                DeleteDLFile(path);
                                Session["PqLicenseFileName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["WorkersCompFileName"])))
                            {
                                DeleteDLComp(path);
                                Session["WorkersCompFileName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["flpGeneralLiabilityName"])))
                            {
                                DeleteGen(path);
                                Session["flpGeneralLiabilityName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["ResumeName"])))
                            {
                                DeleteResume(path);
                                Session["ResumeName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["CirtificationName"])))
                            {
                                DeleteCirtification(path);
                                Session["CirtificationName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["SkillAttachmentName"])))
                            {
                                DeleteAssessment(path);
                                Session["SkillAttachmentName"] = "";
                            }
                        }
                        else
                        {
                            var path = Server.MapPath("~/Sr_App/UploadedFile/" + filesTodeleteString);
                            System.IO.File.Delete(path);
                            string attachmentsString = Convert.ToString(Session["attachments"]);
                            var updatedAttachments = attachmentsString.Split(',').Except(filesTodelete).ToArray();
                            Session["attachments"] = string.Join(",", updatedAttachments);
                            if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["UploadedPictureName"])))
                            {
                                DeleteImageFile(path);
                                Session["UploadedPictureName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["PqLicenseFileName"])))
                            {
                                DeleteDLFile(path);
                                Session["PqLicenseFileName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["ResumeName"])))
                            {
                                DeleteResume(path);
                                Session["ResumeName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["CirtificationName"])))
                            {
                                DeleteCirtification(path);
                                Session["CirtificationName"] = "";
                            }
                            else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["SkillAttachmentName"])))
                            {
                                DeleteAssessment(path);
                                Session["SkillAttachmentName"] = "";
                            }
                        }
                    }
                    else
                    {
                        var path = Server.MapPath("~/Sr_App/UploadedFile/" + filesTodeleteString);
                        System.IO.File.Delete(path);
                        string attachmentsString = Convert.ToString(Session["attachments"]);
                        var updatedAttachments = attachmentsString.Split(',').Except(filesTodelete).ToArray();
                        Session["attachments"] = string.Join(",", updatedAttachments);
                        if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["UploadedPictureName"])))
                        {
                            DeleteImageFile(path);
                            Session["UploadedPictureName"] = "";
                        }
                        else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["PqLicenseFileName"])))
                        {
                            DeleteDLFile(path);
                            Session["PqLicenseFileName"] = "";
                        }
                        else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["ResumeName"])))
                        {
                            DeleteResume(path);
                            Session["ResumeName"] = "";
                        }
                        else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["CirtificationName"])))
                        {
                            DeleteCirtification(path);
                            Session["CirtificationName"] = "";
                        }
                        else if (Convert.ToString(ViewState["FilesToDelete"]).Equals(Convert.ToString(Session["SkillAttachmentName"])))
                        {
                            DeleteAssessment(path);
                            Session["SkillAttachmentName"] = "";
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
        }

        private void DeleteAssessment(string Path)
        {
            InstallUserBLL.Instance.DeleteAssessment(Path);
        }

        private void DeleteResume(string Path)
        {
            InstallUserBLL.Instance.DeleteResume(Path);
        }

        private void DeleteCirtification(string Path)
        {
            InstallUserBLL.Instance.DeleteCirtification(Path);
        }

        private void DeleteDLComp(string Path)
        {
            InstallUserBLL.Instance.DeleteComp(Path);
        }

        private void DeleteGen(string Path)
        {
            InstallUserBLL.Instance.DeleteGeneral(Path);
        }

        private void DeleteDLFile(string DLPath)
        {
            InstallUserBLL.Instance.DeletePLLic(DLPath);
        }
        private void DeleteImageFile(string ImagePath)
        {
            InstallUserBLL.Instance.DeleteImage(ImagePath);
            Image2.ImageUrl = "";
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
        protected void btndelete_Click1(object sender, EventArgs e)
        {

            // if (lstboxUploadedImages.SelectedIndex >= 0)
            // {
            //     int Index = lstboxUploadedImages.SelectedIndex;
            //     string strDelete = lstboxUploadedImages.SelectedItem.Text;
            //     lstboxUploadedImages.Items.Remove(strDelete);
            //     var path = Server.MapPath("~/CustomerDocs/LocationPics/" + strDelete);
            //     System.IO.File.Delete(path);

            //     if (lstboxUploadedImages.Items.Count > 0)
            //     {
            //         //lstboxUploadedImages.SelectedIndex = Index - 1;
            //         lstboxUploadedImages.SelectedIndex = 0;
            //         string Remainfile = lstboxUploadedImages.SelectedItem.Text;
            //         Image2.ImageUrl = "~/CustomerDocs/LocationPics/" + Remainfile;
            //     }

            //     if (lstboxUploadedImages.SelectedIndex != -1)
            //     {
            //         lstboxUploadedImages.SelectedIndex = 0;
            //         string Remainfile = lstboxUploadedImages.SelectedItem.Text;
            //         Image2.ImageUrl = "~/CustomerDocs/LocationPics/" + Remainfile;
            //     }
            //     else
            //     {
            //         Image2.ImageUrl = "";
            //     }
            //}
        }
        protected void txtemail_TextChanged(object sender, EventArgs e)
        {
            ViewState["Email"] = txtemail.Text;
            txtaddress.Focus();

        }
        protected void txtPhone_TextChanged(object sender, EventArgs e)
        {
            ViewState["Phone"] = txtPhone.Text;
            ddldesignation.Focus();
        }
        protected void txtssn_TextChanged(object sender, EventArgs e)
        {
            ViewState["SSN"] = txtssn.Text;
        }
        protected void txtssn0_TextChanged(object sender, EventArgs e)
        {
            ViewState["SSN1"] = txtssn0.Text;
        }
        protected void txtssn1_TextChanged(object sender, EventArgs e)
        {
            ViewState["SSN2"] = txtssn1.Text;
            string strssn = ((txtssn.Text != "") ? txtssn.Text : "") + ((txtssn0.Text != "") ? "- " + txtssn0.Text : string.Empty) + ((txtssn1.Text != "") ? "-" + txtssn1.Text : string.Empty);
            ViewState["ssn"] = strssn;
            if (txtssn1.Text != "")
            {
                lblReqEIN.Visible = false;
                rqEin1.Enabled = false;
                rqEIN2.Enabled = false;
            }
        }
        protected void DOBdatepicker_TextChanged(object sender, EventArgs e)
        {
            ViewState["DOB"] = DOBdatepicker.Text;
        }
        protected void txtBusinessName_TextChanged(object sender, EventArgs e)
        {
            ViewState["BusinessName"] = txtBusinessName.Text;
        }
        protected void txtSignature_TextChanged(object sender, EventArgs e)
        {
            ViewState["Signature"] = txtSignature.Text;
        }
        //protected void txtState_TextChanged(object sender, EventArgs e)
        //{
        //    ViewState["State"] = txtSignature.Text;
        //}
        //protected void txtCity_TextChanged(object sender, EventArgs e)
        //{
        //    ViewState["City"] = txtSignature.Text;
        //}

        //        protected void txttin_textchanged(object sender, eventargs e)
        //      {
        //
        //  viewstate["tin"] = txttin.text;
        //}

        protected void ddlcitizen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["citizen"] = ddlcitizen.SelectedValue;
        }

        protected void lnkI9_Click(object sender, EventArgs e)
        {
            var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/I-9.pdf"));
            var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
            formFieldMap["Text2"] = Session["FirstName"] == null ? "" : Session["FirstName"].ToString();
            formFieldMap["Text1"] = Session["LastName"] == null ? "" : Session["LastName"].ToString();
            formFieldMap["Text4"] = Session["Address"] == null ? "" : Session["Address"].ToString();
            // formFieldMap["Text8"] = Session["Zip"] == null ? "" : Session["Zip"].ToString();
            formFieldMap["Text37"] = ViewState["ssn"] == null ? "" : ViewState["ssn"].ToString();
            formFieldMap["Text6"] = ViewState["DOB"] == null ? "" : ViewState["DOB"].ToString();
            formFieldMap["Text40"] = ViewState["Signature"] == null ? "" : ViewState["Signature"].ToString();
            formFieldMap["Text41"] = System.DateTime.Now.ToShortDateString();
            formFieldMap["Text43"] = ViewState["zipEsrow"] == null ? "" : ViewState["zipEsrow"].ToString();
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
            string PdflDirectory = Server.MapPath("/PDFS");
            string Filename = "I9_" + txtfirstname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
            string path = PdflDirectory + "\\" + Filename;
            PDFHelper.ReturnPDF(pdfContents, Filename);
        }

        protected void txtEIN_TextChanged(object sender, EventArgs e)
        {
            ViewState["ein1"] = txtEIN.Text;
        }

        protected void txtEIN2_TextChanged(object sender, EventArgs e)
        {
            ViewState["ein2"] = txtEIN2.Text;
            string strein = ((txtEIN.Text != "") ? txtEIN.Text : "") + ((txtEIN2.Text != "") ? "- " + txtEIN2.Text : string.Empty);
            ViewState["ein"] = strein;
            if (txtEIN2.Text != "")
            {
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
            }
        }


        protected void lnkw4Details_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Show();
        }

        protected void ddlmaritalstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ViewState["maritalstatus"] = ddlmaritalstatus.SelectedValue;
        }

        protected void lnkW4_Click(object sender, EventArgs e)
        {
            var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/w4.pdf"));
            var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);
            formFieldMap["topmostSubform[0].Page1[0].f1_09_0_[0]"] = Session["FirstName"] == null ? "" : Session["FirstName"].ToString();
            formFieldMap["topmostSubform[0].Page1[0].f1_10_0_[0]"] = Session["LastName"] == null ? "" : Session["LastName"].ToString();
            formFieldMap["topmostSubform[0].Page1[0].f1_14_0_[0]"] = Session["Address"] == null ? "" : Session["Address"].ToString();
            formFieldMap["topmostSubform[0].Page1[0].f1_15_0_[0]"] = Session["Zip"] == null ? "" : Session["Zip"].ToString();
            formFieldMap["topmostSubform[0].Page1[0].f1_13_0_[0]"] = ViewState["ssn"] == null ? "" : ViewState["ssn"].ToString();
            formFieldMap["topmostSubform[0].Page1[0].f1_22_0_[0]"] = ViewState["ein"] == null ? "" : ViewState["ein"].ToString();
            formFieldMap["Text2"] = ViewState["Signature"] == null ? "" : ViewState["Signature"].ToString();
            formFieldMap["Text1"] = System.DateTime.Now.ToShortDateString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_01_0_[0]"] = txtA.Text == null ? "" : txtA.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_02_0_[0]"] = txtB.Text == null ? "" : txtB.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_03_0_[0]"] = txtC.Text == null ? "" : txtC.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_04_0_[0]"] = txtD.Text == null ? "" : txtD.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_05_0_[0]"] = txtE.Text == null ? "" : txtE.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_06_0_[0]"] = txtF.Text == null ? "" : txtF.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_07_0_[0]"] = txtG.Text == null ? "" : txtG.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_08_0_[0]"] = txtH.Text == null ? "" : txtH.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_16_0_[0]"] = txt5.Text == null ? "" : txt5.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_17_0_[0]"] = txt6.Text == null ? "" : txt6.Text.ToString();
            //formFieldMap["topmostSubform[0].Page1[0].f1_18_0_[0]"] = txt7.Text == null ? "" : txt7.Text.ToString();
            if (ViewState["maritalstatus"] != null)
            {
                //if (ddlmaritalstatus.SelectedValue == "Single")
                //{
                //    formFieldMap["topmostSubform[0].Page1[0].p1-cb1[0]"] = "1";
                //}
                //else
                //{
                //    formFieldMap["Check Box2"] = "Yes";
                //}
            }
            var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);
            string fileName = "pdfDocument" + DateTime.Now.Ticks + ".pdf";
            string PdflDirectory = Server.MapPath("/PDFS");
            string Filename = "W4_" + txtfirstname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
            string path = PdflDirectory + "\\" + Filename;
            PDFHelper.ReturnPDF(pdfContents, Filename);
        }

        

        //protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlstatus.SelectedValue == "Deactive")
        //    {
        //        //For Applicant Status....
        //       // rfvUserStatus.Enabled = false;
        //        if (Convert.ToString(Session["PreviousStatusNew"]) != "Active")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User should be active to deactivate.')", true);
        //            return;
        //        }
        //    }
        //    if (ddlstatus.SelectedValue == "Applicant")
        //    {
                
        //        dtInterviewDate.Visible = false;
        //        txtReson.Visible = false;
        //        pnlNew.Visible = true;
        //    }
        //    else if (ddlstatus.SelectedValue == "Rejected")
        //    {
               
        //        dtInterviewDate.Visible = false;
        //        ddlInsteviewtime.Visible = false;
        //        txtReson.Visible = true;
        //        RequiredFieldValidator7.Enabled = true;
        //        }
        //    else if (ddlstatus.SelectedValue == "InterviewDate")
        //    {
               
        //        txtReson.Visible = false;
        //        dtInterviewDate.Visible = true;
        //        dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
        //        ddlInsteviewtime.Visible = true;
        //        ddlInsteviewtime.SelectedValue = "10:00 AM";
        //    }
        //    else  if (ddlstatus.SelectedValue == "Deactive")
        //    {
               
        //        dtInterviewDate.Visible = false;
        //        ddlInsteviewtime.Visible = false;
        //        txtReson.Visible = true;
        //        RequiredFieldValidator7.Enabled = true;
        //    }
        //    else
        //    {
                
        //        txtReson.Visible = false;
        //        dtInterviewDate.Visible = false;
        //        ddlInsteviewtime.Visible = false;
        //    }
        //    if (ddlstatus.SelectedValue == "Active" && (Convert.ToString(Session["usertype"]) != "Admin" && Convert.ToString(Session["usertype"]) != "SM"))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
        //        if (Convert.ToString(Session["ddlStatus"]) != "")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
        //        }
        //        else
        //        {
        //            ddlstatus.SelectedValue = "Applicant";
        //        }
        //        return;
        //    }
        //    if (ddlstatus.SelectedValue == "Deactive" && (Convert.ToString(Session["usertype"]) != "Admin" && Convert.ToString(Session["usertype"]) != "SM"))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
        //        if (Convert.ToString(Session["ddlStatus"]) != "")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
        //        }
        //        else
        //        {
        //            ddlstatus.SelectedValue = "Applicant";
        //        }
        //        return;
        //    }

        //    if (ddlstatus.SelectedValue == "Active")
        //    {
        //        //pnlFngPrint.Visible = true;
        //        //pnlGrid.Visible = true;
        //        //pnl4.Visible = false;
        //        //pnlnewHire.Visible = true;
        //        //pnlNew2.Visible = true;
        //        //btnNewPluse.Visible = false;
        //        //btnNewMinus.Visible = true;
        //    }
        //    else
        //    {
        //        //pnlFngPrint.Visible = false;
        //        //pnlGrid.Visible = false;
        //        //pnlnewHire.Visible = false;
        //        //pnlNew2.Visible = false;
        //        //btnNewPluse.Visible = true;
        //        //btnNewMinus.Visible = false;
        //        //pnl4.Visible = true;
        //    }

        //    if (ddlstatus.SelectedValue == "Applicant" || ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "PhoneScreened")
        //    {
        //        btnMinusNew.Visible = true;
        //        btnPlusNew.Visible = false;
        //        Panel3.Visible = true;
        //        Panel4.Visible = true;
        //        pnlNew.Visible = true;
        //    }
        //    else
        //    {
        //        btnMinusNew.Visible = false;
        //        btnPlusNew.Visible = true;
        //        Panel3.Visible = false;
        //        Panel4.Visible = false;
        //        pnlNew.Visible = false;
        //    }

        //    if (ddlstatus.SelectedValue == "Active" && (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer"))
        //    {
        //        //pnlAll.Visible = true;
        //    }
        //    else
        //    {
        //        //pnlAll.Visible = false;
        //    }

        //    //if (ddlstatus.SelectedValue == "Active")
        //    //{
        //    //    lblReqEIN.Visible = true;
        //    //    rqEin1.Enabled = true;
        //    //    rqEIN2.Enabled = true;
        //    //    rqDesignition.Enabled = true;
        //    //    rqPrimaryTrade.Enabled = true;
        //    //    rqSecondaryTrade.Enabled = true;
        //    //    rqFirstName.Enabled = true;
        //    //    rqEmail.Enabled = true;
        //    //    //rvEmail.Enabled = true;
        //    //    rqZip.Enabled = true;
        //    //    rqState.Enabled = true;
        //    //    lblStateReq.Visible = true;
        //    //    rqCity.Enabled = true;
        //    //    lblCityReq.Visible = true;
        //    //    password.Enabled = true;
        //    //    rqPass.Enabled = true;
        //    //    //rqMaritalStatus.Enabled = true;
        //    //    rqNotes.Enabled = true;
        //    //    lblNotesReq.Visible = true;
        //    //    lblNotesReq.Visible = true;
        //    //    rqSource.Enabled = true;
        //    //    //lblSource.Visible = true;
        //    //    //rfLastName.Enabled = true;
        //    //    rqAddress.Enabled = true;
        //    //    lblAddressReq.Visible = true;
        //    //    rqPhone.Enabled = true;
        //    //    lblPhoneReq.Visible = true;
        //    //    rqPass.Enabled = true;
        //    //    rqSSN1.Enabled = true;
        //    //    rqSSN2.Enabled = true;
        //    //    rqSSN3.Enabled = true;
        //    //    rqDOB.Enabled = true;
        //    //    rqPenalty.Enabled = true;
        //    //    lblReqSSN.Visible = true;
        //    //    rqSSN1.Enabled = true;
        //    //    rqSSN2.Enabled = true;
        //    //    rqSSN3.Enabled = true;
        //    //}
        //    //else
        //    //{
        //    //    lblReqEIN.Visible = false;
        //    //    rqEin1.Enabled = false;
        //    //    rqEIN2.Enabled = false;
        //    //    rqDesignition.Enabled = false;
        //    //    rqPrimaryTrade.Enabled = true;
        //    //    rqSecondaryTrade.Enabled = true;
        //    //    rqFirstName.Enabled = true;
        //    //    rqEmail.Enabled = true;
        //    //    //rvEmail.Enabled = true;
        //    //    rqZip.Enabled = true;
        //    //    rqState.Enabled = true;
        //    //    lblStateReq.Visible = true;
        //    //    rqCity.Enabled = true;
        //    //    lblCityReq.Visible = true;
        //    //    password.Enabled = true;
        //    //    rqPass.Enabled = false;
        //    //    //rqMaritalStatus.Enabled = false;
        //    //    rqNotes.Enabled = false;
        //    //    lblNotesReq.Visible = false;
        //    //    lblNotesReq.Visible = false;
        //    //    rqSource.Enabled = true;
        //    //    //lblSource.Visible = true;
        //    //    //lblSource.Visible = true;
        //    //    //rfLastName.Enabled = true;
        //    //    rqAddress.Enabled = true;
        //    //    lblAddressReq.Visible = true;
        //    //    rqPhone.Enabled = true;
        //    //    lblPhoneReq.Visible = true;
        //    //    rqPass.Enabled = false;
        //    //    rqSSN1.Enabled = false;
        //    //    rqSSN2.Enabled = false;
        //    //    rqSSN3.Enabled = false;
        //    //    rqDOB.Enabled = false;
        //    //    rqPenalty.Enabled = false;
        //    //    lblReqSSN.Visible = false;
        //    //    rqSSN1.Enabled = false;
        //    //    rqSSN2.Enabled = false;
        //    //    rqSSN3.Enabled = false;
        //    //}

        //    #region NewRequiredFields
        //    //if (ddlstatus.SelectedValue == "Install Prospect")
        //    //{
        //    //    rqDesignition.Enabled = false;
        //    //    RequiredFieldValidator3.Enabled = false;
        //    //    lblReqDesig.Visible = false;
        //    //    lblReqPtrade.Visible = true;
        //    //    rqPrimaryTrade.Enabled = true;
        //    //    lblReqSTrate.Visible = true;
        //    //    rqSecondaryTrade.Enabled = true;
        //    //    lblReqLastName.Visible = true;
        //    //    rqLastName.Enabled = true;
        //    //    RequiredFieldValidator5.Enabled = true;
        //    //    lblReqFName.Visible = true;
        //    //    rqFirstName.Enabled = true;
        //    //    RequiredFieldValidator4.Enabled = true;
        //    //    lblPhoneReq.Visible = true;
        //    //    rqPhone.Enabled = true;
        //    //    lblSourceReq.Visible = true;
        //    //    rqSource.Enabled = true;
        //    //    lblNotesReq.Visible = false;
        //    //    rqNotes.Enabled = false;
        //    //    lblReqEmail.Visible = false;
        //    //    rqEmail.Enabled = false;
        //    //    reEmail.Enabled = true;
        //    //    lblReqZip.Visible = false;
        //    //    rqZip.Enabled = false;
        //    //    lblStateReq.Visible = false;
        //    //    rqState.Enabled = false;
        //    //    lblCityReq.Visible = false;
        //    //    rqCity.Enabled = false;
        //    //    lblPassReq.Visible = false;
        //    //    rqPass.Enabled = false;
        //    //    lblReqSig.Visible = false;
        //    //    //rqSign.Enabled = false;
        //    //    //lblReqMarSt.Visible = false;
        //    //    //rqMaritalStatus.Enabled = false;
        //    //    lblReqPicture.Visible = false;
        //    //    lblReqDL.Visible = false;
        //    //    lblAddressReq.Visible = false;
        //    //    rqAddress.Enabled = false;
        //    //    Label1.Visible = false;
        //    //    RequiredFieldValidator6.Enabled = false;
        //    //    lblConfirmPass.Visible = true;
        //    //    rqConPass.Enabled = false;
        //    //    lblReqSSN.Visible = false;
        //    //    rqSSN1.Enabled = false;
        //    //    rqSSN2.Enabled = false;
        //    //    rqSSN3.Enabled = false;
        //    //    //lblReqDOB.Visible = false;
        //    //    rqDOB.Enabled = false;
        //    //    lblReqPOP.Visible = false;
        //    //    rqPenalty.Enabled = false;
        //    //    rqSource.Enabled = true;
        //    //    lblConfirmPass.Visible = false;
        //    //}
        //    if (ddlstatus.SelectedValue == "Applicant")
        //    {
        //        rqDesignition.Enabled = false;
        //        RequiredFieldValidator3.Enabled = false;
        //        //lblReqDesig.Visible = false;
        //        lblReqPtrade.Visible = true;
        //        rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        rqLastName.Enabled = true;
        //        RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //      //  rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        rqNotes.Enabled = false;
        //        lblReqEmail.Visible = false;
        //        rqEmail.Enabled = false;
        //        reEmail.Enabled = true;
        //        lblReqZip.Visible = false;
        //        rqZip.Enabled = false;
        //        lblStateReq.Visible = false;
        //        rqState.Enabled = false;
        //        lblCityReq.Visible = false;
        //        rqCity.Enabled = false;
        //        lblPassReq.Visible = false;
        //        rqPass.Enabled = false;
        //        lblReqSig.Visible = false;
        //        //rqSign.Enabled = false;
        //        //lblReqMarSt.Visible = false;
        //        //rqMaritalStatus.Enabled = false;
        //        lblReqPicture.Visible = false;
        //        lblReqDL.Visible = false;
        //        lblAddressReq.Visible = false;
        //        rqAddress.Enabled = false;
        //        Label1.Visible = false;
        //        RequiredFieldValidator6.Enabled = false;
        //        lblConfirmPass.Visible = true;
        //        rqConPass.Enabled = false;
        //        lblReqSSN.Visible = false;
        //        rqSSN1.Enabled = false;
        //        rqSSN2.Enabled = false;
        //        rqSSN3.Enabled = false;
        //        //lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //     //   rqSource.Enabled = true;
        //        lblConfirmPass.Visible = false;
        //    }
        //    else if (ddlstatus.SelectedValue == "OfferMade")
        //    {
                
        //        rqDesignition.Enabled = true;
        //        RequiredFieldValidator3.Enabled = true;
        //        //lblReqDesig.Visible = true;
        //        lblReqPtrade.Visible = true;
        //        rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        rqLastName.Enabled = true;
        //        RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //    //    rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        rqNotes.Enabled = false;
        //        lblReqEmail.Visible = false;
        //        rqEmail.Enabled = false;
        //        reEmail.Enabled = true;
        //        lblReqZip.Visible = true;
        //        rqZip.Enabled = true;
        //        lblStateReq.Visible = true;
        //        rqState.Enabled = true;
        //        lblCityReq.Visible = true;
        //        rqCity.Enabled = true;
        //        lblPassReq.Visible = false;
        //        rqPass.Enabled = false;
        //        lblReqSig.Visible = false;
        //        //rqSign.Enabled = false;
        //        //lblReqMarSt.Visible = false;
        //        //rqMaritalStatus.Enabled = false;
        //        lblReqPicture.Visible = false;
        //        lblReqDL.Visible = false;

        //        #region Req from NewHire
        //        lblAddressReq.Visible = true;
        //        rqAddress.Enabled = true;
        //        Label1.Visible = true;
        //        RequiredFieldValidator6.Enabled = true;
        //        lblConfirmPass.Visible = true;
        //        rqConPass.Enabled = false;
        //        lblReqSSN.Visible = false;
        //        rqSSN1.Enabled = false;
        //        rqSSN2.Enabled = false;
        //        rqSSN3.Enabled = false;
        //        //lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //     //   rqSource.Enabled = true;
        //        lblConfirmPass.Visible = false;
        //        //lblReqHireDate.Visible = true;
        //        //rqHireDate.Enabled = true;
        //        //lblReqWWC.Visible = true;
        //        //rqWorkCompCode.Enabled = true;
        //        //lblReqPayRates.Visible = true;
        //        //rqPayRate.Enabled = true;
        //        //lblReqEmpType.Visible = true;
        //        //rqEmpType.Enabled = true;
        //        txtReson.Visible = true;
        //        RequiredFieldValidator7.Enabled = true;

        //        #endregion
        //    }
        //    else if (ddlstatus.SelectedValue == "Active")
        //    {
        //        rqDesignition.Enabled = true;
        //        RequiredFieldValidator3.Enabled = true;
        //        //lblReqDesig.Visible = true;
        //        lblReqPtrade.Visible = true;
        //        rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        rqLastName.Enabled = true;
        //        RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //      //  rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        rqNotes.Enabled = false;
        //        lblReqEmail.Visible = false;
        //        rqEmail.Enabled = false;
        //        reEmail.Enabled = true;
        //        lblReqZip.Visible = true;
        //        rqZip.Enabled = true;
        //        lblStateReq.Visible = true;
        //        rqState.Enabled = true;
        //        lblCityReq.Visible = true;
        //        rqCity.Enabled = true;
        //        lblPassReq.Visible = true;
        //        rqPass.Enabled = true;
        //        lblReqSig.Visible = false;
        //        lblConfirmPass.Visible = true;
        //        password.Enabled = true;
        //        rqConPass.Enabled = true;
        //        //rqSign.Enabled = false;
        //        //lblReqMarSt.Visible = true;
        //        //rqMaritalStatus.Enabled = true;
        //        lblReqPicture.Visible = false;
        //        lblReqDL.Visible = false;

        //        #region Req from NewHire
        //        lblAddressReq.Visible = true;
        //        rqAddress.Enabled = true;
        //        Label1.Visible = true;
        //        RequiredFieldValidator6.Enabled = true;
        //        lblConfirmPass.Visible = true;
        //        rqConPass.Enabled = false;
        //        lblReqSSN.Visible = true;
        //        rqSSN1.Enabled = true;
        //        rqSSN2.Enabled = true;
        //        rqSSN3.Enabled = true;
        //        //lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = true;
        //        rqPenalty.Enabled = true;
        //      //  rqSource.Enabled = true;
        //        lblConfirmPass.Visible = false;
        //        //lblReqHireDate.Visible = true;
        //        //rqHireDate.Enabled = true;
        //        //lblReqWWC.Visible = true;
        //        //rqWorkCompCode.Enabled = true;
        //        //lblReqPayRates.Visible = true;
        //        //rqPayRate.Enabled = true;
        //        //lblReqEmpType.Visible = true;
        //        //rqEmpType.Enabled = true;
        //        txtReson.Visible = true;
        //        RequiredFieldValidator7.Enabled = true;
        //        #endregion

        //    }
        //    else if (ddlstatus.SelectedValue == "Deactive")
        //    {
        //        rqDesignition.Enabled = false;
        //        RequiredFieldValidator3.Enabled = false;
        //        lblReqDesig.Visible = false;
        //        lblReqPtrade.Visible = false;
        //        rqPrimaryTrade.Enabled = false;
        //        lblReqSTrate.Visible = false;
        //        rqSecondaryTrade.Enabled = false;
        //        lblReqLastName.Visible = false;
        //        rqLastName.Enabled = false;
        //        RequiredFieldValidator5.Enabled = false;
        //        lblReqFName.Visible = false;
        //        rqFirstName.Enabled = false;
        //        RequiredFieldValidator4.Enabled = false;
        //        lblPhoneReq.Visible = false;
        //        rqPhone.Enabled = false;
        //        lblSourceReq.Visible = false;
        //      //  rqSource.Enabled = false;
        //        lblNotesReq.Visible = false;
        //        rqNotes.Enabled = false;
        //        lblReqEmail.Visible = false;
        //        rqEmail.Enabled = false;
        //        reEmail.Enabled = false;
        //        lblReqZip.Visible = false;
        //        rqZip.Enabled = false;
        //        lblStateReq.Visible = false;
        //        rqState.Enabled = false;
        //        lblCityReq.Visible = false;
        //        rqCity.Enabled = false;
        //        lblPassReq.Visible = false;
        //        rqPass.Enabled = false;
        //        lblReqSig.Visible = false;
        //        lblConfirmPass.Visible = false;
        //        password.Enabled = false;
        //        rqConPass.Enabled = false;
        //        lblReqPicture.Visible = false;
        //        lblReqDL.Visible = false;
        //        //txtDateSourced.Text = DateTime.Now.ToShortDateString();
        //        #region Req from NewHire
        //        lblAddressReq.Visible = false;
        //        rqAddress.Enabled = false;
        //        Label1.Visible = false;
        //        RequiredFieldValidator6.Enabled = false;
        //        lblConfirmPass.Visible = false;
        //        rqConPass.Enabled = false;
        //        lblReqSSN.Visible = false;
        //        rqSSN1.Enabled = false;
        //        rqSSN2.Enabled = false;
        //        rqSSN3.Enabled = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //       // rqSource.Enabled = false;
        //        lblConfirmPass.Visible = false;
        //        RequiredFieldValidator7.Enabled = true;
        //        #endregion
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
        //        //return;
        //    }
        //    #endregion


        //}
        protected void No_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender2.Hide();
            ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "ClosePopup();", true);
            //RadWindow_ExisatingRecord.VisibleOnPageLoad = false;
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
                objuser.fristname = txtfirstname.Text;
                objuser.lastname = txtlastname.Text;
                objuser.email = txtemail.Text.Trim();
                objuser.address = txtaddress.Text;
                objuser.zip = txtZip.Text;
                objuser.state = txtState.Text;
                objuser.city = txtCity.Text;
                objuser.password = txtpassword.Text;
                objuser.designation = ddldesignation.SelectedItem.Text;
                objuser.status = ddlstatus.SelectedValue;
                objuser.phone = txtPhone.Text;
                // if (lstboxUploadedImages.Items.Count != 0)
                // {
                //     foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                //    {
                //       fn = fn + "," + img.Text;
                ////   }
                //   objuser.picture = fn.TrimStart(',');
                //  }
                // else if(lstboxUploadedImages.Items.Count>0)
                //  {
                //       foreach (System.Web.UI.WebControls.ListItem img in lstboxUploadedImages.Items)
                //       {
                //           fn = fn + "," + img.Text;
                //       }
                //       objuser.picture = fn.TrimStart(',');
                //  }            
                //else
                //  {
                objuser.picture = Convert.ToString(Session["UplaodPicture"]);
                //  }
                objuser.attachements = GetUpdateAttachments();
                objuser.businessname = "";
                objuser.ssn = txtssn.Text;
                objuser.ssn1 = txtssn0.Text;
                objuser.ssn2 = txtssn1.Text;
                objuser.signature = txtSignature.Text;
                objuser.dob = DOBdatepicker.Text;
                objuser.citizenship = ddlcitizen.SelectedValue;
                // objuser.tin = txtTIN.Text;
                objuser.ein1 = "";
                objuser.ein2 = "";
                //objuser.a = txtA.Text;
                //objuser.b = txtB.Text;
                //objuser.c = txtC.Text;
                //objuser.d = txtD.Text;
                //objuser.e = txtE.Text;
                //objuser.f = txtF.Text;
                //objuser.g = txtG.Text;
                //objuser.h = txtH.Text;
                //objuser.i = txt5.Text;
                //objuser.j = txt6.Text;
                //objuser.k = txt7.Text;
                //objuser.maritalstatus = ddlmaritalstatus.SelectedValue;
                objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                objuser.Source = ddlSource.SelectedValue;
                objuser.Notes = txtNotes.Text;
                if (txtReson.Visible == true && ddlstatus.SelectedValue != "InterviewDate")
                {
                    objuser.Reason = txtReson.Text;
                }
                else if (dtInterviewDate.Visible == true && ddlstatus.SelectedValue == "InterviewDate")
                {
                    objuser.Reason = Convert.ToString(dtInterviewDate.Text);
                }
                else
                {
                    objuser.Reason = "";
                }
                objuser.PqLicense = Convert.ToString(Session["PqLicense"]);
                objuser.WorkersComp = Convert.ToString(Session["WorkersComp"]);
                objuser.GeneralLiability = Convert.ToString(Session["flpGeneralLiability"]);
                //objuser.HireDate = txtHireDate.Text;
                //objuser.TerminitionDate = dtResignation.Text;
                //objuser.WorkersCompCode = Convert.ToString(ddlWorkerCompCode.Text);
                //objuser.NextReviewDate = dtReviewDate.Text;
                //objuser.EmpType = ddlEmpType.SelectedValue;
                //objuser.LastReviewDate = dtLastDate.Text;
                //objuser.PayRates = txtPayRates.Text;
                objuser.ExtraEarning = Convert.ToString(Session["ExtraIncomeName"]);
               // objuser.ExtraEarningAmt = Convert.ToString(Session["ExtraIncomeAmt"]);
                //if (rdoCheque.Checked)
                //{
                //    objuser.PayMethod = "Check";
                //}
                //else
                //{
                //    objuser.PayMethod = "Direct Deposite";
                //}
                if (Convert.ToString(Session["DeductionType"]) == "")
                {
                    //if (rdoCheque.Checked)
                    //{
                    //    objuser.DeductionType = "Check";
                    //}
                    //else
                    //{
                    //    objuser.DeductionType = "Direct Deposite";
                    //}
                }
                else
                {
                    objuser.DeductionType = Convert.ToString(Session["DeductionType"]);
                }

                if (Convert.ToString(Session["DeductionAmount"]) == "")
                {
                    //if (txtDeduction.Text != "")
                    //{
                    //    objuser.Deduction = txtDeduction.Text;
                    //}
                    //else
                    //{
                    //    objuser.Deduction = "0";
                    //}
                }
                else
                {
                    objuser.Deduction = Convert.ToString(Session["DeductionAmount"]);
                }
                if (Convert.ToString(Session["DeductionReason"]) == "")
                {
                    //objuser.DeductionReason = txtDeducReason.Text;
                }
                else
                {
                    objuser.DeductionReason = Convert.ToString(Session["DeductionReason"]);
                }
                //objuser.AbaAccountNo = txtRoutingNo.Text;
                //objuser.AccountNo = txtAccountNo.Text;
                //objuser.AccountType = txtAccountType.Text;
                objuser.PTradeOthers = txtOtherTrade.Text;
                objuser.STradeOthers = txtSecTradeOthers.Text;
                //objuser.DateSourced = txtDateSourced.Text;
                int id = Convert.ToInt32(Session["ID"]);
                objuser.str_SuiteAptRoom = txtSuiteAptRoom.Text;
                if (rdoAttchmentYes.Checked)
                {
                    objuser.skillassessmentstatus = true;
                }
                else if (rdoAttchmentNo.Checked)
                {
                    objuser.skillassessmentstatus = false;
                }
                objuser.assessmentPath = Convert.ToString(Session["SkillAttachment"]);
                if (txtFullTimePos.Text != "")
                {
                    objuser.FullTimePosition = Convert.ToInt32(txtFullTimePos.Text);
                }
                objuser.ContractorsBuilderOwner = txtContractor1.Text + "#" + txtContractor2.Text + "#" + txtContractor3.Text;
                objuser.MajorTools = txtMajorTools.Text;
                if (rdoDrugtestYes.Checked)
                {
                    objuser.DrugTest = true;
                }
                else if (rdoDrugtestNo.Checked)
                {
                    objuser.DrugTest = false;
                }
                if (rdoDriveLicenseYes.Checked)
                {
                    objuser.ValidLicense = true;
                }
                else if (rdoDriveLicenseNo.Checked)
                {
                    objuser.ValidLicense = false;
                }
                if (rdoTruckToolsYes.Checked)
                {
                    objuser.TruckTools = true;
                }
                else if (rdoTruckToolsNo.Checked)
                {
                    objuser.TruckTools = false;
                }
                if (rdoJMApplyYes.Checked)
                {
                    objuser.PrevApply = true;
                }
                else if (rdoJMApplyYes.Checked)
                {
                    objuser.PrevApply = false;
                }
                //if (rdoLicenseYes.Checked)
                //{
                //    objuser.LicenseStatus = true;
                //}
                //else if (rdoLicenseNo.Checked)
                //{
                //    objuser.LicenseStatus = false;
                //}
                if (rdoGuiltyYes.Checked)
                {
                    objuser.CrimeStatus = true;
                }
                else if (rdoGuiltyNo.Checked)
                {
                    objuser.CrimeStatus = false;
                }
                objuser.StartDate = Convert.ToString(txtStartDateNew.Text);
                objuser.SalaryReq = txtSalRequirement.Text;
                objuser.Avialability = txtAvailability.Text;
                //objuser.WarrentyPolicy = txtWarrantyPolicy.Text;
                if (txtYrs.Text != "")
                {
                    objuser.businessYrs = Convert.ToDouble(txtYrs.Text);
                }
                if (txtCurrentComp.Text != "")
                {
                    objuser.underPresentComp = Convert.ToDouble(txtCurrentComp.Text);
                }
                objuser.websiteaddress = txtWebsiteUrl.Text;
                objuser.ResumePath = Convert.ToString(Session["ResumePath"]);
                objuser.CirtificationTraining = Convert.ToString(Session["CirtificationPath"]);
                objuser.PersonName = Convert.ToString(Session["PersonName"]);
                objuser.PersonType = Convert.ToString(Session["PersonType"]);
                objuser.CompanyPrinciple = txtPrinciple.Text;
                //if (ddldesignation.SelectedValue != "0" && ddlInstallerType.Visible == true)
                //{
                //    objuser.InstallerType = ddlInstallerType.SelectedValue;
                //}
                if (ddlstatus.SelectedValue == "InterviewDate")
                {
                    objuser.InterviewTime = ddlInsteviewtime.SelectedItem.Text;
                }
                else
                {
                    objuser.InterviewTime = "";
                }
                if (ddlstatus.SelectedValue == "Active")
                {
                    objuser.ActivationDate = DateTime.Today.ToShortDateString();
                    objuser.UserActivated = Convert.ToString(Session["Username"]);
                }
                else
                {
                    objuser.ActivationDate = "";
                    objuser.UserActivated = "";
                }
                bool result = InstallUserBLL.Instance.UpdateInstallUser(objuser, Convert.ToInt32(Session["EmailEdiId"]));
                GoogleCalendarEvent.CreateCalendar(txtemail.Text, txtaddress.Text);
                //lblmsg.Visible = true;
                //lblmsg.CssClass = "success";
                //lblmsg.Text = "User has been created successfully";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('InstallUser  Update successfully');", true);
                clearcontrols();
                if (ddldesignation.SelectedItem.Text == "Installer")
                {
                    //lblInstallerType.Visible = true;
                    //ddlInstallerType.Visible = true;
                }
                else
                {
                    //lblInstallerType.Visible = false;
                    //ddlInstallerType.Visible = false;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('InstallUser  Update successfully.');window.location ='EditInstallUser.aspx';", true);
                //Server.Transfer("EditInstallUser.aspx");
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnPluse_Click(object sender, EventArgs e)
        {
            pnl4.Visible = true;
            btnPluse.Visible = false;
            btnMinus.Visible = true;
        }

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            pnl4.Visible = false;
            btnPluse.Visible = true;
            btnMinus.Visible = false;
        }

        protected void btnGeneralLiability_Click(object sender, EventArgs e)
        {
            if (flpGeneralLiability.HasFile)
            {
                string filename = Path.GetFileName(flpGeneralLiability.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpGeneralLiability.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["flpGeneralLiability"] = null;
                Session["flpGeneralLiabilityName"] = null;
                Session["flpGeneralLiabilityName"] = filename;
                Session["flpGeneralLiability"] = "~/Sr_App/UploadedFile/" + filename;
                //lblPL.Text = "Uploaded file Name: " + filename;
                //lblGL.Text = "Uploaded file Name: " + filename;
                FillDocument();
            }
            else
            {
                //lblGL.Text = "";
                Session["flpGeneralLiabilityName"] = "";
                Session["flpGeneralLiability"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload General Liability.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected void btnPqLicense_Click(object sender, EventArgs e)
        {
            if (flpPqLicense.HasFile)
            {
                string filename = Path.GetFileName(flpPqLicense.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpPqLicense.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["PqLicense"] = null;
                Session["PqLicenseFileName"] = null;
                Session["PqLicenseFileName"] = filename;
                Session["PqLicense"] = "~/Sr_App/UploadedFile/" + filename;
                
                //lblPL.Text = "Uploaded file Name: " + filename;
                FillDocument();
            }
            else
            {
                //lblPL.Text = "";
                Session["PqLicense"] = "";
                Session["PqLicenseFileName"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload DL License.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected void btnWorkersComp_Click(object sender, EventArgs e)
        {
            if (flpWorkersComp.HasFile)
            {
                string filename = Path.GetFileName(flpWorkersComp.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpWorkersComp.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["WorkersComp"] = null;
                Session["WorkersCompFileName"] = null;
                Session["WorkersCompFileName"] = filename;
                Session["WorkersComp"] = "~/Sr_App/UploadedFile/" + filename;
                lblWC.Text = "Uploaded file Name: " + filename;
                FillDocument();
            }
            else
            {

                lblWC.Text = "";
                Session["WorkersComp"] = "";
                Session["WorkersCompFileName"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Workers Comp.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected void btn_UploadFiles_Click1(object sender, EventArgs e)
        {
            if (flpUploadFiles.HasFile)
            {
                string filename = Path.GetFileName(flpUploadFiles.PostedFile.FileName);
                flpUploadFiles.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["UploadFiles"] = null;
                Session["UploadFiles"] = "~/Sr_App/UploadedFile/" + filename;
                lblUF.Text = "Uploaded file Name: " + filename;
            }
            else
            {
                lblUF.Text = "";
                Session["UploadFiles"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Attachment.');", true);
            }
        }

        protected void btnNewPluse_Click(object sender, EventArgs e)
        {
            //pnlnewHire.Visible = true;
            //pnlNew2.Visible = true;
            //btnNewPluse.Visible = false;
            //btnNewMinus.Visible = true;
            //pnlFngPrint.Visible = true;
            //pnlGrid.Visible = true;
        }

        protected void btnNewMinus_Click(object sender, EventArgs e)
        {
            //pnlFngPrint.Visible = false;
            //pnlGrid.Visible = false;
            //pnlnewHire.Visible = false;
            //pnlNew2.Visible = false;
            //btnNewPluse.Visible = true;
            //btnNewMinus.Visible = false;
        }

        protected void btnAddExtraIncome_Click(object sender, EventArgs e)
        {
            Double extraincome;
            String strExtraincomeName;
            if (Convert.ToString(Session["ExtraIncomeName"]) == "")
            {
                //strExtraincomeName = ddlExtraEarning.SelectedItem.Text;
                //ddlExtraEarning.SelectedIndex = 0;
            }
            else
            {
                //strExtraincomeName = Convert.ToString(Session["ExtraIncomeName"]);
                //Session["ExtraIncomeName"] = strExtraincomeName + ", " + ddlExtraEarning.SelectedItem.Text;
                //ddlExtraEarning.SelectedIndex = 0;
            }
            if (Convert.ToString(Session["ExtraIncomeAmt"]) == "")
            {
                //extraincome = Convert.ToDouble(txtExtraIncome.Text);
                //txtExtraIncome.Text = "";
            }
            else
            {
                //extraincome = Convert.ToDouble(Session["ExtraIncomeAmt"]) + Convert.ToDouble(txtExtraIncome.Text);
                //Session["ExtraIncomeAmt"] = extraincome;
                //txtExtraIncome.Text = "";
            }
        }

        protected void btnAddType_Click(object sender, EventArgs e)
        {

        }

        protected void ddldesignation_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddldesignation.SelectedItem.Text != "SubContractor" && (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer"))
            {
                if (Request.QueryString["ID"] != null)
                {
                    Response.Redirect("~/Sr_App/InstallCreateUser.aspx?ID=" + Request.QueryString["ID"]+"&Desig=" + ddldesignation.SelectedItem.Text);
                }
                else
                {
                    string PageData = "";
                    if (rdoAttchmentYes.Checked)
                    {
                        PageData = "AttchmentYes";
                    }
                    else if (rdoAttchmentNo.Checked)
                    {
                        PageData = "AttchmentNo";
                    }
                    else
                    {
                        PageData = "NotGiven";
                    }
                    PageData = PageData + "," + txtContractor1.Text;
                    PageData = PageData + "," + txtContractor2.Text;
                    PageData = PageData + "," + txtContractor3.Text;
                    if (rdoDrugtestYes.Checked)
                    {
                        PageData = PageData + ",rdoDrugtestYes";
                    }
                    else if (rdoDrugtestNo.Checked)
                    {
                        PageData = PageData + ",rdoDrugtestNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    if (rdoTruckToolsYes.Checked)
                    {
                        PageData = PageData + ",rdoTruckToolsYes";
                    }
                    else if (rdoTruckToolsNo.Checked)
                    {
                        PageData = PageData + ",rdoTruckToolsNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    PageData = PageData + "," + txtStartDateNew.Text;
                    PageData = PageData + "," + txtAvailability.Text;
                    PageData = PageData + "," + txtPrinciple.Text;
                    PageData = PageData + "," + txtFullTimePos.Text;
                    PageData = PageData + "," + txtMajorTools.Text;
                    if (rdoDriveLicenseYes.Checked)
                    {
                        PageData = PageData + ",DriveLicenseYes";
                    }
                    else if (rdoDriveLicenseNo.Checked)
                    {
                        PageData = PageData + ",rdoDriveLicenseNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    if (rdoJMApplyYes.Checked)
                    {
                        PageData = PageData + ",JMApplyYes";
                    }
                    else if (rdoJMApplyNo.Checked)
                    {
                        PageData = PageData + ",JMApplyNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    if (rdoGuiltyYes.Checked)
                    {
                        PageData = PageData + ",GuiltyYes";
                    }
                    else if (rdoGuiltyNo.Checked)
                    {
                        PageData = PageData + ",rdoGuiltyNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    PageData = PageData + "," + txtSalRequirement.Text;
                    PageData = PageData + "," + ddlType.SelectedValue;
                    PageData = PageData + "," + txtName.Text;
                    if (rdoWarrantyYes.Checked)
                    {
                        PageData = PageData + ",rdoWarrantyYes";
                    }
                    else if (rdoWarrantyNo.Checked)
                    {
                        PageData = PageData + ",rdoWarrantyNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    PageData = PageData + "," + txtCurrentComp.Text;
                    PageData = PageData + "," + txtWarentyTimeYrs.Text;
                    PageData = PageData + "," + txtYrs.Text;
                    PageData = PageData + "," + txtCEO.Text;
                    PageData = PageData + "," + txtLeagalOfficer.Text;
                    PageData = PageData + "," + txtSoleProprietorShip.Text;
                    PageData = PageData + "," + txtPartnetsName.Text;
                    PageData = PageData + "," + ddlBusinessType.SelectedValue;
                    if (rdoBusinessMinorityYes.Checked)
                    {
                        PageData = PageData + ",rdoBusinessMinorityYes";
                    }
                    else if (rdoWarrantyNo.Checked)
                    {
                        PageData = PageData + ",rdoBusinessMinorityNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    if (rdoWomenYes.Checked)
                    {
                        PageData = PageData + ",rdoWomenYes";
                    }
                    else if (rdoWarrantyNo.Checked)
                    {
                        PageData = PageData + ",rdoWomenNo";
                    }
                    else
                    {
                        PageData = PageData + ",NotGiven";
                    }
                    PageData = PageData + "," + txtWebsiteUrl.Text;
                    PageData = PageData + "," + ddldesignation.SelectedValue;
                    PageData = PageData + "," + ddlPrimaryTrade.SelectedValue;
                    PageData = PageData + "," + txtOtherTrade.Text;
                    PageData = PageData + "," + ddlSecondaryTrade.SelectedValue;
                    PageData = PageData + "," + txtSecTradeOthers.Text;
                    PageData = PageData + "," + txtfirstname.Text;
                    PageData = PageData + "," + txtemail.Text;
                    PageData = PageData + "," + txtZip.Text;
                    PageData = PageData + "," + txtState.Text;
                    PageData = PageData + "," + txtCity.Text;
                    PageData = PageData + "," + txtpassword.Text;
                    PageData = PageData + "," + txtSignature.Text;
                    PageData = PageData + "," + ddlstatus.SelectedValue;
                    PageData = PageData + "," + txtReson.Text;
                    PageData = PageData + "," + dtInterviewDate.Text;
                    PageData = PageData + "," + txtDateSource.Text;
                    PageData = PageData + "," + txtNotes.Text;
                    PageData = PageData + "," + txtlastname.Text;
                    PageData = PageData + "," + ddlSource.SelectedValue;
                    PageData = PageData + "," + txtSource.Text;
                    PageData = PageData + "," + txtaddress.Text;
                    PageData = PageData + "," + txtMailingAddress.Text;
                    PageData = PageData + "," + txtSuiteAptRoom.Text;
                    PageData = PageData + "," + txtpassword1.Text;
                    PageData = PageData + "," + txtPhone.Text;
                    PageData = PageData + "," + txtssn.Text;
                    PageData = PageData + "," + txtssn0.Text;
                    PageData = PageData + "," + txtssn1.Text;
                    PageData = PageData + "," + DOBdatepicker.Text;
                    PageData = PageData + "," + ddlcitizen.SelectedValue;
                    Session["PageData"] = PageData;
                    PageData = string.Empty;
                    Response.Redirect("~/Sr_App/InstallCreateUser.aspx?Desig=" + ddldesignation.SelectedItem.Text+"&Toggle=Yes");
                }
                //Response.Redirect("InstallCreateUser.aspx?);
            }
        }

        protected void ddlPrimaryTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPrimaryTrade.SelectedValue == "17")
            {
                txtOtherTrade.Visible = true;
            }
            else
            {
                txtOtherTrade.Visible = false;
            }
        }

        protected void ddlSecondaryTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSecondaryTrade.SelectedValue == "17")
            {
                txtSecTradeOthers.Visible = true;
            }
            else
            {
                txtSecTradeOthers.Visible = false;
            }
        }

        protected void btnDeleteSource_Click(object sender, EventArgs e)
        {
            //if (txtSource.Text != "")
            //{
            if (ddlSource.SelectedItem.Text != "Select Source")
            {
                string source = ddlSource.SelectedItem.Text;

                DataSet ds = InstallUserBLL.Instance.CheckSource(source);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Source does not exists.')", true);
                }
                else
                {
                    InstallUserBLL.Instance.DeleteSource(txtSource.Text);
                    DataSet dsadd = InstallUserBLL.Instance.GetSource();
                    if (dsadd.Tables[0].Rows.Count > 0)
                    {
                        ddlSource.DataSource = dsadd.Tables[0];
                        ddlSource.DataTextField = "Source";
                        ddlSource.DataValueField = "Source";
                        ddlSource.DataBind();
                        ddlSource.Items.Insert(0, "Select Source");
                        ddlSource.SelectedIndex = 0;
                        txtSource.Text = "";
                    }
                    else
                    {
                        ddlSource.DataSource = dsadd;
                        ddlSource.DataBind();
                        ddlSource.Items.Add("Select Source");
                        ddlSource.SelectedIndex = 0;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select source to delete.')", true);
            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter value to delete')", true);
            //}
        }

        protected void btnAddSource_Click(object sender, EventArgs e)
        {
            if (txtSource.Text != "")
            {
                string source = txtSource.Text;
                DataSet ds = InstallUserBLL.Instance.CheckSource(source);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Source already exists.')", true);
                }
                else
                {
                    DataSet dsadd = InstallUserBLL.Instance.AddSource(source);
                    if (dsadd.Tables[0].Rows.Count > 0)
                    {
                        ddlSource.DataSource = dsadd.Tables[0];
                        ddlSource.DataTextField = "Source";
                        ddlSource.DataValueField = "Source";
                        ddlSource.DataBind();
                        ddlSource.Items.Insert(0, "Select Source");
                        ddlSource.SelectedValue = source;
                    }
                }
                txtSource.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter value to add.')", true);
            }
        }

        protected void btnPlusNew_Click(object sender, EventArgs e)
        {
            btnMinusNew.Visible = true;
            btnPlusNew.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = true;
            pnlNew.Visible = true;
            //pnlGrid2.Visible = true;
            //pnlGrid.Visible = true;
            lblSectionHeading.Visible = true;

        }

        protected void btnMinusNew_Click(object sender, EventArgs e)
        {
            btnMinusNew.Visible = false;
            btnPlusNew.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            pnlNew.Visible = false;
            //pnlGrid2.Visible = false;
            //pnlGrid.Visible = false;
            lblSectionHeading.Visible = false;
        }

        protected void btnUploadSkills_Click(object sender, EventArgs e)
        {
            if (flpSkillAssessment.HasFile)
            {
                string filename = Path.GetFileName(flpSkillAssessment.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpPqLicense.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["SkillAttachment"] = null;
                Session["SkillAttachmentName"] = null;
                Session["SkillAttachmentName"] = filename;
                Session["SkillAttachment"] = "~/Sr_App/UploadedFile/" + filename;
                FillDocument();
                txtFullTimePos.Enabled = false;
                txtContractor1.Enabled = false;
                txtContractor2.Enabled = false;
                txtContractor3.Enabled = false;
                txtMajorTools.Enabled = false;
                rdoDrugtestYes.Enabled = false;
                rdoDrugtestNo.Enabled = false;
                rdoDriveLicenseYes.Enabled = false;
                rdoDriveLicenseNo.Enabled = false;
                rdoTruckToolsYes.Enabled = false;
                rdoTruckToolsNo.Enabled = false;
                rdoJMApplyYes.Enabled = false;
                rdoJMApplyNo.Enabled = false;
                //rdoLicenseYes.Enabled = false;
                //rdoLicenseNo.Enabled = false;
                rdoGuiltyNo.Enabled = false;
                rdoGuiltyYes.Enabled = false;
                txtStartDateNew.Enabled = false;
                txtSalRequirement.Enabled = false;
                txtAvailability.Enabled = false;
                //txtWarrantyPolicy.Enabled = false;
                ddlType.Enabled = false;
                txtName.Enabled = false;
                txtPrinciple.Enabled = false;
                btnAddEmpPartner.Enabled = false;
                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                ddlBusinessType.Enabled = false;
                txtCEO.Enabled = false;
                txtLeagalOfficer.Enabled = false;
                txtPresident.Enabled = false;
                txtSoleProprietorShip.Enabled = false;
                txtPartnetsName.Enabled = false;
                //txtMailingAddress.Enabled = false;
                rdoWarrantyNo.Enabled = false;
                rdoWarrantyYes.Enabled = false;
                txtWarentyTimeYrs.Enabled = false;
                rdoBusinessMinorityNo.Enabled = false;
                rdoBusinessMinorityYes.Enabled = false;
                rdoWomenNo.Enabled = false;
                rdoWomenYes.Enabled = false;
                rdoAttchmentYes.Checked = true;
                if (ddlstatus.SelectedValue != "PhoneScreened")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
            }
            else
            {
                //lblPL.Text = "";
                Session["SkillAttachment"] = "";
                Session["SkillAttachmentName"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Skill assessment.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected void btnPSYes_Click(object sender, EventArgs e)
        {
            ddlstatus.SelectedValue = "PhoneScreened";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ClosePS()", true);
            return;
        }

        protected void btnPSNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ClosePS()", true);
            return;
        }

        protected void btnResume_Click(object sender, EventArgs e)
        {
            if (flpResume.HasFile)
            {
                string filename = Path.GetFileName(flpResume.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpPqLicense.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["ResumePath"] = null;
                Session["ResumeName"] = null;
                Session["ResumeName"] = filename;
                Session["ResumePath"] = "~/Sr_App/UploadedFile/" + filename;
                FillDocument();
            }
            else
            {
                //lblPL.Text = "";
                Session["ResumeName"] = "";
                Session["ResumePath"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload resume.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected void btnCirtification_Click(object sender, EventArgs e)
        {
            if (flpCirtification.HasFile)
            {
                string filename = Path.GetFileName(flpCirtification.PostedFile.FileName);
                filename = DateTime.Now.ToString() + filename;
                filename = filename.Replace("/", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace(" ", "");
                flpPqLicense.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
                Session["CirtificationName"] = null;
                Session["CirtificationPath"] = null;
                Session["CirtificationName"] = filename;
                Session["CirtificationPath"] = "~/Sr_App/UploadedFile/" + filename;
                FillDocument();
            }
            else
            {
                //lblPL.Text = "";
                Session["CirtificationName"] = "";
                Session["CirtificationPath"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Cirtification.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
        }

        protected void btnAddEmpPartner_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedIndex != 0)
            {
                DataTable dtNew = (DataTable)(Session["PersonTypeData"]);
                if (Convert.ToString(Session["PersonName"]) == "")
                {
                    Session["PersonName"] = txtName.Text;
                }
                else
                {
                    Session["PersonName"] = Convert.ToString(Session["PersonName"]) + "," + txtName.Text;
                }
                if (Convert.ToString(Session["PersonType"]) == "")
                {
                    Session["PersonType"] = ddlType.SelectedValue;
                }
                else
                {
                    Session["PersonType"] = Convert.ToString(Session["PersonType"]) + "," + Convert.ToString(ddlType.SelectedValue);
                }
                DataRow drNew = dtNew.NewRow();
                drNew["PersonName"] = txtName.Text;
                drNew["PersonType"] = ddlType.SelectedValue;
                dtNew.Rows.Add(drNew);
                Session["PersonTypeData"] = dtNew;
                GridView2.DataSource = dtNew;
                GridView2.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select User Type')", true);
                return;
            }
        }

        protected void rdoAttchmentYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAttchmentYes.Checked)
            {
                txtFullTimePos.Enabled = false;
                txtContractor1.Enabled = false;
                txtContractor2.Enabled = false;
                txtContractor3.Enabled = false;
                txtMajorTools.Enabled = false;
                rdoDrugtestYes.Enabled = false;
                rdoDrugtestNo.Enabled = false;
                rdoDriveLicenseYes.Enabled = false;
                rdoDriveLicenseNo.Enabled = false;
                rdoTruckToolsYes.Enabled = false;
                rdoTruckToolsNo.Enabled = false;
                rdoJMApplyYes.Enabled = false;
                rdoJMApplyNo.Enabled = false;
                //rdoLicenseYes.Enabled = false;
                //rdoLicenseNo.Enabled = false;
                rdoGuiltyNo.Enabled = false;
                rdoGuiltyYes.Enabled = false;
                txtStartDateNew.Enabled = false;
                txtSalRequirement.Enabled = false;
                txtAvailability.Enabled = false;
                //txtWarrantyPolicy.Enabled = false;
               
                ddlType.Enabled = false;
                txtName.Enabled = false;
                txtPrinciple.Enabled = false;
                btnAddEmpPartner.Enabled = false;
                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                ddlBusinessType.Enabled = false;
                txtCEO.Enabled = false;
                txtLeagalOfficer.Enabled = false;
                txtPresident.Enabled = false;
                txtSoleProprietorShip.Enabled = false;
                txtPartnetsName.Enabled = false;
                //txtMailingAddress.Enabled = false;
                rdoWarrantyNo.Enabled = false;
                rdoWarrantyYes.Enabled = false;
                txtWarentyTimeYrs.Enabled = false;
                rdoBusinessMinorityNo.Enabled = false;
                rdoBusinessMinorityYes.Enabled = false;
                rdoWomenNo.Enabled = false;
                rdoWomenYes.Enabled = false;
            }
            else
            {
                txtFullTimePos.Enabled = true;
                txtContractor1.Enabled = true;
                txtContractor2.Enabled = true;
                txtContractor3.Enabled = true;
                txtMajorTools.Enabled = true;
                rdoDrugtestYes.Enabled = true;
                rdoDrugtestNo.Enabled = true;
                rdoDriveLicenseYes.Enabled = true;
                rdoDriveLicenseNo.Enabled = true;
                rdoTruckToolsYes.Enabled = true;
                rdoTruckToolsNo.Enabled = true;
                rdoJMApplyYes.Enabled = true;
                rdoJMApplyNo.Enabled = true;
                //rdoLicenseYes.Enabled = true;
                //rdoLicenseNo.Enabled = true;
                rdoGuiltyNo.Enabled = true;
                rdoGuiltyYes.Enabled = true;
                txtStartDateNew.Enabled = true;
                txtSalRequirement.Enabled = true;
                txtAvailability.Enabled = true;
                //txtWarrantyPolicy.Enabled = true;
                txtYrs.Enabled = true;
                txtCurrentComp.Enabled = true;
                txtWebsiteUrl.Enabled = true;
                ddlType.Enabled = true;
                txtName.Enabled = true;
                txtPrinciple.Enabled = true;
                btnAddEmpPartner.Enabled = true;
                ddlBusinessType.Enabled = false;
                txtCEO.Enabled = false;
                txtLeagalOfficer.Enabled = false;
                txtPresident.Enabled = false;
                txtSoleProprietorShip.Enabled = false;
                txtPartnetsName.Enabled = false;
                //txtMailingAddress.Enabled = false;
                rdoWarrantyNo.Enabled = false;
                rdoWarrantyYes.Enabled = false;
                txtWarentyTimeYrs.Enabled = false;
                rdoBusinessMinorityNo.Enabled = false;
                rdoBusinessMinorityYes.Enabled = false;
                rdoWomenNo.Enabled = false;
                rdoWomenYes.Enabled = false;
            }
            //if (ddldesignation.SelectedItem.Text == "Installer")
            //{
            //    lblInstallerType.Visible = true;
            //    ddlInstallerType.Visible = true;
            //}
            //else
            //{
            //    lblInstallerType.Visible = false;
            //    ddlInstallerType.Visible = false;
            //}
        }

        protected void rdoAttchmentNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAttchmentYes.Checked)
            {
                txtFullTimePos.Enabled = false;
                txtContractor1.Enabled = false;
                txtContractor2.Enabled = false;
                txtContractor3.Enabled = false;
                txtMajorTools.Enabled = false;
                rdoDrugtestYes.Enabled = false;
                rdoDrugtestNo.Enabled = false;
                rdoDriveLicenseYes.Enabled = false;
                rdoDriveLicenseNo.Enabled = false;
                rdoTruckToolsYes.Enabled = false;
                rdoTruckToolsNo.Enabled = false;
                rdoJMApplyYes.Enabled = false;
                rdoJMApplyNo.Enabled = false;
                //rdoLicenseYes.Enabled = false;
                //rdoLicenseNo.Enabled = false;
                rdoGuiltyNo.Enabled = false;
                rdoGuiltyYes.Enabled = false;
                txtStartDateNew.Enabled = false;
                txtSalRequirement.Enabled = false;
                txtAvailability.Enabled = false;
                //txtWarrantyPolicy.Enabled = false;
                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                ddlType.Enabled = false;
                txtName.Enabled = false;
                txtPrinciple.Enabled = false;
                btnAddEmpPartner.Enabled = false;
                ddlBusinessType.Enabled = false;
                txtCEO.Enabled = false;
                txtLeagalOfficer.Enabled = false;
                txtPresident.Enabled = false;
                txtSoleProprietorShip.Enabled = false;
                txtPartnetsName.Enabled = false;
                //txtMailingAddress.Enabled = false;
                rdoWarrantyNo.Enabled = false;
                rdoWarrantyYes.Enabled = false;
                txtWarentyTimeYrs.Enabled = false;
                rdoBusinessMinorityNo.Enabled = false;
                rdoBusinessMinorityYes.Enabled = false;
                rdoWomenNo.Enabled = false;
                rdoWomenYes.Enabled = false;
            }
            else
            {
                txtFullTimePos.Enabled = true;
                txtContractor1.Enabled = true;
                txtContractor2.Enabled = true;
                txtContractor3.Enabled = true;
                txtMajorTools.Enabled = true;
                rdoDrugtestYes.Enabled = true;
                rdoDrugtestNo.Enabled = true;
                rdoDriveLicenseYes.Enabled = true;
                rdoDriveLicenseNo.Enabled = true;
                rdoTruckToolsYes.Enabled = true;
                rdoTruckToolsNo.Enabled = true;
                rdoJMApplyYes.Enabled = true;
                rdoJMApplyNo.Enabled = true;
                //rdoLicenseYes.Enabled = true;
                //rdoLicenseNo.Enabled = true;
                rdoGuiltyNo.Enabled = true;
                rdoGuiltyYes.Enabled = true;
                txtStartDateNew.Enabled = true;
                txtSalRequirement.Enabled = true;
                txtAvailability.Enabled = true;
                //txtWarrantyPolicy.Enabled = true;
                txtYrs.Enabled = true;
                txtCurrentComp.Enabled = true;
                txtWebsiteUrl.Enabled = true;
                ddlType.Enabled = true;
                txtName.Enabled = true;
                txtPrinciple.Enabled = true;
                btnAddEmpPartner.Enabled = true;
                ddlBusinessType.Enabled = false;
                txtCEO.Enabled = false;
                txtLeagalOfficer.Enabled = false;
                txtPresident.Enabled = false;
                txtSoleProprietorShip.Enabled = false;
                txtPartnetsName.Enabled = false;
                //txtMailingAddress.Enabled = false;
                rdoWarrantyNo.Enabled = false;
                rdoWarrantyYes.Enabled = false;
                txtWarentyTimeYrs.Enabled = false;
                rdoBusinessMinorityNo.Enabled = false;
                rdoBusinessMinorityYes.Enabled = false;
                rdoWomenNo.Enabled = false;
                rdoWomenYes.Enabled = false;
            }
            //if (ddldesignation.SelectedItem.Text == "Installer")
            //{
            //    lblInstallerType.Visible = true;
            //    ddlInstallerType.Visible = true;
            //}
            //else
            //{
            //    lblInstallerType.Visible = false;
            //    ddlInstallerType.Visible = false;
            //}
        }

        protected void chkMaddAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaddAdd.Checked)
            {
                txtMailingAddress.Text = txtaddress.Text;
                txtMailingAddress.ReadOnly = true;
            }
            else
            {
                txtMailingAddress.Text = "";
                txtMailingAddress.ReadOnly = false;
            }
        }

        protected void txtFullTimePos_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 12;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 13;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtMajorTools_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtContractor1_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtContractor2_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtContractor3_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoDriveLicenseYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoDriveLicenseNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoJMApplyYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoJMApplyNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoDrugtestYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoDrugtestNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoGuiltyYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoGuiltyNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoTruckToolsYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoTruckToolsNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtStartDateNew_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtSalRequirement_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtPrinciple_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoWarrantyYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoWarrantyNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtWarentyTimeYrs_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtCEO_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtPresident_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtPartnetsName_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoBusinessMinorityYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoBusinessMinorityNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtWebsiteUrl_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void rdoWomenYes_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtCurrentComp_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtYrs_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtLeagalOfficer_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void txtSoleProprietorShip_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                }

                if (txtContractor2.Text != "")
                {
                    Counter = 2;
                }

                if (txtContractor1.Text != "")
                {
                    Counter = 3;
                }
                if (txtContractor3.Text != "")
                {
                    Counter = 4;
                }
                if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                {
                    Counter = 5;
                }
                if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                {
                    Counter = 6;
                }
                if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                {
                    Counter = 7;
                }
                if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                {
                    Counter = 8;
                }
                if (txtStartDateNew.Text != "")
                {
                    Counter = 9;
                }
                if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                {
                    Counter = 10;
                }
                if (txtAvailability.Text != "")
                {
                    Counter = 11;
                }
                //if (txtWarrantyPolicy.Text != "")
                //{
                //    Counter = 12;
                //}
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                if (txtPrinciple.Text != "")
                {
                    Counter = 14;
                }
                if (txtName.Text != "")
                {
                    Counter = 15;
                }
                if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                {
                    Counter = 16;
                }
                if (txtCurrentComp.Text != "")
                {
                    Counter = 17;
                }
                if (txtWarentyTimeYrs.Text != "")
                {
                    Counter = 18;
                }
                if (txtYrs.Text != "")
                {
                    Counter = 19;
                }
                if (txtCEO.Text != "")
                {
                    Counter = 20;
                }
                if (txtLeagalOfficer.Text != "")
                {
                    Counter = 21;
                }
                if (txtPresident.Text != "")
                {
                    Counter = 22;
                }
                if (txtSoleProprietorShip.Text != "")
                {
                    Counter = 23;
                }
                if (txtPartnetsName.Text != "")
                {
                    Counter = 24;
                }
                if (ddlBusinessType.SelectedValue != "0")
                {
                    Counter = 25;
                }
                if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                {
                    Counter = 26;
                }
                if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                {
                    Counter = 27;
                }
                if (txtWebsiteUrl.Text != "")
                {
                    Counter = 28;
                }
                if (Counter >= 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
                #endregion
            }
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["PreviousStatusNew"]) == "Active" && (!(Convert.ToString(Session["usertype"]).Contains("Admin"))))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to any other status other than Deactive once user is Active')", true);
                if (Convert.ToString(Session["PreviousStatusNew"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                }
                return;
            }
            if ((Convert.ToString(Session["PreviousStatusNew"]) == "Active") && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
            {
                //binddata();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have rights change the status.')", true);
                return;
            }
            else if ((Convert.ToString(Session["PreviousStatusNew"]) == "Active") && ((Convert.ToString(Session["usertype"]).Contains("Admin")) || (Convert.ToString(Session["usertype"]).Contains("SM"))))
            {/*
                int isvaliduser = 0;
                isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtUserPassword.Text);
                if (isvaliduser == 0)
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your password is incorrect.')", true);
                    return;
                }*/

                if (ddlstatus.SelectedValue == "Install Prospect")
                {
                    if (Convert.ToString(Session["PreviousStatusNew"]) != "")
                    {
                        ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                    }
                    else
                    {
                        ddlstatus.SelectedValue = "Applicant";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Install Prospect')", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
                    return;
                }


            }
            if (ddlstatus.SelectedValue == "Install Prospect")
            {
                if (Convert.ToString(Session["PreviousStatusNew"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                }
                else
                {
                    ddlstatus.SelectedValue = "Applicant";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Install Prospect')", true);
                return;
            }
            else if (ddlstatus.SelectedValue == "Deactive")
            {
                if (Convert.ToString(Session["PreviousStatusNew"]) != "Active" && Convert.ToString(Session["PreviousStatusNew"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User should be active to deactivate.')", true);
                    return;
                }
            }
            else if (ddlstatus.SelectedValue == "Applicant")
            {
                dtInterviewDate.Visible = false;
                txtReson.Visible = false;
                pnlNew.Visible = true;
            }
            else if (ddlstatus.SelectedValue == "Rejected")
            {
                dtInterviewDate.Visible = false;
                ddlInsteviewtime.Visible = false;
                txtReson.Visible = true;
                RequiredFieldValidator7.Enabled = true;
            }
            else if (ddlstatus.SelectedValue == "InterviewDate")
            {
                txtReson.Visible = false;
                dtInterviewDate.Visible = true;
                dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
                ddlInsteviewtime.Visible = true;
                ddlInsteviewtime.SelectedValue = "10:00 AM";
            }

            else if (ddlstatus.SelectedValue == "Deactive")
            {

                dtInterviewDate.Visible = false;
                ddlInsteviewtime.Visible = false;
                txtReson.Visible = true;
                RequiredFieldValidator7.Enabled = true;
            }
           
            else
            {

                txtReson.Visible = false;
                dtInterviewDate.Visible = false;
                ddlInsteviewtime.Visible = false;
            }
            if (ddlstatus.SelectedValue == "Active" && (Convert.ToString(Session["usertype"]) != "Admin" && Convert.ToString(Session["usertype"]) != "SM"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
                if (Convert.ToString(Session["ddlStatus"]) != "")
                {

                    ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
                }
                else
                {
                    ddlstatus.SelectedValue = "Applicant";
                }
                return;
            }
            if (ddlstatus.SelectedValue == "Deactive" && (Convert.ToString(Session["usertype"]) != "Admin" && Convert.ToString(Session["usertype"]) != "SM"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
                if (Convert.ToString(Session["ddlStatus"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
                }
                else
                {
                    ddlstatus.SelectedValue = "Applicant";
                }
                return;
            }

            if (ddlstatus.SelectedValue == "Active")
            {
                //pnlFngPrint.Visible = true;
                //pnlGrid.Visible = true;
                //pnl4.Visible = false;
                //pnlnewHire.Visible = true;
                //pnlNew2.Visible = true;
                //btnNewPluse.Visible = false;
                //btnNewMinus.Visible = true;
            }
            else
            {
                //pnlFngPrint.Visible = false;
                //pnlGrid.Visible = false;
                //pnlnewHire.Visible = false;
                //pnlNew2.Visible = false;
                //btnNewPluse.Visible = true;
                //btnNewMinus.Visible = false;
                //pnl4.Visible = true;
            }

            if (ddlstatus.SelectedValue == "Applicant" || ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "PhoneScreened")
            {
                btnMinusNew.Visible = true;
                btnPlusNew.Visible = false;
                Panel3.Visible = true;
                Panel4.Visible = true;
                pnlNew.Visible = true;
            }
            else
            {
                btnMinusNew.Visible = false;
                btnPlusNew.Visible = true;
                Panel3.Visible = false;
                Panel4.Visible = false;
                pnlNew.Visible = false;
            }

            if (ddlstatus.SelectedValue == "Active" && (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer"))
            {
                //pnlAll.Visible = true;
            }
            else
            {
                //pnlAll.Visible = false;
            }

            //if (ddlstatus.SelectedValue == "Active")
            //{
            //    lblReqEIN.Visible = true;
            //    rqEin1.Enabled = true;
            //    rqEIN2.Enabled = true;
            //    rqDesignition.Enabled = true;
            //    rqPrimaryTrade.Enabled = true;
            //    rqSecondaryTrade.Enabled = true;
            //    rqFirstName.Enabled = true;
            //    rqEmail.Enabled = true;
            //    //rvEmail.Enabled = true;
            //    rqZip.Enabled = true;
            //    rqState.Enabled = true;
            //    lblStateReq.Visible = true;
            //    rqCity.Enabled = true;
            //    lblCityReq.Visible = true;
            //    password.Enabled = true;
            //    rqPass.Enabled = true;
            //    //rqMaritalStatus.Enabled = true;
            //    rqNotes.Enabled = true;
            //    lblNotesReq.Visible = true;
            //    lblNotesReq.Visible = true;
            //    rqSource.Enabled = true;
            //    //lblSource.Visible = true;
            //    //rfLastName.Enabled = true;
            //    rqAddress.Enabled = true;
            //    lblAddressReq.Visible = true;
            //    rqPhone.Enabled = true;
            //    lblPhoneReq.Visible = true;
            //    rqPass.Enabled = true;
            //    rqSSN1.Enabled = true;
            //    rqSSN2.Enabled = true;
            //    rqSSN3.Enabled = true;
            //    rqDOB.Enabled = true;
            //    rqPenalty.Enabled = true;
            //    lblReqSSN.Visible = true;
            //    rqSSN1.Enabled = true;
            //    rqSSN2.Enabled = true;
            //    rqSSN3.Enabled = true;
            //}
            //else
            //{
            //    lblReqEIN.Visible = false;
            //    rqEin1.Enabled = false;
            //    rqEIN2.Enabled = false;
            //    rqDesignition.Enabled = false;
            //    rqPrimaryTrade.Enabled = true;
            //    rqSecondaryTrade.Enabled = true;
            //    rqFirstName.Enabled = true;
            //    rqEmail.Enabled = true;
            //    //rvEmail.Enabled = true;
            //    rqZip.Enabled = true;
            //    rqState.Enabled = true;
            //    lblStateReq.Visible = true;
            //    rqCity.Enabled = true;
            //    lblCityReq.Visible = true;
            //    password.Enabled = true;
            //    rqPass.Enabled = false;
            //    //rqMaritalStatus.Enabled = false;
            //    rqNotes.Enabled = false;
            //    lblNotesReq.Visible = false;
            //    lblNotesReq.Visible = false;
            //    rqSource.Enabled = true;
            //    //lblSource.Visible = true;
            //    //lblSource.Visible = true;
            //    //rfLastName.Enabled = true;
            //    rqAddress.Enabled = true;
            //    lblAddressReq.Visible = true;
            //    rqPhone.Enabled = true;
            //    lblPhoneReq.Visible = true;
            //    rqPass.Enabled = false;
            //    rqSSN1.Enabled = false;
            //    rqSSN2.Enabled = false;
            //    rqSSN3.Enabled = false;
            //    rqDOB.Enabled = false;
            //    rqPenalty.Enabled = false;
            //    lblReqSSN.Visible = false;
            //    rqSSN1.Enabled = false;
            //    rqSSN2.Enabled = false;
            //    rqSSN3.Enabled = false;
            //}

            #region NewRequiredFields
            //if (ddlstatus.SelectedValue == "Install Prospect")
            //{
            //    rqDesignition.Enabled = false;
            //    RequiredFieldValidator3.Enabled = false;
            //    lblReqDesig.Visible = false;
            //    lblReqPtrade.Visible = true;
            //    rqPrimaryTrade.Enabled = true;
            //    lblReqSTrate.Visible = true;
            //    rqSecondaryTrade.Enabled = true;
            //    lblReqLastName.Visible = true;
            //    rqLastName.Enabled = true;
            //    RequiredFieldValidator5.Enabled = true;
            //    lblReqFName.Visible = true;
            //    rqFirstName.Enabled = true;
            //    RequiredFieldValidator4.Enabled = true;
            //    lblPhoneReq.Visible = true;
            //    rqPhone.Enabled = true;
            //    lblSourceReq.Visible = true;
            //    rqSource.Enabled = true;
            //    lblNotesReq.Visible = false;
            //    rqNotes.Enabled = false;
            //    lblReqEmail.Visible = false;
            //    rqEmail.Enabled = false;
            //    reEmail.Enabled = true;
            //    lblReqZip.Visible = false;
            //    rqZip.Enabled = false;
            //    lblStateReq.Visible = false;
            //    rqState.Enabled = false;
            //    lblCityReq.Visible = false;
            //    rqCity.Enabled = false;
            //    lblPassReq.Visible = false;
            //    rqPass.Enabled = false;
            //    lblReqSig.Visible = false;
            //    //rqSign.Enabled = false;
            //    //lblReqMarSt.Visible = false;
            //    //rqMaritalStatus.Enabled = false;
            //    lblReqPicture.Visible = false;
            //    lblReqDL.Visible = false;
            //    lblAddressReq.Visible = false;
            //    rqAddress.Enabled = false;
            //    Label1.Visible = false;
            //    RequiredFieldValidator6.Enabled = false;
            //    lblConfirmPass.Visible = true;
            //    rqConPass.Enabled = false;
            //    lblReqSSN.Visible = false;
            //    rqSSN1.Enabled = false;
            //    rqSSN2.Enabled = false;
            //    rqSSN3.Enabled = false;
            //    //lblReqDOB.Visible = false;
            //    rqDOB.Enabled = false;
            //    lblReqPOP.Visible = false;
            //    rqPenalty.Enabled = false;
            //    rqSource.Enabled = true;
            //    lblConfirmPass.Visible = false;
            //}
            if (ddlstatus.SelectedValue == "Applicant")
            {
                Label2.Visible = false;
                
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                //lblReqDesig.Visible = false;
                lblReqPtrade.Visible = true;
                rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                rqLastName.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //  rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                rqEmail.Enabled = false;
                reEmail.Enabled = true;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                rqPass.Enabled = false;
                lblReqSig.Visible = false;
                //rqSign.Enabled = false;
                //lblReqMarSt.Visible = false;
                //rqMaritalStatus.Enabled = false;
                lblReqPicture.Visible = false;
                lblReqDL.Visible = false;
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                //lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                //   rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
                lblReqDL.Visible = false;
                lblReqPicture.Visible = false;
                lblReqEmail.Visible = false;
                rqEmail.Enabled = false;
                reEmail.Enabled = false;
                lblConfirmPass.Visible = false;
                rqConPass.Enabled = false;
                password.Enabled = false;
                lblPassReq.Visible = false;
                rqPass.Enabled = false;
            }
            else if (ddlstatus.SelectedValue == "OfferMade")
            {
                Label2.Visible = false;
                rqDesignition.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                //lblReqDesig.Visible = true;
                lblReqPtrade.Visible = true;
                rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                rqLastName.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //    rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                rqEmail.Enabled = false;
                reEmail.Enabled = true;
                lblReqZip.Visible = true;
                rqZip.Enabled = true;
                lblStateReq.Visible = true;
                rqState.Enabled = true;
                lblCityReq.Visible = true;
                rqCity.Enabled = true;
                lblPassReq.Visible = false;
                rqPass.Enabled = false;
                lblReqSig.Visible = false;
                //rqSign.Enabled = false;
                //lblReqMarSt.Visible = false;
                //rqMaritalStatus.Enabled = false;
                lblReqPicture.Visible = false;
                lblReqDL.Visible = false;

                #region Req from NewHire
                lblAddressReq.Visible = true;
                rqAddress.Enabled = true;
                Label1.Visible = true;
                RequiredFieldValidator6.Enabled = true;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                //lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                //   rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
                //lblReqHireDate.Visible = true;
                //rqHireDate.Enabled = true;
                //lblReqWWC.Visible = true;
                //rqWorkCompCode.Enabled = true;
                //lblReqPayRates.Visible = true;
                //rqPayRate.Enabled = true;
                //lblReqEmpType.Visible = true;
                //rqEmpType.Enabled = true;
                txtReson.Visible = true;
                RequiredFieldValidator7.Enabled = true;
                lblReqDL.Visible = false;
                lblReqPicture.Visible = false;
                lblReqEmail.Visible = true;
                rqEmail.Enabled = true;
                reEmail.Enabled = true;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = true;
                password.Enabled = true;
                lblPassReq.Visible = true;
                rqPass.Enabled = true;
                #endregion
            }
            else if (ddlstatus.SelectedValue == "Active")
            {
                Label2.Visible = true;
                rqDesignition.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                //lblReqDesig.Visible = true;
                lblReqPtrade.Visible = true;
                rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                rqLastName.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //  rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                rqEmail.Enabled = false;
                reEmail.Enabled = true;
                lblReqZip.Visible = true;
                rqZip.Enabled = true;
                lblStateReq.Visible = true;
                rqState.Enabled = true;
                lblCityReq.Visible = true;
                rqCity.Enabled = true;
                lblPassReq.Visible = true;
                rqPass.Enabled = true;
                lblReqSig.Visible = false;
                lblConfirmPass.Visible = true;
                password.Enabled = true;
                rqConPass.Enabled = true;
                //rqSign.Enabled = false;
                //lblReqMarSt.Visible = true;
                //rqMaritalStatus.Enabled = true;
                lblReqPicture.Visible = false;
                lblReqDL.Visible = false;

                #region Req from NewHire
                lblAddressReq.Visible = true;
                rqAddress.Enabled = true;
                Label1.Visible = true;
                RequiredFieldValidator6.Enabled = true;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = false;
                lblReqSSN.Visible = true;
                rqSSN1.Enabled = true;
                rqSSN2.Enabled = true;
                rqSSN3.Enabled = true;
                //lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = true;
                rqPenalty.Enabled = true;
                //  rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
                //lblReqHireDate.Visible = true;
                //rqHireDate.Enabled = true;
                //lblReqWWC.Visible = true;
                //rqWorkCompCode.Enabled = true;
                //lblReqPayRates.Visible = true;
                //rqPayRate.Enabled = true;
                //lblReqEmpType.Visible = true;
                //rqEmpType.Enabled = true;
                txtReson.Visible = true;
                RequiredFieldValidator7.Enabled = true;
                lblReqDL.Visible = true;
                lblReqPicture.Visible = true;
                lblReqEmail.Visible = true;
                rqEmail.Enabled = true;
                reEmail.Enabled = true;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = true;
                password.Enabled = true;
                lblPassReq.Visible = true;
                rqPass.Enabled = true;
                #endregion

            }
            else if (ddlstatus.SelectedValue == "Deactive")
            {
                Label2.Visible = false;
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                lblReqDesig.Visible = false;
                lblReqPtrade.Visible = false;
                rqPrimaryTrade.Enabled = false;
                lblReqSTrate.Visible = false;
                rqSecondaryTrade.Enabled = false;
                lblReqLastName.Visible = false;
                rqLastName.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
                lblReqFName.Visible = false;
                rqFirstName.Enabled = false;
                RequiredFieldValidator4.Enabled = false;
                lblPhoneReq.Visible = false;
                rqPhone.Enabled = false;
                lblSourceReq.Visible = false;
                //  rqSource.Enabled = false;
                lblNotesReq.Visible = false;
                rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                rqEmail.Enabled = false;
                reEmail.Enabled = false;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                rqPass.Enabled = false;
                lblReqSig.Visible = false;
                lblConfirmPass.Visible = false;
                password.Enabled = false;
                rqConPass.Enabled = false;
                lblReqPicture.Visible = false;
                lblReqDL.Visible = false;
                //txtDateSourced.Text = DateTime.Now.ToShortDateString();
                #region Req from NewHire
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = false;
                rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                // rqSource.Enabled = false;
                lblConfirmPass.Visible = false;
                RequiredFieldValidator7.Enabled = true;
                lblReqDL.Visible = false;
                lblReqPicture.Visible = false;
                #endregion
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
                //return;
            }
            #endregion


        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            int isvaliduser = 0;
            isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtUserPassword.Text);
            if (isvaliduser > 0)
            {
                InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReson.Text);

                //Changes Status.....
                string a = ddlstatus.SelectedValue;
                if (a == "Rejected")
                {
                    ddlInsteviewtime.Visible = false;
                    dtInterviewDate.Visible = false;
                    RequiredFieldValidator7.Enabled = true;

                    txtReson.Visible = true;
                }
                else if (a == "InterviewDate")
                {
                    txtReson.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                    dtInterviewDate.Visible = true;
                    dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
                    ddlInsteviewtime.Visible = true;
                    ddlInsteviewtime.SelectedValue = "10:00 AM";
                }
                else if (a == "Deactive")
                {
                    if (Convert.ToString(Session["PreviousStatusNew"]) != "Active" && Convert.ToString(Session["PreviousStatusNew"]) != "")
                    {
                        ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User should be active to deactivate.')", true);
                        return;
                    }
                }
                else if (a == "Applicant")
                {
                    ddlInsteviewtime.Visible = false;
                    dtInterviewDate.Visible = false;
                    txtReson.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                }

                else
                {
                    ddlInsteviewtime.Visible = false;
                    txtReson.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                    dtInterviewDate.Visible = false;
                }

            }
            else
            {
                ddlstatus.SelectedValue = "Active";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter Correct password to change status.')", true);
            }
           

        }
    }
}