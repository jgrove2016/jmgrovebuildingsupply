<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="ProgressReport.aspx.cs" Inherits="JG_Prospect.ProgressReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            Progress Report</h1>
        <div class="form_panel_custom">
            <%--<div class="clr">
        </div>--%>
            <ul>
                <li style="width: 49%;">
                    <table cellpadding="0" cellspacing="0" border="0" runat="server">
                        <tr>
                            <td>
                                <label>
                                    <strong>Select Period:</strong></label><br />
                                <label style="width: 50px; text-align: right;">
                                    From :</label><br />
                                <asp:TextBox ID="txtfrmdate" CssClass="date" runat="server"  AutoPostBack="true" TabIndex="1" onkeypress="return isDateKey(event);"
                                    MaxLength="10" Style="width: 150px;" OnTextChanged="txtfrmdate_TextChanged"></asp:TextBox>
                                <label>
                                </label>
                                <%--      <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtfrmdate">
                            </ajaxtoolkit:calendarextender>--%>
                                <%--  <asp:RequiredFieldValidator ID="requirefrmdate" ControlToValidate="txtfrmdate" ValidationGroup="submit" Display="Dynamic"
                                runat="server" ErrorMessage=" Select From date" ForeColor="Red">
                            </asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <label style="width: 50px; text-align: right;">
                                    <br />
                                    To :</label>
                                <asp:TextBox ID="txtTodate" CssClass="date" runat="server"  AutoPostBack="true" TabIndex="2" onkeypress="return isDateKey(event);"
                                    MaxLength="10" Style="width: 150px;" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>
                                <%--  <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtTodate">
                            </ajaxtoolkit:calendarextender>--%>
                                <%-- <asp:RequiredFieldValidator ID="Requiretodate" ControlToValidate="txtTodate" Display="Dynamic" ValidationGroup="submit"
                                runat="server" ErrorMessage=" Select To date" ForeColor="Red">
                            </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <strong>Select User:</strong></label>&nbsp;
                                <label style="width: 50px; text-align: right;">
                                </label>
                                <asp:DropDownList ID="ddlusername" runat="server" Width="160px" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </li>
                <li class="last" style="width: 49%;">
                    <table id="Table1" cellpadding="0" cellspacing="0" border="0" runat="server">
                        <tr>
                            <td>
                                <br />
                                <br />
                                <label>
                                    <strong>Select Pay Period : </strong>
                                </label>
                                <asp:DropDownList ID="drpPayPeriod" runat="server" Width="250px" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpPayPeriod_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button runat="server" ID="btnshow" Text="Show" CssClass="cancel" OnClick="btnshow_Click"
                    ValidationGroup="submit" TabIndex="4" />
            </div>
            <div class="grid_h">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblTotalSeen" runat="server" Visible="false"><strong>Total Seen EST  :   </strong></asp:Label>
                            <asp:Label ID="lblTotalSeenEST" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblProspects" runat="server" Visible="false"><strong>Total Prospects  :  </strong></asp:Label>
                            <asp:Label ID="lblTotalProspects" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTotalSet" runat="server" Visible="false"><strong>Total Set EST  :   </strong></asp:Label>
                            <asp:Label ID="lblTotalSetEST" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblre" runat="server" Visible="false"><strong>Rehash %  :  </strong></asp:Label>
                            <asp:Label ID="lblRehash" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGrSales" runat="server" Visible="false"><strong>Total Gross Sales  :   </strong></asp:Label>
                            <asp:Label ID="lblGrossSales" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTPDC" runat="server" Visible="false"><strong>Total Prospect Data Completion % : </strong></asp:Label>
                            <asp:Label ID="lblTotalProspectDataCompletion" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Visible="false"><strong>Installer Prospect : </strong></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <div class="grid">
                    <asp:GridView ID="grddata" runat="server" Width="1000px" CssClass="tableClass" AutoGenerateColumns="false"
                        HeaderStyle-Wrap="true" AllowSorting="true" OnSorting="grddata_Sorting">
                        <Columns>
                            <asp:BoundField DataField="Username" HeaderText="Jr. Sales Rep" SortExpression="Username"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="Status" HeaderText="Status/Date" SortExpression="Status"
                                HeaderStyle-Width="250px" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="CustomerId" HeaderText="CustomerId/ProspectId" SortExpression="CustomerId"
                                HeaderStyle-Width="70px" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="ProductOfInterest" HeaderText="ProductOfInterest" SortExpression="ProductOfInterest"
                                HeaderStyle-Width="130px" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="Source" HeaderText="Source" HeaderStyle-Width="130px"
                                SortExpression="Source" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="GrossSale" HeaderText="GrossSale" HeaderStyle-Width="100px"
                                SortExpression="GrossSale" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="PayPeriod" HeaderText="PayPeriod" HeaderStyle-Width="150px"
                                SortExpression="PayPeriod" HeaderStyle-Wrap="true" />
                            <asp:BoundField DataField="ProspectDataCompletion" HeaderText="ProspectDataCompletion%"
                                SortExpression="ProspectDataCompletion" HeaderStyle-Width="70px" HeaderStyle-Wrap="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="clr">
            </div>
            <br />
            <div class="btn_sec">
                <asp:Button runat="server" ID="btnExporttoExcel" Text="Export to Excel" CssClass="cancel"
                    Visible="false" OnClick="btnExporttoExcel_Click" />
            </div>
            <div class="clr">
            </div>
        </div>
    </div>
</asp:Content>
