using System;
using System.Data;
using JG_Prospect.DAL;

namespace JG_Prospect.BLL
{
    public class TaskBLL
    {
        private static TaskBLL taskBLL = new TaskBLL();

        private TaskBLL()
        {
        }
        public static TaskBLL Instance
        {
            get { return taskBLL; }
            private set {; }
        }

        public DataSet GetTaskList(string title, string designation, string assignedUser, int? status, DateTime? createdDate)
        {
            return TaskDAL.Instance.GetTaskList(title, designation, assignedUser, status, createdDate);
        }

        public DataTable GetAllDesignation()
        {
            return TaskDAL.Instance.GetAllDesignation();
        }

        public DataTable GetUsersByDesignation(string designation)
        {
            return TaskDAL.Instance.GetUsersByDesignation(designation);
        }
    }
}
