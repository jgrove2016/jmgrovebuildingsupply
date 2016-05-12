using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JG_Prospect.WebAPI.Models
{
    public class clsMisPunch
    {
        public string Date { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int EmployeeID { get; set; }
    }
    public class BLLAttendenceRepo
    {
        public List<clsMisPunch> GetEmployeeReportHistory(int EmployeeID)
        {
            SqlParameter[] param = { new SqlParameter("@EmployeeID", EmployeeID),
                                   };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnectionString, CommandType.StoredProcedure, "getMissPunchReportsByEmployeeId", param);

            List<clsMisPunch> objData = new List<clsMisPunch>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objData.Add(new clsMisPunch()
                    {
                        Status = dr["Status"] != DBNull.Value ? dr["Status"].ToString() : "",
                        Reason = dr["Reason"] != DBNull.Value ? dr["Reason"].ToString() : "",
                        Date = dr["Date"] != DBNull.Value ? Convert.ToDateTime(dr["Date"].ToString()).ToShortDateString() : "",
                    });
                }
            }
            return objData;

        }

        public bool AddEmployeeReport(clsMisPunch clsmls)
        {
            try
            {
                SqlParameter[] param = { 
                                            new SqlParameter("@EmployeeID",clsmls.EmployeeID),
                                            new SqlParameter("@Date",clsmls.Date),
                                            new SqlParameter("@Reason",clsmls.Reason),
                                       };
                return SqlHelper.ExecuteNonQuery(Connection.ConnectionString, CommandType.StoredProcedure, "postMissPunchReport", param) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}