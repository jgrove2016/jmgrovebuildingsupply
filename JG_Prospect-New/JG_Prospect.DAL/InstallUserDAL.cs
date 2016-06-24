using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using JG_Prospect.DAL.Database;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;

namespace JG_Prospect.DAL
{
    public class InstallUserDAL
    {
        public static InstallUserDAL m_InstallUserDAL = new InstallUserDAL();
        private InstallUserDAL()
        {
        }
        public static InstallUserDAL Instance
        {
            get { return m_InstallUserDAL; }
            private set { ;}
        }
        public DataSet returndata;
        #region userlogin

        public bool AddIntsallUser(user objuser)
        {
            StringBuilder strerr = new StringBuilder();

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddInstallUser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@FristName", DbType.String, objuser.fristname);
                    database.AddInParameter(command, "@LastName", DbType.String, objuser.lastname);
                    database.AddInParameter(command, "@Email", DbType.String, objuser.email);
                    database.AddInParameter(command, "@phone", DbType.String, objuser.phone);
                    database.AddInParameter(command, "@phonetype", DbType.String, objuser.phonetype );
                    database.AddInParameter(command, "@Address", DbType.String, objuser.address);
                    database.AddInParameter(command, "@Zip", DbType.String, objuser.zip);
                    database.AddInParameter(command, "@State", DbType.String, objuser.state);
                    database.AddInParameter(command, "@City", DbType.String, objuser.city);
                    database.AddInParameter(command, "@password", DbType.String, objuser.password);
                    database.AddInParameter(command, "@designation", DbType.String, objuser.designation);
                    database.AddInParameter(command, "@status", DbType.String, objuser.status);
                    database.AddInParameter(command, "@Picture", DbType.String, objuser.picture);
                    database.AddInParameter(command, "@Attachements", DbType.String, objuser.attachements);
                    database.AddInParameter(command, "@bussinessname", DbType.String, objuser.businessname);
                    database.AddInParameter(command, "@ssn", DbType.String, objuser.ssn);
                    database.AddInParameter(command, "@ssn1", DbType.String, objuser.ssn1);
                    database.AddInParameter(command, "@ssn2", DbType.String, objuser.ssn2);
                    database.AddInParameter(command, "@signature", DbType.String, objuser.signature);
                    database.AddInParameter(command, "@dob", DbType.String, objuser.dob);
                    database.AddInParameter(command, "@citizenship", DbType.String, objuser.citizenship);
                    database.AddInParameter(command, "@ein1 ", DbType.String, objuser.ein1);
                    database.AddInParameter(command, "@ein2 ", DbType.String, objuser.ein2);
                    database.AddInParameter(command, "@a", DbType.String, objuser.a);
                    database.AddInParameter(command, "@b", DbType.String, objuser.b);
                    database.AddInParameter(command, "@c", DbType.String, objuser.c);
                    database.AddInParameter(command, "@d", DbType.String, objuser.d);
                    database.AddInParameter(command, "@e", DbType.String, objuser.e);
                    database.AddInParameter(command, "@f", DbType.String, objuser.f);
                    database.AddInParameter(command, "@g", DbType.String, objuser.g);
                    database.AddInParameter(command, "@h", DbType.String, objuser.h);
                    database.AddInParameter(command, "@i", DbType.String, objuser.i);
                    database.AddInParameter(command, "@j", DbType.String, objuser.j);
                    database.AddInParameter(command, "@k", DbType.String, objuser.k);
                    database.AddInParameter(command, "@maritalstatus", DbType.String, objuser.maritalstatus);
                    database.AddInParameter(command, "@PrimeryTradeId", DbType.Int32, objuser.PrimeryTradeId);
                    database.AddInParameter(command, "@SecondoryTradeId", DbType.Int32, objuser.SecondoryTradeId);
                    database.AddInParameter(command, "@Source", DbType.String, objuser.Source);
                    database.AddInParameter(command, "@Notes", DbType.String, objuser.Notes);
                    database.AddInParameter(command, "@StatusReason", DbType.String, objuser.Reason);
                    database.AddInParameter(command, "@GeneralLiability", DbType.String, objuser.GeneralLiability);
                    database.AddInParameter(command, "@PCLiscense", DbType.String, objuser.PqLicense);
                    database.AddInParameter(command, "@WorkerComp", DbType.String, objuser.WorkersComp);
                    database.AddInParameter(command, "@HireDate", DbType.String, objuser.HireDate);
                    database.AddInParameter(command, "@TerminitionDate", DbType.String, objuser.TerminitionDate);
                    database.AddInParameter(command, "@WorkersCompCode", DbType.String, objuser.WorkersCompCode);
                    database.AddInParameter(command, "@NextReviewDate", DbType.String, objuser.NextReviewDate);
                    database.AddInParameter(command, "@EmpType", DbType.String, objuser.EmpType);
                    database.AddInParameter(command, "@LastReviewDate", DbType.String, objuser.LastReviewDate);
                    database.AddInParameter(command, "@PayRates", DbType.String, objuser.PayRates);
                    database.AddInParameter(command, "@ExtraEarning", DbType.String, objuser.ExtraEarning);
                    database.AddInParameter(command, "@ExtraIncomeType", DbType.String, objuser.ExtraIncomeType);
                    database.AddInParameter(command, "@ExtraEarningAmt", DbType.String, objuser.ExtraEarningAmt);
                    database.AddInParameter(command, "@PayMethod", DbType.String, objuser.PayMethod);
                    database.AddInParameter(command, "@Deduction", DbType.String, objuser.Deduction);
                    database.AddInParameter(command, "@DeductionType", DbType.String, objuser.DeductionType);
                    database.AddInParameter(command, "@AbaAccountNo", DbType.String, objuser.AbaAccountNo);
                    database.AddInParameter(command, "@AccountNo", DbType.String, objuser.AccountNo);
                    database.AddInParameter(command, "@AccountType", DbType.String, objuser.AccountType);
                    //database.AddInParameter(command, "@StatusReason", DbType.String, objuser.Reason);
                    database.AddInParameter(command, "@InstallId", DbType.String, objuser.InstallId);
                    database.AddInParameter(command, "@PTradeOthers", DbType.String, objuser.PTradeOthers);
                    database.AddInParameter(command, "@STradeOthers", DbType.String, objuser.STradeOthers);
                    database.AddInParameter(command, "@DeductionReason", DbType.String, objuser.DeductionReason);
                    database.AddInParameter(command, "@SuiteAptRoom", DbType.String, objuser.str_SuiteAptRoom);


                    database.AddInParameter(command, "@FullTimePosition", DbType.Int32, objuser.FullTimePosition);
                    database.AddInParameter(command, "@ContractorsBuilderOwner", DbType.String, objuser.ContractorsBuilderOwner);
                    database.AddInParameter(command, "@MajorTools", DbType.String, objuser.MajorTools);
                    database.AddInParameter(command, "@DrugTest", DbType.Boolean, objuser.DrugTest);
                    database.AddInParameter(command, "@ValidLicense", DbType.Boolean, objuser.ValidLicense);
                    database.AddInParameter(command, "@TruckTools", DbType.Boolean, objuser.TruckTools);
                    database.AddInParameter(command, "@PrevApply", DbType.Boolean, objuser.PrevApply);
                    database.AddInParameter(command, "@LicenseStatus", DbType.Boolean, objuser.LicenseStatus);
                    database.AddInParameter(command, "@CrimeStatus", DbType.Boolean, objuser.CrimeStatus);
                    database.AddInParameter(command, "@StartDate", DbType.String, objuser.StartDate);
                    database.AddInParameter(command, "@SalaryReq", DbType.String, objuser.SalaryReq);
                    database.AddInParameter(command, "@Avialability", DbType.String, objuser.Avialability);
                    database.AddInParameter(command, "@ResumePath", DbType.String, objuser.ResumePath);
                    database.AddInParameter(command, "@skillassessmentstatus", DbType.Boolean, objuser.skillassessmentstatus);
                    database.AddInParameter(command, "@assessmentPath", DbType.String, objuser.assessmentPath);
                    database.AddInParameter(command, "@WarrentyPolicy", DbType.String, objuser.WarrentyPolicy);
                    database.AddInParameter(command, "@CirtificationTraining", DbType.String, objuser.CirtificationTraining);
                    database.AddInParameter(command, "@businessYrs", DbType.Decimal, objuser.businessYrs);
                    database.AddInParameter(command, "@underPresentComp", DbType.Decimal, objuser.underPresentComp);
                    database.AddInParameter(command, "@websiteaddress", DbType.String, objuser.websiteaddress);
                    database.AddInParameter(command, "@PersonName", DbType.String, objuser.PersonName);
                    database.AddInParameter(command, "@PersonType", DbType.String, objuser.PersonType);
                    database.AddInParameter(command, "@CompanyPrinciple", DbType.String, objuser.CompanyPrinciple);
                    database.AddInParameter(command, "@UserType", DbType.String, objuser.UserType);
                    database.AddInParameter(command, "@Email2", DbType.String, objuser.Email2);
                    database.AddInParameter(command, "@Phone2", DbType.String, objuser.Phone2);
                    database.AddInParameter(command, "@CompanyName", DbType.String, objuser.CompanyName);
                    database.AddInParameter(command, "@SourceUser", DbType.String, objuser.SourceUser);
                    database.AddInParameter(command, "@DateSourced", DbType.String, objuser.DateSourced);
                    database.AddInParameter(command, "@InstallerType", DbType.String, objuser.InstallerType);

                    database.AddInParameter(command, "@BusinessType", DbType.String, objuser.BusinessType);
                    database.AddInParameter(command, "@CEO", DbType.String, objuser.CEO);
                    database.AddInParameter(command, "@LegalOfficer", DbType.String, objuser.LegalOfficer);
                    database.AddInParameter(command, "@President", DbType.String, objuser.President);
                    database.AddInParameter(command, "@Owner", DbType.String, objuser.Owner);
                    database.AddInParameter(command, "@AllParteners", DbType.String, objuser.AllParteners);
                    database.AddInParameter(command, "@MailingAddress", DbType.String, objuser.MailingAddress);
                    database.AddInParameter(command, "@Warrantyguarantee", DbType.Boolean, objuser.Warrantyguarantee);
                    database.AddInParameter(command, "@WarrantyYrs", DbType.Int32, objuser.WarrantyYrs);
                    database.AddInParameter(command, "@MinorityBussiness", DbType.Boolean, objuser.MinorityBussiness);
                    database.AddInParameter(command, "@WomensEnterprise", DbType.Boolean, objuser.WomensEnterprise);
                    database.AddInParameter(command, "@InterviewTime", DbType.String, objuser.InterviewTime);
                    database.AddInParameter(command, "@ActivationDate", DbType.String, objuser.ActivationDate);
                    database.AddInParameter(command, "@UserActivated", DbType.String, objuser.UserActivated);
                    database.AddInParameter(command, "@LIBC", DbType.String, objuser.UserActivated);

                    database.AddInParameter(command, "@CruntEmployement", DbType.Boolean, objuser.CruntEmployement);
                    database.AddInParameter(command, "@CurrentEmoPlace", DbType.String, objuser.CurrentEmoPlace);
                    database.AddInParameter(command, "@LeavingReason", DbType.String, objuser.LeavingReason);
                    database.AddInParameter(command, "@CompLit", DbType.Boolean, objuser.CompLit);
                    database.AddInParameter(command, "@FELONY", DbType.Boolean, objuser.FELONY);
                    database.AddInParameter(command, "@shortterm", DbType.String, objuser.shortterm);
                    database.AddInParameter(command, "@LongTerm", DbType.String, objuser.LongTerm);
                    database.AddInParameter(command, "@BestCandidate", DbType.String, objuser.BestCandidate);
                    database.AddInParameter(command, "@TalentVenue", DbType.String, objuser.TalentVenue);
                    database.AddInParameter(command, "@Boardsites", DbType.String, objuser.Boardsites);
                    database.AddInParameter(command, "@NonTraditional", DbType.String, objuser.NonTraditional);
                    database.AddInParameter(command, "@ConSalTraning", DbType.String, objuser.ConSalTraning);
                    database.AddInParameter(command, "@BestTradeOne", DbType.String, objuser.BestTradeOne);
                    database.AddInParameter(command, "@BestTradeTwo", DbType.String, objuser.BestTradeTwo);
                    database.AddInParameter(command, "@BestTradeThree", DbType.String, objuser.BestTradeThree);

                    database.AddInParameter(command, "@aOne", DbType.String, objuser.aOne);
                    database.AddInParameter(command, "@aOneTwo", DbType.String, objuser.aOneTwo);
                    database.AddInParameter(command, "@bOne", DbType.String, objuser.bOne);
                    database.AddInParameter(command, "@cOne", DbType.String, objuser.cOne);
                    database.AddInParameter(command, "@aTwo", DbType.String, objuser.aTwo);
                    database.AddInParameter(command, "@aTwoTwo", DbType.String, objuser.aTwoTwo);
                    database.AddInParameter(command, "@bTwo", DbType.String, objuser.bTwo);
                    database.AddInParameter(command, "@cTwo", DbType.String, objuser.cTwo);
                    database.AddInParameter(command, "@aThree", DbType.String, objuser.aThree);
                    database.AddInParameter(command, "@aThreeTwo", DbType.String, objuser.aThreeTwo);
                    database.AddInParameter(command, "@bThree", DbType.String, objuser.bThree);
                    database.AddInParameter(command, "@cThree", DbType.String, objuser.cThree);

                    database.AddInParameter(command, "@RejectionDate", DbType.String, objuser.RejectionDate);
                    database.AddInParameter(command, "@RejectionTime", DbType.String, objuser.RejectionTime);

                    database.AddInParameter(command, "@RejectedUserId", DbType.Int32, objuser.RejectedUserId);
                    database.AddInParameter(command, "@TC", DbType.Boolean, objuser.TC);
                    database.AddInParameter(command, "@AddedBy", DbType.Int32, objuser.AddedBy);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (res == 1)
                        return true;
                    else
                        return false;
                }
            }

            catch (Exception ex)
            {
                return false;

            }

        }

        public void UpdateProspect(user objuser)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_UpdateInstallUserFromProspect");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, objuser.id);
                    database.AddInParameter(command, "@FristName", DbType.String, objuser.fristname);
                    database.AddInParameter(command, "@LastName", DbType.String, objuser.lastname);
                    database.AddInParameter(command, "@Email", DbType.String, objuser.email);
                    database.AddInParameter(command, "@phone", DbType.String, objuser.phone);
                    database.AddInParameter(command, "@Attachements", DbType.String, objuser.attachements);
                    database.AddInParameter(command, "@PrimeryTradeId", DbType.Int32, objuser.PrimeryTradeId);
                    database.AddInParameter(command, "@SecondoryTradeId", DbType.Int32, objuser.SecondoryTradeId);
                    database.AddInParameter(command, "@Notes", DbType.String, objuser.Notes);
                    //database.AddInParameter(command, "@StatusReason", DbType.String, objuser.Reason);
                    database.AddInParameter(command, "@PTradeOthers", DbType.String, objuser.PTradeOthers);
                    database.AddInParameter(command, "@STradeOthers", DbType.String, objuser.STradeOthers);
                    database.AddInParameter(command, "@UserType", DbType.String, objuser.UserType);
                    database.AddInParameter(command, "@Email2", DbType.String, objuser.Email2);
                    database.AddInParameter(command, "@Phone2", DbType.String, objuser.Phone2);
                    database.AddInParameter(command, "@CompanyName", DbType.String, objuser.CompanyName);
                    database.AddInParameter(command, "@SourceUser", DbType.String, objuser.SourceUser);
                    database.AddInParameter(command, "@DateSourced", DbType.String, objuser.DateSourced);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public DataSet ChangeSatatus(string Status, int StatusId, string RejectionDate, string RejectionTime, int RejectedUserId, string StatusReason = "")
        {
            DataSet dsTemp = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_ChangeStatus");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, StatusId);
                    database.AddInParameter(command, "@Status", DbType.String, Status);
                    database.AddInParameter(command, "@RejectionDate", DbType.String, RejectionDate);
                    database.AddInParameter(command, "@RejectionTime", DbType.String, RejectionTime);
                    database.AddInParameter(command, "@RejectedUserId", DbType.Int32, RejectedUserId);
                    database.AddInParameter(command, "@StatusReason", DbType.String, StatusReason);
                    dsTemp = database.ExecuteDataSet(command);
                    return dsTemp;
                }
            }
            catch (Exception ex)
            {
            }
            return dsTemp;
        }





        public DataSet addSkillUser(string Name, string Type, string PerOwner, string Phone, string Email, string Address, string UserId)
        {
            DataSet dsTemp = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddSkillUser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Name", DbType.String, Name);
                    database.AddInParameter(command, "@Title", DbType.String, Type);
                    database.AddInParameter(command, "@PerOwnership", DbType.String, PerOwner);
                    database.AddInParameter(command, "@Phone", DbType.String, Phone);
                    database.AddInParameter(command, "@EMail", DbType.String, Email);
                    database.AddInParameter(command, "@Address", DbType.String, Address);
                    database.AddInParameter(command, "@UserId", DbType.String, UserId);
                    dsTemp = database.ExecuteDataSet(command);
                    return dsTemp;
                }
            }
            catch (Exception ex)
            {

            }
            return dsTemp;
        }

        public DataSet GetSkillUser(string UserId, string Id)
        {
            DataSet dsTemp = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetNewSkillUserById");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@UserId", DbType.String, UserId);
                    database.AddInParameter(command, "@Id", DbType.String, Id);
                    dsTemp = database.ExecuteDataSet(command);
                    return dsTemp;
                }
            }
            catch (Exception ex)
            {

            }
            return dsTemp;
        }

        public string GetPassword(string UserName)
        {
            string password = "";
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_GetPassword");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Login_Id", DbType.String, UserName);
                    returndata = database.ExecuteDataSet(command);
                    if (returndata.Tables[0].Rows.Count > 0)
                    {
                        password = Convert.ToString(returndata.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        password = "";
                    }
                    return password;
                }
            }
            catch (Exception ex)
            {
                return password;
            }
        }

        public string GetUserName(string PhoneNumber)
        {
            string LoginName = "";
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_GetUserNameByPhoneNumber");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Phone", DbType.String, PhoneNumber);
                    returndata = database.ExecuteDataSet(command);
                    if (returndata.Tables[0].Rows.Count > 0)
                    {
                        LoginName = Convert.ToString(returndata.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        LoginName = "";
                    }
                    return LoginName;
                }
            }
            catch (Exception ex)
            {
                return LoginName;
            }
        }

        public bool AddCustomer(string UserName, string Password, string PhoneNo, string dob)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DateTime EstDate = DateTime.Now.AddDays(+7);
                    DbCommand command = database.GetStoredProcCommand("UDP_AddSignUpCustom");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CellPh", DbType.String, PhoneNo);
                    database.AddInParameter(command, "@Email", DbType.String, UserName);
                    database.AddInParameter(command, "@Password", DbType.String, Password);
                    database.AddInParameter(command, "@DateOfBirth", DbType.Date, Convert.ToDateTime(EstDate, JGConstant.CULTURE));
                    database.ExecuteScalar(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddUser(string UserName, string Password, string PhoneNo, string DOB)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_Registration");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Login_Id", DbType.String, UserName);
                    database.AddInParameter(command, "@Password", DbType.String, Password);
                    database.AddInParameter(command, "@Phone", DbType.String, PhoneNo);
                    database.AddInParameter(command, "@DOB", DbType.String, DOB);
                    database.ExecuteScalar(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddUserFB(string UserName)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_RegistrationFB");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Login_Id", DbType.String, UserName);
                    database.ExecuteScalar(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet CheckDuplicateInstaller(string UserName, string PhoneNo)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USp_CheckDuplicate");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Email", DbType.String, UserName);
                    database.AddInParameter(command, "@Phone", DbType.String, PhoneNo);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public DataSet CheckDuplicateInstallerOnEdit(string UserName, string PhoneNo, int Id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    //  DbCommand command = database.GetStoredProcCommand("USp_CheckDuplicateOnEdit");
                    //Altetred by Neeta...
                    DbCommand command = database.GetStoredProcCommand("jgrov_User.USp_CheckDuplicateOn Edit");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, Id);
                    database.AddInParameter(command, "@Email", DbType.String, UserName);
                    database.AddInParameter(command, "@Phone", DbType.String, PhoneNo);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataSet CheckRegistration(string UserName, string PhoneNo)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_CheckUserName");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Login_Id", DbType.String, UserName);
                    database.AddInParameter(command, "@Phone", DbType.String, PhoneNo);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public DataSet CheckCustomerRegistration(string UserName, string PhoneNo)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_CheckCustomerEmail");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Email", DbType.String, UserName);
                    database.AddInParameter(command, "@CellPh", DbType.String, PhoneNo);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }


        public DataSet GetProspectCount(int Id, string dt1, string dt2)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAddedProspectCount");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, Id);
                    database.AddInParameter(command, "@DateSourced1", DbType.String, dt1);
                    database.AddInParameter(command, "@DateSourced2", DbType.String, dt2);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public DataSet AddSource(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_AddSource");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Source", DbType.String, Source);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public void DeleteSource(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteSource");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Source", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DeleteImage(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteImagePath");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Picture", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DeletePLLicense(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeletePLLicense");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@PCLiscense", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }


        public void DeleteAssessment(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteAssessment");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@assessmentPath", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void DeleteResume(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteResume");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ResumePath", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void UpdatePath(string NewPath, string OldPath)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateDocPath");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ResumePathNew", DbType.String, NewPath);
                    database.AddInParameter(command, "@ResumePathOld", DbType.String, OldPath);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void DeleteCirtification(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteCirtification");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CirtificationTraining", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }



        public void DeleteWorkerComp(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DELETEWorkerComp");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@WorkerComp", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void DeleteLibilities(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteGeneralLiability");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@GeneralLiability", DbType.String, Source);
                    database.ExecuteScalar(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public DataSet getSource()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetSource");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }


        public DataSet CheckDuplicateSource(string Source)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_CheckDuplicateSource");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Source", DbType.String, Source);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }


        public DataSet getZip(string zip)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getcitybyzip");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@zipcode", DbType.String, zip);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }

        }

        public bool UpdateInstallUser(user objuser, int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateInstallUsers");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.String, Convert.ToInt32(id));
                    database.AddInParameter(command, "@FristName", DbType.String, objuser.fristname);
                    database.AddInParameter(command, "@LastName", DbType.String, objuser.lastname);
                    database.AddInParameter(command, "@Email", DbType.String, objuser.email);
                    database.AddInParameter(command, "@phone", DbType.String, objuser.phone);
                    database.AddInParameter(command, "@Address", DbType.String, objuser.address);
                    database.AddInParameter(command, "@Zip", DbType.String, objuser.zip);
                    database.AddInParameter(command, "@State", DbType.String, objuser.state);
                    database.AddInParameter(command, "@City", DbType.String, objuser.city);
                    database.AddInParameter(command, "@password", DbType.String, objuser.password);
                    database.AddInParameter(command, "@designation", DbType.String, objuser.designation);
                    database.AddInParameter(command, "@status", DbType.String, objuser.status);
                    database.AddInParameter(command, "@Picture", DbType.String, objuser.picture);
                    database.AddInParameter(command, "@attachement", DbType.String, objuser.attachements);
                    database.AddInParameter(command, "@bussinessname", DbType.String, objuser.businessname);
                    database.AddInParameter(command, "@ssn", DbType.String, objuser.ssn);
                    database.AddInParameter(command, "@ssn1", DbType.String, objuser.ssn1);
                    database.AddInParameter(command, "@ssn2", DbType.String, objuser.ssn2);
                    database.AddInParameter(command, "@signature", DbType.String, objuser.signature);
                    database.AddInParameter(command, "@dob", DbType.String, objuser.dob);
                    database.AddInParameter(command, "@citizenship", DbType.String, objuser.citizenship);
                    //database.AddInParameter(command, "@tin ", DbType.String, objuser.tin);
                    database.AddInParameter(command, "@ein1 ", DbType.String, objuser.ein1);
                    database.AddInParameter(command, "@ein2 ", DbType.String, objuser.ein2);
                    database.AddInParameter(command, "@a", DbType.String, objuser.a);
                    database.AddInParameter(command, "@b", DbType.String, objuser.b);
                    database.AddInParameter(command, "@c", DbType.String, objuser.c);
                    database.AddInParameter(command, "@d", DbType.String, objuser.d);
                    database.AddInParameter(command, "@e", DbType.String, objuser.e);
                    database.AddInParameter(command, "@f", DbType.String, objuser.f);
                    database.AddInParameter(command, "@g", DbType.String, objuser.g);
                    database.AddInParameter(command, "@h", DbType.String, objuser.h);
                    database.AddInParameter(command, "@i", DbType.String, objuser.i);
                    database.AddInParameter(command, "@j", DbType.String, objuser.j);
                    database.AddInParameter(command, "@k", DbType.String, objuser.k);
                    database.AddInParameter(command, "@maritalstatus", DbType.String, objuser.maritalstatus);
                    database.AddInParameter(command, "@PrimeryTradeId", DbType.Int32, objuser.PrimeryTradeId);
                    database.AddInParameter(command, "@SecondoryTradeId", DbType.Int32, objuser.SecondoryTradeId);
                    database.AddInParameter(command, "@Source", DbType.String, objuser.Source);
                    database.AddInParameter(command, "@Notes", DbType.String, objuser.Notes);
                    database.AddInParameter(command, "@StatusReason", DbType.String, objuser.Reason);
                    database.AddInParameter(command, "@GeneralLiability", DbType.String, objuser.GeneralLiability);
                    database.AddInParameter(command, "@PCLiscense", DbType.String, objuser.PqLicense);
                    database.AddInParameter(command, "@WorkerComp", DbType.String, objuser.WorkersComp);
                    database.AddInParameter(command, "@HireDate", DbType.String, objuser.HireDate);
                    database.AddInParameter(command, "@TerminitionDate", DbType.String, objuser.TerminitionDate);
                    database.AddInParameter(command, "@WorkersCompCode", DbType.String, objuser.WorkersCompCode);
                    database.AddInParameter(command, "@NextReviewDate", DbType.String, objuser.NextReviewDate);
                    database.AddInParameter(command, "@EmpType", DbType.String, objuser.EmpType);
                    database.AddInParameter(command, "@LastReviewDate", DbType.String, objuser.LastReviewDate);
                    database.AddInParameter(command, "@PayRates", DbType.String, objuser.PayRates);
                    database.AddInParameter(command, "@ExtraEarning", DbType.String, objuser.ExtraEarning);
                    database.AddInParameter(command, "@ExtraIncomeType", DbType.String, objuser.ExtraIncomeType);
                    database.AddInParameter(command, "@ExtraEarningAmt", DbType.String, objuser.ExtraEarningAmt);
                    database.AddInParameter(command, "@PayMethod", DbType.String, objuser.PayMethod);
                    database.AddInParameter(command, "@Deduction", DbType.String, objuser.Deduction);
                    database.AddInParameter(command, "@DeductionType", DbType.String, objuser.DeductionType);
                    database.AddInParameter(command, "@AbaAccountNo", DbType.String, objuser.AbaAccountNo);
                    database.AddInParameter(command, "@AccountNo", DbType.String, objuser.AccountNo);
                    database.AddInParameter(command, "@AccountType", DbType.String, objuser.AccountType);
                    database.AddInParameter(command, "@PTradeOthers", DbType.String, objuser.PTradeOthers);
                    database.AddInParameter(command, "@STradeOthers", DbType.String, objuser.STradeOthers);
                    database.AddInParameter(command, "@DeductionReason", DbType.String, objuser.DeductionReason);
                    database.AddInParameter(command, "@SuiteAptRoom", DbType.String, objuser.str_SuiteAptRoom);

                    database.AddInParameter(command, "@FullTimePosition", DbType.Int32, objuser.FullTimePosition);
                    database.AddInParameter(command, "@ContractorsBuilderOwner", DbType.String, objuser.ContractorsBuilderOwner);
                    database.AddInParameter(command, "@MajorTools", DbType.String, objuser.MajorTools);
                    database.AddInParameter(command, "@DrugTest", DbType.Boolean, objuser.DrugTest);
                    database.AddInParameter(command, "@ValidLicense", DbType.Boolean, objuser.ValidLicense);
                    database.AddInParameter(command, "@TruckTools", DbType.Boolean, objuser.TruckTools);
                    database.AddInParameter(command, "@PrevApply", DbType.Boolean, objuser.PrevApply);
                    database.AddInParameter(command, "@LicenseStatus", DbType.Boolean, objuser.LicenseStatus);
                    database.AddInParameter(command, "@CrimeStatus", DbType.Boolean, objuser.CrimeStatus);
                    database.AddInParameter(command, "@StartDate", DbType.String, objuser.StartDate);
                    database.AddInParameter(command, "@SalaryReq", DbType.String, objuser.SalaryReq);
                    database.AddInParameter(command, "@Avialability", DbType.String, objuser.Avialability);
                    database.AddInParameter(command, "@ResumePath", DbType.String, objuser.ResumePath);
                    database.AddInParameter(command, "@skillassessmentstatus", DbType.Boolean, objuser.skillassessmentstatus);
                    database.AddInParameter(command, "@assessmentPath", DbType.String, objuser.assessmentPath);
                    database.AddInParameter(command, "@WarrentyPolicy", DbType.String, objuser.WarrentyPolicy);
                    database.AddInParameter(command, "@CirtificationTraining", DbType.String, objuser.CirtificationTraining);
                    database.AddInParameter(command, "@businessYrs", DbType.Decimal, objuser.businessYrs);
                    database.AddInParameter(command, "@underPresentComp", DbType.Decimal, objuser.underPresentComp);
                    database.AddInParameter(command, "@websiteaddress", DbType.String, objuser.websiteaddress);
                    database.AddInParameter(command, "@PersonName", DbType.String, objuser.PersonName);
                    database.AddInParameter(command, "@PersonType", DbType.String, objuser.PersonType);
                    database.AddInParameter(command, "@CompanyPrinciple", DbType.String, objuser.CompanyPrinciple);
                    database.AddInParameter(command, "@UserType", DbType.String, objuser.UserType);
                    database.AddInParameter(command, "@Email2", DbType.String, objuser.Email2);
                    database.AddInParameter(command, "@Phone2", DbType.String, objuser.Phone2);
                    database.AddInParameter(command, "@CompanyName", DbType.String, objuser.CompanyName);
                    database.AddInParameter(command, "@SourceUser", DbType.String, objuser.SourceUser);
                    database.AddInParameter(command, "@DateSourced", DbType.String, objuser.DateSourced);
                    database.AddInParameter(command, "@InstallerType", DbType.String, objuser.InstallerType);

                    database.AddInParameter(command, "@BusinessType", DbType.String, objuser.BusinessType);
                    database.AddInParameter(command, "@CEO", DbType.String, objuser.CEO);
                    database.AddInParameter(command, "@LegalOfficer", DbType.String, objuser.LegalOfficer);
                    database.AddInParameter(command, "@President", DbType.String, objuser.President);
                    database.AddInParameter(command, "@Owner", DbType.String, objuser.Owner);
                    database.AddInParameter(command, "@AllParteners", DbType.String, objuser.AllParteners);
                    database.AddInParameter(command, "@MailingAddress", DbType.String, objuser.MailingAddress);
                    database.AddInParameter(command, "@Warrantyguarantee", DbType.Boolean, objuser.Warrantyguarantee);
                    database.AddInParameter(command, "@WarrantyYrs", DbType.Int32, objuser.WarrantyYrs);
                    database.AddInParameter(command, "@MinorityBussiness", DbType.Boolean, objuser.MinorityBussiness);
                    database.AddInParameter(command, "@WomensEnterprise", DbType.Boolean, objuser.WomensEnterprise);
                    database.AddInParameter(command, "@InterviewTime", DbType.String, objuser.InterviewTime);
                    database.AddInParameter(command, "@LIBC", DbType.String, objuser.UserActivated);
                    database.AddInParameter(command, "@Flag", DbType.Int32, objuser.Flag);

                    database.AddInParameter(command, "@CruntEmployement", DbType.Boolean, objuser.CruntEmployement);
                    database.AddInParameter(command, "@CurrentEmoPlace", DbType.String, objuser.CurrentEmoPlace);
                    database.AddInParameter(command, "@LeavingReason", DbType.String, objuser.LeavingReason);
                    database.AddInParameter(command, "@CompLit", DbType.Boolean, objuser.CompLit);
                    database.AddInParameter(command, "@FELONY", DbType.Boolean, objuser.FELONY);
                    database.AddInParameter(command, "@shortterm", DbType.String, objuser.shortterm);
                    database.AddInParameter(command, "@LongTerm", DbType.String, objuser.LongTerm);
                    database.AddInParameter(command, "@BestCandidate", DbType.String, objuser.BestCandidate);
                    database.AddInParameter(command, "@TalentVenue", DbType.String, objuser.TalentVenue);
                    database.AddInParameter(command, "@Boardsites", DbType.String, objuser.Boardsites);
                    database.AddInParameter(command, "@NonTraditional", DbType.String, objuser.NonTraditional);
                    database.AddInParameter(command, "@ConSalTraning", DbType.String, objuser.ConSalTraning);
                    database.AddInParameter(command, "@BestTradeOne", DbType.String, objuser.BestTradeOne);
                    database.AddInParameter(command, "@BestTradeTwo", DbType.String, objuser.BestTradeTwo);
                    database.AddInParameter(command, "@BestTradeThree", DbType.String, objuser.BestTradeThree);


                    database.AddInParameter(command, "@aOne", DbType.String, objuser.aOne);
                    database.AddInParameter(command, "@aOneTwo", DbType.String, objuser.aOneTwo);
                    database.AddInParameter(command, "@bOne", DbType.String, objuser.bOne);
                    database.AddInParameter(command, "@cOne", DbType.String, objuser.cOne);
                    database.AddInParameter(command, "@aTwo", DbType.String, objuser.aTwo);
                    database.AddInParameter(command, "@aTwoTwo", DbType.String, objuser.aTwoTwo);
                    database.AddInParameter(command, "@bTwo", DbType.String, objuser.bTwo);
                    database.AddInParameter(command, "@cTwo", DbType.String, objuser.cTwo);
                    database.AddInParameter(command, "@aThree", DbType.String, objuser.aThree);
                    database.AddInParameter(command, "@aThreeTwo", DbType.String, objuser.aThreeTwo);
                    database.AddInParameter(command, "@bThree", DbType.String, objuser.bThree);
                    database.AddInParameter(command, "@cThree", DbType.String, objuser.cThree);

                    database.AddInParameter(command, "@AddedBy", DbType.Int32, objuser.AddedBy);



                    database.AddInParameter(command, "@RejectionDate", DbType.String, objuser.RejectionDate);
                    database.AddInParameter(command, "@RejectionTime", DbType.String, objuser.RejectionTime);

                    database.AddInParameter(command, "@RejectedUserId", DbType.Int32, objuser.RejectedUserId);
                    database.AddInParameter(command, "@TC", DbType.Boolean, objuser.TC);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (res == 1)
                        return true;
                    else
                        return false;
                }
            }

            catch (Exception ex)
            {
                return false;

            }
        }

        public bool UpdateInstallUserStatus(string Status, int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("jgrov_User.UDP_UpdateStatus ");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.String, Convert.ToInt32(id));
                    database.AddInParameter(command, "@status", DbType.String, Status);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (res == 1)
                        return true;
                    else
                        return false;
                }
            }

            catch (Exception ex)
            {
                return false;

            }
        }

        public void ChangeStatus(string Status, int id, string RejectionDate, string RejectionTime, int RejectedUserId, string StatusReason = "")
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_AddIntalledStatus");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.String, Convert.ToInt32(id));
                    database.AddInParameter(command, "@status", DbType.String, Status);
                    database.AddInParameter(command, "@RejectionDate", DbType.String, RejectionDate);
                    database.AddInParameter(command, "@RejectionTime", DbType.String, RejectionTime);
                    database.AddInParameter(command, "@RejectedUserId", DbType.Int32, RejectedUserId);
                    database.AddInParameter(command, "@StatusReason", DbType.String, StatusReason);
                    database.ExecuteScalar(command);
                    //int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                }
            }

            catch (Exception ex)
            {
            }
        }



        public void ChangeStatusToInterviewDate(string Status, int id, string RejectionDate, string RejectionTime, int RejectedUserId, string time, string StatusReason = "")
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_UpdateToInterviewDateFromEdit");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.String, Convert.ToInt32(id));
                    database.AddInParameter(command, "@status", DbType.String, Status);
                    database.AddInParameter(command, "@RejectionDate", DbType.String, RejectionDate);
                    database.AddInParameter(command, "@RejectionTime", DbType.String, RejectionTime);
                    database.AddInParameter(command, "@RejectedUserId", DbType.Int32, RejectedUserId);
                    database.AddInParameter(command, "@StatusReason", DbType.String, StatusReason);
                    database.AddInParameter(command, "@InterviewTime", DbType.Int32, time);
                    database.ExecuteScalar(command);
                    //int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                }
            }

            catch (Exception ex)
            {
            }
        }


        public bool InsertIntoHRReport(int SourceId, int InstallerId, string Status)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("Usp_UpdateInstallStatus");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@SourceId", DbType.Int32, SourceId);
                    database.AddInParameter(command, "@InstallerId", DbType.Int32, InstallerId);
                    database.AddInParameter(command, "@Status", DbType.String, Status);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (res == 1)
                        return true;
                    else
                        return false;
                }
            }

            catch (Exception ex)
            {
                return false;

            }
        }


        public DataSet getallInstallusers()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetallInstallusersdataNew");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetAllSalesInstallUsers()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_GetAllSalesInstallUsers");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetAllEditSalesUser()
        {
            DataSet returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("GetAllEditSalesUser");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);
                }
            }

            catch
            {
                throw;
            }

            return returndata;
        }

        public DataSet ExportAllInstallUsersData()
        {
            DataSet returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                   // DbCommand command = database.GetStoredProcCommand("[jgrov_User].[ExportAllInstallUsersData]");
                    DbCommand command = database.GetStoredProcCommand("ExportAllInstallUsersData");
                    
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);


                }
            }

            catch
            {
                throw;
            }
            return returndata;
        }


        public DataSet ExportAllSalesUsersData()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("[jgrov_User].[ExportAllSalesUsersData]");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }




        public DataSet getTrade()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetTrade");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public DataSet getUserList()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetUserList");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public bool DeleteInstallUser(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_deleteInstalluser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, id);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (res == 1)
                        return true;
                    else
                        return false;
                }
            }

            catch (Exception ex)
            {
                return false;

            }

        }

        public DataSet getuserdetails(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GETInstallUserDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, id);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }


        public DataSet getalluserdetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GETAllInstallUserDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public DataTable getMaxId(string userType, string userStatus)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataTable dtId = new DataTable();
                    DbCommand command = database.GetStoredProcCommand("UDP_GEtMaxId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@designation", DbType.String, userType);
                    database.AddInParameter(command, "@status", DbType.String, userStatus);
                    dtId = database.ExecuteDataSet(command).Tables[0];
                    return dtId;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetMaxSalesId(string Designition)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataTable dtId = new DataTable();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetSalesMaxId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@designation", DbType.String, Designition);
                    dtId = database.ExecuteDataSet(command).Tables[0];
                    return dtId;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet getInstallerUserDetailsByLoginId(string loginid)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetInstallerUserDetailsByLoginId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@loginid", DbType.String, loginid);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public DataSet getInstallerUserDetailsByLoginId(string Email, string Password)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerDetailsByLoginId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Email", DbType.String, Email);
                    database.AddInParameter(command, "@Password", DbType.String, Password);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public DataSet getCustomerLogin(string Email, string Password)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_CUSTOMERLOGIN");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Email", DbType.String, Email);
                    database.AddInParameter(command, "@Password", DbType.String, Password);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }




        public void Activateuser(string loginid)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_ActivateUser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Login_Id", DbType.String, loginid);
                    returndata = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public DataSet GetAttachment(int id)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAttachments");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, id);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;

            }
        }


        public int IsValidInstallerUser(string userid, string password)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_IsValidInstallerUser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@userid", DbType.String, userid);
                    database.AddInParameter(command, "@password", DbType.String, password);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));

                    return res;
                }
            }

            catch (Exception ex)
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }

        public DataSet GetJobsForInstaller()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetJobsForInstaller");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }

        public string GetAllCategoriesForReferenceId(string refId)
        {
            string result = string.Empty;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllCategoriesForSoldJobId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@refId", DbType.String, refId);
                    database.AddOutParameter(command, "@result", DbType.String, 500);
                    returndata = database.ExecuteDataSet(command);
                    result = returndata.Tables[0].Rows[0][0].ToString();
                    //result = Convert.ToString(database.GetParameterValue(command, "@result"));
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return result;
        }


        #endregion

        public DataSet GetInstallerAvailability(string referenceId, int installerId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetInstallerAvailability");
                    database.AddInParameter(command, "@referenceId", DbType.String, referenceId);
                    database.AddInParameter(command, "@installerId", DbType.Int16, installerId);
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }
        public void AddEditInstallerAvailability(Availability a)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddEditInstallerAvailability");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@installerId", DbType.Int16, a.InstallerId);
                    database.AddInParameter(command, "@primary", DbType.String, a.Primary);
                    database.AddInParameter(command, "@secondary1", DbType.String, a.Secondary1);
                    database.AddInParameter(command, "@secondary2", DbType.String, a.Secondary2);
                    database.AddInParameter(command, "@referenceId", DbType.String, a.ReferenceId);
                    // database.AddInParameter(command, "@jobSequenceId", DbType.Int16, a.JobSequenceId);
                    database.ExecuteDataSet(command);
                }
            }

            catch (Exception ex)
            {
            }

        }

        public bool ChangeInstallerPassword(int loginid, string password)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_ChangeInstallerPassword");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@loginid", DbType.Int16, loginid);
                    database.AddInParameter(command, "@password", DbType.String, password);

                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteScalar(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (res == 1)
                        return true;
                    else
                        return false;
                }
            }

            catch (Exception ex)
            {
                return false;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }


        public DataTable getEmailTemplate(string status, string Part)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataTable dtId = new DataTable();
                    DbCommand command = database.GetStoredProcCommand("usp_GetTemplate");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@status", DbType.String, status);
                    database.AddInParameter(command, "@Part", DbType.String, Part);
                    dtId = database.ExecuteDataSet(command).Tables[0];
                    return dtId;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetSalesTouchPointLogData(int CustomerId, int userid)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchSalesUserTouchPointLogData");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerId", DbType.Int32, CustomerId);
                    database.AddInParameter(command, "@userid", DbType.Int32, userid);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }

        public int AddSalesFollowUp(int customerid, DateTime meetingdate, string Status, int userId)
        {
            int result = 0;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddEntryInSalesUser_followup");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@custId", DbType.Int32, customerid);
                    database.AddInParameter(command, "@MeetingDate", DbType.DateTime, meetingdate);
                    database.AddInParameter(command, "@MeetingStatus", DbType.String, Status);
                    database.AddInParameter(command, "@UserId", DbType.Int32, userId);

                    database.ExecuteNonQuery(command);
                }
                return result;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return 0;
            }
        }
    }
}
