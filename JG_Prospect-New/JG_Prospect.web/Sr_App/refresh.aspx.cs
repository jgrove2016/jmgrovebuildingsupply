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
        string show = "select CustomerId, UserId, COUNT(*) from tblCustom group by CustomerId, UserId having count(*)>1";
        SqlDataAdapter da = new SqlDataAdapter(show, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        con.Close();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        con.Open();
        string del = "delete from tblCustom where UserId= NULL OR UserId='0' AND ProposalTerms='NULL' OR ProposalTerms=''";
        SqlCommand com1 = new SqlCommand(del, con);
        com1.ExecuteNonQuery();
        con.Close();
        string script = "alert(\"Null value deleted...\");";
        ScriptManager.RegisterStartupScript(this, GetType(),
                              "ServerControlScript", script, true);
    }
}