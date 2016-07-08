<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" EnableEventValidation ="false"
    CodeBehind="SalesReort.aspx.cs" Inherits="JG_Prospect.Sr_App.SalesReort" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    
    <link href="datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
     
  
     <script language="JavaScript" type="text/javascript">
         $(document).ready(function () {
             $(".date").datepicker();
             $('.time').ptTimeSelect();

         });
       
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
<!-- appointment tabs section end -->
      <h1>
        Sales Report</h1>
        <div class="form_panel_custom">
        <ul>
                <li style="width: 49%;">
            <table cellpadding="0" cellspacing="0" border="0" runat="server">
                        <tr>
                            <td>
                                <label>
                                    <strong>Select Period: </strong><span>*</span> From : </label>
                               <%-- <label style="width: 50px; text-align: right;">
                                    <span>*</span> From : </label>--%>
                                <asp:TextBox ID="txtfrmdate"  runat="server" TabIndex="2" CssClass="date" 
                                    onkeypress="return false" MaxLength="10" AutoPostBack="true"
                                    Style="width: 150px;" ontextchanged="txtfrmdate_TextChanged"></asp:TextBox>
                               <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrmdate">
                                </ajaxToolkit:CalendarExtender>--%>
                                <label></label>
                                <asp:RequiredFieldValidator ID="requirefrmdate" ControlToValidate="txtfrmdate" 
                                    runat="server" ErrorMessage=" Select From date" ForeColor="Red" ValidationGroup="display" >
                                </asp:RequiredFieldValidator>
                            </td>
                        
                            <td>
                                <label style="width: 50px; text-align: right;">
                                <span><br />
                                *</span> To : </label>
                                <asp:TextBox ID="txtTodate" CssClass="date" onkeypress="return false" 
                                    MaxLength="10" runat="server" TabIndex="3" AutoPostBack="true"
                                     Style="width: 150px;" ontextchanged="txtTodate_TextChanged"></asp:TextBox>
                               <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate">
                                </ajaxToolkit:CalendarExtender>  --%>                              
                                <asp:RequiredFieldValidator ID="Requiretodate" ControlToValidate="txtTodate" 
                                    runat="server" ErrorMessage=" Select To date" ForeColor="Red" ValidationGroup="display"  >
                                </asp:RequiredFieldValidator>
                               <%-- <asp:CompareValidator runat ="server" ID ="CompareToDate" ControlToValidate ="txtTodate" ControlToCompare ="txtfrmdate" 
                                Operator="GreaterThanEqual" Type="Date" ErrorMessage ="Please select a date greater than or equal to From Date"
                                ForeColor="Red" ValidationGroup="display"></asp:CompareValidator>--%>
                            </td>
                           
                        </tr>
                 
                        <tr>
                            <td>
                                <label>
                                    <strong>Select User : </strong></label>                       
                                    <asp:DropDownList ID ="drpUser" runat ="server" Width ="250px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="drpUser" 
                                    runat="server" ErrorMessage=" Select User" ForeColor="Red" ValidationGroup="display" >
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    </li>
                   
                <li style="width: 49%;" class="last">
                <table cellpadding="0" cellspacing="0" border="0" runat="server">
                   <tr>
                     <td><br />
                                <br />
                                <label>
                                    <strong>Select Pay Period : </strong></label>                       
                                    <asp:DropDownList ID ="drpPayPeriod" runat ="server" Width ="250px" AutoPostBack="true"
                             onselectedindexchanged="drpPayPeriod_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            
                    </tr>
                </table>
                </li>
                </ul>
               <br />
            <div class="btn_sec">
                <asp:Button runat="server" ID="btnshow" Text="Show" CssClass="cancel" OnClick="btnshow_Click" ValidationGroup="display"   TabIndex="4"/>
            </div>
           
            <div class="grid_h">
               <table width ="100%">
                <tr>
                    <td>
                    <asp:Label Id="lblLeads" runat ="server" Visible ="false"><strong>Leads Issued  :   </strong></asp:Label>
                     <asp:Label ID="lblLeadsIssued" runat ="server" Visible ="false"></asp:Label>
                    </td>
                    
                    <td>
                    <asp:Label Id="lblOClosing" runat ="server" Visible ="false"><strong>OverAll Closing %  :  </strong></asp:Label>
                    <asp:Label ID="lblOverallClosing" runat ="server" Visible ="false"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                    <asp:Label Id="lblLdSeen" runat ="server" Visible ="false"><strong>Leads Seen  :   </strong></asp:Label>
                    <asp:Label ID="lblLeadsSeen" runat ="server" Visible ="false"></asp:Label>
                    </td>
                    
                    <td>
                    <asp:Label Id="lblre" runat ="server" Visible ="false"><strong>Rehash %  :  </strong></asp:Label>
                     <asp:Label ID="lblRehash" runat ="server" Visible ="false"></asp:Label>
                    </td>
                   
                </tr>

                <tr>
                    <td>
                    <asp:Label Id="lblGrSales" runat ="server" Visible ="false"><strong>Gross Sales  :   </strong></asp:Label>
                    <asp:Label ID="lblGrossSales" runat ="server" Visible ="false"></asp:Label>
                    </td>
                    
                     <td>
                    <asp:Label Id="lblTMProfit" runat ="server" Visible ="false"><strong>Total Mean Profit % : </strong></asp:Label>
                    <asp:Label ID="lblTotalMeanProfit" runat ="server" Visible ="false"></asp:Label>
                    </td>
                   
                </tr>
               </table>
               <br />
               <br />

            <div class="grid">
                <asp:GridView ID="grddata" runat="server" AutoGenerateColumns ="false" CssClass="tableClass" AllowSorting="true" OnSorting="grddata_Sorting" 
                    onrowdatabound="grddata_RowDataBound" HeaderStyle-Wrap="true" Width="1000px">
                <Columns >
                <asp:BoundField HeaderText ="Rep" DataField ="UserName" SortExpression="UserName" HeaderStyle-Width="100px" HeaderStyle-Wrap="true"/>
                 <asp:TemplateField HeaderText ="Customer/Prospect Id" SortExpression="CustomerId"  HeaderStyle-Width="100px" HeaderStyle-Wrap="true" >
                    <ItemTemplate >
                        <asp:LinkButton id="lnkCustomerId" Text='<%#Eval("CustomerId")%>' OnClick ="lnkCustomerId_Click" runat ="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText="Customer/Prospect Id" DataField ="CustomerId" />--%>
                <asp:TemplateField HeaderText ="Job Packet #" SortExpression="JobId"  HeaderStyle-Width="100px" HeaderStyle-Wrap="true" >
                    <ItemTemplate >
                        <asp:LinkButton id="lnkJobPacket" Text='<%#Eval("JobId")%>' runat ="server" OnClick ="lnkJobPacket_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText ="Amount($)" DataField ="Amount" SortExpression="Amount" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                <asp:BoundField HeaderText ="Status/Date" DataField ="StatusWithDate" SortExpression="StatusWithDate" HeaderStyle-Width="300px" HeaderStyle-Wrap="true" />
                <asp:BoundField HeaderText ="Source" DataField ="Source" SortExpression="Source" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                <asp:BoundField HeaderText ="Profit%" DataField ="ProfitPercent" SortExpression="ProfitPercent" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" />
                <asp:TemplateField HeaderText ="Commission($)" SortExpression="Commission" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" >
                    <ItemTemplate >
                        <asp:Label id="lblCommission" Text='<%#Eval("Commission")%>' runat ="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText ="Commission" DataField ="Commission" />--%>
                <%--<asp:BoundField HeaderText ="Product Type" DataField ="ProductName" />--%>
                 <asp:TemplateField HeaderText ="Product Type" SortExpression="ProductName" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" >
                    <ItemTemplate >
                        <asp:Label id="lblProductType" Text='<%#Eval("ProductName")%>' runat ="server" ></asp:Label>
                        <asp:HiddenField ID ="hdnProductTypeId" Value ='<%#Eval("productTypeId")%>' runat ="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                </asp:GridView>
            </div>
           
            <div class="btn_sec">
                <asp:Button runat="server" ID="btnExporttoExcel" Text="Export to Excel" CssClass="cancel" TabIndex="5"
                    Visible="false" OnClick="btnExporttoExcel_Click" />
            </div>
            
        </div>
    </div>
    </div>
</asp:Content>
