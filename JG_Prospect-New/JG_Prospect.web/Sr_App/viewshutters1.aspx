<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="viewshutters1.aspx.cs" Inherits="JG_Prospect.Sr_App.viewshutters1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
<h1><b>Customer</b></h1>
<!-- Tabs starts -->
          <div id="tabs">
            <ul>
              <li style="margin:0;"><a href="#tabs-2">Customer</a></li>
            </ul>
            <div id="tabs-1">
            <div class="form_panel">
            <h2>Details</h2>
                   <asp:GridView ID="grdshutters" runat="server" AutoGenerateColumns="False" CssClass="grid" 
            Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ORDER">
            <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField HeaderText="ORDER">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"><%#Eval("ORDER")%></asp:LinkButton>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Manufacturer">
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Manufacturer")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Style">
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Style")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Color">
        <ItemTemplate>
            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Color")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>


         <asp:TemplateField HeaderText="Image">
        <ItemTemplate>
            <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Image") %>' Width="50px" Height="50px" />       
        </ItemTemplate>
        </asp:TemplateField>


              <asp:TemplateField HeaderText="Create Contract" >
        <ItemTemplate>
            <asp:Button ID="btncreatecontract" runat="server" Text="Create Contract" 
                onclick="btncreatecontract_Click" CommandArgument='<%#Eval("ORDER")%>' />
        </ItemTemplate>
        </asp:TemplateField>


        </Columns>
            <FooterStyle />
            <HeaderStyle />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#cccccc" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>

            
            </div>
            </div>
            <div id="tabs-2"></div>
          </div>
          <!-- Tabs endss -->

</div>
</asp:Content>
