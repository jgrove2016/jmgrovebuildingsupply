using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JG_Prospect.App_Code
{
    public class PDFOtherFieldType : PDFFieldType
    {
        public override int Type
        {
            get { return -1; }
        }

        public override string ToString()
        {
            return "Other";
        }
    }
}