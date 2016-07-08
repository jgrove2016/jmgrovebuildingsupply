<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="DefinePeriod.aspx.cs" Inherits="JG_Prospect.DefinePeriod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />  

    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            //			    $('code').each(
            //					function () {
            //					    eval($(this).html());
            //					}
            //				)


            $(".date").datepicker();
            $('.time').ptTimeSelect();
        });	
    </script>

 <script type="text/javascript">
     function ConfirmDelete() {
         var Ok = confirm('Are you sure you want to Delete this Pay Schedule?');
         if (Ok)
             return true;
         else
             return false;
     }
    
           </script>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
     <ul class="appointment_tab">
          <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
          <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
          <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
    </ul>
  <h1>Define Pay Schedule</h1>
    <div class="form_panel">    
        <span >
        <asp:Label ID="lblmsg" runat="server" Visible="false" ></asp:Label>
        </span>
        <ul>
            <li>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div class="listbx">
                                <label>View Existing Pay schedule</label>
                                <asp:ListBox ID="selectlist" runat="server" TabIndex="4" Width="250px" AutoPostBack="True"
                                    OnSelectedIndexChanged="selectlist_SelectedIndexChanged" Height="131px"></asp:ListBox>
                                    <br />
                                    <asp:RequiredFieldValidator ControlToValidate="selectlist" ID="lstvalidator" runat="server" ErrorMessage="Please select pay schedule" ForeColor="Red" ValidationGroup="bb"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    
                    
                </table>
            </li>
            <li class="last">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <label>
                                <span>*</span> Schedule Name:</label>
                            <asp:TextBox ID="txtperiod" CssClass="required" runat="server" TabIndex="1" 
                                MaxLength="70" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="payschdname" runat="server" ErrorMessage="Please Define Pay Schedule Name" ControlToValidate="txtperiod" ValidationGroup="aa" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td>
                            <label>
                                <span>*</span>From Date:</label>
                            <asp:TextBox ID="txtfrmdate" CssClass="date" runat="server" TabIndex="2" onkeypress="return false"
                                MaxLength="70"></asp:TextBox>
                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrmdate">
                            </ajaxToolkit:CalendarExtender>--%>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Define Pay Schedule from date" ControlToValidate="txtfrmdate" ValidationGroup="aa" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <span>*</span>To Date:</label>
                            <asp:TextBox ID="txtTodate" CssClass="date" runat="server" TabIndex="3" onkeypress="return false"
                                MaxLength="70"></asp:TextBox>
                       <%--     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate">
                            </ajaxToolkit:CalendarExtender>--%>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Define Pay Schedule to date" ControlToValidate="txtfrmdate" ValidationGroup="aa" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                           <asp:CompareValidator ID="comparetodate" runat="server" ForeColor="Red" ControlToCompare="txtTodate" ControlToValidate="txtfrmdate" Type="Date" Operator="LessThanEqual"  ErrorMessage="To Date Should Be Greater Than From Date" ValidationGroup="aa"></asp:CompareValidator>   
                                    </td>
                    </tr>                  
                </table>
            </li>
        </ul>
    <div class="btn_sec">
       <%-- <input type="button" onclick="enable()" value="Edit" tabindex="5" />--%>
        <asp:Button runat="server" ID="Submit" Text="Save"
            TabIndex="5" OnClick="Submit_Click" ValidationGroup="aa" />
        <asp:Button OnClientClick="return ConfirmDelete()" ID="btnDelete" Text="Delete" runat="server"
            TabIndex="6" OnClick="btnDelete_Click" ValidationGroup="bb" />
        <asp:Button CssClass="cancel" ID="reset" runat="server" Text="Reset" TabIndex="7"
            OnClick="reset_Click" />
    </div>

    </div>
    </div>
</asp:Content>
