<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="shutterproposal.aspx.cs" Inherits="JG_Prospect.Sr_App.shutterproposal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <%---------start script for Datetime Picker----------%>
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            $("ol").attr('class', '');
            $(".date").datepicker();
            $('.time').ptTimeSelect();
            $('#trauthpass').hide();
            $('#trcheque').hide();
            $('#trcard').hide();
            $('#btnsavesold').hide();          

            $('#ContentPlaceHolder1_chksignature').attr('checked', false);
            $('#ContentPlaceHolder1_lblcheck').show();
            if ($('#ContentPlaceHolder1_HidCV').val() == '') {
                $('#ContentPlaceHolder1_chkedit').attr('checked', false);
                $('#trauthpass').hide();
            }
            else {
                $('#ContentPlaceHolder1_chkedit').attr('checked', true);
                $('#trauthpass').show();
            }

            if ($('#ContentPlaceHolder1_chkedit').is(':checked')) {
                $('#trauthpass').show();
                $('#ContentPlaceHolder1_txtAmount').removeAttr('readonly');
            }
            else {
                $('#trauthpass').hide();
                $('#ContentPlaceHolder1_txtAmount').attr('readonly', 'readonly');
            }

            var count = $('#ContentPlaceHolder1_hidDownPayment').val();
            $('#ContentPlaceHolder1_txtAmount').val(count);

            $('#ContentPlaceHolder1_ddlpaymode').change(function () {
                if ($(this).val() == "Cash") {
                    $('#trcheque').hide();
                    $('#trcard').hide();
                }
                else if ($(this).val() == "Check") {
                    $('#trcheque').show();
                    $('#trcard').hide();
                }
                else if ($(this).val() == "Credit Card") {
                    $('#trcheque').hide();
                    $('#trcard').show();
                }
                if ($('#chksignature').is(':checked') == true) {
                    $('#lblcheck').hide();
                    $('#btnsavesold').show();
                    $('#trauthpass').show();
                }
                else {
                    $('#lblcheck').show();
                    $('#btnsavesold').hide();
                }

                if ($('#ContentPlaceHolder1_chkedit').is(':checked') == true) {
                    $('#trauthpass').show();
                }
                else {
                    $('#trauthpass').hide();
                }
            });

        });

        //        $('#ContentPlaceHolder1_chkedit').click(function ()
        //        {
        //            if ($('#ContentPlaceHolder1_chkedit').is(':checked') == true)
        //             {
        //                $('#trcheque').hide();
        //                $('#trcard').hide();
        //            }
        //        });
        $(function () {
            $('#ContentPlaceHolder1_btnsavesold').click(function () {
                //               alert($('#ContentPlaceHolder1_hidDownPayment').val());
                //               alert($('#ContentPlaceHolder1_txtAmount').val());

                if ($('#ContentPlaceHolder1_chksignature').is(':checked') == false) {

                    return false;
                }
                else if ($('#ContentPlaceHolder1_txtAmount').val() == '') {
                    $('#ContentPlaceHolder1_lblAmount').show();
                    return false;
                }
                else if ($('#ContentPlaceHolder1_chkedit').is(':checked') == true) {
                    if ($('#ContentPlaceHolder1_txtauthpass').val() == '') {
                        $('#ContentPlaceHolder1_lblPassword').show();
                        return false;
                    }

                    else {

                        var val = $('#ContentPlaceHolder1_txtAmount').val();
                        $('#ContentPlaceHolder1_hidDownPayment').val(val);

                        return true;
                    }
                }
                else {

                    var val = $('#ContentPlaceHolder1_txtAmount').val();
                    $('#ContentPlaceHolder1_hidDownPayment').val(val);

                    return true;
                }
            });
        });
        $(function () {
            $('#ContentPlaceHolder1_chkedit').click(function () {
                if ($(this).is(':checked')) {
                    $('#trauthpass').show();
                    $('#ContentPlaceHolder1_txtAmount').removeAttr('readonly');
                }
                else {
                    $('#trauthpass').hide();
                    $('#ContentPlaceHolder1_txtAmount').attr('readonly', 'readonly');
                }
            });
        });
        $(function () {
            $('#ContentPlaceHolder1_chksignature').click(function () {
                if ($(this).is(':checked')) {
                    $('#ContentPlaceHolder1_lblcheck').hide();
                }
                else {
                    $('#ContentPlaceHolder1_lblcheck').show();
                }
            });
        });


        $(function () {
            $('#ContentPlaceHolder1_btnCancelsold').click(function () {

                $('#ContentPlaceHolder1_chksignature').attr('checked', false);
                $('#ContentPlaceHolder1_lblcheck').hide();
                $('#ContentPlaceHolder1_lblAmount').hide();
                $('#ContentPlaceHolder1_chkedit').attr('checked', false);
                $('#trauthpass').hide();
            });
        });
       
    </script>
    <style type="text/css">
        .grid td
        {
            line-height:10px;
            padding: 3px;
            text-align: left;
            min-height: 5px;
            border: #ccc 1px solid;
            border-top: none;
            border-bottom: none;
            word-break: break-word;
        }
        
        #ContentPlaceHolder1_pnlpopup_backgroundElement, #ContentPlaceHolder1_pnlpopup_backgroundElement
        {
            background: #000;
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
     <ul class="appointment_tab">
        <li><a href="home.aspx">Personal Appointment</a></li>
        <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
        <li><a href="#">Construction Calendar</a></li>
        <li><a href="CallSheet.aspx">Call Sheet</a></li>
    </ul>
        <h1>
            <b>Customer Contract</b></h1>
        <!-- Tabs starts -->
        <div id="tabs" style="background-color:#FFFFFF;">
            <%--  <ul>
                <li style="margin: 0;"><a href="#tabs-2">Customer</a></li>
            </ul>--%>
            <div id="tabs-1"  style="background-color:#FFFFFF;">
                <div class="form_panel shutter_proposal" style="background-color:#FFFFFF; background:none;" >
                    <asp:Literal ID="LiteralHeader" runat="server"></asp:Literal><br />
                    
                    <asp:GridView ID="grdproductlines" style="border-color:rgba(111,111,111,000) transparent transparent;" runat="server" CssClass="grid" Width="100%" AutoGenerateColumns="false"
                        PageSize="10" AllowPaging="true" OnRowDataBound="grdproductlines_RowDataBound" Visible="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Please Select Proposal" ControlStyle-CssClass="no_line proposal_list"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Vertical" runat="server"
                                        Width="100%" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="A" Selected="True" Text="Proposal A"></asp:ListItem>
                                        <asp:ListItem Value="B" Text="Proposal B"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                                <ControlStyle CssClass="no_line proposal_list"></ControlStyle>
                                <HeaderStyle HorizontalAlign="Center" BackColor="Transparent"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="200px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenFieldEstimate" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="HiddenFieldProduct" runat="server" Value='<%# Eval("productId") %>'></asp:HiddenField>
                                    
                                    <asp:HiddenField ID="HDAmountA" runat="server" Value=''></asp:HiddenField>
                                    <asp:HiddenField ID="HDAmountB" runat="server" Value=''></asp:HiddenField>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" class="no_line" style="font-family: tahoma,geneva,sans-serif;
                                        text-align: left; font-size: 10pt;">
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LiteralBody" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" BackColor="Transparent"></HeaderStyle>
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--style="background:url(../img/Logo_BG.png) center no-repeat;background-size: 41%;"--%>
                    
                    <asp:GridView ID="grdCustom"  runat="server" style="border-color:rgba(111,111,111,000) transparent transparent;" CssClass="grid" Width="100%" AutoGenerateColumns="false"
                        PageSize="10" AllowPaging="true" OnRowDataBound="grdCustom_RowDataBound" Visible="false">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenFieldEstimateCustom" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="HiddenFieldProduct" runat="server" Value='<%# Eval("productId") %>'></asp:HiddenField>
                                    
                                    <asp:HiddenField ID="HDAmountACustom" runat="server" Value=''></asp:HiddenField>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" class="no_line" style="font-family: tahoma,geneva,sans-serif;
                                        text-align: left; font-size: 10pt;">
                                        <tr align="left">
                                            <td align="justify">
                                                <asp:Literal ID="LiteralBodyCustom" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" BackColor="Transparent"></HeaderStyle>
                                <ItemStyle VerticalAlign="Top" BackColor="Transparent"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Literal ID="LiteralBody2" runat="server"></asp:Literal>
                    <div class="btn_sec">
                         <asp:Button ID="btnSold" runat="server" Text="Sold" TabIndex="1" />
                        <span>
                            <asp:LinkButton ID="btnNotSold" runat="server" Style="background: none; width: auto;
                                height: auto; box-shadow: none; color: #0000ff; text-decoration: underline; font-size: 12px;
                                font-weight: normal;" Text="Not Sold" TabIndex="2" 
                             onclick="btnNotSold_Click1" />
                        </span>
                    </div>
                    <div>
                    <asp:Button ID="btnfake" runat="server" style="display:none;" />
                    <asp:Button ID="btnfake1" runat="server" style="display:none;" />
                      <ajaxToolkit:ModalPopupExtender ID="mpeCustomerEmail" runat="server" TargetControlID="btnfake1"
                            PopupControlID="pnlCustomerEmail" CancelControlID="btnCancel">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlCustomerEmail" runat="server" BackColor="White" Height="269px" Width="500px"
                            Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%;
                                background: #fff;" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">
                                        Customer Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        Customer Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                    ValidationGroup="vgEmail" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                    ValidationGroup="vgEmail"> </asp:RegularExpressionValidator>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="100" ValidationGroup="vgEmail" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="mpeCustomerEmailSold" runat="server" TargetControlID="btnfake"
                            PopupControlID="pnlCustomerEmailSold" CancelControlID="btnCancelEmailSold">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlCustomerEmailSold" runat="server" BackColor="White" Height="269px" Width="500px"
                            Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%;
                                background: #fff;" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">
                                        Customer Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        Customer Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailSold" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmailSold"
                                    ValidationGroup="vgEmailSold" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="emailcheck" ControlToValidate="txtEmailSold" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                    ValidationGroup="vgEmailSold"> </asp:RegularExpressionValidator>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSaveEmailSold" runat="server" Text="Save" OnClick="btnSaveEmailSold_Click" Width="100" ValidationGroup="vgEmailSold" />
                                        <asp:Button ID="btnCancelEmailSold" runat="server" Text="Cancel" Width="100" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Button ID="btnFakeNotSold" runat="server" style="display:none;" />
                        <ajaxToolkit:ModalPopupExtender ID="mp_notsold" runat="server" TargetControlID="btnFakeNotSold"
                            PopupControlID="pnlnotsold" CancelControlID="btnCancelnotsold">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlnotsold" runat="server" BackColor="White" Height="269px" Width="500px"
                            Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%;
                                background: #fff;" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">
                                        Not Sold Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        Follow Up Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfollowupdate" runat="server" CssClass="date" onkeypress="return false"
                                            MaxLength="30"></asp:TextBox>
                                        <label>
                                        </label>
                                        <%--<asp:RequiredFieldValidator ID="Requiredfollowupdate" runat="server" ControlToValidate="txtfollowupdate"
                                            ValidationGroup="notsold" ErrorMessage="Please Enter follow up date." ForeColor="Red"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        Status:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlstatus" runat="server" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true">
                                           <%-- <asp:ListItem Text="PTW est" Value="PTW est"></asp:ListItem>--%>
                                            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
                                            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
                                            <%--<asp:ListItem Text="EST-one legger" Value="EST-one legger"></asp:ListItem>--%>
                                            <asp:ListItem Text="Follow up" Value="Follow up"></asp:ListItem>
                                            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
                                            <asp:ListItem Text="cancelation-no rehash" Value="cancelation-no rehash"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 31%" colspan="2">
                                        <asp:CheckBox ID="chkSendMailNotSold" runat="server" Text="Send email to customer" Checked="true" />
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnsavenotsold" CommandName="Insert" runat="server" Text="Save" ValidationGroup="notsold"
                                            OnClick="btnNotSold_Click" Width="100" />
                                        <asp:Button ID="btnCancelnotsold" runat="server" Text="Cancel" Width="100" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="mp_sold" runat="server" TargetControlID="btnSold"
                            PopupControlID="pnlsold" CancelControlID="btnCancelsold">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlsold" runat="server" BackColor="White" Height="269px" Width="500px"
                            Style="display: none;position:fixed;">
                             <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;
                                background: #fff;" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">
                                        Sold Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        
                                    </td>
                                    <td>
                                        <asp:Label ID="lblcheck" runat="server" Text="Please Accept Terms & Conditions" ForeColor="Red"></asp:Label>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        Payment Mode:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpaymode" runat="server">
                                            <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                            <asp:ListItem Text="Cheque" Value="Check"></asp:ListItem>
                                            <asp:ListItem Text="Credit Card" Value="Credit Card"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        Amount($):
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumericKey(event);"
                                            MaxLength="20" ReadOnly="true" ViewStateMode="Disabled"></asp:TextBox>
                                        <asp:CheckBox ID="chkedit" runat="server" Text="Edit" />
                                        <label>
                                            <asp:Label ID="lblAmount" runat="server" Text="Please Enter Amount" ForeColor="Red" CssClass="hide"></asp:Label>
                                        </label>
                                    </td>
                                </tr>
                                <tr id="trauthpass" class="hide">
                               
                                 <td align="right" style="width: 31%">
                                        Admin Password:
                                    </td>
                                    <td>                                       
                                        <asp:TextBox ID="txtauthpass" runat="server" TextMode="Password"></asp:TextBox>
                                       <label>
                                            <asp:Label ID="lblPassword" runat="server" Text="Please Enter Password" ForeColor="Red" CssClass="hide"></asp:Label>
                                        </label>
                                            <asp:CustomValidator ID="CV" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                                    </td>
                                   
                                </tr>
                                <tr id="trcheque" class="hide">
                                    <td align="right" style="width: 31%">
                                        Check #:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtchequeno" runat="server" onkeypress="return isNumericKey(event);"
                                            MaxLength="50"></asp:TextBox>
                                        <label>
                                        
                                        </label>
                                    </td>
                                </tr>
                                <tr id="trcard" class="hide">
                                    <td align="right" style="width: 31%">
                                        Card Holder's Details:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcardholderNm" runat="server" MaxLength="200"></asp:TextBox>
                                        <label>
                                        </label>
                                    </td>
                                </tr>
                                <tr id="trsignature">
                                    <td align="right" style="width: 31%">
                                        <asp:CheckBox ID="chksignature" Checked="false" runat="server" />
                                    </td>
                                    <td>
                                        I Signed & Agreed on Terms & Conditions
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 31%" colspan="2">
                                        <asp:CheckBox ID="chkSendEmailSold" runat="server" Text="Send email to customer" Checked="true" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnsavesold" CommandName="Insert" runat="server" Text="Save" ValidationGroup="sold"
                                            OnClick="btnSold_Click" Width="100" />
                                        <asp:Button ID="btnCancelsold" runat="server" Text="Cancel" Width="100" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <asp:Literal ID="LiteralFooter" runat="server"></asp:Literal><br />
                    <asp:HiddenField ID="HiddenFieldtotalAmount" runat="server" />
                    <asp:HiddenField ID="hidDownPayment" runat="server" />
                      <asp:HiddenField ID="HidCV" runat="server" />
                      <asp:HiddenField ID="HidShutterProposal" runat="server" />
                </div>
            </div>
            <div id="tabs-2">
            </div>
        </div>
        <!-- Tabs endss -->
    </div>
</asp:Content>
