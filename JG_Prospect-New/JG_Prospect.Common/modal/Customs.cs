using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JG_Prospect.Common.modal
{
    public class Customs
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string WorkArea { get; set; }
        public string ProposalTerms { get; set; }
        public Decimal ProposalCost { get; set; }
        public string Attachment { get; set; }
        public string SpecialInstruction { get; set; }
        public string LocationImage { get; set; }
        public string MainImage { get; set; }
        public int ProductTypeId { get; set; }
        public string Status { get; set; }
        public string Others { get; set; }
        public List<CustomerLocationPic> CustomerLocationPics { get; set; }      
        //public DataSet CustomerLocationPics { get; set; }
        public string CustSuppliedMaterial{ get; set; }
        public bool IsCustSupMatApplicable { get; set; }
        public string MaterialStorage{ get; set; }
        public bool IsMatStorageApplicable{ get; set; }
        public bool IsPermitRequired{ get; set; }
        public bool IsHabitat { get; set; }
        public int ProductId { get; set; }
        public bool IsDumpStorageApplicable { get; set; }
        public string DumpStorage { get; set; }
    }
}
