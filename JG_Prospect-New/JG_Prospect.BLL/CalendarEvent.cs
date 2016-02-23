using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JG_Prospect.JGCalender
{
    /// <summary>
    /// Summary description for CalendarEvent
    /// </summary>
    public class CalendarEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool allDay { get; set; }
    }
    public class DropdownObject
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}