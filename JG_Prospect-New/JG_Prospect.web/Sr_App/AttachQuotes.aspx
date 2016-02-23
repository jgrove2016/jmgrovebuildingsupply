<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="AttachQuotes.aspx.cs" Inherits="JG_Prospect.Sr_App.AttachQuotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    function displayText(input) {
        var filename = ($(input).val());
        var lastIndex = filename.lastIndexOf("\\");
        if (lastIndex >= 0) {
            filename = filename.substring(lastIndex + 1);
        }
        $('#<%= txtFileUpload.ClientID%>').val(filename);
            }
</script>
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- appointment tabs section start -->
    <ul class="appointment_tab">
        <li><a href="home.aspx">Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
    </ul>
    <h1>
        <b>Upload Vendor Quotes</b></h1>
    <div class="form_panel">
        <div class="right_panel">
            <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td>
                        <div class="grid_h">
                            <strong>List Of Documents Attached</strong>
                        </div>
                        <div class="grid">
                            <asp:GridView ID="grdAttachQuotes" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="tableClass" OnRowCommand="grdAttachQuotes_RowCommand" 
                                OnRowDeleting="grdAttachQuotes_RowDeleting" 
                                onrowdatabound="grdAttachQuotes_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select"  CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VendorCategoryNm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VendorName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Document">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocName") %>'
                                                CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                            <%--<asp:Label ID="lblFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"originalFileName") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:UpdatePanel ID="upAttachQuotes" runat="server">
                            <ContentTemplate>
                                <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 15%">
                                            Vendor Category
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpVendorCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpVendorCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 15%">
                                            Vendor Name
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpVendorName" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 15%">
                                            Upload Document
                                        </td>
                                        <td style="width: 25%">
                                            <asp:FileUpload ID="uploadvendorquotes" runat="server" onchange="displayText(this);"   style="display: none"/>
                                            <input id="btnFileUpload" type="button" value="Browse" runat="server" style="width: 70px" />
                                            <asp:TextBox ID="txtFileUpload" runat="server" Width="310px" Enabled="false"/>
                                            <%--CssClass="multi MultiFile-wrap" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblOR" Text="OR" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%">
                                            File Name
                                        </td>
                                        <td style="width: 90%">
                                            <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%">
                                            File Content
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFileContent" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
            </table>
            <div class="btn_sec">
                <asp:Button ID="btnSaveQuotes" CommandName="Delete" runat="server" Text="Save" OnClick="btnSaveQuotes_Click" />
                <asp:Button ID="btnResetQuotes" CommandName="Reset" runat="server" Text="Reset" 
                    onclick="btnResetQuotes_Click" />
                <asp:Button ID="btnCancelQuotes" runat="server" Text="Cancel" OnClick="btnCancelQuotes_Click" />
            </div>
        </div>
    </div>
    <%-- </div>--%>
</asp:Content>
