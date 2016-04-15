using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using JG_Prospect.DAL.Database;
using System.Data;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;


namespace JG_Prospect.DAL
{
    public class CustomDAL
    {
        private static CustomDAL m_CustomDAL = new CustomDAL();

        private CustomDAL()
        {

        }

        public static CustomDAL Instance
        {
            get { return m_CustomDAL; }
            private set { ;}
        }

        public bool AddCustom(Customs custom)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddCustom");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@Id", DbType.Int32, custom.Id);
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, custom.CustomerId);
                    database.AddInParameter(command, "@UserId", DbType.Int32, custom.UserId );
                    database.AddInParameter(command, "@Customer", DbType.String, custom.Customer);
                    database.AddInParameter(command, "@WorkArea", DbType.String, custom.WorkArea);
                    database.AddInParameter(command, "@ProposalTerms", DbType.String, custom.ProposalTerms);
                    database.AddInParameter(command, "@ProposalCost", DbType.Decimal, custom.ProposalCost);
                    database.AddInParameter(command, "@Attachment", DbType.String, custom.Attachment);
                    database.AddInParameter(command, "@SpecialInstruction", DbType.String, custom.SpecialInstruction);
                    database.AddInParameter(command, "@LocationImg", DbType.String, custom.LocationImage);
                    database.AddInParameter(command, "@MainImage", DbType.String, custom.MainImage);
                    database.AddInParameter(command, "@ProductTypeId", DbType.String, custom.ProductTypeId);
                    database.AddInParameter(command, "@CustomerSuppliedMaterial", DbType.String, custom.CustSuppliedMaterial);
                    database.AddInParameter(command, "@IsCustSupMatApplicable", DbType.Boolean, custom.IsCustSupMatApplicable);
                    database.AddInParameter(command, "@MaterialStorage", DbType.String, custom.MaterialStorage);
                    database.AddInParameter(command, "@IsMatStorageApplicable", DbType.Boolean, custom.IsMatStorageApplicable);
                    database.AddInParameter(command, "@IsPermitRequired", DbType.Boolean, custom.IsPermitRequired);
                    database.AddInParameter(command, "@IsHabitat", DbType.Boolean, custom.IsHabitat);
                    database.AddInParameter(command, "@Others", DbType.String, custom.Others);

                    result = database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        public bool AddCustomMaterialList(CustomMaterialList cm, string jobid) //, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_Add_Custom_MaterialList");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@materialList", DbType.String, cm.MaterialList);
                    database.AddInParameter(command, "@vendorCategoryId", DbType.Int16, cm.VendorCategoryId);
                    database.AddInParameter(command, "@vendorId", DbType.Int16, cm.VendorId);
                    database.AddInParameter(command, "@amount", DbType.Decimal, cm.Amount);
                    database.AddInParameter(command, "@foremanPermission", DbType.String, cm.IsForemanPermission);
                    database.AddInParameter(command, "@salesmanPermissionF", DbType.String, cm.IsSrSalemanPermissionF);
                    database.AddInParameter(command, "@adminPermission", DbType.String, cm.IsAdminPermission);
                    database.AddInParameter(command, "@salesmanPermissionA", DbType.String, cm.IsSrSalemanPermissionA);
                    database.AddInParameter(command, "@emailStatus", DbType.String, cm.EmailStatus);


                    database.AddInParameter(command, "@ProductCatId", DbType.Int32, cm.ProductCatId);
                    database.AddInParameter(command, "@Line", DbType.String, cm.Line);
                    database.AddInParameter(command, "@JGSkuPartNo", DbType.String, cm.JGSkuPartNo);
                    database.AddInParameter(command, "@Description", DbType.String, cm.Description);
                    database.AddInParameter(command, "@Quantity", DbType.Int32, Convert.ToInt32(cm.Quantity));
                    database.AddInParameter(command, "@UOM", DbType.String, cm.UOM);
                    //database.AddInParameter(command, "@VendorQuotesPath"	varchar(MAX) = '',
                    database.AddInParameter(command, "@MaterialCost", DbType.Decimal, cm.MaterialCost);
                    database.AddInParameter(command, "@extend", DbType.String, cm.extend);
                    database.AddInParameter(command, "@Total", DbType.Decimal, cm.Total);
                    database.AddInParameter(command, "@JobSeqId", DbType.Int32, cm.JobSeqId);
                    database.AddInParameter(command, "@VendorIds", DbType.String, cm.VendorIds);
                    database.AddInParameter(command, "@Visible", DbType.String, cm.DisplaDLL);
                    database.AddInParameter(command, "@ID", DbType.String, cm.Id);
                    database.AddInParameter(command, "@InstallerID", DbType.String, cm.InstallerID);
                    database.AddInParameter(command, "@RequestStatus", DbType.String, cm.RequestStatus);


                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);
                    result = database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        public void UpdateProductTypeInMaterialList(int pProdCatID, int pOldProdCatID, string pSoldJobID) //, int productTypeId, int estimateId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_UpdateProdCatOfCustomMaterial");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ProdCatID", DbType.Int32, pProdCatID);
                    database.AddInParameter(command, "@OldProdCatID", DbType.Int32, pOldProdCatID);
                    database.AddInParameter(command, "@SoldJobID", DbType.String, pSoldJobID);
                    database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteCustomMaterialList(int pID) //, int productTypeId, int estimateId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteCustomMaterial");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ID", DbType.String, pID);
                    database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCustomMaterialListByProductCatID(int pProdCatID) //, int productTypeId, int estimateId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_DeleteCustomMaterialByProdCat");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ProdCatID", DbType.String, pProdCatID);
                    database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCustomMaterialList(string id)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteCustom_MaterialList");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, id);
                   // database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                   // database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    result = database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        public bool DeleteWorkorders(string soldJobId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteWorkorders");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    result = database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        public bool UpdateEmailStatusOfCustomMaterialList(string jobid, string emailStatus)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateEmailStatusOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@emailStatus", DbType.String, emailStatus);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    result = database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        public int UpdateSrSalesmanPermissionOfCustomMaterialList(string jobid, char permissionStatus, string SrSalesEmail)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateSrSalesmanPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@SrSalesEmail", DbType.String, SrSalesEmail);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16 (database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result ;
        }

        public int UpdateSrSalesmanPermissionOfCustomMaterialListF(string jobid, char permissionStatus, string SrSalemanAEmail)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateSrSalesmanPermissionOfCustomMaterialListF");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@SrSalemanAEmail", DbType.String, SrSalemanAEmail);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int UpdateAdminPermissionOfCustomMaterialList(string jobid, char permissionStatus, string AdminEmail)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateAdminPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@AdminEmail", DbType.String, AdminEmail);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int UpdateForemanPermissionOfCustomMaterialList(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateForemanPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@UpdatedBy", DbType.Int32, updatedby);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    
                    
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int UpdateForemanPermissionOfCustomMaterialList2(string jobid, char permissionStatus, string FormanEmail)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateForemanPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@FormanEmail", DbType.String, FormanEmail);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int WhetherCustomMaterialListExists(string jobid)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_WhetherCustomMaterialListExists");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int WhetherVendorInCustomMaterialListExists(string jobid)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_WhetherVendorInCustomMaterialListExists");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int CheckPermissionsForVendors(string jobid)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CheckPermissionsForVendors");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                   // database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                  //  database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int CheckPermissionsForCategories(string jobid)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CheckPermissionsForCategories");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int CheckPermissionsForCategories(string jobid, int pProductCatID)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CheckPermissionsForCategoriesOfProductCat");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@productCatId", DbType.Int32, pProductCatID);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int WhetherVendorQuotesExists(string soldJobId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_WhetherVendorQuotesExists");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, soldJobId);
                    database .AddOutParameter (command ,"@result",DbType.Int16 ,result);
                   
                    database.ExecuteNonQuery(command);
                    
                    result=Convert.ToInt16 (database .GetParameterValue (command ,"@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result ;//> JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        #region Password is taken from tblInstallUsers table....
        public DataSet GetForeManEmail(string jobid)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetForManEmailId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetFormanNameAndID(int ID)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("GetForemanNameAndID");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ID", DbType.Int32,ID);
                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        #endregion

        public DataSet GetAdminEmail(string jobid)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetAdminEmailId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetSrSalesFEmail(string jobid)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetSrSalesEmailId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetSrSalesAEmail(string jobid)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetSrSalesAEmailId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetAllPermissionOfCustomMaterialList(string jobid)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);

                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        public DataSet GetEmailStatusOfCustomMaterialList(string jobid)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetEmailStatusOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);

                    ds = database.ExecuteDataSet(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        //public bool DeleteCustomMaterialList(int vendorCategory, string jobid, int productTypeId, int estimateId)
        //{
        //    int result = JGConstant.RETURN_ZERO;
        //    try
        //    {
        //        SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
        //        {
        //            DbCommand command = database.GetStoredProcCommand("UDP_Delete_Custom_MaterialList");
        //            command.CommandType = CommandType.StoredProcedure;

        //            database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
        //            database.AddInParameter(command, "@vendorCategoryId", DbType.Int16, vendorCategory);
        //            database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
        //            database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

        //            result = database.ExecuteNonQuery(command);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        //}
        public DataSet GetCustom_MaterialList(string jobId, int pCustomerID)//, int productTypeId, int estimateId)
        {
            DataSet returndata = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustom_MaterialList");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@soldJobId", DbType.String, jobId);
                    database.AddInParameter(command, "@customerID", DbType.String, pCustomerID);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCustomMaterialList(string jobId, int pCustomerID)
        {
            DataSet lListOfCustomMaterial = new DataSet();
            SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {

                DbCommand command = database.GetStoredProcCommand("USP_GetCustomMaterialList");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@soldJobId", DbType.String, jobId);
                database.AddInParameter(command, "@customerID", DbType.String, pCustomerID);
                lListOfCustomMaterial = database.ExecuteDataSet(command);
            }
            return lListOfCustomMaterial;
        }

        public DataSet GetRequestMaterialList(string jobId, int pCustomerID, int pInstallerID)
        {
            DataSet lListOfCustomMaterial = new DataSet();
            SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {

                DbCommand command = database.GetStoredProcCommand("USP_GetRequestMaterialList");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@soldJobId", DbType.String, jobId);
                database.AddInParameter(command, "@customerID", DbType.String, pCustomerID);
                database.AddInParameter(command, "@InstallerID", DbType.String, pInstallerID);
                
                lListOfCustomMaterial = database.ExecuteDataSet(command);
            }
            return lListOfCustomMaterial;
        }
       
        public Customs GetCustomDetail(Customs custom)
        {
            DataSet returndata = null;
            List<CustomerLocationPic> customerLocationPics = new List<CustomerLocationPic>();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetLocationPics");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", SqlDbType.Int, custom.CustomerId);
                    database.AddInParameter(command, "@ProductId", SqlDbType.Int, custom.Id);
                    database.AddInParameter(command, "@ProductTypeId", SqlDbType.Int, custom.ProductTypeId);

                    returndata = database.ExecuteDataSet(command);
                    var varCustom = from row in returndata.Tables[0].AsEnumerable()
                                    select new Customs
                                    {
                                        Id = custom.Id,
                                        CustomerId = custom.CustomerId,
                                        ProductTypeId = custom.ProductTypeId,
                                        Attachment = row["Attachment"].ToString(),
                                        Customer = row["Customer"].ToString(),
                                        ProposalCost = decimal.Parse(row["ProposalCost"].ToString()),
                                        ProposalTerms = row["ProposalTerms"].ToString(),
                                        SpecialInstruction = row["SpecialInstruction"].ToString(),
                                        WorkArea = row["WorkArea"].ToString(),
                                        CustSuppliedMaterial = row["CustSuppliedMaterial"].ToString(),
                                        IsCustSupMatApplicable = row["IsCustSupMatApplicable"].ToString() == "" ? false : Convert.ToBoolean(row["IsCustSupMatApplicable"].ToString()),
                                        MaterialStorage = row["MaterialStorage"].ToString(),
                                        IsMatStorageApplicable =  row["IsMatStorageApplicable"].ToString() == "" ? false : Convert.ToBoolean( row["IsMatStorageApplicable"].ToString()),
                                        IsPermitRequired = row["IsPermitRequired"].ToString() == "" ? false : Convert.ToBoolean(row["IsPermitRequired"].ToString()),
                                        IsHabitat = row["IsHabitat"].ToString() == "" ? false : Convert.ToBoolean(row["IsHabitat"].ToString()),
                                        CustomerLocationPics = returndata.Tables[1].AsEnumerable().
                                        Select(aa => new CustomerLocationPic
                                        {
                                            LocationPicture = aa["LocationPicture"].ToString(),
                                            RowSerialNo = Convert.ToInt32(aa["RowSerialNo"].ToString())
                                        }).ToList()
                                    };

                    return varCustom.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet getCustomerLocationPic(Customs custom)
        {
            DataSet returndata = null;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetCustomerLocationPic");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", SqlDbType.Int, custom.CustomerId);
                    database.AddInParameter(command, "@ProductId", SqlDbType.Int, custom.Id);
                    database.AddInParameter(command, "@ProductTypeId", SqlDbType.Int, custom.ProductTypeId);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateSrSalesmanPermissionOfCustomMaterialListF(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateSrSalesmanPermissionOfCustomMaterialListF");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@UpdatedBy", DbType.Int32, updatedby);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int UpdateAdminPermissionOfCustomMaterialList(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateAdminPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@UpdatedBy", DbType.String, updatedby);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public int UpdateSrSalesmanPermissionOfCustomMaterialList(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateSrSalesmanPermissionOfCustomMaterialList");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@soldJobId", DbType.String, jobid);
                    database.AddInParameter(command, "@permissionStatus", DbType.String, permissionStatus);
                    database.AddInParameter(command, "@UpdatedBy", DbType.String, updatedby);
                    database.AddOutParameter(command, "@result", DbType.Int16, result);
                    //database.AddInParameter(command, "@productId", DbType.Int16, productTypeId);
                    //database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt16(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public bool AddInstallerToMaterialList(String pSoldJobID, Int32 pInstallerID)
        {
            Int32 lRecordExisted = 0;
            SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {
                DbCommand command = database.GetStoredProcCommand("usp_AddInstallerToCustomMaterial");
                command.CommandType = CommandType.StoredProcedure;

                database.AddInParameter(command, "@InstallerID", DbType.Int32, pInstallerID);
                database.AddInParameter(command, "@SoldJobID", DbType.String, pSoldJobID);
                database.AddOutParameter(command, "@RecordExisted", DbType.Int32, lRecordExisted);
                database.ExecuteNonQuery(command);
            }
            return (lRecordExisted != 1);
        }

        public Int32 UpdateInstallerPrmToMaterialList(String pSoldJobID, Int32 pInstallerID, String pInstallerPwd)
        {
            Int16 lPasswordCrt = -1;
            SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {
                DbCommand command = database.GetStoredProcCommand("usp_approveInstallerRequest");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@InstallerID", DbType.Int32, pInstallerID);
                database.AddInParameter(command, "@SoldJobID", DbType.String, pSoldJobID);
                database.AddInParameter(command, "@InstallerPwd", DbType.String, pInstallerPwd);
                database.AddOutParameter(command, "@PasswordCrt", DbType.Int32, lPasswordCrt);
                database.ExecuteNonQuery(command);
                lPasswordCrt = Convert.ToInt16(database.GetParameterValue(command, "@PasswordCrt"));
            }
            return lPasswordCrt;
        }

        public void RemoveInstallerFromMaterialList(Int32 ID)
        {
            SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {
                DbCommand command = database.GetStoredProcCommand("usp_DeleteInstallerFromMaterialList");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@ID", DbType.Int32, ID);
                database.ExecuteNonQuery(command);
            }
        }
        public void UpdateVendorIDs(String pVendorIDs, Int32 pProductCatID, String pExcludedMaterialListID, String pSoldJobID)
        {
            SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {
                DbCommand command = database.GetStoredProcCommand("USP_SaveVendorIDs");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@ExcMaterialListId", DbType.String, pExcludedMaterialListID);
                database.AddInParameter(command, "@ProductCatID", DbType.Int32, pProductCatID);
                database.AddInParameter(command, "@VendorIDs", DbType.String, pVendorIDs);
                database.AddInParameter(command, "@SoldJobID", DbType.String, pSoldJobID);
                database.ExecuteNonQuery(command);
            }
        }
        public void UpdateVendorIDForSpecMaterial(String pVendorIDs, Int32 pMaterialListID)
        {
             SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
            {
                DbCommand command = database.GetStoredProcCommand("usp_SaveVendorsForSpecMaterial");
                command.CommandType = CommandType.StoredProcedure;
                database.AddInParameter(command, "@ID", DbType.String, pMaterialListID);
                database.AddInParameter(command, "@VendorIDs", DbType.String, pVendorIDs);
                database.ExecuteNonQuery(command);
            }
        }
    }
}


            