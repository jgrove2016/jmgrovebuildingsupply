<%@ WebHandler Language="C#" Class="JG_Prospect.JGCalender.JsonResponse" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web.Script.Serialization;

namespace JG_Prospect.JGCalender
{
    public class JsonResponse : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DateTime start = Convert.ToDateTime(context.Request.QueryString["start"]);
            DateTime end = Convert.ToDateTime(context.Request.QueryString["end"]);

            string userIds = context.Request.QueryString["eIds"];
            var iSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var s = iSerializer.Deserialize<List<int>>(userIds);
            var iDs = "";
            var searchFilter= string.Empty;
            searchFilter = context.Request.QueryString["searchText"];
            if (s != null)
                iDs = string.Join(",", s);
            
            List<int> idList = new List<int>();
            List<ImproperCalendarEvent> tasksList = new List<ImproperCalendarEvent>();

            //Generate JSON serializable events
            foreach (CalendarEvent cevent in EventDAO.getEvents(start, end, iDs, searchFilter))
            {
                tasksList.Add(new ImproperCalendarEvent
                {
                    id = cevent.id,
                    title = cevent.title,
                    start = String.Format("{0:s}", cevent.start),
                    end = String.Format("{0:s}", cevent.end),
                    description = cevent.description,
                    allDay = cevent.allDay,
                    status = cevent.status
                }
                    );
                idList.Add(cevent.id);
            }

            context.Session["idList"] = idList;

            //Serialize events to string
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(tasksList);

            //Write JSON to response object
            context.Response.Write(sJSON);
        }

        public bool IsReusable
        {
            get { return false; }
        }

    }

}