using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using JG_Prospect.DAL.Database;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
namespace JG_Prospect.DAL
{
    public class ShutterPriceControlDAL
    {
        private static ShutterPriceControlDAL m_ShutterPriceControlDAL = new ShutterPriceControlDAL();
        private ShutterPriceControlDAL()
        {
        }



        public static ShutterPriceControlDAL Instance
        {
            get { return m_ShutterPriceControlDAL; }
            set { ;}
        }
        private DataSet DS = new DataSet();

        public DataSet fetchshutterdetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchshuttersdetails");
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
    
        public static bool InsertTransaction(string ccNumber, string ccSecurityCode, string ccFirstName, string ccLastName, string ExpirationDate, decimal ccPriceValue, bool ccStatus, string ccMessage, string ccResponse, string ccRequest, int CustomerId, int ProductId, string AuthorizationCode, string PaylineTransectionId, string pSoldJobID)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataSet DS = new DataSet();
                    DbCommand cm = database.GetStoredProcCommand("InsertTransaction");
                    cm.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(cm, "@ccNumber", DbType.String, ccNumber);
                    database.AddInParameter(cm, "@ccSecurityCode", DbType.String, ccSecurityCode);
                    database.AddInParameter(cm, "@ccFirstName", DbType.String, ccFirstName);
                    database.AddInParameter(cm, "@ccLastName", DbType.String, ccLastName);
                    database.AddInParameter(cm, "@ExpirationDate", DbType.Int32, ExpirationDate);
                    database.AddInParameter(cm, "@ccPriceValue", DbType.Int32, ccPriceValue);
                    database.AddInParameter(cm, "@ccStatus", DbType.Byte, ccStatus);
                    database.AddInParameter(cm, "@ccMessage", DbType.String, ccMessage);
                    database.AddInParameter(cm, "@ccResponse", DbType.String, ccResponse);
                    database.AddInParameter(cm, "@ccRequest", DbType.String, ccRequest);
                    database.AddInParameter(cm, "@CustomerId", DbType.Int32, CustomerId);
                    database.AddInParameter(cm, "@ProductId", DbType.Int32, ProductId);
                    database.AddInParameter(cm, "@AuthorizationCode", DbType.String, AuthorizationCode);
                    database.AddInParameter(cm, "@PaylineTransectionId", DbType.String, PaylineTransectionId);
                    database.AddInParameter(cm, "@SoldJobID", DbType.String, pSoldJobID);
                    DS = database.ExecuteDataSet(cm);
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        public DataSet fetchtopshutterdetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchtopshutterdetails");
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
        public DataSet fetchshuttercolordetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchshuttercolordetails");
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
        public DataSet fetchshutteraccessoriesdetails()
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchshutteraccessoriesdetails");
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
        public DataSet fetchshutterprice(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchshutterprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shutter_id", DbType.Int32, id);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchtopshutterprice(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchtopshutterprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shuttertop_id", DbType.Int32, id);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchshuttercolorprice(string colorcode)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchshuttercolorprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@colorcode", DbType.String, colorcode);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet fetchshutteraccessoriesprice(int id)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_fetchshutteraccessoriesprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shutteraccessories_id", DbType.Int32, id);
                    DS = database.ExecuteDataSet(command);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool updateshutterprice(int id, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_updateshutterprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shutter_id", DbType.Int32, id);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updatetopshutterprice(int id, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_updatetopshutterprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@topshutter_id", DbType.Int32, id);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool updateshuttercolorprice(string colorcode, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_updateshuttercolorprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@colorcode", DbType.String, colorcode);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateshutteraccessoriesprice(int id, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_updateshutteraccessoriesprice");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shutteraccessories_id", DbType.Int32, id);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool saveshuttertop(string shuttertopname, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_saveshuttertop");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shuttertop_name", DbType.String, shuttertopname);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool saveshuttercolor(String colorcode, String shuttercolorname, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_saveshuttercolor");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@colorcode", DbType.Int32, colorcode);
                    database.AddInParameter(command, "@shuttercolor_name", DbType.String, shuttercolorname);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool saveshutteraccessories(String shutteraccessoriesname, decimal price)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    DbCommand command = database.GetStoredProcCommand("UDP_saveshutteraccessories");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@shutteraccessories_name", DbType.String, shutteraccessoriesname);
                    database.AddInParameter(command, "@price", DbType.Decimal, price);
                    DS = database.ExecuteDataSet(command);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
