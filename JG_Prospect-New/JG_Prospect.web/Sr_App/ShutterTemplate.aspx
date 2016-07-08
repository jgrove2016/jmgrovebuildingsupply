<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="ShutterTemplate.aspx.cs" Inherits="JG_Prospect.Sr_App.ShutterTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
     <ul class="appointment_tab">
        <li><a href="home.aspx">Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
    </ul>
        <h1>
            Edit Work Order Template</h1>
        <br />      
        <div>
            <cc1:Editor ID="Editor1" Width="1000px" Height="1000px" runat="server" />
        </div>
        <br />        
        <div class="btn_sec">
            <asp:Button ID="btnsave" runat="server" Text="Update" ValidationGroup="save" TabIndex="1" OnClick="btnsave_Click" />
        </div>
    </div>
</asp:Content>
