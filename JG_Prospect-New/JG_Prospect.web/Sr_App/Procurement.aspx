<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Procurement.aspx.cs" Inherits="JG_Prospect.Sr_App.Procurement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
   <%-- <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
    </style>--%>
   <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/btn-minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/btn_maximize.png");
            $(this).closest("tr").next().remove();
        });
    </script>--%>
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- appointment tabs section start -->
    <ul class="appointment_tab">
        <li><a href="Procurement.aspx">Sold Jobs</a></li>
        <li><a href="#">Overhead Expences</a></li>
    </ul>
    <h1>
        <b>Procurement</b></h1>
    <div class="form_panel">
        <div class="right_panel">
            <!-- appointment tabs section end -->
            <div class="grid_h">
                <strong>Sold Jobs</strong></div>
            <div class="grid">
                <div>
                    <asp:GridView ID="grdsoldjobs" runat="server" AutoGenerateColumns="false" CssClass="tableClass" 
                        Width="100%" OnRowDataBound="grdsoldjobs_RowDataBound">
                        <Columns>
                           
                            <asp:BoundField HeaderText="Date Sold" DataField="SoldDate" DataFormatString="{0:d}"
                                HeaderStyle-Width="11%" />
                            <asp:TemplateField HeaderText="Customer ID" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkcustomerid" runat="server" Text='<%#Eval("CustomerId") %>'
                                        OnClick="lnkcustomerid_Click"></asp:LinkButton>
                                    <asp:HiddenField ID="hdnproductid" runat="server" Value='<%#Eval("ProductId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Customer's LastName" DataField="LastName" HeaderStyle-Width="10%" />
                            <asp:TemplateField HeaderText="Sold Jobs #" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lblsoldjobid" runat="server" Text='<%#Eval("SoldJob#") %>'></asp:Label>--%>
                                    <asp:LinkButton ID="lnksoldjobid" runat="server" Text='<%#Eval("SoldJob#") %>' OnClick="lnksoldjobid_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Quotes" HeaderStyle-Width="16%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkaddvendorquotes" runat="server" Text="Attach Quotes" OnClick="lnkaddvendorquotes_Click"
                                        HeaderStyle-Width="200px"></asp:LinkButton>
                                    <%-- <asp:GridView runat ="server" ID ="grdAttachQuotes" AutoGenerateColumns ="false" 
                                OnRowDataBound ="grdAttachQuotes_RowDataBound" OnRowCommand ="grdAttachQuotes_RowCommand" Width="150px" HeaderStyle-Wrap="true">
                                    <Columns >
                                        <asp:TemplateField  HeaderStyle-Width="75px" HeaderStyle-Wrap="true">
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkDelete" Text ="X" runat ="server" CommandName="removeFile" CommandArgument ='<%#Eval("TempName") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-Width="75px" HeaderStyle-Wrap="true">
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkQuote" runat ="server" Text='<%#Eval("DocName") %>' CommandName="viewFile" CommandArgument ='<%#Eval("TempName") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField ="DocName" HeaderText ="Quote Name" />--%>
                                    <%-- </Columns>
                                </asp:GridView>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Material List" HeaderStyle-Width="16%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkmateriallist" runat="server" Text='<%#Eval("MaterialList") %>'
                                        OnClick="lnkmateriallist_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status/Approval" HeaderStyle-Width="16%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnStatusId" runat="server" Value='<%#Eval("StatusId") %>' />
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    <asp:DropDownList ID="ddlstatus" runat="server" Visible="false" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlstatus_selectedindexchanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField HeaderText="Status/Approval" DataField="Status"  HeaderStyle-Width="16%"/>--%>
                            <asp:TemplateField HeaderText="ProductType">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductType" runat="server" Text='<%#Eval("ProductTypeId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
                <div class="btn_sec">
                    <asp:Button ID="btnAddcategory" runat="server" Text="Add Category" />
                    <asp:Button ID="btndeletecategory" runat="server" Text="Delete Category" />
                    <asp:Button ID="btnaddvendors" runat="server" Text="Add/Edit Vendor" OnClick="btnaddvendors_Click" />
                </div>
                <div>
                    <asp:ModalPopupExtender ID="mpevendorcatelog" runat="server" TargetControlID="btnAddcategory"
                        PopupControlID="pnlpopup" CancelControlID="btnCancel">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="550px"
                        Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                            cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger;
                                    width: 100%;" align="center">
                                    Add Vendor Category
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">
                                    Vendor Category Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtname" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredvendorcategoryname" runat="server" ControlToValidate="txtname"
                                        ValidationGroup="addvendorcategory" ErrorMessage="Enter Category Name." ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnUpdate" CommandName="Update" Style="width: 100px;" runat="server"
                                        Text="Save" OnClick="btnsave_Click" ValidationGroup="addvendorcategory" />
                                    <asp:Button ID="btnCancel" runat="server" Style="width: 100px;" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="Mpedeletecategory" runat="server" TargetControlID="btndeletecategory"
                        PopupControlID="pnlpopup2" CancelControlID="btnCancel2">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" Height="269px" Width="550px"
                        Style="display: none">
                        <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%"
                            cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                    align="center">
                                    Delete Vendor Category
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 45%">
                                    Select Vendor Category Name
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlvendercategoryname" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btndeletevender" CommandName="Delete" runat="server" Style="width: 100px;"
                                        Text="Delete" OnClick="btndelete_Click" />
                                    <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Style="width: 100px;" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <button id="btnquotes" style="display: none" runat="server">
                    </button>
                    <button id="btnmateriallist" style="display: none" runat="server">
                    </button>
                    <asp:ModalPopupExtender ID="ModalMateriallist" runat="server" PopupControlID="PanelMateriallist"
                        TargetControlID="btnmateriallist" CancelControlID="btncancelmateriallist">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="PanelMateriallist" runat="server" BackColor="White" Height="269px"
                        Width="550px" Style="display: none">
                        <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%"
                            cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                    align="center">
                                    Edit Material List
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 45%">
                                    Material List
                                </td>
                                <td>
                                    <asp:TextBox ID="txtmateriallist" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnsavematerials" CommandName="Save" runat="server" Style="width: 100px;"
                                        Text="Save Quotes" />
                                    <asp:Button ID="btncancelmateriallist" runat="server" Text="Cancel" Style="width: 100px;" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <br />
                <div class="grid_h">
                    <strong>Vendors List</strong>
                </div>
                <div class="grid">
                    <asp:UpdatePanel ID="upVendors" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdvendors" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="tableClass" OnRowDataBound="grdvendors_RowDataBound" HeaderStyle-Wrap="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Category" DataField="VendorCategoryNm" HeaderStyle-Width="14%"
                                        HeaderStyle-Wrap="true" />
                                    <asp:BoundField HeaderText="CategoryId" DataField="VendorCategpryId" />
                                    <%--<asp:BoundField HeaderText="Vendor" DataField="VendorName" />--%>
                                    <asp:TemplateField HeaderText="Vendor" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpVendorName" CssClass="drpVendorName" runat="server" Width="150px"
                                                OnSelectedIndexChanged="drpVendorName_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact #" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactNumber" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFax" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" HeaderStyle-Width="16%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                        <asp:BoundField HeaderText="Contact #" DataField="ContactNumber" />
                        <asp:BoundField HeaderText="Fax" DataField="Fax" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />--%>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
</asp:Content>
