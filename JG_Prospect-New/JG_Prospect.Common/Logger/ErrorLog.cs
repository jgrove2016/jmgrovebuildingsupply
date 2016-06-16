using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO ;
/// <summary>
/// Summary description for ErrorLog
/// </summary>
/// 
namespace JG_Prospect.Common.Logger
{
    public class ErrorLog
    {
        private static ErrorLog m_LogManager = new ErrorLog();
        string PageName, FILENAME;

        public static ErrorLog Instance
        {
            get { return m_LogManager; }
            private set { }
        }

        public void writeToLog(Exception ex, string page, string clientIP)
        {
            FILENAME = ConfigurationManager.AppSettings.Get("logfile");
            PageName = page;
            StreamWriter objStreamWriter;
            if (File.Exists(FILENAME))
            {
                FileInfo fInfo = new FileInfo(FILENAME);
                long fileSize = (((fInfo.Length) / 1024) / 1024);  //in mb
                //long fileSize = ((fInfo.Length));  //in mb
                if (fileSize < 2)
                {
                    objStreamWriter = File.AppendText(FILENAME);
                    write(objStreamWriter, ex, PageName, clientIP);
                }
                else
                {
                    File.Move(FILENAME, FILENAME.Remove(FILENAME.Length - 4, 4) + "_" + DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "." + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + ".txt");
                    objStreamWriter = File.CreateText(FILENAME);
                    write(objStreamWriter, ex, PageName, clientIP);
                }
            }
            else
            {
                //objStreamWriter = File.CreateText(FILENAME);
                //write(objStreamWriter, ex, PageName, clientIP);
            }
        }
        protected void write(StreamWriter sw, Exception ex, string page, string clientIP)
        {
            sw.WriteLine("--------------------------------------------------------------------------");
            sw.WriteLine("Client IP ::" + clientIP);
            sw.WriteLine("Error logged :: " + DateTime.Now.ToString());
            sw.WriteLine("Error Is :: " + ex.Message.ToString());
            sw.WriteLine("Function Is :: " + ex.TargetSite.ToString());
            sw.WriteLine("Assembly Is :: " + ex.Source.ToString());
            sw.WriteLine("Error Type :: " + ex.GetType().Name.ToString());
            sw.WriteLine("Error on Page :: " + page);
            sw.WriteLine("StackTrace :: " + ex.StackTrace);
            sw.Close();
            sw.Dispose();
        }
    }
}
