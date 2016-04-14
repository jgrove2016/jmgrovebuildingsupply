<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="JG_Prospect.Controls.Header" %>
<!--tabs jquery-->
<script type="text/javascript" src="/js/jquery.ui.core.js"></script>
<script type="text/javascript" src="/js/jquery.ui.widget.js"></script>
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
</style>

<div class="header">
  <img src="img/logo.png" alt="logo" width="88" height="89" class="logo" />
  <div class="user_panel">
  Welcome! <span>
  <asp:Label ID="lbluser" runat="server" Text="User"></asp:Label>
  <asp:Button ID="btnlogout" runat="server" Text="Logout" onclick="btnlogout_Click" 
      />
      </span> 
  &nbsp;<div class="clr"></div>
	<ul> 
    <li id="Lihome" runat="server" ><a href="<%= Page.ResolveUrl("~/home.aspx")%>">Home</a></li>
    <li>|</li>
    <li><a href="<%= Page.ResolveUrl("~/changepassword.aspx")%>">Change Password</a></li>
    </ul>
  </div>
  <!--nav section-->
  <div class="nav">
  <ul>
  <li id="Lidashboard" runat="server" ><a href="<%= Page.ResolveUrl("~/home.aspx")%>">Dashboard</a></li>
  <%--<li id="Lidefineperiod" runat="server" visible="false"><a href="/DefinePeriod.aspx">Pay Schedule</a></li>--%>
      <li id="Lidefineperiod" runat="server" visible="false"><a href="<%= Page.ResolveUrl("~/DefinePeriod.aspx")%>">Pay Schedule</a></li>
    
  <li ><a href="<%= Page.ResolveUrl("~/Prospectmaster.aspx")%>">Add/Update Prospect</a></li>
  <li id="LiUploadprospect" runat="server"><a href="<%= Page.ResolveUrl("~/upload.aspx")%>">Upload Prospects</a></li>
  <%--<li id="Licreateuser" runat="server" visible="false"><a href="/CreateUser.aspx">Create User</a></li>
  <li id="Liedituser" runat="server" visible="false" ><a href="/EditUser.aspx" runat="server" id="edituser">Edit User</a></li>--%>
  <%--<li><a href="<%= Page.ResolveUrl("~/Leads_summaryreport.aspx")%>" runat="server" id="Summaryreport">Prospect List</a></li>--%>
      <li><a href="<%= Page.ResolveUrl("~/Leads_summaryreport.aspx")%>">Prospect List</a></li>
  <%--<li id="Liprogress" runat="server" visible="true"><a href="<%= Page.ResolveUrl("~/ProgressReport.aspx")%>" runat="server" id="ProgressReport">Progress Report</a></li>--%>
       <li id="Liprogress" runat="server" visible="true"><a href="<%= Page.ResolveUrl("~/ProgressReport.aspx")%>">Progress Report</a></li>

  <%--<li id="Li_AssignProspect" runat="server" visible="true">--%><%--<a href="/AssignProspect.aspx" runat="server" id="AssignProspect">Assign Customer</a>--%></li>
  <%--<li id="Li_sr_app" runat="server" visible="false"><a href="<%= Page.ResolveUrl("~/Sr_App/home.aspx")%>" runat="server" id="sr_app">Senior App</a></li>  --%>
      <li id="Li_sr_app" runat="server" visible="false"><a href="<%= Page.ResolveUrl("~/Sr_App/home.aspx")%>">Senior App</a></li>  
    <%--<li id="Li1" runat="server" visible="true"><a href="~/Sr_App/ConstructionCalendar.aspx" runat="server" id="A1">Old Calendar</a></li>--%>  
 </ul>
  </div>
</div>