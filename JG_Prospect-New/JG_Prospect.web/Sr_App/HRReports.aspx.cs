using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;
using System.Data;
using JG_Prospect.BLL;
namespace JG_Prospect.Sr_App
{
    public partial class HRReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alsert('Your session has expired,login to contineu');window.location='../login.aspx'", true);
            }
            if (!IsPostBack)
            {
                FillCustomer();
                DataSet dsCurrentPeriod = UserBLL.Instance.Getcurrentperioddates();
                bindPayPeriod(dsCurrentPeriod);
            }
        }

        private void FillCustomer()
        {
            DataSet dds = new DataSet();
            dds = new_customerBLL.Instance.GeUsersForDropDown();
            DataRow dr = dds.Tables[0].NewRow();
            dr["Id"] = "0";
            dr["Username"] = "--Select--";
            dds.Tables[0].Rows.InsertAt(dr, 0);
            if (dds.Tables[0].Rows.Count > 0)
            {
                ddlUsers.DataSource = dds.Tables[0];
                ddlUsers.DataValueField = "Id";
                ddlUsers.DataTextField = "Username";
                ddlUsers.DataBind();
            }

        }

        private void bindPayPeriod(DataSet dsCurrentPeriod)
        {
            DataSet ds = UserBLL.Instance.getallperiod();

            if (ds.Tables[0].Rows.Count > 0)
            {
                drpPayPeriod.Items.Insert(0, new ListItem("Select", "0"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    drpPayPeriod.Items.Add(new ListItem(dr["Periodname"].ToString(), dr["Id"].ToString()));
                }
                drpPayPeriod.SelectedValue = dsCurrentPeriod.Tables[0].Rows[0]["Id"].ToString();
                txtDtFrom.Text = Convert.ToDateTime(dsCurrentPeriod.Tables[0].Rows[0]["FromDate"].ToString()).ToString("MM/dd/yyyy");
                txtDtTo.Text = Convert.ToDateTime(dsCurrentPeriod.Tables[0].Rows[0]["ToDate"].ToString()).ToString("MM/dd/yyyy");
            }
            else
            {
                drpPayPeriod.DataSource = null;
                drpPayPeriod.DataBind();
            }

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataSet dsRejected = new DataSet();
            DateTime fromDate = Convert.ToDateTime(txtDtFrom.Text, JG_Prospect.Common.JGConstant.CULTURE);
            DateTime toDate = Convert.ToDateTime(txtDtTo.Text, JG_Prospect.Common.JGConstant.CULTURE);
            try
            {
                ds = InstallUserBLL.Instance.GetHrData(fromDate, toDate, Convert.ToInt16(ddlUsers.SelectedValue));
                if (ddlUsers.SelectedValue == "0")
                {
                    if (txtDtFrom.Text != "" && txtDtTo.Text != "")
                    {
                        //ds = new_customerBLL.Instance.GetHRCount("", txtDtFrom.Text, txtDtTo.Text);
                        dsRejected = new_customerBLL.Instance.GetRejected("", txtDtFrom.Text, txtDtTo.Text);
                    }
                    else if (txtDtFrom.Text == "" && txtDtTo.Text == "")
                    {
                        //ds = new_customerBLL.Instance.GetHRCount("", "", "");
                        dsRejected = new_customerBLL.Instance.GetRejected("", "", "");
                    }
                }

                if (ds != null)
                {
                    DataTable dtHrData = ds.Tables[0];
                    List<HrData> lstHrData = new List<HrData>();
                    foreach (DataRow row in dtHrData.Rows)
                    {
                        HrData hrdata = new HrData();
                        hrdata.status = row["status"].ToString();
                        hrdata.count = row["cnt"].ToString();
                        lstHrData.Add(hrdata);
                    }

                    if (dtHrData.Rows.Count > 0)
                    {
                        var rowActive = lstHrData.Where(r => r.status == "Active").FirstOrDefault();
                        if (rowActive != null)
                        {
                            string count = rowActive.count;
                            lblActive.Text = count;
                        }
                        else
                        {
                            lblActive.Text = "0";
                        }
                        var rowRejected = lstHrData.Where(r => r.status == "Rejected").FirstOrDefault();
                        if (rowRejected != null)
                        {
                            string count = rowRejected.count;
                            lblRejected.Text = count;
                        }
                        else
                        {
                            lblRejected.Text = "0";
                        }
                        var rowApplicant = lstHrData.Where(r => r.status == "Applicant").FirstOrDefault();
                        string Applicantcount = "0";
                        if (rowApplicant != null)
                        {
                            Applicantcount = rowApplicant.count;
                            lblApplicant.Text = Applicantcount;
                        }
                        else
                        {
                            Applicantcount = "0";
                            lblApplicant.Text = "0";
                        }
                        var rowPhoneScreened = lstHrData.Where(r => r.status == "PhoneScreened").FirstOrDefault();
                        if (rowPhoneScreened != null)
                        {
                            string count = rowPhoneScreened.count;
                            lblPhoneVideo.Text = count;
                        }
                        else
                        {
                            lblPhoneVideo.Text = "0";
                        }
                        var rowInterviewDate = lstHrData.Where(r => r.status == "InterviewDate").FirstOrDefault();
                        if (rowInterviewDate != null)
                        {
                            string count = rowInterviewDate.count;
                            lblInterviewDate.Text = count;
                        }
                        else
                        {
                            lblInterviewDate.Text = "0";
                        }

                        // Ratio calculations
                        lblAppInterRatio.Text = Convert.ToString(Convert.ToDouble(lblInterviewDate.Text) / Convert.ToDouble(lblApplicant.Text));
                        lblAppHireRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblApplicant.Text));
                        lblInterNewRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblInterviewDate.Text));
                    }
                    else
                    {
                        lblActive.Text = "0";
                        lblRejected.Text = "0";
                        lblApplicant.Text = "0";
                        lblInterviewDate.Text = "0";
                        lblPhoneVideo.Text = "0";
                        lblAppInterRatio.Text = "0";
                        lblAppHireRatio.Text = "0";
                        lblInterNewRatio.Text = "0";
                    }
                }
                //else if (ddlUsers.SelectedValue != "0")
                //{
                //    if (txtDtFrom.Text != "" && txtDtTo.Text != "")
                //    {
                //        ds = new_customerBLL.Instance.GetHRCount(ddlUsers.SelectedValue, txtDtFrom.Text, txtDtTo.Text);
                //    }
                //    else if (txtDtFrom.Text == "" && txtDtTo.Text == "")
                //    {
                //        ds = new_customerBLL.Instance.GetHRCount(ddlUsers.SelectedValue, "", "");
                //    }
                //}


                if (dsRejected.Tables.Count > 0)
                {
                    //if (dsRejected.Tables[0].Rows.Count > 0)
                    //{
                    //    rptCustomers.DataSource = dsRejected.Tables[0];
                    //    rptCustomers.DataBind();
                    //}
                    rptCustomers.DataSource = dsRejected.Tables[0];
                    rptCustomers.DataBind();
                }
                if (ds.Tables.Count > 0)
                {
                    //string expression = "";
                    //if (ddlUsers.SelectedValue == "0")
                    //{
                    //expression = "Status = 'Applicant'";
                    //DataRow[] resultApp = ds.Tables[0].Select(expression);
                    //lblApplicant.Text = Convert.ToString(resultApp.Length);
                    //expression = "Status = 'InterviewDate'";
                    //DataRow[] resultInDt = ds.Tables[0].Select(expression);
                    //lblInterviewDate.Text = Convert.ToString(resultInDt.Length);
                    //expression = "Status = 'PhoneScreened'";
                    //DataRow[] resultPS = ds.Tables[0].Select(expression);
                    //lblPhoneVideo.Text = Convert.ToString(resultPS.Length);
                    //expression = "Status = 'Rejected'";
                    //DataRow[] resultR = ds.Tables[0].Select(expression);
                    //lblRejected.Text = Convert.ToString(resultR.Length);
                    //expression = "Status = 'Active'";
                    //DataRow[] resultA = ds.Tables[0].Select(expression);
                    //lblActive.Text = Convert.ToString(resultA.Length);
                    //////Retio calculations
                    //lblAppInterRatio.Text = Convert.ToString(Convert.ToDouble(lblInterviewDate.Text) / Convert.ToDouble(lblApplicant.Text));
                    //lblAppHireRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblApplicant.Text));
                    //lblInterNewRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblInterviewDate.Text));
                    //}
                    //else if (ddlUsers.SelectedValue != "0")
                    //{
                    //    expression = "Status = 'Applicant' AND SourceId = "+Convert.ToInt32(ddlUsers.SelectedValue);
                    //    DataRow[] resultApp = ds.Tables[0].Select(expression);
                    //    lblApplicant.Text = Convert.ToString(resultApp.Length);
                    //    expression = "Status = 'InterviewDate' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                    //    DataRow[] resultInDt = ds.Tables[0].Select(expression);
                    //    lblInterviewDate.Text = Convert.ToString(resultInDt.Length);
                    //    expression = "Status = 'PhoneScreened' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                    //    DataRow[] resultPS = ds.Tables[0].Select(expression);
                    //    lblPhoneVideo.Text = Convert.ToString(resultPS.Length);
                    //    expression = "Status = 'Rejected' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                    //    DataRow[] resultR = ds.Tables[0].Select(expression);
                    //    lblRejected.Text = Convert.ToString(resultR.Length);
                    //    expression = "Status = 'Active' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                    //    DataRow[] resultA = ds.Tables[0].Select(expression);
                    //    lblActive.Text = Convert.ToString(resultA.Length);
                    //    ////Retio calculations
                    //    lblAppInterRatio.Text = Convert.ToString(Convert.ToDouble(lblInterviewDate.Text) / Convert.ToDouble(lblApplicant.Text));
                    //    lblAppHireRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblApplicant.Text));
                    //    lblInterNewRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblInterviewDate.Text));
                    //}
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
        protected void drpPayPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpPayPeriod.SelectedIndex != -1)
            {
                DataSet ds = UserBLL.Instance.getperioddetails(Convert.ToInt16(drpPayPeriod.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDtFrom.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"].ToString()).ToString("MM/dd/yyyy");
                    txtDtTo.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"].ToString()).ToString("MM/dd/yyyy");
                }
            }
        }
    }
}