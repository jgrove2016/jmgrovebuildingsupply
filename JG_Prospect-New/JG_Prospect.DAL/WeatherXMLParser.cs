using System;
using System.IO;
using System.Xml;

namespace Weather
{
    public class DayWeatherData
    {
        public DateTime DateTime;
        public string LowTempF;
        public string HighTempF;
        public string LowTempC;
        public string HighTempC;
        public string CloudIconURL;
    }

    public class WeatherXMLParser
    {
        private class WeatherTable
        {
            public XmlNode nodeTimeLayout;
            public XmlNode nodeData;
        }

        // http://www.nws.noaa.gov/forecasts/xml/
        public static DayWeatherData[] ParseWeatherXML(string xmlWeather)
        {
            try
            {
                DayWeatherData[] arrDayWeatherData;
                XmlDocument xmlDoc = new XmlDocument();

                // load XML data into a tree
                xmlDoc.LoadXml(xmlWeather);

                // locate dwml/data/time-layout nodes. There should be three of them: 
                //      - next week nighttime temperatures (lows)
                //      - next week daytime temperatures (highs)
                //      - next week cloud data

                // Find roots nodes for temperature and cloud data
                WeatherTable wtLowTemp = new WeatherTable();
                WeatherTable wtHighTemp = new WeatherTable();
                WeatherTable wtClouds = new WeatherTable();

                wtLowTemp.nodeData = xmlDoc.SelectSingleNode("/dwml/data/parameters/temperature[@type='minimum']");
                wtHighTemp.nodeData = xmlDoc.SelectSingleNode("/dwml/data/parameters/temperature[@type='maximum']");
                wtClouds.nodeData = xmlDoc.SelectSingleNode("/dwml/data/parameters/conditions-icon");

                // Find out corresponding time layout table for each top data node
                wtLowTemp.nodeTimeLayout = FindLayoutTable(xmlDoc, wtLowTemp.nodeData);
                wtHighTemp.nodeTimeLayout = FindLayoutTable(xmlDoc, wtHighTemp.nodeData);
                wtClouds.nodeTimeLayout = FindLayoutTable(xmlDoc, wtClouds.nodeData);

                // number of day data is min of low and high temperatures
                XmlNodeList listLowTimes = wtLowTemp.nodeTimeLayout.SelectNodes("start-valid-time");
                XmlNodeList listHighTimes = wtHighTemp.nodeTimeLayout.SelectNodes("start-valid-time");

                int cTimes = Math.Min(listLowTimes.Count, listHighTimes.Count);
                
                arrDayWeatherData = new DayWeatherData[cTimes];
                for(int i = 0; i < cTimes; i++)
                    arrDayWeatherData[i] = new DayWeatherData();

                // Fill highs and lows
                FillLowTemperatures(wtLowTemp.nodeData, ref arrDayWeatherData);
                FillHighTemperatures(wtHighTemp.nodeData, ref arrDayWeatherData);

                FillDayNameAndTime(listLowTimes, listHighTimes, ref arrDayWeatherData);

                FillCloudData(wtClouds, ref arrDayWeatherData);
                
                return arrDayWeatherData;
            }
            catch(Exception)
            {
                return null;
            }
        }

        private static void FillCloudData(WeatherTable wt, ref DayWeatherData[] arrDayWeatherData)
        {
            // Cloud data is typically much longer than day high/low data
            // We need to find times that match ones in high and low temp tables.
            XmlNodeList listTimes = wt.nodeTimeLayout.SelectNodes("start-valid-time");
            XmlNodeList listIcons = wt.nodeData.SelectNodes("icon-link");
            int cWeatherData = 0;
            int cNodes = 0;
            int hourDiff = Int32.MaxValue;
            
            foreach(XmlNode node in listTimes)
            {
                DateTime dt = ParseDateTime(node.InnerText);

                if (dt.Date > arrDayWeatherData[cWeatherData].DateTime.Date)
                {
                    cWeatherData++;
                    if (cWeatherData >= arrDayWeatherData.Length)
                        break;

                    hourDiff = Int32.MaxValue;
                }

                if (dt.Date == arrDayWeatherData[cWeatherData].DateTime.Date)
                {
                    int diff = Math.Abs(dt.Hour - arrDayWeatherData[cWeatherData].DateTime.Hour);

                    if(diff < hourDiff)
                    {
                        hourDiff = diff;
                        arrDayWeatherData[cWeatherData].CloudIconURL = listIcons[cNodes].InnerText;
                    }
                }
                    
                cNodes++;
            }
        }

        private static void FillDayNameAndTime(XmlNodeList nodeLows, XmlNodeList nodeHighs, ref DayWeatherData[] arrDayWeatherData)
        {
            // Choose first elemnt from low or high list depending on what is close to the current time.
            // Typically weather report is about 'today' or 'tonight' on the current day or about 
            // day temperatures for the coming days. Therefoce remaining elements always come from 
            // the high temperatures list. 
            
            DateTime dtFirstLow = ParseDateTime(nodeLows[0].InnerText);
            DateTime dtFirstHigh = ParseDateTime(nodeHighs[0].InnerText);
            DateTime dtNow = DateTime.Now;
            XmlNodeList nodeList;
            
            if(dtFirstLow.Day == dtNow.Day && dtFirstHigh.Day == dtNow.Day)
            {
                // choose nearest
                int diffFromLow = Math.Abs(dtFirstLow.Hour - dtNow.Hour);
                int diffFromHigh = Math.Abs(dtFirstHigh.Hour - dtNow.Hour);
                
                if(diffFromHigh < diffFromLow)
                    nodeList = nodeHighs;
                else
                    nodeList = nodeLows;
            }
            else if(dtFirstHigh.Day == dtNow.Day) // choose highs
            {
                nodeList = nodeHighs;
            }
            else // choose lows
            {
                nodeList = nodeLows;
            }

            for (int i = 0; i < arrDayWeatherData.Length; i++)
            {
                DateTime dt = ParseDateTime(nodeList[i].InnerText);
                
                if(i == 0)
                    arrDayWeatherData[i].DateTime = dt;
                else  // just assign noon for the daytime
                    arrDayWeatherData[i].DateTime = new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0);
            }
        }

        private static DateTime ParseDateTime(string s)
        {
            s = s.Replace('T', ' ');
            int indexOfMinusSeparator = s.LastIndexOf('-');

            s = s.Substring(0, indexOfMinusSeparator);

            return DateTime.Parse(s);
        }

        private static void FillLowTemperatures(XmlNode node, ref DayWeatherData[] arrDayWeatherData)
        {
            XmlNodeList nodes = node.SelectNodes("value");
            
            for (int i = 0; i < arrDayWeatherData.Length; i++)
            {
                arrDayWeatherData[i].LowTempF = nodes[i].InnerText;

                int temp = (int) Math.Round(5.0 * (Double.Parse(arrDayWeatherData[i].LowTempF) - 32.0) / 9.0);
                arrDayWeatherData[i].LowTempC = temp.ToString();
            }
        }

        private static void FillHighTemperatures(XmlNode node, ref DayWeatherData[] arrDayWeatherData)
        {
            XmlNodeList nodes = node.SelectNodes("value");

            for (int i = 0; i < arrDayWeatherData.Length; i++)
            {
                arrDayWeatherData[i].HighTempF = nodes[i].InnerText;

                int temp = (int)Math.Round(5.0 * (Double.Parse(arrDayWeatherData[i].HighTempF) - 32.0) / 9.0);
                arrDayWeatherData[i].HighTempC = temp.ToString();
            }
        }

        private static XmlNode FindLayoutTable(XmlDocument xmlDoc, XmlNode topDataNode)
        {
            XmlNodeList nlTimeLayouts = xmlDoc.SelectNodes("/dwml/data/time-layout");
            string timeLayout = topDataNode.Attributes["time-layout"].Value;

            foreach (XmlNode node in nlTimeLayouts)
            {
                if (timeLayout == node.SelectSingleNode("layout-key").InnerText)
                    return node;
            }

            return null;
        }
    }
}
