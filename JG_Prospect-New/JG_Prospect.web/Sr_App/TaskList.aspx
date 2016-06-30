<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="JG_Prospect.Sr_App.TaskList" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/dd.css" rel="stylesheet" />

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
            height: 5%;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }

        @font-face {
            font-family: 'barcode_fontregular';
            src: url('../fonts/barcodefont-webfont.eot');
            src: url('../fonts//barcodefont-webfont.eot?#iefix') format('embedded-opentype'), url('../fonts/barcodefont-webfont.woff2') format('woff2'), url('../fonts/barcodefont-webfont.woff') format('woff'), url('../fonts/barcodefont-webfont.ttf') format('truetype'), url('../fonts/barcodebarcodefont-webfont.svg#barcode_fontregular') format('svg');
            font-weight: normal;
            font-style: normal;
        }

        .GridView1 {
            border-collapse: collapse;
        }

            .GridView1 > tbody > tr > td {
                border: 1px solid #CCCCCC;
                margin: 0;
                padding: 0px !important;
            }

        .grid td {
            padding: 0px !important;
        }


        .clsGrid {
            overflow: inherit !important;
        }

        .clsFixWidth > tbody tr td {
            word-break: break-all;
        }

        .clsOverFlow {
            overflow: auto;
            height: 150px;
        }
    </style>

    <style type="text/css">
        .Autocomplete {
            overflow: auto;
            height: 150px;
        }

        .style1 {
            width: 451px;
        }

        .style2 {
            width: 451px;
        }

        .auto-style10 {
            width: 58px;
        }

        .auto-style11 {
            width: 100%;
        }

        .auto-style13 {
        }

        .auto-style14 {
            height: 137px;
            width: 447px;
        }

        .auto-style15 {
            width: 447px;
        }
    </style>

    <style>
        /* Absolute Center Spinner */
        .loading {
            position: fixed;
            z-index: 999;
            height: 2em;
            width: 2em;
            overflow: show;
            margin: auto;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
        }

            /* Transparent Overlay */
            .loading:before {
                content: '';
                display: block;
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
            }

            /* :not(:required) hides these rules from IE9 and below */
            .loading:not(:required) {
                /* hide "loading..." text */
                font: 0/0 a;
                color: transparent;
                text-shadow: none;
                background-color: transparent;
                border: 0;
            }

                .loading:not(:required):after {
                    content: '';
                    display: block;
                    font-size: 10px;
                    width: 1em;
                    height: 1em;
                    margin-top: -0.5em;
                    -webkit-animation: spinner 1500ms infinite linear;
                    -moz-animation: spinner 1500ms infinite linear;
                    -ms-animation: spinner 1500ms infinite linear;
                    -o-animation: spinner 1500ms infinite linear;
                    animation: spinner 1500ms infinite linear;
                    border-radius: 0.5em;
                    -webkit-box-shadow: rgba(0, 0, 0, 0.75) 1.5em 0 0 0, rgba(0, 0, 0, 0.75) 1.1em 1.1em 0 0, rgba(0, 0, 0, 0.75) 0 1.5em 0 0, rgba(0, 0, 0, 0.75) -1.1em 1.1em 0 0, rgba(0, 0, 0, 0.5) -1.5em 0 0 0, rgba(0, 0, 0, 0.5) -1.1em -1.1em 0 0, rgba(0, 0, 0, 0.75) 0 -1.5em 0 0, rgba(0, 0, 0, 0.75) 1.1em -1.1em 0 0;
                    box-shadow: rgba(0, 0, 0, 0.75) 1.5em 0 0 0, rgba(0, 0, 0, 0.75) 1.1em 1.1em 0 0, rgba(0, 0, 0, 0.75) 0 1.5em 0 0, rgba(0, 0, 0, 0.75) -1.1em 1.1em 0 0, rgba(0, 0, 0, 0.75) -1.5em 0 0 0, rgba(0, 0, 0, 0.75) -1.1em -1.1em 0 0, rgba(0, 0, 0, 0.75) 0 -1.5em 0 0, rgba(0, 0, 0, 0.75) 1.1em -1.1em 0 0;
                }

        /* Animation */

        @-webkit-keyframes spinner {
            0% {
                -webkit-transform: rotate(0deg);
                -moz-transform: rotate(0deg);
                -ms-transform: rotate(0deg);
                -o-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
                -moz-transform: rotate(360deg);
                -ms-transform: rotate(360deg);
                -o-transform: rotate(360deg);
                transform: rotate(360deg);
            }
        }

        @-moz-keyframes spinner {
            0% {
                -webkit-transform: rotate(0deg);
                -moz-transform: rotate(0deg);
                -ms-transform: rotate(0deg);
                -o-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
                -moz-transform: rotate(360deg);
                -ms-transform: rotate(360deg);
                -o-transform: rotate(360deg);
                transform: rotate(360deg);
            }
        }

        @-o-keyframes spinner {
            0% {
                -webkit-transform: rotate(0deg);
                -moz-transform: rotate(0deg);
                -ms-transform: rotate(0deg);
                -o-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
                -moz-transform: rotate(360deg);
                -ms-transform: rotate(360deg);
                -o-transform: rotate(360deg);
                transform: rotate(360deg);
            }
        }

        @keyframes spinner {
            0% {
                -webkit-transform: rotate(0deg);
                -moz-transform: rotate(0deg);
                -ms-transform: rotate(0deg);
                -o-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
                -moz-transform: rotate(360deg);
                -ms-transform: rotate(360deg);
                -o-transform: rotate(360deg);
                transform: rotate(360deg);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="loading" style="display: none">Loading&#8230;</div>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>

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
                <h1>Task List
                </h1>

                <div class="form_panel_custom">
                    <table class="filter_section" style="width: 100%;">
                        <tr>
                            <td>
                                <span>Filter by :</span>

                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" placeholder="Task title" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation" runat="server">
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlUsers" runat="server">
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlTaskStatus" runat="server">
                                    <asp:ListItem Text="--Status--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Assigned" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="In Progress" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Re-Opened"></asp:ListItem>
                                    <asp:ListItem Text="Closed"></asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtCreatedDate" runat="server"></asp:TextBox></td>
                            <td class="btn_sec">
                                <asp:Button ID="btnSearch" runat="server" CssClass="ui-button" OnClick="btnSearch_Click" Text="Search" /></td>
                            <td><span>
                                <a id="btnAdd" class="btn btn-primary" onclick="EditTask(0);">Add New</a></span>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvTasks" runat="server" AllowCustomPaging="true" AllowPaging="true" PageSize="20" CssClass="table" Width="100%" CellSpacing="0" CellPadding="0" BorderStyle="Solid" BorderWidth="1" AutoGenerateColumns="False" OnRowDataBound="gvTasks_RowDataBound" OnPageIndexChanging="gvTasks_PageIndexChanging">
                        <HeaderStyle CssClass="trHeader " />
                        <RowStyle CssClass="FirstRow" />
                        <AlternatingRowStyle CssClass="AlternateRow " />
                        <Columns>
                            <asp:BoundField DataField="InstallId" HeaderText="Install ID" />
                            <asp:TemplateField HeaderText="Task Title">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypTask" runat="server"><%# Eval("Title") %></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                    <%-- <asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedUser" runat="server" Text='<%# Eval("FristName") %>'></asp:Label>
                                    <%--<asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblTaskStatus" runat="server"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTaskDueDate" runat="server" Text='<%#Eval("DueDate")%>'></asp:Label>
                                    <%--<asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- End of Right panel -->
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script src="../js/jquery.dd.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {



            //On UpdatePanel Refresh
            //debugger;
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                // debugger;
                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $(".loading").show();
                    }
                });
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $(".loading").hide();
                    }
                });
            };

        });


    </script>

</asp:Content>
