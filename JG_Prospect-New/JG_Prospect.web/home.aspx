<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="home.aspx.cs" Inherits="JG_Prospect.home" %>

<%--<%@ Register Src="~/Controls/left.ascx" TagName="leftmenu" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <div class="left_panel arrowlistmenu" style="height: 400px">
        <uc1:leftmenu ID="left1" runat="server" />
    </div>--%>
    <div class="right_panel">
      
            <ul class="appointment_tab">
                <li><a id="A1" href="home.aspx" runat="server" class="active">Personal Prospect Calendar</a> </li>
                <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
                <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
            </ul>
        <h1><b>Dashboard</b></h1>
        <h2>
            Personal Prospect Calendar</h2>
        <div class="calendar" style="margin: 0;">
            <iframe src="/calendar/calendar.aspx" width="100%" height="800" style="border: 0;">
            </iframe>
        </div>
    </div>
</asp:Content>
