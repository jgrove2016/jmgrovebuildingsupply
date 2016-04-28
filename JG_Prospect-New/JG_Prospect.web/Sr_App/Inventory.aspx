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
                margin-left: 320px;
                box-sizing: border-box;
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
    </style>
    <script type="text/javascript">
        $(function () {
            $(".inventroy .left_inventroy ul li a").click(function () {
                //$(this).parent("li").siblings().find("ul").hide();
                $(this).parent("li").find("ul:first").toggleClass('active');
            });

        });
        function EditSubCat(id, name, IsRetail_Wholesale, IsManufacturer) {
            $("#<%=hdnSubCategoryId.ClientID%>").val(id);
            $("#<%=txtVendorSubCatEdit.ClientID%>").val(name);
            if (IsRetail_Wholesale == "True") {
                $("#<%=chkVSCRetail_WholesaleEdit.ClientID%>").attr("checked", true);
            }
            if (IsManufacturer == "True") {
                $("#<%=chkVSCManufacturerEdit.ClientID%>").attr("checked", true);
            }
            console.log($find("<%=ModalPopupExtender1.BehaviorID%>"));
            //Sys.Application.add_load(function () {
                $("#<%=ModalPopupExtender1.ClientID%>").show();
            //});
            return false;
            // alert( $("#<%=lnkEditVendorSubCategory.ClientID%>").size())
            //$("#<%=lnkEditVendorSubCategory.ClientID%>").click();
        }
        function popupClk() {
            //alert('shyam')
            // $find('<%=ModalPopupExtender1.BehaviorID %>').show();
            //$find("ModalPopupExtender1").show();
            //__doPostBack('<%= lnkEditVendorSubCategory.ClientID %>', 'OnClick');
            return true
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- appointment tabs section start -->
    <ul class="appointment_tab">
        <li><a href="Price_control.aspx">Product Line Estimate</a></li>
        <li><a href="Inventory.aspx">Inventory</a></li>
        <li><a href="Maintenace.aspx">Maintainance</a></li>
    </ul>
    <!-- appointment tabs section end -->
    <h1>Inventory
    </h1>
    <div class="form_panel">
        <div class="right_panel">

            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="rdoRetailWholesale" runat="server" Checked="true" Text="Retail/Wholesale" GroupName="MT" OnCheckedChanged="rdoRetailWholesale_CheckedChanged" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoManufacturer" runat="server" Text="Manufacturer" GroupName="MT" OnCheckedChanged="rdoManufacturer_CheckedChanged" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkAddVendorCategory1" Text="Add Vendor Category" runat="server"></asp:LinkButton>
                        <br />
                        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="lnkAddVendorCategory1"
                            PopupControlID="pnlpopupVendorCategory" CancelControlID="btnCancelVendor">
                        </asp:ModalPopupExtender>

                        <asp:Panel ID="pnlpopupVendorCategory" runat="server" CssClass="vendorFilter" BackColor="White" Height="269px" Width="550px"
                            Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                            <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                                cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                        align="center">Add Vendor Category
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Product Category Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProductCatgoryPopup" runat="server" Width="150px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProductCatgoryPopup"
                                            ValidationGroup="addvendorcat" ErrorMessage="Select Product Category Name." InitialValue="Select" ForeColor="Red"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Vendor Category Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtnewVendorCat" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnewVendorCat"
                                            ValidationGroup="addvendorcat" ErrorMessage="Enter Vendor Category Name." ForeColor="Red"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Manufacturer Type
                                    </td>
                                    <td>

                                        <asp:CheckBox ID="chkVCRetail_Wholesale" runat="server" Text="Retail/Wholesale" />
                                        <asp:CheckBox ID="chkVCManufacturer" runat="server" Text="Manufacturer" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnNewVendor" Style="width: 100px;" runat="server"
                                            OnClick="btnNewVendor_Click" Text="Save" ValidationGroup="addvendorcat" />
                                        <asp:Button ID="btnCancelVendor" runat="server" Style="width: 100px;" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </td>
                    <td>
                        <asp:LinkButton ID="lnkAddVendorSubCategory" Text="Add Vendor Sub Category" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="lnkAddVendorSubCategory"
                            PopupControlID="pnlvendorSubCat" CancelControlID="btnCancelVendorSubCat">
                        </asp:ModalPopupExtender>

                        <asp:Panel ID="pnlvendorSubCat" runat="server" CssClass="vendorFilter" BackColor="White" Height="269px" Width="550px"
                            Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                            <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                                cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                        align="center">Add Vendor Sub Category
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Vendor Category Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVendorCatPopup" runat="server" Width="150px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlVendorCatPopup"
                                            ValidationGroup="addvendorsubcat" ErrorMessage="Select Vendor Category Name." InitialValue="Select" ForeColor="Red"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Vendor Sub Category Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVendorSubCat" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVendorSubCat"
                                            ValidationGroup="addvendorsubcat" ErrorMessage="Enter Vendor Sub Category Name." ForeColor="Red"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Manufacturer Type
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVSCRetail_Wholesale" runat="server" Text="Retail/Wholesale" />
                                        <asp:CheckBox ID="chkVSCManufacturer" runat="server" Text="Manufacturer" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnVendorSubCat" Style="width: 100px;" runat="server"
                                            OnClick="btnNewVendorSubCat_Click" Text="Save" ValidationGroup="addvendorsubcat" />
                                        <asp:Button ID="btnCancelVendorSubCat" runat="server" Style="width: 100px;" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </td>
                </tr>
            </table>
           <%-- <asp:UpdatePanel ID="updtpnlfilter" runat="server">
                <ContentTemplate>--%>
                    <asp:LinkButton ID="lnkEditVendorSubCategory" Visible="false" Text="Edit Vendor Sub Category" CssClass="clsEditVendorSubCategory" runat="server"></asp:LinkButton>
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkEditVendorSubCategory"
                        PopupControlID="pnlEditVendorSubCat" CancelControlID="btnCancelVendorSubCatEdit">
                    </asp:ModalPopupExtender>

                    <asp:Panel ID="pnlEditVendorSubCat" runat="server" CssClass="vendorFilter" BackColor="White" Height="269px" Width="550px"
                        Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                            cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                    align="center">Update Vendor Sub Category
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 31%">Vendor Sub Category Name
                                </td>
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
                                    <asp:Button ID="btnUdpateVendorSubCat" Style="width: 100px;" runat="server"
                                        OnClick="btnUdpateVendorSubCat_Click" Text="Save" ValidationGroup="Updatevendorsubcat" />
                                    <asp:Button ID="btnCancelVendorSubCatEdit" runat="server" Style="width: 100px;" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>

            <div class="clearfix inventroy">
                <div class="left_inventroy">
                    <asp:Literal ID="ltrInventoryCategoryList" runat="server"></asp:Literal>
                </div>
                <div class="right_inventroy"></div>
            </div>
        </div>
    </div>
</asp:Content>
