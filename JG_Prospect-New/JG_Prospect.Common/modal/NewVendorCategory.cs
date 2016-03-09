using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class NewVendorCategory
    {
        private string _VendorName;
        public string VendorName
        {
            get { return _VendorName; }
            set { _VendorName = value; }
        }

        private string _vendorid;
        public string VendorId
        {
            get { return _vendorid; }
            set { _vendorid = value; }
        }
        private string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        private string _productid;
        public string ProductId
        {
            get { return _productid; }
            set { _productid = value; }
        }

        public bool IsRetail_Wholesale { get; set; }
        public bool IsManufacturer { get; set; }
    }
}
