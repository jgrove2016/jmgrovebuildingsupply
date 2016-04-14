<%@ Page Title="" Language="C#" MasterPageFile="~/Installer/InstallerMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="JG_Prospect.Installer.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="tabs-1">
      <div class="login_form_panel">
      
      <h1>Change Password</h1>
            <ul>
            <li class="last">
            <table border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
    <td><label>Enter Your New Password<span>*</span></label>
        <asp:TextBox ID="txtUser_Password" runat="server" TextMode="Password" TabIndex="1"
            Height="18px" Width="251px"></asp:TextBox>
        
      <%--<input type="text" name="textfield" id="textfield" />--%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="a" 
            ControlToValidate="txtUser_Password" ErrorMessage=" Please Enter password" 
            ForeColor="Red"></asp:RequiredFieldValidator>
      </td>
  </tr>
  <tr>
    <td><label>Confirm  Password<span>*</span></label>
        <asp:TextBox ID="txtConPwd" runat="server" TextMode="Password" Height="18px" TabIndex="2"
            Width="250px"></asp:TextBox>     
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="a" 
            ControlToValidate="txtConPwd" ErrorMessage=" Please Enter password" 
            ForeColor="Red"></asp:RequiredFieldValidator>   
        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ValidationGroup="a" 
            ControlToCompare="txtUser_Password" ControlToValidate="txtConPwd" 
            ErrorMessage=" Password did not matched" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
      </td>
  </tr>  
  
</table>

            </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="a" TabIndex="3"
                    onclick="btnsubmit_Click" />        
            </div>
            </div>
            </div>
          <div id="tabs">
           
            
            <div id="tabs-2"></div>
          </div>
          <!-- Tabs endss -->
</asp:Content>
