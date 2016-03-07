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
using System.Configuration;
using System.Data.SqlClient;
using JG_Prospect.Common;
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

        public DataSet GetMaterialListData(string soldJobId, int CustomerId)
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
                    database.AddInParameter(command, "@VendorSubCategoryId", DbType.Int16, objvendor.vendor_subcategory_id);
                    database.AddInParameter(command, "@VendorStatus", DbType.String, objvendor.VendorStatus);
                    database.AddInParameter(command, "@Website", DbType.String, objvendor.Website);
                    database.AddInParameter(command, "@ContactExten", DbType.String, objvendor.ContactExten);

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

        public DataSet GetVendorList(string FilterParams, string FilterBy, string ManufacturerType, string VendorCategoryId)
        {
            try
            {
                {
                    SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("USP_GetVendorList");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@FilterParams", DbType.String, FilterParams);
                    database.AddInParameter(command, "@FilterBy", DbType.String, FilterBy);
                    database.AddInParameter(command, "@ManufacturerType", DbType.String, ManufacturerType);
                    database.AddInParameter(command, "@VendorCategoryId", DbType.String, VendorCategoryId);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string SaveNewVendorCategory(NewVendorCategory objNewVendorCat)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("sp_newVendorCategory");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendorCatName", DbType.String, objNewVendorCat.VendorName);
                    database.AddInParameter(command, "@action", DbType.Int16, 1);
                    object VendorId = database.ExecuteScalar(command);
                    return VendorId.ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public bool SaveNewVendorProduct(NewVendorCategory objNewVendorCat)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("sp_newVendorCategory");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@vendorCatId", DbType.String, objNewVendorCat.VendorId);
                    database.AddInParameter(command, "@vendorCatName", DbType.String, objNewVendorCat.VendorName);
                    database.AddInParameter(command, "@productCatId", DbType.String, objNewVendorCat.ProductId);
                    database.AddInParameter(command, "@productCatName", DbType.String, objNewVendorCat.ProductName);
                    database.AddInParameter(command, "@action", DbType.Int16, 2);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SaveNewVendorSubCat(VendorSubCategory objVendorSubCat)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("sp_VendorSubCat");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@VendorCategoryId", DbType.String, objVendorSubCat.VendorCatId);
                    database.AddInParameter(command, "@VendorSubCategoryName", DbType.String, objVendorSubCat.Name);
                    database.AddInParameter(command, "@action", DbType.Int16, 1);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public bool DeleteVendorSubCat(VendorSubCategory objVendorSubCat)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("sp_VendorSubCat");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@VendorSubCategoryId", DbType.String, objVendorSubCat.Id);
                    database.AddInParameter(command, "@action", DbType.Int16, 2);
                    database.ExecuteNonQuery(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet GetVendorSubCategory()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                DS = new DataSet();
                DbCommand command = database.GetStoredProcCommand("sp_VendorSubCat");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@action", DbType.Int16, 3);
                DS = database.ExecuteDataSet(command);
                return DS;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool InsertVendorEmail(Vendor objVendor)
        {
            try
            {
                string consString = ConfigurationManager.ConnectionStrings[DBConstants.CONFIG_CONNECTION_STRING_KEY].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_VendorEmail"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblVendorEmail", objVendor.tblVendorEmail);
                        cmd.Parameters.AddWithValue("@action", 1);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
                //SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                //{
                //    DbCommand command = database.GetStoredProcCommand("sp_VendorEmail");
                //    command.CommandType = CommandType.StoredProcedure;
                //    database.AddInParameter(command, "@tblVendorEmail", DbType.Object, objVendor.tblVendorEmail);
                //    database.AddInParameter(command, "@action", DbType.Int16, 1);
                //    database.ExecuteNonQuery(command);
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertVendorAddress(Vendor objVendor)
        {
            try
            {
                string consString = ConfigurationManager.ConnectionStrings[DBConstants.CONFIG_CONNECTION_STRING_KEY].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_VendorAddress"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tblVendorAddress", objVendor.tblVendorAddress);
                        cmd.Parameters.AddWithValue("@action", 1);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
