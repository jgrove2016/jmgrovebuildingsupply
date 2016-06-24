<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="HRReports.aspx.cs" Inherits="JG_Prospect.Sr_App.HRReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/ScrollableTablePlugin_1.0_min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#Table1').Scrollable({
                ScrollHeight: 100
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
        }

        .auto-style3 {
            width: 1px;
        }

        .auto-style9 {
        }

        .auto-style10 {
            width: 168px;
        }

        .auto-style11 {
            width: 111px;
        }

        .auto-style12 {
            width: 154px;
        }
         table.tblshowhrdata {
            width: 100%;
            border: 1px solid #ddd;
            background: #fff;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="HRReports.aspx">HR Reports</a></li>
            <li><a href="InstallCreateUser.aspx">Create Install User</a></li>
            <li><a href="EditInstallUser.aspx">Edit Install User</a></li>
            <li><a href="CreateSalesUser.aspx">Create Sales User</a></li>
            <li><a href="EditUser.aspx">Edit Sales User</a></li>
        </ul>
        <h1>HR Reports
        </h1>
        <div class="form_panel_custom">
            <ul style="margin-bottom: 10px;">
                <li style="width: 101%; height: 1265px;">
                    <table class="auto-style1">
                        <tr>
                            <td colspan="2" style="font-size: medium; font-weight: bold; font-style: normal">PayRoll 1099 Summery&nbsp;
                            </td>
                            <td style="font-size: large; font-weight: bold; font-style: normal" class="auto-style12">User
                            </td>
                            <td style="font-size: large; font-weight: bold; font-style: normal" class="auto-style10">
                                <asp:DropDownList ID="ddlUsers" Width="150px" runat="server"></asp:DropDownList>
                            </td>
                            <td colspan="2" style="font-size: large; font-weight: bold; font-style: normal">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Company Location
                                &nbsp;
                            </td>
                            <td style="font-size: large; font-weight: bold; font-style: normal">
                                <asp:TextBox ID="txtCompanyLocation" runat="server"></asp:TextBox>

                            </td>
                            <td style="font-size: large; font-weight: bold; font-style: normal" class="auto-style9" colspan="2">&nbsp;
                            </td>
                            <td colspan="2" style="font-size: large; font-weight: bold; font-style: normal">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;Pay Period
                            
                            </td>
                            <td style="font-size: large; font-weight: bold; font-style: normal">
                                <%-- <asp:TextBox ID="txtPayPeriod" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="drpPayPeriod" runat="server" Width="250px" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpPayPeriod_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td colspan="4" style="font-size: large; font-weight: bold; font-style: normal">Select Period
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Check Date
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtCheckDate" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style12">From: 
                            </td>
                            <td class="auto-style10">
                                <asp:TextBox ID="txtDtFrom" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDtFrom" Format="dd/MM/yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="auto-style11">To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtTo" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDtTo" Format="dd/MM/yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">Prepared/Approved By:
                            </td>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtPrepared" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style12">&nbsp;
                            </td>
                            <td class="auto-style10">
                                <asp:Button ID="btnSubmit" Text="Submit" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" runat="server" Height="30px" Width="75px" OnClick="btnSubmit_Click" />
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                        </tr>
                    </table>

                    <div class="showhrdata">
                        <table class="tblshowhrdata">
                            <tr>
                                <td>New Applicants
                                </td>
                                <td>
                                    <asp:Label ID="lblApplicant" runat="server"></asp:Label>
                                </td>
                                <td>applicant/interview
                                </td>
                                <td>
                                    <asp:Label ID="lblAppInterRatio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server">New Active</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblActive" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDeactivated" runat="server">applicant/new hire</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAppHireRatio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>New Phone/Video Screened
                                </td>
                                <td>
                                    <asp:Label ID="lblPhoneVideo" runat="server"></asp:Label>
                                </td>
                                <td>interview/new hire
                                </td>
                                <td>
                                    <asp:Label ID="lblInterNewRatio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>New Interview Date
                                </td>
                                <td>
                                    <asp:Label ID="lblInterviewDate" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>New Rejected
                                </td>
                                <td>
                                    <asp:Label ID="lblRejected" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;
                                </td>
                                <td colspan="3">

                                    <asp:Repeater ID="rptCustomers" runat="server">
                                        <HeaderTemplate>
                                            <table id="Table1" cellspacing="0" rules="all" border="1">
                                                <tr>
                                                    <th scope="col">Date
                                                    </th>
                                                    <th scope="col">Time
                                                    </th>
                                                    <th scope="col">User
                                                    </th>
                                                    <th scope="col">Reason
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("RejectionDate") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTime" runat="server" Text='<%# Eval("RejectionTime") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUser" runat="server" Text='<%# Eval("Username") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblReason" runat="server" Text='<%# Eval("StatusReason") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                </td>
                            </tr>
                        </table>
                    </div>


                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="pnlGrid">
                                <div class="form_panel" style="padding-bottom: 0px; min-height: 100px;">
                                    <div class="grid">
                                        <%--<table id="table2" class="auto-style11">
                                    <tr>
                                        <td>--%>
                                        <asp:GridView ID="gvYtd" Width="100%" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="false" HeaderStyle-BackColor="#cccccc" AllowSorting="false" runat="server">
                                            <EmptyDataTemplate>
                                                No data to display
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Employee ID" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeID" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Last Name" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLname" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="First Name" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFname" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Designition" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Gross Pay" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrosspay" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Bonus" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbonus" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Total Taxes" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotaltaxes" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Attendance" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblattendancelink" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <br />
                                        <label>
                                            YTD :
                                        </label>
                                        <br />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="Panel1">
                                <div class="form_panel" style="padding-bottom: 0px; min-height: 100px;">
                                    <div class="grid">
                                        <%--<table id="table2" class="auto-style11">
                                    <tr>
                                        <td>--%>
                                        <asp:GridView ID="GridView1" Width="100%" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="false" HeaderStyle-BackColor="#cccccc" AllowSorting="false" runat="server">
                                            <EmptyDataTemplate>
                                                No data to display
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Subcontractor ID" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubcontractorID" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Last Name" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubLname" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="First Name" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubLname" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Cutomer Id-Job#" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcutomeridjob" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Gross Pay" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubgrosspay" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Bonus" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbonus" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="Labor invoice packets" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLaborinvoicepackets" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="True" HeaderText="(LOG) Notes" ControlStyle-ForeColor="Black"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLOGNotes" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle ForeColor="Black" />
                                                    <ControlStyle ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <br />
                                        <label>
                                            YTD :
                                        </label>
                                        <br />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                    <div class="btn_sec">
                        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                    </div>


                </li>
            </ul>
            <div>
            </div>
        </div>
    </div>
</asp:Content>
