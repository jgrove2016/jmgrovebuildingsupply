using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Net;
using System.Net.Mail;

namespace JG_Prospect.Sr_App
{
    public partial class EditInstallUser : System.Web.UI.Page
    {
        user objuser = new user();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alsert('Your session has expired,login to contineu');window.location='../login.aspx'", true);
            }

            if (Convert.ToString(Session["usertype"]).Contains("Admin"))
            {
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
            if (!IsPostBack)
            {
                CalendarExtender1.StartDate = DateTime.Now;
                Session["DeactivationStatus"] = "";
                Session["FirstNameNewSC"] = "";
                Session["LastNameNewSC"] = "";
                Session["DesignitionSC"] = "";
                binddata();
            }
        }


        private void binddata()
        {
            // DataSet DS = new DataSet();
            DataSet dsNew = new DataSet();
            StringBuilder strb = new StringBuilder();
            try
            {
                dsNew = InstallUserBLL.Instance.getallInstallusers();
                DataSet dsExport = InstallUserBLL.Instance.ExportAllInstallUsersData();
                Session["GridData"] = dsExport.Tables[0];
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    strb.Append("DS data found");
                    Session["GridDataSort"] = dsNew.Tables[0];
                    GridViewUser.DataSource = dsNew.Tables[0];
                    GridViewUser.DataBind();
                }
                else
                {
                    strb.Append("DS no data found");
                }

            }
            catch (Exception ex)
            {
                lblErrNew.Text = ex.Message + ex.StackTrace;
                //throw;
            }
            // lblErrNew.Text = Convert.ToString(strb);

        }


        protected void GridViewUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUser.EditIndex = -1;
            // binddata();
        }

        protected void GridViewUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string key = GridViewUser.DataKeys[e.RowIndex].Values[0].ToString();
                bool result = InstallUserBLL.Instance.DeleteInstallUser(Convert.ToInt32(key));
                binddata();

                if (result)
                {
                    //    lblmsg.Visible = true;
                    //    lblmsg.CssClass = "success";
                    //    lblmsg.Text = "User has been deleted successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User Deleted Successfully');", true);
                }
            }
            catch (Exception ex)
            {
                //return ex
            }
        }

        protected void GridViewUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUser.EditIndex = e.NewEditIndex;
            binddata();
        }

        protected void GridViewUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridViewUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            StringBuilder strb = new StringBuilder();
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Find the DropDownList in the Row
                    DropDownList ddlStatus = (e.Row.FindControl("ddlStatus") as DropDownList);
                    //Select the status in DropDownList
                    string Status = Convert.ToString((e.Row.FindControl("lblStatus") as HiddenField).Value);
                    if (Status != "")
                    {
                        strb.Append("Start if ");
                        if (Status == "Interview Date")
                        {
                            Status = "InterviewDate";
                            ddlStatus.Items.FindByValue(Status).Selected = true;
                        }
                        else if (Status == "Offer Made")
                        {
                            Status = "OfferMade";
                            ddlStatus.Items.FindByValue(Status).Selected = true;
                        }
                        else if (Status == "Phone Screened")
                        {
                            Status = "PhoneScreened";
                            ddlStatus.Items.FindByValue(Status).Selected = true;
                        }
                        else
                        {
                            ddlStatus.Items.FindByValue(Status).Selected = true;
                        }

                    }
                    else
                    {
                        strb.Append("Start else ");
                        ddlStatus.Items.FindByValue("Applicant").Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                strb.Append("Exception " + ex);
                // Response.Write("" + ex.Message);
            }
            // lblErrNew.Text = Convert.ToString(strb);
        }

        protected void GridViewUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["JGPA"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            if (e.CommandName == "Edit")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                Label desig = (Label)(GridViewUser.Rows[index].Cells[4].FindControl("lblDesignation"));
                string designation = desig.Text;
                string ID1 = e.CommandArgument.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("select Usertype from tblInstallUsers where Id='" + ID1 + "' ", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                string type = "";
                while (rdr.Read())
                {
                    type = rdr[0].ToString();

                }
                con.Close();
                if (designation != "SubContractor" && type != "Sales")
                {
                    string ID = e.CommandArgument.ToString();
                    Response.Redirect("InstallCreateUser.aspx?id=" + ID);
                }
                else if (designation == "SubContractor" && type != "Sales")
                {
                    string ID = e.CommandArgument.ToString();
                    Response.Redirect("InstallCreateUser2.aspx?id=" + ID);
                }
                else if (type == "Sales")
                {
                    string ID = e.CommandArgument.ToString();
                    Response.Redirect("CreateSalesUser.aspx?id=" + ID);
                }

            }
            else if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                InstallUserBLL.Instance.DeleteInstallUser(id);
            }
            else if (e.CommandName == "ShowPicture")
            {
                string ImagePath = "";
                string ImageName = e.CommandArgument.ToString();
                ImagePath = "UploadedFile/" + Path.GetFileName(ImageName);
                img_InstallerImage.ImageUrl = ImagePath;
                mp1.Show();
            }
            else if (e.CommandName == "ChangeStatus")
            {
                GridViewRow gvRow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int Index = gvRow.RowIndex;
                DropDownList ddlStatus = (DropDownList)gvRow.FindControl("ddlStatus");
                int StatusId = Convert.ToInt32(e.CommandArgument);
                string Status = ddlStatus.SelectedValue;
                bool result = InstallUserBLL.Instance.UpdateInstallUserStatus(Status, StatusId);
            }

        }

        protected void GridViewUser_Sorting(object sender, GridViewSortEventArgs e)
        {
            binddata();
            DataTable dt = new DataTable();
            dt = (DataTable)(Session["GridDataSort"]);
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                GridViewUser.DataSource = sortedView;
                GridViewUser.DataBind();
            }
        }

        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (BulkProspectUploader.HasFile)
                {
                    string ext = Path.GetExtension(BulkProspectUploader.FileName);
                    if (ext == ".xls" || ext == ".xlsx")
                    {
                        string FileName = Path.GetFileName(BulkProspectUploader.PostedFile.FileName);
                        string Extension = Path.GetExtension(BulkProspectUploader.PostedFile.FileName);
                        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                        string FilePath = Server.MapPath(FolderPath + FileName);
                        BulkProspectUploader.SaveAs(FilePath);
                        Import_To_Grid(FilePath, Extension);
                        binddata();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Select xls or xlsx file')", true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //private string GetId(string UserType, string UserStatus)
        //{
        //    DataTable dtId;
        //    string installId = string.Empty;

        //    string newId = string.Empty;
        //    dtId = InstallUserBLL.Instance.getMaxId(UserType, UserStatus);
        //    if (dtId.Rows.Count > 0)
        //    {
        //        installId = Convert.ToString(dtId.Rows[0][0]);
        //    }
        //    if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Applicant" || UserStatus == "InterviewDate" || UserStatus == "OfferMade" || UserStatus == "PhoneScreened" || UserStatus == "Rejected"))
        //    {
        //        if (installId != "")
        //        {
        //            newId = installId.Substring(10);
        //            newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
        //            installId = installId.Remove(10);
        //            installId = installId + newId;
        //        }
        //        else
        //        {
        //            installId = "P-OPP-00001";
        //        }
        //    }
        //    else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Active"))
        //    {
        //        if (installId != "")
        //        {
        //            newId = installId.Substring(8);
        //            newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
        //            installId = installId.Remove(8);
        //            installId = installId + newId;
        //        }
        //        else
        //        {
        //            installId = "OPP-00001";
        //        }
        //    }
        //    else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Deactive"))
        //    {
        //        if (installId != "")
        //        {
        //            newId = installId.Substring(10);
        //            newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
        //            installId = installId.Remove(10);
        //            installId = installId + newId;
        //        }
        //        else
        //        {
        //            installId = "X-OPP-00001";
        //        }
        //    }
        //    else if ((UserType == "SubContractor") && (UserStatus == "Applicant" || UserStatus == "InterviewDate" || UserStatus == "OfferMade" || UserStatus == "PhoneScreened" || UserStatus == "Rejected"))
        //    {
        //        if (installId != "")
        //        {
        //            newId = installId.Substring(8);
        //            newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
        //            installId = installId.Remove(8);
        //            installId = installId + newId;
        //        }
        //        else
        //        {
        //            installId = "P-SC-00001";
        //        }
        //    }
        //    else if ((UserType == "SubContractor") && (UserStatus == "Active"))
        //    {
        //        if (installId != "")
        //        {
        //            newId = installId.Substring(6);
        //            newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
        //            installId = installId.Remove(6);
        //            installId = installId + newId;
        //        }
        //        else
        //        {
        //            installId = "SC-00001";
        //        }
        //    }
        //    else if ((UserType == "SubContractor") && (UserStatus == "Deactive"))
        //    {
        //        if (installId != "")
        //        {
        //            newId = installId.Substring(8);
        //            newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
        //            installId = installId.Remove(8);
        //            installId = installId + newId;
        //        }
        //        else
        //        {
        //            installId = "X-SC-00001";
        //        }
        //    }
        //    Session["installId"] = installId;
        //    return installId;
        //}

        private string GetId(string UserType, string UserStatus)
        {
            DataTable dtId;
            string LastInt = string.Empty;
            string installId = string.Empty;

            dtId = InstallUserBLL.Instance.getMaxId(UserType, UserStatus);
            if (dtId.Rows.Count > 0)
            {
                installId = Convert.ToString(dtId.Rows[0][0]);
            }
            if ((UserType == "ForeMan") && (UserStatus != "Deactive"))
            {
                if (installId != "")
                {
                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "FRM000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);

                    //IndexS = installId.IndexOf("FRM");
                    //PrefixId = installId.Substring(IndexS, 1);
                    //previouPrefix = installId.Substring(0, IndexS);
                    //PrefixId = previouPrefix + PrefixId;
                    //newId = installId.Substring(IndexS + 6);
                    //newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //PrefixId = PrefixId + "FRM000" + newId;
                    //}
                    //if (installId.Contains("INS"))
                    //{
                    //    IndexS = installId.IndexOf("INS");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "FRM000" + newId;
                    //}
                    //if (installId.Contains("SUB"))
                    //{
                    //    IndexS = installId.IndexOf("SUB");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "FRM000" + newId;
                    //}

                }
                else
                {
                    installId = "FRM0001";
                }
            }
            else if (UserType == "ForeMan" && UserStatus == "Deactive")
            {
                installId = installId + "-X";
            }
            if (UserType == "Installer" && UserStatus != "Deactive")
            {
                if (installId != "")
                {

                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "INS000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);

                    //IndexS = installId.IndexOf("INS");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "INS000" + newId;
                    //}
                    //else if (installId.Contains("FRM"))
                    //{
                    //    IndexS = installId.IndexOf("FRM");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "INS000" + newId;
                    //}
                    //if (installId.Contains("SUB"))
                    //{
                    //    IndexS = installId.IndexOf("SUB");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "INS000" + newId;
                    //}
                }
                else
                {
                    installId = "INS0001";
                }
            }
            else if (UserType == "Installer" && UserStatus == "Deactive")
            {
                installId = installId + "-X";
            }


            if ((UserType == "SubContractor") && (UserStatus != "Deactive"))
            {
                if (installId != "")
                {
                    LastInt = installId.Substring(installId.Length - 4);
                    installId = "SUB000" + Convert.ToString(Convert.ToInt32(LastInt) + 1);

                    //if (installId.Contains("SUB"))
                    //{
                    //    IndexS = installId.IndexOf("SUB");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "SUB000" + newId;
                    //}
                    //else if (installId.Contains("FRM"))
                    //{
                    //    IndexS = installId.IndexOf("FRM");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "SUB000" + newId;
                    //}
                    //if (installId.Contains("INS"))
                    //{
                    //    IndexS = installId.IndexOf("INS");
                    //    PrefixId = installId.Substring(IndexS, 1);
                    //    previouPrefix = installId.Substring(0, IndexS);
                    //    PrefixId = previouPrefix + PrefixId;
                    //    newId = installId.Substring(IndexS + 6);
                    //    newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
                    //    PrefixId = PrefixId + "SUB000" + newId;
                    //}
                }
                else
                {
                    installId = "SUB0001";
                }
            }
            else if (UserType == "SubContractor" && UserStatus == "Deactive")
            {
                installId = installId + "-X";
            }

            //else if ((UserType == "ForeMan" || UserType == "") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "OPP-00001";
            //    }
            //}
            //else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(10);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(10);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "SUB-0001-X";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus != "Active" && UserStatus != "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "P-SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(6);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(6);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "X-SC-00001";
            //    }
            //}





            //if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus != "Active" && UserStatus != "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(10);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(10);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "P-OPP-00001";
            //    }
            //}
            //else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "OPP-00001";
            //    }
            //}
            //else if ((UserType == "ForeMan" || UserType == "Installer") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(10);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(10);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "X-OPP-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus != "Active" && UserStatus != "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "P-SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Active"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(6);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(6);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "SC-00001";
            //    }
            //}
            //else if ((UserType == "SubContractor") && (UserStatus == "Deactive"))
            //{
            //    if (installId != "")
            //    {
            //        newId = installId.Substring(8);
            //        newId = Convert.ToString(Convert.ToUInt32(newId) + 1);
            //        installId = installId.Remove(8);
            //        installId = installId + newId;
            //    }
            //    else
            //    {
            //        installId = "X-SC-00001";
            //    }
            //}
            Session["installId"] = installId;
            Session["IdGenerated"] = installId;
            return installId;
        }

        private void Import_To_Grid(string FilePath, string Extension)
        {
            string conStr = "";
            int count = 0;
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
                    //conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                    //         .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0;";
                    //conStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
                    //conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                    //          .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dtExcel = new DataTable();
            cmdExcel.Connection = connExcel;
            string IdGenerated = "";
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dtExcel);

            for (int i = 0; i < dtExcel.Rows.Count; i++)
            {
                try
                {
                    objuser.fristname = dtExcel.Rows[i][0].ToString().Trim();
                    objuser.lastname = dtExcel.Rows[i][1].ToString().Trim();
                    objuser.CompanyName = dtExcel.Rows[i][2].ToString().Trim();
                    objuser.phone = dtExcel.Rows[i][3].ToString().Trim();
                    objuser.Phone2 = dtExcel.Rows[i][4].ToString().Trim();
                    objuser.email = dtExcel.Rows[i][5].ToString().Trim();
                    objuser.Email2 = dtExcel.Rows[i][6].ToString().Trim();
                    objuser.PrimeryTradeId = Convert.ToInt32(dtExcel.Rows[i][7].ToString().Trim());
                    objuser.SecondoryTradeId = Convert.ToInt32(dtExcel.Rows[i][8].ToString().Trim());
                    objuser.SourceUser = Convert.ToString(Session["userid"]);
                    objuser.Source = Convert.ToString(Session["Username"]);
                    objuser.designation = dtExcel.Rows[i][11].ToString().Trim();
                    objuser.status = dtExcel.Rows[i][12].ToString().Trim();
                    DataSet dsCheckDuplicate = InstallUserBLL.Instance.CheckInstallUser(dtExcel.Rows[i][5].ToString().Trim(), dtExcel.Rows[i][3].ToString().Trim());
                    if (dsCheckDuplicate.Tables[0].Rows.Count == 0) //Initially ....           
                    {
                        IdGenerated = GetId(dtExcel.Rows[i][11].ToString().Trim(), dtExcel.Rows[i][12].ToString().Trim());
                        objuser.InstallId = IdGenerated;
                        DataSet ds = InstallUserBLL.Instance.CheckSource(Convert.ToString(Session["Username"]));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //do nothing
                        }
                        else
                        {
                            DataSet dsadd = InstallUserBLL.Instance.AddSource(Convert.ToString(Session["Username"]));
                        }
                        objuser.DateSourced = Convert.ToString(dtExcel.Rows[i][9].ToString());
                        objuser.Notes = dtExcel.Rows[i][10].ToString().Trim();
                        bool result = InstallUserBLL.Instance.AddUser(objuser);
                        count += Convert.ToInt32(result);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prospect Uploaded Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Upload file contains data error or matching data exists, please check and upload again');", true);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            //if (count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Upload file contains data error or matching data exists, please check and upload again');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Prospect Uploaded Successfully');", true);
            //}
            connExcel.Close();
        }

        //Hide Password popup....       
        //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Below 4 lines is to get that particular row control values
        //    DropDownList ddlNew = sender as DropDownList;
        //    string strddlNew = ddlNew.SelectedValue;
        //    Session["Status"] = strddlNew;
        //    GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        //    Label lblDesignation = (Label)(grow.FindControl("lblDesignation"));
        //    Label lblFirstName = (Label)(grow.FindControl("lblFirstName"));
        //    Label lblLastName = (Label)(grow.FindControl("lblLastName"));
        //    HiddenField lblStatus = (HiddenField)(grow.FindControl("lblStatus"));
        //    Label Id = (Label)grow.FindControl("lblid");
        //    DropDownList ddl = (DropDownList)grow.FindControl("ddlStatus");
        //    Session["EditId"] = Id.Text;
        //    Session["EditStatus"] = ddl.SelectedValue;
        //    Session["DesignitionSC"] = lblDesignation.Text;
        //    Session["FirstNameNewSC"] = lblFirstName.Text;
        //    Session["LastNameNewSC"] = lblLastName.Text;

        //    if ((lblStatus.Value == "Active") && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {
        //        binddata();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have rights change the status.')", true);
        //        return;
        //    }
        //    else if ((lblStatus.Value == "Active" && ddl.SelectedValue != "Deactive") && ((Convert.ToString(Session["usertype"]).Contains("Admin")) || (Convert.ToString(Session["usertype"]).Contains("SM"))))
        //   // else if (lblStatus.Value == "Active"  && ((Convert.ToString(Session["usertype"]).Contains("Admin")) || (Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {/*
        //        //As per client discussion....
        //        int isvaliduser = 0;
        //        isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtPassword.Text);
        //        if (isvaliduser > 0)
        //        {
        //            InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
        //            binddata();
        //        }*/
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
        //        return;
        //    }
        //    //else if ((lblStatus.Value == "Active" && ddl.SelectedValue != "Deactive") && (Convert.ToString(Session["usertype"]).Contains("SM")))
        //    //{
        //    //     ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
        //    //     return;
        //    //}
        //    bool status = CheckRequiredFields(ddl.SelectedValue, Convert.ToInt32(Id.Text));
        //    if (!status)
        //    {
        //        binddata();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed as required field for selected status are not field')", true);
        //        return;
        //    }
        //    if (ddl.SelectedValue == "Install Prospect")
        //    {
        //        ddl.SelectedValue = lblStatus.Value;
        //        //binddata();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to InstallProspect')", true);
        //        return;
        //    }
        //    if ((ddl.SelectedValue == "Active" || ddl.SelectedValue == "Deactive") && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {
        //        ddl.SelectedValue = Convert.ToString(lblStatus.Value);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
        //        return;
        //    }
        //    else if (ddl.SelectedValue == "Rejected")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlay()", true);
        //        return;
        //    }
        //    else if (ddl.SelectedValue == "InterviewDate")
        //    {
        //        ddlInsteviewtime.DataSource = GetTimeIntervals();
        //        ddlInsteviewtime.DataBind();
        //        dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
        //        ddlInsteviewtime.SelectedValue = "10:00 AM";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayInterviewDate()", true);
        //        return;
        //    }
        //    else if (ddl.SelectedValue == "Deactive" && ((Convert.ToString(Session["usertype"]).Contains("Admin")) && (Convert.ToString(Session["usertype"]).Contains("SM"))))
        //    {
        //        Session["DeactivationStatus"] = "Deactive";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlay()", true);
        //        return;
        //    }
        //    else if (ddl.SelectedValue == "OfferMade")
        //    {
        //        DataSet ds = new DataSet();
        //        string email = "";
        //        string HireDate = "";
        //        string EmpType = "";
        //        string PayRates = "";
        //        ds = InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
        //        if (ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
        //                {
        //                    email = Convert.ToString(ds.Tables[0].Rows[0][0]);
        //                }
        //                if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
        //                {
        //                    HireDate = Convert.ToString(ds.Tables[0].Rows[0][1]);
        //                }
        //                if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
        //                {
        //                    EmpType = Convert.ToString(ds.Tables[0].Rows[0][2]);
        //                }
        //                if (Convert.ToString(ds.Tables[0].Rows[0][3]) != "")
        //                {
        //                    PayRates = Convert.ToString(ds.Tables[0].Rows[0][3]);
        //                }
        //            }
        //        }
        //        SendEmail(email, lblFirstName.Text, lblLastName.Text, "Offer Made", txtReason.Text, lblDesignation.Text, HireDate, EmpType, PayRates);
        //        binddata();
        //        return;
        //    }

        //    if (lblStatus.Value == "Active" && (!(Convert.ToString(Session["usertype"]).Contains("Admin"))))
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to any other status other than Deactive once user is Active')", true);
        //        if (Convert.ToString(Session["PreviousStatusNew"]) != "")
        //        {
        //            ddl.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
        //        }
        //        return;
        //    }
        //    else
        //    {
        //        InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
        //        binddata();
        //        return;
        //    }

        //    if (ddl.SelectedValue == "Install Prospect")
        //    {
        //        if (lblStatus.Value != "")
        //        {
        //            ddl.SelectedValue = lblStatus.Value;
        //        }
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Install Prospect')", true);
        //        return;
        //    }

        //    //else
        //    //{

        //    //    int StatusId = Convert.ToInt32(Id.Text);
        //    //    string Status = ddl.SelectedValue;
        //    //    //bool result = InstallUserBLL.Instance.UpdateInstallUserStatus(Status, StatusId);
        //    //    InstallUserBLL.Instance.ChangeStatus(Status, StatusId, Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
        //    //}

        //    //call: updateStauts() function to update it in database.
        //}

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Below 4 lines is to get that particular row control values
            DropDownList ddlNew = sender as DropDownList;
            string strddlNew = ddlNew.SelectedValue;
            Session["Status"] = strddlNew;
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            Label lblDesignation = (Label)(grow.FindControl("lblDesignation"));
            Label lblFirstName = (Label)(grow.FindControl("lblFirstName"));

            Label lblLastName = (Label)(grow.FindControl("lblLastName"));

            Session["Firstname"] = lblFirstName.Text;
            Session["LastName"] = lblLastName.Text;
            Session["Designation"] = lblDesignation.Text;

            HiddenField lblStatus = (HiddenField)(grow.FindControl("lblStatus"));
            Label Id = (Label)grow.FindControl("lblid");
            DropDownList ddl = (DropDownList)grow.FindControl("ddlStatus");
            Session["EditId"] = Id.Text;
            Session["EditStatus"] = ddl.SelectedValue;
            Session["DesignitionSC"] = lblDesignation.Text;
            Session["FirstNameNewSC"] = lblFirstName.Text;
            Session["LastNameNewSC"] = lblLastName.Text;

            // if ((lblStatus.Value == "Active") && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
            if ((!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
            {
                binddata();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have rights change the status.')", true);
                return;
            }
            // else if ((lblStatus.Value == "Active" && ddl.SelectedValue != "Deactive") && ((Convert.ToString(Session["usertype"]).Contains("Admin")) || (Convert.ToString(Session["usertype"]).Contains("SM"))))
            else if (lblStatus.Value == "Active" && ((Convert.ToString(Session["usertype"]).Contains("Admin")) || (Convert.ToString(Session["usertype"]).Contains("SM"))))
            {/*
                //As per client discussion....
                int isvaliduser = 0;
                isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtPassword.Text);
                if (isvaliduser > 0)
                {
                    InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                    binddata();
                }*/

                if (ddl.SelectedValue == "Install Prospect")
                {
                    ddl.SelectedValue = lblStatus.Value;
                    //binddata();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to InstallProspect')", true);
                    return;
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
                    return;
                }
            }
            //else if ((lblStatus.Value == "Active" && ddl.SelectedValue != "Deactive") && (Convert.ToString(Session["usertype"]).Contains("SM")))
            //{
            //     ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayPassword();", true);
            //     return;
            //}
            bool status = CheckRequiredFields(ddl.SelectedValue, Convert.ToInt32(Id.Text));
            if (!status)
            {
                binddata();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed as required field for selected status are not field')", true);
                return;
            }
            if (ddl.SelectedValue == "Install Prospect")
            {
                // ddl.SelectedValue = lblStatus.Value;
                //binddata();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to InstallProspect')", true);
                return;
            }
            if ((ddl.SelectedValue == "Active" || ddl.SelectedValue == "Deactive") && (!(Convert.ToString(Session["usertype"]).Contains("Admin")) && !(Convert.ToString(Session["usertype"]).Contains("SM"))))
            {
                ddl.SelectedValue = Convert.ToString(lblStatus.Value);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You dont have permission to Activate or Deactivate user')", true);
                return;
            }
            else if (ddl.SelectedValue == "Rejected")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlay()", true);
                return;
            }
            else if (ddl.SelectedValue == "InterviewDate")
            {
                ddlInsteviewtime.DataSource = GetTimeIntervals();
                ddlInsteviewtime.DataBind();
                dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
                ddlInsteviewtime.SelectedValue = "10:00 AM";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayInterviewDate()", true);
                return;
            }
            else if (ddl.SelectedValue == "Deactive" && ((Convert.ToString(Session["usertype"]).Contains("Admin")) && (Convert.ToString(Session["usertype"]).Contains("SM"))))
            {
                Session["DeactivationStatus"] = "Deactive";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlay()", true);
                return;
            }
            else if (ddl.SelectedValue == "OfferMade")
            {
                DataSet ds = new DataSet();
                string email = "";
                string HireDate = "";
                string EmpType = "";
                string PayRates = "";
                ds = InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                        {
                            email = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
                        {
                            HireDate = Convert.ToString(ds.Tables[0].Rows[0][1]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
                        {
                            EmpType = Convert.ToString(ds.Tables[0].Rows[0][2]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][3]) != "")
                        {
                            PayRates = Convert.ToString(ds.Tables[0].Rows[0][3]);
                        }
                    }
                }
                SendEmail(email, lblFirstName.Text, lblLastName.Text, "Offer Made", txtReason.Text, lblDesignation.Text, HireDate, EmpType, PayRates);
                binddata();
                return;
            }

            if (lblStatus.Value == "Active" && (!(Convert.ToString(Session["usertype"]).Contains("Admin"))))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to any other status other than Deactive once user is Active')", true);
                if (Convert.ToString(Session["PreviousStatusNew"]) != "")
                {
                    ddl.SelectedValue = Convert.ToString(Session["PreviousStatusNew"]);
                }
                return;
            }
            else
            {
                InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                binddata();
                return;
            }

            if (ddl.SelectedValue == "Install Prospect")
            {
                if (lblStatus.Value != "")
                {
                    ddl.SelectedValue = lblStatus.Value;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Install Prospect')", true);
                return;
            }

            //else
            //{

            //    int StatusId = Convert.ToInt32(Id.Text);
            //    string Status = ddl.SelectedValue;
            //    //bool result = InstallUserBLL.Instance.UpdateInstallUserStatus(Status, StatusId);
            //    InstallUserBLL.Instance.ChangeStatus(Status, StatusId, Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]));
            //}

            //call: updateStauts() function to update it in database.
        }


        private bool CheckRequiredFields(string SelectedStatus, int Id)
        {
            DataSet dsNew = new DataSet();
            dsNew = InstallUserBLL.Instance.getuserdetails(Id);
            if (dsNew.Tables.Count > 0)
            {
                if (dsNew.Tables[0].Rows.Count > 0)
                {
                    if (SelectedStatus == "Applicant")
                    {
                        if (Convert.ToString(dsNew.Tables[0].Rows[0][1]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][2]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][3]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][8]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][36]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][37]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][38]) == "")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Applicant as required fields for it are not filled.')", true);
                            return false;
                        }
                    }
                    else if (SelectedStatus == "OfferMade" || SelectedStatus == "Offer Made")
                    {
                        if (Convert.ToString(dsNew.Tables[0].Rows[0][1]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][2]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][4]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][5]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][11]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][12]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][13]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][3]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][8]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][36]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][37]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][38]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][44]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][46]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][48]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][50]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][100]) == "")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Offer Made as required fields for it are not filled.')", true);
                            return false;
                        }
                    }
                    else if (SelectedStatus == "Active")
                    {
                        if (Convert.ToString(dsNew.Tables[0].Rows[0][1]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][2]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][3]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][4]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][5]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][7]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][9]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][11]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][12]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][13]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][17]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][16]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][17]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][8]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][18]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][19]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][20]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][35]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][36]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][37]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][38]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][39]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][44]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][46]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][48]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][50]) == "" || Convert.ToString(dsNew.Tables[0].Rows[0][100]) == "")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Status cannot be changed to Offer Made as required fields for it are not filled.')", true); 
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void SendEmail(string emailId, string FName, string LName, string status, string Reason, string Designition, string HireDate, string EmpType, string PayRates)
        {
            try
            {
                string fullname = FName + " " + LName;
                string HTML_TAG_PATTERN = "<.*?>";
                string strHeader = GetEmailHeader(status);
                string strBody = GetEmailBody(status);
                string strFooter = GetFooter(status);
                strBody = strBody.Replace("LBL name", FName);
                strBody = strBody.Replace("Lbl Full name", fullname);
                strBody = strBody.Replace("LBL position", Designition);
                //strBody = strBody.Replace("lbl: start date", txtHireDate.Text);
                //strBody = strBody.Replace("($ rate","$"+ txtHireDate.Text);
                strBody = strBody.Replace("Reason", Reason);
                //strBody = Regex.Replace(strBody, HTML_TAG_PATTERN, string.Empty);
                //strHeader = Regex.Replace(strHeader, HTML_TAG_PATTERN, string.Empty);
                //strFooter = Regex.Replace(strFooter, HTML_TAG_PATTERN, string.Empty);
                StringBuilder Body = new StringBuilder();
                //MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                //Msg.From = new MailAddress("qat2015team@gmail.com");
                // Recipient e-mail address.
                //Msg.To.Add(emailId);
                //Msg.Subject = "JG Prospect Notification";
                //StringBuilder Body = new StringBuilder();
                //Body.Append("Hello " + FName + " " + LName + ",");
                //Body.Append("<br>");
                //Body.Append("Your stattus for the JG Prospect is :" + status);
                //Body.Append("<br>");
                ////if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
                ////{
                //Body.Append(Reason);
                ////}
                //Body.Append("<br>");
                //Body.Append("Tanking you");
                Body.Append(strHeader);
                Body.Append("<br>");
                Body.Append(strBody);
                Body.Append("<br>");
                //if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
                //{
                //Body.Append(Reason);
                //}
                Body.Append("<br>");
                Body.Append(strFooter);
                if (status == "OfferMade")
                {
                    createForeMenForJobAcceptance(Convert.ToString(Body), FName, LName, Designition, emailId, HireDate, EmpType, PayRates);
                }
                if (status == "Deactive")
                {
                    CreateDeactivationAttachment(Convert.ToString(Body), FName, LName, Designition, emailId, HireDate, EmpType, PayRates);
                }
                //Msg.Body = Convert.ToString(Body);
                //// your remote SMTP server IP.
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.Credentials = new System.Net.NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                //smtp.EnableSsl = true;
                //smtp.Send(Msg);
                //Msg = null;
                //Page.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            // //SmtpClient smtp = new SmtpClient();
            // //MailMessage email_msg = new MailMessage();
            // //email_msg.To.Add(emailId);
            // //email_msg.From = new MailAddress("customsoft.test@gmail.com", "Credit Chex");
            // StringBuilder Body = new StringBuilder();
            // Body.Append("Hello " + FName + " " + LName + ",");
            // Body.Append("<br>");
            // Body.Append("Your stattus for the JG Prospect is :" + status);
            // Body.Append("<br>");
            // //if (status == "Source" || status == "Rejected" || status == "Interview Date" || status == "Offer Made")
            // //{
            //     Body.Append(Reason);
            // //}
            // Body.Append("<br>");
            // Body.Append("Tanking you");
            //// AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
            //// LinkedResource imagelink3 = new LinkedResource(HttpContext.Current.Server.MapPath("~/images/logo.png"), "image/png");
            // //imagelink3.ContentId = "imageId1";
            // //imagelink3.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            // //htmlView.LinkedResources.Add(imagelink3);
            // var smtp = new System.Net.Mail.SmtpClient();
            // {
            //     smtp.Host = "smtp.gmail.com";
            //     smtp.Port = 587;
            //     smtp.EnableSsl = true;
            //     smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //     smtp.Credentials = new NetworkCredential("","q$7@wt%j*j*65ba#3M@9P6");
            //     smtp.Timeout = 20000;
            // }
            // // Passing values to smtp object
            // smtp.Send("", emailId, "JG Prospect Notification", Convert.ToString(Body));

        }

        private string GetFooter(string status)
        {
            string Footer = string.Empty;
            DataTable DtFooter;
            DtFooter = InstallUserBLL.Instance.getTemplate(status, "footer");
            if (DtFooter.Rows.Count > 0)
            {
                Footer = DtFooter.Rows[0][0].ToString();
            }
            return Footer;
        }

        private string GetEmailBody(string status)
        {
            string Body = string.Empty;
            DataTable DtBody;
            DtBody = InstallUserBLL.Instance.getTemplate(status, "Body");
            if (DtBody.Rows.Count > 0)
            {
                Body = DtBody.Rows[0][0].ToString();
            }
            return Body;
        }

        private string GetEmailHeader(string status)
        {
            string Header = string.Empty;
            DataTable DtHeader;
            DtHeader = InstallUserBLL.Instance.getTemplate(status, "Header");
            if (DtHeader.Rows.Count > 0)
            {
                Header = DtHeader.Rows[0][0].ToString();
            }
            return Header;
        }

        private void FindAndReplace(Word.Application wordApp, object findText, object replaceText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref findText, ref matchCase,
                ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format,
                ref replaceText, ref replace, ref matchKashida,
                        ref matchDiacritics,
                ref matchAlefHamza, ref matchControl);
        }

        public void createForeMenForJobAcceptance(string str_Body, string FName, string LName, string Designition, string emailId, string HireDate, string EmpType, string PayRates)
        {
            //copy sample file for Foreman Job Acceptance letter template
            string str_date = DateTime.Now.ToString().Replace("/", "");
            str_date = str_date.Replace(":", "");
            str_date = str_date.Replace("-", "");
            str_date = str_date.Replace(" ", "");
            string SourcePath = @"~/Sr_App/MailDocSample/ForemanJobAcceptancelettertemplate.docx";
            string TargetPath = @"~/Sr_App/MailDocument/" + str_date + FName + "ForemanJobAcceptanceletter.docx";
            System.IO.File.Copy(Server.MapPath(SourcePath), Server.MapPath(TargetPath), true);
            //modify word document
            object missing = System.Reflection.Missing.Value;
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = null;
            object Target = Server.MapPath(TargetPath);
            if (File.Exists(Server.MapPath(TargetPath)))
            {
                DateTime today = DateTime.Now;
                object readonlyNew = false;
                object isVisible = false;
                wordApp.Visible = false;
                FileInfo objFInfo = new FileInfo(Server.MapPath(TargetPath));
                objFInfo.IsReadOnly = false;
                aDoc = wordApp.Documents.Open(ref Target, ref missing, ref readonlyNew, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
                aDoc.Activate();
                this.FindAndReplace(wordApp, "LBL Date", DateTime.Now.ToShortDateString());
                this.FindAndReplace(wordApp, "Lbl Full name", FName + " " + LName);
                this.FindAndReplace(wordApp, "LBL name", FName + " " + LName);
                this.FindAndReplace(wordApp, "LBL position", Designition);
                this.FindAndReplace(wordApp, "lbl fulltime", EmpType);
                this.FindAndReplace(wordApp, "lbl: start date", HireDate);
                this.FindAndReplace(wordApp, "$ rate", PayRates);
                this.FindAndReplace(wordApp, "lbl: next pay period", "");
                this.FindAndReplace(wordApp, "lbl: paycheck date", "");
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
            }
            using (MailMessage mm = new MailMessage("qat2015team@gmail.com", emailId))
            {
                try
                {
                    mm.Subject = "Foreman Job Acceptance";
                    mm.Body = str_Body;
                    mm.Attachments.Add(new Attachment(Server.MapPath(TargetPath)));
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "')", true);
                }
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
            }
        }


        private void CreateDeactivationAttachment(string MailBody, string FName, string LName, string Designition, string emailId, string HireDate, string EmpType, string PayRates)
        {
            string str_date = DateTime.Now.ToString().Replace("/", "");
            str_date = str_date.Replace(":", "");
            str_date = str_date.Replace("-", "");
            str_date = str_date.Replace(" ", "");
            string SourcePath = @"~/Sr_App/MailDocSample/DeactivationMail.doc";
            string TargetPath = @"~/Sr_App/MailDocument/" + str_date + FName + "DeactivationMail.doc";
            System.IO.File.Copy(Server.MapPath(SourcePath), Server.MapPath(TargetPath), true);
            //modify word document
            object missing = System.Reflection.Missing.Value;
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = null;
            object Target = Server.MapPath(TargetPath);
            if (File.Exists(Server.MapPath(TargetPath)))
            {
                DateTime today = DateTime.Now;
                object readonlyNew = false;
                object isVisible = false;
                wordApp.Visible = false;
                FileInfo objFInfo = new FileInfo(Server.MapPath(TargetPath));
                objFInfo.IsReadOnly = false;
                aDoc = wordApp.Documents.Open(ref Target, ref missing, ref readonlyNew, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
                aDoc.Activate();
                this.FindAndReplace(wordApp, "name", FName + " " + LName);
                this.FindAndReplace(wordApp, "HireDate", HireDate);
                this.FindAndReplace(wordApp, "full time or part  time", EmpType);
                this.FindAndReplace(wordApp, "HourlyRate", PayRates);
                this.FindAndReplace(wordApp, "WorkingStatus", "No");
                this.FindAndReplace(wordApp, "LastWorkingDay", DateTime.Now.ToShortDateString());
                aDoc.SaveAs(ref Target, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.Close(ref missing, ref missing, ref missing);
            }
            using (MailMessage mm = new MailMessage("qat2015team@gmail.com", emailId))
            {
                try
                {
                    mm.Subject = "Deactivation";
                    mm.Body = MailBody;
                    mm.Attachments.Add(new Attachment(Server.MapPath(TargetPath)));
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("qat2015team@gmail.com", "q$7@wt%j*65ba#3M@9P6");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "')", true);
                }
            }
        }

        public List<string> GetTimeIntervals()
        {
            List<string> timeIntervals = new List<string>();
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
            for (int i = 0; i < 48; i++)
            {
                int minutesToBeAdded = 30 * i;      // Increasing minutes by 30 minutes interval
                TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
                TimeSpan t = startTime.Add(timeToBeAdded);
                DateTime result = startDate + t;
                timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
            }
            return timeIntervals;
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Users.xls"));
            Response.ContentType = "application/ms-excel";
            // DataSet ds = InstallUserBLL.Instance.getalluserdetails();
            DataTable dt = (DataTable)(Session["GridData"]);
            // dt.Columns.Remove("PrimeryTradeId");
            // dt.Columns.Remove("SecondoryTradeId");
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["DeactivationStatus"]) == "Deactive")
            {
                DataSet ds = new DataSet();
                string email = "";
                string HireDate = "";
                string EmpType = "";
                string PayRates = "";
                ds = InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                        {
                            email = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
                        {
                            HireDate = Convert.ToString(ds.Tables[0].Rows[0][1]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
                        {
                            EmpType = Convert.ToString(ds.Tables[0].Rows[0][2]);
                        }
                        if (Convert.ToString(ds.Tables[0].Rows[0][3]) != "")
                        {
                            PayRates = Convert.ToString(ds.Tables[0].Rows[0][3]);
                        }
                    }
                }
                SendEmail(email, Convert.ToString(Session["FirstNameNewSC"]), Convert.ToString(Session["LastNameNewSC"]), "Deactivation", txtReason.Text, Convert.ToString(Session["DesignitionSC"]), HireDate, EmpType, PayRates);
            }
            else
            {
                InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                binddata();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "ClosePopup()", true);
            return;
        }

        //Interview Details click......ID For Edit 
        protected void btnSaveInterview_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string email = "";
            string HireDate = "";
            string EmpType = "";
            string PayRates = "";
            // ds = InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
            ds = InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), dtInterviewDate.Text, ddlInsteviewtime.SelectedValue, Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                    {
                        email = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
                    {
                        HireDate = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
                    {
                        EmpType = Convert.ToString(ds.Tables[0].Rows[0][2]);
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0][3]) != "")
                    {
                        PayRates = Convert.ToString(ds.Tables[0].Rows[0][3]);
                    }
                }
            }
            SendEmail(email, Convert.ToString(Session["FirstNameNewSC"]), Convert.ToString(Session["LastNameNewSC"]), "Deactivation", txtReason.Text, Convert.ToString(Session["DesignitionSC"]), HireDate, EmpType, PayRates);
            binddata();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "ClosePopupInterviewDate()", true);
            return;
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            int isvaliduser = 0;
            isvaliduser = UserBLL.Instance.chklogin(Convert.ToString(Session["loginid"]), txtPassword.Text);
            if (isvaliduser > 0)
            {
                InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                binddata();
                if (Session["Status"] != null)
                {
                    string a = Convert.ToString(Session["Status"]);
                    if (a == "Rejected")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlay()", true);
                        return;
                    }
                    else if (a == "InterviewDate")
                    {
                        ddlInsteviewtime.DataSource = GetTimeIntervals();
                        ddlInsteviewtime.DataBind();
                        dtInterviewDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
                        ddlInsteviewtime.SelectedValue = "10:00 AM";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlayInterviewDate()", true);
                        return;
                    }
                    else if (a == "Deactive")
                    {
                        Session["DeactivationStatus"] = "Deactive";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Overlay", "overlay()", true);
                        return;
                    }
                    else if (a == "OfferMade")
                    {
                        DataSet ds = new DataSet();
                        string email = "";
                        string HireDate = "";
                        string EmpType = "";
                        string PayRates = "";
                        ds = InstallUserBLL.Instance.ChangeStatus(Convert.ToString(Session["EditStatus"]), Convert.ToInt32(Session["EditId"]), Convert.ToString(DateTime.Today.ToShortDateString()), DateTime.Now.ToShortTimeString(), Convert.ToInt32(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()]), txtReason.Text);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[0][0]) != "")
                                {
                                    email = Convert.ToString(ds.Tables[0].Rows[0][0]);
                                }
                                if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
                                {
                                    HireDate = Convert.ToString(ds.Tables[0].Rows[0][1]);
                                }
                                if (Convert.ToString(ds.Tables[0].Rows[0][2]) != "")
                                {
                                    EmpType = Convert.ToString(ds.Tables[0].Rows[0][2]);
                                }
                                if (Convert.ToString(ds.Tables[0].Rows[0][3]) != "")
                                {
                                    PayRates = Convert.ToString(ds.Tables[0].Rows[0][3]);
                                }
                            }
                        }
                        string First = Convert.ToString(Session["Firstname"]);
                        string Last = Convert.ToString(Session["LastName"]);
                        string Design = Convert.ToString(Session["Designation"]);


                        SendEmail(email, First, Last, "Offer Made", txtReason.Text, Design, HireDate, EmpType, PayRates);
                        binddata();
                        return;
                    }
                }

            }
            else
            {
                binddata();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter Correct password to change status.')", true);
            }
        }
    }
}