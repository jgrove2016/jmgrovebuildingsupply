using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class clsSku
    {
        public int Id { get; set; }
        public string SkuName { get; set; }
        public float TotalCost { get; set; }
        public string UOM { get; set; }
        public float Unit { get; set; }
        public string CostDescription{ get; set; }
        public string VendorPart { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
    }
}
