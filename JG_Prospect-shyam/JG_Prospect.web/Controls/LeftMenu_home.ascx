<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu_home.ascx.cs" Inherits="JG_Prospect.Controls.LeftMenu_home" %>

<asp:LinkButton ID="lnkcalendar" Text="Personal Prospect Calendar" runat="server" 
    onclick="lnkcalendar_Click"></asp:LinkButton> 
    <asp:LinkButton ID="lnkClendarView" Text=" Master Prospect Calendar" 
    runat="server" onclick="lnkClendarView_Click"></asp:LinkButton>
<asp:LinkButton ID="lnkstaticreport" Text="Call Sheet" runat="server" 
    onclick="lnkstaticreport_Click"></asp:LinkButton> 