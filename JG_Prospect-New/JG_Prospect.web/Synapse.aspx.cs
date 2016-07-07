using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
namespace JG_Prospect
{
    public partial class Synapse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sandbox.synapsepay.com/api/v2/user/create");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"email\":\"aaaanikuaaaanjaa@synapsepay.com\"," +
                              "\"fullname\":\"nik\"," +
                              "\"phonenumber\":\"111\"," +
                              "\"ip_address\":\"121.247.24.98\"," +
                              "\"password\":\"123123123\"," +
                              "\"client_id\":\"1111111111111111\"," +
                              "\"client_secret\":\"2222222222222222222\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        
        }
    }
}