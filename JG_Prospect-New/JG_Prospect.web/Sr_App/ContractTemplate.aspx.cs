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
    public partial class ContractTemplate : System.Web.UI.Page
    {
        private static int ProductID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ProductId"].ToString() == "1")
                {
                    ProductID = 1;
                    bind(1);
                }
                else
                {
                    ProductID = 4;
                    bind(4);
                }
            }
        }

        protected void bind(int id)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(id);
           // ds= AdminBLL.Instance.FetchContractTemplate(id);
            if (ds != null)
            {
                string header = ds.Tables[0].Rows[0][0].ToString();
                header = header.Replace(@"width=""100%""", @"width=""1000""");
                HeaderEditor.Content = header;

                string footer = ds.Tables[0].Rows[0][2].ToString();
                footer = footer.Replace(@"width=""100%""", @"width=""1000""");
                FooterEditor.Content = footer;
               
                #region Commented for Blank body template
                //  BodyEditor2.Content = ds.Tables[0].Rows[0][3].ToString();
                BodyEditor2.Content = "";
                #endregion
                BodyEditor2.Content = "";
                if (id == JG_Prospect.Common.JGConstant.ONE)
                {
                    BodyEditorB.Visible = true;
                    #region Commented for Blank body template
                  //  BodyEditor.Content = ds.Tables[0].Rows[0][4].ToString();
                   // BodyEditorB.Content = ds.Tables[0].Rows[0][5].ToString();
                    BodyEditor.Content = "";
                    BodyEditorB.Content = "";
                    #endregion
                    
                }
                else
                {
                    #region Commented for Blank body template
                   // BodyEditorB.Visible = false;
                   // BodyEditor.Content = ds.Tables[0].Rows[0][1].ToString();
                    BodyEditorB.Visible = false;
                    BodyEditor.Content = "";
                    #endregion

                   
                }
            }
        }
         protected void bindData(string templateName)
        {
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchingContractTemplateDetail(templateName);
            if (ds != null)
            {
                string header = ds.Tables[0].Rows[0][0].ToString();
                header = header.Replace(@"width=""100%""", @"width=""1000""");
                HeaderEditor.Content = header;

                string footer = ds.Tables[0].Rows[0][2].ToString();
                footer = footer.Replace(@"width=""100%""", @"width=""1000""");
                FooterEditor.Content = footer;
               
                #region Commented for Blank body template
                //  BodyEditor2.Content = ds.Tables[0].Rows[0][3].ToString();
                BodyEditor2.Content = "";
                #endregion
                BodyEditor2.Content = "";
                //if (id == JG_Prospect.Common.JGConstant.tex)
                //{
                //    BodyEditorB.Visible = true;
                //    #region Commented for Blank body template
                //  //  BodyEditor.Content = ds.Tables[0].Rows[0][4].ToString();
                //   // BodyEditorB.Content = ds.Tables[0].Rows[0][5].ToString();
                //    BodyEditor.Content = "";
                //    BodyEditorB.Content = "";
                //    #endregion
                    
                //}
                //else
                //{
                //    #region Commented for Blank body template
                //   // BodyEditorB.Visible = false;
                //   // BodyEditor.Content = ds.Tables[0].Rows[0][1].ToString();
                //    BodyEditorB.Visible = false;
                //    BodyEditor.Content = "";
                //    #endregion

                   
                //}
            }
        }
        
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string Editor_contentHeader = HeaderEditor.Content;
            Editor_contentHeader = Editor_contentHeader.Replace(@"width=""1000""", @"width=""100%""");
            string Editor_contentBodyA = BodyEditor.Content;
            string Editor_contentBodyB = "";
            string Editor_contentFooter = FooterEditor.Content;
            Editor_contentFooter = Editor_contentFooter.Replace(@"width=""1000""", @"width=""100%""");
            string Editor_contentBody2 = BodyEditor2.Content;
            bool result = false;
            if (ProductID == JG_Prospect.Common.JGConstant.ONE)
            {
                Editor_contentBodyB = BodyEditorB.Content;   
            }

            result = AdminBLL.Instance.UpdateContractTemplate(Editor_contentHeader, Editor_contentBodyA, Editor_contentBodyB, Editor_contentFooter, Editor_contentBody2, ProductID);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Contract Template Updated Successfully');", true);
            }
        }
    }
}