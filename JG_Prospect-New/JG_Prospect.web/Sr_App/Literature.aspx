<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="Literature.aspx.cs" Inherits="JG_Prospect.Sr_App.Literature" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="right_panel">
        <div class="page_content">
            <h1>
                Literature</h1>
            <asp:GridView ID="GrdLiterature" Width="100%" AlternatingRowStyle-Height="50px"
                runat="server" DataKeyNames="Id" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="Link" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkliterature" runat="server" Text='<%#Eval("Link")%>' OnClick="lnkliterature_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbldescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
