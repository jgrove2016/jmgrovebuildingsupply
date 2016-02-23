using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JG_Prospect.DAL;
namespace JG_Prospect.BLL
{
   public  class ShutterPriceControlBLL
    {
         private static ShutterPriceControlBLL m_ShutterPriceControlBLL=new ShutterPriceControlBLL();
         private ShutterPriceControlBLL()
        { 
        }
         public static ShutterPriceControlBLL Instance
        {
            get { return m_ShutterPriceControlBLL; }
            set { ;}
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
         public bool updateshutterprice(int id,decimal price)
         {
             return ShutterPriceControlDAL.Instance.updateshutterprice(id,price);
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
         public bool saveshuttercolor(String colorcode,String shuttercolorname, decimal price)
         {
             return ShutterPriceControlDAL.Instance.saveshuttercolor(colorcode,shuttercolorname, price);
         }
         public bool saveshutteraccessories(String shutteraccessoriesname, decimal price)
         {
             return ShutterPriceControlDAL.Instance.saveshutteraccessories(shutteraccessoriesname, price);
         }
    }
}
