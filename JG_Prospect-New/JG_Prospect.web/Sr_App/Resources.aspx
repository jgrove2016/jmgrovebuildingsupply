<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Resources.aspx.cs" Inherits="JG_Prospect.Sr_App.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .resources
        {
            background:#ccc;
        }
        .resources ul
        {
            list-style-type: none;
            float: left;
            width: 100%;
        }
        .resources ul li
        {
            float: left;
            text-align: center;
            padding: 20px 0 40px;
            width: 48%;            
            border:none;            
        }
        .resources ul li img
        {
            text-align: center;
        }
        .resources ul li span
        {
            display: block;
            font-size:15px;
            margin:10px 0;
        }
    </style>
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
            <b>Resources</b></h1>
        <div class="form_panel_custom">
            <div class="resources">
                <ul>
                    <li><a href="javascript:window.open('SharedDocuments.aspx','mywindow','width=600,height=400')" target="_blank">
                        <img alt="Shared Documents" src="../img/doc.png" /><span>Shared Documents</span></a></li>
                    <li><a href="javascript:window.open('video.aspx','mywindow','width=600,height=400')" target="_blank">
                        <img alt="Videos"  src="../img/videos.png" /><span>Videos</span></a></li>
                    <li><a href="javascript:window.open('Literature.aspx','mywindow','width=600,height=400')" target="_blank">
                        <img alt="Literature" src="../img/literature.png" /><span>Literature</span></a></li>
                    <li><a href="javascript:window.open('TrainingTools.aspx','mywindow','width=600,height=400')">
                        <img alt="Training Tools" src="../img/tools.png" /><span>Training Tools</span></a></li>
                </ul>
                <div class="clr">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
