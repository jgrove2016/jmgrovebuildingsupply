using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using JG_Prospect.DAL.Database;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using JG_Prospect.Common.modal;
namespace JG_Prospect.DAL
{
    public class VendorDAL
    {
        private static VendorDAL m_VendorDAL = new VendorDAL();
        private VendorDAL()
        {
        }
        public static VendorDAL Instance
        {
            get { return m_VendorDAL; }
            set { ;}
        }
        private DataSet DS = new DataSet();
        public DataSet fetchallvendorDetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchallvendorDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchAllVendorCategoryHavingVendors()
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchAllVendorCategoryHavingVendors");
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchallvendorcategory()
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchAllVendorCategory");
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet fetchVendorNamesByVendorCategory(int vendorcategoryId)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchVendorNamesByVendorCategory");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendorcategoryId", DbType.Int32, vendorcategoryId);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AddVendorQuotes(string soldJobId, string originalFileName, string temporaryFileName, int vendorId)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddVendorQuotes");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    database.AddInParameter(command, "@OriginalFileName", DbType.String, originalFileName);
                    database.AddInParameter(command, "@tempFileName", DbType.String, temporaryFileName);
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
        public void RemoveAttachedQuote(string fileName)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_RemoveAttachedQuotes");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@tempName", DbType.String, fileName);
                    database.ExecuteNonQuery(command);
                }

            }
            catch (Exception ex)
            {

            }

        }
        public DataSet GetVendorQuoteByVendorId(string soldJobId, int vendorId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetVendorQuotesByVendorId");
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    //database.AddInParameter(command, "@customerId", DbType.Int16, customerId);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productId);
                    //database.AddInParameter(command, "@productTypeId", DbType.Int16, productTypeId);
                    database.AddInParameter(command, "@vendorId", DbType.Int16, vendorId);
                    command.CommandType = CommandType.StoredProcedure;
                    ds = database.ExecuteDataSet(command);
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return ds;
        }
        public bool UpdateAttachedQuote(string soldJobId, string originalFileName, string temporaryFileName, int vendorId)
        {

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateAttachedQuote");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    //database.AddInParameter(command, "@customerid", DbType.Int32, customerid);
                    //database.AddInParameter(command, "@estimateId", DbType.Int32, productid);
                    database.AddInParameter(command, "@docName", DbType.String, originalFileName);
                    database.AddInParameter(command, "@tempName", DbType.String, temporaryFileName);
                    //database.AddInParameter(command, "@productTypeId", DbType.Int32, productTypeId);
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

        public DataSet GetMaterialListData(string soldJobId,int CustomerId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetProductCategoryByCustIdandSoldId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.String, CustomerId);
                    database.AddInParameter(command, "@SoldJobId", DbType.String, soldJobId);
                    //database.AddInParameter(command, "@customerId", DbType.Int16, customerId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);
                    //database.AddInParameter(command, "@productTypeId", DbType.Int16, productTypeId);

                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }



        public DataSet GetAllAttachedQuotes(string soldJobId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchAttachedQuotes");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    //database.AddInParameter(command, "@customerId", DbType.Int16, customerId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);
                    //database.AddInParameter(command, "@productTypeId", DbType.Int16, productTypeId);

                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetMaterialListNew(string soldJobId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetMaterialList");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }


        public DataSet fetchVendorDetailsByVendorId(int vendorId)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchVendorDetailsByVendorId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendorId", DbType.Int32, vendorId);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet fetchMaterialListForEmail(string vendorCategory)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetMaterialListForEmail");
                    database.AddInParameter(command, "@vendorCategory", DbType.String, vendorCategory);
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet getVendorDetails(string VendorIds)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_GetVendorDetails");
                    database.AddInParameter(command, "@VendorIds", DbType.String, VendorIds);
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataSet getVendorEmailId(string vendorName)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetVendorEmailId");
                    database.AddInParameter(command, "@vendorName", DbType.String, vendorName);
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchVendorListByCategoryForEmail(int category)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchAllVendorsByCategory");
                    database.AddInParameter(command, "@vendorCategory", DbType.Int16, category);
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool deletevendor(int vendorid)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_deletevendor");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendor_id", DbType.Int32, vendorid);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool deletevendorcategory(int vendorcategoryid)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_deletevendorcategory");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendorcategory_id", DbType.Int32, vendorcategoryid);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool savevendor(Vendor objvendor)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UPP_savevendor");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendor_id", DbType.Int32, objvendor.vendor_id);
                    database.AddInParameter(command, "@vendor_name", DbType.String, objvendor.vendor_name);
                    database.AddInParameter(command, "@vendor_category", DbType.Int32, objvendor.vendor_category_id);
                    database.AddInParameter(command, "@contact_person", DbType.String, objvendor.contract_person);
                    database.AddInParameter(command, "@contact_number", DbType.String, objvendor.contract_number);
                    database.AddInParameter(command, "@fax", DbType.String, objvendor.fax);
                    database.AddInParameter(command, "@email", DbType.String, objvendor.mail);
                    database.AddInParameter(command, "@address", DbType.String, objvendor.address);
                    database.AddInParameter(command, "@notes", DbType.String, objvendor.notes);
                    database.AddInParameter(command, "@ManufacturerType", DbType.String, objvendor.ManufacturerType);
                    database.AddInParameter(command, "@BillingAddress", DbType.String, objvendor.BillingAddress);
                    database.AddInParameter(command, "@TaxId", DbType.String, objvendor.TaxId);
                    database.AddInParameter(command, "@ExpenseCategory", DbType.String, objvendor.ExpenseCategory);
                    database.AddInParameter(command, "@AutoTruckInsurance", DbType.String, objvendor.AutoTruckInsurance);

                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataSet FetchvendorDetails(int vendorid)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchvendorDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendor_id", DbType.Int32, vendorid);
                    DS = database.ExecuteDataSet(command);

                    return DS;
                }
            }

            catch (Exception ex)
            {
                return null;
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }
        public bool savevendorcatalogdetails(Vendor_Catalog objcatalog)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UPP_savevendorcatalogdetails");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@catalog_name", DbType.String, objcatalog.catalog_name);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public DataSet GetAllvendorDetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchAllVendors");
                    command.CommandType = CommandType.StoredProcedure;
                    DS = database.ExecuteDataSet(command);
                    return DS;
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
