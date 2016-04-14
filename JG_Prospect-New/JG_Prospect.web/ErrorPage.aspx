<%@ Page Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="JG_Prospect.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="tabs-1">
      <div class="login_form_panel">
       <h1>Error Page</h1>
        <table id="error" runat="server" style="width:100%">
            <tr>
                <td align="center"  style="height:40px">
                  
                </td>
            </tr>
             <tr>
                <td  align="center" class="textEntry">
                    There is some problem in page.Please report to web server.
                </td>
            </tr>
             <tr>
                <td  align="center">
                   Go To : <asp:LinkButton ID="lnkError" Text="Home" runat="server" onclick="lnkError_Click"> </asp:LinkButton>
                </td>
            </tr>
        </table>
        </div>
       
    </div>
  </asp:Content>
