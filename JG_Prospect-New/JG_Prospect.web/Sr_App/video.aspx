<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="video.aspx.cs" Inherits="JG_Prospect.Sr_App.video" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
<div style="text-align:center" >
<iframe width="640" height="360" src="<%= VideoURL %>" frameborder="0" allowfullscreen></iframe>
</div>
<br />
<div style="text-align:center" >
  <%= VideoDescription %> 
</div>
<br />
     <%--   <iframe width="640" height="390" src="<%= VideoURL %>?wmode=transparent" frameborder="0" ></iframe>--%>        
          <div class="page_content">
          <h1>Videos	</h1>
       
         <asp:GridView ID="grdvideo" Width="100%" AlternatingRowStyle-Height="50px" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" >
         <Columns>
         <asp:TemplateField AccessibleHeaderText="Link" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>         
         <asp:LinkButton ID="lnkvideourl" runat="server" Text='<%#Eval("Link")%>' 
                 onclick="lnkvideourl_Click"></asp:LinkButton>
         </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
         </asp:TemplateField>
         <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
         <ItemTemplate>
         <asp:Label ID="lbldescription" runat="server" Text='<%#Eval("Description")%>' ></asp:Label>
         </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
         </asp:TemplateField>
         </Columns>
         </asp:GridView> 
         </div>     
         </div>
</asp:Content>
