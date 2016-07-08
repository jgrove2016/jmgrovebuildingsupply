<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailTemplateForVendors.aspx.cs" Inherits="JG_Prospect.Sr_App.EmailTemplateForVendors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
    <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <div class="right_panel">
        <h1>
            Edit Email Template For Vendor</h1>
        <div>
            <h2>
                Header Template</h2>
            <cc1:Editor ID="HeaderEditor" Width="1000px" Height="200px" runat="server" />
            <h2>
                Body Template</h2>
            <asp:Label ID="lblMaterials" runat="server"></asp:Label>
            <h2>
                Footer Template</h2>
            <cc1:Editor ID="FooterEditor" Width="1000px" Height="200px" runat="server" />
        </div>
        <br />
        <br />
        <div class="btn_sec">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
        </div>
    </div>
    
     </form>
</body>
</html>
