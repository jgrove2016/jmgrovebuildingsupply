<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs" Inherits="JG_Prospect.EditUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         .black_overlay
        {
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

        .white_content
        {
            display: none;
            position: absolute;
            top: 10%;
            left: 20%;
            width: 60%;
            height: 12%;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
    <script type="text/javascript">
        function ConfirmDelete() {
            var Ok = confirm('Are you sure you want to Delete this User?');
            if (Ok)
                return true;
            else
                return false;
        }

        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }


        function ClosePopupInterviewDate() {
            document.getElementById('interviewDatelite').style.display = 'none';
            document.getElementById('interviewDatefade').style.display = 'none';
        }

        function overlayInterviewDate() {
            document.getElementById('interviewDatelite').style.display = 'block';
            document.getElementById('interviewDatefade').style.display = 'block';
        }


        var validFilesTypes = ["xls", "xlsx"];
        function ValidateFile() {
            var file = document.getElementById("<%=BulkProspectUploader.ClientID%>");
         var label = document.getElementById("<%=Label1.ClientID%>");
         var path = file.value;
         var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
         var isValidFile = false;
         for (var i = 0; i < validFilesTypes.length; i++) {
             if (ext == validFilesTypes[i]) {
                 isValidFile = true;
                 break;
             }
         }
         if (!isValidFile) {
             alert('Select file of type xls or xlsx');
             //label.style.color = "red";
             //label.innerHTML = "Invalid File. Please upload a File with" +

             // " extension:\n\n" + validFilesTypes.join(", ");

         }
         return isValidFile;
     }

    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 2px;
            width: 129px;
            height: 173px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="HRReports.aspx">HR Reports</a></li>
            <li><a href="InstallCreateUser.aspx">Create Install User</a></li>
            <li><a href="EditInstallUser.aspx">Edit Install User</a></li>
            <li><a href="CreateSalesUser.aspx">Create Sales User</a></li>
            <li><a href="EditUser.aspx">Edit Sales User</a></li>
        </ul>
        <h1>Edit User</h1>
        <div class="form_panel">
            <span>
                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
            </span>
            <label>
                Upload Prospects using xlsx file:
             <asp:FileUpload ID="BulkProspectUploader"  runat="server" /></label>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="BulkProspectUploader" runat="server" ErrorMessage="Select file to import data." ValidationGroup="BulkImport"></asp:RequiredFieldValidator>--%>
            <div class="btn_sec">
                <asp:Button ID="btnUpload" runat="server" Text="Upload"
                    OnClientClick="return ValidateFile()"  OnClick="btnUpload_Click" />
                <br />
                <asp:Label ID="Label1" runat="server" />
            </div>
            <asp:LinkButton ID="lnkDownload" Text="Download Sample Format For Bulk Upload" CommandArgument='../UserFile/SalesSample.xlsx' runat="server" OnClick="DownloadFile"></asp:LinkButton>
            <div class="grid">
                <%-- <asp:UpdatePanel ID="updatepanel" runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="GridViewUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" AllowSorting="true"
                    OnRowCancelingEdit="GridViewUser_RowCancelingEdit" OnRowEditing="GridViewUser_RowEditing"
                    OnRowUpdating="GridViewUser_RowUpdating" OnRowDeleting="GridViewUser_RowDeleting"
                    OnRowDataBound="GridViewUser_RowDataBound" OnSelectedIndexChanged="GridViewUser_SelectedIndexChanged"
                    Width="1243px" OnRowCommand="GridViewUser_RowCommand" OnSorting="GridViewUser_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbltest" Text="Edit" CommandName="Edit" runat="server"
                                    CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lnkDelete" Text="Delete" CommandName="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                    CommandArgument='<%#Eval("Id")%>' ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Id" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtid" runat="server" MaxLength="30" Text='<%#Eval("Id")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Id" SortExpression="Id" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInstallid" runat="server" MaxLength="30" Text='<%#Eval("InstallId")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblInstallid" runat="server" Text='<%#Eval("InstallId")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Picture" SortExpression="picture">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPicture" Text="Picture" CommandName="ShowPicture" runat="server"
                                    CommandArgument='<%#Eval("picture")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="First Name" SortExpression="FristName" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" Text='<%#Eval("FristName")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FristName")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last name" SortExpression="Lastname" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtlastname" runat="server" Text='<%# Bind("Lastname") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("Lastname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Primary Trade" SortExpression="PTradeName" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPTrade" runat="server" Text='<%#Eval("PTradeName")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPTrade" runat="server" Text='<%#Eval("PTradeName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Secondary Trade" SortExpression="STradeName" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSTrade" runat="server" Text='<%#Eval("STradeName")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSTrade" runat="server" Text='<%#Eval("STradeName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Designation" SortExpression="Designation" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" SortExpression="Status">
                            <ItemTemplate>
                                <asp:HiddenField ID="lblStatus" runat="server" Value='<%#Eval("Status")%>'></asp:HiddenField>
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" runat="server" DataValueField='<%#Eval("Status")%>'>
                                    <asp:ListItem Text="Applicant" Value="Applicant"></asp:ListItem>
                                    <asp:ListItem Text="Phone/Video Screened" Value="PhoneScreened"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                    <asp:ListItem Text="Interview Date" Value="InterviewDate"></asp:ListItem>
                                    <asp:ListItem Text="Offer Made" Value="OfferMade"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                    <asp:ListItem Text="Deactive" Value="Deactive"></asp:ListItem>
                                    <asp:ListItem Text="Install Prospect" Value="Install Prospect"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Added By" SortExpression="AddedBy" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAddedBy" runat="server" Text='<%#Eval("AddedBy")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Added On" SortExpression="CreatedDateTime" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblHireDate" runat="server" Text='<%#Eval("CreatedDateTime")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Source" SortExpression="Source" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSource" runat="server" Text='<%#Eval("Source")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        

                        <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Center" SortExpression="Phone">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Zip" ItemStyle-HorizontalAlign="Center" SortExpression="Zip">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZip" runat="server" Text='<%# Bind("Zip") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zip") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--   <asp:TemplateField HeaderText="Delete User">
                <ItemTemplate>
                 <asp:HiddenField ID="id" runat="server" Value='<%# Eval("Id") %>' />
                    <asp:Button ID="btndelete" runat="server" CommandName="delete" Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>--%>

                        <%--<asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDelete" runat="server" Text="Delete" CommandName="Delete"
                                             CommandArgument='<%#Eval("ID")%>' OnClientClick="return ConfirmDelete()"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        <%--
                    <asp:TemplateField>
                        <EditItemTemplate>
                          <asp:LinkButton ID="lnkAction" Text="Cancel" CommandName="SelPastAttendance" CommandArgument='<%#Eval("Id")+","+ Eval("EmployeePrimaryDetail.EmployeeCode")+","+ Eval("RequestStatus") %>'
                                            runat="server"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        --%>
                    </Columns>
                </asp:GridView>
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <br />
            <br />
            <div class="btn_sec">
                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"  />
            </div>
        </div>
    </div>
    <%--<asp:UpdatePanel ID="updatepanel1" runat="server">
                    <ContentTemplate>--%>
    <asp:Button ID="Button1" Style="display: none;" runat="server" Text="Button" />
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="Button1" 
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <asp:Image ID="img_InstallerImage" runat="server" Height="150px" Width="118px" />
        <br />
        <asp:Button ID="btnClose" runat="server" Text="Close" />
    </asp:Panel>



       <asp:Panel ID="panelPopup" runat="server">
        <div id="light" class="white_content">
            <h3>Reason
                </h3>
            <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
                Close</a>
            <table width="100%" style="border: Solid 3px #b04547; width: 100%; height: 70%"
                cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" colspan="2" style="height:15px;">
                    <asp:TextBox ID="txtReason" runat="server" placeholder="Enter Reason" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqReason" runat="server" ErrorMessage="Enter reason" ControlToValidate="txtReason" ValidationGroup="Reason"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                         <asp:Button ID="btnSave" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                            Style="height: 26px; font-weight: 700; line-height: 1em;" Text="Save" Width="100px" ValidationGroup="Reason"
                            TabIndex="119" OnClick="btnSave_Click" />
                        <%--<asp:Button ID="Button2" runat="server" OnClick="" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div id="fade" class="black_overlay">
    </div>
    <asp:Panel ID="panel2" runat="server">
        <div id="interviewDatelite" class="white_content">
            <h3>Interview Details
                </h3>
            <a href="javascript:void(0)" onclick="document.getElementById('interviewDatelight').style.display='none';document.getElementById('interviewDatefade').style.display='none'">
                Close</a>
            <table width="100%" style="border: Solid 3px #b04547; width: 100%; height: 70%"
                cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="height:15px;">
                    <asp:TextBox ID="dtInterviewDate" placeholder="Select Date" runat="server" ClientIDMode="Static" onkeypress="return false" TabIndex="104" Width="127px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="dtInterviewDate" Format="MM/dd/yyyy" runat="server"></cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Date" ControlToValidate="dtInterviewDate" ValidationGroup="InterviewDate"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInsteviewtime" runat="server" TabIndex="105" Width="112px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                         <asp:Button ID="btnSaveInterview" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                            Style="height: 26px; font-weight: 700; line-height: 1em;" Text="Save" Width="100px" ValidationGroup="InterviewDate"
                            TabIndex="119" OnClick="btnSaveInterview_Click" />
                        <%--<asp:Button ID="Button2" runat="server" OnClick="" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div id="interviewDatefade" class="black_overlay">
    </div>






    <asp:Panel ID="panel3" runat="server">
        <div id="litePassword" class="white_content">
            <h3>Password
                </h3>
            <a href="javascript:void(0)" onclick="document.getElementById('litePassword').style.display='none';document.getElementById('fadePassword').style.display='none'">
                Close</a>
            <table width="100%" style="border: Solid 3px #b04547; width: 100%; height: 70%"
                cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="height:54px;width:200px;">
                    Enter Password To Change Status
                    </td>
                    <td align="center" style="height:54px;">
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="height:54px;">
                         <asp:Button ID="btnPassword" runat="server" BackColor="#327FB5" ForeColor="White" Height="32px"
                            Style="height: 26px; font-weight: 700; line-height: 1em;" Text="Save" Width="100px" ValidationGroup="Password"
                            TabIndex="119" OnClick="btnPassword_Click"  />
                        <%--<asp:Button ID="Button2" runat="server" OnClick="" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div id="fadePassword" class="black_overlay">
    </div>

    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridViewUser" EventName="RowCommand" />
        </Triggers>
                </asp:UpdatePanel>--%>
</asp:Content>
