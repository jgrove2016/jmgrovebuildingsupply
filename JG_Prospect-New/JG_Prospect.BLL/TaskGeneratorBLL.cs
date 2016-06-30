using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JG_Prospect.DAL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using JG_Prospect.DAL.Database;

namespace JG_Prospect.BLL
{
    public class TaskGeneratorBLL
    {
        private static TaskGeneratorBLL m_TaskGeneratorBLL = new TaskGeneratorBLL();

        private TaskGeneratorBLL()
        {

        }

        public static TaskGeneratorBLL Instance
        {
            get { return m_TaskGeneratorBLL; }
            set { ;}
        }
        public Int64 SaveOrDeleteTask(Task objTask)
        {
            return TaskGeneratorDAL.Instance.SaveOrDeleteTask(objTask);
        }
        public bool SaveOrDeleteTaskUser(TaskUser objTaskUser)
        {
            return TaskGeneratorDAL.Instance.SaveOrDeleteTaskUser(objTaskUser);
        }
        public bool SaveOrDeleteTaskUserFiles(TaskUser objTaskUser)
        {
            return TaskGeneratorDAL.Instance.SaveOrDeleteTaskUserFiles(objTaskUser);
        }
        public DataSet GetTaskDetails(UInt16 Mode)
        {
            return TaskGeneratorDAL.Instance.GetTaskDetails(Mode);
        }
        public DataSet GetTaskUserDetails(Int16 Mode)
        {
            return TaskGeneratorDAL.Instance.GetTaskUserDetails(Mode);
        }
        public DataSet GetInstallUsers(int key, string Designation)
        {
            return TaskGeneratorDAL.Instance.GetInstallUsers(key, Designation);
        }
        public DataSet GetInstallUserDetails(Int32 Id)
        {
            return TaskGeneratorDAL.Instance.GetInstallUserDetails(Id);
        }





        public DataSet GetTasksList(int? UserID, string Title, string Designation, Int16? Status, DateTime? CreatedOn, int Start, int PageLimit)
        {
            return TaskGeneratorDAL.Instance.GetTasksList( UserID,  Title,  Designation,  Status,  CreatedOn, Start,PageLimit);
        }

        public DataSet GetAllUsersNDesignationsForFilter()
        {
            return TaskGeneratorDAL.Instance.GetAllUsersNDesignationsForFilter();
        }

       
        
    }
}
