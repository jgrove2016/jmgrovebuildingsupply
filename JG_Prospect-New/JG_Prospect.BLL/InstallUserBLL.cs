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
    public class InstallUserBLL
    {
        private static InstallUserBLL m_InstallUserBLL = new InstallUserBLL();

        private InstallUserBLL()
        {
        }
        public static InstallUserBLL Instance
        {
            get { return m_InstallUserBLL; }
            private set { ;}
        }
        public string AddHoursToAvailability(DateTime dt)
        {
            int month = dt.Month;
            int day = dt.Day;
            int year = dt.Year;
            int hours = dt.Hour;
            string ampm = hours >= 12 ? "pm" : "am";
            int addedHours = hours + 2;
            string ampmAddedHours = hours >= 12 ? "pm" : "am";
            hours = hours % 12;
            hours = hours != 0 ? hours : 12; // the hour '0' should be '12'

            addedHours = addedHours % 12;
            addedHours = addedHours != 0 ? addedHours : 12; // the hour '0' should be '12'

            string result = month + "/" + day + "/" + year + " " + hours + ":00 " + ampm + " to " + addedHours + ":00 " + ampmAddedHours;

            return result;
            //  var d1 = new Date($(obj).val());
            //            var month = d1.getMonth();
            //            var day = d1.getDate();
            //            var year = d1.getFullYear();
            //            var hours = d1.getHours();
            //            var ampm = hours >= 12 ? 'pm' : 'am';
            //            var addedHours = hours + 2;
            //            var ampmAddedHours = addedHours >= 12 ? 'pm' : 'am';

            //            hours = hours % 12;
            //            hours = hours ? hours : 12; // the hour '0' should be '12'
            //            addedHours = addedHours % 12;
            //            addedHours = addedHours ? addedHours : 12; // the hour '0' should be '12'

            //            var f = month + '/' + day + '/' + year + ' ' + hours + ':00 ' + ampm + ' to ' + addedHours + ':00 ' + ampmAddedHours;
            //            $(obj).val(f);
        }
        public bool AddUser(user objuser)
        {
            return InstallUserDAL.Instance.AddIntsallUser(objuser);
        }
        public int AddSalesFollowUp(int customerid, int userId, DateTime meetingdate, string Status)
        {
            return InstallUserDAL.Instance.AddSalesFollowUp(customerid, meetingdate, Status, userId);
        }

        public DataSet GetSalesTouchPointLogData(int CustomerId, int userid)
        {
            return InstallUserDAL.Instance.GetSalesTouchPointLogData(CustomerId, userid);
        }
        public void UpdateProspect(user objuser)
        {
            InstallUserDAL.Instance.UpdateProspect(objuser);
        }

        public DataSet AddSkillUser(string Name, string Type, string PerOwner, string Phone, string Email, string Address, string UserId)
        {
            return InstallUserDAL.Instance.addSkillUser(Name, Type, PerOwner, Phone, Email, Address, UserId);
        }

        public DataSet GetSkillUser(string UserId, string Id)
        {
            return InstallUserDAL.Instance.GetSkillUser(UserId, Id);
        }

        public void AddEditInstallerAvailability(Availability a)
        {
            InstallUserDAL.Instance.AddEditInstallerAvailability(a);
        }

        public void DeletePLLic(string LicPath)
        {
            InstallUserDAL.Instance.DeletePLLicense(LicPath);
        }

        public void DeleteAssessment(string Source)
        {
            InstallUserDAL.Instance.DeleteAssessment(Source);
        }

        public void DeleteResume(string Path)
        {
            InstallUserDAL.Instance.DeleteResume(Path);
        }

        public void UpdateDocPath(string NewPath, string OldPath)
        {
            InstallUserDAL.Instance.UpdatePath(NewPath, OldPath);
        }

        public void DeleteCirtification(string Path)
        {
            InstallUserDAL.Instance.DeleteCirtification(Path);
        }

        public void DeleteGeneral(string Path)
        {
            InstallUserDAL.Instance.DeleteLibilities(Path);
        }

        public void DeleteComp(string Path)
        {
            InstallUserDAL.Instance.DeleteWorkerComp(Path);
        }

        public void DeleteImage(string imagePath)
        {
            InstallUserDAL.Instance.DeleteImage(imagePath);
        }
        public DataSet AddSource(string Source)
        {
            return InstallUserDAL.Instance.AddSource(Source);
        }


        public DataSet GetProspectCount(int UserId, string dt1, string dt2)
        {
            return InstallUserDAL.Instance.GetProspectCount(UserId, dt1, dt2);
        }

        public DataSet CheckSource(string Source)
        {
            return InstallUserDAL.Instance.CheckDuplicateSource(Source);
        }

        public DataSet GetSource()
        {
            return InstallUserDAL.Instance.getSource();
        }

        public void DeleteSource(string Source)
        {
            InstallUserDAL.Instance.DeleteSource(Source);
        }
        public DataSet GetAttachment(int id)
        {
            return InstallUserDAL.Instance.GetAttachment(id);
        }

        public DataSet CheckInstallUser(string UserName, string PhoneNo)
        {
            return InstallUserDAL.Instance.CheckDuplicateInstaller(UserName, PhoneNo);
        }

        public DataSet CheckInstallUserOnEdit(string UserName, string PhoneNo, int Id)
        {
            return InstallUserDAL.Instance.CheckDuplicateInstallerOnEdit(UserName, PhoneNo, Id);
        }

        public DataSet getzip(string zip)
        {
            return InstallUserDAL.Instance.getZip(zip);
        }
        public DataSet GetInstallerAvailability(string referenceId, int installerId)
        {
            return InstallUserDAL.Instance.GetInstallerAvailability(referenceId, installerId);
        }
        public bool UpdateInstallUser(user objuser, int id)
        {
            return InstallUserDAL.Instance.UpdateInstallUser(objuser, id);
        }
        public DataSet GetJobsForInstaller()
        {
            DataSet ds = InstallUserDAL.Instance.GetJobsForInstaller();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    dr["Category"] = InstallUserDAL.Instance.GetAllCategoriesForReferenceId(dr["ReferenceId"].ToString());
                }
            }
            return ds;
        }
        public DataSet getUser(string loginid)
        {
            return UserDAL.Instance.getUser(loginid);
        }
        public DataSet getallInstallusers()
        {
            DataSet dsNew = new DataSet();
            try
            {
                dsNew = InstallUserDAL.Instance.getallInstallusers();
            }
            catch
            {
                throw;
            }
            return dsNew;
        }
        public DataSet GetAllEditSalesUser()
        {
            return InstallUserDAL.Instance.GetAllEditSalesUser();
        }

        public DataSet GetAllSalesUserToExport()
        {
            return InstallUserDAL.Instance.ExportAllSalesUsersData();
        }

        public DataSet ExportAllInstallUsersData()
        {
            return InstallUserDAL.Instance.ExportAllInstallUsersData();
        }
        public DataSet getTrades()
        {
            return InstallUserDAL.Instance.getTrade();
        }

        public DataSet getUserList()
        {
            return InstallUserDAL.Instance.getUserList();
        }

        public DataSet getSrusers()
        {
            return UserDAL.Instance.getSrusers();
        }
        public DataSet getuserdetails(int id)
        {
            return InstallUserDAL.Instance.getuserdetails(id);
        }

        public DataSet getalluserdetails()
        {
            return InstallUserDAL.Instance.getalluserdetails();
        }
        public DataTable getMaxId(string userType, string userStatus)
        {
            return InstallUserDAL.Instance.getMaxId(userType, userStatus);
        }

        public DataTable GetMaxSalesId(string Designition)
        {
            return InstallUserDAL.Instance.GetMaxSalesId(Designition);
        }

        public DataTable getTemplate(string status, string Part)
        {
            return InstallUserDAL.Instance.getEmailTemplate(status, Part);
        }

        public bool DeleteInstallUser(int id)
        {
            return InstallUserDAL.Instance.DeleteInstallUser(id);
        }

        public bool updateProspectstatus(int Estimateid, string status, DateTime? followupdate)
        {
            return UserDAL.Instance.updateProspectstatus(Estimateid, status, followupdate);
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

        public DataSet getInstallerUserDetailsByLoginId(string loginid)
        {
            return InstallUserDAL.Instance.getInstallerUserDetailsByLoginId(loginid);
        }

        public DataSet getCustomerUserDetails(string Email, string Password)
        {
            return InstallUserDAL.Instance.getInstallerUserDetailsByLoginId(Email, Password);
        }


        public DataSet getCustomerUserLogin(string Email, string Password)
        {
            return InstallUserDAL.Instance.getCustomerLogin(Email, Password);
        }

        public void ActivateUser(string loginid)
        {
            InstallUserDAL.Instance.Activateuser(loginid);
        }

        public DataSet CheckRegistration(string loginid, string PhoneNo)
        {
            return InstallUserDAL.Instance.CheckRegistration(loginid, PhoneNo);
        }

        public DataSet CheckCustomerRegistration(string loginid, string PhoneNo)
        {
            return InstallUserDAL.Instance.CheckCustomerRegistration(loginid, PhoneNo);
        }

        public void AddUser(string loginid, string password, string phoneNo, string DOB)
        {
            InstallUserDAL.Instance.AddUser(loginid, password, phoneNo, DOB);
        }

        public void AddCustomer(string loginid, string password, string phoneNo, string DateOfBirth)
        {
            InstallUserDAL.Instance.AddCustomer(loginid, password, phoneNo, DateOfBirth);
        }

        public void AddUserFB(string loginid)
        {
            InstallUserDAL.Instance.AddUserFB(loginid);
        }

        public string GetPassword(string loginid)
        {
            return InstallUserDAL.Instance.GetPassword(loginid);
        }

        public string GetUserName(string PhoneNumber)
        {
            return InstallUserDAL.Instance.GetUserName(PhoneNumber);
        }

        public int IsValidInstallerUser(string loginid, string password)
        {
            return InstallUserDAL.Instance.IsValidInstallerUser(loginid, password);
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

        public bool ChangeInstallerPassword(int loginid, string password)
        {
            return InstallUserDAL.Instance.ChangeInstallerPassword(loginid, password);
        }

        public bool UpdateInstallUserStatus(string Status, int StatusId)
        {
            return InstallUserDAL.Instance.UpdateInstallUserStatus(Status, StatusId);
        }

        public DataSet ChangeStatus(string Status, int StatusId, string RejectionDate, string RejectionTime, int RejectedUserId, string StatusReason = "")
        {

            return InstallUserDAL.Instance.ChangeSatatus(Status, StatusId, RejectionDate, RejectionTime, RejectedUserId, StatusReason);
        }

        public void ChangeStatusToInterviewDate(string Status, int StatusId, string RejectionDate, string RejectionTime, int RejectedUserId, string time, string StatusReason = "")
        {
            InstallUserDAL.Instance.ChangeStatusToInterviewDate(Status, StatusId, RejectionDate, RejectionTime, RejectedUserId, time, StatusReason);
        }

        public bool UpdateOfferMade(int Id, string Email, string password)
        {
            return InstallUserDAL.Instance.UpdateOfferMade(Id, Email, password);
        }

        public DataSet GetHrData(DateTime fromdate, DateTime todate, int userid)
        {
            return InstallUserDAL.Instance.GetHrData(fromdate, todate, userid);
        }

        public DataSet GetHrDataForHrReports(DateTime fromDate, DateTime toDate, int userid)
        {
            return InstallUserDAL.Instance.GetHrDataForHrReports(fromDate, toDate, userid);
        }

        public DataSet FilteHrData(DateTime fromDate, DateTime toDate, string designation, string status)
        {
            return InstallUserDAL.Instance.FilteHrData(fromDate, toDate, designation, status);
        }

        public DataSet GetActiveUsers()
        {
            return InstallUserDAL.Instance.GetActiveUsers();
        }
        public DataSet GetActiveContractors()
        {
            return InstallUserDAL.Instance.GetActiveContractors();

        }


    }
}
