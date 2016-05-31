<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="JG_Prospect.Sr_App.Inventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .inventroy {
        }

        .invetory_heading {
            margin-left: 10px;
            font-size: 20px;
            color: #fff;
            background: #B94C4E;
            padding: 10px;
            border-radius: 5px 5px 0px 0px;
            margin-bottom: -9px;
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
                display: none;
                margin-left: 5px;
            }

            .inventroy .right_inventroy .breadcrumb .vName {
                display: none;
                margin-left: 5px;
            }

            .inventroy .right_inventroy .breadcrumb .vSName {
                display: none;
                margin-left: 5px;
            }

            .inventroy .right_inventroy .breadcrumb .text {
                margin-left: 5px;
            }
    </style>
    <script type="text/javascript">
        function bindClickEvent() {
            $(".inventroy .left_inventroy ul li a").click(function () {
                //$(this).parent("li").siblings().find("ul").hide();
                $(this).parent("li").find("ul:first").toggleClass('active');
            });
        }

        function productClick(btn, productId, productName) {
            //$(btn).find("ul:first").toggleClass('active');
            $(".breadcrumb .vSName").hide();
            $(".breadcrumb .vName").hide();
            $(".breadcrumb .pName").show();
            $(".breadcrumb .pName .text").html(productName);
        }
        function vendorClick(btn, productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            //$(btn).find("ul:first").toggleClass('active');
            $(".breadcrumb .vSName").hide();
            $(".breadcrumb .pName").show();
            $(".breadcrumb .vName").show();
            $(".breadcrumb .pName .text").html(productName);
            $(".breadcrumb .vName .text").html(vName);
        }
        function vendorSubClick(btn, vsId, vsName, vId, vName, IsRetail_Wholesale, IsManufacturer, productId, productName) {
            $(".breadcrumb .pName").show();
            $(".breadcrumb .vName").show();
            $(".breadcrumb .vSName").show();
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

        function AddVenodrCat(btn, productId, productName) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);
            $("#<%=txtProudctName.ClientID%>").val(productName);

            $("#addVendorCat").show();
            $("#<%=btnAddVendorCat.ClientID%>").show();
            $("#<%=pnlVendorCat.ClientID%>").show();
        }

        function AddSubCat(btn, productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);
            $("#<%=hdnVendorCatID.ClientID%>").val(vId);
            $("#<%=txtVendorCatName.ClientID%>").val(vName);

            $("#addVendorSubCat").show();
            $("#<%=btnNewVendorSubCat.ClientID%>").show();
            $("#<%=pnlVendorSubCat.ClientID%>").show();
        }
        function EditVendorCat(btn, productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
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
        function DeleteVendorCat(btn, productId, productName, vId, vName, IsRetail_Wholesale, IsManufacturer) {
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

        function EditSubCat(btn, vsId, vsName, vId, vName, IsRetail_Wholesale, IsManufacturer, productId, productName) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);

            $("#<%=hdnSubCategoryId.ClientID%>").val(vsId);
            $("#<%=txtVendorSubCatEdit.ClientID%>").val(vsName);

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

        function DeleteSubCat(btn, vsId, vsName, vId, vName, IsRetail_Wholesale, IsManufacturer, productId, productName) {
            ResetAllValue();
            $("#<%=hdnProductID.ClientID%>").val(productId);

            $("#<%=hdnSubCategoryId.ClientID%>").val(vsId);
            $("#<%=txtVendorSubCatEdit.ClientID%>").val(vsName);

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
    <style type="text/css">
        #tabs.ui-tabs {
            background: transparent;
        }

            #tabs.ui-tabs .ui-tabs-nav {
                height: auto;
                margin-left: 0;
            }

        .ui-tabs .ui-tabs-nav li {
            width: 20%;
        }

        #tabs.ui-tabs .ui-tabs-nav li.ui-tabs-selected {
            background: #ffffff;
        }

        .ui-tabs.ui-widget-content {
            border: 1px solid #aaaaaa !important;
        }

        .ui-tabs .ui-tabs-panel {
            padding: 10px 0px !important;
        }

        .uiblack {
            display: block;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.55);
            z-index: 99;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
            overflow-y: hidden;
        }
    </style>
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
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">JG Construction</a></li>
                <li><a href="#tabs-2">JG Supply</a></li>
                <li><a href="#tabs-3">JG Realestate</a></li>
            </ul>
            <div id="tabs-1">
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
                                    <div class="invetory_heading">Product Category</div>
                                    <asp:Literal ID="ltrInventoryCategoryList" runat="server"></asp:Literal>
                                </div>
                                <div class="right_inventroy">
                                    <div class="breadcrumb">
                                        <span class="pName">>><span class="text"></span></span><span class="vName">>><span class="text"></span></span><span class="vSName">>><span class="text"></span></span>
                                    </div>
                                    <div>
                                        <table border="1">
                                            <tr>
                                                <th>JG sku</th>
                                                <th>Total Cost:$</th>
                                                <th>UOM</th>
                                                <th>$Unit</th>
                                                <th>Cost Description</th>
                                                <th>Vendor Part</th>
                                                <th>Model#</th>
                                                <th>Image</th>
                                            </tr>
                                            <tr>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                                <td>Test</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </div>
            <div id="tabs-2">
                <div class="clearfix inventroy">
                    <div class="left_inventroy">
                        <div class="invetory_heading">Shop By Department</div>
                        <ul class="clearfix inventory_main">
                            <li><a>Appliances</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Appliance Parts & Accessories</a></li>
                                    <li><a href="javascript:;">Beverage Centers & Wine Chillers</a></li>
                                    <li><a href="javascript:;">Cooktops</a></li>
                                    <li><a href="javascript:;">Dishwashers</a></li>
                                    <li><a href="javascript:;">Freezers & Ice Makers</a></li>
                                    <li><a href="javascript:;">Garbage Disposals</a></li>
                                    <li><a href="javascript:;">Microwaves</a></li>
                                    <li><a href="javascript:;">Range Hoods</a></li>
                                    <li><a href="javascript:;">Ranges</a></li>
                                    <li><a href="javascript:;">Refrigerators</a></li>
                                    <li><a href="javascript:;">Small Appliances</a></li>
                                    <li><a href="javascript:;">Trash Compactors</a></li>
                                    <li><a href="javascript:;">Vacuum Cleaners & Floor Care</a></li>
                                    <li><a href="javascript:;">Wall Ovens</a></li>
                                    <li><a href="javascript:;">Washers & Dryers</a></li>
                                    <li><a href="javascript:;">Water Coolers</a></li>
                                </ul>
                            </li>
                            <li><a>Addtions & Sunrooms</a></li>
                            <li><a>Building Material & Lumber</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Appearance Boards</a></li>
                                    <li><a href="javascript:;">Asphalt, Concrete & Masonry</a></li>
                                    <li><a href="javascript:;">Ceilings</a></li>
                                    <li><a href="javascript:;">Columns & Accessories</a></li>
                                    <li><a href="javascript:;">Dimensional Lumber</a></li>
                                    <li><a href="javascript:;">Drywall</a></li>
                                    <li><a href="javascript:;">Erosion Control </a></li>
                                    <li><a href="javascript:;">Fencing & Gates</a></li>
                                    <li><a href="javascript:;">Furring Strips</a></li>
                                    <li><a href="javascript:;">Glass & Acrylic</a></li>
                                    <li><a href="javascript:;">Insulation & Accessories</a></li>
                                    <li><a href="javascript:;">Jack Posts</a></li>
                                    <li><a href="javascript:;">Lath</a></li>
                                    <li><a href="javascript:;">Lattice</a></li>
                                    <li><a href="javascript:;">MDF</a></li>
                                    <li><a href="javascript:;">OSB</a></li>
                                    <li><a href="javascript:;">Particle Board</a></li>
                                    <li><a href="javascript:;">Permit Boxes</a></li>
                                    <li><a href="javascript:;">Plywood</a></li>
                                    <li><a href="javascript:;">Shims</a></li>
                                    <li><a href="javascript:;">Table Parts</a></li>
                                    <li><a href="javascript:;">Ventilation</a></li>
                                    <li><a href="javascript:;">Wood Veneer </a></li>
                                </ul>
                            </li>
                            <li><a>Electrical & Lighting </a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Batteries</a></li>
                                    <li><a href="javascript:;">Cable & Wire Connectors</a></li>
                                    <li><a href="javascript:;">Circuit Breakers, Load Centers & Fuses</a></li>
                                    <li><a href="javascript:;">Conduit & Conduit Fittings</a></li>
                                    <li><a href="javascript:;">Cords & Surge Protectors</a></li>
                                    <li><a href="javascript:;">Doorbells</a></li>
                                    <li><a href="javascript:;">Electrical Boxes & Covers</a></li>
                                    <li><a href="javascript:;">Electrical Outlets & Adapters</a></li>
                                    <li><a href="javascript:;">Electrical Testers & Tools</a></li>
                                    <li><a href="javascript:;">Electrical Wire & Cable</a></li>
                                    <li><a href="javascript:;">Generators</a></li>
                                    <li><a href="javascript:;">Home Audio & Video</a></li>
                                    <li><a href="javascript:;">Home Automation & Security</a></li>
                                    <li><a href="javascript:;">Light Switches & Dimmers</a></li>
                                    <li><a href="javascript:;">Smoke, Carbon Monoxide & Radon Detectors</a></li>
                                    <li><a href="javascript:;">Solar Power</a></li>
                                    <li><a href="javascript:;">Wall Plates</a></li>
                                </ul>
                            </li>
                            <li><a>Flooring</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Attic Flooring Panels </a></li>
                                    <li><a href="javascript:;">Carpet & Carpet Tile</a></li>
                                    <li><a href="javascript:;">Flooring Underlayment</a></li>
                                    <li><a href="javascript:;">Garage Flooring</a></li>
                                    <li><a href="javascript:;">Grout & Mortar</a></li>
                                    <li><a href="javascript:;">Hardwood Flooring & Accessories</a></li>
                                    <li><a href="javascript:;">Laminate Flooring & Accessories</a></li>
                                    <li><a href="javascript:;">Multipurpose Flooring</a></li>
                                    <li><a href="javascript:;">Surface Preparation</a></li>
                                    <li><a href="javascript:;">Tile & Stone</a></li>
                                    <li><a href="javascript:;">Underfloor Heating</a></li>
                                    <li><a href="javascript:;">Vinyl Flooring</a></li>
                                    <li><a href="javascript:;">Wall Base</a></li>
                                </ul>
                            </li>
                            <li><a>Heating & Coooling</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Air Conditioners & Fans</a></li>
                                    <li><a href="javascript:;">Air Filters</a></li>
                                    <li><a href="javascript:;">Air Purifiers & Accessories</a></li>
                                    <li><a href="javascript:;">Baseboard Heaters & Accessories </a></li>
                                    <li><a href="javascript:;">Electric Wall Heaters & Accessories</a></li>
                                    <li><a href="javascript:;">Fireplaces & Stove</a></li>
                                    <li><a href="javascript:;">Fireplaces & Stoves</a></li>
                                    <li><a href="javascript:;">Furnaces & Furnace Accessories</a></li>
                                    <li><a href="javascript:;">Garage Heaters</a></li>
                                    <li><a href="javascript:;">Heat Pumps</a></li>
                                    <li><a href="javascript:;">Humidifiers & Dehumidifiers</a></li>
                                    <li><a href="javascript:;">HVAC Duct & Fittings</a></li>
                                    <li><a href="javascript:;">Registers & Grilles</a></li>
                                    <li><a href="javascript:;">Space & Kerosene Heaters</a></li>
                                    <li><a href="javascript:;">Thermostats</a></li>

                                </ul>
                            </li>
                            <li><a>Kitchens & Bathrooms</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Backsplashes & Wall Tile</a></li>
                                    <li><a href="javascript:;">Bathroom & Pedestal Sinks </a></li>
                                    <li><a href="javascript:;">Bathroom Accessories & Hardware </a></li>
                                    <li><a href="javascript:;">Bathroom Fans & Parts</a></li>
                                    <li><a href="javascript:;">Bathroom Mirrors</a></li>
                                    <li><a href="javascript:;">Bathroom Safety</a></li>
                                    <li><a href="javascript:;">Bathroom Storage</a></li>
                                    <li><a href="javascript:;">Bathroom Vanities & Vanity Tops</a></li>
                                    <li><a href="javascript:;">Bathtubs & Whirlpool Tubs</a></li>
                                    <li><a href="javascript:;">Commercial Bathroom Components </a></li>
                                    <li><a href="javascript:;">Cookware </a></li>
                                    <li><a href="javascript:;">Kitchen & Bar Faucets </a></li>
                                    <li><a href="javascript:;">Kitchen & Bar Sinks</a></li>
                                    <li><a href="javascript:;">Kitchen Cabinets </a></li>
                                    <li><a href="javascript:;">Kitchen Countertops</a></li>
                                    <li><a href="javascript:;">Kitchen Sink Accessories</a></li>
                                    <li><a href="javascript:;">Kitchen Sinks</a></li>
                                    <li><a href="javascript:;">Saunas & Components </a></li>
                                    <li><a href="javascript:;">Showers & Shower Accessories</a></li>
                                    <li><a href="javascript:;">Toilets & Toilet Seats</a></li>

                                </ul>
                            </li>
                            <li><a>Landscaping & Hardscaping</a>
                            </li>
                            <li><a>Paint & Paint Supplies</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Buckets & Bucket Lids </a></li>
                                    <li><a href="javascript:;">Caulking </a></li>
                                    <li><a href="javascript:;">Cleaners & Chemicals </a></li>
                                    <li><a href="javascript:;">Drop Cloths & Sheeting  </a></li>
                                    <li><a href="javascript:;">Garage Floor Epoxy  </a></li>
                                    <li><a href="javascript:;">Paint & Primer  </a></li>
                                    <li><a href="javascript:;">Paint Brushes, Rollers & Kits </a></li>
                                    <li><a href="javascript:;">Paint Sprayers </a></li>
                                    <li><a href="javascript:;">Paint Thinners & Removers </a></li>
                                    <li><a href="javascript:;">Patching & Repair </a></li>
                                    <li><a href="javascript:;">Plastic Dip Coatings </a></li>
                                    <li><a href="javascript:;">Resurfacing Kits </a></li>
                                    <li><a href="javascript:;">Sandpaper & Steel Wool </a></li>
                                    <li><a href="javascript:;">Scrapers, Blades & Tools </a></li>
                                    <li><a href="javascript:;">Specialty Paint </a></li>
                                    <li><a href="javascript:;">Stains & Sealers </a></li>

                                </ul>
                            </li>
                            <li><a>Plumbing</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Augers, Plungers & Drain Openers </a></li>
                                    <li><a href="javascript:;">Pipe & Fittings </a></li>
                                    <li><a href="javascript:;">Plumbing Parts & Repair </a></li>
                                    <li><a href="javascript:;">Plumbing Tools & Cements </a></li>
                                    <li><a href="javascript:;">Shut-Off Valves & Connectors </a></li>
                                    <li><a href="javascript:;">Supply Lines & Shut-Off Valves </a></li>
                                    <li><a href="javascript:;">Utility Sinks & Faucets </a></li>
                                    <li><a href="javascript:;">Valves & Valve Repair </a></li>
                                    <li><a href="javascript:;">Water Filtration & Water Softeners </a></li>
                                    <li><a href="javascript:;">Water Heaters </a></li>
                                    <li><a href="javascript:;">Water Pumps & Tanks </a></li>
                                </ul>
                            </li>
                            <li><a>Roofing,Gutters & Skylights</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Downspout Components</a></li>
                                    <li><a href="javascript:;">Downspout Guards</a></li>
                                    <li><a href="javascript:;">Flashings </a></li>
                                    <li><a href="javascript:;">Gutter Connectors </a></li>
                                    <li><a href="javascript:;">Gutter End Caps </a></li>
                                    <li><a href="javascript:;">Gutter Guards </a></li>
                                    <li><a href="javascript:;">Gutter Hangers & Brackets </a></li>
                                    <li><a href="javascript:;">Gutter Sealants</a></li>
                                    <li><a href="javascript:;">Gutter Tools </a></li>
                                    <li><a href="javascript:;">Gutters </a></li>
                                    <li><a href="javascript:;">Roll Roofing</a></li>
                                    <li><a href="javascript:;">Roof Coatings</a></li>
                                    <li><a href="javascript:;">Roof Panels & Accessories </a></li>
                                    <li><a href="javascript:;">Roof Seam Sealers </a></li>
                                    <li><a href="javascript:;">Roof Shingles </a></li>
                                    <li><a href="javascript:;">Roofing Tools</a></li>
                                    <li><a href="javascript:;">Roofing Underlayment</a></li>
                                    <li><a href="javascript:;">Splash Blocks</a></li>
                                </ul>
                            </li>
                            <li><a>Siding & Trim</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Brick Veneer & Accessories</a></li>
                                    <li><a href="javascript:;">Fascia </a></li>
                                    <li><a href="javascript:;">Fiber Cement Siding</a></li>
                                    <li><a href="javascript:;">Gable Vent Keystones</a></li>
                                    <li><a href="javascript:;">Metal Siding Trim</a></li>
                                    <li><a href="javascript:;">Mounting Blocks </a></li>
                                    <li><a href="javascript:;">Siding Tools </a></li>
                                    <li><a href="javascript:;">Skirting </a></li>
                                    <li><a href="javascript:;">Soffit</a></li>
                                    <li><a href="javascript:;">Stone Veneer & Accessories</a></li>
                                    <li><a href="javascript:;">Vinyl Siding & Accessories</a></li>
                                    <li><a href="javascript:;">Wood & Shingle Siding</a></li>
                                </ul>
                            </li>
                            <li><a>Tools & Equipment</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Air Tools & Compressors </a></li>
                                    <li><a href="javascript:;">Hand Tools </a></li>
                                    <li><a href="javascript:;">Home 3D Printers & Filament </a></li>
                                    <li><a href="javascript:;">Ladders & Scaffolding </a></li>
                                    <li><a href="javascript:;">Levels & Measuring Tools </a></li>
                                    <li><a href="javascript:;">Power Tools </a></li>
                                    <li><a href="javascript:;">Shop Vacuums & Accessories </a></li>
                                    <li><a href="javascript:;">Tool Storage & Work Benches </a></li>
                                    <li><a href="javascript:;">Welding & Soldering </a></li>
                                </ul>
                            </li>
                            <li><a>Deck & Railings</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Deck Accents</a></li>
                                    <li><a href="javascript:;">Deck Adapters</a></li>
                                    <li><a href="javascript:;">Deck Adapters</a></li>
                                    <li><a href="javascript:;">Deck Boards</a></li>
                                    <li><a href="javascript:;">Deck Boards</a></li>
                                    <li><a href="javascript:;">Deck Post Sleeves </a></li>
                                    <li><a href="javascript:;">Deck Posts </a></li>
                                    <li><a href="javascript:;">Deck Railings </a></li>
                                    <li><a href="javascript:;">Deck Stairs</a></li>
                                    <li><a href="javascript:;">Interior Railings & Stair Parts </a></li>
                                    <li><a href="javascript:;">Porches</a></li>
                                    <li><a href="javascript:;">Stair Hardware</a></li>
                                    <li><a href="javascript:;">Staircase Kits </a></li>
                                    <li><a href="javascript:;">Staircase Poles</a></li>
                                    <li><a href="javascript:;">Treads & Risers</a></li>
                                    <li><a href="javascript:;">Under Deck Ceiling Systems</a></li>
                                </ul>
                            </li>
                            <li><a>Doors & Windows</a>
                                <ul class="clearfix inventory_cat">
                                    <li><a href="javascript:;">Awnings & Accessories</a></li>
                                    <li><a href="javascript:;">Doors</a></li>
                                    <li><a href="javascript:;">Exterior Shutters & Accessories</a></li>
                                    <li><a href="javascript:;">Garage Doors & Openers</a></li>
                                    <li><a href="javascript:;">Hurricane Shutter Panels</a></li>
                                    <li><a href="javascript:;">Weatherstripping </a></li>
                                    <li><a href="javascript:;">Windows </a></li>
                                </ul>
                            </li>

                        </ul>
                    </div>
                    <div class="right_inventroy">
                        <asp:UpdatePanel ID="updtpnlsku" runat="server">
                            <ContentTemplate>
                        <div class="form_panel">
                        <table>
                            <tr>
                                <td>Sku Name</td>
                                <td>
                                    <asp:Label ID="lblSkuId" runat="server" Visible="false"/>
                                    <asp:TextBox ID="txtsku" runat="server"></asp:TextBox>
                                     <asp:Button ID="btnAddsku" runat="server" Text="Add" ValidationGroup="sku" OnClick="btnAddsku_Click"/>
                                    <asp:Label ID="lblres" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsku" Display="Dynamic"
                                        ValidationGroup="sku" ErrorMessage="Enter Name" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                           <%-- <tr>
                                <td colspan="2" align="center">
                                  
                                </td>
                            </tr>--%>
                            <td>
                                <asp:DropDownList ID="ddlskuName" runat="server"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlskuName" Display="Dynamic"
                                        ValidationGroup="skuE" ErrorMessage="Please Select" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                 <asp:Button ID="btnEditsku" runat="server" Text="Edit" ValidationGroup="skuE" OnClick="btnEditsku_Click"/> 
                                <asp:Button ID="btnDeleteSku" runat="server" Text="Delete" ValidationGroup="skuE" OnClick="btnDeleteSku_Click"/>
                                 <asp:Label ID="lblDelRes" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                            </td>
                            </table>
                            </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="tabs-3">
                <p>&nbsp</p>
            </div>
        </div>
    </div>
</asp:Content>
