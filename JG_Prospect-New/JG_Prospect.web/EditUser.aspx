<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs" Inherits="JG_Prospect.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ConfirmDelete() {
            var Ok = confirm('Are you sure you want to Delete this User?');
            if (Ok)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="right_panel">
     <ul class="appointment_tab">
          <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
          <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
          <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
    </ul>
    <h1>
        Edit User</h1>
    <div class="form_panel">
  
        <span>
            <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
        </span>

        
        <div class="grid">
       
        <asp:UpdatePanel ID="updatepanel" runat="server" >
        <ContentTemplate >
            <asp:GridView ID="GridViewUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="tableClass"
                AutoGenerateEditButton="false" OnRowCancelingEdit="GridViewUser_RowCancelingEdit"
                OnRowEditing="GridViewUser_RowEditing" OnRowUpdating="GridViewUser_RowUpdating"
                OnRowDeleting="GridViewUser_RowDeleting" OnRowDataBound="GridViewUser_RowDataBound"
                Width="1550px"  HeaderStyle-Wrap="true">
                
                <Columns>
                    <asp:TemplateField ShowHeader="True" HeaderText="User Name" ControlStyle-ForeColor="Black"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtusername" runat="server" MaxLength="30" Text='<%#Eval("Username")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblusername" runat="server" Text='<%#Eval("Username")%>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Login Id" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="250px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtloginid" runat="server" MaxLength="40" Text='<%#Eval("Login_Id")%>' Width="250px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblloginid" runat="server" Text='<%#Eval("Login_Id")%>' Width="250px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="250px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%#Eval("Email")%>' Width="250px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>' Width="250px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPassword" runat="server" Text='<%#Eval("Password")%>' Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password")%>' Width="50px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usertype" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUsertype" runat="server" DataValueField='<%#Eval("Usertype")%>'>
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>             
                                <asp:ListItem Text="Admin Secretary" Value="AdminSec"></asp:ListItem>                            
                                <asp:ListItem Text="Jr Sales Executive" Value="JSE"></asp:ListItem>
                                <asp:ListItem Text="Sr Sales Executive" Value="SSE"></asp:ListItem>
                                <asp:ListItem Text="Marketing Manager" Value="MM"></asp:ListItem>
                                <asp:ListItem Text="Sales Manager" Value="SM"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlUsertype" runat="server" DataValueField='<%#Eval("Usertype")%>'
                                Enabled="false">                                
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>             
                                <asp:ListItem Text="Admin Secretary" Value="AdminSec"></asp:ListItem>                            
                                <asp:ListItem Text="Jr Sales Executive" Value="JSE"></asp:ListItem>
                                <asp:ListItem Text="Sr Sales Executive" Value="SSE"></asp:ListItem>
                                <asp:ListItem Text="Marketing Manager" Value="MM"></asp:ListItem>
                                <asp:ListItem Text="Sales Manager" Value="SM"></asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:Label ID="lblUsertype" runat="server" Text='<%#Eval("Usertype")%>'></asp:Label>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlDesignation" runat="server" DataValueField='<%#Eval("Designation")%>'>                           
                                <asp:ListItem Text="Administrator" Value="Admin"></asp:ListItem>
                                 <asp:ListItem Text="Admin Secretary" Value="AdminSec"></asp:ListItem> 
                                 <asp:ListItem Text="Jr Sales Executive" Value="JSE"></asp:ListItem> 
                                <asp:ListItem Text="Sr Sales Executive" Value="SSE"></asp:ListItem>
                                <asp:ListItem Text="Sales Manager" Value="SM"></asp:ListItem>
                                <asp:ListItem Text="Marketing Manager" Value="MM"></asp:ListItem>
                            </asp:DropDownList>                           
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDesignation" runat="server" DataValueField='<%#Eval("Designation")%>'
                                Enabled="false">
                                <asp:ListItem Text="Administrator" Value="Admin"></asp:ListItem>
                                 <asp:ListItem Text="Admin Secretary" Value="AdminSec"></asp:ListItem> 
                                 <asp:ListItem Text="Jr Sales Executive" Value="JSE"></asp:ListItem> 
                                <asp:ListItem Text="Sr Sales Executive" Value="SSE"></asp:ListItem>
                                <asp:ListItem Text="Sales Manager" Value="SM"></asp:ListItem>
                                <asp:ListItem Text="Marketing Manager" Value="MM"></asp:ListItem>
                            </asp:DropDownList>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" DataValueField='<%#Eval("Status")%>'>
                                <asp:ListItem Value="Active"></asp:ListItem>
                                <asp:ListItem Value="Deactive"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPhone" runat="server" onkeypress="return isNumericKey(event);" MaxLength="15" Text='<%#Eval("Phone")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Phone")%>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderStyle-Wrap="true">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddress" runat="server" Text='<%#Eval("Address")%>' Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address")%>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ShowHeader="True" HeaderText="Delete User" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" HeaderStyle-Wrap="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdelete" runat="server" CausesValidation="false" OnClientClick="return ConfirmDelete()"
                                CommandName="Delete" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="True" HeaderText="Edit User Info" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px" HeaderStyle-Wrap="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkupdate" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="lnkcancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                   
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <%--   <asp:TemplateField HeaderText="Delete User">
                <ItemTemplate>
                 <asp:HiddenField ID="id" runat="server" Value='<%# Eval("Id") %>' />
                    <asp:Button ID="btndelete" runat="server" CommandName="delete" Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>--%>
                </Columns>
       
            </asp:GridView>
                  </ContentTemplate>
        </asp:UpdatePanel>
        </div>
                
    </div>
    </div>
</asp:Content>
