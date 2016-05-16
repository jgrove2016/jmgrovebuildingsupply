using JG_Prospect.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JG_Prospect.DAL;
using System.Data;

namespace JG_Prospect.BLL
{
    public class BLLAttendence
    {
        public int EmployeeID { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string TotalHours { get; set; }
    }
    public class BLLAttendenceRepo
    {
        public List<BLLAttendence> GetEmployeeHistory(int EmployeeID)
        {
            DLLAttendence objAttendence = new DLLAttendence();

            var dataset = objAttendence.GetEmployeeHistory(EmployeeID);
            List<BLLAttendence> objData = new List<BLLAttendence>();

            if (dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    objData.Add(new BLLAttendence()
                    {
                        CheckInTime = dr["CheckInTime"] != DBNull.Value ? dr["CheckInTime"].ToString() : "",
                        CheckOutTime = dr["CheckOutTime"] != DBNull.Value ? dr["CheckOutTime"].ToString() : "",
                        TotalHours = dr["TotalHours"] != DBNull.Value ? dr["TotalHours"].ToString() : "",
                        EmployeeID = dr["EmployeeID"] != DBNull.Value ? Convert.ToInt32(dr["EmployeeID"].ToString()) : 0,
                    });
                }
            }
            return objData;

        }
    }
}
