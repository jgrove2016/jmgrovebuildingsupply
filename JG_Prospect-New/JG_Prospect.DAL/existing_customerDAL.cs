using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JG_Prospect.DAL.Database;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace JG_Prospect.DAL
{
    public class existing_customerDAL
    {

        private static existing_customerDAL m_existing_customerDAL = new existing_customerDAL();

        private existing_customerDAL()
        {

        }


        public static existing_customerDAL Instance
        {
            get { return m_existing_customerDAL; }
            private set { ;}
        }

        private DataSet returndata;
        public int UpdateCustomer(int id, string name, string strno, string jobloc, DateTime estdate, DateTime todaydate, string ph1, string secph, string mail, string calltakenby, string servicetax, string leadtype)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateCustomer");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, id);
                    database.AddInParameter(command, "@Name", DbType.String, name);
                    database.AddInParameter(command, "@StreetNo", DbType.String, strno);
                    database.AddInParameter(command, "@JobLoc", DbType.String, jobloc);
                    database.AddInParameter(command, "@EstTime", DbType.DateTime, estdate);
                    database.AddInParameter(command, "@ToDate", DbType.DateTime, todaydate);
                    database.AddInParameter(command, "@Ph1", DbType.String, ph1);
                    database.AddInParameter(command, "@Ph2", DbType.String, secph);
                    database.AddInParameter(command, "@Mail", DbType.String, mail);
                    database.AddInParameter(command, "@callTake", DbType.String, calltakenby);
                    database.AddInParameter(command, "@ServiceTax", DbType.String, servicetax);
                    database.AddInParameter(command, "@leadtype", DbType.String, leadtype);
                    int a = database.ExecuteNonQuery(command);
                    return a;
                }
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateAdmin(string pass1, string id, string usertype)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_UpdateAdmin");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Pass1", DbType.String, pass1);

                    database.AddInParameter(command, "@Id", DbType.String, id);
                    database.AddInParameter(command, "@UserType", DbType.String, usertype);
                    int a = database.ExecuteNonQuery(command);
                    return a;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return 0;


            }
        }


        public DataSet GetExistingCustomer(string AddedBy)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CustomerlistPerlogin");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@AddedBy", DbType.String, AddedBy);                    
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }


        public DataSet UpdateCustomerAssignId(int CustomerId, int assigntoId)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_updateCustomerassignid");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.String, CustomerId);
                    database.AddInParameter(command, "@assigntoid", DbType.String, assigntoId);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }



        public DataSet GetExistingCustomerDetail(int Id,string cust_mailid, string AddedBy)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CustomerDetail");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@Id", DbType.Int32, Id);
                    database.AddInParameter(command, "@Email", DbType.String, cust_mailid);
                    database.AddInParameter(command, "@AddedBy", DbType.String, AddedBy);
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }

        public DataSet GetExistingCustomerDetailById(int Id)
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_CustomerDetailById");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, Id);                  
                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
            }
            return returndata;
        }

        public int getmaxorderno(string Email, string AddedBy)
        {
            int ordrno = 1;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    string s;
                    DbCommand command = database.GetStoredProcCommand("UDP_Getmaxorderno");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@Email", DbType.String, Email);
                    database.AddInParameter(command, "@AddedBy", DbType.String, AddedBy);
                    s = database.ExecuteScalar(command).ToString();
                    if (s == "")
                    {
                        return ordrno;
                    }
                    else
                    {
                        ordrno = Convert.ToInt32(s.ToString());
                        return ordrno;
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return ordrno;
            }
        }

        public DataSet Allcustomer()
        {
            returndata = new DataSet();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AllCustomer");
                    command.CommandType = CommandType.StoredProcedure;

                    returndata = database.ExecuteDataSet(command);
                    return returndata;
                }
            }

            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex);
                return returndata;


            }
        }


        public int AddNewEstimate(DateTime DateFrstContc, string Email, string Followup1, string Followup2, string Followup3, string shuttertop,
            string Style, string Color, string SurfaceMount, Double widht, Double height, int qnty, string Workarea, string spclIntr, string Img, string AddedBy)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {

                    string orderid = "";
                    if (orderid == "")
                    {
                        orderid = "Estimate-" + (getmaxorderno(Email, AddedBy) + 1).ToString();
                    }
                    //else
                    //{
                    //    orderid = ;
                    //}


                    DbCommand command = database.GetStoredProcCommand("UDP_AddEstimate");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@DateFrstContc", DbType.DateTime, DateFrstContc);
                    database.AddInParameter(command, "@Email", DbType.String, Email);
                    database.AddInParameter(command, "@Followup1", DbType.String, Followup1);
                    database.AddInParameter(command, "@Followup2", DbType.String, Followup2);
                    database.AddInParameter(command, "@Followup3", DbType.String, Followup3);
                    database.AddInParameter(command, "@OrderId", DbType.String, orderid);

                    database.AddInParameter(command, "@shuttertop", DbType.String, shuttertop);
                    database.AddInParameter(command, "@Style", DbType.String, Style);
                    database.AddInParameter(command, "@Color", DbType.String, Color);
                    database.AddInParameter(command, "@SurfaceMount", DbType.String, SurfaceMount);

                    database.AddInParameter(command, "@width", DbType.Double, widht);
                    database.AddInParameter(command, "@height", DbType.Double, height);
                    database.AddInParameter(command, "@quantity", DbType.Int32, qnty);

                    database.AddInParameter(command, "@Workarea", DbType.String, Workarea);
                    database.AddInParameter(command, "@Spcl_instr", DbType.String, spclIntr);
                    database.AddInParameter(command, "@Img", DbType.String, Img);
                    database.AddInParameter(command, "@AddedBy", DbType.String, AddedBy);
                    database.AddOutParameter(command, "@result", DbType.Boolean, 1);
                    database.ExecuteNonQuery(command);
                    int id = Convert.ToInt32(database.GetParameterValue(command, "@result"));
                    if (id > 0)
                    {

                    }

                    return id;
                }
            }

            catch
            {
                return 0;
                //LogManager.Instance.WriteToFlatFile(ex);
            }

        }


    }
}
