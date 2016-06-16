<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEvents.aspx.cs" MasterPageFile="~/Sr_App/SR_app.Master" Inherits="JG_Prospect.Sr_App.AddEvents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />


    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {

          
            $(".date").datepicker({
                minDate: 0
    
                });
            $('.time').ptTimeSelect();
            minDate: 0

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="right_panel">
    <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="GoogleCalendarView.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <h1>
            <b>Add Events</b></h1>
        <div class="form_panel">
            <span>
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
            </span>
            <ul style="width: 100%;">
                <li style="width: 98%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                     Event Name <span>*</span></label>
                                <asp:TextBox ID="txtEventName" runat="server"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit"
                                    runat="server" ControlToValidate="txtEventName" ForeColor="Red" ErrorMessage="Please Enter Event Name" Display="None">                                 
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Event Date <span>*</span></label>
                                 <asp:TextBox ID="txtHolidayDate" CssClass="date" onkeypress="return false" MaxLength="10"
                                    TabIndex="1" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit" runat="server"
                                   ControlToValidate="txtHolidayDate" ForeColor="Red" ErrorMessage="Please Enter Event Date" Display="None">
                                </asp:RequiredFieldValidator>
                             </td>
                        </tr>
                        <%--<tr>
                           
                            <td>
                                <label>
                                    Description</label>
                                <asp:TextBox ID="txtdescription" TextMode="MultiLine" TabIndex="4" runat="server" Height="95px"></asp:TextBox>
                            </td>
                        </tr>--%>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
            <asp:Button ID="btnadd" Text="Add" runat="server" Visible="false" OnClick="btnadd_Click"/>
                <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit" 
                    ValidationGroup="Submit" OnClick="btnSubmit_Click"/>
                <asp:Button ID="btnreset" runat="server" TabIndex="6" Text="Reset" OnClick="btnreset_Click"/>
            </div>
        </div>
    </div>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowSummary="False" ShowMessageBox="True" />
</asp:Content>
