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
    public class TaskGeneratorDAL
    {
        public static TaskGeneratorDAL m_TaskGeneratorDAL = new TaskGeneratorDAL();
        private TaskGeneratorDAL()
        {
        }
        public static TaskGeneratorDAL Instance
        {
            get { return m_TaskGeneratorDAL; }
            private set {; }
        }
        public DataSet returndata;

        /// <summary>
        /// Will fetch task lists based on various filter parameters provided.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Title"></param>
        /// <param name="Designation"></param>
        /// <param name="Status"></param>
        /// <param name="CreatedOn"></param>
        /// <param name="Start"></param>
        /// <param name="PageLimit"></param>
        /// <returns></returns>
        public DataSet GetTasksList(int? UserID, string Title, string Designation, Int16? Status, DateTime? CreatedOn, int Start, int PageLimit)
        {
            returndata = new DataSet();
            
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("usp_SearchUserTasks");

                    if (UserID != null)
                    {
                        database.AddInParameter(command, "@UserID", DbType.Int32, Convert.ToInt32(UserID));
                    }
                    else
                    {
                        database.AddInParameter(command, "@UserID", DbType.Int32, DBNull.Value);
                    }

                    if (!String.IsNullOrEmpty(Title))
                    {
                        database.AddInParameter(command, "@Title", DbType.String, Title);
                    }
                    else
                    {
                        database.AddInParameter(command, "@Title", DbType.String, DBNull.Value);
                    }

                    if (!String.IsNullOrEmpty(Designation))
                    {
                        database.AddInParameter(command, "@Designation", DbType.String, Designation);
                    }
                    else
                    {
                        database.AddInParameter(command, "@Designation", DbType.String, DBNull.Value);
                    }

                    if (Status != null)
                    {
                        database.AddInParameter(command, "@Status", DbType.Int16, Convert.ToInt16(Status));
                    }
                    else
                    {
                        database.AddInParameter(command, "@Status", DbType.Int16, DBNull.Value);
                    }
                    if (CreatedOn != null)
                    {
                        database.AddInParameter(command, "@CreatedOn", DbType.DateTime, Convert.ToDateTime(CreatedOn));
                    }
                    else
                    {
                        database.AddInParameter(command, "@CreatedOn", DbType.DateTime, DBNull.Value);
                    }

                    database.AddInParameter(command, "@Start", DbType.Int32, Start);
                    database.AddInParameter(command, "@PageLimit", DbType.Int32, PageLimit);
                    
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

        /// <summary>
        /// Get all Users and their designtions in system for whom tasks are available in system.
        /// <returns></returns>
        public DataSet GetAllUsersNDesignationsForFilter()
        {
            returndata = new DataSet();

            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("usp_GetUsersNDesignationForTaskFilter");
                                        
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

    }
}
