using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using JG_Prospect.DAL.Database;
using System.Data.Common;

using JG_Prospect.Common.modal;
using JG_Prospect.Common;

namespace JG_Prospect.DAL
{
    public class shuttersDAL
    {
        private static shuttersDAL m_shuttersDAL = new shuttersDAL();
        private DataSet returndata;

        private shuttersDAL()
        {

        }

        public static shuttersDAL Instance
        {
            get { return m_shuttersDAL; }
            private set { ;}
        }

        public bool DeleteShutterDetails(int estimateId)
        {
            bool result;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_DeleteShutterDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@EstimateId", DbType.Int32, estimateId);
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

        public DataSet GetShutters(string Email, string AddedBy)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetSutters");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Email", DbType.String, Email);
                    database.AddInParameter(command, "@AddedBy", DbType.String, AddedBy);
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

        public DataSet GetShutterDetail(string orderno, string Email, string AddedBy)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetShutterDetail");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@orderno", DbType.String, orderno);
                    database.AddInParameter(command, "@custmail", DbType.String, Email);
                    database.AddInParameter(command, "@addedby", DbType.String, AddedBy);
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

        public DataSet FetchShutterDetails(int CustomerId, int ProductId, int ProductTypeId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchShutterDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, CustomerId);
                    database.AddInParameter(command, "@ProductId", DbType.Int32, ProductId);
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

        public DataSet FetchShutterDetails(int estimateId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchShutterDetailsByEstimateId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@EstimateId", DbType.Int32, estimateId);
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
        
        public DataSet FetchContractdetails(int EstimateId,int productType)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataSet DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchContractDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@EstimateId", DbType.Int32, EstimateId);
                    database.AddInParameter(command, "@ProductType", DbType.Int32, productType);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int UpdateShutterOrder(string orderno, string Email, string AddedBy, string Shuttertop, string Style, string Color, string SurfaceMount, Double width,
            Double height, int quantity, string Workarea,
            string Spcl_instr, string Img, DateTime DateFrstContc, string Followup1, string Followup2, string Followup3)
        {
            int result = 0;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateShutterDetail");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@orderno", DbType.String, orderno);
                    database.AddInParameter(command, "@custmail", DbType.String, Email);
                    database.AddInParameter(command, "@addedby", DbType.String, AddedBy);
                    database.AddInParameter(command, "@Shuttertop", DbType.String, Shuttertop);
                    database.AddInParameter(command, "@Style", DbType.String, Style);
                    database.AddInParameter(command, "@Color", DbType.String, Color);
                    database.AddInParameter(command, "@SurfaceMount", DbType.String, SurfaceMount);
                    database.AddInParameter(command, "@width", DbType.Double, width);
                    database.AddInParameter(command, "@height", DbType.Double, height);
                    database.AddInParameter(command, "@quantity", DbType.Int32, quantity);
                    database.AddInParameter(command, "@Workarea", DbType.String, Workarea);
                    database.AddInParameter(command, "@Spcl_instr", DbType.String, Spcl_instr);
                    database.AddInParameter(command, "@Img", DbType.String, Img);
                    database.AddInParameter(command, "@DateFrstContc", DbType.DateTime, DateFrstContc);
                    database.AddInParameter(command, "@Followup1", DbType.String, Followup1);
                    database.AddInParameter(command, "@Followup2", DbType.String, Followup2);
                    database.AddInParameter(command, "@Followup3", DbType.String, Followup3);

                    database.AddOutParameter(command, "@result", DbType.Boolean, 1);
                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return result;
        }

        public DataSet fetchShutterMount()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchShutterMount");
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

        public DataSet GetAllShutters()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetAllShutters");
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

        public DataSet GetShutter_subfields(int shutterID)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("Getshutter_subfields");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@ShutterId", DbType.Int32, shutterID);
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

        public bool AddShutterEstimate(shutters objshutter)
        {
            int result = 0;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddShutterEstimate");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@estimateId", DbType.Int32, objshutter.estimateID);
                    database.AddInParameter(command, "@CustomerId", DbType.String, objshutter.CustomerId);
                    database.AddInParameter(command, "@UserId", DbType.String, objshutter.UserId );
                    database.AddInParameter(command, "@ShutterId", DbType.Int32, objshutter.ShutterId);
                    database.AddInParameter(command, "@ShutterTopId", DbType.Int32, objshutter.ShutterTopId);
                    database.AddInParameter(command, "@ShutterWidthId", DbType.Int32, objshutter.ShutterWidthId);
                    database.AddInParameter(command, "@ShutterLength", DbType.String, objshutter.height);
                    database.AddInParameter(command, "@ShutterColorId", DbType.Int32, objshutter.ColorCode);
                    database.AddInParameter(command, "@Quantity", DbType.Int32, objshutter.quantity);
                    database.AddInParameter(command, "@SurfaceMount", DbType.Int32, objshutter.surfaceofmount);
                    database.AddInParameter(command, "@Workarea", DbType.String, objshutter.workarea);
                    database.AddInParameter(command, "@Style", DbType.Int32, objshutter.quantity);
                    database.AddInParameter(command, "@Spcl_instr", DbType.String, objshutter.specialinstructions);
                    //database.AddInParameter(command, "@Loc_Pic", DbType.String, "~/CustomerDocs/LocationPics/" + objshutter.MainImage);
                    database.AddInParameter(command, "@Loc_Pic", DbType.String,  objshutter.MainImage);
                    database.AddInParameter(command, "@ProductTypeId", DbType.Int32, objshutter.ProductType);
                    database.AddInParameter(command, "@LocationPicsXml", DbType.Xml, objshutter.locationpicture);
                    database.AddInParameter(command, "@ShuttersEstimateAccXml", DbType.Xml, objshutter.ShutterAccessorie);
                    database.AddOutParameter(command, "@result", DbType.Int32, 1);
                    database.ExecuteNonQuery(command);
                    result = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                }
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return false;
            }
            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        #region Added for procurement grid
         public string UpdateShutterEstimateForSold(string estimateId, string status, int customerId, int userId, string paymentMode, decimal amount, string checkNo, string creditCardDetails,string filename)
        {
            string result = string.Empty;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateShutterEstimate");
                    command.CommandType = CommandType.StoredProcedure;
                    //database.AddInParameter(command, "@estimateId", DbType.Int32, estimateId);
                    database.AddInParameter(command, "@estimateIdAndProductType", DbType.String, "Test");
                    database.AddInParameter(command, "@userId", DbType.Int32, userId);
                    database.AddInParameter(command, "@status", DbType.String, status);
                    //database.AddInParameter(command, "@productId", DbType.Int32, productType);
                    database.AddInParameter(command, "@customerId", DbType.Int32, customerId);
                    database.AddInParameter(command, "@PaymentMode", DbType.String, paymentMode);
                    database.AddInParameter(command, "@Amount", DbType.Decimal, amount);
                    database.AddInParameter(command, "@CheckNo", DbType.String, checkNo);
                    database.AddInParameter(command, "@CreditCardNo", DbType.String, creditCardDetails);
                    if (filename != "")
                    {
                        database.AddInParameter(command, "@InvoicePath", DbType.String, filename);
                    }
                    database.AddOutParameter(command,"@jobId", DbType.String,100);
                    database.ExecuteNonQuery(command);

                    result = (string)database.GetParameterValue(command, "@jobId");
                }

                return result;
            }
            catch (Exception e)
            {
                
            }
            return null;
            }
        #endregion

        //public bool UpdateShutterEstimate(int estimateId, string status)
        //{            
        //    try
        //    {
        //        SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
        //        {
        //            DbCommand command = database.GetStoredProcCommand("UDP_UpdateShutterEstimate");
        //            command.CommandType = CommandType.StoredProcedure;
        //            database.AddInParameter(command, "@estimateId", DbType.Int32, estimateId);
        //            database.AddInParameter(command, "@status", DbType.String, status);
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

       // public bool UpdateShutterEstimate(int estimateId, string status, int productType, int customerId,int userId,string paymentMode, decimal amount, string checkNo, string creditCardDetails)
        public string UpdateShutterEstimate(string estimateIdAndProductType, string status, int customerId, int userId, string paymentMode, decimal amount, string checkNo, string creditCardDetails,string filename)
        {
            string result = string.Empty;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateShutterEstimate");
                    command.CommandType = CommandType.StoredProcedure;
                    //database.AddInParameter(command, "@estimateId", DbType.Int32, estimateId);
                    database.AddInParameter(command, "@estimateIdAndProductType", DbType.String, estimateIdAndProductType);
                    database.AddInParameter(command, "@userId", DbType.Int32, userId );
                    database.AddInParameter(command, "@status", DbType.String, status);
                    //database.AddInParameter(command, "@productId", DbType.Int32, productType);
                    database.AddInParameter(command, "@customerId", DbType.Int32, customerId);
                    database.AddInParameter(command, "@PaymentMode", DbType.String, paymentMode);
                    database.AddInParameter(command, "@Amount", DbType.Decimal, amount);
                    database.AddInParameter(command, "@CheckNo", DbType.String, checkNo);
                    database.AddInParameter(command, "@CreditCardNo", DbType.String, creditCardDetails);
                    if (filename != "")
                    {
                        database.AddInParameter(command, "@InvoicePath", DbType.String, filename);
                    }
                    database.AddOutParameter(command, "@jobId", DbType.String, 100);
                    database.ExecuteNonQuery(command);

                    result = (string)database.GetParameterValue(command, "@jobId");
                }

                return result;
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                result = string.Empty;
                return result;
            }
        }

        public void SoldJobByAdmin(int CustomID, int ProID, int EstID, string cID, int E, string date, string RefID, string invoic)
      // public string SoldJobByAdmin(int CustomID, string ProID, string EstID, string cID, int E, string date, string RefID, string invoic)
       {
           try
           {
               SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
               {
                   DbCommand command = database.GetStoredProcCommand("SoldJobByAdmin");
                   command.CommandType = CommandType.StoredProcedure;
                   database.AddInParameter(command, "@CustomerID", DbType.Int32, CustomID);
                   
                   database.AddInParameter(command, "@ProductID", DbType.Int32,ProID);
                    database.AddInParameter(command, "@EstimateID", DbType.Int32,EstID);
                   database.AddInParameter(command, "@SoldJobId", DbType.String, cID);
                   database.AddInParameter(command, "@StatusId", DbType.Int32, E);
                   database.AddInParameter(command, "@CreatedOn", DbType.Date,Convert.ToDateTime(date));
                   database.AddInParameter(command, "@ReferenceId", DbType.String, RefID);
                   database.AddInParameter(command, "@InvoicePath", DbType.String, invoic);
                   database.ExecuteNonQuery(command);


               }
           }
           catch (Exception)
           {

               throw;
           }
       }

        public void UpdateEmails(string AdditionalEmails, int Id)
        {
            string result = string.Empty;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UpdateCustEmail");
                    command.CommandType = CommandType.StoredProcedure;
                    //database.AddInParameter(command, "@estimateId", DbType.Int32, estimateId);
                    database.AddInParameter(command, "@AdditionalEmail", DbType.String, AdditionalEmails);
                    database.AddInParameter(command, "@id", DbType.Int32, Id);
                    database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
        }


        //public DataSet GetMaterialList(int customerid, int productid)
        //{
        //    returndata = new DataSet();
        //    try
        //    {
        //        SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
        //        {
        //            DbCommand command = database.GetStoredProcCommand("UDP_GetMaterialList");
        //            command.CommandType = CommandType.StoredProcedure;
        //            database.AddInParameter(command, "@customerid", DbType.Int32, customerid);
        //            database.AddInParameter(command, "@productid", DbType.Int32, productid);
        //            returndata = database.ExecuteDataSet(command);
        //            return returndata;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //LogManager.Instance.WriteToFlatFile(ex);
        //    }
        //    return returndata;
        //}

        public DataSet GetMaterialList(int customerid, string soldJobId)//, int productType, int estimateId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetMaterialList");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerid", DbType.Int32, customerid);
                    //database.AddInParameter(command, "@productid", DbType.Int32, productid);
                  //  database.AddInParameter(command, "@productType", DbType.Int32, productType);
                    database.AddInParameter(command, "@soldJobId", DbType.String , soldJobId);
                   // database.AddInParameter(command, "@estimateId", DbType.Int16, estimateId);

                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetProductLineItems(int CustId, string Status)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_GetProductLineItems");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, CustId);
                    database.AddInParameter(command, "@Status", DbType.String, Status);
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

        public DataSet GetShutterProposal(int estimateId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_ShutterProposal");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@estimateId", DbType.String, estimateId);
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

        public DataSet GetEmails(int CustId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("USP_GetEmails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, CustId);
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



        public DataSet FetchSalesReport()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchSalesReport");
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
        //public DataSet FetchWorkOrderContractdetails(int CustomerId, int EstimateId)
        //{
        //    try
        //    {
        //        SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
        //        {
        //            DataSet DS = new DataSet();
        //            DbCommand command = database.GetStoredProcCommand("UDP_FetchWorkOrderDetails");
        //            command.CommandType = CommandType.StoredProcedure;
        //            database.AddInParameter(command, "@customerid", DbType.Int32, CustomerId);
        //            database.AddInParameter(command, "@estimateid", DbType.Int32, EstimateId);
        //            DS = database.ExecuteDataSet(command);
        //            return DS;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public DataSet FetchWorkOrderContractdetails(int CustomerId, int EstimateId, int ProductType)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataSet DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchWorkOrderDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@customerid", DbType.Int32, CustomerId);
                    database.AddInParameter(command, "@estimateid", DbType.Int32, EstimateId);
                    database.AddInParameter(command, "@prodcuttypeid", DbType.Int32, ProductType);

                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddProductSoldDetails(ProductSoldDetails objPS)
        {
            bool result;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddProductSoldDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, objPS.CustomerId);
                    database.AddInParameter(command, "@EstimateId", DbType.Int32, objPS.EstimateId);
                    database.AddInParameter(command, "@ProductTypeId", DbType.Int32, objPS.ProductTypeId);
                    database.AddInParameter(command, "@PaymentMode", DbType.String, objPS.PaymentMode);
                    database.AddInParameter(command, "@Amount", DbType.Decimal, objPS.Amount);
                    database.AddInParameter(command, "@Check_no", DbType.String, objPS.Check_no);
                    database.AddInParameter(command, "@CreditCard_no", DbType.String, objPS.CreditCard_no);
                    database.AddInParameter(command, "@ExpirationDate", DbType.Date, objPS.ExpirationDate);
                    database.AddInParameter(command, "@SecurityCode", DbType.String, objPS.SecurityCode);
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

    }
}
