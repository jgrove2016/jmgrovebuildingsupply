<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Gutter.aspx.cs" Inherits="JG_Prospect.Sr_App.Product_Line.Gutter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript" >
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
            Details: Gutter</h1>
        <div class="form_panel_custom">
            <ul>
                <li style="width: 49%;">
                    <table id="tblcustom" runat="server" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="2">
                                <label>
                                    Customer: <span>*</span></label>
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
                                    New Gutter Material:
                                </label>
                                <asp:DropDownList ID="ddlNewgutterMat" runat="server" TabIndex="3">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Aluminium</asp:ListItem>
                                    <asp:ListItem>Copper</asp:ListItem>
                                    <asp:ListItem>Galvanized</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvGutterMaterial" runat="server" InitialValue="0" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlNewgutterMat" ErrorMessage="Select New Gutter Material"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Tear Off Material:
                                </label>
                                <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="5">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Aluminium</asp:ListItem>
                                    <asp:ListItem>Copper</asp:ListItem>
                                    <asp:ListItem>Galvanized</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="rfvProposalCost" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtProposalCost" ErrorMessage="Enter Proposal Cost"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Gutter Color:</label>
                                <asp:DropDownList ID="ddlGuttercolor" runat="server" TabIndex="7">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Royal Brown</asp:ListItem>
                                    <asp:ListItem>Dark Bronze</asp:ListItem>
                                    <asp:ListItem>Herringbone</asp:ListItem>
                                    <asp:ListItem>Brandywine</asp:ListItem>
                                    <asp:ListItem>Wicker</asp:ListItem>
                                    <asp:ListItem>Ivy Green</asp:ListItem>
                                    <asp:ListItem>Sandcastle</asp:ListItem>
                                    <asp:ListItem>Norwood</asp:ListItem>
                                    <asp:ListItem>Clay</asp:ListItem>
                                    <asp:ListItem>Heritage Blue</asp:ListItem>
                                    <asp:ListItem>Black</asp:ListItem>
                                    <asp:ListItem>80 White</asp:ListItem>
                                    <asp:ListItem>Almond</asp:ListItem>
                                    <asp:ListItem>Copper</asp:ListItem>
                                    <asp:ListItem>30 White</asp:ListItem>
                                    <asp:ListItem>Linen</asp:ListItem>
                                    <asp:ListItem>Pearl Gray</asp:ListItem>
                                    <asp:ListItem>Dark Gray</asp:ListItem>
                                    <asp:ListItem>Heather</asp:ListItem>
                                    <asp:ListItem>Ivory</asp:ListItem>
                                    <asp:ListItem>Terratone</asp:ListItem>
                                    <asp:ListItem>Grecian</asp:ListItem>
                                    <asp:ListItem>Cream</asp:ListItem>
                                    <asp:ListItem>Eggshell</asp:ListItem>
                                    <asp:ListItem>Scotch Red</asp:ListItem>
                                    <asp:ListItem>Musket</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="FuPicture" ErrorMessage="Select Atleast One Picture"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Downspout Install Footage:
                                </label>
                                <asp:TextBox ID="txtDownSpot" runat="server" MaxLength="35" TabIndex="9"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Inside Miter:
                                </label>
                                <asp:TextBox ID="txtInsideMiter" CssClass="required" runat="server" MaxLength="35"
                                    TabIndex="11"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Outside Miter:
                                </label>
                                <asp:TextBox ID="txtOutsideMiter" runat="server" MaxLength="35" TabIndex="13"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 65%;">
                                <label style="width: 54%;">
                                    Fascia board rotted/size:</label>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="100px" TabIndex="15">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>1"X6"</asp:ListItem>
                                    <asp:ListItem>1"X8"</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                                <%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="FuPicture" ErrorMessage="Select Atleast One Picture"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <label>
                                    Quantity:
                                </label>
                                <asp:TextBox ID="txtQuantity" runat="server" Width="50px" MaxLength="35" TabIndex="16"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    Gutter Extension lin ft:<br />
                                    (Optional)
                                </label>
                                <asp:TextBox ID="txtextension" runat="server" MaxLength="35" TabIndex="18"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Gutter Style:<span>*</span>
                                </label>
                                <asp:DropDownList ID="ddlGutterStyle" runat="server" TabIndex="2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>K Gutter</asp:ListItem>
                                    <asp:ListItem>Half Round</asp:ListItem>
                                    <asp:ListItem>Yankee</asp:ListItem>
                                    <asp:ListItem>Box Gutter </asp:ListItem>
                                    <asp:ListItem>T & M </asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvGutterStyle" runat="server" InitialValue="0" ForeColor="Red"
                                    ValidationGroup="save" ControlToValidate="ddlGutterStyle" ErrorMessage="Select Gutter Style"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Gutter Size:<span>*</span>
                                </label>
                                <asp:DropDownList ID="ddlGutterSize" runat="server" TabIndex="4">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>5"</asp:ListItem>
                                    <asp:ListItem>6"</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvGutterSize" runat="server" InitialValue="0" ForeColor="Red"
                                    ValidationGroup="save" ControlToValidate="ddlGutterSize" ErrorMessage="Select Gutter Size"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Gutter Install Footage
                                </label>
                                <asp:TextBox ID="txtGutterInstall" runat="server" MaxLength="35" TabIndex="6"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Tear Off Footage:
                                </label>
                                <asp:TextBox ID="txttearoff" runat="server" MaxLength="35" TabIndex="8"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Funnel:
                                </label>
                                <asp:TextBox ID="txtFunnel" runat="server" MaxLength="35" TabIndex="10"></asp:TextBox>
                                <label>
                                </label>
                                <%--<asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save" ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                                --%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Gutter Application<span>*</span> :
                                </label>
                                <asp:DropDownList ID="ddlGutterApplication" runat="server" TabIndex="12">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Flat Face Board-Hidden Hanger</asp:ListItem>
                                    <asp:ListItem>Extension hanger-reuse shenk/clips</asp:ListItem>
                                    <asp:ListItem>Extension hanger-replace shenk/clips</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvGutterAppli" runat="server" ForeColor="Red" ValidationGroup="save"
                                    InitialValue="0" ControlToValidate="ddlGutterApplication" ErrorMessage="Select Gutter Application"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Work Area: <span>*</span></label>
                                <asp:TextBox ID="txtworkarea" runat="server" MaxLength="35" TabIndex="14"></asp:TextBox>
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
                                <asp:FileUpload ID="flpUploadLoc_img" runat="server" TabIndex="17" onchange="readURL(this);" />
                                <label>
                                    Location Picture 2.<span>*</span></label>
                                <asp:FileUpload ID="FileUpload2" runat="server" TabIndex="17" onchange="readURL(this);" />
                                <label>
                                    Location Picture 3.</label>
                                <asp:FileUpload ID="FileUpload3" runat="server" TabIndex="17" onchange="readURL(this);" />
                                <label>
                                    Location Picture 4.</label>
                                <asp:FileUpload ID="FileUpload4" runat="server" TabIndex="17" onchange="readURL(this);" />
                                <label>
                                    Location Picture 5.</label>
                                <asp:FileUpload ID="FileUpload5" runat="server" TabIndex="17" onchange="readURL(this);" />
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
                     <%--   <tr>
                            <td>
                                <label>
                                    Location Picture: <span>*</span></label>
                                <asp:FileUpload ID="FuPicture" runat="server" class="multi" TabIndex="17" onchange="readURL(this);"/>
                                <asp:RequiredFieldValidator ID="rfvpicture" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="FuPicture" ErrorMessage="Select Atleast One Picture"></asp:RequiredFieldValidator>
                                        <asp:Image ID="imgplc_Loc" runat="server" Width="100%" Height="200" /> 
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <label>
                                    Special Instructions/ Exemptions:
                                </label>
                                <asp:TextBox ID="txtspecialIns" runat="server" TextMode="MultiLine" TabIndex="19"></asp:TextBox>
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
                    TabIndex="20" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" ValidationGroup="exit" TabIndex="21"
                    OnClick="btnexit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
