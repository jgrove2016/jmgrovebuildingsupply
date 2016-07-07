using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JG_Prospect.DAL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Data;

namespace JG_Prospect.BLL
{
    public class UserBLL
    {
        private static UserBLL m_UserBLL = new UserBLL();

        private UserBLL()
        {

        }

        public static UserBLL Instance
        {
            get { return m_UserBLL; }
            set { ;}
        }

        public DataSet GetuserData(int id)
        {
            return UserDAL.Instance.GetuserData(id);
        }
        public bool UpdatingUser(user objuser, int id)
        {
            return UserDAL.Instance.UpdatingUser(objuser, id);
        }
        public int chklogin(string loginid, string password)
        {
            return UserDAL.Instance.chklogin(loginid, password);
        }

        public bool AddUser(user objuser)
        {
            return UserDAL.Instance.AddUser(objuser);
        }

        public bool UpdateUser(user objuser, int id)
        {
            return UserDAL.Instance.UpdateUser(objuser, id);
        }

        public DataSet getUser(string loginid)
        {
            return UserDAL.Instance.getUser(loginid);
        }
        public DataSet GetAllProducts()
        {
            return UserDAL.Instance.GetAllProducts();
        }

        public DataSet FetchSalesReport(int userId, DateTime fromDate, DateTime toDate)
        {
            DataSet ds = UserDAL.Instance.FetchSalesReport(userId, fromDate, toDate);
            return ds;
        }

        public int CalculateLeadsIssued(int userId, DateTime fromDate, DateTime toDate)
        {
            int leadsIssued = UserDAL.Instance.CalculateLeadsIssued(userId, fromDate, toDate);
            return leadsIssued;
        }
        public int CalculateTotalSeenEST(int userId, DateTime fromDate, DateTime toDate)
        {
            int totalProspect = UserDAL.Instance.CalculateTotalSeenEST(userId, fromDate, toDate);
            return totalProspect;
        }
        public int CalculateTotalSetEST(int userId, DateTime fromDate, DateTime toDate)
        {
            int totalSetEST = UserDAL.Instance.CalculateTotalSetEST(userId, fromDate, toDate);
            return totalSetEST;
        }
        public int CalculateTotalProspects(int userId, DateTime fromDate, DateTime toDate)
        {
            int totalSeenEST = UserDAL.Instance.CalculateTotalProspects(userId, fromDate, toDate);
            return totalSeenEST;
        }
        public decimal CalculateJrRehashPercent(int userId, DateTime fromDate, DateTime toDate)
        {
            decimal totalRehash = UserDAL.Instance.CalculateJrRehashPercent(userId, fromDate, toDate);
            return totalRehash;
        }
        public decimal CalculateJrTotalGrossSale(int userId, DateTime fromDate, DateTime toDate)
        {
            decimal totalSale = UserDAL.Instance.CalculateJrTotalGrossSale(userId, fromDate, toDate);
            return totalSale;
        }
        public decimal CalculateJrTotalProspectDataCompletion(int userId, DateTime fromDate, DateTime toDate)
        {
            decimal totalData = UserDAL.Instance.CalculateJrTotalProspectDataCompletion(userId, fromDate, toDate);
            return totalData;
        }
        public decimal CalculateOverAllClosingPercent(int userId, DateTime fromDate, DateTime toDate)
        {
            return UserDAL.Instance.CalculateOverAllClosingPercent(userId, fromDate, toDate);
        }

        public int CalculateLeadsSeen(int userId, DateTime fromDate, DateTime toDate)
        {
            return UserDAL.Instance.CalculateLeadsSeen(userId, fromDate, toDate);
        }

        public decimal CalculateGrossSalesOfUser(int userId, DateTime fromDate, DateTime toDate)
        {
            decimal grossSales = UserDAL.Instance.CalculateGrossSalesOfUser(userId, fromDate, toDate);
            return grossSales;
        }

        public DataSet getallusers(string usertype)
        {
            return UserDAL.Instance.getallusers(usertype);
        }

        public DataSet getallSalesusers()
        {
            return UserDAL.Instance.getSalesusers();
        }

         public DataSet GetAllEditSalesUser()
        {
            return UserDAL.Instance.GetAllEditSalesUser();
        }
        public DataSet GetAllUsers()
        {
            return UserDAL.Instance.GetAllUsers();
        }

        public DataSet getSrusers()
        {
            return UserDAL.Instance.getSrusers();
        }
        public DataSet getuserdetails(int id)
        {
            return UserDAL.Instance.getuserdetails(id);
        }

        public DataSet getSSEuserdetails(int id)
        {
            return UserDAL.Instance.getSSEuserdetails(id);
        }

        public bool DeleteUser(int id)
        {
            return UserDAL.Instance.DeleteUser(id);
        }

        public bool updateProspectstatus(int Estimateid, string status, DateTime? followupdate)
        {
            return UserDAL.Instance.updateProspectstatus(Estimateid,status,followupdate);
        }
        public bool changepassword(int id, string password)//, string usertype
        {
            return UserDAL.Instance.changepassword(id, password);//, usertype
        }

        public bool SavePeriod(period objperiod)
        {
            return UserDAL.Instance.SavePeriod(objperiod);
        }

        public bool deleteperiod(int periodname)
        {
            return UserDAL.Instance.deleteperiod(periodname);
        }

        public DataSet getallperiod()
        {
            return UserDAL.Instance.getallperiod();
        }

        public DataSet getperioddetails(int periodId)
        {
            return UserDAL.Instance.getperioddetails(periodId);
        }

        public int addprospect(prospect objprospect)
        {
            return UserDAL.Instance.addprospect(objprospect);
        }

        public int updateprospect(prospect objprospect)
        {
            return UserDAL.Instance.updateprospect(objprospect);
        }

        public DataSet Fetchleadssummary(DateTime? frmdate, DateTime? todate, string username)
        {
            return UserDAL.Instance.Fetchleadssummary(frmdate, todate, username);
        }

        public DataSet FetchProspectstoassign(DateTime? frmdate, DateTime? todate, string username)
        {
            return UserDAL.Instance.FetchProspectstoassign(frmdate, todate, username);
        }

        public DataSet Fetchprogressreport(DateTime frmdate, DateTime todate, string username)
        {
            return UserDAL.Instance.Fetchprogressreport(frmdate, todate, username);
        }

        public DataSet Fetchstaticreport(prospect objprospect)
        {
            return UserDAL.Instance.Fetchstaticreport(objprospect);
        }

        public DataSet getprospectdetails(int id)
        {
            return UserDAL.Instance.getprospectdetails(id);
        }
        public string GetProductNameByProductId(int id)
        {
            return UserDAL.Instance.GetProductNameByProductId(id);
        }
        public int GetProductIDByProductName(string name)
        {
            return UserDAL.Instance.GetProductIDByProductName(name);
        }
        public DataSet fetchzipcode(string zipcode)
        {
            return UserDAL.Instance.fetchzipcode(zipcode);
        }

        public DataSet fetchcitystate(string zipcode)
        {
            return UserDAL.Instance.fetchcitystate(zipcode);
        }

        public DataSet GetAllVideo()
        {
            return UserDAL.Instance.GetAllVideo();
        }

        public DataSet GetResources(string type)
        {
            return UserDAL.Instance.GetResources(type);
        }

        public bool SaveResources(string link, string description, string type)
        {
            return UserDAL.Instance.SaveResources(link, description, type);
        }

        public DataSet Getcurrentperioddates()
        {
            return UserDAL.Instance.Getcurrentperioddates();
        }
        public DataSet fetchPrimaryContactDetails(int intContactId)
        {
            return UserDAL.Instance.fetchPrimaryContactDetails(intContactId);
        }
        public DataSet BindJobImage()
        {
            return UserDAL.Instance.BindJobImage();
        }
        public DataSet FetchLocationImage(int CustomerId, string strJobSoldId)
        {
            return UserDAL.Instance.FetchLocationImage(CustomerId, strJobSoldId);
        }
        public DataSet BindEndAddress(string EndAddress, int CustomerId)
        {
            return UserDAL.Instance.BindEndAddress(CustomerId, EndAddress);
        }
        public DataSet fetchAllScripts(int? intScriptId)
        {
            return UserDAL.Instance.fetchAllScripts(intScriptId);
        }
        public DataSet manageScripts(PhoneDashboard objPhoneDashboard)
        {
            return UserDAL.Instance.manageScripts(objPhoneDashboard);
        }
    }
}
