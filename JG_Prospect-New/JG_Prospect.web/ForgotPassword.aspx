<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="JG_Prospect.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>JG Prospect</title>
    <link href="/css/screen.css" rel="stylesheet" media="screen" type="text/css" />
    <link href="/css/jquery.ui.theme.css" rel="stylesheet" media="screen" type="text/css" />
    <script type="text/javascript" src="/js/jquery-latest.js"></script>
    <!--accordion jquery-->
    <script type="text/javascript" src="/js/ddaccordion.js"></script>
    <script type="text/javascript">
        $(function () {
            // Tabs
            $('#tabs').tabs();
        });
    </script>
    <style type="text/css">
        .ui-widget-header {
            border: 0;
            background: none /*{bgHeaderRepeat}*/;
            color: #222 /*{fcHeader}*/;
        }

        .auto-style1 {
            width: 100%;
        }
    </style>
    <div class="header">
        <img src="/img/logo.png" alt="logo" width="88" height="89" class="logo" />
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
                    <h1><b>Forgot Password</b></h1>
                    <!-- Tabs starts -->
                    <div id="tabs-1">
                        <div class="login_form_panel">
                            <ul>
                                <li class="last">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Login Id<span>*</span></label>
                                                <asp:TextBox ID="txtloginid" runat="server" TabIndex="1" Width="312px"></asp:TextBox>
                                                <br />
                                                <label></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Login"
                                                    ControlToValidate="txtloginid" Display="Dynamic" ForeColor="Red">Please Enter UserName</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                            </ul>
                            <div class="btn_sec">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="Login" TabIndex="3" OnClick="btnsubmit_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="4" PostBackUrl="login.aspx" />
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <div id="tabs">
                    <div id="tabs-2"></div>
                </div>--%>
                <!-- Tabs endss -->

            </div>
        </div>

        <!--footer section-->
        <div class="footer_panel">
            <ul>
                <li>&copy; 2012 JG All Rights Reserved.</li>
                <li><a href="#">Terms of Use</a></li>
                <li>|</li>
                <li><a href="#">Privacy Policy</a></li>
            </ul>
        </div>
    </form>
</body>
</html>
