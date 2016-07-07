<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calender.aspx.cs" Inherits="JG_Prospect.JGCalender.Calender" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />

    <link rel="stylesheet" href="css/jquery-ui.css" />
    <script src="jquery/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="jquery/jquery-ui-1.11.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places&key=AIzaSyCKkBhvDXVd3K53AdMRXjbKpE0utScfWZM"></script>
    <script type="text/javascript">
        var source, destination;
        var directionsDisplay;
        var directionsService = new google.maps.DirectionsService();

        google.maps.event.addDomListener(window, 'load', function () {
            new google.maps.places.SearchBox(document.getElementById('txtFromLocation'));
            new google.maps.places.SearchBox(document.getElementById('txtToLocation'));
            directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
        });

        function GetRoute() {
            // var mumbai = new google.maps.LatLng(18.9750, 72.8258);
            var philadelphia = new google.maps.LatLng(39.9526, 75.1652);
            var mapOptions = {
                zoom: 7,
                center: philadelphia
            };

            map = new google.maps.Map(document.getElementById('dvMap'), mapOptions);
            directionsDisplay.setMap(map);

            //directionsDisplay.setPanel(document.getElementById('dvPanel'));

            //*********DIRECTIONS AND ROUTE**********************//
            source = document.getElementById("txtFromLocation").value;
            destination = document.getElementById("txtToLocation").value;

            var request = {
                origin: source,
                destination: destination,
                travelMode: google.maps.TravelMode.DRIVING
            };
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                }
            });

            //*********DISTANCE AND DURATION**********************//
            var service = new google.maps.DistanceMatrixService();
            service.getDistanceMatrix({
                origins: [source],
                destinations: [destination],
                travelMode: google.maps.TravelMode.DRIVING,
                unitSystem: google.maps.UnitSystem.METRIC,
                avoidHighways: false,
                avoidTolls: false
            }, function (response, status) {
                if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS") {
                    var distance = response.rows[0].elements[0].distance.text;
                    var duration = response.rows[0].elements[0].duration.text;
                    var dvDistance = document.getElementById("dvDistance");
                    dvDistance.innerHTML = "";
                    dvDistance.innerHTML += "Distance: " + distance + "<br />";
                    dvDistance.innerHTML += "Duration:" + duration;

                } else {
                    alert("Unable to find the distance via road.");
                }
            });
        }
    </script>
    <link href="datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />

    <link href="datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <%--
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
        <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>

    <script src="http://netdna.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <style>
        .caret-right {
            width: 0;
            height: 0;
            border-top: 4px solid rgba(0, 0, 0, 0);
            border-bottom: 4px solid rgba(0, 0, 0, 0);
            border-left: 4px solid #777777;
        }

        .dropdown-menu > li {
            padding-left: 10px;
        }

        dropdown-menu .divider1 {
            height: 1px;
            margin: 5px 0 !important;
            overflow: hidden;
            background-color: #e5e5e5;
            /*DropDown Menu Employees*/
        }

        .customer-check-box {
            margin-right: 5px;
        }

        input[type=checkbox] {
            margin: 4px 10px 0;
            margin-top: 1px\9;
            line-height: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hdnCustomerIds" runat="server" />
        <div style="width: 100%;">
            <table>
                <tr>
                    <td>
                        <table>

                            <tr>
                                <td style="width: 1200px;">

                                    <%-- <div class="date dateCalender" id="datepicker"></div>--%>


                                    <div style="margin-left: 10px; width: 65%; padding-top: 10px;">
                                        <div class="form-group has-feedback" style="align-self: center">
                                            <input type="text" class="form-control" name="txtSearch" id="txtSearch" placeholder="Search Calendar" />
                                            <span class="glyphicon glyphicon-search form-control-feedback"></span>
                                        </div>
                                    </div>
                                    <div class="calender-header" style="padding: 5px;">
                                        <ul class="nav navbar-nav" style="display: -webkit-inline-box">

                                            <li>
                                                <a>
                                                    <label>
                                                        <input type='radio' id="chkAllLead" name="chkLead" value="All" checked="checked" />All Lead
                                                    </label>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <label>
                                                        <input type="radio" id="chkPersonalLead" name="chkLead" value="Personal" />Personal Lead
                                                    </label>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Sales Calendar <b class="caret"></b></a>
                                                <ul id="ul-customer" class="dropdown-menu" style="width: 250px; height: 250px; overflow-y: scroll;">
                                            </li>


                                        </ul>
                                    </div>
                                </td>


                            </tr>

                            <tr>
                                <td>
                                    <iframe src="SalesCalender.aspx" id="iframeSalesCalender" width="100%" height="600" style="border: 0;"></iframe>
                                </td>

                            </tr>
                            <tr>

                                <td style="width: 1200px;">
                                    <div style="float: left; width:18%">
                                        <table style="font-family: Verdana; font-size: small;" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="font-family: Verdana; font-size: 11pt; color: red;">
                                                        <b>A:</b><textarea name="txtFromLocation" rows="2" cols="10" id="txtFromLocation" style="color: Black; width: 180px;">3502 Scotts Ln Philadelphia, PA 19129</textarea>
                                                        <br>
                                                        <br>
                                                        <b>B:</b><textarea name="txtToLocation" rows="2" cols="10" id="txtToLocation" style="color: Black; width: 180px;"></textarea>
                                                        <br>
                                                        <div class="btn_sec">
                                                            <input type="submit" name="btnGetDirection" value="Get Direction" id="btnGetDirection" onclick="GetRoute();" tabindex="3" style="height: 40px; width: 190px;margin: 6px;">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="direction_steps_holder" style="width: 100%">
                                                            <div id="dvDistance" style="padding:5px;">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div style="float: left;width: 47%;padding: 0 0 0 80px;">
                                        <div id="dvMap" style="width:95%; height: 300px;">
                                            <%--<tr>
                                                    <td colspan="2">
                                                        <div id="map_canvasMarkers" style="width: 500px; height: 400px"></div>
                                                    </td>
                                                </tr>--%>
                                        </div>
                                    </div>

                                    <div style="float: right; width:35%;">
                                        <div class="date dateCalender" id="datepicker"></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top;"></td>
                </tr>
            </table>


        </div>
    </form>
    <script type="text/javascript">
        Date.prototype.yyyymmdd = function () {
            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
            var dd = this.getDate().toString();
            return yyyy + "-" + (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]); // padding
        };

        var isPersonal = false;
        var date = '';
        $(document).ready(function () {
            $('#datepicker').datepicker({
                dateFormat: 'DD, d MM, yy',
                inline: true,
                onSelect: function (dateText, inst) {
                    var d = new Date(dateText);
                    date = d.yyyymmdd();
                    LoadCalender();

                }

            });
            //$('#datepicker').hide();
            //$("#lnkDatePicker").click(function () {
            //    $('#datepicker').toggle();
            //});
            $('.navbar a.dropdown-toggle').on('click', function (e) {

                var elmnt = $(this).parent().parent();
                if (!elmnt.hasClass('nav')) {
                    var li = $(this).parent();
                    var heightParent = parseInt(elmnt.css('height').replace('px', '')) / 2;
                    var widthParent = parseInt(elmnt.css('width').replace('px', '')) - 10;

                    if (!li.hasClass('open')) li.addClass('open')
                    else li.removeClass('open');
                    $(this).next().css('top', heightParent + 'px');
                    $(this).next().css('left', widthParent + 'px');

                    return false;
                }
            });

            $.getJSON("Customer.ashx", function (data) {

                var items = [];
                $.each(data, function (key, val) {
                    // items.push("<li id='" + key + "'>" + val + "</li>");
                    items.push("  <li class='divider'></li> <li class='li-emp'><input type='checkbox' class='customer-check-box' customer_id='" + val.id + "' id='chk_" + val.id + "' />" + val.name + "</li>");
                });


                $("#ul-customer").append(items.join(""));

                $('.customer-check-box').on('click', function () {

                    if (isPersonal) {

                        LoadCalender();
                    }

                });


                $("#txtSearch").keyup(function (event) {

                    if (event.keyCode == 13) {
                        LoadCalender();
                        return false;
                    }
                });

            });

            $(function () {
                $("form").submit(function () { return false; });
            });

            $('input[name=chkLead]').on('change', function () {
                ClearSearchText();
                ClearCustomerCheckBox();
                var chkBox = $('input[name=chkLead]:checked').val();
                ClearCustomerCheckBox();
                if (chkBox == 'All') {
                    isPersonal = false;

                    LoadCalender();
                }
                if (chkBox == 'Personal') {
                    isPersonal = true;
                }

            });

        });

        function ClearCustomerCheckBox() {
            $('input:checkbox.customer-check-box').each(function () {
                if (this.checked)
                    this.checked = false;
            });
        }

        function ClearSearchText() {
            $('#txtSearch').val('');
        }

        function LoadCalender() {
            var customers = [];
            var txtSearch = $('#txtSearch').val();

            $('input:checkbox.customer-check-box').each(function () {
                if (this.checked)
                    customers.push($(this).attr('customer_id'))
            });
            $("#iframeSalesCalender").attr("src", "SalesCalender.aspx?e=" + JSON.stringify(customers) + "&s=" + txtSearch + "&d=" + date);

            date = '';
        }

    </script>
</body>
</html>
