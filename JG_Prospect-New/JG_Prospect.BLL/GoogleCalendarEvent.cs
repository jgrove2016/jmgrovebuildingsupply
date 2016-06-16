using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Google.GData.Calendar;
using EventEntry = Google.GData.Calendar.EventEntry;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Net;
using System.Configuration;
using Google.GData.AccessControl;

namespace JG_Prospect.BLL
{
    public class GoogleCalendarEvent
    {
        public static CalendarService GetService(string applicationName, string userName, string password)
        {
            CalendarService service = new CalendarService(applicationName);
            try
            {

                service.setUserCredentials(userName, password);

            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex.Message);
            }
            return service;
        }
        static string AdminuserName = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
        static string AdminuserPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
        //static string user1name = ConfigurationManager.AppSettings["CalendarUser1"].ToString();
        //static string user1pwd = ConfigurationManager.AppSettings["Calendarpass1"].ToString();
        public static void AddEvent1(CalendarService service, string id, string title, string contents, string location, DateTime startTime, DateTime endTime, string calendarName,string emailId)
        {
            try
            {

                Google.GData.Calendar.EventEntry entry = new Google.GData.Calendar.EventEntry();
                // Set the title and content of the entry.
                entry.Title.Text = id + "-" + title;
                entry.Content.Content = contents;

                // Set a location for the event.
                Where eventLocation = new Where();
                eventLocation.ValueString = location;
                entry.Locations.Add(eventLocation);

                When eventTime = new When(startTime, endTime);
                entry.Times.Add(eventTime);
                GoogleCalendar ggadmin = new GoogleCalendar(calendarName, AdminuserName, AdminuserPwd);
               // string CalendarId = ggadmin.GetCalendarId();

                //CalendarService Calservice = new CalendarService("CalendarSampleApp");
                AclEntry aclEntry = new AclEntry();

                aclEntry.Scope = new AclScope();
                aclEntry.Scope.Type = AclScope.SCOPE_USER;
                aclEntry.Scope.Value = emailId;
                aclEntry.Role = AclRole.ACL_CALENDAR_READ;
               // Uri postUri = new Uri("https://www.google.com/calendar/feeds/" + CalendarId + "/private/full");

                Uri aclUri = new Uri(string.Format("https://www.google.com/calendar/feeds/{0}/acl/full", service.Credentials.Username.ToString()));

               


                GDataGAuthRequestFactory requestFactory = (GDataGAuthRequestFactory)service.RequestFactory;
                IWebProxy iProxy = WebRequest.GetSystemWebProxy();
                WebProxy myProxy = new WebProxy();
                // potentially, setup credentials on the proxy here
                myProxy.Credentials = CredentialCache.DefaultCredentials;
                myProxy.UseDefaultCredentials = false;

              // requestFactory.CreateRequest(GDataRequestType.Insert, postUri);//  = myProxy;
                // Send the request and receive the response:
                //AtomEntry insertEntry = service.Insert(postUri, entry);

                AclEntry insertedEntry = service.Insert(aclUri, aclEntry) as AclEntry;
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex.Message);
            }
        }
        public static void AddEvent(CalendarService service, string id, string title, string contents, string location, DateTime startTime, DateTime endTime, string calendarName)
        {
            try
            {

                Google.GData.Calendar.EventEntry entry = new Google.GData.Calendar.EventEntry();
                // Set the title and content of the entry.
                entry.Title.Text = id + "-" + title;
                entry.Content.Content = contents;

                // Set a location for the event.
                Where eventLocation = new Where();
                eventLocation.ValueString = location;
                entry.Locations.Add(eventLocation);

                When eventTime = new When(startTime, endTime);
                entry.Times.Add(eventTime);
                GoogleCalendar ggadmin = new GoogleCalendar(calendarName, AdminuserName, AdminuserPwd);
                string CalendarId = ggadmin.GetCalendarId();


                Uri postUri = new Uri("https://www.google.com/calendar/feeds/" + CalendarId + "/private/full");

                
                GDataGAuthRequestFactory requestFactory = (GDataGAuthRequestFactory)service.RequestFactory;
                IWebProxy iProxy = WebRequest.GetSystemWebProxy();
                WebProxy myProxy = new WebProxy();
                // potentially, setup credentials on the proxy here
                myProxy.Credentials = CredentialCache.DefaultCredentials;
                myProxy.UseDefaultCredentials = false;

                requestFactory.CreateRequest(GDataRequestType.Insert, postUri);//  = myProxy;
                // Send the request and receive the response:
                AtomEntry insertedEntry = service.Insert(postUri, entry);

            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile(ex.Message);
            }
        }

        public static void EditEvent(string oldTitle, string newTitle, string contents, string location, DateTime startTime, DateTime endTime, string calendarname)
        {
            GoogleCalendar ggadmin = new GoogleCalendar(calendarname, AdminuserName, AdminuserPwd);
            string CalendarId = ggadmin.GetCalendarId();
            EventQuery query = new EventQuery("https://www.google.com/calendar/feeds/" + CalendarId + "/private/full");
            query.Query = oldTitle;
            string[] oldtitle = oldTitle.Split('-');
            //query.StartDate = startTime;
            //query.EndDate = endTime;
            EventFeed feed = GetService(AdminuserName, AdminuserName, AdminuserPwd).Query(query);
            foreach (CalendarEntry entry in feed.Entries)
            {
                string[] item = entry.Title.Text.Split('-');

                //if (oldTitle.Equals(entry.Title.Text))
                //{
                //    entry.Delete(); break;                    
                //}
                if (oldTitle[0].ToString() == item[0].ToString())
                {
                    entry.Delete();
                    break;
                }
            }

            //if (feed.Entries.Count > 0)
            //{
            //    AtomEntry firstMatchEntry = feed.Entries[0];
            //    String myEntryTitle = firstMatchEntry.Title.Text;
            //}
            // Create an batch entry to update an existing event.
            //EventEntry toDelete = (EventEntry)feed.Entries[0];
            //toDelete.Title.Text = oldTitle;// "updated First New";

            //toUpdate.Content.Content = contents;

            //Where eventLocation = new Where();
            //eventLocation.ValueString = location;
            //toUpdate.Locations.Clear();
            //toUpdate.Locations.Add(eventLocation);


            //When eventTime = new When(startTime, endTime);
            //toUpdate.Locations.Clear();
            //toUpdate.Times.Add(eventTime);           
            //toUpdate.Update();
            // toDelete.Delete();



        }

        public static bool DeleteEvent(string oldTitle, string newTitle, string contents, string location, DateTime startTime, DateTime endTime, string calendarname)
        {
            bool result = false;
            try
            {
                GoogleCalendar ggadmin = new GoogleCalendar(calendarname, AdminuserName, AdminuserPwd);
                string CalendarId = ggadmin.GetCalendarId();
                Uri oCalendarUri = new Uri("http://www.google.com/calendar/feeds/" + CalendarId + "/private/full");
                string[] oldtitle = oldTitle.Split('-');
                //Search for Event
                EventQuery oEventQuery = new EventQuery(oCalendarUri.ToString());
                //oEventQuery.Query = oldTitle;
                //oEventQuery.StartDate = startTime;
                //oEventQuery.EndDate = startTime.AddDays(1);
                Google.GData.Calendar.EventFeed oEventFeed = GetService("GoogleCalendar", AdminuserName, AdminuserPwd).Query(oEventQuery);

                //Delete Related Events
                foreach (EventEntry oEventEntry in oEventFeed.Entries)
                {
                    //Do your stuff here
                    string[] item = oEventEntry.Title.Text.Split('-');
                    if (oldtitle[0].ToString().Trim() == item[0].ToString().Trim())
                    {
                        oEventEntry.Delete();
                    }
                }
                result =true ;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }


        public static void DeleteEventfromall(string oldTitle,string newTitle,string contents,string location,DateTime startTime,DateTime endTime,string calendarname)
        {
            GoogleCalendar ggadmin = new GoogleCalendar(calendarname, AdminuserName, AdminuserPwd);            
            string CalendarId = ggadmin.GetCalendarId();


            Uri oCalendarUri = new Uri("http://www.google.com/calendar/feeds/" + CalendarId + "/private/full");
            string[] oldtitle = oldTitle.Split('-');
            //Search for Event
            EventQuery oEventQuery = new EventQuery(oCalendarUri.ToString());
            //oEventQuery.Query = oldTitle;
            //oEventQuery.StartDate = startTime;
            //oEventQuery.EndDate = startTime.AddDays(1);
            Google.GData.Calendar.EventFeed oEventFeed = GetService("GoogleCalendar", AdminuserName, AdminuserPwd).Query(oEventQuery);

            //Delete Related Events
            foreach (EventEntry oEventEntry in oEventFeed.Entries)
            {
                //Do your stuff here
                string[] item = oEventEntry.Title.Text.Split('-');
                if (oldtitle[0].ToString() == item[0].ToString())
                {
                    oEventEntry.Delete();
                }
            }
        }


        public static void CreateCalendar(string calendarName, string Location)
        {
            try
            {
                string AdminuserName = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                string AdminuserPwd =ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                CalendarEntry calendar = new CalendarEntry();
                calendar.Title.Text = calendarName;
                calendar.Summary.Text = "This calendar contains the prospects.";
                //calendar.TimeZone = "America/Los_Angeles";
                //calendar.Hidden = false;
                calendar.Color = "#2952A3";
                calendar.Location = new Where("", "", "London");
                TimeSpan tm = new TimeSpan(29, 10, 0, 0);
                calendar.Published.Date.Add(tm);                
                //  calendar.Id = new AtomId("c4o4i7m2lbamc4k26sc2vokh5g%40group.calendar.google.com");
                Uri postUri = new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full");
                CalendarEntry createdCalendar = (CalendarEntry)GetService("GoogleCalendar", AdminuserName, AdminuserPwd).Insert(postUri, calendar);
                GCalAccessLevel acc = new GCalAccessLevel();                
               
            }
            catch (Exception)
            {
            }
        }

    }
}
