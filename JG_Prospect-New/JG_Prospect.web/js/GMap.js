// JScript File

function CreateMarker(record)
 {
    //alert(record);
    var information = new Array();
    information = record.split(',');
    var imageurl = information[0].toString();
    var description =   information[1].toString();
    var imageheading =  information[2].toString();
    var latitude =information[3].toString();
    var longitude =   information[4].toString();
    var pt = new GLatLng(latitude, longitude);

    //var baseIcon = new GIcon();
    //baseIcon.image = "http://www.capp.net.au/images/icons/"+data[3]+"/"+data[4]+".png";

    //baseIcon.shadow = "http://www.google.com/mapfiles/shadow50.png";
    //  baseIcon.iconSize = new GSize(27, 40);
    //baseIcon.shadowSize = new GSize(27, 40);
    //  baseIcon.iconAnchor = new GPoint(9, 34);
    //  baseIcon.infoWindowAnchor = new GPoint(9, 2);
    //  baseIcon.infoShadowAnchor = new GPoint(18, 25);

    //markerOptions = {icon: baseIcon };
    var Marker = new GMarker(pt);
   
//    var WINDOW_HTML = "<html><head><title>" + imageheading + "</title></head><body><div width='250' height='150'><img width='120' height='120' src='" + document.location.protocol + "//" + document.location.host + "LocationofImageFolder" + imageurl + "' /></div><div><b>" + imageheading + "</b></div><div>" + description + "</div></body></html>";

    var WINDOW_HTML = "<html><head><title>" + imageheading + "</title></head><body><div width='250' height='150'><img width='120' height='120' src='" + imageurl + "' /></div><div><b>" + imageheading + "</b></div><div>" + description + "</div></body></html>";
    GEvent.addListener(Marker, "click", function() {

        if (WINDOW_HTML) {

            Marker.openInfoWindowHtml(WINDOW_HTML);
        }

    });

    return Marker;
}


