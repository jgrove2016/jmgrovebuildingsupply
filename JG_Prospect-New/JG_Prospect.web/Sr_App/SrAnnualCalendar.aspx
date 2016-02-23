<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SrAnnualCalendar.aspx.cs" MasterPageFile="~/Sr_App/SR_app.Master" Inherits="JG_Prospect.Sr_App.SrAnnualCalendar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Src="~/Controls/left.ascx" TagName="leftmenu" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />


    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {

            $(".date").datepicker();
            $('.time').ptTimeSelect();

            $("#btnSubmit").click(function () {
                var isduplicate = document.getElementById('hdnisduplicate').value;
                var custid = document.getElementById('hdnCustId').value;
                if (isduplicate.toString() == "1") {
                    if (confirm('Duplicate contact, Press Ok to add the another appointment for existing customer.')) {
                        window.open("../Prospectmaster.aspx?title=" + custid);
                    }
                    else {
                        // alert('false');
                    }
                }
            });
        });

        function fnCheckOne(me) {
            me.checked = true;
            var chkary = document.getElementsByTagName('input');
            for (i = 0; i < chkary.length; i++) {
                if (chkary[i].type == 'checkbox') {
                    if (chkary[i].id != me.id)
                        chkary[i].checked = false;
                }
            }
        }
    </script>

    <code>$('#time1 input').ptTimeSelect({ onBeforeShow: function(i){ $('#time1 #time1-data').append('onBeforeShow(event)
        Input field: ' + $(i).attr('name') + "<br />
        "); }, onClose: function(i) { $('#time1 #time1-data').append('onClose(event)Time
        selected:' + $(i).val() + "<br />
        "); } }); $('#time2 input').ptTimeSelect({ onBeforeShow: function(i){ $('#time2
        #time2-data').append('onBeforeShow(event) Input field: ' + $(i).attr('name') + "<br />
        "); }, onClose: function(i) { $('#time2 #time2-data').append('onClose(event)Time
        selected:' + $(i).val() + "<br />
        "); } }); </code>
    <%---------end script for Datetime Picker----------%>

    
     <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            display: none;
        }

        .myfont
        {
            font-family: 'barcode_fontregular';
            font-size: 37px;
        }

        .black_overlay
        {
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

        .white_content
        {
            display: none;
            position: absolute;
            top: 10%;
            left: 33%;
            width: auto;
            height: auto;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
    
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
            width: 100% !important;/*108*/
            height: 24px !important;/*44*/
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
    </style>
    <script type="text/javascript">
        function ClosePopupAddEvent() {
            document.getElementById('lightSetStatus').style.display = 'none';
            document.getElementById('fadeSetStatus').style.display = 'none';
        }

        function AddEventPopup() {
            document.getElementById('lightSetStatus').style.display = 'block';
            document.getElementById('fadeSetStatus').style.display = 'block';
        }
        
     </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--  <div class="left_panel arrowlistmenu">
        <uc1:leftmenu ID="left1" runat="server" />
    </div>--%>
    <div class="right_panel">
        <ul class="appointment_tab">
             
            <li><a id="A1" href="home.aspx" runat="server">Sales Calendar</a> </li>
            <li><a id="A2" href="GoogleCalendarView.aspx" runat="server" >Master Prospect Calendar</a></li>
            <li><a id="A5" href="#" runat="server">Construction Calendar</a></li>
            <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
            <li><a id="A4" href="SrAnnualCalendar.aspx" runat="server" class="active">Annual Event Calendar</a></li>
        </ul>
        <h1>
            <b>Dashboard</b></h1>
        <h2>
            Annual Event Calendar              <asp:Button ID="btnAddEvent" runat="server" Text="Add Event" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Height="30px" Width="75px" OnClick="btnAddEvent_Click"/> </h2>
        <div class="calendar" style="margin: 0;">

            <div id="calendarBodyDiv">
                    <telerik:RadScheduler ID="rsAppointments" runat="server" DataKeyField="id" DayStartTime="7:00:00" DayEndTime="20:59:59"
                        AllowEdit="false" DataStartField="EventDate" DataEndField="EventDate" DataSubjectField="EventName"
                        ShowHeader="true" Width="100%" Height="100%" TimelineView-NumberOfSlots="0" TimelineView-ShowDateHeaders="false"
                        EnableExactTimeRendering="true" EnableDatePicker="true" SelectedView="WeekView"
                        AppointmentContexcalendarBodyDivtMenuSettings-EnableDefault="true" TimelineView-GroupingDirection="Vertical"
                        TimelineView-ReadOnly="true" DisplayDeleteConfirmation="false" OnAppointmentClick="rsAppointments_AppointmentClick"><%-- OnClientAppointmentClick="OnClientAppointmentClick" OnClientTimeSlotClick="OnClientTimeSlotClick"--%>
                        <AdvancedForm Modal="True" />
                    </telerik:RadScheduler>
                    <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Title="No Appointment available"
                        Behaviors="Close">
                    </telerik:RadWindow>

            </div>
           <%-- <iframe src="https://www.google.com/calendar/embed?mode=WEEK&amp;src=kf0evh90ibsidlkgur33a0s0h4%40group.calendar.google.com&ctz=America/St_Thomas"
                style="border: 0; width: 100%; height: 800px;" frameborder="0" scrolling="no">
            </iframe>--%>
          <%--  <iframe src="https://www.google.com/calendar/embed?mode=WEEK&amp;src=j3h50vq9am0at74sopc8dferk4%40group.calendar.google.com&ctz=America/St_Thomas"  style="border: 0; width: 100%; height: 800px;" frameborder="0" scrolling="no"></iframe>--%>
        </div>
    </div>
    
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" ShowContentDuringLoad="false" Width="400px"
                Height="400px" Title="Annual Event Calendar" Behaviors="Default">
                <ContentTemplate>
                    <br /><br /><br />
              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                      <asp:Label ID="Label1" runat="server" Text="Event Id : " Visible="false"></asp:Label> 
                <asp:LinkButton ID="lbtCustomerID" runat="server" OnClick="lbtCustomerID_Click" Visible="false"></asp:LinkButton> <%--Text='<%#Eval("id")%>'--%>
                      <br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Event Name : "></asp:Label> 
                    <asp:TextBox ID="txtEventName" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit"
                                    runat="server" ControlToValidate="txtEventName" ForeColor="Red" ErrorMessage="Please Enter Event Name" Display="None">                                 
                                </asp:RequiredFieldValidator>
                     <br /><br />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Event Date : " ></asp:Label> 
                     <%--<asp:Label ID="lblPhone" runat="server"></asp:Label><br />--%>
                    <asp:TextBox ID="txtHolidayDate" CssClass="date" onkeypress="return false" MaxLength="10"
                                    TabIndex="1" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" runat="server"
                                   ControlToValidate="txtHolidayDate" ForeColor="Red" ErrorMessage="Please Enter Event Date" Display="None">
                                </asp:RequiredFieldValidator>

                    <br /><br /><br /><br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button  ID="btnsave" runat="server" ValidationGroup="Submit" OnClick="btnsave_Click" Text="Update" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Height="30px" Width="75px"/> &nbsp; &nbsp;
                    <asp:Button  ID="btnDelete" runat="server" ValidationGroup="Submit" Text="Delete" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;" Height="30px" Width="75px" OnClick="btnDelete_Click"/> &nbsp; &nbsp;
                    <asp:Button  ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Style="background: url(img/main-header-bg.png) repeat-x; color: #fff;"  Height="30px" Width="75px"/>

                     </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

     <asp:Panel ID="pnlStatusSet" runat="server">
            <div id="lightSetStatus" class="white_content">
                <h3>Add Event</h3>
                <a href="javascript:void(0)" onclick="document.getElementById('lightSetStatus').style.display='none';document.getElementById('fadeSetStatus').style.display='none'">Close</a>
                <table>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                           Event Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtEventNameSR" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Event"
                               runat="server" ControlToValidate="txtEventNameSR" ForeColor="Red" ErrorMessage="Please Enter Event Name" Display="None">                                 
                             </asp:RequiredFieldValidator>
                        </td>
                        <td>
                             Event Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtEventDate" runat="server" CssClass="date" onkeypress="return false" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Event" runat="server"
                                   ControlToValidate="txtEventDate" ForeColor="Red" ErrorMessage="Please Enter Event Date" Display="None">
                                </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="btn_sec">
                                <asp:Button ID="btnSaveEvent" runat="server" Text="Save" ValidationGroup="Event" OnClick="btnSaveEvent_Click"/>
                            </div>
                        </td>
                        
                     </tr>
                </table>
            </div>
        </asp:Panel>
    <div id="fadeSetStatus" class="black_overlay">
        </div>


    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Event" ShowSummary="False" ShowMessageBox="True" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowSummary="False" ShowMessageBox="True" />
</asp:Content>

