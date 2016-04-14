using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JG_Prospect.BLL;
using System.IO;
using System.Runtime.InteropServices;

namespace JG_Prospect.Sr_App
{
    public partial class AddResources : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string path = "";
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ResourceType = ddltype.SelectedValue;         
            string filename;
            bool result;
            if (uploadlink.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Enter Web Link of resource or upload resource file ');", true);
                return;
            }
            else
            {
                try
                {
                    // Get the HttpFileCollection
                    HttpFileCollection hfc = Request.Files;                 
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];
                        path =  Path.GetDirectoryName(hpf.FileName);
                        if (hpf.ContentLength > 0)
                        {                            
                            hpf.SaveAs(Server.MapPath("~/Resources") + "\\" + ResourceType + "\\" + 
                              System.IO.Path.GetFileName(hpf.FileName));
                            //Response.Write("<b>File: </b>" + hpf.FileName + " <b>Size:</b> " +
                            //    hpf.ContentLength + " <b>Type:</b> " + hpf.ContentType + " Uploaded Successfully <br/>");
                        }
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }

                //bool result = UserBLL.Instance.SaveResources(ResourceLink, "", ResourceType);
                //filename = "/Resources/" + Guid.NewGuid().ToString().Substring(0, 4) + uploadlink.FileName;
                //path = Server.MapPath("/Resources/" + Guid.NewGuid().ToString().Substring(0, 4) + uploadlink.FileName);
                //path = Server.MapPath(filename);
                //uploadlink.PostedFile.SaveAs(path);
                //ResourceLink = ".." + filename;

                if (result == true)
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "success";
                    lblmsg.Text = "Resource Added successfully";
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "error";
                    lblmsg.Text = "There is some error in adding the Customer";
                }
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddltype.SelectedValue = "0";
            lblmsg.Visible = false;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process objP = new System.Diagnostics.Process();
            objP.StartInfo.UseShellExecute = false;
            objP.StartInfo.FileName = @"C:\Program Files\FileZilla FTP Client\filezilla.exe";
            objP.Start();
        }

    }
}