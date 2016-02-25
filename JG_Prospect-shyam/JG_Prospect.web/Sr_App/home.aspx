<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="JG_Prospect.Sr_App.home" %>

<%@ Register Src="~/Sr_App/LeftPanel.ascx" TagName="LeftPanel" TagPrefix="uc2" %>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script src="https://maps.googleapis.com/maps/api/js?v=3.exp" type="text/javascript"></script>--%>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">  
    </script>




     <%--<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>--%>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>







    <script type="text/javascript">

        source = null;
        destination = null;

        function PushLocationData(objsource, objdestination) {
            source = objsource;
            destination = objdestination;
        }

        $(document).ready(function () {

            directionsService = new google.maps.DirectionsService();

            //Source Location - Red Fort, New Delhi, India
            sourceLatLng = new google.maps.LatLng(source.Latitude, source.Longitude);

            //Destiation Location - 27.175114, 78.042154 – Taj Mahal, Agra
            destinationLatLng = new google.maps.LatLng(destination.Latitude, destination.Longitude);

            //set the map options
            var mapOptions = {
                zoom: 20,
                center: destinationLatLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                navigationControl: true
            };

            //create the map object
            map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

            directionsRenderer = new google.maps.DirectionsRenderer({
                'map': map,
                'draggable': false,
                'hideRouteList': true,
                'suppressMarkers': true
            });

            directionsService.route({
                'origin': sourceLatLng,
                'destination': destinationLatLng,
                'travelMode': 'DRIVING'
            },
            function (directions, status) {
                for (var i = 0; i < directions.routes.length; i++) {
                    var thisRoute = directions.routes[i];

                    for (var j = 0; j < thisRoute.legs.length; j++) {
                        var thisLeg = thisRoute.legs[j];

                        var useDistance = thisLeg.distance.text;
                        var useDuration = thisLeg.duration.text;

                        var directionsHTML = '';
                        for (var k = 0; k < thisLeg.steps.length; k++) {
                            var thisStep = thisLeg.steps[k];
                            directionsHTML += '<div>' + (k + 1) + '. ' + thisStep.instructions + '<BR/>-------------------' + thisStep.distance.text + '</div>';
                            directionsHTML += '<div></div>';
                        }
                        $('#total_distance').html(useDistance);
                        $('#total_duration').html(useDuration);
                        $('#direction_steps').append(directionsHTML);
                    }
                }
                directionsRenderer.setDirections(directions);
            });
        });

    </script>

   <%--  <script type="text/javascript">
         function initialize() {
             var markers = JSON.parse('<%=ConvertDataTabletoString() %>');
    var mapOptions = {
        center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
        zoom: 5,
        mapTypeId: google.maps.MapTypeId.ROADMAP
        //  marker:true
    };
    var infoWindow = new google.maps.InfoWindow();
    var map = new google.maps.Map(document.getElementById("map_canvasMarkers"), mapOptions);
    for (i = 0; i < markers.length; i++) {
        var data = markers[i]
        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: data.title
        });
        (function (marker, data) {

            // Attaching a click event to the current marker
            google.maps.event.addListener(marker, "click", function (e) {
                infoWindow.setContent(data.description);
                infoWindow.open(map, marker);
            });
        })(marker, data);
    }
}
</script>--%>







    <%--Code For Google Showing Marker...............Added by Neeta.....--%>
    <script type="text/javascript">
        var markers = [
        <asp:Repeater ID="rptMarkers" runat="server">
        <ItemTemplate>
                 {
             
                     "title": '<%# Eval("Address") %>',
                     "lat": '<%# Eval("Latitude") %>',
                     "lng": '<%# Eval("Longitude") %>'
                 }
    </ItemTemplate>
    <SeparatorTemplate>
        ,
    </SeparatorTemplate>
    </asp:Repeater>
        ];
    </script>

    <script type="text/javascript">
        debugger;
        window.onload = function () {
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var infoWindow = new google.maps.InfoWindow();
            var map = new google.maps.Map(document.getElementById("dvGoogleMap"), mapOptions);
            for (i = 0; i < markers.length; i++) {
                var data = markers[i]
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data.title
                });
                (function (marker, data) {
                    google.maps.event.addListener(marker, "click", function (e) {
                        infoWindow.setContent(data.description);
                        infoWindow.open(map, marker);
                    });
                })(marker, data);
            }
        }
    </script>



    <style type="text/css">
        .rsAptDelete
        {
            display: none;
        }

        .RadScheduler .rsHeader .rsHeaderTimeline
        {
            background-position: -228px -31px;
            float: left;
            font-size: 0;
            height: 24px;
            line-height: 21px;
            margin: -26px 0 0 86px !important;
            overflow: hidden;
            text-indent: -9999px;
            width: 21px;
        }

        *, *:before, *:after
        {
            box-sizing: none !important;
        }

        .RadScheduler_Default .rsHeader .rsSelected em, .RadScheduler_Default .rsHeader ul a:hover span
        {
            color: #fff;
            font-size: 11px;
        }

        .RadScheduler .rsMonthView .rsWrap
        {
            /*height: 71px !important;
            width: 155px !important;*/
            margin: 0 0 10px 0 !important;
        }

         .RadScheduler .rsWeekView .rsApt
        {
            /*height: 66px !important;
            width: 160px !important;*/
            width: 100% !important;
            height: 67px !important;
        }

        .RadScheduler .rsMonthView .rsApt
        {
            /*height: 66px !important;
            width: 160px !important;*/
            width: 100% !important;
            height: 69px !important;
        }


          .RadScheduler .rsDayView .rsApt
        {
            /*height: 66px !important;
            width: 160px !important;*/
            width: 50% !important;
            height: 44px !important;
        }
        tr
        {
            height: none !important;
        }

        .RadScheduler, .RadScheduler *
        {
            margin: 0;
            padding: 0;
            box-sizing: initial !important;
            
        }

          .RadScheduler .rsOvertimeArrow {
                display: none !important;
            }
    </style>


    <script type="text/javascript">
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }
       // debugger;
        function Save()
        {
            alert('Saved Successfully');
        }
        function UpdateCustomer()
        {
            alert('Updated Successfully');
        }
        function YetToDeveloped()
        {
            alert('Customer Profile not Develop');
        }
        function EnterLocation()
        {
            alert('Enter location to search');
        }
    </script>




    <%--Getting User Current Location--%>

    <%--<script type="text/javascript">
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            alert("There is Some Problem on your current browser to get Geo Location!");
        }

        function success(position) {
            // var lat = position.coords.latitude;
            //var long = position.coords.longitude;
            //var city = position.coords.locality;
            var lat = 39.9522222;
            var long =-75.1641667;
            var city ="philadelphia pa";
           // var long = position.coords.longitude;
            
            var LatLng = new google.maps.LatLng(lat, long);
            var mapOptions = {
                center: LatLng,
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById("MyMapLOC"), mapOptions);
            var marker = new google.maps.Marker({
                position: LatLng,
                title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: "
                            + lat + +"<br />Longitude: " + long
            });

            marker.setMap(map);
            var getInfoWindow = new google.maps.InfoWindow({
                content: "<b>Your Current Location</b><br/> Latitude:" +
                                        lat + "<br /> Longitude:" + long + ""
            });
            getInfoWindow.open(map, marker);
        }
    </script>--%>

    <%--Getting Route Direction From User Current Location to Destination--%>

    <%--<script type="text/javascript">
        debugger;
        function SearchRoute() {
            document.getElementById("MyMapLOC").style.display = 'none';

            var markers = new Array();
            var myLatLng;

            //Find the current location of the user.  
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (p) {
                    // var Startaddress = document.getElementById("txtStart").value;//Added by Neeta....
                    var myLatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
                    //  var myLatLng = new google.maps.LatLng(Startaddress.coords.latitude, Startaddress.coords.longitude);
                    var m = {};
                    m.title = "JG Office Addresss";
                    m.lat = 39.9522222;
                    m.lng = -75.1641667;
                    // m.lat = p.coords.latitude; //Originally
                    // m.lng = p.coords.longitude;
                    markers.push(m);

                    //Find Destination address location.  
                    var address = document.getElementById("ContentPlaceHolder1_txtDestinationId").value;
                    var geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'address': address }, function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            m = {};
                            m.title = address;
                            m.lat = results[0].geometry.location.lat();
                            m.lng = results[0].geometry.location.lng();
                            markers.push(m);
                            var mapOptions = {
                                center: myLatLng,
                                zoom: 4,
                                mapTypeId: google.maps.MapTypeId.ROADMAP
                            };
                            var map = new google.maps.Map(document.getElementById("MapRoute"), mapOptions);
                            var infoWindow = new google.maps.InfoWindow();
                            var lat_lng = new Array();
                            var latlngbounds = new google.maps.LatLngBounds();

                            for (i = 0; i < markers.length; i++) {
                                var data = markers[i];
                                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                                lat_lng.push(myLatlng);
                                var marker = new google.maps.Marker({
                                    position: myLatlng,
                                    map: map,
                                    title: data.title
                                });
                                latlngbounds.extend(marker.position);
                                (function (marker, data) {
                                    google.maps.event.addListener(marker, "click", function (e) {
                                        infoWindow.setContent(data.title);
                                        infoWindow.open(map, marker);
                                    });
                                })(marker, data);
                            }
                            map.setCenter(latlngbounds.getCenter());
                            map.fitBounds(latlngbounds);

                            //***********ROUTING****************//  

                            //Initialize the Path Array.  
                            var path = new google.maps.MVCArray();

                            //Getting the Direction Service.  
                            var service = new google.maps.DirectionsService();

                            //Set the Path Stroke Color.  
                            var poly = new google.maps.Polyline({ map: map, strokeColor: '#4986E7' });

                            //Loop and Draw Path Route between the Points on MAP.  
                            for (var i = 0; i < lat_lng.length; i++) {
                                if ((i + 1) < lat_lng.length) {
                                    var src = lat_lng[i];
                                    var des = lat_lng[i + 1];
                                    path.push(src);
                                    poly.setPath(path);
                                    service.route({
                                        origin: src,
                                        destination: des,
                                        travelMode: google.maps.DirectionsTravelMode.DRIVING
                                    }, function (result, status) {
                                        if (status == google.maps.DirectionsStatus.OK) {
                                            for (var i = 0, len = result.routes[0].overview_path.length; i < len; i++) {
                                                path.push(result.routes[0].overview_path[i]);
                                            }
                                        } else {
                                            alert("Invalid location.");
                                            window.location.href = window.location.href;
                                        }
                                    });
                                }
                            }
                        } else {
                            alert("Enter correct location to search.");//alert("Request failed.")
                        }
                    });

                });
            }
            else {
                alert('Some Problem in getting Geo Location.');
                return;
            }
        }
    </script>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">

        <ul class="appointment_tab">

            <li><a id="A1" href="home.aspx" runat="server" class="active">Sales Calendar</a> </li>
            <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Calendar</a></li>
            <li><a id="A5" href="#" runat="server">Construction Calendar</a></li>
            <li><a id="A3" href="CallSheet.aspx" runat="server">Call Sheet</a></li>
            <%--<li><a id="A4" href="SrAnnualCalendar.aspx" runat="server">Annual Event Calendar</a></li>--%>
        </ul>
        <h1><b>Dashboard</b></h1>
        <h2>Sales Calendar</h2>
        <div class="calendar" style="margin: 0;" runat="server" id="divCaledar">
            <div id="calendarBodyDiv">
                <%--DayStartTime="E" DayEndTime="E"--%>
                <telerik:RadScheduler ID="rsAppointments" runat="server" DataKeyField="id" DayStartTime="7:00:00" DayEndTime="20:59:59"
                    AllowEdit="false" DataStartField="EventDateTime" DataEndField="EventDateTime" DataSubjectField="CustomerName"
                    ShowHeader="true" Width="100%" Height="100%" TimelineView-NumberOfSlots="0" TimelineView-ShowDateHeaders="false"
                    EnableExactTimeRendering="true" EnableDatePicker="true" SelectedView="WeekView" RowHeight="40px" OnAppointmentDataBound="rsAppointments_AppointmentDataBound"
                    AppointmentContexcalendarBodyDivtMenuSettings-EnableDefault="true" TimelineView-GroupingDirection="Vertical"
                    TimelineView-ReadOnly="true" DisplayDeleteConfirmation="false" OnFormCreated="rsAppointments_FormCreated" OnAppointmentCreated="rsAppointments_AppointmentCreated" 
                    OverflowBehavior="Expand"   CustomAttributeNames="CustomerName,id,ProductOfReturns,Status" OnTimeSlotCreated="rsAppointments_TimeSlotCreated">
                    <%--   OnClientAppointmentClick="OnClientAppointmentClick"  OnClientTimeSlotClick="OnClientTimeSlotClick" OnAppointmentClick="rsAppointments_AppointmentClick" --%>
                   <%-- <AdvancedForm Modal="True">

                        
                    </AdvancedForm>--%>

                     <AdvancedInsertTemplate>
                         <asp:DropDownList ID="ddlE" runat="server">
                                <%--<asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                            <asp:ListItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"></asp:ListItem>
                            <asp:ListItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"></asp:ListItem>
                            <asp:ListItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"></asp:ListItem>
                            <asp:ListItem Text="Ordered(3)" Value="Ordered(3)"></asp:ListItem>




                            <asp:ListItem Text="Follow up" Value="Follow up"></asp:ListItem>
                            <asp:ListItem Text="Estimate Given" Value="Estimate Given"></asp:ListItem>
                            <asp:ListItem Text="Sold-in Progress" Value="Sold-in Progress"></asp:ListItem>
                            <asp:ListItem Text="Sold" Value="Sold"></asp:ListItem>
                            <asp:ListItem Text="Assigned" Value="Assigned"></asp:ListItem>
                            <asp:ListItem Text="Paid Final" Value="Paid Final"></asp:ListItem>
                           <asp:ListItem Text="Received “storage location?”" Value="Received “storage location?”"></asp:ListItem>
                           <asp:ListItem Text="On Standby @ vendor link to vendor profile" Value="On Standby @ vendor link to vendor profile"></asp:ListItem>
                              <asp:ListItem Text="Being delivered to job site" Value="Being delivered to job site"></asp:ListItem>--%>
                         </asp:DropDownList>
                     </AdvancedInsertTemplate>

                     


                   <AppointmentTemplate>
                     <%-- <div class="appointmentHeader">
                    <asp:Panel ID="RecurrencePanel" CssClass="rsAptRecurrence" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="RecurrenceExceptionPanel" CssClass="rsAptRecurrenceException" runat="server"
                        Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="ReminderPanel" CssClass="rsAptReminder" runat="server" Visible="false">
                    </asp:Panel>--%>
                    <asp:LinkButton ID="lbtCustID" runat="server" OnClick="lbtCustID_Click" Text='<%#Eval("id") %>' ForeColor="White"></asp:LinkButton>
                       <%#Eval("CustomerName") %> 
                           
                <%--</div>--%>

                       <%-- <telerik:RadComboBox Runat="server" ID="ddlstatus1" Width="135px" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus1_SelectedIndexChanged">
                            <Items><telerik:RadComboBoxItem Text="Set" Value="Set" /> </Items>
                            <Items><telerik:RadComboBoxItem Text="Prospect" Value="Prospect"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="est>$1000" Value="est>$1000"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="est<$1000" Value="est<$1000"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="sold>$1000" Value="sold>$1000"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="sold<$1000" Value="sold<$1000"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Rehash" Value="Rehash"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Ordered(3)" Value="Ordered(3)"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Follow up" Value="Follow up"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Estimate Given" Value="Estimate Given"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Sold-in Progress" Value="Sold-in Progress"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Sold" Value="Sold"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Assigned" Value="Assigned"/> </Items>
                            <Items><telerik:RadComboBoxItem Text="Paid Final" Value="Paid Final"/> </Items>
                           <Items><telerik:RadComboBoxItem Text="Received “storage location?”" Value="Received “storage location?”"/> </Items>
                           <Items><telerik:RadComboBoxItem Text="On Standby @ vendor link to vendor profile" Value="On Standby @ vendor link to vendor profile"/> </Items>
                              <Items><telerik:RadComboBoxItem Text="Being delivered to job site" Value="Being delivered to job site"/> </Items>
                </telerik:RadComboBox>--%>


                        

                     <asp:DropDownList ID="ddlstatus1" runat="server" Width="135px" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus1_SelectedIndexChanged" > <%--DataTextField='<%#Eval("Status") %>' DataValueField='<%#Eval("Status") %>'--%>
                         <%--<asp:ListItem Text="Select" Value="Select"></asp:ListItem> --%>
                         <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                            <asp:ListItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"></asp:ListItem>
                            <asp:ListItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"></asp:ListItem>
                            <asp:ListItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"></asp:ListItem>
                            <asp:ListItem Text="Ordered(3)" Value="Ordered(3)"></asp:ListItem>




                            <asp:ListItem Text="Follow up" Value="Follow up"></asp:ListItem>
                            <asp:ListItem Text="Estimate Given" Value="Estimate Given"></asp:ListItem>
                            <asp:ListItem Text="Sold-in Progress" Value="Sold-in Progress"></asp:ListItem>
                            <asp:ListItem Text="Sold" Value="Sold"></asp:ListItem>
                            <asp:ListItem Text="Assigned" Value="Assigned"></asp:ListItem>
                            <asp:ListItem Text="Paid Final" Value="Paid Final"></asp:ListItem>
                           <asp:ListItem Text="Received “storage location?”" Value="Received “storage location?”"></asp:ListItem>
                           <asp:ListItem Text="On Standby @ vendor link to vendor profile" Value="On Standby @ vendor link to vendor profile"></asp:ListItem>
                              <asp:ListItem Text="Being delivered to job site" Value="Being delivered to job site"></asp:ListItem>
                          
                        </asp:DropDownList>                        
                        <%--<asp:Label runat="server" ID="lblID" Text='<%#Eval("id") %>'></asp:Label>--%>

                       <asp:Label ID="lblProductOfReturn" runat="server"  Text='<%#Eval("ProductOfReturns") %>' ForeColor="White"></asp:Label>
                   
                  </AppointmentTemplate>
                    <TimelineView UserSelectable="false"></TimelineView>
                    <TimeSlotContextMenuSettings EnableDefault="true"></TimeSlotContextMenuSettings>
                    <AppointmentContextMenuSettings EnableDefault="true"></AppointmentContextMenuSettings>

                </telerik:RadScheduler>


                <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Title="No Appointment available"
                    Behaviors="Close">
                </telerik:RadWindow>


                <%-- <iframe src="/calendar/calendar.aspx" width="100%" height="800" style="border: 0;">
            </iframe>--%>
            </div>
        </div>



        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
            <Windows>
                <telerik:RadWindow ID="RadWindow2" runat="server" ShowContentDuringLoad="false" Width="400px"
                    Height="400px" Title="Sales Calendar" Behaviors="Default">
                    <ContentTemplate>
                        <br />
                        <br />
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                      <asp:Label ID="Label1" runat="server" Text="Customer Id : "></asp:Label>
                        <asp:LinkButton ID="lbtCustomerID" runat="server" OnClick="lbtCustomerID_Click"></asp:LinkButton>
                        <%--Text='<%#Eval("id")%>'--%>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Customer Name : "></asp:Label>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Phone Number : "></asp:Label>
                        <asp:Label ID="lblPhone" runat="server"></asp:Label><br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                     <asp:Label ID="Label4" runat="server" Text="ZIP : "></asp:Label>
                        <asp:Label ID="lblZip" runat="server"></asp:Label>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label5" runat="server" Text="City : "></asp:Label>
                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                     <asp:Label ID="Label6" runat="server" Text="Address : "></asp:Label>
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label7" runat="server" Text="Product Line : "></asp:Label>
                        <asp:Label ID="lblProductLine" runat="server"></asp:Label>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
                            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>
                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
                            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                            <asp:ListItem Text="Cancelation-no rehash" Value="Cancelation-no rehash"></asp:ListItem>
                            <asp:ListItem Text="Material Confirmation(1)" Value="Material Confirmation(1)"></asp:ListItem>
                            <asp:ListItem Text="Procurring Quotes(2)" Value="Procurring Quotes(2)"></asp:ListItem>
                            <asp:ListItem Text="Ordered(3)" Value="Ordered(3)"></asp:ListItem>

                        </asp:DropDownList>

                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Height="30px" Width="75px" />
                        &nbsp; &nbsp;
                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Height="30px" Width="75px" />

                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

       <%-- <table>--%>
            <%--style="border: solid 15px blue; width: 100%; vertical-align: central;"--%>
            <%--<tr>  
            <td style="padding-left: 20px; padding-top: 20px; padding-bottom: 20px; background-color: skyblue;  
                text-align: center; font-family: Verdana; font-size: 20pt; color: Green;">  
                Draw Route Between User's Current Location & Destination On Google Map  
            </td>  
        </tr>--%>
            <%--<tr>
                <td style="font-family: Verdana; font-size: 14pt; color: red;">--%><%--background-color: skyblue;--%>
                    <%--<br />
                    <br />
                    <b>A:</b>
                    <input type="text" id="txtStart" value="JG Office Addresss" style="width: 200px;height:30px;" runat="server" />
                    <b>B:</b>
                    <input type="text" id="txtDestinationId" name="txtDestination" style="width: 200px;height:30px;" runat="server" />
                    <div style="margin-top: -34px; margin-left: 500px; height:10px" class="btn_sec">             
                    <input type="button" value="Get Direction" onclick="SearchRoute()"/>  
                    </div>--%>
                    <%--<div style="margin-top: -19px; margin-left: 600px">
                        
                       <input type="button" value="Get Direction" onclick="SearchRoute()" />
                    </div>--%>
                    <%----%>
                
                <%--</td>--%>
               <%-- <td style="margin-top: -19px; margin-left: 600px">
                    <asp:Button ID="btnGetDirection" runat="server" OnClientClick="SearchRoute()" Text="Get Direction" BackColor="#327FB5" ForeColor="White" Height="32px"
                            Style="height: 26px; font-weight: 700; line-height: 1em;" Width="100px"/>
                       <asp:Button  ID="btnAAA" runat="server" Text="Get"/>
                </td>--%>
              
            <%--</tr>
            <tr>
                <td>
                    <div style="margin-left: 1px; margin-top: 1px;">--%><%--style="margin-left: 1px; margin-top: 1px;"--%>
                        <%--margin-top:-340px;"--%>
                       <%-- <div id="dvGoogleMap" style="width: 300px; height: 300px; margin-top:10px">
                        </div> --%>
                         <%--<div id="MyMapLOC" style="width: 300px; height: 300px">
                         </div>
                         <div id="MapRoute" style="width: 300px; height: 300px; margin-top:10px; margin-left:10px">
                         </div>--%>
                        <%--<br />--%>
                   
                    <%--</div>
                    <br />
                    <br />
                </td>
                <td>
                    
            </td>
            </tr>
        </table>--%>

        <table style="border: thin solid #000000; font-family: Verdana; font-size: small;"
        width="100%">
            <tr>
                <td style="font-family: Verdana; font-size: 14pt; color: red;">
                    <b>A:</b><asp:TextBox ID="txtFromLocation" ForeColor="Black" runat="server" ReadOnly="true" TextMode="MultiLine" Width="184px"></asp:TextBox>
                    <br />
                    <br />
                    <b>B:</b><asp:TextBox ID="txtToLocation" ForeColor="Black" runat="server" TextMode="MultiLine" Width="183px"></asp:TextBox>
                    <br />
                    <div class="btn_sec">
                        <asp:Button ID="btnGetDirection" runat="server" Text="Get Direction" OnClick="btnsubmit_Click" ValidationGroup="Login" TabIndex="3" Width="200px" Height="40px" />
                        <%--<asp:Button ID="Button1" runat="server" Text="Get Direction" OnClick="btnsubmit_Click" ValidationGroup="Login" TabIndex="3" Width="200px" Height="40px" />--%>
                    </div>
                </td>
            </tr>
        <tr>
            <td valign="top" width="40%">
                <div id="direction_steps_holder" style="width: 100%">
                    <div>
                        <b>Directions to <asp:Label ID="lblToDirection" runat="server" Text=""></asp:Label> (<span id="total_distance"></span> - about <span
                            id="total_duration"></span>) </b>
                    </div>
                    <div id="direction_steps" style="height: 400px;overflow-y: scroll;overflow-x:hidden;">
                    </div>
                </div>
            </td>
            <td valign="top">
                <div id="map_canvas" style="width: 608px; height: 700px;margin-top:-28%;">
                </div>
            </td>
        </tr>
            <tr>
                <td>
                    <b>Today's all appointments</b>
                </td>
            </tr>
            <tr>
             <td colspan="2" >
                <div id="dvGoogleMap" style="width: 100%; height: 400px"></div>
            </td>
        </tr>

    </table>




    </div>
</asp:Content>
