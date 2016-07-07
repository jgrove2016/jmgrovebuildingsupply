using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JG_Prospect.WebAPI.Models
{
    public class BLLAttendence
    {
        public int EmployeeID { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string TotalHours { get; set; }
        public string Date { get; set; }
    }
    public class BLLAttendenceRepo
    {
        public List<BLLAttendence> GetEmployeeHistory(int EmployeeID)
        {
            SqlParameter[] param = { new SqlParameter("@EmployeeID", EmployeeID),
                                   };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "getAttendanceHistoryByEmployeeId", param);

            List<BLLAttendence> objData = new List<BLLAttendence>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData.Add(new BLLAttendence()
                    {
                        CheckInTime = dr["CheckInTime"] != DBNull.Value ? dr["CheckInTime"].ToString() : "",
                        CheckOutTime = dr["CheckOutTime"] != DBNull.Value ? dr["CheckOutTime"].ToString() : "",
                        TotalHours = dr["TotalHours"] != DBNull.Value ? dr["TotalHours"].ToString() : "",
                        EmployeeID = EmployeeID,
                        Date = dr["Date"] != DBNull.Value ? Convert.ToDateTime(dr["Date"].ToString()).ToShortDateString() : "",
                    });
                }
            }
            return objData;

        }
    }
}