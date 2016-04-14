using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.Common.modal;
using JG_Prospect.BLL;

namespace JG_Prospect.Sr_App
{
    public partial class Vendors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                bindAllVendors();
                bindvendorcategory();
                bindfordeletevender();
                ScriptManager.RegisterStartupScript(this, GetType(), "initialize", "initialize();", true);       
            }
        }
        protected void bindAllVendors()
        {
            lstVendors.Items.Clear();
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchallvendordetails();
            lstVendors.DataSource = ds;
            lstVendors.DataTextField = "VendorName";
            lstVendors.DataValueField = "VendorId";

            lstVendors.DataBind();
        }
        protected void bindvendor(int selectedVendorCategoryId)
        {
            lstVendors.Items.Clear();

            DataSet dsVendorNames = VendorBLL.Instance.fetchVendorNamesByVendorCategory(selectedVendorCategoryId);
            lstVendors.DataSource = dsVendorNames;
            lstVendors.DataTextField = "VendorName";
            lstVendors.DataValueField = "VendorId";
            lstVendors.DataBind();
            //DataSet ds = new DataSet();
            //ds = VendorBLL.Instance.fetchallvendordetails();
            //lstVendors.DataSource = ds.Tables[0];
            //lstVendors.DataTextField = ds.Tables[0].Columns[1].ToString();
            //lstVendors.DataValueField = ds.Tables[0].Columns[0].ToString();

            //lstVendors.DataBind();


        }
        protected void bindvendorcategory()
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchallvendorcategory();
            ddlVendorCategory.DataSource = ds;
            ddlVendorCategory.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlVendorCategory.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlVendorCategory.DataBind();
            ddlVendorCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void clear()
        {
            txtcontactnumber.Text = txtcontactperson.Text = txtfax.Text = txtmail.Text = txtVendorNm.Text = null;
            ddlVendorCategory.ClearSelection();
            btnSave.Text = "Save";
        }

        protected void lstVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.FetchvendorDetails(Convert.ToInt32(lstVendors.SelectedValue));

            txtVendorNm.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
            ddlVendorCategory.SelectedValue = ds.Tables[0].Rows[0]["VendorCategoryId"].ToString();
            txtcontactperson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
            txtcontactnumber.Text = ds.Tables[0].Rows[0]["ContactNumber"].ToString();
            txtfax.Text = ds.Tables[0].Rows[0]["Fax"].ToString();
            txtmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
            ddlMenufacturer.SelectedValue = ds.Tables[0].Rows[0]["ManufacturerType"].ToString();
            txtBillingAddress.Text = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
            txtTaxId.Text = ds.Tables[0].Rows[0]["TaxId"].ToString();
            txtExpenseCat.Text = ds.Tables[0].Rows[0]["ExpenseCategory"].ToString();
            txtAutoInsurance.Text = ds.Tables[0].Rows[0]["AutoTruckInsurance"].ToString();
            txtVendorId.Text = lstVendors.SelectedValue;
            btnSave.Text = "Update";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstVendors.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('please select Record for delete');", true);
                return;

            }
            bool res = VendorBLL.Instance.deletevendor(Convert.ToInt32(lstVendors.SelectedValue));

            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been Deleted Successfully');", true);
            }
            
            bindvendor(Convert.ToInt16(ddlVendorCategory.SelectedValue));
            //clear();
            clearcontrols();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            bool result = VendorBLL.Instance.deletevendorcategory(Convert.ToInt32(ddlvendercategoryname.SelectedValue));
            bindfordeletevender();
            bindvendorcategory();
            lstVendors.Items.Clear();
            clearcontrols();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Vendor objvendor = new Vendor();
            if (ddlVendorCategory.SelectedValue == "Select")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('please select Vendor Category');", true);
                return;
            }

            if (lstVendors.SelectedValue != "")
                objvendor.vendor_id = Convert.ToInt32(lstVendors.SelectedValue);
            objvendor.vendor_name = txtVendorNm.Text;

            objvendor.vendor_category_id = Convert.ToInt32(ddlVendorCategory.SelectedValue);
            objvendor.contract_person = txtcontactperson.Text;
            objvendor.contract_number = txtcontactnumber.Text;
            objvendor.fax = txtfax.Text;
            objvendor.mail = txtmail.Text;
            objvendor.address = txtaddress.Text;
            objvendor.notes = txtNotes.Text;
            objvendor.ManufacturerType = ddlMenufacturer.SelectedValue;
            objvendor.BillingAddress = txtBillingAddress.Text;
            objvendor.TaxId = txtTaxId.Text;
            objvendor.ExpenseCategory = txtExpenseCat.Text;
            objvendor.AutoTruckInsurance = txtAutoInsurance.Text;
            bool res = VendorBLL.Instance.savevendor(objvendor);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Saved/Updated Successfully');", true);
            }
            bindvendor(Convert.ToInt16(ddlVendorCategory.SelectedValue));
            //bindvendorcategory();
            //clear();
            clearcontrols();

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Vendor_Catalog objcatalog = new Vendor_Catalog();

            objcatalog.catalog_name = txtname.Text;
            bool res = VendorBLL.Instance.savevendorcatalogdetails(objcatalog);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been inserted Successfully');", true);
                bindvendorcategory();
                bindfordeletevender();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('There is some error');", true);
            }

        }

       

        protected void bindfordeletevender()
        {
            DataSet ds = new DataSet();
            ds = VendorBLL.Instance.fetchallvendorcategory();
            ddlvendercategoryname.DataSource = ds;
            ddlvendercategoryname.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlvendercategoryname.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlvendercategoryname.DataBind();


        }

        protected void Lnkdeletevendercategory_Click(object sender, EventArgs e)
        {

        }

        protected void btndeletevender_Click(object sender, EventArgs e)
        {
            bool result = VendorBLL.Instance.deletevendorcategory(Convert.ToInt32(ddlvendercategoryname.SelectedValue));
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category has been deleted Successfully');", true);

            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Vendor Category cannot be deleted, delete all vendors of this category');", true);
            bindvendorcategory();
            bindfordeletevender();
        }

        protected void clearcontrols()
        {
            txtVendorNm.Text = "";
            txtcontactperson.Text = "";
            txtcontactnumber.Text = "";
            txtfax.Text = "";
            txtmail.Text = "";
            ddlMenufacturer.SelectedValue = "0";
            txtBillingAddress.Text = "";
            txtTaxId.Text = "";
            txtExpenseCat.Text = "";
            txtAutoInsurance.Text = "";
        }

        protected void ddlVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVendorCategory.SelectedValue != "Select")
            {
                int selectedVendorCategoryId = Convert.ToInt16(ddlVendorCategory.SelectedValue);
                bindvendor(selectedVendorCategoryId);
            }
            else
            {
                lstVendors.Items.Clear();
            }

            clearcontrols();
            btnSave.Text = "Save";
            
        }

    }
}