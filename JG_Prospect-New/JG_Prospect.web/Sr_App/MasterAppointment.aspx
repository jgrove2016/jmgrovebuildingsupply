<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="MasterAppointment.aspx.cs" Inherits="JG_Prospect.Sr_App.MasterAppointment" %>
<%@ Register Src="~/Sr_App/LeftPanel.ascx" TagName="leftmenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- <div class="left_panel arrowlistmenu">
        <uc1:leftmenu ID="left1" runat="server" />
    </div>--%>
    <div class="right_panel">
        <ul class="appointment_tab">
        <li><a href="home.aspx" >Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx" class="active">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
    </ul>
        <h1>
            <b>Dashboard</b></h1>
        <h2>
            Master Appointment Calendar</h2>
        <div class="calendar" style="margin: 0;">           
            <iframe src="https://www.google.com/calendar/embed?mode=WEEK&amp;src=bc4f5sm6ggs8puv4an6iftpj9s%40group.calendar.google.com&ctz=America/St_Thomas"  style="border: 0; width: 100%; height: 800px;" frameborder="0" scrolling="no"></iframe>
        </div>
    </div>
</asp:Content>
