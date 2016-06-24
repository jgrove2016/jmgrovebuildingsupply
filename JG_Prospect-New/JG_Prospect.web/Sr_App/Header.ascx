<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="JG_Prospect.Sr_App.Header" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!--tabs jquery-->
<%--<script type="text/javascript" src="../js/jquery.ui.core.js"></script>
<script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
<!--tabs jquery ends-->
<script type="text/javascript">
    $(function () {
        // Tabs
        $('#tabs').tabs();
    });
		</script>
<style type="text/css">
.ui-widget-header {
	border: 0;
	background:none/*{bgHeaderRepeat}*/;
	color: #222/*{fcHeader}*/;
}
</style>--%>
  <link href="../css/screen.css" rel="stylesheet" media="screen" type="text/css" />
<style type="text/css">

    .modal-header {
    padding: 2px 16px;
    background-color: #5cb85c;
    color: white;
}

/* Modal Body */
.modal-body {padding: 2px 16px;}

/* Modal Footer */
.modal-footer {
    padding: 2px 16px;
    background-color: #5cb85c;
    color: white;
}

/* Modal Content */
.modal-content {
    position: relative;
    background-color: #fefefe;
    margin: auto;
    padding: 0;
    border: 1px solid #888;
    width: 80%;
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
}

/* Add Animation */
@-webkit-keyframes animatetop {
    from {top: -300px; opacity: 0} 
    to {top: 0; opacity: 1}
}

@keyframes animatetop {
    from {top: -300px; opacity: 0}
    to {top: 0; opacity: 1}
}
.modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}



/* The Close Button */
.close {
    color: #aaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;
}
</style>

<script type="text/javascript">

    var modal = document.getElementById('divAddNewTask');

    // Get the button that opens the modal
    var btn = document.getElementById("<%=lbAddNewTask.ClientID%>");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal 
    btn.onclick = function () {
        modal.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
</script>
<div class="header">
    <img src="../img/logo.png" alt="logo" width="88" height="89" class="logo" />
    <div style="width:60%; height:120px; position:relative; margin-left:20%"> 
        <asp:LinkButton ID="lbAddNewTask" runat="server">Add New</asp:LinkButton>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:TextBoxWatermarkExtender ID="TBWESearch" runat="server" TargetControlID="txtSearch" WatermarkText="Search"></asp:TextBoxWatermarkExtender>
        <asp:TextBox ID="txtCreatedOn" runat="server" AutoPostBack="True" OnTextChanged="txtCreatedOn_TextChanged"></asp:TextBox>
        <asp:CalendarExtender ID="CECreatedOn" runat="server" TargetControlID="txtCreatedOn"></asp:CalendarExtender>
        <asp:DropDownList ID="ddlDesignationSearch" runat="server" style="width:180px;" AutoPostBack="True" OnSelectedIndexChanged="ddlDesignationSearch_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList ID="ddlAssignedToSearch" runat="server" style="width:180px;" AutoPostBack="True" OnSelectedIndexChanged="ddlAssignedToSearch_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList ID="ddlStatusSearch" runat="server" style="width:180px;" AutoPostBack="True" OnSelectedIndexChanged="ddlStatusSearch_SelectedIndexChanged">
                        <asp:ListItem Value="1">Assigned</asp:ListItem>
                        <asp:ListItem Value="2">In Progress</asp:ListItem>
                        <asp:ListItem Value="3">Pending</asp:ListItem>
                        <asp:ListItem Value="4">Re-Opened</asp:ListItem>
                        <asp:ListItem Value="5">Closed</asp:ListItem>
        </asp:DropDownList>

        <br />

        <asp:GridView ID="gvTaskList" runat="server" AutoGenerateColumns="False" Height="50px">
        <Columns>
            <asp:TemplateField HeaderText="Install ID">
                <HeaderStyle HorizontalAlign="Left"  />
                <ItemTemplate>
                <asp:Label runat="server" ID ="lblInstallId" Width="130px" Text='<%#Eval("InstallId") %>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Task Title">
                <HeaderStyle HorizontalAlign="Left"  />
                   <ItemTemplate>
                       <asp:LinkButton ID="lbTaskTitle" runat="server" Width="250px" Text='<%#Eval("TaskTitle") %>'></asp:LinkButton>             
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Designation">
                <ItemTemplate>

                    <asp:DropDownList ID="ddlDesignation" runat="server"  Width="200px"></asp:DropDownList>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Assigned">
                 <ItemTemplate>

                    <asp:DropDownList ID="ddlAssignedTo" runat="server"  Width="200px"></asp:DropDownList>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                   <ItemTemplate>

                    <asp:DropDownList ID="ddlStatus" runat="server"  Width="200px">

                         <asp:ListItem Value="0">Select One</asp:ListItem>
                        <asp:ListItem Value="1">Assigned</asp:ListItem>
                        <asp:ListItem Value="2">In Progress</asp:ListItem>
                        <asp:ListItem Value="3">Pending</asp:ListItem>
                        <asp:ListItem Value="4">Re-Opened</asp:ListItem>
                        <asp:ListItem Value="5">Closed</asp:ListItem>

                    </asp:DropDownList>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    

    <div class="user_panel">
        Welcome! <span>
            <asp:Label ID="lbluser" runat="server" Text="User"></asp:Label>
            <asp:Button ID="btnlogout" runat="server" Text="Logout" CssClass="cancel" ValidationGroup="header" OnClick="btnlogout_Click" />
        </span>&nbsp;<div class="clr">
        </div>
        <ul>
            <li><a href="home.aspx">Home</a></li>
            <li>|</li>
            <li><a href="/changepassword.aspx">Change Password</a></li>
        </ul>
        <div class="clr">
        </div>
        <ul>
            <li><a id="idPhoneLink" class="clsPhoneLink" onclick="GetPhoneDiv()">Phone Dashboard</a></li>
        </ul>
         <div class="clr">
        </div>
        <ul>
            <li>Voice Mail(0)</li>
            <li>|</li>
            <li>Chat(1)</li>
        </ul>
    </div>
    </div>
    <!--nav section-->
    <div class="nav">
        <ul>
            <li><a href="home.aspx">Home</a></li>
            <li><a href="new_customer.aspx">Add Customer</a></li>
            <%-- <li><a href="view_customer.aspx">Review / Edit Customer Estimate</a></li>--%>
            <li><a href="ProductEstimate.aspx">Product Estimate</a></li>
            <li><a href="SalesReort.aspx">Sales Report</a></li>
            <%--<li><a href="Vendors.aspx">Vendor Master</a></li>--%>
            <li id="Li_Jr_app" runat="server" visible="true"><a href="~/home.aspx" runat="server"
                id="Jr_app">Junior App</a></li>
            <li id="Li_Installer" runat="server" visible="true"><a href="~/Installer/InstallerHome.aspx" runat="server"
                id="A1">Installer</a></li>
               
                 
            <%-- <li><a href="/EditUser.aspx" runat="server" id="edituser">EditUser</a></li>
  <li><a href="/Accounts/newuser.aspx" runat="server" id ="newuser">CreateUser</a></li>--%>
        </ul>

        <div id="divAddNewTask" class="modal">

            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header">
                    <span class="close">×</span>
                    <h2>Modal Header</h2>
                </div>
                <div class="modal-body">
                    Task Title * :
                    <asp:TextBox ID="txtTaskTitle" runat="server"></asp:TextBox>
                    Task Description * :
                    <asp:TextBox ID="txtTaskDescription" runat="server" TextMode="MultiLine" Height="300px"></asp:TextBox>
                    Designation * :
                    <asp:DropDownList ID="ddlDesignation" runat="server"></asp:DropDownList>
                    Assigned To :
                    <asp:DropDownList ID="ddlAssignedTo" runat="server"></asp:DropDownList>
                    Status * :
                    <asp:DropDownList ID="ddlStatus" runat="server">
                       
                        <asp:ListItem Value="1">Assigned</asp:ListItem>
                        <asp:ListItem Value="2">In Progress</asp:ListItem>
                        <asp:ListItem Value="3">Pending</asp:ListItem>
                        <asp:ListItem Value="4">Re-Opened</asp:ListItem>
                        <asp:ListItem Value="5">Closed</asp:ListItem>

                    </asp:DropDownList>

                    User Acceptance :

                    <asp:DropDownList ID="ddlUserAcceptance" runat="server">
                         <asp:ListItem Value="1">Accepted</asp:ListItem>
                         <asp:ListItem Value="2">Rejected</asp:ListItem>
                    </asp:DropDownList>
                    Due Date * :  
                    <asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CEDueDate" runat="server" TargetControlID="txtDueDate"></asp:CalendarExtender>
                    Hrs of Task * :
                     <asp:TextBox ID="txtHoursOfTask" runat="server" MaxLength="4"></asp:TextBox>

                    <asp:GridView ID="gvUpdatesHistory" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Install ID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUsername" Text='<%#Eval("Username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Note">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNote" Text='<%#Eval("Note") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Files">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFiles" Text='<%#Eval("Files") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Updated On">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUpdatedOn" Text='<%#Eval("UpdatedOn") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    Notes :  <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="300px" Width="50%" runat="server"></asp:TextBox>
                    Attachment : <asp:FileUpload ID="fuAttachment" runat="server" />
                </div>
                <div class="modal-footer">
                    <h3>Modal Footer</h3>
                </div>
            </div>

        </div>
    </div>
<p>
    llllll</p>
