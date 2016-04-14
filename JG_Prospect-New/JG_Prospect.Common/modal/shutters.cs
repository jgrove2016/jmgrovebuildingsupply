using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class shutters
    {
        public int Id { get; set; }
        public int ProductType { get; set; }
        public int estimateID;
        public int CustomerId;
        public int UserId { get; set; }
        public string CustomerNm;
        public int ShutterId;
        public string ShutterName;
        public int ShutterTopId;
        public string Shutters_top;
        public int ShutterWidthId;
        public string width;
        public string height;
        public string ColorCode;
        public string Color;
        public List<Shutter_accessories> ShutterAccessories;
        public string ShutterAccessorie;
        public string style;
        public int surfaceofmount;
        public int quantity;
        public string workarea;
        public string locationpicture;
        public List<CustomerLocationPic> CustomerLocationPics;
        public string specialinstructions;
        public decimal Totalcost;
        public string MainImage;
    }
}
