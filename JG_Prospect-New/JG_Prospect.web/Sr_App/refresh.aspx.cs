using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public partial class Sr_App_refresh : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JGPA"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        con.Open();
        string s1 = "delete from tblShuttersEstimate where WorkArea IS NULL OR  WorkArea=''";
        SqlCommand com = new SqlCommand(s1, con);
        com.ExecuteNonQuery();
        con.Close();

        
        
    }
}