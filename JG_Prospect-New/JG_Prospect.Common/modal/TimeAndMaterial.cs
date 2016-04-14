using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class TimeAndMaterial
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string WorkArea { get; set; }
        public string TimeAndMaterialTerm { get; set; }
        public string MainImage { get; set; }
        public string LocationImage{ get; set; }
        public int ProductType { get; set; }
        public string Customer { get; set; }
        public string SpecialInstruction { get; set; }
        public List<CustomerLocationPic> CustomerLocationPics { get; set; }
    }
}
