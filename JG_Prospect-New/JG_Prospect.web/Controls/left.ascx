<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="left.ascx.cs" Inherits="JG_Prospect.Controls.left" %>
<script type="text/javascript" src="../js/ddaccordion.js"></script>
<script type="text/javascript">


    ddaccordion.init({
        headerclass: "expandable", //Shared CSS class name of headers group that are expandable
        contentclass: "categoryitems", //Shared CSS class name of contents group
        revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click", "clickgo", or "mouseover"
        mouseoverdelay: 200, //if revealtype="mouseover", set delay in milliseconds before header expands onMouseover
        collapseprev: true, //Collapse previous content (so only one open at any time)? true/false 
        defaultexpanded: [0], //index of content(s) open by default [index1, index2, etc]. [] denotes no content
        onemustopen: false, //Specify whether at least one header should be open always (so never all headers closed)
        animatedefault: false, //Should contents open by default be animated into view?
        persiststate: true, //persist state of opened contents within browser session?
        toggleclass: ["", "openheader"], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
        togglehtml: ["prefix", "", ""], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
        animatespeed: "fast", //speed of animation: integer in milliseconds (ie: 200), or keywords "fast", "normal", or "slow"
        oninit: function (headers, expandedindices) { //custom code to run when headers have initalized
            //do nothing
        },
        onopenclose: function (header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
            //do nothing
        }
    })


</script>
<!--accordion jquery ends-->
<!--tabs jquery-->
<script type="text/javascript" src="../js/jquery.ui.core.js"></script>
<script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
<!--tabs jquery ends-->
<script type="text/javascript">
    $(function () {
        // Tabs
        if ($('#tabs').length) {
            $('#tabs').tabs();
        }
    });
</script>
<style type="text/css">
    .ui-widget-header
    {
        border: 0;
        background: none /*{bgHeaderRepeat}*/;
        color: #222 /*{fcHeader}*/;
    }
</style>
<%--<link href="../css/screen.css" rel="stylesheet" media="screen" type="text/css" />--%>
<div class="scroll_left_panel">
    <div class="filter_section">
        <asp:TextBox ID="txtProspectsearch" runat="server" OnTextChanged="txtProspectsearch_TextChanged"></asp:TextBox>
        <br />
        <asp:DropDownList ID="ddlsearchtype" runat="server">
            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
            <asp:ListItem Text="Customer Name" Value="CustomerName"></asp:ListItem>
            <asp:ListItem Text="Customer Address" Value="CustomerAddress"></asp:ListItem>
            <asp:ListItem Text="Customer Phone" Value="CustomerPhone"></asp:ListItem>
            <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>  
            <asp:ListItem Text="PTW est" Value="PTW est"></asp:ListItem>
            <asp:ListItem Text="est>$1000" Value="est>$1000"></asp:ListItem>
            <asp:ListItem Text="est<$1000" Value="est<$1000"></asp:ListItem>
            <asp:ListItem Text="EST-one legger" Value="EST-one legger"></asp:ListItem>
            <asp:ListItem Text="sold>$1000" Value="sold>$1000"></asp:ListItem>
            <asp:ListItem Text="sold<$1000" Value="sold<$1000"></asp:ListItem>
             <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
            <asp:ListItem Text="cancelation-no rehash" Value="cancelation-no rehash"></asp:ListItem>
            <asp:ListItem Text="CustomerId" Value="CustomerId"></asp:ListItem>
            <asp:ListItem Text="ZipCode" Value="ZipCode"></asp:ListItem>
            <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/search_btn.png"
            CssClass="search_btn" OnClick="ImageButton1_Click" />
    </div>
    <asp:Repeater ID="rptcutomerlist" runat="server" OnItemCommand="rptcutomerlist_ItemCommand" onitemdatabound="rptcutomerlist_ItemDataBound">
        <ItemTemplate>
            <h3 class="menuheader">
                <asp:LinkButton ID="lnkprospect" runat="server" Text='<%# Eval("CustomerName")%>'
                    CommandArgument='<%# Eval("id")%>' OnClick="prospect_click"></asp:LinkButton>
                     <asp:HiddenField ID="hdnCustomerColor" runat="server" Value='<%# Eval("CustomerColor")%>' />
            </h3>
            <asp:HiddenField ID="hdncustomer" runat="server" Value='<%# Eval("Email")%>' />
        </ItemTemplate>
    </asp:Repeater>
</div>
