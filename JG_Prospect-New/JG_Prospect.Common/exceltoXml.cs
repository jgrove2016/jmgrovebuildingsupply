using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Xml;
//using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace JG_Prospect.Common
{
    public class exceltoXml
    {
        /// <summary>
        /// Returns the given Excel file as an XML document object.
        /// If the firstRowIsHeader flag is TRUE then it will read
        /// the first row as column names and it will reflect as each
        /// XML node name.
        /// </summary>
        /// <param name="pathToExcelFile">full path including file name to Excel file</param>
        /// <param name="firstRowIsHeader">set to true if first row contains column names</param>
        /// <returns></returns>
       /*
        private static XmlDocument GetExcelAsXMLDoc(string pathToExcelFile, bool firstRowIsHeader)
        {
            DataSet excelAsDataset = GetExcelAsDataSet(pathToExcelFile, firstRowIsHeader);
            string inputXML = excelAsDataset.GetXml();
            XmlDocument returnDoc = new XmlDocument();
            returnDoc.LoadXml(inputXML);
            return returnDoc;
        }
        */

        /// <summary>
        /// Returns the first sheet in the workbook contained in the given Excel fileName
        /// as a DataSet.
        /// </summary>
        /// <param name="fileName">full path including file name for input Excel file</param>
        /// <param name="firstRowIsHeader">set to true if first row contains header field names</param>
        /// <returns></returns>
        /// 

        public static DataSet getdata(string filename)
        {
            try
            {
                string strmail = string.Empty;
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=Excel 12.0;";
                OleDbConnection objConn = new OleDbConnection(connectionString);
                objConn.Open();
                String strConString = "SELECT * FROM [Sheet1$]";
                //where date = CDate('" + DateTime.Today.ToShortDateString() + "')";
                OleDbCommand objCmdSelect = new OleDbCommand(strConString, objConn);
                // Create new OleDbDataAdapter that is used to build a DataSet
                // based on the preceding SQL SELECT statement.
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                // Pass the Select command to the adapter.
                objAdapter1.SelectCommand = objCmdSelect;
                // Create new DataSet to hold information from the worksheet.
                DataSet objDataset1 = new DataSet();
                // Fill the DataSet with the information from the worksheet.
                objAdapter1.Fill(objDataset1, "ExcelData");
                return objDataset1;
                // Clean up objects.
                objConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //byte[] ar = new byte[(int)fs.Length];
            //fs.Read(ar, 0, (int)fs.Length);
            //fs.Close();

            //Response.AddHeader("content-disposition", "attachment;filename=" + Guid.NewGuid().ToString().Substring(0, 4) + freightupload.FileName);
            //Response.ContentType = "application/excel";
            //Response.BinaryWrite(ar);
            //Response.End();
        }

        /*
        public static DataSet GetExcelAsDataSet(string fileName, bool firstRowIsHeader)
        {
            Application oXL;
            Workbook oWB;
            Worksheet oSheet;
            Range oRng;
            try
            {
                //  creat a Application object
                oXL = new Application();
                //   get   WorkBook  object
                oWB = oXL.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value);

                //   get   WorkSheet object 
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Sheets[1];
                System.Data.DataTable dt = new System.Data.DataTable("RowItem");
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                DataRow dr;

                StringBuilder sb = new StringBuilder();
                int kValue = oSheet.Application.UsedObjects.Count;

                int jValue = oSheet.UsedRange.Cells.Columns.Count;
                int iValue = oSheet.UsedRange.Cells.Rows.Count;
                //  get data columns
                for (int j = 1; j <= jValue; j++)
                {
                    dt.Columns.Add("column" + j, System.Type.GetType("System.String"));
                }

                //  get data in cell.
                // If the user set the firstRowIsHeader flag then save these cell values
                // as column names instead of actual row values.
                for (int i = 1; i <= iValue; i++)
                {
                    dr = dt.NewRow();
                    for (int j = 1; j <= jValue; j++)
                    {
                        oRng = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[i, j];
                        string strValue = oRng.Text.ToString();
                        if (strValue == "")
                        {
                            strValue = "0";
                        }
                        if (firstRowIsHeader && i == 1)
                        {
                            dt.Columns[j - 1].Caption = strValue;
                        }
                        else
                        {
                            dr["column" + j] = strValue.Trim();
                        }

                    }

                    //don't add an empty row if we are just reading in header field names
                    if (!(firstRowIsHeader && i == 1))
                    {
                        dt.Rows.Add(dr);
                    }
                }

                //now rename the column names to the header field names
                //if the user said the first row was a header.
                if (firstRowIsHeader)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        dt.Columns[k].ColumnName = (dt.Columns[k].Caption.Replace(" ", ""));
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                //log or print your exception!
                throw ex;
            }
            finally
            {
                //clean up file handles and objects
            }
        }
        */
       
    }
}
