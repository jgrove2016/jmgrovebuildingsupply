using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Calendar;
using EventEntry = Google.GData.Calendar.EventEntry;

namespace JG_Prospect.BLL
{
    public class GoogleCalendar
    {
        private const string CALENDARS_URL = "https://www.google.com/calendar/feeds/default/owncalendars/full";
        private const string CALENDAR_TEMPLATE = "https://www.google.com/calendar/feeds{0}/private/full";
        private string m_CalendarUrl = null;
        private string m_CalendarId = null;
        private readonly CalendarService m_Service = null;

        private readonly string m_CalendarName;
        private readonly string m_UserName;
        private readonly string m_Password;



        public GoogleCalendar(string calendarName, string username, string password)
        {
            try
            {
                m_CalendarName = calendarName;
                m_UserName = username;
                m_Password = password;
                m_Service = new CalendarService("Calendar");


                if (Authenticate())
                {
                }
            }
            catch (Exception ex1)
            {
                //LogManager.Instance.WriteToFlatFile("step1"+ex1.Message);
            }

        }

        public CalendarEventObject[] GetEvents()
        {
            try
            {
                if (Authenticate())
                {
                    EventQuery query = new EventQuery(m_CalendarUrl);
                    EventFeed feed = m_Service.Query(query);
                    return (from EventEntry entry in feed.Entries
                            select new CalendarEventObject() { Date = entry.Times[0].StartTime, Title = entry.Title.Text }).ToArray();
                }
                else
                {
                    return new CalendarEventObject[0];
                }
            }
            catch (Exception)
            {
                return new CalendarEventObject[0];
            }
        }

        private bool Authenticate()
        {
            try
            {
                m_Service.setUserCredentials(m_UserName, m_Password);
            }
            catch (Exception ex2)
            {
                //LogManager.Instance.WriteToFlatFile("step11" + ex2.Message);
            }
            return SaveCalendarIdAndUrl();
        }

        private bool SaveCalendarIdAndUrl()
        {
            CalendarFeed resultFeed = null;

            CalendarService myService = new CalendarService("MyCalendar-1");
            m_Service.setUserCredentials(m_UserName, m_Password);

            CalendarQuery query = new CalendarQuery();
            // query.Uri = new Uri(CALENDARS_URL);
            query.Uri = new Uri("https://www.google.com/calendar/feeds/default/allcalendars/full");

            resultFeed = (CalendarFeed)m_Service.Query(query);


            foreach (CalendarEntry entry in resultFeed.Entries)
            {
                //LogManager.Instance.WriteToFlatFile(entry.Title.Text);
                //LogManager.Instance.WriteToFlatFile(m_CalendarName);
                if (entry.Title.Text == m_CalendarName)
                {
                    int p = entry.Id.AbsoluteUri.IndexOf("full/");
                    m_CalendarId = entry.Id.AbsoluteUri.Substring(p + 5);
                      //m_CalendarId = entry.Id.AbsoluteUri.Substring(63);
                    m_CalendarUrl = string.Format(CALENDAR_TEMPLATE, m_CalendarId);
                    //"https://www.google.com/calendar/feeds/" + m_CalendarId + "/private/full";// string.Format(CALENDAR_TEMPLATE, m_CalendarId);

                    return true;
                }
            }

            return false;
        }


        //public string SaveCalendarId(string calendarName, string username, string password)
        //{
        //    m_Service.setUserCredentials(username, password);
        //    CalendarQuery query = new CalendarQuery();
        //    query.Uri = new Uri(CALENDARS_URL);
        //    CalendarFeed resultFeed = (CalendarFeed)m_Service.Query(query);

        //    foreach (CalendarEntry entry in resultFeed.Entries)
        //    {
        //        if (entry.Title.Text == calendarName)
        //        {
        //            m_CalendarId = entry.Id.AbsoluteUri.Substring(63);                   
        //            return m_CalendarId;
        //        }
        //    }
        //    return m_CalendarId;
        //}

        public string GetCalendarId()
        {
            return m_CalendarId;
        }

    }

    public class CalendarEventObject
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}
