<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Gutter_Guards.aspx.cs" Inherits="JG_Prospect.Sr_App.Product_Line.Gutter_Guards" %>

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
            Details: Gutter Guards</h1>
        <div class="form_panel_custom">
            <ul>
                <li style="width: 49%;">
                    <table id="tblcustom" runat="server" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
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
                            <td>
                                <label>
                                    Gutter Type:<span>*</span>
                                </label>
                                <asp:DropDownList ID="ddlGutterType" runat="server" TabIndex="3">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>K Gutter</asp:ListItem>
                                    <asp:ListItem>Half Round</asp:ListItem>
                                    <asp:ListItem>Yankee</asp:ListItem>
                                    <asp:ListItem>Custom Box Gutter </asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvGutterType" runat="server" InitialValue="0" ForeColor="Red"
                                    ValidationGroup="save" ControlToValidate="ddlGutterType" ErrorMessage="Select Gutter Type"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Linear Install Footage:
                                </label>
                                <asp:TextBox ID="txtLinearFootage" runat="server" TabIndex="5"></asp:TextBox>
                                <label>
                                </label>
                                <%-- <asp:RequiredFieldValidator ID="rfvProposalCost" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtProposalCost" ErrorMessage="Enter Proposal Cost"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Color:</label>
                                <asp:DropDownList ID="ddlcolor" runat="server" TabIndex="7">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Brandywine Red</asp:ListItem>
                                    <asp:ListItem>Grecian Green</asp:ListItem>
                                    <asp:ListItem>Heritage Blue</asp:ListItem>
                                    <asp:ListItem>Wicker</asp:ListItem>
                                    <asp:ListItem>Sandcastle</asp:ListItem>
                                    <asp:ListItem>Eggshell</asp:ListItem>
                                    <asp:ListItem>White</asp:ListItem>
                                    <asp:ListItem>Brown</asp:ListItem>
                                    <asp:ListItem>Cream</asp:ListItem>
                                    <asp:ListItem>Ivory</asp:ListItem>
                                    <asp:ListItem>Bronze</asp:ListItem>
                                    <asp:ListItem>Territone</asp:ListItem>
                                    <asp:ListItem>Light Gray</asp:ListItem>
                                    <asp:ListItem>Black</asp:ListItem>
                                    <asp:ListItem>Clay</asp:ListItem>
                                    <asp:ListItem>Dark Gray</asp:ListItem>
                                    <asp:ListItem>Gloss White</asp:ListItem>
                                    <asp:ListItem>Muskett Brown</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="FuPicture" ErrorMessage="Select Atleast One Picture"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Existing Gutter Material:<span>*</span>
                                </label>
                                <asp:DropDownList ID="ddlexistgutter" runat="server" TabIndex="2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem>Aluminium</asp:ListItem>
                                    <asp:ListItem>Copper</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvExistGutter" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="ddlexistgutter" InitialValue="0" ErrorMessage="Select Existing Gutter Material"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Work Area: <span>*</span></label>
                                <asp:TextBox ID="txtworkarea" runat="server" MaxLength="35" TabIndex="4"></asp:TextBox>
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
                                <asp:FileUpload ID="flpUploadLoc_img" runat="server" TabIndex="6" onchange="readURL(this);" />
                                <label>
                                    Location Picture 2.<span>*</span></label>
                                <asp:FileUpload ID="FileUpload2" runat="server" TabIndex="6" onchange="readURL(this);" />
                                <label>
                                    Location Picture 3.</label>
                                <asp:FileUpload ID="FileUpload3" runat="server" TabIndex="6" onchange="readURL(this);" />
                                <label>
                                    Location Picture 4.</label>
                                <asp:FileUpload ID="FileUpload4" runat="server" TabIndex="6" onchange="readURL(this);" />
                                <label>
                                    Location Picture 5.</label>
                                <asp:FileUpload ID="FileUpload5" runat="server" TabIndex="6" onchange="readURL(this);" />
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
                                    Location Picture: <span>*</span></label>
                                <asp:FileUpload ID="FuPicture" runat="server" class="multi" TabIndex="6" onchange="readURL(this);" />
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
                                    Special Instructions/ Exemptions:
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
