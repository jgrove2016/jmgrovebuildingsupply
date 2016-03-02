<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="Procurement.aspx.cs" Inherits="JG_Prospect.Sr_App.Procurement" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js"></script>
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
    <%-- <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
    </style>--%>
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../img/btn-minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../img/btn_maximize.png");
            $(this).closest("tr").next().remove();
        });
    </script>--%>

    <script type="text/javascript">
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }

        var mapProp;
        function initialize() {
            mapProp = {
                center: new google.maps.LatLng(51.508742, -0.120850),
                zoom: 5,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
        }
        google.maps.event.addDomListener(window, 'load', initialize);

        function loadScript() {
            var script = document.createElement("script");
            script.src = "http://maps.googleapis.com/maps/api/js?callback=initialize";
            document.body.appendChild(script);
        }
        var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);




        function Email(e) {

            //<input type='text' ID='TextBox2' TabIndex='7' MaxLength='15' placeholder='EMail' value='bbb@gmail.com'>
            var dataTypeValue = $(e).attr("data-type");
            var subCount = $(e).closest('table').find('tr').length - 1;
            $(e).closest('table').append("<tr><td>" +
                                            "<input type='text' id='txtEMail" + dataTypeValue + subCount + "' name='nametxtEMail" + dataTypeValue + subCount + "'  placeholder='Email' clientidmode='Static' /></td></tr>");
            return false;
        }


        function AddLocation(e) {

            var dataTypeValue = $(e).attr("data-type");
            var subCount = $(e).closest('table').find('tr').length - 1;
            $(e).closest('table').append("<tr class='newAddressrow'>" +
                                            "<td><textarea id='txtAddress" + dataTypeValue + subCount + "' name='nametxtAddress" + dataTypeValue + subCount + "' placeholder='Address' rows='2' cols='20' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txtCity" + dataTypeValue + subCount + "' name='nametxtCity" + dataTypeValue + subCount + "' placeholder='City' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txtZip" + dataTypeValue + subCount + "' name='nametxtZip" + dataTypeValue + subCount + "' placeholder='Zip' clientidmode='Static' /></td>" +
                                            "<td></td></tr>");
        }

        function AddEmail(e) {
            var EmailType = $(e).attr("data-EmailType");
            var subCount = $(e).closest('table').find('tr').length;
            $(e).closest('table').append("<tr>" +
                                            "<td><input type='text' id='txt" + EmailType + "Email" + subCount + "' name='nametxt" + EmailType + "Email" + subCount + "' placeholder='Email' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txt" + EmailType + "FName" + subCount + "' name='nametxt" + EmailType + "FName" + subCount + "' placeholder='First Name' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txt" + EmailType + "LName" + subCount + "' name='nametxt" + EmailType + "LName" + subCount + "' placeholder='Last Name' clientidmode='Static' /></td>" +
                                            "<td><div class='newcontactdiv'><input type='text' id='txt" + EmailType + "ContactExten" + subCount + "' name='nametxt" + EmailType + "ContactExten" + subCount + "' style='width:35%' class='clsmaskphoneexten' placeholder='Extension' clientidmode='Static' />" +
                                            "&nbsp;<input type='text' id='txt" + EmailType + "Contact" + subCount + "' name='nametxt" + EmailType + "Contact" + subCount + "' style='width:50%' class='clsmaskphone' placeholder='___-___-____' clientidmode='Static' />" +
                                            "<a onclick='AddContact(this)' style='cursor:pointer' data-type='" + subCount + "' data-EmailType='" + EmailType + "' clientidmode='Static'>Add Contact</a><br/></div></td>" +
                                            "</tr>");
            $('.clsmaskphone').mask("(999) 999-9999");
            $('.clsmaskphoneexten').mask("999999");
        }

        function AddContact(e) {

            var dataTypeValue = $(e).attr("data-type");
            var EmailType = $(e).attr("data-EmailType");
            var subCount = $(e).closest('td').find('.clsmaskphone').length - 1;
            console.log(subCount)
            $(e).closest('td').append(
                                            "<br/><div class='newcontactdiv'><input type='text' id='txt" + EmailType + "ContactExten" + dataTypeValue + subCount + "' name='nametxt" + EmailType + "ContactExten" + dataTypeValue + subCount + "' style='width:35%;' maxlength='6' class='clsmaskphoneexten' placeholder='Extension' clientidmode='Static' />&nbsp;" +
                                            "<input type='text' id='txt" + EmailType + "contact" + dataTypeValue + subCount + "' name='nametxt" + EmailType + "contact" + dataTypeValue + subCount + "' style='width:50%;' maxlength='10' class='clsmaskphone' placeholder='___-___-____' clientidmode='Static' /><br/></div>");
            $('.clsmaskphone').mask("(999) 999-9999");
            $('.clsmaskphoneexten').mask("999999");

        }


        function GetVendorDetails(vid) {
            var AddressData = [];
            var PrimaryEmailData = [];
            var SecEmailData = [];
            var AltEmailData = [];

            var formPushData = [];
            $("#tblVendorLocation").find(".newAddressrow").each(function (index, node) {
                // console.log(index);
                //console.log(node);
                AddressData.push({
                    Address: $("#txtAddress1" + index).val(),
                    City: $("#txtCity1" + index).val(),
                    Zip: $("#txtZip1" + index).val()
                })
            });
            $("#tblPrimaryEmail").find("tr").each(function (index, node) {
                // console.log(index);
                //console.log(node);
                if (index != 0) {
                    var c = [];
                    $(this).find(".newcontactdiv").each(function () {
                        // console.log($("#txtPrimaryContact" + index).html());
                        c.push({
                            Extension: $(this).find(".clsmaskphoneexten").val(),
                            Number: $(this).find(".clsmaskphone").val()
                        });
                    });
                    var EmailData = {
                        Email: $("#txtPrimaryEmail" + index).val(),
                        FirstName: $("#txtPrimaryFName" + index).val(),
                        LastName: $("#txtPrimaryLName" + index).val(),
                        Contact: c
                    };
                    PrimaryEmailData.push(EmailData);
                }
            });
            $("#tblSecEmail").find("tr").each(function (index, node) {
                // console.log(index);
                //console.log(node);
                if (index != 0) {
                    var c = [];
                    $(this).find(".newcontactdiv").each(function () {
                        // console.log($("#txtPrimaryContact" + index).html());
                        c.push({
                            Extension: $(this).find(".clsmaskphoneexten").val(),
                            Number: $(this).find(".clsmaskphone").val()
                        });
                    });
                    var EmailData = {
                        Email: $("#txtSecEmail" + index).val(),
                        FirstName: $("#txtSecFName" + index).val(),
                        LastName: $("#txtSecLName" + index).val(),
                        Contact: c
                    };
                    SecEmailData.push(EmailData);
                }
            });
            $("#tblAltEmail").find("tr").each(function (index, node) {
                // console.log(index);
                //console.log(node);
                if (index != 0) {
                    var c = [];
                    $(this).find(".newcontactdiv").each(function () {
                        // console.log($("#txtPrimaryContact" + index).html());
                        c.push({
                            Extension: $(this).find(".clsmaskphoneexten").val(),
                            Number: $(this).find(".clsmaskphone").val()
                        });
                    });
                    var EmailData = {
                        Email: $("#txtAltEmail" + index).val(),
                        FirstName: $("#txtAltFName" + index).val(),
                        LastName: $("#txtAltLName" + index).val(),
                        Contact: c
                    };
                    AltEmailData.push(EmailData);
                }
            });
            console.log(PrimaryEmailData);
            console.log(SecEmailData);
            console.log(AltEmailData);
            console.log(AddressData);
            console.log($)
            $.ajax({
                type: "POST",
                url: "Procurement.aspx/GetVendorDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: JSON.stringify({ vendorid: vid, Address: AddressData, PrimaryEmail: PrimaryEmailData, SecEmail: SecEmailData, AltEmail: AltEmailData }),
                success: function (data) {
                    console.log(data);
                }
            });
        }

    </script>

    <style type="text/css">
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
            width: auto;
            height: auto;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }



        .tabs {
            position: relative;
            top: 1px;
            z-index: 2;
        }

        .tab {
            border: 1px solid black;
            background-image: url(images/navigation.jpg);
            background-repeat: repeat-x;
            color: White;
            padding: 2px 10px;
        }

        .selectedtab {
            background: none;
            background-repeat: repeat-x;
            color: black;
        }





        .tabcontents {
            border: 1px solid black;
            padding: 10px;
            /*width: 600px;
            height: 500px;*/
            background-color: white;
        }



        div.dd_chk_select div#caption {
            width: 152px;
            overflow: hidden;
            height: 16px;
            margin-right: 20px;
            margin-left: 2px;
            text-align: left;
            vertical-align: middle;
            position: relative;
            top: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- appointment tabs section start -->
    <ul class="appointment_tab">
        <li><a href="Procurement.aspx">Sold Jobs</a></li>
        <li><a href="#">Overhead Expences</a></li>
    </ul>

    <h1>
        <b>Procurement </b>
        <b>
            <asp:Label ID="lblerrornew" runat="server"></asp:Label></b>
    </h1>
    <div class="form_panel">
        <div class="right_panel">
            <!-- appointment tabs section end -->
            <%-- <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Menu1" />
                </Triggers>
                <ContentTemplate>--%>
            <div class="tabcontents">
                <div class="grid_h">
                    <asp:Button ID="btnDisable" runat="server" Text="Disable" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Height="40px" Width="75px" OnClick="btnDisable_Click" />
                    <%--<strong>Sold Jobs</strong></div>--%>
                    <div class="grid">
                        <div>
                            <asp:GridView ID="grdsoldjobs" runat="server" AutoGenerateColumns="false" CssClass="tableClass" DataKeyNames="CustomerId"
                                                              Width="100%" OnRowDataBound="grdsoldjobs_RowDataBound" OnSelectedIndexChanged="grdsoldjobs_SelectedIndexChanged">

                                <Columns>

                                    <asp:BoundField HeaderText="Date Sold" DataField="SoldDate" DataFormatString="{0:d}"
                                        HeaderStyle-Width="11%" />
                                    <asp:TemplateField HeaderText="Customer ID" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkcustomerid" runat="server" Text='<%#Eval("CustomerId") %>'
                                                OnClick="lnkcustomerid_Click"></asp:LinkButton>
                                            <asp:HiddenField ID="hdnproductid" runat="server" Value='<%#Eval("ProductId") %>' />
                                            <asp:HiddenField ID="hdnProductTypeId" runat="server" Value='<%#Eval("ProductTypeId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Customer's" DataField="LastName" HeaderStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="Sold Jobs #" HeaderStyle-Width="11%">
                                        <ItemTemplate>
                                            <%-- <asp:Label ID="lblsoldjobid" runat="server" Text='<%#Eval("SoldJob#") %>'></asp:Label>--%>
                                            <asp:LinkButton ID="lnksoldjobid" runat="server" Text='<%#Eval("SoldJob#") %>' OnClick="lnksoldjobid_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Install Category" HeaderStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>' HeaderStyle-Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Material List" HeaderStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkaddvendorquotes" runat="server" Text="Attach Quotes" OnClick="lnkaddvendorquotes_Click"
                                                HeaderStyle-Width="200px"></asp:LinkButton>--%>
                                    <%-- <asp:GridView runat ="server" ID ="grdAttachQuotes" AutoGenerateColumns ="false" 
                                OnRowDataBound ="grdAttachQuotes_RowDataBound" OnRowCommand ="grdAttachQuotes_RowCommand" Width="150px" HeaderStyle-Wrap="true">
                                    <Columns >
                                        <asp:TemplateField  HeaderStyle-Width="75px" HeaderStyle-Wrap="true">
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkDelete" Text ="X" runat ="server" CommandName="removeFile" CommandArgument ='<%#Eval("TempName") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-Width="75px" HeaderStyle-Wrap="true">
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkQuote" runat ="server" Text='<%#Eval("DocName") %>' CommandName="viewFile" CommandArgument ='<%#Eval("TempName") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField ="DocName" HeaderText ="Quote Name" />--%>
                                    <%-- </Columns>
                                </asp:GridView>--%>
                                    <%-- </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Final Material List" HeaderStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkmateriallist" runat="server" Text='<%#Eval("MaterialList") %>'
                                                OnClick="lnkmateriallist_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status/Approval" HeaderStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnStatusId" runat="server" Value='<%#Eval("StatusId") %>' />
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' ClientIDMode="Static"></asp:Label>
                                            <asp:DropDownList ID="ddlstatus" runat="server" Visible="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlstatus_selectedindexchanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status/Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PasswordStatus" HeaderStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblADMPassword" Text="ADM" runat="server"></asp:Label>
                                            <asp:Label ID="lblfrmPassword" Text="FRM" runat="server"></asp:Label>
                                            <asp:Label ID="lblSalePassword" Text="SLE" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Status/Reaason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status/Reaason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusName" runat="server" Text='<%#Eval("StatusName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="btn_sec">
                        <asp:Button ID="btnAddcategory" runat="server" Text="Add Category" OnClick="btnAddcategory_Click" />
                        <asp:Button ID="btndeletecategory" runat="server" Text="Delete Category" OnClick="btndeletecategory_Click" />
                        <%--<asp:Button ID="btnaddvendors" runat="server" Text="Add/Edit Vendor" OnClick="btnaddvendors_Click" />--%>
                        <br />
                        <br />
                        <br />
                    </div>
                    <div>
                        <asp:ModalPopupExtender ID="mpevendorcatelog" runat="server" TargetControlID="btnAddcategory"
                            PopupControlID="pnlpopup" CancelControlID="btnCancel">
                        </asp:ModalPopupExtender>

                        <%--  <asp:UpdatePanel ID="upnlCPass" runat="server">
                            <ContentTemplate>--%>
                        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="550px"
                            Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                            <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                                cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                        align="center">Add Vendor Category
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Vendor Category Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtname" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="Requiredvendorcategoryname" runat="server" ControlToValidate="txtname"
                                            ValidationGroup="addvendorcategory" ErrorMessage="Enter Category Name." ForeColor="Red"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnUpdate" CommandName="Update" Style="width: 100px;" runat="server"
                                            Text="Save" OnClick="btnsave_Click" ValidationGroup="addvendorcategory" />
                                        <asp:Button ID="btnCancel" runat="server" Style="width: 100px;" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <asp:ModalPopupExtender ID="Mpedeletecategory" runat="server" TargetControlID="btndeletecategory"
                            PopupControlID="pnlpopup2" CancelControlID="btnCancel2">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" Height="269px" Width="550px" CssClass="pnlDeleteVendor"
                            Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">Delete Vendor Category
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 45%">Select Vendor Category Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlvendercategoryname" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btndeletevender" CommandName="Delete" runat="server" Style="width: 100px;"
                                            Text="Delete" OnClick="btndelete_Click" />
                                        <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Style="width: 100px;" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <button id="btnquotes" style="display: none" runat="server">
                        </button>
                        <button id="btnmateriallist" style="display: none" runat="server">
                        </button>
                        <asp:ModalPopupExtender ID="ModalMateriallist" runat="server" PopupControlID="PanelMateriallist"
                            TargetControlID="btnmateriallist" CancelControlID="btncancelmateriallist">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="PanelMateriallist" runat="server" BackColor="White" Height="269px"
                            Width="550px" Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">Edit Material List
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 45%">Material List
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtmateriallist" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnsavematerials" CommandName="Save" runat="server" Style="width: 100px;"
                                            Text="Save Quotes" />
                                        <asp:Button ID="btncancelmateriallist" runat="server" Text="Cancel" Style="width: 100px;" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                    <div id="divVendorFilter" class="vendorFilter">
                        <asp:UpdatePanel ID="updtpnlfilter" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdoRetail" runat="server" Checked="true" Text="Retail" GroupName="MT" OnCheckedChanged="rdoRetail_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdoWholeSale" runat="server" Text="Wholesale" GroupName="MT" OnCheckedChanged="rdoWholeSale_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td>Product Category</td>
                                        <td>
                                            <asp:DropDownList ID="ddlprdtCategory" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlprdtCategory_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td>Vendor Category</td>
                                        <td>
                                            <asp:DropDownList ID="ddlVndrCategory" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlVndrCategory_SelectedIndexChanged"></asp:DropDownList>
                                            <br />
                                            <asp:LinkButton ID="lnkAddVendorCategory1" Text="Add Vendor Category" runat="server" OnClick="lnkAddVendorCategory1_Click"></asp:LinkButton>
                                            <br />
                                            <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="lnkAddVendorCategory1"
                                                PopupControlID="pnlpopupVendorCategory" CancelControlID="btnCancelVendor">
                                            </asp:ModalPopupExtender>

                                            <asp:Panel ID="pnlpopupVendorCategory" runat="server" BackColor="White" Height="269px" Width="550px"
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
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="btnNewVendor" Style="width: 100px;" runat="server"
                                                                OnClick="btnNewVendor_Click" Text="Save" ValidationGroup="addvendorcat" />
                                                            <asp:Button ID="btnCancelVendor" runat="server" Style="width: 100px;" Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                            <asp:LinkButton ID="Lnkdeletevendercategory1" Text="Delete Vendor Category" runat="server"></asp:LinkButton>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="Lnkdeletevendercategory1"
                                                PopupControlID="pnlpopup2" CancelControlID="btnCancel2">
                                            </asp:ModalPopupExtender>
                                        </td>
                                        <td>Vendor Sub Category</td>
                                        <td>
                                            <asp:DropDownList ID="ddlVendorSubCategory" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlVendorSubCategory_SelectedIndexChanged"></asp:DropDownList>
                                            <br />
                                            <asp:LinkButton ID="lnkAddVendorSubCategory" Text="Add Vendor Sub Category" runat="server"></asp:LinkButton>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="lnkAddVendorSubCategory"
                                                PopupControlID="pnlvendorSubCat" CancelControlID="btnCancelVendorSubCat">
                                            </asp:ModalPopupExtender>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlvendorSubCat" runat="server" BackColor="White" Height="269px" Width="550px"
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
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="btnVendorSubCat" Style="width: 100px;" runat="server"
                                                                        OnClick="btnNewVendorSubCat_Click" Text="Save" ValidationGroup="addvendorsubcat" />
                                                                    <asp:Button ID="btnCancelVendorSubCat" runat="server" Style="width: 100px;" Text="Cancel" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:LinkButton ID="lnkdeletevendersubcategory" Text="Delete Vendor Sub Category" runat="server"></asp:LinkButton>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="Lnkdeletevendersubcategory"
                                                PopupControlID="pnldeleteVendorSubCat" CancelControlID="btnCancelDelete">
                                            </asp:ModalPopupExtender>


                                            <asp:Panel ID="pnldeleteVendorSubCat" runat="server" BackColor="White" Height="269px" Width="550px"
                                                Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                                                <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                                                    cellspacing="0">
                                                    <tr style="background-color: #A33E3F">
                                                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                                            align="center">Delete Vendor Sub Category
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 31%">Select Vendor Sub Category
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlvendorsubcatpopup" runat="server" Width="150px"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlvendorsubcatpopup"
                                                                ValidationGroup="deletevendorsubcat" ErrorMessage="Select Vendor Sub Category Name." InitialValue="Select" ForeColor="Red"
                                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="btndeleteVendorSubCat" Style="width: 100px;" runat="server"
                                                                OnClick="btndeleteVendorSubCat_Click" Text="Delete" ValidationGroup="deletevendorsubcat" />
                                                            <asp:Button ID="btnCancelDelete" runat="server" Style="width: 100px;" Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                        </td>
                                    </tr>

                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <div>
                                                Vendor List
                                         <div class="grid_h">
                                             <div class="grid" style="width: 50%">
                                                 <asp:GridView ID="grdVendorList" runat="server" AutoGenerateColumns="false" CssClass="tableClass" Width="100%">
                                                     <Columns>
                                                         <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-Width="10%">
                                                             <ItemTemplate>
                                                                 <asp:LinkButton ID="lnkVendorName" runat="server" Text='<%#Eval("VendorName") %>' OnClick="lnkVendorName_Click"></asp:LinkButton>
                                                                 <asp:HiddenField ID="hdnVendorId" runat="server" Value='<%#Eval("VendorId") %>' />
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                     </Columns>
                                                     <EmptyDataTemplate>
                                                         No Data Found.
                                                     </EmptyDataTemplate>
                                                 </asp:GridView>
                                             </div>
                                         </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="googlemap1">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


                    <asp:UpdatePanel ID="updtpnlAddVender" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div id="divVendor" class="vendorForm">
                                <ul style="padding: 0px;">
                                    <li style="width: 99%; padding-left: 0px; margin-left: 0px;">
                                        <table border="0" cellspacing="0" cellpadding="0" style="padding: 0px; margin: 0px;">
                                            <tr>
                                                <td style="font-weight: bold; font-size: large; font-style: normal">Add Vendor
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Vendor Id:</label><br />
                                                    <asp:TextBox ID="txtVendorId" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                        <span>* </span>Vendor Name:
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="txtVendorNm" runat="server" MaxLength="30" AutoComplete="off" onkeypress="return isAlphaKey(event);" TabIndex="1"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="Requiredvendorname" runat="server" ControlToValidate="txtVendorNm" Display="Dynamic"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Vendor Name." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>


                                                <td>
                                                    <label>Vendor Status:</label><br />
                                                    <asp:DropDownList ID="ddlVendorStatus" runat="server">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Prospect</asp:ListItem>
                                                        <asp:ListItem>Active-Past</asp:ListItem>
                                                        <asp:ListItem>No Transactions</asp:ListItem>
                                                        <asp:ListItem>Deactivate</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <label>
                                                        Tax Id:</label><br />
                                                    <asp:TextBox ID="txtTaxId" runat="server" MaxLength="50"></asp:TextBox>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 0px;">
                                                    <fieldset style="margin: 0px !important;">
                                                        <legend style="width: 100%;">
                                                            <span style="font-weight: bold; font-size: 15px; font-style: normal; padding: 15px 15px 5px; display: block;">Vendor Address</span>
                                                            <table class="vendor_table">
                                                                <tr>
                                                                    <td>
                                                                        <label><span>* </span>Primary Address:</label><br />
                                                                        <asp:TextBox ID="txtPrimaryAddress" runat="server" TabIndex="7" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RfvAddress" runat="server" ControlToValidate="txtPrimaryAddress" Display="Dynamic"
                                                                            ValidationGroup="addvendor" ErrorMessage="Please Enter Primary Address." ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <label>City:</label><br />
                                                                        <asp:TextBox ID="txtPrimaryCity" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </td>
                                                                    <td>
                                                                        <label>Zip:</label><br />
                                                                        <asp:TextBox ID="txtPrimaryZip" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label><span>* </span>Secondary Address:</label><br />
                                                                        <asp:TextBox ID="txtSecAddress" runat="server" TabIndex="7" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvSecAddress" runat="server" ControlToValidate="txtSecAddress" Display="Dynamic"
                                                                            ValidationGroup="addvendor" ErrorMessage="Please Enter Secondary Address." ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <label>City:</label><br />
                                                                        <asp:TextBox ID="txtSecCity" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </td>
                                                                    <td>
                                                                        <label>Zip:</label><br />
                                                                        <asp:TextBox ID="txtSeczip" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label><span>* </span>Billing Address:</label><br />
                                                                        <asp:TextBox ID="txtBillingAddr" runat="server" TabIndex="7" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvbillAdd" runat="server" ControlToValidate="txtBillingAddr" Display="Dynamic"
                                                                            ValidationGroup="addvendor" ErrorMessage="Please Enter Billing Address." ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <label>City:</label><br />
                                                                        <asp:TextBox ID="txtBillingCity" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </td>
                                                                    <td>
                                                                        <label>Zip:</label><br />
                                                                        <asp:TextBox ID="txtBillingZip" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                            <table id="tblVendorLocation">
                                                                <tr>
                                                                    <td>
                                                                        <a onclick="AddLocation(this)" style="cursor: pointer" data-type="1">Add Location</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </legend>
                                                    </fieldset>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td>
                                                    <label>
                                                        Website:</label><br />
                                                    <asp:TextBox ID="txtWebsite" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                        Fax</label><br />
                                                    <asp:TextBox ID="txtVendorFax" runat="server" MaxLength="50"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>* </span>
                                                        Primary Contact Email</label><br />
                                                    <asp:TextBox ID="txtprimaryemail" runat="server" MaxLength="50"></asp:TextBox>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="revemail" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtprimaryemail" ValidationGroup="addvendor"
                                                        ErrorMessage="Please enter correct email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="Requiredmail" runat="server" Display="Dynamic" ControlToValidate="txtprimaryemail"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Email." ForeColor="Red"></asp:RequiredFieldValidator>

                                                </td>
                                                <td>
                                                    <label>
                                                        First Name</label><br />
                                                    <asp:TextBox ID="txtFName" runat="server" MaxLength="50"></asp:TextBox>
                                                    <br />

                                                </td>

                                                <td>
                                                    <label>
                                                        Last Name</label><br />
                                                    <asp:TextBox ID="txtLName" runat="server" MaxLength="50"></asp:TextBox>
                                                    <br />

                                                </td>
                                                <td>
                                                    <label>
                                                        Contact 1
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="txtContactExten1" runat="server" placeholder="Extension" class="clsmaskphoneexten" MaxLength="6" Width="35%"></asp:TextBox>
                                                    <asp:TextBox ID="txtContact1" runat="server" MaxLength="10" placeholder='___-___-____' CssClass="clsmaskphone" Width="50%"></asp:TextBox>

                                                    <br />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 0px;">
                                                    <table id="tblPrimaryEmail" style="margin: 0px;">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Email 2
                                                                </label>
                                                                <asp:TextBox ID="txtPrimaryEmail0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />
                                                                <a style="cursor: pointer" data-emailtype="Primary" onclick="AddEmail(this)">Add Row</a>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    First Name</label><br />
                                                                <asp:TextBox ID="txtPrimaryFName0" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>

                                                            <td>
                                                                <label>
                                                                    Last Name</label><br />
                                                                <asp:TextBox ID="txtPrimaryLName0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />

                                                            </td>
                                                            <td>
                                                                <label>
                                                                    Contact 2
                                                                </label>
                                                                <br />
                                                                <asp:TextBox ID="txtPrimaryContactExten0" runat="server" placeholder="Extension" class="clsmaskphoneexten" MaxLength="6" Width="34%"></asp:TextBox>
                                                                <asp:TextBox ID="txtPrimaryContact0" runat="server" placeholder='___-___-____' MaxLength="10" CssClass="clsmaskphone" Width="50%"></asp:TextBox>
                                                                <br />
                                                                <a onclick="AddContact(this)" style="cursor: pointer" data-emailtype="Primary" data-type="0">Add Contact</a><br />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 0px;">
                                                    <table id="tblSecEmail" style="margin: 0px; padding: 0px;">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Secondary Contact Email</label><br />
                                                                <asp:TextBox ID="txtSecEmail0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />

                                                                <a style="cursor: pointer" data-emailtype="Sec" onclick="AddEmail(this)">Add Row</a>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    First Name</label><br />
                                                                <asp:TextBox ID="txtSecFName0" runat="server" MaxLength="50"></asp:TextBox>
                                                            </td>

                                                            <td>
                                                                <label>
                                                                    Last Name</label><br />
                                                                <asp:TextBox ID="txtSecLName0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />

                                                            </td>
                                                            <td>
                                                                <label>
                                                                    Contact 3
                                                                </label>
                                                                <br />
                                                                <asp:TextBox ID="txtSecContactExten0" runat="server" MaxLength="6" class="clsmaskphoneexten" placeholder="Extension" Width="35%"></asp:TextBox>
                                                                <asp:TextBox ID="txtSecContact0" runat="server" MaxLength="10" placeholder='___-___-____' CssClass="clsmaskphone" Width="50%"></asp:TextBox>
                                                                <br />
                                                                <a onclick="AddContact(this)" data-emailtype="Sec" style="cursor: pointer" data-type="0">Add Contact</a>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding: 0px;">
                                                    <table id="tblAltEmail" style="margin: 0px; padding: 0px;">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Alt. Contact Email</label><br />
                                                                <asp:TextBox ID="txtAltEmail0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />
                                                                <a style="cursor: pointer" data-emailtype="Alt" onclick="AddEmail(this)">Add Row</a>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    First Name</label><br />
                                                                <asp:TextBox ID="txtAltFName0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />

                                                            </td>

                                                            <td>
                                                                <label>
                                                                    Last Name</label><br />
                                                                <asp:TextBox ID="txtAltLName0" runat="server" MaxLength="50"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    Contact 4
                                                                </label>
                                                                <br />
                                                                <asp:TextBox ID="txtAltContactExten0" runat="server" MaxLength="6" class="clsmaskphoneexten" placeholder="Extension" Width="32%"></asp:TextBox>
                                                                <asp:TextBox ID="txtAltContact0" runat="server" MaxLength="10" CssClass="clsmaskphone" placeholder='___-___-____' Width="50%"></asp:TextBox>
                                                                <br />
                                                                <a onclick="AddContact(this)" style="cursor: pointer" data-emailtype="Alt" data-type="0">Add Contact</a>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                        </table>
                                    </li>
                                </ul>


                                <ul style="padding: 0px; display: none">
                                    <li style="width: 49%; padding-left: 0px; margin-left: 0px;">
                                        <table border="0" cellspacing="0" cellpadding="0" style="padding: 0px; margin0px;">
                                            <tr>
                                                <td style="font-weight: bold; font-size: large; font-style: normal">Add Vendor
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>*</span> Vendor Category:
                                                    </label>
                                                    <asp:DropDownList ID="ddlVendorCategory" runat="server" TabIndex="2"
                                                        OnSelectedIndexChanged="ddlVendorCategory_SelectedIndexChanged1" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <label>
                                                    </label>
                                                    <asp:LinkButton ID="lnkAddVendorCategory" Text="Add New Category" runat="server"></asp:LinkButton>
                                                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkAddVendorCategory"
                                                        PopupControlID="pnlpopup" CancelControlID="btnCancel">
                                                    </asp:ModalPopupExtender>
                                                    <asp:Panel ID="Panel1" runat="server" BackColor="White" Height="269px" Width="550px"
                                                        Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                                                        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr style="background-color: #A33E3F">
                                                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger; width: 100%;"
                                                                    align="center">Vendor Catalog Details
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 31%">Vendor Catagory Name
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox1" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtname"
                                                                        ValidationGroup="addvendorcategory" ErrorMessage="Enter Category Name." ForeColor="Red"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="Button1" CommandName="Update" Style="width: 100px;" runat="server"
                                                                        Text="Save" OnClick="btnsave_Click" ValidationGroup="addvendorcategory" />
                                                                    <asp:Button ID="Button2" runat="server" Style="width: 100px;" Text="Cancel" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    &nbsp;
                                <asp:LinkButton ID="Lnkdeletevendercategory" Text="Delete Vendor Category" runat="server"></asp:LinkButton>
                                                    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Lnkdeletevendercategory"
                                                        PopupControlID="pnlpopup2" CancelControlID="btnCancel2">
                                                    </asp:ModalPopupExtender>
                                                    <asp:Panel ID="Panel2" runat="server" BackColor="White" Height="269px" Width="550px"
                                                        Style="display: none">
                                                        <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%"
                                                            cellpadding="0" cellspacing="0">
                                                            <tr style="background-color: #A33E3F">
                                                                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                                                    align="center">Vendor Catalog Details
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 45%">Select Vendor Catagory Name
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="Button3" CommandName="Delete" runat="server" Style="width: 100px;"
                                                                        Text="Delete" OnClientClick="return confirm('All materials list associated with this vendor will be deleted.Are you sure you want to perform this operation')" OnClick="btndeletevender_Click" />
                                                                    <asp:Button ID="Button4" runat="server" Text="Cancel" Style="width: 100px;" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>*</span> Vendor Name:
                                                    </label>
                                                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="30" AutoComplete="off" onkeypress="return isAlphaKey(event);" TabIndex="1"></asp:TextBox>
                                                    <%-- OnTextChanged="txtVendorNm_TextChanged" AutoPostBack="true"--%>
                                                    <label>
                                                    </label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtVendorNm"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Vendor Name." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>*</span>  Contact Person:
                                                    </label>
                                                    <asp:TextBox ID="txtcontactperson" runat="server" MaxLength="30" AutoComplete="off" TabIndex="3" onkeyup="javascript:Alpha(this)"
                                                        onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                                    <%-- OnTextChanged="txtcontactperson_TextChanged" AutoPostBack="true"--%>
                                                    <label>
                                                    </label>
                                                    <asp:RequiredFieldValidator ID="Requiredcontactperson" runat="server" ControlToValidate="txtcontactperson"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Contact Person." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>*</span> Contact Number:
                                                    </label>
                                                    <asp:TextBox ID="txtcontactnumber" runat="server" MaxLength="15" AutoComplete="off" TabIndex="4" onkeyup="javascript:Numeric(this)"
                                                        onkeypress="return isNumericKey(event);"></asp:TextBox><%-- OnTextChanged="txtcontactnumber_TextChanged" AutoPostBack="true"--%>
                                                    <label>
                                                    </label>
                                                    <asp:RequiredFieldValidator ID="Requiredcontactnumber" runat="server" ControlToValidate="txtcontactnumber"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Contact Number." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>*</span> Fax:
                                                    </label>
                                                    <asp:TextBox ID="txtfax" runat="server" MaxLength="20" AutoComplete="off" onkeypress="return isfax(event);" TabIndex="5"></asp:TextBox>
                                                    <%--OnTextChanged="txtfax_TextChanged" AutoPostBack="true"--%>
                                                    <label>
                                                    </label>
                                                    <asp:RequiredFieldValidator ID="Requiredfax" runat="server" ControlToValidate="txtfax"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Fax Number." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <span>*</span> Mail:
                                                    </label>
                                                    <asp:TextBox ID="txtmail" runat="server" MaxLength="50" AutoComplete="off" TabIndex="6"></asp:TextBox>
                                                    <%--OnTextChanged="txtmail_TextChanged" AutoPostBack="true"--%>
                                                    <label>
                                                    </label>
                                                    <asp:RegularExpressionValidator ID="revfax" runat="server" ForeColor="Red" ControlToValidate="txtmail" ValidationGroup="addvendor"
                                                        ErrorMessage="Please enter correct email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    <label>
                                                    </label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtmail"
                                                        ValidationGroup="addvendor" ErrorMessage="Please Enter Email." ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Address:</label>
                                                    <asp:TextBox ID="txtaddress" runat="server" TabIndex="7" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- OnTextChanged="txtaddress_TextChanged" AutoPostBack="true"--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Notes:</label>
                                                    <asp:TextBox ID="txtNotes" runat="server" TabIndex="8" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- OnTextChanged="txtNotes_TextChanged" AutoPostBack="true"--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                    <li style="width: 48%">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Vendor Id:</label><asp:TextBox ID="TextBox4" runat="server" MaxLength="50"></asp:TextBox>
                                                    <%-- OnTextChanged="txtVendorId_TextChanged" AutoPostBack="true"--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Menufacturer:</label>
                                                    <asp:DropDownList ID="ddlMenufacturer" runat="server">
                                                        <%--OnSelectedIndexChanged="ddlMenufacturer_SelectedIndexChanged" AutoPostBack="true"--%>
                                                        <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Wholesale" Value="Wholesale"></asp:ListItem>
                                                        <asp:ListItem Text="Retail" Value="Retail"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Vendors List:</label>
                                                    <asp:DropDownCheckBoxes ID="ddlVendorName" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged1" style="width: 190px !important;" UseSelectAllNode="true">
                                                    </asp:DropDownCheckBoxes>


                                                    <%--<asp:ListBox ID="lstVendors" Rows="8" runat="server" OnSelectedIndexChanged="lstVendors_SelectedIndexChanged" TabIndex="7"
                                                        AutoPostBack="True" Height="44px" Width="229px" SelectionMode="Multiple"></asp:ListBox>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="googleMap" style="width: 421px; height: 254px;"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Billing Address:</label>
                                                    <asp:TextBox ID="txtBillingAddress" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    <%--OnTextChanged="txtBillingAddress_TextChanged" AutoPostBack="true"--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Tax Id:</label>
                                                    <asp:TextBox ID="TextBox5" runat="server" MaxLength="50"></asp:TextBox>
                                                    <%--AutoPostBack="true" OnTextChanged="txtTaxId_TextChanged"--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Expense Category:</label>
                                                    <asp:TextBox ID="txtExpenseCat" runat="server" MaxLength="50"></asp:TextBox>
                                                    <%--AutoPostBack="true" OnTextChanged="txtExpenseCat_TextChanged"--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Auto & Truck Insurance:</label>
                                                    <asp:TextBox ID="txtAutoInsurance" runat="server" MaxLength="50"></asp:TextBox>
                                                    <%--AutoPostBack="true" OnTextChanged="txtAutoInsurance_TextChanged"--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                    <li style="width: 100%"></li>
                                </ul>
                                <div class="btn_sec">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="8" />
                                    <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" TabIndex="9" />ValidationGroup="addvendor"OnClientClick="GetVendorDetails();"--%>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <%--<asp:Panel runat="server" ID="pnlMaterialList">--%>
                    <asp:Panel ID="pnlpopupMaterialList" runat="server" BackColor="White" Height="175px" Width="300px"
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


                    <%--</asp:Panel>--%>



                    <%--   <div class="grid_h">
                    <strong>Vendors List</strong>
                </div>
                <div class="grid">
                    <asp:UpdatePanel ID="upVendors" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdvendors" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="tableClass" OnRowDataBound="grdvendors_RowDataBound" HeaderStyle-Wrap="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Category" DataField="VendorCategoryNm" HeaderStyle-Width="14%"
                                        HeaderStyle-Wrap="true" />
                                    <asp:BoundField HeaderText="CategoryId" DataField="VendorCategpryId" />
                                    <%--<asp:BoundField HeaderText="Vendor" DataField="VendorName" />
                                    <asp:TemplateField HeaderText="Vendor" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpVendorName" CssClass="drpVendorName" runat="server" Width="150px"
                                                OnSelectedIndexChanged="drpVendorName_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact #" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactNumber" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax" HeaderStyle-Width="14%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFax" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" HeaderStyle-Width="16%" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                        <asp:BoundField HeaderText="Contact #" DataField="ContactNumber" />
                        <asp:BoundField HeaderText="Fax" DataField="Fax" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>--%>
                </div>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
        <asp:Panel ID="panelPopup" runat="server">
            <div id="light" class="white_content">
                <h3>Disable</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">Close</a>
                <br />
                <table>
                    <tr>
                        <td>Enter reaason to disable the Customer.
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtReasonDisable" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="txtReasonDisable" ValidationGroup="Reason" runat="server" ErrorMessage="Enter Reason."></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveDisable" Height="27px" Width="60px" runat="server" Text="Submit" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" ValidationGroup="Reason" OnClick="btnSaveDisable_Click" />
                        </td>
                    </tr>
                </table>

            </div>
        </asp:Panel>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <%-- </div>
    </div>
    </div>--%>
    <script src="../Scripts/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('.clsmaskphone').mask("(999) 999-9999");
            $('.clsmaskphoneexten').mask("999999");
        });
    </script>
</asp:Content>
