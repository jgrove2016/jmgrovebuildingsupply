using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using JG_Prospect.Common;
using JG_Prospect.Common.Logger;
using JG_Prospect.Common.modal;
using JG_Prospect.BLL;
using System.Configuration;

namespace JG_Prospect
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        string path = "";

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_upload.Update();

                if (dataupload.HasFile)
                {
                    string filename = Guid.NewGuid().ToString().Substring(0, 4) + dataupload.FileName;
                    path = Server.MapPath("/UploadedFiles/" + filename);
                    Session["filename"] = filename;
                    Session["FileExcel"] = path;
                    dataupload.PostedFile.SaveAs(path);

                    //Response.End();
                    //  
                }

                else
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "error";
                    lblmsg.Text = "Please select a Excel file cointaining Product data to upload";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please select a Excel file cointaining Product data to upload');", true);
                }
                if (!File.Exists(path))
                {
                    return;
                }

                DataSet ds = new DataSet();
                ds = exceltoXml.getdata(Session["FileExcel"].ToString());
                //  ds = exceltoXml.GetExcelAsDataSet(Session["FileExcel"].ToString(), true);
                if (ds != null)
                {
                    grddata.DataSource = ds;
                    grddata.DataBind();


                    if (grddata.Rows.Count > 0)
                    {
                        btnsave.Visible = true;
                        lblmsg.Visible = true;
                        lblmsg.CssClass = "success";
                        lblmsg.Text = "Data has been Imported successfully";
                    }
                }

            }
            catch (Exception ex1)
            {
                LogError(ex1);
            }
        }
        void LogError(Exception ex)
        {
            ErrorLog obj = new ErrorLog();
            obj.writeToLog(ex, this.Page.ToString(), Request.ServerVariables["remote_addr"].ToString());
            Session.Abandon();
            Response.Redirect("~/errorlog.txt");
        }


        protected void Save_Click(object sender, EventArgs e)
        {
            int result = 0;


            foreach (GridViewRow dr in grddata.Rows)
            {
                foreach (TableCell c in dr.Cells)
                {
                    if (c.Text == "&nbsp;")
                        c.Text = "";
                }

                string[] date_time = (dr.Cells[1].Text).Split('-');
                string CustomerNm = dr.Cells[2].Text + " " + dr.Cells[3].Text;
                string CellPh = dr.Cells[4].Text;
                string AltPh = dr.Cells[5].Text;
                string HomePh = dr.Cells[6].Text;
                string Email = dr.Cells[7].Text;
                string Address = dr.Cells[8].Text;
                string[] CityStateZip = dr.Cells[9].Text.Split(',');
                string followupstatus = dr.Cells[10].Text;
                string followupdate = dr.Cells[11].Text;
                string Productofinterest = dr.Cells[12].Text;
                string Besttimeofcontact = dr.Cells[13].Text;
                string meetingstatus = dr.Cells[14].Text;
                string CallTakenBy = dr.Cells[15].Text;
                string Notes = dr.Cells[16].Text;
                string primarycontact = dr.Cells[17].Text;
                string ContactPreference = dr.Cells[18].Text;
                string BillingAddress = dr.Cells[19].Text;
                string JobLocation = dr.Cells[20].Text;
                string LeadType = dr.Cells[21].Text;
                string AssignedTo = dr.Cells[22].Text;
                string Reasonofclose = dr.Cells[23].Text;

                Customer objcust = new Customer();
                objcust.missingcontacts = 0;

                if (CellPh == "0" || CellPh == "")
                {
                    objcust.missingcontacts++;
                }
                if (AltPh == "0" || AltPh == "")
                {
                    objcust.missingcontacts++;
                }
                if (HomePh == "0" || HomePh == "")
                {
                    objcust.missingcontacts++;
                }


                if (objcust.missingcontacts > 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Please Fill atleast one contact(Cell Phone, Home Phone or Alt. Phone');", true);
                    return;
                }

                int primarycont = new_customerBLL.Instance.Searchprimarycontact(HomePh, CellPh, AltPh,0);
                if (primarycont == 1)
                {
                    objcust.Isrepeated = false;
                }
                else
                {
                    objcust.Isrepeated = true;
                }

                objcust.EstDate = (date_time != null) ? date_time[0].ToString() : "1/1/1753";
                objcust.EstTime = (date_time != null) ? date_time[1].ToString() : "";
                objcust.customerNm = CustomerNm;
                objcust.CellPh = CellPh;
                objcust.AltPh = AltPh;
                objcust.HousePh = HomePh;
                objcust.Email = Email;
                objcust.CustomerAddress = Address;
                objcust.City = CityStateZip[0].ToString();
                objcust.state = CityStateZip[1].ToString();
                objcust.Zipcode = CityStateZip[2].ToString();
                objcust.followupdate = followupdate != "" ? followupdate : "1/1/1753";
                int productId = UserBLL.Instance.GetProductIDByProductName(Productofinterest);
                objcust.Productofinterest = productId;
                objcust.BestTimetocontact = Besttimeofcontact;
                objcust.status = meetingstatus;
                objcust.CallTakenby = CallTakenBy;
                objcust.Notes = Notes;
                objcust.PrimaryContact = primarycontact;
                objcust.ContactPreference = ContactPreference;
                if (BillingAddress == "same")
                {
                    objcust.BillingAddress = objcust.CustomerAddress + "," + objcust.City + "," + objcust.state + "," + objcust.Zipcode;
                }
                objcust.JobLocation = JobLocation;
                objcust.Leadtype = CallTakenBy;
                objcust.Addedby = CallTakenBy;
                objcust.CallTakenby = CallTakenBy;

                objcust.Map1 = objcust.customerNm + "-" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                objcust.Map2 = objcust.customerNm + "-" + "Direction" + Guid.NewGuid().ToString().Substring(0, 5) + ".Jpeg";
                string DestinationPath = Server.MapPath("~/CustomerDocs/Maps/");
                new_customerBLL.Instance.SaveMapImage(objcust.Map1, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode, DestinationPath);
                new_customerBLL.Instance.SaveMapImageDirection(objcust.Map2, objcust.CustomerAddress, objcust.City, objcust.state, objcust.Zipcode, DestinationPath);


                result = new_customerBLL.Instance.AddCustomer(objcust);

                if (primarycontact == "Cell Phone")
                {

                }

                string date = Convert.ToDateTime(objcust.EstDate).ToShortDateString();
                string datetime;


                if (date != "1/1/1753")
                {
                    datetime = Convert.ToDateTime(date + " " + objcust.EstTime).ToString("MM/dd/yy hh:mm tt");
                }
                else
                {
                    datetime = Convert.ToDateTime(objcust.followupdate).ToString("MM/dd/yy hh:mm tt");
                }

                if (result > 0)
                {
                    string AdminId = ConfigurationManager.AppSettings["AdminUserId"].ToString();
                    string Adminuser = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
                    string AdminPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
                    if (meetingstatus == "Set")
                    {
                        string gtitle = objcust.EstTime + " -" + primarycontact + " -" + objcust.CallTakenby;
                        string gcontent = "Name: " + objcust.customerNm + " ,Product of Interest: " + Productofinterest + ", Phone: " + objcust.CellPh + ", Alt. phone: " + objcust.AltPh + ", Email: " + objcust.Email + ",Notes: " + objcust.Notes + ",Status: " + objcust.status;
                        string gaddress = objcust.CustomerAddress + " " + objcust.City + "," + objcust.state + "," + objcust.Zipcode;

                        if (CallTakenBy != AdminId)
                            GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), CallTakenBy);
                        GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), AdminId);

                        gtitle = objcust.EstTime + " -" + primarycontact + " -" + Session["loginid"].ToString();
                        gcontent = "Name: " + objcust.customerNm + " , Cell Phone: " + objcust.CellPh + ", Alt. phone: " + objcust.AltPh + ", Email: " + objcust.Email + ",Service: " + objcust.Notes + ",Status: " + objcust.status;
                        gaddress = Address + " " + objcust.City + "," + objcust.state + " -" + objcust.Zipcode;
                        GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", Adminuser, AdminPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(datetime), Convert.ToDateTime(datetime).AddHours(1), JGConstant.CustomerCalendar);

                    }

                    AdminBLL.Instance.UpdateStatus(result, meetingstatus, objcust.followupdate);
                    int userId = Convert.ToInt16(Session[JG_Prospect.Common.SessionKey.Key.UserId.ToString()].ToString());
                    new_customerBLL.Instance.AddCustomerFollowUp(result, Convert.ToDateTime(objcust.followupdate), meetingstatus, userId, false,0,"");

                    lblmsg.Visible = true;
                    lblmsg.CssClass = "success";
                    lblmsg.Text = "Data has been saved successfully";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been saved successfully');", true);                  
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.CssClass = "error";
                    lblmsg.Text = "There is some error in adding the Prospect";
                }
            }
        }


        //protected void Save_Click(object sender, EventArgs e)
        //{
        //    int result = 0;
        //    foreach (GridViewRow dr in grddata.Rows)
        //    {
        //        int missing_contacts = 0;
        //        foreach (TableCell c in dr.Cells)
        //        {
        //            if (c.Text == "" || c.Text == "&nbsp;")
        //                c.Text = "0";
        //        }

        //        string[] datetime = (dr.Cells[0].Text).Split('-');

        //        if (dr.Cells[3].Text == "0" && dr.Cells[4].Text == "0")
        //        {
        //            missing_contacts++;
        //        }
        //        if (dr.Cells[5].Text == "0")
        //        {
        //            missing_contacts++;
        //        }
        //        if (dr.Cells[6].Text == "0")
        //        {
        //            missing_contacts++;
        //        }



        //        prospect objprospect = new prospect();
        //        objprospect.user = (Session["loginid"].ToString() != null) ? Session["loginid"].ToString() : "Admin";
        //        if (dr.Cells[0].Text != "0" && dr.Cells[0].Text != null)
        //        {
        //            objprospect.estimatedate = Convert.ToDateTime(datetime[0].ToString());
        //            objprospect.estimatetime = datetime[1].ToString();
        //            objprospect.status = "Set";
        //        }
        //        else
        //        {
        //            objprospect.estimatedate = Convert.ToDateTime("1/1/1753");
        //            objprospect.status = "Follow up";
        //        }
        //        objprospect.firstname = dr.Cells[1].Text;
        //        objprospect.lastname = dr.Cells[2].Text;
        //        objprospect.cellphone = dr.Cells[3].Text;
        //        objprospect.alt_phone = dr.Cells[4].Text;
        //        objprospect.email = dr.Cells[5].Text;
        //        objprospect.address = dr.Cells[6].Text;
        //        objprospect.city = dr.Cells[7].Text;
        //        objprospect.followup_date = dr.Cells[8].Text;
        //        objprospect.followup_status = dr.Cells[9].Text;
        //        objprospect.product_of_interest = dr.Cells[10].Text;
        //        objprospect.best_time_to_contact = dr.Cells[11].Text;
        //        objprospect.notes = dr.Cells[12].Text;
        //        objprospect.missing_contacts = missing_contacts;
        //        result = UserBLL.Instance.addprospect(objprospect);

        //        string date = Convert.ToDateTime(objprospect.estimatedate).ToShortDateString();
                
        //        string gdate;
        //        if (date != "1/1/1753")
        //        {
        //            gdate = Convert.ToDateTime(date + " " + objprospect.estimatetime).ToString("MM/dd/yy hh:mm tt");
        //        }
        //        else
        //        {
        //            gdate = Convert.ToDateTime(objprospect.followup_date).ToString("MM/dd/yy hh:mm tt");
        //        }

        //        if (result > 0)
        //        {
        //            string gtitle = objprospect.estimatetime + " -" + objprospect.Primarycontact + " -" + objprospect.user;                
        //            string gcontent = "Name: " + objprospect.firstname + objprospect.lastname + " ,Product of Interest: " + objprospect.product_of_interest + " , Phone: " + objprospect.cellphone + ", Alt. phone: " + objprospect.alt_phone + ", Email: " + objprospect.email + ",Notes: " + objprospect.notes + ",Status: " + objprospect.status + ", Follow up: " + objprospect.followup_date + objprospect.followup_status;
        //            string gaddress = objprospect.address + " ," + objprospect.city + "," + objprospect.state + "," + objprospect.zip ;
        //            string userName = ConfigurationManager.AppSettings["AdminCalendarUser"].ToString();
        //            string userPwd = ConfigurationManager.AppSettings["AdminCalendarPwd"].ToString();
        //            if (Session["AdminUserId"] == null)
        //                GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", userName, userPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(gdate), Convert.ToDateTime(gdate), Session["loginid"].ToString());
        //            GoogleCalendarEvent.AddEvent(GoogleCalendarEvent.GetService("GoogleCalendar", userName, userPwd), result.ToString(), gtitle, gcontent, gaddress, Convert.ToDateTime(gdate), Convert.ToDateTime(gdate), userName);
        //        }

        //    }

        //    if (result > 0)
        //    {
        //        lblmsg.Visible = true;
        //        lblmsg.CssClass = "success";
        //        lblmsg.Text = "Data has been saved successfully";
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertBox", "alert('Data has been saved successfully');", true);
        //    }
           
        //}
    }
}