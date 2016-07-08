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
    public partial class InstallCreateUser : System.Web.UI.Page
    {

        user objuser = new user();
        string fn;
        List<string> newAttachments = new List<string>();
        DataTable dtDeduction = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            //  CalendarExtender10.StartDate = DateTime.Now;
            if (Session["Username"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alsert('Your session has expired,login to contineu');window.location='../login.aspx'", true);
            }
            CalendarExtender4.StartDate = DateTime.Now;
            CalendarExtender5.EndDate = DateTime.Now;
            CalendarExtender10.EndDate = DateTime.Now;


            //createForeMenForJobAcceptance();
            if (!IsPostBack)
            {
                Session["PhoneScreenedPopUp"] = null;
                Session["Toggle"] = "";
                if (Request.QueryString["Toggle"] == null)
                {
                    Session["PageData"] = "";
                }
                grdNew.ShowFooter = true;
                lblSectionHeading.Visible = false;
                CalendarExtender1.StartDate = DateTime.Today;
                this.BarcodeImage.Visible = false;
                //DateTime dt = DateTime.Parse("00:00 PM");
                //MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                //if (dt.ToString("tt") == "AM")
                //{
                //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                //}
                //else
                //{
                //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                //}
                //TimeSelector1.SetTime(dt.Hour, dt.Minute, am_pm);
                //Session["DtTemp"] = null;
                dtReviewDate.Attributes.Add("readonly", "readonly");
                dtLastDate.Attributes.Add("readonly", "readonly");
                txtDateSourced.Attributes.Add("readonly", "readonly");
                btnMinusNew.Visible = true;
                btnPlusNew.Visible = false;
                Panel3.Visible = true;
                Panel4.Visible = true;
                pnl4.Visible = false;
                ddlInsteviewtime.DataSource = GetTimeIntervals();
                ddlInsteviewtime.DataBind();
                ddlInsteviewtime.Visible = false;
                DataTable dtTemp = GetTable();
                DataTable dtPersonType = GetTableForPersonType();
                DataTable dtExtraTemp = GetExtraIncome();
                Session["PersonTypeData"] = dtPersonType;
                Session["DtTemp"] = dtTemp;
                Session["ExtraDtTemp"] = dtExtraTemp;
                rqHireDate.Enabled = false;
                rqWorkCompCode.Enabled = false;
                rqEmpType.Enabled = false;
                rqPayRate.Enabled = false;
                //rqRoutingNo.Enabled = false;
                //rqAccountNo.Enabled = false;
                //rqAccountType.Enabled = false;
                rqdtResignition.Enabled = false;
                //rqDtNewReview.Enabled = false;
                //rqLastReviewDate.Enabled = false;
                rqExtraEarnings.Enabled = false;
                rqExtraEarningAmt.Enabled = false;
                rqDeduction.Enabled = false;
                rqDeductionAmt.Enabled = false;
                rqDesignition.Enabled = true;
                //rqPrimaryTrade.Enabled = true;
                //rqSecondaryTrade.Enabled = true;
                rqFirstName.Enabled = true;
                //rqEmail.Enabled = true;
                //reEmail.Enabled = true;
                rqZip.Enabled = true;
                rqState.Enabled = true;
                lblStateReq.Visible = true;
                rqCity.Enabled = true;
                lblCityReq.Visible = true;
                lblCityReq.Visible = true;
                //password.Enabled = true;
                //rqConPass.Enabled = true;
                lblConfirmPass.Visible = true;
                rqSign.Enabled = false;
                lblReqSig.Visible = false;
                rqMaritalStatus.Enabled = false;
                lblReqMarSt.Visible = false;
                ////rqNotes.Enabled = false;
                lblNotesReq.Visible = false;
                // rqSource.Enabled = true;
                lblSourceReq.Visible = true;
                lblSourceReq.Visible = true;
                //rqLastName.Enabled = true;
                rqAddress.Enabled = true;
                lblAddressReq.Visible = true;
                ////rqPhone.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPass.Enabled = true;
                lblPassReq.Visible = true;
                rqSSN1.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                rqDOB.Enabled = false;
                rqPenalty.Enabled = false;
                lblReqPOP.Visible = false;
                Session["PreviousStatusNew"] = "";
                Session["UploadFileCountSkill"] = 0;
                Session["installId"] = "";
                Session["DeductionType"] = "";
                Session["DeductionAmount"] = "";
                Session["DeductionReason"] = "";
                Session["ExtraReasonNew"] = "";
                Session["ExtraTypeNew"] = "";
                Session["ExtraAmountNew"] = "";
                Session["UploadedPictureName"] = "";
                Session["PqLicenseFileName"] = "";
                Session["SkillAttachment"] = "";
                Session["ResumePath"] = "";
                Session["ResumeName"] = "";
                Session["CirtificationName"] = "";
                Session["CirtificationPath"] = "";
                Session["PersonName"] = "";
                Session["PersonType"] = "";
                Session["attachments"] = "";
                Session["PrevDesig"] = "";
                CreateDeductionDatatable();
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
                txtSecTradeOthers.Visible = false;
                pnlAll.Visible = false;
                Session["ExtraIncomeName"] = null;
                Session["ExtraIncomeAmt"] = null;
                gvYtd.DataSource = null;
                gvYtd.DataBind();
                pnlFngPrint.Visible = false;
                pnlGrid.Visible = false;
                pnlnewHire.Visible = false;
                pnlNew2.Visible = false;
                btnNewPluse.Visible = true;
                btnNewMinus.Visible = false;
                txtReson.Visible = false;
                dtInterviewDate.Visible = false;
                btnPluse.Visible = true;
                pnl4.Visible = false;
                btnMinus.Visible = false;
                btnUpdate.Visible = false;
                //lblExtraDollar.Visible = false;
                //lblExtraEarning.Visible = false;
                //btn_UploadFiles.Visible = false;
                gvUploadedFiles.Visible = false;
                DataSet ds;
                DataSet dsTrade;
                DataSet dsSource;
                dsTrade = InstallUserBLL.Instance.getTrades();
                dsSource = InstallUserBLL.Instance.GetSource();
                lnkW9.Visible = false;
                lnkI9.Visible = true;
                lnkW4.Visible = true;
                //lblInstallerType.Visible = true;
                //ddlInstallerType.Visible = true;


                #region Default Required Fields
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                lblReqDesig.Visible = false;
                lblReqPtrade.Visible = true;
                //rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                //rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                //rqLastName.Enabled = true;
                //RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //  rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                ////rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                //rqEmail.Enabled = false;
                //reEmail.Enabled = true;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                //rqPass.Enabled = false;
                lblReqSig.Visible = false;
                rqSign.Enabled = false;
                lblReqMarSt.Visible = false;
                rqMaritalStatus.Enabled = false;
                lblReqPicture.Style["display"] = "none";
                // lblReqPicture.Visible = false;
                //  lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "none";
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = true;
                //rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                //  rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
                RequiredFieldValidator7.Enabled = false;
                #endregion




                if (Request.QueryString["Desig"] != null)
                {
                    ddldesignation.SelectedValue = Convert.ToString(Request.QueryString["Desig"]);
                }
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
                    btnUpdate.Visible = true;
                    lnkSubmit.Visible = false;
                    //btncreate.Visible = false;
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
                        ddldesignation.SelectedValue = ds.Tables[0].Rows[0][5].ToString();
                        Session["PrevDesig"] = ds.Tables[0].Rows[0][5].ToString();
                        if (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer")
                        {
                            lnkW9.Visible = false;
                            lnkI9.Visible = true;
                            lnkW4.Visible = true;
                            //lnkEscrow.Visible = false;
                            //lnkFacePage.Visible = false;
                        }
                        else
                        {
                            lnkW9.Visible = true;
                            lnkI9.Visible = true;
                            //lnkEscrow.Visible = true;
                            //lnkFacePage.Visible = true;
                            lnkI9.Visible = true;
                            lnkW4.Visible = false;
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][6]) != "")
                        {
                            ddlstatus.SelectedValue = ds.Tables[0].Rows[0][6].ToString();
                            Session["PreviousStatusNew"] = Convert.ToString(ds.Tables[0].Rows[0][6]);
                            if (ddlstatus.SelectedValue == "Install Prospect")
                            {

                                rqDesignition.Enabled = false;
                                RequiredFieldValidator3.Enabled = false;
                                lblReqDesig.Visible = false;
                                lblReqPtrade.Visible = true;
                                //rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                //rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                //rqLastName.Enabled = true;
                                //RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                //rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                                //   rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                ////rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                //rqEmail.Enabled = false;
                                //reEmail.Enabled = true;
                                lblReqZip.Visible = false;
                                rqZip.Enabled = false;
                                lblStateReq.Visible = false;
                                rqState.Enabled = false;
                                lblCityReq.Visible = false;
                                rqCity.Enabled = false;
                                lblPassReq.Visible = false;
                                //rqPass.Enabled = false;
                                lblReqSig.Visible = false;
                                rqSign.Enabled = false;
                                lblReqMarSt.Visible = false;
                                rqMaritalStatus.Enabled = false;
                                lblReqPicture.Style["display"] = "none";
                                // lblReqPicture.Visible = false;
                                // lblReqDL.Visible = false;
                                lblReqDL.Style["display"] = "none";
                                lblAddressReq.Visible = false;
                                rqAddress.Enabled = false;
                                Label1.Visible = false;
                                RequiredFieldValidator6.Enabled = false;
                                lblConfirmPass.Visible = true;
                                //rqConPass.Enabled = false;
                                lblReqSSN.Visible = false;
                                rqSSN1.Enabled = false;
                                rqSSN2.Enabled = false;
                                rqSSN3.Enabled = false;
                                lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = false;
                                rqPenalty.Enabled = false;
                                // rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                                chkMaddAdd.Checked = true;
                            }
                            else if (ddlstatus.SelectedValue == "Applicant")
                            {
                                rqDesignition.Enabled = false;
                                RequiredFieldValidator3.Enabled = false;
                                lblReqDesig.Visible = false;
                                lblReqPtrade.Visible = true;
                                //rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                //rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                //rqLastName.Enabled = true;
                                //RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                //rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                                //  rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                ////rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                //rqEmail.Enabled = false;
                                //reEmail.Enabled = true;
                                lblReqZip.Visible = false;
                                rqZip.Enabled = false;
                                lblStateReq.Visible = false;
                                rqState.Enabled = false;
                                lblCityReq.Visible = false;
                                rqCity.Enabled = false;
                                lblPassReq.Visible = false;
                                //rqPass.Enabled = false;
                                lblReqSig.Visible = false;
                                rqSign.Enabled = false;
                                lblReqMarSt.Visible = false;
                                rqMaritalStatus.Enabled = false;
                                // lblReqPicture.Visible = false;
                                lblReqPicture.Style["display"] = "none";
                                // lblReqDL.Visible = false;
                                lblReqDL.Style["display"] = "none";
                                lblAddressReq.Visible = false;
                                rqAddress.Enabled = false;
                                Label1.Visible = false;
                                RequiredFieldValidator6.Enabled = false;
                                lblConfirmPass.Visible = true;
                                //rqConPass.Enabled = false;
                                lblReqSSN.Visible = false;
                                rqSSN1.Enabled = false;
                                rqSSN2.Enabled = false;
                                rqSSN3.Enabled = false;
                                lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = false;
                                rqPenalty.Enabled = false;
                                //   rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                            }
                            else if (ddlstatus.SelectedValue == "OfferMade")
                            {
                                rqDesignition.Enabled = true;
                                RequiredFieldValidator3.Enabled = true;
                                lblReqDesig.Visible = true;
                                lblReqPtrade.Visible = true;
                                //rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                //rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                //rqLastName.Enabled = true;
                                //RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                //rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                                //  rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                ////rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                //rqEmail.Enabled = false;
                                //reEmail.Enabled = true;
                                lblReqZip.Visible = true;
                                rqZip.Enabled = true;
                                lblStateReq.Visible = true;
                                rqState.Enabled = true;
                                lblCityReq.Visible = true;
                                rqCity.Enabled = true;
                                lblPassReq.Visible = false;
                                //rqPass.Enabled = false;
                                lblReqSig.Visible = false;
                                rqSign.Enabled = false;
                                lblReqMarSt.Visible = false;
                                rqMaritalStatus.Enabled = false;
                                //lblReqPicture.Visible = false;
                                lblReqPicture.Style["display"] = "none";
                                //  lblReqDL.Visible = false;
                                lblReqDL.Style["display"] = "none";

                                #region Req from NewHire
                                lblAddressReq.Visible = true;
                                rqAddress.Enabled = true;
                                Label1.Visible = true;
                                RequiredFieldValidator6.Enabled = true;
                                lblConfirmPass.Visible = true;
                                //rqConPass.Enabled = false;
                                lblReqSSN.Visible = false;
                                rqSSN1.Enabled = false;
                                rqSSN2.Enabled = false;
                                rqSSN3.Enabled = false;
                                lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = false;
                                rqPenalty.Enabled = false;
                                //  rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                                lblReqHireDate.Visible = true;
                                rqHireDate.Enabled = true;
                                lblReqWWC.Visible = true;
                                rqWorkCompCode.Enabled = true;
                                lblReqPayRates.Visible = true;
                                rqPayRate.Enabled = true;
                                lblReqEmpType.Visible = true;
                                rqEmpType.Enabled = true;
                                #endregion
                            }
                            else if (ddlstatus.SelectedValue == "Active")
                            {
                                rqDesignition.Enabled = true;
                                RequiredFieldValidator3.Enabled = true;
                                lblReqDesig.Visible = true;
                                lblReqPtrade.Visible = true;
                                //rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                //rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                //rqLastName.Enabled = true;
                                //RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                //rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                                //   rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                //rqNotes.Enabled = false;
                                lblReqEmail.Visible = false;
                                //rqEmail.Enabled = false;
                                //reEmail.Enabled = true;
                                lblReqZip.Visible = true;
                                rqZip.Enabled = true;
                                lblStateReq.Visible = true;
                                rqState.Enabled = true;
                                lblCityReq.Visible = true;
                                rqCity.Enabled = true;
                                lblPassReq.Visible = true;
                                //rqPass.Enabled = true;
                                lblReqSig.Visible = false;
                                lblConfirmPass.Visible = true;
                                password.Enabled = true;
                                rqConPass.Enabled = true;
                                rqSign.Enabled = false;
                                lblReqMarSt.Visible = true;
                                rqMaritalStatus.Enabled = true;
                                lblReqPicture.Style["display"] = "block";
                                // lblReqPicture.Visible = true;
                                //  lblReqDL.Visible = false;
                                lblReqDL.Style["display"] = "block";

                                #region Req from NewHire
                                lblAddressReq.Visible = true;
                                rqAddress.Enabled = true;
                                Label1.Visible = true;
                                RequiredFieldValidator6.Enabled = true;
                                lblConfirmPass.Visible = true;
                                //rqConPass.Enabled = false;
                                lblReqSSN.Visible = true;
                                rqSSN1.Enabled = true;
                                rqSSN2.Enabled = true;
                                rqSSN3.Enabled = true;
                                lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = true;
                                rqPenalty.Enabled = true;
                                // rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                                lblReqHireDate.Visible = true;
                                rqHireDate.Enabled = true;
                                lblReqWWC.Visible = true;
                                rqWorkCompCode.Enabled = true;
                                lblReqPayRates.Visible = true;
                                rqPayRate.Enabled = true;
                                lblReqEmpType.Visible = true;
                                rqEmpType.Enabled = true;
                                #endregion
                            }
                            else if (ddlstatus.SelectedValue == "InterviewDate")
                            {
                                rqDesignition.Enabled = false;
                                RequiredFieldValidator3.Enabled = false;
                                lblReqDesig.Visible = false;
                                lblReqPtrade.Visible = true;
                                //rqPrimaryTrade.Enabled = true;
                                lblReqSTrate.Visible = true;
                                //rqSecondaryTrade.Enabled = true;
                                lblReqLastName.Visible = true;
                                //rqLastName.Enabled = true;
                                //RequiredFieldValidator5.Enabled = true;
                                lblReqFName.Visible = true;
                                rqFirstName.Enabled = true;
                                RequiredFieldValidator4.Enabled = true;
                                lblPhoneReq.Visible = true;
                                //rqPhone.Enabled = true;
                                lblSourceReq.Visible = true;
                                // rqSource.Enabled = true;
                                lblNotesReq.Visible = false;
                                //rqNotes.Enabled = false;
                                lblReqEmail.Visible = true;
                                //rqEmail.Enabled = true;
                                //reEmail.Enabled = true;
                                lblReqZip.Visible = false;
                                rqZip.Enabled = false;
                                lblStateReq.Visible = false;
                                rqState.Enabled = false;
                                lblCityReq.Visible = false;
                                rqCity.Enabled = false;
                                lblPassReq.Visible = false;
                                //rqPass.Enabled = false;
                                lblReqSig.Visible = false;
                                rqSign.Enabled = false;
                                lblReqMarSt.Visible = false;
                                rqMaritalStatus.Enabled = false;
                                lblReqPicture.Style["display"] = "none";
                                //lblReqPicture.Visible = false;
                                //  lblReqDL.Visible = false;
                                lblReqDL.Style["display"] = "none";
                                lblAddressReq.Visible = false;
                                rqAddress.Enabled = false;
                                Label1.Visible = false;
                                RequiredFieldValidator6.Enabled = false;
                                lblConfirmPass.Visible = true;
                                //rqConPass.Enabled = false;
                                lblReqSSN.Visible = false;
                                rqSSN1.Enabled = false;
                                rqSSN2.Enabled = false;
                                rqSSN3.Enabled = false;
                                lblReqDOB.Visible = false;
                                rqDOB.Enabled = false;
                                lblReqPOP.Visible = false;
                                rqPenalty.Enabled = false;
                                // rqSource.Enabled = true;
                                lblConfirmPass.Visible = false;
                            }
                        }
                        txtYrs.Text = Convert.ToString(ds.Tables[0].Rows[0][81]);
                        txtCurrentComp.Text = Convert.ToString(ds.Tables[0].Rows[0][82]);
                        txtWebsiteUrl.Text = Convert.ToString(ds.Tables[0].Rows[0][83]);
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
                            //txtCEO.Text = Convert.ToString(ds.Tables[0].Rows[0][95]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][96]) != "")
                        {
                            //txtLeagalOfficer.Text = Convert.ToString(ds.Tables[0].Rows[0][96]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][97]) != "")
                        {
                            //txtPresident.Text = Convert.ToString(ds.Tables[0].Rows[0][97]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][98]) != "")
                        {
                            //txtSoleProprietorShip.Text = Convert.ToString(ds.Tables[0].Rows[0][98]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][99]) != "")
                        {
                            //txtPartnetsName.Text = Convert.ToString(ds.Tables[0].Rows[0][99]);
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
                        Session["PreviousStatus"] = Convert.ToString(ds.Tables[0].Rows[0][6]);
                        if (ds.Tables[0].Rows[0][6].ToString() == "Active" || ds.Tables[0].Rows[0][6].ToString() == "OfferMade")
                        {
                            pnlFngPrint.Visible = true;
                            pnlGrid.Visible = true;
                            pnl4.Visible = false;
                            pnlnewHire.Visible = true;
                            pnlNew2.Visible = true;
                            btnNewPluse.Visible = false;
                            btnNewMinus.Visible = true;
                        }
                        else
                        {
                            pnlFngPrint.Visible = false;
                            pnlGrid.Visible = false;
                            pnlnewHire.Visible = false;
                            pnlNew2.Visible = false;
                            btnNewPluse.Visible = true;
                            btnNewMinus.Visible = false;
                            pnl4.Visible = false;
                        }
                        if (ds.Tables[0].Rows[0][6].ToString() == "Deactive")
                        {
                            ddlstatus.Enabled = false;
                        }
                        if ((ds.Tables[0].Rows[0][6].ToString() == "Active" || ds.Tables[0].Rows[0][6].ToString() == "OfferMade") && (ds.Tables[0].Rows[0][5].ToString() == "ForeMan" || ds.Tables[0].Rows[0][5].ToString() == "Installer"))
                        {
                            pnlAll.Visible = true;
                        }
                        else
                        {
                            pnlAll.Visible = false;
                        }
                        if (ds.Tables[0].Rows[0][38].ToString() != "")
                        {
                            ddlSource.SelectedValue = ds.Tables[0].Rows[0][38].ToString();
                        }
                        if (ds.Tables[0].Rows[0][39].ToString() != "")
                        {
                            txtNotes.Text = ds.Tables[0].Rows[0][39].ToString();
                        }
                        if (ds.Tables[0].Rows[0][40].ToString() != "" && ds.Tables[0].Rows[0][6].ToString() != "InterviewDate")
                        {
                            txtReson.Text = ds.Tables[0].Rows[0][40].ToString();
                            txtReson.Visible = true;
                            ddlInsteviewtime.Visible = false;
                        }
                        else if (ds.Tables[0].Rows[0][40].ToString() != "" && ds.Tables[0].Rows[0][6].ToString() == "InterviewDate")
                        {
                            dtInterviewDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0][40]).ToShortDateString();
                            dtInterviewDate.Visible = true;
                            txtReson.Visible = false;
                            ddlInsteviewtime.Visible = true;
                            if (Convert.ToString(ds.Tables[0].Rows[0][105]) != "")
                            {
                                ddlInsteviewtime.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][105]);
                            }
                        }
                        else
                        {
                            ddlInsteviewtime.Visible = false;
                            txtReson.Visible = false;
                            dtInterviewDate.Visible = false;
                        }
                        txtPhone.Text = ds.Tables[0].Rows[0][8].ToString();
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

                        Session["attachments"] = ds.Tables[0].Rows[0][10].ToString();
                        //lblUF.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][10].ToString());
                        Session["UploadFiles"] = ds.Tables[0].Rows[0][10].ToString();
                        //FillDocument();
                        //txtBusinessName.Text = ds.Tables[0].Rows[0][14].ToString();
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
                        //txtEIN.Text = ds.Tables[0].Rows[0][22].ToString();
                        ViewState["ein1"] = ds.Tables[0].Rows[0][22].ToString();
                        //txtEIN2.Text = ds.Tables[0].Rows[0][23].ToString();
                        ViewState["ein2"] = ds.Tables[0].Rows[0][23].ToString();
                        string strein = ((ds.Tables[0].Rows[0][22].ToString() != "") ? ds.Tables[0].Rows[0][22].ToString() : "") + ((ds.Tables[0].Rows[0][23].ToString() != "") ? "- " + ds.Tables[0].Rows[0][23].ToString() : string.Empty); ViewState["ein"] =
                        ViewState["ein"] = strein;
                        txtA.Text = ds.Tables[0].Rows[0][24].ToString();
                        txtB.Text = ds.Tables[0].Rows[0][25].ToString();
                        txtC.Text = ds.Tables[0].Rows[0][26].ToString();
                        txtD.Text = ds.Tables[0].Rows[0][27].ToString();
                        txtE.Text = ds.Tables[0].Rows[0][28].ToString();
                        txtF.Text = ds.Tables[0].Rows[0][29].ToString();
                        txtG.Text = ds.Tables[0].Rows[0][30].ToString();
                        txtH.Text = ds.Tables[0].Rows[0][31].ToString();
                        txt5.Text = ds.Tables[0].Rows[0][32].ToString();
                        txt6.Text = ds.Tables[0].Rows[0][33].ToString();
                        txt7.Text = ds.Tables[0].Rows[0][34].ToString();
                        ddlmaritalstatus.SelectedValue = ds.Tables[0].Rows[0][35].ToString();
                        ddlPrimaryTrade.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][36]);
                        ddlSecondaryTrade.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][37]);
                        //if (ds.Tables[0].Rows[0][41].ToString() != "")
                        //{
                        //    Session["flpGeneralLiability"] = ds.Tables[0].Rows[0][41].ToString();
                        //    lblGL.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][41].ToString());

                        //}
                        if (ds.Tables[0].Rows[0][42].ToString() != "")
                        {
                            Session["PqLicense"] = ds.Tables[0].Rows[0][42].ToString();
                            lblPL.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][42].ToString());
                            Session["PqLicenseFileName"] = Path.GetFileName(ds.Tables[0].Rows[0][42].ToString());

                        }
                        //if (ds.Tables[0].Rows[0][43].ToString() != "")
                        //{
                        //    Session["WorkersComp"] = ds.Tables[0].Rows[0][43].ToString();
                        //    lblWC.Text = "Uploaded file Name: " + Path.GetFileName(ds.Tables[0].Rows[0][43].ToString());

                        //}
                        txtHireDate.Text = ds.Tables[0].Rows[0][44].ToString();
                        dtResignation.Text = ds.Tables[0].Rows[0][45].ToString();

                        string WorkersCompCode = Convert.ToString(ds.Tables[0].Rows[0]["WorkersCompCode"]);
                        if (WorkersCompCode == "5221 Concrete or Cem")
                        {
                            ddlWorkerCompCode.SelectedIndex = 2;
                        }
                        else if (WorkersCompCode == "Roofing code")
                        {
                            ddlWorkerCompCode.SelectedIndex = 3;
                        }
                        else if (WorkersCompCode == "0652 CARPENTRY - RES")
                        {
                            ddlWorkerCompCode.SelectedIndex = 1;
                        }
                        else
                        {
                            ddlWorkerCompCode.SelectedIndex = 0;
                        }
                        // ddlWorkerCompCode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["WorkersCompCode]);

                        //  ddlWorkerCompCode.SelectedValue = ds.Tables[0].Rows[0][46].ToString();
                        dtReviewDate.Text = ds.Tables[0].Rows[0][47].ToString();
                        ddlEmpType.SelectedValue = ds.Tables[0].Rows[0][48].ToString();
                        dtLastDate.Text = ds.Tables[0].Rows[0][49].ToString();
                        txtPayRates.Text = ds.Tables[0].Rows[0][50].ToString();
                        //ddlExtraEarning.SelectedValue = ;
                        Session["ExtraIncomeName"] = ds.Tables[0].Rows[0][51].ToString();
                        Session["ExtraReasonNew"] = ds.Tables[0].Rows[0][51].ToString();
                        if (ds.Tables[0].Rows[0][92].ToString() != "")
                        {
                            txtDateSourced.Text = ds.Tables[0].Rows[0][92].ToString();
                        }
                        Session["ExtraAmountNew"] = ds.Tables[0].Rows[0][52].ToString();
                        Session["ExtraTypeNew"] = ds.Tables[0].Rows[0][134].ToString();
                        Session["ExtraIncomeAmt"] = ds.Tables[0].Rows[0][52].ToString();
                        if (Convert.ToString(Session["ExtraIncomeName"]) != "" && Convert.ToString(Session["ExtraIncomeAmt"]) != "")
                        {
                            //lblExtraDollar.Visible = true;
                            //lblExtraEarning.Visible = true;
                        }
                        //lblExtra.Text = Convert.ToString(Session["ExtraIncomeName"]);
                        //lblDoller.Text = "$" + Convert.ToString(Session["ExtraIncomeAmt"]);
                        //txtExtraIncome.Text =  
                        if (ds.Tables[0].Rows[0][53].ToString() == "Check")
                        {
                            rdoCheque.Checked = true;
                            txtRoutingNo.Visible = false;
                            txtAccountNo.Visible = false;
                            txtAccountType.Visible = false;
                        }
                        else
                        {
                            rdoDeposite.Checked = true;
                            txtRoutingNo.Visible = true;
                            txtAccountNo.Visible = true;
                            txtAccountType.Visible = true;
                        }
                        //txtDeduction.Text = ds.Tables[0].Rows[0][54].ToString();
                        Session["DeductionAmount"] = ds.Tables[0].Rows[0][54].ToString();
                        //if (ds.Tables[0].Rows[0][55].ToString() == "One Time")
                        //{
                        Session["DeductionType"] = ds.Tables[0].Rows[0][55].ToString();
                        // rdoOneTime.Checked = true;
                        //}
                        //else
                        //{
                        //    Session["DeductionType"] = ds.Tables[0].Rows[0][55].ToString();
                        //    rdoReoccurance.Checked = true;
                        //}

                        string mailAddr = Convert.ToString(ds.Tables[0].Rows[0][100]);
                        if (mailAddr == "")
                        {
                            chkMaddAdd.Checked = false;

                        }
                        else
                        {
                            chkMaddAdd.Checked = true;
                            txtMailingAddress.Text = Convert.ToString(ds.Tables[0].Rows[0][100]);
                        }
                        if (txtMailingAddress.Text == "" && txtaddress.Text == "")
                        {
                            chkMaddAdd.Checked = true;
                        }


                        // txtMailingAddress.Text = ds.Tables[0].Rows[0][100].ToString();
                        txtRoutingNo.Text = ds.Tables[0].Rows[0][56].ToString();
                        txtAccountNo.Text = ds.Tables[0].Rows[0][57].ToString();
                        txtAccountType.Text = ds.Tables[0].Rows[0][58].ToString();
                        txtSuiteAptRoom.Text = ds.Tables[0].Rows[0][63].ToString();
                        if (ds.Tables[0].Rows[0][59].ToString() != "")
                        {
                            txtOtherTrade.Text = ds.Tables[0].Rows[0][59].ToString();
                            txtOtherTrade.Visible = true;
                        }
                        else
                        {
                            txtOtherTrade.Visible = false;
                        }
                        if (ds.Tables[0].Rows[0][61].ToString() != "")
                        {
                            Session["DeductionReason"] = ds.Tables[0].Rows[0][61].ToString();
                        }
                        if (ds.Tables[0].Rows[0][60].ToString() != "")
                        {
                            txtSecTradeOthers.Text = ds.Tables[0].Rows[0][60].ToString();
                            txtSecTradeOthers.Visible = true;
                        }
                        else
                        {
                            txtSecTradeOthers.Visible = false;
                        }
                        //if (lblExtra.Text == "")
                        //{
                        rqExtraEarnings.Enabled = true;
                        rqExtraEarningAmt.Enabled = true;
                        //}
                        //else
                        //{
                        //rqExtraEarnings.Enabled = false;
                        //rqExtraEarningAmt.Enabled = false;
                        //}

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
                        else
                        {

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
                        txtWarrantyPolicy.Text = Convert.ToString(ds.Tables[0].Rows[0][79]);
                        txtYrs.Text = Convert.ToString(ds.Tables[0].Rows[0][81]);
                        txtCurrentComp.Text = Convert.ToString(ds.Tables[0].Rows[0][82]);
                        txtWebsiteUrl.Text = Convert.ToString(ds.Tables[0].Rows[0][83]);
                        //txtPrinciple.Text = Convert.ToString(ds.Tables[0].Rows[0][86]);
                        Session["PersonName"] = Convert.ToString(ds.Tables[0].Rows[0][84]);
                        Session["PersonType"] = Convert.ToString(ds.Tables[0].Rows[0][85]);
                        //if (Convert.ToString(ds.Tables[0].Rows[0][93]) != "")
                        //{
                        //    ddlInstallerType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][93]);
                        //    lblInstallerType.Visible = true;
                        //    ddlInstallerType.Visible = true;
                        //}
                        //else
                        //{
                        //    lblInstallerType.Visible = false;
                        //    ddlInstallerType.Visible = false;
                        //}
                        if (Convert.ToString(ds.Tables[0].Rows[0]["InstallerType"]) != "")
                        {
                            //string b = Convert.ToString(ds.Tables[0].Rows[0]["InstallerType"]);
                            ////ddlInstallerType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Designation"]);
                            //if (b == "Labor")
                            //{

                            //    ddlInstallerType.SelectedIndex = 0;
                            //}
                            //else if (b == "Journeyman")
                            //{
                            //    ddlInstallerType.SelectedIndex = 1;
                            //}
                            //else if (b == "Leads Mechanic")
                            //{
                            //    ddlInstallerType.SelectedIndex = 2;
                            //}
                            //else
                            //{
                            //    ddlInstallerType.SelectedIndex = 3;
                            //}

                        }


                        if (Convert.ToString(ds.Tables[0].Rows[0]["Designation"]) != "")
                        {
                            string a = Convert.ToString(ds.Tables[0].Rows[0]["Designation"]);
                            //ddlInstallerType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Designation"]);
                            if (a == "Installer")
                            {

                                ddldesignation.SelectedIndex = 0;
                                //lblInstallerType.Visible = true;
                                //ddlInstallerType.Visible = true;
                            }
                            else
                            {
                                ddldesignation.SelectedIndex = 1;
                                //lblInstallerType.Visible = false;
                                //ddlInstallerType.Visible = false;
                            }
                        }
                        else
                        {
                            //lblInstallerType.Visible = false;
                            //ddlInstallerType.Visible = false;
                        }


                        if (Convert.ToString(ds.Tables[0].Rows[0][105]) != "")
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0][105]) == "Yes")
                            {
                                rdoEmptionYse.Checked = true;
                            }
                            else
                            {
                                rdoEmptionNo.Checked = true;
                            }
                        }
                        FillDocument();
                        if (Convert.ToString(Session["ExtraReasonNew"]) != "" && Convert.ToString(Session["ExtraTypeNew"]) != "" && Convert.ToString(Session["ExtraAmountNew"]) != "")
                        {
                            GetValueToFillExtraIncom((DataTable)(Session["DtTemp"]), (string)(Session["ExtraReasonNew"]), (string)(Session["ExtraAmountNew"]), (string)(Session["ExtraTypeNew"]));
                        }
                        if (Convert.ToString(Session["DeductionReason"]) != "" && Convert.ToString(Session["DeductionAmount"]) != "" && Convert.ToString(Session["DeductionType"]) != "")
                        {
                            GetValueToDisplay((DataTable)(Session["DtTemp"]), (string)(Session["DeductionReason"]), (string)(Session["DeductionAmount"]), (string)(Session["DeductionType"]));
                        }
                        if (Convert.ToString(Session["PersonName"]) != "" && Convert.ToString(Session["PersonType"]) != "")
                        {
                            GetPersonToDisplay((DataTable)(Session["PersonTypeData"]), (string)(Session["PersonName"]), (string)(Session["PersonType"]));
                        }
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

                    }
                    if (Request.QueryString["Desig"] != null)
                    {
                        ddldesignation.SelectedValue = Request.QueryString["Desig"];
                    }
                }
                else
                {
                    txtDateSourced.Text = DateTime.Today.ToShortDateString();
                    clearcontrols();
                    chkMaddAdd.Checked = true;
                    lnkW9.Visible = false;
                    lnkI9.Visible = true;
                    lnkW4.Visible = true;
                    //lnkEscrow.Visible = false;
                    //lnkFacePage.Visible = false;
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
                    txtFullTimePos.Text = split[9];
                    txtMajorTools.Text = split[10];
                    if (split[11] == "DriveLicenseYes")
                    {
                        rdoDriveLicenseYes.Checked = true;
                    }
                    else if (split[11] == "rdoDriveLicenseNo")
                    {
                        rdoDriveLicenseNo.Checked = true;
                    }
                    if (split[12] == "JMApplyYes")
                    {
                        rdoJMApplyYes.Checked = true;
                    }
                    else if (split[12] == "JMApplyNo")
                    {
                        rdoJMApplyNo.Checked = true;
                    }

                    if (split[13] == "GuiltyYes")
                    {
                        rdoGuiltyYes.Checked = true;
                    }
                    else if (split[13] == "rdoGuiltyNo")
                    {
                        rdoGuiltyNo.Checked = true;
                    }
                    txtSalRequirement.Text = split[14];
                    //ddlType.SelectedValue = split[15];
                    //txtName.Text = split[16];
                    if (split[17] == "rdoWarrantyYes")
                    {
                        rdoGuiltyYes.Checked = true;
                    }
                    else if (split[17] == "rdoWarrantyNo")
                    {
                        rdoGuiltyNo.Checked = true;
                    }
                    txtCurrentComp.Text = split[18];
                    txtWarentyTimeYrs.Text = split[19];
                    txtYrs.Text = split[20];
                    //txtCEO.Text = split[21];
                    //txtLeagalOfficer.Text = split[22];
                    //txtSoleProprietorShip.Text = split[23];
                    //txtPartnetsName.Text = split[24];
                    ddlBusinessType.SelectedValue = split[25];
                    if (split[26] == "rdoBusinessMinorityYes")
                    {
                        rdoBusinessMinorityYes.Checked = true;
                    }
                    else if (split[26] == "rdoBusinessMinorityNo")
                    {
                        rdoBusinessMinorityNo.Checked = true;
                    }
                    if (split[27] == "rdoWomenYes")
                    {
                        rdoWomenYes.Checked = true;
                    }
                    else if (split[27] == "rdoWomenNo")
                    {
                        rdoWomenNo.Checked = true;
                    }
                    txtWebsiteUrl.Text = split[28];
                    ddldesignation.SelectedValue = split[29];
                    ddlPrimaryTrade.SelectedValue = split[30];
                    txtOtherTrade.Text = split[31];
                    ddlSecondaryTrade.SelectedValue = split[32];
                    txtSecTradeOthers.Text = split[33];
                    txtfirstname.Text = split[34];
                    txtemail.Text = split[35];
                    txtZip.Text = split[36];
                    txtState.Text = split[37];
                    txtCity.Text = split[38];
                    txtpassword.Text = split[39];
                    txtSignature.Text = split[40];
                    ddlstatus.SelectedValue = split[41];
                    txtReson.Text = split[42];
                    dtInterviewDate.Text = split[43];
                    txtDateSourced.Text = split[44];
                    txtNotes.Text = split[45];
                    txtlastname.Text = split[46];
                    ddlSource.SelectedValue = split[47];
                    txtSource.Text = split[48];
                    txtaddress.Text = split[49];
                    txtMailingAddress.Text = split[50];
                    txtSuiteAptRoom.Text = split[51];
                    txtpassword1.Text = split[52];
                    txtPhone.Text = split[53];
                    txtssn.Text = split[54];
                    txtssn0.Text = split[55];
                    txtssn1.Text = split[56];
                    DOBdatepicker.Text = split[57];
                    ddlcitizen.SelectedValue = split[58];
                    //Added by Neeta For Installer type.....
                    // ddlInstallerType.SelectedValue = split[59];
                }
            }
            else
            {
                //RadWindow_ExisatingRecord.VisibleOnPageLoad = false;
            }
            if (rdoDeposite.Checked)
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                //rqRoutingNo.Enabled = true;
                //rqAccountNo.Enabled = true;
                //rqAccountType.Enabled = true;
            }
            else
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                //rqRoutingNo.Enabled = false;
                //rqAccountNo.Enabled = false;
                //rqAccountType.Enabled = false;
            }


            //if (ddlstatus.SelectedValue == "OfferMade")
            //{
            //    ValidationSummary1.ValidationGroup = lnkSubmit.ValidationGroup = "OfferMade";
            //}
            //else
            //{
            //    ValidationSummary1.ValidationGroup = lnkSubmit.ValidationGroup = "submit";
            //}

        }

        private void LoadNewSkillUser(int i)
        {
            DataSet dsTemp = new DataSet();
            dsTemp = InstallUserBLL.Instance.GetSkillUser(Convert.ToString(Session["Username"]), "0");
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
                txtWarrantyPolicy.Enabled = false;
                //ddlType.Enabled = false;
                //txtName.Enabled = false;
                //txtPrinciple.Enabled = false;
                //btnAddEmpPartner.Enabled = false;
                rdoAttchmentNo.Checked = false;
                rdoAttchmentNo.Enabled = false;
                rdoAttchmentYes.Enabled = false;
                rdoAttchmentYes.Checked = true;


                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                ddlBusinessType.Enabled = false;
                //txtCEO.Enabled = false;
                //txtLeagalOfficer.Enabled = false;
                //txtPresident.Enabled = false;
                //txtSoleProprietorShip.Enabled = false;
                //txtPartnetsName.Enabled = false;
                rdoWarrantyNo.Enabled = false;
                rdoWarrantyYes.Enabled = false;
                txtWarentyTimeYrs.Enabled = false;
                rdoBusinessMinorityNo.Enabled = false;
                rdoBusinessMinorityYes.Enabled = false;
                rdoWomenNo.Enabled = false;
                rdoWomenYes.Enabled = false;
                if (ddlstatus.SelectedValue != "PhoneScreened")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                    return;
                }
            }
            else
            {
                lblPL.Text = "";
                Session["SkillAttachment"] = "";
                Session["SkillAttachmentName"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Skill assessment.');", true);
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
            //Session["UploadFileCountSkill"] = 1;
            //FillDocument();
            //lblPL.Text = "";
            //Session["SkillAttachment"] = "";
            //Session["SkillAttachmentName"] = "";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Pq License.');", true);
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
            }
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

        }

        protected void AsyncFileUploadCustomerAttachment_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            //if (Convert.ToInt32(Session["UploadFileCountSkill"]) == 0)
            //{
            //    string fileNameall = "";
            //    string filename = Path.GetFileName(AsyncFileUploadCustomerAttachment.FileName);
            //    filename = DateTime.Now.ToString() + filename;
            //    filename = filename.Replace("/", "");
            //    filename = filename.Replace(":", "");
            //    filename = filename.Replace(" ", "");
            //    flpPqLicense.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
            //    Session["SkillAttachment"] = null;
            //    Session["SkillAttachmentName"] = null;
            //    Session["SkillAttachmentName"] = filename;
            //    Session["SkillAttachment"] = "~/Sr_App/UploadedFile/" + filename;
            //}
            //else
            //{
            //    Session["UploadFileCountSkill"] = 0;
            //}
        }

        private void GetValueToFillExtraIncom(DataTable dt, string Reason, string Amount, string Type)
        {
            try
            {
                DataRow drNew = dt.NewRow();
                Session["loop6"] = "";
                Session["loop7"] = "";
                Session["loop8"] = "";
                string[] str_Reason = Reason.Split(',');
                string[] str_Amt = Amount.Split(',');
                string[] str_Type = Type.Split(',');
            label:
                drNew = dt.NewRow();
                for (int i = 0; i < str_Reason.Length; i++)
                {
                    if (Convert.ToString(Session["loop6"]) != "")
                    {
                        i = Convert.ToInt32(Session["loop6"]) + 1;
                    }
                    if (str_Reason[i] != "")
                    {
                        drNew["ExtraFor"] = str_Reason[i];
                        Session["loop6"] = i;
                        break;
                    }
                }
                for (int j = 0; j < str_Type.Length; j++)
                {
                    if (Convert.ToString(Session["loop7"]) != "")
                    {
                        j = Convert.ToInt32(Session["loop7"]) + 1;
                    }
                    if (str_Type[j] != "")
                    {
                        drNew["Type"] = str_Type[j];
                        Session["loop7"] = j;
                        break;
                    }
                }
                for (int k = 0; k < str_Amt.Length; k++)
                {
                    if (Convert.ToString(Session["loop8"]) != "")
                    {
                        k = Convert.ToInt32(Session["loop8"]) + 1;
                    }
                    if (str_Amt[k] != "")
                    {
                        drNew["Amount"] = str_Amt[k];
                        Session["loop3"] = k;
                        if (k == str_Amt.Length - 1)
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
                Session["ExtraDtTemp"] = null;
                Session["ExtraDtTemp"] = dt;
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {

            }
        }




        private void GetValueToDisplay(DataTable dt, string Reason, string Amount, string Type)
        {
            try
            {
                DataRow drNew = dt.NewRow();
                Session["loop1"] = "";
                Session["loop2"] = "";
                Session["loop3"] = "";
                string[] str_Reason = Reason.Split(',');
                string[] str_Amt = Amount.Split(',');
                string[] str_Type = Type.Split(',');
            label:
                drNew = dt.NewRow();
                for (int i = 0; i < str_Reason.Length; i++)
                {
                    if (Convert.ToString(Session["loop1"]) != "")
                    {
                        i = Convert.ToInt32(Session["loop1"]) + 1;
                    }
                    if (str_Reason[i] != "")
                    {
                        drNew["DeductionFor"] = str_Reason[i];
                        Session["loop1"] = i;
                        break;
                    }
                }
                for (int j = 0; j < str_Type.Length; j++)
                {
                    if (Convert.ToString(Session["loop2"]) != "")
                    {
                        j = Convert.ToInt32(Session["loop2"]) + 1;
                    }
                    if (str_Type[j] != "")
                    {
                        drNew["Type"] = str_Type[j];
                        Session["loop2"] = j;
                        break;
                    }
                }
                for (int k = 0; k < str_Amt.Length; k++)
                {
                    if (Convert.ToString(Session["loop3"]) != "")
                    {
                        k = Convert.ToInt32(Session["loop3"]) + 1;
                    }
                    if (str_Amt[k] != "")
                    {
                        drNew["Amount"] = str_Amt[k];
                        Session["loop3"] = k;
                        if (k == str_Amt.Length - 1)
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
                Session["DtTemp"] = null;
                Session["DtTemp"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
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
            //GridView2.DataSource = dt;
            //GridView2.DataBind();
        }

        private void CreateDeductionDatatable()
        {
            dtDeduction.Columns.Add("Deduction For", typeof(string));
            dtDeduction.Columns.Add("Type", typeof(string));
            dtDeduction.Columns.Add("Ammount", typeof(string));
            Session["dtDeduction"] = dtDeduction;
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
            //clearcontrols();
            //lblmsg.Visible = false;
            Response.Redirect("../Sr_App/InstallCreateUser.aspx");
        }

        private void clearcontrols()
        {

            //Session["ExtraIncomeName"] = Session["ExtraIncomeAmt"] = lblDoller.Text = lblExtra.Text = "";
            // Session["DtTemp"] = "";
            txtReson.Text = ""; dtInterviewDate.Text = "";
            GridView1.DataSource = null;
            ddlcitizen.SelectedValue = "0";
            ddldesignation.SelectedValue = "Installer";
            ddlstatus.SelectedValue = "Applicant";
            //ddlInstallerType.SelectedValue = "Labor";
            //txtHireDate.Text = DateTime.Today.ToShortDateString();
            ddlWorkerCompCode.SelectedValue = "0";
            txtRoutingNo.Text = txtDeduction.Text = txtDeducReason.Text = txtAccountNo.Text = txtAccountType.Text = txtExtraIncome.Text = dtReviewDate.Text = "";
            ddlExtraEarning.SelectedValue = ddlEmpType.SelectedValue = "0";
            dtLastDate.Text = "";
            txtPayRates.Text = txtfirstname.Text = txtlastname.Text = dtResignation.Text = txtemail.Text = txtpassword.Text = txtpassword1.Text = txtPhone.Text = txtZip.Text = txtState.Text = txtCity.Text = txtaddress.Text = null;
            gvUploadedFiles.Visible = false;
            // lstboxUploadedImages.Items.Clear();
            Image2.Visible = false;
            //lnkEscrow.Visible = lnkFacePage.Visible = 
            lnkW4.Visible = lnkW9.Visible = false;
            lnkI9.Visible = true;
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
            txtWarrantyPolicy.Enabled = true;
            txtYrs.Enabled = true;
            txtCurrentComp.Enabled = true;
            txtWebsiteUrl.Enabled = true;
            //ddlType.Enabled = true;
            //txtName.Enabled = true;
            //txtPrinciple.Enabled = true;
            //btnAddEmpPartner.Enabled = true;
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
            txtFullTimePos.Text = "";
            txtContractor1.Text = "";
            txtContractor2.Text = "";
            txtContractor3.Text = "";
            txtMajorTools.Text = "";
            rdoDrugtestYes.Checked = false;
            rdoDrugtestNo.Checked = false;
            rdoDriveLicenseYes.Checked = false;
            rdoDriveLicenseNo.Checked = false;
            rdoTruckToolsYes.Checked = false;
            rdoTruckToolsNo.Checked = false;
            rdoJMApplyYes.Checked = false;
            rdoJMApplyNo.Checked = false;
            //rdoLicenseYes.Checked = false;
            //rdoLicenseNo.Checked = false;
            rdoGuiltyNo.Checked = false;
            rdoGuiltyYes.Checked = false;
            txtStartDateNew.Text = "";
            txtSalRequirement.Text = "";
            txtAvailability.Text = "";
            txtWarrantyPolicy.Text = "";
            txtYrs.Text = "";
            txtCurrentComp.Text = "";
            txtWebsiteUrl.Text = "";
            //ddlType.SelectedValue = "0";
            //txtName.Text = "";
            //txtPrinciple.Text = "";
            //btnAddEmpPartner.Enabled = true;
            Session["SkillAttachment"] = null;
            Session["SkillAttachmentName"] = null;
            Session["ResumePath"] = null;
            Session["ResumeName"] = null;
            Session["CirtificationName"] = "";
            Session["CirtificationPath"] = "";
            Session["PersonType"] = Session["PersonTypeData"] = "";
            //GridView2.DataSource = null;
            Session["PersonName"] = "";
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
                lnkI9.Visible = true;
                lnkW4.Visible = true;
                //lnkEscrow.Visible = false;
                //lnkFacePage.Visible = false;
            }
            else
            {
                lnkW9.Visible = true;
                lnkI9.Visible = true;
                //lnkEscrow.Visible = true;
                //lnkFacePage.Visible = true;
                lnkI9.Visible = true;
                lnkW4.Visible = false;
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
                objuser.maritalstatus = ddlmaritalstatus.SelectedValue;
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
                objuser.HireDate = txtHireDate.Text;
                objuser.TerminitionDate = dtResignation.Text;
                objuser.WorkersCompCode = Convert.ToString(ddlWorkerCompCode.Text);
                objuser.NextReviewDate = dtReviewDate.Text;
                objuser.EmpType = ddlEmpType.SelectedValue;
                objuser.LastReviewDate = dtLastDate.Text;
                objuser.PayRates = txtPayRates.Text;
                //objuser.ExtraEarning = Convert.ToString(Session["ExtraIncomeName"]);
                //if (Convert.ToString(Session["ExtraIncomeAmt"]) != "")
                //{
                //    objuser.ExtraEarningAmt = Convert.ToDouble(Session["ExtraIncomeAmt"]);
                //}
                objuser.ExtraEarning = Convert.ToString(Session["ExtraReasonNew"]);
                //if (Convert.ToString(Session["ExtraIncomeAmt"]) != "")
                //{
                objuser.ExtraEarningAmt = Convert.ToString(Session["ExtraAmountNew"]);
                //}
                objuser.ExtraIncomeType = Convert.ToString(Session["ExtraTypeNew"]);
                if (rdoCheque.Checked)
                {
                    objuser.PayMethod = "Check";
                }
                else
                {
                    objuser.PayMethod = "Direct Deposite";
                }
                if (Convert.ToString(Session["DeductionType"]) == "")
                {
                    if (rdoCheque.Checked)
                    {
                        objuser.DeductionType = "Check";
                    }
                    else
                    {
                        objuser.DeductionType = "Direct Deposite";
                    }
                }
                else
                {
                    objuser.DeductionType = Convert.ToString(Session["DeductionType"]);
                }

                if (Convert.ToString(Session["DeductionAmount"]) == "")
                {
                    if (txtDeduction.Text != "")
                    {
                        objuser.Deduction = txtDeduction.Text;
                    }
                    else
                    {
                        objuser.Deduction = "0";
                    }
                }
                else
                {
                    objuser.Deduction = Convert.ToString(Session["DeductionAmount"]);
                }
                if (Convert.ToString(Session["DeductionReason"]) == "")
                {
                    objuser.DeductionReason = txtDeducReason.Text;
                }
                else
                {
                    objuser.DeductionReason = Convert.ToString(Session["DeductionReason"]);
                }
                objuser.AbaAccountNo = txtRoutingNo.Text;
                objuser.AccountNo = txtAccountNo.Text;
                objuser.AccountType = txtAccountType.Text;
                objuser.PTradeOthers = txtOtherTrade.Text;
                objuser.STradeOthers = txtSecTradeOthers.Text;
                objuser.DateSourced = txtDateSourced.Text;
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
                objuser.WarrentyPolicy = txtWarrantyPolicy.Text;
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
                //objuser.CompanyPrinciple = txtPrinciple.Text;
                //if (ddldesignation.SelectedValue != "0" && ddlInstallerType.Visible == true)
                //{
                //    //objuser.InstallerType = ddlInstallerType.SelectedValue;
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

        //Create Sales User......Button
        protected void btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                string InstallId = string.Empty;
                string str_Status = string.Empty;
                string str_Reason = string.Empty;

                //For DOB.. Code Added by Neeta..
                //if (DOBdatepicker.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Date Of Birth.');", true);
                //    return;
                //}

                if ((ddlstatus.SelectedValue == "Active") && (txtA.Text == "" || txtB.Text == "" || txtC.Text == "" || txtD.Text == "" || txtE.Text == "" || txtF.Text == "" || txtG.Text == "" || txtH.Text == "" || txt5.Text == "" || txt6.Text == "" || txt7.Text == ""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill W4 details.');", true);
                    return;
                }
                if ((Convert.ToString(Session["UploadedPictureName"]) == "" || Convert.ToString(Session["UploadedPictureName"]) == null) && ddlstatus.SelectedValue == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select image file');", true);
                    return;
                }
                if ((Convert.ToString(Session["PqLicense"]) == "") && ddlstatus.SelectedValue == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select DL License file');", true);
                    return;
                }
                //if (txtemail.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Email Address');", true);
                //    return;
                //}
                //if (ddlstatus.SelectedValue == "Applicant")
                //{
                //    if (ddlSource.SelectedIndex == 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Source');", true);
                //        return;
                //    }
                //}
                btn_UploadFiles_Click(sender, e);

                //As per client Rquirement......
                if (chkboxcondition.Checked != true && ddlstatus.SelectedValue == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Term and Conditions');", true);
                    return;
                }
                //          
                else
                {
                    if (Convert.ToString(Session["IdGenerated"]) == "")
                    {
                        GetId(ddldesignation.SelectedValue, ddlstatus.SelectedValue);
                    }
                    InstallId = Convert.ToString(Session["IdGenerated"]);
                    objuser.SourceUser = Convert.ToString(Session["userid"]);
                    objuser.status = ddlstatus.SelectedValue;
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
                    objuser.DateSourced = txtDateSourced.Text;
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
                    //objuser.attachements = Convert.ToString(Session["UploadFiles"]);
                    objuser.attachements = Convert.ToString(Session["attachments"]);
                    //}
                    //else
                    //{
                    //    objuser.attachements = strFileName;
                    //}
                    if (rdoEmptionYse.Checked)
                    {
                        objuser.LIBC = "Yes";
                    }
                    else
                    {
                        objuser.LIBC = "No";
                    }
                    objuser.businessname = "";
                    objuser.ssn = txtssn.Text;
                    objuser.ssn1 = txtssn0.Text;
                    objuser.ssn2 = txtssn1.Text;
                    string ssn = txtssn.Text + "-" + txtssn0.Text + "-" + txtssn1.Text;
                    ViewState[ssn] = ssn;
                    objuser.signature = txtSignature.Text;
                    objuser.dob = DOBdatepicker.Text;
                    objuser.citizenship = ddlcitizen.SelectedValue;
                    //objuser.tin = txtTIN.Text;
                    objuser.ein1 = "";
                    objuser.ein2 = "";
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
                    objuser.maritalstatus = ddlmaritalstatus.SelectedValue;
                    str_Status = ddlstatus.SelectedValue;
                    objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                    objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                    objuser.Source = ddlSource.SelectedValue;
                    objuser.Notes = txtNotes.Text;
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
                    objuser.HireDate = txtHireDate.Text;
                    objuser.TerminitionDate = dtResignation.Text;
                    objuser.WorkersCompCode = Convert.ToString(ddlWorkerCompCode.SelectedValue);
                    objuser.NextReviewDate = dtReviewDate.Text;
                    objuser.EmpType = ddlEmpType.SelectedValue;
                    objuser.LastReviewDate = dtLastDate.Text;
                    objuser.PayRates = txtPayRates.Text;
                    objuser.ExtraEarning = Convert.ToString(Session["ExtraReasonNew"]);
                    //if (Convert.ToString(Session["ExtraIncomeAmt"]) != "")
                    //{
                    objuser.ExtraEarningAmt = Convert.ToString(Session["ExtraAmountNew"]);
                    //}
                    objuser.ExtraIncomeType = Convert.ToString(Session["ExtraTypeNew"]);

                    if (Convert.ToString(Session["DeductionType"]) == "")
                    {
                        if (rdoCheque.Checked)
                        {
                            objuser.DeductionType = "Check";
                        }
                        else
                        {
                            objuser.DeductionType = "Direct Deposite";
                        }
                    }
                    else
                    {
                        objuser.DeductionType = Convert.ToString(Session["DeductionType"]);
                    }

                    if (Convert.ToString(Session["DeductionAmount"]) == "")
                    {
                        if (txtDeduction.Text != "")
                        {
                            objuser.Deduction = txtDeduction.Text;
                        }
                        else
                        {
                            objuser.Deduction = "0";
                        }
                    }
                    else
                    {
                        objuser.Deduction = Convert.ToString(Session["DeductionAmount"]);
                    }
                    if (Convert.ToString(Session["DeductionReason"]) == "")
                    {
                        objuser.DeductionReason = txtDeducReason.Text;
                    }
                    else
                    {
                        objuser.DeductionReason = Convert.ToString(Session["DeductionReason"]);
                    }
                    objuser.AbaAccountNo = txtRoutingNo.Text;
                    objuser.AccountNo = txtAccountNo.Text;
                    objuser.AccountType = txtAccountType.Text;
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
                    else
                    {
                        objuser.skillassessmentstatus = null;
                    }
                    objuser.assessmentPath = Convert.ToString(Session["SkillAttachment"]);
                    if (txtFullTimePos.Text != "")
                    {
                        objuser.FullTimePosition = Convert.ToInt32(txtFullTimePos.Text);
                    }
                    else
                    {
                        objuser.FullTimePosition = Convert.ToInt32(0);
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
                    if (Convert.ToString(txtStartDateNew.Text) != "")
                    {
                        objuser.StartDate = Convert.ToString(txtStartDateNew.Text);
                    }
                    objuser.SalaryReq = txtSalRequirement.Text;
                    objuser.Avialability = txtAvailability.Text;
                    objuser.WarrentyPolicy = txtWarrantyPolicy.Text;
                    //if (txtYrs.Text != "")
                    //{
                    //    objuser.businessYrs = Convert.ToDouble(txtYrs.Text);
                    //}
                    //if (txtCurrentComp.Text != "")
                    //{
                    //    objuser.underPresentComp = Convert.ToDouble(txtCurrentComp.Text);
                    //}

                    //objuser.websiteaddress = txtWebsiteUrl.Text;
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
                        // objuser.PersonName = txtName.Text;
                    }
                    if (Convert.ToString(Session["PersonType"]) != "")
                    {
                        objuser.PersonType = Convert.ToString(Session["PersonType"]);
                    }
                    //else if (txtName.Text != "")
                    //{
                    //    if (ddlType.SelectedIndex != 0)
                    //    {
                    //        objuser.PersonType = ddlType.SelectedValue;
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select person type')", true);
                    //        ddlType.Focus();
                    //        return;
                    //    }
                    //}
                    //objuser.CompanyPrinciple = txtPrinciple.Text;
                    //if (ddldesignation.SelectedValue != "0" && ddlInstallerType.Visible == true)
                    //{
                    //    //objuser.InstallerType = ddlInstallerType.SelectedValue;
                    //}
                    if (ddlstatus.SelectedValue == "InterviewDate")
                    {
                        objuser.InterviewTime = ddlInsteviewtime.SelectedItem.Text;
                    }
                    else
                    {
                        objuser.InterviewTime = "";
                    }
                    objuser.DateSourced = DateTime.Today.ToShortDateString();
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
                    if (txtMailingAddress.Text != "")
                    {
                        objuser.MailingAddress = txtMailingAddress.Text;
                    }
                    else
                    {
                        objuser.MailingAddress = txtaddress.Text;
                    }
                    objuser.BusinessType = ddlBusinessType.SelectedValue;
                    //objuser.CEO = txtCEO.Text;
                    //objuser.LegalOfficer = txtLeagalOfficer.Text;
                    //objuser.President = txtPresident.Text;
                    //objuser.Owner = txtSoleProprietorShip.Text;
                    //objuser.AllParteners = txtPartnetsName.Text;
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

                    //For Event Added by.....ID
                    objuser.AddedBy = Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]);


                    if (chkboxcondition.Checked)
                    {
                        objuser.TC = true;
                    }
                    else
                    {
                        objuser.TC = false;
                    }
                    DataSet dsCheckDuplicate;
                    dsCheckDuplicate = InstallUserBLL.Instance.CheckInstallUser(txtemail.Text, txtPhone.Text);
                    //if (dsCheckDuplicate.Tables[0].Rows.Count > 0)
                    if (dsCheckDuplicate.Tables.Count > 0 && dsCheckDuplicate.Tables[0].Rows.Count > 0)
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
                        if (rdoEmptionYse.Checked)
                        {
                            GeneratePDF();
                        }
                        bool result = InstallUserBLL.Instance.AddUser(objuser);

                        GoogleCalendarEvent.CreateCalendar(txtemail.Text, txtaddress.Text);
                        //lblmsg.Visible = true;
                        //lblmsg.CssClass = "success";
                        //lblmsg.Text = "User has been created successfully";
                        if (ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "OfferMade" || ddlstatus.SelectedValue == "Deactive")
                        {
                            SendEmail(txtemail.Text, txtfirstname.Text, txtlastname.Text, ddlstatus.SelectedValue, str_Reason);
                        }

                        //
                        Session["installId"] = "";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User has been created successfully');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User has been created successfully');window.location ='EditInstallUser.aspx';", true);
                        clearcontrols();
                        //if (ddldesignation.SelectedItem.Text == "Installer")
                        //{
                        //    //lblInstallerType.Visible = true;
                        //    //ddlInstallerType.Visible = true;
                        //}
                        //else
                        //{
                        //    //lblInstallerType.Visible = false;
                        //    //ddlInstallerType.Visible = false;
                        //}
                    }
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Accept Term and Conditions');", true);
                //}
            }
            catch (Exception ex)
            {

                // throw;
            }
        }

        private void GeneratePDF()
        {
            //var pdfPath = Path.Combine(Server.MapPath("~/Sr_App/MailDocSample/LIBC.docx"));
            //var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);


            string str_date = DateTime.Now.ToString().Replace("/", "");
            str_date = str_date.Replace(":", "");
            str_date = str_date.Replace("-", "");
            str_date = str_date.Replace(" ", "");
            string SourcePath = @"~/Sr_App/MailDocSample/LIBC.docx";
            string TargetPath = @"~/Sr_App/MailDocument/" + str_date + txtfirstname.Text + "LIBC.docx";
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
                string date = Convert.ToString(DateTime.Now.Day);
                string month = Convert.ToString(DateTime.Now.Month);
                string Year = Convert.ToString(DateTime.Now.Year);
                this.FindAndReplace(wordApp, "lblMM", month);
                this.FindAndReplace(wordApp, "lblDD", date);
                this.FindAndReplace(wordApp, "lblYYYY", Year);
                this.FindAndReplace(wordApp, "lblCORPORATIONS1 lblFULL1 lblLEGAL1 lblNAME", "");
                this.FindAndReplace(wordApp, "lblTITLE1 lblOF1 lblEXECUTIVE1 lblOFFICER1", "");
                this.FindAndReplace(wordApp, "lblFIRST1", txtfirstname.Text);
                this.FindAndReplace(wordApp, "lblLAST1", txtlastname.Text);
                this.FindAndReplace(wordApp, "lblSUFFIX1", "");
                this.FindAndReplace(wordApp, "lblSSS1", txtssn.Text);
                this.FindAndReplace(wordApp, "lblAA1", txtssn0);
                this.FindAndReplace(wordApp, "lblBBBB1", txtssn1);
                this.FindAndReplace(wordApp, "lblPER%", "");
                this.FindAndReplace(wordApp, "lblCODE", txtPhone.Text);
                this.FindAndReplace(wordApp, "lblDIG1", "");
                this.FindAndReplace(wordApp, "lblNUMBER1", "");
                this.FindAndReplace(wordApp, "lblADDRESS1 lblOF1 lblBUSINESS1 lblOR1 lblRECIDENTIAL1", txtaddress.Text);
                this.FindAndReplace(wordApp, "lblCITYPLACE", txtCity.Text);
                this.FindAndReplace(wordApp, "lblCODE1", txtState.Text);
                this.FindAndReplace(wordApp, "lblZIP1", txtZip.Text);
                this.FindAndReplace(wordApp, "lblZIP2", "");
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
                string path = Server.MapPath(TargetPath);
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                //Document document = new Document();
                //document.LoadFromFile(@"E:\work\documents\TestSample.docx");

                ////Convert Word to PDF
                //document.SaveToFile("toPDF.PDF", FileFormat.PDF);
            }
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


        private string GetId_old(string UserType, string UserStatus)
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

                    //IndexS = installId.IndexOf("FRM");
                    //PrefixId = installId.Substring(IndexS, 1);
                    //previouPrefix = installId.Substring(0, IndexS);
                    //PrefixId = previouPrefix + PrefixId;
                    //newId = installId.Substring(IndexS + 6);
                    //newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //PrefixId = PrefixId + "FRM000" + newId;
                    //}
                    //if (installId.Contains("INS"))
                    //{
                    //    IndexS = installId.IndexOf("INS");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "FRM000" + newId;
                    //}
                    //if (installId.Contains("SUB"))
                    //{
                    //    IndexS = installId.IndexOf("SUB");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "FRM000" + newId;
                    //}

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

            if (UserType.Contains("Installer"))
            {
                if (UserStatus == "Deactive")
                {
                    installId = installId + "-X";
                }
                else
                {
                    if (installId != "")
                    {
                        LastInt = (Convert.ToInt32(installId.Replace("INS", "")) + 1).ToString(); //This will remove leading zeros
                        installId = "INS" + (LastInt.Length == 1 ? "00" : (LastInt.Length == 2 ? "0" : (LastInt.Length == 3 ? "0" : ""))) + LastInt;
                    }
                    else
                    {
                        installId = "INS001";
                    }
                }
            }
            else
            {
                if (UserStatus == "Deactive")
                {
                    installId = installId + "-X";
                }
                else
                {
                    if (installId != "")
                    {
                        LastInt = (Convert.ToInt32(installId.Replace("SUB", "")) + 1).ToString(); //This will remove leading zeros
                        installId = "SUB" + (LastInt.Length == 1 ? "00" : (LastInt.Length == 2 ? "0" : (LastInt.Length == 3 ? "0" : ""))) + LastInt;
                    }
                    else
                    {
                        installId = "SUB001";
                    }
                }
            }

            if (UserType == "Installer" && UserStatus != "Deactive")
            {
                if (installId != "")
                {

                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "INS000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);

                    //IndexS = installId.IndexOf("INS");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "INS000" + newId;
                    //}
                    //else if (installId.Contains("FRM"))
                    //{
                    //    IndexS = installId.IndexOf("FRM");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "INS000" + newId;
                    //}
                    //if (installId.Contains("SUB"))
                    //{
                    //    IndexS = installId.IndexOf("SUB");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "INS000" + newId;
                    //}
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

                    //if (installId.Contains("SUB"))
                    //{
                    //    IndexS = installId.IndexOf("SUB");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "SUB000" + newId;
                    //}
                    //else if (installId.Contains("FRM"))
                    //{
                    //    IndexS = installId.IndexOf("FRM");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "SUB000" + newId;
                    //}
                    //if (installId.Contains("INS"))
                    //{
                    //    IndexS = installId.IndexOf("INS");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "SUB000" + newId;
                    //}
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

            //else if ((UserType == "ForeMan" || UserType == "") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "OPP-00001";
            //    }
            //}
            //else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(10);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(10);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "SUB-0001-X";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus != "Active" && UserStatus != "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "P-SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(6);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(6);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "X-SC-00001";
            //    }
            //}





            //if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus != "Active" && UserStatus != "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(10);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(10);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "P-OPP-00001";
            //    }
            //}
            //else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "OPP-00001";
            //    }
            //}
            //else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(10);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(10);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "X-OPP-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus != "Active" && UserStatus != "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "P-SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(6);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(6);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "X-SC-00001";
            //    }
            //}
            Session["installId"] = installId;
            Session["IdGenerated"] = installId;
            return installId;
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
            if (UserType.Contains("Installer"))
            {
                if (UserStatus == "Deactive")
                {
                    installId = installId + "-X";
                }
                else
                {
                    if (installId != "")
                    {
                        LastInt = (Convert.ToInt32(installId.Replace("INS", "")) + 1).ToString(); //This will remove leading zeros
                        installId = "INS" + (LastInt.Length == 1 ? "00" : (LastInt.Length == 2 ? "0" : (LastInt.Length == 3 ? "0" : ""))) + LastInt;
                    }
                    else
                    {
                        installId = "INS001";
                    }
                }
            }
            else
            {
                if (UserStatus == "Deactive")
                {
                    installId = installId + "-X";
                }
                else
                {
                    if (installId != "")
                    {
                        LastInt = (Convert.ToInt32(installId.Replace("SUB", "")) + 1).ToString(); //This will remove leading zeros
                        installId = "SUB" + (LastInt.Length == 1 ? "00" : (LastInt.Length == 2 ? "0" : (LastInt.Length == 3 ? "0" : ""))) + LastInt;
                    }
                    else
                    {
                        installId = "SUB001";
                    }
                }
            }

            Session["installId"] = installId;
            Session["IdGenerated"] = installId;
            return installId;
        }
        private void SendEmail(string emailId, string FName, string LName, string status, string Reason)
        {
            try
            {
                string fullname = FName + " " + LName;
                string HTML_TAG_PATTERN = "<.*?>";
                string strHeader = GetEmailHeader(status);
                string strBody = GetEmailBody(status);
                string strFooter = GetFooter(status);
                strBody = strBody.Replace("LBL name", FName);
                strBody = strBody.Replace("Lbl Full name", fullname);
                strBody = strBody.Replace("LBL position", ddldesignation.SelectedItem.Text);
                strBody = strBody.Replace("lbl: start date", txtHireDate.Text);
                //strBody = strBody.Replace("($ rate","$"+ txtHireDate.Text);
                strBody = strBody.Replace("Reason", Reason);
                //strBody = Regex.Replace(strBody, HTML_TAG_PATTERN, string.Empty);
                //strHeader = Regex.Replace(strHeader, HTML_TAG_PATTERN, string.Empty);
                //strFooter = Regex.Replace(strFooter, HTML_TAG_PATTERN, string.Empty);
                StringBuilder Body = new StringBuilder();
                //MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                //Msg.From = new MailAddress("qat2015team@gmail.com");
                // Recipient e-mail address.
                //Msg.To.Add(emailId);
                //Msg.Subject = "JG Prospect Notification";
                //StringBuilder Body = new StringBuilder();
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
                this.FindAndReplace(wordApp, "HireDate", txtHireDate.Text);
                this.FindAndReplace(wordApp, "full time or part  time", ddlEmpType.SelectedValue);
                this.FindAndReplace(wordApp, "HourlyRate", txtPayRates);
                if (dtResignation.Text != "")
                {
                    this.FindAndReplace(wordApp, "WorkingStatus", "No");
                    this.FindAndReplace(wordApp, "LastWorkingDay", dtResignation.Text);
                }
                else
                {
                    this.FindAndReplace(wordApp, "WorkingStatus", "No");
                    this.FindAndReplace(wordApp, "LastWorkingDay", dtResignation.Text);
                }
                //this.FindAndReplace(wordApp, "$ rate", txtPayRates.Text);
                //this.FindAndReplace(wordApp, "lbl: next pay period", "");
                //this.FindAndReplace(wordApp, "lbl: paycheck date", "");
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
            }
            using (MailMessage mm = new MailMessage("qat2015team@gmail.com", txtemail.Text))
            // using (MailMessage mm = new MailMessage("support@jmgroveconstruction.com", txtemail.Text))
            {
                try
                {
                    mm.Subject = "Deactivation";
                    mm.Body = MailBody;
                    mm.Attachments.Add(new Attachment(Server.MapPath(TargetPath)));
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    // smtp.Host = "mail.jmgroveconstruction.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                    //NetworkCredential NetworkCred = new NetworkCredential("support@jmgroveconstruction.com", "kq2u0D3%");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25;
                    smtp.Send(mm);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "')", true);
                }
            }
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
            string fileName = string.Empty;
            string save = string.Empty;
            Int32 cnt;
            Int32 i;
            if (txtfirstname.Text != "" && txtlastname.Text != "")
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
                        //else if (Convert.ToString(Session["installId"]) == "")
                        //{
                        //    if (Convert.ToString(Session["PrevDesig"]) != ddldesignation.SelectedValue || ddlstatus.SelectedValue == "Deactive")
                        //    {
                        //        GetUpdatedId(Convert.ToString(Session["installId"]));
                        //    }
                        //}
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
                    return;
                }

                //lblDtae.Text = DateTime.Now.ToString();
                //UpdatePanel4.Update();
                if (rdoCheque.Checked)
                {
                    lblAba.Visible = false;
                    txtRoutingNo.Visible = false;
                    lblAccount.Visible = false;
                    txtAccountNo.Visible = false;
                    lblAccountType.Visible = false;
                    txtAccountType.Visible = false;
                    txtOtherTrade.Visible = false;
                }
                else
                {
                    lblAba.Visible = true;
                    txtRoutingNo.Visible = true;
                    lblAccount.Visible = true;
                    txtAccountNo.Visible = true;
                    lblAccountType.Visible = true;
                    txtAccountType.Visible = true;
                    txtOtherTrade.Visible = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(Please Select Designition,Enter First Name & last Name)", true);
                return;
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
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
                if ((ddlstatus.SelectedValue == "Active") && (txtA.Text == "" || txtB.Text == "" || txtC.Text == "" || txtD.Text == "" || txtE.Text == "" || txtF.Text == "" || txtG.Text == "" || txtH.Text == "" || txt5.Text == "" || txt6.Text == "" || txt7.Text == ""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please fill W4 details.');", true);
                    return;
                }
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
                if (chkboxcondition.Checked != true && ddlstatus.SelectedValue == "Active")
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
                objuser.status = ddlstatus.SelectedValue;
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
                if (rdoEmptionYse.Checked)
                {
                    objuser.LIBC = "Yes";
                }
                else
                {
                    objuser.LIBC = "No";
                }
                objuser.maritalstatus = ddlmaritalstatus.SelectedValue;
                objuser.PrimeryTradeId = Convert.ToInt32(ddlPrimaryTrade.SelectedValue);
                objuser.SecondoryTradeId = Convert.ToInt32(ddlSecondaryTrade.SelectedValue);
                objuser.Source = ddlSource.SelectedValue;
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
                    str_Reason = Convert.ToString(dtInterviewDate.Text);
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
                objuser.HireDate = txtHireDate.Text;
                objuser.TerminitionDate = dtResignation.Text;
                objuser.WorkersCompCode = Convert.ToString(ddlWorkerCompCode.Text);
                objuser.NextReviewDate = dtReviewDate.Text;
                objuser.EmpType = ddlEmpType.SelectedValue;
                objuser.LastReviewDate = dtLastDate.Text;
                objuser.PayRates = txtPayRates.Text;
                //objuser.ExtraEarning = Convert.ToString(Session["ExtraIncomeName"]);
                //if (Convert.ToString(Session["ExtraIncomeAmt"]) != "")
                //{
                //    objuser.ExtraEarningAmt = Convert.ToDouble(Session["ExtraIncomeAmt"]);
                //}
                objuser.ExtraEarning = Convert.ToString(Session["ExtraReasonNew"]);
                //if (Convert.ToString(Session["ExtraIncomeAmt"]) != "")
                //{
                objuser.ExtraEarningAmt = Convert.ToString(Session["ExtraAmountNew"]);
                //}
                objuser.ExtraIncomeType = Convert.ToString(Session["ExtraTypeNew"]);
                if (rdoCheque.Checked)
                {
                    objuser.PayMethod = "Check";
                }
                else
                {
                    objuser.PayMethod = "Direct Deposite";
                }
                if (Convert.ToString(Session["DeductionType"]) == "")
                {
                    if (rdoCheque.Checked)
                    {
                        objuser.DeductionType = "Check";
                    }
                    else
                    {
                        objuser.DeductionType = "Direct Deposite";
                    }
                }
                else
                {
                    objuser.DeductionType = Convert.ToString(Session["DeductionType"]);
                }

                if (Convert.ToString(Session["DeductionAmount"]) == "")
                {
                    if (txtDeduction.Text != "")
                    {
                        objuser.Deduction = txtDeduction.Text;
                    }
                    else
                    {
                        objuser.Deduction = "0";
                    }
                }
                else
                {
                    objuser.Deduction = Convert.ToString(Session["DeductionAmount"]);
                }
                if (Convert.ToString(Session["DeductionReason"]) == "")
                {
                    objuser.DeductionReason = txtDeducReason.Text;
                }
                else
                {
                    objuser.DeductionReason = Convert.ToString(Session["DeductionReason"]);
                }
                objuser.AbaAccountNo = txtRoutingNo.Text;
                objuser.AccountNo = txtAccountNo.Text;
                objuser.AccountType = txtAccountType.Text;
                objuser.PTradeOthers = txtOtherTrade.Text;
                objuser.STradeOthers = txtSecTradeOthers.Text;
                objuser.DateSourced = txtDateSourced.Text;
                int id = Convert.ToInt32(Session["ID"]);
                objuser.str_SuiteAptRoom = txtSuiteAptRoom.Text;
                if (txtMailingAddress.Text != "")
                {
                    objuser.MailingAddress = txtMailingAddress.Text;
                }
                else
                {
                    objuser.MailingAddress = txtaddress.Text;
                }
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
                objuser.WarrentyPolicy = txtWarrantyPolicy.Text;
                if (txtYrs.Text != "")
                {
                    objuser.businessYrs = Convert.ToDouble(txtYrs.Text);
                }
                if (txtCurrentComp.Text != "")
                {
                    objuser.underPresentComp = Convert.ToDouble(txtCurrentComp.Text);
                }
                if (Convert.ToString(Session["PreviousStatus"]) != ddlstatus.SelectedValue)
                {
                    objuser.Flag = 1;
                }
                else
                {
                    objuser.Flag = 0;
                }
                objuser.websiteaddress = txtWebsiteUrl.Text;
                objuser.ResumePath = Convert.ToString(Session["ResumePath"]);
                objuser.CirtificationTraining = Convert.ToString(Session["CirtificationPath"]);
                objuser.PersonName = Convert.ToString(Session["PersonName"]);
                objuser.PersonType = Convert.ToString(Session["PersonType"]);
                //objuser.CompanyPrinciple = txtPrinciple.Text;
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
                objuser.BusinessType = ddlBusinessType.SelectedValue;
                //objuser.CEO = txtCEO.Text;
                //objuser.LegalOfficer = txtLeagalOfficer.Text;
                //objuser.President = txtPresident.Text;
                //objuser.Owner = txtSoleProprietorShip.Text;
                //objuser.AllParteners = txtPartnetsName.Text;
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
                DataSet dsCheckDuplicate = InstallUserBLL.Instance.CheckInstallUserOnEdit(txtemail.Text, txtPhone.Text, id);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('InstallUser  Update successfully.');window.location ='EditInstallUser.aspx';", true);
                    //Server.Transfer("EditInstallUser.aspx");
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
            if (Session["attachments"] != null && Convert.ToString(Session["attachments"]) != "")
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

                        if (Session["UploadedPictureName"] != null && Convert.ToString(Session["UploadedPictureName"]) != "")
                        {
                            attach = attach + "," + Convert.ToString(Session["UploadedPictureName"]);
                        }
                        if (Session["PqLicenseFileName"] != null && Convert.ToString(Session["PqLicenseFileName"]) != "")
                        {
                            attach = attach + "," + Convert.ToString(Session["PqLicenseFileName"]);
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

            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
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
                        Session["ResumePathTemp"] = Session["ResumePath"];
                        string url = Convert.ToString(Session["ResumePath"]);
                        string str_fileName = Path.GetFileName(Convert.ToString(Session["ResumePath"]));
                        string file_name = Server.MapPath("~/Sr_App/UploadedFile/") + str_fileName;
                        Session["ResumePath"] = file_name;
                        save_file_from_url(file_name, url);
                        UpdateDocPath(Convert.ToString(Session["ResumePath"]), Convert.ToString(Session["ResumePathTemp"]));
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

        private void DeleteDLFile(string DLPath)
        {
            InstallUserBLL.Instance.DeletePLLic(DLPath);

        }

        private void DeleteAssessment(string Path)
        {
            InstallUserBLL.Instance.DeleteAssessment(Path);
        }

        private void DeleteResume(string Path)
        {
            InstallUserBLL.Instance.DeleteResume(Path);
        }
        private void UpdateDocPath(string NewPath, string OldPath)
        {
            InstallUserBLL.Instance.UpdateDocPath(NewPath, OldPath);
        }

        private void DeleteCirtification(string Path)
        {
            InstallUserBLL.Instance.DeleteCirtification(Path);
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
        }
        protected void DOBdatepicker_TextChanged(object sender, EventArgs e)
        {
            ViewState["DOB"] = DOBdatepicker.Text;
        }
        protected void txtBusinessName_TextChanged(object sender, EventArgs e)
        {
            //ViewState["BusinessName"] = txtBusinessName.Text;
        }
        protected void txtSignature_TextChanged(object sender, EventArgs e)
        {
            ViewState["Signature"] = txtSignature.Text;
        }
        protected void txtState_TextChanged(object sender, EventArgs e)
        {
            ViewState["State"] = txtSignature.Text;
        }
        protected void txtCity_TextChanged(object sender, EventArgs e)
        {
            ViewState["City"] = txtSignature.Text;
        }

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
            //ViewState["ein1"] = txtEIN.Text;
        }

        protected void txtEIN2_TextChanged(object sender, EventArgs e)
        {
            //ViewState["ein2"] = txtEIN2.Text;
            //string strein = ((txtEIN.Text != "") ? txtEIN.Text : "") + ((txtEIN2.Text != "") ? "- " + txtEIN2.Text : string.Empty);
            //ViewState["ein"] = strein;
        }


        protected void lnkw4Details_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void ddlmaritalstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["maritalstatus"] = ddlmaritalstatus.SelectedValue;
        }

        protected void lnkW4_Click(object sender, EventArgs e)
        {
            //
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
            if (ViewState["maritalstatus"] != null)
            {
                if (ddlmaritalstatus.SelectedValue == "Single")
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
            string Filename = "W4_" + txtfirstname.Text.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
            string path = PdflDirectory + "\\" + Filename;
            PDFHelper.ReturnPDF(pdfContents, Filename);
        }


        protected void btnPluse_Click(object sender, EventArgs e)
        {
            pnl4.Visible = true;
            btnPluse.Visible = false;
            btnMinus.Visible = true;
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

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            pnl4.Visible = false;
            btnPluse.Visible = true;
            btnMinus.Visible = false;
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

        //protected void btnGeneralLiability_Click(object sender, EventArgs e)
        //{
        //    if (flpGeneralLiability.HasFile)
        //    {
        //        string filename = Path.GetFileName(flpGeneralLiability.PostedFile.FileName);
        //        flpGeneralLiability.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
        //        Session["flpGeneralLiability"] = null;
        //        Session["flpGeneralLiability"] = "~/Sr_App/UploadedFile/" + filename;
        //        lblGL.Text = "Uploaded file Name: " + filename; 
        //    }
        //    else
        //    {
        //        lblGL.Text = "";
        //        Session["flpGeneralLiability"] = "";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload General Liability.');", true);
        //    }
        //    if (rdoCheque.Checked)
        //    {
        //        lblAba.Visible = false;
        //        txtRoutingNo.Visible = false;
        //        lblAccount.Visible = false;
        //        txtAccountNo.Visible = false;
        //        lblAccountType.Visible = false;
        //        txtAccountType.Visible = false;
        //        txtOtherTrade.Visible = false;
        //    }
        //    else
        //    {
        //        lblAba.Visible = true;
        //        txtRoutingNo.Visible = true;
        //        lblAccount.Visible = true;
        //        txtAccountNo.Visible = true;
        //        lblAccountType.Visible = true;
        //        txtAccountType.Visible = true;
        //        txtOtherTrade.Visible = true;
        //    }
        //}

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

                lblPL.Text = "Uploaded file Name: " + filename;
                FillDocument();
            }
            else
            {
                lblPL.Text = "";
                Session["PqLicense"] = "";
                Session["PqLicenseFileName"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload DL License.');", true);
            }
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
        }

        //protected void btnWorkersComp_Click(object sender, EventArgs e)
        //{
        //    if (flpWorkersComp.HasFile)
        //    {

        //        string filename = Path.GetFileName(flpWorkersComp.PostedFile.FileName);
        //        flpWorkersComp.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
        //        Session["WorkersComp"] = null;
        //        Session["WorkersComp"] = "~/Sr_App/UploadedFile/" + filename;

        //        lblWC.Text = "Uploaded file Name: " + filename;
        //    }
        //    else
        //    {
        //        lblWC.Text = "";
        //        Session["WorkersComp"] = "";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Workers Comp.');", true);
        //    }
        //    if (rdoCheque.Checked)
        //    {
        //        lblAba.Visible = false;
        //        txtRoutingNo.Visible = false;
        //        lblAccount.Visible = false;
        //        txtAccountNo.Visible = false;
        //        lblAccountType.Visible = false;
        //        txtAccountType.Visible = false;
        //        txtOtherTrade.Visible = false;
        //    }
        //    else
        //    {
        //        lblAba.Visible = true;
        //        txtRoutingNo.Visible = true;
        //        lblAccount.Visible = true;
        //        txtAccountNo.Visible = true;
        //        lblAccountType.Visible = true;
        //        txtAccountType.Visible = true;
        //        txtOtherTrade.Visible = true;
        //    }
        //}

        //protected void btn_UploadFiles_Click1(object sender, EventArgs e)
        //{
        //    if (flpUploadFiles.HasFile)
        //    {
        //        foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
        //        {
        //            string fileName = Path.GetFileName(postedFile.FileName);
        //            postedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
        //        }
        //        string filename = Path.GetFileName(flpUploadFiles.PostedFile.FileName);
        //        flpUploadFiles.SaveAs(Server.MapPath("~/Sr_App/UploadedFile/" + filename));
        //        Session["UploadFiles"] = null;
        //        Session["UploadFiles"] = "~/Sr_App/UploadedFile/" + filename;

        //        lblUF.Text = "Uploaded file Name: " + filename;
        //    }
        //    else
        //    {
        //        lblUF.Text = "";
        //        Session["UploadFiles"] = "";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Attachment.');", true);
        //    }
        //    if (rdoCheque.Checked)
        //    {
        //        lblAba.Visible = false;
        //        txtRoutingNo.Visible = false;
        //        lblAccount.Visible = false;
        //        txtAccountNo.Visible = false;
        //        lblAccountType.Visible = false;
        //        txtAccountType.Visible = false;
        //        txtOtherTrade.Visible = false;
        //    }
        //    else
        //    {
        //        lblAba.Visible = true;
        //        txtRoutingNo.Visible = true;
        //        lblAccount.Visible = true;
        //        txtAccountNo.Visible = true;
        //        lblAccountType.Visible = true;
        //        txtAccountType.Visible = true;
        //        txtOtherTrade.Visible = true;
        //    }
        //}

        protected void btnNewPluse_Click(object sender, EventArgs e)
        {
            pnlnewHire.Visible = true;
            pnlNew2.Visible = true;
            btnNewPluse.Visible = false;
            btnNewMinus.Visible = true;
            pnlFngPrint.Visible = true;
            pnlGrid.Visible = true;
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
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

        protected void btnNewMinus_Click(object sender, EventArgs e)
        {
            pnlFngPrint.Visible = false;
            pnlGrid.Visible = false;
            pnlnewHire.Visible = false;
            pnlNew2.Visible = false;
            btnNewPluse.Visible = true;
            btnNewMinus.Visible = false;
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
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

        protected void btnAddExtraIncome_Click(object sender, EventArgs e)
        {
            DataTable dtNew = new DataTable();

            //if (((DataTable)Session["DtTemp"]).Rows.Count > 0)
            if (Session["ExtraDtTemp"] != null)
            {
                dtNew = (DataTable)(Session["ExtraDtTemp"]);
                //return;
            }
            else
            {
                if (dtNew.Columns.Count == 0)
                {
                    dtNew.Columns.Add("ExtraFor");
                    dtNew.Columns.Add("Type");
                    dtNew.Columns.Add("Amount");
                }
            }
            if (Convert.ToString(Session["ExtraReasonNew"]) == "")
            {
                Session["ExtraReasonNew"] = ddlExtraEarning.SelectedValue;
            }
            else
            {
                Session["ExtraReasonNew"] = Convert.ToString(Session["ExtraReasonNew"]) + "," + ddlExtraEarning.SelectedValue;
            }
            if (Convert.ToString(Session["ExtraAmountNew"]) == "")
            {
                Session["ExtraAmountNew"] = txtExtraIncome.Text;
            }
            else
            {
                Session["ExtraAmountNew"] = Convert.ToString(Session["ExtraAmountNew"]) + "," + Convert.ToDouble(txtExtraIncome.Text);
            }
            if (rdoExtraOnes.Checked && (Convert.ToString(Session["ExtraTypeNew"]) == null || Convert.ToString(Session["ExtraTypeNew"]) == ""))
            {
                Session["ExtraTypeNew"] = "One Time";
            }
            else if (rdoExtraOnes.Checked && Convert.ToString(Session["ExtraTypeNew"]) != null)
            {
                Session["ExtraTypeNew"] = Convert.ToString(Session["ExtraTypeNew"]) + "," + "One Time";
            }
            else if (rdoExtraReoccurance.Checked && (Convert.ToString(Session["ExtraTypeNew"]) == null || Convert.ToString(Session["ExtraTypeNew"]) == ""))
            {
                Session["ExtraTypeNew"] = "Reoccurance";
            }
            else if (rdoExtraReoccurance.Checked && Convert.ToString(Session["DeductionType"]) != null)
            {
                Session["ExtraTypeNew"] = Convert.ToString(Session["ExtraTypeNew"]) + "," + "Reoccurance";
            }
            string[] DeductionReason;
            string[] DeductionType;
            string[] DeductionAmount;
            DeductionReason = Convert.ToString(Session["ExtraReasonNew"]).Split(',');
            DeductionType = Convert.ToString(Session["ExtraTypeNew"]).Split(',');
            DeductionAmount = Convert.ToString(Session["ExtraAmountNew"]).Split(',');
            int k = DeductionReason.Count();
            int l = DeductionType.Count();
            int m = DeductionAmount.Count();
            if (dtNew.Rows.Count == 0)
            {
                if (k == l && l == m)
                {
                    for (int i = 0; i < DeductionReason.Count(); i++)
                    {
                        DataRow drNew = dtNew.NewRow();
                        drNew["ExtraFor"] = DeductionReason[i];
                        drNew["Type"] = DeductionType[i];
                        drNew["Amount"] = DeductionAmount[i];
                        dtNew.Rows.Add(drNew);
                    }
                }
            }
            else
            {
                DataRow drNew = dtNew.NewRow();
                drNew["ExtraFor"] = ddlExtraEarning.SelectedValue;
                if (rdoExtraOnes.Checked)
                {
                    drNew["Type"] = "One Time";
                }
                else if (rdoExtraReoccurance.Checked)
                {
                    drNew["Type"] = "Re-Occurance";
                }
                drNew["Amount"] = txtExtraIncome.Text;
                dtNew.Rows.Add(drNew);
            }
            Session["ExtraDtTemp"] = dtNew;
            GridView2.DataSource = dtNew;
            GridView2.DataBind();
            //Double extraincome;
            //String strExtraincomeName;
            //if (Convert.ToString(Session["ExtraIncomeName"]) == "")
            //{
            //    //lblExtraDollar.Visible = true;
            //    //lblExtraEarning.Visible = true;
            //    strExtraincomeName = ddlExtraEarning.SelectedItem.Text;
            //    //lblExtra.Text = ddlExtraEarning.SelectedItem.Text;
            //    Session["ExtraIncomeName"] = ddlExtraEarning.SelectedItem.Text;
            //    ddlExtraEarning.SelectedIndex = 0;
            //}
            //else
            //{
            //    strExtraincomeName = Convert.ToString(Session["ExtraIncomeName"]);
            //    Session["ExtraIncomeName"] = strExtraincomeName + ", " + ddlExtraEarning.SelectedItem.Text;
            //    //lblExtra.Text = Convert.ToString(Session["ExtraIncomeName"]);
            //    ddlExtraEarning.SelectedIndex = 0;
            //}
            //if (Convert.ToString(Session["ExtraIncomeAmt"]) == "")
            //{
            //    extraincome = Convert.ToDouble(txtExtraIncome.Text);
            //    //lblDoller.Text = "$ " + txtExtraIncome.Text;
            //    Session["ExtraIncomeAmt"] = txtExtraIncome.Text;
            //    txtExtraIncome.Text = "";
            //    //lblExtra.Text = ddlExtraEarning.SelectedValue.ToString();
            //}
            //else
            //{
            //    if (txtExtraIncome.Text != "")
            //    {
            //        extraincome = Convert.ToDouble(Session["ExtraIncomeAmt"]) + Convert.ToDouble(txtExtraIncome.Text);
            //        Session["ExtraIncomeAmt"] = extraincome;
            //        //lblDoller.Text = "$ " + extraincome;
            //        txtExtraIncome.Text = "";
            //    }
            //}
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
            }
            //if (lblExtra.Text == "")
            //{
            rqExtraEarnings.Enabled = true;
            rqExtraEarningAmt.Enabled = true;
            //}
            //else
            //{
            //    rqExtraEarnings.Enabled = false;
            //    rqExtraEarningAmt.Enabled = false;
            //}
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
        }

        protected void btnAddType_Click(object sender, EventArgs e)
        {
            DataTable dtNew = new DataTable();

            //if (((DataTable)Session["DtTemp"]).Rows.Count > 0)
            if (Session["DtTemp"] != null)
            {
                dtNew = (DataTable)(Session["DtTemp"]);
                //return;
            }
            else
            {
                if (dtNew.Columns.Count == 0)
                {
                    dtNew.Columns.Add("DeductionFor");
                    dtNew.Columns.Add("Type");
                    dtNew.Columns.Add("Amount");
                }
            }
            if (Convert.ToString(Session["DeductionReason"]) == "")
            {
                Session["DeductionReason"] = txtDeducReason.Text;
            }
            else
            {
                Session["DeductionReason"] = Convert.ToString(Session["DeductionReason"]) + "," + txtDeducReason.Text;
            }
            if (Convert.ToString(Session["DeductionAmount"]) == "")
            {
                Session["DeductionAmount"] = txtDeduction.Text;
            }
            else
            {
                Session["DeductionAmount"] = Convert.ToString(Session["DeductionAmount"]) + "," + Convert.ToDouble(txtDeduction.Text);
            }
            if (rdoOneTime.Checked && (Convert.ToString(Session["DeductionType"]) == null || Convert.ToString(Session["DeductionType"]) == ""))
            {
                Session["DeductionType"] = "One Time";
            }
            else if (rdoOneTime.Checked && Convert.ToString(Session["DeductionType"]) != null)
            {
                Session["DeductionType"] = Convert.ToString(Session["DeductionType"]) + "," + "One Time";
            }
            else if (rdoReoccurance.Checked && (Convert.ToString(Session["DeductionType"]) == null || Convert.ToString(Session["DeductionType"]) == ""))
            {
                Session["DeductionType"] = "Reoccurance";
            }
            else if (rdoReoccurance.Checked && Convert.ToString(Session["DeductionType"]) != null)
            {
                Session["DeductionType"] = Convert.ToString(Session["DeductionType"]) + "," + "Reoccurance";
            }
            string[] DeductionReason;
            string[] DeductionType;
            string[] DeductionAmount;
            DeductionReason = Convert.ToString(Session["DeductionReason"]).Split(',');
            DeductionType = Convert.ToString(Session["DeductionType"]).Split(',');
            DeductionAmount = Convert.ToString(Session["DeductionAmount"]).Split(',');
            int k = DeductionReason.Count();
            int l = DeductionType.Count();
            int m = DeductionAmount.Count();
            if (dtNew.Rows.Count == 0)
            {
                if (k == l && l == m)
                {
                    for (int i = 0; i < DeductionReason.Count(); i++)
                    {
                        DataRow drNew = dtNew.NewRow();
                        drNew["DeductionFor"] = DeductionReason[i];
                        drNew["Type"] = DeductionType[i];
                        drNew["Amount"] = DeductionAmount[i];
                        dtNew.Rows.Add(drNew);
                    }
                }
            }
            else
            {
                DataRow drNew = dtNew.NewRow();
                drNew["DeductionFor"] = txtDeducReason.Text;
                if (rdoOneTime.Checked)
                {
                    drNew["Type"] = "One Time";
                }
                else if (rdoReoccurance.Checked)
                {
                    drNew["Type"] = "Re-Occurance";
                }
                drNew["Amount"] = txtDeduction.Text;
                dtNew.Rows.Add(drNew);
            }
            //if (rdoOneTime.Checked)
            //{
            //    drNew["Type"] = "One Time";
            //}
            //else if (rdoReoccurance.Checked)
            //{
            //    drNew["Type"] = "Re-Occurance";
            //}
            //drNew["Amount"] = txtDeduction.Text;
            //dtNew.Rows.Add(drNew);
            Session["DtTemp"] = dtNew;
            GridView1.DataSource = dtNew;
            GridView1.DataBind();
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
        }

        private DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("DeductionFor", typeof(String));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Amount", typeof(string));
            return table;
        }

        private DataTable GetExtraIncome()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ExtraFor", typeof(String));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Amount", typeof(string));
            return table;
        }

        private DataTable GetTableForPersonType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PersonName", typeof(String));
            table.Columns.Add("PersonType", typeof(string));
            return table;
        }

        //session.....
        protected void ddldesignation_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue == "Active" && (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer"))
            {
                pnlAll.Visible = true;
            }
            else
            {
                pnlAll.Visible = false;
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
            if (ddldesignation.SelectedItem.Text == "SubContractor")
            {
                if (Request.QueryString["ID"] != null)
                {
                    Response.Redirect("~/Sr_App/InstallCreateUser2.aspx?ID=" + Request.QueryString["ID"]);
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
                    //PageData = PageData + "," + txtPrinciple.Text;
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
                    //PageData = PageData + "," + ddlType.SelectedValue;
                    //PageData = PageData + "," + txtName.Text;
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
                    //PageData = PageData + "," + txtCEO.Text;
                    //PageData = PageData + "," + txtLeagalOfficer.Text;
                    //PageData = PageData + "," + txtSoleProprietorShip.Text;
                    //PageData = PageData + "," + txtPartnetsName.Text;
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
                    PageData = PageData + "," + txtDateSourced.Text;
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


                    // PageData = PageData + "," + ddlInstallerType.SelectedValue;//Added by Neeta...

                    Session["PageData"] = PageData;
                    PageData = string.Empty;
                    Response.Redirect("~/Sr_App/InstallCreateUser2.aspx?Toggle=Yes");

                }
            }
            //if (ddldesignation.SelectedItem.Text == "SubContractor")
            //{
            //    lnkw4Details.Visible = false;
            //    lblStatus.Visible = false;
            //    lnkI9.Visible = false;
            //    ddlmaritalstatus.Visible = false;
            //    //lblSignature.Visible = false;
            //    //txtSignature.Visible = false;
            //}
            //else
            //{
            //    lnkw4Details.Visible = true;
            //    lblStatus.Visible = true;
            //    ddlmaritalstatus.Visible = true;
            //    lnkI9.Visible = true;
            //    //lblSignature.Visible = true;
            //    //txtSignature.Visible = true;
            //}
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

        protected void rdoDeposite_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDeposite.Checked)
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                //rqRoutingNo.Enabled = true;
                //rqAccountNo.Enabled = true;
                //rqAccountType.Enabled = true;
            }
            else
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                //rqRoutingNo.Enabled = false;
                //rqAccountNo.Enabled = false;
                //rqAccountType.Enabled = false;
            }
        }

        protected void rdoCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDeposite.Checked)
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                //rqRoutingNo.Enabled = true;
                //rqAccountNo.Enabled = true;
                //rqAccountType.Enabled = true;
            }
            else
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                //rqRoutingNo.Enabled = false;
                //rqAccountNo.Enabled = false;
                //rqAccountType.Enabled = false;
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
                    InstallUserBLL.Instance.DeleteSource(ddlSource.SelectedItem.Text);
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
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter value to delete')", true);
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Resume Uploaded Successfully.');", true);
            }
            else
            {
                lblPL.Text = "";
                Session["ResumeName"] = "";
                Session["ResumePath"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Resume.');", true);
            }
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Certification/Training Uploaded successfully.');", true);
            }
            else
            {
                lblPL.Text = "";
                Session["CirtificationName"] = "";
                Session["CirtificationPath"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Upload Certification.');", true);
            }
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
            }
            if (Convert.ToString(Session["IdGenerated"]) != "")
            {
                GenerateBarCode(Convert.ToString(Session["IdGenerated"]));
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
        }

        protected void btnPlusNew_Click(object sender, EventArgs e)
        {
            btnMinusNew.Visible = true;
            btnPlusNew.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = true;
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
            lblSectionHeading.Visible = true;
        }

        protected void btnMinusNew_Click(object sender, EventArgs e)
        {
            btnMinusNew.Visible = false;
            btnPlusNew.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
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
            lblSectionHeading.Visible = false;
        }

        protected void btnAddEmpPartner_Click(object sender, EventArgs e)
        {
            //if (ddlType.SelectedIndex != 0)
            //{
            //    DataTable dtNew = (DataTable)(Session["PersonTypeData"]);
            //    if (Convert.ToString(Session["PersonName"]) == "")
            //    {
            //        Session["PersonName"] = txtName.Text;
            //    }
            //    else
            //    {
            //        Session["PersonName"] = Convert.ToString(Session["PersonName"]) + "," + txtName.Text;
            //    }
            //    if (Convert.ToString(Session["PersonType"]) == "")
            //    {
            //        Session["PersonType"] = ddlType.SelectedValue;
            //    }
            //    else
            //    {
            //        Session["PersonType"] = Convert.ToString(Session["PersonType"]) + "," + Convert.ToString(ddlType.SelectedValue);
            //    }
            //    DataRow drNew = dtNew.NewRow();
            //    drNew["PersonName"] = txtName.Text;
            //    drNew["PersonType"] = ddlType.SelectedValue;
            //    dtNew.Rows.Add(drNew);
            //    Session["PersonTypeData"] = dtNew;
            //    GridView2.DataSource = dtNew;
            //    GridView2.DataBind();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select User Type')", true);
            //    return;
            //}
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
            btnPluse.Visible = true;
            btnMinus.Visible = false;
            pnl4.Visible = false;
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
                txtWarrantyPolicy.Enabled = false;
                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                //ddlType.Enabled = false;
                //txtName.Enabled = false;
                //txtPrinciple.Enabled = false;
                //btnAddEmpPartner.Enabled = false;
                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                ddlBusinessType.Enabled = false;
                //txtCEO.Enabled = false;
                //txtLeagalOfficer.Enabled = false;
                //txtPresident.Enabled = false;
                btnUploadSkills.Enabled = true;
                btnUploadSkills.Visible = true;
                flpSkillAssessment.Enabled = true;
                flpSkillAssessment.Visible = true;
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
                txtWarrantyPolicy.Enabled = true;
                txtYrs.Enabled = true;
                txtCurrentComp.Enabled = true;
                txtWebsiteUrl.Enabled = true;
                //ddlType.Enabled = true;
                //txtName.Enabled = true;
                //txtPrinciple.Enabled = true;
                //btnAddEmpPartner.Enabled = true;
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
                this.FindAndReplace(wordApp, "lbl fulltime", ddlEmpType.SelectedValue);
                this.FindAndReplace(wordApp, "lbl: start date", txtHireDate.Text);
                this.FindAndReplace(wordApp, "$ rate", txtPayRates.Text);
                this.FindAndReplace(wordApp, "lbl: next pay period", "");
                this.FindAndReplace(wordApp, "lbl: paycheck date", "");
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
            }
            using (MailMessage mm = new MailMessage("qat2015team@gmail.com", txtemail.Text))
            //  using (MailMessage mm = new MailMessage("support@jmgroveconstruction.com", txtemail.Text))
            {
                try
                {
                    mm.Subject = "Foreman Job Acceptance";
                    mm.Body = str_Body;
                    mm.Attachments.Add(new Attachment(Server.MapPath(TargetPath)));
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    //  smtp.Host = "mail.jmgroveconstruction.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                    // NetworkCredential NetworkCred = new NetworkCredential("support@jmgroveconstruction.com", "kq2u0D3%");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25;
                    smtp.Send(mm);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "')", true);
                }
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
            /*
            wordApp.Selection.Find.Execute(ref findText, ref matchCase,
                ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format,
                ref replaceText, ref replace, ref matchKashida,
                        ref matchDiacritics,
                ref matchAlefHamza, ref matchControl);*/
        }

        protected void rdoAttchmentNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAttchmentNo.Checked)
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
                txtWarrantyPolicy.Enabled = true;
                txtYrs.Enabled = true;
                txtCurrentComp.Enabled = true;
                txtWebsiteUrl.Enabled = true;
                //ddlType.Enabled = true;
                //txtName.Enabled = true;
                //txtPrinciple.Enabled = true;
                //btnAddEmpPartner.Enabled = true;
                btnUploadSkills.Enabled = false;
                btnUploadSkills.Visible = false;
                flpSkillAssessment.Enabled = false;
                flpSkillAssessment.Visible = false;
            }
            else
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
                txtWarrantyPolicy.Enabled = false;
                txtYrs.Enabled = false;
                txtCurrentComp.Enabled = false;
                txtWebsiteUrl.Enabled = false;
                //ddlType.Enabled = false;
                //txtName.Enabled = false;
                //txtPrinciple.Enabled = false;
                //btnAddEmpPartner.Enabled = false;
                btnUploadSkills.Enabled = true;
                flpSkillAssessment.Enabled = true;
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

        protected void rdoEmptionYse_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoEmptionYse.Checked)
            {
                //string filePath = "../Sr_App/MailDocSample/LIBC.pdf";
                //Response.Clear();
                //Response.ContentType = "application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                //Response.TransmitFile(filePath);
                //Response.End();
            }
        }
        //For phone/Screened Status....Yes
        protected void btnPSYes_Click(object sender, EventArgs e)
        {
            if (Session["PhoneScreenedPopUp"] == null)
            {
                ddlstatus.SelectedValue = "PhoneScreened";
                Session["PhoneScreenedPopUp"] = "PopUpClosed";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ClosePS()", true);
                return;
            }

        }

        protected void btnPSNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ClosePS()", true);
            return;
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

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtContractor1_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtContractor1.Text != "")
                {
                    Counter = 1;
                    if (txtFullTimePos.Text != "")
                    {
                        Counter = 2;
                    }

                    if (txtContractor2.Text != "")
                    {
                        Counter = 3;
                    }

                    if (txtContractor3.Text != "")
                    {
                        Counter = 4;
                    }
                    if (txtMajorTools.Text != "")
                    {
                        Counter = 5;
                    }
                    if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                    {
                        Counter = 6;
                    }
                    if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                    {
                        Counter = 7;
                    }
                    if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                    {
                        Counter = 8;
                    }
                    if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                    {
                        Counter = 9;
                    }
                    if (txtStartDateNew.Text != "")
                    {
                        Counter = 10;
                    }
                    if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                    {
                        Counter = 11;
                    }
                    if (txtAvailability.Text != "")
                    {
                        Counter = 12;
                    }
                    if (txtWarrantyPolicy.Text != "")
                    {
                        Counter = 13;
                    }
                    if (txtSalRequirement.Text != "")
                    {
                        Counter = 14;
                    }
                    //if (txtPrinciple.Text != "")
                    //{
                    //    Counter = 15;
                    //}
                    //if (txtName.Text != "")
                    //{
                    //    Counter = 16;
                    //}
                    if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                    {
                        Counter = 17;
                    }
                    if (txtCurrentComp.Text != "")
                    {
                        Counter = 18;
                    }
                    if (txtWarentyTimeYrs.Text != "")
                    {
                        Counter = 19;
                    }
                    if (txtYrs.Text != "")
                    {
                        Counter = 20;
                    }
                    //if (txtCEO.Text != "")
                    //{
                    //    Counter = 21;
                    //}
                    //if (txtLeagalOfficer.Text != "")
                    //{
                    //    Counter = 22;
                    //}
                    //if (txtPresident.Text != "")
                    //{
                    //    Counter = 23;
                    //}
                    //if (txtSoleProprietorShip.Text != "")
                    //{
                    //    Counter = 24;
                    //}
                    //if (txtPartnetsName.Text != "")
                    //{
                    //    Counter = 25;
                    //}
                    if (ddlBusinessType.SelectedValue != "0")
                    {
                        Counter = 26;
                    }
                    if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                    {
                        Counter = 27;
                    }
                    if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                    {
                        Counter = 28;
                    }
                    if (txtWebsiteUrl.Text != "")
                    {
                        Counter = 29;
                    }
                    if (Counter >= 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                        return;
                    }
                }
                #endregion
            }
        }

        protected void txtFullTimePos_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                int Counter = 0;
                if (txtFullTimePos.Text != "")
                {
                    Counter = 1;
                    if (txtContractor1.Text != "")
                    {
                        Counter = 2;
                    }

                    if (txtContractor2.Text != "")
                    {
                        Counter = 3;
                    }

                    if (txtContractor3.Text != "")
                    {
                        Counter = 4;
                    }
                    if (txtMajorTools.Text != "")
                    {
                        Counter = 5;
                    }
                    if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                    {
                        Counter = 6;
                    }
                    if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                    {
                        Counter = 7;
                    }
                    if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                    {
                        Counter = 8;
                    }
                    if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                    {
                        Counter = 9;
                    }
                    if (txtStartDateNew.Text != "")
                    {
                        Counter = 10;
                    }
                    if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                    {
                        Counter = 11;
                    }
                    if (txtAvailability.Text != "")
                    {
                        Counter = 12;
                    }
                    if (txtWarrantyPolicy.Text != "")
                    {
                        Counter = 13;
                    }
                    if (txtSalRequirement.Text != "")
                    {
                        Counter = 14;
                    }
                    //if (txtPrinciple.Text != "")
                    //{
                    //    Counter = 15;
                    //}
                    //if (txtName.Text != "")
                    //{
                    //    Counter = 16;
                    //}
                    if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                    {
                        Counter = 17;
                    }
                    if (txtCurrentComp.Text != "")
                    {
                        Counter = 18;
                    }
                    if (txtWarentyTimeYrs.Text != "")
                    {
                        Counter = 19;
                    }
                    if (txtYrs.Text != "")
                    {
                        Counter = 20;
                    }
                    //if (txtCEO.Text != "")
                    //{
                    //    Counter = 21;
                    //}
                    //if (txtLeagalOfficer.Text != "")
                    //{
                    //    Counter = 22;
                    //}
                    //if (txtPresident.Text != "")
                    //{
                    //    Counter = 23;
                    //}
                    //if (txtSoleProprietorShip.Text != "")
                    //{
                    //    Counter = 24;
                    //}
                    //if (txtPartnetsName.Text != "")
                    //{
                    //    Counter = 25;
                    //}
                    if (ddlBusinessType.SelectedValue != "0")
                    {
                        Counter = 26;
                    }
                    if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                    {
                        Counter = 27;
                    }
                    if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                    {
                        Counter = 28;
                    }
                    if (txtWebsiteUrl.Text != "")
                    {
                        Counter = 29;
                    }
                    if (Counter >= 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                        return;
                    }
                }
            }
        }

        protected void txtContractor2_TextChanged(object sender, EventArgs e)
        {
            if (ddlstatus.SelectedValue != "PhoneScreened")
            {
                #region Check Any three
                int Counter = 0;
                if (txtContractor3.Text != "")
                {
                    Counter = 1;
                    if (txtFullTimePos.Text != "")
                    {
                        Counter = 2;
                    }

                    if (txtContractor2.Text != "")
                    {
                        Counter = 3;
                    }

                    if (txtContractor1.Text != "")
                    {
                        Counter = 4;
                    }
                    if (txtMajorTools.Text != "")
                    {
                        Counter = 5;
                    }
                    if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                    {
                        Counter = 6;
                    }
                    if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                    {
                        Counter = 7;
                    }
                    if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                    {
                        Counter = 8;
                    }
                    if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                    {
                        Counter = 9;
                    }
                    if (txtStartDateNew.Text != "")
                    {
                        Counter = 10;
                    }
                    if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                    {
                        Counter = 11;
                    }
                    if (txtAvailability.Text != "")
                    {
                        Counter = 12;
                    }
                    if (txtWarrantyPolicy.Text != "")
                    {
                        Counter = 13;
                    }
                    if (txtSalRequirement.Text != "")
                    {
                        Counter = 14;
                    }
                    //if (txtPrinciple.Text != "")
                    //{
                    //    Counter = 15;
                    //}
                    //if (txtName.Text != "")
                    //{
                    //    Counter = 16;
                    //}
                    if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                    {
                        Counter = 17;
                    }
                    if (txtCurrentComp.Text != "")
                    {
                        Counter = 18;
                    }
                    if (txtWarentyTimeYrs.Text != "")
                    {
                        Counter = 19;
                    }
                    if (txtYrs.Text != "")
                    {
                        Counter = 20;
                    }
                    //if (txtCEO.Text != "")
                    //{
                    //    Counter = 21;
                    //}
                    //if (txtLeagalOfficer.Text != "")
                    //{
                    //    Counter = 22;
                    //}
                    //if (txtPresident.Text != "")
                    //{
                    //    Counter = 23;
                    //}
                    //if (txtSoleProprietorShip.Text != "")
                    //{
                    //    Counter = 24;
                    //}
                    //if (txtPartnetsName.Text != "")
                    //{
                    //    Counter = 25;
                    //}
                    if (ddlBusinessType.SelectedValue != "0")
                    {
                        Counter = 26;
                    }
                    if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                    {
                        Counter = 27;
                    }
                    if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                    {
                        Counter = 28;
                    }
                    if (txtWebsiteUrl.Text != "")
                    {
                        Counter = 29;
                    }
                    if (Counter >= 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                        return;
                    }
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
                if (txtContractor2.Text != "")
                {
                    Counter = 1;
                    if (txtFullTimePos.Text != "")
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
                    if (txtMajorTools.Text != "")
                    {
                        Counter = 5;
                    }
                    if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                    {
                        Counter = 6;
                    }
                    if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                    {
                        Counter = 7;
                    }
                    if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                    {
                        Counter = 8;
                    }
                    if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                    {
                        Counter = 9;
                    }
                    if (txtStartDateNew.Text != "")
                    {
                        Counter = 10;
                    }
                    if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                    {
                        Counter = 11;
                    }
                    if (txtAvailability.Text != "")
                    {
                        Counter = 12;
                    }
                    if (txtWarrantyPolicy.Text != "")
                    {
                        Counter = 13;
                    }
                    if (txtSalRequirement.Text != "")
                    {
                        Counter = 14;
                    }
                    //if (txtPrinciple.Text != "")
                    //{
                    //    Counter = 15;
                    //}
                    //if (txtName.Text != "")
                    //{
                    //    Counter = 16;
                    //}
                    if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                    {
                        Counter = 17;
                    }
                    if (txtCurrentComp.Text != "")
                    {
                        Counter = 18;
                    }
                    if (txtWarentyTimeYrs.Text != "")
                    {
                        Counter = 19;
                    }
                    if (txtYrs.Text != "")
                    {
                        Counter = 20;
                    }
                    //if (txtCEO.Text != "")
                    //{
                    //    Counter = 21;
                    //}
                    //if (txtLeagalOfficer.Text != "")
                    //{
                    //    Counter = 22;
                    //}
                    //if (txtPresident.Text != "")
                    //{
                    //    Counter = 23;
                    //}
                    //if (txtSoleProprietorShip.Text != "")
                    //{
                    //    Counter = 24;
                    //}
                    //if (txtPartnetsName.Text != "")
                    //{
                    //    Counter = 25;
                    //}
                    if (ddlBusinessType.SelectedValue != "0")
                    {
                        Counter = 26;
                    }
                    if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                    {
                        Counter = 27;
                    }
                    if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                    {
                        Counter = 28;
                    }
                    if (txtWebsiteUrl.Text != "")
                    {
                        Counter = 29;
                    }
                    if (Counter >= 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                        return;
                    }
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
                if (txtMajorTools.Text != "")
                {
                    Counter = 1;
                    if (txtFullTimePos.Text != "")
                    {
                        Counter = 2;
                    }

                    if (txtContractor2.Text != "")
                    {
                        Counter = 3;
                    }

                    if (txtContractor1.Text != "")
                    {
                        Counter = 4;
                    }
                    if (txtContractor3.Text != "")
                    {
                        Counter = 5;
                    }
                    if (rdoDrugtestNo.Checked || rdoDrugtestNo.Checked)
                    {
                        Counter = 6;
                    }
                    if (rdoDriveLicenseNo.Checked || rdoDriveLicenseYes.Checked)
                    {
                        Counter = 7;
                    }
                    if (rdoTruckToolsYes.Checked || rdoTruckToolsNo.Checked)
                    {
                        Counter = 8;
                    }
                    if (rdoJMApplyYes.Checked || rdoJMApplyNo.Checked)
                    {
                        Counter = 9;
                    }
                    if (txtStartDateNew.Text != "")
                    {
                        Counter = 10;
                    }
                    if (rdoGuiltyNo.Checked || rdoGuiltyYes.Checked)
                    {
                        Counter = 11;
                    }
                    if (txtAvailability.Text != "")
                    {
                        Counter = 12;
                    }
                    if (txtWarrantyPolicy.Text != "")
                    {
                        Counter = 13;
                    }
                    if (txtSalRequirement.Text != "")
                    {
                        Counter = 14;
                    }
                    //if (txtPrinciple.Text != "")
                    //{
                    //    Counter = 15;
                    //}
                    //if (txtName.Text != "")
                    //{
                    //    Counter = 16;
                    //}
                    if (rdoWarrantyNo.Checked || rdoWarrantyYes.Checked)
                    {
                        Counter = 17;
                    }
                    if (txtCurrentComp.Text != "")
                    {
                        Counter = 18;
                    }
                    if (txtWarentyTimeYrs.Text != "")
                    {
                        Counter = 19;
                    }
                    if (txtYrs.Text != "")
                    {
                        Counter = 20;
                    }
                    //if (txtCEO.Text != "")
                    //{
                    //    Counter = 21;
                    //}
                    //if (txtLeagalOfficer.Text != "")
                    //{
                    //    Counter = 22;
                    //}
                    //if (txtPresident.Text != "")
                    //{
                    //    Counter = 23;
                    //}
                    //if (txtSoleProprietorShip.Text != "")
                    //{
                    //    Counter = 24;
                    //}
                    //if (txtPartnetsName.Text != "")
                    //{
                    //    Counter = 25;
                    //}
                    if (ddlBusinessType.SelectedValue != "0")
                    {
                        Counter = 26;
                    }
                    if (rdoBusinessMinorityYes.Checked || rdoBusinessMinorityNo.Checked)
                    {
                        Counter = 27;
                    }
                    if (rdoWomenYes.Checked || rdoWomenNo.Checked)
                    {
                        Counter = 28;
                    }
                    if (txtWebsiteUrl.Text != "")
                    {
                        Counter = 29;
                    }
                    if (Counter >= 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "overlayPS()", true);
                        return;
                    }
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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

        protected void txtAvailability_TextChanged(object sender, EventArgs e)
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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

        protected void txtWarrantyPolicy_TextChanged(object sender, EventArgs e)
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
        //protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (Convert.ToString(Session["PreviousStatusNew"]) == "Active" && (!(Convert.ToString(Session["usertype"]).Contains("Admin"))))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to any other status other than Deactive once user is Active')", true);
        //        if (Convert.ToString(Session["PreviousStatusNew"]) != "")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //        }
        //        return;
        //    }
        //    if ((Convert.ToString(Session["PreviousStatusNew"]) == "Active") && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {
        //        //binddata();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have rights change the status.')", true);
        //        return;
        //    }
        //    else if ((Convert.ToString(Session["PreviousStatusNew"]) == "Active" && ddlstatus.SelectedValue != "Deactive") && ((Convert.ToString(Session["usertype"]).Contains("Admin")) ))
        //    {
        //        int isvaliduser = 0;
        //        isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtUserPassword.Text);
        //        if (isvaliduser == 0)
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your password is incorrect.')", true);
        //            return;
        //        }
        //    }
        //    else if ((Convert.ToString(Session["PreviousStatusNew"]) == "Active" && ddlstatus.SelectedValue != "Deactive") && ((Convert.ToString(Session["usertype"]).Contains("SM")) ))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
        //        return;
        //    }

        //    if (ddlstatus.SelectedValue == "Install Prospect")
        //    {
        //        if (Convert.ToString(Session["PreviousStatusNew"]) != "")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //        }
        //        else
        //        {
        //            ddlstatus.SelectedValue = "Applicant";
        //        }
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Install Prospect')", true);
        //        return;
        //    }
        //    else if (ddlstatus.SelectedValue == "Deactive")
        //    {
        //        if (Convert.ToString(Session["PreviousStatusNew"]) != "Active" && Convert.ToString(Session["PreviousStatusNew"])!="")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User should be active to deactivate.')", true);
        //            return;
        //        }
        //    }
        //    else if (ddlstatus.SelectedValue == "Applicant")
        //    {
        //        ddlInsteviewtime.Visible = false;
        //        dtInterviewDate.Visible = false;
        //        txtReson.Visible = false;
        //        RequiredFieldValidator7.Enabled = false;
        //    }
        //    else if (ddlstatus.SelectedValue == "Rejected")
        //    {
        //        ddlInsteviewtime.Visible = false;
        //        dtInterviewDate.Visible = false;
        //        RequiredFieldValidator7.Enabled = true;

        //        txtReson.Visible = true;
        //    }
        //    else if (ddlstatus.SelectedValue == "InterviewDate")
        //    {
        //        txtReson.Visible = false;
        //        RequiredFieldValidator7.Enabled = false;
        //        dtInterviewDate.Visible = true;
        //        dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
        //        ddlInsteviewtime.Visible = true;
        //        ddlInsteviewtime.SelectedValue = "10:00 AM";
        //    }
        //    else if (ddlstatus.SelectedValue == "Deactive")
        //    {
        //        ddlInsteviewtime.Visible = false;
        //        txtReson.Visible = true;
        //        RequiredFieldValidator7.Enabled = true;
        //        dtInterviewDate.Visible = false;
        //    }
        //    else if (ddlstatus.SelectedValue == "OfferMade")
        //    {
        //        ddlInsteviewtime.Visible = false;
        //        txtReson.Visible = false;
        //        RequiredFieldValidator7.Enabled = false;
        //       // RequiredFieldValidator7.Enabled = true;
        //        dtInterviewDate.Visible = false;
        //        pnlnewHire.Visible = true;
        //        pnlNew2.Visible = true;
        //        btnNewPluse.Visible = false;
        //        btnNewMinus.Visible = true;
        //    }
        //    else
        //    {
        //        ddlInsteviewtime.Visible = false;
        //        txtReson.Visible = false;
        //        RequiredFieldValidator7.Enabled = false;
        //        dtInterviewDate.Visible = false;
        //    }
        //    if (ddlstatus.SelectedValue == "Active")
        //    {
        //        //txtHireDate.Text = DateTime.Now.ToShortDateString();
        //        dtReviewDate.Text = DateTime.Now.AddDays(30).ToShortDateString();
        //        pnlFngPrint.Visible = true;
        //        pnlGrid.Visible = true;
        //        pnl4.Visible = false;
        //        pnlnewHire.Visible = true;
        //        pnlNew2.Visible = true;
        //        btnNewPluse.Visible = false;
        //        btnNewMinus.Visible = true;
        //    }
        //    else
        //    {
        //        pnlFngPrint.Visible = false;
        //        pnlGrid.Visible = false;
        //        pnlnewHire.Visible = false;
        //        pnlNew2.Visible = false;
        //        btnNewPluse.Visible = true;
        //        btnNewMinus.Visible = false;
        //        pnl4.Visible = false;
        //    }
        //    if (ddlstatus.SelectedValue == "Deactive")
        //    {
        //        dtResignation.Text = DateTime.Now.ToShortDateString();
        //    }
        //    if (ddlstatus.SelectedValue == "OfferMade")
        //    {
        //        rqdtResignition.Enabled = false;
        //        pnlnewHire.Visible = true;
        //        pnlAll.Visible = true;
        //        pnlNew2.Visible = true;
        //        btnNewPluse.Visible = false;
        //        btnNewMinus.Visible = true;
        //        //rqDtNewReview.Enabled = false;
        //        //rqLastReviewDate.Enabled = false;
        //    }
        //    else if (ddlstatus.SelectedValue == "Active")
        //    {
        //        rqdtResignition.Enabled = true;
        //        //rqDtNewReview.Enabled = true;
        //        //rqLastReviewDate.Enabled = true;
        //    }
        //    else
        //    {
        //        rqdtResignition.Enabled = false;
        //        //rqDtNewReview.Enabled = false;
        //        //rqLastReviewDate.Enabled = false;
        //    }
        //    if (ddlstatus.SelectedValue == "Active" && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
        //        if (Convert.ToString(Session["ddlStatus"]) != "")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
        //        }
        //        //else
        //        //{
        //        //    ddlstatus.SelectedValue = "Applicant";
        //        //}
        //        return;
        //    }

        //    if (ddlstatus.SelectedValue == "Deactive" && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
        //        if (Convert.ToString(Session["ddlStatus"]) != "")
        //        {
        //            ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
        //        }
        //        //else
        //        //{
        //        //    ddlstatus.SelectedValue = "Applicant";
        //        //}
        //        return;
        //    }
        //    if ((ddlstatus.SelectedValue == "OfferMade") && (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer"))
        //    {
        //        pnlAll.Visible = true;
        //        txtReson.Visible = false;
        //        //pnlnewHire.Visible = true;
        //        RequiredFieldValidator7.Enabled = true;


        //    }
        //    else if((ddlstatus.SelectedValue == "Active"))
        //    {

        //        pnlAll.Visible = true;
        //        txtReson.Visible = false;
        //        //pnlnewHire.Visible = true;
        //        RequiredFieldValidator7.Enabled = false;
        //    }
        //    else
        //    {
        //        pnlAll.Visible = false;
        //    }
        //    if (rdoCheque.Checked)
        //    {
        //        lblAba.Visible = false;
        //        txtRoutingNo.Visible = false;
        //        lblAccount.Visible = false;
        //        txtAccountNo.Visible = false;
        //        lblAccountType.Visible = false;
        //        txtAccountType.Visible = false;
        //        txtOtherTrade.Visible = false;
        //    }
        //    else
        //    {
        //        lblAba.Visible = true;
        //        txtRoutingNo.Visible = true;
        //        lblAccount.Visible = true;
        //        txtAccountNo.Visible = true;
        //        lblAccountType.Visible = true;
        //        txtAccountType.Visible = true;
        //        txtOtherTrade.Visible = true;
        //    }
        //    #region Previous required fields




        //    //if (ddlstatus.SelectedValue == "Active" || ddlstatus.SelectedValue == "OfferMade")
        //    //{
        //    //    rqHireDate.Enabled = true;
        //    //    rqWorkCompCode.Enabled = true;
        //    //    rqEmpType.Enabled = true;
        //    //    rqPayRate.Enabled = true;
        //    //    //rqRoutingNo.Enabled = true;
        //    //    //rqAccountNo.Enabled = true;
        //    //    //rqAccountType.Enabled = true;
        //    //    rqdtResignition.Enabled = false;
        //    //    lblTermination.Visible = false;
        //    //    //lblNextReviewDate.Visible = false;
        //    //    //lblLastReview.Visible = false;
        //    //    //rqDtNewReview.Enabled = false;
        //    //    //rqLastReviewDate.Enabled = true;
        //    //    rqExtraEarnings.Enabled = true;
        //    //    rqExtraEarningAmt.Enabled = true;
        //    //    rqDeduction.Enabled = true;
        //    //    rqDeductionAmt.Enabled = true;
        //    //    rqDesignition.Enabled = true;
        //    //    //rqPrimaryTrade.Enabled = true;
        //    //    //rqSecondaryTrade.Enabled = true;
        //    //    rqFirstName.Enabled = true;
        //    //    rqEmail.Enabled = true;
        //    //    reEmail.Enabled = true;
        //    //    rqZip.Enabled = true;
        //    //    rqState.Enabled = true;
        //    //    lblStateReq.Visible = true;
        //    //    rqCity.Enabled = true;
        //    //    lblCityReq.Visible = true;
        //    //    lblCityReq.Visible = true;
        //    //    password.Enabled = true;
        //    //    rqConPass.Enabled = true;
        //    //    lblConfirmPass.Visible = true;
        //    //    rqSign.Enabled = true;
        //    //    lblReqSig.Visible = true;
        //    //    rqMaritalStatus.Enabled = true;
        //    //    lblReqMarSt.Visible = true;
        //    //    //rqNotes.Enabled = true;
        //    //    lblNotesReq.Visible = true;
        //    //    rqSource.Enabled = true;
        //    //    lblSourceReq.Visible = true;
        //    //    //rqLastName.Enabled = true;
        //    //    rqAddress.Enabled = true;
        //    //    lblAddressReq.Visible = true;
        //    //    //rqPhone.Enabled = true;
        //    //    lblPhoneReq.Visible = true;
        //    //    rqPass.Enabled = true;
        //    //    lblPassReq.Visible = true;
        //    //    lblPassReq.Visible = true;
        //    //    rqSSN1.Enabled = true;
        //    //    lblReqSSN.Visible = true;
        //    //    rqSSN2.Enabled = true;
        //    //    rqSSN3.Enabled = true;
        //    //    rqDOB.Enabled = true;
        //    //    rqPenalty.Enabled = true;
        //    //    lblReqPOP.Visible = true;
        //    //}
        //    //else
        //    //{
        //    //    rqHireDate.Enabled = false;
        //    //    rqWorkCompCode.Enabled = false;
        //    //    rqEmpType.Enabled = false;
        //    //    rqPayRate.Enabled = false;
        //    //    //rqRoutingNo.Enabled = false;
        //    //    //rqAccountNo.Enabled = false;
        //    //    //rqAccountType.Enabled = false;
        //    //    rqdtResignition.Enabled = false;
        //    //    //rqDtNewReview.Enabled = false;
        //    //    //rqLastReviewDate.Enabled = false;
        //    //    rqExtraEarnings.Enabled = false;
        //    //    rqExtraEarningAmt.Enabled = false;
        //    //    rqDeduction.Enabled = false;
        //    //    rqDeductionAmt.Enabled = false;
        //    //    rqDesignition.Enabled = false;
        //    //    //rqPrimaryTrade.Enabled = true;
        //    //    //rqSecondaryTrade.Enabled = true;
        //    //    rqFirstName.Enabled = true;
        //    //    rqEmail.Enabled = true;
        //    //    reEmail.Enabled = true;
        //    //    rqZip.Enabled = true;
        //    //    rqState.Enabled = true;
        //    //    lblStateReq.Visible = true;
        //    //    rqCity.Enabled = true;
        //    //    lblCityReq.Visible = true;
        //    //    lblCityReq.Visible = true;
        //    //    password.Enabled = true;
        //    //    rqConPass.Enabled = true;
        //    //    lblConfirmPass.Visible = false;
        //    //    rqSign.Enabled = false;
        //    //    lblReqSig.Visible = false;
        //    //    rqMaritalStatus.Enabled = false;
        //    //    lblReqMarSt.Visible = false;
        //    //    //rqNotes.Enabled = false;
        //    //    lblNotesReq.Visible = false;
        //    //    rqSource.Enabled = true;
        //    //    lblSourceReq.Visible = true;
        //    //    //rqLastName.Enabled = true;
        //    //    rqAddress.Enabled = true;
        //    //    lblAddressReq.Visible = true;
        //    //    //rqPhone.Enabled = true;
        //    //    lblPhoneReq.Visible = true;
        //    //    rqPass.Enabled = true;
        //    //    lblPassReq.Visible = true;
        //    //    rqSSN1.Enabled = false;
        //    //    lblReqSSN.Visible = false;
        //    //    rqSSN2.Enabled = false;
        //    //    rqSSN3.Enabled = false;
        //    //    rqDOB.Enabled = false;
        //    //    rqPenalty.Enabled = false;
        //    //    lblReqPOP.Visible = false;
        //    //}
        //    #endregion
        //    if (ddldesignation.SelectedItem.Text == "Installer")
        //    {
        //        lblInstallerType.Visible = true;
        //        ddlInstallerType.Visible = true;
        //    }
        //    else
        //    {
        //        lblInstallerType.Visible = false;
        //        ddlInstallerType.Visible = false;
        //    }
        //    if (ddlstatus.SelectedValue == "Applicant" || ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "PhoneScreened")
        //    {
        //        btnMinusNew.Visible = true;
        //        btnPlusNew.Visible = false;
        //        Panel3.Visible = true;
        //        Panel4.Visible = true;
        //    }
        //    else
        //    {
        //        btnMinusNew.Visible = false;
        //        btnPlusNew.Visible = true;
        //        Panel3.Visible = false;
        //        Panel4.Visible = false;
        //    }






        //    #region NewRequiredFields
        //    if (ddlstatus.SelectedValue == "Install Prospect")
        //    {
        //        rqDesignition.Enabled = false;
        //        RequiredFieldValidator3.Enabled = false;
        //        lblReqDesig.Visible = false;
        //        lblReqPtrade.Visible = true;
        //        //rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        //rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        //rqLastName.Enabled = true;
        //        //RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        //rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //      //  rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        //rqNotes.Enabled = false;
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
        //        rqSign.Enabled = false;
        //        lblReqMarSt.Visible = false;
        //        rqMaritalStatus.Enabled = false;
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
        //        lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //      //  rqSource.Enabled = true;
        //        lblConfirmPass.Visible = false;
        //    }
        //    else if (ddlstatus.SelectedValue == "Applicant")
        //    {
        //        rqDesignition.Enabled = false;
        //        RequiredFieldValidator3.Enabled = false;
        //        lblReqDesig.Visible = false;
        //        lblReqPtrade.Visible = true;
        //        //rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        //rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        //rqLastName.Enabled = true;
        //        //RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        //rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //       // rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        //rqNotes.Enabled = false;
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
        //        rqSign.Enabled = false;
        //        lblReqMarSt.Visible = false;
        //        rqMaritalStatus.Enabled = false;
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
        //        lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //       // rqSource.Enabled = true;
        //        lblConfirmPass.Visible = false;
        //    }
        //    else if (ddlstatus.SelectedValue == "OfferMade")
        //    {
        //        rqDesignition.Enabled = true;
        //        RequiredFieldValidator3.Enabled = true;
        //        lblReqDesig.Visible = true;
        //        lblReqPtrade.Visible = true;
        //        //rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        //rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        //rqLastName.Enabled = true;
        //        //RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        //rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //      //  rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        //rqNotes.Enabled = false;
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
        //        rqSign.Enabled = false;
        //        lblReqMarSt.Visible = false;
        //        rqMaritalStatus.Enabled = false;
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
        //        lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //      //  rqSource.Enabled = true;
        //        lblConfirmPass.Visible = false;
        //        lblReqHireDate.Visible = true;
        //        rqHireDate.Enabled = true;
        //        lblReqWWC.Visible = true;
        //        rqWorkCompCode.Enabled = true;
        //        lblReqPayRates.Visible = true;
        //        rqPayRate.Enabled = true;
        //        lblReqEmpType.Visible = true;
        //        rqEmpType.Enabled = true;
        //        #endregion
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
        //        return;
        //    }
        //    else if (ddlstatus.SelectedValue == "Active")
        //    {
        //        rqDesignition.Enabled = true;
        //        RequiredFieldValidator3.Enabled = true;
        //        lblReqDesig.Visible = true;
        //        lblReqPtrade.Visible = true;
        //        //rqPrimaryTrade.Enabled = true;
        //        lblReqSTrate.Visible = true;
        //        //rqSecondaryTrade.Enabled = true;
        //        lblReqLastName.Visible = true;
        //        //rqLastName.Enabled = true;
        //        //RequiredFieldValidator5.Enabled = true;
        //        lblReqFName.Visible = true;
        //        rqFirstName.Enabled = true;
        //        RequiredFieldValidator4.Enabled = true;
        //        lblPhoneReq.Visible = true;
        //        //rqPhone.Enabled = true;
        //        lblSourceReq.Visible = true;
        //      //  rqSource.Enabled = true;
        //        lblNotesReq.Visible = false;
        //        //rqNotes.Enabled = false;
        //        lblReqEmail.Visible = true;
        //        rqEmail.Enabled = true;
        //        reEmail.Enabled = true;
        //        lblReqZip.Visible = true;
        //        rqZip.Enabled = true;
        //        lblStateReq.Visible = true;
        //        rqState.Enabled = true;
        //        lblCityReq.Visible = true;
        //        rqCity.Enabled = true;
        //        lblPassReq.Visible = true;
        //        rqPass.Enabled = true;
        //        lblReqSig.Visible = true;
        //        lblConfirmPass.Visible = true;
        //        password.Enabled = true;
        //        rqConPass.Enabled = true;
        //        rqSign.Enabled = true;
        //        lblReqMarSt.Visible = true;
        //        rqMaritalStatus.Enabled = true;
        //        lblReqPicture.Visible = false;
        //        lblReqDL.Visible = false;
        //        txtDateSourced.Text = DateTime.Now.ToShortDateString();

        //        #region Req from NewHire
        //        lblAddressReq.Visible = true;
        //        rqAddress.Enabled = true;
        //        Label1.Visible = true;
        //        RequiredFieldValidator6.Enabled = true;
        //        lblConfirmPass.Visible = true;
        //        rqConPass.Enabled = true;
        //        lblReqSSN.Visible = true;
        //        rqSSN1.Enabled = true;
        //        rqSSN2.Enabled = true;
        //        rqSSN3.Enabled = true;
        //        lblReqDOB.Visible = true;
        //        rqDOB.Enabled = true;
        //        lblReqPOP.Visible = true;
        //        rqPenalty.Enabled = true;
        //       // rqSource.Enabled = true;
        //        lblConfirmPass.Visible = true;
        //        lblReqHireDate.Visible = true;
        //        rqHireDate.Enabled = true;
        //        lblReqWWC.Visible = true;
        //        rqWorkCompCode.Enabled = true;
        //        lblReqPayRates.Visible = true;
        //        rqPayRate.Enabled = true;
        //        lblReqEmpType.Visible = true;
        //        rqEmpType.Enabled = true;
        //        rqdtResignition.Enabled = false;
        //        lblTermination.Visible = false;
        //        #endregion
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
        //        //return;
        //    }
        //    else if (ddlstatus.SelectedValue == "Deactive")
        //    {
        //        rqDesignition.Enabled = false;
        //        RequiredFieldValidator3.Enabled = false;
        //        lblReqDesig.Visible = false;
        //        lblReqPtrade.Visible = false;
        //        //rqPrimaryTrade.Enabled = false;
        //        lblReqSTrate.Visible = false;
        //        //rqSecondaryTrade.Enabled = false;
        //        lblReqLastName.Visible = false;
        //        //rqLastName.Enabled = false;
        //        //RequiredFieldValidator5.Enabled = false;
        //        lblReqFName.Visible = false;
        //        rqFirstName.Enabled = false;
        //        RequiredFieldValidator4.Enabled = false;
        //        lblPhoneReq.Visible = false;
        //        //rqPhone.Enabled = false;
        //        lblSourceReq.Visible = false;
        //      //  rqSource.Enabled = false;
        //        lblNotesReq.Visible = false;
        //        //rqNotes.Enabled = false;
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
        //        rqSign.Enabled = false;
        //        lblReqMarSt.Visible = false;
        //        rqMaritalStatus.Enabled = false;
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
        //        lblReqDOB.Visible = false;
        //        rqDOB.Enabled = false;
        //        lblReqPOP.Visible = false;
        //        rqPenalty.Enabled = false;
        //     //   rqSource.Enabled = false;
        //        lblConfirmPass.Visible = false;
        //        lblReqHireDate.Visible = true;
        //        rqHireDate.Enabled = false;
        //        lblReqWWC.Visible = false;
        //        rqWorkCompCode.Enabled = false;
        //        lblReqPayRates.Visible = false;
        //        rqPayRate.Enabled = false;
        //        lblReqEmpType.Visible = false;
        //        rqEmpType.Enabled = false;
        //        RequiredFieldValidator7.Enabled = true;
        //        rqdtResignition.Enabled = true;
        //        lblTermination.Visible = true;
        //        dtResignation.Text = DateTime.Now.ToShortDateString();
        //        #endregion
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
        //        //return;
        //    }
        //    #endregion

        //}
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationSummary1.ValidationGroup = lnkSubmit.ValidationGroup = "submit";

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

                ddlInsteviewtime.Visible = false;
                dtInterviewDate.Visible = false;
                txtReson.Visible = false;
                RequiredFieldValidator7.Enabled = false;
            }
            else if (ddlstatus.SelectedValue == "Rejected")
            {
                ddlInsteviewtime.Visible = false;
                dtInterviewDate.Visible = false;
                RequiredFieldValidator7.Enabled = true;

                txtReson.Visible = true;
            }
            else if (ddlstatus.SelectedValue == "InterviewDate")
            {
                txtReson.Visible = false;
                RequiredFieldValidator7.Enabled = false;
                dtInterviewDate.Visible = true;
                dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
                ddlInsteviewtime.Visible = true;
                ddlInsteviewtime.SelectedValue = "10:00 AM";
            }
            else if (ddlstatus.SelectedValue == "Deactive")
            {
                ddlInsteviewtime.Visible = false;
                txtReson.Visible = true;
                RequiredFieldValidator7.Enabled = true;
                dtInterviewDate.Visible = false;
            }
            else if (ddlstatus.SelectedValue == "OfferMade")
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["id"])) &&
                    (string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtpassword.Text) || string.IsNullOrWhiteSpace(txtpassword1.Text)))
                {
                    ModalPopupExtender2.Show();
                    ddlstatus.SelectedValue = "Applicant";
                    return;
                }
                else
                {
                    ValidationSummary1.ValidationGroup = lnkSubmit.ValidationGroup = "OfferMade";
                    ddlInsteviewtime.Visible = false;
                    txtReson.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                    // RequiredFieldValidator7.Enabled = true;
                    dtInterviewDate.Visible = false;
                    pnlnewHire.Visible = true;
                    pnlNew2.Visible = true;
                    btnNewPluse.Visible = false;
                    btnNewMinus.Visible = true;
                }
            }
            else
            {
                ddlInsteviewtime.Visible = false;
                txtReson.Visible = false;
                RequiredFieldValidator7.Enabled = false;
                dtInterviewDate.Visible = false;
            }
            if (ddlstatus.SelectedValue == "Active")
            {
                //txtHireDate.Text = DateTime.Now.ToShortDateString();
                lblReqDL.Style["display"] = "block";
                lblReqPicture.Style["display"] = "block";
                dtReviewDate.Text = DateTime.Now.AddDays(30).ToShortDateString();
                pnlFngPrint.Visible = true;
                pnlGrid.Visible = true;
                pnl4.Visible = false;
                pnlnewHire.Visible = true;
                pnlNew2.Visible = true;
                btnNewPluse.Visible = false;
                btnNewMinus.Visible = true;
            }
            else
            {
                pnlFngPrint.Visible = false;
                pnlGrid.Visible = false;
                pnlnewHire.Visible = false;
                pnlNew2.Visible = false;
                btnNewPluse.Visible = true;
                btnNewMinus.Visible = false;
                pnl4.Visible = false;
            }
            if (ddlstatus.SelectedValue == "Deactive")
            {
                dtResignation.Text = DateTime.Now.ToShortDateString();
            }
            if (ddlstatus.SelectedValue == "OfferMade")
            {
                rqdtResignition.Enabled = false;
                pnlnewHire.Visible = true;
                pnlAll.Visible = true;
                pnlNew2.Visible = true;
                btnNewPluse.Visible = false;
                btnNewMinus.Visible = true;
                //rqDtNewReview.Enabled = false;
                //rqLastReviewDate.Enabled = false;
            }
            else if (ddlstatus.SelectedValue == "Active")
            {
                rqdtResignition.Enabled = true;
                //rqDtNewReview.Enabled = true;
                //rqLastReviewDate.Enabled = true;
            }
            else
            {
                rqdtResignition.Enabled = false;
                //rqDtNewReview.Enabled = false;
                //rqLastReviewDate.Enabled = false;
            }
            if (ddlstatus.SelectedValue == "Active" && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
            {
                lblReqPicture.Style["display"] = "block";
                lblReqDL.Style["display"] = "block";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
                if (Convert.ToString(Session["ddlStatus"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
                }
                //else
                //{
                //    ddlstatus.SelectedValue = "Applicant";
                //}
                return;
            }

            if (ddlstatus.SelectedValue == "Deactive" && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
                if (Convert.ToString(Session["ddlStatus"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Session["ddlStatus"]);
                }
                //else
                //{
                //    ddlstatus.SelectedValue = "Applicant";
                //}
                return;
            }
            if ((ddlstatus.SelectedValue == "OfferMade") && (ddldesignation.SelectedItem.Text == "ForeMan" || ddldesignation.SelectedItem.Text == "Installer"))
            {
                pnlAll.Visible = true;
                txtReson.Visible = false;
                //pnlnewHire.Visible = true;
                RequiredFieldValidator7.Enabled = true;


            }
            else if ((ddlstatus.SelectedValue == "Active"))
            {

                pnlAll.Visible = true;
                txtReson.Visible = false;
                //pnlnewHire.Visible = true;
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                pnlAll.Visible = false;
            }
            if (rdoCheque.Checked)
            {
                lblAba.Visible = false;
                txtRoutingNo.Visible = false;
                lblAccount.Visible = false;
                txtAccountNo.Visible = false;
                lblAccountType.Visible = false;
                txtAccountType.Visible = false;
                txtOtherTrade.Visible = false;
            }
            else
            {
                lblAba.Visible = true;
                txtRoutingNo.Visible = true;
                lblAccount.Visible = true;
                txtAccountNo.Visible = true;
                lblAccountType.Visible = true;
                txtAccountType.Visible = true;
                txtOtherTrade.Visible = true;
            }
            #region Previous required fields




            //if (ddlstatus.SelectedValue == "Active" || ddlstatus.SelectedValue == "OfferMade")
            //{
            //    rqHireDate.Enabled = true;
            //    rqWorkCompCode.Enabled = true;
            //    rqEmpType.Enabled = true;
            //    rqPayRate.Enabled = true;
            //    //rqRoutingNo.Enabled = true;
            //    //rqAccountNo.Enabled = true;
            //    //rqAccountType.Enabled = true;
            //    rqdtResignition.Enabled = false;
            //    lblTermination.Visible = false;
            //    //lblNextReviewDate.Visible = false;
            //    //lblLastReview.Visible = false;
            //    //rqDtNewReview.Enabled = false;
            //    //rqLastReviewDate.Enabled = true;
            //    rqExtraEarnings.Enabled = true;
            //    rqExtraEarningAmt.Enabled = true;
            //    rqDeduction.Enabled = true;
            //    rqDeductionAmt.Enabled = true;
            //    rqDesignition.Enabled = true;
            //    //rqPrimaryTrade.Enabled = true;
            //    //rqSecondaryTrade.Enabled = true;
            //    rqFirstName.Enabled = true;
            //    rqEmail.Enabled = true;
            //    reEmail.Enabled = true;
            //    rqZip.Enabled = true;
            //    rqState.Enabled = true;
            //    lblStateReq.Visible = true;
            //    rqCity.Enabled = true;
            //    lblCityReq.Visible = true;
            //    lblCityReq.Visible = true;
            //    password.Enabled = true;
            //    rqConPass.Enabled = true;
            //    lblConfirmPass.Visible = true;
            //    rqSign.Enabled = true;
            //    lblReqSig.Visible = true;
            //    rqMaritalStatus.Enabled = true;
            //    lblReqMarSt.Visible = true;
            //    //rqNotes.Enabled = true;
            //    lblNotesReq.Visible = true;
            //    rqSource.Enabled = true;
            //    lblSourceReq.Visible = true;
            //    //rqLastName.Enabled = true;
            //    rqAddress.Enabled = true;
            //    lblAddressReq.Visible = true;
            //    //rqPhone.Enabled = true;
            //    lblPhoneReq.Visible = true;
            //    rqPass.Enabled = true;
            //    lblPassReq.Visible = true;
            //    lblPassReq.Visible = true;
            //    rqSSN1.Enabled = true;
            //    lblReqSSN.Visible = true;
            //    rqSSN2.Enabled = true;
            //    rqSSN3.Enabled = true;
            //    rqDOB.Enabled = true;
            //    rqPenalty.Enabled = true;
            //    lblReqPOP.Visible = true;
            //}
            //else
            //{
            //    rqHireDate.Enabled = false;
            //    rqWorkCompCode.Enabled = false;
            //    rqEmpType.Enabled = false;
            //    rqPayRate.Enabled = false;
            //    //rqRoutingNo.Enabled = false;
            //    //rqAccountNo.Enabled = false;
            //    //rqAccountType.Enabled = false;
            //    rqdtResignition.Enabled = false;
            //    //rqDtNewReview.Enabled = false;
            //    //rqLastReviewDate.Enabled = false;
            //    rqExtraEarnings.Enabled = false;
            //    rqExtraEarningAmt.Enabled = false;
            //    rqDeduction.Enabled = false;
            //    rqDeductionAmt.Enabled = false;
            //    rqDesignition.Enabled = false;
            //    //rqPrimaryTrade.Enabled = true;
            //    //rqSecondaryTrade.Enabled = true;
            //    rqFirstName.Enabled = true;
            //    rqEmail.Enabled = true;
            //    reEmail.Enabled = true;
            //    rqZip.Enabled = true;
            //    rqState.Enabled = true;
            //    lblStateReq.Visible = true;
            //    rqCity.Enabled = true;
            //    lblCityReq.Visible = true;
            //    lblCityReq.Visible = true;
            //    password.Enabled = true;
            //    rqConPass.Enabled = true;
            //    lblConfirmPass.Visible = false;
            //    rqSign.Enabled = false;
            //    lblReqSig.Visible = false;
            //    rqMaritalStatus.Enabled = false;
            //    lblReqMarSt.Visible = false;
            //    //rqNotes.Enabled = false;
            //    lblNotesReq.Visible = false;
            //    rqSource.Enabled = true;
            //    lblSourceReq.Visible = true;
            //    //rqLastName.Enabled = true;
            //    rqAddress.Enabled = true;
            //    lblAddressReq.Visible = true;
            //    //rqPhone.Enabled = true;
            //    lblPhoneReq.Visible = true;
            //    rqPass.Enabled = true;
            //    lblPassReq.Visible = true;
            //    rqSSN1.Enabled = false;
            //    lblReqSSN.Visible = false;
            //    rqSSN2.Enabled = false;
            //    rqSSN3.Enabled = false;
            //    rqDOB.Enabled = false;
            //    rqPenalty.Enabled = false;
            //    lblReqPOP.Visible = false;
            //}
            #endregion
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
            if (ddlstatus.SelectedValue == "Applicant" || ddlstatus.SelectedValue == "InterviewDate" || ddlstatus.SelectedValue == "PhoneScreened")
            {
                btnMinusNew.Visible = true;
                btnPlusNew.Visible = false;
                Panel3.Visible = true;
                Panel4.Visible = true;
            }
            else
            {
                btnMinusNew.Visible = false;
                btnPlusNew.Visible = true;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }






            #region NewRequiredFields
            if (ddlstatus.SelectedValue == "Install Prospect")
            {
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                lblReqDesig.Visible = false;
                lblReqPtrade.Visible = true;
                //rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                //rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                //rqLastName.Enabled = true;
                //RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //  rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                //rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                //rqEmail.Enabled = false;
                //reEmail.Enabled = true;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                //rqPass.Enabled = false;
                lblReqSig.Visible = false;
                rqSign.Enabled = false;
                lblReqMarSt.Visible = false;
                rqMaritalStatus.Enabled = false;
                // lblReqPicture.Visible = false;
                lblReqPicture.Style["display"] = "none";
                //   lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "none";
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = true;
                //rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                //  rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
            }
            else if (ddlstatus.SelectedValue == "Applicant")
            {
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                lblReqDesig.Visible = false;
                lblReqPtrade.Visible = true;
                //rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                //rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                //rqLastName.Enabled = true;
                //RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                // rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                //rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                //rqEmail.Enabled = false;
                //reEmail.Enabled = true;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                //rqPass.Enabled = false;
                lblReqSig.Visible = false;
                rqSign.Enabled = false;
                lblReqMarSt.Visible = false;
                rqMaritalStatus.Enabled = false;
                //  lblReqPicture.Visible = false;
                lblReqPicture.Style["display"] = "none";

                //  lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "none";
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = true;
                //rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                // rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
            }
            else if (ddlstatus.SelectedValue == "OfferMade")
            {
                rqDesignition.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                lblReqDesig.Visible = true;
                lblReqPtrade.Visible = true;
                //rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                //rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                //rqLastName.Enabled = true;
                //RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //  rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                //rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                //rqEmail.Enabled = false;
                //reEmail.Enabled = true;
                lblReqZip.Visible = true;
                rqZip.Enabled = true;
                lblStateReq.Visible = true;
                rqState.Enabled = true;
                lblCityReq.Visible = true;
                rqCity.Enabled = true;
                lblPassReq.Visible = false;
                //rqPass.Enabled = false;
                lblReqSig.Visible = false;
                rqSign.Enabled = false;
                lblReqMarSt.Visible = false;
                rqMaritalStatus.Enabled = false;
                //  lblReqPicture.Visible = false;
                lblReqPicture.Style["display"] = "none";
                //  lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "none";

                #region Req from NewHire
                lblAddressReq.Visible = true;
                rqAddress.Enabled = true;
                Label1.Visible = true;
                RequiredFieldValidator6.Enabled = true;
                lblConfirmPass.Visible = true;
                //rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                //  rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
                lblReqHireDate.Visible = true;
                rqHireDate.Enabled = true;
                lblReqWWC.Visible = true;
                rqWorkCompCode.Enabled = true;
                lblReqPayRates.Visible = true;
                rqPayRate.Enabled = true;
                lblReqEmpType.Visible = true;
                rqEmpType.Enabled = true;
                #endregion
                string lMessage = "";
                if (txtemail.Text == "")
                {
                    lMessage += "Email is required\n";
                }
                if (txtpassword.Text == "")
                {
                    lMessage += "Password is required\n";
                }
                else
                {
                    if(txtpassword1.Text != txtpassword.Text){
                        lMessage += "Password and Confirm password does not match\n";
                    }
                }
                if (lMessage!="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + lMessage + "')", true);
                    return;
                }
                

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
                //return;
            }
            else if (ddlstatus.SelectedValue == "Active")
            {
                rqDesignition.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                lblReqDesig.Visible = true;
                lblReqPtrade.Visible = true;
                //rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                //rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                //rqLastName.Enabled = true;
                //RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                //  rqSource.Enabled = true;



                lblNotesReq.Visible = false;
                //rqNotes.Enabled = false;
                lblReqEmail.Visible = true;
                //rqEmail.Enabled = true;
                //reEmail.Enabled = true;
                lblReqZip.Visible = true;
                rqZip.Enabled = true;
                lblStateReq.Visible = true;
                rqState.Enabled = true;
                lblCityReq.Visible = true;
                rqCity.Enabled = true;
                lblPassReq.Visible = true;
                //rqPass.Enabled = true;
                lblReqSig.Visible = true;
                lblConfirmPass.Visible = true;
                password.Enabled = true;
                rqConPass.Enabled = true;
                rqSign.Enabled = true;
                lblReqMarSt.Visible = true;
                rqMaritalStatus.Enabled = true;
                //  lblReqPicture.Visible = true;
                lblReqPicture.Style["display"] = "block";

                // lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "block";
                txtDateSourced.Text = DateTime.Now.ToShortDateString();

                #region Req from NewHire
                lblAddressReq.Visible = true;
                rqAddress.Enabled = true;
                Label1.Visible = true;
                RequiredFieldValidator6.Enabled = true;
                lblConfirmPass.Visible = true;
                rqConPass.Enabled = true;
                lblReqSSN.Visible = true;
                rqSSN1.Enabled = true;
                rqSSN2.Enabled = true;
                rqSSN3.Enabled = true;
                lblReqDOB.Visible = true;
                rqDOB.Enabled = true;
                lblReqPOP.Visible = true;
                rqPenalty.Enabled = true;
                // rqSource.Enabled = true;
                lblConfirmPass.Visible = true;
                lblReqHireDate.Visible = true;
                rqHireDate.Enabled = true;
                lblReqWWC.Visible = true;
                rqWorkCompCode.Enabled = true;
                lblReqPayRates.Visible = true;
                rqPayRate.Enabled = true;
                lblReqEmpType.Visible = true;
                rqEmpType.Enabled = true;
                rqdtResignition.Enabled = false;
                lblTermination.Visible = false;
                //  lblReqPicture.Visible = true;
                lblReqPicture.Style["display"] = "block";
                lblReqDL.Style["display"] = "block";

                #endregion
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
                return;
            }
            else if (ddlstatus.SelectedValue == "Deactive")
            {
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                lblReqDesig.Visible = false;
                lblReqPtrade.Visible = false;
                //rqPrimaryTrade.Enabled = false;
                lblReqSTrate.Visible = false;
                //rqSecondaryTrade.Enabled = false;
                lblReqLastName.Visible = false;
                //rqLastName.Enabled = false;
                //RequiredFieldValidator5.Enabled = false;
                lblReqFName.Visible = false;
                rqFirstName.Enabled = false;
                RequiredFieldValidator4.Enabled = false;
                lblPhoneReq.Visible = false;
                //rqPhone.Enabled = false;
                lblSourceReq.Visible = false;
                //  rqSource.Enabled = false;
                lblNotesReq.Visible = false;
                ////rqNotes.Enabled = false;
                lblReqEmail.Visible = false;
                //rqEmail.Enabled = false;
                //reEmail.Enabled = false;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                //rqPass.Enabled = false;
                lblReqSig.Visible = false;
                lblConfirmPass.Visible = false;
                password.Enabled = false;
                //rqConPass.Enabled = false;
                rqSign.Enabled = false;
                lblReqMarSt.Visible = false;
                rqMaritalStatus.Enabled = false;
                lblReqPicture.Style["display"] = "none";
                //lblReqPicture.Visible = false;
                // lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "none";
                //txtDateSourced.Text = DateTime.Now.ToShortDateString();
                #region Req from NewHire
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = false;
                //rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                //   rqSource.Enabled = false;
                lblConfirmPass.Visible = false;
                lblReqHireDate.Visible = true;
                rqHireDate.Enabled = false;
                lblReqWWC.Visible = false;
                rqWorkCompCode.Enabled = false;
                lblReqPayRates.Visible = false;
                rqPayRate.Enabled = false;
                lblReqEmpType.Visible = false;
                rqEmpType.Enabled = false;
                RequiredFieldValidator7.Enabled = true;
                rqdtResignition.Enabled = true;
                lblTermination.Visible = true;
                dtResignation.Text = DateTime.Now.ToShortDateString();
                #endregion
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fill new hire section above')", true);
                //return;
            }
            else if (ddlstatus.SelectedValue == "InterviewDate")
            {
                rqDesignition.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                lblReqDesig.Visible = false;
                lblReqPtrade.Visible = true;
                //rqPrimaryTrade.Enabled = true;
                lblReqSTrate.Visible = true;
                //rqSecondaryTrade.Enabled = true;
                lblReqLastName.Visible = true;
                //rqLastName.Enabled = true;
                //RequiredFieldValidator5.Enabled = true;
                lblReqFName.Visible = true;
                rqFirstName.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                lblPhoneReq.Visible = true;
                //rqPhone.Enabled = true;
                lblSourceReq.Visible = true;
                // rqSource.Enabled = true;
                lblNotesReq.Visible = false;
                ////rqNotes.Enabled = false;
                lblReqEmail.Visible = true;
                //rqEmail.Enabled = true;
                //reEmail.Enabled = true;
                lblReqZip.Visible = false;
                rqZip.Enabled = false;
                lblStateReq.Visible = false;
                rqState.Enabled = false;
                lblCityReq.Visible = false;
                rqCity.Enabled = false;
                lblPassReq.Visible = false;
                //rqPass.Enabled = false;
                lblReqSig.Visible = false;
                rqSign.Enabled = false;
                lblReqMarSt.Visible = false;
                rqMaritalStatus.Enabled = false;
                // lblReqPicture.Visible = false;
                lblReqPicture.Style["display"] = "none";
                // lblReqDL.Visible = false;
                lblReqDL.Style["display"] = "none";
                lblAddressReq.Visible = false;
                rqAddress.Enabled = false;
                Label1.Visible = false;
                RequiredFieldValidator6.Enabled = false;
                lblConfirmPass.Visible = true;
                //rqConPass.Enabled = false;
                lblReqSSN.Visible = false;
                rqSSN1.Enabled = false;
                rqSSN2.Enabled = false;
                rqSSN3.Enabled = false;
                lblReqDOB.Visible = false;
                rqDOB.Enabled = false;
                lblReqPOP.Visible = false;
                rqPenalty.Enabled = false;
                // rqSource.Enabled = true;
                lblConfirmPass.Visible = false;
            }
            #endregion
            InstallUserBLL.Instance.UpdateInstallUserStatus(ddlstatus.SelectedValue,  Convert.ToInt32(Session["ID"]));
            
        }

        //protected void btnPassword_Click(object sender, EventArgs e)
        //{
        //    int isvaliduser = 0;
        //    isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtUserPassword.Text);
        //    if (isvaliduser == 0)
        //    {
        //        ddlstatus.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your password is incorrect.')",true);
        //        return;
        //    }
        //}
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            int isvaliduser = 0;
            isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtUserPassword.Text);
            if (isvaliduser > 0)
            {
                InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReson.Text);
                //Status changes....
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
                else if (ddlstatus.SelectedValue == "OfferMade")
                {
                    ddlInsteviewtime.Visible = false;
                    txtReson.Visible = false;
                    RequiredFieldValidator7.Enabled = false;
                    // RequiredFieldValidator7.Enabled = true;
                    dtInterviewDate.Visible = false;
                    pnlnewHire.Visible = true;
                    pnlNew2.Visible = true;
                    btnNewPluse.Visible = false;
                    btnNewMinus.Visible = true;
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
                if (txtWarrantyPolicy.Text != "")
                {
                    Counter = 12;
                }
                if (txtSalRequirement.Text != "")
                {
                    Counter = 13;
                }
                //if (txtPrinciple.Text != "")
                //{
                //    Counter = 14;
                //}
                //if (txtName.Text != "")
                //{
                //    Counter = 15;
                //}
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
                //if (txtCEO.Text != "")
                //{
                //    Counter = 20;
                //}
                //if (txtLeagalOfficer.Text != "")
                //{
                //    Counter = 21;
                //}
                //if (txtPresident.Text != "")
                //{
                //    Counter = 22;
                //}
                //if (txtSoleProprietorShip.Text != "")
                //{
                //    Counter = 23;
                //}
                //if (txtPartnetsName.Text != "")
                //{
                //    Counter = 24;
                //}
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
        protected void ddlstatus_DataBinding(object sender, EventArgs e)
        {
            foreach (System.Web.UI.WebControls.ListItem myItem in ddlstatus.Items)
            {
                switch (myItem.Value)
                {
                    case "OfferMade":
                        myItem.Attributes.Add("style", "background-color:#00008B");
                        break;
                    default:
                        myItem.Attributes.Add("style", "background-color:#111111");
                        break;
                }
                //Do some things to determine the color of the item
                //Set the item background-color like so:
                //myItem.Attributes.Add("style", "background-color:#111111");
            }
        }

        protected void ddlstatus_PreRender(object sender, EventArgs e)
        {
            string imageURL = "";
            for (int i = 0; i < ddlstatus.Items.Count; i++)
            {
                switch (ddlstatus.Items[i].Value)
                {
                    case "Applicant": imageURL = "../Sr_App/img/red-astrek.png";
                        ddlstatus.Items[i].Attributes["data-image"] = imageURL;
                        break;
                    case "OfferMade": imageURL = "../Sr_App/img/dark-blue-astrek.png";
                        ddlstatus.Items[i].Attributes["data-image"] = imageURL;
                        break;
                    case "PhoneScreened": imageURL = "../Sr_App/img/yellow-astrek.png";
                        ddlstatus.Items[i].Attributes["data-image"] = imageURL;
                        break;
                    case "Active": imageURL = "../Sr_App/img/green-astrek.png";
                        ddlstatus.Items[i].Attributes["data-image"] = imageURL;
                        break;
                    case "InterviewDate": imageURL = "../Sr_App/img/purple-astrek.png";
                        ddlstatus.Items[i].Attributes["data-image"] = imageURL;
                        break;
                    default:
                        ddlstatus.Items[i].Attributes["data-image"] = "../Sr_App/img/white-astrek.png";
                        break;
                }
                //System.Web.UI.WebControls.ListItem item = ddlCountry.Items[i];
                //item.Attributes["data-image"] = imageURL;
            }
        }

    }
}