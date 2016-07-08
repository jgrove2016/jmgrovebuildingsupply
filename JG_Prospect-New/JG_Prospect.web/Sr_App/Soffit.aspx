<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Soffit.aspx.cs" Inherits="JG_Prospect.Sr_App.Soffit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var counter = $('#ContentPlaceHolder1_hdncount').val();
            var ddl = $('#ContentPlaceHolder1_hdntxtlength').val();
            var ddlarray = ddl.split('#');
            var txtbx = $('#ContentPlaceHolder1_hdntxtwidth').val();
            var txtbxarray = txtbx.split('#');


            for (var i = 1; i < counter; i++) {
                $('#add_hangsize').before('<div style="margin-top:5px;" id="add_hangsize_section' + i + '"><label style="width:203px; text-align:right;">Length</label><input name="length' + i + '" id="length' + i + '" type="text" style="width:20%;" onkeypress="return isNumericKey(event);" style="width:10%;"><label style="width:50px; text-align:right;">Width</label><input name="width' + i + '" type="text" value="' + $.trim(txtbxarray[i]) + '" onkeypress="return isNumericKey(event);" id="width' + i + '" style="width:10%;"></div>');
                $('#length' + i).val(ddlarray[i]);

            };
            if (counter == 1) {
                $('#del_file').hide();
            }
            //$('#del_file').hide();
            $('#add_file').click(function () {
                $('#add_hangsize').before('<div style="margin-top:5px;" id="add_hangsize_section' + counter + '"><label style="width:203px; text-align:right;">Length</label><input name="length' + counter + '" id="length' + counter + '" type="text" style="width:20%;" onkeypress="return isNumericKey(event);" style="width:10%;"><label style="width:50px; text-align:right;">Width</label><input name="width' + counter + '" type="text" value="" onkeypress="return isNumericKey(event);" id="width' + counter + '" style="width:10%;"></div>');

                $('#del_file').fadeIn(0);
                $('#ContentPlaceHolder1_hdncount').val(counter);
                counter++;
            });
            $('#del_file').click(function () {
                if (counter == 2) {
                    $('#del_file').hide();
                }
                counter--;
                $('#add_hangsize_section' + counter).remove();
                $('#ContentPlaceHolder1_hdncount').val(counter - 1);
            });
        });
        function readURL(input) {
            if (input.files && input.files[0]) {//Check if input has files.             
                var reader = new FileReader(); //Initialize FileReader.

                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_imgplc_Loc').attr('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
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
<!-- appointment tabs section end -->
        <h1>
            Details: Soffit</h1>
        <div class="form_panel_custom">
            <ul>
                <li style="width: 49%;">
                    <table id="tblcustom" runat="server" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="2">
                                <label>
                                    Customer:<span>*</span>
                                </label>
                                <asp:TextBox ID="txtCustomer" runat="server" MaxLength="35" onkeypress="return isAlphaKey(event);"
                                    onkeyup="javascript:Alpha(this)" TabIndex="1"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvcustomer" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtCustomer" ErrorMessage="Enter Customer Name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Over Hang Size: <span>*</span>
                                </label>
                                <br />
                                <label style="width: 203px; text-align: right;">
                                    Length</label><asp:TextBox ID="txtlength" runat="server" Style="width: 20%; display: inline-block;
                                        margin-right: 14px;" TabIndex="3"></asp:TextBox>
                                <label style="display: inline-block; width: 32px !important;">
                                    Width</label>
                                <asp:TextBox ID="txtwidth" runat="server" Style="width: 10%;" Text="" TabIndex="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvlength" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtlength" ErrorMessage="Enter Length" Style="margin-left: 146px;"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvwidth" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtwidth" ErrorMessage="Enter Width" Style="margin-left: 95px;"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdncount" Value="1" runat="server" />
                                <asp:HiddenField ID="hdntxtlength" runat="server" />
                                <asp:HiddenField ID="hdntxtwidth" runat="server" />
                                <div id="add_hangsize">
                                    <label>
                                    </label>
                                    <span id="add_file">
                                        <img src="../img/plus.png" alt="Add" />Add</span> <span id="del_file">
                                            <img src="../img/minus.png" alt="Delete" />Delete</span>
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Tear Off Footage: <span>*</span></label>
                                <asp:TextBox ID="txttearoff" runat="server" TabIndex="6"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvProposalCost" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txttearoff" ErrorMessage="Enter Tear Off Footage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Soffit Color:</label>
                                <asp:DropDownList ID="ddlsoffitcolor" runat="server" TabIndex="2">
                                </asp:DropDownList>
                                <label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Work Area: <span>*</span></label>
                                <asp:TextBox ID="txtworkarea" runat="server" MaxLength="35" TabIndex="5"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvworkarea" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtworkarea" ErrorMessage="Enter Work Area"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Location Picture 1.<span>*</span></label>
                                <asp:FileUpload ID="flpUploadLoc_img" runat="server" TabIndex="7" onchange="readURL(this);" />
                                <label>
                                    Location Picture 2.<span>*</span></label>
                                <asp:FileUpload ID="FileUpload2" runat="server" TabIndex="7" onchange="readURL(this);" />
                                <label>
                                    Location Picture 3.</label>
                                <asp:FileUpload ID="FileUpload3" runat="server" TabIndex="7" onchange="readURL(this);" />
                                <label>
                                    Location Picture 4.</label>
                                <asp:FileUpload ID="FileUpload4" runat="server" TabIndex="7" onchange="readURL(this);" />
                                <label>
                                    Location Picture 5.</label>
                                <asp:FileUpload ID="FileUpload5" runat="server" TabIndex="7" onchange="readURL(this);" />
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Select atleat 2 Location Pictures." ControlToValidate="flpUploadLoc_img"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="Requiredlocpicture2" runat="server" ValidationGroup="save"
                                    ErrorMessage="Please Select atleat 2 Location Pictures." ControlToValidate="FileUpload2"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:Image ID="imgplc_Loc" runat="server" Width="100%" Height="200" />
                            </td>
                        </tr>
                      <%--  <tr>
                            <td>
                                <label>
                                    Location Pictures:
                                </label>
                                <asp:FileUpload ID="FuPicture" runat="server" class="multi" TabIndex="7" onchange="readURL(this);" />
                                <label>
                                </label>
                                         <asp:RequiredFieldValidator ID="rfvpicture" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="FuPicture" ErrorMessage="Select Atleast One Picture"></asp:RequiredFieldValidator>
                                    <asp:Image ID="imgplc_Loc" runat="server" Width="100%" Height="200" />
                            </td>
                        </tr>--%>

                        <tr>
                            <td>
                                <label>
                                    Special Instructions / Exemptions:
                                </label>
                                <asp:TextBox ID="txtspecialIns" runat="server" TextMode="MultiLine" TabIndex="8"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvspecialIns" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="txtspecialIns" ErrorMessage="Enter Special Instruction"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="save" OnClick="btnsave_Click"
                    TabIndex="9" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" ValidationGroup="exit" TabIndex="10"
                    OnClick="btnexit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
