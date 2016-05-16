using JG_Prospect.DAL.Database;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG_Prospect.DAL
{
    public class DLLAttendence
    {
        public DataSet GetEmployeeHistory(int EmployeeID)
        {
            DataSet result = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("getAttendanceHistoryByEmployeeId");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@EmployeeID", DbType.Int32, EmployeeID);
                    result = database.ExecuteDataSet(command);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
