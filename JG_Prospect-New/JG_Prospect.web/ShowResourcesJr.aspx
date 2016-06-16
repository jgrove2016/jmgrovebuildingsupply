<%@ Page Title="" Language="C#" MasterPageFile="~/JG.Master" AutoEventWireup="true" CodeBehind="ShowResourcesJr.aspx.cs" Inherits="JG_Prospect.ShowResourcesJr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .leafnode
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="right_panel">
    <!-- appointment tabs section start -->
     <ul class="appointment_tab">
        <li><a id="A1" href="home.aspx" runat="server">Personal Prospect Calendar</a> </li>
        <li><a id="A2" href="GoogleCalendarView.aspx" runat="server">Master Prospect Calendar</a></li>
        <li><a id="A3" href="StaticReport.aspx" runat="server">Call Sheet</a></li>
    </ul>
<!-- appointment tabs section end -->
        <h1>
            <b>Show Resources</b></h1>
        <div class="form_panel tree-view">
        <div class="tree-view" style="width:98%; background-color:#E5E5E5; margin:3px auto; border:1px solid #c9c9c9; ">       
            <table>
                <tr>
                    <td>
                        <asp:TreeView ID="TreeView1" ClientIDMode="Static" runat="server" 
                        RootNodeStyle-ImageUrl="~/img/Resource.png" LeafNodeStyle-ImageUrl="~/img/File.png" NodeStyle-ImageUrl="~/img/folder.png" >
                            <LeafNodeStyle  CssClass="leafnode" />
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
            </div>
             <div class="btn_sec">
                <asp:Button ID="btnDelete" runat="server" Text="Delete Resource(s)" onclick="btnDelete_Click" /> 
            </div>
            <button id="btnDeleteResource" style="display: none" runat="server"></button>
            <ajaxToolkit:ModalPopupExtender ID="mpeDeleteResource" PopupControlID="pnlDeleteResource" runat="server" TargetControlID="btnDeleteResource" CancelControlID="btnNo">
            </ajaxToolkit:ModalPopupExtender>

             <asp:Panel ID="pnlDeleteResource" runat="server" BackColor="White" Height="220px" Width="300px"
            Style="z-index: 111; background-color: White; position: absolute; left: 35%;
            top: 12%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #b5494c">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        Delete Resource Confirmation <asp:Button ID="btnXAdmin" runat ="server" OnClick ="btnX_Click" Text ="X" style=" float: right; text-decoration: none" />
                    </td>
                </tr>
                        
                        <tr>
                            <td>
                                Are you sure you want to delete the resource(s)?
                            </td> 
                         </tr> 
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnYes" runat="server"  Text="Yes" OnClick="DeleteResource" Style="width: 100px;"  />
                                <asp:Button ID="btnNo" runat="server" Text="No" Style="width: 100px;" />
                            </td>
                        </tr>   
                    </table>                  
                </asp:Panel>
        </div>
    </div>
    <script type="text/javascript">
        $(window).load(function () {
            $('a.leafnode').each(function () {
                $(this).removeAttr('onclick').attr('target', '_blank');
                //});
                //$('a.leafnode').click(function () {
                var link = $(this).attr('href');
                var minuslink = link.substr(link.lastIndexOf(",") + 3, link.length);
                minuslink = minuslink.replace(/\\\\/g, '/');
                //minuslink = minuslink.replace(/['][)]/g, '');
                //alert(minuslink);             
                minuslink = minuslink.slice(0, -2);
                $(this).attr('href', '../' + minuslink);

                //alert(minuslink);
            });

        });

    </script>
</asp:Content>
