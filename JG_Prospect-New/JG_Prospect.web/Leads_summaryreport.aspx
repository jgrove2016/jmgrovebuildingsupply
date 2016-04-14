<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="Leads_summaryreport.aspx.cs" Inherits="JG_Prospect.Leads_summaryreport" %>

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
        Prospect List</h1>
    <div class="form_panel">
        <%--<div class="clr">
        </div>--%>
        <ul>
            <li>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <label>
                                <strong>Select Period:</strong></label>
                            <label style="width: 50px; text-align: right;">
                                <span>*</span> From :</label>
                            <asp:TextBox ID="txtfrmdate" CssClass="date" runat="server" TabIndex="1" MaxLength="70"
                                Style="width: 150px;"></asp:TextBox>
                            <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrmdate">
                            </ajaxToolkit:CalendarExtender>--%>
                            <asp:RequiredFieldValidator ID="requirefrmdate" ControlToValidate="txtfrmdate" Display="Dynamic"
                                runat="server" ErrorMessage=" Select From date" ValidationGroup="aa" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </li>
            <li class="last">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <label style="width: 50px; text-align: right;">
                                <span>*</span> To :</label>
                            <asp:TextBox ID="txtTodate" CssClass="date" runat="server" TabIndex="2" MaxLength="70"
                                Style="width: 150px;"></asp:TextBox>
                            <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate">
                            </ajaxToolkit:CalendarExtender>--%>
                            <asp:RequiredFieldValidator ID="Requiretodate" ControlToValidate="txtTodate" Display="Dynamic"
                                runat="server" ErrorMessage=" Select To date" ValidationGroup="aa" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </li>
            <li>
                <table cellpadding="0" cellspacing="0" border="0">
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
        </ul>
        <div class="btn_sec">
            <asp:Button runat="server" ID="btnshow" Text="Show" CssClass="cancel" ValidationGroup="aa"
                OnClick="btnshow_Click" TabIndex="4" />
        </div>
        <div class="clr">
        </div>
        <div class="grid_h">
            <strong>Prospect List</strong></div>
        <div class="grid">
            <asp:GridView ID="grddata" runat="server" Width="1730px" AutoGenerateColumns="false" CssClass="tableClass" HeaderStyle-Wrap="true">
                <Columns>
                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="10px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkprospectid" runat="server" Text='<% #Eval("ID")%>' 
                                onclick="lnkprospectid_Click" Width="5px"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Estimate Date/Time" DataField="Estimate Date/Time"  HeaderStyle-Width="250px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName"  HeaderStyle-Width="140px" HeaderStyle-Wrap="true" />                  
                    <asp:BoundField HeaderText="Phone" DataField="Phone"  HeaderStyle-Width="140px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Alt. Phone" DataField="Alt. Phone"   HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Email" DataField="Email"   HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Address" DataField="Address"   HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="City, State, Zip" DataField="City_State_Zip"  HeaderStyle-Width="250px" HeaderStyle-Wrap="true" />
                   <%-- <asp:BoundField HeaderText="Follow Up/Status" DataField="Follow Up/Status" />--%>
                    <asp:BoundField HeaderText="Product Of Interest" DataField="Product Of Interest"   HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Best Time To Contact" DataField="Best Time To Contact"   HeaderStyle-Width="50px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Status" DataField="Status"  HeaderStyle-Width="50px" HeaderStyle-Wrap="true" />
                    <asp:BoundField HeaderText="Pay Period" DataField="Pay Period"   HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                    <asp:BoundField HeaderText="Jr. Sales" DataField="Jr. Sales"   HeaderStyle-Width="140px" HeaderStyle-Wrap="true"/>
                </Columns>
            </asp:GridView>
        </div>
        <div class="clr">
        </div>
        <br />
        <div class="btn_sec">
            <asp:Button runat="server" ID="btnExporttoExcel" Text="Export to Excel" CssClass="cancel"
                Visible="false" TabIndex="5" OnClick="btnExporttoExcel_Click" />
        </div>
        <div class="clr">
        </div>
    </div>
    </div>
</asp:Content>
