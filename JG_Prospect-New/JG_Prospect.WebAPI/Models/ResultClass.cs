using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JG_Prospect.WebAPI.Models
{
    public class ResultClass
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}