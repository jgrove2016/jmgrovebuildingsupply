using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using JG_Prospect.DAL.Database;
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

        private DataSet returndata;

        public Int64 SaveOrDeleteTask(Task objTask)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_SaveOrDeleteTask");

                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Mode", DbType.Int16, objTask.Mode);
                    database.AddInParameter(command, "@TaskId", DbType.Int64, objTask.TaskId);
                    database.AddInParameter(command, "@Title", DbType.String, objTask.Title);
                    database.AddInParameter(command, "@Description", DbType.String, objTask.Description);
                    database.AddInParameter(command, "@Status", DbType.Int16, objTask.Status);
                    database.AddInParameter(command, "@DueDate", DbType.String, objTask.DueDate);
                    database.AddInParameter(command, "@Hours", DbType.Int32, objTask.Hours);
                    database.AddInParameter(command, "@Notes", DbType.String, objTask.Notes);
                    database.AddInParameter(command, "@Attachment", DbType.String, objTask.Attachment);
                    database.AddInParameter(command, "@CreatedBy", DbType.Int32, objTask.CreatedBy);
                    database.AddInParameter(command, "@InstallId", DbType.String, objTask.InstallId);
                    database.AddOutParameter(command, "@Result", DbType.Int32, 0);

                    int result = database.ExecuteNonQuery(command);

                    if (objTask.Mode == 0)
                    {
                        Int64 Identity = 0;
                        Identity = Convert.ToInt64(database.GetParameterValue(command, "@Result"));
                        return Identity;
                    }
                    else
                    {
                        return Convert.ToInt64(result);
                    }

                }
            }

            catch (Exception ex)
            {
                return 0;
            }

        }

        public bool SaveOrDeleteTaskUser(ref TaskUser objTaskUser)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_SaveOrDeleteTaskUser");

                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Mode", DbType.Int16, objTaskUser.Mode);
                    database.AddInParameter(command, "@Id", DbType.Int64, objTaskUser.Id);
                    database.AddInParameter(command, "@TaskId", DbType.Int64, objTaskUser.TaskId);
                    database.AddInParameter(command, "@UserId", DbType.String, objTaskUser.UserId);
                    database.AddInParameter(command, "@UserType", DbType.Boolean, objTaskUser.UserType);
                    database.AddInParameter(command, "@Status", DbType.Int16, objTaskUser.Status);
                    database.AddInParameter(command, "@Notes", DbType.String, objTaskUser.Notes);
                    database.AddInParameter(command, "@UserAcceptance", DbType.Boolean, objTaskUser.UserAcceptance);
                    database.AddOutParameter(command, "@TaskUpdateId", SqlDbType.BigInt, Int32.MaxValue);
                    

                    int result = database.ExecuteNonQuery(command);

                    objTaskUser.TaskUpdateId = Convert.ToInt32(database.GetParameterValue(command, "@TaskUpdateId"));

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            catch (Exception ex)
            {
                return false;
            }

        }



        public bool SaveOrDeleteTaskUserFiles(TaskUser objTaskUser)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_SaveOrDeleteTaskUserFiles");

                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Mode", DbType.UInt16, objTaskUser.Mode);
                    database.AddInParameter(command, "@Id", DbType.UInt64, objTaskUser.Id);
                    database.AddInParameter(command, "@TaskId", DbType.UInt64, objTaskUser.TaskId);
                    database.AddInParameter(command, "@UserId", DbType.String, objTaskUser.UserId);
                    database.AddInParameter(command, "@Attachment", DbType.Boolean, objTaskUser.Attachment);
                    database.AddInParameter(command, "@TaskUpdateId", DbType.Int32, objTaskUser.TaskUpdateId);


                    int result = database.ExecuteNonQuery(command);

                   

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                return false;
            }

        }

        //Get details for task with user and attachments
        public DataSet GetTaskDetails(Int32 TaskId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("usp_GetTaskDetails");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@TaskId", DbType.Int32, TaskId);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetInstallUsers(int Key, string Designastion)
        {
            DataSet result = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_GetInstallUsers");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Key", DbType.Int16, Key);
                    database.AddInParameter(command, "@Designation", DbType.String, Designastion);
                    result = database.ExecuteDataSet(command);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet GetTaskUserDetails(Int16 TaskID)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_GetTaskUserDetailsByTaskId");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@TaskId", DbType.Int16, TaskID);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// to GetInstallUserDetails by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet GetInstallUserDetails(Int32 Id)
        {
            DataSet result = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_GetInstallUserDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int16, Id);
                    result = database.ExecuteDataSet(command);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




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
