using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JG_Prospect.DAL;
using JG_Prospect.Common.modal;
using System.Data;

namespace JG_Prospect.BLL
{
    public class TaskBLL
    {
        private static TaskBLL taskBLL = new TaskBLL();

        public static TaskBLL Instance
        {
            get { return taskBLL; }
            set {; }
        }

        public Int64 SaveOrDeleteTask(Task objTask)
        {
            return TaskDAL.Instance.SaveOrDeleteTask(objTask);
        }
        public bool SaveOrDeleteTaskUser(TaskUser objTaskUser)
        {
            return TaskDAL.Instance.SaveOrDeleteTaskUser(objTaskUser);
        }
        public bool SaveOrDeleteTaskUserFiles(TaskUser objTaskUser)
        {
            return TaskDAL.Instance.SaveOrDeleteTaskUserFiles(objTaskUser);
        }
        public DataSet GetTaskDetails(UInt16 Mode)
        {
            return TaskDAL.Instance.GetTaskDetails(Mode);
        }

      }
}
