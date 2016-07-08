using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JG_Prospect.DAL;

namespace JG_Prospect.BLL
{
    public class existing_customerBLL
    {
        private static existing_customerBLL m_existing_customerBLL = new existing_customerBLL();

        private existing_customerBLL()
        {
        }

        public static existing_customerBLL Instance
        {
            get { return m_existing_customerBLL; }
            set { ;}
        }

        public DataSet GetExistingCustomer(string AddedBy)
        {
            return existing_customerDAL.Instance.GetExistingCustomer(AddedBy);
        }

        public DataSet GetExistingCustomerDetail(int Id, string cust_mailid, string AddedBy)
        {
            return existing_customerDAL.Instance.GetExistingCustomerDetail(Id, cust_mailid, AddedBy);
        }

        public DataSet GetExistingCustomerDetailById(int Id)
        {
            return existing_customerDAL.Instance.GetExistingCustomerDetailById(Id);
        }

        public DataSet UpdateCustomerAssignId(int CustomerId, int assigntoId)
        {
            return existing_customerDAL.Instance.UpdateCustomerAssignId(CustomerId, assigntoId);
        }

        public int UpdateCustomer(int Id, string name, string strno, string jobloc, DateTime estdate, DateTime todaydate, string ph1, string secph, string mail, string calltakenby, string servicetax, string leadtype)
        {
            return existing_customerDAL.Instance.UpdateCustomer(Id, name, strno, jobloc, estdate, todaydate, ph1, secph, mail, calltakenby, servicetax, leadtype);

        }

        public int AddNewEstimate(DateTime DateFrstContc, string Email, string Followup1, string Followup2, string Followup3, string shuttertop,
          string Style, string Color, string SurfaceMount, Double widht, Double height, int qnty, string Workarea, string spclIntr, string Img, string AddedBy)
        {
            return existing_customerDAL.Instance.AddNewEstimate(DateFrstContc, Email, Followup1, Followup2, Followup3, shuttertop, Style, Color, SurfaceMount, widht, height, qnty, Workarea, spclIntr, Img, AddedBy);
        }
        public int ChangePassword(string pass1, string id, string usertype)
        {
            return existing_customerDAL.Instance.UpdateAdmin(pass1, id, usertype);

        }
        public DataSet AllCustomer()
        {
            return existing_customerDAL.Instance.Allcustomer();
        }
    }
}
