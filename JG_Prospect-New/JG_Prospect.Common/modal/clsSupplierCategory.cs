using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
   public class clsSupplierCategory
    {
        private string _SupplierSubCatName;
        public string SupplierSubCatName
        {
            get { return _SupplierSubCatName; }
            set { _SupplierSubCatName = value; }
        }

        private string _SupplierSubCatId;
        public string SupplierSubCatId
        {
            get { return _SupplierSubCatId; }
            set { _SupplierSubCatId = value; }
        }

        private string _SupplierId;
        public string SupplierId
        {
            get { return _SupplierId; }
            set { _SupplierId = value; }
        }
    }

}
