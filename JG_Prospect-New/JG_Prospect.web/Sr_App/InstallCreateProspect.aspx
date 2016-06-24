<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="false" CodeBehind="InstallCreateProspect.aspx.cs"
    Inherits="JG_Prospect.Sr_App.InstallCreateProspect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="~/Scripts/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        function hidePnl() {
            $("#ContentPlaceHolder1_pnlpopup").hide();
            return true;
        }
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }
        function lettersOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
          ((evt.which) ? evt.which : 0));
            if (charCode == 32 || charCode == 46 || evt.keyCode == 37 || evt.keyCode == 39)
                return true;
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
          (charCode < 97 || charCode > 122)) {
                return false;
            }
            else
                return true;
        }



        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(46); //Delete

        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode == 9) || specialKeys.indexOf(keyCode) != -1 || e.keyCode == 39 || e.keyCode == 37);
            return ret;
        }
        function uploadComplete2() {

            alert("File uploaded successfully");
            //location.href = "InstallCreateProspect.aspx?ImageUpload=Yes";
            var btnImageUploadClick = document.getElementById("btn_UploadFiles");
            btn_UploadFiles.click();

        }
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            display: none;
        }

<<<<<<< HEAD
          .black_overlay
        {
=======
        .black_overlay {
>>>>>>> refs/remotes/origin/master
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
            overflow-y: hidden;
        }
<<<<<<< HEAD
        .white_content
        {
=======

        .white_content {
>>>>>>> refs/remotes/origin/master
            display: none;
            position: absolute;
            top: 10%;
            left: 20%;
            width: 60%;
            height: 5%;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
    <%-- <script>
        function AssemblyFileUpload_Started(sender, args) {
            var filename = args.get_fileName();
            var ext = filename.substring(filename.lastIndexOf(".") + 1);
            if (ext != 'png' && ext != 'jpg' && ext != 'bmp') {
                throw {
                    name: "Invalid File Type",
                    level: "Error",
                    message: "Invalid File Type (Only .png)",
                    htmlMessage: "Invalid File Type (Only .png,.jpg and bmp)"
                }
                return false;
            }
            return true;
        }

</script>--%>
    <style type="text/css">
        .Autocomplete {
            overflow: auto;
            height: 150px;
        }

        .style1 {
            height: "";
            width: 451px;
        }

        .style2 {
            width: 451px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <%--<li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="MasterAppointment.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>--%>
        </ul>
        <!-- appointment tabs section End -->
        <h1>Install Create Prospect
        </h1>
        <div class="form_panel_custom">
            <span>
                <%--<asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>--%>
            </span>
            <ul>
                <li style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblException" runat="server" Visible="false"></asp:Label>
                                    First Name<span>*</span></label>
                                <asp:TextBox ID="txtfirstname" runat="server" MaxLength="25" TabIndex="101" autocomplete="off"
<<<<<<< HEAD
                                    EnableViewState="false" onkeypress="return lettersOnly(event);"  AutoCompleteType="None" OnTextChanged="txtfirstname_TextChanged" Width="218px"></asp:TextBox>
=======
                                    EnableViewState="false" onkeypress="return lettersOnly(event);" AutoCompleteType="None" OnTextChanged="txtfirstname_TextChanged" Width="218px"></asp:TextBox>
>>>>>>> refs/remotes/origin/master
                                &nbsp;&nbsp;
                                <br />
                                <label></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirstname"
                                    ForeColor="Red" ValidationGroup="submit" ErrorMessage="Enter FirstName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
<<<<<<< HEAD
                                    Company Name <span></span>
                                </label>
                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="50" TabIndex="103" Width="218px" autocomplete="off"
                                    EnableViewState="false" AutoCompleteType="None" onkeypress="return lettersOnly(event);" OnTextChanged="txtCompanyName_TextChanged"></asp:TextBox>
                                <br />
                                <label></label>
                            </td>
=======
                                    &nbsp;Designation<span>*</span></label>
                                <asp:DropDownList ID="ddldesignation" runat="server" Width="249px" TabIndex="101" ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqDesignition" runat="server" ControlToValidate="ddldesignation"
                                    ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ErrorMessage="Please Select Designation"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>

>>>>>>> refs/remotes/origin/master
                        </tr>


                        <tr>
                            <td class="style2">
                                <label>
                                    &nbsp;Phone #<span>*</span></label>
                                <asp:TextBox ID="txtPhone" runat="server" Width="218px" MaxLength="12" autocomplete="off" onkeypress="return IsNumeric(event);"
                                    TabIndex="104"></asp:TextBox>
                                <br />
                                <label></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone"
                                    ForeColor="Red" Display="Dynamic" ValidationGroup="submit">Enter Phone#</asp:RequiredFieldValidator><br />


                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Email</label>
                                <asp:TextBox ID="txtemail" runat="server" MaxLength="50" TabIndex="106" Width="218px" autocomplete="off" OnTextChanged="txtemail_TextChanged"></asp:TextBox>
                                <br />
                                <label></label>
                                <asp:RegularExpressionValidator ID="emailcheck0" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                    ValidationGroup="submit">
                                </asp:RegularExpressionValidator>
                                <br />
                            </td>
                        </tr>


<<<<<<< HEAD

=======
                        <%if(isInstallUser==true){ %>
>>>>>>> refs/remotes/origin/master
                        <tr>
                            <td>
                                <%--<asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>--%>
<<<<<<< HEAD
                                        <label>
                                            Primary Trade<span>*</span></label>
                                        <%--"ddlPrimaryTrade_SelectedIndexChanged"--%>
                                        <asp:DropDownList ID="ddlPrimaryTrade" runat="server" TabIndex="108" CausesValidation="false"
                                            OnSelectedIndexChanged="ddlPrimaryTrade_SelectedIndexChanged" Style=" border: 1px solid; border-radius: 5px; width: 227px;" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <br />
                                        <label>
                                        </label>
                                        <asp:TextBox ID="txtOtherTrade" TabIndex="109" runat="server" Width="217px"></asp:TextBox>
                                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPrimaryTrade" Display="Dynamic"
                                    ValidationGroup="submit" InitialValue="0" ForeColor="Red" ErrorMessage="Please Select Primary Trade"></asp:RequiredFieldValidator>


                            </td>
                        </tr>

=======
                                <label>
                                    Primary Trade<span>*</span></label>
                                <%--"ddlPrimaryTrade_SelectedIndexChanged"--%>
                                <asp:DropDownList ID="ddlPrimaryTrade" runat="server" TabIndex="108" CausesValidation="false"
                                    OnSelectedIndexChanged="ddlPrimaryTrade_SelectedIndexChanged" Style="border: 1px solid; border-radius: 5px; width: 227px;" AutoPostBack="True">
                                </asp:DropDownList>
                                <br />
                                <label>
                                </label>
                                <asp:TextBox ID="txtOtherTrade" TabIndex="109" runat="server" Width="217px"></asp:TextBox>
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>


                                <asp:RequiredFieldValidator ID="RfvPrimaryTrade" runat="server" ControlToValidate="ddlPrimaryTrade" Display="Dynamic"
                                    ValidationGroup="submit" InitialValue="0" ForeColor="Red" ErrorMessage="Please Select Primary Trade"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%} %>
>>>>>>> refs/remotes/origin/master
                        <tr>
                            <td class="style2">
                                <label>
                                    Date Sourced
                                </label>
                                <asp:TextBox ID="DateSourced" TabIndex="112" ClientIDMode="Static" runat="server" Width="218px" autocomplete="off"
                                    onkeypress="return false" ReadOnly="True"></asp:TextBox>
                                <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="DateSourced" runat="server"></ajaxToolkit:CalendarExtender>--%>
                                <br />
                                <label></label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DateSourced"
                                    ErrorMessage="Invalid Date Format" Type="Date" Operator="DataTypeCheck" Display="Dynamic"
                                    Text="Invalid Date Format" ForeColor="Red" ValidationGroup="submit"></asp:CompareValidator>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <label>
                                    Attachments<span></span></label>
                                <ajaxToolkit:AsyncFileUpload ID="AsyncFileUploadCustomerAttachment" runat="server" ClientIDMode="AutoID" ThrobberID="abc"
                                    OnUploadedComplete="AsyncFileUploadCustomerAttachment_UploadedComplete" CompleteBackColor="White" TabIndex="114"
                                    Style="width: 22% !important;" OnClientUploadComplete="uploadComplete2" />
                                <%--<asp:Button ID="btn_UploadFiles" style="display:none;" ClientIDMode="AutoID" runat="server" OnClick="btn_UploadFiles_Click" CssClass="cancel"
                                    with="10%" Text="Upload" TabIndex="113" Height="29px" />--%>
                                <asp:Button ID="btn_UploadFiles" ClientIDMode="Static" runat="server" CausesValidation="false"
<<<<<<< HEAD
                                                        Text="hidden" Style="display: none" OnClick="btn_UploadFiles_Click" />
=======
                                    Text="hidden" Style="display: none" OnClick="btn_UploadFiles_Click" />
>>>>>>> refs/remotes/origin/master
                                <%--<asp:FileUpload ID="flpUploadFiles" MaxLength="40" runat="server" class="multi" Width="74%"
                                    TabIndex="112" />--%>
                                &nbsp;
                               
                                <br />
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                <asp:GridView ID="gvUploadedFiles" runat="server" AutoGenerateColumns="False" Width="90%"
                                    DataKeyNames="FileName" EmptyDataText="No files uploaded" OnRowCommand="gvUploadedFiles_RowCommand"
                                    PageSize="5">
                                    <Columns>
                                        <asp:BoundField DataField="FileName" HeaderText="FileName" ControlStyle-Width="60%" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%#Eval("FileName")%>'
                                                    CommandName="DownloadRecord" Text="Download"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("FileName")%>'
                                                    CommandName="deleteRecord" Text="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
<<<<<<< HEAD
                                        <%--</ContentTemplate>
=======
                                <%--</ContentTemplate>
>>>>>>> refs/remotes/origin/master
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="AsyncFileUploadCustomerAttachment" EventName="UploadedComplete" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </td>
                        </tr>


                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="style1">
                                <label>
                                    Last Name<span>*</span></label>
<<<<<<< HEAD
                                <asp:TextBox ID="txtlastname" runat="server"  MaxLength="25" onkeypress="return lettersOnly(event);" Width="218px" TabIndex="102" autocomplete="off"
=======
                                <asp:TextBox ID="txtlastname" runat="server" MaxLength="25" onkeypress="return lettersOnly(event);" Width="218px" TabIndex="102" autocomplete="off"
>>>>>>> refs/remotes/origin/master
                                    OnTextChanged="txtlastname_TextChanged"></asp:TextBox>
                                &nbsp;&nbsp;
                                <br />
                                <label></label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtlastname"
                                    ForeColor="Red" Display="Dynamic" ValidationGroup="submit">Enter LastName</asp:RequiredFieldValidator><br />
                            </td>
                        </tr>
<<<<<<< HEAD
                        <tr>
                            <td class="style2">
                                <label>
                                    &nbsp;</label>
                                <br />
                                <br />
                            </td>
                        </tr>
=======
                        <%if(isInstallUser==true){ %>
                        <tr>
                            <td class="style2">

                                <label>
                                    Company Name <span></span>
                                </label>
                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="50" TabIndex="103" Width="218px" autocomplete="off"
                                    EnableViewState="false" AutoCompleteType="None" onkeypress="return lettersOnly(event);" OnTextChanged="txtCompanyName_TextChanged"></asp:TextBox>
                                <br />
                                <label></label>

                            </td>
                        </tr>
                        <%} %>
>>>>>>> refs/remotes/origin/master
                        <tr>
                            <td class="style2">
                                <label>
                                    Phone# 2</label>
                                <asp:TextBox ID="txtPhone2" runat="server" MaxLength="12" TabIndex="105" Width="218px" OnTextChanged="txtPhone2_TextChanged"
                                    autocomplete="off" onkeypress="return IsNumeric(event);"></asp:TextBox>
                                <br />
                                <label></label>


                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Email 2</label>
                                <asp:TextBox ID="txtemail2" runat="server" MaxLength="50" TabIndex="107" Width="218px" autocomplete="off"></asp:TextBox>
                                <br />
                                <label></label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtemail2" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                    ValidationGroup="submit">
                                </asp:RegularExpressionValidator>





                            </td>
                        </tr>





<<<<<<< HEAD

=======
                        <%if(isInstallUser==true){ %>
>>>>>>> refs/remotes/origin/master
                        <tr>
                            <td>

                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
<<<<<<< HEAD
                                        <label>
                                            Secondary Trade <span>*</span></label>

                                        <asp:DropDownList ID="ddlSecondaryTrade" runat="server" TabIndex="110"
                                            OnSelectedIndexChanged="ddlSecondaryTrade_SelectedIndexChanged" Style=" border: 1px solid; border-radius: 5px; width: 227px;" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <br />
                                        <label>
                                        </label>
                                        <asp:TextBox ID="txtSecTradeOthers" TabIndex="111" runat="server" Width="215px"></asp:TextBox>
                                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSecondaryTrade"
                                    ValidationGroup="submit" InitialValue="0" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Select Secondary Trade"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

=======
                                <label>
                                    Secondary Trade <span>*</span></label>

                                <asp:DropDownList ID="ddlSecondaryTrade" runat="server" TabIndex="110"
                                    OnSelectedIndexChanged="ddlSecondaryTrade_SelectedIndexChanged" Style="border: 1px solid; border-radius: 5px; width: 227px;" AutoPostBack="True">
                                </asp:DropDownList>
                                <br />
                                <label>
                                </label>
                                <asp:TextBox ID="txtSecTradeOthers" TabIndex="111" runat="server" Width="215px"></asp:TextBox>
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <asp:RequiredFieldValidator ID="RfvSecondaryTrade" runat="server" ControlToValidate="ddlSecondaryTrade"
                                    ValidationGroup="submit" InitialValue="0" Display="Dynamic" ForeColor="Red" ErrorMessage="Please Select Secondary Trade"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%} %>
>>>>>>> refs/remotes/origin/master
                        <tr>
                            <td>
                                <label>
                                    Source <span>*</span></label>
                                <asp:TextBox ID="txtSource" runat="server" Width="218px" autocomplete="off" TabIndex="113"
                                    EnableViewState="false" Enabled="false" AutoCompleteType="None"></asp:TextBox>
                                &nbsp;&nbsp;
                                <br />
                                <label></label>
                            </td>
                        </tr>

                        <tr>
                            <td class="style2">
                                <label>
                                    &nbsp;Notes</label>
                                <asp:TextBox ID="txtnotes" runat="server" TextMode="MultiLine" Height="40px" Width="218px"
                                    TabIndex="115"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                    </table>
                </li>
                <asp:Label ID="lblMessage" runat="server" Visible="False"></asp:Label>
            </ul>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="btn_sec">
                <asp:Button ID="btncreate" Text="Create Prospect" runat="server" OnClick="btncreate_Click"
                    TabIndex="116" ValidationGroup="submit" />
                <asp:Button ID="btnreset" Text="Reset" runat="server" OnClick="btnreset_Click" TabIndex="117" />
                <asp:Button ID="btnUpdate" Text="Update" runat="server" TabIndex="118" ValidationGroup="submit" />
                <br />
                <br />
            </div>
            <br />


            <%--<ul>
                <li style="width: 49%;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label style="font-size: medium; font-weight: bold">
                                    Select Period</label>
                                &nbsp;&nbsp;
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    From 
                                </label>
                                <asp:TextBox ID="txtFrom" runat="server"  Width="223px" TabIndex="117" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="MM/dd/yyyy"  TargetControlID="txtFrom" runat="server"></ajaxToolkit:CalendarExtender>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <label>
                                    Select User</label>
                                <asp:DropDownList ID="ddlUsersList" runat="server" Width="229px" TabIndex="119"></asp:DropDownList>
                                <br />
                                <label></label>
                                </td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="style1">
                                
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <label>
                                    To</label>
                                <asp:TextBox ID="txtTo" runat="server" Width="217px" TabIndex="118" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTo" Format="MM/dd/yyyy"  runat="server"></ajaxToolkit:CalendarExtender>
                                <br />


                            </td>
                        </tr>
                        <tr>
                            <td>
                                

                            </td>
                        </tr>
                    </table>
                </li>
                
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnReport" Text="Show" runat="server" OnClick="btnReport_Click" TabIndex="120" />
            </div>
            <br />
            <br />
            <div class="btn_sec">
                <center>
            <asp:Label ID="lblCount" Text="Installed Prospect" ForeColor="Black" runat="server" Font-Size="Medium" Font-Bold="True"></asp:Label>
                                
                                <label>&nbsp;&nbsp; <asp:Label ID="lblTotCount" ForeColor="Black"  runat="server" Font-Size="Small"></asp:Label></label>    
                    </center>
            </div>--%>
        </div>
<<<<<<< HEAD
         <asp:Panel ID="panelPopup" runat="server">
        <div id="light" class="white_content">
            <h3>
                </h3>
            <%--<a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
                Close</a>--%>
            <table width="100%" style="border: Solid 3px #b04547; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr style="background-color: #b04547">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: larger"
                        align="center"></td>
                </tr>
                <tr>
                    <td align="left" colspan="2">Email or Phone number already exists,do you want to update the existing record?
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="height:15px;">
                    </td>
                </tr>
                <tr>
                    <td align="center">
                         <asp:Button ID="btnYesEdit" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                            Style="height: 26px; font-weight: 700; line-height: 1em;" Text="Yes" Width="100px" 
                            ValidationGroup="IndiCred" TabIndex="119" OnClick="Yes_Click" />
                        <%--<asp:Button ID="Button2" runat="server" OnClick="" />--%>
                    </td>
                    <td align="center">
                        <asp:Button ID="Button2" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                            Style="height: 26px; font-weight: 700; line-height: 1em;" Text="No" Width="100px"
                            ValidationGroup="IndiCred" TabIndex="119" OnClick="No_Click" />
                        <%--<asp:Button ID="Button3" runat="server" OnClick="No_Click" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div id="fade" class="black_overlay">
    </div>
=======
        <asp:Panel ID="panelPopup" runat="server">
            <div id="light" class="white_content">
                <h3></h3>
                <%--<a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
                Close</a>--%>
                <table width="100%" style="border: Solid 3px #b04547; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #b04547">
                        <td colspan="2" style="color: White; font-weight: bold; font-size: larger"
                            align="center"></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">Email or Phone number already exists,do you want to update the existing record?
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="height: 15px;"></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnYesEdit" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                                Style="height: 26px; font-weight: 700; line-height: 1em;" Text="Yes" Width="100px"
                                ValidationGroup="IndiCred" TabIndex="119" OnClick="Yes_Click" />
                            <%--<asp:Button ID="Button2" runat="server" OnClick="" />--%>
                        </td>
                        <td align="center">
                            <asp:Button ID="Button2" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                                Style="height: 26px; font-weight: 700; line-height: 1em;" Text="No" Width="100px"
                                ValidationGroup="IndiCred" TabIndex="119" OnClick="No_Click" />
                            <%--<asp:Button ID="Button3" runat="server" OnClick="No_Click" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div id="fade" class="black_overlay">
        </div>
>>>>>>> refs/remotes/origin/master


    </div>
</asp:Content>
