<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Custom_MaterialList.aspx.cs" Inherits="JG_Prospect.Sr_App.Custom_MaterialList" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        div.dd_chk_select {
            height: 24px !important;
        }
    </style>
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
        function endRequestHandler() {
            try {
                if (g_CurrentTextBox != null) {
                
                    $get(g_CurrentTextBox).focus();
                    $get(g_CurrentTextBox).select();
                }
            }
            catch(e){}
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
        </table>
        <asp:HiddenField ID="hdnAdmin" runat="server" />
        <asp:HiddenField ID="hdnForeman" runat="server" />
        <asp:HiddenField ID="hdnSrA" runat="server" />
        <asp:HiddenField ID="hdnSrF" runat="server" />


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
        <div class="grid" style="display:none">
            <fieldset >
                <legend>Material requested by Installer</legend>
                <asp:ListView ID="lstRequestedMaterial" runat="server"  ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder">
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
            <asp:UpdatePanel ID="updMaterialList" runat="server">
                <ContentTemplate>
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
                                    <h3 align="left">Product Category: 
                                        <asp:DropDownList ID="ddlCategory" Width="150px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                        <asp:LinkButton ID="lnkAddProdCat" Visible="false" OnClick="lnkAddProdCat_Click" runat="server">Add</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDeleteProdCat" CommandArgument='<%#Eval("ProductCatID") %>' OnClick="lnkDeleteProdCat_Click" runat="server" OnClientClick="return confirm('Deleting product category will delete all associated line items. Are you sure you want to delete?')">Delete</asp:LinkButton>
                                        <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("ProductCatID") %>' onclick="btnDelete_Click" OnClientClick="return confirm('Deleting product category will delete all associated line items. Are you sure you want to delete?')" />--%>
                                        <div style="clear: both"></div>
                                    </h3>



                                    <asp:GridView ID="grdProdLines" Width="100%" runat="server" OnRowDataBound="grdProdLines_RowDataBound" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Line" HeaderStyle-Width="4%">
                                                <ItemTemplate>
                                                    <asp:TextBox CssClass="text-style" ID="txtLine" Text='<%# Eval("Line") %>' MaxLength="4" runat="server" ClientIDMode="Static" OnTextChanged="txtLine_TextChanged" Enabled="false"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>' />
                                                    <asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>' />
                                                    <asp:HiddenField ID="hdnForemanPermission" runat="server" Value='<%#Eval("IsForemanPermission")%>' />
                                                    <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>' />
                                                    <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>' />
                                                    <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>' />
                                                    <asp:HiddenField ID="hdnProductCatID" runat="server" Value='<%#Eval("ProductCatID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JG sku- vendor part #">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updSku" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtSkuPartNo" CssClass="text-style" Text='<%# Eval("JGSkuPartNo") %>' MaxLength="18" runat="server" onfocus="document.getElementById('__LASTFOCUS').value=this.id;"  OnTextChanged="txtSkuPartNo_TextChanged" onblur="ShowProgress()" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtSkuPartNo" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updDesc" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtDescription" CssClass="text-style" Text='<%# Eval("MaterialList") %>' onblur="ShowProgress()" runat="server" onfocus="document.getElementById('__LASTFOCUS').value=this.id;"  OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtDescription" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updQty" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtQTY" runat="server" CssClass="text-style" MaxLength="4" onblur="ShowProgress()" onfocus="document.getElementById('__LASTFOCUS').value=this.id;" Text='<%# Eval("Quantity") %>' onkeypress="return isNumberKey(event)" OnTextChanged="txtQTY_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtQTY" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updUOM" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtUOM" runat="server" CssClass="text-style" onblur="ShowProgress()" onfocus="document.getElementById('__LASTFOCUS').value=this.id;"  Text='<%# Eval("UOM") %>' OnTextChanged="txtUOM_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtUOM" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost"><%--Material Cost Per Item--%>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updMC" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtMaterialCost" onblur="ShowProgress()" CssClass="text-style" onfocus="document.getElementById('__LASTFOCUS').value=this.id;"  AutoPostBack="true" Text='<%# Eval("MaterialCost") %>' OnTextChanged="txtMaterialCost_TextChanged" runat="server"  onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtMaterialCost" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Extended">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updExt" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="txtExtended" onblur="ShowProgress()" runat="server" onfocus="document.getElementById('__LASTFOCUS').value=this.id;" CssClass="text-style"  Text='<%# Eval("Extend") %>' OnTextChanged="txtExtended_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtExtended" EventName="TextChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor Quotes/Invoice" Visible="true">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updVend" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dldVendorCategory" AutoPostBack="true" OnSelectedIndexChanged="dldVendorCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                            <asp:RadioButton ID="rdoManufacturer" GroupName="VendorType" AutoPostBack="true" OnCheckedChanged="rdoManufacturer_CheckedChanged" Text="Manufacturer" runat="server" />
                                                            <asp:RadioButton ID="rdoWholeSaler"  GroupName="VendorType" AutoPostBack="true" OnCheckedChanged="rdoWholeSaler_CheckedChanged" Checked="true" Text="Wholesaler / Retailer" runat="server" />
                                                            <asp:DropDownCheckBoxes ID="ddlVendorName" onblur="ShowProgress()" CssClass="text-style" ClientIDMode="AutoID" EnableViewState="true" runat="server" Style="margin: -2em 0 0;" UseSelectAllNode="true" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged1" AutoPostBack="true">
                                                            </asp:DropDownCheckBoxes>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlVendorName" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="dldVendorCategory" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="rdoManufacturer" EventName="CheckedChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="rdoWholeSaler" EventName="CheckedChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
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
                                    

                                </ContentTemplate>

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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="btn_sec">
            <asp:Button ID="btnSendMail" runat="server" Text="Save" OnClick="btnSendMail_Click" OnClientClick="return ValidatePermissions()"
                Style="background: url(../img/btn1.png) no-repeat;" Width="300" Visible="false" />
            <asp:Button ID="btnSendEmailToVendors" runat="server" Text="Send Mail to Vendors" OnClick="btnSendEmailToVendors_Click" OnClientClick="return ValidatePermissions()" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
        </div>
        <h1>Edit Email Templates</h1>
        <div>
            <label>
                &nbsp;</label>
            <%--<a href="EmailTemplateForVendorCategories.aspx" title="Edit Email Template for vendor category">
               Edit Email Template for vendor category</a>--%>
            <asp:LinkButton runat="server" ID="lnkVendorCategory" OnClick="lnkVendorCategory_Click"
                CausesValidation="false">Edit Email Template for vendor category</asp:LinkButton>
            <br />
            <label>
                &nbsp;</label>
            <%-- <a href="EmailTemplateForVendorCategories.aspx" title="Edit Email Template for vendor">
                Edit Email Template for vendor</a>--%>
            <asp:LinkButton runat="server" ID="lnkVendor" OnClick="lnkVendor_Click" CausesValidation="false">Edit Email Template for vendor</asp:LinkButton>
            <%-- <iframe src="EmailTemplateForVendorCategories.aspx" width="100%" height="100%" id="ifEmailTemplate" runat="server">
        </iframe>--%>
            <asp:Panel ID="pnlEmailTemplateForVendorCategories" runat="server">
                <h2 style="text-align: center">
                    <b>Email Template For Vendor Category</b></h2>
                <div>
                    <h2>Subject:
                        <asp:TextBox ID="txtVendorSubject" Width="500px" runat="server"></asp:TextBox></h2>
                    <div>
                        Attach File:
                        <asp:FileUpload ID="flVendCat" runat="server" class="multi" />
                        <asp:GridView ID="grdVendCatAtc" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" CellSpacing="22">
                            <Columns>
                                <asp:BoundField DataField="DocumentName" HeaderText="File Name" />
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hypDownload" Target="_blank" NavigateUrl='<%#Eval("DocumentPath") %>' runat="server">Download</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%#Eval("Id") %>'
                                            runat="server" OnClick="DeleteFile" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <h2>Header Template</h2>
                    <cc1:Editor ID="HeaderEditor" Width="1000px" Height="200px" runat="server" />
                    <h2>Body Template</h2>
                    <asp:Label ID="lblMaterials" runat="server"></asp:Label>
                    <h2>Footer Template</h2>
                    <cc1:Editor ID="FooterEditor" Width="1000px" Height="200px" runat="server" />

                </div>
                <br />
                <br />
                <div class="btn_sec">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlEmailTemplateForVendors" runat="server" Visible="false">
            <h2 style="text-align: center">
                <b>Email Template For Vendors</b></h2>
            <div>
                <h2>Subject:
                    <asp:TextBox ID="txtSubject" Width="500px" runat="server"></asp:TextBox></h2>
                <div>
                    Attach File:
                    <asp:FileUpload ID="flVend" runat="server" class="multi" />
                    <asp:GridView ID="grdVendAtc" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" CellSpacing="22">
                        <Columns>
                            <asp:BoundField DataField="DocumentName" HeaderText="File Name" />
                            <asp:TemplateField HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypDownload" Target="_blank" NavigateUrl='<%#Eval("DocumentPath") %>' runat="server">Download</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%#Eval("Id") %>'
                                        runat="server" OnClick="DeleteFile" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <h2>Header Template</h2>
                <cc1:Editor ID="HeaderEditorVendor" Width="1000px" Height="200px" runat="server" />
                <h2>Body Template</h2>
                <asp:Label ID="lblMaterialsVendor" runat="server"></asp:Label>
                <h2>Footer Template</h2>
                <cc1:Editor ID="FooterEditorVendor" Width="1000px" Height="200px" runat="server" />


            </div>
            <br />
            <br />
            <div class="btn_sec">
                <asp:Button ID="btnUpdateVendor" runat="server" Text="Update" OnClick="btnUpdateVendor_Click" />
            </div>
        </asp:Panel>
    </div>
    <%--<ajaxToolkit:ModalPopupExtender ID="popup_permission" TargetControlID="btnSendMail"
            runat="server" CancelControlID="btnClose1" PopupControlID="pnlpopup">
        </ajaxToolkit:ModalPopupExtender>--%>



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
            return lIsValidated;
        }

        function ShowProgress() {
            document.getElementById('cover').style.display = '';
            document.getElementById('dvLoader').style.display = '';
            setTimeout(function () { HideProgress();  }, 5000);
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
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
        HideProgress();
    </script>
   
    
</asp:Content>
