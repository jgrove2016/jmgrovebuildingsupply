<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="EditEmailTemplate.aspx.cs" Inherits="JG_Prospect.Sr_App.EditEmailTemplate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

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
        <h1>Edit Email Templates</h1>
        <div>
            <label>
                &nbsp;</label>
            <%--<a href="EmailTemplateForVendorCategories.aspx" title="Edit Email Template for vendor category">
               Edit Email Template for vendor category</a>--%>
            <%--<asp:LinkButton runat="server" ID="lnkVendorCategory" OnClick="lnkVendorCategory_Click"
                CausesValidation="false">Edit Email Template for vendor category</asp:LinkButton>--%>
            <br />
            <label>
                &nbsp;</label>
            <%-- <a href="EmailTemplateForVendorCategories.aspx" title="Edit Email Template for vendor">
                Edit Email Template for vendor</a>--%>
            <%--<asp:LinkButton runat="server" ID="lnkVendor" OnClick="lnkVendor_Click" CausesValidation="false">Edit Email Template for vendor</asp:LinkButton>--%>
            <%-- <iframe src="EmailTemplateForVendorCategories.aspx" width="100%" height="100%" id="ifEmailTemplate" runat="server">
        </iframe>--%>
            <asp:Panel ID="pnlEmailTemplateForVendorCategories" runat="server">
                <h2 style="text-align: center">
                    <b>Email Template For Vendor Category</b></h2>
                <div>
                    <h2>Subject:
                        <asp:TextBox ID="txtVendorSubject" Width="500px" runat="server"></asp:TextBox></h2>
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
                    <asp:Label ID="lblMaterials" runat="server"></asp:Label>
                    <cc1:Editor ID="BodyEditor" Width="1000px" Height="200px" runat="server" />
                    <h2>Footer Template</h2>
                    <cc1:Editor ID="FooterEditor" Width="1000px" Height="200px" runat="server" />

                </div>
                <br />
                <br />
                <div class="btn_sec">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlEmailTemplateForVendors" runat="server" Visible="false">
            <h2 style="text-align: center">
                <b>Email Template For Vendors</b></h2>
            <div>
                <h2>Subject:
                    <asp:TextBox ID="txtSubject" Width="500px" runat="server"></asp:TextBox></h2>
                <div>
                    Attach File:
                    <asp:FileUpload ID="flVend" runat="server" class="multi" />
                    <asp:GridView ID="grdVendAtc" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" CellSpacing="22">
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
                <cc1:Editor ID="HeaderEditorVendor" Width="1000px" Height="200px" runat="server" />
                <h2>Body Template</h2>
                <asp:Label ID="lblMaterialsVendor" runat="server"></asp:Label>
                <h2>Footer Template</h2>
                <cc1:Editor ID="FooterEditorVendor" Width="1000px" Height="200px" runat="server" />


            </div>
            <br />
            <br />
            <div class="btn_sec">
                <asp:Button ID="btnUpdateVendor" runat="server" Text="Update" OnClick="btnUpdateVendor_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
