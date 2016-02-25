using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text.pdf;
using System.Collections;
using iTextSharp.text;
using System.Text;
using iTextSharp;

namespace JG_Prospect.App_Code
{
    public class PDFHelper
    {
        public static Dictionary<string, string> GetFormFieldNames(string pdfPath)
        {
            var fields = new Dictionary<string, string>();

            var reader = new PdfReader(pdfPath);
            foreach (var entry in reader.AcroFields.Fields)
            {
                fields.Add(entry.Key.ToString(), string.Empty);
            }
                               
            reader.Close();

            return fields;
        }
        public static byte[] GeneratePDF(string pdfPath, Dictionary<string, string> formFieldMap)
        {
            var output = new MemoryStream();
            var reader = new PdfReader(pdfPath);
            var stamper = new PdfStamper(reader, output);
            var formFields = stamper.AcroFields;
          

            foreach (var fieldName in formFieldMap.Keys)
                formFields.SetField(fieldName, formFieldMap[fieldName]);

            stamper.FormFlattening = false;
            stamper.Close();
            reader.Close();

            return output.ToArray();
        }
        public static string GetExportValue(AcroFields.Item item)
        {
            var valueDict = item.GetValue(0);
            var appearanceDict = valueDict.GetAsDict(PdfName.AP);

            if (appearanceDict != null)
            {
                var normalAppearances = appearanceDict.GetAsDict(PdfName.N);
                // /D is for the "down" appearances.

                // if there are normal appearances, one key will be "Off", and the other
                // will be the export value... there should only be two.
                if (normalAppearances != null)
                {
                    foreach (var curKey in normalAppearances.Keys)
                        if (!PdfName.OFF.Equals(curKey))
                            return curKey.ToString().Substring(1); // string will have a leading '/' character, so remove it!
                }
            }

            // if that doesn't work, there might be an /AS key, whose value is a name with 
            // the export value, again with a leading '/', so remove it!
            var curVal = valueDict.GetAsName(PdfName.AS);
            if (curVal != null)
                return curVal.ToString().Substring(1);
            else
                return string.Empty;
        }
        public static void ReturnPDF(byte[] contents)
        {
            ReturnPDF(contents,null);
        }
        //public static void ReturnPDF(byte[] contents, string path, string fileName)
        //{
        //    var response = HttpContext.Current.Response;
        //    var document = new Document();
        //    try
        //    {

        //        FileStream FS = new FileStream(path + fileName, FileMode.Create);
        //        //if (!string.IsNullOrEmpty(attachmentFilename))
        //        //    response.AddHeader("Content-Disposition", "attachment; filename=" + attachmentFilename);
        //        response.ContentType = "application/pdf";
        //              response.AddHeader("content-disposition", "attachment; filename=" + fileName);;
        //        PdfWriter.GetInstance(document, FS);
        //        StringBuilder strB = new StringBuilder();
        //        document.Open();
        //        response.BinaryWrite(contents);
        //        response.Write(fileName);
        //        response.End();
        //        response.Clear();
        //        response.Flush();
                   
        //    }
        //    catch (Exception ee)
        //    {
        //        throw ee;
        //        //  lblMessage.Text = ee.ToString();
        //    }
        //    finally
        //    {
        //        //if (document.IsOpen())
        //        //    document.Close();

        //    }
        //}

        public static void ReturnPDF(byte[] contents, string attachmentFilename)
        {
            var response = HttpContext.Current.Response;

            if (!string.IsNullOrEmpty(attachmentFilename))
                response.AddHeader("Content-Disposition", "attachment; filename=" + attachmentFilename);

            response.ContentType = "application/pdf";
            response.BinaryWrite(contents);
            response.End();

        }
           
    }
}