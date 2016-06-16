<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddress.ascx.cs" Inherits="JG_Prospect.UserControl.UCAddress" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table>
    <tr>
        <td>
            <div>
                <%--<asp:CheckBox ID="chkaddress" style="width: 5%" TabIndex="20" runat="server" Checked="true" />--%>
                <input type="checkbox" id="chkaddress" style="width: 5%" tabindex="20" checked="checked" runat="server" />
            </div>
            <%--<label id="lblAddress" runat="server">Address<span>*</span></label>--%>
            <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtaddress" Text="">Address<span>*</span></asp:Label>

            <asp:TextBox ID="txtaddress" runat="server"  TextMode="MultiLine" TabIndex="9"></asp:TextBox>
            <label>
            </label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="addcust"
                ErrorMessage="Please enter Address." ControlToValidate="txtaddress" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <%--<label id="lblAddressType" runat="server">Address Type<span>*</span></label>--%>
            <asp:Label ID="lblAddressType" runat="server" AssociatedControlID="DropDownList1" Text="">Address Type<span>*</span></asp:Label>
            <asp:DropDownList ID="DropDownList1" AutoPostBack="false" runat="server" TabIndex="4">
                 <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Primary Residence">Primary Residence</asp:ListItem>
                <asp:ListItem Value="Business">Business</asp:ListItem>
                <asp:ListItem Value="Vacation House">Vacation House</asp:ListItem>
                <asp:ListItem Value="Rental">Rental</asp:ListItem>
                <asp:ListItem Value="Condo">Condo</asp:ListItem>
                <asp:ListItem Value="Apartment">Apartment</asp:ListItem>
                <asp:ListItem Value="Mobile Home">Mobile Home</asp:ListItem>
                <asp:ListItem Value="Other">Other</asp:ListItem>
            </asp:DropDownList>
            <label>
            </label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="addcust"
                ErrorMessage="Please Select Address Type." ControlToValidate="DropDownList1" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <%--<label id="lblZip" runat="server">Zip<span>*</span></label>--%>
            <asp:Label ID="lblZip" runat="server" AssociatedControlID="txtzip" Text="">Zip<span>*</span></asp:Label>
            <asp:TextBox runat="server" ID="txtzip" Text="" onkeypress="return isNumericKey(event);" onblur="GetCityStateOnBlur(this)"
                TabIndex="11" autocomplete="off"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" OnClientItemSelected="onclientselect"
                UseContextKey="false" CompletionInterval="200" MinimumPrefixLength="2" ServiceMethod="GetZipcodes"
                TargetControlID="txtzip" EnableCaching="False" CompletionListCssClass="list_limit">
            </ajaxToolkit:AutoCompleteExtender>
            <label>
            </label>
            <asp:RequiredFieldValidator ID="Requiredzip" runat="server" ControlToValidate="txtzip"
                ErrorMessage="Please Enter Zip Code" ForeColor="Red" ValidationGroup="addcust"> </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <%--<label id="lblCity" runat="server">City<span>*</span></label>--%>
            <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtcity" Text="">City<span>*</span></asp:Label>
            <asp:TextBox ID="txtcity" Text="" runat="server" onkeypress="return isAlphaKey(event);"
                TabIndex="13"></asp:TextBox>
            <label>
            </label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcity"
                ErrorMessage="Please Enter City" ForeColor="Red" ValidationGroup="addcust"> </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <%--<label id="lblState" runat="server">State<span>*</span></label>--%>
            <asp:Label ID="lblState" runat="server" AssociatedControlID="txtstate" Text="">State<span>*</span></asp:Label>
            <asp:TextBox runat="server" ID="txtstate" onkeypress="return isAlphaKey(event);"
                Text="" TabIndex="15"></asp:TextBox>
            <label>
            </label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtstate"
                ErrorMessage="Please Enter State" ForeColor="Red" ValidationGroup="addcust"> </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>

