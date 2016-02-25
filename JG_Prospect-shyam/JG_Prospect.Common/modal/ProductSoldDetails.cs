using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class ProductSoldDetails
    {
        public int CustomerId;
        public int ProductTypeId;
        public int EstimateId;
        public string PaymentMode;
        public Decimal Amount;
        public string Check_no;
        public string CreditCard_no;
        public Nullable<DateTime> ExpirationDate;
        public string SecurityCode;
    }
}
