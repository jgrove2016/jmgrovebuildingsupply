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

namespace JG_Prospect
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }


        private void binddata()
        {
            string usertype = Session["usertype"].ToString();
            DataSet DS = new DataSet();
            DS = UserBLL.Instance.getallusers(usertype);

            GridViewUser.DataSource = DS.Tables[0];
            GridViewUser.DataBind();
        }


        protected void GridViewUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUser.EditIndex = -1;
            binddata();
        }

        protected void GridViewUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string key = GridViewUser.DataKeys[e.RowIndex].Values[0].ToString();
               bool result= UserBLL.Instance.DeleteUser(Convert.ToInt32(key));
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
            bool result = false;
            string key = GridViewUser.DataKeys[e.RowIndex].Values[0].ToString();
            GridViewRow gr = GridViewUser.Rows[e.RowIndex];
            user objuser = new user();
            objuser.username = ((TextBox)(gr.FindControl("txtusername"))).Text;
            objuser.loginid = ((TextBox)(gr.FindControl("txtloginid"))).Text;
            objuser.email = ((TextBox)(gr.FindControl("txtEmail"))).Text;
            objuser.password = ((TextBox)(gr.FindControl("txtPassword"))).Text;
            objuser.usertype = ((DropDownList)gr.FindControl("ddlUsertype")).SelectedValue;
            objuser.designation = ((DropDownList)(gr.FindControl("ddlDesignation"))).SelectedValue;
            objuser.status = ((DropDownList)gr.FindControl("ddlStatus")).SelectedItem.Text;
            objuser.phone = ((TextBox)(gr.FindControl("txtPhone"))).Text;
            objuser.address = ((TextBox)(gr.FindControl("txtAddress"))).Text;
          
            result = UserBLL.Instance.UpdateUser(objuser,Convert.ToInt32(key));

            if (result)
            {
                //lblmsg.Visible = true;
                //lblmsg.CssClass = "success";
                //lblmsg.Text = "User has been Updated successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('User Updated Successfully');", true);
            }

            GridViewUser.EditIndex = -1;

            binddata();
        } 

        protected void GridViewUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int Id;
            DataSet ds = new DataSet();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Id = Convert.ToInt32(GridViewUser.DataKeys[e.Row.RowIndex].Values[0]);
                ds = UserBLL.Instance.getuserdetails(Id);
                DropDownList ddlusertype = (DropDownList)e.Row.FindControl("ddlUsertype");
                DropDownList ddldesignation = (DropDownList)e.Row.FindControl("ddlDesignation");               

                ddlusertype.SelectedValue = ds.Tables[0].Rows[0]["Usertype"].ToString();
                ddldesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation"].ToString();
            }
        }

    }
}