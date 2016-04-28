<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="JG_Prospect.Sr_App.Inventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .inventroy {
        }

            .inventroy .left_inventroy {
                display: inline-block;
                width: 320px;
            }

            .inventroy .right_inventroy {
                display: inline-block;
                width: 100%;
                margin-left: -352px;
                padding-left: 372px;
                box-sizing: border-box;
                vertical-align: top;
                margin-top: 10px;
            }

            .inventroy .left_inventroy ul {
                margin: 0;
                padding: 0;
                list-style: none;
                margin-top: 10px;
            }

                .inventroy .left_inventroy ul li {
                    padding: 5px 0px;
                    background: #B3484A;
                    position: relative;
                    float: none;
                    width: 100%;
                    margin: 0 0 0 0;
                }

                    .inventroy .left_inventroy ul li a {
                        text-decoration: none;
                        color: #fff;
                        font-size: 13px;
                        padding: 5px;
                    }

                        .inventroy .left_inventroy ul li a span {
                        }

            .inventroy .left_inventroy .inventory_main {
                margin-left: 10px;
            }

                .inventroy .left_inventroy .inventory_main li {
                }

                    .inventroy .left_inventroy .inventory_main li a {
                    }

                        .inventroy .left_inventroy .inventory_main li a span {
                        }

                    .inventroy .left_inventroy .inventory_main li .inventory_cat {
                        visibility: hidden;
                        position: absolute;
                        width: 100%;
                    }

                        .inventroy .left_inventroy .inventory_main li .inventory_cat.active {
                            visibility: visible;
                            position: relative;
                            width: inherit;
                            overflow: hidden;
                        }

                        .inventroy .left_inventroy .inventory_main li .inventory_cat:before {
                            content: '>';
                            position: absolute;
                            right: 6px;
                            top: -29px;
                            font-size: 19px;
                            color: #fff;
                            visibility: visible;
                        }

                        .inventroy .left_inventroy .inventory_main li .inventory_cat.active:before {
                            content: '>';
                            transform: rotate(-270deg);
                        }

                        .inventroy .left_inventroy .inventory_main li .inventory_cat li {
                            background: #D26668;
                            padding-left: 10px;
                        }

                            .inventroy .left_inventroy .inventory_main li .inventory_cat li .inventory_subcat {
                                visibility: hidden;
                                position: absolute;
                                width: 100%;
                                margin-left: -10px;
                            }

                                .inventroy .left_inventroy .inventory_main li .inventory_cat li .inventory_subcat.active {
                                    visibility: visible;
                                    position: relative;
                                    width: inherit;
                                    overflow: hidden;
                                }

                                .inventroy .left_inventroy .inventory_main li .inventory_cat li .inventory_subcat:before {
                                    content: '>';
                                    position: absolute;
                                    right: 6px;
                                    top: -29px;
                                    font-size: 19px;
                                    color: #fff;
                                    visibility: visible;
                                }

                                .inventroy .left_inventroy .inventory_main li .inventory_cat li .inventory_subcat.active:before {
                                    content: '>';
                                    transform: rotate(-270deg);
                                }

                                .inventroy .left_inventroy .inventory_main li .inventory_cat li .inventory_subcat li {
                                    background: #D8898A;
                                    padding-left: 20px;
                                }

        .right_panel {
            margin-bottom: 20px;
        }

        .form_panel ul li {
            overflow: hidden !important;
        }

        .inventroy .left_inventroy .inventory_main > li > a span.buttons {
            float: right;
            margin-right: 0px;
        }

        .inventroy .left_inventroy .inventory_cat > li > a span.buttons {
            float: right;
            margin-right: 10px;
        }

        .inventroy .left_inventroy .inventory_subcat li a span.buttons {
            float: right;
            margin-right: 20px;
        }

        .inventroy .left_inventroy .inventory_main li a span.buttons i {
            margin-right: 10px;
        }

            .inventroy .left_inventroy .inventory_main li a span.buttons i:first-child {
            }

            .inventroy .left_inventroy .inventory_main li a span.buttons i:last-child {
            }

        .vendorFilter {
            position: fixed;
            z-index: 999;
            margin: 0px auto;
            top: 50%;
            margin-top: -135px;
        }

        .inventroy .right_inventroy .breadcrumb {
                font-size: 13px;
        }
        
            .inventroy .right_inventroy .breadcrumb .pName {
                display:none;
                    margin-left: 5px;
            }

            .inventroy .right_inventroy .breadcrumb .vName {
                display:none;
                    margin-left: 5px;
            }

            .inventroy .right_inventroy .breadcrumb .vSName {
                display:none;
                    margin-left: 5px;
            }

            .inventroy .right_inventroy .breadcrumb .text {
                    margin-left: 5px;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".inventroy .left_inventroy ul li a").click(function () {
                //$(this).parent("li").siblings().find("ul").hide();
                $(this).parent("li").find("ul:first").toggleClass('active');
            });

        });

        function productClick(btn, productId, productName) {
            $(btn).find("ul:first").toggleClass('active');
            $(".breadcrumb .vSName").hide();
            $(".breadcrumb .vName").hide();
            $(".breadcrumb .pName").show();
            $(".breadcrumb .pName .text").html(productName);
        }
        function vendorClick(btn, productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            $(btn).find("ul:first").toggleClass('active');
            $(".breadcrumb .vSName").hide();
            $(".breadcrumb .vName").show();
            $(".breadcrumb .pName .text").html(productName);
            $(".breadcrumb .vName .text").html(vName);
        }
        function vendorSubClick(btn, vsId, vsName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            $(".breadcrumb .vSName").show();
            var productName = $(btn).closest("ul.inventory_cat").siblings("a").find("span.text").text();
            $(".breadcrumb .pName .text").html(productName);
            $(".breadcrumb .vName .text").html(vName);
            $(".breadcrumb .vSName .text").html(vsName);
        }

        function ResetAllValue() {

            $("#<%=hdnProductID.ClientID%>").val("0");
            $("#<%=txtProudctName.ClientID%>").val("");

            $("#<%=hdnVendorID.ClientID%>").val("0");
            $("#<%=txtVendorCateogryName.ClientID%>").val("");

            $("#<%=hdnVendorCatID.ClientID%>").val("0");
            $("#<%=txtVendorCatName.ClientID%>").val("");


            $("#<%=hdnSubCategoryId.ClientID%>").val("0");
            $("#<%=txtVendorSubCatEdit.ClientID%>").val("");

            $("#<%=chkVendorCRetail_WholesaleEdit.ClientID%>").removeAttr("checked");
            $("#<%=chkVendorCManufacturerEdit.ClientID%>").removeAttr("checked");

            $("#<%=chkVSCRetail_WholesaleEdit.ClientID%>").removeAttr("checked");
            $("#<%=chkVSCManufacturerEdit.ClientID%>").removeAttr("checked");


            $("#<%=btnAddVendorCat.ClientID%>").hide();
            $("#<%=btnNewVendorSubCat.ClientID%>").hide();
            $("#<%=btnUpdateVendorCat.ClientID%>").hide();
            $("#<%=btnDeleteVendorCat.ClientID%>").hide();
            $("#<%=btnUdpateVendorSubCat.ClientID%>").hide();
            $("#<%=btnDeleteVendorSubCat.ClientID%>").hide();

            $("#addVendorCat").hide();
            $("#updateVendorCat").hide();
            $("#deleteVendorCat").hide();
            $("#addVendorSubCat").hide();
            $("#updateVendorSubCat").hide();
            $("#deleteVendorSubCat").hide();
        }

        function AddVenodrCat(productId, productName) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);
            $("#<%=txtProudctName.ClientID%>").val(productName);

            $("#addVendorCat").show();
            $("#<%=btnAddVendorCat.ClientID%>").show();
            $("#<%=pnlVendorCat.ClientID%>").show();
        }

        function AddSubCat(vId, vName) {
            ResetAllValue();
            $("#<%=hdnVendorCatID.ClientID%>").val(vId);
            $("#<%=txtVendorCatName.ClientID%>").val(vName);

            $("#addVendorSubCat").show();
            $("#<%=btnNewVendorSubCat.ClientID%>").show();
            $("#<%=pnlVendorSubCat.ClientID%>").show();
        }
        function EditVendorCat(productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);
            $("#<%=txtProudctName.ClientID%>").val(productName);
            $("#<%=hdnVendorID.ClientID%>").val(vId);
            $("#<%=txtVendorCateogryName.ClientID%>").val(vName);

            if (IsRetail_Wholesale == "True") {
                $("#<%=chkVendorCRetail_WholesaleEdit.ClientID%>").attr("checked", true);
            }
            if (IsManufacturer == "True") {
                $("#<%=chkVendorCManufacturerEdit.ClientID%>").attr("checked", true);
            }

            $("#updateVendorCat").show();
            $("#<%=btnUpdateVendorCat.ClientID%>").show();
            $("#<%=pnlVendorCat.ClientID%>").show();
        }
        function DeleteVendorCat(productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);
            $("#<%=txtProudctName.ClientID%>").val(productName);
            $("#<%=hdnVendorID.ClientID%>").val(vId);
            $("#<%=txtVendorCateogryName.ClientID%>").val(vName);

            if (IsRetail_Wholesale == "True") {
                $("#<%=chkVendorCRetail_WholesaleEdit.ClientID%>").attr("checked", true);
            }
            if (IsManufacturer == "True") {
                $("#<%=chkVendorCManufacturerEdit.ClientID%>").attr("checked", true);
             }

             $("#deleteVendorCat").show();
             $("#<%=btnDeleteVendorCat.ClientID%>").show();
            $("#<%=pnlVendorCat.ClientID%>").show();
        }

        function EditSubCat(id, name, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            ResetAllValue();
            $("#<%=hdnSubCategoryId.ClientID%>").val(id);
            $("#<%=txtVendorSubCatEdit.ClientID%>").val(name);

            $("#<%=hdnVendorCatID.ClientID%>").val(vId);
            $("#<%=txtVendorCatName.ClientID%>").val(vName);

            if (IsRetail_Wholesale == "True") {
                $("#<%=chkVSCRetail_WholesaleEdit.ClientID%>").attr("checked", true);
            }
            if (IsManufacturer == "True") {
                $("#<%=chkVSCManufacturerEdit.ClientID%>").attr("checked", true);
            }

            $("#updateVendorSubCat").show();
            $("#<%=btnUdpateVendorSubCat.ClientID%>").show();
            $("#<%=pnlVendorSubCat.ClientID%>").show();
        }

        function DeleteSubCat(id, name, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            ResetAllValue();
            $("#<%=hdnSubCategoryId.ClientID%>").val(id);
            $("#<%=txtVendorSubCatEdit.ClientID%>").val(name);

            $("#<%=hdnVendorCatID.ClientID%>").val(vId);
            $("#<%=txtVendorCatName.ClientID%>").val(vName);

            if (IsRetail_Wholesale == "True") {
                $("#<%=chkVSCRetail_WholesaleEdit.ClientID%>").attr("checked", true);
            }
            if (IsManufacturer == "True") {
                $("#<%=chkVSCManufacturerEdit.ClientID%>").attr("checked", true);
            }
            $("#deleteVendorSubCat").show();
            $("#<%=btnDeleteVendorSubCat.ClientID%>").show();
            $("#<%=pnlVendorSubCat.ClientID%>").show();
        }

        function closePupup() {
            $(".vendorFilter").hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="Price_control.aspx">Product Line Estimate</a></li>
            <li><a href="Inventory.aspx">Inventory</a></li>
            <li><a href="Maintenace.aspx">Maintainance</a></li>
        </ul>
        <!-- appointment tabs section end -->
        <h1>Inventory</h1>
        <div class="form_panel">

            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoRetailWholesale" runat="server" Checked="true" Text="Retail/Wholesale" GroupName="MT" OnCheckedChanged="rdoRetailWholesale_CheckedChanged" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoManufacturer" runat="server" Text="Manufacturer" GroupName="MT" OnCheckedChanged="rdoManufacturer_CheckedChanged" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="updtpnlfilter" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlVendorCat" runat="server" CssClass="vendorFilter" BackColor="White" Height="269px" Width="550px" Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;" align="center">
                                    <div id="addVendorCat" style="display: none">Add Vendor Category</div>
                                    <div id="updateVendorCat" style="display: none">Update Vendor Category</div>
                                    <div id="deleteVendorCat" style="display: none;">Delete Vendor Category</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Product Category Name	</td>
                                <td>
                                    <asp:HiddenField ID="hdnProductID" runat="server" />
                                    <asp:TextBox ID="txtProudctName" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Vendor Category Name </td>
                                <td>
                                    <asp:HiddenField ID="hdnVendorID" runat="server" />
                                    <asp:TextBox ID="txtVendorCateogryName" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVendorCateogryName"
                                        ValidationGroup="Updatevendorcat" ErrorMessage="Enter Vendor Category Name." ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Manufacturer Type
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkVendorCRetail_WholesaleEdit" runat="server" Text="Retail/Wholesale" />
                                    <asp:CheckBox ID="chkVendorCManufacturerEdit" runat="server" Text="Manufacturer" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnAddVendorCat" Style="width: 100px; display: none;" runat="server"
                                        OnClick="btnAddVendorCat_Click" Text="Save" ValidationGroup="Updatevendorcat" />
                                    <asp:Button ID="btnUpdateVendorCat" Style="width: 100px; display: none;" runat="server"
                                        OnClick="btnUpdateVendorCat_Click" Text="Save" ValidationGroup="Updatevendorcat" />
                                    <asp:Button ID="btnDeleteVendorCat" Style="width: 100px; display: none;" runat="server"
                                        OnClick="btnDeleteVendorCat_Click" Text="Delete" />
                                    <input type="button" onclick="closePupup()" style="width: 100px;" value="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlVendorSubCat" runat="server" CssClass="vendorFilter" BackColor="White" Height="269px" Width="550px" Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0" cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;" align="center">
                                    <div id="addVendorSubCat" style="display: none">Add Vendor Sub Category</div>
                                    <div id="updateVendorSubCat" style="display: none">Update Vendor Sub Category</div>
                                    <div id="deleteVendorSubCat" style="display: none;">Delete Vendor Sub Category</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Vendor Category Name </td>
                                <td>
                                    <asp:HiddenField ID="hdnVendorCatID" runat="server" />
                                    <asp:TextBox ID="txtVendorCatName" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Vendor Sub Category Name </td>
                                <td>
                                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
                                    <asp:TextBox ID="txtVendorSubCatEdit" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtVendorSubCatEdit" runat="server" ControlToValidate="txtVendorSubCatEdit"
                                        ValidationGroup="Updatevendorsubcat" ErrorMessage="Enter Vendor Sub Category Name." ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Manufacturer Type
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkVSCRetail_WholesaleEdit" runat="server" Text="Retail/Wholesale" />
                                    <asp:CheckBox ID="chkVSCManufacturerEdit" runat="server" Text="Manufacturer" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnNewVendorSubCat" Style="width: 100px; display: none;" runat="server"
                                        OnClick="btnNewVendorSubCat_Click" Text="Save" ValidationGroup="Updatevendorsubcat" />
                                    <asp:Button ID="btnUdpateVendorSubCat" Style="width: 100px; display: none;" runat="server"
                                        OnClick="btnUdpateVendorSubCat_Click" Text="Save" ValidationGroup="Updatevendorsubcat" />
                                    <asp:Button ID="btnDeleteVendorSubCat" Style="width: 100px; display: none;" runat="server"
                                        OnClick="btnDeleteVendorSubCat_Click" Text="Delete" />
                                    <input type="button" onclick="closePupup()" style="width: 100px;" value="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="clearfix inventroy">
                <div class="left_inventroy">
                    <asp:Literal ID="ltrInventoryCategoryList" runat="server"></asp:Literal>
                </div>
                <div class="right_inventroy">
                    <div class="breadcrumb"><span class="text">JG Inventory</span><span class="pName">>><span class="text"></span></span><span class="vName">>><span class="text"></span></span><span class="vSName">>><span class="text"></span></span></div>
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            
        </div>
    </div>
</asp:Content>
