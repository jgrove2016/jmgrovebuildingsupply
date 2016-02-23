<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Custom_MaterialList.aspx.cs" Inherits="JG_Prospect.Sr_App.Custom_MaterialList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
      <script src="../js/jquery-latest.js" type="text/javascript"></script>
  <script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>--%>
    <%-- <script type="text/javascript">
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
                      document.getElementById('<%= txtauthpass.ClientID %>').value;
                      SaveandSendMail();
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
            if (document.getElementById('<%= txtauthpass.ClientID%>').value == '') {
                alert('Please enter admin code!');
                return false;
            }
            else {
                var pagePath = "Custom_MaterialList.aspx/Exists";
                var dataString = "{ 'value':'" + document.getElementById('<%= txtauthpass.ClientID%>').value + "' }";
                var textboxid = "#<%= txtauthpass.ClientID%>";
                var errorlableid = "#<%= lblError.ClientID%>";

                IsExists(pagePath, dataString, textboxid, errorlableid);
                return true;
            }
        }

        $(".btnClose").live('click', function () {


            $('#<%=txtauthpass.ClientID %>, #<%=lblError.ClientID %>').val('');

            HidePopup();
        });

    </script>--%>
    <%--  var ddlCategory;
        function GetVendorCategories() {
            ddlCategory = document.getElementById("<%=ddlVendorCategory.ClientID %>");
            ddlCategory.options.length = 0;
            AddOption("Loading", "0");
            PageMethods.GetVendorCategories(OnSuccess);
        }

        window.onload = GetVendorCategories;

        function OnSuccess(response) {
            ddlCategory.options.length = 0;
            AddOption("select", "0");
            for (var i in response) {
                AddOption(response[i].Name, response[i].value);
            }
        }
        function AddOption(text, value) {
            var option = document.createElement('<option value="' + value + '">');
            ddlCategory.options.add(option);
            option.innerText = text;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
         <h1 id="h1Heading" runat="server">Material List</h1>
           
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:LinkButton ID="lnkForemanPermission" runat="server" Text="Foreman Permission"></asp:LinkButton>
                    <asp:LinkButton ID="lnkAdminPermission" runat="server" Text="Admin Permission" Visible="false"></asp:LinkButton>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:LinkButton ID="lnkSrSalesmanPermissionA" runat="server" Text="Sr. Salesman Permission"
                        Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="lnkSrSalesmanPermissionF" runat="server" Text="Sr. Salesman Permission"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnAdmin" runat="server" />
        <asp:HiddenField ID="hdnForeman" runat="server" />
        <asp:HiddenField ID="hdnSrA" runat="server" />
        <asp:HiddenField ID="hdnSrF" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="popupAdmin_permission" TargetControlID="lnkAdminPermission"
            runat="server" CancelControlID="btnCloseAdmin" PopupControlID="pnlpopup">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="175px" Width="300px"
            Style="z-index: 111; background-color: White; position: absolute; left: 35%;
            top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Admin Verification
                        <asp:Button ID="btnXAdmin" runat="server" OnClick="btnXAdmin_Click" Text="X" Style="float: right;
                            text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="LabelValidate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Admin Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVAdmin" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifyAdmin" runat="server" Text="Verify" OnClick="VerifyAdminPermission" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseAdmin" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popupSrSalesmanPermissionA" TargetControlID="lnkSrSalesmanPermissionA"
            runat="server" CancelControlID="btnCloseSrSalesmanA" PopupControlID="pnlSrSalesmanPermissionA">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlSrSalesmanPermissionA" runat="server" BackColor="White" Height="175px"
            Width="300px" Style="z-index: 111; background-color: White; position: absolute;
            left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Sr. Salesman Verification
                        <asp:Button ID="btnXSrSalesmanA" runat="server" OnClick="btnXSrSalesmanA_Click" Text="X"
                            Style="float: right; text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Sr. Salesman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSrSalesmanPasswordA" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVSrSalesmanA" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifySrSalesmanA" runat="server" Text="Verify" OnClick="VerifySrSalesmanPermissionA" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseSrSalesmanA" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popupForeman_permission" TargetControlID="lnkForemanPermission"
            runat="server" CancelControlID="btnCloseForeman" PopupControlID="pnlForemanPermission">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlForemanPermission" runat="server" BackColor="White" Height="175px"
            Width="300px" Style="z-index: 111; background-color: White; position: absolute;
            left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Foreman Verification
                        <asp:Button ID="btnXForeman" runat="server" OnClick="btnXForeman_Click" Text="X"
                            Style="float: right; text-decoration: none" /><%--<a id="A1" style="color: white; float: right; text-decoration: none"
                            class="btnClose" href="#">X</a>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Foreman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtForemanPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblErrorForeman" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVForeman" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifyForeman" runat="server" Text="Verify" OnClick="VerifyForemanPermission" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseForeman" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popupSrSalesmanPermissionF" TargetControlID="lnkSrSalesmanPermissionF"
            runat="server" CancelControlID="btnCloseSrSalesmanF" PopupControlID="pnlSrSalesManPermissionF">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlSrSalesManPermissionF" runat="server" BackColor="White" Height="175px"
            Width="300px" Style="z-index: 111; background-color: White; position: absolute;
            left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Sr. Salesman Verification
                        <asp:Button ID="btnXSrSalesmanF" runat="server" OnClick="btnXSrSalesmanF_Click" Text="X"
                            Style="float: right; text-decoration: none" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 45%; text-align: center;">
                        <asp:Label ID="Label4" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Sr. Salesman Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSrSalesmanPasswordF" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        <asp:CustomValidator ID="CVSrSalesmanF" runat="server" ErrorMessage="Invalid Password"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnVerifySrSalesmanF" runat="server" Text="Verify" OnClick="VerifySrSalesmanPermissionF" />
                        <%-- <input type="button" class="btnVerify" value="Verify" runat="server" onclick="btnSendMail_Click"/>--%>
                        &nbsp;&nbsp;
                        <input type="button" id="btnCloseSrSalesmanF" class="btnClose" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="grid">
            <asp:GridView ID="grdcustom_material_list" runat="server" Width="108%" AutoGenerateColumns="false"
                OnRowDataBound="grdcustom_material_list_RowDataBound" OnRowDeleting="grdcustom_material_list_RowDeleting" OnRowCommand="grdcustom_material_list_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No.">
                        <ItemTemplate>
                            <asp:Label ID="lblsrno" Text="0" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material List">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMateriallist" Text='<%#Eval("MaterialList")%>' TextMode="MultiLine"
                                runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor Category">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlVendorCategory" ClientIDMode="Static" runat="server" Width="150px"
                                OnSelectedIndexChanged="ddlVendorCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="requiredvendorcategory" Display="Dynamic" runat="server"
                                InitialValue="0" ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlVendorCategory">
                            </asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor Name">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlVendorName" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged"
                                ClientIDMode="Static" runat="server" Width="150px" Enabled="false" AutoPostBack="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Quote">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkQuote" runat="server" Text='<%#Eval("DocName")%>' CommandArgument='<%#Eval("TempName") %>' CommandName="View" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount($)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text="0.00" onkeypress="return isNumericKey(event);"
                                MaxLength="15" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="Add_Click"></asp:LinkButton>
                            <label>
                                &nbsp;</label>
                            <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
                            <asp:HiddenField ID="hdnMaterialListId" runat="server" Value='<%#Eval("Id")%>'/>
                            <asp:HiddenField ID="hdnEmailStatus" runat="server" Value='<%#Eval("EmailStatus")%>'/>
                            <asp:HiddenField ID="hdnForemanPermission" runat="server"  Value='<%#Eval("IsForemanPermission")%>'/>
                            <asp:HiddenField ID="hdnSrSalesmanPermissionF" runat="server" Value='<%#Eval("IsSrSalemanPermissionF")%>'/>
                            <asp:HiddenField ID="hdnAdminPermission" runat="server" Value='<%#Eval("IsAdminPermission")%>'/>
                            <asp:HiddenField ID="hdnSrSalesmanPermissionA" runat="server" Value='<%#Eval("IsSrSalemanPermissionA")%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="btn_sec">
            <asp:Button ID="btnSendMail" runat="server" Text="Save" OnClick="btnSendMail_Click"
                Style="background: url(../img/btn1.png) no-repeat;" Width="300" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
        </div>
        <h1>
            Edit Email Templates</h1>
        <div>
            <label>
                &nbsp;</label>
            <%--<a href="EmailTemplateForVendorCategories.aspx" title="Edit Email Template for vendor category">
               Edit Email Template for vendor category</a>--%>
            <asp:LinkButton runat="server" ID="lnkVendorCategory" OnClick="lnkVendorCategory_Click"
                CausesValidation="false">Edit Email Template for vendor category</asp:LinkButton>
            <br />
            <label>
                &nbsp;</label>
            <%-- <a href="EmailTemplateForVendorCategories.aspx" title="Edit Email Template for vendor">
                Edit Email Template for vendor</a>--%>
            <asp:LinkButton runat="server" ID="lnkVendor" OnClick="lnkVendor_Click" CausesValidation="false">Edit Email Template for vendor</asp:LinkButton>
            <%-- <iframe src="EmailTemplateForVendorCategories.aspx" width="100%" height="100%" id="ifEmailTemplate" runat="server">
        </iframe>--%>
            <asp:Panel ID="pnlEmailTemplateForVendorCategories" runat="server">
                <h2 style="text-align: center">
                    <b>Email Template For Vendor Category</b></h2>
                <div>
                    <h2>
                        Header Template</h2>
                    <cc1:Editor ID="HeaderEditor" Width="1000px" Height="200px" runat="server" />
                    <h2>
                        Body Template</h2>
                    <asp:Label ID="lblMaterials" runat="server"></asp:Label>
                    <h2>
                        Footer Template</h2>
                    <cc1:Editor ID="FooterEditor" Width="1000px" Height="200px" runat="server" />
                </div>
                <br />
                <br />
                <div class="btn_sec">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlEmailTemplateForVendors" runat="server" Visible="false">
            <h2 style="text-align: center">
                <b>Email Template For Vendors</b></h2>
            <div>
                <h2>
                    Header Template</h2>
                <cc1:Editor ID="HeaderEditorVendor" Width="1000px" Height="200px" runat="server" />
                <h2>
                    Body Template</h2>
                <asp:Label ID="lblMaterialsVendor" runat="server"></asp:Label>
                <h2>
                    Footer Template</h2>
                <cc1:Editor ID="FooterEditorVendor" Width="1000px" Height="200px" runat="server" />
            </div>
            <br />
            <br />
            <div class="btn_sec">
                <asp:Button ID="btnUpdateVendor" runat="server" Text="Update" OnClick="btnUpdateVendor_Click" />
            </div>
        </asp:Panel>
    </div>
    <%--<ajaxToolkit:ModalPopupExtender ID="popup_permission" TargetControlID="btnSendMail"
            runat="server" CancelControlID="btnClose1" PopupControlID="pnlpopup">
        </ajaxToolkit:ModalPopupExtender>--%>
    </div>
</asp:Content>
