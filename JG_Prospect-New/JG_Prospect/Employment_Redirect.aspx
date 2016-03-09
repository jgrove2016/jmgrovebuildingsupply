<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employment_Redirect.aspx.cs" Inherits="JG_Prospect.Employment_Redirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">




<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">


    <title>JG Employment Redirection</title>
    <link href="css/screen.css" rel="stylesheet" media="screen" type="text/css" />



    <style type="text/css">
        .ui-widget-header
        {
            border: 0;
            background: none /*{bgHeaderRepeat}*/;
            color: #222 /*{fcHeader}*/;
        }

        .auto-style1
        {
            width: 100%;
        }

        label
        {
            float: left;
            font-size: 12px;
            font-family: Verdana;
        }

        .padding
        {
            padding: 5px;
        }


        select
        {
            -webkit-appearance: button;
            -moz-appearance: button;
            -webkit-user-select: none;
            -moz-user-select: none;
            -webkit-padding-end: 20px;
            -moz-padding-end: 20px;
            -webkit-padding-start: 2px;
            -moz-padding-start: 2px;
            border: 1px solid #AAA;
            border-radius: 2px;
            box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.1);
            font-size: 12px;
            font-family: Verdana;
            height: 30px;
            margin: 0;
            overflow: hidden;
            padding-top: 2px;
            padding-bottom: 2px;
            text-overflow: ellipsis;
            white-space: nowrap;
        }
    </style>


    <img src="img/logo.png" alt="logo" width="88" height="89" class="logo" />
    <script type="text/javascript">

        function Redirect() {

            var e = document.getElementById('<%=ddlJobs.ClientID %>');
            var position = e.options[e.selectedIndex].value;
            var pathArray = window.location.pathname.split('/');
            var URL = window.location.protocol + "//" + window.location.host;

            var url1 = URL + "/Sr_App/EditInstallUser.aspx";
            var url2 = URL + "/Sr_App/EditUser.aspx";
            if (position == "1") {
                alert("Please select valid entry!");
                return;
            }
            if (position == "2" || position == "3" || position == "4" || position == "7") {

                window.open(url1, "_self");
            }
            else {
                window.open(url2, "_self");
            }

        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <!--header section-->
            <div class="header">
            </div>
            <div class="content_panel">
                <div class="login_right_panel">


                    <h1><b>Employment Selection</b></h1>
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td class="padding">
                                <label>1. This dropdown works on javascript</label>
                            </td>

                        </tr>
                        <tr>
                            <td class="padding">
                                <asp:DropDownList ID="ddlJobs" runat="server" AutoPostBack="false" onchange="Redirect();" TabIndex="1">
                                    <asp:ListItem Text="Position applying for *" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Staffing Coordinator" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Entry level sales associates" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Project Managers" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Window & door installers" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Roofing installers" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Administrative" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="8"></asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="padding">
                                <label>2. This dropdown works on serverside</label>
                            </td>

                        </tr>
                        <tr>
                            <td class="padding">
                                <asp:ValidationSummary
                                    ID="valdropdown"
                                    DisplayMode="BulletList"
                                    EnableClientScript="true"
                                    ShowMessageBox="true"
                                    ShowSummary="true"
                                    HeaderText="You must enter a value in the following fields:"
                                    Font-Names="verdana"
                                    Font-Size="12"
                                    runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <td class="padding">

                                <asp:DropDownList ID="ddlPositions" runat="server" AutoPostBack="true" TabIndex="2"  OnSelectedIndexChanged="ddlPositions_SelectedIndexChanged">
                                    <asp:ListItem Text="Position applying for *" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Staffing Coordinator" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Entry level sales associates" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Project Managers" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Window & door installers" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Roofing installers" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Administrative" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="8"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>

                        </tr>
                       
                    </table>




                </div>
            </div>
        </div>
    </form>
    <!-- Begin INDEED conversion code -->
    <script type="text/javascript">
        /* <![CDATA[ */
        var indeed_conversion_id = '7832796345422302';
        var indeed_conversion_label = '';
        /* ]]> */
    </script>
    <script type="text/javascript" src="//conv.indeed.com/pagead/conversion.js"> 
    </script>
    <noscript>
        <img height="1" width="1" border="0" src="//conv.indeed.com/pagead/conv/7832796345422302/?script=0">
    </noscript>
    <!-- End INDEED conversion code -->

</body>
</html>
