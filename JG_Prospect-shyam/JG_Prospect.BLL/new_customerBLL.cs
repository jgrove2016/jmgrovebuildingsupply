using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JG_Prospect.DAL;
using System.Data;
using System.IO;
using System.Net;
using JG_Prospect.Common.modal;


namespace JG_Prospect.BLL
{
    public class new_customerBLL
    {
        private static new_customerBLL m_new_customerBLL = new new_customerBLL();

        private new_customerBLL()
        {

        }

        public static new_customerBLL Instance
        {
            get { return m_new_customerBLL; }
            set { ;}
        }

        public int SearchEmailId(string email, string addedby)
        {
            return new_customerDAL.Instance.SearchEmailId(email, addedby);
        }

        public int Searchprimarycontact(string housePh, string cellph, string altPh, int cid)
        {
            return new_customerDAL.Instance.Searchprimarycontact(housePh, cellph, altPh, cid);
        }

        public int AddCustomer(Customer objcustomer)
        {
            return new_customerDAL.Instance.AddCustomer(objcustomer);
        }

        public int UpdateCustomer(Customer objcustomer) //string CustomerName, string CustomerStreet, string Statename, string Cityname, string ZipCode, string BillAddress, DateTime EstDate, string EstTime, string cellphone, string HousePhone, string altphone, string Email, string CallTakenBy, string Service, string besttimetocontact, string primarycontact, string leadtype, string contactprefernece, string AddedBy, int customerid, string Map1, string Map2
        {
            return new_customerDAL.Instance.UpdateCustomer(objcustomer);
        }

        public void UpdateCustomerFollowUpDate(DateTime followupDate, int custId)
        {
            new_customerDAL.Instance.UpdateCustomerFollowUpDate(followupDate,custId);
        }

        public DataSet GetAutoSuggestiveCustomers(string prefix)
        {
            return new_customerDAL.Instance.GetAutoSuggestiveCustomers(prefix);
        }


        public DataSet GetCustomerForDropDown()
        {
            return new_customerDAL.Instance.GetCustomerForDropDown();
        }

        public DataSet GeUsersForDropDown()
        {
            return new_customerDAL.Instance.GetUsersForDropDown();
        }

        public DataSet GetHRCount(string SourceId, string fromDate, string ToDate)
        {
            return new_customerDAL.Instance.GetHRCount(SourceId,fromDate,ToDate);
        }

        public DataSet GetRejected(string SourceId, string fromDate, string ToDate)
        {
            return new_customerDAL.Instance.GetRejected(SourceId, fromDate, ToDate);
        }


        public DataSet SearchCustomers(string searchparameter, string searchstring, string user)
        {
            return new_customerDAL.Instance.SearchCustomers(searchparameter, searchstring,user);
        }

        public DataSet SearchProspect(string searchparameter, string searchstring, string user)
        {
            return new_customerDAL.Instance.SearchProspect(searchparameter, searchstring, user);
        }

        public bool DeleteCustomerDetails(int Id)
        {
            return new_customerDAL.Instance.DeleteCustomerDetails(Id);
        }
        public DataSet GetProposalTerm(int ProductTypeId)
        {
            return new_customerDAL.Instance.GetProposalTermById(ProductTypeId);
        }
        public DataSet GetCustomerDetails(int Id)
        {
            return new_customerDAL.Instance.GetCustomerDetails(Id);
        }
        public DataSet GetCustomerStatusHistory(int CustomerId)
        {
            return new_customerDAL.Instance.GetCustomerStatusHistory(CustomerId);
        }
        public DataSet FetchAllStatus()
        {
            return new_customerDAL.Instance.FetchAllStatus();
        }
        public bool UpdateEmailOfCustomer(int customerID, string email)
        {
            return new_customerDAL.Instance.UpdateEmailOfCustomer(customerID, email);
        }
        public bool UpdateStatusOfCustomer(string soldJobId, int statusId)//, int productTypeId, int estimateId)
        {
            return new_customerDAL.Instance.UpdateStatusOfCustomer(soldJobId, statusId);//, productTypeId, estimateId);
        }
        public DataSet GetTouchPointLogData(int CustomerId)
        {
            return new_customerDAL.Instance.GetTouchPointLogData(CustomerId);
        }
        public DataSet GetProductAndEstimateIdOfSoldJob(string soldJobId)
        {
            return new_customerDAL.Instance.GetProductAndEstimateIdOfSoldJob(soldJobId);
        }

        public DataSet GetCustIdAndName(string soldJobId)
        {
            return new_customerDAL.Instance.GetCustIdAndName(soldJobId);
        }

        public DataSet GetCustName(int Id)
        {
            return new_customerDAL.Instance.GetCustName(Id);
        }

        public DataSet GetCustomerFollowUpDetails(int Id)
        {
            return new_customerDAL.Instance.GetCustomerFollowUpDetails(Id);
        }
        public int AddCustomerFollowUp(int customerid, DateTime meetingdate, string Status, int userId, bool IsNotes, int assignuserid,string strFileName = "")
        {
            return new_customerDAL.Instance.AddCustomerFollowUp(customerid, meetingdate, Status, userId, IsNotes, assignuserid,strFileName);
        }
        public DataSet GetCustomerDocsDetails(int customerId, int productId, int productTypeId)
        {
            return new_customerDAL.Instance.GetCustomerDocsDetails(customerId, productId, productTypeId);
        }
        public DataSet GetCustomerJobPackets(string soldJobId)
        {
            return new_customerDAL.Instance.GetCustomerJobPackets(soldJobId);
        }
        public DataSet GetSoldjobsforprocurement()
        {
            return new_customerDAL.Instance.GetSoldjobsforprocurement();
        }
        public DataSet GetSoldJobsForProcurementBySoldJobId(string soldJobId)
        {
            return new_customerDAL.Instance.GetSoldJobsForProcurementBySoldJobId(soldJobId);
        }
        public void AddAnnualEvent(AnnualEvent Event)
        {
             new_customerDAL.Instance.AddAnnualEvent(Event);
        }

        public DataSet CheckDuplicateAnnualEvent(AnnualEvent Event)
        {
            return new_customerDAL.Instance.CheckDuplicateAnnualEvent(Event);
        }
        public void AddForeManName(AddForemanName foremanname)
        {
            new_customerDAL.Instance.AddForemanName(foremanname);
        }
        public void UpdateAnnualEvent(AnnualEvent Event) //string CustomerName, string CustomerStreet, string Statename, string Cityname, string ZipCode, string BillAddress, DateTime EstDate, string EstTime, string cellphone, string HousePhone, string altphone, string Email, string CallTakenBy, string Service, string besttimetocontact, string primarycontact, string leadtype, string contactprefernece, string AddedBy, int customerid, string Map1, string Map2
        {
            new_customerDAL.Instance.UpdateAnnualEvent(Event);
        }
        public void DeleteAnnualEvent(AnnualEvent Event) //string CustomerName, string CustomerStreet, string Statename, string Cityname, string ZipCode, string BillAddress, DateTime EstDate, string EstTime, string cellphone, string HousePhone, string altphone, string Email, string CallTakenBy, string Service, string besttimetocontact, string primarycontact, string leadtype, string contactprefernece, string AddedBy, int customerid, string Map1, string Map2
        {
            new_customerDAL.Instance.DeleteAnnualEvent(Event);
        }
        
        
        public DataSet FetchCustomerCallSheet(string status, string user, string usertyp)
        {
            return new_customerDAL.Instance.FetchCustomerCallSheet(status, user, usertyp);
        }

        public bool AddCustomerDocs(int customerid, int productid, string originalFileName, string documenttype, string temporaryFileName, int productTypeId,int vendorId)
        {
            return new_customerDAL.Instance.AddCustomerDocs(customerid, productid, originalFileName, documenttype, temporaryFileName, productTypeId,vendorId);
        }
        //public bool AddCustomerDocs(int customerid, int productid, string originalFileName, string documenttype, string temporaryFileName)
        //{
        //    return new_customerDAL.Instance.AddCustomerDocs(customerid, productid, originalFileName, documenttype, temporaryFileName);
        //}
       
        public bool AddCustomerLocationPics(int customerid, string PictureNm)
        {
            return new_customerDAL.Instance.AddCustomerLocationPics(customerid, PictureNm);
        }

        public void SaveMapImage(string Map1, string CustomerStreet, string Cityname, string Statename, string ZipCode,string DestinationPath)
        {
            StringBuilder queryAddress = new StringBuilder();
            //Appending the Basic format of the Staticmap from Google maps Here
            queryAddress.Append("http://maps.google.com/maps/api/staticmap?size=600x500&zoom=14&maptype=roadmap&markers=size:mid|color:red|");
            string location = CustomerStreet + " " + Cityname + " , " + Statename + " " + ZipCode;
            queryAddress.Append(location + ',' + '+');
            queryAddress.Append("&sensor=false");             
            string filename = Map1;
            string path = DestinationPath + "/" + filename;
            string url = queryAddress.ToString();
            //String Url to Bytes
            byte[] bytes = GetBytesFromUrl(url);

            //Bytes Saved to Specified File
            WriteBytesToFile(DestinationPath + "/" + filename, bytes);
        }
        public void SaveMapImageDirection(string Map2, string CustomerStreet, string Cityname, string Statename, string ZipCode, string DestinationPath)
        {
            StringBuilder queryAddress = new StringBuilder();
            //Appending the Basic format of the Staticmap from Google maps Here
            queryAddress.Append("http://maps.google.com/maps/api/staticmap?size=600x500&zoom=14&maptype=roadmap&markers=size:mid|color:red|");
            string location = CustomerStreet + " " + Cityname + " , " + Statename + " " + ZipCode;
            queryAddress.Append(location + ',' + '+');
            queryAddress.Append('|');
            queryAddress.Append("220 krams Ave Manayunk, PA 19127");
            queryAddress.Append("&sensor=false");          
            string filename = Map2;
            string path = DestinationPath + "/" + filename;
            string url = queryAddress.ToString();
            //String Url to Bytes
            byte[] bytes = GetBytesFromUrl(url);

            //Bytes Saved to Specified File
            WriteBytesToFile(DestinationPath + "/" + filename, bytes);
        }

        public byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();
            Stream stream = myResp.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }

        //Writing the bytes in to a Specified File to Save

        public void WriteBytesToFile(string fileName, byte[] content)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
            }
        }
        public int GETCustomerId(int prospectid)
        {
            return new_customerDAL.Instance.GETCustomerId(prospectid);
        }


        public Customer fetchcustomer(int custId)
        {
            DataSet ds = new DataSet();
            ds = UserBLL.Instance.getprospectdetails(custId);

            Customer c = new Customer();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["EstDateSchdule"].ToString() != "")
                    {
                        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["EstDateSchdule"]).ToShortDateString() != "1/1/1753")
                        {
                            c.EstDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EstDateSchdule"]).ToShortDateString();
                        }
                        else
                        {
                            c.EstDate = null;
                        }
                    }
                    string[] citystatezip = ds.Tables[0].Rows[0]["city_state_zip"].ToString().Split(',');

                    c.EstTime = ds.Tables[0].Rows[0]["EstTime"].ToString();
                    c.customerNm = ds.Tables[0].Rows[0]["CustomerName"].ToString();

                    c.CellPh = ds.Tables[0].Rows[0]["CellPh"].ToString();
                    c.AltPh = ds.Tables[0].Rows[0]["AlternatePh"].ToString();
                    c.HousePh = ds.Tables[0].Rows[0]["HousePh"].ToString();
                    c.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    c.CustomerAddress = ds.Tables[0].Rows[0]["CustomerAddress"].ToString();
                    c.Zipcode = citystatezip[2];
                    c.state = citystatezip[1];
                    c.City = citystatezip[0];
                    if (ds.Tables[0].Rows[0]["Followup_date"].ToString() != "")
                        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToShortDateString() != "1/1/1753")
                        {
                            c.followupdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Followup_date"]).ToShortDateString();
                        }
                    c.Productofinterest = Convert.ToInt16(ds.Tables[0].Rows[0]["ProductOfInterest"].ToString());
                    c.BestTimetocontact = ds.Tables[0].Rows[0]["BestTimetocontact"].ToString();
                    c.status = ds.Tables[0].Rows[0]["Status"].ToString();
                    c.BillingAddress = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
                    c.PrimaryContact = ds.Tables[0].Rows[0]["PrimaryContact"].ToString();
                    c.Notes = ds.Tables[0].Rows[0]["Service"].ToString();
                    c.ContactPreference = ds.Tables[0].Rows[0]["ContactPreference"].ToString();
                    c.CallTakenby = ds.Tables[0].Rows[0]["AddedBy"].ToString();
                    c.Addedby = ds.Tables[0].Rows[0]["AddedBy"].ToString();
                }
            }

            return c;
        }

        public int SearchCustomerId(string cellph)
        {
            return new_customerDAL.Instance.SearchCustomerId(cellph);
        }
        public DataSet GetCustomerLocationPics(int customerId, int productId, int productTypeId)
        {
            return new_customerDAL.Instance.GetCustomerLocationPics(customerId, productId, productTypeId);
        }
        public DataSet GetCustomerWorkOrders(int customerId, int productId, int productTypeId)
        {
            return new_customerDAL.Instance.GetCustomerWorkOrders(customerId, productId, productTypeId);
        }
        public DataSet GetCustomerFinalQuotes(int customerId, int productId, int productTypeId)
        {
            return new_customerDAL.Instance.GetCustomerFinalQuotes(customerId, productId, productTypeId);
        }

        public DataSet GetCustomerProfileProducts(int CustId)
        {
            return new_customerDAL.Instance.GetCustomerProfileProducts(CustId);
        }
    }
}
