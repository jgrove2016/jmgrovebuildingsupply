using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    [Serializable]
    public class AttachedQuotes
    {
        public int id {get;set;}
        public string TempName { get; set; }
        public string DocName { get; set; }
        public string action;
        public int VendorId { get; set; }
        public string VendorCategoryNm { get; set; }
        public string VendorName {get;set;}
        public int VendorCategoryId { get; set; }
    }
}
