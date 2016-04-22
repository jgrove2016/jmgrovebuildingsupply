using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JG_Prospect.JGCalender
{
    //Do not use this object, it is used just as a go between between javascript and asp.net
    public class ImproperCalendarEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string status { get; set; }
        public int customerid { get; set; }
        public string lastname { get; set; }
        public string primarycontact { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string productline { get; set; }
        public string addedby { get; set; }
    }
}