<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskGenerator.ascx.cs" Inherits="JG_Prospect.Sr_App.Controls.TaskGenerator" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<link rel="stylesheet" href="css/jquery-ui.css" />
<script>
    $(function () {
        setDatePicker();
        var dlg = $("#divModal").dialog({
            modal: true,
            autoOpen: false,
            height: 600,
            width: 800,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
        dlg.parent().appendTo(jQuery("form:first"));
    });
    function EditTask(id) {
        $('#divModal').dialog("open");
    }
    function setDatePicker() {
        $('.datepicker').datepicker();
    }
</script>
<style>
    table tr th {
        border: 1px solid;
        padding: 0px;
    }

    table.table tr.trHeader {
        background: #000000;
        color: #ffffff;
    }

    .FirstRow {
        background: #f57575;
        padding: 2px;
    }

    .AlternateRow {
        background: #f6f1f3;
        padding: 2px;
    }
     .AlternateRow  a , .FirstRow a {
        color:#111;
    }
      .textbox
    {
        padding: 5px;
        border-radius: 5px;
        border: #b5b4b4 1px solid;
        margin-left: 0;
        margin-right: 0;
        margin-bottom: 5px;
    }

    .tablealign
    {
        margin-top: 5px;
    }

    div.dd_chk_select
    {
        height: 30px;
    }

        div.dd_chk_select div#caption
        {
            top: 7px;
            margin-left: 10px;
        }

    div.dd_chk_drop
    {
        top: 30px;
    }

</style>

<div class="tasklist">
    <asp:UpdatePanel ID="upnlTasks" runat="server">
        <ContentTemplate>
            <table>
                <tr>

                    <td><span style="color: #fefefe;">Filter Tasks:</span>

                        <asp:TextBox ID="txtSearch" placeholder="Task title" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlDesignation" runat="server">
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlUsers" runat="server">
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlTaskStatus" runat="server">
                            <asp:ListItem Text="--Status--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Assigned" Value="1"></asp:ListItem>
                            <asp:ListItem Text="In Progress" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Re-Opened"></asp:ListItem>
                            <asp:ListItem Text="Closed"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:TextBox ID="txtCreatedDate" CssClass="datepicker" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="/img/search_btn.png" CssClass="searchbtn" OnClick="btnSearch_Click" />
                    </td>
                    <td><span>
                        <a id="btnAdd" class="btn btn-primary" onclick="EditTask(0);">Add New</a></span> |
                    </td>
                    <td>
                        <a id="hypTaskListMore" href="../Sr_App/TaskList.aspx">View All</a>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvTasks" runat="server" EmptyDataText="No task available!" CssClass="table" Width="100%" CellSpacing="0" CellPadding="0" BorderStyle="Solid" BorderWidth="1" AutoGenerateColumns="False" OnRowDataBound="gvTasks_RowDataBound">
                <HeaderStyle CssClass="trHeader " />
                <RowStyle CssClass="FirstRow" />
                <AlternatingRowStyle CssClass="AlternateRow " />
                <Columns>
                    <asp:BoundField DataField="InstallId" HeaderText="Install ID" />
                    <asp:TemplateField HeaderText="Task Title">
                        <ItemTemplate>
                            <asp:HyperLink ID="hypTask" runat="server"><%# Eval("Title") %></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lblUserDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                            <%-- <asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned To">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedUser" runat="server" Text='<%# Eval("FristName") %>'></asp:Label>
                            <%--<asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskStatus" runat="server"></asp:Label>
                            <%--<asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Due Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskDueDate" runat="server" Text='<%#Eval("DueDate")%>'></asp:Label>
                            <%--<asp:DropDownList ID="ddlRole" runat="server">
                                            <asp:ListItem Text="Sr.Developer"></asp:ListItem>
                                            <asp:ListItem Text="Jr.Developer"></asp:ListItem>
                                        </asp:DropDownList>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>

<div id="divModal" title="Task : Title" style="background: #efeeee url(../img/form-bg.png) repeat-x top">

    <hr />
    <table class="tablealign" style="float: left">
        <tr>
            <td>Task Title:</td>
            <td>
                <asp:TextBox ID="txtTaskTitle" runat="server" CssClass="textbox"></asp:TextBox></td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Submit"
                runat="server" ControlToValidate="txtTaskTitle" ForeColor="Red" ErrorMessage="Please Enter Task Title" Display="None">                                 
            </asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td>Task Description:</td>
            <td>
                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="textbox"></asp:TextBox></td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Submit"
                runat="server" ControlToValidate="txtDescription" ForeColor="Red" ErrorMessage="Please Enter Task Description" Display="None">                                 
            </asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td>Designation:</td>
            <td>
                <asp:UpdatePanel ID="upnlDesignation" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="txtDesignation" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="txtDesignation_SelectedIndexChanged">
                           
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>Assigned:</td>
            <td>
                <asp:UpdatePanel ID="upnlUsers" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownCheckBoxes ID="ddcbAssigned" runat="server" UseSelectAllNode="false" AutoPostBack="True" OnSelectedIndexChanged="ddcbAssigned_SelectedIndexChanged">
                            <Style SelectBoxWidth="195" DropDownBoxBoxWidth="100" DropDownBoxBoxHeight="90" />

                        </asp:DropDownCheckBoxes>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <%--                       <asp:DropDownList ID="ddlAssigned" runat="server" CssClass="textbox" onchange="addRow(this)">
                        <asp:ListItem Text="Darmendra"></asp:ListItem>
                        <asp:ListItem Text="Shabir"></asp:ListItem>
                       </asp:DropDownList>--%>
                  
            </td>
        </tr>

        <%--            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" Text="Save" CssClass="ui-button" runat="server"></asp:Button>
                </td>
            </tr>--%>
    </table>
    <table class="tablealign" style="float: left; margin-left: 100px">
        <tr>
            <td>Staus:</td>
            <td>
                <asp:DropDownList ID="cmbStatus" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Started" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Submitted" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Approve" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Reject" Value="5"></asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>User Acceptance:</td>
            <td>
                <asp:DropDownList ID="ddlUserAcceptance" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Reject" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Due Date:</td>
            <td>
                <asp:TextBox ID="txtDueDate" runat="server" CssClass="textbox">
                </asp:TextBox>
                <asp:CalendarExtender ID="CEDueDate" runat="server" TargetControlID="txtDueDate"></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Submit"
                    runat="server" ControlToValidate="txtDueDate" ForeColor="Red" ErrorMessage="Please Enter Due Date" Display="None">                                 
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Hrs of Task:</td>
            <td>
                <asp:TextBox ID="txtHours" runat="server" CssClass="textbox">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Submit"
                    runat="server" ControlToValidate="txtHours" ForeColor="Red" ErrorMessage="Please Enter Hours Of Task" Display="None">                                 
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <div class="grid">

        <%--<table class="table" border="1" style="border-collapse: collapse; width: 100%" id="tbltaskuser">
            <thead class="trheader">
                <tr>
                    <th>user</th>
                    <th>status</th>
                    <th>note</th>
                    <th>files</th>
                    <th>datetime</th>
                </tr>
            </thead>
            <tbody style="border: 1px solid; padding: 10px;">
                <tr class="">
                    <td>justine</td>
                    <td>started</td>
                    <td>almost worked</td>
                    <td>0</td>
                    <td>6/12/2016</td>
                </tr>
                <tr class="">
                    <td>dharmendra</td>
                    <td>submitted</td>
                    <td>done all work</td>
                    <td>1</td>
                    <td>6/14/2016</td>
                </tr>
            </tbody>
        </table>--%>
        <asp:UpdatePanel ID="upnlGridview1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gdTaskUsers" runat="server" EmptyDataText="No data found!" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" AllowSorting="false"  BackColor="White" PageSize="3">
                    <%--<EmptyDataTemplate>
                    </EmptyDataTemplate>--%>
                    <Columns>
                        <asp:TemplateField ShowHeader="True" HeaderText="User" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbluser" runat="server" Text='<%#Eval("FristName")%>'></asp:Label>

                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <HeaderStyle Font-Size="Small"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false"  Visible="false" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbluserId" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false" Visible="false" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbluserType" runat="server" Text='<%#Eval("UserType")%>' Visible="false"></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Status" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>

                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <HeaderStyle Font-Size="Small"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="True" HeaderText="Notes" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblNotes" runat="server" Text='<%#Eval("Notes")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <HeaderStyle Font-Size="Small"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="True" HeaderText="Files" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblFiles" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <HeaderStyle Font-Size="Small"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="True" HeaderText="updateDate" ControlStyle-ForeColor="White" HeaderStyle-Font-Size="Small"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblupdateDate" runat="server" Text='<%#Eval("StartDate")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <HeaderStyle Font-Size="Small"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle BackColor="Black" ForeColor="White"></HeaderStyle>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- OnRowDataBound="GridView1_RowDataBound"    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"--%>
    </div>

    <br />
    <table cellspacing="0" cellpadding="0" width="950px" border="1" style="width: 100%; border-collapse: collapse;">
        <tr>
            <td>Notes:
            </td>
            <td>
                <asp:TextBox ID="txtLog" runat="server" TextMode="MultiLine" Width="400px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Attachment:
            </td>
            <td>
                <asp:FileUpload ID="fuUpload" runat="server" CssClass="textbox" />
            </td>
        </tr>
        <tr>
            <td>
                <div style="padding-left: 40px; padding-top: 20px;" class="btn_sec">
                    <asp:Button ID="btnNotes" runat="server" Text="Add Notes" CssClass="ui-button" />
                    <asp:Button ID="btnSaveTask" runat="server" Text="save" CssClass="ui-button" ValidationGroup="Submit" OnClick="btnSaveTask_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowSummary="False" ShowMessageBox="True" />
</div>
