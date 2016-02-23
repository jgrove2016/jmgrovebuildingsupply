using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JG_Prospect.DAL;
using JG_Prospect.Common.modal;
namespace JG_Prospect.BLL
{
    public class VendorBLL
    {
        private static VendorBLL m_VendorBLL = new VendorBLL();
        private VendorBLL()
        { 
        }
        public static VendorBLL Instance
        {
            get { return m_VendorBLL; }
            set { ;}
        }
        public bool AddVendorQuotes(string soldJobId, string originalFileName, string temporaryFileName, int vendorId)
        {
            return VendorDAL.Instance.AddVendorQuotes(soldJobId, originalFileName, temporaryFileName, vendorId);
        }
        public void RemoveAttachedQuote(string file)
        {
            VendorDAL.Instance.RemoveAttachedQuote(file);
        }
        public bool UpdateAttachedQuote(string soldJobId, string originalFileName, string temporaryFileName, int vendorId)
        {
            return VendorDAL.Instance.UpdateAttachedQuote(soldJobId, originalFileName, temporaryFileName, vendorId);
        }
        public DataSet GetAllAttachedQuotes(string soldJobId)
        {
            DataSet ds = VendorDAL.Instance.GetAllAttachedQuotes(soldJobId);
            return ds;
        }

        public DataSet GetMaterialListData(string soldJobId,int CustomerId)
        {
            DataSet ds = VendorDAL.Instance.GetMaterialListData(soldJobId, CustomerId);
            return ds;
        }
          public DataSet GetVendorQuoteByVendorId(string soldJobId, int vendorId)
        {
            return VendorDAL.Instance.GetVendorQuoteByVendorId(soldJobId, vendorId);
        }      

        public DataSet fetchallvendordetails()
        {
            return VendorDAL.Instance.fetchallvendorDetails();
        }
        public DataSet fetchallvendorcategory()
        {
            return VendorDAL.Instance.fetchallvendorcategory();
        }
        public DataSet fetchAllVendorCategoryHavingVendors()
        {
            return VendorDAL.Instance.fetchAllVendorCategoryHavingVendors();
        }
        public DataSet fetchMaterialListForEmail(string vendorCategory)
        {
            return VendorDAL.Instance.fetchMaterialListForEmail(vendorCategory);
        }

        public DataSet fetchVendorNamesByVendorCategory(int vendorCategoryId)
        {
            return VendorDAL.Instance.fetchVendorNamesByVendorCategory(vendorCategoryId);
        }
        public DataSet fetchVendorDetailsByVendorId(int vendorId)
        {
            return VendorDAL.Instance.fetchVendorDetailsByVendorId(vendorId);
        }
        public DataSet getVendorEmailId(string vendorName)
        {
            return VendorDAL.Instance.getVendorEmailId(vendorName);
        }

        public DataSet getVendorDetails(string VendorIds)
        {
            return VendorDAL.Instance.getVendorDetails(VendorIds);
        }

        public DataSet fetchVendorListByCategoryForEmail(int category)
        {
            return VendorDAL.Instance.fetchVendorListByCategoryForEmail(category);
        }

        public bool deletevendor(int vendorid)
        {
            return VendorDAL.Instance.deletevendor(vendorid);
        }
        public bool deletevendorcategory(int vendorcategogyid)
        {
            return VendorDAL.Instance.deletevendorcategory(vendorcategogyid);
        }
        public bool savevendor(Vendor objvendor)
        {
            return VendorDAL.Instance.savevendor(objvendor);
        }
        public DataSet FetchvendorDetails(int vendorid)
        { 
        return VendorDAL.Instance.FetchvendorDetails(vendorid);
        }
        public bool savevendorcatalogdetails(Vendor_Catalog objcatalog)
        {
            return VendorDAL.Instance.savevendorcatalogdetails(objcatalog );
        }

        public DataSet GetAllvendorDetails()
        {
            return VendorDAL.Instance.GetAllvendorDetails();
        }

    }
}
