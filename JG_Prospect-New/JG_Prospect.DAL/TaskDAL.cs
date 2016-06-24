using System;
using System.Data;
using System.Data.Common;
using JG_Prospect.Common.modal;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using JG_Prospect.DAL.Database;




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

        private DataSet returndata;

        public Int64 SaveOrDeleteTask(Task objTask)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_SaveOrDeleteTask");

                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Mode", DbType.UInt16, objTask.Mode);
                    database.AddInParameter(command, "@TaskId", DbType.Int64, objTask.TaskId);
                    database.AddInParameter(command, "@Title", DbType.String, objTask.Title);
                    database.AddInParameter(command, "@Description", DbType.String, objTask.Description);
                    database.AddInParameter(command, "@Status", DbType.UInt16, objTask.Status);
                    database.AddInParameter(command, "@DueDate", DbType.String, objTask.DueDate);
                    database.AddInParameter(command, "@Hours", DbType.UInt32, objTask.Hours);
                    database.AddInParameter(command, "@Notes", DbType.String, objTask.Notes);
                    database.AddInParameter(command, "@Attachment", DbType.String, objTask.Attachment);
                    database.AddInParameter(command, "@CreatedBy", DbType.UInt32, objTask.CreatedBy);
                    database.AddInParameter(command, "@Result", DbType.String, 0);

                    int result = database.ExecuteNonQuery(command);

                    if (objTask.Mode == 1)
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

        public bool SaveOrDeleteTaskUser(TaskUser objTaskUser)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("SP_SaveOrDeleteTaskUser");

                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Mode", DbType.UInt16, objTaskUser.Mode);
                    database.AddInParameter(command, "@Id", DbType.UInt64, objTaskUser.Id);
                    database.AddInParameter(command, "@TaskId", DbType.UInt64, objTaskUser.TaskId);
                    database.AddInParameter(command, "@UserId", DbType.String, objTaskUser.UserId);
                    database.AddInParameter(command, "@UserType", DbType.Boolean, objTaskUser.UserType);
                    database.AddInParameter(command, "@Status", DbType.UInt16, objTaskUser.Status);
                    database.AddInParameter(command, "@Notes", DbType.String, objTaskUser.Notes);
                    database.AddInParameter(command, "@UserAcceptance", DbType.Boolean, objTaskUser.UserAcceptance);

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

        public DataSet GetTaskDetails(UInt16 Mode)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("SP_GetTaskDetails");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@Mode", DbType.Int16, Mode);

                    returndata = database.ExecuteDataSet(command);

                    return returndata;
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
