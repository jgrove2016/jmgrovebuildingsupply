using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using JG_Prospect.BLL;
using System.Data;

namespace JG_Prospect.Sr_App
{
    public partial class AttachQuotes : System.Web.UI.Page
    {
        List<AttachedQuotes> aqList = new List<AttachedQuotes>();
        int productId = 0, customerId = 0, productTypeId = 0;
        static int selectedVendorID=0;
        static string selectedRowIndex="-1";
        string emailStatus = string.Empty, SoldJobID=string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString[QueryStringKey.Key.ProductId.ToString()] != null)
            //{
            //    productId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductId.ToString()]);
            //}
            //if (Request.QueryString[QueryStringKey.Key.CustomerId.ToString()] != null)
            //{
            //    customerId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.CustomerId.ToString()]);
            //}
            //if (Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()] != null)
            //{
            //    productTypeId = Convert.ToInt16(Request.QueryString[QueryStringKey.Key.ProductTypeId.ToString()]);
            //}
            if (Request.QueryString[QueryStringKey.Key.EmailStatus.ToString()] != null)
            {
                emailStatus = Request.QueryString[QueryStringKey.Key.EmailStatus.ToString()];
            }
            if (Session[SessionKey.Key.JobId.ToString()] != null)
            {
                SoldJobID = Session[SessionKey.Key.JobId.ToString()].ToString();
            }
            if (!IsPostBack)
            {
                ViewState["Id"] = 0;
                BindDropDowns();
                BindGridView();
                selectedRowIndex = "-1";
                DisableControlsBasedOnEmailStatus(emailStatus);
            }
            btnFileUpload.Attributes.Add("onclick", "document.getElementById('" + uploadvendorquotes.ClientID + "').click();");  

        }

        private void DisableControlsBasedOnEmailStatus(string emailStatus)
        {
            if (emailStatus == "V")
            {
                btnSaveQuotes.Visible = false;
                btnResetQuotes.Visible = false;
                drpVendorCategory.Enabled = false;
                drpVendorName.Enabled = false;
                btnFileUpload.Disabled = true;
                txtFileName.Enabled = false;
                txtFileContent.Enabled = false;
                btnCancelQuotes.Text = "Close";
                foreach (GridViewRow r in grdAttachQuotes.Rows)
                {
                    LinkButton lnkdelete = (LinkButton)r.FindControl("lnkdelete");
                    lnkdelete.Enabled = false;
                    lnkdelete.ForeColor = System.Drawing.Color.DarkGray;
                    lnkdelete.Attributes.Remove("onclick");
                    LinkButton lnkSelect = (LinkButton)r.FindControl("lnkSelect");
                    lnkSelect.Enabled = false;
                }
            }
        }

        private void BindGridView()
        {
            DataSet ds = VendorBLL.Instance.GetAllAttachedQuotes(SoldJobID);
            grdAttachQuotes.DataSource = ds;
            grdAttachQuotes.DataBind();
            List<AttachedQuotes> fileList = new List<AttachedQuotes>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    AttachedQuotes a = new AttachedQuotes();
                    a.id = Convert.ToInt16(dr["Id"]);
                    a.DocName = dr["DocName"].ToString();
                    a.TempName = dr["TempName"].ToString();
                    a.VendorName = dr["VendorName"].ToString();
                    a.VendorCategoryNm = dr["VendorCategoryNm"].ToString();
                    a.action = "Add";

                    if (Convert.IsDBNull(dr["VendorCategpryId"]))
                    {
                        a.VendorCategoryId = 0;
                    }
                    else
                    {
                        a.VendorCategoryId = Convert.ToInt16(dr["VendorCategpryId"]);
                    }

                    if (Convert.IsDBNull(dr["VendorId"]))
                    {
                        a.VendorId = 0;
                    }
                    else
                    {
                        a.VendorId = Convert.ToInt16(dr["VendorId"]);
                    }
                    fileList.Add(a);
                }
            }
            ViewState["FileList"] = fileList;
        }
        private void BindDropDowns()
        {
            DataSet ds = VendorBLL.Instance.fetchAllVendorCategoryHavingVendors();
            drpVendorCategory.DataSource = ds;
            drpVendorCategory.DataTextField = "VendorCategoryNm";
            drpVendorCategory.DataValueField = "VendorCategpryId";
            drpVendorCategory.DataBind();
            drpVendorCategory.Items.Insert(0, new ListItem("Select", "Select"));
            drpVendorName.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void BindFiles(List<AttachedQuotes> fileList = null)
        {
            if (fileList == null)
            {
                fileList = GetFilesFromViewState();
            }
            grdAttachQuotes.DataSource = null;
            grdAttachQuotes.DataSource = fileList.Where(c => c.action != "Delete").ToList();
            grdAttachQuotes.DataBind();

        }
        protected void btnCancelQuotes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Procurement.aspx");
        }
        private List<AttachedQuotes> GetFilesFromViewState()
        {
            List<AttachedQuotes> fileList = null;

            if (ViewState["FileList"] == null)
            {
                fileList = new List<AttachedQuotes>();
            }
            else
            {
                fileList = ViewState["FileList"] as List<AttachedQuotes>;
            }
            return fileList;
        }

        protected void UpdateFileList(AttachedQuotes file, int rowIndex = 0)
        {
            List<AttachedQuotes> fileList = GetFilesFromViewState();

            switch (file.action)
            {
                case "Add":
                    fileList.Add(file);
                    break;
                case "Delete":
                    fileList.Remove(file);
                    //file.id
                    break;
                case "Update" :
                    fileList[rowIndex] = file;
                    break;
                default:
                    break;
            }

            ViewState["FileList"] = fileList;
            Session["FileList"] = fileList.ToList();
            BindFiles(fileList);

        }

        protected void grdAttachQuotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string rowIndex = Convert.ToString(e.CommandArgument);
                var fileList = ViewState["FileList"] as List<AttachedQuotes>;
                string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");
                if (e.CommandName.Equals("Delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    var file = fileList.Where(a => a.TempName == rowIndex).FirstOrDefault();

                    if (!File.Exists(sourceDir + file.TempName))
                    {
                        File.Delete(sourceDir + file.TempName);
                    }

                    file.action = "Delete";
                    VendorBLL.Instance.RemoveAttachedQuote(file.TempName);
                    UpdateFileList(file);
                }
                if (e.CommandName.Equals("Select", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedRowIndex = rowIndex;
                    
                    var file = fileList.Where(a => a.TempName == rowIndex).FirstOrDefault();
                    selectedVendorID = file.VendorId;

                    if (file.VendorCategoryNm != "")
                    {
                        drpVendorCategory.SelectedValue = file.VendorCategoryId.ToString();
                    }
                    else
                    {
                        drpVendorCategory.Items.Insert(0, new ListItem("Select", "Select"));
                    }
                    if (file.VendorName != "")
                    {
                        drpVendorName.SelectedItem.Value = file.VendorName;
                        drpVendorName.SelectedItem.Text = file.VendorName;
                    }
                    else
                    {
                        drpVendorName.Items.Insert(0, new ListItem("Select", "Select"));
                    }
                    if (file.DocName.Contains(".txt"))
                    {
                        txtFileName.Text = file.DocName;
                        string fileContent = File.ReadAllText(sourceDir + file.TempName);
                        txtFileContent.Text = fileContent;
                        txtFileUpload.Text = "";
                    }
                    else
                    {
                        txtFileUpload.Text = file.DocName;
                        txtFileName.Text = "";
                        txtFileContent.Text = "";
                    }
                    disableDropdowns();
                }
                if (e.CommandName.Equals("View", StringComparison.InvariantCultureIgnoreCase))
                {
                    string domainName = Request.Url.GetLeftPart(UriPartial.Authority);

                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Myscript", "<script language='javascript'>window.open('" + domainName + "/CustomerDocs/VendorQuotes/" + rowIndex + "', null, 'width=487px,height=455px,center=1,resize=0,scrolling=1,location=no');</script>");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void grdAttachQuotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnSaveQuotes_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRowIndex=="-1")
                {
                    bool res = false;
                    bool fileUploaded = false, fileCreated = false;

                    if (drpVendorCategory.SelectedValue != "Select" && drpVendorName.SelectedValue != "Select")
                    {
                        int vendorId = Convert.ToInt16(drpVendorName.SelectedValue); // need check of blank
                        List<AttachedQuotes> fileList = GetFilesFromViewState();
                        foreach (var item in fileList)
                        {
                            if (vendorId == item.VendorId)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have alreay attached a quote with this vendor.');", true);
                                return;
                            }
                        }
                        if (uploadvendorquotes.HasFile)
                            fileUploaded = true;
                        if (txtFileName.Text != "" && txtFileContent.Text != "")
                            fileCreated = true;

                        if (fileCreated == true && fileUploaded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please either upload file or write file. Both operations in one go is not possible.');", true);
                            return;
                        }
                        else if (fileUploaded == true)
                        {
                            HttpPostedFile uploadfile = uploadvendorquotes.PostedFile;

                            string tempFileName = DateTime.Now.Ticks + Path.GetFileName(uploadfile.FileName);
                            string originalFileName = Path.GetFileName(uploadfile.FileName);

                            if (uploadfile.ContentLength > 0)
                            {
                                uploadfile.SaveAs(Server.MapPath("~/CustomerDocs/VendorQuotes/") + tempFileName);
                            }

                            DataSet ds= VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);
                            
                            AttachedQuotes aq = new AttachedQuotes();
                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Add";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
                            aqList.Add(aq);
                            UpdateFileList(aq);

                            res = VendorBLL.Instance.AddVendorQuotes(SoldJobID, originalFileName, tempFileName, vendorId);

                            txtFileUpload.Text = "";
                            drpVendorCategory.SelectedIndex = -1;
                            drpVendorName.Items.Clear();
                            drpVendorName.Items.Insert(0, new ListItem("Select", "Select"));
                        } 
                        else if (fileCreated == true)
                        {
                            AttachedQuotes aq = new AttachedQuotes();
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                            string filename = "";
                            if (!txtFileName.Text.Contains(".txt"))
                                filename = txtFileName.Text + ".txt";
                            else
                                filename = txtFileName.Text;

                            string tempFileName = DateTime.Now.Ticks + filename;
                            string originalFileName = filename;

                            File.WriteAllText(sourceDir + tempFileName, txtFileContent.Text);
                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Add";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);

                            aqList.Add(aq);

                            UpdateFileList(aq);

                            ResetControls();

                            res = VendorBLL.Instance.AddVendorQuotes(SoldJobID, originalFileName, tempFileName, vendorId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please upload document or specify file name and content.');", true);
                        }

                        if (res)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote attached successfully');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Vendor Category and Vendor Name');", true);
                    }
                }
                else //if record gets updated
                {
                     bool res = false;
                     int vendorId = selectedVendorID;

                     if (drpVendorCategory.SelectedValue != "Select" && drpVendorName.SelectedValue != "Select")
                     {
                        List<AttachedQuotes> fileListViewState = GetFilesFromViewState();
                        int rowIndex = 0;
                        foreach (var item in fileListViewState)
                        {
                            if (selectedRowIndex == item.TempName)
                            {
                                break;
                            }
                            rowIndex++;
                        }

                        var fileList = ViewState["FileList"] as List<AttachedQuotes>;
                        var selectedRecord = fileList.Where(a => a.TempName == selectedRowIndex).FirstOrDefault();
                        if (vendorId != selectedRecord.VendorId)
                        {
                            List<AttachedQuotes> file = fileList.Where(a => a.TempName != selectedRowIndex).ToList();

                            foreach (var item in file)
                            {
                                if (vendorId == item.VendorId)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('You have alreay attached a quote with this vendor.');", true);
                                    return;
                                }
                            }
                        }
                        string uploadedFilename="",createdFilename="";
                        bool fileUploaded = false, fileCreated = false;
                        if(uploadvendorquotes.HasFile)
                        {
                            uploadedFilename=txtFileUpload.Text;
                            fileUploaded = true;
                        }
                        if(txtFileName.Text!="" && txtFileContent.Text != "")
                        {
                            createdFilename=txtFileName.Text;
                            fileCreated = true;
                        }

                        if (fileCreated == true && fileUploaded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please either upload file or write file. Both operations in one go is not possible.');", true);
                        }
                        else if (fileUploaded == true)
                        {
                            if (uploadvendorquotes.HasFile)
                            {

                                HttpPostedFile uploadfile = uploadvendorquotes.PostedFile;

                                string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");
                
                                if (File.Exists(sourceDir + selectedRecord.TempName))   // to delete previous file
                                {
                                    File.Delete(sourceDir + selectedRecord.TempName);
                                }

                                string tempFileName = DateTime.Now.Ticks + Path.GetFileName(uploadfile.FileName);
                                string originalFileName = Path.GetFileName(uploadfile.FileName);

                                if (uploadfile.ContentLength > 0)
                                {
                                    uploadfile.SaveAs(Server.MapPath("~/CustomerDocs/VendorQuotes/") + tempFileName);
                                }

                                DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);

                                AttachedQuotes aq = new AttachedQuotes();
                                aq.TempName = tempFileName;
                                aq.DocName = originalFileName;
                                aq.action = "Update";
                                aq.VendorId = vendorId;
                                aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                                aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                                aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
                                aqList.Add(aq);
                                UpdateFileList(aq,rowIndex);

                                res = VendorBLL.Instance.UpdateAttachedQuote(SoldJobID, originalFileName, tempFileName, vendorId);

                                txtFileUpload.Text = "";
                                drpVendorCategory.SelectedIndex = -1;
                                drpVendorName.Items.Clear();
                                drpVendorName.Items.Insert(0, new ListItem("Select", "Select"));
                            }

                            selectedRowIndex = "-1";   //means now no row is selected
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote updated successfully');", true);
                        }
                        else if (fileCreated == true)
                        {
                            AttachedQuotes aq = new AttachedQuotes();
                            string sourceDir = Server.MapPath("~/CustomerDocs/VendorQuotes/");

                            string filename="";
                            if (!txtFileName.Text.Contains(".txt"))
                                filename = txtFileName.Text + ".txt";
                            else
                                filename = txtFileName.Text;

                            string tempFileName = DateTime.Now.Ticks + filename;
                            string originalFileName = filename;

                            File.WriteAllText(sourceDir + tempFileName, txtFileContent.Text);
                            DataSet ds = VendorBLL.Instance.fetchVendorDetailsByVendorId(vendorId);
                            aq.TempName = tempFileName;
                            aq.DocName = originalFileName;
                            aq.action = "Update";
                            aq.VendorId = vendorId;
                            aq.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
                            aq.VendorCategoryNm = ds.Tables[0].Rows[0]["VendorCategoryNm"].ToString();
                            aq.VendorCategoryId = Convert.ToInt16(ds.Tables[0].Rows[0]["VendorCategpryId"]);
 
                            aqList.Add(aq);
                            UpdateFileList(aq,rowIndex);

                            if (File.Exists(sourceDir + selectedRecord.TempName))   // to delete previous file
                            {
                                File.Delete(sourceDir + selectedRecord.TempName);
                            }

                            res = VendorBLL.Instance.UpdateAttachedQuote(SoldJobID, originalFileName, tempFileName, vendorId);

                            ResetControls();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Quote updated successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please upload document or specify file name and content.');", true);
                        }


                     }
                     else
                     {
                          ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select Vendor Category and Vendor Name');", true);
                     }
                     enableDropdowns();

                     
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private void disableDropdowns()
        {
            drpVendorCategory.Enabled = false;
            drpVendorName.Enabled = false;
        }

        private void enableDropdowns()
        {
            drpVendorCategory.Enabled = true;
            drpVendorName.Enabled = true;
        }

        protected void ResetControls()
        {
            txtFileName.Text = "";
            txtFileContent.Text = "";
            txtFileUpload.Text = "";
            drpVendorCategory.SelectedIndex = -1;
            drpVendorName.Items.Clear();
            drpVendorName.Items.Insert(0, new ListItem("Select", "Select"));
            selectedRowIndex = "-1";
            enableDropdowns();
        }

        protected void drpVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpVendorCategory.SelectedValue != "Select")
            {
                DataSet ds = VendorBLL.Instance.fetchVendorNamesByVendorCategory(Convert.ToInt16(drpVendorCategory.SelectedValue));
                drpVendorName.DataSource = ds;
                drpVendorName.DataTextField = "VendorName";
                drpVendorName.DataValueField = "VendorId";
                drpVendorName.DataBind();
            }
            else
            {
                drpVendorName.Items.Clear();
                drpVendorName.Items.Insert(0, new ListItem("Select", "Select"));
            }

        }

        protected void btnResetQuotes_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void grdAttachQuotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this document?');");
            }
        }

    }
}