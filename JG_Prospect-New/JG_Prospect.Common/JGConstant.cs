using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace JG_Prospect.Common
{
    public class JGConstant
    {
        public static CultureInfo CULTURE = System.Globalization.CultureInfo.GetCultureInfo("en-US");

        public const string JUSTIN_LOGIN_ID = "jgrove@jmgroveconstruction.com";//"jgtest2@gmail.com"; //" jgrove@jmgroveconstruction.com"
                                                                               //public const string JUSTIN_LOGIN_ID ="jgtest2@gmail.com";
        public const string PAGE_STATIC_REPORT = "StaticReport";
        public const string COLOR_RED = "red";
        public const int RETURN_ZERO = 0;
        public const int ZERO = 0;
        public const int ONE = 1;
        public const int STATUS_ID_RECEIVED_STORAGE_LOCATION = 18;
        public const int STATUS_ID_ON_STANDBY_VENDOR_LINK_TO_VENDOR_PROFILE = 19;
        public const int STATUS_ID_BEING_DELEIVERED_TO_JOBSITE = 20;
        public const bool RETURN_TRUE = true;
        public const bool RETURN_FALSE = false;
        public const string TRUE = "Yes";
        public const string UPDATE = "Update";
        public const string SAVE = "Save";
        public const string SELECT = "Select";
        public static string CustomerCalendar = ConfigurationManager.AppSettings["CustomerCalendar"].ToString();
        public const string EMAIL_STATUS_VENDORCATEGORIES = "C";
        public const string EMAIL_STATUS_VENDOR = "V";
        public const string EMAIL_STATUS_NONE = "N";
        // public const string EMAILID_VENDORCATEGORIES = "accountspayable@jmgroveconstruction.com";
        // public const string EMAILID_VENDOR = "purchasing@jmgroveconstruction.com";
        // public const string PASSWORD_VENDORCATEGORIES = "Sunrise1";
        // public const string PASSWORD_VENDOR = "Bquality1";
        public const string PROCURRING_QUOTES = "Procurring Quotes";
        public const string PRODUCT_CUSTOM = "Custom";
        public const string PRODUCT_SHUTTER = "Shutter";

        public const string Sorting_UserName = "UserName";
        //public const string Sorting_SortDirection_DESC = "DESC";
        //public const string Sorting_SortDirection_ASC = "ASC";

        public const char PERMISSION_STATUS_GRANTED = 'G';
        public const char PERMISSION_STATUS_NOTGRANTED = 'N';

        public const string USER_TYPE_ADMIN = "Admin";
        public const string USER_TYPE_JSE = "JSE";
        public const string USER_TYPE_SSE = "SSE";
        public const string USER_TYPE_MM = "MM";
        public const string USER_TYPE_SM = "SM";
        public const string USER_TYPE_ADMINSEC = "AdminSec";

        public const string CUSTOMER_STATUS_SET = "Set";
        public const string CUSTOMER_STATUS_FOLLOWUP = "Follow up";
        public const string CUSTOMER_STATUS_ASSIGNED = "Assigned";
        public const string CUSTOMER_STATUS_ORDERED = "Ordered(3)";

        public const string PageIndex = "PageIndex";
        public const string SortExpression = "SortExpression";
        public const string SortDirection = "SortDirection";
        public const string Sorting_ReferenceId = "ReferenceId";
        public const string Sorting_SortDirection_DESC = "DESC";
        public const string Sorting_SortDirection_ASC = "ASC";
        public const string GridViewData = "GridViewData";

        public const string DROPDOWNLIST = "DROPDOWNLIST";
        public const string TEXTBOX = "TEXTBOX";

        public enum ProductType
        {
            shutter = 1,
            custom = 4
        }

        public enum CustomMaterialListStatus
        {
            Unchanged = 0,
            Added = 1,
            Deleted = 2,
            Modified = 3,
        }

        public enum TaskStatus
        {
            Assigned = 1,
            InProgress = 2,
            Pending = 3,
            ReOpened = 4,
            Closed = 5

        }
    }
}
