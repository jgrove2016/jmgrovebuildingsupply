using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace JG_Prospect
{
    public class GoogleDetail
    {
        #region private members
        private string googleaddress;
        private string postalcode;
        private string imagepath;
        private string information;
        private string imageheading;
        private string latitude;
        private string longitude;
        //private string farmerId;
        //private string villageName;
        //private string districtName;
        private string strgooglepoint;
        private string image;
        string centerLatitude;
        string centerLongitude;
        //public string P_FarmerId
        //{
        //    get { return farmerId; }
        //    set { farmerId = value; }
        //}
        //public string P_Village_Name
        //{
        //    get { return villageName; }
        //    set { villageName = value; }
        //}
        //public string P_DistrictName
        //{
        //    get { return districtName; }
        //    set { districtName = value; }
        //}
        public string P_Image
        {
            get { return image; }
            set { image = value; }
        }


        #endregion

        public GoogleDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region methods

        public string getGoogleData(string str_Google_id, string lat, string lang)
        {


            centerLatitude = "";
            centerLongitude = "";

            string[,] googlepoint = new string[1, 5];


            googlepoint[0, 0] = image;
            googlepoint[0, 1] = "Latitude " + lat.ToString();
            googlepoint[0, 2] = "Longitude " + lang.ToString();

            centerLatitude = lat.ToString();
            centerLongitude = lang.ToString();
            centerLongitude = googlepoint[0, 3] = lat.ToString();
            centerLatitude = googlepoint[0, 4] = lang.ToString();
            strgooglepoint = "";
            strgooglepoint = converArrayToString(googlepoint, 0);

            if (strgooglepoint != null)
            {
                return strgooglepoint;
            }
            else
            {
                strgooglepoint = "NOGPSDATA";
                return strgooglepoint;
            }


        }
        public string converArrayToString(string[,] googlepoint, int int_Size)
        {
            string[,] googlepoint1 = (string[,])googlepoint.Clone();
            for (int j = 0; j < 5; j++)
            {
                strgooglepoint += string.Concat(googlepoint1[0, j], ",");
            }

            strgooglepoint += "|";
            return strgooglepoint;
        }

        public void Put_Markers(string str_Google_id, out string js_load, out string js, int int_Start_Zoom, string lat, string lang)
        {
            Control c = new Control();
            /****************************************************************/
            System.Configuration.AppSettingsReader a = new AppSettingsReader();
            string str_googlekey = String.Empty;
            str_googlekey = (string)a.GetValue("googlekey", str_googlekey.GetType());
            /****************************************************************/
            js_load = "<script src=\"http://maps.google.com/maps?file=api&amp;v=2&amp;key=" + str_googlekey + "\" type=\"text/javascript\"></script><script src=\"http://www.google.com/jsapi?key=" + str_googlekey + "\" type=\"text/javascript\"></script><script src=\"" + c.ResolveUrl("~/JS/GMap.js\"") + " language=\"javascript\" type=\"text/javascript\"></script>";

            //GoogleDetail google = new GoogleDetail();

            string googlepoint = getGoogleData(str_Google_id, lat, lang);
            if (googlepoint == "NOGPSDATA")
            {
                js = "";
            }
            else
            {
                // string googlepoint = google.getGoogleData(str_Google_id, out centerLatitude, out centerLongitude);
                js = "var centerLatitude = " + centerLatitude + ";" +
                            "var centerLongitude = " + centerLongitude + ";" +
                             "var startZoom = " + int_Start_Zoom.ToString() + ";" +
                            "var map;" +
                            "var marker=new Array();" +
                            "var googlepoint;" +
                    "function init(){" +
                    "googlepoint = \"" + googlepoint + "\";" +
                   "if (GBrowserIsCompatible()) {" +
                    "map = new GMap2(document.getElementById('map'));" +
                    "var location = new GLatLng(centerLatitude, centerLongitude);" +
                    "map.setCenter(location, startZoom);" +
                    // Below Function is create and object of GLargeMapControl and GMapTypeControl for Hybrid,satelite or etc. view
                     "map.setMapType(G_SATELLITE_MAP);" +
                    "map.addControl(new GLargeMapControl());" +
                    "map.addControl(new GMapTypeControl());" +
                    "var markers=new Array();" +
                    "markers=googlepoint.split('|');" +
                    "for(i=0;i<markers.length-1;i++)" +
                           "{" +
                                "var Marker = CreateMarker(markers[i]);" +
                                "map.addOverlay(Marker);" +
                                "GEvent.trigger(Marker,\"click\");" +
                            "}" +
                    "}" +
                    "}" +
                    "window.onload = init;" +
                    "window.onunload = GUnload;";
            }
        }

        #endregion

    }
}