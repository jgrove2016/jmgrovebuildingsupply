<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="MaterialConfirmation.aspx.cs" Inherits="JG_Prospect.Sr_App.MaterialConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <h1>
            <b>Material Confirmation:</b></h1>
        <div class="form_panel">
            <ul>
                <li>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>

                            </td>                            
                        </tr>
                        <tr>
                            <td>
                            </td>                            
                        </tr>
                    </table>
                </li>
                 <li>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>

                            </td>                            
                        </tr>
                        <tr>
                            <td>

                            </td>                            
                        </tr>
                    </table>
                </li>              
            </ul>
              <div class="btn_sec">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Confirm" />
                    </div>
        </div>
    </div>
</asp:Content>
