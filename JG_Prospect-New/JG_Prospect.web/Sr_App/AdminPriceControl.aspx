<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="AdminPriceControl.aspx.cs" Inherits="JG_Prospect.Sr_App.AdminPriceControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        <h1>
            Admin Price Control</h1>
        <div class="form_panel">
            <ul>
                <li>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <label>
                                    <strong>Select Shutter:</strong></label>
                            
                                <asp:DropDownList ID="ddlshutter" runat="server" Style="width: 150px;" AutoPostBack="True" TabIndex="1"
                                    OnSelectedIndexChanged="ddlshutter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                            <td>
                                <label>
                                    <strong>Shutter Price:</strong></label>
                            
                                <asp:TextBox ID="txtshutterprice" Enabled="false" CssClass="required date" runat="server"  onkeyup="javascript:Numeric(this)"
                                    TabIndex="3" MaxLength="10" Style="width: 150px;" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredsprice" runat="server" ControlToValidate="txtshutterprice"
                                    ErrorMessage="Please Enter Shutter Price." ValidationGroup="updateshutter" 
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="btn_sec">
                        <asp:Button runat="server" ID="btnshutter" Text="Update" ValidationGroup="updateshutter" TabIndex="5"
                            CssClass="cancel" OnClick="btnshutter_Click" />
                        <asp:LinkButton ID="lnkAddshutter" Text="Add New" runat="server" TabIndex="6"
                            OnClick="lnkAddshutter_Click"></asp:LinkButton>
                    </div>
                    
                </li>
                <li class="last">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <label>
                                    <strong>Select Top Shutter:</strong></label>
                            
                                <asp:DropDownList ID="ddltopshutter" runat="server" Style="width: 150px;" AutoPostBack="True" TabIndex="2"
                                    OnSelectedIndexChanged="ddltopshutter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                            <td>
                                <label>
                                    <strong>Top Shutter Price:</strong></label>
                            
                                <asp:TextBox ID="txttopshutterprice" Enabled="false" CssClass="required date" runat="server" onkeyup="javascript:Numeric(this)"
                                    TabIndex="4" MaxLength="10" Style="width: 150px;" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredstopprice" runat="server" ControlToValidate="txttopshutterprice"
                                    ErrorMessage="Please Enter Shutter Top Price." ValidationGroup="updateshuttertop"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="btn_sec">
                        <asp:Button runat="server" ID="btntopshutter" Text="Update" ValidationGroup="updateshuttertop" TabIndex="7"
                            CssClass="cancel" OnClick="btntopshutter_Click" />
                        <asp:LinkButton ID="lnkaddtopshutter" Text="Add New " runat="server" TabIndex="8"></asp:LinkButton>
                    </div>
                    
                    <asp:ModalPopupExtender ID="mpetopshutter" runat="server" TargetControlID="lnkaddtopshutter"
                        PopupControlID="pnlpopup" CancelControlID="btnCancel">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="550px"
                        Style="display: none">
                        <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background:#fff;"
                            cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                    align="center">
                                    Shutter Top Details
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">
                                    Shutter Top Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtshuttertopname" runat="server" onkeypress="return isAlphaKey(event);"
                                        MaxLength="30"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredshuttertopname" runat="server" ControlToValidate="txtshuttertopname"
                                        ValidationGroup="addshuttertop" ErrorMessage="Please Enter Shutter Top Name."
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">
                                    Shutter Top Price
                                </td>
                                <td>
                                    <asp:TextBox ID="txtshuttertopprice" runat="server" MaxLength="10" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredshuttertopprice" runat="server" ControlToValidate="txtshuttertopprice"
                                        ValidationGroup="addshuttertop" ErrorMessage="Please Enter Shutter Top Price."
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                              
                               
                                <td align="center" colspan="2">
                                <asp:Button ID="btnshuttertop" CommandName="Insert" runat="server" Text="Save" OnClick="btnaddshuttertop_Click"
                                        ValidationGroup="addshuttertop" Width="100" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </li>
            </ul>
            <ul>
                <li>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <label>
                                    <strong>Select Shutter Color:</strong></label>
                           
                                <asp:DropDownList ID="ddlshuttercolor" runat="server" Style="width: 150px;" AutoPostBack="True" TabIndex="9"
                                    OnSelectedIndexChanged="ddlshuttercolor_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                            <td>
                                <label>
                                    <strong>Shutter Color Price:</strong></label>
                            
                                <asp:TextBox ID="txtshuttercolorprice" Enabled="false" CssClass="required date" runat="server"
                                    TabIndex="11" MaxLength="10" Style="width: 150px;" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredscolorprice" runat="server" ControlToValidate="txtshuttercolorprice"
                                    ErrorMessage="Please Enter Shutter Color Price." ValidationGroup="updateshuttercolor"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="btn_sec">
                        <asp:Button runat="server" ID="btnshutterclor" Text="Update" CssClass="cancel" OnClick="btnshuttercolor_Click" TabIndex="13"
                            ValidationGroup="updateshuttercolor" />
                        <asp:LinkButton ID="lnkaddshuttercolor" Text="Add New" TabIndex="14" runat="server"></asp:LinkButton>
                    </div>
                    
                    <asp:ModalPopupExtender ID="mdeaddshuttercolor" runat="server" TargetControlID="lnkaddshuttercolor"
                        PopupControlID="pnlpopupshuttercolor" CancelControlID="btnCancel">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlpopupshuttercolor" runat="server" BackColor="White" Height="269px"
                        Width="550px" Style="display: none">
                        <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background:#fff;"
                            cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                    align="center">
                                    Shutter Color Details
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">
                                    Shutter Color Code
                                </td>
                                <td>
                                    <asp:TextBox ID="txtshuttercolorcode" runat="server" onkeypress="return isNumericKey(event);"
                                        MaxLength="5"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredshuttercolorcode" runat="server" ControlToValidate="txtshuttercolorcode"
                                        ValidationGroup="addshuttercolor" ErrorMessage="Please Enter Shutter Color Code."
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Shutter Color Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtshuttercolorname" runat="server" onkeypress="return isAlphaKey(event);"
                                        MaxLength="30"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredshuttercolorname" runat="server" ControlToValidate="txtshuttercolorname"
                                        ValidationGroup="addshuttercolor" ErrorMessage="Please Enter Shutter Color Name."
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Shutter Color Price
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcolorprice" runat="server" MaxLength="10" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredshuttercolorprice" runat="server" ControlToValidate="txtcolorprice"
                                        ValidationGroup="addshuttercolor" ErrorMessage="Please Enter Shutter Color Price."
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                <asp:Button ID="btnshuttercolor" CommandName="Insert" runat="server" Text="Save"
                                        OnClick="btnaddshuttercolor_Click" ValidationGroup="addshuttercolor" Width="100" />
                                    <asp:Button ID="Button2" runat="server" Text="Cancel" Width="100" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </li>
                <li class="last">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <label>
                                    <strong>Select Accessories:</strong></label>
                           
                                <asp:DropDownList ID="ddlshutteraccessories" runat="server" Style="width: 150px;" TabIndex="10"
                                    OnSelectedIndexChanged="ddlshutteraccessories_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                            <td>
                                <label>
                                    <strong>Accessories Price:</strong></label>
                          
                                <asp:TextBox ID="txtshutteraccessoriesprice" Enabled="false" CssClass="required date"
                                    runat="server" TabIndex="12" MaxLength="10" Style="width: 150px;" 
                                    onkeypress="return isNumericKey(event);" 
                                    ontextchanged="txtshutteraccessoriesprice_TextChanged"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredsaccessoriesprice" runat="server" ControlToValidate="txtshutteraccessoriesprice"
                                    ErrorMessage="Please Enter Shutter Accessories Price." ValidationGroup="updateshutteraccessories"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="btn_sec">
                        <asp:Button runat="server" ID="btnshutterwidth" Text="Update" CssClass="cancel" OnClick="btnshutteraccessories_Click" TabIndex="15"
                            ValidationGroup="updateshutteraccessories" />
                        <asp:LinkButton ID="lnkaddmpeshutteraccessories" Text="Add New" TabIndex="16"
                        runat="server"></asp:LinkButton>
                    </div>
                    
                    <asp:ModalPopupExtender ID="mpeshutteraccessories" runat="server" TargetControlID="lnkaddmpeshutteraccessories"
                        PopupControlID="pnlpopupshutteraccessories" CancelControlID="btnCancel">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlpopupshutteraccessories" runat="server" BackColor="White" Height="269px"
                        Width="550px" Style="display: none">
                        <table width="100%" style="border: Solid 3px #A33E3F; background:#fff; width: 100%; height: 100%"
                            cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                    align="center">
                                    Accessories Details
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">
                                    Accessories Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtshutteraccessoriesname" runat="server" onkeypress="return isAlphaKey(event);"
                                        MaxLength="30"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredtxtshutteraccessoriesname" runat="server"
                                        ControlToValidate="txtshutteraccessoriesname" ValidationGroup="addshutteraccessories"
                                        ErrorMessage="Please Enter Name." ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Accessories Price
                                </td>
                                <td>
                                    <asp:TextBox ID="txttshutteraccessoriesprice" runat="server" MaxLength="10" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="Requiredshutteraccessoriesprice" runat="server" ControlToValidate="txttshutteraccessoriesprice"
                                        ValidationGroup="addshutteraccessories" ErrorMessage="Please Enter Price."
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                               
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnaddshutteraccessories" CommandName="Insert" runat="server" Text="Save"
                                        OnClick="btnaddshutteraccessories_Click" ValidationGroup="addshutteraccessories" Width="100" />
                               
                                    <asp:Button ID="Button3" runat="server" Text="Cancel" Width="100" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
