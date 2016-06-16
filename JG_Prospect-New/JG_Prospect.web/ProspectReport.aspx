<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true" CodeBehind="ProspectReport.aspx.cs" Inherits="JG_Prospect.WebForm2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="~/Scripts/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        function hidePnl() {
            $("#ContentPlaceHolder1_pnlpopup").hide();
            return true;
        }
    </script>

    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            display: none;
        }
    </style>
    <%-- <script>
        function AssemblyFileUpload_Started(sender, args) {
            var filename = args.get_fileName();
            var ext = filename.substring(filename.lastIndexOf(".") + 1);
            if (ext != 'png' && ext != 'jpg' && ext != 'bmp') {
                throw {
                    name: "Invalid File Type",
                    level: "Error",
                    message: "Invalid File Type (Only .png)",
                    htmlMessage: "Invalid File Type (Only .png,.jpg and bmp)"
                }
                return false;
            }
            return true;
        }

</script>--%>
    <style type="text/css">
        .Autocomplete
        {
            overflow: auto;
            height: 150px;
        }

        .style1
        {
            height: "";
            width: 451px;
        }

        .style2
        {
            width: 451px;
        }
    .auto-style1 {
        height: "51px";
        width: 451px;
    }
    .auto-style2 {
        width: 451px;
        height: 72px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <%--<li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>--%>
        </ul>
        <!-- appointment tabs section End -->
        <h1>Install Create Prospect
        </h1>
        <div class="form_panel_custom">
            <span>
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
            </span>
            <br />


             <ul>
                <li style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label style="font-size: medium; font-weight: bold">
                                    Select Period</label>
                                &nbsp;&nbsp;
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    From 
                                </label>
                                <asp:TextBox ID="txtFrom" runat="server"  Width="223px" TabIndex="117" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="MM/dd/yyyy"  TargetControlID="txtFrom" runat="server"></ajaxToolkit:CalendarExtender>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <label>
                                    Select User</label>
                                <asp:DropDownList ID="ddlUsersList" runat="server" Width="229px" TabIndex="119"></asp:DropDownList>
                                <br />
                                <label></label>
                                </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="auto-style1">
                                
                                </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <label>
                                    To</label>
                                <asp:TextBox ID="txtTo" runat="server" Width="217px" TabIndex="118" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTo" Format="MM/dd/yyyy"  runat="server"></ajaxToolkit:CalendarExtender>
                                <br />


                            </td>
                        </tr>
                        <tr>
                            <td>
                                

                            </td>
                        </tr>
                    </table>
                </li>
                
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnReport" Text="Show" runat="server" OnClick="btnReport_Click" TabIndex="120" />
            </div>
            <br />
            <br />
            <div class="btn_sec">
                <center>
            <asp:Label ID="lblCount" Text="Installed Prospect" ForeColor="Black" runat="server" Font-Size="Medium" Font-Bold="True"></asp:Label>
                                
                                <label>&nbsp;&nbsp; <asp:Label ID="lblTotCount" ForeColor="Black"  runat="server" Font-Size="Small"></asp:Label></label>    
                    </center>
            </div>
        </div>
    </div>
</asp:Content>
