<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Price_control.aspx.cs" Inherits="JG_Prospect.Sr_App.Price_control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete1() {
            var res = confirm("Are you sure you wish to remove this Product?");
            if (res == true)
                return true;
            else
                return false;
        }
    </script>



    <script type="text/javascript">

        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }
        $(document).ready(function () {

            $('.show_hide .sub').hide(); // Show Hide Feature onpage load
            $('.show_hide>.sub').show();


            $('h3 span.span_click').click(function () { //Show Hide Feature title onclick 
                $('.show_hide>.sub').animate({ 'height': 'hide', 'opacity': 'hide' }, 300);
                $(this).parent('h3').next('.sub').animate({ 'height': 'show', 'opacity': 'show' }, 300);
                $(this).parent('h3').children('.sub').animate({ 'height': 'show', 'opacity': 'show' }, 300);
                $('.show_hide h3 span').removeClass('down');
                $(this).addClass('down');
            });

            $('div.span_click').click(function () { //Show Hide Feature submenu onclick 
                $(this).closest('.sub').children('.span_click').next('.sub').animate({ 'height': 'hide', 'opacity': 'hide' }, 300);
                $(this).next('.sub').animate({ 'height': 'show', 'opacity': 'show' }, 300);
                $(this).next('.sub').animate({ 'height': 'show', 'opacity': 'show' }, 300);
                $('div.span_click').removeClass('down');
                $(this).addClass('down');
            });



        });
    </script>
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
            left: 0%;
            width: auto;
            height: auto;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        table {
  width: 100%;
  margin-bottom: 20px;
}
table > thead > tr > th,
table > tbody > tr > th,
table > tfoot > tr > th,
table > thead > tr > td,
table > tbody > tr > td,
table > tfoot > tr > td {
  padding: 8px;
  line-height: 1.42857143;
  vertical-align: top;
  border-top: 1px solid #ddd;
}
table > thead > tr > th {
  vertical-align: bottom;
  border-bottom: 2px solid #ddd;
}
table > caption + thead > tr:first-child > th,
table > colgroup + thead > tr:first-child > th,
table > thead:first-child > tr:first-child > th,
table > caption + thead > tr:first-child > td,
table > colgroup + thead > tr:first-child > td,
table > thead:first-child > tr:first-child > td {
  border-top: 0;
}
table > tbody + tbody {
  border-top: 2px solid #ddd;
}
table .table {
  background-color: #fff;
}
table-condensed > thead > tr > th,
table-condensed > tbody > tr > th,
table-condensed > tfoot > tr > th,
table-condensed > thead > tr > td,
table-condensed > tbody > tr > td,
table-condensed > tfoot > tr > td {
  padding: 5px;
}
table-bordered {
  border: 1px solid #ddd;
}
td {
  border: 1px solid #ddd !important;
}
table-bordered > thead > tr > th,
table-bordered > thead > tr > td {
  border-bottom-width: 2px;
}
table-striped > tbody > tr:nth-child(odd) > td,
table-striped > tbody > tr:nth-child(odd) > th {
  background-color: #f9f9f9;
}
.grid td {
    line-height: 14px !important;
    padding: 10px;
    text-align: left;
    min-height: 30px;
    border: #ccc 1px solid !important;
    border-top: none;
    border-bottom: none;
}
table-hover > tbody > tr:hover > td,
table-hover > tbody > tr:hover > th {
  background-color: #f5f5f5;
}
table col[class*="col-"] {
  position: static;
  display: table-column;
  float: none;
}
table td[class*="col-"],
table th[class*="col-"] {
  position: static;
  display: table-cell;
  float: none;
}
.table > thead > tr > td.active,
.table > tbody > tr > td.active,
.table > tfoot > tr > td.active,
.table > thead > tr > th.active,
.table > tbody > tr > th.active,
.table > tfoot > tr > th.active,
.table > thead > tr.active > td,
.table > tbody > tr.active > td,
.table > tfoot > tr.active > td,
.table > thead > tr.active > th,
.table > tbody > tr.active > th,
.table > tfoot > tr.active > th {
  background-color: #f5f5f5;
}
.table-hover > tbody > tr > td.active:hover,
.table-hover > tbody > tr > th.active:hover,
.table-hover > tbody > tr.active:hover > td,
.table-hover > tbody > tr.active:hover > th {
  background-color: #e8e8e8;
}
.table > thead > tr > td.success,
.table > tbody > tr > td.success,
.table > tfoot > tr > td.success,
.table > thead > tr > th.success,
.table > tbody > tr > th.success,
.table > tfoot > tr > th.success,
.table > thead > tr.success > td,
.table > tbody > tr.success > td,
.table > tfoot > tr.success > td,
.table > thead > tr.success > th,
.table > tbody > tr.success > th,
.table > tfoot > tr.success > th {
  background-color: #dff0d8;
}
.table-hover > tbody > tr > td.success:hover,
.table-hover > tbody > tr > th.success:hover,
.table-hover > tbody > tr.success:hover > td,
.table-hover > tbody > tr.success:hover > th {
  background-color: #d0e9c6;
}
.table > thead > tr > td.info,
.table > tbody > tr > td.info,
.table > tfoot > tr > td.info,
.table > thead > tr > th.info,
.table > tbody > tr > th.info,
.table > tfoot > tr > th.info,
.table > thead > tr.info > td,
.table > tbody > tr.info > td,
.table > tfoot > tr.info > td,
.table > thead > tr.info > th,
.table > tbody > tr.info > th,
.table > tfoot > tr.info > th {
  background-color: #d9edf7;
}
.table-hover > tbody > tr > td.info:hover,
.table-hover > tbody > tr > th.info:hover,
.table-hover > tbody > tr.info:hover > td,
.table-hover > tbody > tr.info:hover > th {
  background-color: #c4e3f3;
}
.table > thead > tr > td.warning,
.table > tbody > tr > td.warning,
.table > tfoot > tr > td.warning,
.table > thead > tr > th.warning,
.table > tbody > tr > th.warning,
.table > tfoot > tr > th.warning,
.table > thead > tr.warning > td,
.table > tbody > tr.warning > td,
.table > tfoot > tr.warning > td,
.table > thead > tr.warning > th,
.table > tbody > tr.warning > th,
.table > tfoot > tr.warning > th {
  background-color: #fcf8e3;
}
.table-hover > tbody > tr > td.warning:hover,
.table-hover > tbody > tr > th.warning:hover,
.table-hover > tbody > tr.warning:hover > td,
.table-hover > tbody > tr.warning:hover > th {
  background-color: #faf2cc;
}
.table > thead > tr > td.danger,
.table > tbody > tr > td.danger,
.table > tfoot > tr > td.danger,
.table > thead > tr > th.danger,
.table > tbody > tr > th.danger,
.table > tfoot > tr > th.danger,
.table > thead > tr.danger > td,
.table > tbody > tr.danger > td,
.table > tfoot > tr.danger > td,
.table > thead > tr.danger > th,
.table > tbody > tr.danger > th,
.table > tfoot > tr.danger > th {
  background-color: #f2dede;
}
.table-hover > tbody > tr > td.danger:hover,
.table-hover > tbody > tr > th.danger:hover,
.table-hover > tbody > tr.danger:hover > td,
.table-hover > tbody > tr.danger:hover > th {
  background-color: #ebcccc;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <!-- appointment tabs section start -->
        <ul class="appointment_tab">
            <li><a href="Price_control.aspx">Product Line Estimate</a></li>
            <li><a href="Inventory.aspx">Inventory</a></li>
            <li><a href="Maintenace.aspx">Maintainance</a></li>
        </ul>
        <!-- appointment tabs section end -->
        <h1>
            Admin Price Control
        </h1>


        <asp:Label ID="lblerrorNew" runat="server"></asp:Label>
        <%--<div class="show_hide">
            <h3>
                <span class="span_click">Painting</span><a href="#">Painting Contract</a><a href="#">Painting
                    W.O.</a><a href="#">Painting Vendors</a></h3>
            <div class="sub">
                <div class="span_click">
                    Shutter Style
                </div>
                <div class="sub">
                    Open Louver Standard<br />
                    Raised Panel Standard
                </div>
                <div class="span_click">
                    Color
                </div>
                <div class="sub">
                    White<br />
                    Red
                </div>
            </div>
        </div>--%>


          <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
          <asp:LinkButton ID="lnkAddProduct" runat="server" OnClick="lnkAddProduct_Click">Add Product Line Item</asp:LinkButton>

        <asp:Panel ID="pnlAddProduct" runat="server" Visible="false">
             <br />
             <asp:Label ID="lblProductName" runat="server" Text="Enter Product Name"></asp:Label> <br /> <br />
             <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox> &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Product Line Item" ControlToValidate="txtProductName" ForeColor="Red" Display="Dynamic" ValidationGroup="a1"></asp:RequiredFieldValidator>
           <br/><br />
             <asp:Button ID="btnSave1" runat="server" CausesValidation="true" OnClick="btnSave1_Click" Text="Save" ValidationGroup="a1" />  &nbsp;&nbsp; <asp:Button ID="btncancel1" runat="server" OnClick="btncancel1_Click" Text="Cancel"  />
        </asp:Panel>

        <div class="show_hide">
            <h3>
                <%--<span class="span_click down">Shutter</span>--%>
               <%-- <a href="CustomerEmailTemplate.aspx">Default Customer Email</a>--%>
                <a href="ContractTemplate.aspx?ProductId=1" runat="server" visible="false">Shutter Contract Template</a><a
                    href="ShutterTemplate.aspx">Shutter W.O.</a>
                <label>Change%</label>
                <asp:TextBox ID="txtpercentage" runat="server" Text="0" Width="50px"
                    onkeyup="javascript:Numeric(this)" ValidationGroup="percentagechange"
                    onkeypress="return isNumericKey(event);" MaxLength="5"></asp:TextBox>
                <%--<asp:ImageButton ID="btnPlus" runat="server" ImageUrl="../img/plus.png" 
                    onclick="btnPlus_Click" ValidationGroup="percentagechange" />
                   <asp:ImageButton ID="btnMinus" runat="server" ImageUrl="../img/minus.png" 
                    onclick="btnMinus_Click" ValidationGroup="percentagechange" />                   --%>
                <asp:RequiredFieldValidator ID="requirepercentage" runat="server" ValidationGroup="percentagechange" ErrorMessage="Please fill percentage" ForeColor="Red" Font-Size="Small" ControlToValidate="txtpercentage"></asp:RequiredFieldValidator>
                <%-- <a href="Vendors.aspx">Shutter Vendors</a>--%>
            </h3>



            <div class="grid">
                
                <asp:GridView ID="gvProductLine" GridLines="Both" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                    OnRowCommand="gvProductLine_RowCommand" OnRowDataBound="gvProductLine_RowDataBound" ><%-- Width="636px"--%>
                    <Columns>
                        <asp:TemplateField ShowHeader="True" HeaderText="Product Line Item" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblInstallid" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contract Template">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnContractTemplate" CommandName="ContractTemplate" runat="server"
                                    CommandArgument='<%#Eval("ProductName")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField ShowHeader="True" HeaderText="Install Selling Pricing" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblInstallsale" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>

                         <asp:TemplateField ShowHeader="True" HeaderText="Labor Pricing" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLaborPrice" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>

                         <asp:TemplateField ShowHeader="True" HeaderText="Tool Checklist" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblToolChecklist" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>

                         <asp:TemplateField ShowHeader="True" HeaderText="Edit" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditProduct" CommandArgument='<%#Eval("ProductId")%>'>Edit</asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>


                        <asp:TemplateField ShowHeader="True" HeaderText="Delete" ControlStyle-ForeColor="Black"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" runat="server" OnClientClick="return ConfirmOnDelete1();" CommandName="DeleteProduct" CommandArgument='<%#Eval("ProductId")%>'>Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ControlStyle ForeColor="Black" />
                            <ControlStyle ForeColor="Black" />
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>
            </div>


            <%--<asp:Button ID="Button1" runat="server" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="mp_sold" runat="server" TargetControlID="Button1"
                PopupControlID="pnlsold" CancelControlID="btnsave">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlsold"  runat="server" ScrollBars="Auto" BackColor="White" Height="" Width="500px"
                Style="display: none; position: fixed;">--%>
            <asp:Panel ID="panelPopup" runat="server">
                <div id="light" class="white_content">
                    <center><h1>Contract Template</h1></center>
                    <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">Close</a>
                    <div>
                        <%--<h2>Header Template</h2>
                        <cc1:Editor ID="HeaderEditor" Width="1000px" Height="400px" runat="server" />--%>
                        <h2>Body Template</h2>
                        <cc1:Editor ID="BodyEditor" Width="1000px" Height="400px" runat="server" />
                        <cc1:Editor ID="BodyEditor2" Width="1000px" Height="400px" runat="server" />
                        <%--<h2>Footer Template</h2>
                        <asp:Image ImageUrl="~/img/Bar3.png" ID="img1" runat="server" Width="1000px" Height="40px" />
                        <cc1:Editor ID="FooterEditor" Width="1000px" Height="600px" runat="server" />--%>
                    </div>
                    <div class="btn_sec">
                        <asp:Button ID="btnsave" runat="server" Text="Update" ValidationGroup="save" OnClick="btnsave_Click" />
                    </div>
                </div>
            </asp:Panel>
            <div id="fade" class="black_overlay">
            </div>
            <%--</asp:Panel>--%>


            <%--<div class="sub">
                <div class="span_click">
                    Shutter Style
                </div>
                <div class="sub grid no_width">
                    <asp:UpdatePanel ID="updateshutterstyle" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdShutterStyle" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="tableClass"
                                Width="1000px" AutoGenerateEditButton="True"  OnRowEditing="grdShutterStyle_RowEditing"
                                OnRowUpdating="grdShutterStyle_RowUpdating" OnRowCancelingEdit="grdShutterStyle_RowCancelingEdit"  HeaderStyle-Wrap="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Shutter Style" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblshutterstyle" Text='<%#Eval("shutterstyle")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server" Text='<%#Eval("price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprice" runat="server" Text='<%#Eval("price")%>' onkeyup="javascript:Numeric(this)" MaxLength="8" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                        </EditItemTemplate>                                        
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="span_click">
                    Shutter Color
                </div>
                <div class="sub grid">
                    <asp:UpdatePanel ID="Updateshuttercolor" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdShuttercolor" runat="server" AutoGenerateColumns="false" DataKeyNames="ColorCode" CssClass="tableClass"
                                Width="1000px" AutoGenerateEditButton="true" OnRowCancelingEdit="grdShuttercolor_RowCancelingEdit"
                                OnRowEditing="grdShuttercolor_RowEditing" OnRowUpdating="grdShuttercolor_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Shutter color" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblshuttercolor" Text='<%#Eval("ColorName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprice" runat="server" MaxLength="8" Text='<%#Eval("Price")%>' onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="span_click">
                    Surface of mount
                </div>
                <div class="sub grid">
                    <asp:UpdatePanel ID="Updatesurfacemount" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdsurfaceofmount" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" CssClass="tableClass"
                                Width="1000px" AutoGenerateEditButton="true" OnRowCancelingEdit="grdsurfaceofmount_RowCancelingEdit"
                                OnRowEditing="grdsurfaceofmount_RowEditing" OnRowUpdating="grdsurfaceofmount_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Surface of Mount" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblsurfaceofmount" Text='<%#Eval("SurfaceMount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprice" runat="server" MaxLength="8" Text='<%#Eval("Price")%>' onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="span_click">
                    Width
                </div>
                <div class="sub grid">
                    <asp:UpdatePanel ID="Updateshutterwidth" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdshutterwidth" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" CssClass="tableClass"
                                Width="1000px" AutoGenerateEditButton="true" OnRowCancelingEdit="grdshutterwidth_RowCancelingEdit"
                                OnRowEditing="grdshutterwidth_RowEditing" OnRowUpdating="grdshutterwidth_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Shutter Width" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblshutterwidth" Text='<%#Eval("width")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprice" runat="server" MaxLength="8" Text='<%#Eval("Price")%>' onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="span_click">
                    Accessories
                </div>
                <div class="sub grid">
                    <asp:UpdatePanel ID="Updateaccessories" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdaccessories" runat="server" AutoGenerateColumns="false" DataKeyNames="Accessorie_Id"
                                Width="1000px" AutoGenerateEditButton="true" OnRowCancelingEdit="grdaccessories_RowCancelingEdit" CssClass="tableClass"
                                OnRowEditing="grdaccessories_RowEditing" OnRowUpdating="grdaccessories_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Shutter Accessories" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblshutteraccessories" Text='<%#Eval("AccessoriesName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprice" runat="server" MaxLength="8" Text='<%#Eval("Price")%>' onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                  <div class="span_click">
                    Shutter Top
                </div>
                <div class="sub grid">
                    <asp:UpdatePanel ID="UpdateShutterTop" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdshuttertop" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                Width="1000px" AutoGenerateEditButton="true" CssClass="tableClass"
                                OnRowEditing="grdshuttertop_RowEditing" 
                                OnRowUpdating="grdshuttertop_RowUpdating" 
                                onrowcancelingedit="grdshuttertop_RowCancelingEdit">
                                <Columns>
                                    <asp:TemplateField HeaderText="Shutter Top" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblshuttertop" Text='<%#Eval("ShutterTop")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="500px" HeaderStyle-Wrap="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtprice" runat="server" MaxLength="8" Text='<%#Eval("Price")%>' onkeyup="javascript:Numeric(this)" onkeypress="return isNumericKey(event);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>--%>
            <br />
            <br />
            <div class="clr">
            </div>
        </div>

                  </ContentTemplate>
 
        </asp:UpdatePanel>
    </div>
  <%--  </div>
    </div>--%>
</asp:Content>
