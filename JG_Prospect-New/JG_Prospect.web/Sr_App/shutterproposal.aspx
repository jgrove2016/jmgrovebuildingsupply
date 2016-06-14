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
                debugger;
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
                debugger;
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
        .autocomplete_listItem {
            background-color: #222 !important;
            color: #cfdbe6 !important;
            list-style-type: none !important;
            margin-top: 1px !important;
            border: 1px solid #DDD;
            border-radius: 3px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            font-size: 2px;
        }

        .autocomplete_highlightedListItem {
            background-color: #999 !important;
            color: #111 !important;
            list-style-type: none !important;
            font-size: 2px;
        }

        .autocomplete_completionListElement {
            list-style-type: none !important;
            font-size: 2px;
        }

        .grid td {
            line-height: 10px;
            padding: 3px;
            text-align: left;
            min-height: 5px;
            border: #ccc 1px solid;
            border-top: none;
            border-bottom: none;
            word-break: break-word;
        }

        #ContentPlaceHolder1_pnlpopup_backgroundElement, #ContentPlaceHolder1_pnlpopup_backgroundElement {
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
            <b>Customer Contract</b>

        </h1>

        <asp:Label ID="lblerrornew" runat="server"></asp:Label>

        <!-- Tabs starts -->
        <div id="tabs" style="background-color: #FFFFFF;">
            <%--  <ul>
                <li style="margin: 0;"><a href="#tabs-2">Customer</a></li>
            </ul>--%>
            <div id="tabs-1" style="background-color: #FFFFFF;">
                <div class="form_panel shutter_proposal" style="background-color: #FFFFFF; background: none;">
                    <asp:Literal ID="LiteralHeader" runat="server"></asp:Literal><br />

                    <asp:GridView ID="grdproductlines" Style="border-color: rgba(111,111,111,000) transparent transparent;" runat="server" CssClass="grid" Width="100%" AutoGenerateColumns="false"
                        PageSize="10" AllowPaging="true" OnRowDataBound="grdproductlines_RowDataBound" Visible="false">
                        <%----%>
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
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"><%----%>
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenFieldEstimate" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="HiddenFieldProduct" runat="server" Value='<%# Eval("productId") %>'></asp:HiddenField>

                                    <asp:HiddenField ID="HDAmountA" runat="server" Value=''></asp:HiddenField>
                                    <asp:HiddenField ID="HDAmountB" runat="server" Value=''></asp:HiddenField>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" class="no_line" style="font-family: tahoma,geneva,sans-serif; text-align: left; font-size: 10pt;">
                                        <tr>
                                            <td>
                                                <asp:Literal ID="LiteralBody" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" BackColor="Transparent"></HeaderStyle>
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                <%-- --%>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--style="background:url(../img/Logo_BG.png) center no-repeat;background-size: 41%;"--%>

                    <asp:GridView ID="grdCustom" runat="server" Style="border-color: rgba(111,111,111,000) transparent transparent;" CssClass="grid" Width="100%" AutoGenerateColumns="false"
                        PageSize="10" AllowPaging="true" OnRowDataBound="grdCustom_RowDataBound" Visible="false">
                        <%--Visible="false"--%>
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left"><%-- ItemStyle-VerticalAlign="Top"--%>
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenFieldEstimateCustom" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="HiddenFieldProduct" runat="server" Value='<%# Eval("productId") %>'></asp:HiddenField>

                                    <asp:HiddenField ID="HDAmountACustom" runat="server" Value=''></asp:HiddenField>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" class="no_line" style="font-family: tahoma,geneva,sans-serif; text-align: left; font-size: 10pt;">
                                        <tr align="left">
                                            <td align="justify">
                                                <asp:Literal ID="LiteralBodyCustom" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" BackColor="Transparent"></HeaderStyle>
                                <ItemStyle BackColor="Transparent" VerticalAlign="Top"></ItemStyle>
                                <%-- --%>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Literal ID="LiteralBody2" runat="server"></asp:Literal>
                    <div class="btn_sec">
                        <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Button ID="btnSold" runat="server" Text="Sold" TabIndex="1" OnClick="btnSold_Click2" />
                        <span>
                            <asp:LinkButton ID="btnNotSold" runat="server" Style="background: none; width: auto; height: auto; box-shadow: none; color: #0000ff; text-decoration: underline; font-size: 12px; font-weight: normal;"
                                Text="Not Sold" TabIndex="2"
                                OnClick="btnNotSold_Click1" />
                        </span>
                    </div>
                    <div>
                        <asp:Button ID="btnfake" runat="server" Style="display: none;" />
                        <asp:Button ID="btnfake1" runat="server" Style="display: none;" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeCustomerEmail" runat="server" TargetControlID="btnfake1"
                            PopupControlID="pnlCustomerEmail" CancelControlID="btnCancel">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlCustomerEmail" runat="server" BackColor="White" Height="269px" Width="500px"
                            Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background: #fff;"
                                cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">Customer Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Customer Email:
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
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background: #fff;"
                                cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">Customer Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Customer Email:
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
                        <asp:Button ID="btnFakeNotSold" runat="server" Style="display: none;" />
                        <ajaxToolkit:ModalPopupExtender ID="mp_notsold" runat="server" TargetControlID="btnFakeNotSold"
                            PopupControlID="pnlnotsold" CancelControlID="btnCancelnotsold">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlnotsold" runat="server" BackColor="White" Height="269px" Width="500px"
                            Style="display: none">
                            <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background: #fff;"
                                cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">Not Sold Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">Follow Up Date:
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
                                    <td align="right" style="width: 31%">Status:
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
                                    <td align="right" style="width: 31%">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkAddNotSoldEmail" OnClick="lnkAddNotSoldEmail_Click" customerid='<%=customerid %>' OnClientClick="window.open('customer_profile.aspx?Customerid='+this.getAttribute('customerid'));return false;" runat="server">Add Email</asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtNotSoldEmail" runat="server" placeholder="Email Id"
                                                    ViewStateMode="Disabled"></asp:TextBox>
                                                <asp:Panel ID="NotSoldEmails" runat="server" Style="width: 300px;">
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lnkbtnAdd" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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
                        <asp:Button ID="Button1" runat="server" Style="display: none;" />
                        <ajaxToolkit:ModalPopupExtender ID="mp_sold" runat="server" TargetControlID="Button1"
                            PopupControlID="pnlsold" CancelControlID="btnCancelsold">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlsold" runat="server" BackColor="White" Height="" Width="500px"
                            Style="display: none; position: fixed;">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background: #fff;"
                                        cellpadding="0" cellspacing="0">
                                        <tr style="background-color: #A33E3F">
                                            <td colspan="4" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                                align="center">Sold Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 31%"></td>
                                            <td colspan="3">
                                                <asp:Label ID="lblcheck" runat="server" Text="Please Accept Terms & Conditions" ForeColor="Red"></asp:Label>
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 31%">Payment Mode:
                                            </td>
                                            <td>
                                                <%--<asp:DropDownList ID="ddlpaymode" runat="server" >--%>


                                                <asp:DropDownList ID="ddlpaymode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpaymode_SelectedIndexChanged">
                                                    <%--<asp:ListItem Text="Select" Value="Select"></asp:ListItem> 
                                                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                                    <asp:ListItem Text="Debit/Check" Value="Debit"></asp:ListItem>
                                                    <asp:ListItem Text="Check/Escrow" Value="Escrow"></asp:ListItem>
                                                    <asp:ListItem Text="Financing" Value="Financing"></asp:ListItem>
                                                    <asp:ListItem Text="Checking/Saving" Value="Checking/Saving"></asp:ListItem>--%>
                                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="E-Check" Value="E-Check"></asp:ListItem>
                                                    <asp:ListItem Text="Credit/Debit Card (3% fee)" Value="CreditCard"></asp:ListItem>
                                                   
                                                    <asp:ListItem Text="Check/Escrow" Value="Cash/Escrow"></asp:ListItem>
                                                    <asp:ListItem Text="Financing" Value="Financing"></asp:ListItem>
                                                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                                </asp:DropDownList>

                                            </td>

                                            
                                            <td align="right" style="width: 31%">
                                            
                                                <asp:Label runat="server"  ID="lblMsg" Text="" />
                                            
                                                <asp:Label ID="lblPro" runat="server" Text="Promotional Code:"></asp:Label>
                                                <%--Promotional Code:--%>
                                                <asp:Label ID="lblPwd" runat="server" Text="Password" Visible="false"></asp:Label>
                                            </td>
                                            <td>

                                                <asp:TextBox ID="txtPromotionalcode" runat="server"
                                                    MaxLength="30" ViewStateMode="Disabled"></asp:TextBox>
                                                <asp:TextBox ID="txtPwd" runat="server" Visible="false" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <%-- <tr>
                                     <td align="right" style="width: 31%">
                                          
                                     </td> 
                                     <td>
                                       
                                     </td>                                               
                                 </tr>--%>

                                        <tr id="otheramount" runat="server">
                                            <td align="right" style="width: 31%">Amount($)<asp:Label ID="lblReqAmt" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" EnableViewState="true" onkeypress="return isNumericKey(event);"
                                                    MaxLength="20" ReadOnly="true"></asp:TextBox>
                                                <asp:CheckBox ID="chkedit" runat="server" Text="Edit"  onclick="if(this.checked) {ShowPopup();}" />
                                                <label>
                                                    <asp:Label ID="lblAmount" runat="server" Text="Please Enter Amount" ForeColor="Red" CssClass="hide"></asp:Label>
                                                </label>
                                            </td>
                                            <td colspan="2">
                                                <asp:RadioButton ID="rdoChecking" runat="server" Text="Checking" GroupName="checkSave" Checked="true" />
                                                &nbsp;
                                        <asp:RadioButton ID="rdoSaving" runat="server" Text="Saving" GroupName="checkSave" />
                                            </td>

                                        </tr>

                                        
                                       
                                        <!-- Cradit Card -->
                                       <tr id="Name" runat="server" visible="false">
                                            <td align="center" style="width: 31%">First Name<asp:Label ID="lblFirstName" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFirstName" MaxLength="40" runat="server" Height="20px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Enter First Name" ForeColor="Red" Display="Dynamic" ValidationGroup="CCsold"></asp:RequiredFieldValidator>
                                                  <asp:Label runat="server" Text="As displayed on card" Colon="False" ID="Label8" /></small>
                                            </td>
                                            <td align="center" style="width: 31%">Last Name<asp:Label ID="lblLastName" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLastName" Height="20px" runat="server" MaxLength="40"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Enter Last Name" ForeColor="Red" Display="Dynamic" ValidationGroup="CCsold"></asp:RequiredFieldValidator>
                                                <small>
                                                    <asp:Label runat="server" Text="As displayed on card" Colon="False" ID="lblLastNameMsg" /></small>
                                            </td>
                                        </tr>

                                        <tr id="Address" runat="server" visible="false">
                                            <td align="center" style="width: 31%">Address<asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <textarea name="txtAddress" rows="2" cols="15" id="txtAddress" style="height:33px;width:167px;" runat="server"></textarea>
                                                
                                            </td>
                                            <td align="right" style="width: 31%" id="labelAmount" visible="false" runat="server">Amount($)<asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td id="amountvalue" visible="false" runat="server">
                                                <asp:TextBox ID="txtccamount" runat="server" EnableViewState="true" onkeypress="return isNumericKey(event);"
                                                    MaxLength="20" ReadOnly="true"></asp:TextBox>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Edit" onclick="if(this.checked) {ShowPopup();}" />
                                                <label>
                                                    <asp:Label ID="Label14" runat="server" Text="Please Enter Amount" ForeColor="Red" CssClass="hide"></asp:Label>
                                                </label>
                                            </td>

                                        </tr>

                                         <tr id="CountryState" runat="server" visible="false">
                                            <td align="center" style="width: 31%">Country<asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                               <asp:DropDownList runat="server" ID="ddlCountry" AutoPostBack="true">
                                                    <asp:ListItem Text="US" Value="US" ></asp:ListItem>
                                                </asp:DropDownList>
                                               
                                                  
                                            </td>
                                            <td align="center" style="width: 31%">State<asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                 <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true">
                                                    <asp:ListItem Text="Pennsylvania" Value="Pennsylvania" ></asp:ListItem>
                                                </asp:DropDownList>
                                               
                                               
                                            </td>
                                        </tr>

                                        <tr id="CityZip" runat="server" visible="false">
                                            <td align="center" style="width: 31%">City<asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlCity" AutoPostBack="true">
                                                    <asp:ListItem Text="Malvern" Value="Malvern" ></asp:ListItem>
                                                </asp:DropDownList>
                                               
                                                 
                                            </td>
                                            <td align="center" style="width: 31%">Zip<asp:Label ID="Label17" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtZip" Height="20px" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip" ErrorMessage="Enter Zip" ForeColor="Red" Display="Dynamic" ValidationGroup="CCsold"></asp:RequiredFieldValidator>
                                               <asp:Label runat="server" Text="As displayed on card" Colon="False" ID="Label18" />
                                            </td>
                                        </tr>


                                       <tr id="Currency" runat="server" visible="false">
                                            <td align="center" style="width: 31%">Currency<asp:Label ID="lblCurrency" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlCurrency" AutoPostBack="true">
                                                    <asp:ListItem Text="U.S. Dollar" Value="USD" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <td align="center" style="width: 31%">Expiration Date <asp:Label ID="lblExpDate" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ccExpireMonth" runat="server">
                                                </asp:DropDownList>
                                                
                                                  <asp:DropDownList ID="ccExpireYear" runat="server">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>

                                        <tr id="Card" runat="server" visible="false">
                                            <td align="center" style="width: 31%">Card Number<asp:Label ID="lblCardNumber" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCardNumber" Height="20px" runat="server" MaxLength="17"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"  ControlToValidate="txtCardNumber" ErrorMessage="Enter Card Number" ForeColor="Red" Display="Dynamic" ValidationGroup="CCsold"></asp:RequiredFieldValidator>
                                            </td>


                                            <td align="center" style="width: 31%">Security Code <asp:Label ID="lblSecurityCode" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSecurityCode" TextMode="Password" Height="20px" runat="server" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSecurityCode" ErrorMessage="Enter Security Code" ForeColor="Red" Display="Dynamic" ValidationGroup="CCsold"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <!-- Cradit Card -->

                                        <asp:Panel ID="PanelHide" runat="server">

                                            <%--<tr>
                                                <td align="right" style="width: 31%">
                                                   
                                        Amount($)<asp:Label ID="lblReqAmt" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumericKey(event);"
                                                        MaxLength="20" ReadOnly="true" ViewStateMode="Disabled"></asp:TextBox>
                                                    <asp:CheckBox ID="chkedit" runat="server" Text="Edit" />
                                                    <label>
                                                        <asp:Label ID="lblAmount" runat="server" Text="Please Enter Amount" ForeColor="Red" CssClass="hide"></asp:Label>
                                                    </label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:RadioButton ID="rdoChecking" runat="server" Text="Checking" GroupName="checkSave" Checked="true" />
                                                    &nbsp;
                                        <asp:RadioButton ID="rdoSaving" runat="server" Text="Saving" GroupName="checkSave" />
                                                </td>

                                            </tr>--%>
                                            <tr>
                                                <td align="right" style="width: 31%">
                                                    <%--<asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label>--%>
                                        Bank<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <%-- <asp:DropDownList ID="ddlBanks" runat="server">
                                            <asp:ListItem Text="Select Bank" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Ally" Value="ally"></asp:ListItem>
                                            <asp:ListItem Text="Bank of America" Value="bofa"></asp:ListItem>
                                            <asp:ListItem Text="BB&T Bank" Value="bbt"></asp:ListItem>
                                            <asp:ListItem Text="Chase" Value="chase"></asp:ListItem>
                                            <asp:ListItem Text="Citibank" Value="citi"></asp:ListItem>
                                            <asp:ListItem Text="Charles Schwab" Value="schwab"></asp:ListItem>
                                            <asp:ListItem Text="Capital One 360" Value="capone360"></asp:ListItem>
                                            <asp:ListItem Text="Fidelity" Value="fidelity"></asp:ListItem>
                                            <asp:ListItem Text="First Tennessee" Value="firsttennessee"></asp:ListItem>
                                            <asp:ListItem Text="US Bank" Value="us"></asp:ListItem>
                                            <asp:ListItem Text="USAA" Value="usaa"></asp:ListItem>
                                            <asp:ListItem Text="Wells Fargo" Value="wells"></asp:ListItem>
                                            <asp:ListItem Text="PNC" Value="pnc"></asp:ListItem>
                                            <asp:ListItem Text="SunTrust" Value="suntrust"></asp:ListItem>
                                            <asp:ListItem Text="TD Bank" Value="td"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtBank" runat="server" CssClass="OnFocus_Cls" Height="20px" pleasholder="Name"
                                                        TabIndex="2" Width="179px"></asp:TextBox>

                                                    <ajaxToolkit:AutoCompleteExtender ID="tdsearchbyname_AutoCompleteExtender" runat="server"
                                                        ServiceMethod="GetCompletionList" MinimumPrefixLength="1" EnableCaching="true"
                                                        CompletionSetCount="1" CompletionInterval="1000" TargetControlID="txtBank">
                                                    </ajaxToolkit:AutoCompleteExtender>


                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBank" ErrorMessage="Enter Bank" ForeColor="Red" Display="Dynamic" ValidationGroup="sold"></asp:RequiredFieldValidator>
                                                    <%--<asp:TextBox ID="txtBank" runat="server"
                                            MaxLength="30" ViewStateMode="Disabled"></asp:TextBox>--%>
                                                </td>
                                                <td align="right" style="width: 31%">
                                                    <%--<asp:Label ID="lblAccNo" runat="server" Text="Account #"></asp:Label>--%>
                                        Account #<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMFAATRT" runat="server" Height="20px" onkeypress="return isNumericKey(event);"
                                                        MaxLength="10" ViewStateMode="Disabled"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMFAATRT" ErrorMessage="Enter Account #" ForeColor="Red" ValidationGroup="sold" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                   
                                    <td align="right" style="width: 31%">
                                        
                                    </td>
                                   
                                </tr>--%>

                                            <tr>
                                                <td align="right" style="width: 31%">
                                                    <%-- <asp:Label ID="lblRouting" runat="server" Text="Routing #"></asp:Label>--%>
                                        Routing #<asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoutingNo" MaxLength="10" runat="server" Height="20px" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRoutingNo" ErrorMessage="Enter Synapse User Name" ForeColor="Red" Display="Dynamic" ValidationGroup="sold"></asp:RequiredFieldValidator>
                                                    <%--<asp:TextBox ID="txtBank" runat="server"
                                            MaxLength="30" ViewStateMode="Disabled"></asp:TextBox>--%>
                                                </td>
                                                <td align="right" style="width: 31%">
                                                    <%--<asp:Label ID="lblSSN4" runat="server" Text="Last 4 SSN"></asp:Label>--%>
                                        Last 4 SSN<asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLASTSSN" TextMode="Password" Height="20px" runat="server" onkeypress="return isNumericKey(event);"
                                                        MaxLength="4" ViewStateMode="Disabled"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLASTSSN" ErrorMessage="Enter SSN" ForeColor="Red" Display="Dynamic" ValidationGroup="sold"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 31%">
                                                    <%--<asp:Label ID="lblDOB" runat="server" Text="D.O.B"></asp:Label>--%>
                                        D.O.B<asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDOB" Height="20px" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="calExt" runat="server" TargetControlID="txtDOB"></ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDOB" ErrorMessage="Enter Date of Birth" ForeColor="Red" Display="Dynamic" ValidationGroup="sold"></asp:RequiredFieldValidator>
                                                    <%--<asp:TextBox ID="txtBank" runat="server"
                                            MaxLength="30" ViewStateMode="Disabled"></asp:TextBox>--%>
                                                </td>
                                                <td align="right" style="width: 31%">
                                                    <%--<asp:Label ID="lblPerBus" runat="server" Text="Personal/Bussiness"></asp:Label>--%>
                                        Personal/Bussiness<asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlperbus" runat="server">
                                                        <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Personal" Value="Personal"></asp:ListItem>
                                                        <asp:ListItem Text="Bussiness" Value="Bussiness"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlperbus" ErrorMessage="Select Account Type" ForeColor="Red" InitialValue="0" Display="Dynamic" ValidationGroup="sold"></asp:RequiredFieldValidator>
                                                    <%----%>
                                                </td>
                                            </tr>

                                        </asp:Panel>
                                        <tr>
                                            <td align="right" style="width: 31%">
                                                <asp:UpdatePanel ID="upnlAdd" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkbtnAdd" OnClick="lnkbtnAdd_Click" OnClientClick="window.open('customer_profile.aspx?Customerid=<%=customerid %>');return false;" runat="server">Add Email</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td colspan="3">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtEmailId" runat="server" placeholder="Email Id"
                                                            ViewStateMode="Disabled"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmailId" ErrorMessage="Enter Email Id" ForeColor="Red" Display="Dynamic" ValidationGroup="sold"></asp:RequiredFieldValidator>
                                                        <asp:Panel ID="pnlControls" runat="server" Style="width: 300px;">
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkbtnAdd" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>

                                        </tr>
                                        <%--  <tr>
                                    <td align="right" style="width: 31%">
                                        Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSynapseEmail" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 31%">
                                        Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                        <tr id="trauthpass" class="hide">

                                            <td align="right" style="width: 31%">Admin Password:
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
                                            <td align="right" style="width: 31%">Check #:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtchequeno" runat="server" onkeypress="return isNumericKey(event);"
                                                    MaxLength="50"></asp:TextBox>
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr id="trcard" class="hide">
                                            <td align="right" style="width: 31%">Card Holder's Details:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcardholderNm" runat="server" MaxLength="200"></asp:TextBox>
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 31%">
                                                <asp:CheckBox ID="chkSendEmailSold" runat="server" Checked="true" />
                                            </td>
                                            <td>Send email to customer
                                            </td>
                                            <td align="right" colspan="2">
                                                <asp:CheckBox ID="chksignature" Checked="false" runat="server" />
                                                I Signed & Agreed on Terms & Conditions
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnsavesold" CommandName="Insert" runat="server" Text="Save" ValidationGroup="sold"
                                                    OnClick="btnSold_Click" Style="margin-left: -150px; margin-top: 25px" />
                                                <%--Width="100"--%>


                                                <asp:Button ID="btnSaveSold2" runat="server" OnClick="btnSaveSold2_Click" ValidationGroup="CCsold" Text="Save" Visible="false" Style="margin-left: -150px; margin-top: 25px" />
                                                <asp:Button ID="btnCancelsold" runat="server" Text="Cancel" Width="100" Style="margin-top: -15px" OnClick="btnCancelsold_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSaveSold2" />
                                    <asp:PostBackTrigger ControlID="btnsavesold" />
                                    
                                </Triggers>
                            </asp:UpdatePanel>
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




    <ajaxToolkit:ModalPopupExtender ID="mpChangeAmount" runat="server" TargetControlID="Button1"
        PopupControlID="pnlChangeAmt" CancelControlID="btnCancelsold">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlChangeAmt" runat="server" BackColor="White" Height="" Width="500px"
        Style="display: none; position: fixed;">
        <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%; background: #fff;"
            cellpadding="0" cellspacing="0">
            <tr style="background-color: #A33E3F">
                <td colspan="4" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                    align="center">Admin Details
                </td>
            </tr>


            <tr>

                <td>Password
                </td>
                <td align="right" style="width: 31%">
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSaveAdminDetails" CommandName="Insert" runat="server" Text="Save" ValidationGroup="sold"
                        Width="100" OnClick="btnSaveAdminDetails_Click" />
                    <asp:Button ID="btnCancelAdminDetails" runat="server" Text="Cancel" Width="100" OnClick="btnCancelAdminDetails_Click" />
                </td>
            </tr>
        </table>
        <input type="hidden" id="hdnAmount" runat="server" />
    </asp:Panel>
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        #mask
        {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup() {

            if (document.getElementById('<%=txtccamount.ClientID%>')) {
                $('#<%=txtccamount.ClientID%>').attr('readonly', 'readonly');
                $('#<%=txtChangeAmount.ClientID%>').focus();
                if (document.getElementById('<%=txtccamount.ClientID %>').value != '') {
                    document.getElementById('<%=txtChangeAmount.ClientID %>').value = document.getElementById('<%=txtccamount.ClientID %>').value;
                }
            }
            if (document.getElementById('<%=txtAmount.ClientID%>')) {
                $('#<%=txtAmount.ClientID%>').attr('readonly', 'readonly');
                $('#<%=txtChangeAmount.ClientID%>').focus();
                if (document.getElementById('<%=txtAmount.ClientID %>').value != '') {
                    document.getElementById('<%=txtChangeAmount.ClientID %>').value = document.getElementById('<%=txtAmount.ClientID %>').value;
                }
            }
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {

            $('#<%=txtChangeAmount.ClientID%>, #<%=txtadminCode.ClientID%>').val('');
            $('#<%=lblError.ClientID%>').text('');
            if (document.getElementById('<%=CheckBox1.ClientID %>')) { document.getElementById('<%=CheckBox1.ClientID %>').checked = false; }
            if (document.getElementById('<%=chkedit.ClientID %>')) { document.getElementById('<%=chkedit.ClientID %>').checked = false; }
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }
        function IsExists(pagePath, dataString, textboxid, errorlableid) {
            $.ajax({
                type: "POST",
                url: pagePath,
                data: dataString,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
          function (XMLHttpRequest, textStatus, errorThrown) {
              $(errorlableid).show();
              $(errorlableid).html("Error");
          },
                success:
          function (result) {
              if (result != null) {
                  var flg = (result.d);

                  if (flg == "True") {
                      $(errorlableid).show();
                      $(errorlableid).html('Verified');
                      if (document.getElementById('<%=txtccamount.ClientID %>')) { document.getElementById('<%=txtccamount.ClientID %>').value = document.getElementById('<%=txtChangeAmount.ClientID %>').value; }
                      if (document.getElementById('<%=txtAmount.ClientID %>')) { document.getElementById('<%=txtAmount.ClientID %>').value = document.getElementById('<%=txtChangeAmount.ClientID %>').value; }
                      document.getElementById('<%=hdnAmount.ClientID %>').value = document.getElementById('<%=txtChangeAmount.ClientID %>').value;
                      
                      $('#mask').hide();
                      $('#<%=pnlpopup.ClientID %>').hide();
                      if (document.getElementById('<%=CheckBox1.ClientID %>')) { document.getElementById('<%=CheckBox1.ClientID %>').checked = false; }
                      if (document.getElementById('<%=chkedit.ClientID %>')) { document.getElementById('<%=chkedit.ClientID %>').checked = false; }
                  }
                  else {
                      $(errorlableid).show();
                      $(errorlableid).html('failure');
                  }
              }
          }
            });
  }

  function focuslost() {
      if (document.getElementById('<%= txtChangeAmount.ClientID%>').value == '') {
                alert('Please enter proposal cost!');
                return false;
            }
            else if (document.getElementById('<%= txtadminCode.ClientID%>').value == '') {
                alert('Please enter admin code!');
                return false;
            }
            else {
                var pagePath = "Custom.aspx/Exists";
                var dataString = "{ 'value':'" + document.getElementById('<%= txtadminCode.ClientID%>').value + "' }";
                var textboxid = "#<%= txtadminCode.ClientID%>";
                var errorlableid = "#<%= lblError.ClientID%>";

                IsExists(pagePath, dataString, textboxid, errorlableid);
                return true;
            }
    }
    </script>
    <div id="mask">
    </div>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="175px" Width="300px"
        Style="z-index: 999999; background-color: White; position: absolute; left: 35%;
        top: 6%; border: outset 2px gray; padding: 5px; display: none">
        <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
            <tr style="background-color: #b5494c">
                <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                    align="center">
                    Admin Verification <a id="closebtn" style="color: white; float: right; text-decoration: none"
                        class="btnClose" href="#">X</a>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 45%; text-align: center;">
                    <asp:Label ID="LabelValidate" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 45%">
                    Amount:
                </td>
                <td>
                    <asp:TextBox ID="txtChangeAmount" runat="server" onkeypress="return isNumericKey(event);"
                        MaxLength="20" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Admin Password:
                </td>
                <td>
                    <asp:TextBox ID="txtadminCode" runat="server" TextMode="Password" Text=""></asp:TextBox>
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="button" class="btnVerify" value="Verify" onclick="javascript: return focuslost();" />
                    &nbsp;&nbsp;
                    <input type="button" class="btnClose" value="Cancel" onclick="javascript:HidePopup();" />
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
