<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="Procurement.aspx.cs" Inherits="JG_Prospect.Sr_App.Procurement" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js"></script>
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }

        //google.maps.event.addDomListener(window, 'load', initialize);

        function AddLocation(e) {
            var dataTypeValue = $(e).attr("data-type");
            var subCount = $(e).closest('table').find('tr').length - 1;
            $(e).closest('table').append("<tr class='newAddressrow'>" +
                                            "<td><select name='nameddlAddresstype" + dataTypeValue + subCount + "' id='ddlAddressType" + dataTypeValue + subCount + "'>" +
                                                    "<option value='--Select--'>Select</option>" +
                                                    "<option value='Primary'>Primary</option>" +
                                                    "<option value='Secondary'>Secondary</option>" +
                                                    "<option value='Billing'>Billing</option></select></td>" +
                                            "<td><textarea id='txtAddress" + dataTypeValue + subCount + "' name='nametxtAddress" + dataTypeValue + subCount + "' placeholder='Address' rows='2' cols='40' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txtCity" + dataTypeValue + subCount + "' name='nametxtCity" + dataTypeValue + subCount + "' placeholder='City' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txtZip" + dataTypeValue + subCount + "' name='nametxtZip" + dataTypeValue + subCount + "' placeholder='Zip' clientidmode='Static' /></td>" +
                                            "</tr>");
        }

        function AddEmailRow(e) {
            var EmailType = $(e).attr("data-EmailType");
            var subCount = $(e).closest('table').find('tr').length;
            $(e).closest('table').append("<tr>" +
                                            "<td><div class='newEmaildiv'><input type='text' id='txt" + EmailType + "Email" + subCount + "' name='nametxt" + EmailType + "Email" + subCount + "' placeholder='Email' class='clsemail' clientidmode='Static' />" +
                                            "<br/><a onclick='AddEmail(this)' style='cursor: pointer' data-emailtype='" + EmailType + "' data-type='" + subCount + "'>Add Email</a><br/></div></td>" +
                                            "<td><input type='text' id='txt" + EmailType + "FName" + subCount + "' name='nametxt" + EmailType + "FName" + subCount + "' placeholder='First Name' clientidmode='Static' /></td>" +
                                            "<td><input type='text' id='txt" + EmailType + "LName" + subCount + "' name='nametxt" + EmailType + "LName" + subCount + "' placeholder='Last Name' clientidmode='Static' /></td>" +
                                            "<td><div class='newcontactdiv'><input type='text' id='txt" + EmailType + "ContactExten" + subCount + "' name='nametxt" + EmailType + "ContactExten" + subCount + "' style='width:35%' maxlength='6' class='clsmaskphoneexten' placeholder='Extension' clientidmode='Static' />" +
                                            "&nbsp;<input type='text' id='txt" + EmailType + "Contact" + subCount + "' name='nametxt" + EmailType + "Contact" + subCount + "' style='width:50%' class='clsmaskphone' maxlength='10' placeholder='___-___-____' clientidmode='Static' />" +
                                            "<a onclick='AddContact(this)' style='cursor:pointer' data-type='" + subCount + "' data-EmailType='" + EmailType + "' clientidmode='Static'>Add Contact</a><br/></div></td>" +
                                            "</tr>");
            $('.clsmaskphone').mask("(999) 999-9999");
            $('.clsmaskphoneexten').mask("999999");
        }

        function AddEmail(e) {
            var dataTypeValue = $(e).attr("data-type");
            var EmailType = $(e).attr("data-EmailType");
            var subCount = $(e).closest('tr').find('.newEmaildiv').length;
            $(e).closest('td').append(
                        "<br/><div class='newEmaildiv'><input type='text' id='txt" + EmailType + "Email" + dataTypeValue + subCount + "' name='nametxt" + EmailType + "Email" + dataTypeValue + subCount + "' class='clsemail' clientidmode='Static' />"
                );
        }

        function AddContact(e) {
            var dataTypeValue = $(e).attr("data-type");
            var EmailType = $(e).attr("data-EmailType");
            var subCount = $(e).closest('td').find('.clsmaskphone').length - 1;
            $(e).closest('td').append(
                                            "<br/><div class='newcontactdiv'><input type='text' id='txt" + EmailType + "ContactExten" + dataTypeValue + subCount + "' name='nametxt" + EmailType + "ContactExten" + dataTypeValue + subCount + "' style='width:35%;' maxlength='6' class='clsmaskphoneexten' placeholder='Extension' clientidmode='Static' />&nbsp;" +
                                            "<input type='text' id='txt" + EmailType + "Contact" + dataTypeValue + subCount + "' name='nametxt" + EmailType + "Contact" + dataTypeValue + subCount + "' style='width:50%;' maxlength='10' class='clsmaskphone' maxlength='10' placeholder='___-___-____' clientidmode='Static' /><br/></div>");
            $('.clsmaskphone').mask("(999) 999-9999");
            $('.clsmaskphoneexten').mask("999999");

        }


        function GetVendorDetails(e) {

            var AddressData = [];
            var VendorEmailData = [];
            var vid = $('.clsvendorid').val();
            var AddrType = "Other";
            //$(".vendor_table").find(".fixedAddressrow").each(function (index, node) {
            //    if (index == 0) {
            AddressData.push({
                AddressID: $(".clsvendoraddress").val() == undefined || $(".clsvendoraddress").val() == "Select" ? "0" : $(".clsvendoraddress").val(),
                AddressType: $(".clstxtAddressType0").val(),
                Address: $(".clstxtAddress0").val(),
                City: $(".clstxtCity0").val(),
                Zip: $(".clstxtZip0").val(),
                Country: $(".clstxtCountry0").val()
            })
            //}
            // });

            $("#tblVendorLocation").find(".newAddressrow").each(function (index, node) {
                AddressData.push({
                    AddressType: $("#ddlAddressType1" + index).val(),
                    Address: $("#txtAddress1" + index).val(),
                    City: $("#txtCity1" + index).val(),
                    Zip: $("#txtZip1" + index).val()
                })
            });
            $("#tblPrimaryEmail").find("tr").each(function (index, node) {
                var c = [];
                var Emails = [];
                $(this).find(".newEmaildiv").each(function () {
                    Emails.push({ Email: $(this).find(".clsemail").val() });
                });
                $(this).find(".newcontactdiv").each(function () {
                    c.push({
                        Extension: $(this).find(".clsmaskphoneexten").val(),
                        Number: $(this).find(".clsmaskphone").val()
                    });
                });
                var EmailData = {
                    EmailType: "Primary",
                    Email: Emails,
                    FirstName: $("input[name=nametxtPrimaryFName" + index + "]").val(),
                    LastName: $("input[name=nametxtPrimaryLName" + index + "]").val(),
                    Contact: c,
                    AddressID: $(".clsvendoraddress").val() == undefined || $(".clsvendoraddress").val() == "Select" ? "0" : $(".clsvendoraddress").val(),
                };
                VendorEmailData.push(EmailData);

            });
            $("#tblSecEmail").find("tr").each(function (index, node) {
                var c = [];
                var Emails = [];
                $(this).find(".newEmaildiv").each(function () {
                    Emails.push({ Email: $(this).find(".clsemail").val() });
                });
                $(this).find(".newcontactdiv").each(function () {
                    c.push({
                        Extension: $(this).find(".clsmaskphoneexten").val(),
                        Number: $(this).find(".clsmaskphone").val()
                    });
                });
                var EmailData = {
                    EmailType: "Secondary",
                    Email: Emails,
                    FirstName: $("#txtSecFName" + index).val(),
                    LastName: $("#txtSecLName" + index).val(),
                    Contact: c,
                    AddressID: $(".clsvendoraddress").val() == undefined || $(".clsvendoraddress").val() == "Select" ? "0" : $(".clsvendoraddress").val(),
                };
                VendorEmailData.push(EmailData);
            });
            $("#tblAltEmail").find("tr").each(function (index, node) {
                var c = [];
                var Emails = [];
                $(this).find(".newEmaildiv").each(function () {
                    Emails.push({ Email: $(this).find(".clsemail").val() });
                });
                $(this).find(".newcontactdiv").each(function () {
                    // console.log($("#txtPrimaryContact" + index).html());
                    c.push({
                        Extension: $(this).find(".clsmaskphoneexten").val(),
                        Number: $(this).find(".clsmaskphone").val()
                    });
                });
                var EmailData = {
                    EmailType: "Alternate",
                    Email: Emails,
                    FirstName: $("#txtAltFName" + index).val(),
                    LastName: $("#txtAltLName" + index).val(),
                    Contact: c,
                    AddressID: $(".clsvendoraddress").val() == undefined || $(".clsvendoraddress").val() == "Select" ? "0" : $(".clsvendoraddress").val(),
                };
                VendorEmailData.push(EmailData);
            });
            //console.log(JSON.stringify(VendorEmailData));
            //console.log(JSON.stringify(AddressData));

            var datalength = JSON.parse(JSON.stringify(VendorEmailData)).length;
            $.ajax({
                type: "POST",
                url: "Procurement.aspx/PostVendorDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: JSON.stringify({ vendorid: vid, Address: AddressData, VendorEmails: VendorEmailData }),
                success: function (data) {
                    console.log(data);
                    //AddOldEmailContent(datalength);
                }
            });
        }

        function checkAddress() {
            if ($(".clsvendoraddress").val() == undefined || $(".clsvendoraddress").val() == "") {
                alert("select address from Address Dropdown or Add new address");
                return false;
            }
            else {
                return true;
            }
        }

        function AddVenderEmails(data) {
            var PID = -1;
            var SID = -1;
            var AID = -1;
            for (var i = 0; i < data.length; i++) {
                var AddressID = data[i].AddressID;
                var Email = JSON.parse(data[i].Email);
                var Contact = JSON.parse(data[i].Contact);
                var EmailType = data[i].EmailType;
                var FName = data[i].FName;
                var LName = data[i].LName;
                var SeqNo = data[i].SeqNo;
                var VendorId = data[i].VendorId;
                var TempID = data[i].TempID;
                var ID = "";
                if (EmailType == "Primary") {
                    ID = "Primary";
                    PID++;
                }
                if (EmailType == "Secondary") {
                    ID = "Sec";
                    SID++;
                }
                if (EmailType == "Alternate") {
                    ID = "Alt";
                    AID++;
                }

                var NewRow = 0;
                if (PID > 0 || SID > 0 || AID > 0) {
                    if (EmailType == "Primary") {
                        NewRow = PID;
                    }
                    if (EmailType == "Secondary") {
                        NewRow = SID;
                    }
                    if (EmailType == "Alternate") {
                        NewRow = SID;
                    }
                }

                GenereateHTML(data[i], ID, NewRow);
            }
        }

        function GenereateHTML(data, ID, NewRow) {
            var ContentPlaceHolder = "ContentPlaceHolder1_";
            var AddressID = data.AddressID;
            var Email = JSON.parse(data.Email);
            var Contact = JSON.parse(data.Contact);
            var EmailType = data.EmailType;
            var FName = data.FName;
            var LName = data.LName;
            var SeqNo = data.SeqNo;
            var VendorId = data.VendorId;
            var TempID = data.TempID;


            if (NewRow > 0) {
                var MainHTML = '<tr><td><div class="newEmaildiv">';
                MainHTML += '<input type="text" id="txt' + ID + 'Email' + NewRow + '" name="nametxt' + ID + 'Email' + NewRow + '" value="' + Email[0].Email + '" placeholder="Email" class="clsemail" clientidmode="Static"/><br>';
                MainHTML += '<a onclick="AddEmail(this)" style="cursor: pointer" data-emailtype="Primary" data-type="1">Add Email</a><br></div></td>';
                MainHTML += '<td><input type="text" id="txt' + ID + 'FName' + NewRow + '" name="nametxt' + ID + 'FName' + NewRow + '" value="' + FName + '" placeholder="First Name" clientidmode="Static"></td>';
                MainHTML += '<td><input type="text" id="txt' + ID + 'LName' + NewRow + '" name="nametxt' + ID + 'LName' + NewRow + '" value="' + LName + '" placeholder="Last Name" clientidmode="Static"></td>';
                MainHTML += '<td><div class="newcontactdiv"><input type="text" id="txt' + ID + 'ContactExten' + NewRow + '" name="nametxt' + ID + 'ContactExten' + NewRow + '" value="' + Contact[0].Extension + '" style="width:35%" maxlength="6" class="clsmaskphoneexten" placeholder="Extension" clientidmode="Static"/>&nbsp;<input type="text" id="txt' + ID + 'Contact' + NewRow + '" name="nametx' + ID + 'Contact' + NewRow + '" value="' + Contact[0].Number + '" style="width:50%" class="clsmaskphone" maxlength="10" placeholder="___-___-____" clientidmode="Static"/>';
                MainHTML += '<a onclick="AddContact(this)" style="cursor:pointer" data-type="1" data-emailtype="Primary" clientidmode="Static">Add Contact</a><br></div></td></tr>';
                $("#tbl" + ID + "Email").find("tr:last-child").after(MainHTML);
                for (j = 1; j < Email.length; j++) {
                    var HTML = '<br/>';
                    HTML += '<div class="newEmaildiv"><input type="text" id="txt' + ID + 'Email' + NewRow + '' + j + '" name="nametxt' + ID + 'Email' + NewRow + '' + j + '" class="clsemail" value="' + Email[j].Email + '" clientidmode="Static"?></div>';
                    $("#tbl" + ID + "Email").find("tr:last-child .newEmaildiv").append(HTML);
                    //$("#txt" + ID + "Email0" + j).val(Email[j].Email);
                }
                for (j = 1; j < Contact.length; j++) {
                    var n = j - 1;
                    var HTML = '<br/>';
                    HTML += '<div class="newcontactdiv"><input type="text" id="txt' + ID + 'ContactExten' + NewRow + '' + n + '" name="nametxt' + ID + 'ContactExten' + NewRow + '' + n + '" style="width:35%;" maxlength="6" class="clsmaskphoneexten" placeholder="Extension" value="' + Contact[j].Extension + '" clientidmode="Static"/>&nbsp;<input type="text" id="txt' + ID + 'Contact0' + n + '" name="nametxt' + ID + 'Contact' + NewRow + '' + n + '" style="width:50%;" maxlength="10" class="clsmaskphone" placeholder="___-___-____" value="' + Contact[j].Number + '" clientidmode="Static"/><br></div>';
                    $("#tbl" + ID + "Email").find("tr:last-child .newcontactdiv").append(HTML);
                    //$("#" + ContentPlaceHolder + "txt" + ID + "ContactExten0" + j).val(Contact[j].Extension);
                    //$("#" + ContentPlaceHolder + "txt" + ID + "Contact0" + j).val(Contact[j].Number);
                }
            }
            else {
                $("#txt" + ID + "Email" + NewRow).val(Email[0].Email);
                $("#txt" + ID + "FName" + NewRow).val(FName);
                $("#txt" + ID + "LName" + NewRow).val(LName);
                $("#" + ContentPlaceHolder + "txt" + ID + "ContactExten" + NewRow).val(Contact[0].Extension);
                $("#" + ContentPlaceHolder + "txt" + ID + "Contact" + NewRow).val(Contact[0].Number);
                for (j = 1; j < Email.length; j++) {
                    var HTML = '<br/>';
                    HTML += '<div class="newEmaildiv"><input type="text" id="txt' + ID + 'Email' + NewRow + '' + j + '" name="nametxt' + ID + 'Email' + NewRow + '' + j + '" class="clsemail" value="' + Email[j].Email + '" clientidmode="Static"?></div>';
                    $("#tbl" + ID + "Email").find("tr:last-child .newEmaildiv").append(HTML);
                    //$("#txt" + ID + "Email0" + j).val(Email[j].Email);
                }
                for (j = 1; j < Contact.length; j++) {
                    var n = j - 1;
                    var HTML = '<br/>';
                    HTML += '<div class="newcontactdiv"><input type="text" id="txt' + ID + 'ContactExten' + NewRow + '' + n + '" name="nametxt' + ID + 'ContactExten' + NewRow + '' + n + '" style="width:35%;" maxlength="6" class="clsmaskphoneexten" placeholder="Extension" value="' + Contact[j].Extension + '" clientidmode="Static"/>&nbsp;<input type="text" id="txt' + ID + 'Contact0' + n + '" name="nametxt' + ID + 'Contact' + NewRow + '' + n + '" style="width:50%;" maxlength="10" class="clsmaskphone" placeholder="___-___-____" value="' + Contact[j].Number + '" clientidmode="Static"/><br></div>';
                    $("#tbl" + ID + "Email").find("tr:last-child .newcontactdiv").append(HTML);
                    //$("#" + ContentPlaceHolder + "txt" + ID + "ContactExten0" + j).val(Contact[j].Extension);
                    //$("#" + ContentPlaceHolder + "txt" + ID + "Contact0" + j).val(Contact[j].Number);
                }
            }
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
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }

        .clsbtnEditVendor {
            display: none;
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
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="btn_sec">
                        <asp:Button ID="btnAddcategory" runat="server" Text="Add Category" OnClick="btnAddcategory_Click" />
                        <asp:Button ID="btndeletecategory" runat="server" Text="Delete Category" OnClick="btndeletecategory_Click" />
                        <br />
                        <br />
                        <br />
                    </div>
                    <div>
                        <asp:ModalPopupExtender ID="mpevendorcatelog" runat="server" TargetControlID="btnAddcategory"
                            PopupControlID="pnlpopup" CancelControlID="btnCancel">
                        </asp:ModalPopupExtender>


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
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updtpnlfilter">
                            <ProgressTemplate>
                                <div class="modal">
                                    <div class="center">
                                        <img alt="Loading..." src="../img/loader.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="updtpnlfilter" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdoRetailWholesale" runat="server" Checked="true" Text="Retail/Wholesale" GroupName="MT" OnCheckedChanged="rdoRetailWholesale_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdoManufacturer" runat="server" Text="Manufacturer" GroupName="MT" OnCheckedChanged="rdoManufacturer_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td colspan="2"></td>
                                        <td colspan="2">
                                            <div class="ui-widget">
                                                <asp:TextBox ID="txtVendorSearchBox" CssClass="VendorSearchBox" runat="server" placeholder="Search" Width="90%"></asp:TextBox>
                                            </div>
                                        </td>
                                        <input type="hidden" id="hdnvendorId" name="vendorId" />
                                        <asp:Button ID="btnEditVendor" runat="server" Text="EditVendor" CssClass="clsbtnEditVendor" OnClick="btneditVendor_Click" />

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
                                                        <td align="right" style="width: 31%">Manufacturer Type
                                                        </td>
                                                        <td>

                                                            <asp:CheckBox ID="chkVCRetail_Wholesale" runat="server" Text="Retail/Wholesale" />
                                                            <asp:CheckBox ID="chkVCManufacturer" runat="server" Text="Manufacturer" />
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
                                                                <td align="right" style="width: 31%">Manufacturer Type
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkVSCRetail_Wholesale" runat="server" Text="Retail/Wholesale" />
                                                                    <asp:CheckBox ID="chkVSCManufacturer" runat="server" Text="Manufacturer" />
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
                                            <div style="width: 100%">

                                                <div class="grid_h" style="width: 47%; float: left">
                                                    Vendor List
                                             <div class="grid">
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
                                                <div style="width: 450px; float: left; margin-left: 25px; margin-top: 20px;">
                                                    <div id="googleMap" style="width: 100%; height: 254px;"></div>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="tabs">
                        <ul>
                            <li><a href="#tabs-1">Add/Edit Vendor</a></li>
                            <li><a href="#tabs-2">Add Product</a></li>
                        </ul>
                        <div id="tabs-1">
                            <asp:UpdatePanel ID="updtpnlAddVender" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div id="divVendor" class="vendorForm">
                                        <ul style="padding: 0px;">
                                            <li style="width: 99%; padding-left: 0px; margin-left: 0px;">
                                                <table border="0" cellspacing="0" cellpadding="0" style="padding: 0px; margin: 0px;">
                                                    <tr>
                                                        <td colspan="4" style="font-weight: bold; font-size: large; font-style: normal">Add Vendor
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <label>Vendor Id:</label><br />
                                                            <asp:TextBox ID="txtVendorId" CssClass="clsvendorid" runat="server" MaxLength="50"></asp:TextBox>
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
                                                            <asp:DropDownList ID="ddlVendorStatus" runat="server" Style="width: 180px;">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Prospect</asp:ListItem>
                                                                <asp:ListItem>Active-Past</asp:ListItem>
                                                                <%--<asp:ListItem>No Transactions</asp:ListItem>--%>
                                                                <asp:ListItem>Deactivate</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style1">
                                                            <label>
                                                                Vendor Source<asp:Label ID="lblSourceReq" runat="server" Text="*" ForeColor="Green"></asp:Label></label>
                                                            <asp:DropDownList ID="ddlSource" runat="server" Width="250px">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtSource" runat="server" Width="125px"></asp:TextBox>
                                                            <asp:Button runat="server" ID="btnAddSource" Text="Add" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" OnClick="btnAddSource_Click" Height="30px" />&nbsp;
                               
                                <asp:Button runat="server" ID="btnDeleteSource" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Text="Delete" OnClick="btnDeleteSource_Click" Height="30px" />
                                                            <%--<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSource"
                                    ForeColor="Green" Display="Dynamic" ValidationGroup="submit" ErrorMessage="Please select the source." InitialValue="Select Source"></asp:RequiredFieldValidator>--%>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Address:</label><br />
                                                            <asp:DropDownList ID="DrpVendorAddress" AutoPostBack="true" OnSelectedIndexChanged="DrpVendorAddress_SelectedIndexChanged" runat="server" Style="width: 180px;" CssClass="clsvendoraddress">
                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Tax Id:</label><br />
                                                            <asp:TextBox ID="txtTaxId" runat="server" MaxLength="50"></asp:TextBox>

                                                        </td>
                                                        <td>
                                                            <label>
                                                                Website:</label><br />
                                                            <asp:TextBox ID="txtWebsite" runat="server" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Fax</label><br />
                                                            <asp:TextBox ID="txtVendorFax" runat="server" MaxLength="20"></asp:TextBox>
                                                            <br />
                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Payment Terms
                                                            </label>
                                                            <asp:DropDownList ID="DrpPaymentTerms" runat="server" Style="width: 180px;">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Pay In Advance</asp:ListItem>
                                                                <asp:ListItem>COD</asp:ListItem>
                                                                <asp:ListItem>NET 15</asp:ListItem>
                                                                <asp:ListItem>Net 30</asp:ListItem>
                                                                <asp:ListItem>Net 60</asp:ListItem>
                                                                <asp:ListItem>1% 10 Net 30</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                Payment Method:</label><br />
                                                            <asp:DropDownList ID="DrpPaymentMode" runat="server" Style="width: 180px;">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>amex2343</asp:ListItem>
                                                                <asp:ListItem>Discover3494</asp:ListItem>
                                                                <asp:ListItem>echeck101</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="padding: 0px;">
                                                            <fieldset style="margin: 0px !important;">
                                                                <legend style="width: 100%;">
                                                                    <span style="font-weight: bold; font-size: 15px; font-style: normal; padding: 15px 15px 5px; display: block;">Vendor Address</span>
                                                                    <table class="vendor_table">
                                                                        <tr class="fixedAddressrow">
                                                                            <td style="width: 12%">
                                                                                <label>Address Type:</label><br />
                                                                                <asp:DropDownList ID="ddlAddressType" runat="server" CssClass="clstxtAddressType0" Width="190px">
                                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                                    <asp:ListItem>Primary</asp:ListItem>
                                                                                    <asp:ListItem>Secondary</asp:ListItem>
                                                                                    <asp:ListItem>Billing</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </td>
                                                                            <td style="width: 12%">
                                                                                <label>City:</label><br />
                                                                                <asp:TextBox ID="txtPrimaryCity" runat="server" TabIndex="7" placeholder="City" CssClass="clstxtCity0"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 12%">
                                                                                <label>Zip:</label><br />
                                                                                <asp:TextBox ID="txtPrimaryZip" runat="server" TabIndex="7" placeholder="Zip" CssClass="clstxtZip0"></asp:TextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <label>Address:</label><br />
                                                                                <asp:TextBox ID="txtPrimaryAddress" runat="server" TabIndex="7" placeholder="Address" TextMode="MultiLine" Columns="40" Rows="2" CssClass="clstxtAddress0" Width="86%"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RfvAddress" runat="server" ControlToValidate="txtPrimaryAddress" Display="Dynamic"
                                                                                    ValidationGroup="addvendor" ErrorMessage="Please Enter Primary Address." ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </td>


                                                                            <td>
                                                                                <label>Country:</label><br />
                                                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="190px" CssClass="clstxtCountry0">
                                                                                    <asp:ListItem Value="" Selected="True">Select Country</asp:ListItem>
                                                                                    <asp:ListItem Value="AF">Afghanistan</asp:ListItem>
                                                                                    <asp:ListItem Value="AL">Albania</asp:ListItem>
                                                                                    <asp:ListItem Value="DZ">Algeria</asp:ListItem>
                                                                                    <asp:ListItem Value="AS">American Samoa</asp:ListItem>
                                                                                    <asp:ListItem Value="AD">Andorra</asp:ListItem>
                                                                                    <asp:ListItem Value="AO">Angola</asp:ListItem>
                                                                                    <asp:ListItem Value="AI">Anguilla</asp:ListItem>
                                                                                    <asp:ListItem Value="AQ">Antarctica</asp:ListItem>
                                                                                    <asp:ListItem Value="AG">Antigua And Barbuda</asp:ListItem>
                                                                                    <asp:ListItem Value="AR">Argentina</asp:ListItem>
                                                                                    <asp:ListItem Value="AM">Armenia</asp:ListItem>
                                                                                    <asp:ListItem Value="AW">Aruba</asp:ListItem>
                                                                                    <asp:ListItem Value="AU">Australia</asp:ListItem>
                                                                                    <asp:ListItem Value="AT">Austria</asp:ListItem>
                                                                                    <asp:ListItem Value="AZ">Azerbaijan</asp:ListItem>
                                                                                    <asp:ListItem Value="BS">Bahamas</asp:ListItem>
                                                                                    <asp:ListItem Value="BH">Bahrain</asp:ListItem>
                                                                                    <asp:ListItem Value="BD">Bangladesh</asp:ListItem>
                                                                                    <asp:ListItem Value="BB">Barbados</asp:ListItem>
                                                                                    <asp:ListItem Value="BY">Belarus</asp:ListItem>
                                                                                    <asp:ListItem Value="BE">Belgium</asp:ListItem>
                                                                                    <asp:ListItem Value="BZ">Belize</asp:ListItem>
                                                                                    <asp:ListItem Value="BJ">Benin</asp:ListItem>
                                                                                    <asp:ListItem Value="BM">Bermuda</asp:ListItem>
                                                                                    <asp:ListItem Value="BT">Bhutan</asp:ListItem>
                                                                                    <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                                                                                    <asp:ListItem Value="BA">Bosnia And Herzegowina</asp:ListItem>
                                                                                    <asp:ListItem Value="BW">Botswana</asp:ListItem>
                                                                                    <asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
                                                                                    <asp:ListItem Value="BR">Brazil</asp:ListItem>
                                                                                    <asp:ListItem Value="IO">British Indian Ocean Territory</asp:ListItem>
                                                                                    <asp:ListItem Value="BN">Brunei Darussalam</asp:ListItem>
                                                                                    <asp:ListItem Value="BG">Bulgaria</asp:ListItem>
                                                                                    <asp:ListItem Value="BF">Burkina Faso</asp:ListItem>
                                                                                    <asp:ListItem Value="BI">Burundi</asp:ListItem>
                                                                                    <asp:ListItem Value="KH">Cambodia</asp:ListItem>
                                                                                    <asp:ListItem Value="CM">Cameroon</asp:ListItem>
                                                                                    <asp:ListItem Value="CA">Canada</asp:ListItem>
                                                                                    <asp:ListItem Value="CV">Cape Verde</asp:ListItem>
                                                                                    <asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="CF">Central African Republic</asp:ListItem>
                                                                                    <asp:ListItem Value="TD">Chad</asp:ListItem>
                                                                                    <asp:ListItem Value="CL">Chile</asp:ListItem>
                                                                                    <asp:ListItem Value="CN">China</asp:ListItem>
                                                                                    <asp:ListItem Value="CX">Christmas Island</asp:ListItem>
                                                                                    <asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="CO">Colombia</asp:ListItem>
                                                                                    <asp:ListItem Value="KM">Comoros</asp:ListItem>
                                                                                    <asp:ListItem Value="CG">Congo</asp:ListItem>
                                                                                    <asp:ListItem Value="CK">Cook Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                                                                                    <asp:ListItem Value="CI">Cote D'Ivoire</asp:ListItem>
                                                                                    <asp:ListItem Value="HR">Croatia (Local Name: Hrvatska)</asp:ListItem>
                                                                                    <asp:ListItem Value="CU">Cuba</asp:ListItem>
                                                                                    <asp:ListItem Value="CY">Cyprus</asp:ListItem>
                                                                                    <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                                                                                    <asp:ListItem Value="DK">Denmark</asp:ListItem>
                                                                                    <asp:ListItem Value="DJ">Djibouti</asp:ListItem>
                                                                                    <asp:ListItem Value="DM">Dominica</asp:ListItem>
                                                                                    <asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
                                                                                    <asp:ListItem Value="TP">East Timor</asp:ListItem>
                                                                                    <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                                                                                    <asp:ListItem Value="EG">Egypt</asp:ListItem>
                                                                                    <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                                                                                    <asp:ListItem Value="GQ">Equatorial Guinea</asp:ListItem>
                                                                                    <asp:ListItem Value="ER">Eritrea</asp:ListItem>
                                                                                    <asp:ListItem Value="EE">Estonia</asp:ListItem>
                                                                                    <asp:ListItem Value="ET">Ethiopia</asp:ListItem>
                                                                                    <asp:ListItem Value="FK">Falkland Islands (Malvinas)</asp:ListItem>
                                                                                    <asp:ListItem Value="FO">Faroe Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="FJ">Fiji</asp:ListItem>
                                                                                    <asp:ListItem Value="FI">Finland</asp:ListItem>
                                                                                    <asp:ListItem Value="FR">France</asp:ListItem>
                                                                                    <asp:ListItem Value="GF">French Guiana</asp:ListItem>
                                                                                    <asp:ListItem Value="PF">French Polynesia</asp:ListItem>
                                                                                    <asp:ListItem Value="TF">French Southern Territories</asp:ListItem>
                                                                                    <asp:ListItem Value="GA">Gabon</asp:ListItem>
                                                                                    <asp:ListItem Value="GM">Gambia</asp:ListItem>
                                                                                    <asp:ListItem Value="GE">Georgia</asp:ListItem>
                                                                                    <asp:ListItem Value="DE">Germany</asp:ListItem>
                                                                                    <asp:ListItem Value="GH">Ghana</asp:ListItem>
                                                                                    <asp:ListItem Value="GI">Gibraltar</asp:ListItem>
                                                                                    <asp:ListItem Value="GR">Greece</asp:ListItem>
                                                                                    <asp:ListItem Value="GL">Greenland</asp:ListItem>
                                                                                    <asp:ListItem Value="GD">Grenada</asp:ListItem>
                                                                                    <asp:ListItem Value="GP">Guadeloupe</asp:ListItem>
                                                                                    <asp:ListItem Value="GU">Guam</asp:ListItem>
                                                                                    <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                                                                                    <asp:ListItem Value="GN">Guinea</asp:ListItem>
                                                                                    <asp:ListItem Value="GW">Guinea-Bissau</asp:ListItem>
                                                                                    <asp:ListItem Value="GY">Guyana</asp:ListItem>
                                                                                    <asp:ListItem Value="HT">Haiti</asp:ListItem>
                                                                                    <asp:ListItem Value="HM">Heard And Mc Donald Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="VA">Holy See (Vatican City State)</asp:ListItem>
                                                                                    <asp:ListItem Value="HN">Honduras</asp:ListItem>
                                                                                    <asp:ListItem Value="HK">Hong Kong</asp:ListItem>
                                                                                    <asp:ListItem Value="HU">Hungary</asp:ListItem>
                                                                                    <asp:ListItem Value="IS">Icel And</asp:ListItem>
                                                                                    <asp:ListItem Value="IN">India</asp:ListItem>
                                                                                    <asp:ListItem Value="ID">Indonesia</asp:ListItem>
                                                                                    <asp:ListItem Value="IR">Iran (Islamic Republic Of)</asp:ListItem>
                                                                                    <asp:ListItem Value="IQ">Iraq</asp:ListItem>
                                                                                    <asp:ListItem Value="IE">Ireland</asp:ListItem>
                                                                                    <asp:ListItem Value="IL">Israel</asp:ListItem>
                                                                                    <asp:ListItem Value="IT">Italy</asp:ListItem>
                                                                                    <asp:ListItem Value="JM">Jamaica</asp:ListItem>
                                                                                    <asp:ListItem Value="JP">Japan</asp:ListItem>
                                                                                    <asp:ListItem Value="JO">Jordan</asp:ListItem>
                                                                                    <asp:ListItem Value="KZ">Kazakhstan</asp:ListItem>
                                                                                    <asp:ListItem Value="KE">Kenya</asp:ListItem>
                                                                                    <asp:ListItem Value="KI">Kiribati</asp:ListItem>
                                                                                    <asp:ListItem Value="KP">Korea, Dem People'S Republic</asp:ListItem>
                                                                                    <asp:ListItem Value="KR">Korea, Republic Of</asp:ListItem>
                                                                                    <asp:ListItem Value="KW">Kuwait</asp:ListItem>
                                                                                    <asp:ListItem Value="KG">Kyrgyzstan</asp:ListItem>
                                                                                    <asp:ListItem Value="LA">Lao People'S Dem Republic</asp:ListItem>
                                                                                    <asp:ListItem Value="LV">Latvia</asp:ListItem>
                                                                                    <asp:ListItem Value="LB">Lebanon</asp:ListItem>
                                                                                    <asp:ListItem Value="LS">Lesotho</asp:ListItem>
                                                                                    <asp:ListItem Value="LR">Liberia</asp:ListItem>
                                                                                    <asp:ListItem Value="LY">Libyan Arab Jamahiriya</asp:ListItem>
                                                                                    <asp:ListItem Value="LI">Liechtenstein</asp:ListItem>
                                                                                    <asp:ListItem Value="LT">Lithuania</asp:ListItem>
                                                                                    <asp:ListItem Value="LU">Luxembourg</asp:ListItem>
                                                                                    <asp:ListItem Value="MO">Macau</asp:ListItem>
                                                                                    <asp:ListItem Value="MK">Macedonia</asp:ListItem>
                                                                                    <asp:ListItem Value="MG">Madagascar</asp:ListItem>
                                                                                    <asp:ListItem Value="MW">Malawi</asp:ListItem>
                                                                                    <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                                                                                    <asp:ListItem Value="MV">Maldives</asp:ListItem>
                                                                                    <asp:ListItem Value="ML">Mali</asp:ListItem>
                                                                                    <asp:ListItem Value="MT">Malta</asp:ListItem>
                                                                                    <asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="MQ">Martinique</asp:ListItem>
                                                                                    <asp:ListItem Value="MR">Mauritania</asp:ListItem>
                                                                                    <asp:ListItem Value="MU">Mauritius</asp:ListItem>
                                                                                    <asp:ListItem Value="YT">Mayotte</asp:ListItem>
                                                                                    <asp:ListItem Value="MX">Mexico</asp:ListItem>
                                                                                    <asp:ListItem Value="FM">Micronesia, Federated States</asp:ListItem>
                                                                                    <asp:ListItem Value="MD">Moldova, Republic Of</asp:ListItem>
                                                                                    <asp:ListItem Value="MC">Monaco</asp:ListItem>
                                                                                    <asp:ListItem Value="MN">Mongolia</asp:ListItem>
                                                                                    <asp:ListItem Value="MS">Montserrat</asp:ListItem>
                                                                                    <asp:ListItem Value="MA">Morocco</asp:ListItem>
                                                                                    <asp:ListItem Value="MZ">Mozambique</asp:ListItem>
                                                                                    <asp:ListItem Value="MM">Myanmar</asp:ListItem>
                                                                                    <asp:ListItem Value="NA">Namibia</asp:ListItem>
                                                                                    <asp:ListItem Value="NR">Nauru</asp:ListItem>
                                                                                    <asp:ListItem Value="NP">Nepal</asp:ListItem>
                                                                                    <asp:ListItem Value="NL">Netherlands</asp:ListItem>
                                                                                    <asp:ListItem Value="AN">Netherlands Ant Illes</asp:ListItem>
                                                                                    <asp:ListItem Value="NC">New Caledonia</asp:ListItem>
                                                                                    <asp:ListItem Value="NZ">New Zealand</asp:ListItem>
                                                                                    <asp:ListItem Value="NI">Nicaragua</asp:ListItem>
                                                                                    <asp:ListItem Value="NE">Niger</asp:ListItem>
                                                                                    <asp:ListItem Value="NG">Nigeria</asp:ListItem>
                                                                                    <asp:ListItem Value="NU">Niue</asp:ListItem>
                                                                                    <asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
                                                                                    <asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="NO">Norway</asp:ListItem>
                                                                                    <asp:ListItem Value="OM">Oman</asp:ListItem>
                                                                                    <asp:ListItem Value="PK">Pakistan</asp:ListItem>
                                                                                    <asp:ListItem Value="PW">Palau</asp:ListItem>
                                                                                    <asp:ListItem Value="PA">Panama</asp:ListItem>
                                                                                    <asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
                                                                                    <asp:ListItem Value="PY">Paraguay</asp:ListItem>
                                                                                    <asp:ListItem Value="PE">Peru</asp:ListItem>
                                                                                    <asp:ListItem Value="PH">Philippines</asp:ListItem>
                                                                                    <asp:ListItem Value="PN">Pitcairn</asp:ListItem>
                                                                                    <asp:ListItem Value="PL">Poland</asp:ListItem>
                                                                                    <asp:ListItem Value="PT">Portugal</asp:ListItem>
                                                                                    <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                                                                                    <asp:ListItem Value="QA">Qatar</asp:ListItem>
                                                                                    <asp:ListItem Value="RE">Reunion</asp:ListItem>
                                                                                    <asp:ListItem Value="RO">Romania</asp:ListItem>
                                                                                    <asp:ListItem Value="RU">Russian Federation</asp:ListItem>
                                                                                    <asp:ListItem Value="RW">Rwanda</asp:ListItem>
                                                                                    <asp:ListItem Value="KN">Saint K Itts And Nevis</asp:ListItem>
                                                                                    <asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
                                                                                    <asp:ListItem Value="VC">Saint Vincent, The Grenadines</asp:ListItem>
                                                                                    <asp:ListItem Value="WS">Samoa</asp:ListItem>
                                                                                    <asp:ListItem Value="SM">San Marino</asp:ListItem>
                                                                                    <asp:ListItem Value="ST">Sao Tome And Principe</asp:ListItem>
                                                                                    <asp:ListItem Value="SA">Saudi Arabia</asp:ListItem>
                                                                                    <asp:ListItem Value="SN">Senegal</asp:ListItem>
                                                                                    <asp:ListItem Value="SC">Seychelles</asp:ListItem>
                                                                                    <asp:ListItem Value="SL">Sierra Leone</asp:ListItem>
                                                                                    <asp:ListItem Value="SG">Singapore</asp:ListItem>
                                                                                    <asp:ListItem Value="SK">Slovakia (Slovak Republic)</asp:ListItem>
                                                                                    <asp:ListItem Value="SI">Slovenia</asp:ListItem>
                                                                                    <asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="SO">Somalia</asp:ListItem>
                                                                                    <asp:ListItem Value="ZA">South Africa</asp:ListItem>
                                                                                    <asp:ListItem Value="GS">South Georgia , S Sandwich Is.</asp:ListItem>
                                                                                    <asp:ListItem Value="ES">Spain</asp:ListItem>
                                                                                    <asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
                                                                                    <asp:ListItem Value="SH">St. Helena</asp:ListItem>
                                                                                    <asp:ListItem Value="PM">St. Pierre And Miquelon</asp:ListItem>
                                                                                    <asp:ListItem Value="SD">Sudan</asp:ListItem>
                                                                                    <asp:ListItem Value="SR">Suriname</asp:ListItem>
                                                                                    <asp:ListItem Value="SJ">Svalbard, Jan Mayen Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="SZ">Sw Aziland</asp:ListItem>
                                                                                    <asp:ListItem Value="SE">Sweden</asp:ListItem>
                                                                                    <asp:ListItem Value="CH">Switzerland</asp:ListItem>
                                                                                    <asp:ListItem Value="SY">Syrian Arab Republic</asp:ListItem>
                                                                                    <asp:ListItem Value="TW">Taiwan</asp:ListItem>
                                                                                    <asp:ListItem Value="TJ">Tajikistan</asp:ListItem>
                                                                                    <asp:ListItem Value="TZ">Tanzania, United Republic Of</asp:ListItem>
                                                                                    <asp:ListItem Value="TH">Thailand</asp:ListItem>
                                                                                    <asp:ListItem Value="TG">Togo</asp:ListItem>
                                                                                    <asp:ListItem Value="TK">Tokelau</asp:ListItem>
                                                                                    <asp:ListItem Value="TO">Tonga</asp:ListItem>
                                                                                    <asp:ListItem Value="TT">Trinidad And Tobago</asp:ListItem>
                                                                                    <asp:ListItem Value="TN">Tunisia</asp:ListItem>
                                                                                    <asp:ListItem Value="TR">Turkey</asp:ListItem>
                                                                                    <asp:ListItem Value="TM">Turkmenistan</asp:ListItem>
                                                                                    <asp:ListItem Value="TC">Turks And Caicos Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="TV">Tuvalu</asp:ListItem>
                                                                                    <asp:ListItem Value="UG">Uganda</asp:ListItem>
                                                                                    <asp:ListItem Value="UA">Ukraine</asp:ListItem>
                                                                                    <asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
                                                                                    <asp:ListItem Value="GB">United Kingdom</asp:ListItem>
                                                                                    <asp:ListItem Value="US">United States</asp:ListItem>
                                                                                    <asp:ListItem Value="UM">United States Minor Is.</asp:ListItem>
                                                                                    <asp:ListItem Value="UY">Uruguay</asp:ListItem>
                                                                                    <asp:ListItem Value="UZ">Uzbekistan</asp:ListItem>
                                                                                    <asp:ListItem Value="VU">Vanuatu</asp:ListItem>
                                                                                    <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                                                                                    <asp:ListItem Value="VN">Viet Nam</asp:ListItem>
                                                                                    <asp:ListItem Value="VG">Virgin Islands (British)</asp:ListItem>
                                                                                    <asp:ListItem Value="VI">Virgin Islands (U.S.)</asp:ListItem>
                                                                                    <asp:ListItem Value="WF">Wallis And Futuna Islands</asp:ListItem>
                                                                                    <asp:ListItem Value="EH">Western Sahara</asp:ListItem>
                                                                                    <asp:ListItem Value="YE">Yemen</asp:ListItem>
                                                                                    <asp:ListItem Value="ZR">Zaire</asp:ListItem>
                                                                                    <asp:ListItem Value="ZM">Zambia</asp:ListItem>
                                                                                    <asp:ListItem Value="ZW">Zimbabwe</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <%--<table id="tblVendorLocation">
                                                                        <tr>
                                                                            <td>
                                                                                <a onclick="AddLocation(this)" style="cursor: pointer" data-type="1">Add Location</a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>--%>
                                                                </legend>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="padding: 0px;">
                                                            <table id="tblPrimaryEmail" style="margin: 0px;">
                                                                <tr>
                                                                    <td>

                                                                        <label>
                                                                            Primary Contact Email
                                                                        </label>
                                                                        <div class="newEmaildiv">
                                                                            <input type='text' id="txtPrimaryEmail0" name="nametxtPrimaryEmail0" placeholder="Email" class="clsemail" clientidmode='Static' />
                                                                            <br />
                                                                            <a style="cursor: pointer" data-emailtype="Primary" onclick="AddEmailRow(this)">Add New Row</a> &nbsp;&nbsp;
                                                                    <a onclick="AddEmail(this)" style="cursor: pointer" data-emailtype="Primary" data-type="0">Add Email</a>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            First Name</label><br />
                                                                        <input type='text' id="txtPrimaryFName0" name="nametxtPrimaryFName0" maxlength="50" clientidmode='Static' />
                                                                    </td>

                                                                    <td>
                                                                        <label>
                                                                            Last Name</label><br />
                                                                        <input type='text' id="txtPrimaryLName0" name="nametxtPrimaryLName0" maxlength="50" clientidmode='Static' />
                                                                        <%--    <asp:TextBox ID="txtPrimaryLName0" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                        <br />

                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Contact #
                                                                        </label>
                                                                        <br />
                                                                        <div class='newcontactdiv'>
                                                                            <asp:TextBox ID="txtPrimaryContactExten0" runat="server" placeholder="Extension" class="clsmaskphoneexten" MaxLength="6" Width="34%"></asp:TextBox>
                                                                            <asp:TextBox ID="txtPrimaryContact0" runat="server" placeholder='___-___-____' MaxLength="10" CssClass="clsmaskphone" Width="50%"></asp:TextBox>
                                                                            <br />
                                                                            <a onclick="AddContact(this)" style="cursor: pointer" data-emailtype="Primary" data-type="0">Add Contact</a><br />
                                                                        </div>
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
                                                                        <div class="newEmaildiv">
                                                                            <input type='text' id="txtSecEmail0" name="nametxtSecEmail0" maxlength="50" class="clsemail" clientidmode='Static' />
                                                                            <br />
                                                                            <a style="cursor: pointer" data-emailtype="Sec" onclick="AddEmailRow(this)">Add New Row</a> &nbsp;&nbsp;
                                                                    <a onclick="AddEmail(this)" style="cursor: pointer" data-emailtype="Sec" data-type="0">Add Email</a>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            First Name</label><br />
                                                                        <input type='text' id="txtSecFName0" name="nametxtSecFName0" maxlength="50" clientidmode='Static' />
                                                                        <%--<asp:TextBox ID="txtSecFName0" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                    </td>

                                                                    <td>
                                                                        <label>
                                                                            Last Name</label><br />
                                                                        <input type='text' id="txtSecLName0" name="nametxtSecLName0" maxlength="50" clientidmode='Static' />
                                                                        <%--  <asp:TextBox ID="txtSecLName0" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                        <br />

                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Contact #
                                                                        </label>
                                                                        <br />
                                                                        <div class='newcontactdiv'>
                                                                            <asp:TextBox ID="txtSecContactExten0" runat="server" MaxLength="6" class="clsmaskphoneexten" placeholder="Extension" Width="35%"></asp:TextBox>
                                                                            <asp:TextBox ID="txtSecContact0" runat="server" MaxLength="10" placeholder='___-___-____' CssClass="clsmaskphone" Width="50%"></asp:TextBox>
                                                                            <br />
                                                                            <a onclick="AddContact(this)" data-emailtype="Sec" style="cursor: pointer" data-type="0">Add Contact</a>
                                                                            <br />
                                                                        </div>
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
                                                                        <div class="newEmaildiv">
                                                                            <input type='text' id="txtAltEmail0" name="nametxtAltEmail0" maxlength="50" class="clsemail" clientidmode='Static' />
                                                                            <br />
                                                                            <a style="cursor: pointer" data-emailtype="Alt" onclick="AddEmailRow(this)">Add New Row</a> &nbsp;&nbsp;
                                                                    <a onclick="AddEmail(this)" style="cursor: pointer" data-emailtype="Alt" data-type="0">Add Email</a>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            First Name</label><br />
                                                                        <input type='text' id="txtAltFName0" name="nametxtAltFName0" maxlength="50" clientidmode='Static' />
                                                                        <%--<asp:TextBox ID="txtAltFName0" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                        <br />

                                                                    </td>

                                                                    <td>
                                                                        <label>
                                                                            Last Name</label><br />
                                                                        <input type='text' id="txtAltLName0" name="nametxtAltLName0" maxlength="50" clientidmode='Static' />
                                                                        <%--<asp:TextBox ID="txtAltLName0" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                        <br />
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            Contact #
                                                                        </label>
                                                                        <br />
                                                                        <div class='newcontactdiv'>
                                                                            <asp:TextBox ID="txtAltContactExten0" runat="server" MaxLength="6" class="clsmaskphoneexten" placeholder="Extension" Width="32%"></asp:TextBox>
                                                                            <asp:TextBox ID="txtAltContact0" runat="server" MaxLength="10" CssClass="clsmaskphone" placeholder='___-___-____' Width="50%"></asp:TextBox>
                                                                            <br />
                                                                            <a onclick="AddContact(this)" style="cursor: pointer" data-emailtype="Alt" data-type="0">Add Contact</a>
                                                                            <br />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <div style="text-align: right;">
                                                    <asp:LinkButton ID="BtnSaveLoaction" runat="server" Text="Save Address" OnClientClick="GetVendorDetails(this)" ValidationGroup="addaddress" OnClick="BtnSaveLoaction_Click" />
                                                    <br />
                                                    <asp:Label ID="lbladdress" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </li>
                                        </ul>
                                        <div class="btn_sec">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return checkAddress()" OnClick="btnSave_Click" ValidationGroup="addvendor" TabIndex="8" /><%--OnClick="btnSave_Click" ValidationGroup="addvendor"--%>
                                            <br />
                                            <asp:Label ID="LblSave" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="tabs-2">
                            <p>&nbsp</p>
                        </div>
                    </div>
                    <div class="form_panel_custom vendorFilter">
                        <table id="Table1" cellpadding="0" cellspacing="0" border="0" runat="server">
                            <tr>
                                <td>
                                    <label>
                                        <strong>Select Period: </strong><span>*</span>
                                    </label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <label style="width: 50%;">
                                        <strong>Select Pay Period : </strong>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        From :
                                    </label>
                                    <asp:TextBox ID="txtfrmdate" runat="server" TabIndex="2" CssClass="date"
                                        onkeypress="return false" MaxLength="10" AutoPostBack="true"
                                        Style="width: 150px;"></asp:TextBox>
                                    <label></label>
                                    <asp:RequiredFieldValidator ID="requirefrmdate" ControlToValidate="txtfrmdate"
                                        runat="server" ErrorMessage=" Select From date" ForeColor="Red" ValidationGroup="display">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label style="width: 50px; text-align: right;">
                                        <span>*</span> To :
                                    </label>
                                    <asp:TextBox ID="txtTodate" CssClass="date" onkeypress="return false"
                                        MaxLength="10" runat="server" TabIndex="3" AutoPostBack="true"
                                        Style="width: 150px;"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="Requiretodate" ControlToValidate="txtTodate"
                                        runat="server" ErrorMessage=" Select To date" ForeColor="Red" ValidationGroup="display">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>

                                    <asp:DropDownList ID="drpPayPeriod" runat="server" Width="250px" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpPayPeriod_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div class="grid">

                            <asp:GridView ID="grdtransations" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="tableClass" HeaderStyle-Wrap="true" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                    <asp:BoundField HeaderText="Total Amount" DataField="TotalAmount" />
                                    <asp:BoundField HeaderText="Description" DataField="Description" />
                                    <asp:BoundField HeaderText="Payment Method" DataField="PaymentMethod" />
                                    <asp:BoundField HeaderText="Transaction # - Transaction type" DataField="Transations" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                    <asp:BoundField HeaderText="Invoice Attachement" DataField="InvoiceAttach" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <div class="grid">
                            <div style="float: left; width: 23%;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="rdoRetailWholesale1" runat="server" Text="Retail/Wholesale" GroupName="MT" />
                                            <br />
                                            <asp:CheckBox ID="rdoManufacturer1" runat="server" Text="Manufacturer" GroupName="MT" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Product Category
                                            <br />
                                            <asp:DropDownList ID="ddlprdtCategory1" runat="server" Width="162px" AutoPostBack="true" OnSelectedIndexChanged="ddlprdtCategory1_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Vendor Category
                                            <br />
                                            <asp:DropDownList ID="ddlVndrCategory1" runat="server" Width="162px" AutoPostBack="True" OnSelectedIndexChanged="ddlVndrCategory1_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Vendor Sub Category
                                            <br />
                                            <asp:DropDownList ID="ddlVendorSubCategory1" runat="server" Width="162px" AutoPostBack="True"></asp:DropDownList>
                                            <%--OnSelectedIndexChanged="ddlVendorSubCategory1_SelectedIndexChanged"--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SKU
                                            <br />
                                            <asp:DropDownList ID="DrpSku" runat="server" Width="162px">
                                                <asp:ListItem>--Select</asp:ListItem>
                                                <asp:ListItem Value="SOF001">SOF001</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Description
                                            <br />
                                            <asp:TextBox ID="TxtDescription" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="float: right; width: 76%; vertical-align: top;">
                                <asp:GridView ID="grdprimaryvendor" runat="server" AutoGenerateColumns="false" Width="100%"
                                    CssClass="tableClass" HeaderStyle-Wrap="true" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:BoundField HeaderText="Primary vendor - Vendor List" DataField="PrimaryVendor" />
                                        <asp:BoundField HeaderText="Total Cost:$ - Notes & fees " DataField="TotalCost" />
                                        <asp:BoundField HeaderText="UOM - $Unit Cost - $Bulk Cost" DataField="UnitCost" />
                                        <asp:BoundField HeaderText="Vendor Part#- Model#" DataField="VendorPart" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <br />
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
                            Style="float: right; text-decoration: none" />
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
                                    &nbsp;&nbsp;
                        <input type="button" id="btnCloseSrSalesmanF" class="btnClose" value="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
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

    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-ui.js"></script>
    <script src="../Scripts/jquery.maskedinput.min.js" type="text/javascript"></script>
    <style type="text/css">
        #tabs.ui-tabs {
            background: transparent;
        }

            #tabs.ui-tabs .ui-tabs-nav {
                height: auto;
                margin-left: 0;
            }

        .ui-tabs .ui-tabs-nav li {
            width: 20%;
        }

        #tabs.ui-tabs .ui-tabs-nav li.ui-tabs-selected {
            background: #ffffff;
        }

        .ui-tabs.ui-widget-content {
            border: 1px solid #aaaaaa !important;
        }

        .ui-tabs .ui-tabs-panel {
            padding: 10px 0px !important;
        }
    </style>
    <script type="text/javascript">

        SearchText();
        $('.clsmaskphone').mask("(999) 999-9999");
        $('.clsmaskphoneexten').mask("999999");

        var mapProp;
        function initialize() {
            mapProp = {
                center: new google.maps.LatLng(40.748492, -73.985496),
                zoom: 5,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
        }
        function SearchText() {
            $(".VendorSearchBox").autocomplete({
                minLength: 0,
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Procurement.aspx/SearchVendor",
                        data: "{'searchstring':'" + $(".VendorSearchBox").val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.parseJSON(data.d));
                        },
                        error: function (result) {
                            console.log("No Match");
                        }
                    });
                },
                select: function (event, ui) {
                    $(".VendorSearchBox").val(ui.item.value);
                    $("#hdnvendorId").val(ui.item.id);
                    $(".clsbtnEditVendor").trigger("click");
                    return false;
                }
            });
        }

        $('#tabs').tabs();
    </script>
</asp:Content>
