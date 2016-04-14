<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/JG.Master"
    AutoEventWireup="true" CodeBehind="StaticReport.aspx.cs" Inherits="JG_Prospect.StaticReport" %>

<%--<%@ Register Src="~/Controls/left.ascx" TagName="leftmenu" TagPrefix="uc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%---------start script for Datetime Picker----------%>
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker);
            bindPicker();
        });

        function bindPicker() {
            $("input[type=text][id*=ContentPlaceHolder1_grdstaticdata_txtfollowup]").datepicker();
        }

        $(function () {
            $(".chg").change(function () {
                if ($(this).find('option:selected').text() == "Follow up") {
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdstaticdata_txtfollowup']")._show();
                }
                else {
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdstaticdata_txtfollowup']")._hide();
                }
            });
        });

    </script>
    <%-- <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            $(".date").datepicker();
            $('.time').ptTimeSelect();
            $('#ContentPlaceHolder1_grdstaticdata_txtfollowup_0').datepicker();
        });       
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <div class="left_panel arrowlistmenu">
        <uc1:leftmenu ID="left1" runat="server" />
    </div>--%>
    <div class="right_panel">
        <ul class="appointment_tab">
            <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
            <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
            <li><a id="A3" href="StaticReport.aspx" runat="server" class="active">Call Sheet</a></li>
        </ul>
        <h1>
            <b>Dashboard</b></h1>
        <h2>
            (Rehash / Follow up) Call Sheet:</h2>
        <div class="form_panel">
            <%--<ul>
            <li>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <label>
                                First Name</label>
                            <asp:TextBox ID="txt_fname" runat="server" TabIndex="1" ValidationGroup="submit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Phone</label>
                            <asp:TextBox ID="txtphone" runat="server" TabIndex="3" ValidationGroup="submit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Follow up Date</label>
                            <asp:TextBox ID="txtfollowupdate" runat="server" TabIndex="5"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfollowupdate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </li>
            <li class="last">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <label>
                                Last Name</label>
                            <asp:TextBox ID="txtlastname" runat="server" TabIndex="2" ValidationGroup="submit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Email</label>
                            <asp:TextBox ID="txtemail" runat="server" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Status</label>
                            <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="14">
                                <asp:ListItem Value="Select" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="Set"></asp:ListItem>
                                <asp:ListItem Value="Prospect"></asp:ListItem>
                                <asp:ListItem Value="PTW est"></asp:ListItem>
                                <asp:ListItem Value="est>$1000"></asp:ListItem>
                                <asp:ListItem Value="est<$1000"></asp:ListItem>
                                <asp:ListItem Value="EST-one legger"></asp:ListItem>
                                <asp:ListItem Value="sold>$1000"></asp:ListItem>
                                <asp:ListItem Value="sold<$1000"></asp:ListItem>
                                <asp:ListItem Value="rehash"></asp:ListItem>
                                <asp:ListItem Value="cancelation-no rehash"></asp:ListItem>                              
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
        <div class="btn_sec">
            <asp:Button runat="server" ID="btnsearch" Text="Search" TabIndex="7" Visible="true"
                OnClick="btnsearch_Click" />
            <asp:Button runat="server" ID="btnreset" Visible="true" Text="Reset" TabIndex="8"
                OnClick="btnreset_Click" />
        </div>--%>
            <div class="grid_h" id="divcallsheet" runat="server">
            </div>
            <div class="grid">
                <asp:UpdatePanel ID="Upgrdstaticdata" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdstaticdata" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="tableClass"
                            OnRowDataBound="grdstaticdata_RowDataBound">                         
                            <Columns>
                                <asp:TemplateField HeaderText="Estimate ID">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkestimateid" runat="server" Text='<%# Bind("id") %>' CommandArgument='<%# Bind("id") %>'
                                            OnClick="lnkestimateid_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                    <asp:HiddenField ID="hdnCustomerColor" runat="server" Value='<%# Eval("CustomerColor")%>' />
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Cell Phone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblphone" runat="server" Text='<%#Eval("CellPh")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("CustomerAddress")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Best time to contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBesttimetocontact" runat="server" Text='<%#Eval("BestTimetocontact")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Meeting Status" >
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfmeetingstatus" runat="server" Value='<%#Eval("Status")%>' />
                                        <asp:DropDownList ID="ddlmeetingstatus" runat="server" CssClass="chg" OnSelectedIndexChanged="ddlmeetingstatus_selectedindexchanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>                                       
                                    </ItemTemplate>                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Follow Up Date">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdffollowupdate" OnDataBinding="hdffollowupdate_OnDataBinding" runat="server" />        <%--Value='<%#Eval("Followup_date")%>'--%>
                                
                                        <asp:TextBox ID="txtfollowup" Text='<%#Eval("Followup_date")%>' Visible="false" runat="server" AutoPostBack="true" OnTextChanged="txtfollowup_TextChanged"></asp:TextBox>
                                    </ItemTemplate>                              
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusername" runat="server" Text='<%#Eval("Username")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Save">
                                    <ItemTemplate>
                                        <asp:Button ID="btnsave" runat="server" Text="Save" Style="background-color: Gray;"
                                            OnClick="btnsave_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <div class="btn_sec">
               <%-- <asp:Button ID="btnsave" runat="server" Visible="false" Text="Save" OnClick="btnsave_Click" />--%>
                <asp:Button ID="Btnexcel" runat="server" OnClick="Btnexcel_Click" Visible="false" Text="Excel Export" />
            </div>
        </div>
    </div>
</asp:Content>
