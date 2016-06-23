<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskList.ascx.cs" Inherits="TestingTask.TaskList" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>

<div class="row">
    <div class="col-lg-2 col-md-2">
        <div class="form-group">
            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="Title"></asp:TextBox>
        </div>
    </div>
    <div class="col-lg-2 col-md-2">
        <div class="form-group">
            <asp:DropDownList runat="server" ID="ddlDesignation" class="form-control" OnSelectedIndexChanged="DdlDesignation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>
    <div class="col-lg-3 col-md-3">
        <div class="form-group">
            <asp:UpdatePanel ID="updAssignedUser" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:DropDownCheckBoxes runat="server" ID="ddlAsignedUser" Width="180px" >
                <Style DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="200" />
            </asp:DropDownCheckBoxes>
                    </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddlDesignation" />
               </Triggers>
                </asp:UpdatePanel>
        </div>
    </div>
    <div class="col-lg-2 col-md-2">
        <div class="form-group">
            <asp:DropDownList runat="server" ID="ddlStatus" class="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="col-lg-2 col-md-2">
        <div class="form-group">
            <asp:TextBox ID="txtDatePicker" runat="server" class="form-control" placeholder="Date"></asp:TextBox>
        </div>
    </div>
    <div class="col-lg-1 col-md-1">
        <div class="form-group">
            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary btn-md" OnClick="btnSearch_Click" />
        </div>
    </div>

</div>
<div>
    <asp:Label ID="lblmsg" runat="server" Visible="false"  ForeColor="Red" Font-Size="13px"></asp:Label>
      <asp:UpdatePanel ID="UpdTask" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
    <asp:Repeater ID="rptTaskList" runat="server" OnItemDataBound="RptTaskList_ItemDataBound">
        <HeaderTemplate>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Designation</th>
                        <th  style="width:30%">Assigned User</th>
                        <th>Status</th>
                        <th>Notes</th>
                    </tr>
                </thead>
        </HeaderTemplate>

        <ItemTemplate>
            <tbody>
                <tr>
                    <td><%# Eval("CustomID") %></td>
                    <td><%# Eval("Title") %></td>
                    <td> <asp:DropDownList runat="server" ID="ddlDesignation" class="form-control"></asp:DropDownList></td>
                    <td><asp:DropDownCheckBoxes runat="server" ID="ddlAsignedUser" Width="180px" >
                <Style DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="200" />
            </asp:DropDownCheckBoxes></td>
                    <td><asp:DropDownList runat="server" ID="ddlStatus" class="form-control"></asp:DropDownList></td>
                    <td><%# Eval("Notes") %></td>
            </tbody>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>

    </asp:Repeater>
                      </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnSearch" />
               </Triggers>
                </asp:UpdatePanel>
</div>


<style type="text/css">
    label {
        display: inline-block;
        padding-left: 10px;
    }
</style>


