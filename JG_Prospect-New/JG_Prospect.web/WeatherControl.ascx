<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeatherControl.ascx.cs" Inherits="JG_Prospect.Sr_App.WeatherControl" %>

<%@ import Namespace="System.Xml" %>
<%@ import Namespace="Weather" %>

<script runat="server">

    private Decimal _latitude = new Decimal(47.72);
    private Decimal _longtitude = new Decimal(-122.02);

    private string _cssClassWarm = "Warm";
    private string _cssClassCold = "Cold";
    private string _cssClassNormal = "Normal";
    
    private Unit _width = Unit.Parse("70%");
    
    public string Longtitude 
    { set { _longtitude = Decimal.Parse(value); } }

    public string Latitude
    {  set { _latitude = Decimal.Parse(value); } }

    public string cssClassCold
    { set { _cssClassCold = value; } }

    public string cssClassWarm
    { set { _cssClassWarm = value; } }

    public string cssClassNormal
    { set { _cssClassNormal = value; } }

    public string Width
    { set { _width = Unit.Parse(value); } }
    
    void Page_Load(Object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        
            ndfdXML weatherFetcher = new ndfdXML();
            weatherParametersType weatherParams = new weatherParametersType();
            string xmlWeather;
            
            WeatherTable.Visible = false;
            WeatherTable.Width = _width;
            
            try
            {
                DayWeatherData[] arrDayWeather;
                DateTime dtStart = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
                
                // http://www.nws.noaa.gov/forecasts/xml/
                xmlWeather = weatherFetcher.NDFDgen(_latitude, _longtitude, "glance", dtStart, dtStart.AddYears(1), weatherParams);
                    
                arrDayWeather = WeatherXMLParser.ParseWeatherXML(xmlWeather);
                if(arrDayWeather != null)
                {
                    TableRow tr = new TableRow(); // titles
                    foreach (DayWeatherData dt in arrDayWeather)
                    {
                        TableCell tc = new TableCell();
                        tc.Text = dt.DateTime.ToLongDateString();
                        tr.Cells.Add(tc);
                    }

                    tr.CssClass = _cssClassNormal;
                    tr.HorizontalAlign = HorizontalAlign.Center;
                    tr.VerticalAlign = VerticalAlign.Top;
                    WeatherTable.Rows.Add(tr);

                    tr = new TableRow(); // clouds
                    foreach(DayWeatherData dt in arrDayWeather)
                    {
                        TableCell tc = new TableCell();
                        tc.Text = "<img src='" + dt.CloudIconURL + "'/>";
                        tr.Cells.Add(tc);
                    }

                    tr.CssClass = _cssClassNormal;
                    tr.HorizontalAlign = HorizontalAlign.Center;
                    tr.VerticalAlign = VerticalAlign.Top;
                    WeatherTable.Rows.Add(tr);

                    tr = new TableRow(); // highs
                    foreach (DayWeatherData dt in arrDayWeather)
                    {
                        TableCell tc = new TableCell();
                        tc.Text = dt.HighTempC + "&#176;C (" + dt.HighTempF + "&#176;F)";
                        tc.Wrap = false;
                        tr.Cells.Add(tc);
                    }

                    tr.CssClass = _cssClassWarm;
                    tr.HorizontalAlign = HorizontalAlign.Center;
                    tr.VerticalAlign = VerticalAlign.Top;
                    WeatherTable.Rows.Add(tr);

                    tr = new TableRow(); // lows
                    foreach (DayWeatherData dt in arrDayWeather)
                    {
                        TableCell tc = new TableCell();
                        tc.Text = dt.LowTempC + "&#176;C (" + dt.LowTempF + "&#176;F)" ;
                        tc.Wrap = false;
                        tr.Cells.Add(tc);
                    }

                    tr.CssClass = _cssClassCold;
                    tr.HorizontalAlign = HorizontalAlign.Center;
                    tr.VerticalAlign = VerticalAlign.Top;
                    WeatherTable.Rows.Add(tr);

                    WeatherTable.Visible = true;
                }
            }
            catch(Exception)
            {            
                WeatherTable.Visible = false;
            }
        }
    }
    
</script>

<asp:Table runat="server" id="WeatherTable" />
