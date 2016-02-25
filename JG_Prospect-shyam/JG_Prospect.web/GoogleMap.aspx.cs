using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using JG_Prospect.BLL;
using Telerik.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace JG_Prospect
{
    public partial class GoogleMap : System.Web.UI.Page
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        string strcon = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(strcon);
            DateTime d= System.DateTime.Now.Date;
            int a=2;
            con.Open();
           //string query = "Select * from new_customer Where AssignedToId='" + a + "' AND CreatedDate='" + d + "'";
            string query = "Select * from new_customer Where AssignedToId='" + a + "' ";
            da = new SqlDataAdapter(query, con);
            
            dt = new DataTable();
            da.Fill(dt);

            txtDestination.Value = Convert.ToString(dt.Rows[0]["CustomerAddress"]);
        }
    }
}