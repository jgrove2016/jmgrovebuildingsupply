<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="CreateUser.aspx.cs" Inherits="JG_Prospect.CreateUser" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="Scripts/jquery.MultiFile.js" type="text/javascript"></script>




  <script>
      $(function () {
          $("#DOBdatepicker").datepicker({
              changeMonth: true,
              changeYear: true
          });
      });
  </script>

         <script type = "text/javascript">
             function ValidateCheckBox() {
                 if (document.getElementById("<%=chkboxcondition.ClientID %>").checked == true) {
                     return true
                 } else {
                     alert("Please Accept Term and Conditions")
                     return false;
                 }
             }
    </script>
     <script type="text/javascript">
        function hidePnl() {
            $("#ContentPlaceHolder1_pnlpopup").hide();
            return true;
        }
        </script>
      
     <style type="text/css">
            .Autocomplete{
            overflow:auto;
height:150px;
}
        </style>
         <style type="text/css">
.modalBackground
{
background-color: Gray;
filter: alpha(opacity=80);
opacity: 0.8;
z-index: 10000;
display:none;
}
</style>
<div class="right_panel">
     <ul class="appointment_tab">
          <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
          <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
          <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
    </ul>
    <h1>
        Create Users</h1>
    <div class="form_panel_custom">
        <span>
            <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
        </span>
        <ul>
            <li style="width: 49%;">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                         <td>
                            <label>
                                FristName<span></span></label>
                            <asp:TextBox ID="txtfristname" runat="server" MaxLength="40" TabIndex="1" ontextchanged="txtfristname_TextChanged" 
                               ></asp:TextBox>
                            <label>
                            </label>
                          </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                User Name<span>*</span></label>
                            <asp:TextBox ID="txtusername" runat="server" MaxLength="40" TabIndex="3" 
                                ontextchanged="txtusername_TextChanged"></asp:TextBox>
                            <label>
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusername"
                                Display="Dynamic" ForeColor="Red" ValidationGroup="submit">Enter UserName</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Email<span>*</span></label>
                            <asp:TextBox ID="txtemail" runat="server" MaxLength="40" TabIndex="5" 
                                autocomplete="off" EnableViewState="false" AutoCompleteType="None"  ></asp:TextBox>
                            <label>
                            </label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtemail"
                                ValidationGroup="submit" ForeColor="Red" ErrorMessage="Please Enter Email"></asp:RequiredFieldValidator>&nbsp;&nbsp;
                            <asp:RegularExpressionValidator ID="emailcheck" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                ValidationGroup="submit">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Confirm Password<span>*</span></label>
                            <asp:TextBox ID="txtpassword1" runat="server" TextMode="Password"  autocomplete="off"
                                MaxLength="30" TabIndex="7" EnableViewState="false" 
                                AutoCompleteType="None" ></asp:TextBox>
                            <label>
                            </label>
                            <asp:CompareValidator ID="password" runat="server" ControlToValidate="txtpassword1"
                                Display="Dynamic" ControlToCompare="txtpassword" ForeColor="Red" ErrorMessage="Password didn't matched" ValidationGroup="submit">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Status<span>*</span></label>
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="80px" TabIndex="9">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                <asp:ListItem Text="Deactive" Value="Deactive"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                     
                            <label>
                                Zip<span></span></label>
                            
                            <asp:TextBox ID="txtZip" runat="server" MaxLength="5" TabIndex="10" AutoPostBack="true" 
                                ontextchanged="txtZip_TextChanged"></asp:TextBox>
                             <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListElementID="auto_complete" CompletionListCssClass="Autocomplete"
                                    UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetZipcodes"  
                                    TargetControlID="txtZip" EnableCaching="False">
                                </ajaxToolkit:AutoCompleteExtender>
                            
                        </td>
                    </tr>
                    <tr>
                        <td >
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                        <ContentTemplate>
                            <label>
                                City</label>
                           
                            <asp:TextBox ID="txtCity" runat="server" MaxLength="40" TabIndex="12" 
                                ontextchanged="txtCity_TextChanged"></asp:TextBox>
                        
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtZip" EventName="TextChanged" />
                        </Triggers>
                         </asp:UpdatePanel>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Phone Number</label>
                            <asp:TextBox ID="txtphone" runat="server" MaxLength="15" 
                                onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" 
                                TabIndex="14"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Business Name<span></span></label>
                            <asp:TextBox ID="txtbusinessname" runat="server" MaxLength="40" TabIndex="15" ontextchanged="txtbusinessname_TextChanged" 
                               ></asp:TextBox>
                            <label>
                            </label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                SSN<span></span></label>
                            <asp:TextBox ID="txtssn" runat="server" MaxLength="3" TabIndex="16"   
                                onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" 
                                ontextchanged="txtssn_TextChanged" Width="42px" 
                               ></asp:TextBox>
                            <label>
                            -<asp:TextBox ID="txtssn0" runat="server" MaxLength="2" TabIndex="17"   
                                onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" 
                                 ontextchanged="txtssn0_TextChanged" Width="42px" 
                               ></asp:TextBox>
                            -<asp:TextBox ID="txtssn1" runat="server" MaxLength="4" TabIndex="18"   
                                onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" 
                                ontextchanged="txtssn1_TextChanged" Width="42px" 
                               ></asp:TextBox>
                            </label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <label>
                               TaxpayerIdentNumber  <span></span></label>
                            <asp:TextBox ID="txtTIN" runat="server" MaxLength="15" TabIndex="17" 
                                autocomplete="off" EnableViewState="false" AutoCompleteType="None" ontextchanged="txtTIN_TextChanged" 
                               ></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                         <td>
                            <label>
                                Marital Status <span></span></label>
                            <asp:DropDownList ID="ddlMaritalstatus" runat="server" Width="80px" TabIndex="9" 
                                 onselectedindexchanged="ddlMaritalstatus_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                    <td >
                   <asp:UpdatePanel ID="UpdatepnlPic" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                   
                            <label>
                                Picture<span></span></label>                         
                           <ajaxToolkit:AsyncFileUpload ID="flpUplaodPicture" runat="server" TabIndex="21" 
                                ClientIDMode="AutoID" Width="70%"   
                                OnClientUploadStarted="AssemblyFileUpload_Started" />
                            <asp:Button ID="btn_UploadPicture" runat="server" 
                                onclick="btn_UploadPicture_Click" width="10%" OnClientClick="return CheckFileExistence()"
                                Text="Upload" TabIndex="22" />
                                                                                      
                            <asp:ListBox ID="lstboxUploadedImages" runat="server" AutoPostBack="true" Height="80px" 
                                Width="430px" onselectedindexchanged="lstbxImages_SelectedIndexChanged" 
                                TabIndex="23"></asp:ListBox>
                            &nbsp;&nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" 
                                onclick="btndelete_Click1" TabIndex="24"/>    
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lstboxUploadedImages" EventName="SelectedIndexChanged" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btndelete" EventName="Click" />--%>
                            <asp:PostBackTrigger ControlID="btn_UploadPicture" />
                            <asp:PostBackTrigger ControlID="btndelete" />
                            </Triggers>
                    </asp:UpdatePanel>                    
                    </td>
                    </tr>
                    <tr>
                    <td >
                             
                            <label>
                                Attachments</label>
      
       
                            <asp:FileUpload ID="flpUploadFiles" MaxLength="40" runat="server"  
                                class="multi" Width="70%"
                                TabIndex="25"/>
                           
                            <asp:Button ID="btn_UploadFiles" runat="server" onclick="btn_UploadFiles_Click"  
                                CssClass="cancel"  width="10%"
                                Text="Upload" TabIndex="26" />
                             <asp:GridView ID="gvUploadedFiles" runat="server" AutoGenerateColumns="False" 
                               Width="90%"
                                DataKeyNames="FileName" EmptyDataText="No files uploaded" 
                                onrowcommand="gvUploadedFiles_RowCommand" PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="FileName" HeaderText="FileName" ControlStyle-Width="60%"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" 
                                                CommandArgument='<%#Eval("FileName")%>' CommandName="DownloadRecord" 
                                                Text="Download"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" 
                                                CommandArgument='<%#Eval("FileName")%>' CommandName="deleteRecord" 
                                                Text="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                       
                        </td>
                    </tr>
                    </table>
            </li>
            <li class="last" style="width: 49%;">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       <td>
                            <label>
                                LastName<span></span></label>
                            <asp:TextBox ID="txtlastname" runat="server" MaxLength="40" TabIndex="2" 
                                autocomplete="off" ontextchanged="txtlastname_TextChanged"></asp:TextBox>
                            <label>
                            </label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Login Id<span>*</span></label>
                            <asp:TextBox ID="txtloginid" runat="server" MaxLength="40" TabIndex="4" 
                                autocomplete="off"></asp:TextBox>
                            <label>
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtloginid"
                            ForeColor="Red" Display="Dynamic" ValidationGroup="submit">Enter Login ID</asp:RequiredFieldValidator>
                      
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtloginid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please Enter a valid Email"
                                ValidationGroup="submit">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Password<span>*</span></label>
                            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"  
                                MaxLength="30" TabIndex="6" autocomplete="off"></asp:TextBox>
                            <label>
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpassword"
                                ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ErrorMessage="Please Enter Password"></asp:RequiredFieldValidator><br />
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                 Designation<span>*</span></label>
                            <asp:DropDownList ID="ddlusertype" runat="server" Width="150px" TabIndex="8" >
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>             
                                <asp:ListItem Text="Admin Secretary" Value="AdminSec"></asp:ListItem>                            
                                <asp:ListItem Text="Jr Sales Executive" Value="JSE"></asp:ListItem>
                                <asp:ListItem Text="Sr Sales Executive" Value="SSE"></asp:ListItem>
                                <asp:ListItem Text="Marketing Manager" Value="MM"></asp:ListItem>
                                <asp:ListItem Text="Sales Manager" Value="SM"></asp:ListItem>
                            </asp:DropDownList>
                            <label>
                            </label>
                            <label>
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlusertype"
                                ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ErrorMessage="Please Select  Designation"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                 <%--   <tr>
                        <td>
                            <label>
                                Designation<span>*</span></label>                          
                            <asp:DropDownList ID="ddldesignation" runat="server" Width="150px" 
                                TabIndex="11">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Administrator" Value="Admin"></asp:ListItem>
                                 <asp:ListItem Text="Admin Secretary" Value="AdminSec"></asp:ListItem> 
                                 <asp:ListItem Text="Jr Sales Executive" Value="JSE"></asp:ListItem> 
                                <asp:ListItem Text="Sr Sales Executive" Value="SSE"></asp:ListItem>
                                <asp:ListItem Text="Sales Manager" Value="SM"></asp:ListItem>
                                <asp:ListItem Text="Marketing Manager" Value="MM"></asp:ListItem>
                            </asp:DropDownList>
                            <label>
                            </label>
                            <label>
                            </label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddldesignation"
                                ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ErrorMessage="Please Select Designation"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                    <tr>
                       
                        <td >
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                      
                            <label>
                                State</label>
                           
                            <asp:TextBox ID="txtState" runat="server" MaxLength="40" TabIndex="13" 
                                ontextchanged="txtState_TextChanged"></asp:TextBox>
                             </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtZip" EventName="TextChanged" />
                        </Triggers>
                         </asp:UpdatePanel>
                        </td>
                    </tr>
                  
                    <tr>
                        <td>
                            <label>
                                Address</label>
                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine"  Height="40px" Width="184px"
                                TabIndex="15" ontextchanged="txtaddress_TextChanged1"></asp:TextBox>
                                 <%--<asp:RegularExpressionValidator ID="txtConclusionValidator1" ControlToValidate="txtaddress" ForeColor="Red" Text="Exceeding 200 characters" ValidationExpression="^[a-zA-Z0-9.]{0,200}$" runat="server" />--%><br />
     
                        </td>
                    </tr>
                  
                    <tr>
                        <td>
                            <label>
                                Signature</label>
                            <asp:TextBox ID="txtSignature" runat="server"    Width="184px"
                                TabIndex="19" ontextchanged="txtSignature_TextChanged" ></asp:TextBox>
                        </td>
                    </tr>
                  
                    <tr>
                       <td>
                            <label>
                               Date of Birth</label>
                               <asp:TextBox ID="DOBdatepicker" ClientIDMode="Static"  runat="server" 
                                Width="184px" TabIndex="20" onkeypress="return false" ontextchanged="DOBdatepicker_TextChanged"           
                                 ></asp:TextBox>
                                
                       </td>
                    </tr>
                  
                    <tr>
                        <td class="style2">
                            <label>
                             penalty of perjury </label>
                               <asp:DropDownList ID="ddlcitizen" runat="server"  Width="150px" TabIndex="18" 
                                onselectedindexchanged="ddlcitizen_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="US Citizenship" Value="USCitizenship"></asp:ListItem>
                                <asp:ListItem Text="Non US Citizenship" Value="NonUSCitizenship"></asp:ListItem>
                                 <asp:ListItem Text="lawful permanent resident" Value="permanentresident"></asp:ListItem>
                                 <asp:ListItem Text="alien authorized to work" Value="authorizedwork"></asp:ListItem>
                            </asp:DropDownList>
                                
                       </td>
                    </tr>
                  
                    <tr>
                       <td class="style2">
                            <label>
                                EIN</label>
                            <asp:TextBox ID="txtEIN" runat="server" MaxLength="2" TabIndex="13"   
                                onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" 
                                 Width="42px" ontextchanged="txtEIN_TextChanged" 
                               ></asp:TextBox>
                           
                            -<asp:TextBox ID="txtEIN2" runat="server" MaxLength="7" TabIndex="14"   
                                onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);" 
                                  Width="55px" ontextchanged="txtEIN2_TextChanged" 
                               ></asp:TextBox>
                           
                           
                           
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="style2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                         <ContentTemplate>
           <asp:LinkButton ID="lnkw4Details" runat="server" CausesValidation="false" 
                                 onclick="lnkw4Details_Click" >Add W4 Deatils</asp:LinkButton>
            
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkw4Details" PopupControlID="pnlpopup"
CancelControlID="btnCancel" BackgroundCssClass="modalBackground" Enabled="true">

</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="800px" style="display:none" ScrollBars="Vertical">
<table width="100%" style="border:Solid 3px #b04547; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:#b04547"  >
<td colspan="2" style=" height:10%; color:White; font-weight:bold; font-size:larger" align="center">W4 Details</td>
<td ></td>
</tr>
<tr>
<td align="left">
<b>A.</b>Enter “1” for yourself if no one else can claim you as a dependent . . . .:
</td>
<td>
<asp:TextBox ID="txtA" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>B</b>.Enter “1” if: { • You are single and have only one job; or
• You are married, have only one job, and your spouse does not work; or . . .
• Your wages from a second job or your spouse’s wages (or the total of both) are $1,500 or less.}:
</td>
<td>
<asp:TextBox ID="txtB" Width="25%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>C.</b>Enter “1” for your spouse. But, you may choose to enter “-0-” if you are married and have either a working spouse or more
than one job. (Entering “-0-” may help you avoid having too little tax withheld.) . . . . . . . . . . . . . .:
</td>
<td>
<asp:TextBox ID="txtC" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
D.Enter number of dependents (other than your spouse or yourself) you will claim on your tax return . . . . .:
</td>
<td>
<asp:TextBox ID="txtD" Width="25%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>E.</b>Enter “1” if you will file as head of household on your tax return (see conditions under Head of household above) . .:
</td>
<td>
<asp:TextBox ID="txtE" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>F.</b>Enter “1” if you have at least $1,900 of child or dependent care expenses for which you plan to claim a credit .:
</td>
<td>
<asp:TextBox ID="txtF" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>G.</b>Child Tax Credit (including additional child tax credit). See Pub. 972, Child Tax Credit, for more information.:
• If your total income will be less than $61,000 ($90,000 if married), enter “2” for each eligible child; then less “1” if you have three or more eligible children.
• If your total income will be between $61,000 and $84,000 ($90,000 and $119,000 if married), enter “1” for each eligible
child plus “1” additional if you have six or more eligible children . . . . . . . . . .
</td>
<td>
<asp:TextBox ID="txtG" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>H.</b>Add lines A through G and enter total here. (Note. This may be different from the number of exemptions you claim on your tax return.):
</td>
<td>
<asp:TextBox ID="txtH" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>5.</b>Total number of allowances you are claiming (from line H above or from the applicable worksheet on page 2)..:
</td>
<td>
<asp:TextBox ID="txt5" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>6.</b>Additional amount, if any, you want withheld from each paycheck . . . . . . . .:
</td>
<td>
<asp:TextBox ID="txt6" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left">
<b>7.</b>I claim exemption from withholding for 2011, and I certify that I meet both of the following conditions for exemption.
• Last year I had a right to a refund of all federal income tax withheld because I had no tax liability and
• This year I expect a refund of all federal income tax withheld because I expect to have no tax liability.
If you meet both conditions, write “Exempt” here . . . . . . . . . . . . . . .:
</td>
<td>
<asp:TextBox ID="txt7" Width="40%" runat="server" MaxLength="5"></asp:TextBox>
</td>
</tr>
<tr>
<td align="center" >
<asp:Label ID="lblresult" runat="server"></asp:Label>
<asp:Button ID="Button1" CommandName="Submit" runat="server" Text="Submit"   OnClientClick="return hidePnl();" />
<asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</td>
<td >

</td>
</tr>
</table>
</asp:Panel>
  </ContentTemplate>
</asp:UpdatePanel>                 
                    
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkbuttonW4" Text="W4" runat="server" onclick="lnkbuttonW4_Click"  
                                ></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="linkbuttonI9" Text="I9" runat="server" onclick="linkbuttonI9_Click" 
                                ></asp:LinkButton>
                                 <br />
                                <%-- <asp:LinkButton ID="linkbuttonW9" Text="W9" runat="server" 
                                onclick="linkbuttonW9_Click" ></asp:LinkButton>
                                  --%>

                            </td>
                    </tr>
                    <tr>
                        <td>
                    
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" >
                      <ContentTemplate>                     
                                  <asp:Image ID="Image2" runat="server" Height="150px" Width="300px" />     
                      </ContentTemplate>
                      <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="lstboxUploadedImages" EventName="SelectedIndexChanged" />
                       </Triggers>
                      </asp:UpdatePanel>
     
                        </td>
                    </tr>
                </table>
            </li>
        </ul>
         <div >
        <table ><tr><td>
       <asp:Panel ID="pnl3" runat="server">
           <h2>Salesperson Agreement</h2>
</asp:Panel>

    <div class="cdata">
<asp:Panel ID="pnl4" runat="server">

    This AGREEMENT is made and entered into this day<br />
   <b> <asp:Label ID="lblDay" runat="server"></asp:Label></b>
    &nbsp; by and between J.M. Grove., hereinafter referred to as <b>“<b>COMPANY</b>”</b> and ,<b> <asp:Label ID="lblName" runat="server"></asp:Label></b> hereinafter referred to as <b>“SALESPERSON”</b>.<br />
					<b>WITNESSETH:</b><br />
WHEREAS,<b> COMPANY</b> is engaged in the business of the promotion, sale and installation of replacement windows ,doors, patio rooms, decks , roofing, siding, baths and related products; and
WHEREAS,<b> COMPANY</b> agrees to employ <b>SALESPERSON</b> in said capacity for the sale of its products under terms and conditions hereinafter set forth; and
WHEREAS,<b> SALESPERSON </b> agrees to work for <b>COMPANY </b>in the capacity of a  <b>SALESPERSON </b>of <b>COMPANY’S</b> products under terms and conditions hereinafter set forth.
NOW, THEREFORE, in consideration of the covenants hereinafter set forth, the parties hereto, hereby agree as follows:<br />
　　　　<b>Article I</b><br />
				    <b> WORK ASSIGNMENT</b><br />
1.1  	<b>COMPANY</b> agrees to employ <b>SALESPERSON</b> and <b>SALESPERSON</b> agrees to work for COMPANY in the prospecting/sale of     <b> COMPANY’S</b>   product, out of the ___Manayunk, Pa_____ office..<br />
1.2	<b>SALESPERSON</b> acknowledges that in further consideration of his/her work he/she agrees to be bound by all of the policies, practices, rules and regulations of <b>COMPANY..</b><br />
					<b>Article II</b><br />
					<b> DUTIES</b><br />
2.1	<b>SALESPERSON</b> agrees to devote his/her full attention and best efforts to the performance of his/her duties and responsibilities with <b>COMPANY</b>, which shall include the following:<br />
	(a)	to procure appointments acceptable to <b>COMPANY</b> without making misrepresentations to the customer;<br />
(b)	to understand and comply with all state and federal laws pertaining to in-home sales and third-party   financing of home improvements;<br />
(c)	to read ,understand and comply with the J.M. Grove suggested “Intro level” Sales expectations<br />
(d)	to procure missing or improperly executed documentation when or if such occurs from his/her customers in a timely manner<br />

<b>Article III</b><br />

3.3	Full commission shall apply only when <b>SALESPERSON</b> works alone. In the event field assistance is furnished by the Branch Manager, Assistant Branch Manager or other salespersons of <b>COMPANY</b>, it is agreed that the commission on the net acceptable appointments will be divided among the participants, and reduced commissions shall be credited to <b>SALESPERSON</b> account accordingly.<br />
3.6	In addition to the compensation program outlined above, <b>SALESPERSON</b> will be eligible for additional earnings under regularly announced contest or bonus plans of <b>COMPANY</b>, while such plans are in effect and while he/she is working to the satisfaction of COMPANY. Details of such plans, currently effective, will be available in the appropriate branch offices.<br />
3.7	<b>COMPANY</b> agrees to furnish <b>SALESPERSON</b> with a bi-weekly statement of his/her account which will be considered accepted and correct unless he/she notifies COMPANY to the contrary in writing within seven (7) days of receiving such statement.<br />
3.8	Should <b>SALESPERSON</b> subsequently change from this compensation plan to any other <b>COMPANY</b> plan, he/she agrees that any overdraft accrued to his/her account by virtue of salary or advances not covered by earned commissions, or due to cancellations or jobs not performed, will be transferred with his/her account to such subsequent pay plan. Any advance or overdraft not finally covered by earnings on completed jobs shall constitute a continuing obligation to <b>COMPANY</b>. <b>SALESPERSON</b> agrees that any change in his/her position or compensation plan shall not invalidate any of the other provisions of this AGREEMENT.<br />

<b>Article IV</b><br />
                                <b> REPRESENTATIONS OF SALESPERSON</b><br />


(b) 	He/she certifies that he/she considers himself/herself capable of earning a living in such other fields;<br />


(d)	He/she recognizes that he/she may have access to a <b>CONFIDENTIAL</b> list of customers of <b>COMPANY</b>, which list is of tremendous value to any person or entity engaged in such business;<br />

(e)	He/she recognizes that his/her employment as <b>SALESPERSON</b> is on behalf of <b>COMPANY</b>, and that as such, the product of all work performed and results of his/her effort and individual contribution to that employment shall be considered the sole property <b>COMPANY</b>.<br />

(f)	He /She recognizes and acknowledges that the knowledge, information and relationship with customers which he/she will hereafter receive and establish as a <b>SALESPERSON</b> of <b>COMPANY</b> are valuable, special and unique assets of <b>COMPANY’S</b> business.<br />

<b>Article V</b><br />
			<b>REPRESENTATION OF COMPANY</b><br />
5.1	<b>COMPANY</b> agrees to train <b>SALESPERSON</b> in the products handled by <b>COMPANY</b> and in related methods, procedures, techniques and sales policies of <b>COMPANY</b><br />
<b>Article VI</b>
				<b>SALES EQUIPMENT AND SAMPLES</b><br />
All samples, sales aids, literature and all other “company property” will be returned on fair shape. If all “company property” is not returned the value of “company property” will be held as a draw to commissions.<br />

				<b>Article VII</b><br />
				<b>CUSTOMER PROSPECTS AND LEADS	</b><br />

7.1	All customers, prospects and/ or leads obtained during the employment of <b>SALESPERSON</b> by <b>SALESPERSON</b> or <b>COMPANY</b> shall be considered to be customers, prospects, and/or leads as the case may be, of <b>COMPANY</b> and <b>SALESPERSON</b> agrees not to contact any of such parties, directly, or reveal the identity of any such customer, prospects or leads to any other party at any time without the written consent of <b>COMPANY</b><br />

					<b>Article VIII</b><br />
				           <b>TERMINATION</b><br />

8.1	The AGREEMENT may be terminated by <b>SALESPERSON</b> or <b>COMPANY</b>, without notice or cause, and no compensation shall be earned or due on sales made after such termination.<br />

8.2 	Upon termination, <b>SALESPERSON</b> shall not be entitled to receive any compensation from COMPANY on contracts concerning which installation is not yet completed at the time of termination, although said contracts are procured by <b>SALESPERSON</b>, until the installations concerning all such contracts are completed by <b>COMPANY</b>. Furthermore, <b>SALESPERSON</b> agrees that if he/she should violate any of the provisions set forth in this AGREEMENT he/she shall not be entitled to receive any commissions or compensation otherwise due to him/her or held in abeyance by <b>COMPANY</b> and furthermore, any advances or overdrafts not finally covered by earnings on completed jobs shall be repaid to <b>COMPANY</b> on demand.<br />
					<b>Article IX</b><br />
			<b>NONCOMPETITION AND CONFIDENTIALITY PROVISIONS</b><br />

9.1	In recognition and consideration of <b>SALESPERSON’S</b> employment, compensation and fringe benefits, the training which <b>COMPANY</b>  will give <b>SALESPERSON</b> in <b>COMPANY’S</b> business and <b>SALESPERSON’S</b> introduction to <b>COMPANY’S</b> customers and prospective customers made in the course of <b>SALESPERSON’S</b> employment with <b>COMPANY</b> and the carefully guarded methods of doing business which <b>COMPANY</b> utilizes and deems crucial to the successful operation of its business, SALESPERSON covenants that he/she will not during the term of this AGREEMENT, or for a period of THREE (3) years immediately following termination of his/her employment with <b>COMPANY</b>, irrespective of the reason for termination or which party initiates termination, enter into competition with <b>COMPANY</b>,  within a geographical area included within a 100-mile radius of any office of <b>COMPANY</b>, either directly or indirectly, as an individual on his/her account or in association with others, or as an employee, associate, agent, contractor, partner ,limited partner, general partner, founder, board member, officer, stockholder, manager or consultant, of another person, firm organization, proprietorship, partnership, or corporation, or otherwise in any other manner whatsoever. For the purpose of this agreement, the term “competition” shall include, but not to be limited to any activity which either directly or indirectly, either on behalf of SALESPERSON or for any other person, partnership, or Corporation, relates to the manufacture, distribution, sale, repair, and /or installation of windows, doors, patio enclosures, kitchens ,basements ,bath remodeling, or related products. In addition, in the event of any breach or violation of the restrictions contained herein by SALESPERSON, he/she agrees to an additional restrictive covenant on the same terms to run during the time of any violation or the pendency of any litigation with respect thereto and for three (3) years after such violation has been fully and finally cured or after such litigation has been resolved. These covenants include the following additional terms and conditions:<br />

(a)	SALESPERSON acknowledges that the list of <b>COMPANY</b>’S customers is a valuable , special and unique asset of <b>COMPANY</b>’S business; and SALESPERSON agrees  that for a period of three(3) years following termination of his/her employment, for whatever reason such termination may be caused ,he/she will not, on behalf of himself/herself or on behalf of any other person, firm, or corporation, contact, call upon or cause to be called upon, or solicit or assist in the solicitation of any person, firm, association or corporation which was a customer of <b>COMPANY</b>, or a lead procured by <b>COMPANY</b>’S marketing efforts on the date of the termination of his/her employment, or who was, to SALESPERSON’S knowledge, a customer or account of <b>COMPANY</b> at any time during SALESPERSON’S period of employment, for the purpose of selling or supplying to such customer or account any products or services directly or indirectly competing with those offered or provided by <b>COMPANY</b>;<br />

(b)	SALESPERSON will hold in a fiduciary capacity for the benefit of <b>COMPANY</b>, all secret, proprietary and/or confidential information, knowledge, techniques of doing business or data involving <b>COMPANY</b> and like property coming into his/her possession necessitated by or through and during his/ her employment and will not , during his/ her employment or after termination of such employment, communicate or divulge any such trade secrets, confidential information, knowledge or date to any person, firm, corporation or any other entity. Such nondisclosure shall include , without limitation, all files, procedures and/or training manuals, records, documents, forms, sales materials, lists of customers and/or prospective customers, business plans, policies or strategies and nature of services provided to customers. SALESPERSON therefore agrees that upon termination ,for any reason, of his/her employment with <b>COMPANY</b>, he/she shall promptly return to <b>COMPANY</b> the originals and all copies of any such information which had been supplied to him/her by <b>COMPANY</b> or which shall then have in his/her possession; <br />

(c) 	<b>SALESPERSON</b> acknowledges, recognizes and agrees that the restrictions contained in this AGREEMENT are essential to protect the business interests and goals of <b>COMPANY</b> and that, violations of the restrictions will cause irreparable harm to <b>COMPANY</b>. <b>SALESPERSON</b> further agrees that should he/she violate any of the restrictive covenants, <b>COMPANY</b> shall be entitled to seek special, preliminary and permanent injunctive relief, as well as any other rights or remedies to which <b>COMPANY</b> shall be entitled. The enumeration of legal remedies herein does not limit any other remedies which <b>COMPANY</b> would otherwise possess;<br />

(d) 	<b>SALESPERSON</b> agrees that during the course of his/her employment with <b>COMPANY</b> or following termination of such employment, he/she will not engage in any course of conduct designed to solicit or in any way interfere with the performance of any existing employment agreement or contract between <b>COMPANY</b> and any other person;<br />

(e) 	<b>SALESPERSON</b> admits that in the event of termination of employment, his/her pre-existing capabilities are such that he/she can obtain employment with an organization which does not compete with the services or products of <b>COMPANY</b>; and<br />

(f)	Prior to the entry of any injunction for any violations of these restrictions, <b>SALESPERSON</b> shall be obligated to account to <b>COMPANY</b> for any and all earnings, profits or other compensation earned by him/her in connection with any sales made in violations of the noncompetition and nondisclosure provisions hereof, and to reimburse <b>COMPANY</b> for any and all profits on such sales as if such <b>SALESPERSON</b> had made such sales as a <b>SALESPERSON</b> of <b>COMPANY</b>.<br />


				<b>Article X</b><br />
			Miscellaneous provisions<br />

10.1	In any legal action concerning an alleged breach by <b>SALESPERSON</b> of any provisions in this AGREEMENT, irrespective of the requested remedy, <b>COMPANY</b> shall be entitled to reasonable counsel fees and costs of suit from <b>SALESPERSON</b>.<br />

10.2	This AGREEMENT constitutes the entire agreement between the parties as to compensation and other terms of employment and integrates all prior discussions between them with respect to its subject matter. This AGREEMENT may be amended only by an agreement in writing signed by both <b>SALESPERSON</b> and an authorized representative of <b>COMPANY</b>, and the terms and conditions may not be amended, changed or modified  in any manner whatsoever by any oral representations or statements by any employee, office, agent or representative of <b>COMPANY</b>.<br />

10.3	The provisions of this AGREEMENT are severable and the invalidity of any one or more of such provisions shall not affect the validity of any of the remaining provisions of the AGREEMENT. If one or more of the provisions of the AGREEMENT is , for any reason, held to be excessively broad, it shall be construed by limiting and reducing it so as to be enforceable to the extent compatible with applicable law.<br />

10.4	<b>SALESPERSON</b> acknowledges receipt of a copy of this AGREEMENT.<br />
</asp:Panel>
</div> 
 <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"                  
SuppressPostBack="true" ExpandedImage="~/img/minusicon.png" TargetControlID="pnl4"
CollapseControlID="pnl3" ExpandControlID="pnl3" CollapsedImage="~/img/plusicon.png"
Collapsed="true" ImageControlID="img2">
</ajaxToolkit:CollapsiblePanelExtender>     </td>
                </tr>
                <tr>
                <td>
                 <asp:CheckBox ID="chkboxcondition" runat="server" TabIndex="27" />
                    <asp:Label ID="lblTerms" runat="server" Text="I accept Term and Conditions of the above mentioned SalesPerson"></asp:Label>
                 </td>
                </tr>
            </table>
        </div>
        <div class="btn_sec">
            <asp:Button ID="btncreate" Text="Create User" runat="server" OnClick="btncreate_Click"
                TabIndex="28" ValidationGroup="submit" 
                OnClientClick="return ValidateCheckBox();"  />
            <asp:Button ID="btnreset" Text="Reset" runat="server" OnClick="btnreset_Click" 
                TabIndex="29" />
            <asp:Button ID="btnUpdate" Text="Update User" runat="server" 
                TabIndex="30" ValidationGroup="submit" onclick="btnUpdate_Click" />
        </div>
    </div>
    </div>
</asp:Content>
