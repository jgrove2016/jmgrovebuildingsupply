<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Customer_Profile.aspx.cs" Inherits="JG_Prospect.Sr_App.Customer_Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%---------start script for Datetime Picker----------%>
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            $(".date").datepicker();
            $('.time').ptTimeSelect();
            $('code').each(
					function () {
					    eval($(this).html());
					}
				)

            var txtfollowup2 = $('#ContentPlaceHolder1_txtfollowup2').val();
            if (txtfollowup2 != "") {
                $('#followup2span').show();

            }
            else {
                $('#F3Lbl').text('Follow Up 2');
                $('#followup2span').hide();
            }
            if ($('#txtfollowup1').val() != "") {
                $('#spanS1').show();
                $('#txtfollowup1').attr("disabled", true);
                //  $('#txtfollowup1').attr('disabled', 'disabled');
            }
            else {
                $('#spanS1').hide();
            }
            $('#SpanReason').hide();
        });
        $(document).change(function () {
            if ($('#ddlfollowup3').val() == "Closed") {
                $('#SpanReason').show();
            }
            else if ($('#ddlfollowup3').val() != "Closed") {
                $('#SpanReason').hide();
            }

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
        function ConfirmDelete() {
            var Ok = confirm('All dependent record will be deleted permanently. Do you want to proceed?');
            if (Ok)
                return true;
            else
                return false;
        }
    </script>
    <%--    <code>$('#time1 input').ptTimeSelect({ onBeforeShow: function(i){ $('#time1 #time1-data').append('onBeforeShow(event)
        Input field: ' + $(i).attr('name') + "<br />
        "); }, onClose: function(i) { $('#time1 #time1-data').append('onClose(event)Time
        selected:' + $(i).val() + "<br />
        "); } }); $('#time2 input').ptTimeSelect({ onBeforeShow: function(i){ $('#time2
        #time2-data').append('onBeforeShow(event) Input field: ' + $(i).attr('name') + "<br />
        "); }, onClose: function(i) { $('#time2 #time2-data').append('onClose(event)Time
        selected:' + $(i).val() + "<br />
        "); } }); </code>--%>
    <%---------end script for Datetime Picker----------%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="GoogleCalendarView.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <!-- Tabs starts -->
        <h1>
            Customer Profile</h1>
        <div class="form_panel_custom">
            <span>
                <label>
                    Customer Id:
                </label>
                <b>
                    <asp:Label ID="lblmsg" runat="server" Visible="true"></asp:Label></b> </span>
            <div class="grid_h">
                <strong>Touch Point Log</strong></div>
            <div class="grid">
                <asp:GridView ID="grdTouchPointLog" runat="server" Width="100%" AutoGenerateColumns="false" OnRowDataBound="grdTouchPointLog_RowDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="User Id" DataField="Email" />
                        <asp:BoundField HeaderText="SoldJobId" DataField="SoldJobId" />
                        <asp:BoundField HeaderText="Date & Time" DataField="Date" />
                        <asp:BoundField HeaderText="Note / Status" DataField="Status" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />

            <table border="0" cellspacing="0" cellpadding="0" width="950px" style="padding-left: 55px;">
                <tr>
                    <td>
                        <div class="btn_sec">
                            <asp:Button ID="btnAddNotes" runat="server" Text="Add Notes" OnClick="btnAddNotes_Click" /></div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddNotes" runat="server" TextMode="MultiLine" Height="33px" Width="407px"
                            MaxLength="150"></asp:TextBox>
                    </td>
                    <td>
                        <label id="F3Lbl">
                            Follow Up</label>
                        <asp:TextBox ID="txtfollowup3" ClientIDMode="Static" onkeypress="return false;" CssClass="date"
                            runat="server" TabIndex="3"></asp:TextBox>
                        <br />
                        <%--   <label></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtfollowup3"
                                    ValidationGroup="addcust" ErrorMessage="Please enter Follow Up Date!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <br />
                        <span id="spanS3">
                            <label>
                                Status</label></span>
                        <asp:DropDownList ID="ddlfollowup3" OnSelectedIndexChanged="ddlfollowup3_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" runat="server" TabIndex="4">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <%--<table border="0" cellspacing="0" cellpadding="0" width="950px" style="padding-left: 55px;">
                <tr>
                    <td>
                        <label>
                            Follow Up 1</label><asp:TextBox ID="txtfollowup1" ReadOnly="true" runat="server"
                                TabIndex="1"></asp:TextBox>
                        <br />
                        <br />
                        <span id="spanS1">
                            <label>
                                Status</label></span><asp:Label ID="LblStatus1" runat="server"></asp:Label>
                    </td>
                    <td>
                        <span id="followup2span">
                            <label>
                                Follow Up 2</label><asp:TextBox ID="txtfollowup2" ReadOnly="true" TabIndex="2" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <span id="spanS2">
                                <label>
                                    Status</label></span>
                            <asp:Label ID="LblStatus2" runat="server"></asp:Label>
                        </span>
                    </td>
                    <td>
                        <label id="F3Lbl">
                            Follow Up 3</label>
                        <asp:TextBox ID="txtfollowup3" ClientIDMode="Static" onkeypress="return false;" CssClass="date"
                            runat="server" TabIndex="3"></asp:TextBox>
                        <br />
                        <%--   <label></label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtfollowup3"
                                    ValidationGroup="addcust" ErrorMessage="Please enter Follow Up Date!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                      <%--  <br />
                        <span id="spanS3">
                            <label>
                                Status</label></span>
                        <asp:DropDownList ID="ddlfollowup3" ClientIDMode="Static" runat="server" TabIndex="4">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>--%>
            <ul>
                <li style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Estimate Date<span>*</span></label>
                                <asp:TextBox ID="txtestimate_date" CssClass="date" TabIndex="5" runat="server" onkeypress="return false"
                                    MaxLength="10"></asp:TextBox>
                                <%--<input type="text" name="textfield" id="textfield" />--%>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="addcust"
                                    ErrorMessage="Please enter Estimate Date." ControlToValidate="txtestimate_date"
                                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    First Name(s)<span>*</span></label>
                                <asp:TextBox ID="txtfirstname" runat="server" onkeypress="return isAlphaKey(event);"
                                    TabIndex="7" MaxLength="40"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredCustomerStreet" runat="server" ValidationGroup="addcust"
                                    ErrorMessage="Please enter First Name." ControlToValidate="txtfirstname" ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--<input type="text" name="textfield2" id="textfield2" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Address<span>*</span></label>
                                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="addcust"
                                    ErrorMessage="Please enter Address." ControlToValidate="txtaddress" ForeColor="Red"></asp:RequiredFieldValidator>
                                <%-- <select name="select4" style="width:80px;"><option>Select</option></select>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Zip:<span>*</span>
                                </label>
                                <%-- <asp:DropDownList ID="ddlzip" runat="server" TabIndex="11" Width="100px" OnSelectedIndexChanged="ddlzip_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>--%>
                                <asp:TextBox runat="server" ID="txtzip" Text="" AutoPostBack="true" onkeypress="return isNumericKey(event);"
                                    MaxLength="15" TabIndex="11" OnTextChanged="txtzip_TextChanged"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListElementID="auto_complete"
                                    UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetZipcodes"
                                    TargetControlID="txtzip" EnableCaching="False" CompletionListCssClass="list_limit">
                                </ajaxToolkit:AutoCompleteExtender>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredzip" runat="server" ControlToValidate="txtzip"
                                    ErrorMessage="Please Enter Zip Code" ForeColor="Red" ValidationGroup="addcust"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    City:<span>*</span></label>
                                <asp:TextBox ID="txtcity" Text="" runat="server" onkeypress="return isAlphaKey(event);"
                                    MaxLength="40" TabIndex="13"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcity"
                                    ErrorMessage="Please Enter City" ForeColor="Red" ValidationGroup="addcust"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    State:<span>*</span></label>
                                <asp:TextBox runat="server" ID="txtstate" onkeypress="return isAlphaKey(event);"
                                    Text="" MaxLength="40" TabIndex="15"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtstate"
                                    ErrorMessage="Please Enter State" ForeColor="Red" ValidationGroup="addcust"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <span>*</span> Contact Preference:</label>
                                <asp:CheckBox ID="chbemail" runat="server" Width="14%" Text="Email " TabIndex="17"
                                    onclick="fnCheckOne(this)" />
                                <asp:CheckBox ID="chbmail" runat="server" Text="Mail" Checked="true" Width="14%"
                                    TabIndex="18" onclick="fnCheckOne(this)" />
                                <%--   <asp:CheckBox ID="chbphone" runat="server" Width="14%" Text="Phone" TabIndex="19"
                                    onclick="fnCheckOne(this)" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <span>*</span>Billing Address Same</label>
                                        <asp:CheckBox ID="chkbillingaddress" runat="server" Width="5%" AutoPostBack="true"
                                            TabIndex="20" OnCheckedChanged="chkbillingaddress_CheckedChanged" />
                                        <asp:TextBox ID="txtbill_address" runat="server" Width="50%" TextMode="MultiLine"
                                            TabIndex="21"></asp:TextBox>
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="addcust"
                                            ErrorMessage="Please enter Billing Address." ControlToValidate="txtbill_address"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                        <%--<input type="text" name="textfield2" id="textfield2" />--%>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Service Wanted / Notes</label>
                                <asp:TextBox ID="txtService" runat="server" Rows="5" TabIndex="23" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Product Of Interest</label>
                                <asp:DropDownList ID="drpProductOfInterest1" runat="server" TabIndex="29">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Secondary Product Of Interest</label>
                                <asp:DropDownList ID="drpProductOfInterest2" runat="server" TabIndex="30">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </li>
                <li class="last" style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Estimate Time<span>*</span></label>
                                <asp:TextBox ID="txtestimate_time" CssClass="time" runat="server" TabIndex="6" MaxLength="15"
                                    onkeypress="return false"></asp:TextBox>
                                <label>
                                </label>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtestimate_time"
                                    ValidationGroup="addcust" ErrorMessage="Please Select Estimate Time." ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Last Name<span>*</span></label>
                                <asp:TextBox ID="txtlast_name" runat="server" onkeypress="return isAlphaKey(event);"
                                    TabIndex="8" MaxLength="40"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredPrimaryPhone" runat="server" ControlToValidate="txtlast_name"
                                    ValidationGroup="addcust" ErrorMessage="Please Enter Last Name." ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--<input name="" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cell Phone #<span>*</span></label>
                                <asp:TextBox ID="txtcell_ph" runat="server" onkeypress="return isNumericKey(event);"
                                    TabIndex="10" MaxLength="15"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="addcust"
                                    ErrorMessage="Please enter Cell Phone." ControlToValidate="txtcell_ph" ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--<input name="input" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    House Phone #</label>
                                <asp:TextBox ID="txthome_phone" runat="server" onkeypress="return isNumericKey(event);"
                                    TabIndex="12" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Alt Phone #</label>
                                <asp:TextBox ID="txtalt_phone" runat="server" onkeypress="return isNumericKey(event);"
                                    TabIndex="14" MaxLength="15"></asp:TextBox>
                                <%--<input name="input4" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Set Primary Contact<span>*</span></label>
                                <asp:DropDownList ID="ddlprimarycontact" runat="server" TabIndex="16">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Cell Phone</asp:ListItem>
                                    <asp:ListItem>House Phone</asp:ListItem>
                                    <asp:ListItem>Alt Phone</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Primary Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlprimarycontact"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Call Taken By</label>
                                <asp:TextBox ID="txtcall_taken" Text="" runat="server" MaxLength="40" TabIndex="19"></asp:TextBox>
                                <%--<input name="input4" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Project Manager</label>
                                <asp:TextBox ID="txtProjectManager" Text="" MaxLength="100" TabIndex="22" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            Lead Type<span>*</span></label>
                                        <asp:DropDownList ID="ddlleadtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlleadtype_SelectedIndexChanged"
                                            TabIndex="24" Enabled="false">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <%--<asp:ListItem>Internetmarketing:Gmail</asp:ListItem>
                                            <asp:ListItem>Friendly/Family</asp:ListItem>
                                            <asp:ListItem>Truck</asp:ListItem>
                                            <asp:ListItem>Yardsign</asp:ListItem>
                                            <asp:ListItem>TV</asp:ListItem>
                                            <asp:ListItem>Newspaper</asp:ListItem>
                                            <asp:ListItem>Radio</asp:ListItem>
                                            <asp:ListItem>Website</asp:ListItem>--%>
                                            <asp:ListItem>Website</asp:ListItem>
                                            <asp:ListItem>Referal Family/Friend</asp:ListItem>
                                            <asp:ListItem>Self Generated</asp:ListItem>
                                            <asp:ListItem>Canvasser</asp:ListItem>
                                            <asp:ListItem>TV</asp:ListItem>
                                            <asp:ListItem>Newspaper</asp:ListItem>
                                            <asp:ListItem>Radio</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                        <span id="spanother" runat="server" visible="false">
                                            <label>
                                                Enter Other Choice</label>
                                            <asp:TextBox ID="txtleadtype" runat="server" MaxLength="40" TabIndex="25" Enabled="false"></asp:TextBox></span>
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Lead Type."
                                            ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlleadtype"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--<input name="input4" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Email 1 :</label>
                                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" MaxLength="40" TabIndex="26"></asp:TextBox>
                                <br />
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                    ValidationGroup="submit" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="emailcheck" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                    ValidationGroup="submit"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    E-mail 2:
                                </label>
                                <asp:TextBox ID="txtEmail2" runat="server" MaxLength="40" TabIndex="19"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Email address."
                                    ForeColor="Red" ValidationGroup="addcust" ControlToValidate="txtEmail" 
                                    Display="Dynamic"></asp:RequiredFieldValidator> --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="submit" ControlToValidate="txtEmail2" ErrorMessage="Please Enter a valid Email"
                                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                <%--<input name="input3" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    E-mail 3:
                                </label>
                                <asp:TextBox ID="txtEmail3" runat="server" MaxLength="40" TabIndex="19"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Email address."
                                    ForeColor="Red" ValidationGroup="addcust" ControlToValidate="txtEmail" 
                                    Display="Dynamic"></asp:RequiredFieldValidator> --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="submit" ControlToValidate="txtEmail3" ErrorMessage="Please Enter a valid Email"
                                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                <%--<input name="input3" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Best Time To Contact<span>*</span></label>
                                <asp:DropDownList ID="ddlbesttime" runat="server" TabIndex="27">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Afternoon</asp:ListItem>
                                    <asp:ListItem>Evening</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Best Time To Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlbesttime"></asp:RequiredFieldValidator>
                                <%--<input name="input4" type="text" />--%>
                            </td>
                        </tr>
                        <tr id="SpanReason">
                            <td>
                                <label>
                                    Reason of Closed:</label>
                                <asp:TextBox ID="TextBoxReason" runat="server" Rows="5" TextMode="MultiLine" TabIndex="28"></asp:TextBox>
                                <label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="addcust"
                    TabIndex="31" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                    TabIndex="32" />
                <asp:Button ID="btndelete" runat="server" Text="Delete" TabIndex="33" OnClick="btndelete_Click"
                    OnClientClick="return ConfirmDelete()" />
                <%-- <asp:Button ID="btnTouchPointLog" runat="server" Text="TouchPointLog" TabIndex="34"
                    OnClick="btnTouchPointLog_Click" />--%>
                <asp:HiddenField ID="Hidden3rdFollowupId" runat="server" />
                <asp:HiddenField ID="HiddenFieldassignid" runat="server" />
                <asp:HiddenField ID="Hiddenfieldstatus" runat="server" />
                <%-- <input name="input2" type="submit" value="Submit" />
              <input type="submit" value="Cancel" />--%>
            </div>
            <br />
            <%-- <button id="btnTPLog" style="display: none" runat="server">
            </button>
            <ajaxToolkit:ModalPopupExtender ID="mpeTouchPointLog" PopupControlID="pnlTouchPointLog"
                runat="server" TargetControlID="btnTPLog" CancelControlID="btnCloseLog">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlTouchPointLog" runat="server" BackColor="White" Height="600px"
                Width="1000px" Style="display: none">
                <table width="100%" style="border: Solid 5px #A33E3F; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #A33E3F">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">
                            Touch Point Log
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="grid">
                                <asp:GridView ID="grdTouchPointLog" runat="server" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:d}" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="User Name" DataField="User Name" />
                                        <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnCloseLog" runat="server" Text="Close" Style="width: 100px;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>--%>
            <asp:UpdatePanel ID="update1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" align="center">
                        <tr>
                            <td style="width: 50%" valign="top">
                                <br />
                                <asp:Image ID="imgmap" Width="100%" Height="250px" runat="server" />
                                <br />
                                <br />
                                <table width="100%" cellpadding="0" cellspacing="0" align="right">
                                    <tr>
                                        <td style="float: right">
                                            <asp:LinkButton ID="LinkButtonmap1" runat="server" OnClick="LinkButtonmap1_Click">1</asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="LinkButtonmap2" runat="server" OnClick="LinkButtonmap2_Click">2</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <asp:GridView ID="GridViewLocationPic" runat="server" Width="100%" AutoGenerateColumns="false"
                                    RowStyle-VerticalAlign="Top" AllowPaging="true" PageSize="1" OnPageIndexChanging="GridViewLocationPic_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image ID="imglocation" Width="100%" Height="250px" ImageUrl='<%# Eval("PictureName","~/CustomerDocs/LocationPics/{0}") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <asp:GridView ID="GridViewSoldJobs" runat="server" Width="100%" AutoGenerateColumns="false"
                                    CssClass="grid" RowStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    RowStyle-VerticalAlign="Top" AllowPaging="true" PageSize="5" OnPageIndexChanging="GridViewSoldJobs_PageIndexChanging"
                                    OnRowDataBound="GridViewSoldJobs_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sold Job#" DataField="SoldJobId" />
                                        <asp:BoundField HeaderText="Date Sold" DataField="DateSold" DataFormatString="{0:d}" />
                                        <asp:BoundField HeaderText="Date Closed" />
                                        <asp:TemplateField HeaderText="Attachment">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenFieldEstimate" Value='<%#Eval("Id")%>' runat="server" />
                                                <asp:HiddenField ID="HidProductTypeId" Value='<%#Eval("ProductTypeIdFrom")%>' runat="server" />
                                                <asp:HiddenField ID="HidCustomerId" Value='<%#Eval("CustomerId")%>' runat="server" />
                                                <asp:LinkButton ID="lnkestimateid" runat="server" Text='<%#Eval("Attachment")%>'
                                                    OnClick="lnkestimateid_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="Style" ItemStyle-HorizontalAlign="Center" DataField="Style" />                  --%>
                                        <%-- <asp:TemplateField HeaderText="Work Order">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkwrkordfer" runat="server" Text="" OnClick="lnkwrkordfer_Click"></asp:LinkButton>
                                                <asp:HiddenField ID="HiddenFieldWorkOrder" Value='<%#Eval("WorkOrder")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contract">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkwrkContract" runat="server" Text="" OnClick="lnkContract_Click"></asp:LinkButton>
                                                <asp:HiddenField ID="HiddenFieldContract" Value='<%#Eval("Contract")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Job Packets" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkwrkzip" runat="server" Text='Zip & Download' OnClick="lnkwrkzip_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Packets" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Tabs endss -->
    </div>

</asp:Content>

