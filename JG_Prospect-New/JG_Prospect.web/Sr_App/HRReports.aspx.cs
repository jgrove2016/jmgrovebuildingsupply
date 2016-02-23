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
                txtDtFrom.Text = DateTime.Today.AddDays(-14).ToShortDateString();
                txtDtTo.Text = DateTime.Today.ToShortDateString();
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

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataSet dsRejected = new DataSet();
            try
            {
                if (ddlUsers.SelectedValue == "0")
                {
                    if (txtDtFrom.Text != "" && txtDtTo.Text != "")
                    {
                        ds = new_customerBLL.Instance.GetHRCount("", txtDtFrom.Text, txtDtTo.Text);
                        dsRejected = new_customerBLL.Instance.GetRejected("", txtDtFrom.Text, txtDtTo.Text);
                    }
                    else if (txtDtFrom.Text == "" && txtDtTo.Text == "")
                    {
                        ds = new_customerBLL.Instance.GetHRCount("", "", "");
                        dsRejected = new_customerBLL.Instance.GetRejected("", "", "");
                    }
                }
                else if (ddlUsers.SelectedValue != "0")
                {
                    if (txtDtFrom.Text != "" && txtDtTo.Text != "")
                    {
                        ds = new_customerBLL.Instance.GetHRCount(ddlUsers.SelectedValue, txtDtFrom.Text, txtDtTo.Text);
                    }
                    else if (txtDtFrom.Text == "" && txtDtTo.Text == "")
                    {
                        ds = new_customerBLL.Instance.GetHRCount(ddlUsers.SelectedValue, "", "");
                    }
                }


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
                    string expression = "";
                    if (ddlUsers.SelectedValue == "0")
                    {
                        expression = "Status = 'Applicant'";
                        DataRow[] resultApp = ds.Tables[0].Select(expression);
                        lblApplicant.Text = Convert.ToString(resultApp.Length);
                        expression = "Status = 'InterviewDate'";
                        DataRow[] resultInDt = ds.Tables[0].Select(expression);
                        lblInterviewDate.Text = Convert.ToString(resultInDt.Length);
                        expression = "Status = 'PhoneScreened'";
                        DataRow[] resultPS = ds.Tables[0].Select(expression);
                        lblPhoneVideo.Text = Convert.ToString(resultPS.Length);
                        expression = "Status = 'Rejected'";
                        DataRow[] resultR = ds.Tables[0].Select(expression);
                        lblRejected.Text = Convert.ToString(resultR.Length);
                        expression = "Status = 'Active'";
                        DataRow[] resultA = ds.Tables[0].Select(expression);
                        lblActive.Text = Convert.ToString(resultA.Length);
                        ////Retio calculations
                        lblAppInterRatio.Text = Convert.ToString(Convert.ToDouble(lblInterviewDate.Text) / Convert.ToDouble(lblApplicant.Text));
                        lblAppHireRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblApplicant.Text));
                        lblInterNewRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblInterviewDate.Text));
                    }
                    else if (ddlUsers.SelectedValue != "0")
                    {
                        expression = "Status = 'Applicant' AND SourceId = "+Convert.ToInt32(ddlUsers.SelectedValue);
                        DataRow[] resultApp = ds.Tables[0].Select(expression);
                        lblApplicant.Text = Convert.ToString(resultApp.Length);
                        expression = "Status = 'InterviewDate' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                        DataRow[] resultInDt = ds.Tables[0].Select(expression);
                        lblInterviewDate.Text = Convert.ToString(resultInDt.Length);
                        expression = "Status = 'PhoneScreened' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                        DataRow[] resultPS = ds.Tables[0].Select(expression);
                        lblPhoneVideo.Text = Convert.ToString(resultPS.Length);
                        expression = "Status = 'Rejected' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                        DataRow[] resultR = ds.Tables[0].Select(expression);
                        lblRejected.Text = Convert.ToString(resultR.Length);
                        expression = "Status = 'Active' AND SourceId = " + Convert.ToInt32(ddlUsers.SelectedValue);
                        DataRow[] resultA = ds.Tables[0].Select(expression);
                        lblActive.Text = Convert.ToString(resultA.Length);
                        ////Retio calculations
                        lblAppInterRatio.Text = Convert.ToString(Convert.ToDouble(lblInterviewDate.Text) / Convert.ToDouble(lblApplicant.Text));
                        lblAppHireRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblApplicant.Text));
                        lblInterNewRatio.Text = Convert.ToString(Convert.ToDouble(lblActive.Text) / Convert.ToDouble(lblInterviewDate.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}