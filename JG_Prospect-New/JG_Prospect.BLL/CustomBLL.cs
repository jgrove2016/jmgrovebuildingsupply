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
        public DataSet GetCustom_MaterialList(string jobId)//, int productTypeId, int estimateId)
        {
            DataSet ds = null;
            return ds = CustomDAL.Instance.GetCustom_MaterialList(jobId);//,productTypeId,estimateId);
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
       
    }
}
