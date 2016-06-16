using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;

namespace JG_Prospect.Sr_App
{
    public partial class AdminPriceControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindshutter();
                bindtopshutter();
                bindshuttercolor();
                bindshutteraccessories();
            }
        }
        protected void bindshutter()
        {
            ddlshutter.Items.Clear();
            DataSet ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshutterdetails();
            ddlshutter.DataSource = ds;
            ddlshutter.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlshutter.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlshutter.DataBind();
        }
        protected void bindtopshutter()
        {
            ddltopshutter.Items.Clear();
            DataSet ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchtopshutterdetails();
            ddltopshutter.DataSource = ds;
            ddltopshutter.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddltopshutter.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddltopshutter.DataBind();
        }
        protected void bindshuttercolor()
        {
            ddlshuttercolor.Items.Clear();
            DataSet ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshuttercolordetails();
            ddlshuttercolor.DataSource = ds;
            ddlshuttercolor.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlshuttercolor.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlshuttercolor.DataBind();
        }
        protected void bindshutteraccessories()
        {
            ddlshutteraccessories.Items.Clear();
            DataSet ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshutteraccessoriesdetails();
            ddlshutteraccessories.DataSource = ds;
            ddlshutteraccessories.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlshutteraccessories.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlshutteraccessories.DataBind();
        }
        protected void btnshutter_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.updateshutterprice(Convert.ToInt32(ddlshutter.SelectedValue),Convert.ToDecimal(txtshutterprice.Text));
            txtshutterprice.Enabled = false;
        }
        protected void btntopshutter_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.updatetopshutterprice(Convert.ToInt32(ddltopshutter.SelectedValue), Convert.ToDecimal(txttopshutterprice.Text));
            txttopshutterprice.Enabled = false;
        }
        protected void btnshuttercolor_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.updateshuttercolorprice(ddlshuttercolor.SelectedValue.ToString(), Convert.ToDecimal(txtshuttercolorprice.Text));
            txtshuttercolorprice.Enabled = false;
        }
        protected void btnshutteraccessories_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.updateshutteraccessoriesprice(Convert.ToInt32(ddlshutteraccessories.SelectedValue), Convert.ToDecimal(txtshutteraccessoriesprice.Text));
            txtshutteraccessoriesprice.Enabled = false;
        }
        protected void ddlshutter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
             ds = ShutterPriceControlBLL.Instance.fetchshutterprice(Convert.ToInt32(ddlshutter.SelectedValue));
            
             txtshutterprice.Text = ds.Tables[0].Rows[0][0].ToString();
             txtshutterprice.Enabled = true;
        }
        protected void ddltopshutter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string s = ddltopshutter.SelectedItem.Text;
            ds= ShutterPriceControlBLL.Instance.fetchtopshutterprice(Convert.ToInt32(ddltopshutter.SelectedValue));
            txttopshutterprice.Text = ds.Tables[0].Rows[0][0].ToString();
            txttopshutterprice.Enabled = true;
        }
        protected void ddlshutteraccessories_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshutteraccessorieshprice(Convert.ToInt32(ddlshutteraccessories.SelectedValue));
            txtshutteraccessoriesprice.Text = ds.Tables[0].Rows[0][0].ToString();
            txtshutteraccessoriesprice.Enabled = true;
        }
        protected void ddlshuttercolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = ShutterPriceControlBLL.Instance.fetchshuttercolorprice(ddlshuttercolor.SelectedValue.ToString());
            txtshuttercolorprice.Text = ds.Tables[0].Rows[0][0].ToString();
            txtshuttercolorprice.Enabled = true;
        }
        protected void btnaddshuttertop_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.saveshuttertop(txtshuttertopname.Text, Convert.ToDecimal(txtshuttertopprice.Text));
            bindtopshutter();          
        }
        protected void btnaddshuttercolor_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.saveshuttercolor(txtshuttercolorcode.Text,txtshuttercolorname.Text, Convert.ToDecimal(txtcolorprice.Text));           
            bindshuttercolor();
        }
        protected void btnaddshutteraccessories_Click(object sender, EventArgs e)
        {
            bool result = ShutterPriceControlBLL.Instance.saveshutteraccessories(txtshutteraccessoriesname.Text, Convert.ToDecimal(txttshutteraccessoriesprice.Text));           
            bindshutteraccessories();
        }
        protected void lnkAddshutter_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Sr_App/Shutters.aspx");
        }

        protected void txtshutteraccessoriesprice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}