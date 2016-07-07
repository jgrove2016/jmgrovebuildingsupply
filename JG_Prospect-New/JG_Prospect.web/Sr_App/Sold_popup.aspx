<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sold_popup.aspx.cs" Inherits="JG_Prospect.Sr_App.Sold_popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" >

        $(document).ready(function () {
            $(".date").datepicker();
            $('.time').ptTimeSelect();
            $('#trcheque').hide();
            $('#trcard').hide();
            $('#btnsavesold').hide();
        });


        $(document).change(function () {
            if ($('#ddlpaymode').val() == "Cash") {
                $('#trcheque').hide();
                $('#trcard').hide();
            }
            else if ($('#ddlpaymode').val() == "Check") {
                $('#trcheque').show();
                $('#trcard').hide();
            }
            else if ($('#ddlpaymode').val() == "Credit Card") {
                $('#trcheque').hide();
                $('#trcard').show();
            }
            if ($('#chksignature').is(':checked') == true) {
                $('#lblcheck').hide();
                $('#btnsavesold').show();
            }
            else {
                $('#lblcheck').show();
                $('#btnsavesold').hide();
            }

            if ($('#chkedit').is(':checked') == true) {
                $('#trauthpass').show();
            }
            else {
                $('#trauthpass').hide();
            }
        });

        function okay() {
            window.parent.document.getElementById('btnsavesold').click();
        }
        function cancel() {
            window.parent.document.getElementById('btnCancelsold').click();
        }
        debugger;

    function IsExists(pagePath, dataString, textboxid, errorlableid) {
            $.ajax({
                type: "POST",
                url: "../shutterproposal.aspx/Comparecode",
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
              var flg = true;
              if (result != null) {
                  flg = result.d;
                  if (flg == "true") {
                      $('#lblError').text() = 'Valid';
                  }
                  else {
                      $('#lblError').text() = 'InValid!';
                  }
              }
          }
            });
        }
        function focuslost() {
            var pagePath = window.location.pathname + "/IsExists";
            var dataString = "{ 'value':'" + $("#<%= txtauthpass.ClientID%>").val() + "' }";
            var textboxid = "#<%= txtauthpass.ClientID%>";
            var errorlableid = "#<%= lblError.ClientID%>";
            IsExists(pagePath, dataString, textboxid, errorlableid);
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%;
                                background: #fff;" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #A33E3F">
                                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                        align="center">
                                        Sold Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 31%">
                                        <label>
                                        </label>
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
                                            <asp:ListItem Text="Check" Value="Check"></asp:ListItem>
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
                                            MaxLength="20"></asp:TextBox>
                                        <asp:CheckBox ID="chkedit" runat="server" Text="Edit" />
                                        <label>
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredAmount" runat="server" ControlToValidate="txtAmount"
                                            ValidationGroup="sold" ErrorMessage="Please Enter Amount." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trauthpass">
                                    <td align="right" style="width: 31%">
                                        Admin Password:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtauthpass" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:Label ID="lblError" runat="server" Style="display: none;" Text="valid!"></asp:Label>
                                        <%-- <asp:Button id="btnchk" OnClientClick="javascript:CompareCode()" runat="server" Text="Check" ></asp:Button>--%>
                                    </td>
                                </tr>
                                <tr id="trcheque">
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
                                <tr id="trcard">
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
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnsavesold" CommandName="Insert" runat="server" Text="Save" ValidationGroup="sold"
                                            onclick="okay();" Width="100" />
                                        <asp:Button ID="btnCancelsold" runat="server" Text="Cancel" Width="100" onclick="cancel();"  />
                                    </td>
                                </tr>
                            </table>
    </div>
    </form>
</body>
</html>
