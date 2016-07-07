<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftPanel.ascx.cs" Inherits="JG_Prospect.Sr_App.LeftPanel" %>
<script runat="server">

  
</script>

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
    <%--<div class="alpha_paging">
   <asp:LinkButton ID="A" runat="server" Text="A" onclick="A_Click"></asp:LinkButton><asp:LinkButton ID="B" runat="server" Text="B" onclick="B_Click"></asp:LinkButton><asp:LinkButton ID="C" runat="server" Text="C" onclick="C_Click"></asp:LinkButton><asp:LinkButton ID="D" runat="server" Text="D" onclick="D_Click"></asp:LinkButton><asp:LinkButton ID="E" runat="server" Text="E" onclick="E_Click"></asp:LinkButton><asp:LinkButton ID="F" runat="server" Text="F" onclick="F_Click"></asp:LinkButton><asp:LinkButton ID="G" runat="server" Text="G" onclick="G_Click"></asp:LinkButton><asp:LinkButton ID="H" runat="server" Text="H" onclick="H_Click"></asp:LinkButton><asp:LinkButton ID="I" runat="server" Text="I" onclick="I_Click"></asp:LinkButton><asp:LinkButton ID="J" runat="server" Text="J" onclick="J_Click"></asp:LinkButton><asp:LinkButton ID="K" runat="server" Text="K" onclick="K_Click"></asp:LinkButton><asp:LinkButton ID="L" runat="server" Text="L" onclick="L_Click"></asp:LinkButton><asp:LinkButton ID="M" runat="server" Text="M" onclick="M_Click"></asp:LinkButton><asp:LinkButton ID="N" runat="server" Text="N" onclick="N_Click"></asp:LinkButton><asp:LinkButton ID="O" runat="server" Text="O" onclick="O_Click"></asp:LinkButton><asp:LinkButton ID="P" runat="server" Text="P" onclick="P_Click"></asp:LinkButton><asp:LinkButton ID="Q" runat="server" Text="Q" onclick="Q_Click"></asp:LinkButton><asp:LinkButton ID="R" runat="server" Text="R" onclick="R_Click"></asp:LinkButton><asp:LinkButton ID="S" runat="server" Text="S" onclick="S_Click"></asp:LinkButton><asp:LinkButton ID="T" runat="server" Text="T" onclick="T_Click"></asp:LinkButton><asp:LinkButton ID="U" runat="server" Text="U" onclick="U_Click"></asp:LinkButton><asp:LinkButton ID="V" runat="server" Text="V" onclick="V_Click"></asp:LinkButton><asp:LinkButton ID="W" runat="server" Text="W" onclick="W_Click"></asp:LinkButton><asp:LinkButton ID="X" runat="server" Text="X" onclick="X_Click"></asp:LinkButton><asp:LinkButton ID="Y" runat="server" Text="Y" onclick="Y_Click"></asp:LinkButton><asp:LinkButton ID="Z" runat="server" Text="Z" onclick="Z_Click"></asp:LinkButton>
   </div>--%>
    <div class="filter_section">
        <asp:TextBox ID="txtcustomersearch" runat="server" 
            ontextchanged="txtcustomersearch_TextChanged"></asp:TextBox>
        <br />
        <asp:DropDownList ID="ddlsearchtype" runat="server">
            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
            <asp:ListItem Text="Customer Name" Value="CustomerName"></asp:ListItem>
            <asp:ListItem Text="Customer Address" Value="CustomerAddress"></asp:ListItem>
            <asp:ListItem Text="Customer Phone" Value="CustomerPhone"></asp:ListItem>
            <asp:ListItem Text="Set" Value="Set"></asp:ListItem>
            <asp:ListItem Text="Prospect" Value="Prospect"></asp:ListItem>          
            <asp:ListItem Text="Sold-in Progress" Value="Sold-in Progress"></asp:ListItem>
            <asp:ListItem Text="Open Estimates" Value="Open Estimates"></asp:ListItem>
            <asp:ListItem Text="Closed(not sold)" Value="Closed(not sold)"></asp:ListItem>
            <asp:ListItem Text="Closed(sold)" Value="Closed(sold)"></asp:ListItem>
            <asp:ListItem Text="Rehash" Value="Rehash"></asp:ListItem>
            <asp:ListItem Text="cancelation-no rehash" Value="cancelation-no rehash"></asp:ListItem>
            <asp:ListItem Text="CustomerId" Value="CustomerId"></asp:ListItem>
            <asp:ListItem Text="ZipCode" Value="ZipCode"></asp:ListItem>
            <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/search_btn.png"
          CssClass="search_btn"  OnClick="ImageButton1_Click" />
    </div>
    <asp:Repeater ID="rptcutomerlist" runat="server" 
        OnItemCommand="rptcutomerlist_ItemCommand" 
        onitemdatabound="rptcutomerlist_ItemDataBound">
        <ItemTemplate>
            <h3 class="menuheader">
                <asp:LinkButton ID="lnkcustomer" runat="server" Text='<%# Eval("CustomerName")%>'
                    CommandArgument='<%# Eval("id")%>' OnClick="customername_click"></asp:LinkButton>
                    <asp:HiddenField ID="hdnCustomerColor" runat="server" Value='<%# Eval("CustomerColor")%>' />
                <%--    <asp:Label ID="lblcustumer" runat="server" Text='<%# Eval("CustomerName")%>'></asp:Label>--%>
            </h3>
            <asp:HiddenField ID="hdncustomer" runat="server" Value='<%# Eval("Email")%>' />
            <%--<ul class="categoryitems">
<li>


<a href="viewshutters1.aspx?aa=<%# Eval("Email")%>">Shutters</a>


<a href="javascript:void(0);" onclick="javascript:window.open('viewshutters1.aspx?aa=<%# Eval("Email")%>', 'Directory', 'width=430, height=325,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0');">Shutters</a>

</li>
<li><a href="#">Roofing</a></li>
</ul>--%>
        </ItemTemplate>
    </asp:Repeater>
</div>
