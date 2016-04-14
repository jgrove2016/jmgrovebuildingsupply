<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="ProductEstimate.aspx.cs" Inherits="JG_Prospect.Sr_App.ProductEstimate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function selectAll(invoker) {

            var inputElements = document.getElementsByTagName('input');

            for (var i = 0; i < inputElements.length; i++) {

                var myElement = inputElements[i];

                if (myElement.type === "checkbox") {

                    myElement.checked = invoker.checked;
                }
            }
        }

        function ace_itemSelected(sender, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="GoogleCalendarView.aspx">Master Appointment</a></li>
           <%-- Originaly it Redirect to  MasterAppointment.aspx altered by Neeta and Redirects to GoogleCalendarView.aspx--%>
            <%--<li><a href="MasterAppointment.aspx">Master Appointment</a></li>--%>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <!-- appointment tabs section end -->
        <h1>
            Product Estimate
        </h1>
        <div class="form_panel_custom">
            <ul>
                <li style="width:47% !important;">
                    <table id="tblshutter" runat="server" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                &nbsp;<%--<asp:DropDownList ID="ddlCustomer" runat="server" Width="127px" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                </asp:DropDownList>--%><div class="scroll_left_panel">
                                    <br />
                                        <br />
                                        <br />
                                    <div class="filter_section" style="height:54px !important;">
                                        Customer: <asp:TextBox ID="txtProspectsearch" runat="server" OnTextChanged="txtProspectsearch_TextChanged" Width="60%" AutoPostBack="True"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="temp" runat="server" Visible="false"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ValidationGroup="Add"
                                    ErrorMessage="Please Enter Customer Name." ControlToValidate="txtProspectsearch" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                        <br />
                                        <%--<asp:DropDownList ID="ddlsearchtype" runat="server" style="width: 47% !important;margin-left: 60px !important;">
                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                            <asp:ListItem Text="Customer Name" Value="CustomerName"></asp:ListItem>
                                            <asp:ListItem Text="Customer Address" Value="CustomerAddress"></asp:ListItem>
                                            <asp:ListItem Text="Customer Phone" Value="CustomerPhone"></asp:ListItem>
                                            <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                                            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                                            <asp:ListItem Text="PTW est" Value="PTW est"></asp:ListItem>
                                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                                            <asp:ListItem Text="EST-one legger" Value="EST-one legger"></asp:ListItem>
                                            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                                            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                                            <asp:ListItem Text="cancelation-no rehash" Value="cancelation-no rehash"></asp:ListItem>
                                            <asp:ListItem Text="CustomerId" Value="CustomerId"></asp:ListItem>
                                            <asp:ListItem Text="ZipCode" Value="ZipCode"></asp:ListItem>
                                            <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        &nbsp;
                                        <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/search_btn.png"
                                            CssClass="search_btn" OnClick="ImageButton1_Click" ValidationGroup="Add" Height="28px" />--%>
                                    </div>
                                    <asp:Repeater ID="rptcutomerlist" runat="server" OnItemDataBound="rptcutomerlist_ItemDataBound">
                                        <ItemTemplate>
                                            <h3 class="menuheader">
                                                <asp:LinkButton ID="lnkprospect" runat="server" Text='<%# Eval("CustomerName")%>'
                                                    CommandArgument='<%# Eval("id")%>' OnClick="prospect_click"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnCustomerColor" runat="server" Value='<%# Eval("CustomerColor")%>' />
                                            </h3>
                                            <asp:HiddenField ID="hdncustomer" runat="server" Value='<%# Eval("Email")%>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <%--<asp:HiddenField ID="hdCustID" runat="server" />--%>
                                <%--<asp:TextBox ID="txtCustomer" CssClass="time" runat="server" TabIndex="1" OnTextChanged="txtCustomer_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>--%>
                                <%--<asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                    MinimumPrefixLength="0" ServiceMethod="GetCustomerlist" TargetControlID="txtCustomer"
                                    EnableCaching="False" OnClientItemSelected="ace_itemSelected">
                                </asp:AutoCompleteExtender>--%>
                                <asp:HiddenField ID="hdCustID" runat="server" />
                                <%--<asp:TextBox ID="txtCustomer" CssClass="time" runat="server" Enabled="false" TabIndex="1" onkeypress="return isAlphaKey(event);" onkeyup="javascript:Alpha(this)"></asp:TextBox>--%>
                                <%--<asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListElementID="auto_complete"
                                    UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetCustomerlist"
                                    TargetControlID="txtCustomer" EnableCaching="False">
                                </asp:AutoCompleteExtender>--%>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ValidationGroup="Add"
                                    ErrorMessage="Please Select Customer." ControlToValidate="ddlCustomer" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </li>
                <li style="width:47% !important;">
                    <asp:UpdatePanel ID="up" runat="server">
                        <ContentTemplate>
                            <table id="Table1" runat="server" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <label>
                                            Product Lines: <span>*</span></label>
                                        <asp:DropDownList ID="ddlproductlines" runat="server" Width="200px" AutoPostBack="true"
                                            TabIndex="2" OnSelectedIndexChanged="ddlproductlines_SelectedIndexChanged">
                                            <%-- <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Shutters" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Gutters" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Roofs" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Custom" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Gutter Guards" Value="5"></asp:ListItem>
                                     <asp:ListItem Text="Time And Material" Value="6"></asp:ListItem>
                                       <asp:ListItem Text="Soffit" Value="7"></asp:ListItem>
                                       <asp:ListItem Text="Fascia/Capping" Value="8"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add"
                                            ErrorMessage="Please Select Product Line." InitialValue="0" ControlToValidate="ddlproductlines"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                        </label>
                                        <asp:TextBox ID="txtOther" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlproductlines" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnAddlineitem" runat="server" Text="Add Line item" ValidationGroup="Add"
                    TabIndex="3" OnClick="btnAddlineitem_Click" />
            </div>
            <div class="grid">
                <h3>
                    Customer Line Items
                </h3>
                <asp:GridView ID="grdproductlines" runat="server" OnRowDataBound="grdproductlines_RowDataBound" Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Quote Number" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblQuoteNo" runat="server" Text='<%#Eval("QuoteNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HiddenField ID="HiddenFieldEstimate" Value='<%#Eval("Id")%>' runat="server" />
                                <asp:HiddenField ID="HidProductTypeId" Value='<%#Eval("ProductTypeId")%>' runat="server" />
                                <asp:HiddenField ID="HidCustomerId" Value='<%#Eval("CustomerId")%>' runat="server" />
                                <asp:HiddenField ID="HidProductTypeIdFrom" Value='<%#Eval("ProductTypeIdFrom")%>'
                                    runat="server" />
                                <asp:LinkButton ID="lnkestimateid" runat="server" Text='<%#Eval("ProductName")%>'
                                    OnClick="lnkestimateid_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location Picture" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnlocation" runat="server" Value='<%#Eval("LocationPicture")%>' />
                                <asp:Image ID="imglocation" runat="server" ImageUrl='<%#Eval("LocationPicture")%>'
                                    Height="100px" Width="100px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Create Contract" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" Text=" Create Contract" OnClick="selectAll(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxContract" Checked="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="btn_sec">
                <asp:Button ID="btncreatecontract" runat="server" Text="Create Contract" TabIndex="4"
                    OnClick="btncreatecontract_Click" />
            </div>
        </div>
    </div>
</asp:Content>
