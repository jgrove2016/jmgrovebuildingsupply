<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Shutters.aspx.cs" Inherits="JG_Prospect.Sr_App.Shutters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link class="jsbin" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" class="jsbin" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type="text/javascript" class="jsbin" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/jquery-ui.min.js"></script>
    <script type="text/javascript">
        function uploadComplete() {
            if (Checkfiles() == true) {
                var btnImageUploadClick = document.getElementById("ctl00_ContentPlaceHolder1_btnImageUploadClick");
                btnImageUploadClick.click();
            }
        }
        function Checkfiles() {
            var fup = document.getElementById('ctl00_ContentPlaceHolder1_ajaxFileUpload_ctl02');
            var fileName = fup.value;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toString().toLowerCase();
            if (ext == "gif" || ext == "jpeg" || ext == "jpg" || ext == "tiff" || ext == "tif" || ext == "bmp" || ext == "png") {
                if ($('#ContentPlaceHolder1_hidCount').val().length != 0) {
                    if (parseInt($('#ContentPlaceHolder1_hidCount').val()) == 5) {
                        alert('You can not upload more than five image.');
                        $('#ctl00_ContentPlaceHolder1_ajaxFileUpload_ctl02').val('');
                        fup.focus();
                        return false;
                    }
                }
                return true;
            }
            else {
                alert('Upload Gif,JPG,JPEG,TIFF,TIF,PNG and BMP images only');
                $('#ctl00_ContentPlaceHolder1_ajaxFileUpload_ctl02').val('');
                fup.focus();
                return false;
            }
        }
        $(document).ready(function () {
            var counter = $('#ContentPlaceHolder1_hdncount').val();
            var ddl = $('#ContentPlaceHolder1_hdnddlaccessories').val();
            var ddlarray = ddl.split('#');
            var txtbx = $('#ContentPlaceHolder1_hdntxtqty').val();
            var txtbxarray = txtbx.split('#');


            for (var i = 1; i < counter; i++) {
                $('#add_accessories').before('<div style="margin-top:5px;" id="add_accessories_section' + i + '"><label> </label> <select name="accessories' + i + '" id="accessories' + i + '" style="width:30%;"><option value="Select">Select</option><option value="Hinge &amp; S.Hock">Hinge &amp; S.Hock</option></select> <label style="width:50px; text-align:right;">Quantity</label> <input name="qty' + i + '" type="text" value="' + $.trim(txtbxarray[i]) + '" onkeypress="return isNumericKey(event);" id="qty' + i + '" style="width:10%;"></div>');
                $('#accessories' + i).val(ddlarray[i]);

            };
            if (counter == 1) {
                $('#del_file').hide();
            }
            //$('#del_file').hide();
            $('#add_file').click(function () {
                $('#add_accessories').before('<div style="margin-top:5px;" id="add_accessories_section' + counter + '"><label> </label> <select name="accessories' + counter + '" id="accessories' + counter + '" style="width:30%;"><option value="Select">Select</option><option value="Hinge &amp; S.Hock">Hinge &amp; S.Hock</option></select> <label style="width:50px; text-align:right;">Quantity</label> <input name="qty' + counter + '" type="text" value="1" onkeypress="return isNumericKey(event);" id="qty' + counter + '" style="width:10%;"></div>');
                $('#del_file').fadeIn(0);
                $('#ContentPlaceHolder1_hdncount').val(counter);
                counter++;
            });
            $('#del_file').click(function () {
                if (counter == 2) {
                    $('#del_file').hide();
                }
                counter--;
                $('#add_accessories_section' + counter).remove();
                $('#ContentPlaceHolder1_hdncount').val(counter - 1);
            });
        });

        function readURL(input) {
            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray($(input).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Invalid formats are not allowed.");
                $(input).val("");
                return false;
            }
        }
       

        function ValidateImage() {
            var count = $('#<%=hidCount.ClientID %>').val();
            //alert(count);
            if (count < 2) {
                alert('Upload atleast two image.');
                return false;
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
    <style type="text/css">
        #add_file, #del_file
        {
            display: inline-block;
            font-weight: bold;
            font-size: 13px;
            margin-top: 10px;
        }
        #add_file
        {
            margin-right: 25px;
        }
        #add_file img, #del_file img
        {
            vertical-align: bottom;
            margin-right: 2px;
        }
    </style>
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
        <h1>
            Details: Shutters</h1>
        <div class="form_panel_custom">
         <span>
              <label>Customer Id: </label> <b> <asp:Label ID="lblmsg" runat="server" Visible="true"></asp:Label></b>
            </span>
            <ul>
                <li style="width: 49%;">
                    <table id="tblshutter" runat="server" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Customer: <span>*</span></label>
                                <asp:TextBox ID="txtCustomer" runat="server" MaxLength="35" onkeypress="return isAlphaKey(event);"
                                    onkeyup="javascript:Alpha(this)" TabIndex="1"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListElementID="auto_complete"
                                    UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetCustomerlist"
                                    TargetControlID="txtCustomer" EnableCaching="False">
                                </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Shutter Top:<span>*</span>

                                </label>
                                <asp:DropDownList ID="ddlShutterTop" runat="server" TabIndex="3" 
                                    AutoPostBack="True" onselectedindexchanged="ddlShutterTop_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%-- <input name="input" type="text" />--%>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Select Shutter Top." InitialValue="Select" ControlToValidate="ddlShutterTop"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Color<span>*</span>
                                    
                                </label>

                              
                                    <asp:UpdatePanel ID="pnlColor" runat="server" style="display:inline">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlColor" runat="server" TabIndex="5">
                                            <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlshuttername" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorColor" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Select Color." ControlToValidate="ddlColor"
                                    InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Surface of Mount<span>*</span></label>
                                <asp:DropDownList ID="ddlSurface_mount" runat="server" TabIndex="7">
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredSurfaceMount" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Select Surface Mount." ControlToValidate="ddlSurface_mount"
                                    InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Width</label>
                                    <asp:UpdatePanel ID="pnlWidth" runat="server" style="display:inline">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlWidth" runat="server" TabIndex="8">
                                            <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlshuttername" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlShutterTop" />
                                            </Triggers>
                                   </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Height</label>
                                <asp:TextBox ID="txtHeight" runat="server" MaxLength="35" TabIndex="9" onkeyup="javascript:Numeric(this)"></asp:TextBox>
                                <label>
                                    Note:</label>
                                    <asp:UpdatePanel ID="pnlHeightNote" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblHeight" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlshuttername" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Quantity<span>*</span></label>
                                <asp:TextBox ID="txtQuantity" runat="server" TabIndex="10" MaxLength="3" ValidationGroup="save" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredQuantity" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Enter Quantity." ControlToValidate="txtQuantity" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <%--<asp:UpdatePanel ID="pnlAccesories" runat="server">
                                <ContentTemplate>--%>
                                    <label>
                                        Accessories</label>
                                    <asp:DropDownList ID="ddlaccessories" runat="server" Style="width: 30%;" OnSelectedIndexChanged="ddlaccessories_SelectedIndexChanged"
                                        TabIndex="11">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Hinge &amp; S.Hock</asp:ListItem>
                                    </asp:DropDownList>
                                    <label style="width: 50px; text-align: right;">
                                        Quantity</label>
                                    <asp:TextBox ID="txtqty" runat="server" Style="width: 10%;" Text="1" onkeypress="return isNumericKey(event);"
                                        TabIndex="12"></asp:TextBox>
                                    <asp:HiddenField ID="hdncount" Value="1" runat="server" />
                                    <asp:HiddenField ID="hdnddlaccessories" runat="server" />
                                    <asp:HiddenField ID="hdntxtqty" runat="server" />
                                    <div id="add_accessories">
                                        <label>
                                        </label>
                                        <span id="add_file">
                                            <img src="../img/plus.png" alt="Add" />Add</span> <span id="del_file">
                                                <img src="../img/minus.png" alt="Delete" />Delete</span>
                                    </div>
                                 <%--</ContentTemplate>
                               </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Shutter Style<span>*</span></label>
                                <asp:DropDownList ID="ddlshuttername" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlshuttername_SelectedIndexChanged"
                                    TabIndex="2">
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredShutterType" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Select Shutter." ControlToValidate="ddlshuttername" InitialValue="Select"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Work Area<span>*</span></label>
                                <asp:TextBox ID="txtWork_area" runat="server" MaxLength="40" ValidationGroup="save" TabIndex="4"></asp:TextBox>
                                <%--<input name="input" type="text" />--%>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please enter Work area." ControlToValidate="txtWork_area" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style2">
                                    <tr >
                                        <td style="width:20%;">
                                            Location Image</td>
                                        <td style="width:60%;" >
                                        <asp:AsyncFileUpload ID="ajaxFileUpload" runat="server" ClientIDMode="AutoID"
                                                        OnUploadedComplete="ajaxFileUpload_UploadedComplete"
                                                        ThrobberID="imgLoad" CompleteBackColor="White" OnClientUploadComplete="uploadComplete" Style="width: 92% !important; margin-right: 6px" />
                                                    <asp:Button ID="btnImageUploadClick" ClientIDMode="AutoID" runat="server" CausesValidation="false" Text="hidden" Style="display: none" OnClick="btnImageUploadClick_Click" />
                                            <%--<asp:FileUpload ID="FileUpload1" runat="server" onchange="readURL(this);" />--%>
                                        </td>
                                    
                                        <td style="width:20%;">
                                            <%--<asp:Button ID="bntAdd" runat="server" Text="Attach"  Width="50px" onclick="bntAdd_Click" OnClientClick="return ValidateAddImage()" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        <asp:UpdatePanel ID="pnlUpdate" runat="server">
                                            <ContentTemplate>
                                             <asp:GridView runat="server" ID="grvShutter" AutoGenerateColumns="false" 
                                                 OnRowCommand="grvShutter_RowCommand" DataKeyNames="RowSerialNo" AllowPaging="true"
                                                 onrowdatabound="grvShutter_RowDataBound" PageSize="1" 
                                                 onpageindexchanging="grvShutter_PageIndexChanging">
                                                  <EmptyDataTemplate>
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="Image Not Found."></asp:Label>
                                                  </EmptyDataTemplate>
                                                  <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="90%" />
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Image" runat="server" Text="Image" Font-Bold="true"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Image ID="imglocation" runat="server" ImageUrl='<%#Eval("LocationPicture")%>' Height="100px" Width="100px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="10%" />
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkCategoryDelete" runat="server" Text="X" CommandArgument='<%#Eval("RowSerialNo")%>'
                                                                    CommandName="DeleteRec" CausesValidation="false" OnClientClick='javascript:return confirm("Are you sure want to delete this entry?");'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                             <asp:HiddenField ID="hidCount" runat="server" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Special Instructions / Exemptions</label>
                                <asp:TextBox ID="txtSpcl_inst" runat="server" Rows="5" TextMode="MultiLine" TabIndex="13"></asp:TextBox>
                                <label>
                                </label>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
            <asp:HiddenField ID="hidProdType" runat="server" />
                <asp:HiddenField ID="hidProdId" runat="server" />
                <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="save" OnClientClick="return ValidateImage()" OnClick="btnsave_Click"
                    TabIndex="14" />
                    <asp:Button ID="btndelete" runat="server" Text="Delete" TabIndex="15" 
                    Visible="false" onclick="btndelete_Click" OnClientClick="return ConfirmDelete()"  />
            </div>
        </div>
    </div>
</asp:Content>
