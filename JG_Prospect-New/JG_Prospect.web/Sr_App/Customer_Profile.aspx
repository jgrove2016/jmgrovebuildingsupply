<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Customer_Profile.aspx.cs" Inherits="JG_Prospect.Sr_App.Customer_Profile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UCAddress.ascx" TagPrefix="uc1" TagName="UCAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="../css/pgwslideshow.min.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/jquery.webui-popover.min.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/js/jquery.ptTimeSelect.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #container {
            width: 95%;
            height: 600px;
            margin: 10px;
        }

        #map_canvas {
            float: left;
            width: 100%;
            height: 100%;
        }

        #sidebar {
            float: right;
            width: 247px;
            height: 100%;
            background: white;
            border: 0px solid #DDD;
            overflow: auto;
            overflow-x: hidden;
            z-index: 30;
            box-shadow: -1px 1px 3px -1px #000;
            display: none;
        }

        .shadow {
            /*-moz-box-shadow: 1px 1px 3px 1px #424345;
            -webkit-box-shadow: 1px 1px 3px 1px #424345;
            box-shadow: 1px 1px 3px 1px #424345;*/
        }

        .row {
            padding: 10px;
        }

        .separator {
            width: 96%;
            height: 1px;
            margin: 0 auto;
            border-bottom: 1px solid black;
        }

        .GridView1 {
            border-collapse: collapse;
        }

            .GridView1 > tbody > tr > td {
                border: 1px solid #CCCCCC;
                margin: 0;
                padding: 0px !important;
            }

        .grid td {
            padding: 0px !important;
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

        #btnHideUpdate {
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
            padding: 0px !important;
        }

        .size34 {
            width: 34%;
            float: left;
        }

            .size34 label {
                width: 100% !important;
            }

        .size66 {
            width: 66%;
            float: left;
        }

        .tblBestTimeToContact > tbody tr td {
            height: 0px !important;
            /*padding: 5px 0px!important;*/
            padding: 0px 0px !important;
        }

        .clsFixWidth > tbody tr td {
            word-break: break-all;
        }

        .webui-popover.bottom > .arrow {
            display: none;
        }

        .drop_popover {
            padding: 3px 5px;
            display: block;
            cursor: pointer;
            background: #FFF;
            float: left;
            margin: 5px;
            border: 1px solid #b5b4b4;
            text-decoration: none;
            color: #000;
            width: 200px;
            border-radius: 5px;
            position: relative;
        }

            .drop_popover:after {
                position: absolute;
                width: 0;
                height: 0;
                border-left: 5px solid transparent;
                border-right: 5px solid transparent;
                border-top: 5px solid #808080;
                right: 10px;
                top: 12px;
            }

            .drop_popover:after {
                content: "";
            }

        .webui-popover-content ul {
            padding: 0px;
            list-style-type: none;
        }

            .webui-popover-content ul li {
                float: left;
                width: 100%;
                padding-bottom: 10px;
            }

        .webui-popover.bottom {
            margin: 0px;
            height: 300px;
            overflow-y: scroll;
        }



        .pgwSlideshow .ps-list {
            border-top: 1px solid #555;
            box-shadow: 0 10px 10px -5px #333 inset;
            background: #555;
            overflow: hidden;
            position: absolute;
            top: -68px;
            width: 230px;
            height: 62px;
            right: 140px;
        }

            .pgwSlideshow .ps-list li img {
                width: 40px;
                height: 40px;
            }

            .pgwSlideshow .ps-list li {
                width: 60px;
            }

                .pgwSlideshow .ps-list li .ps-item {
                    padding: 5px -1px;
                    margin: 5px 8px;
                }

            .pgwSlideshow .ps-list .ps-prev, .pgwSlideshow .ps-list .ps-next {
                padding: 5px 10px 5px 5px;
                top: 15px;
            }

        .form_panel_custom ul li {
            border-right: none !important;
        }

        .ps-list > ul {
            margin: 0px 20px !important;
        }

        .pgwSlideshow .ps-current .ps-prev, .pgwSlideshow .ps-current .ps-next {
            display: none !important;
        }

        ps-prev ul li {
            background-color: gray;
        }

        .pgwSlideshow {
            background: #EFEEEE !important;
        }

            .pgwSlideshow .ps-list {
                border: 1px solid #912E31;
                box-shadow: none;
                background: none;
            }

                .pgwSlideshow .ps-list li .ps-item.ps-selected {
                    border: 4px solid #8F2F31;
                }
    </style>

    <script src="../Scripts/jquery.maskedinput.min.js" type="text/javascript"></script>
    <%--<script src="../Scripts/dropDownlistDiv.js" type="text/javascript"></script>--%>
    <script src="../Scripts/jquery.webui-popover.min.js" type="text/javascript"></script>
    <script src="../Scripts/pgwslideshow.min.js" type="text/javascript"></script>
    
    
    <script type="text/javascript">
        var PrimaryRadio = 0;
        var SecondaryRadio = 0;
        var map;
        var directionsDisplay;
        var directionsService;
        try {

        }
        catch(e){}
        

        // directionsService = new google.maps.DirectionsService();

        function GetCityStateOnBlur(e) {
            debugger;
            $.ajax({
                type: "POST",
                url: "Customer_Profile.aspx/GetCityState",
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

        function CheckDuplicateCustomerCred(obj, type) {
            $.ajax({
                type: "POST",
                url: "Customer_Profile.aspx/CheckDuplicateCustomerCredentials",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: "{'pValueForValidation':'" + obj.value + "', 'pValidationType':" + type + "}",

                success: function (data) {
                    debugger;
                    var dataInput = (data.d);
                    if (dataInput != '') {
                        alert(dataInput);
                        obj.value = '';
                    }
                }
            });
        }

        function CheckDuplicatePhone(obj) {
            CheckDuplicateCustomerCred(obj, 1);
        }

        function CheckDuplicateEmail(obj) {
            CheckDuplicateCustomerCred(obj, 2);
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
                url: "Customer_Profile.aspx/CheckForDuplication",
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
                        // else if (dataInput == "PhoneNumberEmpty") {
                        //alert("Please enter the Phone Number");
                        // return;
                        // }
                    else if (dataInput == "EmptyName") {
                        alert("Please enter the FirstName");
                        return;
                    }

                    else if (dataInput != "Contact is NOT Exists") {
                        if (confirm("Contacts are duplicated. Are you sure want to update the existing contact?")) {
                            $("#hdnStatus").attr('value', 1);
                            $("#btnHideUpdate").click();
                        }
                        else {
                            $("#hdnStatus").attr('value', 0);
                            $("#btnHideUpdate").click();
                        }
                    }
                    else {
                        $("#hdnStatus").attr('value', 1);
                        $("#btnHideUpdate").click();
                    }
                }
            });
        }



        function BindJobImage() {
            debugger;
            var a = "<ul class='pgwSlideshow' id='BindImageSlider'>";

            $.ajax({
                type: "POST",
                url: "Customer_Profile.aspx/BindImage",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var JobImage = JSON.parse(response.d);
                    var strBindImage = "";
                    $.each(JobImage, function (key, value) {
                        if (value.JobImage != "") {
                            if (value.JobImage.indexOf('/') == 2) {
                                if (value.JobImage.indexOf('.') == 0) {
                                    // var imgurl = value.JobImage.replace('..', '~');
                                    var ValidImage = value.JobImage;
                                    var Success = imageExists(ValidImage);
                                    if (Success == true) {
                                        strBindImage += "<li><img src='" + ValidImage + "' alt=''/></li>";
                                    }
                                    else {
                                        strBindImage += "<li><img src='../img/no_pic.png' alt=''/></li>";
                                    }
                                }
                            }
                            else if (value.JobImage.indexOf('/') == 1) {
                                if (value.JobImage.indexOf('~') == 0)
                                    //var imgurl = value.JobImage.replace('..', '~');
                                {
                                    var ValidImage = value.JobImage;
                                    var Success = imageExists(ValidImage);
                                    if (Success == true) {
                                        strBindImage += "<li><img src='" + ValidImage + "' alt=''/></li>";
                                    }
                                    else {
                                        strBindImage += "<li><img src='../img/no_pic.png' alt=''/></li>";
                                    }
                                }
                            }
                            else {
                                var ValidImage = "../CustomerDocs/LocationPics/" + value.JobImage;
                                //var ValidImage = "../CustomerDocs/LocationPics/0000b936-d5cc-4430-a16c-c8c6f39d2c55-twitter.jpg";
                                var Success = imageExists(ValidImage);
                                if (Success == true) {
                                    strBindImage += "<li><img src='../CustomerDocs/LocationPics/" + value.JobImage + "' alt=''/></li>";
                                }
                                else {
                                    strBindImage += "<li><img src='../img/no_pic.png' alt=''/></li>";
                                }
                            }
                        }
                    });
                    $('#ImageDisplay').empty();
                    $("#ImageDisplay").append(a);
                    $("#BindImageSlider").append(strBindImage);
                    $('#BindImageSlider').pgwSlideshow();
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        //Checking Exists Image in Solution Folder
        function imageExists(image_url) {

            var http = new XMLHttpRequest();

            http.open('HEAD', image_url, false);
            http.send();

            return http.status != 404;

        }

        function ChangeAddress() {
            debugger;
            var EndAddress = $("#ContentPlaceHolder1_ddlEndAddress option:selected").text();
            if (EndAddress != 'Select') {
                $("#ContentPlaceHolder1_txtAdditionalEndAddress").val(EndAddress);
            }
            else {
                $("#ContentPlaceHolder1_txtAdditionalEndAddress").val("");
            }

        }

        function ChangeTest() {
            debugger;
            $("#ContentPlaceHolder1_ddlEndAddress").val(-1);
        }

        function BindMap() {
            debugger;
            var Start = $("#ContentPlaceHolder1_txtStartAddress").val();
            var End = $('#ContentPlaceHolder1_txtEndAddress').val();
            if (End.length > 3) {
                calcRoute(Start, End);
            }
            else {
                alert("Please Enter Valid Address");
            }
            //var End = $("#ContentPlaceHolder1_ddlEndAddress option:selected").text();
            //var EndAdditional = $("#ContentPlaceHolder1_txtAdditionalEndAddress").val();
            //if (EndAdditional != "" && End == "Select") {
            //    calcRoute(Start, EndAdditional);
            //}
            //else {
            //    calcRoute(Start, End);
            //}
            $('#ContentPlaceHolder1_txtEndAddress').focus();
            return false;
            //calcRoute(start, end);
        }

        function calcRoute(start, end) {
            try {
                debugger;
                var request = {
                    origin: start,
                    destination: end,
                    travelMode: google.maps.TravelMode.DRIVING,
                    provideRouteAlternatives: false
                };
                directionsService.route(request, function (result, status) {
                    debugger;
                    if (status == google.maps.DirectionsStatus.OK) {
                        directionsDisplay.setDirections(result);
                        displayDirections(result);
                    }
                    else {
                        alert(status);
                    }
                });
            }
            catch (e) { }
        }

        function displayDirections(result) {
            debugger;
            var html = '<div style="margin:5px;padding:5px;background-color:#EBF2FC;border-left: 1px solid #EBEFF9;border-right: 1px solid #EBEFF9;text-align:right;">';
            html = html + '<span><strong>' + $.trim(result.routes[0].legs[0].distance.text.replace(/"/g, '')) + ', ' + $.trim(result.routes[0].legs[0].duration.text.replace(/"/g, '')) + '</strong></span></div>';
            $("#divDirections").html(html);

            //Display Steps
            var steps = result.routes[0].legs[0].steps;
            for (i = 0; i < steps.length; i++) {
                var instructions = JSON.stringify(steps[i].instructions);
                var distance = JSON.stringify(steps[i].distance.text);
                var time = JSON.stringify(steps[i].duration.text);
                $("#divDirections").append(getEmbedHTML(i + 1, instructions, distance, time));
            }
        }

        function getEmbedHTML(seqno, instructions, distance, duration) {
            debugger;
            var strhtml = '<div class="row">';
            strhtml = strhtml + '<span>' + seqno + '</span>&nbsp;' + $.trim(instructions.replace(/"/g, '')) + '<br/>'
            strhtml = strhtml + '<div style="text-align:right;"><span>' + $.trim(distance.replace(/"/g, '')) + ' <span></div>'
            strhtml = strhtml + '</div><div class="separator"></div>';

            return strhtml;
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

        function onclientselect(source, eventArgs) {
            debugger;
            var id = source._element.id;
            $.ajax({
                type: "POST",
                url: "Customer_Profile.aspx/GetCityState",
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

        function fnCheckOne(me) {
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
        function ConfirmDelete() {
            var Ok = confirm('All dependent record will be deleted permanently. Do you want to proceed?');
            if (Ok)
                return true;
            else
                return false;
        }

        function BindPrimaryContact() {
            debugger;
            $.ajax({
                type: "POST",
                url: "Customer_Profile.aspx/GetPrimaryContact",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;
                    var Data = response.d.split('^@');
                    var result = JSON.parse(Data[0]);
                    var PrimaryProduct = JSON.parse(Data[1]);
                    var SecondaryProduct = JSON.parse(Data[2]);
                    var ContactTime = JSON.parse(Data[3]);
                    var BillingAddressValue = JSON.parse(Data[4]);
                    var licount;
                    var childCount = "";
                    var chdCount = 0;
                    
                    $.each(result, function (key, value) {
                        debugger;
                        //Top Grid
                        licount = key + 1;

                        if (value.FName == "" && value.LName == "" && value.strContactType == "") {
                            chdCount++;
                            if (childCount == "") {
                                childCount = licount - 1;
                            }

                            if (value.PhoneType != "" || value.PhoneNumber != "") {
                                $("#tblPhone" + childCount + " tr:last").before("<tr><td class='paddingtd'></td>" +
                                  "<td><input type='text' clientidmode='Static' onblur='CheckDuplicatePhone(this);' id='txtPhone" + licount + chdCount + "' name='nametxtPhone" + licount + chdCount + "' data-type='" + licount + "' tabindex='7' class='clsMaskPhone'  placeholder='___-___-____' /></td>" +
                                  "<td><label class='clsFullWidth'>Phone Type</label></td><td>" +
                                  "<select class='clsFullWidth' id='selPhoneType" + licount + chdCount + "' name='nameselPhoneType" + licount + chdCount + "' data-type='" + licount + "' clientidmode='Static' tabindex='4'>" +
                                  "<option value='0'>Select</option><option value='CellPhone'>Cell Phone #</option><option value='HousePhone'>House Phone #</option><option value='WorkPhone'>Work Phone #</option><option value='AltPhone'>Alt. Phone #</option>" +
                                  "</select></td></tr>");
                                $("#txtPhone" + licount + chdCount).val(value.PhoneNumber);
                                $("#selPhoneType" + licount + chdCount).val(value.PhoneType);

                            }
                            if (value.EMail != "") {
                                $("#tblEmail" + childCount + " tr:last").before("<tr><td class='paddingtd'></td>" +
                                    "<td><input type='text' clientidmode='Static' onblur='CheckDuplicateEmail(this)' id='txtEMail" + licount + chdCount + "' name='nametxtEMail" + licount + chdCount + "' data-type='" + licount + "' tabindex='7'  placeholder='EMail' /></td></tr>");
                                $("#txtEMail" + licount + chdCount).val(value.EMail);
                            }
                        }
                        else {

                            $('input[id*="btnParent"]').css('visibility', 'hidden');

                            $("#divPrimaryContact ul li:last").after("<li style='width: 100%;'><div class='tblPrimaryContact' style='margin-top: 10px; width: 100%'><div style='width: 40%; float: left;'>" +
                            "<table id='tblDetails" + licount + "'><tr><td><input type='checkbox' name='chkContactType " + licount + "'/></td><td><select id='selContactType" + licount + "' name='selContactType" + licount + "' clientidmode='Static' tabindex='4' class='drop_down'><option value='0'>Select</option>" +
                            "<option value='DM'>DM</option><option value='Spouse'>Spouse</option><option value='Partner'>Partner</option><option value='Others'>Others</option></select><label></label>" +
                            "</td><td><input type='text' id='txtFName" + licount + "' tabindex='7' name='nametxtFName" + licount + "'  placeholder='First Name' data-type='" + licount + "' /></td><td>" +
                            "<input type='text' tabindex='7' id='txtLName" + licount + "' name='nametxtLName" + licount + "'  placeholder='Last Name' data-type='" + licount + "' /></td></tr><tr><td class='paddingtd'>" +
                            "<input type='button' id='btnParent" + licount + "' value='Add' data-type='" + licount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='AddTemplate(this)' /></td>" +
                            "</tr></table></div><div style='width: 40%; float: left;'><table id='tblPhone" + licount + "'><tr><td class='paddingtd'></td><td>" +
                            "<input type='text' clientidmode='Static' onblur='CheckDuplicatePhone(this);' id='txtPhone" + licount + "' name='nametxtPhone" + licount + "' data-type='" + licount + "' tabindex='7' class='clsMaskPhone'  placeholder='___-___-____' /></td>" +
                            "<td><label class='clsFullWidth'>Phone Type</label></td><td><select class='clsFullWidth' id='selPhoneType" + licount + "' name='nameselPhoneType" + licount + "' data-type='" + licount + "' clientidmode='Static' tabindex='4'>" +
                            "<option value='0'>Select</option><option value='CellPhone'>Cell Phone #</option><option value='HousePhone'>House Phone #</option><option value='WorkPhone'>Work Phone #</option><option value='AltPhone'>Alt. Phone #</option>" +
                            "</select></td></tr><tr><td class='paddingtd'><input type='button' value='Add' data-type='" + licount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='Phone(this)' /></td>" +
                            "</tr></table></div><div style='width: 20%; float: left;'><table id='tblEmail" + licount + "'><tr><td class='paddingtd'></td>" +
                            "<td><input type='text' clientidmode='Static' id='txtEMail" + licount + "' onblur='CheckDuplicateEmail(this)' name='nametxtEMail" + licount + "' data-type='" + licount + "' tabindex='7'  placeholder='EMail' /></td></tr><tr><td class='paddingtd'>" +
                            "<input type='button' value='Add' data-type='" + licount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='Email(this)' /></td></tr></table></div></div></li>");

                            $("#selContactType" + licount).val(value.strContactType);
                            $("#txtFName" + licount).val(value.FName);
                            $("#txtLName" + licount).val(value.LName);
                            $("#txtPhone" + licount).val(value.PhoneNumber);
                            $("#selPhoneType" + licount).val(value.PhoneType);
                            $("#txtEMail" + licount).val(value.EMail);
                            childCount = "";
                            chdCount = 0;
                        }
                        try {
                            $('.clsMaskPhone').mask("999-999-9999");
                        }catch(e1){}
                    });
                    //Primary Product
                    $.each(PrimaryProduct, function (key, value) {
                        debugger;
                        var CurrentDate = new Date;
                        var date = CurrentDate.getDate() + "-" + parseInt(CurrentDate.getMonth() + 1) + "-" + CurrentDate.getFullYear() + ": ";
                        if (value.strType == 'Est') {
                            $('#ContentPlaceHolder1_drpProductOfInterest1').after("<div style='float:right;width: 100%; text-align: right;'>" + value.ProductName + " - " + date +
                           "<input type='radio' id='Primary1" + PrimaryRadio + "' style='width:12px' name='PrimaryRadio" + PrimaryRadio + "' /> Service Or <input type='radio'id='Primary2" + PrimaryRadio + "' name='PrimaryRadio" + PrimaryRadio
                           + "' checked style='width:12px' value='Est'  />Est " +
                           "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnPrimaryId' name='hdnPrimaryId' value='" + value.ProductId + "'/><input type='hidden' id='hdnPrimaryType' name='hdnPrimaryType' value='Primary'/></div>");
                        }
                        else {
                            $('#ContentPlaceHolder1_drpProductOfInterest1').after("<div style='float:right;width: 100%; text-align: right;'>" + value.ProductName + " - " + date +
                           "<input type='radio' id='Primary1" + PrimaryRadio + "' style='width:12px' name='PrimaryRadio" + PrimaryRadio + "' checked value='Service' /> Service Or <input type='radio' id='Primary2" + PrimaryRadio + "' name='PrimaryRadio" + PrimaryRadio
                           + "' style='width:12px'/>Est " +
                           "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnPrimaryId' name='hdnPrimaryId' value='" + value.ProductId + "'/><input type='hidden' id='hdnPrimaryType' name='hdnPrimaryType' value='Primary'/></div>");
                        }
                        PrimaryRadio++;
                    });
                    //Secondary Product
                    $.each(SecondaryProduct, function (key, value) {
                        debugger;
                        var CurrentDate = new Date;
                        var date = CurrentDate.getDate() + "-" + parseInt(CurrentDate.getMonth() + 1) + "-" + CurrentDate.getFullYear() + ": ";
                        if (value.strType == 'Est') {
                            $('#ContentPlaceHolder1_drpProductOfInterest2').after("<div style='float:right;width: 100%; text-align: right;'>" + value.ProductName + " - " + date +
                            "<input type='radio' id='Secondary1" + SecondaryRadio + "'  style='width:12px' name='SecondaryRadio" + SecondaryRadio + "' value='Service'/> Service Or <input type='radio' id='Secondary2" + SecondaryRadio + "' name='SecondaryRadio" + SecondaryRadio
                            + "' checked style='width:12px' value='Est' />Est " +
                            "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnSecondaryId' name='hdnSecondaryId' value='" + value.ProductId + "'/><input type='hidden' id='hdnSecondaryType' name='hdnSecondaryType' value='Secondary'/></div>");
                        }
                        else {
                            $('#ContentPlaceHolder1_drpProductOfInterest2').after("<div style='float:right;width: 100%; text-align: right;'>" + value.ProductName + " - " + date +
                            "<input type='radio' style='width:12px' id='Secondary1" + SecondaryRadio + "' name='SecondaryRadio" + SecondaryRadio + "' checked value='Service' /> Service Or <input type='radio' id='Secondary2" + SecondaryRadio + "' name='SecondaryRadio" + SecondaryRadio
                            + "' style='width:12px' value='Est'  />Est " +
                            "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnSecondaryId' name='hdnSecondaryId' value='" + value.ProductId + "'/><input type='hidden' id='hdnSecondaryType' name='hdnSecondaryType' value='Secondary'/></div>");
                        }
                        SecondaryRadio++;
                    });
                    $.each(ContactTime, function (key, value) {
                        debugger;
                        if (value.BestTimeToContact != "") {
                            var strMultiSelect = "<tr><td colspan='4'>" + value.BestTimeToContact +
                                   " <button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeTime(this)'>X</button>" +
                                   "</td></tr>";
                            $(".tblBestTimeToContact>tbody").append(strMultiSelect);
                        }
                    });
                    //Bind Billing Address
                    var div = "";
                    $.each(BillingAddressValue, function (index, value) {
                        debugger;
                        if (index == 0) {
                            $("#txtbill_address1").val(value.strBillingAddress)
                            $("#selAddressType").val(value.strBillingAddressType)
                            $("#chkbillingaddress").attr("checked", true)
                            div = $("#chkbillingaddress")
                        }
                        else {
                            $(div).closest('tr').after("<tr><td><div>" +
                            "<input type='checkbox' id='chkbillingaddress" + index + "' style='width:5%' tabindex='20' checked='checked' clientidmode='Static' onchange='BillingAddress(this)' />" +
                            "</div><label><span>*</span>Billing Address" + (index) + " Same</label>" +
                            "<textarea id='txtbill_address" + (index + 1) + "' name='BillAddress" + (index + 1) + "' style='width:50%' clientidmode='Static' rows='6' tabindex='21'>" + value.strBillingAddress + "</textarea>" +
                            "<label></label><span id='spnAddress" + (index + 1) + "'></span></br></br><label> Address Type" + (index) + "<span>*</span></label>" +
                            "<select id='selAddressType" + (index + 1) + "' clientidmode='Static' name='AddressType" + (index + 1) + "'><option Value='Select'>Select</option>" +
                            "<option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option>" +
                            "<option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option>" +
                            "<option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
                            $("#selAddressType" + (index + 1)).val(value.strBillingAddressType);

                            if (index == 1) {
                                $("#chkbillingaddress").attr("checked", false);
                            }
                            else {
                                $("#chkbillingaddress" + (index - 1)).attr("checked", false);
                            }

                            div = $("#chkbillingaddress" + index);
                        }

                    });
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function AddTemplate(e) {
            debugger;

            var liCount = $("#divPrimaryContact ul li").length + 1;

            $(e).closest('li').after("<li style='width: 100%;'><div class='tblPrimaryContact' style='margin-top: 10px; width: 100%'><div style='width: 40%; float: left;'>" +
            "<table><tr><td><input type='checkbox' id='chkContactType" + liCount + "' name='chkContactType " + liCount + "'/></td><td><select id='selContactType" + liCount + "' name='selContactType" + liCount + "' clientidmode='Static' tabindex='4' class='drop_down'><option value='0'>Select</option>" +
            "<option value='DM'>DM</option><option value='Spouse'>Spouse</option><option value='Partner'>Partner</option><option value='Others'>Others</option></select><label></label>" +
            "</td><td><input type='text' id='txtFName" + liCount + "' tabindex='7' name='nametxtFName" + liCount + "'  placeholder='First Name' data-type='" + liCount + "' /></td><td>" +
            "<input type='text' tabindex='7' id='txtLName" + liCount + "' name='nametxtLName" + liCount + "'  placeholder='Last Name' data-type='" + liCount + "' /></td></tr><tr><td class='paddingtd'>" +
            "<input type='button' value='Add' data-type='" + liCount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='AddTemplate(this)' /></td>" +
            "</tr></table></div><div style='width: 40%; float: left;'><table><tr><td class='paddingtd'></td><td>" +
            "<input type='text' clientidmode='Static' id='txtPhone" + liCount + "' name='nametxtPhone" + liCount + "' data-type='" + liCount + "' tabindex='7' class='clsMaskPhone'  placeholder='___-___-____' onblur='CheckDuplicatePhone(this)' /></td>" +
            "<td><label class='clsFullWidth'>Phone Type</label></td><td><select class='clsFullWidth' id='selPhoneType" + liCount + "' name='nameselPhoneType" + liCount + "' data-type='" + liCount + "' clientidmode='Static' tabindex='4'>" +
            "<option value='0'>Select</option><option value='CellPhone'>Cell Phone #</option><option value='HousePhone'>House Phone #</option><option value='WorkPhone'>Work Phone #</option><option value='AltPhone'>Alt. Phone #</option>" +
            "</select></td></tr><tr><td class='paddingtd'><input type='button' value='Add' data-type='" + liCount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='Phone(this)' /></td>" +
            "</tr></table></div><div style='width: 20%; float: left;'><table><tr><td class='paddingtd'></td>" +
            "<td><input type='text' clientidmode='Static' id='txtEMail" + liCount + "' name='nametxtEMail" + liCount + "' data-type='" + liCount + "' tabindex='7'  placeholder='EMail' onblur='CheckDuplicateEmail(this)' /></td></tr><tr><td class='paddingtd'>" +
            "<input type='button' value='Add' data-type='" + liCount + "' class='clsFullWidth cls_btn_plus' tabindex='31' onclick='Email(this)' /></td></tr></table></div></div></li>");
            $('.clsMaskPhone').mask("999-999-9999");
            $(e).css("visibility", "hidden");
        }

        function Phone(e) {
            debugger;
            var dataTypeValue = $(e).attr("data-type");
            var subCount = $(e).closest('table').find('tr').length - 1;
            $(e).closest('tr').prev().after("<tr><td class='paddingtd'></td><td><input type='text' clientidmode='Static' id='txtPhone" + dataTypeValue + subCount + "' name='nametxtPhone" + dataTypeValue + subCount + "' tabindex='7' class='clsMaskPhone' placeholder='___-___-____' onblur='CheckDuplicatePhone(this)' /></td>" +
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
                                            "<input type='text' id='txtEMail" + dataTypeValue + subCount + "' tabindex='7' name='nametxtEMail" + dataTypeValue + subCount + "'  placeholder='EMail' clientidmode='Static'  onblur='CheckDuplicateEmail(this)' /></td></tr>");
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
            "<label>Address" + count + "<span>*</span></label><textarea ID='txtaddress" + count + "' TabIndex='9' value=''></textarea><label></label><span id='spnAddress" + count + "'></span></br></br><label> Address Type" + count + "<span>*</span></label><select id='selAddressType" + count + "' name='AddressType" + count + "'><option Value='Select'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>" +
            "<tr><td><label>Zip" + count + "<span>*</span></label><input type='text' value='' ID='txtzip" + count + "' onkeypress='return isNumericKey(event);'  tabindex='11' OnTextChanged='txtzip_TextChanged' />" +
            "</td></tr><tr><td><label>City" + count + "<span>*</span></label><input type='text' value='' ID='txtcity" + count + "' onkeypress='return isAlphaKey(event);'  tabindex='13' />" +
            "</td></tr><tr><td><label>State" + count + "<span>*</span></label><input type='text' value='' ID='txtstate" + count + "' onkeypress='return isAlphaKey(event);'  tabindex='15' /></td></tr>");
        }

        //function BillingAddress(e) {
        //    debugger;
        //    var lastChar = $(e).closest('tr').text().trim().split(" ")[1];
        //    var count = lastChar.slice(-1);
        //    if (count != "s") {
        //        var NewCount = parseInt(count, 10) + 1
        //        if ($("#chkbillingaddress" + count).is(':checked')) {
        //            $("#txtbill_address" + count).val($("#txtaddress" + count).val() + " " + $("#txtcity" + count).val() + " "
        //                + $("#txtstate" + count).val() + " " + $("#txtzip" + count).val());
        //            $(e).closest('tr').next().remove();
        //        }
        //        else {
        //            $("#txtbill_address" + count).val("");
        //            $(e).closest('tr').after("<tr><td><div>" +
        //            "<input type='checkbox' id='chkbillingaddress" + NewCount + "' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
        //            "</div><label><span>*</span>Billing Address" + NewCount + " Same</label><textarea id='txtbill_address" + NewCount + "' name='BillAddress" + NewCount + "' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress" + NewCount + "'></span></br></br><label> Address Type" + NewCount + "<span>*</span></label><select id='selAddressType" + NewCount + "' name='BillAddress" + NewCount + "'><option Value='0'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
        //        }
        //    }
        //    else {

        //        if ($("#chkbillingaddress").is(':checked')) {
        //            $("#txtbill_address").val($("#ContentPlaceHolder1_txtaddress").val() + " " + $("#ContentPlaceHolder1_txtcity").val() + " " + $("#ContentPlaceHolder1_txtstate").val() + " " +
        //            $("#ContentPlaceHolder1_txtzip").val());
        //            $(e).closest('tr').next().remove();
        //        }
        //        else {
        //            $("#txtbill_address").val("");
        //            $(e).closest('tr').after("<tr><td><div>" +
        //           "<input type='checkbox' id='chkbillingaddress1' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
        //           "</div><label><span>*</span>Billing Address1 Same</label><textarea id='txtbill_address1' name='BillAddress1' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress1'></span></br></br><label> Address1 Type <span>*</span></label><select id='selAddressType1' name='AddressType1'><option Value='0'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
        //        }
        //    }

        //}
        function BillingAddress(e) {
            debugger;
            //Start Get value from UserControl
            var UcAddress = $('#ContentPlaceHolder1_UCAddress_txtaddress').val();
            var UcCity = $('#ContentPlaceHolder1_UCAddress_txtcity').val();
            var UcState = $('#ContentPlaceHolder1_UCAddress_txtstate').val();
            var UcZipCode = $('#ContentPlaceHolder1_UCAddress_txtzip').val();
            var UcSelAddress = document.getElementById("ContentPlaceHolder1_UCAddress_DropDownList1");
            var UcAddressType = UcSelAddress.options[UcSelAddress.selectedIndex].value;
            //End Get value from UserControl
            var BillingAddress = $(e).closest('tr').find('textarea').val('');
            var AddressType = $(e).closest('tr').find('select').val('0')
            //var Address = document.getElementById("selAddressType");
            //var BillingAddress = $('#txtbill_address').text();
            //var AddressType = Address.options[Address.selectedIndex].value;
            var lastChar = $(e).closest('tr').text().trim().split(" ")[1];
            var count = lastChar.slice(-1);
            //$('#txtbill_address').text("");
            //$('#selAddressType').val("0");
            if (count != "s") {
                //$('#txtbill_address1').text("");
                //$('#selAddressType1').val("0");
                var NewCount = parseInt(count, 10) + 1
                if ($("#chkbillingaddress" + count).is(':checked')) {
                    var address = ($("#ContentPlaceHolder1_UCAddress_txtaddress" + count).val() == undefined) ? "" : $("#ContentPlaceHolder1_UCAddress1_txtaddress" + count).val();
                    var city = ($("#ContentPlaceHolder1_UCAddress_txtcity" + count).val() == undefined) ? "" : $("#ContentPlaceHolder1_UCAddress1_txtcity" + count).val();
                    var state = ($("#ContentPlaceHolder1_UCAddress_txtstate" + count).val() == undefined) ? "" : $("#ContentPlaceHolder1_UCAddress1_txtstate" + count).val();
                    var zip = ($("#ContentPlaceHolder1_UCAddress_txtzip" + count).val() == undefined) ? "" : $("#ContentPlaceHolder1_UCAddress1_txtzip" + count).val();

                    //$("#txtbill_address" + count).val(address + " " + city + " " + state + " " + zip);
                    $(e).closest('tr').find('textarea').val(UcAddress + " " + UcCity + " " + UcState + " " + UcZipCode)
                    //$(e).closest('tr').next().remove();
                    for (var i = 0; i < $(e).closest('tr').nextAll().length + 1; i++) {
                        if ($(e).closest('tr').next().children().children()[2] != undefined) {

                            $(e).closest('tr').next().remove();
                        }
                    }
                    // $(e).closest('tr').nextAll('tr:not(.bt)').remove();
                }
                else {
                    $("#txtbill_address" + count).val("");
                    $(e).closest('tr').after("<tr><td><div>" +
                    "<input type='checkbox' id='chkbillingaddress" + NewCount + "' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
                    "</div><label><span>*</span>Billing Address" + NewCount + " Same</label><textarea id='txtbill_address" + NewCount + "' name='BillAddress" + NewCount + "' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress" + NewCount + "'></span></br></br><label> Address" + NewCount + " Type<span>*</span></label><select id='selAddressType" + NewCount + "' name='AddressType" + NewCount + "'><option Value='Select'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
                    $("#txtbill_address" + NewCount + "").text(UcAddress + " " + UcCity + " " + UcState + " " + UcZipCode);
                    $("#selAddressType" + NewCount + "").val(UcAddressType);
                }
            }
            else {
                if ($("#chkbillingaddress").is(':checked')) {
                    //$("#txtbill_address").val($("#ContentPlaceHolder1_txtaddress").val() + " " + $("#ContentPlaceHolder1_txtcity").val() + " " + $("#ContentPlaceHolder1_txtstate").val() + " " +
                    //$("#ContentPlaceHolder1_txtzip").val());
                    $(e).closest('tr').find('textarea').val(UcAddress + " " + UcCity + " " + UcState + " " + UcZipCode)
                    $('#selAddressType').val(UcAddressType);
                    //$(e).closest('tr').next().remove();
                    for (var i = 0; i < $(e).closest('tr').nextAll().length + 1; i++) {
                        if ($(e).closest('tr').next().children().children()[2] != undefined) {

                            $(e).closest('tr').next().remove();
                        }
                    }

                }
                else {
                    //$("#txtbill_address").val("");
                    $(e).closest('tr').after("<tr><td><div>" +
                   "<input type='checkbox' id='chkbillingaddress1' style='width:5%' tabindex='20' checked='checked' onchange='BillingAddress(this)' />" +
                   "</div><label><span>*</span>Billing Address1 Same</label><textarea id='txtbill_address1' name='BillAddress1' style='width:50%' rows='6' tabindex='21'></textarea><label></label><span id='spnAddress1'></span></br></br><label> Address1 Type <span>*</span></label><select id='selAddressType1' name='AddressType1'><option Value='Select'>Select</option><option value='Primary Residence'>Primary Residence</option><option value='Business'>Business</option><option value='Vacation House'>Vacation House</option><option value='Rental'>Rental</option><option value='Condo'>Condo</option><option value='Apartment'>Apartment</option><option value='Mobile Home'>Mobile Home</option><option value='Other'>Other</option></select></td></tr>");
                    $('#txtbill_address1').text(UcAddress + " " + UcCity + " " + UcState + " " + UcZipCode);
                    $('#selAddressType1').val(UcAddressType);
                }
            }

        }


        function PrimaryProduct(e) {
            debugger;
            //Awning - 01-12-2015: O Service Or O Est X
            //  var lastChar = $(e).closest('tr').text().trim().split(" ")[1];
            var CurrentDate = new Date;
            var date = CurrentDate.getDate() + "-" + parseInt(CurrentDate.getMonth() + 1) + "-" + CurrentDate.getFullYear() + ": ";
            $(e).after("<div style='float:right;width: 100%;text-align: right;'>" + e.options[e.selectedIndex].innerHTML + " - " + date +
                "<input type='radio' style='width:12px' id='Primary1" + PrimaryRadio + "' name='PrimaryRadio" + PrimaryRadio + "' value='Service'/> Service Or <input type='radio' id='Primary2" + PrimaryRadio + "' name='PrimaryRadio" + PrimaryRadio
                + "' style='width:12px' value='Est' />Est " +
                "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnPrimaryId' name='hdnPrimaryId' value='" + $(e).val() + "'/><input type='hidden' id='hdnPrimaryType' name='hdnPrimaryType' value='Primary'/><input type='hidden' id='hdnPrimary' name='hdnPrimary' value='" + e.options[e.selectedIndex].innerHTML + " - " + date + "_" + PrimaryRadio + "Primary" + "'/></div>");
            PrimaryRadio++;
        }

        function SecondaryProduct(e) {
            debugger;
            var CurrentDate = new Date;
            var date = CurrentDate.getDate() + "-" + parseInt(CurrentDate.getMonth() + 1) + "-" + CurrentDate.getFullYear() + ": ";
            $(e).after("<div style='float:right;width: 100%;text-align: right;'>" + e.options[e.selectedIndex].innerHTML + " - " + date +
                "<input type='radio' name='SecondaryRadio" + SecondaryRadio + "' id='Secondary1" + SecondaryRadio + "' style='width:12px'  value='Service' /> Service Or <input type='radio' id='Secondary2" + SecondaryRadio + "' name='SecondaryRadio" + SecondaryRadio
                + "' style='width:12px' value='Est' /> Est " +
                "<button style='color:white;background-color:#9B3435;width:11px;cursor: pointer;' onclick='removeProduct(this)'>X</button><input type='hidden' id='hdnSecondaryId' name='hdnSecondaryId' value='" + $(e).val() + "'/><input type='hidden' id='hdnSecondaryType' name='hdnSecondaryType' value='Secondary'/></div>");
            SecondaryRadio++;
        }

        function removeProduct(e) {
            e.closest('div').remove()
        }

        function removeTime(e) {
            e.closest('tr').remove();
        }

        function LocationImage(e) {
            debugger;
            var SelectedValue = e.options[e.selectedIndex].innerHTML;
            var a = "<ul class='pgwSlideshow' id='BindImageSlider'>";
            if (SelectedValue != 'Default All') {
                $.ajax({
                    type: "POST",
                    url: "Customer_Profile.aspx/GetLocationImage",
                    data: "{'strJobSoldId':'" + SelectedValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    success: function (response) {
                        debugger;
                        //var Imageurl = data.d.replace('~', '..');
                        //$('#ContentPlaceHolder1_imgBefore').attr("src", Imageurl);
                        //Bind JobImage
                        var JobImage = response.d;
                        // $.each(JobImage, function (key, value) {
                        var strBindImage;
                        $('#ImageDisplay').empty();

                        //$("#BindImageSlider").html("");

                        // JobImage = '../CustomerDocs/LocationPics/08299af7-9d01-4754-ba25-88f024865aa6-facebook.jpg';
                        if (JobImage != "") {
                            if (JobImage.indexOf('/') == 2) {
                                if (JobImage.indexOf('.') == 0) {
                                    //var imgurl = JobImage.replace('..', '~');
                                    var ValidImage = JobImage;
                                    
                                    strBindImage = "<li><img onerror='javascript:this.src=\"../img/no_pic.png\"' src='" + ValidImage + "' alt=''/></li>";

                                    /*var Success = imageExists(ValidImage); #-Commented by Shabbir: This is extremely stupid code
                                    if (Success == true) { 
                                        strBindImage = "<li><img src='" + ValidImage + "' alt=''/></li>";
                                    }
                                    else {
                                        strBindImage = "<li><img src='../img/no_pic.png' alt=''/></li>";
                                    }*/
                                }
                            }
                            else if (JobImage.indexOf('/') == 1) {
                                if (JobImage.indexOf('~') == 0) {
                                    //var imgurl =JobImage.replace('..', '~');
                                    var ValidImage = JobImage;
                                    strBindImage = "<li><img src='" + ValidImage + "' alt=''/></li>";

                                    /*var ValidImage = JobImage;
                                    var Success = imageExists(ValidImage);
                                    if (Success == true) {
                                        strBindImage = "<li><img src='" + ValidImage + "' alt=''/></li>";
                                    }
                                    else {
                                        strBindImage = "<li><img src='../img/no_pic.png' alt=''/></li>";
                                    }*/
                                }
                            }
                            else {
                                var ValidImage = "../CustomerDocs/LocationPics/" + JobImage;
                                strBindImage = "<li><img src='../CustomerDocs/LocationPics/" + JobImage + "' alt=''/></li>";

                                /*var ValidImage = "../CustomerDocs/LocationPics/0000b936-d5cc-4430-a16c-c8c6f39d2c55-twitter.jpg";
                                var Success = imageExists(ValidImage);
                                if (Success == true) {
                                    strBindImage = "<li><img src='../CustomerDocs/LocationPics/" + JobImage + "' alt=''/></li>";
                                }
                                else {
                                    strBindImage = "<li><img src='../img/no_pic.png' alt=''/></li>";
                                }*/

                            }
                            $("#ImageDisplay").append(a);
                            $("#BindImageSlider").append(strBindImage);
                            $('#BindImageSlider').pgwSlideshow();
                            //$("#ImageDisplay").html(a + strBindImage + b);
                        }
                        else {
                            strBindImage = "<li><img src='../img/no_pic.png' alt=''/></li>";
                            $("#ImageDisplay").append(a);
                            $("#BindImageSlider").append(strBindImage);
                            $('#BindImageSlider').pgwSlideshow();
                        }
                        //$('#BindImageSlider').pgwSlideshow();
                        //});
                    }
                });
            }
            else {
                BindJobImage(); onblur
            }
        }


        function OnSelectAddress(sender, e) {

            var selectedAddress = eval("(" + e._text + ")");
            alert(selectedAddress);
        }

        window.onload = function () {
            //$(document).ready(function () {
            //  debugger;
            
            BindPrimaryContact();

            
            
            $('code').each(
					function () {
					    eval($(this).html());
					}
				)

            $('.clsMaskPhone').mask("999-999-9999");
            //$('#txtBestTimetoContact').ptDaySelect({});
            $('#txtBestDayToContact').ptDayOnlySelect({});
            $('#txtBestStartTime').ptTimeOnlySelect({});
            $('#txtBestEndTime').ptTimeOnlySelect({});
            //var txtfollowup2 = $('#ContentPlaceHolder1_txtfollowup2').val();
            //if (txtfollowup2 != "") {
            //    $('#followup2span').show();
            //}
            //else {
            //    $('#F3Lbl').text('Follow Up 2');
            //    $('#followup2span').hide();
            //}
            //if ($('#txtfollowup1').val() != "") {
            //    $('#spanS1').show();
            //    $('#txtfollowup1').attr("disabled", true);
            //    //  $('#txtfollowup1').attr('disabled', 'disabled');
            //}
            //else {
            //    $('#spanS1').hide();
            //}
            // $('#SpanReason').hide();
            //debugger;

            //var latlng = new google.maps.LatLng(40.748492, -73.985496);
            //var myOptions = {
            //    zoom: 1,
            //    center: latlng,
            //    mapTypeId: google.maps.MapTypeId.ROADMAP
            //};
            //map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

            //directionsDisplay = new google.maps.DirectionsRenderer();
            //directionsDisplay.setMap(map);
            //BindJobImage();

            //$('.ps-list').appendTo('#carousel_list');

            //for Dispaly image using slider
            //directionsDisplay.setPanel(document.getElementById("divDirections"));
        }//);
        $(document).change(function () {
            if ($('#ddlfollowup3').val() == "Closed") {
                // $('#SpanReason').show();
            }
            else if ($('#ddlfollowup3').val() != "Closed") {
                //   $('#SpanReason').hide();
            }
        });


    </script>

   <%-- <script src="http://maps.googleapis.com/maps/api/js"></script>--%>


     <%--<script>
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
<%-- <script>
        function initialize() {
            var mapProp = {
                center: new google.maps.LatLng(40.748492, -73.985496),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), mapProp);
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>--%>
    <%---------end script for Datetime Picker----------%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <!-- Tabs starts -->
        <h1>Customer Profile</h1>
        <div class="form_panel_custom">
            <table width="100%">
                <tr>
                    <td style="width: 21%">
                        <span>
                            <label>
                                Customer Id:
                            </label>
                            <b>
                                <asp:Label ID="lblmsg" runat="server" Visible="true"></asp:Label></b>
                        </span>
                    </td>
                    <td style="width: 27%">
                        <span>

                            <label>
                                Last Status:
                            </label>
                            <b>
                                <asp:Label ID="lblLastStatus" runat="server" Visible="true"></asp:Label></b>
                        </span>
                    </td>
                    <td>
                        <span>
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <label>
                                        Lead Source<span>*</span></label>
                                    <asp:DropDownList ID="ddlleadtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlleadtype_SelectedIndexChanged"
                                        TabIndex="24" Enabled="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem>Website</asp:ListItem>
                                        <asp:ListItem>Referal Family/Friend</asp:ListItem>
                                        <asp:ListItem>Self Generated</asp:ListItem>
                                        <asp:ListItem>Canvasser</asp:ListItem>
                                        <asp:ListItem>TV</asp:ListItem>
                                        <asp:ListItem>Newspaper</asp:ListItem>
                                        <asp:ListItem>Radio</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <span id="spanother" runat="server" visible="false">
                                        <label>
                                            Enter Other Choice</label>
                                        <asp:TextBox ID="txtleadtype" runat="server" TabIndex="25"></asp:TextBox></span>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Lead Type."
                                        ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlleadtype"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>
                            <label>
                                *Check Primary Contact 
                            </label>
                        </span>
                    </td>
                </tr>
            </table>
            <div style="width: 100%" id="divPrimaryContact">
                <ul>
                    <li></li>
                </ul>
            </div>

            <div class="grid_h">
                <strong>Touch Point Log</strong>
            </div>
            <div class="grid">
                <table cellspacing="0" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <th style="width: 40px;">Ref #:</th>
                        <th style="width: 125px;">User Id</th>
                        <th style="width: 90px;">Quote/Sold job ID</th>
                        <th style="width: 135px;">Date & Time</th>
                        <th>Note / Status</th>
                    </tr>
                </table>
                <div class="clsOverFlow">

                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <%--<asp:Panel ID="pnlAddress" runat="server"></asp:Panel>--%>
                            <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>
                            <asp:GridView ID="grdTouchPointLog" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="GridView1 clsFixWidth"
                                ShowHeader="false" OnRowDataBound="grdTouchPointLog_RowDataBound">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="50px" HeaderText="Ref #:" DataField="ReferenceNumber" />
                                    <asp:BoundField ItemStyle-Width="135px" HeaderText="User Id" DataField="Email" />
                                    <asp:BoundField ItemStyle-Width="100px" HeaderText="Quote/Sold job ID" DataField="SoldJobId" />
                                    <asp:BoundField ItemStyle-Width="145px" HeaderText="Date & Time" DataField="Date" />
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
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAddNotes" runat="server" TextMode="MultiLine" Height="33px" Width="407px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddNotes" />
                                </Triggers>

                            </asp:UpdatePanel>

                        </div>
                        <%--  <asp:TextBox ID="txtAddNotes" runat="server" TextMode="MultiLine" Height="33px" Width="407px"></asp:TextBox>--%>
                    </td>
                    <td>
                        <span id="spanS3">
                            <%--<label>Status</label></span>--%>
                            <%--<asp:DropDownList ID="ddlfollowup3" AutoPostBack="false" ClientIDMode="Static" runat="server" TabIndex="4">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
            </table>
            <br />
            <div class="grid">
                <table cellspacing="0" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
 <asp:GridView ID="GridViewSoldJobs" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        ShowHeaderWhenEmpty="True" CssClass="GridView1" RowStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        RowStyle-VerticalAlign="Top" AllowPaging="true" PageSize="5" OnPageIndexChanging="GridViewSoldJobs_PageIndexChanging"
                                        OnRowDataBound="GridViewSoldJobs_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Quote or Sold Job#" HeaderStyle-Width="15%" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnproductid" runat="server" Value='<%#Eval("JobSeqID") %>' />
                                                    <asp:HiddenField ID="hdnProductTypeId" runat="server" Value='<%#Eval("ProductTypeId") %>' />
                                                    <asp:LinkButton CommandArgument='<%#Eval("CustomerID") %>' ID="lnkSoldJobDetails" OnClick="lnkSoldJobDetails_Click"  runat="server"  Text='<%# Eval("SoldJobId").ToString().Substring( Eval("SoldJobId").ToString().IndexOf("-")+1)  %>' ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField ItemStyle-Width="50px" HeaderText="Quote or Sold Job#" DataField="SoldJobId" />--%>
                                            
                                             <asp:TemplateField HeaderText="Date Quoted or Sold"  ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblSoldJobDate" Text='<%# Convert.ToDateTime( Eval("DateSold")).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-Width="17%" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlfollowup3" Width="175px" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-Width="22%" HeaderText="Team Members">
                                                <ItemTemplate>
                                                    <div id="ddlDropDown" runat="server">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Team Members">
                                                <ItemTemplate>
                                                    <div class="wrapper-dropdown-2 dd">
                                                        Team Members
                                                        <ul class="dropdown" id="ddlDropDown" runat="server">
                                                            <%--<li><span class="clsCustomerIdLink"><a href="#">Twitter</a></span><span>- Twitter Site</span></li>
                                                            <li><span class="clsCustomerIdLink"><a href="#">Github</a></span><span>- Github Site</span></li>
                                                            <li><span class="clsCustomerIdLink"><a href="#">Facebook</a></span><span>- Facebook Site</span><div></li>
                                                        </ul>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%-- <asp:BoundField ItemStyle-Width="50px" HeaderText="Date Closed(reason)" />--%>
                                            <%--       <asp:TemplateField ItemStyle-Width="50px" HeaderText="Attachment">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HiddenFieldEstimate" Value='<%#Eval("Id")%>' runat="server" />
                                                    <asp:HiddenField ID="HidProductTypeId" Value='<%#Eval("ProductTypeId")%>' runat="server" />
                                                    <asp:HiddenField ID="HidCustomerId" Value='<%#Eval("CustomerId")%>' runat="server" />
                                                    <asp:LinkButton ID="lnkestimateid" runat="server" Text='<%#Eval("Attachment")%>'
                                                        OnClick="lnkestimateid_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Job & Invoice Packet" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkwrkzip" runat="server" Text='Job & Invoice Packet' OnClick="lnkwrkzip_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Customer Service score" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusId" runat="server" Text='<%#Eval("StatusId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--   <as  p:BoundField ItemStyle-Width="50px" HeaderText="Customer Service score" DataField="CustomerServiceScore" />--%>
                                        </Columns>
                                        
                                        <PagerStyle HorizontalAlign="Right" />
                                    </asp:GridView>



<%--                                    <asp:GridView ID="GridViewSoldJobs" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        ShowHeaderWhenEmpty="True" CssClass="GridView1" RowStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        RowStyle-VerticalAlign="Top" AllowPaging="true" PageSize="5" OnPageIndexChanging="GridViewSoldJobs_PageIndexChanging"
                                        OnRowDataBound="GridViewSoldJobs_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Quote or Sold Job#" HeaderStyle-Width="15%" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSoldJobDetails" OnClick="lnkSoldJobDetails_Click" runat="server" Text='<%#Eval("SoldJobId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField ItemStyle-Width="50px" HeaderText="Quote or Sold Job#" DataField="SoldJobId" />--% >
                                            <asp:BoundField ItemStyle-Width="50px" HeaderText="Date Quoted or Sold" DataField="DateSold" DataFormatString="{0:d}" />


                                            <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-Width="17%" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlfollowup3" Width="175px" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-Width="22%" HeaderText="Team Members">
                                                <ItemTemplate>
                                                    <div id="ddlDropDown" runat="server">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            < %--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Team Members">
                                                <ItemTemplate>
                                                    <div class="wrapper-dropdown-2 dd">
                                                        Team Members
                                                        <ul class="dropdown" id="ddlDropDown" runat="server">
                                                            <%--<li><span class="clsCustomerIdLink"><a href="#">Twitter</a></span><span>- Twitter Site</span></li>
                                                            <li><span class="clsCustomerIdLink"><a href="#">Github</a></span><span>- Github Site</span></li>
                                                            <li><span class="clsCustomerIdLink"><a href="#">Facebook</a></span><span>- Facebook Site</span><div></li>
                                                        </ul>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--% >
                                            <%-- <asp:BoundField ItemStyle-Width="50px" HeaderText="Date Closed(reason)" />--% >
                                            <%--       <asp:TemplateField ItemStyle-Width="50px" HeaderText="Attachment">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HiddenFieldEstimate" Value='<%#Eval("Id")%>' runat="server" />
                                                    <asp:HiddenField ID="HidProductTypeId" Value='<%#Eval("ProductTypeId")%>' runat="server" />
                                                    <asp:HiddenField ID="HidCustomerId" Value='<%#Eval("CustomerId")%>' runat="server" />
                                                    <asp:LinkButton ID="lnkestimateid" runat="server" Text='<%#Eval("Attachment")%>'
                                                        OnClick="lnkestimateid_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--% >
                                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Job & Invoice Packet" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkwrkzip" runat="server" Text='Job & Invoice Packet' OnClick="lnkwrkzip_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Customer Service score" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusId" runat="server" Text='<%#Eval("StatusId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <% --   <as  p:BoundField ItemStyle-Width="50px" HeaderText="Customer Service score" DataField="CustomerServiceScore" />--% >
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" />
                                    </asp:GridView>--%>
                                    <asp:Button runat="server" ID="btnModalPopUp" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnModalPopUp" PopupControlID="pnlpopup"
                                        BackgroundCssClass="modalBackground" CancelControlID="btnCancel1" Enabled="true">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="800px" Style="display: none"
                                        ScrollBars="Vertical">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblresult" runat="server"></asp:Label>
                                                    <%--<asp:Button ID="Button5" CommandName="Submit" runat="server" Text="Submit" OnClientClick="return hidePnl();" />--%>
                                                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="grid_h">
                <strong>Customer Quality</strong>
            </div>
            <ul>
                <li style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <%--<tr>
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
                                <label style="line-height:21px;">
                                    Contact Preference</label>
                                <asp:CheckBox ID="chbemail" runat="server" Width="14%" Text="Email " TabIndex="17"
                                    onclick="fnCheckOne(this)" />
                                <asp:CheckBox ID="chbcall" runat="server" Text="Call" Width="14%"
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
                                <asp:TextBox ID="txtestimate_date" CssClass="date" TabIndex="5" runat="server" ></asp:TextBox>
                                <asp:CheckBox ID="chkAutoEmailer" Text="Send Auto Email" Checked="true" runat="server" />
                               
                            </td>
                        </tr>
                        <uc1:UCAddress runat="server" ID="UCAddress" />
                        <%--<tr>
                            <td>
                                <uc1:UCAddress runat="server" id="UCAddress1" />
                            </td>
                        </tr>--%>
                        <asp:UpdatePanel ID="panel4" runat="server">
                            <ContentTemplate>
                                <%--<asp:Panel ID="pnlAddress" runat="server"></asp:Panel>--%>
                                <asp:PlaceHolder runat="server" ID="myPlaceHolder"></asp:PlaceHolder>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddAddress" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                                    Secondary Product of Interest (6 months)</label>
                                <asp:DropDownList ID="drpProductOfInterest2" runat="server" onchange="SecondaryProduct(this)" TabIndex="30">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Competitor Bids<span></span></label>
                                <asp:TextBox ID="txtCompetitorBids" runat="server" TabIndex="6"></asp:TextBox>
                                <label>
                                </label>
                            </td>
                        </tr>
                         <tr>
                            <td>
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
                                <div class="size34">
                                    <label>
                                        Best Time To Contact</label>
                                </div>
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
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Best Time To Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlbesttime"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Best Time To Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="txtBestTimetoContact"></asp:RequiredFieldValidator>--%>
                                <div class="size66">
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
                                <div>
                                    <input type="checkbox" id="chkbillingaddress" style="width: 5%" tabindex="20" checked="checked" onchange="BillingAddress(this)" />
                                </div>
                                <%--<label><span>*</span>Billing Address Same</label>--%>
                                <label>Billing Address Same</label>
                                <textarea id="txtbill_address" runat="server" clientidmode="Static" name="BillAddress" style="width: 50%" rows="6" tabindex="21"></textarea>
                                <label></label>
                                <span id=""></span>
                                <br />
                                <%--   <label>Address Type<span>*</span></label>--%>
                                <label>Address Type</label>
                                <select id="selAddressType" name="AddressType" runat="server" clientidmode="Static">
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
                                <%--<input type="button" id="btnAddAddress" runat="server" value="Add Address" style="width: 110px;" class="cls_btn_plus" tabindex="31" 
                                    onserverclick="btnAddAddress_ServerClick" />--%>


                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAddAddress" runat="server" Text="Add Address" Width="110px" CssClass="cls_btn_plus" TabIndex="31"
                                            OnClick="btnAddAddress_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <%--  <td>
                                <label>
                                    Project Manager</label>
                                <asp:TextBox ID="txtProjectManager" Text="" TabIndex="22" runat="server"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <%--   <tr>
                            <td>
                                <label>
                                    Jr Project Manager</label>
                                <asp:TextBox ID="txtJrProjectManager" Text="" TabIndex="22" runat="server"></asp:TextBox>
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
                        <%--   <tr id="spanreason">
                            <td>
                                <label>
                                    reason of closed:</label>
                                <asp:textbox id="textboxreason" runat="server" rows="5" textmode="multiline" tabindex="28"></asp:textbox>
                                <label>
                                </label>
                            </td>
                        </tr>--%>
                    </table>
                </li>
            </ul>
            <div class="btn_sec" style="padding-left: 30%; position: relative;">
                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>--%>
                <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="addcust" TabIndex="31" OnClientClick="CheckForDuplication()" />--%>
                <input type="button" id="btnUpdate" value="Update" tabindex="31" onclick="CheckForDuplication()" />
                <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="CheckForDuplication()" />--%>
                <asp:HiddenField runat="server" ID="hdnStatus" ClientIDMode="Static" />
                <asp:Button ID="btnHideUpdate" ClientIDMode="Static" runat="server" Text="Update" ValidationGroup="addcust" TabIndex="31" OnClick="btnUpdate_Click" />
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>


                <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                    TabIndex="32" />
                <asp:Button ID="btndelete" runat="server" Text="Delete" TabIndex="33" OnClick="btndelete_Click"
                    OnClientClick="return ConfirmDelete()" />
                <asp:HiddenField ID="Hidden3rdFollowupId" runat="server" />
                <asp:HiddenField ID="HiddenFieldassignid" runat="server" />
                <asp:HiddenField ID="Hiddenfieldstatus" runat="server" />
                <asp:HiddenField ID="hdnBestTimeToContact" runat="server" ClientIDMode="Static" />
            </div>
            <br />
            <table width="100%" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td style="width: 50%;display:" valign="top">
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
                                </td>
                                <%--<td style="width: 42%">
                                    <asp:LinkButton ID="lnkStreetView" runat="server" Font-Bold="true" Style="margin-right: 10px;" OnClientClick="return BindMap()" Text="StreetView?"></asp:LinkButton>
                                </td>--%>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%;display:;" valign="top">
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
                    <td style="width: 50%" valign="top">
                        <div id="container" class="shadow">
                            <div id="map_canvas">
                            <table id ="control" style="width:100%">
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
                            <td><input id="endvalue" type="text" style="width: 301px" /></td>
                            </tr>
                            <tr>
                            <td align ="right">
                                <input id="Button5" type="button" value="GetDirections" onclick="return Button1_onclick()"/></td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            <tr>
                            <td valign ="top">
                            <div id ="map" style="height: 390px; width: 489px"></div>
                            </td>
                            <td valign ="top">
                            <div id ="directionpanel"  style="height: 390px;overflow: auto" ></div>
                            </td>
                            </tr>
                            </table>
                             </div>
                        </div>
                    </td>
                    <td style="width: 50%;" valign="top">
                        <div style="width: 100%" id="ImageDisplay">
                        </div>
                    </td>
                </tr>

            </table>

            <%--<div id="container" class="shadow">
                <div id="map_canvas"></div>--%>
            <%-- <div id="sidebar">
                    <div class="row" style="background:#E3EDFA">
                        <label> Enter Address in/around Newyork</label>
                        <input type="text" id="txtAddress1" class="text" value="350 5th Ave, New York, NY" />
                        <input type="text" id="txtAddress2" class="text" value="1 Brewster Road, Newark, NJ" />
                        <img src="images/search.png" id="btnGetDirections" border="0" width="24" height="24" style="vertical-align:middle;cursor:pointer;"  />
                    </div>
                    <div class="separator"></div>
                    <div id="divDirections"></div>
                </div>--%>
            <%-- </div>--%>
        </div>
        <!-- Tabs endss -->
    </div>

    <link href="../datetime/jq/ui-lightness/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" />
    <link href="../datetime/jq/jquery.ui.timepicker.css" rel="stylesheet" />

    <script src="../datetime/jq/jquery-1.9.0.min.js"></script>
    <script src="../datetime/jq/jquery.ui.core.min.js"></script>
    <script src="../datetime/jq/jquery.ui.position.min.js"></script>
    <script src="../datetime/jq/jquery.ui.tabs.min.js"></script>
    <script src="../datetime/jq/jquery.ui.widget.min.js"></script>
    
    <script src="../datetime/jq/jquery.ui.timepicker.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <asp:HiddenField runat="server" ID="hfWasConfirmed" />
    <asp:Panel ID="Panel1" runat="server">
        <script>
            function getConfirmationValue() {
                if (confirm('Contacts are duplicated. Are you sure want to update the existing record?')) {
                    $('#<%=hfWasConfirmed.ClientID%>').val('true')
                }
                else {
                    $('#<%=hfWasConfirmed.ClientID%>').val('false')
                }
                return true;
            }



            $('.popover').webuiPopover({
                constrains: 'horizontal',
                //trigger: 'click',
                multi: false,
                placement: 'bottom',
                width: 200
            });
        </script>

      
        <script language="javascript" type="text/javascript">
            var directionsDisplay;
            var directionsService ;
            try {
                directionsService = new google.maps.DirectionsService();
            }catch(e){}

            function InitializeMap() {
                try {
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
                    //control.style.display = 'block';
                }catch(e){}
            }
            function calcRoute() {
                try {
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
                }catch(e){}

            }
            function Button1_onclick() {
                calcRoute();
            }

           /* $(document).ready(function () {
                InitializeMap();
            });*/
            function jsFunctions() {
                $('.popover').webuiPopover({
                    constrains: 'horizontal',
                    //trigger: 'click',
                    multi: false,
                    placement: 'bottom',
                    width: 200
                });
            }
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
            //$('.time').ptTimeSelect();
            $('.time').timepicker();
            $(".date").datepicker();
</script>

    </asp:Panel>

</asp:Content>
