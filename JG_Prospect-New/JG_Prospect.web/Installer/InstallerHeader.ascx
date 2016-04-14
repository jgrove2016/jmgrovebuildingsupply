<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstallerHeader.ascx.cs" Inherits="JG_Prospect.Installer.InstallerHeader" %>
<div class="header">
    <img src="../img/logo.png" alt="logo" width="88" height="89" class="logo" />
    <div class="user_panel">
        Welcome! <span>
            <asp:Label ID="lbluser" runat="server" Text="User"></asp:Label>
            <asp:Button ID="btnlogout" runat="server" Text="Logout" CssClass="cancel" ValidationGroup="header" OnClick="btnlogout_Click" />
        </span>&nbsp;<div class="clr">
        </div>
        <ul>
            <li><a href="../Installer/InstallerHome.aspx">Home</a></li>
            <li>|</li>
            <li><a href="../Installer/ChangePassword.aspx">Change Password</a></li>
        </ul>
    </div>
    <div class="nav">
  <ul>
  <li id="Li_Dashboard" runat="server" ><a href="../Installer/InstallerHome.aspx">Dashboard</a></li>
  <li id="Li_sr_app" runat="server" visible="false"><a href="~/Sr_App/home.aspx" runat="server" id="sr_app">Senior App</a></li>  
  </ul>
  </div>
</div>