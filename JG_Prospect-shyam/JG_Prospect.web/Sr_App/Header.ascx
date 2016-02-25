<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="JG_Prospect.Sr_App.Header" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!--tabs jquery-->
<%--<script type="text/javascript" src="../js/jquery.ui.core.js"></script>
<script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
<!--tabs jquery ends-->
<script type="text/javascript">
    $(function () {
        // Tabs
        $('#tabs').tabs();
    });
		</script>
<style type="text/css">
.ui-widget-header {
	border: 0;
	background:none/*{bgHeaderRepeat}*/;
	color: #222/*{fcHeader}*/;
}
</style>--%>
<div class="header">
    <img src="../img/logo.png" alt="logo" width="88" height="89" class="logo" />
    <%--<ul>
     <li><a href="/changepassword.aspx">Click for weather forecast</a></li>
    </ul>--%>
    <div style="margin-left:950px; margin-top:10px;">
        <asp:LinkButton ID="lbtWeather" runat="server" OnClick="lbtWeather_Click" Text="Click for weather forecast" ForeColor="White"></asp:LinkButton>
    </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" ShowContentDuringLoad="false" Width="400px"
                Height="400px" Title="Weather Forecast" Behaviors="Default">
                <ContentTemplate>
                <span style="display: block !important; margin-top:50px; width: 100px; text-align: center; font-family: sans-serif; font-size: 12px;"><a href="http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:19019.1.99999&bannertypeclick=wu_clean2day" title="Philadelphia, Pennsylvania Weather Forecast" target="_blank"><img src="http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=KPNE&ForcedCity=Philadelphia&ForcedState=PA&zip=19019&language=EN" alt="Find more about Weather in Philadelphia, PA" width="500" style="height:100px;"/></a><br><a href="http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:19019.1.99999&bannertypeclick=wu_clean2day" title="Get latest Weather Forecast updates" style="font-family: sans-serif; font-size: 12px" target="_blank"><%--Click for weather forecast--%></a></span>
                    <%--margin-left:240px; margin-top:10px;--%>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

<%--<span style="display: block !important; margin-left:240px; margin-top:10px; width: 820px; text-align: center; font-family: sans-serif; font-size: 12px;"><a href="http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:19019.1.99999&bannertypeclick=wu_clean2day" title="Philadelphia, Pennsylvania Weather Forecast" target="_blank"><img src="http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=KPNE&ForcedCity=Philadelphia&ForcedState=PA&zip=19019&language=EN" alt="Find more about Weather in Philadelphia, PA" width="500" style="height:100px;"/></a><br><a href="http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:19019.1.99999&bannertypeclick=wu_clean2day" title="Get latest Weather Forecast updates" style="font-family: sans-serif; font-size: 12px" target="_blank"><%--Click for weather forecast</a></span>--%>

    <%--<script type="text/javascript">
        function ClosePS() {
            document.getElementById('divPSlite').style.display = 'none';
            document.getElementById('divPSfade').style.display = 'none';
        }

        function overlayPS() {
            document.getElementById('divPSlite').style.display = 'block';
            document.getElementById('divPSfade').style.display = 'block';
        }
</script>--%>





    <div class="user_panel">
        Welcome! <span>
            <asp:Label ID="lbluser" runat="server" Text="User"></asp:Label>
            <asp:Button ID="btnlogout" runat="server" Text="Logout" CssClass="cancel" ValidationGroup="header" OnClick="btnlogout_Click" />
        </span>&nbsp;<div class="clr">
        </div>
        <ul>
            <li><a href="<%= Page.ResolveUrl("~/Sr_App/home.aspx")%>">Home</a></li>
            <li>|</li>
            <li><a href="<%= Page.ResolveUrl("~/changepassword.aspx")%>">Change Password</a></li>
        </ul>
    </div>
    <!--nav section-->
    <div class="nav">
        <ul>
            <li><a href="home.aspx">Home</a></li>
            <li><a href="new_customer.aspx">Add Customer</a></li>
            <%-- <li><a href="view_customer.aspx">Review / Edit Customer Estimate</a></li>--%>
            <li><a href="ProductEstimate.aspx">Product Estimate</a></li>
            <li><a href="SalesReort.aspx">Sales Report</a></li>
            <%--<li><a href="Vendors.aspx">Vendor Master</a></li>--%>
            <li id="Li_Jr_app" runat="server" visible="true"><a href="~/home.aspx" runat="server"
                id="Jr_app">Junior App</a></li>
                <li id="Li_Installer" runat="server" visible="true"><a href="~/Installer/InstallerHome.aspx" runat="server"
                id="A1">Installer</a></li>
               
                 
            <%-- <li><a href="/EditUser.aspx" runat="server" id="edituser">EditUser</a></li>
  <li><a href="/Accounts/newuser.aspx" runat="server" id ="newuser">CreateUser</a></li>--%>
        </ul>
    </div>




</div>
