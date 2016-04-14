using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JG_Prospect.DAL;
namespace JG_Prospect.BLL
{
    public class ShutterPriceControlBLL
    {
        private static ShutterPriceControlBLL m_ShutterPriceControlBLL = new ShutterPriceControlBLL();
        private ShutterPriceControlBLL()
        {
        }




        public static ShutterPriceControlBLL Instance
        {
            get { return m_ShutterPriceControlBLL; }
            set { ;}
        }

        public static bool InsertTransaction(string ccNumber, string ccSecurityCode, string ccFirstName, string ccLastName, string ExpirationDate,
         decimal ccPriceValue, bool ccStatus, string ccMessage, string ccResponse, string ccRequest, int CustomerId, int ProductId, string AuthorizationCode, string PaylineTransectionId)
        {
            try
            {
                return ShutterPriceControlDAL.InsertTransaction(ccNumber, ccSecurityCode, ccFirstName, ccLastName, ExpirationDate, ccPriceValue, ccStatus, ccMessage, ccResponse, ccRequest, CustomerId, ProductId, AuthorizationCode, PaylineTransectionId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string Encode(string strPlainText)
        {

            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(strPlainText);
            string returntext = System.Convert.ToBase64String(mybyte);
            return returntext;

        }
        public static string Decode(string strPlainText)
        {
            byte[] mybyte = System.Convert.FromBase64String(strPlainText);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }

        public DataSet fetchshutterdetails()
        {
            return ShutterPriceControlDAL.Instance.fetchshutterdetails();
        }
        public DataSet fetchtopshutterdetails()
        {
            return ShutterPriceControlDAL.Instance.fetchtopshutterdetails();
        }
        public DataSet fetchshuttercolordetails()
        {
            return ShutterPriceControlDAL.Instance.fetchshuttercolordetails();
        }
        public DataSet fetchshutteraccessoriesdetails()
        {
            return ShutterPriceControlDAL.Instance.fetchshutteraccessoriesdetails();
        }
        public DataSet fetchshutterprice(int id)
        {
            return ShutterPriceControlDAL.Instance.fetchshutterprice(id);
        }
        public DataSet fetchtopshutterprice(int id)
        {
            return ShutterPriceControlDAL.Instance.fetchtopshutterprice(id);
        }
        public DataSet fetchshuttercolorprice(string colorcode)
        {
            return ShutterPriceControlDAL.Instance.fetchshuttercolorprice(colorcode);
        }
        public DataSet fetchshutteraccessorieshprice(int id)
        {
            return ShutterPriceControlDAL.Instance.fetchshutteraccessoriesprice(id);
        }
        public bool updateshutterprice(int id, decimal price)
        {
            return ShutterPriceControlDAL.Instance.updateshutterprice(id, price);
        }
        public bool updatetopshutterprice(int id, decimal price)
        {
            return ShutterPriceControlDAL.Instance.updatetopshutterprice(id, price);
        }
        public bool updateshuttercolorprice(string colorcode, decimal price)
        {
            return ShutterPriceControlDAL.Instance.updateshuttercolorprice(colorcode, price);
        }
        public bool updateshutteraccessoriesprice(int id, decimal price)
        {
            return ShutterPriceControlDAL.Instance.updateshutteraccessoriesprice(id, price);
        }
        public bool saveshuttertop(String shuttertopname, decimal price)
        {
            return ShutterPriceControlDAL.Instance.saveshuttertop(shuttertopname, price);
        }
        public bool saveshuttercolor(String colorcode, String shuttercolorname, decimal price)
        {
            return ShutterPriceControlDAL.Instance.saveshuttercolor(colorcode, shuttercolorname, price);
        }
        public bool saveshutteraccessories(String shutteraccessoriesname, decimal price)
        {
            return ShutterPriceControlDAL.Instance.saveshutteraccessories(shutteraccessoriesname, price);
        }
    }
}
