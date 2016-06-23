<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="JG_Prospect.Sr_App.Header" %>
<!--tabs jquery-->
<%--<script type="text/javascript" src="../js/jquery.ui.core.js"></script>
<script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
<!--tabs jquery ends-->
<script type="text/javascript">
    $(function () {
        // Tabs
        $('#tabs').tabs();
    });
		</script>
<style type="text/css">
.ui-widget-header {
	border: 0;
	background:none/*{bgHeaderRepeat}*/;
	color: #222/*{fcHeader}*/;
}
</style>--%>
<div class="header">
    <img src="../img/logo.png" alt="logo" width="88" height="89" class="logo" />
    <div style="width:550px; margin-left: 133px; margin-top: 20px; max-height: 150px; overflow-y: auto;border: solid 1px red;">
        <uc:tasklist id="ucTaskList" runat="server" />
    </div>
  
</div>
<div style="clear:both;"></div>
  <div class="user_panel" style="float:right;">
        Welcome! <span>
            <asp:Label ID="lbluser" runat="server" Text="User"></asp:Label>
            <asp:Button ID="btnlogout" runat="server" Text="Logout" CssClass="cancel" ValidationGroup="header" OnClick="btnlogout_Click" />
        </span>&nbsp;<div class="clr">
        </div>
        <ul>
            <li><a href="home.aspx">Home</a></li>
            <li>|</li>
            <li><a href="/changepassword.aspx">Change Password</a></li>
        </ul>
        <div class="clr">
        </div>
        <ul>
            <li><a id="idPhoneLink" class="clsPhoneLink" onclick="GetPhoneDiv()">Phone Dashboard</a></li>
        </ul>
        <div class="clr">
        </div>
        <ul>
            <li>Voice Mail(0)</li>
            <li>|</li>
            <li>Chat(1)</li>
        </ul>
    </div>
<!--nav section-->
<div class="nav">
    <ul>
        <li><a href="home.aspx">Home</a></li>
        <li><a href="new_customer.aspx">Add Customer</a></li>
        <%-- <li><a href="view_customer.aspx">Review / Edit Customer Estimate</a></li>--%>
        <li><a href="ProductEstimate.aspx">Product Estimate</a></li>
        <li><a href="SalesReort.aspx">Sales Report</a></li>
        <%--<li><a href="Vendors.aspx">Vendor Master</a></li>--%>
        <li id="Li_Jr_app" runat="server" visible="true"><a href="~/home.aspx" runat="server"
            id="Jr_app">Junior App</a></li>
        <li id="Li_Installer" runat="server" visible="true"><a href="~/Installer/InstallerHome.aspx" runat="server"
            id="A1">Installer</a></li>


        <%-- <li><a href="/EditUser.aspx" runat="server" id="edituser">EditUser</a></li>
  <li><a href="/Accounts/newuser.aspx" runat="server" id ="newuser">CreateUser</a></li>--%>
    </ul>
</div>
