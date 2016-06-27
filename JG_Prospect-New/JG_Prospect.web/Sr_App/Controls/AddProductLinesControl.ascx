<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProductLinesControl.ascx.cs" Inherits="JG_Prospect.Sr_App.Controls.AddProductLinesControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

 <script type = "text/javascript">
     function uploadComplete_multiple() {
         // alert("Complete");
         var btnImageUploadClick = document.getElementById("<%= btnImageUploadClick.ClientID%>");
         btnImageUploadClick.click();
     }
</script>
<div class="product-categories">
    <asp:Panel runat="server" ID="panel1" CssClass="head">
        <table>
            <tr>
                <td>
                    <label>Product Lines:</label>
                <td>
                    <asp:DropDownList ID="ddlProductLines" runat="server" TabIndex="1"></asp:DropDownList>
                    <span runat="server" class="expand" id="spanExpand">(Expand...)</span>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="panel2" Width="98%" CssClass="body">
        <table>
            <tr>
                <td>
                    <label>Work Area:</label></td>
                <td>
                    <asp:TextBox ID="txtWorkArea" runat="server" MaxLength="35" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <label>Proposal Costs:</label><strong>$</strong>
                </td>
                <td>
                    <asp:TextBox ID="txtProposalCost" runat="server" AutoCompleteType="Disabled" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Special Instructions / Exemptions:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtSpecialIns" runat="server" Rows="5" TextMode="MultiLine" TabIndex="3"></asp:TextBox>
                </td>

                <td>
                    <label>Proposal Terms:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtProposalTerm" runat="server" Rows="5" TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Location Image:</label>
                </td>
                <td colspan="3">
                     <ajaxToolkit:AsyncFileUpload ID="ajaxFileUpload" runat="server" ClientIDMode="AutoID" ThrobberID="imgLoader" CompleteBackColor="White" 
                        OnClientUploadError="uploadError_multiple"
                        OnClientUploadStarted="uploadStarted_multiple"
                        OnClientUploadComplete="uploadComplete_multiple"
                        OnUploadedComplete="ajaxFileUpload_UploadedComplete" />
                    <asp:Image ID="imgLoader" runat="server" Width="30px"
                        ImageUrl="~/img/loader.gif" />
                    <asp:Button ID="btnImageUploadClick" ClientIDMode="AutoID" runat="server" CausesValidation="false" Text="hidden" OnClick="btnImageUploadClick_Click" style="display:none;" />

                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:UpdatePanel ID="pnlUpdate" runat="server">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="gvCategory" AutoGenerateColumns="false"
                                DataKeyNames="RowSerialNo" AllowPaging="true" PageSize="2"
                               OnRowCommand="gvCategory_RowCommand" 
                                     OnPageIndexChanging="gvCategory_PageIndexChanging" 
                                     OnRowDataBound="gvCategory_RowDataBound">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoDataFound" runat="server" Text="Image Not Found."></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="90%" />
                                        <HeaderTemplate>
                                            <asp:Label ID="Image" runat="server" Text="Image" Font-Bold="true"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imglocation" runat="server" ImageUrl='<%#Eval("LocationPicture")%>'
                                                Height="100px" Width="100px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="10%" />
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCategoryDelete" runat="server" ToolTip="Delete" Text="X" CommandArgument='<%#Eval("RowSerialNo")%>'
                                                CommandName="DeleteRec" CausesValidation="false" OnClientClick='javascript:return confirm("Are you sure want to delete this entry?");'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hidCount" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td></td>
            </tr>
        </table>
    </asp:Panel>

    <ajaxToolkit:CollapsiblePanelExtender runat="server" ID="cpe" TargetControlID="panel2" CollapseControlID="spanExpand" ExpandControlID="spanExpand" Collapsed="true" CollapsedSize="0" ExpandedText="▲" CollapsedText="▼" TextLabelID="spanExpand">
    </ajaxToolkit:CollapsiblePanelExtender>
</div>