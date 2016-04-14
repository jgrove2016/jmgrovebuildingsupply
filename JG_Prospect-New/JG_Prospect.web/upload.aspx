<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="JG_Prospect.upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
     <ul class="appointment_tab">
          <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
          <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
          <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
    </ul>
<h1>Import Data</h1>
     <div class="form_panel">
       <span >
        <asp:Label ID="lblmsg" runat="server" Visible="false" ></asp:Label>
        </span>
     <ul>
     <li>
     <table>
     <tr>
     <td><asp:UpdatePanel ID="pnl_upload" runat="server" UpdateMode="Conditional">      
         <ContentTemplate>
          <div class="clr"></div>

    <label>&nbsp;&nbsp; Select excel file to upload:</label>
       
    <asp:FileUpload ID="dataupload" runat="server" AllowedFileExtensions=".xls"  TabIndex="1" />
    </ContentTemplate>
    </asp:UpdatePanel></td>
     </tr>
     </table>     
     </li>
     <li class="last">
     <table>
     <tr>
     <td>&nbsp;</td>
     </tr>
     </table>     
     </li>
     </ul>
<div class="btn_sec">
       <asp:Button runat="server" ID="btnImport" Text="Import" CssClass="cancel" TabIndex="2" onclick="btnImport_Click" />
     </div>
         
    
     
        <br />
      <div class="clr"></div>      
      <div class="grid_h"><strong>Imported Records</strong></div>
      <div class="grid">
          <asp:GridView ID="grddata" runat="server" Width="2000">
          </asp:GridView>
      
      </div>
      <br />
         <div class="clr"></div>
         <div class="btn_sec">
          <asp:Button runat="server" ID="btnsave" Visible="false"
              Text="Save" tabindex="3" CssClass="cancel" onclick="Save_Click"  />  
         </div>
         </div>      
    
    </div>
</asp:Content>
