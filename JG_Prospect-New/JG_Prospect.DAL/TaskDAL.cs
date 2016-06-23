using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JG_Prospect.DAL.Database;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace JG_Prospect.DAL
{
    public class TaskDAL
    {
        private static TaskDAL taskDAL = new TaskDAL();

        public static TaskDAL Instance
        {
            get { return taskDAL; }
            private set {; }
        }

        public DataSet GetTaskList(string title, string designation, string assignedUser, int? status, DateTime? createdDate)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("spGetTaskList");
                    database.AddInParameter(command, "@Title", DbType.String, title);
                    database.AddInParameter(command, "@Designation", DbType.String, designation);
                    database.AddInParameter(command, "@AssignedUser", DbType.String, assignedUser);
                    database.AddInParameter(command, "@Status", DbType.Int16, status);
                    database.AddInParameter(command, "@CreatedDate", DbType.DateTime, createdDate);
                    command.CommandType = CommandType.StoredProcedure;
                    ds = database.ExecuteDataSet(command);
                }
            }

            catch (Exception ex)
            {
            }
            return ds;
        }


        public DataTable GetAllDesignation()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("spGetAllDesignation");

                    command.CommandType = CommandType.StoredProcedure;
                    dt = database.ExecuteDataSet(command).Tables[0];
                }
            }

            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetUsersByDesignation(string designation)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("spGetUsersByDesignation");
                    database.AddInParameter(command, "@Designation", DbType.String, designation);
                    command.CommandType = CommandType.StoredProcedure;
                    dt = database.ExecuteDataSet(command).Tables[0];
                }
            }

            catch (Exception ex)
            {
            }
            return dt;
        }


    }

}
