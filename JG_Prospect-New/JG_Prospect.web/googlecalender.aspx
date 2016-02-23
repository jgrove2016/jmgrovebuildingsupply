<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="googlecalender.aspx.cs" Inherits="JG_Prospect.googlecalender" %>

<%@ Register TagPrefix="sds" Namespace="Telerik.Web.SessionDS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>Calendar Visualization Library: Demo</title>
<link href="/css/default.css" type="text/css" rel="stylesheet" />


<script type="text/javascript" src="/js/calvis.js"></script>
<script type="text/javascript" src="/js/Dialog.js"></script>

<script type="text/javascript">

    window.onload = function () {
        window.alert = function () { /* silence google */ };
        calvis.ready(main);
    }

    function main() {
        var calId = document.getElementById('TextBox1').value; // 'developer-calendar@google.com';'ennomail.com_n23cg3denchfegcbk7vu43tfr8%40group.calendar.google.com';//

        var calendar = new calvis.Calendar();

        // set the CSS IDs for various visual components for the calendar container
        calendar.setCalendarBody('calendarBodyDiv');
        calendar.setNavControl('navControlDiv');
        calendar.setViewControl('viewControlDiv');
        calendar.setStatusControl('statusControlDiv');
        calendar.setEventCallback('click', displayEvent);

        // set the calenar to pull data from this Google Calendar account
        calendar.setPublicCalendar(calId);

        calendar.setDefaultView('month');
        // display the calendar
        calendar.render();

        // global lightbox dialog to display event details
        eventWindow = new Widget.Dialog;


    }

    function displayEvent(event) {
        var title = event.getTitle().getText();
        var date = event.getTimes()[0].getStartTime().getDate();
        var content = event.getContent().getText();
        var location_ = event.getLocations()[0].getValueString().split('@')[0];


        var eventHtml = [];
        eventHtml.push('<b>Title: </b>');
        eventHtml.push(title);
        eventHtml.push('<br>');
        eventHtml.push('<br>');
        eventHtml.push('<b>When </b>');
        eventHtml.push(date.toString());
        eventHtml.push('<br>');
        //        eventHtml.push('<br>');
        //        eventHtml.push('<b>Where: </b>');
        //        eventHtml.push(location_);
        //  eventHtml.push('<br>');
        eventHtml.push('<br>');
        eventHtml.push('<b>Description:</b>');
        eventHtml.push('<p style="font-size: 12px;">');
        eventHtml.push(content);
        eventHtml.push('</p>');
        eventHtml.push('<b><a href="');
        eventHtml.push(location_); // /DreamAchievers/MyHome.aspx
        eventHtml.push('" target="_parent">Edit</a></b>');


        // after this the eventHtml is in the DOM  tree
        eventWindow.alert(eventHtml.join(''));

        displayMap(location_);
        displayVideo(title);
    }      



</script>

</head>
<body>

<form id="Form1" runat="server">
   <asp:TextBox ID="TextBox1" runat="server" style="display:none;"  ></asp:TextBox>
  <div style="position: absolute; top: 0px; left: 0px;"  id="statusControlDiv"></div>
  <div style="position: absolute; top: 0px; right: 5px;" id="loginControlDiv"></div>

  <div align="center" style="font-family:Arial, Helvetica, sans-serif;">
    <img src='/images/dot.gif' style='position:absolute; top: -1000px;'>

    <table width="665px" style="background:#1777b1;">
      
      <tr style="text-align: center;" align="left">
        <td valign="top">
          <span style="float: left;" id="navControlDiv"></span>
          <span style="float: right;" id="viewControlDiv"></span>          
        </td>
      </tr> 

      <tr align="center">
        <td valign="top">
        
          <div id="calendarBodyDiv" style="background:#fff;"></div>
        </td>
      </tr>
    </table>
  </div>




    Web Site URL:<br />
        <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
           <asp:RegularExpressionValidator ID="regUrl" 
           runat="server" 
           ControlToValidate="txtUrl" 
           ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$"
           Text="Enter a valid URL" />   
        
         
      <asp:Button ID="btnSubmit" Text="Click this to test validation" runat="server" />











     <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadScheduler1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <telerik:RadScriptBlock runat="Server" ID="RadScriptBlock1">
        <script type="text/javascript" src="scripts.js"></script>
    </telerik:RadScriptBlock>
    <div class="demo-container no-bg">
        <telerik:RadScheduler runat="server" ID="RadScheduler1" SelectedDate="2012-04-16"
            OnDataBound="RadScheduler1_DataBound" AppointmentStyleMode="Default"
            OnAppointmentCreated="RadScheduler1_AppointmentCreated" OnAppointmentDataBound="RadScheduler1_AppointmentDataBound"
            OnClientFormCreated="schedulerFormCreated" CustomAttributeNames="AppointmentColor"
            EnableDescriptionField="true">
            <AdvancedForm Modal="true" EnableTimeZonesEditing="true" />
            <Reminders Enabled="true" />
            <AppointmentTemplate>
                <div class="rsAptSubject">
                    <%# Eval("Subject") %>
                </div>
                <%# Eval("Description") %>
            </AppointmentTemplate>
            <AdvancedEditTemplate>
                <scheduler:AdvancedForm runat="server" ID="AdvancedEditForm1" Mode="Edit" Subject='<%# Bind("Subject") %>'
                    Description='<%# Bind("Description") %>' Start='<%# Bind("Start") %>' End='<%# Bind("End") %>'
                    RecurrenceRuleText='<%# Bind("RecurrenceRule") %>' Reminder='<%# Bind("Reminder") %>'
                    AppointmentColor='<%# Bind("AppointmentColor") %>' UserID='<%# Bind("User") %>'
                    RoomID='<%# Bind("Room") %>' TimeZoneID='<%# Bind("TimeZoneID") %>' />
            </AdvancedEditTemplate>
            <AdvancedInsertTemplate>
                <scheduler:AdvancedForm runat="server" ID="AdvancedInsertForm1" Mode="Insert" Subject='<%# Bind("Subject") %>'
                    Start='<%# Bind("Start") %>' End='<%# Bind("End") %>' Description='<%# Bind("Description") %>'
                    RecurrenceRuleText='<%# Bind("RecurrenceRule") %>' Reminder='<%# Bind("Reminder") %>'
                    AppointmentColor='<%# Bind("AppointmentColor") %>' UserID='<%# Bind("User") %>'
                    RoomID='<%# Bind("Room") %>' TimeZoneID='<%# Bind("TimeZoneID") %>' />
            </AdvancedInsertTemplate>
            <TimelineView UserSelectable="false" />
            <TimeSlotContextMenuSettings EnableDefault="true" />
            <AppointmentContextMenuSettings EnableDefault="true" />
        </telerik:RadScheduler>
    </div>




 </form>
</body>
</html>
