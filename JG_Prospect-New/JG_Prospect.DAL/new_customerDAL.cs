using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using JG_Prospect.DAL.Database;
using JG_Prospect.Common.modal;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using JG_Prospect.Common;

namespace JG_Prospect.DAL
{
    public class new_customerDAL
    {
        private static new_customerDAL m_new_customerDAL = new new_customerDAL();
        //    private DataSet returndata;

        private new_customerDAL()
        {

        }

        public static new_customerDAL Instance
        {
            get { return m_new_customerDAL; }
            private set { ;}
        }


        DataSet returndata = new DataSet();

        public int SearchEmailId(string emailid, string addedby)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_SearchEmailId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@emailid", DbType.String, emailid);
                    database.AddInParameter(command, "@addedby", DbType.String, addedby);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    //result1 = database.ExecuteNonQuery(command);
                    database.ExecuteNonQuery(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    return res;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public int Searchprimarycontact(string housePh, string cellph, string altPh,int cid)
        {          
            try
            {
              
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CheckPrimarycontact");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Cid", DbType.Int32, cid);
                    database.AddInParameter(command, "@housePh", DbType.String, housePh);
                    database.AddInParameter(command, "@cellPh", DbType.String, cellph);
                    database.AddInParameter(command, "@altPh", DbType.String, altPh);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                   // database.AddOutParameter(command, "@custid", DbType.Int32, 1);
                   // result1 = database.ExecuteNonQuery(command);
                    database.ExecuteNonQuery(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                   // int custid = Convert.ToInt32(database.GetParameterValue(command, "@custid"));
                                      

                    return res;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }

        public int AddSrCustomer(Customer c, DataTable dtbleAddress, DataTable dtbleBillingAddress, DataTable dtblePrimary, DataTable dtbleProduct, Boolean bitYesNo)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddnewCustomer_new");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@firstName", DbType.String, c.firstName);
                    database.AddInParameter(command, "@lastName", DbType.String, c.lastName);
                    database.AddInParameter(command, "@CustomerAddress", DbType.String, c.CustomerAddress);
                    database.AddInParameter(command, "@State", DbType.String, c.state);
                    database.AddInParameter(command, "@City", DbType.String, c.City);
                    database.AddInParameter(command, "@ZipCode", DbType.String, c.Zipcode);
                    //string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);
                    //database.AddInParameter(command, "@EstDateSchdule", DbType.DateTime, Convert.ToDateTime(c.EstDate, JGConstant.CULTURE));
                    database.AddInParameter(command, "@EstDateSchdule", DbType.DateTime, string.IsNullOrEmpty(c.EstDate) ? (DateTime?)null : DateTime.Parse(c.EstDate, JGConstant.CULTURE));
                    database.AddInParameter(command, "@EstTime", DbType.String, c.EstTime);
                    database.AddInParameter(command, "@CellPh", DbType.String, c.CellPh);
                    database.AddInParameter(command, "@HousePh", DbType.String, c.HousePh);
                    database.AddInParameter(command, "@Email", DbType.String, c.Email);
                    database.AddInParameter(command, "@Email2", DbType.String, c.Email2);
                    database.AddInParameter(command, "@Email3", DbType.String, c.Email3);
                    database.AddInParameter(command, "@CallTakenBy", DbType.String, c.CallTakenby);
                    //database.AddInParameter(command, "@Service", DbType.String,  c.Notes);
                    database.AddInParameter(command, "@ProductOfInterest", DbType.Int32, c.Productofinterest);
                    database.AddInParameter(command, "@SecProductOfInterest", DbType.Int32, c.SecondaryProductofinterest);
                    database.AddInParameter(command, "@ProjectManager", DbType.String, c.ProjectManager);

                    database.AddInParameter(command, "@AddedBy", DbType.String, c.Addedby);
                    database.AddInParameter(command, "@leadtype", DbType.String, c.Leadtype);
                    database.AddInParameter(command, "@AlternatePh", DbType.String, c.AltPh);
                    database.AddInParameter(command, "@BillingAddress", DbType.String, c.BillingAddress);
                    database.AddInParameter(command, "@BestTimetocontact", DbType.String, c.BestTimetocontact);
                    database.AddInParameter(command, "@PrimaryContact", DbType.String, c.PrimaryContact);
                    database.AddInParameter(command, "@ContactPreference", DbType.String, c.ContactPreference);
                    database.AddInParameter(command, "@Map1", DbType.String, c.Map1);
                    database.AddInParameter(command, "@Map2", DbType.String, c.Map2);
                    database.AddInParameter(command, "@status", DbType.String, c.status);
                    database.AddInParameter(command, "@Isrepeated", DbType.Boolean, c.Isrepeated);
                    database.AddInParameter(command, "@missing_contacts", DbType.Int32, c.missingcontacts);
                    //database.AddInParameter(command, "@followup_date", DbType.Date, Convert.ToDateTime(c.followupdate, JGConstant.CULTURE));

                    database.AddInParameter(command, "@followup_date", DbType.Date, null);

                    database.AddInParameter(command, "@CustAdd", SqlDbType.Structured, dtbleAddress);
                    database.AddInParameter(command, "@CustBilling", SqlDbType.Structured, dtbleBillingAddress);
                    database.AddInParameter(command, "@primary", SqlDbType.Structured, dtblePrimary);

                    database.AddInParameter(command, "@dtPrimaryContact", SqlDbType.Structured, dtbleProduct);
                    database.AddInParameter(command, "@strContactType", DbType.String, c.ContactType);
                    database.AddInParameter(command, "@strPhoneType", DbType.String, c.PhoneType);
                    database.AddInParameter(command, "@strAddressType", DbType.String, c.AddressType);
                    database.AddInParameter(command, "@strBillingAddressType", DbType.String, c.BillingAddressType);
                    database.AddInParameter(command, "@strCompetitorBids", DbType.String, c.CompetitorsBids);
                    database.AddInParameter(command, "@YesNo", DbType.Boolean, bitYesNo);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    //result1 = database.ExecuteNonQuery(command);
                    database.ExecuteNonQuery(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    return res;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }
        public int AddCustomer(Customer c)          
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddnewCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@firstName", DbType.String, c.firstName);
                    database.AddInParameter(command, "@lastName", DbType.String, c.lastName);
                    database.AddInParameter(command, "@CustomerAddress", DbType.String, c.CustomerAddress);
                    database.AddInParameter(command, "@State", DbType.String, c.state);
                    database.AddInParameter(command, "@City", DbType.String, c.City);
                    database.AddInParameter(command, "@ZipCode", DbType.String, c.Zipcode);
                    database.AddInParameter(command, "@EstDateSchdule", DbType.DateTime, Convert.ToDateTime(c.EstDate,JGConstant.CULTURE));
                    database.AddInParameter(command, "@EstTime", DbType.String, c.EstTime);
                    database.AddInParameter(command, "@CellPh", DbType.String, c.CellPh);
                    database.AddInParameter(command, "@HousePh", DbType.String, c.HousePh);
                    database.AddInParameter(command, "@Email", DbType.String, c.Email);
                    database.AddInParameter(command, "@Email2", DbType.String, c.Email2);
                    database.AddInParameter(command, "@Email3", DbType.String, c.Email3);
                    database.AddInParameter(command, "@CallTakenBy", DbType.String, c.CallTakenby);
                    //database.AddInParameter(command, "@Service", DbType.String,  c.Notes);
                    database.AddInParameter(command, "@AddedBy", DbType.String, c.Addedby);
                    database.AddInParameter(command, "@leadtype", DbType.String, c.Leadtype);
                    database.AddInParameter(command, "@AlternatePh", DbType.String, c.AltPh);
                    database.AddInParameter(command, "@BillingAddress", DbType.String, c.BillingAddress);
                    database.AddInParameter(command, "@BestTimetocontact", DbType.String, c.BestTimetocontact);
                    database.AddInParameter(command, "@PrimaryContact", DbType.String, c.PrimaryContact);                   
                    database.AddInParameter(command, "@ContactPreference", DbType.String, c.ContactPreference);                   
                    database.AddInParameter(command, "@Map1", DbType.String, c.Map1);
                    database.AddInParameter(command, "@Map2", DbType.String, c.Map2);
                    database.AddInParameter(command, "@status", DbType.String,c.status);
                    database.AddInParameter(command, "@ProjectManager", DbType.String, c.ProjectManager); 
                    database.AddInParameter(command, "@Isrepeated", DbType.Boolean, c.Isrepeated);
                    database.AddInParameter(command, "@missing_contacts", DbType.Int32, c.missingcontacts);
                    database.AddInParameter(command, "@ProductOfInterest", DbType.Int32, c.Productofinterest);
                    database.AddInParameter(command, "@followup_date", DbType.Date, Convert.ToDateTime(c.followupdate, JGConstant.CULTURE));
                    database.AddInParameter(command, "@SecProductOfInterest", DbType.Int32, c.SecondaryProductofinterest);
                    database.AddInParameter(command, "@strContactType", DbType.Int32, DBNull.Value);
                    database.AddInParameter(command, "@strPhoneType", DbType.Int32, DBNull.Value);
                    database.AddInParameter(command, "@strAddressType", DbType.Int32, DBNull.Value);
                    database.AddInParameter(command, "@strBillingAddressType", DbType.Int32, DBNull.Value);
                    database.AddInParameter(command, "@strCompetitorBids", DbType.Int32, DBNull.Value);
                    
                    
                   // database.AddOutParameter(command, "@result", DbType.Int32,DBNull.Value);
                    database.AddOutParameter(command, "@result", DbType.Int32,1);
                    //result1 = database.ExecuteNonQuery(command);
                    database.ExecuteNonQuery(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    return res;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }

        public int UpdateSrCustomer(Customer objcust, DataTable dtbleAddress, DataTable dtbleBillingAddress, DataTable dtblePrimary, DataTable dtbleProduct, Boolean bitYesNo)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateCustomer_new");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, objcust.id);
                    database.AddInParameter(command, "@CustomerName", DbType.String, objcust.firstName);
                    database.AddInParameter(command, "@lastName", DbType.String, objcust.lastName);
                    database.AddInParameter(command, "@CustomerAddress", DbType.String, objcust.CustomerAddress);
                    database.AddInParameter(command, "@State", DbType.String, objcust.state);
                    database.AddInParameter(command, "@City", DbType.String, objcust.City);
                    database.AddInParameter(command, "@ZipCode", DbType.String, objcust.Zipcode);
                    database.AddInParameter(command, "@EstDateSchdule", DbType.DateTime, Convert.ToDateTime(objcust.EstDate, JGConstant.CULTURE));
                    database.AddInParameter(command, "@EstTime", DbType.String, objcust.EstTime);
                    database.AddInParameter(command, "@ProductOfInterest", DbType.Int32, objcust.Productofinterest);
                    database.AddInParameter(command, "@SecProductOfInterest", DbType.Int32, objcust.SecondaryProductofinterest);
                    database.AddInParameter(command, "@ProjectManager", DbType.String, objcust.ProjectManager);
                    database.AddInParameter(command, "@CellPh", DbType.String, objcust.CellPh);
                    database.AddInParameter(command, "@HousePh", DbType.String, objcust.HousePh);
                    database.AddInParameter(command, "@Email", DbType.String, objcust.Email);
                    database.AddInParameter(command, "@Email2", DbType.String, objcust.Email2);
                    database.AddInParameter(command, "@Email3", DbType.String, objcust.Email3);
                    database.AddInParameter(command, "@CallTakenBy", DbType.String, objcust.CallTakenby);
                    // database.AddInParameter(command, "@Service", DbType.String, objcust.Notes);
                    database.AddInParameter(command, "@AddedBy", DbType.String, objcust.Addedby);
                    database.AddInParameter(command, "@leadtype", DbType.String, objcust.Leadtype);
                    database.AddInParameter(command, "@AlternatePh", DbType.String, objcust.AltPh);
                    database.AddInParameter(command, "@BillingAddress", DbType.String, objcust.BillingAddress);
                    database.AddInParameter(command, "@BestTimetocontact", DbType.String, objcust.BestTimetocontact);
                    database.AddInParameter(command, "@PrimaryContact", DbType.String, objcust.PrimaryContact);
                    database.AddInParameter(command, "@ContactPreference", DbType.String, objcust.ContactPreference);
                    database.AddInParameter(command, "@Map1", DbType.String, objcust.Map1);
                    database.AddInParameter(command, "@Map2", DbType.String, objcust.Map2);

                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.AddInParameter(command, "@followup_date", DbType.Date, null);
                    database.AddInParameter(command, "@CustAdd", SqlDbType.Structured, dtbleAddress);
                    database.AddInParameter(command, "@CustBilling", SqlDbType.Structured, dtbleBillingAddress);
                    database.AddInParameter(command, "@primary", SqlDbType.Structured, dtblePrimary);
                    database.AddInParameter(command, "@dtPrimaryContact", SqlDbType.Structured, dtbleProduct);

                    database.AddInParameter(command, "@strContactType", DbType.String, objcust.ContactType);
                    database.AddInParameter(command, "@strPhoneType", DbType.String, objcust.PhoneType);
                    database.AddInParameter(command, "@strAddressType", DbType.String, objcust.AddressType);
                    database.AddInParameter(command, "@strBillingAddressType", DbType.String, objcust.BillingAddressType);
                    database.AddInParameter(command, "@strCompetitorBids", DbType.String, objcust.CompetitorsBids);
                    database.AddInParameter(command, "@YesNo", DbType.String, bitYesNo);

                    //result1 = database.ExecuteNonQuery(command);
                    database.ExecuteNonQuery(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    return res;
                }
            }
            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }
        public void AddAnnualEvent(AnnualEvent Event)          
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("AddAnnualEvent");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Eventname", DbType.String, Event.EventName);                   
                    database.AddInParameter(command, "@EventDate", DbType.Date, Convert.ToDateTime(Event.Eventdate));
                    database.AddInParameter(command, "@EventAddedBy",DbType.Int32, Event.EventAddedBy);
                    database.ExecuteNonQuery(command);
                   
                   // return res; 
    
                }
            }

            catch
            {
               // return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }


        public void AddForemanName(AddForemanName foreman)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("InsertForemanDetails");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@ProductId", DbType.Int32, foreman.ProductId);
                    database.AddInParameter(command, "@CustomerId", DbType.Int32,foreman.CustomerId);
                    database.AddInParameter(command, "@SoldJob", DbType.String, foreman.SoldJob);
                    database.AddInParameter(command, "@ID", DbType.Int32, foreman.ID);

                    database.ExecuteNonQuery(command);

                    // return res; 

                }
            }

            catch
            {
                // return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }

        public DataSet CheckDuplicateAnnualEvent(AnnualEvent Event)
        {
            DataSet result = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("CheckDuplicateAnnualEvent");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Eventname", DbType.String, Event.EventName);
                    database.AddInParameter(command, "@EventDate", DbType.Date, Convert.ToDateTime(Event.Eventdate)); 
                    result = database.ExecuteDataSet(command);
                }
                return result;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return null;
            }
        }

        public void UpdateAnnualEvent(AnnualEvent Event)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UpdateAnnualEvent");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Eventname", DbType.String, Event.EventName);
                    database.AddInParameter(command, "@EventDate", DbType.Date, Convert.ToDateTime(Event.Eventdate));
                    database.AddInParameter(command, "@ID",DbType.Int32, Event.id);
                    database.ExecuteNonQuery(command);

                    // return res; 

                }
            }

            catch
            {
                // return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }
        public void DeleteAnnualEvent(AnnualEvent Event)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("DeleteAnnualEvent");
                    command.CommandType = CommandType.StoredProcedure;
                    
                    database.AddInParameter(command, "@ID", DbType.Int32, Event.id);
                    database.ExecuteNonQuery(command);

                    // return res; 

                }
            }

            catch
            {
                // return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }

        public int UpdateCustomer(Customer objcust) 
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, objcust.id);
                    database.AddInParameter(command, "@CustomerName", DbType.String, objcust.firstName);
                    database.AddInParameter(command, "@lastName", DbType.String, objcust.lastName);
                    database.AddInParameter(command, "@CustomerAddress", DbType.String,objcust.CustomerAddress);
                    database.AddInParameter(command, "@State", DbType.String, objcust.state);
                    database.AddInParameter(command, "@City", DbType.String, objcust.City);
                    database.AddInParameter(command, "@ZipCode", DbType.String, objcust.Zipcode);
                    database.AddInParameter(command, "@EstDateSchdule", DbType.DateTime, Convert.ToDateTime(objcust.EstDate, JGConstant.CULTURE));
                    database.AddInParameter(command, "@EstTime", DbType.String,objcust.EstTime);
                    database.AddInParameter(command, "@CellPh", DbType.String, objcust.CellPh);
                    database.AddInParameter(command, "@HousePh", DbType.String, objcust.HousePh);
                    database.AddInParameter(command, "@ProjectManager", DbType.String, objcust.ProjectManager); 
                    database.AddInParameter(command, "@Email", DbType.String,objcust.Email);
                    database.AddInParameter(command, "@Email2", DbType.String, objcust.Email2);
                    database.AddInParameter(command, "@Email3", DbType.String, objcust.Email3);
                    database.AddInParameter(command, "@CallTakenBy", DbType.String, objcust.CallTakenby);
                   // database.AddInParameter(command, "@Service", DbType.String, objcust.Notes);
                    database.AddInParameter(command, "@ProductOfInterest", DbType.Int32, objcust.Productofinterest);
                    database.AddInParameter(command, "@SecProductOfInterest", DbType.Int32, objcust.SecondaryProductofinterest);
                    database.AddInParameter(command, "@AddedBy", DbType.String,objcust.Addedby);
                    database.AddInParameter(command, "@leadtype", DbType.String, objcust.Leadtype);
                    database.AddInParameter(command, "@AlternatePh", DbType.String, objcust.AltPh);
                    database.AddInParameter(command, "@BillingAddress", DbType.String, objcust.BillingAddress);
                    database.AddInParameter(command, "@BestTimetocontact", DbType.String, objcust.BestTimetocontact);
                    database.AddInParameter(command, "@PrimaryContact", DbType.String, objcust.PrimaryContact);
                    database.AddInParameter(command, "@ContactPreference", DbType.String, objcust.ContactPreference);
                    database.AddInParameter(command, "@Map1", DbType.String, objcust.Map1);
                    database.AddInParameter(command, "@Map2", DbType.String, objcust.Map2);
                    database.AddInParameter(command, "@followup_date", DbType.Date, Convert.ToDateTime(objcust.followupdate, JGConstant.CULTURE));
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    //result1 = database.ExecuteNonQuery(command);
                    database.ExecuteNonQuery(command);
                    int res = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    return res;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            
        }
        public void UpdateCustomerFollowUpDate(DateTime followupDate,int custId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateFollowupDateOfCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@custId", DbType.Int32, custId);
                    database.AddInParameter(command, "@MeetingDate", DbType.Date, Convert.ToDateTime(followupDate, JGConstant.CULTURE));
                 
                    database.ExecuteNonQuery(command);
                }
            }

            catch
            {
               //LogManager.Instance.WriteToFlatFile(ex);
            }

        }
        public int AddCustomerFollowUp(int customerid, DateTime meetingdate, string Status, int userId, bool IsNotes, int assignuserid, string TempFileName, int pEstimateID = 0,int pProductID =0)
        {
            int result = 0;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_EntryInCustomer_followup");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@custId", DbType.Int32, customerid);
                    database.AddInParameter(command, "@MeetingDate", DbType.DateTime, meetingdate);
                    database.AddInParameter(command, "@MeetingStatus", DbType.String, Status);
                    database.AddInParameter(command, "@UserId", DbType.Int32, userId);
                    database.AddInParameter(command, "@ProductId", DbType.Int32, pProductID );
                    database.AddInParameter(command, "@EstimateId", DbType.Int32, pEstimateID);
                    database.AddInParameter(command, "@IsNotes", DbType.Boolean, IsNotes);
                    database.AddInParameter(command, "@AssignedId", DbType.Int32, assignuserid);
                    database.AddInParameter(command, "@FileName", DbType.String, TempFileName);
                    //database.AddInParameter(command, "@AssignedId", DbType.Int32, assignid);
                    //database.AddInParameter(command, "@Reason", DbType.String, Reason);
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

        /// <summary>
        /// To Check for the customer duplication in data base.
        /// </summary>
        /// <param name="dtAddress"></param>
        /// <param name="dtPhoneNumber"></param>
        /// <param name="dtEMail"></param>
        /// <returns></returns>
        public DataSet CheckCustomerDuplication(DataTable dtAddress, DataTable dtDetails, int CustomerId)
        {
            DataSet dsResult = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("Sp_Address_Validation");
                    command.CommandType = CommandType.StoredProcedure;

                    DbParameter param1 = command.CreateParameter();
                    param1.ParameterName = "@CustomerAddress";
                    param1.Value = dtAddress;
                    command.Parameters.Add(param1);

                    DbParameter param2 = command.CreateParameter();
                    param2.ParameterName = "@CustomersPrimaryContact";
                    param2.Value = dtDetails;
                    command.Parameters.Add(param2);

                    DbParameter param3 = command.CreateParameter();
                    param3.ParameterName = "@customerid";
                    param3.Value = CustomerId;
                    command.Parameters.Add(param3);

                    dsResult = database.ExecuteDataSet(command);
                    return dsResult;
                }
            }
            catch
            {
                return dsResult;
            }
        }
        public DataSet GetAutoSuggestiveCustomers(string prefix)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAutoSuggestiveCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@prefix", DbType.String, prefix);
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

        public DataSet GetCustomerForDropDown()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerNames");
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


        public DataSet GetUsersForDropDown()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetUsers");
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

        
        public DataSet GetHRCount(string SourceId,string fromDate,string ToDate)
        {
            returndata = new DataSet();
            try
            {
 
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GETHRCounts");
                    database.AddInParameter(command, "@SourceUser", DbType.String, SourceId);
                    database.AddInParameter(command, "@DateSourced1", DbType.String, fromDate);
                    database.AddInParameter(command, "@DateSourced2", DbType.String, ToDate);
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


        public DataSet GetRejected(string SourceId, string fromDate, string ToDate)
        {
            returndata = new DataSet();
            try
            {

                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetRejected");
                    database.AddInParameter(command, "@SourceUser", DbType.String, SourceId);
                    database.AddInParameter(command, "@DateSourced1", DbType.String, fromDate);
                    database.AddInParameter(command, "@DateSourced2", DbType.String, ToDate);
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


        public DataSet GetProductAndEstimateIdOfSoldJob(string soldJobId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetProductAndEstimateIdOfSoldJob");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
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

        public DataSet GetCustIdAndName(string soldJobId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetCustNameId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
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

        public DataSet GetCustName(int Id)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetCustomerName");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.String, Id);
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

       
        public bool UpdateStatusOfCustomer(string soldJobId, int statusId)//, int productTypeId, int estimateId)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateStatusOfCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    database.AddInParameter(command, "@statusId", DbType.Int32, statusId);
                   // database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                   // database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return false;
            }
        }
        public DataSet GetTouchPointLogData(int CustomerId, int userid)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchTouchPointLogData");
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

        public bool UpdateEmailOfCustomer(int customerID,string email)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateEmailOfCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Email", DbType.String, email);
                    database.AddInParameter(command, "@Id", DbType.Int32, customerID);

                    database.ExecuteNonQuery(command);
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return false;
            }
        }
        public DataSet SearchCustomers(string searchparameter, string searchstring, string user)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_SearchCustomer");//UDP_SearchProspect
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@searchparameter", DbType.String, searchparameter);
                    database.AddInParameter(command, "@searchstring", DbType.String, searchstring);
                    database.AddInParameter(command, "@user", DbType.String, user);
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


        public DataSet SearchProspect(string searchparameter, string searchstring, string user)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_SearchProspect");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@searchparameter", DbType.String, searchparameter);
                    database.AddInParameter(command, "@searchstring", DbType.String, searchstring);
                    database.AddInParameter(command, "@user", DbType.String, user);
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

        public bool DeleteCustomerDetails(int Id)
        {
            bool result;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteCustomerDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, Id);
                    database.AddOutParameter(command, "@result", DbType.Boolean, 1);
                    database.ExecuteNonQuery(command);
                    result = (bool)database.GetParameterValue(command, "@result");
                }
            }

            catch (Exception ex)
            {
                result = false;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return result;
        }

        public DataSet GetProposalTermById(int ProductTypeId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetProposalTerms");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ProductTypeId", DbType.Int32, ProductTypeId);
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



        public DataSet GetCustomerDetails(int Id)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, Id);
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

        public DataSet GetCustomerStatusHistory(int CustomerId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchCustomerStatusHistory");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerId", DbType.Int32, CustomerId);
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
        public DataSet GetCustomerFollowUpDetails(int Id)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerFollowUpDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, Id);
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

        public DataSet GetTouchPointLogData(int CustomerId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchTouchPointLogData");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerId", DbType.Int32, CustomerId);
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

        public DataSet GetCustomerDocsDetails(int customerId,int productId,int productTypeId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_getcustomerdocuments");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerId", DbType.Int32, customerId);
                    database.AddInParameter(command, "@productId", DbType.Int32, productId);
                    database.AddInParameter(command, "@productTypeId", DbType.Int32, productTypeId);
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
        public DataSet GetCustomerJobPackets(string soldJobId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_getCustomerJobPackets");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@SoldJobId", DbType.String, soldJobId);
                    //database.AddInParameter(command, "@customerId", DbType.Int32, customerId);
                    //database.AddInParameter(command, "@productId", DbType.Int32, productId);
                    //database.AddInParameter(command, "@productTypeId", DbType.Int32, productTypeId);
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

        public DataSet GetCustomerJobPackets(int customerId,int productId,int productTypeId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_getCustomerJobPackets");
                    command.CommandType = CommandType.StoredProcedure;
                    //database.AddInParameter(command, "@SoldJobId", DbType.String, soldJobId);
                    database.AddInParameter(command, "@customerId", DbType.Int32, customerId);
                    database.AddInParameter(command, "@productId", DbType.Int32, productId);
                    database.AddInParameter(command, "@productTypeId", DbType.Int32, productTypeId);
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
       
        public DataSet GetSoldjobsforprocurement()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetSoldjobsforprocurement");
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
        public DataSet GetSoldJobsForProcurementBySoldJobId(string soldJobId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetSoldJobsForProcurementBySoldJobID");
                    database.AddInParameter(command, "@SoldJobId", DbType.String, soldJobId);
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

        public DataSet FetchCustomerCallSheet(string status, string user, string usertyp)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CallSheetforSr");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@status", DbType.String, status);
                    database.AddInParameter(command, "@user", DbType.String, user);
                    database.AddInParameter(command, "@usertype", DbType.String, usertyp);
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


        public bool AddCustomerDocs(int customerid, int productid, string originalFileName, string documenttype, string temporaryFileName, int productTypeId,int vendorId)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddCustomerDocs");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerid", DbType.Int32, customerid);
                    database.AddInParameter(command, "@productid", DbType.Int32, productid);
                    database.AddInParameter(command, "@OriginalFileName", DbType.String, originalFileName);
                    database.AddInParameter(command, "@documenttype", DbType.String, documenttype);
                    database.AddInParameter(command, "@tempFileName", DbType.String, temporaryFileName);
                    database.AddInParameter(command, "@productTypeId", DbType.Int32, productTypeId);
                    database.AddInParameter(command, "@vendorId", DbType.Int32, vendorId);

                    database.ExecuteNonQuery(command);
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return false;
            }
        }

      
        //public bool AddCustomerDocs(int customerid, int productid, string originalFileName, string documenttype, string temporaryFileName)// int productTypeId)
        //{

        //    try
        //    {
        //        SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
        //        {
        //            DbCommand command = database.GetStoredProcCommand("UDP_AddCustomerDocs");
        //            command.CommandType = CommandType.StoredProcedure;
        //            database.AddInParameter(command, "@customerid", DbType.Int32, customerid);
        //            database.AddInParameter(command, "@productid", DbType.Int32, productid);
        //            database.AddInParameter(command, "@OriginalFileName", DbType.String, originalFileName);
        //            database.AddInParameter(command, "@documenttype", DbType.String, documenttype);
        //            database.AddInParameter(command, "@tempFileName", DbType.String, temporaryFileName);
        //           // database.AddInParameter(command, "@productTypeId", DbType.Int32, productTypeId);

        //            database.ExecuteNonQuery(command);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //LogManager.Instance.WriteToFlatFile(ex);
        //        return false;
        //    }
        //}

        public bool AddCustomerLocationPics(int customerid, string PictureNm)
        {          
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddCustomerLocationPics");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, customerid);
                    database.AddInParameter(command, "@PictureNm", DbType.String, PictureNm);                 
                    database.ExecuteNonQuery(command);
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return false;
            }
        }

        public int GETCustomerId(int prospectid)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GETCustomerId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@prospectid", DbType.Int32, prospectid);
                    return Convert.ToInt32(database.ExecuteScalar(command));
                }               
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return 0;
            }
        }


        public int SearchCustomerId(string cellph)
        {
            try
            {
                returndata = new DataSet();
                int res = 0;
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_SearchCustId");
                    command.CommandType = CommandType.StoredProcedure;  
                    database.AddInParameter(command, "@cellPh", DbType.String, cellph);              
                    returndata =  database.ExecuteDataSet(command);
                    if (returndata != null)
                    {
                        res = Convert.ToInt32(returndata.Tables[0].Rows[0][0].ToString());
                    }

                    return res;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
        public DataSet GetCustomerLocationPics(int customerId, int productId, int productTypeId)
        {
            DataSet returndata = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerLocationPic");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", SqlDbType.Int, customerId);
                    database.AddInParameter(command, "@ProductId", SqlDbType.Int, productId);
                    database.AddInParameter(command, "@ProductTypeId", SqlDbType.Int, productTypeId);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCustomerWorkOrders(int customerId, int productId, int productTypeId)
        {
            DataSet returndata = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerWorkOrders");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", SqlDbType.Int, customerId);
                    database.AddInParameter(command, "@ProductId", SqlDbType.Int, productId);
                    database.AddInParameter(command, "@ProductTypeId", SqlDbType.Int, productTypeId);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetCustomerFinalQuotes(int customerId, int productId, int productTypeId)
        {
            DataSet returndata = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerFinalQuotes");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", SqlDbType.Int, customerId);
                    database.AddInParameter(command, "@ProductId", SqlDbType.Int, productId);
                    database.AddInParameter(command, "@ProductTypeId", SqlDbType.Int, productTypeId);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCustomerProfileProducts(int CustId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerProfileProducts");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, CustId);
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

        public DataSet FetchAllStatus()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchAllStatus");
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

        public DataSet GetAllStatus()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetStatusMaster");
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
        public DataSet GetJobTeamMembers(int intCustomerId, string strJobId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_GetJobTeamMembers");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@intCustomerId", DbType.Int32, intCustomerId);
                    database.AddInParameter(command, "@strSoldJobId", DbType.String, strJobId);
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
    
    }
}
