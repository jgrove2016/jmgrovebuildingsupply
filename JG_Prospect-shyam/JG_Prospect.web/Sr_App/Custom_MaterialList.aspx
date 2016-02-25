<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Custom_MaterialList.aspx.cs" Inherits="JG_Prospect.Sr_App.Custom_MaterialList" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .dd_chk_select
        {
            height: 30px !important;
            margin:-2em 0 0;
        }

     
    </style>
     <script type="text/javascript">

     function ClosePS() {
            document.getElementById('divPSlite').style.display = 'none';
            document.getElementById('divPSfade').style.display = 'none';
        }

        function overlayPS() {
            document.getElementById('divPSlite').style.display = 'block';
            document.getElementById('divPSfade').style.display = 'block';
        }


        //For Entering Only Numbers.........
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else
                return true;

        }


        function validateDecimal(value)    {
            var RE = "^\d*\.?\d{0,2}$";
            if(RE.test(value)){
                return true;
            }else{
                return false;
            }
        }
  </script>

    <%-- <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
      <script src="../js/jquery-latest.js" type="text/javascript"></script>
  <script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>--%>
    <%-- <script type="text/javascript">
        function IsExists(pagePath, dataString, textboxid, errorlableid) {
            $.ajax({
                type: "POST",
                url: pagePath,
                data: dataString,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
          function (XMLHttpRequest, textStatus, errorThrown) {
              $(errorlableid).show();
              $(errorlableid).html("Error");
          },
                success:
          function (result) {
              if (result != null) {
                  var flg = (result.d);

                  if (flg == "True") {
                      $(errorlableid).show();
                      $(errorlableid).html('Verified');
                      document.getElementById('<%= txtauthpass.ClientID %>').value;
                      SaveandSendMail();
                  }
                  else {
                      $(errorlableid).show();
                      $(errorlableid).html('failure');
                  }
              }
          }
            });
        }
        function focuslost() {
            if (document.getElementById('<%= txtauthpass.ClientID%>').value == '') {
                alert('Please enter admin code!');
                return false;
            }
            else {
                var pagePath = "Custom_MaterialList.aspx/Exists";
                var dataString = "{ 'value':'" + document.getElementById('<%= txtauthpass.ClientID%>').value + "' }";
                var textboxid = "#<%= txtauthpass.ClientID%>";
                var errorlableid = "#<%= lblError.ClientID%>";

                IsExists(pagePath, dataString, textboxid, errorlableid);
                return true;
            }
        }

        $(".btnClose").live('click', function () {


            $('#<%=txtauthpass.ClientID %>, #<%=lblError.ClientID %>').val('');

            HidePopup();
        });

    </script>--%>
    <%--  var ddlCategory;
        function GetVendorCategories() {
            ddlCategory = document.getElementById("<%=ddlVendorCategory.ClientID %>");
            ddlCategory.options.length = 0;
            AddOption("Loading", "0");
            PageMethods.GetVendorCategories(OnSuccess);
        }

        window.onload = GetVendorCategories;

        function OnSuccess(response) {
            ddlCategory.options.length = 0;
            AddOption("select", "0");
            for (var i in response) {
                AddOption(response[i].Name, response[i].value);
            }
        }
        function AddOption(text, value) {
            var option = document.createElement('<option value="' + value + '">');
            ddlCategory.options.add(option);
            option.innerText = text;
        }
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


        function displayText(input) {
            var filename = ($(input).val());
            var lastIndex = filename.lastIndexOf("\\");
            if (lastIndex >= 0) {
                filename = filename.substring(lastIndex + 1);
            }
            $('#<%= txtFileUpload.ClientID%>').val(filename);
        }

        function checkDecimal(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        // Except only numbers and dot (.) for Cost textbox
        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
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

        .auto-style1 {
            width: 24px;
        }
        textarea {
            
            margin:-1em 0 0;
        }
        .wd-m
        {
            width: 88%;
            padding: 0 6px;
        }
        input
        {
            margin: -26px 0 0;
        }
        table
        {
            width: 100% !important;
        }
           #ContentPlaceHolder1_udp1 table tr
        {
             border-bottom: 1px solid #bbb !important;
        }
           .well {
  min-height: 20px;
  padding: 19px;
  margin-bottom: 20px;
  background-color: #f5f5f5;
  border: 1px solid #e3e3e3;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);
}
.well blockquote {
  border-color: #ddd;
  border-color: rgba(0, 0, 0, .15);
}
.well-lg {
  padding: 24px;
  border-radius: 6px;
}
.well-sm {
  padding: 9px;
  border-radius: 3px;
}
.close {
  float: right;
  font-size: 21px;
  font-weight: bold;
  line-height: 1;
  color: #000;
  text-shadow: 0 1px 0 #fff;
  filter: alpha(opacity=20);
  opacity: .2;
}
.close:hover,
.close:focus {
  color: #000;
  text-decoration: none;
  cursor: pointer;
  filter: alpha(opacity=50);
  opacity: .5;
}
button.close {
  -webkit-appearance: none;
  padding: 0;
  cursor: pointer;
  background: transparent;
  border: 0;
}
.modal-open {
  overflow: hidden;
}
.modal {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 1050;
  display: none;
  overflow: hidden;
  -webkit-overflow-scrolling: touch;
  outline: 0;
}
.modal.fade .modal-dialog {
  -webkit-transition: -webkit-transform .3s ease-out;
       -o-transition:      -o-transform .3s ease-out;
          transition:         transform .3s ease-out;
  -webkit-transform: translate(0, -25%);
      -ms-transform: translate(0, -25%);
       -o-transform: translate(0, -25%);
          transform: translate(0, -25%);
}
.modal.in .modal-dialog {
  -webkit-transform: translate(0, 0);
      -ms-transform: translate(0, 0);
       -o-transform: translate(0, 0);
          transform: translate(0, 0);
}
.modal-open .modal {
  overflow-x: hidden;
  overflow-y: auto;
}
.modal-dialog {
  position: relative;
  width: auto;
  margin: 10px;
}
.modal-content {
  position: relative;
  background-color: #fff;
  -webkit-background-clip: padding-box;
          background-clip: padding-box;
  border: 1px solid #999;
  border: 1px solid rgba(0, 0, 0, .2);
  border-radius: 6px;
  outline: 0;
  -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
          box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
}
.modal-backdrop {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: 1040;
  background-color: #000;
}
.modal-backdrop.fade {
  filter: alpha(opacity=0);
  opacity: 0;
}
.modal-backdrop.in {
  filter: alpha(opacity=50);
  opacity: .5;
}
.modal-header {
  min-height: 16.42857143px;
  padding: 15px;
  border-bottom: 1px solid #e5e5e5;
}
.modal-header .close {
  margin-top: -2px;
}
.modal-title {
  margin: 0;
  line-height: 1.42857143;
}
.modal-body {
  position: relative;
  padding: 15px;
}
.modal-footer {
  padding: 15px;
  text-align: right;
  border-top: 1px solid #e5e5e5;
}
.modal-footer .btn + .btn {
  margin-bottom: 0;
  margin-left: 5px;
}
.modal-footer .btn-group .btn + .btn {
  margin-left: -1px;
}
.modal-footer .btn-block + .btn-block {
  margin-left: 0;
}
.modal-scrollbar-measure {
  position: absolute;
  top: -9999px;
  width: 50px;
  height: 50px;
  overflow: scroll;
}
@media (min-width: 768px) {
  .modal-dialog {
    width: 600px;
    margin: 30px auto;
  }
  .modal-content {
    -webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
            box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
  }
  .modal-sm {
    width: 300px;
  }
}
@media (min-width: 992px) {
  .modal-lg {
    width: 900px;
  }
}
.tooltip {
  position: absolute;
  z-index: 1070;
  display: block;
  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
  font-size: 12px;
  font-style: normal;
  font-weight: normal;
  line-height: 1.42857143;
  text-align: left;
  text-align: start;
  text-decoration: none;
  text-shadow: none;
  text-transform: none;
  letter-spacing: normal;
  word-break: normal;
  word-spacing: normal;
  word-wrap: normal;
  white-space: normal;
  filter: alpha(opacity=0);
  opacity: 0;

  line-break: auto;
}
.tooltip.in {
  filter: alpha(opacity=90);
  opacity: .9;
}
.tooltip.top {
  padding: 5px 0;
  margin-top: -3px;
}
.tooltip.right {
  padding: 0 5px;
  margin-left: 3px;
}
.tooltip.bottom {
  padding: 5px 0;
  margin-top: 3px;
}
.tooltip.left {
  padding: 0 5px;
  margin-left: -3px;
}
.tooltip-inner {
  max-width: 200px;
  padding: 3px 8px;
  color: #fff;
  text-align: center;
  background-color: #000;
  border-radius: 4px;
}
.tooltip-arrow {
  position: absolute;
  width: 0;
  height: 0;
  border-color: transparent;
  border-style: solid;
}
.tooltip.top .tooltip-arrow {
  bottom: 0;
  left: 50%;
  margin-left: -5px;
  border-width: 5px 5px 0;
  border-top-color: #000;
}
.tooltip.top-left .tooltip-arrow {
  right: 5px;
  bottom: 0;
  margin-bottom: -5px;
  border-width: 5px 5px 0;
  border-top-color: #000;
}
.tooltip.top-right .tooltip-arrow {
  bottom: 0;
  left: 5px;
  margin-bottom: -5px;
  border-width: 5px 5px 0;
  border-top-color: #000;
}
.tooltip.right .tooltip-arrow {
  top: 50%;
  left: 0;
  margin-top: -5px;
  border-width: 5px 5px 5px 0;
  border-right-color: #000;
}
.tooltip.left .tooltip-arrow {
  top: 50%;
  right: 0;
  margin-top: -5px;
  border-width: 5px 0 5px 5px;
  border-left-color: #000;
}
.tooltip.bottom .tooltip-arrow {
  top: 0;
  left: 50%;
  margin-left: -5px;
  border-width: 0 5px 5px;
  border-bottom-color: #000;
}
.tooltip.bottom-left .tooltip-arrow {
  top: 0;
  right: 5px;
  margin-top: -5px;
  border-width: 0 5px 5px;
  border-bottom-color: #000;
}
.tooltip.bottom-right .tooltip-arrow {
  top: 0;
  left: 5px;
  margin-top: -5px;
  border-width: 0 5px 5px;
  border-bottom-color: #000;
}
.popover {
  position: absolute;
  top: 0;
  left: 0;
  z-index: 1060;
  display: none;
  max-width: 276px;
  padding: 1px;
  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
  font-size: 14px;
  font-style: normal;
  font-weight: normal;
  line-height: 1.42857143;
  text-align: left;
  text-align: start;
  text-decoration: none;
  text-shadow: none;
  text-transform: none;
  letter-spacing: normal;
  word-break: normal;
  word-spacing: normal;
  word-wrap: normal;
  white-space: normal;
  background-color: #fff;
  -webkit-background-clip: padding-box;
          background-clip: padding-box;
  border: 1px solid #ccc;
  border: 1px solid rgba(0, 0, 0, .2);
  border-radius: 6px;
  -webkit-box-shadow: 0 5px 10px rgba(0, 0, 0, .2);
          box-shadow: 0 5px 10px rgba(0, 0, 0, .2);

  line-break: auto;
}
.popover.top {
  margin-top: -10px;
}
.popover.right {
  margin-left: 10px;
}
.popover.bottom {
  margin-top: 10px;
}
.popover.left {
  margin-left: -10px;
}
.popover-title {
  padding: 8px 14px;
  margin: 0;
  font-size: 14px;
  background-color: #f7f7f7;
  border-bottom: 1px solid #ebebeb;
  border-radius: 5px 5px 0 0;
}
.popover-content {
  padding: 9px 14px;
}
.popover > .arrow,
.popover > .arrow:after {
  position: absolute;
  display: block;
  width: 0;
  height: 0;
  border-color: transparent;
  border-style: solid;
}
.popover > .arrow {
  border-width: 11px;
}
.popover > .arrow:after {
  content: "";
  border-width: 10px;
}
.popover.top > .arrow {
  bottom: -11px;
  left: 50%;
  margin-left: -11px;
  border-top-color: #999;
  border-top-color: rgba(0, 0, 0, .25);
  border-bottom-width: 0;
}
.popover.top > .arrow:after {
  bottom: 1px;
  margin-left: -10px;
  content: " ";
  border-top-color: #fff;
  border-bottom-width: 0;
}
.popover.right > .arrow {
  top: 50%;
  left: -11px;
  margin-top: -11px;
  border-right-color: #999;
  border-right-color: rgba(0, 0, 0, .25);
  border-left-width: 0;
}
.popover.right > .arrow:after {
  bottom: -10px;
  left: 1px;
  content: " ";
  border-right-color: #fff;
  border-left-width: 0;
}
.popover.bottom > .arrow {
  top: -11px;
  left: 50%;
  margin-left: -11px;
  border-top-width: 0;
  border-bottom-color: #999;
  border-bottom-color: rgba(0, 0, 0, .25);
}
.popover.bottom > .arrow:after {
  top: 1px;
  margin-left: -10px;
  content: " ";
  border-top-width: 0;
  border-bottom-color: #fff;
}
.popover.left > .arrow {
  top: 50%;
  right: -11px;
  margin-top: -11px;
  border-right-width: 0;
  border-left-color: #999;
  border-left-color: rgba(0, 0, 0, .25);
}
.popover.left > .arrow:after {
  right: 1px;
  bottom: -10px;
  content: " ";
  border-right-width: 0;
  border-left-color: #fff;
}

.clearfix:before,
.clearfix:after,
.dl-horizontal dd:before,
.dl-horizontal dd:after,
.container:before,
.container:after,
.container-fluid:before,
.container-fluid:after,
.row:before,
.row:after,
.form-horizontal .form-group:before,
.form-horizontal .form-group:after,
.btn-toolbar:before,
.btn-toolbar:after,
.btn-group-vertical > .btn-group:before,
.btn-group-vertical > .btn-group:after,
.nav:before,
.nav:after,
.navbar:before,
.navbar:after,
.navbar-header:before,
.navbar-header:after,
.navbar-collapse:before,
.navbar-collapse:after,
.pager:before,
.pager:after,
.panel-body:before,
.panel-body:after,
.modal-footer:before,
.modal-footer:after {
  display: table;
  content: " ";
}
.clearfix:after,
.dl-horizontal dd:after,
.container:after,
.container-fluid:after,
.row:after,
.form-horizontal .form-group:after,
.btn-toolbar:after,
.btn-group-vertical > .btn-group:after,
.nav:after,
.navbar:after,
.navbar-header:after,
.navbar-collapse:after,
.pager:after,
.panel-body:after,
.modal-footer:after {
  clear: both;
}
.center-block {
  display: block;
  margin-right: auto;
  margin-left: auto;
}
.pull-right {
  float: right !important;
}
.pull-left {
  float: left !important;
}
.hide {
  display: none !important;
}
.show {
  display: block !important;
}
.invisible {
  visibility: hidden;
}
.text-hide {
  font: 0/0 a;
  color: transparent;
  text-shadow: none;
  background-color: transparent;
  border: 0;
}
.hidden {
  display: none !important;
}
.affix {
  position: fixed;
}
@-ms-viewport {
  width: device-width;
}
.visible-xs,
.visible-sm,
.visible-md,
.visible-lg {
  display: none !important;
}
.visible-xs-block,
.visible-xs-inline,
.visible-xs-inline-block,
.visible-sm-block,
.visible-sm-inline,
.visible-sm-inline-block,
.visible-md-block,
.visible-md-inline,
.visible-md-inline-block,
.visible-lg-block,
.visible-lg-inline,
.visible-lg-inline-block {
  display: none !important;
}
@media (max-width: 767px) {
  .visible-xs {
    display: block !important;
  }
  table.visible-xs {
    display: table !important;
  }
  tr.visible-xs {
    display: table-row !important;
  }
  th.visible-xs,
  td.visible-xs {
    display: table-cell !important;
  }
}
@media (max-width: 767px) {
  .visible-xs-block {
    display: block !important;
  }
}
@media (max-width: 767px) {
  .visible-xs-inline {
    display: inline !important;
  }
}
@media (max-width: 767px) {
  .visible-xs-inline-block {
    display: inline-block !important;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  .visible-sm {
    display: block !important;
  }
  table.visible-sm {
    display: table !important;
  }
  tr.visible-sm {
    display: table-row !important;
  }
  th.visible-sm,
  td.visible-sm {
    display: table-cell !important;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  .visible-sm-block {
    display: block !important;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  .visible-sm-inline {
    display: inline !important;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  .visible-sm-inline-block {
    display: inline-block !important;
  }
}
@media (min-width: 992px) and (max-width: 1199px) {
  .visible-md {
    display: block !important;
  }
  table.visible-md {
    display: table !important;
  }
  tr.visible-md {
    display: table-row !important;
  }
  th.visible-md,
  td.visible-md {
    display: table-cell !important;
  }
}
@media (min-width: 992px) and (max-width: 1199px) {
  .visible-md-block {
    display: block !important;
  }
}
@media (min-width: 992px) and (max-width: 1199px) {
  .visible-md-inline {
    display: inline !important;
  }
}
@media (min-width: 992px) and (max-width: 1199px) {
  .visible-md-inline-block {
    display: inline-block !important;
  }
}
@media (min-width: 1200px) {
  .visible-lg {
    display: block !important;
  }
  table.visible-lg {
    display: table !important;
  }
  tr.visible-lg {
    display: table-row !important;
  }
  th.visible-lg,
  td.visible-lg {
    display: table-cell !important;
  }
}
@media (min-width: 1200px) {
  .visible-lg-block {
    display: block !important;
  }
}
@media (min-width: 1200px) {
  .visible-lg-inline {
    display: inline !important;
  }
}
@media (min-width: 1200px) {
  .visible-lg-inline-block {
    display: inline-block !important;
  }
}
@media (max-width: 767px) {
  .hidden-xs {
    display: none !important;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  .hidden-sm {
    display: none !important;
  }
}
@media (min-width: 992px) and (max-width: 1199px) {
  .hidden-md {
    display: none !important;
  }
}
@media (min-width: 1200px) {
  .hidden-lg {
    display: none !important;
  }
}
.visible-print {
  display: none !important;
}
@media print {
  .visible-print {
    display: block !important;
  }
  table.visible-print {
    display: table !important;
  }
  tr.visible-print {
    display: table-row !important;
  }
  th.visible-print,
  td.visible-print {
    display: table-cell !important;
  }
}
.visible-print-block {
  display: none !important;
}
@media print {
  .visible-print-block {
    display: block !important;
  }
}
.visible-print-inline {
  display: none !important;
}
@media print {
  .visible-print-inline {
    display: inline !important;
  }
}
.visible-print-inline-block {
  display: none !important;
}
@media print {
  .visible-print-inline-block {
    display: inline-block !important;
  }
}
@media print {
  .hidden-print {
    display: none !important;
  }
}
/*# sourceMappingURL=bootstrap.css.map */
.btn {
  display: inline-block;
  padding: 6px 12px;
  margin-bottom: 0;
  font-size: 14px;
  font-weight: 400;
  line-height: 1.42857143;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
  touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
  background-image: none;
  border: 1px solid transparent;
  border-radius: 4px;
}
    </style>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

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
        <h1 id="h1Heading" runat="server">Material List</h1>

        <table>
            <tr>
                <td>&nbsp;
                    <asp:Label ID="lblerrNew" runat="server"></asp:Label>
                </td>
                <td class="auto-style1">&nbsp;
                </td>
                <td>
                    <%--<asp:LinkButton ID="lnkForemanPermission" runat="server" Text="Foreman Permission"></asp:LinkButton>--%>
                    
                         <br />
                    <asp:TextBox ID="txtForemanPasswordNew" CssClass="wd-m" Height="30px" runat="server" placeHolder="Enter forman password" OnTextChanged="txtForemanPassword_TextChanged" AutoPostBack="true" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="lblFormanEmail" runat="server" Visible="false"></asp:Label>
                    <asp:LinkButton ID="lnkForemanId" runat="server" OnClick="lnkForemanId_Click"></asp:LinkButton> &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblForemanName" runat="server"></asp:Label>

                    <asp:LinkButton ID="lnkEmployeeId" runat="server"></asp:LinkButton> <asp:Label ID="lnkEmployeeName" runat="server"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtAdminPasswordNew" runat="server" Height="30px" CssClass="wd-m" placeHolder="Enter admin password" OnTextChanged="txtAdminPassword_TextChanged" AutoPostBack="true" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="lblAdmin" runat="server"></asp:Label>
                    <br />
                    &nbsp;<asp:Label ID="lblErrorForeman" runat="server" Text=""></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    <br />
                    &nbsp;<asp:CustomValidator ID="CVForeman" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CustomValidator ID="CVAdmin" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    <%--<asp:LinkButton ID="lnkAdminPermission" runat="server" Text="Admin Permission" Visible="false"></asp:LinkButton>--%>
                    <br />

                </td>
              
                <td>
                    <asp:TextBox ID="txtSrSalesPassword" CssClass="wd-m"  runat="server" Visible="false" Height="30px" placeHolder="Enter sr sales password" AutoPostBack="true" OnTextChanged="txtSrSalesPassword_TextChanged" TextMode="Password" Width="121px"></asp:TextBox>
                    <asp:Label ID="lblSrSales" runat="server"></asp:Label>
                    <asp:TextBox ID="txtSrSalesManPermition"  CssClass="wd-m" runat="server" AutoPostBack="true" Height="30px" placeHolder="Enter sr sales password" OnTextChanged="txtSrSalesManPermition_TextChanged" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="lblSrSalesManPermition" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                    <asp:CustomValidator ID="CVSrSalesmanA" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    &nbsp;
                    <%--<asp:LinkButton ID="lnkSrSalesmanPermissionA" runat="server" Text="Sr. Salesman Permission"
                        Visible="false"></asp:LinkButton>--%>
                    &nbsp;<asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                    <asp:CustomValidator ID="CVSrSalesmanF" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    <%--<asp:LinkButton ID="lnkSrSalesmanPermissionF" runat="server" Text="Sr. Salesman Permission"></asp:LinkButton>--%>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnAdmin" runat="server" />
        <asp:HiddenField ID="hdnForeman" runat="server" />
        <asp:HiddenField ID="hdnSrA" runat="server" />
        <asp:HiddenField ID="hdnSrF" runat="server" />
        <%--<ajaxToolkit:ModalPopupExtender ID="popupAdmin_permission" TargetControlID="lnkAdminPermission"
            runat="server" CancelControlID="btnCloseAdmin" PopupControlID="pnlpopup">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="175px" Width="300px"
            Style="z-index: 111; background-color: White; position: absolute; left: 35%;
            top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Admin Verification
                        <asp:Button ID="btnXAdmin" runat="server" OnClick="btnXAdmin_Click" Text="X" Style="float: right;
                            text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="LabelValidate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Admin Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVAdmin" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifyAdmin" runat="server" Text="Verify" OnClick="VerifyAdminPermission" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>
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
            Width="300px" Style="z-index: 111; background-color: White; position: absolute;
            left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Sr. Salesman Verification
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
                    <td align="right">
                        Sr. Salesman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSrSalesmanPasswordA" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVSrSalesmanA" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifySrSalesmanA" runat="server" Text="Verify" OnClick="VerifySrSalesmanPermissionA" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>
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
            Width="300px" Style="z-index: 111; background-color: White; position: absolute;
            left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Foreman Verification
                        <asp:Button ID="btnXForeman" runat="server" OnClick="btnXForeman_Click" Text="X"
                            Style="float: right; text-decoration: none" /><%--<a id="A1" style="color: white; float: right; text-decoration: none"
                            class="btnClose" href="#">X</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Foreman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtForemanPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<asp:Label ID="lblErrorForeman" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVForeman" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>-
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifyForeman" runat="server" Text="Verify" OnClick="VerifyForemanPermission" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>
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
            Width="300px" Style="z-index: 111; background-color: White; position: absolute;
            left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Sr. Salesman Verification
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
                    <td align="right">
                        Sr. Salesman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSrSalesmanPasswordF" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVSrSalesmanF" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifySrSalesmanF" runat="server" Text="Verify" OnClick="VerifySrSalesmanPermissionF" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseSrSalesmanF" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>--%>
        <div class="grid">
            Customer Id:&nbsp;<asp:LinkButton ID="CusId" runat="server" OnClick="CusId_Click"></asp:LinkButton>&nbsp;&nbsp;
            Jobs#:&nbsp;<asp:LinkButton ID="SSNo" runat="server" OnClick="SSNo_Click"></asp:LinkButton>&nbsp;&nbsp;
            Customer Name:&nbsp;<%--<asp:LinkButton ID="CusName" runat="server" OnClick="CusName_Click"></asp:LinkButton>--%><asp:Label ID="CusName" runat="server" ForeColor="#0066ff"></asp:Label>
            <asp:UpdatePanel ID="udp1" runat="server">
                <ContentTemplate>

              
            <asp:GridView ID="grdcustom_material_list" runat="server" Width="108%" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"  Style="width:636px;border-collapse:collapse"
                OnRowDataBound="grdcustom_material_list_RowDataBound" OnRowDeleting="grdcustom_material_list_RowDeleting" OnRowCommand="grdcustom_material_list_RowCommand" GridLines="Horizontal">  <%--DataKeyNames="SoldJobId"--%>
                <Columns>
                   

                    <asp:TemplateField HeaderText="Product Category" 
                            ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HiddenField ID="HiddenField1" runat="server"  Value='<%# Eval("Visible") %>'/>
                            <asp:HiddenField ID="hfMyRow" runat="server"  Value='<%# Eval("MyRow") %>'/> <%----%>
                            <asp:HiddenField ID="hdnSoldJobId" runat="server" Value='<%# Eval("SoldJobId") %>' />
                            <asp:HiddenField ID="hdnCategorynew" runat="server" Value='<%# Eval("Display") %>' />
                            <asp:HiddenField ID="hidCategory" runat="server" Value='<%# Eval("ProductId") %>' />
                           
                            <asp:DropDownList ID="ddlCategory" Style="height: 30px;" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList> <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("ProductId") %>' Visible="false" />
                             <asp:LinkButton ID="lnkAdd" runat="server" Text="Add Line" CommandName="Add" CommandArgument='<%# Container.DataItemIndex %>' OnClick="Add_Click"></asp:LinkButton>
                            <span></span>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Line-Image">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLine" Text='<%# Eval("Line") %>' Style="height: 30px; width:40px" MaxLength="4" runat="server" ClientIDMode="Static" OnTextChanged="txtLine_TextChanged" AutoPostBack="true"></asp:TextBox>
                           <%-- <asp:RangeValidator ID="rvLine" ErrorMessage="Line must be 3 to 4 digits" ControlToValidate="txtLine" runat="server" MaximumValue="4" MinimumValue="3" ></asp:RangeValidator>--%>
                        <%-- <asp:RegularExpressionValidator  ControlToValidate = "txtLine" ID="rvLine" ValidationExpression = "^[\s\S]{3,4}$" runat="server" ErrorMessage="Line must be 3 to 4 digits."></asp:RegularExpressionValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JG sku-vendor part #"> <%--JG SKU Part #--%>
                        <ItemTemplate>
                            <asp:TextBox ID="txtSkuPartNo" Text='<%# Eval("JGSkuPartNo") %>' Style="height: 30px;  width:150px" MaxLength="18" runat="server" ClientIDMode="Static" OnTextChanged="txtSkuPartNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                         <%--  <asp:RangeValidator ID="rvSKU" ErrorMessage="SKU must be 16 to 18 digits" ControlToValidate="txtSkuPartNo" runat="server" MaximumValue="18" MinimumValue="16" ></asp:RangeValidator>--%>
                        <%--<asp:RegularExpressionValidator  ControlToValidate = "txtSkuPartNo" ID="rvSKU" ValidationExpression = "^[\s\S]{16,18}$" runat="server" ErrorMessage="SKU must be 16 to 18 digits."></asp:RegularExpressionValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescription" Text='<%# Eval("Description") %>' runat="server" Style="height: 30px;" TextMode="MultiLine" ClientIDMode="Static" OnTextChanged="txtDescription_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQTY" Text='<%# Eval("Qty") %>' runat="server" Style="height: 30px;  width:40px" MaxLength="4" ClientIDMode="Static" onkeypress="return isNumberKey(event)" OnTextChanged="txtQTY_TextChanged" AutoPostBack="true"></asp:TextBox>
                          <%--  <asp:RangeValidator ID="rvQTY" ErrorMessage="QTY must be 1 to 4 digits" ControlToValidate="txtQTY" runat="server" MaximumValue="4" MinimumValue="1" ></asp:RangeValidator>--%>
                        <%--<asp:RegularExpressionValidator  ControlToValidate = "txtQTY" ID="rvQTY" ValidationExpression = "^[\s\S]{1,4}$" runat="server" ErrorMessage="QTY must be 1 to 4 digits."></asp:RegularExpressionValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUOM" Text='<%# Eval("UOM") %>' runat="server" Style="height: 30px;  width:50px" MaxLength="5" ClientIDMode="Static" OnTextChanged="txtUOM_TextChanged" AutoPostBack="true"></asp:TextBox>
                           <%-- <asp:RangeValidator ID="rvUOM" ErrorMessage="UOM must be 1 to 4 digits" ControlToValidate="txtUOM" runat="server" MaximumValue="5" MinimumValue="2" ></asp:RangeValidator>--%>
                       <%-- <asp:RegularExpressionValidator  ControlToValidate = "txtUOM" ID="rvUOM" ValidationExpression = "^[\s\S]{2,5}$" runat="server" ErrorMessage="UOM must be 2 to 5 digits."></asp:RegularExpressionValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor Quotes/Invoice">
                        <ItemTemplate>
                            <asp:DropDownCheckBoxes ID="ddlVendorName" ClientIDMode="Static" runat="server" Style="height: 30px; margin:-2em 0 0;" Width="180px" UseSelectAllNode="true" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged1" AutoPostBack="true">
                            </asp:DropDownCheckBoxes>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost">  <%--Material Cost Per Item--%>
                        <ItemTemplate>
                            <asp:UpdatePanel ID="udpMaterialCost" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtMaterialCost" Text='<%# Eval("MaterialCost") %>' AutoPostBack="true" Style="height: 30px; width:140px" OnTextChanged="txtMaterialCost_TextChanged" runat="server" ClientIDMode="Static" onkeypress="return onlyDotsAndNumbers(event)"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<asp:Label ID="lblMaterialCost" runat="server" ClientIDMode="Static"></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Extended">
                        <ItemTemplate>
                           
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlExtent" AutoPostBack="true" runat="server" Style="height: 30px;" Text='<%# Eval("Extent") %>' OnSelectedIndexChanged="ddlExtent_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                        <asp:ListItem Text="Pick Up" Value="PickUp"></asp:ListItem>
                                        <asp:ListItem Text="Job Site Delivery" Value="Jobsitedelivery"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                        <asp:ListItem Text="Office Delivery" Value="OfficeDelivery"></asp:ListItem>
                                        <asp:ListItem Text="Stock Location" Value="StockLocation"></asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                             <asp:UpdatePanel ID="udpCost" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblCost" Text='<%# Eval("SubTotal") %>' runat="server"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtMaterialCost" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="udpTotalCost" runat="server">
                                <ContentTemplate>
                                    <%--<asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>' ClientIDMode="Static"></asp:Label>--%>
                                    <asp:LinkButton ID="lblTotal"  data-toggle="modal" data-target="#myModal" runat="server" Text='<%# Eval("Total") %>' ClientIDMode="Static"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkAttachQuotes" Text="Attach Quotes" runat="server" OnClick="lnkAttachQuotes_Click" ClientIDMode="Static"></asp:LinkButton>
                                     </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlExtent" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAdd" runat="server" Text="Add Line" CommandName="Add" CommandArgument='<%# Container.DataItemIndex %>' OnClick="Add_Click"></asp:LinkButton>

                            <label>
                                &nbsp;</label>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" Text="Delete Line" Visible="false"></asp:LinkButton>                         
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAdd" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.DataItemIndex %>' OnClick="lnkbtnAdd_Click"></asp:LinkButton>

                            <label>
                                &nbsp;</label>
                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                            <%--<asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' Text="Delete"></asp:LinkButton>--%>
                            <%--<asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>' />--%>
                            <%--<asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>' />--%>
                            <%--<asp:HiddenField ID="hdnForemanPermission" runat="server" Value='<%#Eval("IsForemanPermission")%>' />
                            <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>' />
                            <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>' />
                            <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>' />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:CommandField ShowDeleteButton="True" ButtonType="Button" Visible="false"/>
                             <%-- HeaderText="Delete" HeaderStyle-BackColor="#FF286F" />--%>
                </Columns>
            </asp:GridView>

               </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:GridView ID="grdcustom_material_list" runat="server" Width="108%" AutoGenerateColumns="false"
                OnRowDataBound="grdcustom_material_list_RowDataBound" OnRowDeleting="grdcustom_material_list_RowDeleting" OnRowCommand="grdcustom_material_list_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No.">
                        <ItemTemplate>
                            <asp:Label ID="lblsrno" Text="0" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Line">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMateriallist" Text='<%#Eval("MaterialList")%>' TextMode="MultiLine"
                                runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material List">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMateriallist" Text='<%#Eval("MaterialList")%>' TextMode="MultiLine"
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
                    <%--<asp:TemplateField HeaderText="Attach Quotes" HeaderStyle-Width="16%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkaddvendorquotes" runat="server" Text="Attach Quotes" OnClick="lnkaddvendorquotes_Click"
                                HeaderStyle-Width="200px"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Vendor Name">
                        <ItemTemplate>
                            <asp:DropDownCheckBoxes ID="ddlVendorName" ClientIDMode="Static" runat="server" Width="180px" UseSelectAllNode="false">
                            </asp:DropDownCheckBoxes>

                            <%--<asp:DropDownList ID="ddlVendorName" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged"
                                ClientIDMode="Static" runat="server" Width="150px" Enabled="false" AutoPostBack="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quote">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkQuote" runat="server" Text='<%#Eval("DocName")%>' CommandArgument='<%#Eval("TempName") %>' CommandName="View"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount($)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text="0.00" onkeypress="return isNumericKey(event);"
                                MaxLength="15" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="Add_Click"></asp:LinkButton>
                            <label>
                                &nbsp;</label>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                            <asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>' />
                            <asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>' />
                            <asp:HiddenField ID="hdnForemanPermission" runat="server" Value='<%#Eval("IsForemanPermission")%>' />
                            <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>' />
                            <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>' />
                            <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
        </div>
        <div class="btn_sec">
           <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
            <asp:Button ID="btnSendMail" runat="server" Text="Save" OnClick="btnSendMail_Click"
                Style="background: url(../img/btn1.png) no-repeat;" Width="300" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
        </div>
        <h1>Edit Email Templates
        </h1>
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
                    <b>Email Template For Vendor Category</b>
                </h2>
                <div>
                    <h2>Header Template
                    </h2>
                    <div>
                        <asp:FileUpload ID="flpEmailAttachment" Style="margin-bottom: 10px" runat="server" />&nbsp;&nbsp;<asp:Button ID="btnAttach" Width="80px" Height="30px" runat="server" Text="Upload" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff; margin-bottom: 10px;" OnClick="btnAttach_Click" />
                    </div>
                    <cc1:Editor ID="HeaderEditor" Width="1000px" Height="200px" runat="server" />
                    <h2>Body Template
                    </h2>
                    <asp:Label ID="lblMaterials" runat="server"></asp:Label>
                    <h2>Footer Template
                    </h2>
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
                <b>Email Template For Vendors</b>
            </h2>
            <div>
                <h2>Header Template
                </h2>
                <div>
                    <asp:FileUpload ID="flpVendorEmailAttach" Style="margin-bottom: 10px" runat="server" />&nbsp;&nbsp;<asp:Button ID="btnUploadVendorAttach" runat="server" Text="Upload" Width="80px" Height="30px" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff; margin-bottom: 10px" OnClick="btnUploadVendorAttach_Click" />
                </div>
                <cc1:Editor ID="HeaderEditorVendor" Width="1000px" Height="200px" runat="server" />
                <h2>Body Template
                </h2>
                <asp:Label ID="lblMaterialsVendor" runat="server"></asp:Label>
                <h2>Footer Template
                </h2>
                <cc1:Editor ID="FooterEditorVendor" Width="1000px" Height="200px" runat="server" />
            </div>
            <br />
            <br />
            <div class="btn_sec">
                <asp:Button ID="btnUpdateVendor" runat="server" Text="Update" OnClick="btnUpdateVendor_Click" />
            </div>
        </asp:Panel>
    </div>

    <asp:Panel ID="panelPopup" runat="server">
        <div id="light" class="white_content">
            <h3><b>Upload Vendor Quotes</b></h3>
            <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">Close</a>
            <br />
            <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td>
                        <div class="grid_h">
                            <strong>List Of Documents Attached</strong>
                        </div>
                        <div class="grid">
                            <asp:GridView ID="grdAttachQuotes" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="tableClass" OnRowCommand="grdAttachQuotes_RowCommand"
                                OnRowDeleting="grdAttachQuotes_RowDeleting"
                                OnRowDataBound="grdAttachQuotes_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VendorCategoryNm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VendorName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Document">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocName") %>'
                                                CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                            <%--<asp:Label ID="lblFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"originalFileName") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <%--<asp:UpdatePanel ID="upAttachQuotes" runat="server">
                            <ContentTemplate>--%>
                                <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 15%">Vendor Category
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpVendorCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpVendorCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 15%">Vendor Name
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpVendorName" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 15%">Upload Document
                                        </td>
                                        <td style="width: 25%">
                                           <%-- <asp:FileUpload ID="uploadvendorquotes" runat="server" onchange="displayText(this);" Style="display: none" />
                                            <input id="btnFileUpload" type="button" value="Browse" runat="server" style="width: 70px" />
                                            <asp:TextBox ID="txtFileUpload" runat="server" Width="310px" Enabled="false" />--%>
                                            <%--CssClass="multi MultiFile-wrap" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblOR" Text="OR" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%">File Name
                                        </td>
                                        <td style="width: 90%">
                                            <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%">File Content
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFileContent" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                       <%--     </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>--%>
                </tr>

            </table>
            <div class="btn_sec">
                <asp:Button ID="btnSaveQuotes" CommandName="Delete" runat="server" Text="Save" OnClick="btnSaveQuotes_Click" />
                <asp:Button ID="btnResetQuotes" CommandName="Reset" runat="server" Text="Reset"
                    OnClick="btnResetQuotes_Click" />
                <asp:Button ID="btnCancelQuotes" runat="server" Text="Cancel" OnClick="btnCancelQuotes_Click" />
            </div>
        </div>
    </asp:Panel>
    <div id="fade" class="black_overlay">
    </div>

    <%--<ajaxToolkit:ModalPopupExtender ID="popup_permission" TargetControlID="btnSendMail"
            runat="server" CancelControlID="btnClose1" PopupControlID="pnlpopup">
        </ajaxToolkit:ModalPopupExtender>--%>




    <asp:Panel ID="panel6" runat="server">
            <div id="divPSlite" class="white_content">
                <h3>
                </h3>
                <a href="javascript:void(0)" onclick="document.getElementById('divPSlite').style.display='none';document.getElementById('divPSfade').style.display='none'">Close</a>
               

                <div class="form_panel">
        <div class="right_panel">
            <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td>
                        <div class="grid_h">
                            <strong>List Of Documents Attached</strong>
                        </div>
                        <div class="grid">
                            <asp:GridView ID="gvAttachQuotes" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="tableClass" OnRowCommand="gvAttachQuotes_RowCommand" 
                                OnRowDeleting="gvAttachQuotes_RowDeleting" 
                                onrowdatabound="gvAttachQuotes_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select"  CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VendorCategoryNm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VendorName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Document">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocName") %>'
                                                CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                            <%--<asp:Label ID="lblFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"originalFileName") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TempName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 15%">
                                            Vendor Category
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpVendorCategory1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpVendorCategory1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 15%">
                                            Vendor Name
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpVendorName1" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 15%">
                                            Upload Document
                                        </td>
                                        <td style="width: 25%">
                                           <%-- <asp:FileUpload ID="FileUpload1" runat="server" onchange="displayText(this);"   style="display: none"/>
                                            <input id="Button1" type="button" value="Browse" runat="server" style="width: 70px" />
                                            <asp:TextBox ID="TextBox1" runat="server" Width="310px" Enabled="false"/>--%>

                                             <asp:FileUpload ID="uploadvendorquotes" runat="server" onchange="displayText(this);" Style="display: none" />
                                            <input id="btnFileUpload" type="button" value="Browse" runat="server" style="width: 70px" />
                                            <asp:TextBox ID="txtFileUpload" runat="server" Width="310px" Enabled="false" />


                                            <%--CssClass="multi MultiFile-wrap" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" Text="OR" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%">
                                            File Name
                                        </td>
                                        <td style="width: 90%">
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%">
                                            File Content
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
            </table>
            <div class="btn_sec">
                <asp:Button ID="Button2" CommandName="Delete" runat="server" Text="Save" OnClick="btnSaveQuotes1_Click" />
                <asp:Button ID="Button3" CommandName="Reset" runat="server" Text="Reset" 
                    onclick="btnResetQuotes1_Click" />
                <asp:Button ID="Button4" runat="server" Text="Cancel" OnClick="btnCancelQuotes1_Click" />
            </div>
        </div>
    </div>
            </div>
        </asp:Panel>

        <div id="divPSfade" class="black_overlay">
        </div>

    <!-- Button trigger modal
<button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#myModal">
  Launch demo modal
</button> -->

<!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">Procurement Material List</h4>

      </div>
      <div class="modal-body">
       <h5>  Total<br /><br />J&L - 19345 &nbsp;&nbsp;&nbsp;&nbsp;Delivery: $43.75                    
             <br />Misc. Fee:$ 0.00  &nbsp;&nbsp;&nbsp;&nbsp; Total                                             
             <br /> Vendor Invoice: $ 1.00 
             <br />payment method  <br />
             <br />Quality - 19128 &nbsp;&nbsp;&nbsp;&nbsp; Delivery: $43.75
             <br /> Misc. Fee:$ 0.00
             <br />Total Vendor Invoice: $33.75
             <br />payment method <br />
             <br /> Sherwin Williams1029-19054&nbsp;&nbsp;&nbsp;&nbsp;Delivery: $43.75
             <br />Misc. Fee:$ 0.00 
             <br />Total Vendor Invoice: $100.00 
             <br />payment method<br />
             <br />Sales Tax: $  
             <br />Total Job Material $: 
       </h5>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" runat="server" visible="false">Save changes</button>
      </div>
    </div>
  </div>
</div>
</asp:Content>
