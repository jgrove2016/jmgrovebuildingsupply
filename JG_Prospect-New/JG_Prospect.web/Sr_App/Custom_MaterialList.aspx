<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Custom_MaterialList.aspx.cs" Inherits="JG_Prospect.Sr_App.Custom_MaterialList" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script runat="server">
        protected int i = 0;
        protected int GetSerialNumber()
        {
            i += 1;
            return i;
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    </script>
    <style>
        .grid td {
            padding: 1px !important;
            border-bottom: #ccc 1px solid;
        }

        #btnAddProdLines {
            width: 200px !important;
            padding: 0 10px !important;
        }

        #txtLine {
            width: 57px;
        }

        .text-style {
            height: 24px;
            width: 100%;
        }

        .chk-style {
            height: 24px;
            width: 70% !important;
        }

        div.dd_chk_select {
            height: 24px !important;
        }

        .floatRight {
            float: right;
            margin-top: -25px;
            z-index: 9999;
        }
        .form_panel2 {
    background: #efeeee url(../img/form-bg.png) repeat-x top;
    min-height: 200px;
    border-top: #000 10px solid;
    padding: 10px 5px 50px;
}
        .form_panel2 table tr td, .form_panel2 table tr td {
    background: url(../img/line.png) bottom repeat-x;
    padding: 10px 15px 12px 15px;
    line-height: 15px;
    min-height: 5px;
    vertical-align: top;
}
    </style>
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../js/jquery.Actual.min.js"></script>
    <script src="../js/jquery.multiselect.js"></script>

    <script type="text/javascript">

        function VerifyForemanManPwd() {
            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/VerifyForemanPermissionWB",
                data: "{'password':'" + document.getElementById('<%=txtForemanManPwd.ClientID %>').value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    alert(errorThrown);
                    $(errorlableid).show();
                    $(errorlableid).html("Error");
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "success") {
                            document.getElementById('lblForemanPermission').style.display = '';
                            document.getElementById('<%=txtForemanManPwd.ClientID %>').style.display = 'none';
                            document.getElementById('spnforemanelabel').style.display = 'none';
                            window.location = window.location.href;
                            // location.reload();
                        }
                        else {
                            alert(flg);
                        }
                    }
                }
            });
        }

        function VerifySalesManPwd() {
            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/VerifySrSalesmanPermissionFWB",
                data: "{'password':'" + document.getElementById('<%=txtSrSalesManPwd.ClientID %>').value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "success") {
                            document.getElementById('lblSalesmanPermission').style.display = '';
                            document.getElementById('<%=txtSrSalesManPwd.ClientID %>').style.display = 'none';
                            document.getElementById('spnsalesmanelabel').style.display = 'none';
                            window.location = window.location.href;
                            //location.reload();
                        }
                        else {
                            alert(flg);
                        }
                    }
                }
            });
        }

        function VerifyAdminPwd() {

            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/VerifyAdminPermissionWB",
                data: "{'password':'" + document.getElementById('<%=txtAdminPwd.ClientID %>').value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "success") {
                            document.getElementById('lblAdminPermission').style.display = '';
                            document.getElementById('<%=txtAdminPwd.ClientID %>').style.display = 'none';
                            document.getElementById('spnadminlabel').style.display = 'none';
                            window.location = window.location.href;
                            //location.reload();
                        }
                        else {
                            alert(flg);
                        }
                    }
                }
            });
        }

        function VerifySalesManPwd1() {
            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/VerifySrSalesmanPermissionAWB",
                data: "{'password':'" + document.getElementById('<%=txtSrSales1Pwd.ClientID %>').value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "success") {
                            document.getElementById('lblSrSalesmanPermission').style.display = '';
                            document.getElementById('<%=txtSrSales1Pwd.ClientID %>').style.display = 'none';
                            document.getElementById('spnsrsalesmanelabel').style.display = 'none';
                            window.location = window.location.href;
                            //location.reload();
                        }
                        else {
                            alert(flg);
                        }
                    }
                }
            });
        }

        function AllowInstaller(InstID, InstPwd) {
            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/AllowPermissionToInstaller",
                data: "{'pInstallerID':" + InstID + ", 'pPassword':'" + InstPwd.value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "1") {
                            alert('Installer\'s material list request is approved');
                            window.location = window.location.href;
                        }
                        else {
                            alert('Installer password is incorrect');
                            InstPwd.value = '';
                        }
                    }
                }
            });
        }
        function UpdateSpecificProdMat(pFldName, pFldVal, pID) {
            ShowProgress();
            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/UpdateSpecificProductLine",
                data: "{'pFieldName':'" + pFldName + "', 'pFieldValue':'" + escape(pFldVal) + "','pID':'" + pID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                    HideProgress();
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "1") {
                            HideProgress();
                        }
                    }
                }
            });
        }
        var onload = false;
        function SaveVendor(sender) {
            var lProdCatID = sender.parentElement.className;
            var lID = sender.parentElement.id;
            var excList = '';
            var lVendorIDs = '';
            var lExcVendorID = '';
            var lFirstServiceCall = true;
            var lSecondServiceCall = false;
            $("." + sender.parentElement.className).each(function (index, elem) {
                if (index == 0) {
                    $(elem).children().each(function (idx, elm) {
                        if (idx == 0) { //sender.id == elm.id && 
                            lFirstServiceCall = true;
                            if (elm.id.indexOf('lstVendor') > 0) {
                                for (var i = 0; i < elm.options.length; i++) {
                                    if (elm.options[i].selected == true) {
                                        lVendorIDs += (elm.options[i].value) + ",";
                                    }
                                }
                            }
                        }
                    });
                    if (lFirstServiceCall) {
                        $.ajax({
                            type: "POST",
                            url: "Custom_MaterialList.aspx/SaveVendorIds",
                            data: "{'pExcMaterialListId':'" + (excList == '' ? '0' : excList) + "', 'pProductCatID':'" + lProdCatID + "', 'pVendorIds': '" + lVendorIDs.substr(0, lVendorIDs.length - 1) + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "JSON",
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert('in error');
                                alert(errorThrown);
                            },
                            success: function (result) {
                                if (result != null) {
                                    var flg = (result.d);
                                    if (flg == "1") {
                                        // alert('Installer\'s material list request is approved');
                                        //window.location = window.location.href;
                                    }
                                    else {
                                        alert('Transaction failed');
                                        //InstPwd.value = '';
                                    }
                                }
                            }
                        });
                    }
                }
                else {
                    lExcVendorID = '';
                    var lstVendorTmp = null;
                    $(elem).children().each(function (idx, elm) {
                        lID = elm.parentElement.id;
                        if (elm.id.indexOf('lstVendor') > 0) {

                            for (var i = 0; i < elm.options.length; i++) {
                                if (elm.options[i].selected == true) {
                                    lExcVendorID += (elm.options[i].value) + ",";

                                }
                            }
                        }
                        if (elm.className.indexOf('ms-option') >= 0) {
                            lstVendorTmp = elm;
                        }
                        if (elm.id.indexOf('chkDefault') >= 0) {
                            if (!elm.checked) {
                                if (lstVendorTmp)
                                    lstVendorTmp.style.display = '';
                                $.ajax({
                                    type: "POST",
                                    url: "Custom_MaterialList.aspx/SaveVendorIdsForSpecificMaterial",
                                    data: "{'pMaterialListID':" + lID + ", 'pVendorIds':'" + lExcVendorID.substr(0, lExcVendorID.length - 1) + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "JSON",
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        alert(errorThrown);
                                    },
                                    success: function (result) {
                                        if (result != null) {
                                            var flg = (result.d);
                                            if (flg == "1") {

                                            }
                                            else {
                                                alert('Installer password is incorrect');
                                            }
                                        }
                                    }
                                });
                            }
                            else {
                                if (lstVendorTmp)
                                    lstVendorTmp.style.display = 'none';
                            }
                        }
                    });
                }
            });
        }

        function UpdateDefault(sender){
            var lProdCatID = sender.parentElement.className;
            var lID = sender.parentElement.id;

            $.ajax({
                type: "POST",
                url: "Custom_MaterialList.aspx/UpdateDefaultVendor",
                data: "{'pProductLineID':'" +lID + "', 'pProductCatID':'" + lProdCatID + "', 'pDefaultVendor': "+ sender.checked  +"}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                },
                success: function (result) {
                    if (result != null) {
                        var flg = (result.d);
                        if (flg == "1") {
                            var spanID = '';
                            var lnkID='';
                            $("#" + sender.parentElement.id).children().each(function (index, elem) {
                                if (elem.id.indexOf('lblVendorName') >= 0) {spanID=elem;}
                                if (elem.id.indexOf('lnkAddVen') >= 0) {lnkID=elem;}
                                if (elem.id.indexOf('chkDefault') >= 0) {
                                    spanID.style.display = (elem.checked?'none':'');
                                    lnkID.style.display = (elem.checked?'none':'');
                                }
                            });
                        }
                        else {
                            alert('Transaction failed');
                        }
                    }
                }
            });
        }

        function AssociateVendor(sender) {
            var lProdCatID = <%=ProductCategoryID%> ; 
             var lID = <%=ProductLineID %>; 
             var excList = '';
             var lVendorIDs = '';
             var lExcVendorID = '';
             var lFirstServiceCall = true;
             var lSecondServiceCall = false;
             for (var i = 0; i < sender.options.length; i++) {
                 if (sender.options[i].selected == true) {
                     lVendorIDs += (sender.options[i].value) + ",";
                 }
             }
            //Int32 pProductCatID, String pVendorIds, Int32 pProductLineID
             $.ajax({
                 type: "POST",
                 url: "Custom_MaterialList.aspx/AssociateVendorToCat",
                 data: "{'pProductLineID':'" +lID + "', 'pProductCatID':'" + lProdCatID + "', 'pVendorIds': '" + lVendorIDs.substr(0, lVendorIDs.length - 1) + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "JSON",
                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                     alert('in error');
                     alert(errorThrown);
                 },
                 success: function (result) {
                     if (result != null) {
                         var flg = (result.d);
                         if (flg == "1") {
                             // alert('Installer\'s material list request is approved');
                             //window.location = window.location.href;
                         }
                         else {
                             alert('Transaction failed');
                             //InstPwd.value = '';
                         }
                     }
                 }
             });
         }
        function endRequestHandler() {
            try {
                if (g_CurrentTextBox != null) {

                    $get(g_CurrentTextBox).focus();
                    $get(g_CurrentTextBox).select();
                }
            }
            catch (e) { }
        }

        function TransformList() {
            $('.form-control').multiselect({ columns: 2, placeholder: 'Select options', search: true });
        }

        function ShowAttachQuotes(ProdCatID, VendorID){
            document.getElementById('<%=hdnProdCatID.ClientID%>').value = ProdCatID;
            document.getElementById('<%=hdnVendorID.ClientID%>').value = VendorID;
            $find('<%=mpAttachPurchaseOrder.ClientID %>').show();
        }

        function HideAttachQuotes(){
            $find('<%=mpAttachPurchaseOrder.ClientID %>').hide();
        }

    </script>
    <style type="text/css">
        .dd_chk_select {
            width: 180px;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 250px 100px 250px 450px;
            padding: 10px;
            width: 120px;
            height: 120px;
            background-color: White;
            border-radius: 10px;
            background: url(../img/loader.gif) no-repeat;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

        ul, li {
            margin: 0;
            padding: 0;
            list-style: none;
            max-height: 300px;
        }

        .label {
            color: #000;
            font-size: 16px;
        }

        .grid td .container {
            max-width: 728px;
            margin-top: 150px;
            max-height: 250px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="cover" class="modal">
        <div id="dvLoader" class="center">&nbsp;</div>

    </div>
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <h1 id="h1Heading" runat="server">Material List</h1>

        <table width="100%">
            <tr id="trUpdatedRow">
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td valign="top">
                    <fieldset style="border-style: solid; border-width: 1px; padding: 5px;">
                        <legend>Job Details</legend>
                        <b>Job ID: </b><%=ElabJobID %><br />
                        <b>Customer Name:</b> <%=CustomerName %><br />
                    </fieldset>
                </td>
                <td>&nbsp;</td>
                <td valign="top">
                    <fieldset style='border-style: solid; border-width: 1px; padding: 5px; display: <%=(StaffID!=0?"":"none") %>'>
                        <legend>Last Edited By</legend>
                        <b>Staff Internal ID:</b> <%=StaffID %>
                        <br />
                        <b>Staff Name:</b>  <%=StaffName %>
                        <br />
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <asp:Panel ID="pnlForeman" runat="server">
                        <asp:LinkButton ID="lnkForemanPermission" runat="server" Text="Foreman Permission" Visible="false"></asp:LinkButton>
                        <span id="spnforemanelabel" style='display: <%=(ForemanPwdVisibility==""?"none":"")%>'>Foreman Password:</span>
                        <asp:TextBox ID="txtForemanManPwd" runat="server" onblur="VerifyForemanManPwd()"></asp:TextBox>
                        <span id="lblForemanPermission" style='display: <%=ForemanPwdVisibility%>'><%=ForemanMessage %></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlAdmin" runat="server">
                        <asp:LinkButton ID="lnkAdminPermission" runat="server" Text="Admin Permission" Visible="false"></asp:LinkButton>
                        <span id="spnadminlabel" style='display: <%=(AdminPwdVisibility==""?"none":"")%>'>Admin Password:</span>
                        <asp:TextBox ID="txtAdminPwd" runat="server" onblur="VerifyAdminPwd()"></asp:TextBox>
                        <span id="lblAdminPermission" style='display: <%=AdminPwdVisibility%>'><%=AdminMessage %></span>
                    </asp:Panel>

                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>

                <td>
                    <asp:Panel ID="pnlSrSalesman" runat="server">
                        <asp:LinkButton ID="lnkSrSalesmanPermissionA" runat="server" Text="Sr. Salesman Permission 1" Visible="false"></asp:LinkButton>
                        <span id="spnsrsalesmanelabel" style='display: <%=(SrSalesmanPwdVisibility==""?"none":"")%>'>Sr. Salesman Password:</span>
                        <asp:TextBox ID="txtSrSales1Pwd" runat="server" onblur="VerifySalesManPwd1()"></asp:TextBox>
                        <span id="lblSrSalesmanPermission" style='display: <%=SrSalesmanPwdVisibility%>'><%=SrSalesManMessage %></span>
                    </asp:Panel>

                    <asp:Panel ID="pnlSalesF" runat="server">
                        <asp:LinkButton ID="lnkSrSalesmanPermissionF" runat="server" Text="Sr. Salesman Permission 2 " Visible="false"></asp:LinkButton>
                        <span id="spnsalesmanelabel" style='display: <%=(SalesmanPwdVisibility==""?"none":"")%>'>Sr. Salesman Password:</span>
                        <asp:TextBox ID="txtSrSalesManPwd" runat="server" onblur="VerifySalesManPwd()"></asp:TextBox>
                        <span id="lblSalesmanPermission" style='display: <%=SalesmanPwdVisibility%>'><%=SalesmanMessage %></span>
                    </asp:Panel>
                </td>
                <td rowspan="2" align="right">
                    <fieldset style="border-style: solid; border-width: 1px; padding: 5px;">
                        <legend>Add Installer</legend>


                        <asp:DropDownList ID="ddlInstaller" runat="server">
                        </asp:DropDownList>
                        <span class="btn_sec">
                            <asp:Button ID="btnAddInstaller" runat="server" OnClick="btnAddInstaller_Click" Text="Add Installer" /></span>
                        <div>
                            <asp:Repeater ID="rptInstaller" runat="server" OnItemDataBound="rptInstaller_ItemDataBound" OnItemCommand="rptInstaller_ItemCommand">
                                <HeaderTemplate>
                                    <table class="grid">
                                        <tr>
                                            <th>#</th>
                                            <th>Installer Name</th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString() %></td>
                                        <td><%#Eval("QualifiedName") %></td>
                                        <td>
                                            <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkDeleteInstaller" CommandArgument='<%#Eval("ID") %>' CommandName="DeleteInstaller" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>

                                        </td>
                                    </tr>


                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>

                            </asp:Repeater>
                        </div>
                    </fieldset>
                </td>
            </tr>

        </table>
        <asp:HiddenField ID="hdnAdmin" runat="server" />
        <asp:HiddenField ID="hdnForeman" runat="server" />
        <asp:HiddenField ID="hdnSrA" runat="server" />
        <asp:HiddenField ID="hdnSrF" runat="server" />

        <div class="grid" style="display: none">
            <fieldset>
                <legend>Material requested by Installer</legend>
                <asp:ListView ID="lstRequestedMaterial" runat="server" ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder">
                    <LayoutTemplate>
                        <div>
                            <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                        </div>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>

                    </GroupTemplate>
                    <ItemTemplate>
                        <h3 align="left">Product Category: 
                                <asp:Label ID="lblProductCategory" runat="server" Text="Label"></asp:Label>
                            <%--<asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />--%>
                            <div style="clear: both"></div>
                        </h3>
                    </ItemTemplate>
                </asp:ListView>
            </fieldset>
        </div>
        <div class="grid">
            <%-- <asp:UpdatePanel ID="updMaterialList" runat="server">
                <ContentTemplate>--%>
            <div class="btn_sec">
                Select Product Category:
                        <asp:DropDownList ID="ddlCategoryH" Width="150px" runat="server">
                        </asp:DropDownList>
                <asp:Button ID="btnAddProdLines" runat="server" Text="Add Product Category" OnClick="btnAddProdLines_Click" />

            </div>

            <asp:ListView ID="lstCustomMaterialList" OnItemCommand="lstCustomMaterialList_ItemCommand" runat="server" OnItemDataBound="lstCustomMaterialList_ItemDataBound" ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder">
                <LayoutTemplate>
                    <div>
                        <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                <GroupTemplate>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>

                </GroupTemplate>
                <ItemTemplate>
                    <asp:UpdatePanel ID="updMaterialList2" runat="server">
                        <ContentTemplate>
                            <div style="padding-bottom: 2px;">
                                <div style="float: left" align="left">
                                    
                                   <%# ToRoman( GetSerialNumber())%>. Product Category: 
                                            <asp:DropDownList ID="ddlCategory" Width="150px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                    <asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                    <asp:LinkButton ID="lnkAddProdCat" Visible="false" OnClick="lnkAddProdCat_Click" runat="server">Add</asp:LinkButton>
                                    <asp:LinkButton ID="lnkDeleteProdCat" CommandArgument='<%#Eval("ProductCatID") %>' OnClick="lnkDeleteProdCat_Click" runat="server" OnClientClick="return confirm('Deleting product category will delete all associated line items. Are you sure you want to delete?')">Delete</asp:LinkButton>

                                </div>
                       
                                <div style="float: right; width: 34%; display:none">
                                    Vendor Category:
                                            <asp:DropDownList ID="dldVendorCategory" Width="70%" AutoPostBack="false" OnSelectedIndexChanged="dldVendorCategory_SelectedIndexChanged" runat="server"></asp:DropDownList><br />
                                    <asp:RadioButton ID="rdoManufacturer" GroupName="VendorType" AutoPostBack="false" OnCheckedChanged="rdoManufacturer_CheckedChanged" Text="Manufacturer" runat="server" />
                                    <asp:RadioButton ID="rdoWholeSaler" GroupName="VendorType" AutoPostBack="false" OnCheckedChanged="rdoWholeSaler_CheckedChanged" Checked="true" Text="Wholesaler / Retailer" runat="server" />
                                </div>
                                <div style="clear: both"></div>
                            </div>

                            <asp:GridView ID="grdProdLines" Width="100%" runat="server" OnRowDataBound="grdProdLines_RowDataBound" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Line" HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="text-style" ID="txtLine" Text='<%# Eval("Line") %>' MaxLength="4" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>' />
                                            <asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>' />
                                            <asp:HiddenField ID="hdnForemanPermission" runat="server" Value='<%#Eval("IsForemanPermission")%>' />
                                            <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>' />
                                            <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>' />
                                            <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>' />
                                            <asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                            <asp:HiddenField ID="hdnVendorIDs" runat="server" Value='<%#Eval("VendorIDs")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JG sku- vendor part #" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSkuPartNo" CssClass="text-style" Text='<%# Eval("JGSkuPartNo") %>' MaxLength="18" runat="server" onblur="UpdateSpecificProdMat('JGSkuPartNo',this.value,this.getAttribute('materialid'));" materialid='<%#Eval("Id") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" CssClass="text-style" Text='<%# Eval("MaterialList") %>' onblur="UpdateSpecificProdMat('MaterialList',this.value,this.getAttribute('materialid'));" materialid='<%#Eval("Id") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQTY" runat="server" CssClass="text-style" MaxLength="4" onblur="UpdateSpecificProdMat('Quantity',this.value,this.getAttribute('materialid'));" materialid='<%#Eval("Id") %>' Text='<%# Eval("Quantity") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUOM" runat="server" CssClass="text-style" onblur="UpdateSpecificProdMat('UOM',this.value,this.getAttribute('materialid'));" materialid='<%#Eval("Id") %>' onfocus="document.getElementById('__LASTFOCUS').value=this.id;" Text='<%# Eval("UOM") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost" HeaderStyle-Width="5%" ItemStyle-Width="5%"><%--Material Cost Per Item--%>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMaterialCost" onblur="UpdateSpecificProdMat('MaterialCost',this.value,this.getAttribute('materialid'));" materialid='<%#Eval("Id") %>' CssClass="text-style" onfocus="document.getElementById('__LASTFOCUS').value=this.id;" Text='<%# Eval("MaterialCost") %>' runat="server" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Extended" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExtended" onblur="UpdateSpecificProdMat('extend',this.value,this.getAttribute('materialid'));" materialid='<%#Eval("Id") %>' runat="server" onfocus="document.getElementById('__LASTFOCUS').value=this.id;" CssClass="text-style" Text='<%# Eval("Extend") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Quotes/Invoice" Visible="true" HeaderStyle-Width="250px" ItemStyle-Width="250px" ItemStyle-Height="20px">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updVend" runat="server">
                                                <ContentTemplate>
                                                    <div id='<%#Eval("id")   %>' class='<%#Eval("ProductCatID")%>'>
                                                        <%--<asp:ListBox onchange="SaveVendor(this)" ID="lstVendorName" Width="25%" SelectionMode="Multiple" CssClass="form-contr2ol" runat="server"></asp:ListBox>--%>
                                                        <asp:Label ID="lblVendorNames" runat="server" Text=""></asp:Label>
                                                        <asp:LinkButton ID="lnkAddVendors" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="AddVendor" OnClick="lnkAddVendors_Click">+ Add</asp:LinkButton>
                                                        <asp:CheckBox onclick="UpdateDefault(this);" ID="chkDefault" Text="Default" runat="server" />
                                                        <asp:LinkButton ID="lnkaddvendorquotes" runat="server" Text="Attach Quotes" OnClick="lnkaddvendorquotes_Click" HeaderStyle-Width="200px">Attach Quotes</asp:LinkButton>
                                                        
                                                    </div>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDeleteLineItems" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="DeleteLine" OnClick="lnkDeleteLineItems_Click">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" Visible="false">
                                        <ItemTemplate>


                                            <%--<asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>' ClientIDMode="Static"></asp:Label>--%>
                                            <asp:LinkButton ID="lblTotal" data-toggle="modal" data-target="#myModal" runat="server" Text='<%# Eval("Total") %>' ClientIDMode="Static"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkAttachQuotes" Text="Attach Quotes" runat="server" OnClick="lnkAttachQuotes_Click" ClientIDMode="Static"></asp:LinkButton>



                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinkButton ID="lnkAddLines" CommandName="AddLine" CommandArgument='<%#Eval("ProductCatId") %>' OnClick="lnkAddLines_Click1" runat="server">Add Line</asp:LinkButton>
                            <fieldset style="border: 1px solid">
                                <legend>Attachments</legend>
                                <div style="float: left; width: 40%;">
                                    Attach File:
                                            <asp:FileUpload ID="flMaterialList" runat="server" class="multi" />
                                    <div class="btn_sec">
                                        <asp:Button ID="btnAttachFile" runat="server" Text="Attach" OnClick="btnAttachFile_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 30%; vertical-align: top; padding-top: 0px;">
                                    <h5 style="margin-top: 0px;">Email Attachments:</h5>
                                    <asp:DataList ID="grdAttachment" runat="server" RepeatLayout="Table" RepeatColumns="3">
                                        <ItemTemplate>
                                            <span style="width: 250px; background-color: #f8f5f5; padding: 5px;">
                                                <a href='<%#Eval("DocumentPath") %>' target="_blank">
                                                    <%#Eval("DocumentName") %>
                                                </a>&nbsp;<asp:LinkButton ID="lnkDeleteMatLisAttc" OnClick="lnkDeleteMatLisAttc_Click" CommandArgument='<%#Eval("Id") %>' runat="server" Text='Delete'></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                    </asp:DataList>

                                </div>
                                <div style="float: left; width: 30%; vertical-align: top; padding-top: 0px;">
                                    <h5 style="margin-top: 0px;">Attached Quotes:</h5>
                                    <asp:DataList ID="grdPurchaseOrder" runat="server" RepeatLayout="Table" RepeatColumns="3">
                                        <ItemTemplate>
                                            <span style="width: 250px; background-color: #f8f5f5; padding: 5px;">
                                                <a href='<%#Eval("DocumentPath") %>' target="_blank">
                                                    <%#Eval("DocumentName") %>
                                                </a>&nbsp;<asp:LinkButton ID="lnkDeleteMatLisAttc" OnClick="lnkDeleteMatLisAttc_Click" CommandArgument='<%#Eval("Id") %>' runat="server" Text='Delete'></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                    </asp:DataList>

                                </div>
                            </fieldset>
                            <div class="btn_sec">
                                <asp:Button ID="btnSendEmailToVendorsForProd" runat="server" CommandArgument='<%#Eval("ProductCatId") %>' Text="Send Mail to Vendors" OnClick="btnSendEmailToVendorsForProd_Click" OnClientClick="return ValidatePermissions()" />
                            </div>
                            <hr style="border: none; background: #ccc; height: 2px; margin-top: 10px; margin-bottom: 20px" />

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAttachFile" />
                            <asp:PostBackTrigger ControlID="btnSendEmailToVendorsForProd" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ItemTemplate>
            </asp:ListView>

            <asp:GridView ID="grdcustom_material_list" runat="server" Width="108%" AutoGenerateColumns="false" Visible="false"
                OnRowDataBound="grdcustom_material_list_RowDataBound" OnRowDeleting="grdcustom_material_list_RowDeleting" OnRowCommand="grdcustom_material_list_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No.">
                        <ItemTemplate>
                            <asp:Label ID="lblsrno" Text="0" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>' />
                            <asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>' />
                            <asp:HiddenField ID="hdnForemanPermission" runat="server" Value='<%#Eval("IsForemanPermission")%>' />
                            <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>' />
                            <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>' />
                            <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material List">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMateriallist" Text='<%#Eval("MaterialList")%>' TextMode="MultiLine" AutoPostBack="true" OnTextChanged="txtMateriallist_TextChanged"
                                runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor Category">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlVendorCategory" ClientIDMode="Static" runat="server" Width="150px"
                                OnSelectedIndexChanged="ddlVendorCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="requiredvendorcategory" Display="Dynamic" runat="server"
                                InitialValue="0" ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlVendorCategory">
                            </asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor Name">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlVendorName" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged"
                                ClientIDMode="Static" runat="server" Width="150px" Enabled="false" AutoPostBack="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attach Quote">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAttachQuote" runat="server" Text='Attach Quote' CommandArgument='<%#Eval("TempName") %>' CommandName="Attach Quote"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quote">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkQuote" runat="server" Text='<%#Eval("DocName")%>' CommandArgument='<%#Eval("TempName") %>' CommandName="View"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount($)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text="0.00" onkeypress="return isNumericKey(event);" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"
                                MaxLength="15" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="Add_Click"></asp:LinkButton>
                            <label>
                                &nbsp;</label>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("Id")%>' Text="Delete"></asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>

        <div class="btn_sec">
            <asp:Button ID="btnSendMail" runat="server" Text="Save" OnClick="btnSendMail_Click" OnClientClick="return ValidatePermissions()"
                Style="background: url(../img/btn1.png) no-repeat;" Width="300" Visible="false" />
            <asp:Button ID="btnSendEmailToVendors" runat="server" Text="Send Mail to All Vendors" OnClick="btnSendEmailToVendors_Click" OnClientClick="return ValidatePermissions()" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
        </div>
        <div class="form_panel2">
            <button id="btnFake" style="display: none" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpVendorCat" runat="server" TargetControlID="btnFake"
                PopupControlID="pnlVendorCategory" CancelControlID="btnFake">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlVendorCategory" runat="server" BackColor="White"  Width="750px" Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                <%--<asp:UpdatePanel ID="updVendorCategoryFilter" runat="server">
                    <ContentTemplate>--%>
                        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="10"
                                                    cellspacing="0">
                            <tr style="background-color: #A33E3F">
                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                    align="center">Add Vendors to Product Line
                                </td>
                            </tr>                            
                            <tr>
                                <td align="right">
                                    Client Type:
                                </td>
                                <td>
                                    <asp:RadioButton ID="optManf" GroupName="VendorTypeA" Width="120px" AutoPostBack="true" OnCheckedChanged="optManf_CheckedChanged" Text="Manufacturer" runat="server" />
                                    <asp:RadioButton ID="optWholeSaler" GroupName="VendorTypeA" Width="120px" AutoPostBack="true" OnCheckedChanged="optWholeSaler_CheckedChanged" Checked="true" Text="Retail/Wholesale" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vendor Category:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpVendorCat" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="drpVendorCat_SelectedIndexChanged" runat="server"></asp:DropDownList><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vendor Sub Category:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpVendorSubCat" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="drpVendorSubCat_SelectedIndexChanged" runat="server"></asp:DropDownList><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Vendor:</td>
                                <td>
                                    <asp:ListBox onchange="AssociateVendor(this)" ID="lstVendors" Width="500px" SelectionMode="Multiple" CssClass="form-control" runat="server"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Qutoes requested from:</td>
                                <td>
                                    <asp:Label ID="lblGotQuotesFrom" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right"><asp:Button ID="btnCancel" runat="server" Style="width: 100px;" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="" /></td>
                            </tr>
                        </table>
                  <%--  </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpVendorCat" />
                        <asp:AsyncPostBackTrigger ControlID="drpVendorSubCat" />
                        <%--<asp:AsyncPostBackTrigger ControlID="optManf" />
                        <asp:AsyncPostBackTrigger ControlID="optWholeSaler" />--% >
                    </Triggers>
                </asp:UpdatePanel>--%>
                
            </asp:Panel>
        </div>

        <div class="form_panel2">
            <ajaxToolkit:ModalPopupExtender ID="mpAttachPurchaseOrder" runat="server" TargetControlID="btnFake"
                PopupControlID="pnlAttachPurchaseOrder" CancelControlID="btnFake">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlAttachPurchaseOrder" runat="server" BackColor="White"  Width="750px" Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="10" cellspacing="0">
                    <tr style="background-color: #A33E3F">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                            align="center">Attach Quotes
                        </td>
                    </tr>                            
                    <tr>
                        <td align="right">
                            Attach Quotes:
                        </td>
                        <td>
                            <asp:FileUpload ID="flUploadPurchaseOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnUploadPO" runat="server" Text="Attach" OnClick="btnUploadPO_Click" />
                            <asp:HiddenField ID="hdnProdCatID" runat="server" />
                            <asp:HiddenField ID="hdnVendorID" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>

    <script type="text/javascript">

        function ValidatePermissions() {
            var lIsValidated = true;
            if (document.getElementById('spnforemanelabel')) {
                if (document.getElementById('spnforemanelabel').style.display == '') {
                    lIsValidated = false;
                }
            }
            if (document.getElementById('spnsalesmanelabel')) {
                if (document.getElementById('spnsalesmanelabel').style.display == '') {
                    lIsValidated = false;
                }
            }
            /* if (document.getElementById('spnsrsalesmanelabel')) {
                 if (document.getElementById('spnsrsalesmanelabel').style.display == '') {
                     lIsValidated = false;
                 }
             }
             if (document.getElementById('spnadminlabel')) {
                 if (document.getElementById('spnadminlabel').style.display == '') {
                     lIsValidated = false;
                 }
             }*/
            if (!lIsValidated) {
                alert('Please approve the custom material list first.');
            }
            else{
                document.getElementById('cover').style.display = '';
                document.getElementById('dvLoader').style.display = '';
            }
            return lIsValidated;
        }

        function ShowProgress() {
            document.getElementById('cover').style.display = '';
            document.getElementById('dvLoader').style.display = '';
            setTimeout(function () { HideProgress(); }, 5000);
        }

        function HideProgress() {
            document.getElementById('cover').style.display = 'none';
            document.getElementById('dvLoader').style.display = 'none';

        }

        function jsFunctions() {
            try {

                HideProgress();
                endRequestHandler();
                document.getElementById(document.getElementById("__LASTFOCUS").value).focus();
            }
            catch (e) {
                HideProgress();
            }

        }

        TransformList();
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (sender, e) {
            setTimeout(function () {
                TransformList();
                // $('select[multiple]').multiselect('reload');
            }, 1000);
            jsFunctions();
        });
        HideProgress();
        onload = true;
    </script>


        <ajaxToolkit:ModalPopupExtender ID="popupAdmin_permission" TargetControlID="lnkAdminPermission"
            runat="server" CancelControlID="btnCloseAdmin" PopupControlID="pnlpopup">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="175px" Width="300px"
            Style="z-index: 111; background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">Admin Verification
                        <asp:Button ID="btnXAdmin" runat="server" OnClick="btnXAdmin_Click" Text="X" Style="float: right; text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="LabelValidate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Admin Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVAdmin" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnVerifyAdmin" runat="server" Text="Verify" OnClick="VerifyAdminPermission" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseAdmin" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popupSrSalesmanPermissionA" TargetControlID="lnkSrSalesmanPermissionA"
            runat="server" CancelControlID="btnCloseSrSalesmanA" PopupControlID="pnlSrSalesmanPermissionA">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlSrSalesmanPermissionA" runat="server" BackColor="White" Height="175px"
            Width="300px" Style="z-index: 111; background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">Sr. Salesman Verification
                        <asp:Button ID="btnXSrSalesmanA" runat="server" OnClick="btnXSrSalesmanA_Click" Text="X"
                            Style="float: right; text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Sr. Salesman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSrSalesmanPasswordA" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVSrSalesmanA" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnVerifySrSalesmanA" runat="server" Text="Verify" OnClick="VerifySrSalesmanPermissionA" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseSrSalesmanA" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popupForeman_permission" TargetControlID="lnkForemanPermission"
            runat="server" CancelControlID="btnCloseForeman" PopupControlID="pnlForemanPermission">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlForemanPermission" runat="server" BackColor="White" Height="175px"
            Width="300px" Style="z-index: 111; background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">Foreman Verification
                        <asp:Button ID="btnXForeman" runat="server" OnClick="btnXForeman_Click" Text="X"
                            Style="float: right; text-decoration: none" /><%--<a id="A1" style="color: white; float: right; text-decoration: none"
                            class="btnClose" href="#">X</a>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Foreman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtForemanPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblErrorForeman" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVForeman" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnVerifyForeman" runat="server" Text="Verify" OnClick="VerifyForemanPermission" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseForeman" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popupSrSalesmanPermissionF" TargetControlID="lnkSrSalesmanPermissionF"
            runat="server" CancelControlID="btnCloseSrSalesmanF" PopupControlID="pnlSrSalesManPermissionF">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlSrSalesManPermissionF" runat="server" BackColor="White" Height="175px"
            Width="300px" Style="z-index: 111; background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">Sr. Salesman Verification
                        <asp:Button ID="btnXSrSalesmanF" runat="server" OnClick="btnXSrSalesmanF_Click" Text="X"
                            Style="float: right; text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label4" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Sr. Salesman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSrSalesmanPasswordF" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVSrSalesmanF" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnVerifySrSalesmanF" runat="server" Text="Verify" OnClick="VerifySrSalesmanPermissionF" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseSrSalesmanF" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>
