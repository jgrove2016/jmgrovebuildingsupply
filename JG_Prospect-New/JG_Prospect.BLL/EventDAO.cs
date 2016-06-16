using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
/// 
namespace JG_Prospect.JGCalender
{
    public class EventDAO
    {
        //change the connection string as per your database connection.
        private static string connectionString = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;

        //GetCustomers
        public static List<DropdownObject> getCustomers()
        {
            List<DropdownObject> customers = new List<DropdownObject>();
            SqlConnection con = new SqlConnection(connectionString);
            StringBuilder sb = new StringBuilder();
            sb.Append("  select Id as id,case when (isnull(FirstName,'')+' '+isnull(LastName,''))='' then Email end as name,* from tblUsers where Designation in ('JSE','SSE','SM') and [Status] = 'Active' ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), con);
            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new DropdownObject() { id = (int)reader["id"], name = (string)reader["name"] });
                }
            }
            return customers;
        }


        public static List<DropdownObject> getCustomerList()
        {
            List<DropdownObject> customers = new List<DropdownObject>();
            SqlConnection con = new SqlConnection(connectionString);
            StringBuilder sb = new StringBuilder();
            sb.Append("select id,CustomerName from new_customer  ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), con);
            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID"] != DBNull.Value && reader["CustomerName"] != DBNull.Value)
                    {
                        customers.Add(new DropdownObject() { id = (int)reader["id"], name = (string)reader["CustomerName"] });
                    }
                }
            }
            return customers;
        }


        //this method retrieves all events within range start-end
        public static List<CalendarEvent> getEvents(DateTime start, DateTime end, string userIds, string searchFilter)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection con = new SqlConnection(connectionString);
            //SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end, all_day FROM ECICalendarEvent_Test where event_start>=@start AND event_end<=@end", con);
            StringBuilder sb = new StringBuilder();


            sb.Append("  SELECT f.Id as event_id,   ");
            sb.Append("  isnull((Cast( c.id as varchar(10)) +' ## Last Name: '+ c.LastName+' ## Contact: '+ ISNULL(c.PrimaryContact,'')+' ## Address: '   ");
            sb.Append("  + c.CustomerAddress+' ## Zip: '+ c.ZipCode+' ## Status: '+ f.MeetingStatus+ ' ## Product ' +cast( isnull(p.ProductName,'')  as varchar(10))),'') as  description,  ");
            sb.Append("  isnull((Cast( c.id as varchar(10)) +' ## Last Name: '+ c.LastName+' ## Contact: '+ISNULL(c.PrimaryContact,'')+' ## Address: '   ");
            sb.Append("  + c.CustomerAddress+' ## Zip: '+ c.ZipCode+' ## Status: '+ f.MeetingStatus+ ' ## Product ' +cast( isnull(p.ProductName,'')  as varchar(10))),'') as  title,  ");
            sb.Append("  MeetingDate as  event_start,  ");
            sb.Append("  DATEADD(hour,1,MeetingDate) as event_end,   ");
            sb.Append("  0 as all_day,   ");
            sb.Append("  f.MeetingStatus  as status,c.id, c.lastname, ISNULL(c.PrimaryContact,'') AS PrimaryContact, c.CustomerAddress,c.ZipCode  , isnull(p.ProductName,'') as ProductName  ");
            sb.Append("  FROM tblcustomer_followup f   ");
            sb.Append("  left join new_customer c on c.id=f.CustomerId   ");
            sb.Append("  left join tblProductMaster p on p.ProductId=f.ProductId   ");
            sb.Append(" where f.MeetingDate between @start and @end   ");
            if (userIds != "")
                sb.Append(" and f.UserId  in (" + userIds + ")");

            /*Jayanti
             * sb.Append("     ");
             sb.Append("  SELECT f.MeetingStatus as status, f.Id as event_id, t.*,   ");
             sb.Append("  description AS title,   ");
             sb.Append("  DATEADD(hour,1,event_start) AS event_end   ");
             sb.Append("  FROM   ");
             sb.Append("  (    ");
             sb.Append("  SELECT c.AddedBy,  c.id AS cid,   ");
             sb.Append("  isnull((Cast(c.id AS varchar(10)) +' ## Last Name: '+ c.LastName+' ## Contact: '+c.PrimaryContact+' ## Address: ' + c.CustomerAddress+' ## Zip: '+ c.ZipCode),'') AS description,   ");
             sb.Append("  cast((cast(c.EstDateSchdule AS varchar(20))+' '+c.EstTime) AS datetime) AS event_start,   ");
             sb.Append("  0 AS all_day   ");
             sb.Append("  FROM new_customer c)t   ");
             sb.Append("  join tblUsers u on u.Login_Id=t.AddedBy    ");
             sb.Append("  left join tblcustomer_followup f on f.CustomerId=t.cid    ");
             sb.Append("  WHERE t.event_start between @start and @end   ");
          
             * 
              if (userIds != "")
                 sb.Append(" and u.Id  in (" + userIds + ")");*/



            SqlCommand cmd = new SqlCommand(sb.ToString(), con);

            cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            foreach (DataRow reader in dt.Rows)
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = (int)reader["event_id"];
                cevent.title = ((string)reader["title"]).Replace("##", ", "); ;
                cevent.description = ((string)reader["description"]).Replace("##", "<br>");
                cevent.start = (DateTime)reader["event_start"];
                cevent.end = (DateTime)reader["event_end"];
                cevent.allDay = Convert.ToBoolean(reader["all_day"]);
                cevent.status = reader["status"].ToString();
                cevent.customerid = Convert.ToInt32(reader["id"]);
                cevent.lastname = reader["lastname"].ToString();
                cevent.primarycontact = reader["PrimaryContact"].ToString();
                cevent.address = reader["customeraddress"].ToString();
                cevent.zipcode = reader["zipcode"].ToString();
                cevent.productline = reader["productname"].ToString();
                if (string.IsNullOrEmpty(searchFilter) || searchFilter.Trim().Length == 0 || cevent.description.ToLower().Contains(searchFilter.ToLower().Trim()))
                {
                    events.Add(cevent);
                }
            }
            return events;
        }

        //this method updates the event title and description
        public static void updateEvent(int id, String title, String description, String status)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE tblcustomer_followup SET MeetingStatus=@status WHERE Id=@event_id", con);
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //this method updates the event start and end time ... allDay parameter added for FullCalendar 2.x
        public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE tblcustomer_followup SET MeetingDate=@event_start WHERE Id=@event_id", con);
            cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = end;
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = allDay ? 1 : 0;

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //this mehtod deletes event with the id passed in.
        public static void deleteEvent(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM ECICalendarEvent_Test WHERE (event_id = @event_id)", con);
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //this method adds events to the database
        public static int addEvent(CalendarEvent cevent)
        {
            //add event to the database and return the primary key of the added event row

            //insert
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO ECICalendarEvent_Test(title, description, event_start, event_end, all_day) VALUES(@title, @description, @event_start, @event_end, @all_day)", con);
            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
            cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
            cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
            cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;

            int key = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();

                //get primary key of inserted row
                cmd = new SqlCommand("SELECT max(event_id) FROM ECICalendarEvent_Test where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end AND all_day=@all_day", con);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;

                key = (int)cmd.ExecuteScalar();
            }

            return key;
        }
    }


}