using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JG_Prospect.BLL;
using System.Text.RegularExpressions;
using JG_Prospect.Common;
using System.Configuration;
using JG_Prospect.Common.modal;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Ionic.Zip;
namespace JG_Prospect.Installer
{
    public partial class InstallerHome : System.Web.UI.Page
    {
        public string GridViewSortExpression
        {
            get
            {
                return ViewState[JGConstant.SortExpression] == null ? JGConstant.Sorting_ReferenceId : ViewState[JGConstant.SortExpression] as string;
            }
            set
            {
                ViewState[JGConstant.SortExpression] = value;
            }
        }

        public String GridViewSortDirection
        {
            get
            {
                if (ViewState[JGConstant.SortDirection] == null)
                    ViewState[JGConstant.SortDirection] = JGConstant.Sorting_SortDirection_DESC;

                return (string)ViewState[JGConstant.SortDirection];
            }
            set { ViewState[JGConstant.SortDirection] = value; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            DataSet ds = InstallUserBLL.Instance.GetJobsForInstaller();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                DataView sortedView = new DataView(dt);
                sortedView.Sort = GridViewSortExpression + " " + GridViewSortDirection;
                grdInstaller.DataSource = sortedView;
                grdInstaller.DataBind();
            }
        }
        protected void lblSignOffDocument_Click(object sender, EventArgs e)
        {

            string path = Server.MapPath("/CustomerDocs/Pdfs/");
            string fileName = "InstallationCompletionForm.pdf";

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
            Response.TransmitFile("~/CustomerDocs/Pdfs/" + fileName);
            Response.End();
        }
        protected void lnkAvailability_Click(object sender, EventArgs e)
        {
            LinkButton lnkAvailability = (LinkButton)sender;
            GridViewRow grdInstallerRow = (GridViewRow)lnkAvailability.Parent.Parent;
            Label lblReferenceId = (Label)grdInstallerRow.FindControl("lblReferenceId");
            HiddenField hdnJobSequenceId = (HiddenField)grdInstallerRow.FindControl("hdnJobSequenceId");
            ViewState[ViewStateKey.Key.ReferenceId.ToString()] = lblReferenceId.Text;
            ViewState[ViewStateKey.Key.JobSequenceId.ToString()] = Convert.ToInt16(hdnJobSequenceId.Value);

            int installerId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString());
            DataSet ds = InstallUserBLL.Instance.GetInstallerAvailability(lblReferenceId.Text.Trim(), installerId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPrimary.Text = ds.Tables[0].Rows[0]["Primary"].ToString();
                lblSecondary1.Text = ds.Tables[0].Rows[0]["Secondary1"].ToString();
                lblSecondary2.Text = ds.Tables[0].Rows[0]["Secondary2"].ToString();
            }
            else
            {
                txtPrimary.Text = "";
                txtSecondary1.Text = "";
                txtSecondary2.Text = "";
                lblPrimary.Text = "";
                lblSecondary1.Text = "";
                lblSecondary2.Text = "";
            }

            mpe.Show();
        }


        protected void btnSet_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder ContentPlaceHolder1 = (ContentPlaceHolder)btnSet.Parent.Parent;
           // GridView grdInstaller = (GridView)ContentPlaceHolder1.FindControl("grdInstaller");
            string primary = string.Empty, secondary1 = string.Empty, secondary2 = string.Empty;
            if (txtPrimary.Text.Trim() != string.Empty)
            {
                primary = InstallUserBLL.Instance.AddHoursToAvailability(Convert.ToDateTime(txtPrimary.Text.Trim(), JGConstant.CULTURE));
            }
            if (txtSecondary1.Text.Trim() != string.Empty)
            {
                secondary1 = InstallUserBLL.Instance.AddHoursToAvailability(Convert.ToDateTime(txtSecondary1.Text.Trim(), JGConstant.CULTURE));
            }
            if (txtSecondary2.Text.Trim() != string.Empty)
            {
                secondary2 = InstallUserBLL.Instance.AddHoursToAvailability(Convert.ToDateTime(txtSecondary2.Text.Trim(), JGConstant.CULTURE));
            }

            Availability a = new Availability();
            a.ReferenceId = ViewState[ViewStateKey.Key.ReferenceId.ToString()].ToString();
            //a.JobSequenceId = Convert.ToInt16(ViewState[ViewStateKey.Key.JobSequenceId.ToString()].ToString());
            a.InstallerId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString());
            a.Primary = primary;
            a.Secondary1 = secondary1;
            a.Secondary2 = secondary2;

            InstallUserBLL.Instance.AddEditInstallerAvailability(a);
            BindGrid();
        }

        protected void lnkJobPackets_Click(object sender, EventArgs e)
        {
            string path = "";
            string Extention = "";
            LinkButton lnkJobPackets = sender as LinkButton;
            //GridViewRow gr = (GridViewRow)lnkJobPackets.Parent.Parent;
            //HiddenField hdnproductid = (HiddenField)gr.FindControl("hdnproductid");
            //HiddenField hdnProductTypeId = (HiddenField)gr.FindControl("hdnProductTypeId");
            //Label lblCustomerIdJobId = (Label)gr.FindControl("lblCustomerIdJobId");
            //string[] Id = lblCustomerIdJobId.Text.Trim().Split('&');
            //string customerString = Id[0].Trim();
            //string resultString = Regex.Match(customerString, @"\d+").Value;
            //int customerId = Int32.Parse(resultString);
            //int productId = Convert.ToInt16(hdnproductid.Value);

            //int productTypeId = Convert.ToInt16(hdnProductTypeId.Value);
           // Label lblCategory = (Label)gr.FindControl("lblCategory");
           string []s=lnkJobPackets.Text.Trim().Split('-');
           string soldJobId = s[0] + "-" + s[1];
           //soldJobId = ViewStateKey.Key.SoldJobNo.ToString();
           DataSet ds = new DataSet();
           ds = new_customerBLL.Instance.GetCustomerJobPackets(soldJobId);
           //if (ds.Tables.Count > 0)
           //{
           //    if (Convert.ToString(ds.Tables[0].Rows[0][1]) != "")
           //    {
           //        Extention = Path.GetExtension(Convert.ToString(ds.Tables[0].Rows[0][1]));
           //        if (Extention == ".pdf")
           //        {
           //            ds.Tables[0].Rows[0][1] = "Pdfs/pdf.jpg";
           //        }
           //    }
           //}
           Gridviewdocs.DataSource = ds;
           Gridviewdocs.DataBind();
           ScriptManager.RegisterStartupScript(this, GetType(), "overlay", "overlay();", true);
            //ResponseHelper.Redirect("ZipJobPackets.aspx?productId=" + productId + "&productTypeId=" + productTypeId + "&customerId=" + customerId, "_blank", "menubar=0,width=605px,height=630px");
           //ResponseHelper.Redirect("ZipJobPackets.aspx?" + ViewStateKey.Key.SoldJobNo.ToString() +"=" + soldJobId, "_blank", "menubar=0,width=605px,height=630px");
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string UserName = "JG";
            ErrorMessage.InnerHtml = "";   // debugging only
            var filesToInclude = new System.Collections.Generic.List<String>();
            String sMappedPath = Server.MapPath("~/CustomerDocs");
            foreach (GridViewRow gvr in Gridviewdocs.Rows)
            {
                CheckBox chkbox = gvr.FindControl("checkbox1") as CheckBox;
                Label lbl = gvr.FindControl("labelfile") as Label;
                if (chkbox != null && lbl != null)
                {
                    if (chkbox.Checked)
                    {
                        ErrorMessage.InnerHtml += String.Format("adding file: {0}<br/>\n", lbl.Text);
                        filesToInclude.Add(System.IO.Path.Combine(sMappedPath, lbl.Text));
                    }
                }
            }

            if (filesToInclude.Count == 0)
            {
                ErrorMessage.InnerHtml += "You did not select any files?<br/>\n";
            }
            else
            {
                string pass = "0";
                Response.Clear();
                Response.BufferOutput = false;

                System.Web.HttpContext c = System.Web.HttpContext.Current;
                String ReadmeText = String.Format("README.TXT\n\nHello!\n\n" +
                                                 "This is a zip file that was dynamically generated at {0}\n" +
                                                 "by an ASP.NET Page running on the machine named '{1}'.\n" +
                                                 "The server type is: {2}\n" +
                                                 "The password used: '{3}'\n" +
                                                 "Encryption: {4}\n",
                                                 System.DateTime.Now.ToString("G"),
                                                 System.Environment.MachineName,
                                                 c.Request.ServerVariables["SERVER_SOFTWARE"],
                                                 "Nome",
                                                 (pass == "" || pass == string.Empty) ? EncryptionAlgorithm.WinZipAes256.ToString() : "None"
                                                 );
                string archiveName = String.Format("archive-{0}.zip", UserName + " " + DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "inline; filename=\"" + archiveName + "\"");

                using (ZipFile zip = new ZipFile())
                {
                    // the Readme.txt file will not be password-protected.
                    zip.AddEntry("Readme.txt", ReadmeText, Encoding.Default);
                    //if (!String.IsNullOrEmpty(tbPassword.Text))
                    //{
                    //    zip.Password = tbPassword.Text;
                    //    if (chkUseAes.Checked)
                    //        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    //}

                    // filesToInclude is a string[] or List<String>
                    zip.AddFiles(filesToInclude, "files");

                    zip.Save(Response.OutputStream);
                }
                Response.Close();
            }
        }

        protected void Gridviewdocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");
                string s = DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Contains(".pdf") == true)
                {
                    img.ImageUrl = "~/CustomerDocs/Pdfs/pdf.jpg";
                    img.Height = 90;
                    img.Width = 60;
                }
                else
                {
                    img.ImageUrl = DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Replace("/CustomerDocs/CustomerDocs", "/CustomerDocs");
                }
                if (DataBinder.Eval(e.Row.DataItem, "DocumentName").ToString().Contains("VendorQuotes") == true)
                {
                    //img.ImageUrl = "~/CustomerDocs/VendorQuotes/Quote.jpg";
                    //img.Height = 90;
                    //img.Width = 60;
                }
            }
        }


        public static class ResponseHelper
        {
            public static void Redirect(string url, string target, string windowFeatures)
            {
                HttpContext context = HttpContext.Current;

                if ((String.IsNullOrEmpty(target) ||
                    target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
                    String.IsNullOrEmpty(windowFeatures))
                {

                    context.Response.Redirect(url);
                }
                else
                {
                    Page page = (Page)context.Handler;
                    if (page == null)
                    {
                        throw new InvalidOperationException(
                            "Cannot redirect to new window outside Page context.");
                    }
                    url = page.ResolveClientUrl(url);

                    string script;
                    if (!String.IsNullOrEmpty(windowFeatures))
                    {
                        script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                    }
                    else
                    {
                        script = @"window.open(""{0}"", ""{1}"");";
                    }

                    script = String.Format(script, url, target, windowFeatures);
                    ScriptManager.RegisterStartupScript(page,
                        typeof(Page),
                        "Redirect",
                        script,
                        true);
                }
            }
        }

        protected void grdInstaller_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkAvailability = (LinkButton)e.Row.FindControl("lnkAvailability");
                Label lblReferenceId = (Label)e.Row.FindControl("lblReferenceId");
                HiddenField hdnColour = (HiddenField)e.Row.FindControl("hdnColour");
                HiddenField hdnJobSequenceId = (HiddenField)e.Row.FindControl("hdnJobSequenceId");
                int installerId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString());
                DataSet ds = InstallUserBLL.Instance.GetInstallerAvailability(lblReferenceId.Text.Trim(), installerId);

                string availability = string.Empty;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    availability = "Primary: " + ds.Tables[0].Rows[0]["Primary"].ToString() + Environment.NewLine + "Secondary1: " + ds.Tables[0].Rows[0]["Secondary1"].ToString() + Environment.NewLine + "Secondary2: " + ds.Tables[0].Rows[0]["Secondary2"].ToString();
                    lnkAvailability.ToolTip = availability;
                }
                if (hdnColour.Value.ToString() == JGConstant.COLOR_RED)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void grdInstaller_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            if (GridViewSortDirection == JGConstant.Sorting_SortDirection_ASC)
            {
                GridViewSortDirection = JGConstant.Sorting_SortDirection_DESC;
            }
            else
            {
                GridViewSortDirection = JGConstant.Sorting_SortDirection_ASC;
            };
            BindGrid();
        }

    }
}