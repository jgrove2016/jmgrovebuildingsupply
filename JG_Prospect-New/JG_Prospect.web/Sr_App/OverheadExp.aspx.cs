using JG_Prospect.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JG_Prospect.Sr_App
{
    public partial class OverheadExp : System.Web.UI.Page
    {
        #region "Page Properties"

        protected int BankID
        {
            get { return ViewState["BankID"] != null ? Convert.ToInt32(ViewState["BankID"]) : 0; }
            set { ViewState["BankID"] = value; }
        }

        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialDataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (BankID > 0)
            {
                CustomBLL.Instance.UpdateBankDetails(BankID, txtPersonName.Text, txtBankName.Text, txtBankBranch.Text, txtAccountHolderName.Text, txtAccountNumber.Text, txtIFSCCode.Text, txtSwiftCode.Text);
                ClientScript.RegisterStartupScript(this.GetType(), "OnUpdate", "alert('Record updated successfully');", true);
            }
            else
            {
                CustomBLL.Instance.AddBankDetails(txtPersonName.Text, txtBankName.Text, txtBankBranch.Text, txtAccountHolderName.Text, txtAccountNumber.Text, txtIFSCCode.Text, txtSwiftCode.Text);
                ClientScript.RegisterStartupScript(this.GetType(), "OnUpdate", "alert('Record added successfully');", true);
            }
            InitialDataBind();
        }
        protected void lstBankAccount_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "RecEdit")
            {
                DataSet lDsBank = CustomBLL.Instance.GetBanks(Convert.ToInt32(e.CommandArgument));
                BankID = Convert.ToInt32(lDsBank.Tables[0].Rows[0]["BankID"].ToString());
                txtPersonName.Text = lDsBank.Tables[0].Rows[0]["PersonName"].ToString();
                txtBankName.Text = lDsBank.Tables[0].Rows[0]["BankName"].ToString();
                txtBankBranch.Text = lDsBank.Tables[0].Rows[0]["BankBranch"].ToString();
                txtAccountHolderName.Text = lDsBank.Tables[0].Rows[0]["AccountName"].ToString();
                txtAccountNumber.Text = lDsBank.Tables[0].Rows[0]["AccountNumber"].ToString();
                txtIFSCCode.Text = lDsBank.Tables[0].Rows[0]["IFSCCode"].ToString();
                txtSwiftCode.Text = lDsBank.Tables[0].Rows[0]["SWIFTCode"].ToString();
                pnlAddEditAccount.GroupingText = "Edit Bank Account";
                pnlAddEditAccount.Visible = true;
            }
            else if (e.CommandName == "RecDelete")
            {
                CustomBLL.Instance.DeleteBankDetails(Convert.ToInt32(e.CommandArgument));
                ClientScript.RegisterStartupScript(this.GetType(), "OnUpdate", "alert('Record deleted successfully');", true);
                InitialDataBind();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            InitialDataBind();
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            InitialDataBind();
            pnlAddEditAccount.Visible = true;
        }
        #endregion

        #region "Private Methods"

        private void InitialDataBind()
        {
            pnlAddEditAccount.GroupingText = "Add Bank Account";
            pnlAddEditAccount.Visible = false;
            txtPersonName.Text="";
            txtBankName.Text="";
            txtBankBranch.Text="";
            txtAccountHolderName.Text="";
            txtAccountNumber.Text="";
            txtIFSCCode.Text=""; 
            txtSwiftCode.Text="";
            lstBankAccount.DataSource = CustomBLL.Instance.GetBanks();
            lstBankAccount.DataBind();
        }
       
        #endregion



     

    


    }
}