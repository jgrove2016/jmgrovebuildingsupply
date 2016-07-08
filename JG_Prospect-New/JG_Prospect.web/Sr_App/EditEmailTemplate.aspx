<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="EditEmailTemplate.aspx.cs" Inherits="JG_Prospect.Sr_App.EditEmailTemplate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
         <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="Price_control.aspx">Product Line Estimate</a></li>
            <li><a href="Inventory.aspx">Inventory</a></li>
            <li><a href="Maintenace.aspx">Maintainance</a></li>
        </ul>
        <!-- appointment tabs section end -->
        <h1>Edit Email Templates</h1>
        <div>
                <h2 style="text-align: center">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Label"></asp:Label></h2>
                <div>
                    <h2>
                        Choose Category: <asp:DropDownList ID="drpChooseCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpChooseCategory_SelectedIndexChanged"></asp:DropDownList>
                    </h2>
                    <h2>Subject:
                        <asp:TextBox ID="txtSubject" Width="500px" runat="server"></asp:TextBox></h2>
                    <div>
                        Attach File:
                        <asp:FileUpload ID="flVendCat" runat="server" class="multi" />
                        <asp:GridView ID="grdVendCatAtc" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" CellSpacing="22">
                            <Columns>
                                <asp:BoundField DataField="DocumentName" HeaderText="File Name" />
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypDownload" Target="_blank" NavigateUrl='<%#Eval("DocumentPath") %>' runat="server">Download</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%#Eval("Id") %>'
                                            runat="server" OnClick="DeleteFile" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <h2>Header Template</h2>
                    <cc1:Editor ID="HeaderEditor" Width="1000px" Height="200px" runat="server" />
                    <h2>Body Template</h2>
                    <cc1:Editor ID="BodyEditor" Width="1000px" Height="200px" runat="server" />
                    <h2>Footer Template</h2>
                    <cc1:Editor ID="FooterEditor" Width="1000px" Height="200px" runat="server" />

                </div>
                <br />
                <br />
                <div class="btn_sec">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    <input type="button" ID="btnCancel" value="Cancel" onclick="javascript:window.location.href='Maintenace.aspx';"/>
                </div>
            
        </div>
       
    </div>
</asp:Content>
