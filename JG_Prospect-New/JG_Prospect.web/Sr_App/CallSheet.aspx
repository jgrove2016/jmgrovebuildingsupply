<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="CallSheet.aspx.cs" Inherits="JG_Prospect.Sr_App.CallSheet" %>

<%@ Register Src="~/Sr_App/LeftPanel.ascx" TagName="leftmenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker);
            bindPicker();
        });

        function bindPicker() {
            $("input[type=text][id*=ContentPlaceHolder1_grdcallsheet_txtfollowup]").datepicker();
        }

        $(function () {
            $(".chg").change(function () {
                if ($(this).find('option:selected').text() == "[est*]") {
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdcallsheet_txtfollowup']")._show();
                }
                else {
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdcallsheet_txtfollowup']")._hide();
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="GoogleCalendarView.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx" class="active">Call Sheet</a></li>
        </ul>
        <h1>
            <b>Dashboard</b></h1>
        <h2>
            Call Sheet</h2>
        <div class="form_panel">
            <div class="grid_h" id="divcallsheet" runat="server">
            </div>
            <div class="grid">
                <%--       <asp:UpdatePanel ID="Upgrdstaticdata" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <asp:GridView ID="grdcallsheet" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="tableClass"
                    OnRowDataBound="grdcallsheet_RowDataBound"  HeaderStyle-Wrap="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Customer ID" HeaderStyle-Width="5%"  HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkcustomerid" runat="server" Text='<%# Bind("id") %>' CommandArgument='<%# Bind("id") %>'
                                    OnClick="lnkcustomerid_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="5%"  HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnCustomerColor" runat="server" Value='<%# Eval("CustomerColor")%>' />
                                <asp:Label ID="lblCustomerNm" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Primary Contact" HeaderStyle-Width="10%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblprimarycontact" runat="server" Text='<%#Eval("PrimaryContact")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Address" HeaderStyle-Width="10%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerAddress" runat="server" Text='<%#Eval("CustomerAddress")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Best time to contact" HeaderStyle-Width="10%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblBesttimetocontact" runat="server" Text='<%#Eval("BestTimetocontact")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Meeting Status" HeaderStyle-Width="10%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfmeetingstatus" runat="server" Value='<%#Eval("MeetingStatus")%>' />
                                <asp:DropDownList ID="ddlmeetingstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmeetingstatus_selectedindexchanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Follow Up Date" HeaderStyle-Width="12%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdffollowupdate" OnDataBinding="hdffollowupdate_OnDataBinding"  runat="server"/>
                                <%--<asp:HiddenField ID="hdffollowupdate" runat="server" Value='<%#Eval("MeetingDate")%>' />--%>
                                <asp:TextBox ID="txtfollowup" Text='<%#Eval("MeetingDate")%>' Visible="false" runat="server" AutoPostBack="true" OnTextChanged="txtfollowup_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="5%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblusername" runat="server" Text='<%#Eval("Username")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Save" HeaderStyle-Width="6%" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Button ID="btnsave" runat="server" Text="Save" Style="background-color: Gray;"
                                    OnClick="btnsave_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <br />
            <div class="btn_sec">
                <asp:Button ID="btnexporttoexcel" runat="server" Visible="false" Text="Excel Export"
                    OnClick="btnexporttoexcel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
