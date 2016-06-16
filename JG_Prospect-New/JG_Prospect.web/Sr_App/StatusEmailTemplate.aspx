<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="StatusEmailTemplate.aspx.cs" Inherits="JG_Prospect.Sr_App.WebForm3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script src="../js/jquery-latest.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript">
        function uploadComplete() {

            if (Checkfiles() == true) {
                var btnImageUploadClick = document.getElementById("ctl00_ContentPlaceHolder1_btnImageUploadClick");
                btnImageUploadClick.click();
            }
        }
        function Checkfiles() {

            var fup = document.getElementById('ctl00_ContentPlaceHolder1_ajaxFileUpload_ctl02');
            var fileName = fup.value;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toString().toLowerCase();
            if (ext == "gif" || ext == "jpeg" || ext == "jpg" || ext == "tiff" || ext == "tif" || ext == "bmp" || ext == "png") {
                if ($('#ContentPlaceHolder1_hidCount').val().length != 0) {
                    if (parseInt($('#ContentPlaceHolder1_hidCount').val()) == 5) {
                        alert('You can not upload more than five image.');
                        $('#ctl00_ContentPlaceHolder1_ajaxFileUpload_ctl02').val('');
                        fup.focus();
                        return false;
                    }
                }
                return true;
            }
            else {
                alert('Upload Gif,JPG,JPEG,TIFF,TIF,PNG and BMP images only');
                $('#ctl00_ContentPlaceHolder1_ajaxFileUpload_ctl02').val('');
                fup.focus();
                return false;
            }
        }
        function readURL(input) {
            debugger;
            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray($(input).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Invalid formats are not allowed.");
                $(input).val("");
                return false;
            }
        }      

        function IsExists(pagePath, dataString, textboxid, errorlableid) {
            $.ajax({
                type: "POST",
                url: pagePath,
                data: dataString,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
          function (XMLHttpRequest, textStatus, errorThrown) {
              $(errorlableid).show();
              $(errorlableid).html("Error");
          },
                success:
         
                }
            });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right_panel">
        <ul class="appointment_tab">
            <li><a href="home.aspx">Personal Appointment</a></li>
            <li><a href="GoogleCalendarView.aspx">Master Appointment</a></li>
            <li><a href="#">Construction Calendar</a></li>
            <li><a href="CallSheet.aspx">Call Sheet</a></li>
        </ul>
        <h1>
            Edit Status Template</h1>
        <br />
        <div>
            <center><label>
             <asp:Label ID="lblTest" runat="server" ForeColor="Black">
                                    User Status</asp:Label><span>*</span></label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" Width="249px" TabIndex="102" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Interview Date" Value="InterviewDate"></asp:ListItem>
                                            <asp:ListItem Text="Offer Made" Value="OfferMade"></asp:ListItem>
                                            <asp:ListItem Text="Deactive" Value="Deactive"></asp:ListItem>
                                        </asp:DropDownList>
                </center>
        </div>
        <div>
            <h2>
                Header Template</h2>
            <cc1:Editor ID="HeaderEditor" Width="1000px" Height="400px" runat="server" />
            <h2>
                Body Template</h2>
            <cc1:Editor ID="BodyEditor" Width="1000px" Height="400px" runat="server" />
            <%--   <cc1:Editor ID="BodyEditor2" Width="1000px" Height="400px" runat="server" />--%>
            <h2>
                Footer Template</h2>
            <cc1:Editor ID="FooterEditor" Width="1000px" Height="600px" runat="server" />
        </div>
        <div>
           <%-- <label>
                Customer Attachment:</label>
            <asp:FileUpload ID="fileAttachment" runat="server" class="multi" TabIndex="6" />
            <label>
                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" Visible="false" OnClick="lnkDownload_Click"></asp:LinkButton>
            </label>
            <asp:Label ID="listofuploadedfiles" runat="server" Visible="false" />
            <asp:HiddenField ID="hidCount" runat="server" />--%>
            <%-- <asp:DataList ID="dtlist" runat="server" RepeatColumns="4" CellPadding="5">
                <ItemTemplate>
                    <asp:Label Width="100" ID="Image1" NavigateUrl='<%# Bind("DocumentName", "~/CustomerDocs/CustomerEmailDocument/{0}") %>'
                        runat="server" />
                    <br />
                    <asp:HyperLink ID="HyperLink1" Text='<%# Bind("DocumentName") %>' NavigateUrl='<%# Bind("DocumentName", "~/CustomerDocs/CustomerEmailDocument/{0}") %>'
                        runat="server" />
                </ItemTemplate>
                <ItemStyle BorderColor="Brown" BorderStyle="dotted" BorderWidth="3px" HorizontalAlign="Center"
                    VerticalAlign="Bottom" />
            </asp:DataList>--%>
           <%-- <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded" CellSpacing="22">
                <Columns>
                    <asp:BoundField DataField="Text" HeaderText="File Name" />
                    <asp:TemplateField HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>'
                                runat="server" OnClick="DownloadFile"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("Value") %>'
                                runat="server" OnClick="DeleteFile" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
        </div>
        <br />
        <div class="btn_sec">
            <asp:Button ID="btnsave" runat="server" Text="Update" ValidationGroup="save" OnClick="btnsave_Click" />
        </div>
    </div>
</asp:Content>
