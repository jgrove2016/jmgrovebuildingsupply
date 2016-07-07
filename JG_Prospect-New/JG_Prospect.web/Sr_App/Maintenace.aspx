<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true" CodeBehind="Maintenace.aspx.cs" Inherits="JG_Prospect.Sr_App.Maintenace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../datetime/css/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <link href="../datetime/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .cls_textbox {
            margin-bottom: 10px;
        }

        tr td {
            text-align: left;
            padding-left: 15px;
        }

        input[type=text] {
            margin-top: 10px;
            width: 100%;
            height: 70%;
        }
    </style>


    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            GetComapnyAddress();
        });

        function GetComapnyAddress() {
            $.ajax({
                type: "POST",
                url: "Maintenace.aspx/GetCompanyAddress",
                // data: "{'strZip':'" + $(".list_limit li[style*='background-color: lemonchiffon']").text() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    debugger;
                    var response = JSON.parse(data.d);
                    $('#ContentPlaceHolder1_hdnCompanyAddressId').val(response.Table[0].intCompanyId);
                    $('#ContentPlaceHolder1_txtCompanyAddress').val(response.Table[0].strCompanyAddress);
                    $('#ContentPlaceHolder1_txtCity').val(response.Table[0].strCity);
                    $('#ContentPlaceHolder1_txtZip').val(response.Table[0].strZipCode);
                    $('#ContentPlaceHolder1_txtState').val(response.Table[0].strState);
                }
            });
        }
        function btnUpdate() {
            debugger;
            var Id = $('#ContentPlaceHolder1_hdnCompanyAddressId').val();
            var CompanyAddress = $('#ContentPlaceHolder1_txtCompanyAddress').val();
            var CompanyCity = $('#ContentPlaceHolder1_txtCity').val();
            var CompanyState = $('#ContentPlaceHolder1_txtState').val();
            var CompanyZipCode = $('#ContentPlaceHolder1_txtZip').val();
            $.ajax({
                type: "POST",
                url: "Maintenace.aspx/UpdateCompanyAddress",
                data: "{'Id':'" + Id + "','Address':'" + CompanyAddress + "','City':'" + CompanyCity + "','State':'" + CompanyState + "','ZipCode':'" + CompanyZipCode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    debugger;
                    if (data.d = 'Success') {
                        GetComapnyAddress();
                    }
                    else {
                        alert("Updation Failed");
                    }
                }
            });
        }
    </script>
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
        <h1>Maintainance
        </h1>

        <table>
            <tr>
                <td>
                    <label>Company Address</label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="cls_textbox"></asp:TextBox>
                </td>
                <td>
                    <label>Zip</label>
                </td>
                <td>
                    <asp:TextBox ID="txtZip" runat="server" CssClass="cls_textbox"></asp:TextBox>
                </td>
                <td>
                    <label>City</label>
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="cls_textbox"></asp:TextBox>
                </td>

                <td>
                    <label>State<span></span></label>
                </td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" CssClass="cls_textbox"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnCompanyAddressId" runat="server" />
        <input type="button" id="btnupdate" runat="server" onclick="btnUpdate()" value="Update" />
        <div class="show_hide">
            <%--<h3>
                <a href="CustomerEmailTemplate.aspx">Default Customer Email</a>
            </h3>
            <h3>
                <a href="EditEmailTemplate.aspx?ProductId=1">Edit Email Template</a>
            </h3>--%>
            <h3>Auto Email Templates</h3>
            <table width="100%">
                <tr>
                    <td>
                        <h3><a href="CustomerEmailTemplate.aspx">Default Customer Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=100">Sales Auto Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=101">Installer Auto Email</a></h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=102">Customer Service Auto Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=103">Prospecting Auto Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=104">Interview Date Auto Email</a></h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=105">Offer Made Auto Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=106">Active Auto Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=107">Deactive Auto Email</a></h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=14">Vendor Category Auto Email</a></h3>
                    </td>
                    <td>
                        <h3><a href="EditEmailTemplate.aspx?htempID=15">Vendor Auto Email</a></h3>
                    </td>
                    <td></td>
                </tr>

            </table>
        
            <h3>Templates</h3>
            <table width="100%">
                <tr>
                    <td>
                        <a href="ContractTemplate.aspx?ProductId=1">Contract Template</a>
                        <a href="ShutterTemplate.aspx">Shutter W.O.</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clr">
        </div>

    </div>

</asp:Content>
