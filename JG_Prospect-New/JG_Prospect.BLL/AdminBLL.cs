using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JG_Prospect.DAL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace JG_Prospect.BLL
{
    public class AdminBLL
    {
        private static AdminBLL m_AdminBLL = new AdminBLL();

        private AdminBLL()
        {

        }

        public static AdminBLL Instance
        {
            get { return m_AdminBLL; }
            set { ;}
        }

        //public DataSet GetSrAppointment(int id)
        //{
        //    //return AdminDAL.Instance.GetSrAppointment(id);
        //}

        public DataSet AutoFill(string strpre)
        {
            return AdminDAL.Instance.AutoFill(strpre);
        }

        public DataSet GetMaterialList(string SoldJobId)
        {
            return AdminDAL.Instance.FetchMaterialList(SoldJobId);
        }

        public DataSet FetchContractTemplate(int id)
        {
            return AdminDAL.Instance.FetchContractTemplate(id);
        }

        public DataSet FetchWorkOrderTemplate()
        {
            return AdminDAL.Instance.FetchWorkOrderTemplate();
        }
        public DataSet FetchStatusEmailTemplate(string HTML_Name)
        {
            return AdminDAL.Instance.FetchStatusEmailTemplate(HTML_Name);
        }
        public bool UpdateContractTemplate(string ContracttemplateHeader, string ContracttemplateBodyA, string ContracttemplateBodyB, string ContracttemplateFooter, string ContractTemplateBody2, int id)
        {
            return AdminDAL.Instance.UpdateContractTemplate(ContracttemplateHeader, ContracttemplateBodyA, ContracttemplateBodyB, ContracttemplateFooter, ContractTemplateBody2, id);
        }

        public bool UpdateEmailVendorCategoryTemplate(string EmailTemplateHeader, string EmailTemplateFooter, string EAttachment, string subject ="")
        {
            return AdminDAL.Instance.UpdateEmailVendorCategoryTemplate(EmailTemplateHeader, EmailTemplateFooter, EAttachment, subject);
        }

        public void DisableCustomer(int Id, string Reason)
        {
            AdminDAL.Instance.DisableCustomer(Id, Reason);
        }

        public bool UpdateEmailVendorTemplate2(string EmailTemplateHeader, string EmailTemplateFooter, string AttPath)
        {
            return AdminDAL.Instance.UpdateEmailVendorTemplate2(EmailTemplateHeader, EmailTemplateFooter, AttPath);
        }
        public bool UpdateEmailVendorTemplate(string EmailTemplateHeader, string EmailTemplateFooter, string subject, int pHTMLTemplateID, List<CustomerDocument> custList)
        {
            return AdminDAL.Instance.UpdateEmailVendorTemplate(EmailTemplateHeader, EmailTemplateFooter, subject, pHTMLTemplateID, custList);
        }
        /// <summary>
        /// This method deletes the record and returns the path of physical file, so that it could be deleted from server.
        /// </summary>
        /// <param name="pAttachmentID"></param>
        /// <returns></returns>
        public DataSet DeleteEmailAttachment(int pAttachmentID)
        {
            return AdminDAL.Instance.DeleteEmailAttachment(pAttachmentID);
        }
        public bool UpdateWorkOrderTemplate(string WorkOrdertemplate)
        {
            return AdminDAL.Instance.UpdateWorkOrderTemplate(WorkOrdertemplate);
        }

        public DataSet GetShutterStyle()
        {
            return AdminDAL.Instance.GetShutterStyle();
        }

        public DataSet GetProductLineForGrid()
        {
            return AdminDAL.Instance.GetProductLineForGrid();
        }


        public DataSet InsertProduct_PriceControl(string ProductLineName)
        {
            return AdminDAL.Instance.InsertProduct_PriceControl(ProductLineName);
        }

        public DataSet UpdateProduct_PriceControl(string ProductLineName, int ProdId)
        {
            return AdminDAL.Instance.UpdateProduct_PriceControl(ProductLineName, ProdId);
        }

        public DataSet SelectProduct_PriceControl(int ProductId)
        {
            return AdminDAL.Instance.SelectProduct_PriceControl(ProductId);
        }

        public DataSet DeleteProduct_PriceControl(int ProductId)
        {
            return AdminDAL.Instance.DeleteProduct_PriceControl(ProductId);
        }
        public DataSet CheckDuplicateProduct_PriceControl(string Productname)
        {
            return AdminDAL.Instance.CheckDuplicateProduct_PriceControl(Productname);
        }

        public DataSet CheckDuplicateProduct_Update_PriceControl(string Productname, int ProdId)
        {
            return AdminDAL.Instance.CheckDuplicateProduct_Update_PriceControl(Productname, ProdId);
        }



        public DataSet GetContractTemplate(string ProductLineName)
        {
            return AdminDAL.Instance.GetContractTemplateByName(ProductLineName);
        }

        public DataSet InsertProductContract(string ProductLineName, string HTMLHeader, string HTMLBody, string HTMLFooter, string HTMLBody2)
        {
            return AdminDAL.Instance.InsertContract(ProductLineName, HTMLHeader, HTMLBody, HTMLFooter, HTMLBody2);
        }

        public DataSet UpdateProductContract(int Id, string ProductLineName, string HTMLHeader, string HTMLBody, string HTMLFooter, string HTMLBody2)
        {
            return AdminDAL.Instance.UpdateContractTemplate(Id, ProductLineName, HTMLHeader, HTMLBody, HTMLFooter, HTMLBody2);
        }

        public DataSet GetShutterwidth()
        {
            return AdminDAL.Instance.GetShutterwidth();
        }

        public DataSet GetSurfaceofmount()
        {
            return AdminDAL.Instance.GetSurfaceofmount();
        }

        public bool updateshutterwidth(int id, decimal price)
        {
            return AdminDAL.Instance.updateshutterwidth(id, price);
        }

        public bool updatesurfacemount(int id, decimal price)
        {
            return AdminDAL.Instance.updatesurfacemount(id, price);
        }

        public bool Updatepricebypercentage(Decimal percentage, string oper)
        {
            return AdminDAL.Instance.Updatepricebypercentage(percentage, oper);
        }

        public DataSet FetchALLcustomer()
        {
            return AdminDAL.Instance.FetchALLcustomer();
        }

        public DataSet BindGridForSrSales(string str_Search, string str_Criateria, string from, string to)
        {
            return AdminDAL.Instance.BindGridForSrSales(str_Search, str_Criateria, from, to);
        }

        public void UpdateStatusFromGrid(int Id, string str_Status)
        {
            AdminDAL.Instance.UpdateStatusFromGrid(Id, str_Status);
        }

        public void UpdateCloseReason(int Id, string str_Status)
        {
            AdminDAL.Instance.UpdateCloseReason(Id, str_Status);
        }

        public void UpdateSoldStatus(int Id, string str_Status, string Status)
        {
            AdminDAL.Instance.UpdateSold(Id, str_Status, Status);
        }

        public void UpdateEstDate(int Id, string str_Status, string Status)
        {
            AdminDAL.Instance.UpdateEst(Id, str_Status, Status);
        }


        public void MakeAppointments(int UserId, int QuoteId, string Date, string Time, string CalType)
        {
            AdminDAL.Instance.MakeAppointments(UserId, QuoteId, Date, Time, CalType);
        }

        //Start ....Code added by Neeta A....
        public DataSet GetsalesAppointmentsById(int UserId)
        {
            return AdminDAL.Instance.GetSalesAppointmentsById(UserId);
        }

        public DataSet GetToDaysAppointments(int UserId)
        {
            return AdminDAL.Instance.GetTodaysAppointments(UserId);
        }

        public DataSet GetNextAppointmentsById(int UserId)
        {
            return AdminDAL.Instance.GetNextAppointmentsById(UserId);
        }
        //For Status...
        public DataSet GetAllStatusForSalesCustomer()
        {
            return AdminDAL.Instance.GetAllStatusForSalesCustomer();
        }
        //For Color Code....
        public DataSet GetStatusForColorCode(int UserId)
        {
            return AdminDAL.Instance.GetStatusForColorCode(UserId);
        }

        public DataSet GetAllsalesAppointments()
        {
            return AdminDAL.Instance.GetAllsalesAppointments();
        }
        //For All Calendar
        public DataSet GetHRCompanyEventCalendar()
        {
            return AdminDAL.Instance.GetHRCompanyEventCalendar();
        }
        //Event and Company Calendar....
        public DataSet GetEventCompanyCalendar()
        {
            return AdminDAL.Instance.GetEventCompanyCalendar();
        }
        //HR Company Calendar....
        public DataSet GetHRCompanyCalendar()
        {
            return AdminDAL.Instance.GetHRCompanyCalendar();
        }
        public DataSet GetAnnualEventByID(int UserId, string year)
        {
            return AdminDAL.Instance.GetAnnualEventByID(UserId, year);
        }
        public DataSet GetAllAnnualEvent()
        {
            return AdminDAL.Instance.GetAllAnnualEvent();
        }
        //For Event Calendar....
        public DataSet GetEventCalendar()
        {
            return AdminDAL.Instance.GetEventCalendar();
        }
        //For HR Calendar...
        public DataSet GetHRCalendar()
        {
            return AdminDAL.Instance.GetHRCalendar();
        }
        //For Company Calendar...
        public DataSet GetCompanyCalendar()
        {
            return AdminDAL.Instance.GetCompanyCalendar();
        }
        //For HR And Event  Calendar...
        public DataSet GetHREventCalendar()
        {
            return AdminDAL.Instance.GetHREventCalendar();
        }
        public DataSet GetJuniorsalesAppointmentsById(int UserId)
        {
            return AdminDAL.Instance.GetJuniorsalesAppointmentsById(UserId);
        }
        public DataSet GetTodaysSalesAppointment(int UserId)
        {
            return AdminDAL.Instance.GetTodaysSalesAppointment(UserId);
        }
        public DataSet GetInterviewDetails(int UserId)
        {
            return AdminDAL.Instance.GetInterviewDetails(UserId);
        }
        //End ....Code added by Neeta A....

        public DataSet BindGridForQuotes(int CustId)
        {
            return AdminDAL.Instance.BindGridForQuotes(CustId);
        }

        public DataSet BindGridForStatus(int CustId)
        {
            return AdminDAL.Instance.BindGridForStatus(CustId);
        }

        public DataSet BindGridForJobs(int CustId)
        {
            return AdminDAL.Instance.BindGridForJobs(CustId);
        }

        //public DataSet BindStatusOverrideGrid(string str_Search,string str_Criateria)
        //{
        //    string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        //    SqlConnection con = new SqlConnection(strcon);
        //    SqlCommand cmd = new SqlCommand("select id,CustomerName,Status,Email,CellPh,AlternatePh,CustomerAddress,State,City,ZipCode,JobLocation from new_customer",con);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(ds);
        //    return ds;
        //}

        //public DataSet BindStatusOverrideGridById(int Id)
        //{
        //    string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        //    SqlConnection con = new SqlConnection(strcon);
        //    SqlCommand cmd = new SqlCommand("select id,CustomerName,Status,Email,CellPh,AlternatePh,CustomerAddress,State,City,ZipCode,JobLocation from new_customer WHERE id = " + Id, con);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(ds);
        //    return ds;
        //}
        public DataSet FetchingContractTemplateDetail(string templateName)
        {
            return AdminDAL.Instance.FetchingContractTemplateDetails(templateName);
        }

        public bool UpdateStatus(int custid, string status, string followupdate)
        {
            return AdminDAL.Instance.UpdateStatus(custid, status, followupdate);
        }
        public string GetAdminCode()
        {
            return AdminDAL.Instance.GetAdminCode();
        }
        public string GetForemanCode()
        {
            return AdminDAL.Instance.GetForemanCode();
        }
        public DataSet GetForemanPassword(string foremanPwd)
        {
            return AdminDAL.Instance.GetForemanPassword(foremanPwd);
        }
        public DataSet FetchCustomerEmailTemplate(int id)
        {
            return AdminDAL.Instance.FetchCustomerEmailTemplate(id);
        }

        public bool UpdateCustomerEmailTemplate(string ContracttemplateHeader, string ContracttemplateBody, string ContracttemplateFooter, int id, List<CustomerDocument> custList)
        {
            return AdminDAL.Instance.UpdateCustomerEmailtTemplate(ContracttemplateHeader, ContracttemplateBody, ContracttemplateFooter, id, custList);
        }

        public DataSet FetchCustomerAttachmentTemplate()
        {
            return AdminDAL.Instance.FetchCustomerAttachmentTemplate();
        }
        public DataSet FetchCustomerAttachments()
        {
            return AdminDAL.Instance.FetchCustomerAttachments();
        }
        public bool DeleteCustomerAttachment(string custAttachment)
        {
            return AdminDAL.Instance.DeleteCustomerAttachment(custAttachment);
        }

        //Get Conmpany Address
        public DataSet GetCompanyAddress()
        {
            return AdminDAL.Instance.CompanyAddress();
        }
        //Update Company Address
        public string UpdateCompanyAddress(int Addressid, string CompanyAddress, string CompanyCity, string CompanyState, string CompanyZipCode)
        {
            return AdminDAL.Instance.UpdateCompanyAddress(Addressid, CompanyAddress, CompanyCity, CompanyState, CompanyZipCode);
        }
        public bool UpdateEmailVendorCategoryTemplate(string EmailTemplateHeader, string EmailTemplateFooter, string subject, int pHTMLTemplateID,  List<CustomerDocument> custList)
        {
            return AdminDAL.Instance.UpdateEmailVendorCategoryTemplate(EmailTemplateHeader, EmailTemplateFooter,subject,pHTMLTemplateID,custList);
        }
        
        public DataSet GetHTMLTemplateAttachedFile(int pHTMLTemplateID)
        {
            return AdminDAL.Instance.GetHTMLTemplateAttachedFile(pHTMLTemplateID);
        }

        public DataSet GetProductCategory()
        {
            return AdminDAL.Instance.GetProductCategory();
        }
        public DataSet GetVendorCategory(string ProductCategoryId, bool Isretail_wholesale, bool IsManufacturer)
        {
            return AdminDAL.Instance.GetVendorCategory(ProductCategoryId, Isretail_wholesale, IsManufacturer);
        }
        public DataSet GetVendorSubCategory(string VendorCategoryId, bool Isretail_wholesale, bool IsManufacturer)
        {
            return AdminDAL.Instance.GetVendorSubCategory(VendorCategoryId, Isretail_wholesale, IsManufacturer);
        }
    }
}
