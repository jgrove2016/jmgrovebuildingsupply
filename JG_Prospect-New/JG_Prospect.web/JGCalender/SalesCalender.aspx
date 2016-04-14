<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesCalender.aspx.cs" Inherits="JG_Prospect.JGCalender.SalesCalender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void test_Click(object sender, EventArgs e)
    {

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/cupertino/jquery-ui-1.10.3.min.css" rel="stylesheet" type="text/css" />
    <link href="fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.qtip-2.2.0.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <link href="datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />

    <script src="jquery/moment-2.8.1.min.js" type="text/javascript"></script>
    <script src="jquery/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="jquery/jquery-ui-1.11.1.js" type="text/javascript"></script>
    <script src="jquery/jquery.qtip-2.2.0.js" type="text/javascript"></script>
    <script src="fullcalendar/fullcalendar-2.6.0.js" type="text/javascript"></script>
    <script src="scripts/calendarscript.js" type="text/javascript"></script>
    <script src="http://netdna.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <%--<script src="jquery/jquery-ui-timepicker-addon-1.4.5.js" type="text/javascript"></script>--%>

    <style type='text/css'>
        body {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
        }

        .calender-header {
            padding-bottom: 60px;
        }

        #calendar {
            /*width: 900px;*/
            /*margin: 0 auto;*/
        }
        /* css for timepicker */
        .ui-timepicker-div dl {
            text-align: left;
        }

            .ui-timepicker-div dl dt {
                height: 25px;
            }

            .ui-timepicker-div dl dd {
                margin: -25px 0 10px 65px;
            }

        .style1 {
            width: 100%;
        }

        /* table fields alignment*/
        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdnCustomerIds" runat="server" />
        <%-- <div class="calender-header">
            <ul class="nav navbar-nav">

                <li><a href="#" class="dropdown-toggle" data-toggle="dropdown">Sales Calendar <b class="caret"></b></a>
                    <ul id="ul-customer" class="dropdown-menu" style="height: 250px; overflow-y: scroll;">
                </li>
            </ul>

        </div>--%>

        <div id="calendar">
        </div>
        <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;"
            title="Update or Delete Event">
            <table cellpadding="0" class="style1">
                <%-- <tr>
                    <td class="alignRight">Name:</td>
                    <td class="alignLeft">
                        <input id="eventName" type="text" /><br />
                    </td>
                </tr>--%>
                <tr>
                    <td class="alignRight">Description:</td>
                    <td class="alignLeft">
                        <textarea id="eventDesc" disabled="disabled" cols="30" rows="3"></textarea></td>
                </tr>
                <tr>
                    <td class="alignRight">Status:</td>
                    <td class="alignLeft">
                        <select name="eventStatus" id="eventStatus">
                            <option value="0">Select</option>
                            <option value="Set">Set</option>
                            <option value="Prospect">Prospect</option>
                            <option selected="selected" value="est>$1000">est&gt;$1000</option>
                            <option value="est<$1000">est&lt;$1000</option>
                            <option value="sold>$1000">sold&gt;$1000</option>
                            <option value="sold<$1000">sold&lt;$1000</option>
                            <option value="Closed (not sold)">Closed (not sold)</option>
                            <option value="Closed (sold)">Closed (sold)</option>
                            <option value="Rehash">Rehash</option>
                            <option value="cancelation-no rehash">cancelation-no rehash</option>

                        </select></td>
                </tr>
                <tr>
                    <td class="alignRight">Start:</td>
                    <td class="alignLeft">
                        <span id="eventStart"></span></td>
                </tr>
                <tr>
                    <td class="alignRight">End: </td>
                    <td class="alignLeft">
                        <span id="eventEnd"></span>
                        <input type="hidden" id="eventId" /></td>
                </tr>
            </table>
        </div>


        <div id="NewEventDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Create New Event">
            <label id="addEventStartDate" style="display: none"></label>
            <label id="addEventEndDate" style="display: none"></label>
            <ul class="nav navbar-nav">
                <li>
                    <a>
                        <label>
                            <input type='radio' id="chkExisting" name="chkNewEvent" value="Existing" checked="checked" />Existing Customer
                            <%--<asp:CheckBox ID="chkExisting" runat="server" Text="Existing Customer" OnCheckedChanged="chkExisting_CheckedChanged" />--%>
                        </label>
                    </a>
                </li>
                <li>
                    <a>
                        <label>
                            <input type="radio" id="chkNew" name="chkNewEvent" value="New" />New Customer
                            <%--<asp:CheckBox ID="chkNew" runat="server" Text="New Customer" OnCheckedChanged="chkNew_CheckedChanged" />--%>
                        </label>
                    </a>
                </li>
                <li>
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Sales Calendar <b class="caret"></b></a>
                    <ul id="ul-customer" class="dropdown-menu" style="width: 250px; height: 250px; overflow-y: scroll;">
                </li>
            </ul>
        </div>

        <%-- <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px;" title="Add Event">
            <table cellpadding="0" class="style1">
                 <tr>
                    <td class="alignRight">Name:</td>
                    <td class="alignLeft">
                        <input id="addEventName" type="text" size="50" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="alignRight">Description:</td>
                    <td class="alignLeft">
                        <textarea id="addEventDesc" disabled="disabled" cols="30" rows="3"></textarea></td>
                </tr>
                <tr>
                    <td class="alignRight">Start:</td>
                    <td class="alignLeft">
                        <span id="addEventStartDate"></span></td>
                </tr>
                <tr>
                    <td class="alignRight">End:</td>
                    <td class="alignLeft">
                        <span id="addEventEndDate"></span></td>
                </tr>
            </table>

        </div>--%>
        <div runat="server" id="jsonDiv" />
        <input type="hidden" id="hdClient" runat="server" />
    </form>

    <script type="text/javascript">

        var isExisting = true;
        $(document).ready(function () {

            function formatDate(date) {
                var hours = date.getHours();
                var minutes = date.getMinutes();
                var ampm = hours >= 12 ? 'pm' : 'am';
                hours = hours % 12;
                hours = hours ? hours : 12; // the hour '0' should be '12'
                minutes = minutes < 10 ? '0' + minutes : minutes;
                var strTime = hours + ':' + minutes + ' ' + ampm;
                return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
            }

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

            $.getJSON("Customer.ashx?type=customer", function (data) {

                var items = [];
                $.each(data, function (key, val) {
                    // items.push("<li id='" + key + "'>" + val + "</li>");
                    items.push("  <li class='divider'></li> <li class='li-emp'><input type='checkbox' class='customer-check-box' customer_id='" + val.id + "' id='chk_" + val.id + "' />" + val.name + "</li>");
                });


                $("#ul-customer").append(items.join(""));
                $('.customer-check-box').on('click', function () {

                    var addEventStartDate = formatDate(new Date($("#addEventStartDate").text()))
                    var addEventEndDate = $("#addEventEndDate").text();

                    if (isExisting) {
                        var customers = [];
                        if ($('#hdnCustomerIds').val() !== '')
                            customers = JSON.parse($('#hdnCustomerIds').val());
                        if (this.checked) {
                            customers.push($(this).attr('customer_id'))
                            $('#hdnCustomerIds').val(JSON.stringify(customers));

                            window.top.location.href = "../Sr_App/Customer_Profile.aspx?CustomerId=" + $(this).attr('customer_id') + "&es=" + addEventStartDate + "&ee=" + addEventEndDate;
                        }
                    }
                });
            });

            $('input[name=chkNewEvent]').on('change', function () {

                var addEventStartDate = formatDate(new Date($("#addEventStartDate").text()))
                var addEventEndDate = $("#addEventEndDate").text();

                ClearCustomerCheckBox();
                var chkBox = $('input[name=chkNewEvent]:checked').val();
                ClearCustomerCheckBox();
                if (chkBox == 'Existing') {
                    isExisting = true;
                }
                if (chkBox == 'New') {
                    isExisting = false;
                    //window.location.href = "../Sr_App/new_customer.aspx";
                    //$(location).attr("href", "../Sr_App/new_customer.aspx");
                    window.top.location.href = "../Sr_App/new_customer.aspx?es=" + addEventStartDate + "&ee=" + addEventEndDate;
                }

            });

        });

        function ClearCustomerCheckBox() {
            $('input:checkbox.customer-check-box').each(function () {
                if (this.checked)
                    this.checked = false;
            });
        }

    </script>
</body>
</html>
