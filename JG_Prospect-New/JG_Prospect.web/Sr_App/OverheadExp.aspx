<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="OverheadExp.aspx.cs" Inherits="JG_Prospect.Sr_App.OverheadExp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        fieldset{ border: solid 1px;}
       
        .form div {text-align:center;}
        .form table{
            width:70%;
            margin:5px;
        }
        .form table td {padding:5px;}
         .form input[type=text]{
            height:25px;
            min-width:200px;
        }
          .form input[type=submit]{
            background-color:#A13738;
            line-height: 30px;
            padding: 0px 10px 0px 10px;
            color: #fff;
            font-size: 14px;
            margin: 5px 10px;
            cursor: pointer;
            border-radius: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <h1 id="h1Heading" runat="server">Overhead Expenses</h1>
        <div>
            <asp:Panel ID="pnlAddEditAccount" GroupingText="Add Account" runat="server">
               <div class="form">
                    <table  >
                        <tr>
                            <td style="text-align:right;">Person Name:</td>
                            <td><asp:TextBox ID="txtPersonName" ValidationGroup="AddAccount" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="reqPName" ValidationGroup="AddAccount" runat="server" ErrorMessage="Person Name is required" Text="*" ControlToValidate="txtPersonName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Bank Name:</td>
                            <td><asp:TextBox ID="txtBankName" runat="server" ValidationGroup="AddAccount"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="AddAccount" ID="reqBankN" runat="server" ErrorMessage="Bank Name is required" Text="*" ControlToValidate="txtBankName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">Bank Branch:</td>
                            <td><asp:TextBox ID="txtBankBranch" ValidationGroup="AddAccount" runat="server"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="AddAccount" ID="reqBankBr" runat="server" ErrorMessage="Bank Branch is required" Text="*" ControlToValidate="txtBankBranch"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">A/c Holder Name:</td>
                            <td><asp:TextBox ID="txtAccountHolderName" ValidationGroup="AddAccount" runat="server"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="AddAccount" ID="reqAcName" runat="server" ErrorMessage="A/C Holder Name is required" Text="*" ControlToValidate="txtAccountHolderName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">A/C Number:</td>
                            <td><asp:TextBox ID="txtAccountNumber" ValidationGroup="AddAccount" runat="server"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="AddAccount" ID="reqAcNum" runat="server" ErrorMessage="A/C Number is required" Text="*" ControlToValidate="txtAccountNumber"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">IFSC Code:</td>
                            <td><asp:TextBox ID="txtIFSCCode" runat="server" ValidationGroup="AddAccount"></asp:TextBox><asp:RequiredFieldValidator  ValidationGroup="AddAccount" ID="reqIFSC" runat="server" ErrorMessage="IFSC Code is required" Text="*" ControlToValidate="txtIFSCCode"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">SWIFT Code:</td>
                            <td><asp:TextBox ID="txtSwiftCode" runat="server"  ValidationGroup="AddAccount"></asp:TextBox><asp:RequiredFieldValidator   ValidationGroup="AddAccount" ID="reqSwift" runat="server" ErrorMessage="SWIFT Code is required" Text="*" ControlToValidate="txtSwiftCode"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center">
                                <asp:ValidationSummary ID="valAdd" ShowMessageBox="true" ShowSummary="false" runat="server" ValidationGroup="AddAccount" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Add Bank Account" ValidationGroup="AddAccount" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="AddAccount2" CausesValidation="false" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:LinkButton ID="lnkAdd" OnClick="lnkAdd_Click" runat="server">Add</asp:LinkButton>
            <asp:ListView ID="lstBankAccount" runat="server" OnItemCommand="lstBankAccount_ItemCommand" ItemPlaceholderID="plcBankAc">
                <LayoutTemplate>
                    <table class="grid">
                        <tr>
                            <th>#</th>
                            <th>Person Name</th>
                            <th>Bank Name</th>
                            <th>Bank Branch</th>
                            <th>A/c on Name</th>
                            <th>A/c #</th>
                            <th>IFSC Code</th>
                            <th>SWIFT Code</th>
                            <th></th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="plcBankAc"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                         <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td><%#Eval("PersonName") %></td>
                            <td><%#Eval("BankName") %></td>
                            <td><%#Eval("BankBranch") %></td>
                            <td><%#Eval("AccountName") %></td>
                            <td><%#Eval("AccountNumber") %></td>
                            <td><%#Eval("IFSCCode") %></td>
                            <td><%#Eval("SWIFTCode") %></td>
                            <td>
                                <asp:LinkButton ID="lnkEdit" CommandName="RecEdit" CommandArgument='<%#Eval("BankID") %>' runat="server">Edit</asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" CommandName="RecDelete" CommandArgument='<%#Eval("BankID") %>' OnClientClick="return confirm('Are you sure you want to delete this record?')"  runat="server">Delete</asp:LinkButton>
                            </td>
                        </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
 
    </div>
</asp:Content>
