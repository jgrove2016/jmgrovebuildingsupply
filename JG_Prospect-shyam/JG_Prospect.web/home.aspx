<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="home.aspx.cs" Inherits="JG_Prospect.home" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Src="~/Controls/left.ascx" TagName="leftmenu" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp" type="text/javascript"></script>
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
     <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>--%>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">  
    </script> 
    
    <%--Code For Google Showing Marker...............Added by Neeta.....--%>
  <script type="text/javascript">
      var markers = [
      <asp:Repeater ID="rptMarkers" runat="server">
      <ItemTemplate>
               {
             
                   "title": '<%# Eval("Title") %>',
                 "lat": '<%# Eval("Latitude") %>',
                 "lng": '<%# Eval("Longitude") %>',
                 "description": '<%# Eval("Address") %>'
             }
    </ItemTemplate>
    <SeparatorTemplate>
        ,
    </SeparatorTemplate>
    </asp:Repeater>
    ];
    </script>

    <script type="text/javascript">

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

        .RadScheduler .rsMonthView .rsApt
        {
            /*height: 66px !important;
            width: 160px !important;*/
            width: 100% !important;
            height:69px !important;
        }
          .RadScheduler .rsWeekView .rsApt
        {
            /*height: 66px !important;
            width: 160px !important;*/
            width: 100% !important;
            height:67px !important;
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
         debugger;
         function Save()
         {
             alert('Saved Successfully');
         }
         function YetToDeveloped()
         {
             alert('Customer Profile not Develop');
         }
</script>



      
    <%--Getting User Current Location--%>  
  
    <script type="text/javascript">
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            alert("There is Some Problem on your current browser to get Geo Location!");
        }

        function success(position) {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
            var city = position.coords.locality;
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
    </script>  
  
    <%--Getting Route Direction From User Current Location to Destination--%>  
  
    <script type="text/javascript">
        debugger;
        function SearchRoute() {
            document.getElementById("MyMapLOC").style.display = 'none';

            var markers = new Array();
            var myLatLng;
            var a=1;
            //Find the current location of the user.  
           // if (navigator.geolocation) {
            if (a==1) {
               // navigator.geolocation.getCurrentPosition(function (p) {
                      if (a==1){
                    // var Startaddress = document.getElementById("txtStart").value;//Added by Neeta....
                   // var myLatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
                    // var myLatLng = new google.maps.LatLng(Startaddress.coords.latitude, Startaddress.coords.longitude);
                    //  var myLatLng;
                    var m = {};
                    m.title = "Swarget, pune";
                    m.lat = "18.5018322";
                    m.lng = "73.8635912";
                   // var m = {};
                   // m.title = "Swarget, pune";                   
                   // m.lat = p.coords.latitude;
                   // m.lng = p.coords.longitude;
                    markers.push(m);

                          //Find Destination address location.  
                          //alert(document.getElementById('txtDestinationId').value);
                  //  alert($('#txtDestinationId').val());
                   // var address = document.getElementById("txtDestinationId").value;
                    var address="Satara";
                    if(address == null)
                    {
                        var address="Satara";
                    }
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
                }
               // });
            }
            else {
                alert('Some Problem in getting Geo Location.');
                return;
            }
        }
    </script> 
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <div class="left_panel arrowlistmenu" style="height: 400px">
        <uc1:leftmenu ID="left1" runat="server" />
    </div>--%>
    <div class="right_panel">
      
            <ul class="appointment_tab">
                
                <li><a id="A1" href="home.aspx" runat="server" class="active">sales Calendar</a> </li>
                <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master  Calendar</a></li>
                <li><a id="A5" href="#" runat="server">Construction Calendar</a></li>
                <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
                <%--<li><a id="A4" href="JrAnnualCalendar.aspx" runat="server">Annual Event Calendar</a> </li>--%>
            </ul>
        <h1><b>Dashboard</b></h1>
        <h2>
            Sales Calendar</h2>
        <div class="calendar" style="margin: 0;">
            <div id="calendarBodyDiv">
                    <telerik:RadScheduler ID="rsAppointments" runat="server" DataKeyField="id" DayStartTime="7:00:00" DayEndTime="20:59:59"
                    AllowEdit="false" DataStartField="EventDateTime" DataEndField="EventDateTime" DataSubjectField="CustomerName"
                    ShowHeader="true" Width="100%" Height="100%" TimelineView-NumberOfSlots="0" TimelineView-ShowDateHeaders="false"
                    EnableExactTimeRendering="true" EnableDatePicker="true" SelectedView="WeekView" RowHeight="40px"
                    AppointmentContexcalendarBodyDivtMenuSettings-EnableDefault="true" TimelineView-GroupingDirection="Vertical"
                    TimelineView-ReadOnly="true" DisplayDeleteConfirmation="false" OnFormCreated="rsAppointments_FormCreated" OnAppointmentCreated="rsAppointments_AppointmentCreated" 
                    OverflowBehavior="Expand" OnAppointmentDataBound="rsAppointments_AppointmentDataBound"  CustomAttributeNames="CustomerName,id,ProductOfReturns" OnTimeSlotCreated="rsAppointments_TimeSlotCreated">
                    <%--   OnClientAppointmentClick="OnClientAppointmentClick"  OnClientTimeSlotClick="OnClientTimeSlotClick" OnAppointmentClick="rsAppointments_AppointmentClick" --%>
                    <AdvancedForm Modal="True">

                        
                    </AdvancedForm>

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




                        

                     <asp:DropDownList ID="ddlstatus1" runat="server" Width="135px" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus1_SelectedIndexChanged">
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
                    <br /><br /><br />
              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                      <asp:Label ID="Label1" runat="server" Text="Customer Id : "></asp:Label> 
                <asp:LinkButton ID="lbtCustomerID" runat="server" OnClick="lbtCustomerID_Click" ></asp:LinkButton> <%--Text='<%#Eval("id")%>'--%>
                      <br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Customer Name : "></asp:Label> 
                    <asp:Label ID="lblName" runat="server"></asp:Label> <br />
                     &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Phone Number : "></asp:Label> 
                     <asp:Label ID="lblPhone" runat="server"></asp:Label><br />
                   &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                     <asp:Label ID="Label4" runat="server" Text="ZIP : "></asp:Label>                      
                    <asp:Label ID="lblZip" runat="server"></asp:Label> <br />
                     &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label5" runat="server" Text="City : "></asp:Label>                   
                     <asp:Label ID="lblCity" runat="server"></asp:Label> <br />
                   &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                     <asp:Label ID="Label6" runat="server" Text="Address : "></asp:Label> 
                     <asp:Label ID="lblAddress" runat="server"></asp:Label> <br />
                      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label7" runat="server" Text="Product Line : "></asp:Label> 
                    <asp:Label ID="lblProductLine" runat="server"></asp:Label> <br />                  
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

                    <br /><br /><br /><br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button  ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;"  Height="30px" Width="75px"/> &nbsp; &nbsp;
                    <asp:Button  ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;"  Height="30px" Width="75px"/>

                     </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

        <table >  <%--style="border: solid 15px blue; width: 100%; vertical-align: central;"--%>
        <%--<tr>  
            <td style="padding-left: 20px; padding-top: 20px; padding-bottom: 20px; background-color: skyblue;  
                text-align: center; font-family: Verdana; font-size: 20pt; color: Green;">  
                Draw Route Between User's Current Location & Destination On Google Map  
            </td>  
        </tr>--%>  
        <tr>  
            <td style=" text-align: center; font-family: Verdana; font-size: 14pt;  
                color: red;"> <%--background-color: skyblue;--%>
                <br /><br />  
                <b>A:</b> 
                <input type="text" id="txtStart" value="Swarget,Pune" style="width: 200px" runat="server"/> 
                <b>B:</b> 
                <input type="text" id="txtDestinationId"  name="txtDestination" style="width: 200px" runat="server" /> 
                    <%----%>
               <div style="margin-top: -34px; margin-left: 730px" class="btn_sec">             
                    <input type="button" value="Get Direction" onclick="SearchRoute()" />  
                    </div>

            </td>  
        </tr>  
        <tr>  
            <td>   &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <div style="margin-left:50px;">
                <div id="MyMapLOC" style="width: 300px; height: 300px">  
                </div> <br /><br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                <div id="MapRoute" style="width: 300px; height: 300px">  
                </div> 
                     </div>
                
         <br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <div style="margin-left:400px; margin-top:-340px;">
                <div id="dvGoogleMap"  style=" width: 300px; height: 300px" ><%--margin-left: 140px; margin-bottom:100px; margin-top:100px;--%>
                </div>
        </div><br /><br />
            </td>  
        </tr> 
        </table>



</asp:Content>
