<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true" CodeFile="refresh.aspx.cs" Inherits="Sr_App_refresh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style4 {
                width: 1058px;
                float: right;
                    height: 720px;
        }
        .auto-style5 {
            width: 100%;
            height: 338px;
        }
        .auto-style6 {
            width: 481px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="auto-style4">
        
        

        <table class="auto-style5">
            <tr>
                <td align="center" bgcolor="#A14240" colspan="3" style="font-size: 30px; color: #FFFFFF">Show Duplicate Table Data And Remove </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Button ID="Button1" runat="server" BackColor="#A14240" BorderColor="#A14240" ForeColor="White" Height="40px" OnClick="Button1_Click" Text="SHOW" Width="123px" />
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="DELETE" BackColor="#A14240" BorderColor="#A14240" Font-Bold="True" ForeColor="White" Height="40px" Width="123px" OnClick="Button2_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="300px">
                        <center>
                    <asp:GridView ID="GridView1" runat="server" Height="264px" Width="544px" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                            </center>
                         </asp:Panel>
                </td>
            </tr>
           
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        


    </div>
    
    
</asp:Content>

