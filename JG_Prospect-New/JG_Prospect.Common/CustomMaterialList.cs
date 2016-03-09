using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common
{
    [Serializable]
    public class CustomMaterialList
    {
        public int Id { get; set; }
        public int JobSequenceId { get; set; }
        public string MaterialList { get; set; }
        public int VendorCategoryId { get; set; }
        public int VendorId { get; set; }
        public decimal Amount { get; set; }
        public string EmailStatus { get; set; }
        public string IsForemanPermission { get; set; }
        public string IsSrSalemanPermissionF { get; set; }
        public string IsAdminPermission { get; set; }
        public string IsSrSalemanPermissionA { get; set; }
        public DateTime CreatedOn { get; set; }
        public string VendorName { get; set; }
        public string VendorEmail { get; set; }
        public string VendorCategoryName { get; set; }
        public string DocName { get; set; }
        public string TempName { get; set; }
        public int ProductCatId { get; set; }
        public string Line { get; set; }
        public string JGSkuPartNo { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string UOM { get; set; }
        public string VendorQuotesPath { get; set; }
        public Decimal MaterialCost { get; set; }
        public string extend { get; set; }
        public decimal Total { get; set; }
        public int JobSeqId { get; set; }
        public string VendorNames { get; set; }
        public string VendorIds { get; set; }
        public string VendorEmails { get; set; }
        public string DisplaDLL { get; set; }
        public JGConstant.CustomMaterialListStatus Status { get; set; }
        public int ForemaneID { get; set; }
        public string ForemanUserName { get; set; }
        public string ForemanFirstName { get; set; }
        public string ForemanLastName { get; set; }

        public int SrSaleManFID { get; set; }
        public string SrSaleManFUserName { get; set; }
        public string SrSaleManFFirstName { get; set; }
        public string SrSaleManFLastName { get; set; }
        public int SrSaleManAID { get; set; }
        public string SrSaleManAUserName { get; set; }
        public string SrSaleManAFirstName { get; set; }
        public string SrSaleManALastName { get; set; }
        public int AdminID { get; set; }
        public string AdminUserName { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }

        public int StaffID { get; set; }
        public string StaffName { get; set; }
    }
}

