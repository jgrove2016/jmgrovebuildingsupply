using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using JG_Prospect.BLL;

namespace JG_Prospect
{
    public partial class home : System.Web.UI.Page
    {

          StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"] == "Admin")
                    {
                        //  Response.Redirect("/home.aspx");
                    }

                    else if (Session["loginid"] != null)
                    {

                    }
                }

                Session["AppType"] = "JrApp";
            }
       
        }
    }
}