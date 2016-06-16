<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Fascia.aspx.cs" Inherits="JG_Prospect.Sr_App.Fascia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var counter = $('#ContentPlaceHolder1_hdncount').val();
            var ddl = $('#ContentPlaceHolder1_hdnddlcap').val();
            var ddlarray = ddl.split('#');
            var txtbx = $('#ContentPlaceHolder1_hdntxtqty').val();
            var txtbxarray = txtbx.split('#');


            for (var i = 1; i < counter; i++) {
                $('#add_cap').before('<div style="margin-top:5px;" id="add_cap_section' + i + '"><label> </label> <select name="cap' + i + '" id="cap' + i + '" style="margin-left:1%;width:26%;"><option value="0">Select</option><option value="Window <101 U.I">Window <101 U.I</option><option value="Window >101 U.I">Window >101 U.I</option><option value="Entry door">Entry door</option><option value="Frend/sliding door">Frend/sliding door</option><option value="Single Gurage door">Single Gurage door</option><option value="double gurage door">double gurage door</option><option value="Curved window door">Curved window door</option><option value="Posts">Posts</option><option value="Other">Other</option></select> <label style="width:52px; margin-left:32px;">Quantity</label> <input name="qty' + i + '" type="text" value="' + $.trim(txtbxarray[i]) + '" onkeypress="return isNumericKey(event);" id="qty' + i + '" style="width:11%;"></div>');
                $('#cap' + i).val(ddlarray[i]);

            };
            if (counter == 1) {
                $('#del_file').hide();
            }
            //$('#del_file').hide();
            $('#add_file').click(function () {
                $('#add_cap').before('<div style="margin-top:5px;" id="add_cap_section' + counter + '"><label> </label> <select name="cap' + counter + '" id="cap' + counter + '" style="margin-left:1%;width:26%;"><option value="0">Select</option><option value="Window <101 U.I">Window <101 U.I</option><option value="Window >101 U.I">Window >101 U.I</option><option value="Entry door">Entry door</option><option value="Frend/sliding door">Frend/sliding door</option><option value="Single Gurage door">Single Gurage door</option><option value="double gurage door">double gurage door</option><option value="Curved window door">Curved window door</option><option value="Posts">Posts</option><option value="Other">Other</option></select> <label style="width:52px; margin-left:32px;">Quantity</label> <input name="qty' + counter + '" type="text" value="1" onkeypress="return isNumericKey(event);" id="qty' + counter + '" style="width:11%;"></div>');
                $('#del_file').fadeIn(0);
                $('#ContentPlaceHolder1_hdncount').val(counter);
                counter++;
            });
            $('#del_file').click(function () {
                if (counter == 2) {
                    $('#del_file').hide();
                }
                counter--;
                $('#add_cap_section' + counter).remove();
                $('#ContentPlaceHolder1_hdncount').val(counter - 1);
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
            Details: Fascia/Capping</h1>
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
                            <td colspan="2">
                                <label>
                                    Coil:
                                </label>
                                <asp:DropDownList ID="ddlcoil" runat="server" TabIndex="3">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Smooth</asp:ListItem>
                                    <asp:ListItem>PVC(Custom)</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txttearoff" ErrorMessage="Enter Tear Off Footage"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Tear Off Footage: <span>*</span></label>
                                <asp:TextBox ID="txttearoff" runat="server" onkeypress="return isNumericKey(event);"
                                    TabIndex="5"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvProposalCost" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txttearoff" ErrorMessage="Enter Tear Off Footage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Gutter Off/On Footage: <span>*</span></label>
                                <asp:TextBox ID="txtGutterOff" runat="server" onkeypress="return isNumericKey(event);"
                                    TabIndex="7"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvGutterOff" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtGutterOff" ErrorMessage="Enter Gutter Off/On Footage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr >
                          <%--  <td style="width: 66%;">
                                <label style="width: 54%;">
                                    Fascia board rotted/size:<span>*</span></label>
                                <asp:DropDownList ID="ddlfasciabaord" runat="server" Width="42%" TabIndex="10">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>1"X6"</asp:ListItem>
                                    <asp:ListItem>1"X8"</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                                </td>

                            <td>
                                <label style="width: 57px;">
                                    Quantity:<span>*</span>
                                </label>
                                <asp:TextBox ID="txtQuantity" runat="server" Width="50px" MaxLength="35" TabIndex="11"></asp:TextBox>
                                <label>
                                </label>
                            
                            </td>--%>
                             <td colspan="2" >
                                <label style="width:34%;">
                                    Fascia board rotted/size:<span>*</span></label>
                                <asp:DropDownList ID="ddlfasciabaord" runat="server"  Width="26%"  TabIndex="10">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>1"X6"</asp:ListItem>
                                    <asp:ListItem>1"X8"</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                               

                           <label style="width: 53px; margin-left:31px;">
                                    Quantity<span>*</span></label>
                                
                                <asp:TextBox ID="txtQuantity" runat="server" Width="50px"  MaxLength="35" TabIndex="11"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvrotted"   ForeColor="Red"  runat="server"   InitialValue="0" ErrorMessage="Select Fascia board rotted/size" ControlToValidate="ddlfasciabaord" ></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvquan"   ForeColor="Red"  runat="server"    ErrorMessage="Enter Quantity" ControlToValidate="txtQuantity" ></asp:RequiredFieldValidator>
                            
                             </td>
                        </tr>
              <tr>
                        
                       <td colspan="2" >
                                <label style="width:34%;">
                                    Additional Capping:<span>*</span></label>
                                <asp:DropDownList ID="ddlAdditionalCapping" runat="server"  Width="26%" 
                                    TabIndex="13">
                                      <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Window <101 U.I</asp:ListItem>
                                    <asp:ListItem>Window >101 U.I</asp:ListItem>
                                    <asp:ListItem>Entry door</asp:ListItem>
                                    <asp:ListItem>Frend/sliding door</asp:ListItem>
                                    <asp:ListItem>Single Gurage door</asp:ListItem>
                                    <asp:ListItem>double gurage door</asp:ListItem>
                                    <asp:ListItem>Curved window door</asp:ListItem>
                                    <asp:ListItem>Posts</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                                <label style="width: 53px; margin-left:31px;">
                                    Quantity<span>*</span></label>
                                <asp:TextBox ID="txtqty" runat="server" Width="50px"  Text="1" onkeypress="return isNumericKey(event);"
                                    TabIndex="14"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfvcapping"   ForeColor="Red"  runat="server"   InitialValue="0" ErrorMessage="Select Additional Capping" ControlToValidate="ddlAdditionalCapping" ></asp:RequiredFieldValidator>
                           
                                <asp:HiddenField ID="hdncount" Value="1" runat="server" />
                                <asp:HiddenField ID="hdnddlcap" runat="server" />
                                <asp:HiddenField ID="hdntxtqty" runat="server" />
                                <div id="add_cap">
                                    <label>
                                    </label>
                                    <span id="add_file">
                                        <img src="../img/plus.png" alt="Add" />Add</span> <span id="del_file">
                                            <img src="../img/minus.png" alt="Delete" />Delete</span>
                                </div>
                                </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="2">
                                <label>
                                    Coil/Trim Nails/Caulk color:<span>*</span></label>
                                <asp:DropDownList ID="ddlcolor" runat="server" TabIndex="2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Colonial White</asp:ListItem>
                                    <asp:ListItem>Bright White</asp:ListItem>
                                    <asp:ListItem>Snow White</asp:ListItem>
                                    <asp:ListItem>Lomar White</asp:ListItem>
                                    <asp:ListItem>Sandstone Beige</asp:ListItem>
                                    <asp:ListItem>Pearl Bone</asp:ListItem>
                                    <asp:ListItem>Linen</asp:ListItem>
                                    <asp:ListItem>Heritage Cream</asp:ListItem>
                                    <asp:ListItem>Autumn Yellow</asp:ListItem>
                                    <asp:ListItem>Wheat</asp:ListItem>
                                    <asp:ListItem>Herringbone</asp:ListItem>
                                    <asp:ListItem>Almond</asp:ListItem>
                                    <asp:ListItem>Light Maple</asp:ListItem>
                                    <asp:ListItem>Sandpiper</asp:ListItem>
                                    <asp:ListItem>Desert Tan</asp:ListItem>
                                    <asp:ListItem>Linen</asp:ListItem>
                                    <asp:ListItem>SilverAsh</asp:ListItem>
                                    <asp:ListItem>Green Tea</asp:ListItem>
                                    <asp:ListItem>Bermuda Blue</asp:ListItem>
                                    <asp:ListItem>Saddle</asp:ListItem>
                                    <asp:ListItem>Harbor Blue</asp:ListItem>
                                    <asp:ListItem>Sterling Gray</asp:ListItem>
                                    <asp:ListItem>Glacier Blend</asp:ListItem>
                                    <asp:ListItem>Savannah Wicker</asp:ListItem>
                                    <asp:ListItem>Rye</asp:ListItem>
                                    <asp:ListItem>Buckskin</asp:ListItem>
                                    <asp:ListItem>Oxford Blue</asp:ListItem>
                                    <asp:ListItem>Pearl Gray</asp:ListItem>
                                    <asp:ListItem>Natural Clay</asp:ListItem>
                                    <asp:ListItem>Granite Gray</asp:ListItem>
                                    <asp:ListItem>Pebblestone Clay</asp:ListItem>
                                    <asp:ListItem>Seagrass</asp:ListItem>
                                    <asp:ListItem>Cypress</asp:ListItem>
                                    <asp:ListItem>Suede</asp:ListItem>
                                    <asp:ListItem>Greysctone</asp:ListItem>
                                    <asp:ListItem>Charcoal Gray</asp:ListItem>
                                    <asp:ListItem>Spruce</asp:ListItem>
                                    <asp:ListItem>Mountain Cedar</asp:ListItem>
                                    <asp:ListItem>Hearth stone</asp:ListItem>
                                    <asp:ListItem>Flag stone</asp:ListItem>
                                    <asp:ListItem>Terra Cotta</asp:ListItem>
                                    <asp:ListItem>Pacific Blue</asp:ListItem>
                                    <asp:ListItem>Ivy Green</asp:ListItem>
                                    <asp:ListItem>Autumn Red</asp:ListItem>
                                    <asp:ListItem>Lighthouse Red</asp:ListItem>
                                    <asp:ListItem>Sable Brown</asp:ListItem>
                                    <asp:ListItem>Terra Bronze</asp:ListItem>
                                    <asp:ListItem>Forest</asp:ListItem>
                                    <asp:ListItem>Regatta</asp:ListItem>
                                    <asp:ListItem>Tuxedo Gray</asp:ListItem>
                                    <asp:ListItem>Grecian Green</asp:ListItem>
                                    <asp:ListItem>Brown</asp:ListItem>
                                    <asp:ListItem>Royal Brown</asp:ListItem>
                                    <asp:ListItem>Musket Brown C</asp:ListItem>
                                    <asp:ListItem>Musket Brown A</asp:ListItem>
                                    <asp:ListItem>Musket Brown</asp:ListItem>
                                    <asp:ListItem>Dark Bronze</asp:ListItem>
                                    <asp:ListItem>AMP Dark Bronze</asp:ListItem>
                                    <asp:ListItem>Black</asp:ListItem>
                                   
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvcolor" runat="server" InitialValue="0" ForeColor="Red"
                                    ValidationGroup="save" ControlToValidate="ddlcolor" ErrorMessage="Select Coil/Trim Nails/Caulk color"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Fascia Install Footage: <span>*</span></label>
                                <asp:TextBox ID="txtfasciaInstall" runat="server" MaxLength="35" TabIndex="4"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvFasciaInstall" runat="server" ForeColor="Red"
                                    ValidationGroup="save" ControlToValidate="txtfasciaInstall" ErrorMessage="Enter Fascia Install Footage "></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Barge Board Install Footage: <span>*</span></label>
                                <asp:TextBox ID="txtBargeBoard" runat="server" MaxLength="35" TabIndex="6"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvBargeBoard" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtBargeBoard" ErrorMessage="Enter Barge Board Install Footage "></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Rotten Wood:
                                </label>
                                <asp:CheckBox ID="chbYes" runat="server" Width="14%" Text="Yes " TabIndex="8" Checked="true"
                                    onclick="fnCheckOne(this)" />
                                <asp:CheckBox ID="chbNo" runat="server" Width="14%" Text="No " TabIndex="9" onclick="fnCheckOne(this)" />
                                <label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Work Area: <span>*</span></label>
                                <asp:TextBox ID="txtworkarea" runat="server" MaxLength="35" TabIndex="12"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvworkarea" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtworkarea" ErrorMessage="Enter Work Area"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
<%--
                        <tr>
                            <td colspan="2">
                                <label>
                                    Location Pictures:
                                </label>
                                <asp:FileUpload ID="FuPicture" runat="server" class="multi" TabIndex="15"  onchange="readURL(this);"/>
                                   <asp:RequiredFieldValidator ID="rfvpicture" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="FuPicture" ErrorMessage="Select Atleast One Picture"></asp:RequiredFieldValidator>
                                       <asp:Image ID="imgplc_Loc" runat="server" Width="100%" Height="200" /> 
                            </td>
                        </tr>--%>
                           <tr>
                            <td>
                                <label>
                                    Location Picture 1.<span>*</span></label>
                                <asp:FileUpload ID="flpUploadLoc_img" runat="server" TabIndex="15" onchange="readURL(this);" />
                                <label>
                                    Location Picture 2.<span>*</span></label>
                                <asp:FileUpload ID="FileUpload2" runat="server" TabIndex="15" onchange="readURL(this);" />
                                <label>
                                    Location Picture 3.</label>
                                <asp:FileUpload ID="FileUpload3" runat="server" TabIndex="15" onchange="readURL(this);" />
                                <label>
                                    Location Picture 4.</label>
                                <asp:FileUpload ID="FileUpload4" runat="server" TabIndex="15" onchange="readURL(this);" />
                                <label>
                                    Location Picture 5.</label>
                                <asp:FileUpload ID="FileUpload5" runat="server" TabIndex="15" onchange="readURL(this);" />
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
                        <tr>
                            <td colspan="2">
                                <label>
                                    Special Instruction:
                                </label>
                                <asp:TextBox ID="txtspecialIns" runat="server" TextMode="MultiLine" TabIndex="16"></asp:TextBox>
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
                    TabIndex="17" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" ValidationGroup="exit" TabIndex="18"
                    OnClick="btnexit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
