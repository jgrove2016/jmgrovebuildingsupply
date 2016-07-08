using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JG_Prospect.BLL
{
   public class clsProcurementData
    {
       public string Date { get; set; }
       public decimal TotalAmount { get; set; }
       public string Description { get; set; }
       public string PaymentMethod { get; set; }
       public string Transations { get; set; }
       public string Status { get; set; }
       public string InvoiceAttach { get; set; }
    }
   public class clsProcurementDataAll
   {
       public string PrimaryVendor { get; set; }
       public string TotalCost { get; set; }
       public string UnitCost { get; set; }
       public string VendorPart { get; set; }
   }
}
