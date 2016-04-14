using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;
using JG_Prospect.DAL.Database;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Configuration;

namespace JG_Prospect.DAL
{
    public class UserDAL
    {
        private static UserDAL m_UserDAL = new UserDAL();

        private UserDAL()
        {

        }

        public static UserDAL Instance
        {
            get { return m_UserDAL; }
            private set { ;}
        }

        private DataSet returndata;

        #region userlogin

        public bool AddUser(user objuser)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddUser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@username", DbType.String, objuser.username);
                    database.AddInParameter(command, "@loginid", DbType.String, objuser.loginid);
                    database.AddInParameter(command, "@email", DbType.String, objuser.email);
                    database.AddInParameter(command, "@password", DbType.String, objuser.password);
                    database.AddInParameter(command, "@designation", DbType.String, objuser.designation);
                    database.AddInParameter(command, "@usertype", DbType.String, objuser.usertype);
                    database.AddInParameter(command, "@status", DbType.String, objuser.status);
                    database.AddInParameter(command, "@phone", DbType.String, objuser.phone);
                    database.AddInParameter(command, "@address", DbType.String, objuser.address);
                    database.AddInParameter(command, "@picture", DbType.String, objuser.picture);
                    database.AddInParameter(command, "@attachement", DbType.String, objuser.attachements);
                    database.AddInParameter(command, "@firstname", DbType.String, objuser.fristname);
                    database.AddInParameter(command, "@lastname", DbType.String, objuser.lastname);
                    database.AddInParameter(command, "@zip", DbType.String, objuser.zip);
                    database.AddInParameter(command, "@state", DbType.String, objuser.state);
                    database.AddInParameter(command, "@city", DbType.String, objuser.city);
                    database.AddInParameter(command, "@bussinessname", DbType.String, objuser.businessname);
                    database.AddInParameter(command, "@ssn", DbType.String, objuser.ssn);
                    database.AddInParameter(command, "@signature", DbType.String, objuser.signature);
                    database.AddInParameter(command, "@dob", DbType.String, objuser.dob);
                    database.AddInParameter(command, "@ssn1", DbType.String, objuser.ssn1);
                    database.AddInParameter(command, "@ssn2", DbType.String, objuser.ssn2);
                    database.AddInParameter(command, "@citizenship", DbType.String, objuser.citizenship);
                    //database.AddInParameter(command, "@tin", DbType.String, objuser.tin);
                    database.AddInParameter(command, "@ein1", DbType.String, objuser.ein1);
                    database.AddInParameter(command, "@ein2", DbType.String, objuser.ein2);
                    database.AddInParameter(command, "@maritalstatus", DbType.String, objuser.maritalstatus);
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
        public DataSet getUser(string loginid)
        {
            
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getuserdetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@loginid", DbType.String, loginid);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }

        public DataSet GetuserData(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GETAllUserDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.String, id);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public bool UpdatingUser(user objuser, int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateUsers");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.String, Convert.ToInt32(id));
                    database.AddInParameter(command, "@loginid", DbType.String, objuser.loginid);
                    database.AddInParameter(command, "@username", DbType.String, objuser.username);
                    database.AddInParameter(command, "@email", DbType.String, objuser.email);
                    database.AddInParameter(command, "@password", DbType.String, objuser.password);
                    database.AddInParameter(command, "@usertype", DbType.String, objuser.usertype);
                    database.AddInParameter(command, "@designation", DbType.String, objuser.designation);
                    database.AddInParameter(command, "@status", DbType.String, objuser.status);
                    database.AddInParameter(command, "@phone", DbType.String, objuser.phone);
                    database.AddInParameter(command, "@address", DbType.String, objuser.address);
                    database.AddInParameter(command, "@picture", DbType.String, objuser.picture);
                    database.AddInParameter(command, "@attachements", DbType.String, objuser.attachements);
                    database.AddInParameter(command, "@firstname", DbType.String, objuser.fristname);
                    database.AddInParameter(command, "@lastname", DbType.String, objuser.lastname);
                    database.AddInParameter(command, "@zip", DbType.String, objuser.zip);
                    database.AddInParameter(command, "@state", DbType.String, objuser.state);
                    database.AddInParameter(command, "@city", DbType.String, objuser.city);
                    database.AddInParameter(command, "@bussinessname", DbType.String, objuser.businessname);
                    database.AddInParameter(command, "@ssn", DbType.String, objuser.ssn);
                    database.AddInParameter(command, "@signature", DbType.String, objuser.signature);
                    database.AddInParameter(command, "@dob", DbType.String, objuser.dob);
                    database.AddInParameter(command, "@ssn1", DbType.String, objuser.ssn1);
                    database.AddInParameter(command, "@ssn2", DbType.String, objuser.ssn2);
                    database.AddInParameter(command, "@citizenship", DbType.String, objuser.citizenship);
                    //database.AddInParameter(command, "@tin", DbType.String, objuser.tin);
                    database.AddInParameter(command, "@ein1", DbType.String, objuser.ein1);
                    database.AddInParameter(command, "@ein2", DbType.String, objuser.ein2);
                    database.AddInParameter(command, "@maritalstatus", DbType.String, objuser.maritalstatus);
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

        public bool UpdateUser(user objuser, int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateUsersbyAdmin");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, Convert.ToInt32(id));
                    database.AddInParameter(command, "@loginid", DbType.String, objuser.loginid);
                    database.AddInParameter(command, "@username", DbType.String, objuser.username);
                    database.AddInParameter(command, "@email", DbType.String, objuser.email);
                    database.AddInParameter(command, "@password", DbType.String, objuser.password);
                    database.AddInParameter(command, "@usertype", DbType.String, objuser.usertype);
                    database.AddInParameter(command, "@designation", DbType.String, objuser.designation);
                    database.AddInParameter(command, "@status", DbType.String, objuser.status);
                    database.AddInParameter(command, "@phone", DbType.String, objuser.phone);
                    database.AddInParameter(command, "@address", DbType.String, objuser.address);
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



        public int chklogin(string userid, string password)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_chklogin");
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

        public DataSet getallusers(string usertype)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_Getallusersdata");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@usertype", DbType.String, usertype);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet getSalesusers()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_GetSalesUser");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public DataSet GetAllEditSalesUser()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("GetAllEditSalesUser");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public DataSet getSrusers()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllSrUsers");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }



        public DataSet Getcurrentperioddates()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_Getcurrentperioddates");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public DataSet getuserdetails(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getuserstype_n_designation");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, id);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet getSSEuserdetails(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_userstype_n_designation");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, id);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public bool DeleteUser(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_deleteuser");
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
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }


        public bool changepassword(int loginid, string password)//, string usertype, string password)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_changepassword");
                    command.CommandType = CommandType.StoredProcedure;
                    // database.AddInParameter(command, "@usertype", DbType.String, usertype);
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


        #endregion


        #region period

        public bool SavePeriod(period objperiod)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    DbCommand command = database.GetStoredProcCommand("UDP_savePeriod");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@PeriodId", DbType.Int32, objperiod.id);
                    database.AddInParameter(command, "@periodname", DbType.String, objperiod.periodname);
                    database.AddInParameter(command, "@fromdate", DbType.Date, Convert.ToDateTime(objperiod.fromdate).ToShortDateString());
                    database.AddInParameter(command, "@todate", DbType.Date, Convert.ToDateTime(objperiod.todate).ToShortDateString());

                    database.ExecuteNonQuery(command);

                    return true;
                }
            }

            catch (Exception ex)
            {
                return false;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public bool deleteperiod(int periodname)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_deletePeriod");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@periodname", DbType.Int32, periodname);

                    database.ExecuteNonQuery(command);

                    return true;
                }
            }

            catch (Exception ex)
            {
                return false;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public DataSet getallperiod()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getallPeriod");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet getperioddetails(int periodId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetPeriodDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int16, periodId);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        #endregion


        #region prospect

        public int addprospect(prospect objprospect)
        {
            try
            {

                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_saveprospect");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@user", DbType.String, objprospect.user);
                    database.AddInParameter(command, "@date", DbType.Date, Convert.ToDateTime(objprospect.estimatedate).ToShortDateString());
                    database.AddInParameter(command, "@time", DbType.String, objprospect.estimatetime);
                    database.AddInParameter(command, "@firstname", DbType.String, objprospect.firstname);
                    database.AddInParameter(command, "@lastname", DbType.String, objprospect.lastname);
                    database.AddInParameter(command, "@cellphone", DbType.String, objprospect.cellphone);
                    database.AddInParameter(command, "@alt_phone", DbType.String, objprospect.alt_phone);
                    database.AddInParameter(command, "@homephone", DbType.String, objprospect.home_phone);
                    database.AddInParameter(command, "@email", DbType.String, objprospect.email);
                    database.AddInParameter(command, "@address", DbType.String, objprospect.address);
                    database.AddInParameter(command, "@city", DbType.String, objprospect.city);
                    database.AddInParameter(command, "@state", DbType.String, objprospect.state);
                    database.AddInParameter(command, "@zip", DbType.String, objprospect.zip);
                    database.AddInParameter(command, "@followupdate", DbType.String, objprospect.followup_date);
                    database.AddInParameter(command, "@followupstatus", DbType.String, objprospect.followup_status);
                    database.AddInParameter(command, "@product_of_interest", DbType.String, objprospect.product_of_interest);
                    database.AddInParameter(command, "@best_time_to_contact", DbType.String, objprospect.best_time_to_contact);
                    database.AddInParameter(command, "@missing_contact", DbType.Int32, objprospect.missing_contacts);
                    database.AddInParameter(command, "@status", DbType.String, objprospect.status);
                    database.AddInParameter(command, "@notes", DbType.String, objprospect.notes);
                    database.AddInParameter(command, "@Billing_address", DbType.String, objprospect.Billing_address);
                    database.AddInParameter(command, "@Primarycontact", DbType.String, objprospect.Primarycontact);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteNonQuery(command);

                    int i = Convert.ToInt32(database.GetParameterValue(command, "@result"));

                    return i;
                }
            }

            catch (Exception ex)
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public int updateprospect(prospect objprospect)
        {
            try
            {

                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_updateprospect");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, objprospect.ID);
                    database.AddInParameter(command, "@user", DbType.String, objprospect.user);
                    database.AddInParameter(command, "@date", DbType.Date, Convert.ToDateTime(objprospect.estimatedate).ToShortDateString());
                    database.AddInParameter(command, "@time", DbType.String, objprospect.estimatetime);
                    database.AddInParameter(command, "@firstname", DbType.String, objprospect.firstname);
                    database.AddInParameter(command, "@lastname", DbType.String, objprospect.lastname);
                    database.AddInParameter(command, "@phone", DbType.String, objprospect.cellphone);
                    database.AddInParameter(command, "@alt_phone", DbType.String, objprospect.alt_phone);
                    database.AddInParameter(command, "@homephone", DbType.String, objprospect.home_phone);
                    database.AddInParameter(command, "@email", DbType.String, objprospect.email);
                    database.AddInParameter(command, "@address", DbType.String, objprospect.address);
                    database.AddInParameter(command, "@city", DbType.String, objprospect.city);
                    database.AddInParameter(command, "@state", DbType.String, objprospect.state);
                    database.AddInParameter(command, "@zip", DbType.String, objprospect.zip);
                    database.AddInParameter(command, "@followupdate", DbType.String, objprospect.followup_date);
                    database.AddInParameter(command, "@followupstatus", DbType.String, objprospect.followup_status);
                    database.AddInParameter(command, "@product_of_interest", DbType.String, objprospect.product_of_interest);
                    database.AddInParameter(command, "@best_time_to_contact", DbType.String, objprospect.best_time_to_contact);
                    database.AddInParameter(command, "@missing_contact", DbType.Int32, objprospect.missing_contacts);
                    database.AddInParameter(command, "@status", DbType.String, objprospect.status);
                    database.AddInParameter(command, "@notes", DbType.String, objprospect.notes);
                    database.AddInParameter(command, "@Billing_address", DbType.String, objprospect.Billing_address);
                    database.AddInParameter(command, "@Primarycontact", DbType.String, objprospect.Primarycontact);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteNonQuery(command);

                    int i = Convert.ToInt32(database.GetParameterValue(command, "@result"));

                    return i;
                }
            }

            catch (Exception ex)
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet Fetchleadssummary(DateTime? frmdate, DateTime? todate, string username)
        {
            try
            {
                string Event = "onload";
                if (frmdate == DateTime.MinValue && todate == DateTime.MinValue)
                {
                    frmdate = null;
                    todate = null;
                }
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_Fetchleadssummary");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@fromdate", DbType.Date, frmdate);
                    database.AddInParameter(command, "@todate", DbType.Date, todate);
                    database.AddInParameter(command, "@username", DbType.String, username);
                    database.AddInParameter(command, "@Event", DbType.String, Event);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public DataSet FetchProspectstoassign(DateTime? frmdate, DateTime? todate, string username)
        {
            try
            {

                if (frmdate == DateTime.MinValue && todate == DateTime.MinValue)
                {
                    frmdate = null;
                    todate = null;
                }
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchProspectstoassign");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@fromdate", DbType.Date, frmdate);
                    database.AddInParameter(command, "@todate", DbType.Date, todate);
                    database.AddInParameter(command, "@username", DbType.String, username);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet Fetchprogressreport(DateTime frmdate, DateTime todate, string username)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_Fetchprogressreport");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@fromdate", DbType.Date, frmdate);
                    database.AddInParameter(command, "@todate", DbType.Date, todate);
                    database.AddInParameter(command, "@username", DbType.String, username);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet Fetchstaticreport(prospect objprospect)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_Fetchstaticreport");
                    command.CommandType = CommandType.StoredProcedure;
                    //database.AddInParameter(command, "@fname", DbType.String, objprospect.firstname);
                    //database.AddInParameter(command, "@lname", DbType.String, objprospect.lastname);
                    //database.AddInParameter(command, "@phone", DbType.String, objprospect.cellphone);
                    //database.AddInParameter(command, "@email", DbType.String, objprospect.email);
                    //database.AddInParameter(command, "@followupdate", DbType.String, objprospect.followup_date);
                    database.AddInParameter(command, "@status", DbType.String, objprospect.status);
                    database.AddInParameter(command, "@user", DbType.String, objprospect.user);
                    database.AddInParameter(command, "@usertype", DbType.String, objprospect.usertyp);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet getprospectdetails(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getprospectdetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, id);
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public string GetProductNameByProductId(int id)
        {
            try
            {
                string productName = string.Empty;
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetProductNameByProductId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@productId", DbType.Int32, id);
                    productName = database.ExecuteScalar(command).ToString();

                    return productName;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public int GetProductIDByProductName(string name)
        {
            try
            {
                int productId = 0;
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetProductIDByProductName");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@productName", DbType.String, name);
                    productId = Convert.ToInt16(database.ExecuteScalar(command).ToString());

                    return productId;
                }
            }

            catch (Exception ex)
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public DataSet fetchzipcode(string zipcode)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getallzipcode");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@zip", DbType.String, zipcode);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        public DataSet fetchcitystate(string zipcode)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getcitybyzip");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@zipcode", DbType.String, zipcode);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        #endregion

        public bool SaveResources(string link, string description, string type)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_SaveResources");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@link", DbType.String, link);
                    database.AddInParameter(command, "@description", DbType.String, description);
                    database.AddInParameter(command, "@type", DbType.String, type);
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



        public DataSet GetResources(string type)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllResources");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Type", DbType.String, type);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetAllProducts()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllProducts");
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

        public DataSet GetAllLiterature(string type)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllResources");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Type", DbType.String, type);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetAlltrainingtool(string type)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllTrainingtool");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Type", DbType.String, type);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetAllVideo()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_getallvideo");
                    command.CommandType = CommandType.StoredProcedure;
                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
        public bool updateProspectstatus(int Estimateid, string status, DateTime? followupdate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    if (followupdate == DateTime.MinValue)
                        followupdate = null;

                    DbCommand command = database.GetStoredProcCommand("UDP_updateprospectstatus");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@EstimateId", DbType.Int32, Estimateid);
                    database.AddInParameter(command, "@status", DbType.String, status);
                    database.AddInParameter(command, "@followupdate", DbType.Date, followupdate);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int CalculateTotalSeenEST(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    int totalSeenEST = 0;
                    DbCommand command = database.GetSqlStringCommand("SELECT dbo.UDF_CalculateTotalSeenEST(@UserId,@FromDate,@ToDate);");                    
                    command.CommandType = CommandType.Text;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);

                    totalSeenEST = Convert.ToInt16(database.ExecuteScalar(command));

                    return totalSeenEST;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int CalculateTotalProspects(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    int totalProspect = 0;
                    DbCommand command = database.GetSqlStringCommand("SELECT dbo.UDF_CalculateTotalProspects(@UserId,@FromDate,@ToDate);");
                    command.CommandType = CommandType.Text;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);

                    totalProspect = Convert.ToInt16(database.ExecuteScalar(command));

                    return totalProspect;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int CalculateTotalSetEST(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    int totalSetEST = 0;
                    DbCommand command = database.GetSqlStringCommand("SELECT dbo.UDF_CalculateTotalSetEST(@UserId,@FromDate,@ToDate);");
                    command.CommandType = CommandType.Text;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);

                    totalSetEST = Convert.ToInt16(database.ExecuteScalar(command));

                    return totalSetEST;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public decimal CalculateJrRehashPercent(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    decimal totalRehash = 0;
                    DbCommand command = database.GetSqlStringCommand("SELECT dbo.UDF_CalculateJrRehashPercent(@UserId,@FromDate,@ToDate);");
                    command.CommandType = CommandType.Text;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);

                    totalRehash = Convert.ToDecimal(database.ExecuteScalar(command));

                    return totalRehash;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public decimal CalculateJrTotalGrossSale(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    decimal totalSale = 0;
                    DbCommand command = database.GetSqlStringCommand("SELECT dbo.UDF_CalculateGrossSalesOfJrUser(@UserId,@FromDate,@ToDate);");
                    command.CommandType = CommandType.Text;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);

                    totalSale = Convert.ToDecimal(database.ExecuteScalar(command));

                    return totalSale;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public decimal CalculateJrTotalProspectDataCompletion(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    decimal totalData = 0;
                    DbCommand command = database.GetSqlStringCommand("SELECT dbo.UDF_CalculateTotalProspectDataCompletion(@UserId,@FromDate,@ToDate);");
                    command.CommandType = CommandType.Text;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);

                    totalData = Convert.ToDecimal(database.ExecuteScalar(command));

                    return totalData;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int CalculateLeadsIssued(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    int LeadsIssued = 0;
                    // DataSet ds = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_CalculateFinalLeadsIssued");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);
                    database.AddOutParameter(command, "@LeadsIssued", DbType.Int16, LeadsIssued);
                    database.ExecuteScalar(command);
                    LeadsIssued = Convert.ToInt16(database.GetParameterValue(command, "@LeadsIssued"));

                    return LeadsIssued;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public decimal CalculateOverAllClosingPercent(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    decimal OverAllClosingPercent = 0;
                    // DataSet ds = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_CalculateOverAllClosingPercent");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);
                    database.AddOutParameter(command, "@OverAllClosingPercent", DbType.Decimal, sizeof(decimal));
                    database.ExecuteScalar(command);
                    OverAllClosingPercent = Convert.ToInt16(database.GetParameterValue(command, "@OverAllClosingPercent"));

                    return OverAllClosingPercent;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int CalculateLeadsSeen(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    int LeadsSeen = 0;
                    // DataSet ds = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_CalculateLeadsSeen");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);
                    database.AddOutParameter(command, "@LeadsSeen", DbType.Int16, LeadsSeen);
                    database.ExecuteScalar(command);
                    LeadsSeen = Convert.ToInt16(database.GetParameterValue(command, "@LeadsSeen"));

                    return LeadsSeen;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public decimal CalculateGrossSalesOfUser(int userId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    decimal GrossSales = 0;
                    // DataSet ds = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_CalculateGrossSalesOfUser");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);
                    database.AddOutParameter(command, "@GrossSales", DbType.Decimal, sizeof(decimal));
                    database.ExecuteScalar(command);
                    GrossSales = Convert.ToDecimal(database.GetParameterValue(command, "@GrossSales"));

                    return GrossSales;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataSet FetchSalesReport(int userId, DateTime fromDate, DateTime toDate)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    //DbCommand command = database.GetStoredProcCommand("UDP_FetchSalesReportNew");
                    DbCommand command = database.GetStoredProcCommand("UDP_GetSalesReportNew");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@UserId", DbType.Int16, userId);
                    database.AddInParameter(command, "@FromDate", DbType.Date, fromDate);
                    database.AddInParameter(command, "@ToDate", DbType.Date, toDate);
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

        public DataSet GetAllUsers()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataSet ds = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllUsers");
                    command.CommandType = CommandType.StoredProcedure;
                    ds = database.ExecuteDataSet(command);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchPrimaryContactDetails(int intContactId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_Fetching_Primary_Contact_Details");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@id", DbType.Int32, intContactId);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet BindJobImage()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_BindJobImage");
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
        public DataSet FetchLocationImage(int Id, string JobId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_Get_Location_Image_ByID");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerid", DbType.Int32, Id);
                    database.AddInParameter(command, "@SoldJobId", DbType.String, JobId);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
        public DataSet BindEndAddress(int Id, string EndAddress)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_BindEndAddress");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@EndAddress", DbType.String, EndAddress);
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, Id);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
        public DataSet fetchAllScripts(int? intScriptId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_GetScripts");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@intScriptId", DbType.Int32, intScriptId);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
        public DataSet manageScripts(PhoneDashboard objPhoneDashboard)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_ManageScripts");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@intMode", DbType.Int32, objPhoneDashboard.intMode);
                    database.AddInParameter(command, "@intScriptId", DbType.Int32, objPhoneDashboard.intScriptId);
                    database.AddInParameter(command, "@strScriptName", DbType.String, objPhoneDashboard.strScriptName);
                    database.AddInParameter(command, "@strScriptDescription", DbType.String, objPhoneDashboard.strScriptDescription);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
    }
}
