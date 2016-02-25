<%@ Page Title="" Language="C#" MasterPageFile="~/Installer/InstallerMaster.Master"
    AutoEventWireup="true" CodeBehind="InstallerHome.aspx.cs" Inherits="JG_Prospect.Installer.InstallerHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../js/jquery.datetimepicker.css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.datetimepicker.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#txtPrimary').datetimepicker({
                format: 'm/d/Y h:00 a'
            });
            $('#txtSecondary1').datetimepicker({
                format: 'm/d/Y h:00 a'
            });
            $('#txtSecondary2').datetimepicker({
                format: 'm/d/Y h:00 a'
           });
        });
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
            display: none;
        }

        .myfont {
            font-family: 'barcode_fontregular';
            font-size: 37px;
        }
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
            height: 50%;
            padding: 16px;
            border: 10px solid #327FB5;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
    <script type="text/javascript">
        function ClosePopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function overlay() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Dashboard</h1>
    <div class="form_panel">
        <div class="clr">
        </div>
        <asp:LinkButton ID="lblSignOffDocument" Text="Sign Off Document" runat="server" Style="float: right;"
            OnClick="lblSignOffDocument_Click"></asp:LinkButton>
        <br />
        <br />
        <div class="grid">
            <asp:GridView ID="grdInstaller" runat="server" AutoGenerateColumns="false" CssClass="tableClass"
                Width="100%" EmptyDataText="No Record Found" AllowSorting="true"
                onrowdatabound="grdInstaller_RowDataBound" 
                onsorting="grdInstaller_Sorting">
                <Columns>
                 <asp:TemplateField HeaderText="Ref #" HeaderStyle-Width="5%" SortExpression="ReferenceId">
                        <ItemTemplate>
                            <asp:Label ID="lblReferenceId" runat="server" Text='<%#Eval("ReferenceId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="10%" SortExpression="CustomerName">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="ZipCode" HeaderStyle-Width="5%" SortExpression="ZipCode">
                        <ItemTemplate>
                            <asp:Label ID="lblZipCode" runat="server" Text='<%#Eval("ZipCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Date Sold" DataField="SoldDate" DataFormatString="{0:MM/dd/yyyy}" SortExpression="SoldDate"
                        HeaderStyle-Width="10%" />
                    <asp:TemplateField HeaderText="CustomerID & Job#" HeaderStyle-Width="10%" SortExpression="CustomerIdJobId">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerIdJobId" runat="server" Text='<%#Eval("CustomerIdJobId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Packet PDFs'" HeaderStyle-Width="15%" SortExpression="JobPacket">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkJobPackets" runat="server" Text='<%#Eval("JobPacket") %>'
                                OnClick="lnkJobPackets_Click"></asp:LinkButton>
                            <asp:HiddenField ID="hdnproductid" runat="server" Value='<%#Eval("ProductId") %>' />
                            <asp:HiddenField ID="hdnProductTypeId" runat="server" Value='<%#Eval("ProductTypeId") %>' />
                            <asp:HiddenField ID="hdnJobSequenceId" runat="server" Value='<%#Eval("JobSequenceId") %>' />
                            <asp:HiddenField ID="hdnColour" runat="server" Value='<%#Eval("Colour") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Availability" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAvailability" runat="server" Text="Availability" OnClick="lnkAvailability_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="10%" SortExpression="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-Width="15%" />
                    <asp:BoundField HeaderText="Notes" DataField="Notes" HeaderStyle-Width="15%" />
                </Columns>
            </asp:GridView>
            <button id="btnFake" style="display: none" runat="server">
            </button>
            <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" TargetControlID="btnFake"
                PopupControlID="pnlpopup" CancelControlID="btnCancel">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="550px"
                Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                    cellspacing="0">
                    <tr style="background-color: #A33E3F">
                        <td colspan="3" style="height: 10%; color: White; font-weight: bold; font-size: larger;
                            width: 100%;" align="center">
                            Availability
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <strong>Primary</strong>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtPrimary" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblPrimary" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <strong>Secondary1</strong>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtSecondary1" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblSecondary1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%">
                            <strong>Secondary2</strong>
                        </td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtSecondary2" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblSecondary2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnSet" Style="width: 100px;" runat="server"
                                Text="Set Availability" OnClick="btnSet_Click"/>
                            <asp:Button ID="btnCancel" runat="server" Style="width: 100px;" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    

    <asp:Panel ID="panelPopup" runat="server">
        <div id="light" class="white_content">
           <div class="container">
        <h1>
            Job Packets</h1> 
               <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">
                Close</a>       
            <div class="form_panel">   
             <div class="scrollme">     
                <div class="btn_sec" style="float: right; padding-right:10px; padding-top: 35px;">
                    <asp:Button ID="btnGo" Text="Zip & Download" OnClick="btnGo_Click" runat="server" />
                </div><span id="ErrorMessage" style="color: Red" runat="server"></span>
              <div class="clr"></div>
                <div id="divmain" class="target">
                    <asp:GridView ID="Gridviewdocs" runat="server" AutoGenerateColumns="false" CssClass="grid"
                        Width="100%" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
                        OnRowDataBound="Gridviewdocs_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Hiddenid" runat="server" Value='<%# Eval("srno")%>' />
                                    <a href='<%# Eval("DocumentName","../CustomerDocs/{0}") %>' class="preview">
                                        <asp:Image ID="Image1" runat="server" Width="60px" CssClass="preview" ImageUrl='<%# Eval("DocumentName","~/CustomerDocs/{0}") %>'
                                            Height="90px" /></a>
                                    <asp:Label ID="labelfile" ForeColor="Black" runat="server" Text='<%# Eval("DocumentName") %>' />
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="labeldesc" ForeColor="Black" runat="server" Text='<%# Eval("DocDescription") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select File to Archieve">
                                <ItemTemplate>
                                    <asp:CheckBox ID="checkbox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
   </div>
        </div>
    </asp:Panel>
    <div id="fade" class="black_overlay">
    </div>




        </div>

</asp:Content>
