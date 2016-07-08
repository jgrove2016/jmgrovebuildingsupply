var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;

function updateEvent(event, element) {

    //alert(event.description);
    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;

  


    $('#updatedialog').dialog('open');
    $("#eventName").val(event.title);
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    $("#eventStart").text("" + event.start.toLocaleString());
    $("#eventStatus").val(event.status);
    
    $("#txtCustID").html("<a href='../Sr_App/Customer_Profile.aspx?CustomerId=" + event.customerid + "' target='_blank'>" + event.customerid + "</a>");
    
    $("#txtlstName").text(event.lastname);
    $("#spnPrimaryPhone").text(event.primarycontact);
    $("#spnAddress").text(event.address);
    $("#spnZip").text(event.zipcode);
    $("#spnProductLine").text(event.productline);
    $("#spnAddedBy").text(event.addedby);

    if (event.end === null) {
        $("#eventEnd").text("");
    }
    else {
        $("#eventEnd").text("" + event.end.toLocaleString());
    }

    return false;
}

function updateSuccess(updateResult) {
    // document.getElementById("ifrmCalender").contentDocument.location.reload(true);
    //alert(updateResult);
    // loadCalender();

}

function deleteSuccess(deleteResult) {
    //alert(deleteResult);
}

function addSuccess(addResult) {
    // if addresult is -1, means event was not added
    //    alert("added key: " + addResult);

    if (addResult != -1) {
        $('#calendar').fullCalendar('renderEvent',
						{
						    title: $("#addEventName").val(),
						    start: addStartDate,
						    end: addEndDate,
						    id: addResult,
						    description: $("#addEventDesc").val(),
						    allDay: globalAllDay
						},
						true // make the event "stick"
					);


        $('#calendar').fullCalendar('unselect');
    }
}

function UpdateTimeSuccess(updateResult) {
    //alert(updateResult);
}

function selectDate(start, end, allDay) {

    $('#NewEventDialog').dialog('open');
    $("#addEventStartDate").text("" + start.toLocaleString());
    $("#addEventEndDate").text("" + end.toLocaleString());

    //addStartDate = start;
    //addEndDate = end;
    //globalAllDay = allDay;
    //alert(allDay);
}

function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start
    };

    // FullCalendar 1.x
    //if (allDay) {
    //    eventToUpdate.start.setHours(0, 0, 0);
    //}

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;
    }
    else {
        eventToUpdate.end = event.end;

        // FullCalendar 1.x
        //if (allDay) {
        //    eventToUpdate.end.setHours(0, 0, 0);
        //}
    }

    // FullCalendar 1.x
    //eventToUpdate.start = eventToUpdate.start.format("dd-MM-yyyy hh:mm:ss tt");
    //eventToUpdate.end = eventToUpdate.end.format("dd-MM-yyyy hh:mm:ss tt");

    // FullCalendar 2.x
    var endDate;
    if (!event.allDay) {
        endDate = new Date(eventToUpdate.end + 60 * 60000);
        endDate = endDate.toJSON();
    }
    else {
        endDate = eventToUpdate.end.toJSON();
    }

    eventToUpdate.start = eventToUpdate.start.toJSON();
    eventToUpdate.end = eventToUpdate.end.toJSON(); //endDate;
    eventToUpdate.allDay = event.allDay;

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}

function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    // FullCalendar 1.x
    //updateEventOnDropResize(event, allDay);

    // FullCalendar 2.x
    updateEventOnDropResize(event);
}

function eventResized(event, dayDelta, minuteDelta, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return false;

    return pattern.test(stringToCheck);
}

function isAllDay(startDate, endDate) {
    var allDay;

    if (startDate.format("HH:mm:ss") == "00:00:00" && endDate.format("HH:mm:ss") == "00:00:00") {
        allDay = true;
        globalAllDay = true;
    }
    else {
        allDay = false;
        globalAllDay = false;
    }

    return allDay;
}

function qTipText(start, end, description) {
    var text;

    if (end !== null)
        text = "<strong>Start:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>End:</strong> " + end.format("MM/DD/YYYY hh:mm T") + "<br/><br/>" + description;
    else
        text = "<strong>Start:</strong> " + start.format("MM/DD/YYYY hh:mm T") + "<br/><strong>End:</strong><br/><br/>" + description;

    return text;
}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

$(document).ready(function () {
    var loadCalender = function () {
        var dateDefault = null;
        var viewDefault = 'agendaWeek'
        if (getParameterByName('d') != undefined && getParameterByName('d') != '') {
            dateDefault = getParameterByName('d');
            viewDefault = 'agendaDay';
        }
        var eIds = getParameterByName('e');
        var searchText = getParameterByName('s');

        $('#calendar').fullCalendar({
            theme: true,
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay,agendaFourDay'
            },

            views: {
                agendaFourDay: {
                    type: 'agenda',
                    duration: { days: 4 },
                    buttonText: '4 day'
                },
            },

            defaultView: viewDefault,
            //minTime: '07:00:00',
            //maxTime: '21:00:00',
            allDaySlot: false,
            eventClick: updateEvent,
            selectable: true,
            selectHelper: true,
            select: selectDate,
            editable: true,
            events: "JsonResponse.ashx?eids=" + eIds + "&searchText=" + searchText,
            //events: {
            //    url: 'JsonResponse.ashx',

            //    type: 'POST',
            //    data: {
            //        eIds: $('#hdnCustomerIds').val()
            //    },
            //    error: function () {
            //        alert('there was an error while fetching events!');
            //    }
            //},
            defaultDate: dateDefault,
            eventDrop: eventDropped,
            eventResize: eventResized,
            eventRender: function (event, element, view) {
                $(element).find('.fc-title').prepend('<a href="javascript:void(0);" class="delete-event-link" style="color:#fff">' + event.customerid + ',</a>');
                $(element).find('.delete-event-link').click(function (e) {
                    e.stopImmediatePropagation(); //stop click event, add deleted click for anchor link
                    window.top.location.href = "/Sr_App/Customer_Profile.aspx?CustomerId=" + event.customerid;
                });
                //$(element).find('.fc-title').prepend('<a href="javascript:void(0);" class="delete-event-link" style="color:#fff">' + event.customerid + ',</a>');
                element.qtip({
                    content: {
                        text: qTipText(event.start, event.end, event.description),
                        title: '<strong>' + event.title + '</strong>'
                    },
                    position: {
                        my: 'bottom left',
                        at: 'top right'
                    },
                    style: { classes: 'qtip-shadow qtip-rounded' }
                });
            }
        });


    }

    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "update": function () {
                //alert(currentUpdateEvent.title);
                var eventToUpdate = {
                    id: currentUpdateEvent.id,
                    //  title: $("#eventName").val(),
                    //description: $("#eventDesc").val(),
                    status: $("#eventStatus").val()
                };

                if (checkForSpecialChars(eventToUpdate.title) || checkForSpecialChars(eventToUpdate.description)) {
                    alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                }
                else {
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
                    $(this).dialog("close");
                    // loadCalender();
                    location.reload();
                    //$('#ifrmCalender').attr('src', function (i, val) { return val; });
                    //  currentUpdateEvent.title = $("#eventName").val();
                    // currentUpdateEvent.description = $("#eventDesc").val();
                    //currentUpdateEvent.status= $("#eventStatus").val();
                    //$('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
                }

            }
            /*, By jayanti
            "delete": function () {

                if (confirm("do you really want to delete this event?")) {

                    PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
                    $(this).dialog("close");
                    $('#calendar').fullCalendar('removeEvents', $("#eventId").val());
                }
            }
            */
        }
    });


    // new Dialog
    $('#NewEventDialog').dialog({
        autoOpen: false,
        width: 470,
        height: 450
    });


    /*Jayanti
        //add dialog
        $('#addDialog').dialog({
            autoOpen: false,
            width: 470,
            buttons: {
                "Add": function() {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                    var eventToAdd = {
                        title: $("#addEventName").val(),
                        description: $("#addEventDesc").val(),
    
                        // FullCalendar 1.x
                        //start: addStartDate.format("dd-MM-yyyy hh:mm:ss tt"),
                        //end: addEndDate.format("dd-MM-yyyy hh:mm:ss tt")
    
                        // FullCalendar 2.x
                        start: addStartDate.toJSON(),
                        end: addEndDate.toJSON(),
    
                        allDay: isAllDay(addStartDate, addEndDate)
                    };
                    
                    if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
                        alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                    }
                    else {
                        //alert("sending " + eventToAdd.title);
    
                        PageMethods.addEvent(eventToAdd, addSuccess);
                        $(this).dialog("close");
                    }
                }
            }
        });
        */


    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };


    loadCalender();


});


