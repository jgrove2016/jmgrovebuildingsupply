using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JG_Prospect.Common.modal;
using JG_Prospect.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using JG_Prospect.DAL.Database;
using System.Data.Common;
using System.Data;

namespace JG_Prospect.DAL
{
    public class TimeAndMaterialDAL
    {
        private static TimeAndMaterialDAL m_TimeAndMaterial = new TimeAndMaterialDAL();
        private TimeAndMaterialDAL()
        {

        }
        public static TimeAndMaterialDAL Instance
        {
            get{return m_TimeAndMaterial;}
            private set { ;}
        }

        public bool AddTimeAndMaterial(TimeAndMaterial timeAndMaterial)
        {
            int result = JGConstant.RETURN_ZERO;
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DbCommand command = database.GetStoredProcCommand("UDP_AddTimeAndMaterial");
                    command.CommandType = CommandType.StoredProcedure;

                    database.AddInParameter(command, "@Id", DbType.Int32, timeAndMaterial.Id);
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, timeAndMaterial.CustomerId);
                    database.AddInParameter(command, "@WorkArea", DbType.String, timeAndMaterial.WorkArea);
                    database.AddInParameter(command, "@LocationImg", DbType.String, timeAndMaterial.LocationImage);
                    database.AddInParameter(command, "@ProductType", DbType.Int32, timeAndMaterial.ProductType);
                    database.AddInParameter(command, "@MainImage", DbType.String, timeAndMaterial.MainImage);
                    database.AddInParameter(command, "@TimeAndMaterialTerm", DbType.Int32, timeAndMaterial.TimeAndMaterialTerm);
                    database.AddInParameter(command, "@SpecialInstruction", DbType.String, timeAndMaterial.SpecialInstruction);

                    result = database.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > JGConstant.RETURN_ZERO ? JGConstant.RETURN_TRUE : JGConstant.RETURN_FALSE;
        }

        public TimeAndMaterial GetTimeAndMaterial(TimeAndMaterial timeAndMaterial)
        {
            DataSet returndata = null;
            List<CustomerLocationPic> customerLocationPics = new List<CustomerLocationPic>();
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    returndata = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_GetTimeAndMaterial");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", SqlDbType.Int, timeAndMaterial.CustomerId);
                    database.AddInParameter(command, "@ProductId", SqlDbType.Int, timeAndMaterial.Id);
                    database.AddInParameter(command, "@ProductTypeId", SqlDbType.Int, timeAndMaterial.ProductType);

                    returndata = database.ExecuteDataSet(command);
                    var varTimeAndMaterial = from row in returndata.Tables[0].AsEnumerable()
                                    select new TimeAndMaterial
                                    {
                                        Customer = row["Customer"].ToString(),
                                        TimeAndMaterialTerm = row["TimeAndMaterialTerm"].ToString(),
                                        SpecialInstruction = row["SpecialInstruction"].ToString(),
                                        WorkArea = row["WorkArea"].ToString(),
                                        CustomerLocationPics = returndata.Tables[1].AsEnumerable().
                                        Select(aa => new CustomerLocationPic
                                        {
                                            LocationPicture = aa["LocationPicture"].ToString(),
                                            RowSerialNo = Convert.ToInt32(aa["RowSerialNo"].ToString())
                                        }).ToList()

                                    };

                    return varTimeAndMaterial.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
