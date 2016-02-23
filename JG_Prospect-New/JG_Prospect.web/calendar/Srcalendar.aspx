<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Srcalendar.aspx.cs" Inherits="JG_Prospect.calendar.Srcalendar" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        /* optional styling */
        body
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        div table
        {
            background: #fff;
            border: #ccc 5px solid;
        }
        div table table, div div table
        {
            border: none;
        }
        
        #loginControlDiv
        {
            font-size: 11px;
        }
        #navControlDiv
        {
            font-style: "Verdana";
            font-size: 12px;
        }
        #navControlDiv input
        {
            background-color: #A33E3F;
            border: solid #DDD270 1px;
            font-style: "Verdana";
            font-size: 12px;
            color: #fff;
        }
        #navControlDiv select
        {
            border: solid black 1px;
            font-style: "Verdana";
            font-size: 12px;
        }
        #viewControlDiv input
        {
            background-color: #A33E3F;
            border: solid #DDD270 1px;
            font-style: "Verdana";
            font-size: 12px;
            color: #fff;
        }
        #eventDisplayDiv
        {
            padding: 0px;
            color: black;
            background-color: white;
            font-family: "Verdana";
            font-size: 12px;
            text-align: left;
        }
        #statusControlDiv
        {
            font-size: 14px;
            color: #ccc;
            margin: 10px;
        }
        /* required styling */
        
        
        
        .columnHeading
        {
            text-align: center;
            font-size: 12px;
            color: #fff;
            background-color: #A33E3F;
            border: solid #DDD270 1px;
            padding: 10px 0;
            font-family: Tahoma;
        }
        .weekViewCell
        {
            width: 139px;
            height: 370px;
            border: solid #A33E3F 1px;
            text-align: left;
            overflow: auto;
        }
        .monthViewCell
        {
            width: 139px;
            height: 140px;
            border: solid #A33E3F 1px;
            text-align: left;
            overflow: auto;
        }
        .contentCell
        {
            font-size: 12px;
            border: solid black 0px;
            color: gray;
            font-family: Arial, Helvetica, sans-serif;
            overflow: auto;
        }
        .contentCell div
        {
            margin-bottom: 4px;
            padding: 5px 0;
        }
        .eventMouseOver
        {
            color: white;
            background-color: #A33E3F;
        }
        .eventMouseOut
        {
            color: #555;
            background-color: #ccc;
        }
        #dwindow
        {
            border: #A33E3F 2px solid !important;
        }
        #doverlay
        {
            position: fixed !important;
        }
        .weather
        {
            float: left;
            list-style-type: none;
            margin: 0 0 0 20px;
            padding: 0;
        }
        .weather li
        {
            float: left;
            margin: 0 6px 0 0;
            padding: 0;
            width: 139px;
            background: #ccc;
            border-radius: 5px 5px 0 0;
            line-height: 24px;
            font-size: 12px;
        }
        #Panel1{ margin:10px 0 20px 50px; float:left;}
        #Panel1 ul.weather_list{ list-style-type:none; float:left; font-family:Arial; font-size:10px; margin:0 20px; padding:0;}
        #Panel1 ul.weather_list li > span{ display:inline-block; padding:0 5px;}
        #Panel1 ul.weather_list li > span.date,  #Panel1 ul.weather_list li > span.image{ display:block; padding:0;}
        #Panel1 ul.weather_list li > span.desc{ display:none;}
        
    </style>
    <script type="text/javascript" src="http://maps.google.com/maps?file=api&amp;v=2.x&amp;key=ABQIAAAAVcJQrx7VsumiP2heFwp6URQLaiSrhXTkLq3mA9rOmHpVsHwBjxTg7C5-XXHl634dCROpHwKMO9BzmQ"></script>
    <script type="text/javascript" src="calvis.js"></script>
    <script type="text/javascript" src="Dialog.js"></script>





     <script type="text/javascript" src="../js/jquery.ui.core.js"></script>
       <script type="text/javascript" src="../js/jquery-latest.js" ></script>
    <script type="text/javascript" language="javascript">



        window.onload = function () {

            calvis.ready(main);

        }



        function main() {

            var calId = document.getElementById('clid').value;        
            var calendar = new calvis.Calendar();
            // set the CSS IDs for various visual components for the calendar container

            calendar.setCalendarBody('calendarBodyDiv');

            calendar.setNavControl('navControlDiv');

            calendar.setViewControl('viewControlDiv');

            calendar.setStatusControl('statusControlDiv');

            calendar.setEventCallback('click', displayEvent);

           

            // set the calenar to pull data from this Google Calendar account

            calendar.setPublicCalendar(calId);


            calendar.setDefaultView('week');

            // display the calendar

            calendar.render();



            // global lightbox dialog to display event details

            eventWindow = new Widget.Dialog;


        }



        function displayEvent(event) {

            var title = event.getTitle().getText();
            var date = event.getTimes()[0].getStartTime().getDate();

            var content = event.getContent().getText();

            var linkurl = document.getElementById('hd_url').value;
            var location_ = event.getLocations()[0].getValueString().split('@')[0];

            var eventHtml = [];

            eventHtml.push('<b>Title: </b>');

            eventHtml.push(title);

            eventHtml.push('<br>');

            eventHtml.push('<br>');

            eventHtml.push('<b>When </b>');

            eventHtml.push(date.toString());

            eventHtml.push('<br>');

            eventHtml.push('<br>');

            eventHtml.push('<b>Description:</b>');

            eventHtml.push('<p style="font-size: 12px;">');

            eventHtml.push(content);

            eventHtml.push('</p>');

            eventHtml.push('<b>Where: </b>');

            eventHtml.push(location_);

            eventHtml.push('<br><br>');

            eventHtml.push('<div align="center">');

            eventHtml.push('<table>');

            eventHtml.push('<tr>');

            eventHtml.push('<td>');         
               
                eventHtml.push('<div id="edit1"><a id="lnkedit1" href="/Sr_App/Customer_Profile.aspx?title=' + title + '" target="_parent" runat="server">Edit Customer</a></div>');
                eventHtml.push('<div id="estimate" style="text-align:right" runat="server" ><a id="lnkestimate" href="/Sr_App/ProductEstimate.aspx?title=' + title + '" target="_parent" runat="server">Create Estimate</a></div>'); //for sr app
           
            eventHtml.push('</td></tr><tr><td>Direction Details</td></tr>');


            eventHtml.push('<tr><td>');
            eventHtml.push('<div style="border: 1px solid #A33E3F; width: 550px; height: 550px" id="map" >');

            //eventHtml.push('<div id="video"></div>');

            eventHtml.push('</td>');

            eventHtml.push('</tr>');
            eventHtml.push('<tr><td style="text-align:right"><input type="button" id="buttonprint1" value="Print Map" /></td></tr>');
            eventHtml.push('</div>');
            eventHtml.push('<tr><td>Customer House Close up</td></tr>');
            eventHtml.push('<tr>');
            eventHtml.push('<td>');
            eventHtml.push('<div style="border: 1px solid #A33E3F; width: 550px; height: 550px" id="map1">');

            //eventHtml.push('<div id="video"></div>');

            eventHtml.push('</td>');

            eventHtml.push('</tr>');
            eventHtml.push('</div>');

            // after this the eventHtml is in the DOM  tree
            eventHtml.push('<tr><td style="text-align:right"><input type="button" id="buttonprint" value="Print Map" /></td></tr>');


            eventHtml.push('</table>');
            eventWindow.alert(eventHtml.join(''));

            displayMap1(location_);

            displayMap(location_);

            // displayVideo(title);
        }


        function displayMap1(location_) {



            var geocoder = new GClientGeocoder();

            geocoder.getLatLng(

    location_,

    function (point) {

        if (point) {

            var map = new GMap2(document.getElementById("map1"));

            map.addControl(new GSmallMapControl());

            map.setCenter(point, 13);

            var marker = new GMarker(point);

            map.addOverlay(marker);

        } else {

            document.getElementById('map1').parentNode.removeChild(document.getElementById('map1'));

        }

    }

  );

        }

        function displayMap(location_) {



            var geocoder = new GClientGeocoder();

            geocoder.getLatLng(

    location_,

    function (point) {

        if (point) {

            var map = new GMap2(document.getElementById("map"));
            var directionsPanel;
            var directions;

            directionsPanel = document.getElementById("my_textual_div");
            directions = new GDirections(map, directionsPanel);
            var toaddress = location_;
            directions.load("from: 220 krams Ave Manayunk, PA 19127 to: " + toaddress + "");

            map.addControl(new GSmallMapControl());

            map.setCenter(point, 13);

            var marker = new GMarker(point);

            map.addOverlay(marker);

        } else {

            document.getElementById('map').parentNode.removeChild(document.getElementById('map'));

        }

    }

  );

        }



        function getGeocode(json) {

            var mapData = eval(json);

            console.log(mapData);



            var status = mapData.Status.code;

            console.log(status);

            if (status == '200') {

                var lng = mapData.Placemark[0].Point.coordinates[0];

                var lat = mapData.Placemark[0].Point.coordinates[1];

                console.log(lat + ' ' + lng);



                var point = new GLatLng(lat, lng);

                map.setCenter(point, 13);

                map.addOverlay(new GMarker(point));





            }

        }



        function displayVideo(title) {



            var keywords = title.replace(/ /g, '+');



            var callbackName = 'processYouTubeFeed';



            var youtubeFeed =

    ['http://gdata.youtube.com/feeds/api/videos?callback=',

    callbackName, '&alt=json-in-script&vq=',

    keywords].join('');



            var script = document.createElement('script');

            script.src = youtubeFeed;



            document.body.appendChild(script);

        }



        //    function processYouTubeFeed(feed) {



        //        var data = eval(feed);



        //        if (data.feed.openSearch$totalResults.$t > 0) {



        //            var url = data.feed.entry[0].link[0].href;

        //            var id = url.replace('http://www.youtube.com/watch?v=', '');

        //            var videoUrl = 'http://www.youtube.com/v/' + id;



        //            var html = [];



        //            html.push('<object width="250" height="200">');

        //            html.push('<param name="movie" value="');

        //            html.push(videoUrl);

        //            html.push('"></param>');

        //            html.push('<param name="wmode" value="transparent"></param>');

        //            html.push('<embed src="');

        //            html.push(videoUrl);

        //            html.push('" type="application/x-shockwave-flash"');

        //            html.push(' wmode="transparent" width="250" height="200">');

        //            html.push('</embed></object>');



        //            document.getElementById('video').innerHTML = html.join('');

        //        }

        //    }

        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

    </script>
     <script type="text/javascript" src="../js/jquery-latest.js"></script>
      <script type="text/javascript" src="../js/jquery.printElement.min.js"></script>

      <script type="text/javascript">
          $(document).ready(function () {
              //alert('hello');
              $("#buttonprint").live("click", function () {
                  //alert('hello');
                  $('#map').css({
                      width: '600px',
                      height: '600px'
                  });
                  printElem({});
                  $('#map').css({
                      width: '550px',
                      height: '550px'
                  });

              });
              function printElem(options) {
                  $('#map').printElement(options);
              }


          });
          $(document).ready(function () {
              //alert('hello');
              $("#buttonprint1").live("click", function () {
                  //alert('hello');
                  $('#map1').css({
                      width: '600px',
                      height: '600px'
                  });
                  printElem({});
                  $('#map1').css({
                      width: '550px',
                      height: '550px'
                  });

              });
              function printElem(options) {
                  $('#map1').printElement(options);
              }


          });
      </script>
    

</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; top: 0px; left: 0px;" id="statusControlDiv">
    </div>
    <div style="position: absolute; top: 0px; right: 5px;" id="loginControlDiv">
    </div>
    <div align="center">
        <img src='dot.gif' style='position: absolute; top: -1000px;' alt="">      
        <!--weather section start-->
        <div>
     <!-- <table cellpadding="1" cellspacing="2" width="860px" align="center" border="1" style="text-align:center; font-family:Verdana; font-size:10px;">
           <tr>
               <td colspan="6">
               <h2>  <asp:Label ID="Labellocation" runat="server" Text=""></asp:Label><br /><br /></h2> 
             </td></tr>

               <tr style="font-weight:bold; background-color:#DBE2E8"><td style="width:90px;"></td><td style="width:90px;">Date</td><td>Max.Temp.[<sup>0</sup>F]</td><td>Min.Temp.[<sup>0</sup>F]</td><td><label>Weather Desc.</label></td><%--<td><label>Weather Code </label></td>--%><td><label>Precipitation MM </label></td></tr>
            <tr>
               <td colspan="6">
                                 
                
                </td>
            </tr>
        </table>-->
        <asp:Panel ID="Panel1" runat="server">
                  </asp:Panel>  
                  <div class="clr"></div>
        </div>
        <!--weather section end--><br />
        <table>
            <tr style="text-align: center;" align="left">
                <td valign="top">
                    <span style="float: left;" id="navControlDiv"></span><span style="float: right;"
                        id="viewControlDiv"></span>
                </td>
            </tr>          
            <tr align="center">
                <td valign="top">
                    <div id="calendarBodyDiv">
                    </div>
                </td>
            </tr>
        </table>
    </div>
      <asp:HiddenField ID="clid" runat="server" Value="" />
       <asp:HiddenField ID="caltype" runat="server" Value="" />
        <asp:HiddenField ID="hd_url" runat="server" Value="" />
        
        <asp:HiddenField ID="hd_title" runat="server" Value="" />
    </form>
</body>
</html>
