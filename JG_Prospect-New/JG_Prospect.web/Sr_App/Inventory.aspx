<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="JG_Prospect.Sr_App.Inventory" %>

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
                    position:relative;
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
            margin-bottom:20px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".inventroy .left_inventroy ul li a").click(function () {
                //$(this).parent("li").siblings().find("ul").hide();
                $(this).parent("li").find("ul:first").toggleClass('active');
            });
        });
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
        <h1>Inventory
        </h1>
        <div class="clearfix inventroy">
            <div class="left_inventroy">
                <asp:Literal ID="ltrInventoryCategoryList" runat="server"></asp:Literal>
            </div>
            <div class="right_inventroy"></div>
        </div>
    </div>
</asp:Content>
