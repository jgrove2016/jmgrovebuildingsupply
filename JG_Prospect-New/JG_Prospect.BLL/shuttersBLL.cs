using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JG_Prospect.DAL;
using JG_Prospect.Common.modal;

namespace JG_Prospect.BLL
{
    public class shuttersBLL
    {
        private static shuttersBLL m_shuttersBLL = new shuttersBLL();

        private shuttersBLL()
        {
        }

        public static shuttersBLL Instance
        {
            get { return m_shuttersBLL; }
            set { ;}
        }


        public bool DeleteShutterDetails(int shutterid)
        {
            return shuttersDAL.Instance.DeleteShutterDetails(shutterid);
        }
        public DataSet GetShutters(string Email, string AddedBy)
        {
            return shuttersDAL.Instance.GetShutters(Email, AddedBy);
        }

        public DataSet fetchShutterMount()
        {
            return shuttersDAL.Instance.fetchShutterMount();
        }
        public DataSet GetShutterDetail(string orderno, string Email, string AddedBy)
        {
            return shuttersDAL.Instance.GetShutterDetail(orderno, Email, AddedBy);
        }

        public DataSet FetchShutterDetails(int CustomerId, int ProductId, int ProductTypeId)
        {
            return shuttersDAL.Instance.FetchShutterDetails(CustomerId,ProductId,ProductTypeId);
        }

        public DataSet FetchShutterDetails(int estimateId)
        {
            return shuttersDAL.Instance.FetchShutterDetails(estimateId);
        }

        public DataSet FetchContractdetails(int EstimateId,int productType)
        {
            return shuttersDAL.Instance.FetchContractdetails(EstimateId, productType);
        }
                
        public int UpdateShutterOrder(string orderno, string Email, string AddedBy, string Shuttertop, string Style, string Color, string SurfaceMount, Double width,
            Double height, int quantity, string Workarea,
            string Spcl_instr, string Img, DateTime DateFrstContc, string Followup1, string Followup2, string Followup3)
        {
            return shuttersDAL.Instance.UpdateShutterOrder(orderno, Email, AddedBy, Shuttertop, Style, Color, SurfaceMount, width, height, quantity, Workarea, Spcl_instr, Img, DateFrstContc, Followup1, Followup2, Followup3);
        }

        public DataSet GetAllShutters()
        {
            return shuttersDAL.Instance.GetAllShutters();
        }

        public DataSet GetShutter_subfields(int shutterID)
        {
            return shuttersDAL.Instance.GetShutter_subfields(shutterID);
        }

        public bool AddShutterEstimate(shutters shutter)
        {
            System.Xml.Linq.XElement customerLocationPics = new System.Xml.Linq.XElement("root");

            foreach (var r in shutter.CustomerLocationPics)
            {
                var varPics = new System.Xml.Linq.XElement("pics", new System.Xml.Linq.XElement("pic", r.LocationPicture));

                customerLocationPics.Add(varPics);
            }

            shutter.locationpicture = customerLocationPics.ToString();

            System.Xml.Linq.XElement shutterAccessoriesList = new System.Xml.Linq.XElement("root");

            foreach (var r in shutter.ShutterAccessories)
            {
                if (r.accessories != null)
                {
                    var varPics = new System.Xml.Linq.XElement("accessories", new System.Xml.Linq.XElement("accessorie", "<![CDATA[" + r.accessories + "]]> "),
                                                                        new System.Xml.Linq.XElement("qty", r.quantity));

                    shutterAccessoriesList.Add(varPics);
                }
            }
            shutter.ShutterAccessorie = shutterAccessoriesList.ToString();

            return shuttersDAL.Instance.AddShutterEstimate(shutter);
        }

        public DataSet GetProductLineItems(int CustId, string Status)
        {
            return shuttersDAL.Instance.GetProductLineItems(CustId, Status);
        }
        public DataSet GetShutterProposal(int estimateId)
        {
            return shuttersDAL.Instance.GetShutterProposal(estimateId);
        }

        //public bool UpdateShutterEstimate(int estimateId, string status)
        //{
        //    return shuttersDAL.Instance.UpdateShutterEstimate(estimateId, status);
        //}

        public string UpdateShutterEstimateForSold(string estimateId, string status, int customerId, int userId, string paymentMode, decimal amount, string checkNo, string creditCardDetails, string tempInvoiceFileName)
        {
            return shuttersDAL.Instance.UpdateShutterEstimateForSold(estimateId, status, customerId, userId, paymentMode, amount, checkNo, creditCardDetails, tempInvoiceFileName);
        }
       // public bool UpdateShutterEstimate(int estimateId, string status, int productType, int customerId, int userId, string paymentMode, decimal amount, string checkNo, string creditCardDetails)
        public string UpdateShutterEstimate(string estimateIdAndProductType, string status, int customerId, int userId, string paymentMode, decimal amount, string checkNo, string creditCardDetails, string filename)
        {
            //return shuttersDAL.Instance.UpdateShutterEstimate(estimateId, status, productType, customerId, userId, paymentMode,amount,checkNo,creditCardDetails);
            return shuttersDAL.Instance.UpdateShutterEstimate(estimateIdAndProductType, status, customerId, userId, paymentMode, amount, checkNo, creditCardDetails, filename);
        }
      /*  public string SoldJobByAdmin()
        {
            return shuttersDAL.Instance.SoldJobByAdmin();
        }*/
        public void SoldJobByAdmin(int CustomID, int ProID, int EstID, string cID, int E, string date, string RefID, string invoic)
       // public string SoldJobByAdmin(int CustomID, string ProID, string EstID, string cID, int E, string date, string RefID, string invoic)
        {
           // return shuttersDAL.Instance.SoldJobByAdmin(CustomID,ProID,EstID,cID,E,date,RefID,invoic);
             shuttersDAL.Instance.SoldJobByAdmin(CustomID, ProID, EstID, cID, E, date, RefID, invoic);
        }
        public void UpdateEmails(string AdditionalEmails, int Id)
        {
            //return shuttersDAL.Instance.UpdateShutterEstimate(estimateId, status, productType, customerId, userId, paymentMode,amount,checkNo,creditCardDetails);
             shuttersDAL.Instance.UpdateEmails(AdditionalEmails,Id);
        }

        public DataSet GetEmails(int CustId)
        {
            return shuttersDAL.Instance.GetEmails(CustId);
        }

        //public DataSet GetMaterialList(int customerid, int productid)
        //{
        //    return shuttersDAL.Instance.GetMaterialList(customerid, productid);
        //}

        public DataSet GetMaterialList(int customerid, string soldJobId)//, int productType, int estimateId)
        {
            return shuttersDAL.Instance.GetMaterialList(customerid, soldJobId);//, productType, estimateId);
        }

        public DataSet FetchSalesReport()
        {
            return shuttersDAL.Instance.FetchSalesReport();
        }
        public DataSet FetchWorkOrderContractdetails(int CustomerId, int EstimateId, int ProductType)
        {
            return shuttersDAL.Instance.FetchWorkOrderContractdetails(CustomerId, EstimateId, ProductType);
        }
        public bool AddProductSoldDetails(ProductSoldDetails objPS)
        {
            return shuttersDAL.Instance.AddProductSoldDetails(objPS);
        }

    }
}
