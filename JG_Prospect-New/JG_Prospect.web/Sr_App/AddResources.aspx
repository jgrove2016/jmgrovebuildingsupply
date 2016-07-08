<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="AddResources.aspx.cs" Inherits="JG_Prospect.Sr_App.AddResources" %>

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
            <b>Add Resources</b></h1>
        <div class="form_panel">
            <span>
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
            </span>
            <ul style="width: 100%;">
                <li style="width: 98%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    <span>*</span> Type of Resource</label>
                                <asp:DropDownList ID="ddltype" runat="server" TabIndex="1">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Shared Documents" Value="SharedDocument"></asp:ListItem>
                                    <asp:ListItem Text="Videos" Value="Videos"></asp:ListItem>
                                    <asp:ListItem Text="Literature" Value="Literature"></asp:ListItem>
                                    <asp:ListItem Text="Training Tools" Value="TrainingTools"></asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit"
                                    runat="server" ControlToValidate="ddltype" ForeColor="Red" ErrorMessage="Please Select type of resource"
                                    InitialValue="0"> 
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Upload file(s)</label>
                                <asp:FileUpload ID="uploadlink" runat="server" CssClass="multi" multiple />
                            </td>
                        </tr>
                        <%--<tr>
                           
                            <td>
                                <label>
                                    Description</label>
                                <asp:TextBox ID="txtdescription" TextMode="MultiLine" TabIndex="4" runat="server" Height="95px"></asp:TextBox>
                            </td>
                        </tr>--%>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
            <asp:Button ID="btnadd" Text="Add" runat="server" Visible="false" onclick="btnadd_Click" />
                <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit" OnClick="btnSubmit_Click"
                    ValidationGroup="Submit" />
                <asp:Button ID="btnreset" runat="server" TabIndex="6" Text="Reset" OnClick="btnreset_Click" />
            </div>
        </div>
    </div>
</asp:Content>
