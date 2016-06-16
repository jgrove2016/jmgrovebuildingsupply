using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using JG_Prospect.BLL;
using System.Data;

namespace JG_Prospect.Services
{
    public partial class service : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<Name> getAllCustomerNames()
        {
            List<Name> customerNameList = new List<Name>();
            DataSet dsCustomerName = AdminBLL.Instance.FetchALLcustomer();
            
            if (dsCustomerName.Tables[0].Rows.Count > 0)
            {
                for (int counter = 0; counter < dsCustomerName.Tables[0].Rows.Count - 1; counter++)
                {
                    DataRow dr = dsCustomerName.Tables[0].Rows[counter];
                    Name cl = new Name();
                    cl.label = dr["CustomerName"].ToString();
                    int id = Convert.ToInt16(dr["id"].ToString().Substring(1));
                    cl.value = id;
                    customerNameList.Add(cl);
                }
            }
            Comparison<Name> compName = new Comparison<Name>(Name.CompareCustomerName);
            customerNameList.Sort(compName);

            return customerNameList;
        }

        public class Name
        {
            public int value
            {
                get;
                set;
            }
            public string label
            {
                get;
                set;

            }

            public static int CompareCustomerName(Name c1, Name c2)
            {
                return c1.label.CompareTo(c2.label);
            }
        }
    }
}