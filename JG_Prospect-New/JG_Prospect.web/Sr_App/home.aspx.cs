using JG_Prospect.BLL;
using JG_Prospect.Common.modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["AppType"] = "SrApp";
                if (Session["usertype"] == "SM" || Session["usertype"] == "SSE" || Session["usertype"] == "MM")
                {
                    li_AnnualCalender.Visible = true;
                }
                
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetAllScripts(string strScriptId)
        {
            DataSet ds = new DataSet();
            int? intScriptId = Convert.ToInt32(strScriptId);
            if (strScriptId == "0")
                intScriptId = null;
            ds = UserBLL.Instance.fetchAllScripts(intScriptId); ;
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
                return string.Empty;
        }

        [System.Web.Services.WebMethod]
        public static string ManageScripts(string intMode, string intScriptId, string strScriptName, string strScriptDescription)
        {
            DataSet ds = new DataSet();
            PhoneDashboard objPhoneDashboard = new PhoneDashboard();
            objPhoneDashboard.intMode = Convert.ToInt32(intMode);
            objPhoneDashboard.intScriptId = Convert.ToInt32(intScriptId);
            objPhoneDashboard.strScriptName = strScriptName;
            objPhoneDashboard.strScriptDescription = strScriptDescription;

            ds = UserBLL.Instance.manageScripts(objPhoneDashboard);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
                return string.Empty;
        }
    }
}