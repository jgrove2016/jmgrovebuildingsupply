<%@ WebHandler Language="C#" Class="JG_Prospect.JGCalender.Customer" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;

namespace JG_Prospect.JGCalender
{
    public class Customer : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //Serialize events to string
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            string sJSON = string.Empty;
            string type = context.Request.QueryString["type"];
            if (type != null && type == "customer")
            {
                sJSON = oSerializer.Serialize(EventDAO.getCustomerList());
            }
            else
            {
                sJSON = oSerializer.Serialize(EventDAO.getCustomers());
            }
            
            

            //Write JSON to response object
            context.Response.Write(sJSON);
        }

        public bool IsReusable
        {
            get { return false; }
        }

    }
}