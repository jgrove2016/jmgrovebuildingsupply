<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" EnableEventValidation="false" AutoEventWireup="true"
    CodeBehind="StatusOverride.aspx.cs" Inherits="JG_Prospect.Sr_App.StatusOverride" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            display: none;
        }

        .myfont
        {
            font-family: 'barcode_fontregular';
            font-size: 37px;
        }

        .black_overlay
        {
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
            overflow-y: hidden;
        }

        .white_content
        {
            display: none;
            position: absolute;
            top: 10%;
            left: 33%;
            width: auto;
            height: auto;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
    <script type="text/javascript">
        function ClosePopupCancelationnorehash() {
            document.getElementById('Cancelationnorehashlite').style.display = 'none';
            document.getElementById('Cancelationnorehashfade').style.display = 'none';
        }

        function overlayCancelationnorehash() {
            document.getElementById('Cancelationnorehashlite').style.display = 'block';
            document.getElementById('Cancelationnorehashfade').style.display = 'block';
        }

        function overlayEstDate() {
            document.getElementById('estDatelite').style.display = 'block';
            document.getElementById('estDatefade').style.display = 'block';
        }

        function CloseEstDate() {
            document.getElementById('estDatelite').style.display = 'none';
            document.getElementById('estDatefade').style.display = 'none';
        }

        function overlaysoldDate() {
            document.getElementById('soldDatelite').style.display = 'block';
            document.getElementById('soldDatefade').style.display = 'block';
        }

        function ClosesoldDate() {
            document.getElementById('soldDatelite').style.display = 'none';
            document.getElementById('soldDatefade').style.display = 'none';
        }


        function ClosePopupStatusClosedNotSold() {
            document.getElementById('ChangeStatusClosedNotSoldLite').style.display = 'none';
            document.getElementById('ChangeStatusClosedNotSoldFade').style.display = 'none';
        }

        function overlayStatusClosedNotSold() {
            document.getElementById('ChangeStatusClosedNotSoldLite').style.display = 'block';
            document.getElementById('ChangeStatusClosedNotSoldFade').style.display = 'block';
        }

        function ClosePopupStatusJrSales() {
            document.getElementById('JrSalesLite').style.display = 'none';
            document.getElementById('JrSalesFade').style.display = 'none';
        }

        function overlayStatusJrSales() {
            document.getElementById('JrSalesLite').style.display = 'block';
            document.getElementById('JrSalesFade').style.display = 'block';
        }

        function ClosePopupStatus() {
            document.getElementById('divStatusLight').style.display = 'none';
            document.getElementById('divStatusFade').style.display = 'none';
        }

        function overlayStatus() {
            document.getElementById('divStatusLight').style.display = 'block';
            document.getElementById('divStatusFade').style.display = 'block';
        }
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }

        function ClosePopupJobs() {
            document.getElementById('lightJob').style.display = 'none';
            document.getElementById('fadeJobs').style.display = 'none';
        }

        function overlayJobs() {
            document.getElementById('lightJob').style.display = 'block';
            document.getElementById('fadeJobs').style.display = 'block';
        }

        function ClosePopupSetStatus() {
            document.getElementById('lightSetStatus').style.display = 'none';
            document.getElementById('fadeSetStatus').style.display = 'none';
        }

        function overlaySetStatus() {
            document.getElementById('lightSetStatus').style.display = 'block';
            document.getElementById('fadeSetStatus').style.display = 'block';
        }

        function ClosePopupCategory() {
            document.getElementById('divCategoryLight').style.display = 'none';
            document.getElementById('divCategoryFade').style.display = 'none';
        }

        function overlayCategory() {
            document.getElementById('divCategoryLight').style.display = 'block';
            document.getElementById('divCategoryFade').style.display = 'block';
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker);
            bindPicker();
        });

        $(function () {
            $(".chg").change(function () {
                var currentDate = new Date();
                var day = currentDate.getDate() + 7;
                var month = currentDate.getMonth() + 1;
                var year = currentDate.getFullYear();
                var date = month + "/" + day + "/" + year;
                if ($(this).find('option:selected').text() == "Follow up" || $(this).find('option:selected').text() == "est<$1000" || $(this).find('option:selected').text() == "est>$1000" || $(this).find('option:selected').text() == "PTW est" || $(this).find('option:selected').text() == "EST-one legger") {
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdoverridestatus_txtfollowup']")._show();
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdoverridestatus_txtfollowup']").val(date);
                }
                else {
                    $(this).parent().parent().find("[id^='ContentPlaceHolder1_grdoverridestatus_txtfollowup']")._hide();
                }
            });
        });

        function bindPicker() {
            $("input[type=text][id*=ContentPlaceHolder1_grdoverridestatus_txtfollowup]").datepicker();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- appointment tabs section start -->
    <ul class="appointment_tab">
        <li><a href="home.aspx">Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
    </ul>
    <!-- appointment tabs section end -->
    <h1>
        <b>Override Status</b>
    </h1>
    <div>
        <table style="padding-left: 177px; padding-top: 15px;">
            <tr style="height: 50px;">
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" Height="24px" Width="130px"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ValidationGroup="search" ID="reqSearch" runat="server" ControlToValidate="txtSearch" ForeColor="Red" ErrorMessage="Enter value to search" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSearchCustomer" runat="server" Height="27px">
                        <asp:ListItem Text="Search All" Selected="True" Value="Search All"></asp:ListItem>
                        <asp:ListItem Text="Customer Id" Value="CustomerId"></asp:ListItem>
                        <asp:ListItem Text="Customer Name" Value="CustomerName"></asp:ListItem>
                        <asp:ListItem Text="Address" Value="Address"></asp:ListItem>
                        <asp:ListItem Text="Zip Code" Value="ZipCode"></asp:ListItem>
                        <asp:ListItem Text="Phone No" Value="PhoneNo"></asp:ListItem>
                        <asp:ListItem Text="Quote Id" Value="QuoteId"></asp:ListItem>
                        <asp:ListItem Text="Job Id" Value="JobId"></asp:ListItem>
                        <asp:ListItem Text="Status" Value="Status"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblDurationFrom" runat="server" Text="Duration From : "></asp:Label><asp:TextBox ID="txtDurationFrom" runat="server" Height="24px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtFrom" TargetControlID="txtDurationFrom" runat="server"></ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="lblDurationto" runat="server" Text="Duration To : "></asp:Label><asp:TextBox ID="txtdurationto" runat="server" Height="24px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtTo" TargetControlID="txtdurationto" runat="server"></ajaxToolkit:CalendarExtender>
                </td>

            </tr>
            <tr style="height: 50px;">
                <td colspan="2" style="text-align: center">
                    <div class="btn_sec">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="search" OnClick="btnSearch_Click" />
                    </div>
                </td>
                <td colspan="2" style="text-align: center">
                    <div class="btn_sec">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <br />

    </div>
    <div>
        <br />
    </div>
    <div>

        <div>
        </div>
    </div>


    <div class="right_panel">
        <div class="grid_h" id="divstatusoverride" runat="server">
            <div class="grid">
                <asp:GridView ID="grdoverridestatus" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="tableClass"
                    OnRowDataBound="grdoverridestatus_RowDataBound" OnSorting="grdoverridestatus_Sorting" AllowSorting="true" HeaderStyle-Wrap="true" OnRowCommand="grdoverridestatus_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr Sales Rep" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesRep" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" SortExpression="id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkcustomerid" runat="server" Text='<%# Bind("id") %>' CommandArgument='<%# Bind("id") %>'
                                    OnClick="lnkcustomerid_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name" SortExpression="CustomerName" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerNm" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quote Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkQuoteId" runat="server" Text="Quotes" CommandArgument='<%#Eval("id") %>' CommandName="Quotes"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkJobId" runat="server" Text="Jobs" CommandArgument='<%#Eval("id") %>' CommandName="Jobs"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' Visible="false"></asp:Label>--%>
                                <asp:LinkButton ID="lnkLatestStatus" runat="server" Text='<%#Eval("Status")%>' CommandArgument='<%#Eval("id") %>' CommandName="Status"></asp:LinkButton>
                                <%--<asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" runat="server" DataValueField='<%#Eval("Status")%>'>
                                    <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                                    <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                                    <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                                    <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                                    <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                                    <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                                    <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                                    <asp:ListItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"></asp:ListItem>
                                    <asp:ListItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"></asp:ListItem>
                                    <asp:ListItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"></asp:ListItem>
                                    <asp:ListItem Text="Ordered(3)" Value="Ordered(3)"></asp:ListItem>
                                </asp:DropDownList>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone" SortExpression="CellPh" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("CellPh")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alt. Phone" SortExpression="AlternatePh" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblAltPhone" runat="server" Text='<%#Eval("AlternatePh")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address" SortExpression="CustomerAddress" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCAddress" runat="server" Text='<%#Eval("CustomerAddress")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City" SortExpression="City" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" SortExpression="State" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%#Eval("State")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ZipCode" SortExpression="ZipCode" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblbZipCode" runat="server" Text='<%#Eval("ZipCode")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Category" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkProductCategory" runat="server" Text="Product Category" CommandArgument='<%#Eval("id") %>' CommandName="ProductCategory"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <br />
        <br />
        <div class="btn_sec">
            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
        </div>

        <asp:Panel ID="panelPopup" runat="server">
            <div id="light" class="white_content">
                <h3>Qoutes</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">Close</a>
                <div class="grid_h" id="div1" runat="server">
                    <div class="grid">
                        <asp:GridView ID="grdQuotes" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="tableClass"
                            HeaderStyle-Wrap="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPopUpQuote" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductNamePopUpQuote" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quote Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuoteNoPopUpQuote" runat="server" Text='<%#Eval("QuoteNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateOnPopUpQuote" runat="server" Text='<%#Eval("CreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </asp:Panel>
        <div id="fade" class="black_overlay">
        </div>



        <asp:Panel ID="popupJobs" runat="server">
            <div id="lightJob" class="white_content">
                <h3>Jobs Status</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('lightJob').style.display='none';document.getElementById('fadeJobs').style.display='none'">Close</a>
                <div class="grid_h" id="div3" runat="server">
                    <div class="grid">
                        <asp:GridView ID="grdJobsId" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="tableClass"
                            HeaderStyle-Wrap="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPopUpJobs" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductNamePopUpJobs" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobNoPopUpJob" runat="server" Text='<%#Eval("SoldJobId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobStatusPopUpJob" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateOnPopUpQuote" runat="server" Text='<%#Eval("CreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div id="fadeJobs" class="black_overlay">
        </div>

        <asp:Panel ID="pnlStatuses" runat="server">
            <div id="divStatusLight" class="white_content">
                <h3>Status</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('divStatusLight').style.display='none';document.getElementById('divStatusFade').style.display='none'">Close</a>
                <div class="grid_h" id="div4" runat="server">
                    <div class="grid">
                        <asp:GridView ID="grdStatus" runat="server" OnRowDataBound="grdStatus_RowDataBound" Width="600px" AutoGenerateColumns="false" CssClass="tableClass"
                            HeaderStyle-Wrap="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPopUpStatus" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductNamePopUpStatus" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quote Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuoteNoPopUpStatus" runat="server" Text='<%#Eval("QuoteNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateOnPopUpStatus" runat="server" Text='<%#Eval("CreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" runat="server" DataValueField='<%#Eval("Status")%>'>
                                            <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                                            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                                            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                                            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                                            <asp:ListItem Text="Closed (not sold)" Value="Closed (not sold)"></asp:ListItem>
                                            <asp:ListItem Text="Jr sales follow up" Value="Jr sales follow up"></asp:ListItem>
                                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                                            <asp:ListItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"></asp:ListItem>
                                            <asp:ListItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"></asp:ListItem>
                                            <asp:ListItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"></asp:ListItem>
                                            <asp:ListItem Text="Ordered(3)" Value="Ordered(3)"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:LinkButton ID="lnkLatestStatus" runat="server" Text='<%#Eval("Status")%>' CommandArgument='<%#Eval("id") %>' CommandName="Status"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </asp:Panel>
        <div id="divStatusFade" class="black_overlay">
        </div>
        <asp:Panel ID="pnlStatusSet" runat="server">
            <div id="lightSetStatus" class="white_content">
                <h3>Status Set</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('lightSetStatus').style.display='none';document.getElementById('fadeSetStatus').style.display='none'">Close</a>
                <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEstDate" runat="server"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEstDate" ValidationGroup="Set" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Time
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEsttime" runat="server" TabIndex="105" Width="112px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnSaveEst" runat="server" Text="Save" ValidationGroup="Set" OnClick="btnSaveEst_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="fadeSetStatus" class="black_overlay">
        </div>

        <asp:Panel ID="Panel8" runat="server">
            <div id="divCategoryLight" class="white_content">
                <h3>Product Category</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('divCategoryLight').style.display='none';document.getElementById('divCategoryFade').style.display='none'">Close</a>
                <div class="grid_h" id="div5" runat="server">
                    <div class="grid">
                        <asp:GridView ID="gvProductCategory" runat="server" OnRowDataBound="grdStatus_RowDataBound" Width="600px" AutoGenerateColumns="false" CssClass="tableClass"
                            HeaderStyle-Wrap="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPopUpCat" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Category" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductNamePopUpCat" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobNoPopUpCat" runat="server" Text='<%#Eval("SoldJobId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobStatusPopUpCat" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateOnPopUpCat" runat="server" Text='<%#Eval("CreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div id="divCategoryFade" class="black_overlay">
        </div>

        <asp:Panel ID="Panel1" runat="server">
            <div id="ChangeStatusClosedNotSoldLite" class="white_content">
                <h3>Reason</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('ChangeStatusClosedNotSoldLite').style.display='none';document.getElementById('ChangeStatusClosedNotSoldFade').style.display='none'">Close</a>
                 <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter Reason for closing
                        </td>
                        <td>
                            <asp:TextBox ID="txtReason" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReason" ValidationGroup="Reason" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnClosedSubmit" runat="server" Text="Save" ValidationGroup="Reason" OnClick="btnClosedSubmit_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="ChangeStatusClosedNotSoldFade" class="black_overlay">
        </div>


        <asp:Panel ID="Panel3" runat="server">
            <div id="JrSalesLite" class="white_content">
                <h3>Follow Up Date</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('JrSalesLite').style.display='none';document.getElementById('JrSalesFade').style.display='none'">Close</a>
                 <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter follow up date
                        </td>
                        <td>
                            <asp:TextBox ID="txtJrSalesReason" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtJrSalesReason" runat="server"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJrSalesReason" ValidationGroup="JrSalesReason" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnJrFade" runat="server" Text="Save" OnClick="btnJrFade_Click" ValidationGroup="JrSalesReason" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="JrSalesFade" class="black_overlay">
        </div>

        <asp:Panel ID="Panel7" runat="server">
            <div id="soldDatelite" class="white_content">
                <h3>Sold Date</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('soldDatelite').style.display='none';document.getElementById('soldDatefade').style.display='none'">Close</a>
                 <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter Sold Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtSoldDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" TargetControlID="txtSoldDate" runat="server"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="soldDate" runat="server" ControlToValidate="txtSoldDate" ErrorMessage="Enter sold date"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnSoldDate" runat="server" Text="Save" OnClick="btnSoldDate_Click" ValidationGroup="soldDate" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="soldDatefade" class="black_overlay">
        </div>

        <asp:Panel ID="Panel5" runat="server">
            <div id="estDatelite" class="white_content">
                <h3>Estimation Date</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('estDatelite').style.display='none';document.getElementById('estDatefade').style.display='none'">Close</a>
                 <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter estimation date
                        </td>
                        <td>
                            <asp:TextBox ID="txtSetEstDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtSetEstDate" runat="server"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSetEstDate" ValidationGroup="SetEstDate" ErrorMessage="Enter Estimate Date"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnEstDateSet" runat="server" Text="Save" OnClick="btnEstDateSet_Click" ValidationGroup="SetEstDate" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="estDatefade" class="black_overlay">
        </div>


        <asp:Panel ID="Panel6" runat="server">
            <div id="Div9" class="white_content">
                <h3>Follow Up Date</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('JrSalesLite').style.display='none';document.getElementById('JrSalesFade').style.display='none'">Close</a>
                 <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter follow up date
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtJrSalesReason" runat="server"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtJrSalesReason" ValidationGroup="JrSalesReason" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="Button2" runat="server" Text="Save" OnClick="btnJrFade_Click" ValidationGroup="JrSalesReason" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="Div10" class="black_overlay">
        </div>

        <asp:Panel ID="Panel4" runat="server">
            <div id="Cancelationnorehashlite" class="white_content">
                <h3>Reason</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('Cancelationnorehashlite').style.display='none';document.getElementById('Cancelationnorehashfade').style.display='none'">Close</a>
                 <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter Reason
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelationnorehash" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCancelationnorehash" ValidationGroup="Cancelationnorehash" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnCancelationnorehash" ValidationGroup="Cancelationnorehash" runat="server" Text="Save" OnClick="btnCancelationnorehash_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="Cancelationnorehashfade" class="black_overlay">
        </div>


        <asp:Panel ID="Panel2" runat="server">
            <div id="div2" class="white_content">
                <h3>Status</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('divChangeQuoteStatusLite').style.display='none';document.getElementById('divChangeQuoteStatusFade').style.display='none'">Close</a>
                <div class="grid_h" id="div6" runat="server">
                    <div class="grid">
                        <asp:GridView ID="GridView2"  runat="server" Width="600px" AutoGenerateColumns="false" CssClass="tableClass"
                            HeaderStyle-Wrap="true">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdPopUpStatus" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductNamePopUpStatus" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quote Id" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuoteNoPopUpStatus" runat="server" Text='<%#Eval("QuoteNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateOnPopUpStatus" runat="server" Text='<%#Eval("CreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" runat="server" DataValueField='<%#Eval("Status")%>'>
                                            <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                                            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                                            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                                            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                                            <asp:ListItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"></asp:ListItem>
                                            <asp:ListItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"></asp:ListItem>
                                            <asp:ListItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"></asp:ListItem>
                                            <asp:ListItem Text="Ordered(3)" Value="Ordered(3)"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:LinkButton ID="lnkLatestStatus" runat="server" Text='<%#Eval("Status")%>' CommandArgument='<%#Eval("id") %>' CommandName="Status"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </asp:Panel>
        <div id="div7" class="black_overlay">
        </div>






    </div>
</asp:Content>
