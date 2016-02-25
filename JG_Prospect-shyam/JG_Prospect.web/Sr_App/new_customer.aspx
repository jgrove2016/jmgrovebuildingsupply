<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="new_customer.aspx.cs" Inherits="JG_Prospect.Sr_App.new_customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%---------start script for Datetime Picker----------%>
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
           
            //$(".date").datepicker();
            $(".date").datepicker
            ({
                minDate: 0

            });
            $('.time').ptTimeSelect();
            //$('#txtestimate_time').ptTimeSelect
            //({
            //    onHourShow: function (hour) {
            //        var now = new Date();
            //        var datetime = currentdate.getDate();
            //        alert(datetime);
            //        if (datetime) {
            //            if (hour <= now.getHours()) {
            //                return false;
            //            }
            //        }
            //        return true;
            //    }
            //});





            $("#btnSubmit").click(function () {
                var isduplicate = document.getElementById('hdnisduplicate').value;
                var custid = document.getElementById('hdnCustId').value;
                if (isduplicate.toString() == "1") {
                    //if (confirm('Duplicate contact, Press Ok to add the another appointment for existing customer.')) {
                    //    window.open("../Prospectmaster.aspx?title=" + custid);
                   // }
                   // else {
                       // alert('false');
                   // }
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
        <!-- Tabs starts -->
        <!-- appointment tabs section start -->
    <ul class="appointment_tab">
        <li><a href="home.aspx">Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
   </ul>
<!-- appointment tabs section end -->
        <h1>
            Add Customer</h1>
        <div class="form_panel_custom">
            <span>
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
            </span>
            <div class="grid_h">
                <label id="lblTPLHeading" runat="server">
                    Touch Point Log</label></div>
            <div class="grid">
                <asp:GridView ID="grdTouchPointLog" EmptyDataText="No Data Found" runat="server" Width="100%" AutoGenerateColumns="false" 
                OnRowDataBound="grdTouchPointLog_RowDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="User Id" DataField="Email" />
                        <asp:BoundField HeaderText="SoldJobId" DataField="SoldJobId" />
                        <asp:BoundField HeaderText="Date & Time" DataField="Date" />
                        <asp:BoundField HeaderText="Note / Status" DataField="Status" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <table id="tblAddNotes" runat="server" border="0" cellspacing="0" cellpadding="0"
                width="950px" style="padding-left: 55px;">
                <tr>
                    <td>
                        <div class="btn_sec">
                            <asp:Button ID="btnAddNotes" runat="server" Text="Add Notes"/></div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddNotes" runat="server" TextMode="MultiLine" Height="33px" Width="407px"
                            MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <%-- <tr id="trfollowup" runat="server">
                       
                    </tr>
              <tr id="tr_status" runat="server" visible="true">
                       
                    </tr>--%>
            </table>
            <ul>
                <li style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Estimate Date<span>*</span></label>
                                <asp:TextBox ID="txtestimate_date" CssClass="date" onkeypress="return false" MaxLength="10"
                                    TabIndex="1" runat="server"></asp:TextBox>
                                <%--<input type="text" name="textfield" id="textfield" />--%>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="addcust"
                                    ErrorMessage="Please enter Estimate Date." ControlToValidate="txtestimate_date"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    First Name(s)<span>*</span></label>
                                <asp:TextBox ID="txtfirstname" onkeyup="javascript:Alpha(this)" onkeypress="return isAlphaKey(event);"
                                    MaxLength="40" TabIndex="3" runat="server"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredCustomerStreet" runat="server" ValidationGroup="addcust"
                                    ErrorMessage="Please enter First Name." ControlToValidate="txtfirstname" ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--<input type="text" name="textfield2" id="textfield2" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="textarea">
                                <label>
                                    Address<span>*</span></label>
                                <asp:TextBox ID="txtaddress" runat="server" Rows="5" TextMode="MultiLine" TabIndex="5"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RegularExpressionValidator ID="txtConclusionValidator1" ControlToValidate="txtaddress" ForeColor="Red" Text="Exceeding 200 characters" ValidationExpression="^[a-zA-Z0-9-,.]{0,10}$" runat="server" /><br />
                                --%>
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
                                <asp:TextBox runat="server" ID="txtzip" Text="" AutoPostBack="true" onkeyup="javascript:Numeric(this)"
                                    TabIndex="7" onkeypress="return isNumericKey(event);" MaxLength="10" OnTextChanged="txtzip_TextChanged"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListElementID="auto_complete"
                                    UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetZipcodes"
                                    TargetControlID="txtzip" EnableCaching="False" CompletionListCssClass="list_limit">
                                </ajaxToolkit:AutoCompleteExtender>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredzip" runat="server" ControlToValidate="txtzip"
                                    ErrorMessage="Please Enter Zip Code" ForeColor="Red" ValidationGroup="addcust">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    City:<span>*</span></label>
                                <asp:TextBox ID="txtcity" Text="" MaxLength="40" runat="server" TabIndex="9" onkeyup="javascript:Alpha(this)"
                                    onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcity"
                                    ErrorMessage="Please Enter City" ForeColor="Red" ValidationGroup="addcust">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    State:<span>*</span></label>
                                <asp:TextBox runat="server" ID="txtstate" MaxLength="40" Text="" TabIndex="11" onkeyup="javascript:Alpha(this)"
                                    onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtstate"
                                    ErrorMessage="Please Enter State" ForeColor="Red" ValidationGroup="addcust">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdcheck">
                                <label>
                                    <span>*</span> Contact Preference:</label>
                                <asp:CheckBox ID="chbemail" runat="server" Width="14%" Text="Email " onclick="fnCheckOne(this)"
                                    TabIndex="13" />
                                <asp:CheckBox ID="chbmail" runat="server" Text="Mail" Checked="true" TabIndex="14"
                                    Width="14%" onclick="fnCheckOne(this)" />
                                <%--  <asp:CheckBox ID="chbphone" runat="server" Width="14%" TabIndex="15" Text="Phone" onclick="fnCheckOne(this)" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <span>*</span>Billing Address Same</label>
                                        <asp:CheckBox ID="chkbillingaddress" runat="server" Width="20%" AutoPostBack="true"
                                            OnCheckedChanged="chkbillingaddress_CheckedChanged" TabIndex="16" />
                                        <asp:TextBox ID="txtbill_address" runat="server" Rows="5" Width="39%" TextMode="MultiLine"
                                            TabIndex="17"></asp:TextBox>
                                        <label>
                                        </label>
                                        <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtbill_address" ForeColor="Red" Text="Exceeding 200 characters" ValidationExpression="^[a-zA-Z0-9.]{0,200}$" runat="server" /><br />
                                        --%>
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
                                    E-mail 1
                                </label>
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="40" TabIndex="19"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Email address."
                                    ForeColor="Red" ValidationGroup="addcust" ControlToValidate="txtEmail" 
                                    Display="Dynamic"></asp:RequiredFieldValidator> --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="addcust" ControlToValidate="txtEmail" ErrorMessage="Email Address should be in proper format."
                                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                <%--<input name="input3" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    E-mail 2
                                </label>
                                <asp:TextBox ID="txtEmail2" runat="server" MaxLength="40" TabIndex="19"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Email address."
                                    ForeColor="Red" ValidationGroup="addcust" ControlToValidate="txtEmail" 
                                    Display="Dynamic"></asp:RequiredFieldValidator> --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="addcust" ControlToValidate="txtEmail2" ErrorMessage="Email Address should be in proper format."
                                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                <%--<input name="input3" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    E-mail 3
                                </label>
                                <asp:TextBox ID="txtEmail3" runat="server" MaxLength="40" TabIndex="19"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Email address."
                                    ForeColor="Red" ValidationGroup="addcust" ControlToValidate="txtEmail" 
                                    Display="Dynamic"></asp:RequiredFieldValidator> --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="addcust" ControlToValidate="txtEmail3" ErrorMessage="Email Address should be in proper format."
                                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                <%--<input name="input3" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Best Time To Contact<span>*</span></label>
                                <asp:DropDownList ID="ddlbesttime" runat="server" TabIndex="23">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>AfterNoon</asp:ListItem>
                                    <asp:ListItem>Evening</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Best Time To Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlbesttime"></asp:RequiredFieldValidator>
                                <%--<input name="input4" type="text" />--%>
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
                                <asp:TextBox ID="txtestimate_time" CssClass="time" runat="server" onkeypress="return false"
                                    TabIndex="2"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtestimate_time"
                                    ValidationGroup="addcust" ErrorMessage="Please Select Estimate Time." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Last Name<span>*</span></label>
                                <asp:TextBox ID="txtlast_name" runat="server" onkeypress="return isAlphaKey(event);"
                                    onkeyup="javascript:Alpha(this)" MaxLength="35" TabIndex="4"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredPrimaryPhone" runat="server" ControlToValidate="txtlast_name"
                                    ValidationGroup="addcust" ErrorMessage="Please Enter Last Name." ForeColor="Red"></asp:RequiredFieldValidator>
                                <%--<input name="" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="textarea">
                                <label>
                                    Cell Phone #<span>*</span></label>
                                <asp:TextBox ID="txtcell_ph" runat="server" TabIndex="6" onkeyup="javascript:Numeric(this)"
                                    onkeypress="return isNumericKey(event);" MaxLength="15" 
                                    AutoPostBack="true" ontextchanged="txtcell_ph_TextChanged" ></asp:TextBox>
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
                                <asp:TextBox ID="txthome_phone" runat="server" TabIndex="8" onkeyup="javascript:Numeric(this)"
                                    onkeypress="return isNumericKey(event);" MaxLength="15"  
                                    AutoPostBack="true" ontextchanged="txthome_phone_TextChanged"  ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Alt Phone #</label>
                                <asp:TextBox ID="txtalt_phone" runat="server" TabIndex="10" onkeyup="javascript:Numeric(this)"
                                    onkeypress="return isNumericKey(event);" MaxLength="15"  
                                    AutoPostBack="true" ontextchanged="txtalt_phone_TextChanged" ></asp:TextBox>
                                <%--<input name="input4" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Set Primary Contact<span>*</span></label>
                                <asp:DropDownList ID="ddlprimarycontact" runat="server" TabIndex="12">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Cell Phone</asp:ListItem>
                                    <asp:ListItem>House Phone</asp:ListItem>
                                    <asp:ListItem>Alt Phone</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Primary Contact."
                                    ForeColor="Red" ValidationGroup="addcust" InitialValue="0" ControlToValidate="ddlprimarycontact"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdnisduplicate" ClientIDMode="Static" runat="server" />
                                 <asp:HiddenField ID="hdnCustId" ClientIDMode="Static" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Call Taken By</label>
                                <asp:TextBox ID="txtcall_taken" Text="" MaxLength="45" TabIndex="16" runat="server"></asp:TextBox>
                                <%--<input name="input4" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Project Manager</label>
                                <asp:TextBox ID ="txtProjectManager" Text="" TabIndex="18" MaxLength="100" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textarea">
                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            Lead Type<span>*</span></label>
                                        <asp:DropDownList ID="ddlleadtype" runat="server" OnSelectedIndexChanged="ddlleadtype_SelectedIndexChanged"
                                            AutoPostBack="true" TabIndex="20">
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
                                                Enter Other Choice<span>*</span></label>
                                            <asp:TextBox ID="txtleadtype" MaxLength="40" runat="server" TabIndex="21"></asp:TextBox></span>
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
                                <label>Product Of Interest</label>
                                <asp:DropDownList ID="drpProductOfInterest1" TabIndex="24" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Secondary Product Of Interest</label>
                                <asp:DropDownList ID="drpProductOfInterest2" TabIndex="25" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td class="textarea">
                                <label>
                                    Service Wanted / Notes</label>
                                <asp:TextBox ID="txtService" TabIndex="22" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                <label>
                                </label>
                            </td>
                        </tr>--%>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnSubmit" runat="server" ClientIDMode="Static" Text="Submit" ValidationGroup="addcust"
                    TabIndex="26" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                    TabIndex="27" />
                <%-- <input name="input2" type="submit" value="Submit" />
              <input type="submit" value="Cancel" />--%>
            </div>
        </div>
        <!-- Tabs endss -->
    </div>
</asp:Content>
