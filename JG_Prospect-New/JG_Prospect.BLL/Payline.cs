using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Configuration;
using System.Web;
/// <summary>
/// Summary description for Payline
/// </summary>
public class Payline
{
    public Payline()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Business Properties and Methods
    public bool IsApproved 
    { 
        get; 
        set; 
    }

    public string AuthorizationCode 
    { 
        get; 
        set; 
    }

    public string Message 
    { 
        get; 
        set; 
    }

    public string Response
    {
        get;
        set;
    }

    public string AuthCaptureId
    {
        get;
        set;
    }

    public string VoidId
    {
        get;
        set;
    }
    public string Token
    {
        get;
        set;
    }
    public string ProfileId
    {
        get;
        set;
    }

    public string PayerID
    {
        get;
        set;
    }

    public string PayerStatus
    {
        get;
        set;
    }

    public string ResponseOut
    {
        get;
        set;
    }
    public string Request
    {
        get;
        set;
    }
   
    #endregion

    


    public Payline Sale(string firstName, string lastName, string accountNumber, string expirationMonth, string expirationYear, string securityCode, Decimal amount, string currency, string address1, int zip, string city,string state,string country)
    {
        Payline payline = new Payline();

       
        System.Web.HttpServerUtility o = System.Web.HttpContext.Current.Server;
        string parmList = "";
       
        parmList = string.Format("username={0}&password={1}&type=sale&ccnumber={2}&ccexp={3}{4}&cvv={5}&amount={6}&first_name={7}&last_name={8}&currency={9}&payment=creditcard&address1={10}&zip={11}&city={12}&state={13}&country={14}", ConfigurationSettings.AppSettings["PlGP:User"], ConfigurationSettings.AppSettings["PlGP:Pwd"], accountNumber, expirationMonth, expirationYear, securityCode, amount, firstName, lastName, currency, address1, zip, city,state,country);

        payline.Request = parmList;

        // create and setup web request
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["PlGP:HostAddress"]);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.ContentLength = parmList.Length;

        StreamWriter requestStream = null;
        try
        {
            requestStream = new StreamWriter(webRequest.GetRequestStream());
            requestStream.Write(parmList);
        }
        catch (Exception ex)
        {
            
            ErrorHandler("Error in GetAllApplicationData function .", ex.Message, ex.StackTrace);
         }
        finally
        {
            requestStream.Close();
        }

        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
        using (StreamReader responseStream = new StreamReader(webResponse.GetResponseStream()))
        {
            ResponseOut = o.UrlDecode(responseStream.ReadToEnd());
            responseStream.Close();
        }

        payline.Response = ResponseOut;

        string[] segments = ResponseOut.Split("&".ToCharArray());
        foreach (string segment in segments)
        {
            if (segment.StartsWith("response="))
                payline.IsApproved = segment.Replace("response=", "").Trim().ToLower() == "1";
            if (segment.StartsWith("responsetext="))
                payline.Message = segment.Replace("responsetext=", "");
            if (segment.StartsWith("authcode="))
                payline.AuthorizationCode = segment.Replace("authcode=", "");
            if (segment.StartsWith("transactionid="))
                payline.AuthCaptureId = segment.Replace("transactionid=", "");
            if (segment.StartsWith("TOKEN="))
                payline.Token = segment.Replace("TOKEN=", "").Trim();
            if (segment.ToUpper().StartsWith("PROFILEID="))
                payline.ProfileId = segment.ToUpper().Replace("PROFILEID=", "").Trim();
            if (segment.ToUpper().StartsWith("PAYERID="))
                payline.PayerID = segment.ToUpper().Replace("PAYERID=", "").Trim();
            if (segment.ToUpper().StartsWith("PAYERSTATUS="))
                payline.PayerStatus = segment.ToUpper().Replace("PAYERSTATUS=", "").Trim();
        }

        return payline;
    }


    public static void ErrorHandler(string methodName, string exceptionMessage, string stackTrace)
    {
        try
        {
            string dTLogDate = DateTime.Now.ToString("yyyy-MM-dd");

            string folderPath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFolder"];

            bool isExists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(folderPath));

            if (!isExists)
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderPath));

            string filePath = HttpContext.Current.Server.MapPath(folderPath + "/" + dTLogDate + ".txt");

            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            TextWriter(filePath, methodName, exceptionMessage, stackTrace);
        }
        catch (Exception ex)
        {
            //Write down code to send an alert email to ADMIN
        }
    }
    private static void TextWriter(string filePath, string methodName, string exceptionMessage, string stackTrace)
    {
        TextWriter sw = new StreamWriter(filePath, true);

        sw.WriteLine("========================================");

        sw.WriteLine("Method: " + methodName);
        sw.WriteLine("Time: " + DateTime.Now.TimeOfDay);
        sw.WriteLine("Exception: " + exceptionMessage);
        sw.WriteLine("Destination IP Address:" + GetDestinationIP());
        sw.WriteLine("Stack Trace:" + stackTrace);

        sw.WriteLine("========================================");

        sw.Dispose();
        sw.Close();
    }
    private static string GetDestinationIP()
    {
        string strHostName = "";
        strHostName = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
        string ipaddress = Convert.ToString(ipEntry.AddressList[2]);

        return ipaddress.ToString();
    }
}