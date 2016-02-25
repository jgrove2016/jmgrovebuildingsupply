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
