using JG_Prospect.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JG_Prospect.WebAPI.Controllers
{
    public class MissPunchReportController : ApiController
    {
        // GET api/misspunchreport
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public ResultClass Get(int id)
        {
            try
            {
                RepoMisPunch objRepo = new RepoMisPunch();

                return new ResultClass()
                {
                    Message = "Found Successfully",
                    Status = true,
                    Result = objRepo.GetEmployeeReportHistory(id)
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

        // POST api/misspunchreport
        public ResultClass Post([FromBody]int EmployeeID, DateTime Date, string Reason)
        {
            try
            {
                RepoMisPunch objRepo = new RepoMisPunch();

                return new ResultClass()
                {
                    Message = "Found Successfully",
                    Status = true,
                    Result = objRepo.AddEmployeeReport(new clsMisPunch(){
                        Date = Date.ToShortDateString(),
                        EmployeeID = EmployeeID,
                        Reason = Reason
                    })
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

        // PUT api/misspunchreport/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/misspunchreport/5
        public void Delete(int id)
        {
        }
    }
}
