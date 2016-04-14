using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Net;
using System.Xml.Serialization;
using System.Web.UI.WebControls;
using System.Configuration;
using JG_Prospect.BLL;

namespace JG_Prospect.calendar
{
    public partial class calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (Session["loginid"] != null)
            {
                clid.Value = GetCID(Convert.ToString(Session["loginid"]));               
               // CreateControls(5);                              
            }          
          //  WeatherControl();
        }
        private void WeatherControl()  // To show whether of five days
        {
            XmlDocument xmlDoc = new XmlDocument();
            string path = Server.MapPath("~/calendar");
            try
            {
                xmlDoc.Load("http://api.worldweatheronline.com/free/v1/weather.ashx?q=Manayunk&format=xml&num_of_days=5&key=ts9ta6wft8y8ggjz3z3wrugm");
                xmlDoc.Save(path + "/wheather.xml");
            }
            catch (Exception ex)
            {
                xmlDoc.Load(path + "/wheather.xml");
            }          
            //XmlNodeList nodeList = xmlDoc.ChildNodes;
            XmlNodeList weather = xmlDoc.GetElementsByTagName("weather");
            XmlNodeList observation_time = xmlDoc.GetElementsByTagName("observation_time");
            XmlNodeList humidity = xmlDoc.GetElementsByTagName("humidity");
            XmlNodeList pressure = xmlDoc.GetElementsByTagName("pressure");
            XmlNodeList cloudcover = xmlDoc.GetElementsByTagName("cloudcover");

            XmlNodeList date = xmlDoc.GetElementsByTagName("date");
            XmlNodeList query = xmlDoc.GetElementsByTagName("query");
            //XmlNodeList date = xmlDoc.GetElementsByTagName("date");
            XmlNodeList temp_C = xmlDoc.GetElementsByTagName("temp_C");
            XmlNodeList tempMaxF = xmlDoc.GetElementsByTagName("tempMaxF");
            XmlNodeList tempMinF = xmlDoc.GetElementsByTagName("tempMinF");
            XmlNodeList weatherIconUrl = xmlDoc.GetElementsByTagName("weatherIconUrl");
            // XmlNodeList tempMaxC = xmlDoc.GetElementsByTagName("tempMaxC");
            XmlNodeList windspeedKmph = xmlDoc.GetElementsByTagName("windspeedKmph");
            XmlNodeList windspeedMiles = xmlDoc.GetElementsByTagName("windspeedMiles");
            XmlNodeList weatherCode = xmlDoc.GetElementsByTagName("weatherCode");
            XmlNodeList winddirection = xmlDoc.GetElementsByTagName("winddirection");
            XmlNodeList winddir16Point = xmlDoc.GetElementsByTagName("winddir16Point");
            XmlNodeList winddirDegree = xmlDoc.GetElementsByTagName("winddirDegree");
            XmlNodeList weatherDesc = xmlDoc.GetElementsByTagName("weatherDesc");
            XmlNodeList precipMM = xmlDoc.GetElementsByTagName("precipMM");
            //  ((Label)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("ddlAttendTech" + i)).SelectedValue;
            Labellocation.Text = query[0].InnerText;
            for (int c = 0; c < 5; c++)
            {
                Label lbldate = ((Label)this.Page.FindControl("lbldate" + c));
                Label lbltempMaxF = ((Label)this.Page.FindControl("lbltempMaxF" + c));
                Label lbltempMinF = ((Label)this.Page.FindControl("lbltempMinF" + c));
                Label lblwindspeedKmph = ((Label)this.Page.FindControl("lblwindspeedKmph" + c));
                Label lblwindspeedMiles = ((Label)this.Page.FindControl("lblwindspeedMiles" + c));
                Label lblweatherCode = ((Label)this.Page.FindControl("lblweatherCode" + c));
                Label lblwinddirection = ((Label)this.Page.FindControl("lblwinddirection" + c));
                Label lblwinddir16Point = ((Label)this.Page.FindControl("lblwinddir16Point" + c));
                //Label lblwinddirDegree = ((Label)this.Page.FindControl("lblwinddirDegree" + c));
                Label lblweatherDesc = ((Label)this.Page.FindControl("lblweatherDesc" + c));
                Label lblprecipMM = ((Label)this.Page.FindControl("lblprecipMM" + c));

                //Image imgweatherIconUrl = ((Image)this.Page.FindControl("imgweatherIconUrl" + c));
                //imgweatherIconUrl.ImageUrl = weatherIconUrl[c].InnerText;
                string strd = date[c].InnerText;
                DateTime dt = Convert.ToDateTime(strd);
                string Fdate = dt.ToString("ddd, MMM d, yyyy");
                lbldate.Text = Fdate;
                lbltempMaxF.Text = tempMaxF[c].InnerText;
                lbltempMinF.Text = tempMinF[c].InnerText;
                //lblwindspeedMiles.Text = windspeedMiles[c].InnerText;
                //lblwindspeedKmph.Text = windspeedKmph[c].InnerText;
                lblweatherDesc.Text = weatherDesc[c].InnerText;
                //lblweatherCode.Text = weatherCode[c].InnerText;
                //lblwinddirection.Text = winddirection[c].InnerText;
                lblprecipMM.Text = precipMM[c].InnerText;
                //lblwinddir16Point.Text = winddir16Point[c].InnerText;
            }
        
        }
        private void CreateControls(int Number)
        {
            try
            {
                Image weatherIconUrl;
                Label windspeedMiles;
                Label tempMaxC;
                Label date;
                Label tempMinC;
                Label weatherCode;
                Label weatherDesc;
                Label windspeedKmph;
                Label winddirection;
                Label winddir16Point;
                Label precipMM;

                //Label humidity;
                //Label observation_time;  
                //Label visibility;
                //Label pressure;
                //Label cloudcover;
                //Label temp_C;
                //Label temp_F;


                for (int i = 0; i < Number; i++)
                {
                    Panel1.Controls.Add(new LiteralControl("<ul class='weather_list'><li>"));
                    
                    date = new Label();
                    date.ID = "lbldate" + i;
                    Panel1.Controls.Add(new LiteralControl("<span class='date'>"));
                    Panel1.Controls.Add(date);
                    Panel1.Controls.Add(new LiteralControl("</span>"));

                    weatherIconUrl = new Image();
                    //weatherIconUrl.ID = "imgweatherIconUrl" + i;
                    //Panel1.Controls.Add(new LiteralControl("<span class='image'>"));
                    //weatherIconUrl.Width = 90;
                    //weatherIconUrl.Height = 90;
                    //Panel1.Controls.Add(weatherIconUrl);
                    Panel1.Controls.Add(new LiteralControl("</span>"));                    
                    
                    tempMinC = new Label();
                    tempMinC.ID = "lbltempMinF" + i;
                    Panel1.Controls.Add(new LiteralControl("<span class='tempmin'>Min (&deg;F)<br />"));
                    Panel1.Controls.Add(tempMinC);
                    Panel1.Controls.Add(new LiteralControl("</span>"));

                    tempMaxC = new Label();
                    tempMaxC.ID = "lbltempMaxF" + i;
                    Panel1.Controls.Add(new LiteralControl("<span class='tempmax'>Max (&deg;F)<br />"));
                    Panel1.Controls.Add(tempMaxC);
                    Panel1.Controls.Add(new LiteralControl("</span>"));

                    //windspeedMiles = new Label();
                    //windspeedMiles.ID = "lblwindspeedMiles" + i;
                    //Panel1.Controls.Add(new LiteralControl("<td>"));
                    //Panel1.Controls.Add(windspeedMiles);
                    //Panel1.Controls.Add(new LiteralControl("</td>"));

                    //windspeedKmph = new Label();
                    //windspeedKmph.ID = "lblwindspeedKmph" + i;
                    //Panel1.Controls.Add(new LiteralControl("<td>"));
                    //Panel1.Controls.Add(windspeedKmph);
                    //Panel1.Controls.Add(new LiteralControl("</td>"));

                    weatherDesc = new Label();
                    weatherDesc.ID = "lblweatherDesc" + i;
                    Panel1.Controls.Add(new LiteralControl("<span class='desc'>"));
                    Panel1.Controls.Add(weatherDesc);
                    Panel1.Controls.Add(new LiteralControl("</span>"));

                    //weatherCode = new Label();
                    //weatherCode.ID = "lblweatherCode" + i;
                    //Panel1.Controls.Add(new LiteralControl("<td>"));
                    //Panel1.Controls.Add(weatherCode);
                    //Panel1.Controls.Add(new LiteralControl("</td>"));

                    //winddirection = new Label();
                    //winddirection.ID = "lblwinddirection" + i;
                    //Panel1.Controls.Add(new LiteralControl("<td>"));
                    //Panel1.Controls.Add(winddirection);
                    //Panel1.Controls.Add(new LiteralControl("</td>"));

                    precipMM = new Label();
                    precipMM.ID = "lblprecipMM" + i;
                    Panel1.Controls.Add(new LiteralControl("<span class='precip'>Pre (%)<br />"));
                    Panel1.Controls.Add(precipMM);
                    Panel1.Controls.Add(new LiteralControl("</span></li></ul>"));

                    //winddir16Point = new Label();
                    //winddir16Point.ID = "lblwinddir16Point" + i;
                    //Panel1.Controls.Add(new LiteralControl("<td>"));
                    //Panel1.Controls.Add(winddir16Point);
                    //Panel1.Controls.Add(new LiteralControl("</td> "));

                }

            }

            catch (Exception ex)
            {

            }

        }
        public string GetCID(string calendarName)
        {
            string id = null;
            
            try
            {
                string AdminuserName = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                string AdminuserPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                GoogleCalendar calendar = new GoogleCalendar(calendarName, AdminuserName, AdminuserPwd);
                id = calendar.GetCalendarId();
            }
            catch (Exception ex)
            {
                //LogManager.Instance.WriteToFlatFile("stepPage" + ex.Message);
            }
            return id;
        }     
    }
}