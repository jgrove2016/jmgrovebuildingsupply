using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JG_Prospect.DAL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Data;

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
