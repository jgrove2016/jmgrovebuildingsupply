using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JG_Prospect.DAL;
using JG_Prospect.Common.modal;
using System.Data;
using JG_Prospect.Common;
namespace JG_Prospect.BLL
{
    public class CustomBLL
    {
        private static CustomBLL m_CustomBLL = new CustomBLL();

        private CustomBLL()
        {

        }

        public static CustomBLL Instance
        {
            get { return m_CustomBLL; }
            set { ;}
        }

        public bool AddCustom(Customs custom)
        {
            return CustomDAL.Instance.AddCustom(custom);
        }
        public bool AddCustomMaterialList(CustomMaterialList cm, string jobid)//,int productTypeId,int estimateId)
        {
            return CustomDAL.Instance.AddCustomMaterialList(cm, jobid);//, productTypeId, estimateId);
        }
        public void DeleteCustomMaterialList(int pID)//,int productTypeId,int estimateId)
        {
            CustomDAL.Instance.DeleteCustomMaterialList(pID);
        }

        public void UpdateProductTypeInMaterialList(int pProdCatID, int pOldProdCatID, string pSoldJobID)
        {
            CustomDAL.Instance.UpdateProductTypeInMaterialList(pProdCatID, pOldProdCatID, pSoldJobID);
        }
        public void DeleteCustomMaterialListByProductCatID(int pProdCatID)//,int productTypeId,int estimateId)
        {
            CustomDAL.Instance.DeleteCustomMaterialListByProductCatID(pProdCatID);
        }

        public bool DeleteCustomMaterialList(string id)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.DeleteCustomMaterialList(id);//, productTypeId, estimateId);
        }

        public bool DeleteWorkorders(string soldJobId)
        {
            return CustomDAL.Instance.DeleteWorkorders(soldJobId);
        }

        public bool UpdateEmailStatusOfCustomMaterialList(string jobid, string emailStatus)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateEmailStatusOfCustomMaterialList(jobid, emailStatus);//, productTypeId, estimateId);
        }

        public int UpdateForemanPermissionOfCustomMaterialList2(string jobid, char permissionStatus, string FormanEmail)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateForemanPermissionOfCustomMaterialList2(jobid, permissionStatus, FormanEmail);//, productTypeId, estimateId);
        }
        public int UpdateForemanPermissionOfCustomMaterialList(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateForemanPermissionOfCustomMaterialList(jobid, permissionStatus, updatedby);//, productTypeId, estimateId);
        }

        public DataSet GetFormanEmail(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetForeManEmail(jobId);//, productTypeId, estimateId);
        }

        public DataSet GetFormanNameAndID(int ID)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetFormanNameAndID(ID);//, productTypeId, estimateId);
        }

        public DataSet GetAdminEmail(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetAdminEmail(jobId);//, productTypeId, estimateId);
        }

        public DataSet GetSrSalesFEmail(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetSrSalesFEmail(jobId);//, productTypeId, estimateId);
        }

        public DataSet GetSrSalesAEmail(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetSrSalesAEmail(jobId);//, productTypeId, estimateId);
        }

        public int UpdateAdminPermissionOfCustomMaterialList(string jobid, char permissionStatus, string AdminEmail)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateAdminPermissionOfCustomMaterialList(jobid, permissionStatus, AdminEmail);//, productTypeId, estimateId);
        }
        public int WhetherCustomMaterialListExists(string jobid)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.WhetherCustomMaterialListExists(jobid);//,productTypeId,estimateId);
        }
        public int WhetherVendorInCustomMaterialListExists(string jobid)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.WhetherVendorInCustomMaterialListExists(jobid);//, productTypeId, estimateId);
        }

        public int CheckPermissionsForCategories(string jobid)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.CheckPermissionsForCategories(jobid);//, productTypeId, estimateId);
        }
        public int CheckPermissionsForCategories(string jobid, int pProductCatID)
        {
            return CustomDAL.Instance.CheckPermissionsForCategories(jobid, pProductCatID);
        }
        public int CheckPermissionsForVendors(string jobid)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.CheckPermissionsForVendors(jobid);//, productTypeId, estimateId);
        }
        public int UpdateSrSalesmanPermissionOfCustomMaterialList(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialList(jobid, permissionStatus, updatedby);//, productTypeId, estimateId);
        }
        public int UpdateSrSalesmanPermissionOfCustomMaterialList(string jobid, char permissionStatus,string SrSalesEmail)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialList(jobid, permissionStatus, SrSalesEmail);//, productTypeId, estimateId);
        }
        public int UpdateSrSalesmanPermissionOfCustomMaterialListF(string jobid, char permissionStatus, string SrSalemanAEmail)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialListF(jobid, permissionStatus, SrSalemanAEmail);//, productTypeId, estimateId);
        }
        //public bool DeleteCustomMaterialList(int vendorCategory, string jobid)
        //{
        //    return CustomDAL.Instance.DeleteCustomMaterialList(vendorCategory, jobid);
        //}
        public Customs GetCustomDetail(Customs custom)
        {
           return custom = CustomDAL.Instance.GetCustomDetail(custom);
        }
        public int WhetherVendorQuotesExists(string soldJobId)
        {
            int vendorQuotesexists = 0;
            return vendorQuotesexists = CustomDAL.Instance.WhetherVendorQuotesExists(soldJobId);
        }
        public DataSet GetAllPermissionOfCustomMaterialList(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetAllPermissionOfCustomMaterialList(jobId);//, productTypeId, estimateId);
        }
        /// <summary>
        /// Shabbir Kanchwala: Called another method from this method. Earlier it was calling the DAL library function.
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public DataSet GetCustom_MaterialList(string jobId)
        {
            return GetCustom_MaterialList(jobId, 0);
        }
        /// <summary>
        /// Shabbir Kanchwala: Did parameter overloading to achieve customer details
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public DataSet GetCustom_MaterialList(string pJobID, int pCustomerID)
        {
            return CustomDAL.Instance.GetCustom_MaterialList(pJobID, pCustomerID);
        }

        public DataSet GetCustomMaterialList(string pJobID, int pCustomerID)
        {
            return CustomDAL.Instance.GetCustomMaterialList(pJobID, pCustomerID);
        }
        public DataSet GetRequestMaterialList(string jobId, int pCustomerID, int pInstallerID)
        {
            return CustomDAL.Instance.GetRequestMaterialList(jobId, pCustomerID, pInstallerID);
        }

        public string GetEmailStatusOfCustomMaterialList(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = CustomDAL.Instance.GetEmailStatusOfCustomMaterialList(jobId);//, productTypeId, estimateId);
            string emailStatus = string.Empty;
            if (ds.Tables.Count > 0)
            {                
              emailStatus=ds.Tables[0].Rows[0][0].ToString();
            }
            return emailStatus;
        }
        public int UpdateSrSalesmanPermissionOfCustomMaterialListF(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateSrSalesmanPermissionOfCustomMaterialListF(jobid, permissionStatus, updatedby);//, productTypeId, estimateId);
        }
        public int UpdateAdminPermissionOfCustomMaterialList(string jobid, char permissionStatus, int updatedby)//, int productTypeId, int estimateId)
        {
            return CustomDAL.Instance.UpdateAdminPermissionOfCustomMaterialList(jobid, permissionStatus,updatedby);//, productTypeId, estimateId);
        }

        public bool AddInstallerToMaterialList(String pSoldJobID, Int32 pInstallerID)
        {
            return CustomDAL.Instance.AddInstallerToMaterialList(pSoldJobID, pInstallerID);
        }

        public Int32 UpdateInstallerPrmToMaterialList(String pSoldJobID, Int32 pInstallerID, String pInstallerPwd)
        {
            return CustomDAL.Instance.UpdateInstallerPrmToMaterialList(pSoldJobID, pInstallerID, pInstallerPwd);
        }
        public void RemoveInstallerFromMaterialList(Int32 ID)
        {
            CustomDAL.Instance.RemoveInstallerFromMaterialList(ID);
        }

        public string GetRequestStatusText(int pRequestStatusID)
        {
            String lRequestStatus = "";
            switch (pRequestStatusID)
            {
                case 1:
                    lRequestStatus = "Pending";
                    break;
                case 2:
                    lRequestStatus = "Approved";
                    break;
                case 3:
                    lRequestStatus = "Rejected";
                    break;
            }
            return lRequestStatus;
        }
        public void UpdateVendorIDs(String pVendorIDs, Int32 pProductCatID, String pExcludedMaterialListID, String pSoldJobID){
            CustomDAL.Instance.UpdateVendorIDs( pVendorIDs, pProductCatID, pExcludedMaterialListID, pSoldJobID);
        }
        public void UpdateVendorIDForSpecMaterial(String pVendorIDs, Int32 pMaterialListID)
        {
            CustomDAL.Instance.UpdateVendorIDForSpecMaterial(pVendorIDs, pMaterialListID);
        }
        public void UpdateSpecificProductLine(string pFieldName, String pFieldValue, Int32 pID, String pSoldJobID)
        {
            CustomDAL.Instance.UpdateSpecificProductLine(pFieldName, pFieldValue, pID, pSoldJobID);
        }
    }
}
