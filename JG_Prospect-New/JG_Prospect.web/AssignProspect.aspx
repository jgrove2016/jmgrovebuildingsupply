<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="AssignProspect.aspx.cs" Inherits="JG_Prospect.AssignProspect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%---------start script for Datetime Picker----------%>
    <link href="datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            $(".date").datepicker();
            $('.time').ptTimeSelect();

        });
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
     <ul class="appointment_tab">
          <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
          <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
          <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
    </ul>
    <h1>
        Assign Customer</h1>
    <div class="form_panel">
       <%-- <div class="clr">
        </div>--%>
        <ul>
            <li>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <label>
                                <strong>Select Period:</strong></label>
                            <label style="width: 50px; text-align: right;">
                                From :</label>
                            <asp:TextBox ID="txtfrmdate" CssClass="date" runat="server" TabIndex="1" onkeypress="return false"
                                MaxLength="10" Style="width: 150px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </li>
            <li class="last">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <label style="width: 50px; text-align: right;">
                                To :</label>
                            <asp:TextBox ID="txtTodate" CssClass="date" runat="server" TabIndex="2" onkeypress="return false"
                                MaxLength="10" Style="width: 150px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
        <div class="btn_sec">
            <asp:Button runat="server" ID="btnshow" Text="Show" CssClass="cancel" TabIndex="3"
                OnClick="btnshow_Click" />
        </div>
        <div class="clr">
        </div>
        <div class="grid_h">
            <strong>Assign Customer List</strong></div>
        <div class="grid">
            <asp:GridView ID="grddata" runat="server" AutoGenerateColumns="false" Width="1950px" CssClass="tableClass"
                OnRowDataBound="grddata_RowDataBound" DataKeyNames="id" HeaderStyle-Wrap="true">
                <Columns>
               <%-- <asp:BoundField HeaderText="Customer Id" DataField="id" />--%>
                    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="100px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Customer Address" DataField="CustomerAddress" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="State" DataField="State"  HeaderStyle-Width="100px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="City" DataField="City"  HeaderStyle-Width="100px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Zip Code" DataField="ZipCode" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Job Location" DataField="JobLocation" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Est. Date" DataField="EstDateSchdule"  HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Est. Time" DataField="EstTime"  HeaderStyle-Width="100px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Cell Phone" DataField="CellPh"  HeaderStyle-Width="100px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="House Phone" DataField="HousePh" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Call Taken By" DataField="CallTakenBy" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Best Time to contact" DataField="BestTimetocontact" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:TemplateField HeaderText="Assigned To" HeaderStyle-Width="100px" HeaderStyle-Wrap="true">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnassign" runat="server" Value='<%#Eval("AssignedToId")%>' />
                            <asp:DropDownList ID="ddlassignuser" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Meeting Status" DataField="MeetingStatus" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                    <asp:TemplateField HeaderText="Select to Assign Customer" HeaderStyle-Width="100px" HeaderStyle-Wrap="true">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkprospect" runat="server" Checked="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="clr">
        </div>
        <br />
        <div class="btn_sec">
            <asp:Button runat="server" ID="btnSave" Text="Update" CssClass="cancel" Visible="false" TabIndex="4"
                OnClick="btnSave_Click" />
        </div>
        <div class="clr">
        </div>
    </div>
    </div>
</asp:Content>
