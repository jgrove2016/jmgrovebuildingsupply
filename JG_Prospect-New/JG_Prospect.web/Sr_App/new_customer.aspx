<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="new_customer.aspx.cs" Inherits="JG_Prospect.Sr_App.new_customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UCAddress.ascx" TagPrefix="uc1" TagName="UCAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%---------start script for Datetime Picker----------%>
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /*#ddlPrimaryPhoneType{
            width: 70%;
        }

        #txtPrimaryPhoneNumber{
            width: 60%;
        }

        .lblPhonePrimaryContact{
            width: 20%;
        }*/

        .GridView1 {
            border-collapse: collapse;
        }

        #container {
            width: 95%;
            height: 600px;
            margin: 10px;
        }

        .GridView1 > tbody > tr > td {
            border: 1px solid #CCCCCC;
            margin: 0;
            padding: 0;
        }

        .clsPhoneTable {
            /*border: 2px solid chocolate;*/
            border: 4px solid #CCCCCC;
            margin-left: 0px !important;
            margin-right: 0px !important;
            width: 100% !important;
            border-collapse: collapse;
        }

        #tblPhoneNo td {
            /*border:1px solid chocolate;*/
            border: 1px solid #CCCCCC;
            padding: 2px;
            height: 0px;
        }

        #map_canvas {
            float: left;
            width: 100%;
            height: 100%;
        }

        #tblPhoneNo th {
            /*border: 1px solid chocolate;*/
            border: 1px solid #FFFFFF;
            padding: 5px;
            height: 0px;
            /*background-color: bisque;*/
            background-color: #CCCCCC;
        }

        #divPrimaryContact ul li {
            border-right-style: none !important;
        }

        .clsFullWidth {
            width: 100% !important;
        }

        .clsPaddingLeft {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }

        .cls_btn_plus {
            background-color: RGBA(182,74,76,1);
            color: #fff;
            font-weight: bold;
            font-size: 14px;
            box-shadow: 0 0 15px #a1a0a0;
            cursor: pointer;
            border: 2px solid !important;
            border-radius: 6px;
            /* margin-bottom: 4px!important; */
            height: 27px;
            padding-right: 2px !important;
        }

        .clsOverFlow {
            overflow: auto;
            height: 150px;
        }

        .clsGrid {
            overflow: inherit !important;
        }

        .clsCheckBox {
            text-align: left !important;
            display: inline !important;
        }

        .drop_down {
            display: block;
            width: 120px !important;
        }


        .tblPrimaryContact tr td {
            padding-left: 0px !important;
            padding-right: 15px !important;
            padding-bottom: 0px !important;
            padding-top: 0px !important;
            height: 15px !important;
            background-image: none !important;
        }

            .tblPrimaryContact tr td input {
                width: 100% !important;
            }

            .tblPrimaryContact tr td label {
                width: 100% !important;
            }

        .tblPrimaryContact tr td {
            position: relative !important;
        }

            .tblPrimaryContact tr td span {
                position: absolute !important;
                top: 35px !important;
            }

            .tblPrimaryContact tr td [value="Add"] {
                width: 50px !important;
            }

            .tblPrimaryContact tr td [type="checkbox"] {
                width: 30px !important;
                margin-left: -5px !important;
            }

        #divPrimaryContact > ul {
            overflow: hidden !important;
        }

        .paddingtd {
            padding-right: 0px !important;
        }

        #btnHideSubmit {
            display: none;
        }

        #ContentPlaceHolder1_UpdatePanel4 {
            position: absolute;
            left: 250px;
        }

        .tblBestTimeToContact tr td {
            width: 25%;
            background: transparent !important;
        }

        .nopadding {
            padding: 0px!important;
        }

        .size34 {
            width: 34%;
            float: left;
        }

            .size34 label {
                width: 100%!important;
            }

        .size66 {
            width: 66%;
            float: right;
        }

        .tblBestTimeToContact > tbody tr td {
            height: 0px!important;
            /*padding: 5px 0px!important;*/
            padding: 0px 0px!important;
        }
    </style>
    <script src="../Scripts/jquery.maskedinput.min.js" type="text/javascript"></script>
    <%-- <script>
        var myCenter = new google.maps.LatLng(40.748492, -73.985496);

        function initialize() {
            var mapProp = {
                center: myCenter,
                zoom: 5,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById("map_canvas"), mapProp);

            var marker = new google.maps.Marker({
                position: myCenter,
                title: 'Click to zoom'
            });

            marker.setMap(map);

            // Zoom to 9 when clicking on marker
            google.maps.event.addListener(marker, 'click', function () {
                map.setZoom(9);
                map.setCenter(marker.getPosition());
            });

            google.maps.event.addListener(map, 'center_changed', function () {
                // 3 seconds after the center of the map has changed, pan back to the marker
                window.setTimeout(function () {
                    map.panTo(marker.getPosition());
                }, 3000);
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
</script>--%>

    <script language="javascript" type="text/javascript">
        var directionsDisplay;
        var directionsService = new google.maps.DirectionsService();

        function InitializeMap() {
            directionsDisplay = new google.maps.DirectionsRenderer();
            var latlng = new google.maps.LatLng(40.748492, -73.985496);
            var myOptions =
            {
                zoom: 10,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map"), myOptions);

            directionsDisplay.setMap(map);
            directionsDisplay.setPanel(document.getElementById('directionpanel'));

            var control = document.getElementById('control');
           // control.style.display = 'block';


        }



        function calcRoute() {

            var start = document.getElementById('startvalue').value;
            var end = document.getElementById('endvalue').value;
            var request = {
                origin: start,
                destination: end,
                travelMode: google.maps.DirectionsTravelMode.DRIVING
            };
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                }
            });

        }



        function Button1_onclick() {
            calcRoute();
        }

        window.onload = InitializeMap;
    </script>

    <script language="JavaScript" type="text/javascript">
        var PrimaryRadio = 0;
        var SecondaryRadio = 0;

        function GetCityStateOnBlur(e) {
            debugger;
            $.ajax({
                type: "POST",
                url: "new_customer.aspx/GetCityState",
                data: "{'strZip':'" + $(e).val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    debugger;
                    //alert(data.d);
                    var dataInput = (data.d).split("@^");
                    $(e).closest('tr').next().find('input').val(dataInput[0]);
                    $(e).closest('tr').next().next().find('input').val(dataInput[1]);
                }
            });
        }


        function CheckForDuplication() {
            debugger;
            //Get the Value an assign hidden field
            var tbl = document.getElementById("tblBestTime");
            var row = tbl.getElementsByTagName("tr");
            var value = "";
            var BestTime = new Array();
            if (row.length > 1) {
                for (i = 1; i < row.length; i++) {
                    if (BestTime == "") {
                        value = row[i].cells[0].innerText.split('X');
                        BestTime = value[0].trim();;
                    }
                    else {
                        value = row[i].cells[0].innerText.split('X');
                        BestTime += ',' + value[0].trim();
                    }
                }
                $('#hdnBestTimeToContact').val(BestTime);
            }
            var formData = [];
            var formPushData = [];
            $("#form1").find("input[name]:text,select[name],input:hidden[name][id^='hdn'],input[name]:radio,textarea[name],input[name]:checkbox").each(function (index, node) {

                //formData[node.name] = node.value;
                if (node.type == "checkbox") {
                    node.value = $('#' + node.id).is(':checked');
                }
                if (node.type == "radio") {
                    debugger;
                    if ($('#' + node.id).is(':checked') == true) {
                        formPushData.push({
                            key: node.name,
                            value: node.value
                        });
                    }

                    //   node.value = $('#' + node.id).is(':checked');
                    // node.value = $('input[type="radio"]:checked').val();
                    //  node.value = $('#' + node.id).hasClass(':checked');
                }

                else {
                    formPushData.push({
                        key: node.name,
                        value: node.value
                    });
                }

            });
            $.ajax({
                type: "POST",
                url: "new_customer.aspx/CheckForDuplication",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                //data: "{ 'formData' : '" + formData + "'}",
                data: JSON.stringify({ formVars: formPushData }),
                success: function (data) {
                    debugger;
                    var dataInput = (data.d);

                    if (dataInput == "") {
                        alert("Some error occured in checking customer duplication. Please Try again.")
                        return;
                    }
                        //else if (dataInput == "EmptyMail") {
                        //    alert("Please enter the mail id");
                        //    return;
                        //}
                    else if (dataInput == "PhoneNumberEmpty") {
                        alert("Please enter the Phone Number");
                        return;
                    }
                    else if (dataInput == "EmptyName") {
                        alert("Please enter the FirstName");
                        return;
                    }
                    else if (dataInput != "Contact is NOT Exists") {
                        if (confirm("Contacts are duplicated. Are you sure want to update the existing contact?")) {
                            $("#hdnStatus").attr('value', 1);
                            $("#btnHideSubmit").click();
                        }
                        else {
                            $("#hdnStatus").attr('value', 0);
                            $("#btnHideSubmit").click();
                        }
                    }
                    else {
                        $("#hdnStatus").attr('value', 1);
                        $("#btnHideSubmit").click();
                    }
                }
            });
        }

        function onclientselect(source, eventArgs) {
            debugger;
            var id = source._element.id;
            $.ajax({
                type: "POST",
                url: "new_customer.aspx/GetCityState",
                data: "{'strZip':'" + $(".list_limit li[style*='background-color: lemonchiffon']").text() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    debugger;
                    //alert(data.d);
                    var dataInput = (data.d).split("@^");
                    $(source._element).closest('tr').next().find('input').val(dataInput[0]);
                    $(source._element).closest('tr').next().next().find('input').val(dataInput[1]);
                }
            });
        }

        function AddDayTime(e) {
            if ($("#txtBestDayToContact").val().trim() == "") {
                alert("Please Select Best Day to Contact");
                return false;
            }

            if ($("#txtBestStartTime").val().trim() == "") {
                alert("Please Select Start Time to Contact");
                return false;
            }

            if ($("#txtBestEndTime").val().trim() == "") {
                alert("Please Select End Time to Contact");
                return false;
            }
            debugger;
            var strStartTime = $("#txtBestStartTime").val().trim();
            var strEndTime = $("#txtBestEndTime").val().trim();
            var strStartMeredian = strStartTime.slice(-2);
            var strEndmeredian = strEndTime.slice(-2);
            var strStartTimeOnly = strStartTime.substring(0, strStartTime.length - 2);
            var strEndTimeOnly = strEndTime.substring(0, strEndTime.length - 2);

            if (strStartMeredian == strEndmeredian) {
                if (strEndTime == "12AM") {
                    alert("End Time must be greater than Start Time.");
                    $("#txtBestEndTime").val('');
                    return false;
                }
                else if ((parseInt(strStartTimeOnly) > parseInt(strEndTimeOnly)) && strStartTime != "12AM") {
                    alert("End Time must be greater than Start Time.");
                    $("#txtBestEndTime").val('');
                    return false;
                }
                else if (parseInt(strStartTimeOnly) == parseInt(strEndTimeOnly)) {
                    alert("End Time must be greater than Start Time.");
                    $("#txtBestEndTime").val('');
                    return false;
                }
            }
            else if (strStartMeredian == "PM" && strEndmeredian == "AM") {
                alert("End Time must be greater than Start Time.");
                $("#txtBestEndTime").val('');
                return false;
            }

            var strMultiSelect = "<tr><td colspan='4'>" + $("#txtBestDayToContact").val() + " " + strStartTime + " - " + strEndTime +
                                " <button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeTime(this)'>X</button>" +
                                "</td></tr>";
            //$(e).closest('tr').after(strMultiSelect);
            //$(".tblBestTimeToContact>tbody>tr:last").append(strMultiSelect);
            $(".tblBestTimeToContact>tbody").append(strMultiSelect);

            $("#txtBestDayToContact").val('');
            $("#txtBestStartTime").val('');
            $("#txtBestEndTime").val('');
        }

        $(document).ready(function () {

            $(".date").datepicker();
            $('.time').ptTimeSelect();
            $('.clsMaskPhone').mask("999-999-9999");
            //$('#txtBestTimetoContact').ptDaySelect({});
            $('#txtBestDayToContact').ptDayOnlySelect({});
            $('#txtBestStartTime').ptTimeOnlySelect({});
            $('#txtBestEndTime').ptTimeOnlySelect({});

            //$("#btnSubmit").click(function () {
            //    var isduplicate = document.getElementById('hdnisduplicate').value;
            //    var custid = document.getElementById('hdnCustId').value;
            //    if (isduplicate.toString() == "1") {
            //        if (confirm('Duplicate contact, Press Ok to add the another appointment for existing customer.')) {
            //            window.open("../Prospectmaster.aspx?title=" + custid);
            //        }
            //        else {
            //            // alert('false');
            //        }
            //    }
            //});
        });


        function fnCheckOne(me) {
            //debugger;
            //me.checked = true;
            //var chkary = document.getElementsByTagName('input');
            //for (i = 0; i < chkary.length; i++) {
            //    if (chkary[i].type == 'checkbox') {
            //        if (chkary[i].id != me.id)
            //            chkary[i].checked = false;
            //    }
            //}
            $(me).closest('tr').find('input:checkbox').removeAttr('checked');
            me.checked = true;
        }
        function CalendarEvent(attendees, calendarId, colorId, description, endDate, id, location, startDate, title, calendarIds) {
            this.Attendees = attendees;
            this.CalendarId = calendarId;
            this.ColorId = colorId;
            this.Description = description;
            this.EndDate = endDate;
            this.Id = id;
            this.Location = location;
            this.StartDate = startDate;
            this.Title = title;
            this.CalendarId = calendarIds;
        }
        function InsertEvent(ce) {

            alert('hit');
            var url = 'http://localhost:118/' + 'api/Common/Post';
            var m = JSON.parse(ce);

            var mm = [];
            var n = m.Attendees.toString().split(',');
            for (var i = 0; i < n.length; i++) {
                mm[i] = n[i];
            }

            var sa = {
                Attendees: mm,
                CalendarId: m.CalendarId,
                ColorId: m.ColorId,
                Description: m.Description,
                EndDate: m.EndDate,
                Id: m.Id,
                Location: m.Location,
                StartDate: m.StartDate,
                Title: m.Title,
            };


            try {
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(sa),
                    url: url,
                    contentType: "application/json",
                    success: function (data) {
                        alert(data);

                    }
                });


            } catch (e) {
                alert(e.message);

            }
        }
        function AddTemplate(e) {
            debugger;

            var liCount = $("#divPrimaryContact ul li").length + 1;

            $(e).closest('li').after("<li style='width: 100%;'><div class='tblPrimaryContact' style='margin-top: 10px; width: 100%'><div style='width: 40%; float: left;'>" +
            "<table><tr><td><input type='checkbox'  name='chkContactType " + liCount + "' /></td><td><select clientidmode='Static' id='selContactType" + liCount + "'name='selContactType" + liCount + "' tabindex='4' class='drop_down'><option value='0'>Select</option>" +
            "<option value='DM'>DM</option><option value='Spouse'>Spouse</option><option value='Partner'>Partner</option><option value='Others'>Others</option></select><label></label>" +
            "</td><td><input type='text' id='txtFName" + liCount + "' tabindex='7' name='nametxtFName" + liCount + "'  placeholder='First Name' data-type='" + liCount + "' /></td><td>" +
            "<input type='text' tabindex='7' id='txtLName" + liCount + "' name='nametxtLName" + liCount + "'  placeholder='Last Name' data-type='" + liCount + "' /></td></tr><tr><td class='paddingtd'>" +
            "<input type='button' value='Add' data-type='" + liCount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='AddTemplate(this)' /></td>" +
            "</tr></table></div><div style='width: 40%; float: left;'><table><tr><td class='paddingtd'></td><td>" +
            "<input type='text' clientidmode='Static' id='txtPhone" + liCount + "' name='nametxtPhone" + liCount + "' data-type='" + liCount + "' tabindex='7' class='clsMaskPhone'  placeholder='___-___-____' /></td>" +
            "<td><label class='clsFullWidth'>Phone Type</label></td><td><select class='clsFullWidth' id='selPhoneType" + liCount + "' name='nameselPhoneType" + liCount + "' data-type='" + liCount + "' clientidmode='Static' tabindex='4'>" +
            "<option value='0'>Select</option><option value='CellPhone'>Cell Phone #</option><option value='HousePhone'>House Phone #</option><option value='WorkPhone'>Work Phone #</option><option value='AltPhone'>Alt. Phone #</option>" +
            "</select></td></tr><tr><td class='paddingtd'><input type='button' value='Add' data-type='" + liCount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='Phone(this)' /></td>" +
            "</tr></table></div><div style='width: 20%; float: left;'><table><tr><td class='paddingtd'></td>" +
            "<td><input type='text' clientidmode='Static' id='txtEMail" + liCount + "' name='nametxtEMail" + liCount + "' data-type='" + liCount + "' tabindex='7'  placeholder='EMail' /></td></tr><tr><td class='paddingtd'>" +
            "<input type='button' value='Add' data-type='" + liCount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='Email(this)' /></td></tr></table></div></div></li>");
            $('.clsMaskPhone').mask("999-999-9999");
            $(e).css("visibility", "hidden");
        }


        function Phone(e) {
            debugger;
            var dataTypeValue = $(e).attr("data-type");
            var subCount = $(e).closest('table').find('tr').length - 1;
            $(e).closest('tr').prev().after("<tr><td class='paddingtd'></td><td><input type='text' clientidmode='Static' id='txtPhone" + dataTypeValue + subCount + "' name='nametxtPhone" + dataTypeValue + subCount + "' tabindex='7' class='clsMaskPhone' placeholder='___-___-____' /></td>" +
            "<td><label class='clsFullWidth'>Phone Type</label></td><td><select id='selPhoneType" + dataTypeValue + subCount + "' name='nameselPhoneType" + dataTypeValue + subCount + "' class='clsFullWidth' clientidmode='Static' tabindex='4'>" +
            "<option value='0'>Select</option><option value='CellPhone'>Cell Phone #</option><option value='HousePhone'>House Phone #</option><option value='WorkPhone'>Work Phone #</option>" +
            "<option value='AltPhone'>Alt. Phone #</option></select></td></tr>");
            $('.clsMaskPhone').mask("999-999-9999");
        }

        function Email(e) {
            debugger;
            //<input type='text' ID='TextBox2' TabIndex='7' MaxLength='15' placeholder='EMail' value='bbb@gmail.com'>
            var dataTypeValue = $(e).attr("data-type");
            var subCount = $(e).closest('table').find('tr').length - 1;
            $(e).closest('tr').prev().after("<tr><td class='paddingtd'></td><td>" +
                                            "<input type='text' id='txtEMail" + dataTypeValue + subCount + "' tabindex='7' name='nametxtEMail" + dataTypeValue + subCount + "'  placeholder='EMail' clientidmode='Static' /></td></tr>");
        }
        function AddAddress(e) {
            debugger;
            var lastChar = $(e).closest('li').prev().find('table tr:last').text().trim().split("*")[0];
            var count = lastChar.slice(-1);
            if (count != "e") {
                count++;
            }
            else {
                count = 1;
            }
            $(e).closest('li').prev().find('table tr:last').after("<tr><td><div><input type='checkbox' id='chkaddress" + count + "' style='width:5%' tabindex='20' checked='checked' /></div>" +
            "<label>Address" + count + "<span>*</span></label><textarea ID='txtaddress" + count + "' TabIndex='9' value=''></textarea><label></label><span id='spnAddress" + count + "'></span></br></br><label> Address Type" + count + "<span>*</span></label><select id='selAddressType" + count + "' ><option Value='Select'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>" +
            "<tr><td><label>Zip" + count + "<span>*</span></label><input type='text' value='' ID='txtzip" + count + "' onkeypress='return isNumericKey(event);'  tabindex='11' OnTextChanged='txtzip_TextChanged' />" +
            "</td></tr><tr><td><label>City" + count + "<span>*</span></label><input type='text' value='' ID='txtcity" + count + "' onkeypress='return isAlphaKey(event);'  tabindex='13' />" +
            "</td></tr><tr><td><label>State" + count + "<span>*</span></label><input type='text' value='' ID='txtstate" + count + "' onkeypress='return isAlphaKey(event);'  tabindex='15' /></td></tr>");
        }

        function BillingAddress(e) {
            debugger;
            var lastChar = $(e).closest('tr').text().trim().split(" ")[1];
            var count = lastChar.slice(-1);
            if (count == 's') {
                var PrimaryBillingAddress = $("#txtbill_address").val();
                var PrimaryBillingAddressType = $('#selAddressType').val();
            }
            else {
                var PrimaryBillingAddress1 = $("#txtbill_address" + count).val();
                var PrimaryBillingAddressType1 = $('#selAddressType' + count).val();
            }

            //if (count != "s") {
            //    var NewCount = parseInt(count, 10) + 1
            //    if ($("#chkbillingaddress" + count).is(':checked')) {
            //        $("#txtbill_address" + count).val($("#txtaddress" + count).val() + " " + $("#txtcity" + count).val() + " "
            //            + $("#txtstate" + count).val() + " " + $("#txtzip" + count).val());
            //        $(e).closest('tr').next().remove();
            //    }
            //    else {
            //        $("#txtbill_address" + count).val("");
            //        $(e).closest('tr').after("<tr><td><div>" +
            //        "<input type='checkbox' id='chkbillingaddress" + NewCount + "' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
            //        "</div><label><span>*</span>Billing Address" + NewCount + " Same</label><textarea id='txtbill_address" + NewCount + "' name='BillAddress" + NewCount + "' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress" + NewCount + "'></span></br></br><label> Address Type" + NewCount + "<span>*</span></label><select id='selAddressType" + NewCount + "' name='AddressType" + NewCount + "'><option Value='0'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
            //    }
            //}
            //else {

            //    if ($("#chkbillingaddress").is(':checked')) {
            //        $("#txtbill_address").val($("#ContentPlaceHolder1_txtaddress").val() + " " + $("#ContentPlaceHolder1_txtcity").val() + " " + $("#ContentPlaceHolder1_txtstate").val() + " " +
            //        $("#ContentPlaceHolder1_txtzip").val());
            //        $(e).closest('tr').next().remove();
            //    }
            //    else {
            //        $("#txtbill_address").val("");
            //        $(e).closest('tr').after("<tr><td><div>" +
            //       "<input type='checkbox' id='chkbillingaddress1' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
            //       "</div><label><span>*</span>Billing Address1 Same</label><textarea id='txtbill_address1' name='BillAddress1' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress1'></span></br></br><label> Address Type1 <span>*</span></label><select id='selAddressType1' name='AddressType'><option Value='0'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
            //    }
            //}
            if (count != "s") {
                //$('#txtbill_address1').text("");
                //$('#selAddressType1').val("0");
                var NewCount = parseInt(count, 10) + 1
                if ($("#chkbillingaddress" + count).is(':checked')) {
                    for (var i = 0; i < $(e).closest('tr').nextAll().length + 1; i++) {
                        if ($(e).closest('tr').next().children().children()[2] != undefined) {
                            $(e).closest('tr').next().remove();
                        }
                    }
                    // $(e).closest('tr').nextAll('tr:not(.bt)').remove();
                }
                else {
                    $("#txtbill_address" + count).val("");
                    $("#selAddressType" + count).val("Select");
                    $(e).closest('tr').after("<tr><td><div>" +
                    "<input type='checkbox' id='chkbillingaddress" + NewCount + "' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
                    "</div><label><span>*</span>Billing Address" + NewCount + " Same</label><textarea id='txtbill_address" + NewCount + "' name='BillAddress" + NewCount + "' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress" + NewCount + "'></span></br></br><label> Address Type" + NewCount + "<span>*</span></label><select id='selAddressType" + NewCount + "' name='AddressType" + NewCount + "'><option Value='Select'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
                    $("#txtbill_address" + NewCount + "").val(PrimaryBillingAddress1);
                    $("#selAddressType" + NewCount + "").val(PrimaryBillingAddressType1);
                }
            }
            else {
                if ($("#chkbillingaddress").is(':checked')) {
                    //$(e).closest('tr').find('textarea').val();
                    //$('#selAddressType').val();
                    for (var i = 0; i < $(e).closest('tr').nextAll().length + 1; i++) {
                        if ($(e).closest('tr').next().children().children()[2] != undefined) {
                            $(e).closest('tr').next().remove();
                        }
                    }

                }
                else {
                    $("#txtbill_address").val("");
                    $('#selAddressType').val("Select");
                    $(e).closest('tr').after("<tr><td><div>" +
                   "<input type='checkbox' id='chkbillingaddress1' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
                   "</div><label><span>*</span>Billing Address1 Same</label><textarea id='txtbill_address1' name='BillAddress1' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress1'></span></br></br><label> Address Type1 <span>*</span></label><select id='selAddressType1' name='AddressType'><option Value='Select'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
                    $('#txtbill_address1').val(PrimaryBillingAddress);
                    $('#selAddressType1').val(PrimaryBillingAddressType);
                    // $('#selAddressType1').val(UcSelAddress.options[UcSelAddress.selectedIndex].value);
                }
            }
        }

        function PrimaryProduct(e) {
            debugger;
            //Awning - 01-12-2015: O Service Or O Est X

            var CurrentDate = new Date;
            var date = CurrentDate.getDate() + "-" + parseInt(CurrentDate.getMonth() + 1) + "-" + CurrentDate.getFullYear() + ": ";
            $(e).after("<div style='float:right;width: 100%;text-align: right;'>" + e.options[e.selectedIndex].innerHTML + " - " + date +
                "<input type='radio' name='primaryRadio" + PrimaryRadio + "' style='width:12px' id='primaryRadio1" + PrimaryRadio + "'  value='Service' /> Service Or" + "<input name='primaryRadio" + PrimaryRadio
                + "' type='radio' style='width:12px'  id='primaryRadio2" + PrimaryRadio + "' value='Est' />Est " +
                "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnPrimaryId' name='hdnPrimaryId' value='" + $(e).val() + "'/><input type='hidden' id='hdnPrimaryType' name='hdnPrimaryType' value='Primary'/></div>");
            PrimaryRadio++;
        }

        function removeProduct(e) {
            e.closest('div').remove()
        }

        function removeTime(e) {
            e.closest('tr').remove();
        }

        function SecondaryProduct(e) {
            var CurrentDate = new Date;
            var date = CurrentDate.getDate() + "-" + parseInt(CurrentDate.getMonth() + 1) + "-" + CurrentDate.getFullYear() + ": ";
            $(e).after("<div style='float:right;width: 100%;text-align: right;'>" + e.options[e.selectedIndex].innerHTML + " - " + date +
                "<input type='radio' name='SecondaryRadio" + SecondaryRadio + "' id='SecondaryRadio1" + SecondaryRadio + "' style='width:12px'  value='Service' /> Service Or <input type='radio' id='SecondaryRadio2" + SecondaryRadio + "' name='SecondaryRadio" + SecondaryRadio
                + "' style='width:12px' value='Est' /> Est " +
                "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnSecondaryId' name='hdnSecondaryId' value='" + $(e).val() + "'/><input type='hidden' id='hdnSecondaryType' name='hdnSecondaryType' value='Secondary'/></div>");
            SecondaryRadio++;
        }

    </script>

    <code>$('#time1 input').ptTimeSelect({ onBeforeShow: function(i){ $('#time1 #time1-data').append('onBeforeShow(event)
        Input field: ' + $(i).attr('name') + "<br />
        "); }, onClose: function(i) { $('#time1 #time1-data').append('onClose(event)Time
        selected:' + $(i).val() + "<br />
        "); } }); $('#time2 input').ptTimeSelect({ onBeforeShow: function(i){ $('#time2
        #time2-data').append('onBeforeShow(event) Input field: ' + $(i).attr('name') + "<br />
        "); }, onClose: function(i) { $('#time2 #time2-data').append('onClose(event)Time
        selected:' + $(i).val() + "<br />
        "); } }); </code>
    <%---------end script for Datetime Picker----------%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <asp:Panel ID="pan1" runat="server" ScrollBars="Auto">--%>
        <div class="right_panel">
            <!-- Tabs starts -->
            <!-- appointment tabs section start -->
            <ul class="appointment_tab">
                <li><a href="home.aspx">Personal Appointment</a></li>
                <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
                <li><a href="#">Construction Calendar</a></li>
                <li><a href="CallSheet.aspx">Call Sheet</a></li>
            </ul>
            <!-- appointment tabs section end -->
            <h1>Add Customer</h1>
            <div class="form_panel_custom">
                <span>*Check Primary Contact 
                </span>
                <span>
                    <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                </span>
                <div style="width: 100%" id="divPrimaryContact">
                    <ul>
                        <li style="width: 100%;">
                            <div class="tblPrimaryContact" style="margin-top: 10px; width: 100%">
                                <div style="width: 40%; float: left;">
                                    <table>

                                        <tr>
                                            <td>
                                                <input type="checkbox" id="chkContactType1" name="chkContactType1" />
                                            </td>
                                            <td>
                                                <select id="selContactType1" clientidmode="Static" runat="server" tabindex="4" name="selContactType1" class="drop_down">
                                                    <option value="0">Select</option>
                                                    <option value="DM">DM</option>
                                                    <option value="Spouse">Spouse</option>
                                                    <option value="Partner">Partner</option>
                                                    <option value="Others">Others</option>
                                                </select>
                                                <label></label>
                                            </td>
                                            <td>
                                                <input type="text" id="txtFName1" runat="server" tabindex="7" placeholder="First Name" />
                                            </td>
                                            <td>
                                                <input type="text" id="txtLName1" runat="server" tabindex="7" placeholder="Last Name" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="paddingtd">
                                                <input type="button" id="Button8" style="visibility: hidden" value="Add" class="clsFullWidth cls_btn_plus" tabindex="31" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 40%; float: left;">
                                    <table>
                                        <tr>
                                            <td class="paddingtd"></td>
                                            <td>
                                                <input type="text" id="txtPhone1" runat="server" class='clsMaskPhone' clientidmode="Static" data-type="1" tabindex="7" placeholder="___-___-____" />
                                            </td>
                                            <td>
                                                <label class="clsFullWidth">Phone Type</label>
                                            </td>
                                            <td>
                                                <select id="selPhoneType1" class="clsFullWidth" clientidmode="Static" data-type="1" runat="server" tabindex="4" name="selPhoneType1">
                                                    <option value="0">Select</option>
                                                    <option value="CellPhone">Cell Phone #</option>
                                                    <option value="HousePhone">House Phone #</option>
                                                    <option value="WorkPhone">Work Phone #</option>
                                                    <option value="AltPhone">Alt. Phone #</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="paddingtd">
                                                <input type="button" id="Button4" runat="server" value="Add" data-type="1" class="clsFullWidth cls_btn_plus" tabindex="31"
                                                    onclick="Phone(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 20%; float: left;">
                                    <table>
                                        <tr>
                                            <td class="paddingtd"></td>
                                            <td>
                                                <input type="text" id="txtEMail1" runat="server" tabindex="7" placeholder="EMail" clientidmode="Static" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="paddingtd">
                                                <input type="button" id="Button7" runat="server" value="Add" data-type="1" class="clsFullWidth cls_btn_plus" tabindex="31"
                                                    onclick="Email(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </li>

                        <li style="width: 100%;">
                            <div class="tblPrimaryContact" style="margin-top: 10px; width: 100%">
                                <div style="width: 40%; float: left;">
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="checkbox" id="chkContactType2" name="namechkContactType2" />
                                            </td>
                                            <td>
                                                <select id="selContactType2" clientidmode="Static" runat="server" tabindex="4" name="selContactType2" class="drop_down">
                                                    <option value="0">Select</option>
                                                    <option value="DM">DM</option>
                                                    <option value="Spouse">Spouse</option>
                                                    <option value="Partner">Partner</option>
                                                    <option value="Others">Others</option>
                                                </select>
                                                <label></label>
                                            </td>
                                            <td>
                                                <input type="text" id="txtFName2" runat="server" tabindex="7" placeholder="First Name" />
                                            </td>
                                            <td>
                                                <input type="text" id="txtLName2" runat="server" tabindex="7" placeholder="Last Name" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="paddingtd">
                                                <input type="button" id="Button1" runat="server" value="Add" class="clsFullWidth cls_btn_plus" tabindex="31"
                                                    onclick="AddTemplate(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 40%; float: left;">
                                    <table>
                                        <tr>
                                            <td class="paddingtd"></td>
                                            <td>
                                                <input type="text" id="txtPhone2" runat="server" tabindex="7" data-type="2" class='clsMaskPhone' clientidmode="Static" placeholder="___-___-____" />
                                            </td>
                                            <td>
                                                <label class="clsFullWidth">Phone Type</label>
                                            </td>
                                            <td>
                                                <select id="selPhoneType2" class="clsFullWidth" clientidmode="Static" runat="server" tabindex="4" data-type="2">
                                                    <option value="0">Select</option>
                                                    <option value="CellPhone">Cell Phone #</option>
                                                    <option value="HousePhone">House Phone #</option>
                                                    <option value="WorkPhone">Work Phone #</option>
                                                    <option value="AltPhone">Alt. Phone #</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="paddingtd">
                                                <input type="button" id="Button2" runat="server" value="Add" class="clsFullWidth cls_btn_plus" tabindex="31" data-type="2"
                                                    onclick="Phone(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width: 20%; float: left;">
                                    <table>
                                        <tr>
                                            <td class="paddingtd"></td>
                                            <td>
                                                <input type="text" id="txtEMail2" runat="server" tabindex="7" placeholder="EMail" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="paddingtd">
                                                <input type="button" id="Button3" runat="server" value="Add" class="clsFullWidth cls_btn_plus" tabindex="31" data-type="2"
                                                    onclick="Email(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="grid_h">
                    <label id="lblTPLHeading" runat="server">
                        Touch Point Log</label>
                </div>

                <div class="grid">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--<asp:Panel ID="pnlAddress" runat="server"></asp:Panel>--%>
                            <asp:PlaceHolder runat="server" ID="PlaceHolder2"></asp:PlaceHolder>
                            <asp:GridView ID="grdTouchPointLog" EmptyDataText="No Data Found" runat="server" Width="100%" AutoGenerateColumns="false"
                                OnRowDataBound="grdTouchPointLog_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="User Id" DataField="Email" />
                                    <asp:BoundField HeaderText="SoldJobId" DataField="SoldJobId" />
                                    <asp:BoundField HeaderText="Date & Time" DataField="Date" />
                                    <asp:BoundField HeaderText="Note / Status" DataField="Status" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddNotes" />
                        </Triggers>
                        <%--   <asp:Button ID="btnAddNotes" runat="server" Text="Add Notes" OnClick="btnAddNotes_Click" ClientIDMode="Static" />--%>
                    </asp:UpdatePanel>
                </div>
                <br />
                <table cellspacing="0" cellpadding="0" width="950px" border="1" style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <td>
                            <div class="btn_sec">
                                <asp:Button ID="btnAddNotes" runat="server" Text="Add Notes" OnClick="btnAddNotes_Click" />
                            </div>
                        </td>
                        <td>
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:PlaceHolder runat="server" ID="PlaceHolder3"></asp:PlaceHolder>
                                        <asp:TextBox ID="txtAddNotes" runat="server" TextMode="MultiLine" Height="33px" Width="407px"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAddNotes" />
                                    </Triggers>

                                </asp:UpdatePanel>

                            </div>

                        </td>
                        <td>
                            <span id="spanS3">
                                <%--<label>Status</label></span>--%>
                                <%--<asp:DropDownList ID="ddlfollowup3" AutoPostBack="false" ClientIDMode="Static" runat="server" TabIndex="4">
                        </asp:DropDownList>--%>
                        </td>
                    </tr>
                </table>
                <div class="grid_h">
                    <strong>Customer Quality</strong>
                </div>
                <ul>
                    <li style="width: 49%;">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <%-- <tr>
                            <td>
                                <label>
                                    Service or EST<span></span></label>
                                <asp:DropDownList ID="ddlServiceEst" AutoPostBack="true" ClientIDMode="Static" runat="server" TabIndex="4">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Demographics-Age</asp:ListItem>
                                    <asp:ListItem>Property Value</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                            </td>
                        </tr>--%>
                            <tr>
                                <td>
                                    <label>
                                        Primary Product of Interest (3 months)</label>
                                    <asp:DropDownList ID="drpProductOfInterest1" runat="server" onchange="PrimaryProduct(this)" TabIndex="29">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <%-- <span>*</span> Contact Preference</label>--%>
                                     Contact Preference</label>
                                    <asp:CheckBox ID="chbemail" runat="server" Width="14%" Text="Email " TabIndex="17"
                                        onclick="fnCheckOne(this)" />
                                    <asp:CheckBox ID="chbcall" runat="server" Text="Call" Checked="false" Width="14%"
                                        TabIndex="18" onclick="fnCheckOne(this)" />
                                    <asp:CheckBox ID="chbtext" runat="server" Width="14%" Text="Text " TabIndex="17"
                                        onclick="fnCheckOne(this)" />
                                    <asp:CheckBox ID="chbmail" runat="server" Text="Mail" Width="14%"
                                        TabIndex="18" onclick="fnCheckOne(this)" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Estimate Date</label>
                                    <asp:TextBox ID="txtestimate_date" CssClass="date" TabIndex="5" runat="server" onkeypress="return false"></asp:TextBox>
                                    <%--<input type="text" name="textfield" id="textfield" />--%>
                                    <label>
                                    </label>
                                </td>
                            </tr>

                            <uc1:UCAddress runat="server" ID="UCAddress" />

                            <asp:UpdatePanel ID="panel4" runat="server">
                                <ContentTemplate>
                                    <%--<asp:Panel ID="pnlAddress" runat="server"></asp:Panel>--%>
                                    <asp:PlaceHolder runat="server" ID="myPlaceHolder"></asp:PlaceHolder>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnHideSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <%--<tr>
                            <td>
                                <uc1:UCAddress runat="server" id="UCAddress1" />
                            </td>
                        </tr>--%>

                            <%--<tr>
                            <td>
                                
                            </td>
                        </tr>--%>
                        </table>
                    </li>
                    <li class="last" style="width: 49%;">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <label>
                                        Secondary Product of Interest (6 months - 1 year)</label>
                                    <asp:DropDownList ID="drpProductOfInterest2" runat="server" onchange="SecondaryProduct(this)" TabIndex="30">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Competitor Bids<span></span></label>
                                    <asp:TextBox ID="txtCompetitorBids" runat="server" TabIndex="6">
                                    </asp:TextBox>
                                    <label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="size34">
                                        <label>
                                            Best Time To Contact</label>
                                    </div>
                                    <div class="size66">
                                        <%--<asp:DropDownList ID="ddlbesttime" runat="server" TabIndex="27">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Afternoon</asp:ListItem>
                                    <asp:ListItem>Evening</asp:ListItem>
                                </asp:DropDownList>--%>
                                        <%--<asp:TextBox ID="txtBestTimetoContact" runat="server" TabIndex="6" ClientIDMode="Static"
                                    onkeypress="return false"></asp:TextBox>
                                <label>
                                </label>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Best Time To Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlbesttime"></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Best Time To Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="txtBestTimetoContact"></asp:RequiredFieldValidator>--%>

                                        <table class="tblBestTimeToContact" id="tblBestTime" runat="server" clientidmode="Static">
                                            <tr>
                                                <td class="nopadding" style="width: 160px; padding-right: 10px!important;">
                                                    <asp:TextBox ID="txtBestDayToContact" runat="server" TabIndex="6" ClientIDMode="Static"
                                                        onkeypress="return false" Width="90%"></asp:TextBox>
                                                </td>
                                                <td class="nopadding" style="width: 95px;">
                                                    <asp:TextBox ID="txtBestStartTime" runat="server" TabIndex="6" ClientIDMode="Static"
                                                        onkeypress="return false"></asp:TextBox>
                                                </td>
                                                <td class="nopadding" style="width: 95px;">
                                                    <asp:TextBox ID="txtBestEndTime" runat="server" TabIndex="6" ClientIDMode="Static"
                                                        onkeypress="return false"></asp:TextBox>
                                                </td>
                                                <td class="nopadding">
                                                    <input type="button" id="btnAddTime" runat="server" value="Add" class="clsFullWidth cls_btn_plus" tabindex="31"
                                                        onclick="AddDayTime(this)" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--  <label>
                                    Estimate Time<span>*</span></label>--%>
                                    <label>
                                        Estimate Time</label>
                                    <asp:TextBox ID="txtestimate_time" CssClass="time" runat="server" TabIndex="6"
                                        onkeypress="return false"></asp:TextBox>
                                    <label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <input type="checkbox" id="chkbillingaddress" style="width: 5%" tabindex="20" checked="checked" onchange="BillingAddress(this)" />
                                    </div>
                                    <%--   <label><span>*</span>Billing Address Same</label>--%>
                                    <label>Billing Address Same</label>
                                    <textarea cols="" id="txtbill_address" name="BillAddress" style="width: 50%" rows="6" tabindex="21"></textarea>
                                    <label></label>
                                    <span id=""></span>
                                    <br />
                                    <%--  <label>Address Type<span>*</span></label>--%>
                                    <label>Address Type</label>
                                    <select id="selAddressType" name="AddressType">
                                        <option value="Select">Select</option>
                                        <option value="Primary Residence">Primary Residence</option>
                                        <option value="Business">Business</option>
                                        <option value="Vacation House">Vacation House</option>
                                        <option value="Rental">Rental</option>
                                        <option value="Condo">Condo</option>
                                        <option value="Apartment">Apartment</option>
                                        <option value="Mobile Home">Mobile Home</option>
                                        <option value="Other">Other</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<input type="button" id="Button6" runat="server" value="Add Address" style="width: 110px;" class="cls_btn_plus" tabindex="31"
                                    onclick="AddAddress(this)" />--%>

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnAddAddress" runat="server" Text="Add Address" Width="110px" CssClass="cls_btn_plus" TabIndex="31"
                                                OnClick="btnAddAddress_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <%--  <tr>
                            <td>
                                <label>
                                    Project Manager</label>
                                <asp:TextBox ID="txtProjectManager" Text=""  TabIndex="22" runat="server"></asp:TextBox>
                            </td>
                        </tr>--%>
                            <%--  <tr>
                            <td>
                                <label>
                                    Jr Project Manager</label>
                                <asp:TextBox ID="txtJrProjectManager" Text=""  TabIndex="22" runat="server"></asp:TextBox>
                            </td>
                        </tr>--%>
                            <%-- <tr>
                            <td>
                                <label id="F3Lbl">
                                    Follow Up</label>
                                <asp:TextBox ID="txtfollowup3" ClientIDMode="Static" onkeypress="return false;" CssClass="date"
                                    runat="server" TabIndex="3"></asp:TextBox>
                                <br />
                            </td>
                        </tr>--%>
                            <%-- <tr id="SpanReason">
                            <td>
                                <label>
                                    Reason of Closed:</label>
                                <asp:TextBox ID="TextBoxReason" runat="server" Rows="5" TextMode="MultiLine" TabIndex="28"></asp:TextBox>
                                <label>
                                </label>
                            </td>
                        </tr>--%>
                        </table>
                    </li>
                </ul>
                <asp:HiddenField ID="hdnBestTimeToContact" runat="server" ClientIDMode="Static" />
                <div class="btn_sec">

                    <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="addcust" TabIndex="31" OnClientClick="CheckForDuplication()" />--%>
                    <input type="button" id="btnSubmit" value="Submit" tabindex="31" onclick="CheckForDuplication()" />
                    <asp:HiddenField runat="server" ID="hdnStatus" ClientIDMode="Static" />
                    <asp:Button ID="btnHideSubmit" runat="server" ClientIDMode="Static" Text="Submit" ValidationGroup="addcust"
                        TabIndex="26" OnClick="btnSubmit_Click" />


                    <%-- <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" Text="Submit" ValidationGroup="addcust"
                    TabIndex="26" OnClick="btnSubmit_Click" />--%>
                    <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                        TabIndex="27" />
                    <%-- <input name="input2" type="submit" value="Submit" />
              <input type="submit" value="Cancel" />--%>
                </div>
                <table width="100%" cellpadding="0" cellspacing="0" align="center">
                    <tr>
                        <td style="width: 50%; display: none;" valign="top">
                            <table>
                                <%--<tr>
                                <td colspan="3" style="width: 50%">
                                    <asp:Label ID="lblDefault" runat="server" Font-Size="15px" Font-Bold="true">:</asp:Label>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td colspan="3" style="width: 50%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 42%">
                                        <asp:Label ID="lblSatartAddress" runat="server" Font-Size="15px" Font-Bold="true">Start:</asp:Label>
                                        <textarea cols="3" id="txtStartAddress" runat="server" style="width: 230px; font-size: 15px;"></textarea>
                                    </td>
                                    <td style="width: 42%">
                                        <asp:Label ID="lblEndAddress" runat="server" Font-Size="15px" Font-Bold="true">End:</asp:Label>
                                        <%-- <asp:DropDownList ID="ddlEndAddress" Width="150px" Height="25px" runat="server" onchange="ChangeAddress()">
                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                    </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtEndAddress" runat="server" Width="205px" Height="25px" onblur="return BindMap()"></asp:TextBox>

                                        <ajaxToolkit:AutoCompleteExtender ID="ddlCompany1" runat="server" TargetControlID="txtEndAddress" Enabled="True"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServicePath=""
                                            ServiceMethod="LoadAddress" DelimiterCharacters="" OnClientItemSelected="OnSelectAddress">
                                        </ajaxToolkit:AutoCompleteExtender>
                                        <%--<asp:TextBox ID="txtAdditionalEndAddress" runat="server" Style="margin-left: 30px; margin-top: 10px; height: 25px" onchange="ChangeTest();"></asp:TextBox>--%>
                                        <%--<asp:Button ID="Button9" runat="server" Text="Button"/> --%>
                                    </td>
                                    <%--<td style="width: 42%">
                                    <asp:LinkButton ID="lnkStreetView" runat="server" Font-Bold="true" Style="margin-right: 10px;" OnClientClick="return BindMap()" Text="StreetView?"></asp:LinkButton>
                                </td>--%>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%; display: none;" valign="top">
                            <table>
                                <%-- <tr>
                               
                                <td style="width: 42%">

                                    <div id="carousel_list"></div>
                                   
                                </td>
                              
                            </tr>--%>
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="lblSoldJobId" runat="server" Font-Size="15px" Font-Italic="true"></asp:Label>
                                    </td>
                                    <td style="width: 42%">
                                        <asp:Label ID="iblBeforeImg" runat="server" Font-Size="15px" Font-Bold="true" Font-Underline="true">Before</asp:Label>
                                    </td>
                                    <td style="width: 44%">
                                        <asp:Label ID="lblAfterImg" runat="server" Font-Size="15px" Font-Bold="true" Font-Underline="true">After</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlSoldJobId" Width="150px" Height="25px" runat="server" onchange="LocationImage(this)">
                                            <asp:ListItem Value="">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td>
                                   <asp:Image ID="imgBefore" Width="40%" Height="40px" runat="server" />
                                </td>--%>
                                    <td>
                                        <span></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                            <div id="container" class="shadow">
                                <%--
                                <asp:Panel ID="Panel5" runat="server">
                                </asp:Panel>
                                --%>

                                <div id="map_canvas">
                                    <table id="control" style="width: 100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Start: </td>
                                                        <td>
                                                            <input id="startvalue" type="text" style="width: 305px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>End: </td>
                                                        <td>
                                                            <input id="endvalue" type="text" style="width: 301px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <input id="Button5" type="button" value="GetDirections" onclick="return Button1_onclick()" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div id="map" style="height: 390px; width: 489px"></div>
                                            </td>
                                            <td valign="top">
                                                <div id="directionpanel" style="height: 390px; overflow: auto"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </td>
                        <td style="width: 50%; position: relative;" valign="top">
                            <!--<div style="width: 550px" id="ImageDisplay">
                                        <%--<ul class="pgwSlideshow">
                                            <li>
                                                <asp:Image ImageUrl="~/CustomerDocs/LocationPics/0000b936-d5cc-4430-a16c-c8c6f39d2c55-twitter.jpg"  runat="server" data-description="Slide1" />
                                             </li>
                                            <li>
                                                 <asp:Image ImageUrl="~/CustomerDocs/LocationPics/001f0aba-c251-4224-a986-67375b5acfe2-twitter.jpg" runat="server" alt="" data-description="Slide2" /></li>
                                            <li>
                                                <asp:Image ImageUrl="~/CustomerDocs/LocationPics/017bc8c3-6cff-4f2d-92fc-ade4af354595-JG-Logo-white (1).gif"  runat="server" alt="" data-description="Slide3" /></li>
                                            <li>
                                                  <asp:Image ImageUrl="~/CustomerDocs/LocationPics/0502083b-a649-401b-b4d2-af6fa666d403-facebook.jpg"  runat="server" alt="" data-description="Slide4" /></li>
                                            <li>
                                                 <asp:Image ImageUrl="~/CustomerDocs/LocationPics/05903a55-1ee2-46bc-8755-c187604231ae-twitter.jpg"  runat="server" alt="" data-description="Slide5" /></li>
                                            <li>
                                                 <asp:Image ImageUrl="~/CustomerDocs/LocationPics/0619e7db-845c-4ba7-9881-e927a28a2c81-JG-Logo-white (1).gif" runat="server" alt="" data-description="Slide6" /></li>
                                            <li>
                                                  <asp:Image ImageUrl="~/CustomerDocs/LocationPics/06fc05bf-aa19-452d-a10a-88c89d59d7f8-twitter.jpg"  runat="server" alt="" data-description="Slide7" /></li>
                                            <li>
                                                <asp:Image ImageUrl="~/img/slider_img/f.jpg" runat="server" alt="" data-description="Slide8" /></li>
                                        </ul>--%>
                                    </div> -->
                        </td>
                    </tr>

                </table>

            </div>
            <!-- Tabs endss -->
        </div>
   <%-- </asp:Panel>--%>
</asp:Content>
