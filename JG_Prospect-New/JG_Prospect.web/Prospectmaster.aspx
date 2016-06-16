<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="Prospectmaster.aspx.cs" Inherits="JG_Prospect.Prospectmaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%---------start script for Datetime Picker----------%>
    <link href="datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            var esttimeVale = $('#ContentPlaceHolder1_txtestimatetime').val();
            var estdate = $('#ContentPlaceHolder1_txtEst_date').val();
            var type = $('#ContentPlaceHolder1_hdntype').val();
            var followupdate = $('#ContentPlaceHolder1_txtfollowupdate').val();
            var ddlstatus = document.getElementById("<%=ddlstatus.ClientID %>");
            if (estdate != "" && esttimeVale != "" && type == "new") {
                ddlstatus.options[1].selected = true;
                // ddlstatus.options[ddlstatus.selectedIndex].value = "Set";
                $('#ddlstatus').attr('disabled', 'disabled');
            }
            else if (followupdate != "" && type == "new") {
                ddlstatus.options[11].selected = true;
                $('#ddlstatus').attr('disabled', 'disabled');
            }
            else {
                $('#ddlstatus').enableSelection();
            }


            $(".date").datepicker
            ({
                minDate: 0

            });



            <%--$(".date").datepicker({
                
             
                onSelect: function () {
                    if (estdate != "" && esttimeVale != "" && type == "new") {
                       
                        ddlstatus.options[1].selected = true;
                        $('#ddlstatus').attr('disabled', 'disabled');
                    }
                    else if (followupdate != "" && type == "new") {
                        ddlstatus.options[11].selected = true;
                        $('#ddlstatus').attr('disabled', 'disabled');
                    }
                    else {
                        $('#ContentPlaceHolder1_trfollowup').show();
                        $('#ddlstatus').removeAttr('disabled');
                    }

                    __doPostBack('<%=txtfollowupdate.UniqueID %>', "onkeyup");


                    $('#ContentPlaceHolder1_txtzip').blur(function () {
                        if (estdate != "" && esttimeVale != "" && type == "new") {
                            ddlstatus.options[1].selected = true;
                            $('#ddlstatus').attr('disabled', 'disabled');
                        }
                        else if (followupdate != "" && type == "new") {
                            ddlstatus.options[11].selected = true;
                            $('#ddlstatus').attr('disabled', 'disabled');
                        }
                        else {
                            $('#ddlstatus').removeAttr('disabled');
                        }
                    })
                }
            });--%>

            $('.time').ptTimeSelect();

            $("#btnsave").click(function () {
                var isduplicate = document.getElementById('hdnisduplicate').value;
                var custid = document.getElementById('hdnCustId').value;
                // var url = "../Prospectmaster.aspx?title=" + custid;
                if (isduplicate.toString() == "1") {
                    if (confirm('Duplicate contact. Press Ok to add the another appointment for existing customer.')) {
                        window.open("../Prospectmaster.aspx?title=" + custid);
                        // window.location = '<%= ResolveUrl("'+ url +'") %>';
                    }
                    else {
                        //alert('false');
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <ul class="appointment_tab">
            <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
            <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
            <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
        </ul>
        <h1>
            Add/Update Prospect</h1>
        <div class="form_panel_custom">
            <span>
                <label id="lblIdHeading" runat="server">
                    Id:
                </label>
                <b>
                    <asp:Label ID="lblId" runat="server" Visible="true"></asp:Label></b> </span>
            <span>
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="hdntype" runat="server" />
                <asp:HiddenField ID="HiddenStatus" runat="server" />
                <asp:HiddenField ID="hdnisduplicate" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnCustId" ClientIDMode="Static" runat="server" />
            </span>
            <div class="grid_h">
                <label id="lblTPLHeading" runat="server">
                    Touch Point Log</label></div>
            <div class="grid">
                <asp:GridView ID="grdTouchPointLog" EmptyDataText="No Data Found" runat="server" Width="100%" AutoGenerateColumns="false" OnRowDataBound="grdTouchPointLog_RowDataBound" >
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
                            <asp:Button ID="btnAddNotes" runat="server" Text="Add Notes" OnClick="btnAddNotes_Click" /></div>
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
                                <label id="F3Lbl">
                                    Follow Up</label>
                                     <asp:TextBox ID="txtfollowupdate" runat="server" TabIndex="4"  AutoPostBack="true" CssClass="date" OnTextChanged="txtfollowupdate_TextChanged"
                                     MaxLength="10"></asp:TextBox>
                               <%-- <asp:TextBox ID="txtfollowupdate" runat="server" TabIndex="4" CssClass="date" oncopy="return false" AutoPostBack="true"
                                    onpaste="return false" onkeypress="return isDateKey(event);" OnTextChanged="txtfollowupdate_TextChanged" MaxLength="10"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estimate Date:</label>
                                <asp:TextBox ID="txtEst_date" runat="server" CssClass="date" TabIndex="1" oncopy="return false" ClientIDMode="Static"
                                    onpaste="return false" onkeypress="return isDateKey(event);" MaxLength="10"></asp:TextBox>
                                <%-- <asp:TextBox ID="txtEst_date" runat="server" CssClass="date" TabIndex="1" oncopy="return false"
                                onpaste="return false" onkeypress="return false" MaxLength="10"></asp:TextBox>--%>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    First Name<span>*</span></label>
                                <asp:TextBox ID="txt_fname" runat="server" onkeyup="javascript:Alpha(this)" TabIndex="5"
                                    onkeypress="return isAlphaKey(event);" ValidationGroup="submit" MaxLength="35"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_fname"
                                    ErrorMessage="Please Enter First Name" ForeColor="Red" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cell Phone</label>
                                <asp:TextBox ID="txtcellphone" runat="server" onkeypress="return isNumericKey(event);"
                                    onkeyup="javascript:Numeric(this)" MaxLength="15" TabIndex="7" ValidationGroup="submit"
                                    AutoPostBack="true" OnTextChanged="txtcellphone_TextChanged"></asp:TextBox>
                                <%--  <label></label>
                                 <asp:RequiredFieldValidator ID="RequiredCellPhone" runat="server" ControlToValidate="txtcellphone"
                                ErrorMessage="Please Enter Cell Phone" ForeColor="Red" ValidationGroup="submit">
                            </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    E-mail 1</label>
                                <asp:TextBox ID="txtemail" runat="server" TabIndex="9" MaxLength="50"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemail"
                                ErrorMessage="Please Enter Email" ForeColor="Red" ValidationGroup="submit">
                            </asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="submit" ControlToValidate="txtemail" ErrorMessage="Email Address should be in proper format."
                                    ForeColor="Red"></asp:RegularExpressionValidator>
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
                                    ValidationGroup="submit" ControlToValidate="txtEmail2" ErrorMessage="Email Address should be in proper format."
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
                                    ValidationGroup="submit" ControlToValidate="txtEmail3" ErrorMessage="Email Address should be in proper format."
                                    ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                <%--<input name="input3" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdcheck">
                                <label>
                                    <span>*</span> Contact Preference:</label>
                                <asp:CheckBox ID="chbemail" runat="server" Width="14%" Text="Email" onclick="fnCheckOne(this)"
                                    TabIndex="11" />
                                <asp:CheckBox ID="chbmail" runat="server" Text="Mail" Checked="true" TabIndex="12"
                                    Width="14%" onclick="fnCheckOne(this)" />
                            </td>
                        </tr>
                        <asp:UpdatePanel ID="updatezip" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        <label>
                                            Zip:<span>*</span>
                                        </label>
                                        <%-- <asp:DropDownList ID="ddlzip" runat="server" TabIndex="11" Width="100px" OnSelectedIndexChanged="ddlzip_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>--%>
                                        <asp:TextBox runat="server" ID="txtzip" Text="" AutoPostBack="true" TabIndex="14"
                                            onkeypress="return isNumericKey(event);" onkeyup="javascript:Numeric(this)" MaxLength="15"
                                            OnTextChanged="txtzip_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListElementID="auto_complete"
                                            UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetZipcodes"
                                            TargetControlID="txtzip" EnableCaching="False" CompletionListCssClass="list_limit">
                                        </ajaxToolkit:AutoCompleteExtender>
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="Requiredzip" Display="Dynamic" runat="server" ControlToValidate="txtzip"
                                            ErrorMessage="Please Enter Zip Code" ForeColor="Red" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            City:<span>*</span></label>
                                        <asp:TextBox ID="txtcity" Text="" runat="server" TabIndex="16" MaxLength="50" onkeyup="javascript:Alpha(this)"
                                            onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtcity"
                                            ErrorMessage="Please Enter City" ForeColor="Red" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            State:<span>*</span></label>
                                        <asp:TextBox runat="server" ID="txtstate" Text="" TabIndex="18" MaxLength="50" onkeyup="javascript:Alpha(this)"
                                            onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtstate"
                                            ErrorMessage="Please Enter State" ForeColor="Red" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                <label>
                                    SET Primary Contact<span>*</span></label>
                                <asp:DropDownList ID="ddlprimarycontact" runat="server" TabIndex="22">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem>Cell Phone</asp:ListItem>
                                    <asp:ListItem>House Phone</asp:ListItem>
                                    <asp:ListItem>Alt Phone</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredPrimarycontact" Display="Dynamic" runat="server"
                                    ControlToValidate="ddlprimarycontact" ErrorMessage="Please Select Primary Contact"
                                    ForeColor="Red" InitialValue="Select" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </li>
                <li class="last" style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0" runat="server">
                        <tr>
                            <td>
                                <label>
                                    Status<span>*</span></label>
                                <asp:DropDownList ID="ddlstatus" runat="server" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredStatus" Display="Dynamic" runat="server"
                                    ControlToValidate="ddlstatus" ErrorMessage="Please Select Status" ForeColor="Red"
                                    InitialValue="Select" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="time1">
                                    <label>
                                        Estimate Time:</label>
                                    <asp:TextBox runat="server" ID="txtestimatetime" CssClass="time" TabIndex="2" MaxLength="10"></asp:TextBox>
                                    <%-- <asp:TextBox runat="server" ID="txtestimatetime" CssClass="time" TabIndex="2" 
                                    onkeypress="return false;" MaxLength="10"></asp:TextBox>--%>
                                    <%--  <input name="s2Time1" class="time" value="" id="txtestimatetime" runat="server"
                                        tabindex="2" />--%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Last Name<span>*</span></label>
                                <asp:TextBox ID="txtlastname" runat="server" onkeyup="javascript:Alpha(this)" onkeypress="return isAlphaKey(event);"
                                    TabIndex="6" ValidationGroup="submit" MaxLength="35"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlastname"
                                    ErrorMessage="Please Enter Last Name" ForeColor="Red" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Alt. Phone</label>
                                <asp:TextBox ID="txt_altphone" runat="server" TabIndex="8" ValidationGroup="submit"
                                    onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" MaxLength="15"
                                    AutoPostBack="true" OnTextChanged="txt_altphone_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Home Phone
                                </label>
                                <asp:TextBox ID="txt_homephone" runat="server" TabIndex="10" ValidationGroup="submit"
                                    onkeyup="javascript:Numeric(this)" AutoPostBack="true" onkeypress="return isNumericKey(event);"
                                    MaxLength="15" OnTextChanged="txt_homephone_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Address<span>*</span></label>
                                <asp:TextBox ID="txtaddress" runat="server" TabIndex="13" TextMode="MultiLine"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RegularExpressionValidator ID="txtConclusionValidator1" ControlToValidate="txtaddress" ForeColor="Red" Text="Exceeding 200 characters" ValidationExpression="^[a-zA-Z0-9.]{0,200}$" runat="server" />--%><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaddress"
                                    ErrorMessage="Please Enter Address" ForeColor="Red" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%-- <tr>
                        <td>
                            <label>
                                Follow up Status</label>
                            <asp:TextBox ID="txtfollowupstatus" runat="server" TabIndex="11"></asp:TextBox>
                        </td>
                    </tr>--%>
                        <tr>
                            <td>
                                <label>
                                    Best Time to contact<span>*</span></label>
                                <asp:DropDownList ID="ddlbesttimetocontact" runat="server" TabIndex="15">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem Text="Morning" Value="Morning"></asp:ListItem>
                                    <asp:ListItem Text="Afternoon" Value="Afternoon"></asp:ListItem>
                                    <asp:ListItem Text="Evening" Value="Evening"></asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%--  <asp:TextBox ID="txtbesttimetocontact" runat="server" name="s2Time1" TabIndex="13"></asp:TextBox>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="Select" runat="server"
                                    ControlToValidate="ddlbesttimetocontact" ErrorMessage="Please Select best time to contact"
                                    ForeColor="Red" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       <%-- <tr id="tr_notes" runat="server">
                            <td>
                                <label>
                                    Notes</label>
                                <asp:TextBox ID="txtnotes" TabIndex="17" TextMode="MultiLine" runat="server" MaxLength="50"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtnotes" ForeColor="Red" Text="Exceeding 200 characters" ValidationExpression="^[a-zA-Z0-9.]{0,200}$" runat="server" />--%>
                            <%--</td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                    <ContentTemplate>
                                        <label>
                                            <asp:CheckBox ID="chkbillingaddress" runat="server" Width="20%" AutoPostBack="true"
                                                OnCheckedChanged="chkbillingaddress_CheckedChanged" />Same Billing Address<span>*</span></label>
                                        <asp:TextBox ID="txtbillingaddress" TabIndex="19" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtbillingaddress"
                                            ErrorMessage="Please Enter Billing Address" ForeColor="Red" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Product of interest<span>*</span></label>
                                <%--<asp:TextBox ID="txtproductofinterest" runat="server" TabIndex="15" MaxLength="50"></asp:TextBox>--%>
                                <asp:DropDownList ID="drpProductOfInterest1" runat="server" TabIndex="20" ClientIDMode="Static">
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  Display="Dynamic" runat="server" ControlToValidate="drpProductOfInterest"
                                ErrorMessage="Please Select Product of interest" ForeColor="Red" ValidationGroup="submit" InitialValue="Select" >
                            </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Secondary Product Of Interest</label>
                                <asp:DropDownList ID="drpProductOfInterest2" runat="server" TabIndex="21" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button runat="server" ID="btnsave" ClientIDMode="Static" Visible="true" Text="Save"
                    TabIndex="23" CssClass="cancel" OnClick="Save_Click" ValidationGroup="submit" />
                <asp:Button runat="server" ID="btnupdate" Text="Update" TabIndex="24" CssClass="cancel"
                    OnClick="btnupdate_Click" Visible="false" ValidationGroup="submit" />
                <asp:Button runat="server" ID="btnreset" Visible="true" Text="Reset" TabIndex="25"
                    CssClass="cancel" OnClick="btnreset_Click" />
                <%-- <asp:Button ID="btnTouchPointLog" runat="server" Text="TouchPointLog" TabIndex="34" OnClick="btnTouchPointLog_Click" Visible="false"/>--%>
            </div>
            <%-- <button id="btnTPLog" style="display: none" runat="server"></button>
            <ajaxToolkit:ModalPopupExtender ID="mpeTouchPointLog" PopupControlID="pnlTouchPointLog" runat="server" TargetControlID="btnTPLog" CancelControlID="btnCloseLog">
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
                                        <asp:BoundField HeaderText="Status" DataField="Status"/>
                                        <asp:BoundField HeaderText="User Name" DataField="User Name"/>
                                        <asp:BoundField HeaderText="Designation" DataField="Designation"/>
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
            <div class="clr">
            </div>
        </div>
    </div>
</asp:Content>
