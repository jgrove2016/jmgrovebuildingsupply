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
        public ResultClass Post(clsMisPunch obj)
        {
            try
            {
                RepoMisPunch objRepo = new RepoMisPunch();

                Boolean result = false;
                if (obj!=null && obj.EmployeeID > 0)
                {
                    result = objRepo.AddEmployeeReport(obj);
                }
                return new ResultClass()
                {
                    Message = result ? "Added Successfully" : "Unable To add",
                    Status = true,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new ResultClass()
                {
                    Message = ex.Message,
                    Status = false,
                    Result = false
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
