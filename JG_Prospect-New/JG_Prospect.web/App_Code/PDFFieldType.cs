using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JG_Prospect.App_Code
{
    public abstract class PDFFieldType
    {
        public static PDFFieldType GetPDFFieldType(int type)
        {
            switch (type)
            {

                case 4:
                    return new PDFTextFieldType();
                case 2:
                    return new PDFCheckBoxFieldType();
                default:
                    return new PDFOtherFieldType();
            }
        }

        public abstract int Type { get; }
    }
}