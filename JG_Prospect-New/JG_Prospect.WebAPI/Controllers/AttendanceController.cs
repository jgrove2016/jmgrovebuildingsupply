using JG_Prospect.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JG_Prospect.WebAPI.Controllers
{
    public class AttendanceController : ApiController
    {
        // GET api/attendance
        public ResultClass Get(int id)
        {
            try
            {
                BLLAttendenceRepo objRepo = new BLLAttendenceRepo();

                return new ResultClass()
                {
                    Message = "Found Successfully",
                    Status = true,
                    Result = objRepo.GetEmployeeHistory(id)
                };
            }
            catch (Exception ex)
            {
                return new ResultClass()
                {
                    Message = ex.Message,
                    Status = false,
                };
            }
        }
    }
}
