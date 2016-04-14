<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="view_customer.aspx.cs" Inherits="JG_Prospect.Sr_App.view_customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
<h1><b>Customer</b></h1>
<!-- Tabs starts -->
          <div id="tabs">
            <ul>
              <li style="margin:0;"><a href="#tabs-2">Customer</a></li>
            </ul>
            <div id="tabs-1">
            <div class="form_panel">
            <h2>Details</h2>
            <ul>
            <li>
            <table border="0" cellspacing="0" cellpadding="0">
    <tr>
    <td><label>Customer Name<span>*</span></label>
        <asp:TextBox ID="txtCust_name" runat="server"></asp:TextBox>
      <%--<input type="text" name="textfield" id="textfield" />--%>
      </td>
  </tr>
  <tr>
    <td><label>Customer Street<span>*</span></label>
        <asp:TextBox ID="txtCust_strt" runat="server"></asp:TextBox>
      <%--<input type="text" name="textfield2" id="textfield2" />--%>
      </td>
  </tr>
  <tr>
    <td><label>Customer State, 
        <br />
         City,Zip Code<span>*</span></label>
        <asp:DropDownList ID="ddlState" runat="server" Width="80px"></asp:DropDownList>
     <%-- <select name="" style="width:80px;"><option>Select</option></select>--%>

        <asp:DropDownList ID="ddlCity" runat="server" Width="80px"></asp:DropDownList>
    <%--  <select name="select" style="width:80px;"><option>Select</option></select>--%>

        <asp:DropDownList ID="ddlzipcode" runat="server" Width="80px"></asp:DropDownList>
     <%-- <select name="select4" style="width:80px;"><option>Select</option></select>--%>
      
      </td>
  </tr>
  
  <tr>
    <td><label>Job Location<span>*</span></label>
        <asp:TextBox ID="txtJob_loc" runat="server"></asp:TextBox>
    <%--<input type="text" name="textfield2" id="textfield2" />--%>
    </td>
  </tr>
  <tr>
    <td><label>Est. Date Schedule<span>*</span></label>
        <asp:TextBox ID="txtEst_date" runat="server"></asp:TextBox>
    <%--<input type="text" name="textfield2" id="textfield2" />--%>
    </td>
  </tr>
  <tr>
    <td><label>Service Wanted / 
Notes<span>*</span></label>
        <asp:TextBox ID="txtService" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
    <%--<textarea rows="5"></textarea>--%>
    </td>
  </tr>
</table>

            </li>
            <li class="last">
            <table border="0" cellspacing="0" cellpadding="0">

  <tr>
    <td><label>Today Date<span>*</span></label>
        <asp:TextBox ID="txtToday_date" runat="server"></asp:TextBox>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToday_date">
                                                </ajaxToolkit:CalendarExtender>
    <%--<input name="" type="text" />--%>
    </td>
  </tr>
  <tr>
    <td><label>Primary Phone #<span>*</span></label>
        <asp:TextBox ID="txtPri_ph" runat="server"></asp:TextBox>
    <%--<input name="" type="text" />--%>
    </td>
  </tr>
  <tr>
    <td><label>Secondary Phone #<span>*</span></label>
        <asp:TextBox ID="txtSec_ph" runat="server"></asp:TextBox>
      <%--<input name="input" type="text" />--%>
      </td>
  </tr>
 
  <tr>
    <td><label>E-mail<span>*</span></label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
      <%--<input name="input3" type="text" />--%>
      </td>
  </tr>
  <tr>
    <td><label>Call Taken by<span>*</span></label>
        <asp:TextBox ID="txtCall_takenby" runat="server"></asp:TextBox>
      <%--<input name="input4" type="text" />--%>
      </td>
  </tr>
  <tr>
                                <td>
                                    <label>
                                        Lead Type</label>
                                    <asp:DropDownList ID="ddlleadtype" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlleadtype_SelectedIndexChanged">
                                        <asp:ListItem>Internet</asp:ListItem>
                                        <asp:ListItem>Referall</asp:ListItem>
                                        <asp:ListItem>Repeat customer</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="tr_leadtype" runat="server" visible="false">
                                <td>
                                    <label>
                                        Other Lead Type</label>
                                    <asp:TextBox ID="txtleadtype" Text="" runat="server"></asp:TextBox>
                                    <%--<input name="input4" type="text" />--%>
                                </td>
                            </tr>
            </table>

            </li>            
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                    onclick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
             <%-- <input name="input2" type="submit" value="Submit" />
              <input type="submit" value="Cancel" />--%>
            </div>
            </div>
            </div>
            <div id="tabs-2"></div>
          </div>
          <!-- Tabs endss -->

</div>
</asp:Content>
