<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.aspx.cs" Inherits="JG_Prospect.GoogleMap" %>

<!DOCTYPE html>
<%--Js Added for Google Map and marker--%>
    
<html xmlns="http://www.w3.org/1999/xhtml">
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">--%>
<head id="Head1" runat="server">  
    <title>Search Route Direction</title>  
  
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>  
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false"> </script>      
  
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

            //Find the current location of the user.  
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (p) {
                   // var Startaddress = document.getElementById("txtStart").value;//Added by Neeta....
                     var myLatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
                //  var myLatLng = new google.maps.LatLng(Startaddress.coords.latitude, Startaddress.coords.longitude);
                    var m = {};
                    m.title = "Swarget, pune";
                    m.lat = p.coords.latitude;
                    m.lng = p.coords.longitude;
                    markers.push(m);

                    //Find Destination address location.  
                    var address = document.getElementById("txtDestination").value;
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
                            alert("Request failed.")
                        }
                    });

                });
            }
            else {
                alert('Some Problem in getting Geo Location.');
                return;
            }
        }
    </script>  
  
</head> 
    <%--</asp:Content>--%>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> --%>
<body>  
    <form id="form1" runat="server">  
    <table >  <%--style="border: solid 15px blue; width: 100%; vertical-align: central;"--%>
        <%--<tr>  
            <td style="padding-left: 20px; padding-top: 20px; padding-bottom: 20px; background-color: skyblue;  
                text-align: center; font-family: Verdana; font-size: 20pt; color: Green;">  
                Draw Route Between User's Current Location & Destination On Google Map  
            </td>  
        </tr>--%>  
        <tr>  
            <td style="background-color: skyblue; text-align: center; font-family: Verdana; font-size: 14pt;  
                color: red;">  
                <b>Enter Destination:</b> 
                <input type="text" id="txtStart" value="Swarget,Pune" style="width: 200px" runat="server"/>   
                <input type="text" id="txtDestination"  style="width: 200px" runat="server"/>  
                <input type="button" value="Search Route" onclick="SearchRoute()" />  
            </td>  
        </tr>  
        <tr>  
            <td>  
                <div id="MyMapLOC" style="width: 550px; height: 470px">  
                </div>  
                <div id="MapRoute" style="width: 500px; height: 500px">  
                </div>  
            </td>  
        </tr> 
        </table> 








<%--For Workorder--%>

        <table width="100%" cellspacing="0" cellpadding="0" border="0" style="font-family: arial,helvetica,sans-serif; font-size: 12px;">        
<tbody>
<tr>            
<td align="center">                &nbsp;             </td>        </tr>        
<tr>            
<td align="center">                <strong>INSTALLER WORK ORDER</strong>            </td>        </tr>        
<tr>            
<td>                
<table width="100%" cellspacing="0" cellpadding="0" border="0" style="font-size: 10px;">                    
<tbody>
<tr>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                        
<td width="6%" valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            <strong>Salesman:</strong>                        </td>                        
<td valign="top" colspan="2">&nbsp;lblSalesman &nbsp;</td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            <strong>Date Sold:</strong>                        </td>                        
<td valign="top" colspan="2">&nbsp;lbldatesold</td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            <strong>Cell #:</strong>                        </td>                        
<td valign="top" colspan="2">&nbsp;lblSalesmanCell</td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            &nbsp;                         </td>                        
<td valign="top" colspan="2">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="13">                            Inspect all material and job site for estimate accuracy and material matches work                             order, Must call the customer if running late or can't make it at all, you CAN leave                             a message.(Rain or Shine)                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong><u>Installer Info</u></strong>                        </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong><u>Customer Info</u></strong>                        </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Installer:</strong> lblInstaller<label /></td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Name:</strong> lblCustomerinfo<label /></td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Phone #: </strong>lblInstallerPh<label /></td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Job Location:</strong> lblJobLocation<label /></td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Work Scheduled Date: </strong>lblworkschd_date<label /></td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Phone #s: </strong>lblCustomerPh<label /></td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            <strong>Disposal:</strong> lbldisposal</td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="center" colspan="6">                            <strong><u>Special Instructions:</u></strong>                        </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="center" colspan="6">                            <strong><u>Work Area:</u></strong>                        </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6" style="border-color: #000000;">                            
<table width="100%" cellspacing="0" cellpadding="0" border="0">                                
<tbody>
<tr>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                </tr>                                
<tr>                                    
<td>                                        &nbsp;                                     </td>                                    
<td colspan="10">                                        lblSpclInstructions</td>                                    
<td>                                        &nbsp;                                     </td>                                </tr>                                
<tr>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                </tr>                            </tbody></table>                        </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6" style="border-color: #000000;">                            
<table width="100%" cellspacing="0" cellpadding="0" border="0">                                
<tbody>
<tr>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                    
<td width="8%">                                        &nbsp;                                     </td>                                </tr>                                
<tr>                                    
<td>                                        &nbsp;                                     </td>                                    
<td colspan="10">                                        lblworkArea</td>                                    
<td>                                        &nbsp;                                     </td>                                </tr>                                
<tr>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                    
<td>                                        &nbsp;                                     </td>                                </tr>                            </tbody></table>                        </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="right" colspan="6">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>              
<td valign="top" colspan="13" align="center">                           
<h3>Materials List</h3></td> </tr>                                   
<tr>  
<td colspan="12">
<table>
<tbody>
<tr>           
<td valign="top">lblproductname   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;</td>  
<td valign="top">lblQuantity &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;</td> 
<td valign="top">lblColor &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;</td>   
<td valign="top">lblStyle &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;</td>  
<td valign="top">lblLength &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;</td>  
<td valign="top">lblWidth &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;   &nbsp;</td>           </tr></tbody></table></td>     </tr>                         
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="6">                            &nbsp;                         </td>                                                
<td valign="top" colspan="6">                            &nbsp;                         </td>                                                
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="13">                            Any additional work needed that is not listed on work order you must call salesman                             and homeowners approval to complete job. Ex rotted wood, metal etc. Poor workmanship                             or covering up shoddy work will not be tolerated!!! All excess material scrap aluminum,                             steel, &amp; copper is JMG property and must be returned. It is considered theft                             if not returned!                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="4">                            Reschedule Date 1: lblRescheduleDate1</td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" colspan="4">                            Reschedule Date 2:&nbsp;lblRescheduleDate2</td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="center">                            1.                         </td>                        
<td valign="top" colspan="11">                            Is job done? Y - N                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="center">                            2.                         </td>                        
<td valign="top" colspan="11">                            Leave sign: Y - N                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="center">                            3.                         </td>                        
<td valign="top" colspan="11">                            Does customer need an EST? Y - N                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top" align="center">                            4.                         </td>                        
<td valign="top" colspan="11">                            Walk around / Phone Call &amp; Collection and any additional add on(s)? Y - N                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                    </tr>                    
<tr>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>                        
<td valign="top">                            &nbsp;                         </td>     

  <%--For Workorder--%>
    </form>  
</body>  
</html>
 <%--</asp:Content>--%>