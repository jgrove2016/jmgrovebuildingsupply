using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    [Serializable]
    public class CustomerDocument
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int HTMLTemplateID { get; set; }
    }
}
