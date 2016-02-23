<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Vendors.aspx.cs" Inherits="JG_Prospect.Sr_App.Vendors" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js"></script>
<script type="text/javascript">
    function initialize() {
        var mapProp = {
            center: new google.maps.LatLng(51.508742, -0.120850),
            zoom: 5,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    }
    google.maps.event.addDomListener(window, 'load', initialize);

    function loadScript() {
        var script = document.createElement("script");
        script.src = "http://maps.googleapis.com/maps/api/js?callback=initialize";
        document.body.appendChild(script);
    }
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <h1>
            <b>Vendors</b></h1>
        <div class="form_panel">
            <ul style="padding:0px;">
                <li style="width:49%;padding-left:0px;margin-left:0px;">
                    <table border="0" cellspacing="0" cellpadding="0" style="padding:0px;margin0px;">
                    <tr>
                            <td >
                                <label>
                                   <span>*</span> Vendor Category:
                                </label>
                                <asp:DropDownList ID="ddlVendorCategory" runat="server" TabIndex="2" 
                                    onselectedindexchanged="ddlVendorCategory_SelectedIndexChanged" AutoPostBack ="true">
                                </asp:DropDownList>
                                <label>
                                </label>
                                <asp:LinkButton ID="lnkAddVendorCategory" Text="Add New Category" runat="server" ></asp:LinkButton>
                                <asp:ModalPopupExtender ID="mpevendorcatelog" runat="server" TargetControlID="lnkAddVendorCategory"
                                    PopupControlID="pnlpopup" CancelControlID="btnCancel">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="550px" 
                                    Style="display: none; border: Solid 3px #A33E3F; border-radius: 10px 10px 0 0;">
                                    <table style="border: Solid 3px #A33E3F; width: 100%; height: 100%;" cellpadding="0"
                                        cellspacing="0">
                                        <tr style="background-color: #A33E3F">
                                            <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger;
                                                width: 100%;" align="center">
                                                Vendor Catalog Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 31%">
                                                Vendor Catagory Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtname" runat="server" onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Requiredvendorcategoryname" runat="server" ControlToValidate="txtname"
                                                    ValidationGroup="addvendorcategory" ErrorMessage="Enter Category Name." ForeColor="Red"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnUpdate" CommandName="Update" Style="width: 100px;" runat="server"
                                                    Text="Save" OnClick="btnsave_Click" ValidationGroup="addvendorcategory" />
                                                <asp:Button ID="btnCancel" runat="server" Style="width: 100px;" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                &nbsp;
                                <asp:LinkButton ID="Lnkdeletevendercategory" Text="Delete Vendor Category" runat="server"
                                    OnClick="Lnkdeletevendercategory_Click"></asp:LinkButton>
                                <asp:ModalPopupExtender ID="Mpedeletecategory" runat="server" TargetControlID="Lnkdeletevendercategory"
                                    PopupControlID="pnlpopup2" CancelControlID="btnCancel2">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" Height="269px" Width="550px"
                                    Style="display: none">
                                    <table width="100%" style="border: Solid 3px #A33E3F; width: 100%; height: 100%"
                                        cellpadding="0" cellspacing="0">
                                        <tr style="background-color: #A33E3F">
                                            <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                                                align="center">
                                                Vendor Catalog Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 45%">
                                                Select Vendor Catagory Name
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendercategoryname" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btndeletevender" CommandName="Delete" runat="server" Style="width: 100px;"
                                                    Text="Delete" OnClientClick="return confirm('All materials list associated with this vendor will be deleted.Are you sure you want to perform this operation')" OnClick="btndeletevender_Click" />
                                                <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Style="width: 100px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                   <span>*</span> Vendor Name:
                                </label>
                                <asp:TextBox ID="txtVendorNm" runat="server" MaxLength="30" AutoComplete="off" onkeypress="return isAlphaKey(event);" TabIndex="1"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredvendorname" runat="server" ControlToValidate="txtVendorNm"
                                    ValidationGroup="addvendor" ErrorMessage="Please Enter Vendor Name." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td >
                                <label>
                                  <span>*</span>  Contact Person:
                                </label>
                                <asp:TextBox ID="txtcontactperson" runat="server" MaxLength="30" AutoComplete="off" TabIndex="3" onkeyup="javascript:Alpha(this)"
                                    onkeypress="return isAlphaKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredcontactperson" runat="server" ControlToValidate="txtcontactperson"
                                    ValidationGroup="addvendor" ErrorMessage="Please Enter Contact Person." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                   <span>*</span> Contact Number:
                                </label>
                                <asp:TextBox ID="txtcontactnumber" runat="server" MaxLength="15" AutoComplete="off" TabIndex="4" onkeyup="javascript:Numeric(this)"
                                    onkeypress="return isNumericKey(event);"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredcontactnumber" runat="server" ControlToValidate="txtcontactnumber"
                                    ValidationGroup="addvendor" ErrorMessage="Please Enter Contact Number." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                   <span>*</span> Fax:
                                </label>
                                <asp:TextBox ID="txtfax" runat="server" MaxLength="20" AutoComplete="off" onkeypress="return isfax(event);" TabIndex="5"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredfax" runat="server" ControlToValidate="txtfax"
                                    ValidationGroup="addvendor" ErrorMessage="Please Enter Fax Number." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                   <span>*</span> Mail:
                                </label>
                                <asp:TextBox ID="txtmail" runat="server" MaxLength="50" AutoComplete="off" TabIndex="6"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RegularExpressionValidator ID="revfax" runat="server" ForeColor="Red" ControlToValidate="txtmail" ValidationGroup="addvendor"
                                    ErrorMessage="Please enter correct email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="Requiredmail" runat="server" ControlToValidate="txtmail"
                                    ValidationGroup="addvendor" ErrorMessage="Please Enter Email." ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>Address:</label>
                                <asp:TextBox ID="txtaddress" runat="server" TabIndex="7" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>Notes:</label>
                                <asp:TextBox ID="txtNotes" runat="server" TabIndex="8" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </li>
                <li style="width:48%">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Vendor Id:</label><asp:TextBox ID="txtVendorId" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                    Menufacturer:</label>
                                <asp:DropDownList ID="ddlMenufacturer" runat="server" >
                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Wholesale" Value="Wholesale" ></asp:ListItem>
                                    <asp:ListItem Text="Retail" Value="Retail" ></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                    Vendors List:</label><asp:ListBox ID="lstVendors" Rows="8" runat="server" OnSelectedIndexChanged="lstVendors_SelectedIndexChanged" TabIndex="7"
                                        AutoPostBack="True" Height="44px" Width="229px"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <div id="googleMap" style="width:421px; height:254px;"></div>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                    Billing Address:</label>
                                <asp:TextBox ID="txtBillingAddress" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                    Tax Id:</label>
                                <asp:TextBox ID="txtTaxId" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                     Expense Category:</label>
                                <asp:TextBox ID="txtExpenseCat" runat="server" MaxLength="50" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>
                                    Auto & Truck Insurance:</label>
                                <asp:TextBox ID="txtAutoInsurance" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </li>
                <li style="width:100%">

                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="addvendor"  TabIndex="8"/>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" TabIndex="9" />
            </div>
        </div>
    </div>
</asp:Content>
