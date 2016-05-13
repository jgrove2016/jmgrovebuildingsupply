<%@ Page Title="" Language="C#" MasterPageFile="~/Installer/InstallerMaster.Master"
    AutoEventWireup="true" CodeBehind="InstallerHome.aspx.cs" Inherits="JG_Prospect.Installer.InstallerHome" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../js/jquery.datetimepicker.css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.datetimepicker.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#txtPrimary').datetimepicker({
                format: 'm/d/Y h:00 a'
            });
            $('#txtSecondary1').datetimepicker({
                format: 'm/d/Y h:00 a'
            });
            $('#txtSecondary2').datetimepicker({
                format: 'm/d/Y h:00 a'
            });
        });
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            display: none;
        }

        .myfont {
            font-family: 'barcode_fontregular';
            font-size: 37px;
        }

        .black_overlay {
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

        .white_content {
            display: none;
            position: absolute;
            top: 10%;
            left: 20%;
            width: 60%;
            height: 50%;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }

        .grid td {
            padding: 1px !important;
            border-bottom: #ccc 1px solid;
        }

        #btnAddProdLines {
            width: 200px !important;
            padding: 0 10px !important;
        }

        #txtLine {
            width: 57px;
        }

        .text-style {
            height: 24px;
            width: 100%;
        }

        div.dd_chk_select {
            height: 24px !important;
        }
        fieldset{
            border:solid 1px;
            padding:5px;
        }
         .form_panel ul li table tr td, .form_panel table tr td {
            padding: 3px;

        }
    </style>
    <script type="text/javascript">
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Dashboard</h1>
    <div class="form_panel">
        <div class="clr">
        </div>
        <asp:LinkButton ID="lblSignOffDocument" Text="Sign Off Document" runat="server" Style="float: right;"
            OnClick="lblSignOffDocument_Click"></asp:LinkButton>
        <br />
        <br />
        <div class="grid">
            <asp:GridView ID="grdInstaller" runat="server" AutoGenerateColumns="false" CssClass="tableClass"
                Width="100%" EmptyDataText="No Record Found" AllowSorting="true"
                OnRowDataBound="grdInstaller_RowDataBound"
                OnSorting="grdInstaller_Sorting">
                <Columns>
                    <asp:TemplateField HeaderText="Ref #" HeaderStyle-Width="5%" SortExpression="ReferenceId">
                        <ItemTemplate>
                            <asp:Label ID="lblReferenceId" runat="server" Text='<%#Eval("ReferenceId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="10%" SortExpression="CustomerName">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ZipCode" HeaderStyle-Width="5%" SortExpression="ZipCode">
                        <ItemTemplate>
                            <asp:Label ID="lblZipCode" runat="server" Text='<%#Eval("ZipCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Date Sold" DataField="SoldDate" DataFormatString="{0:MM/dd/yyyy}" SortExpression="SoldDate"
                        HeaderStyle-Width="10%" />
                    <asp:TemplateField HeaderText="CustomerID & Job#" HeaderStyle-Width="10%" SortExpression="CustomerIdJobId">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerIdJobId" runat="server" Text='<%#Eval("CustomerIdJobId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Packet PDFs'" HeaderStyle-Width="15%" SortExpression="JobPacket">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkJobPackets" runat="server" Text='<%#Eval("JobPacket") %>'
                                OnClick="lnkJobPackets_Click"></asp:LinkButton>
                            <asp:HiddenField ID="hdnproductid" runat="server" Value='<%#Eval("ProductId") %>' />
                            <asp:HiddenField ID="hdnProductTypeId" runat="server" Value='<%#Eval("ProductTypeId") %>' />
                            <asp:HiddenField ID="hdnJobSequenceId" runat="server" Value='<%#Eval("JobSequenceId") %>' />
                            <asp:HiddenField ID="hdnColour" runat="server" Value='<%#Eval("Colour") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Availability" HeaderStyle-Width="15%" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAvailability" runat="server" Text="Availability" OnClick="lnkAvailability_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="10%" SortExpression="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-Width="15%" />
                    <asp:BoundField HeaderText="Notes" DataField="Notes" HeaderStyle-Width="30%" />
                </Columns>
            </asp:GridView>
            <button id="btnFake" style="display: none" runat="server">
            </button>
            <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" TargetControlID="btnFake"
                PopupControlID="pnlpopup" CancelControlID="btnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="550px"
                Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                    cellspacing="0">
                    <tr style="background-color: #A33E3F">
                        <td colspan="3" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                            align="center">Availability
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <strong>Primary</strong>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtPrimary" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblPrimary" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <strong>Secondary1</strong>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtSecondary1" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblSecondary1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <strong>Secondary2</strong>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtSecondary2" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblSecondary2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnSet" Style="width: 100px;" runat="server"
                                Text="Set Availability" OnClick="btnSet_Click" />
                            <asp:Button ID="btnCancel" runat="server" Style="width: 100px;" Text="Cancel" OnClientClick="overlay();" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>


        <asp:Panel ID="panelPopup" runat="server">
            <div id="light" class="white_content">
                <div class="container">
                    <h1>Job Packets</h1>
                    <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">Close</a>
                    <div class="form_panel">
                        <div class="scrollme">
                            <div style="float:right">
                                <fieldset style="border-style: solid; border-width: 1px; padding: 5px;">
                                    <legend>Add Installer</legend>
                                    <asp:DropDownList ID="ddlInstaller" runat="server">
                                    </asp:DropDownList>
                                    <span class="btn_sec">
                                        <asp:Button ID="btnAddInstaller" runat="server" OnClick="btnAddInstaller_Click" Text="Add Installer" /></span>
                                    <div>
                                        <asp:Repeater ID="rptInstaller" runat="server" OnItemDataBound="rptInstaller_ItemDataBound" OnItemCommand="rptInstaller_ItemCommand">
                                            <HeaderTemplate>
                                                <table class="grid">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Installer Name</th>
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString() %></td>
                                                    <td><%#Eval("QualifiedName") %></td>
                                                    <td>
                                                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDeleteInstaller" CommandArgument='<%#Eval("ID") %>' CommandName="DeleteInstaller" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>

                                                    </td>
                                                </tr>


                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>

                                        </asp:Repeater>
                                    </div>
                                </fieldset>

                            </div>
                            <div class="clr"></div>
                            <div class="btn_sec" style="float: right; padding-right: 10px; padding-top: 35px;">
                                <asp:Button ID="btnGo" Text="Zip & Download" OnClick="btnGo_Click" runat="server" />
                            </div>
                            <span id="ErrorMessage" style="color: Red" runat="server"></span>
                            <div class="clr"></div>
                            <div id="dvRequestedMaterial">
                                <fieldset>
                                    <legend>Installer Edit / Request Material</legend>
                                    <asp:UpdatePanel ID="updMaterialList" runat="server">
                                        <ContentTemplate>
                                            <div class="btn_sec">
                                                Select Product Category:
                                                <asp:DropDownList ID="ddlCategoryH" Width="150px" runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnAddProdLines" runat="server" Text="Add Product Category" OnClick="btnAddProdLines_Click" />

                                            </div>
                                            <asp:ListView ID="lstRequestMaterial" OnItemCommand="lstRequestMaterial_ItemCommand" runat="server" OnItemDataBound="lstRequestMaterial_ItemDataBound" ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder">
                                                <LayoutTemplate>
                                                    <div>
                                                        <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                                                    </div>
                                                </LayoutTemplate>
                                                <GroupTemplate>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>

                                                </GroupTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updMaterialList2" runat="server">
                                                        <ContentTemplate>
                                                            <h6 align="left">Product Category: 
                                                                <asp:DropDownList ID="ddlCategory" Width="150px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>

                                                                <asp:HiddenField ID="hdnProductCatIDReq" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                                                <asp:LinkButton ID="lnkAddProdCat" Visible="false" OnClick="lnkAddProdCat_Click" runat="server">Add</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDeleteProdCat" CommandArgument='<%#Eval("ProductCatID") %>' OnClick="lnkDeleteProdCat_Click" runat="server" OnClientClick="return confirm('Deleting product category will delete all associated line items. Are you sure you want to delete?')">Delete</asp:LinkButton>
                                                                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("ProductCatID") %>' onclick="btnDelete_Click" OnClientClick="return confirm('Deleting product category will delete all associated line items. Are you sure you want to delete?')" />--%>
                                                                <div style="clear: both"></div>
                                                            </h6>



                                                            <asp:GridView GridLines="Both" CellPadding="0" ID="grdProdLinesReq" Width="100%" runat="server"  AutoGenerateColumns="false" OnRowDataBound="grdProdLinesReq_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Line" HeaderStyle-Width="4%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox CssClass="text-style" ID="txtLine" Text='<%# Eval("Line") %>' Visible="false" MaxLength="4" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                                            <%# Eval("Line") %>
                                                                            <asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>' />
                                                                            <asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>' />
                                                                            <asp:HiddenField ID="hdnForemanPermission" runat="server" Value='<%#Eval("IsForemanPermission")%>' />
                                                                            <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>' />
                                                                            <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>' />
                                                                            <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>' />
                                                                            <asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="JG sku- vendor part #" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSkuPartNo" runat="server" Text='<%# Eval("JGSkuPartNo") %>'></asp:Label>
                                                                            <asp:UpdatePanel ID="updSku" runat="server" Visible="true">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtSkuPartNo" CssClass="text-style" Text='<%# Eval("JGSkuPartNo") %>' MaxLength="18" runat="server" ClientIDMode="Static" OnTextChanged="txtSkuPartNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtSkuPartNo" EventName="TextChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="25%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("MaterialList") %>'></asp:Label>
                                                                            <asp:UpdatePanel ID="updDesc" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtDescription" CssClass="text-style" Text='<%# Eval("MaterialList") %>' runat="server" ClientIDMode="Static" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtDescription" EventName="TextChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="updQty" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtQTY" runat="server" CssClass="text-style" MaxLength="4" ClientIDMode="Static" Text='<%# Eval("Quantity") %>' onkeypress="return isNumberKey(event)" OnTextChanged="txtQTY_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtQTY" EventName="TextChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="updUOM" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtUOM" runat="server" CssClass="text-style" ClientIDMode="Static" Text='<%# Eval("UOM") %>' OnTextChanged="txtUOM_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtUOM" EventName="TextChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cost" Visible="false"><%--Material Cost Per Item--%>
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="updMC" runat="server" Visible="false">
                                                                                <ContentTemplate>

                                                                                    <asp:TextBox ID="txtMaterialCost" CssClass="text-style" AutoPostBack="true" Text='<%# Eval("MaterialCost") %>' OnTextChanged="txtMaterialCost_TextChanged" runat="server" ClientIDMode="Static" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtMaterialCost" EventName="TextChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Extended" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="updExt" runat="server">
                                                                                <ContentTemplate>

                                                                                    <asp:TextBox ID="txtExtended" runat="server" CssClass="text-style" ClientIDMode="Static" Text='<%# Eval("Extend") %>' OnTextChanged="txtExtended_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtExtended" EventName="TextChanged" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Request Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReqStatus" runat="server" Text='<%#(Eval("RequestStatus").ToString() == "1" ? "Pending": (Eval("RequestStatus").ToString() == "3" ?"Rejected":"Accepted" )) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDeleteLineItems" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteLine" OnClick="lnkDeleteLineItems_Click">Delete</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:LinkButton ID="lnkAddLines" CommandName="AddLine" CommandArgument='<%#Eval("ProductCatId") %>' OnClick="lnkAddLines_Click1" runat="server">Add Line</asp:LinkButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkAddLines" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="lnkDeleteProdCat" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAddProdLines" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </fieldset>
                            </div>
                            <div id="dvCustomMaterial" style="display:none">
                                <asp:ListView ID="lstCustomMaterialList" runat="server" OnItemDataBound="lstCustomMaterialList_ItemDataBound" ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder">
                                    <LayoutTemplate>
                                        <div>
                                            <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </div>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>

                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updMaterialList2" runat="server">
                                            <ContentTemplate>
                                                <h5 align="left">Product Category: '<%#Eval("ProductName")%>' 
                                                    <asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                                </h5>

                                                <asp:GridView ID="grdProdLines" Width="100%" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Line - Image">
                                                            <ItemTemplate>
                                                                <%# Eval("Line") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JG sku- vendor part #">
                                                            <ItemTemplate>
                                                                <%# Eval("JGSkuPartNo") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <%# Eval("MaterialList") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <%# Eval("Quantity") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <%# Eval("UOM") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                            <br />
                            <div id="divmain" class="target">
                                <asp:GridView ID="Gridviewdocs" runat="server" AutoGenerateColumns="false" CssClass="grid"
                                    Width="100%" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
                                    OnRowDataBound="Gridviewdocs_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="Hiddenid" runat="server" Value='<%# Eval("srno")%>' />
                                                <a href='<%# Eval("DocumentName","../{0}") %>' class="preview">
                                                    <asp:Image ID="Image1" runat="server" Width="60px" CssClass="preview"
                                                        Height="90px" /></a>
                                                <asp:Label ID="labelfile" ForeColor="Black" runat="server" Text='<%# Eval("DocumentName") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="labeldesc" ForeColor="Black" runat="server" Text='<%# Eval("DocDescription") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select File to Archieve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkbox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:HiddenField runat="server" ID="hdnReferenceID" Value="" />
                            <asp:HiddenField runat="server" ID="hdnJobSeqID" Value="" />
                            <asp:LinkButton ID="lnkAvailJobPckt" runat="server" Text="Availability" OnClick="lnkAvailJobPckt_Click"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div id="fade" class="black_overlay">
        </div>




    </div>

</asp:Content>