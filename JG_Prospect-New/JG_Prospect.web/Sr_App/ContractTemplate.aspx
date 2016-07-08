<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="ContractTemplate.aspx.cs" Inherits="JG_Prospect.Sr_App.ContractTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
    <ul class="appointment_tab">
        <li><a href="home.aspx" >Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
    </ul>
        <h1>
            Edit Contract Template</h1>
        <br />
        <div><h2>Header Template</h2>
            <cc1:Editor ID="HeaderEditor" Width="1000px" Height="400px" runat="server" />
               <h2>Body Template</h2>
             <cc1:Editor ID="BodyEditor" Width="1000px" Height="400px" runat="server" />
             <cc1:Editor ID="BodyEditorB" Width="1000px" Height="400px" runat="server" Visible="false" />
             <cc1:Editor ID="BodyEditor2" Width="1000px" Height="400px" runat="server" />
            <h2>Footer Template</h2>
            <asp:Image ImageUrl="~/img/Bar3.png" ID="img1" runat="server" Width="1000px" Height="40px" />
             <cc1:Editor ID="FooterEditor" Width="1000px" Height="600px" runat="server" />
        </div>
        <br />
        <div class="btn_sec">
            <asp:Button ID="btnsave" runat="server" Text="Update" ValidationGroup="save" OnClick="btnsave_Click" />
        </div>

    </div>
</asp:Content>
