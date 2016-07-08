<%@ Page Title="" Language="C#" MasterPageFile="~/Sr_App/SR_app.Master" AutoEventWireup="true"
    CodeBehind="Custom.aspx.cs" Inherits="JG_Prospect.Sr_App.Product_Line.Custom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/dropzone/css/basic.css" rel="stylesheet" />
    <link href="../css/dropzone/css/dropzone.css" rel="stylesheet" />
    <link href="../css/JCarousel.css" rel="stylesheet" />
    <link href="../css/connected-carousel.css" rel="stylesheet" />
    <%--<script src="https://code.jquery.com/jquery-1.11.3.min.js"></script>--%>
    <%--<script src="../js/jquery-latest.js" type="text/javascript"></script>--%>
    <script src="../js/jquery.ui.widget.js"></script>
    <%--<script type="text/javascript" src="../../Scripts/jquery.MultiFile.js"></script>--%>
    <script type="text/javascript" src="../js/dropzone.js"></script>
    <script type="text/javascript" src="../js/JCarousel.js"></script>

    <script type="text/javascript">
        function uploadComplete() {

            if (Checkfiles() == true) {
                var btnImageUploadClick = document.getElementById("ctl00_ContentPlaceHolder1_btnImageUploadClick");
                btnImageUploadClick.click();
            }
        }
        function uploadComplete2() {


            var btnImageUploadClick = document.getElementById("ctl00_ContentPlaceHolder1_btnImageUploadClick");
            btnImageUploadClick.click();

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

        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtworkarea').focus();
            $('#custattachmentaccordion').accordion({
                activate: function (event, ui) {
                    //on display of accordion preview file section set file carousel.
                    if (ui.newHeader[0].innerText == "Preview Customer Attachments") {
                        setupCustomerAttachmentCarousel();
                    }
                }
            });
            $('#locationimgaccordion').accordion({
                activate: function (event, ui) {
                    //on display of accordion preview image section set image carousel.
                    if (ui.newHeader[0].innerText == "Preview Location Images") {
                        setUpConnectedCarousels();
                    }

                }
            });

        });

        function ValidateImage() {
            // Technical Interview Task#9 : yogesh keraliya
            //var count = $('#<%=hidCount.ClientID %>').val();
            //alert(count);
            //if (count < 2) {
            //    alert('Upload atleast two image.');
            //    return false;
            //}

            return true;
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
          function (result) {
              if (result != null) {
                  var flg = (result.d);

                  if (flg == "True") {
                      $(errorlableid).show();
                      $(errorlableid).html('Verified');
                      document.getElementById('<%=txtProposalCost.ClientID %>').value = document.getElementById('<%=txtAmount.ClientID %>').value;
                      $('#mask').hide();
                      $('#<%=pnlpopup.ClientID %>').hide();
                  }
                  else {
                      $(errorlableid).show();
                      $(errorlableid).html('failure');
                  }
              }
          }
            });
  }

  function focuslost() {
      if (document.getElementById('<%= txtAmount.ClientID%>').value == '') {
          alert('Please enter proposal cost!');
          return false;
      }
      else if (document.getElementById('<%= txtauthpass.ClientID%>').value == '') {
          alert('Please enter admin code!');
          return false;
      }
      else {
          var pagePath = "Custom.aspx/Exists";
          var dataString = "{ 'value':'" + document.getElementById('<%= txtauthpass.ClientID%>').value + "' }";
          var textboxid = "#<%= txtauthpass.ClientID%>";
          var errorlableid = "#<%= lblError.ClientID%>";

          IsExists(pagePath, dataString, textboxid, errorlableid);
          return true;
      }
}
function ShowPopup() {


    $('#ContentPlaceHolder1_txtProposalCost').attr('readonly', 'readonly');
    $('#ContentPlaceHolder1_txtAmount').focus();
    if (document.getElementById('<%=txtProposalCost.ClientID %>').value != '') {
        document.getElementById('<%=txtAmount.ClientID %>').value = document.getElementById('<%=txtProposalCost.ClientID %>').value;
    }
    $('#mask').show();
    $('#<%=pnlpopup.ClientID %>').show();
}
function HidePopup() {

    $('#ContentPlaceHolder1_txtAmount, #ContentPlaceHolder1_txtauthpass').val('');
    $('#ContentPlaceHolder1_lblError').text('');

    $('#mask').hide();
    $('#<%=pnlpopup.ClientID %>').hide();
}

// Yogesh Keraliya : .on method is deprecated since jquery version 1.9
// Replacement is  .on() method.

//$(".btnClose").live('click', function () {
$("body").on('click', '.btnClose', function () {
    $('#<%=txtAmount.ClientID %>, #<%=txtauthpass.ClientID %>, #<%=lblError.ClientID %>').val('');
    HidePopup();
});

/* location picture upload & preview section starts */

//Technical Interview : Yogesh Keraliya
//Code for drag and drop image upload.
//File Upload response from the server
Dropzone.options.dropzoneForm = {
    maxFiles: 5,
    url: "dropzoneimageupload.aspx",
    thumbnailWidth: 100,
    thumbnailHeight: 100,
    init: function () {
        this.on("maxfilesexceeded", function (data) {
            //var res = eval('(' + data.xhr.responseText + ')');
            alert('you are reached maximum pictures limit.');
        });

        // when file is uploaded successfully store its corresponding server side file name to preview element to remove later from server.
        this.on("success", function (file, response) {
            var filename = response.split("^");
            $(file.previewTemplate).append('<span class="server_file">' + filename[0] + '</span>');

            AddLocationPictoViewState(filename[0]);

        });

        //when file is removed from dropzone element, remove its corresponding server side file.
        this.on("removedfile", function (file) {
            var server_file = $(file.previewTemplate).children('.server_file').text();
            RemoveLocPicFromServer(server_file);
        });

        // When is added to dropzone element, add its remove link.
        this.on("addedfile", function (file) {

            // Create the remove button
            var removeButton = Dropzone.createElement("<a><small>Remove file</smalll></a>");

            // Capture the Dropzone instance as closure.
            var _this = this;

            // Listen to the click event
            removeButton.addEventListener("click", function (e) {
                // Make sure the button click doesn't submit the form:
                e.preventDefault();
                e.stopPropagation();
                // Remove the file preview.
                _this.removeFile(file);
            });

            // Add the button to the file preview element.
            file.previewElement.appendChild(removeButton);
        });
    }
};


function RemoveLocPicFromServer(filename) {

    var param = { serverfilename: filename };

    $.ajax({
        type: "POST",
        data: JSON.stringify(param),
        url: "dropzoneimageupload.aspx/RemoveLocationPic",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnLocationPicRemoveSuccess,
        error: OnLocationPicRemoveError
    });

}

function OnLocationPicRemoveSuccess(data) {
    var result = data.d;
    if (result) {

        RemoveLocationPicFromViewState(result);
    }

}

function OnLocationPicRemoveError() {

}

//Add uploaded location picture to viewstate of page to save later.
function AddLocationPictoViewState(serverfilename) {

    var images;

    if ($('#<%= locimages.ClientID %>').val()) {
        images = $('#<%= locimages.ClientID %>').val() + serverfilename + "^";
    }
    else {
        images = serverfilename + "^";
    }

    $('#<%= locimages.ClientID %>').val(images);
    console.log($('#<%= locimages.ClientID %>').val());
    LoadImagePreview();
}

function RemoveLocationPicFromViewState(filename) {
    console.log($('#<%= locimages.ClientID %>').val());
    if ($('#<%= locimages.ClientID %>').val()) {

        //split images added by ^ seperator
        var images = $('#<%= locimages.ClientID %>').val().split("^");

        console.log(images);

        if (images.length > 0) {
            //find index of filename and remove it.
            var index = images.indexOf(filename);

            if (index > -1) {
                images.splice(index, 1);
            }
            console.log(images);


            //join remaining images.
            if (images.length > 0) {
                $('#<%= locimages.ClientID %>').val(images.join("^"));
                LoadImagePreview();
            }
            else {
                $('#<%= locimages.ClientID %>').val("");
            }
        }

    }
}

function LoadImagePreview() {


    var imageli = "<li ><img src=\"../CustomerDocs/LocationPics/**imagename**\"></li>";
    var thumbimageli = "<li data-jcarouselcontrol=\"true\"><img width=\"50\" height=\"50\"  src=\"../CustomerDocs/LocationPics/**imagename**\"></li>";

    if ($('#<%= locimages.ClientID %>').val()) {

        //split images added by ^ seperator
        var images = $('#<%= locimages.ClientID %>').val().split("^");

        if (images.length > 0) {

            $('#ulLocPic').empty();
            $('#ulLocPicThumb').empty();

            //loop through each image and add it to 
            for (var i = 0; i < images.length; i++) {

                if (images[i] != "") {
                    $('#ulLocPic').append(imageli.replace("**imagename**", images[i]));
                    $('#ulLocPicThumb').append(thumbimageli.replace("**imagename**", images[i]));
                }

            }

        }

    }


}

function setUpConnectedCarousels() {

    var connector = function (itemNavigation, carouselStage) {
        return carouselStage.jcarousel('items').eq(itemNavigation.index());
    };

    //remove if already carousel is setup
    //if ($('#locpicMainCarousel').jcarousel()) {
    //    $('#locpicMainCarousel').jcarousel('destroy');
    //}
    //if ($('#locpicthumbCarousel').jcarousel()) {
    //    $('#locpicthumbCarousel').jcarousel('destroy');
    //}

    // Setup the carousels. Adjust the options for both carousels here.
    var carouselStage = $('#locpicMainCarousel')
        .on('jcarousel:reload jcarousel:create', function () {
            var carousel = $(this),
                width = carousel.innerWidth();

            if (width >= 600) {
                width = width / 3;
            } else if (width >= 350) {
                width = width / 1;
            }

            carousel.jcarousel('items').css('width', Math.ceil(width) + 'px');
        })
        .jcarousel({
            auto: 1,
            animation: {
                duration: 800,
                easing: 'linear',
                complete: function () {
                }
            }
        });

    var carouselNavigation = $('#locpicthumbCarousel').jcarousel({
        auto: 1,
        animation: {
            duration: 800,
            easing: 'linear',
            complete: function () {
            }
        }
    });

    // We loop through the items of the navigation carousel and set it up
    // as a control for an item from the stage carousel.
    carouselNavigation.jcarousel('items').each(function () {
        var item = $(this);

        // This is where we actually connect to items.
        var target = connector(item, carouselStage);

        item
            .on('jcarouselcontrol:active', function () {
                carouselNavigation.jcarousel('scrollIntoView', this);
                item.addClass('active');
            })
            .on('jcarouselcontrol:inactive', function () {
                item.removeClass('active');
            })
            .jcarouselControl({
                target: target,
                carousel: carouselStage
            });
    });

    // Setup controls for the stage carousel
    $('.jcarousel-control-prev')
        .on('jcarouselcontrol:inactive', function () {
            $(this).addClass('inactive');
        })
        .on('jcarouselcontrol:active', function () {
            $(this).removeClass('inactive');
        })
        .jcarouselControl({
            target: '-=1'
        });

    $('.jcarousel-control-next')
        .on('jcarouselcontrol:inactive', function () {
            $(this).addClass('inactive');
        })
        .on('jcarouselcontrol:active', function () {
            $(this).removeClass('inactive');
        })
        .jcarouselControl({
            target: '+=1'
        });

    // Setup controls for the navigation carousel
    $('#locpicthumbprev')
        .on('jcarouselcontrol:inactive', function () {
            $(this).addClass('inactive');
        })
        .on('jcarouselcontrol:active', function () {
            $(this).removeClass('inactive');
        })
        .jcarouselControl({
            target: '-=1'
        });

    $('#locpicthumbnext')
        .on('jcarouselcontrol:inactive', function () {
            $(this).addClass('inactive');
        })
        .on('jcarouselcontrol:active', function () {
            $(this).removeClass('inactive');
        })
        .jcarouselControl({
            target: '+=1'
        });

}

/* location picture upload & preview section ends */


/*customer attachment upload section start */



//Technical Interview : Yogesh Keraliya
//Code for drag and drop customer attachment upload.
//File Upload response from the server
Dropzone.options.cusattchForm = {
    url: "customerattachmentupload.aspx",
    thumbnailWidth: 100,
    thumbnailHeight: 100,
    init: function () {

        // when file is uploaded successfully store its corresponding server side file name to preview element to remove later from server.
        this.on("success", function (file, response) {
            var filename = response.split("^");
            $(file.previewTemplate).append('<span class="server_file">' + filename[0] + '</span>');

            AddCustomerAttachmenttoViewstate(filename[0]);

        });

        //when file is removed from dropzone element, remove its corresponding server side file.
        this.on("removedfile", function (file) {
            var server_file = $(file.previewTemplate).children('.server_file').text();
            RemoveCustomerAttchmentFromServer(server_file);
        });

        // When is added to dropzone element, add its remove link.
        this.on("addedfile", function (file) {

            // Create the remove button
            var removeButton = Dropzone.createElement("<a><small>Remove file</smalll></a>");

            // Capture the Dropzone instance as closure.
            var _this = this;

            // Listen to the click event
            removeButton.addEventListener("click", function (e) {
                // Make sure the button click doesn't submit the form:
                e.preventDefault();
                e.stopPropagation();
                // Remove the file preview.
                _this.removeFile(file);
            });

            // Add the button to the file preview element.
            file.previewElement.appendChild(removeButton);
        });
    }
};

function setupCustomerAttachmentCarousel() {

    ////remove if already carousel is setup

    //if ($('#customerattachmentCarousel').jcarousel()) {
    //    $('#customerattachmentCarousel').jcarousel('destroy');
    //}

    // Setup the carousels. Adjust the options for both carousels here.
    var carouselNavigation = $('#customerattachmentCarousel').jcarousel({
        auto: 1,
        animation: {
            duration: 800,
            easing: 'linear'
        }
    });

    // We loop through the items of the navigation carousel and set it up
    // as a control for file preview.
    carouselNavigation.jcarousel('items').each(function () {
        var item = $(this);


        item
            .on('jcarouselcontrol:active', function () {
                carouselNavigation.jcarousel('scrollIntoView', this);
                item.addClass('active');
            })
            .on('jcarouselcontrol:inactive', function () {
                item.removeClass('active');
            });

        //set preview code here.
        item.on('click', function (e) {
            e.preventDefault();
            var atag = item.find('a:first');            
            var url = atag.attr("data-src");
            var viewer = atag.attr("data-viewer");
            //console.log("fileurl: " + url);
            //console.log("fileviewer: " + viewer);
            setPreviewFile(url, viewer);
        });

    });
       
    // Setup controls for the navigation carousel
    $('#custattachprev')
        .on('jcarouselcontrol:inactive', function () {
            $(this).addClass('inactive');
        })
        .on('jcarouselcontrol:active', function () {
            $(this).removeClass('inactive');
        })
        .jcarouselControl({
            target: '-=1'
        });

    $('#custattachnext')
        .on('jcarouselcontrol:inactive', function () {
            $(this).addClass('inactive');
        })
        .on('jcarouselcontrol:active', function () {
            $(this).removeClass('inactive');
        })
        .jcarouselControl({
            target: '+=1'
        });

}

function RemoveCustomerAttchmentFromServer(filename) {

    var param = { serverfilename: filename };

    $.ajax({
        type: "POST",
        data: JSON.stringify(param),
        url: "customerattachmentupload.aspx/RemoveUploadedattachment",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnAttachmentRemoveSuccess,
        error: OnAttachmentRemoveError
    });

}

//Once file is removed from server, remove it from hidden field as well.
function OnAttachmentRemoveSuccess(data) {
    var result = data.d;
    if (result) {

        RemoveCustomerAttachmentFromViewState(result);
    }

}

function OnAttachmentRemoveError() {

}

//Add uploaded attachment file to hidden field of page to save later.
function AddCustomerAttachmenttoViewstate(serverfilename) {

    var files;

    if ($('#<%= hdnAttachments.ClientID %>').val()) {
        files = $('#<%= hdnAttachments.ClientID %>').val() + serverfilename + "^";
    }
    else {
        files = serverfilename + "^";
    }

    $('#<%= hdnAttachments.ClientID %>').val(files);
    console.log($('#<%= hdnAttachments.ClientID %>').val());
    LoadCustomerAttachmentPreview();
}

function RemoveCustomerAttachmentFromViewState(filename) {

    console.log($('#<%= hdnAttachments.ClientID %>').val());
    if ($('#<%= hdnAttachments.ClientID %>').val()) {

        //split images added by ^ seperator
        var files = $('#<%= hdnAttachments.ClientID %>').val().split("^");

        console.log(files);

        if (files.length > 0) {
            //find index of filename and remove it.
            var index = files.indexOf(filename);

            if (index > -1) {
                files.splice(index, 1);
            }
            console.log(files);

            //join remaining files.
            if (files.length > 0) {
                $('#<%= hdnAttachments.ClientID %>').val(files.join("^"));
                LoadCustomerAttachmentPreview();
            }
            else {
                $('#<%= hdnAttachments.ClientID %>').val("");
            }
        }

    }

}

function LoadCustomerAttachmentPreview() {


    var fileli = "<li style=\"width:50px !important;\" data-jcarouselcontrol=\"true\"><a data-viewer=\"**viewer**\" data-src=\"**url**\"><img width=\"50\" height=\"50\" src=\"**imagename**\" alt=\"\"><span style=\"color: red; position: relative; display: block; text-align: center;\">**filename**</span></a></li>";

    if ($('#<%= hdnAttachments.ClientID %>').val()) {

        //split files added by ^ seperator
        var files = $('#<%= hdnAttachments.ClientID %>').val().split("^");

        if (files.length > 0) {

            $('#ulattchmentThumb').empty();

            //loop through each file and add it to preview
            for (var i = 0; i < files.length; i++) {

                if (files[i] != "") {

                    var fileextension = (files[i].split("."))[1];

                    //get appropriate icon image based on file type.
                    var iconImageURL = getIconImageUrl(fileextension);
                    //get appropriate viewer based on file type.
                    var fileviewer = getfileViewer(fileextension);
                    //build file url to be viewed.
                    var fileurl = encodeURI(document.location.origin + '/UploadedFiles/' + files[i]);

                    // replace iconimage url, file viewer - google/microsoft , fileurl to send viewer.
                    $('#ulattchmentThumb').append(fileli.replace("**imagename**", iconImageURL).replace("**viewer**", fileviewer).replace("**url**", fileurl).replace("**filename**",files[i]));
                }

            }

        }

    }

}

// get icon of file type from extension of file.
function getIconImageUrl(fileextension) {
    var iconUrl = "../img/icons/";
    switch (fileextension) {
        case "jpg":
        case "jpeg":
            iconUrl = iconUrl + "jpg-image-file-format.png";
            break;
        case "png":
            iconUrl = iconUrl + "png-file-extension-interface-symbol.png";
            break;
        case "gif":
            iconUrl = iconUrl + "gif-file-format.png";
            break;
        case "pdf":
            iconUrl = iconUrl + "pdf-file-format-symbol.png";
            break;
        case "doc":
        case "docx":
            iconUrl = iconUrl + "doc-file-format-symbol.png";
            break;
        case "xls":
        case "xlsx":
            iconUrl = iconUrl + "xls-file-format-symbol.png";
            break;
        default:
            iconUrl = iconUrl + "blank-file.png";
            break;

    }
    return iconUrl;
}

function getfileViewer(fileextension) {
    var viewer;
    switch (fileextension) {
        case "doc":
        case "docx":
        case "xls":
        case "xlsx":
            viewer = "msoffice";
            break;
        default:
            viewer = "google";
            break;

    }
    return viewer;

}

// set dynamic url to iframe with google doc viewer.
function setPreviewFile(url, viewer) {

    var googledocsviewerurl = "http://docs.google.com/viewer?url=**url**&embedded=true";
    var officeappsviewerurl = "http://view.officeapps.live.com/op/view.aspx?src=**url**";

    var viewurl;

    // if office document needs to view than use microsoft office app viwer.
    if (viewer == "msoffice") {
        viewurl = officeappsviewerurl.replace("**url**", url);
    }
    else {// use google docs viewer.
        viewurl = googledocsviewerurl.replace("**url**", url);
    }

    $('#previewFrame').attr("src", viewurl);

}



/* customer attachment upload section end */

    </script>
    <style type="text/css">
        .style2 {
            width: 100%;
        }

        .dz-max-files-reached {
            background-color: #ffffff;
        }

        #mask {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
    </style>

   <%--Style for adding multiple product categories - controls are in AddProductLinesControl.ascx--%>
    <style>
        .product-categories { margin: 0 3%; background: url('../img/line.png') repeat-x 50% bottom; padding: 10px 0; }
            .product-categories .expand { padding: 10px 10px 10px 0; cursor: pointer; font-size: 20px; font-weight: bold; line-height: 0px; width: 12px; display: inline-block; }
            .product-categories > table { width: 100%; }
                .product-categories > table tr td { width: 5%; }
                    .product-categories > table tr td:nth-child(even) { width: 95%; }

            .product-categories .body table tr td { vertical-align: top; padding: 10px 5px 0 0; background: url('../img/line.png') repeat-x 50% top;}
                .product-categories .body table tr td strong { float: right; margin-top: 8px; }

            .product-categories label { margin: 10px 0; display: inline-block; font-weight: normal; width:140px; }

            .product-categories input,
            .product-categories textarea,
            .product-categories select { padding: 5px; margin: 0; border-radius: 5px; border: #b5b4b4 1px solid; line-height: 14px; width: 225px;margin-right:75px; }
    </style>
    <script>
        function uploadError_multiple() {
            // alert("Error");
        }
        function uploadStarted_multiple() {
            // alert("uploadStarted");
        }
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
        <h1 id="h1Heading" runat="server"></h1>
        <div class="form_panel_custom" id="customDiv">
            <span>
                <label>
                    Customer Id:
                </label>
                <b>
                    <asp:Label ID="lblmsg" runat="server" Visible="true"></asp:Label></b> </span>
            <ul style="margin-bottom:0;">
                <li style="width: 95%;" class="last">
                    <table>
                        <tr>
                            <td style="width:25%; padding:10px 0 0 0;">
                                <label>
                                    Customer: <span>*</span></label>
                                <asp:TextBox ID="txtCustomer" runat="server" MaxLength="35" onkeypress="return isAlphaKey(event);"
                                    onkeyup="javascript:Alpha(this)" TabIndex="1"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvcustomer" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtCustomer" ErrorMessage="Enter Customer Name"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width:75%">
                                <div class="btn_sec" style="text-align:left">
                                    <asp:Button ID="btnAddProductLine" type="submit" runat="server" Text="Add Product Category" OnClick="btnAddProductLine_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
            </ul>
             <asp:PlaceHolder ID="placeHolderProductLines" runat="server"></asp:PlaceHolder>
            <br />
            <br />
            <br/>
            <ul>
                <li style="width: 49%;">
                    <table id="tblcustom" runat="server" border="0" cellspacing="0" cellpadding="0">
                        
                        <tr>
                            <td>
                                <label>
                                    Proposal Terms:
                                </label>
                                <asp:TextBox ID="txtProposalTerm" runat="server" TextMode="MultiLine" autocomplete="false"
                                    TabIndex="3"></asp:TextBox>
                                <label>
                                    <asp:RequiredFieldValidator ID="rfvTerms" runat="server" ForeColor="Red" ValidationGroup="save"
                                        ControlToValidate="txtProposalTerm" ErrorMessage="Enter Proposal Term"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Proposal Costs: <span>*</span></label><strong>$</strong>
                                <%--<asp:TextBox ID="txtProposalCost" AutoCompleteType="Disabled" runat="server" onclick="ShowPopup()"
                                    onkeypress="ShowPopup()" TabIndex="5"></asp:TextBox>--%>
                                <asp:TextBox ID="txtProposalCost" AutoCompleteType="Disabled" runat="server" TabIndex="5"></asp:TextBox>
                                <label>
                                </label>
                                <asp:RequiredFieldValidator ID="rfvProposalCost" runat="server" ForeColor="Red" ValidationGroup="save"
                                    ControlToValidate="txtProposalCost" ErrorMessage="Enter Proposal Cost"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="custattachmentaccordion">

                                    <h3><span>Upload Customer Attachments
                                    <input id="hdnAttachments" runat="server" type="hidden" />
                                    </span></h3>
                                    <div>
                                        <div class="dropzone" style="overflow: auto; max-height: 300px;" id="cusattchForm">
                                            <div class="fallback">
                                                <input name="file" type="file" multiple />
                                                <input type="submit" value="Upload" />
                                            </div>
                                        </div>
                                    </div>
                                    <h3>Preview Customer Attachments</h3>
                                    <div>
                                        <iframe id="previewFrame" style="border-style: none; width: 470px; height: 400px;"></iframe>
                                        <div style="width: 470px; max-height: 200px;">
                                            <div class="connected-carousels">
                                                <div class="navigation">
                                                    <a id="custattachprev" class="prev prev-navigation" href="#" data-jcarouselcontrol="true">‹</a>
                                                    <a id="custattachnext" class="next next-navigation" href="#" data-jcarouselcontrol="true">›</a>
                                                    <div id="customerattachmentCarousel" class="carousel carousel-navigation" data-jcarousel="true">
                                                        <ul id="ulattchmentThumb">
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <ajaxToolkit:AsyncFileUpload ID="AsyncFileUploadCustomerAttachment" runat="server" ClientIDMode="AutoID" ThrobberID="abc"
                                    OnUploadedComplete="AsyncFileUploadCustomerAttachment_UploadedComplete" CompleteBackColor="White"
                                    Style="width: 22% !important; display: none;" OnClientUploadComplete="uploadComplete2" />
                                <%-- <asp:FileUpload ID="fileAttachment" runat="server" class="multi" TabIndex="6" />--%>

                                <label>

                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="" Visible="true" OnClick="lnkDownload_Click"></asp:LinkButton>

                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Customer Supplied Material:</label>
                                <asp:DropDownList ID="drpMaterial" runat="server" Enabled="false" AutoPostBack="True" OnTextChanged="drpMaterial_TextChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem Text="Driveway" Value="Driveway"></asp:ListItem>
                                    <asp:ListItem Text="Garage" Value="Garage"></asp:ListItem>
                                    <asp:ListItem Text="Front Yard" Value="Front Yard"></asp:ListItem>
                                    <asp:ListItem Text="Back Yard" Value="Back Yard"></asp:ListItem>
                                    <asp:ListItem Text="Lside" Value="Lside"></asp:ListItem>
                                    <asp:ListItem Text="Rside" Value="Rside"></asp:ListItem>

                                    <asp:ListItem Text="other" Value="other"></asp:ListItem>
                                </asp:DropDownList>

                                <label>
                                    <asp:CheckBox ID="chkCustSupMaterial" runat="server" Text="N/A" AutoPostBack="true"
                                        Checked="true" TextAlign="Right" OnCheckedChanged="chkCustSupMaterial_CheckedChanged" />
                                </label>


                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCustSupMaterial" runat="server" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Material / Dumpster Storage:</label>
                                <asp:DropDownList ID="drpStorage" runat="server" Enabled="false" AutoPostBack="True" OnTextChanged="drpStorage_TextChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                    <asp:ListItem Text="Driveway" Value="Driveway"></asp:ListItem>
                                    <asp:ListItem Text="Garage" Value="Garage"></asp:ListItem>
                                    <asp:ListItem Text="Front Yard" Value="Front Yard"></asp:ListItem>
                                    <asp:ListItem Text="Back Yard" Value="Back Yard"></asp:ListItem>
                                    <asp:ListItem Text="Lside" Value="Lside"></asp:ListItem>
                                    <asp:ListItem Text="Rside" Value="Rside"></asp:ListItem>

                                    <asp:ListItem Text="other" Value="other"></asp:ListItem>
                                </asp:DropDownList>

                                <label>
                                    <asp:CheckBox ID="chkStorage" runat="server" Text="N/A" TextAlign="Right" AutoPostBack="true"
                                        Checked="true" OnCheckedChanged="chkStorage_CheckedChanged" />
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStorage" runat="server" Visible="false"></asp:TextBox></td>
                        </tr>
                    </table>
                </li>
                <li style="width: 49%;" class="last">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label>
                                    Work Area:                      
                                    <asp:TextBox ID="txtworkarea" runat="server" MaxLength="35" TabIndex="2"></asp:TextBox>
                                    <label>
                                    </label>
                                    <asp:RequiredFieldValidator ID="rfvworkarea" runat="server" ForeColor="Red" ValidationGroup="save"
                                        ControlToValidate="txtworkarea" ErrorMessage="Enter Work Area"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="locationimgaccordion">
                                    <h3><span>Upload Location Images: (Upload maximum 5 images) 
                                    <input id="locimages" runat="server" type="hidden" />
                                    </span></h3>
                                    <div class="dropzone" style="overflow: auto; max-height: 300px;" id="dropzoneForm">
                                        <div class="fallback">
                                            <input name="file" type="file" multiple />
                                            <input type="submit" value="Upload" />
                                        </div>
                                    </div>
                                    <h3>Preview Location Images</h3>
                                    <div>
                                        <div style="width: 480px;">
                                            <div class="jcarousel-wrapper">
                                                <div id="locpicMainCarousel" class="jcarousel carousel-stage" data-jcarousel="true">
                                                    <ul id="ulLocPic">
                                                    </ul>
                                                </div>

                                                <a class="jcarousel-control-prev" href="#" data-jcarouselcontrol="true"><span>‹</span></a>
                                                <a class="jcarousel-control-next" href="#" data-jcarouselcontrol="true"><span>›</span></a>
                                            </div>
                                            <div class="connected-carousels">
                                                <div class="navigation">
                                                    <a id="locpicthumbprev" class="prev prev-navigation" href="#" data-jcarouselcontrol="true">‹</a>
                                                    <a id="locpicthumbnext" class="next next-navigation" href="#" data-jcarouselcontrol="true">›</a>
                                                    <div id="locpicthumbCarousel" class="carousel carousel-navigation" data-jcarousel="true">
                                                        <ul id="ulLocPicThumb">
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <table style="display: none;" cellpadding="0" cellspacing="0" class="style2">
                                    <tr>
                                        <td style="width: 20%;">Location Image
                                        </td>
                                        <td style="width: 60%; display: none;">

                                            <ajaxToolkit:AsyncFileUpload ID="ajaxFileUpload" runat="server" ClientIDMode="AutoID"
                                                OnUploadedComplete="ajaxFileUpload_UploadedComplete" ThrobberID="imgLoad" CompleteBackColor="White"
                                                OnClientUploadComplete="uploadComplete" Style="width: 92% !important; margin-right: 6px" />

                                            <asp:Button ID="btnImageUploadClick" ClientIDMode="AutoID" runat="server" CausesValidation="false"
                                                Text="hidden" Style="display: none" OnClick="btnImageUploadClick_Click" />

                                            <%--<asp:FileUpload ID="FileUpload1" runat="server" onchange="readURL(this);" TabIndex="4"/>--%>
                                            <%--<asp:RequiredFieldValidator ID="reqUpload" runat="server" ControlToValidate="FileUpload1" 
                                                ErrorMessage="Upload atleast two image." Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="save">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td></td>
                                        <td style="width: 20%;">
                                            <%--<asp:Button ID="bntAdd" runat="server" Text="Attach" Width="50px" OnClick="bntAdd_Click"
                                                OnClientClick="return ValidateAddImage()" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:UpdatePanel ID="pnlUpdate" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="Image" runat="server" Text="Image" Font-Bold="true" Visible="false"></asp:Label><br />
                                                    <asp:Image ID="imglocation" runat="server"
                                                        Height="100px" Width="100px" Visible="false" />
                                                    <asp:DataList ID="gvCategory1" runat="server" OnItemCommand="gvCategory1_ItemCommand"
                                                        DataKeyField="RowSerialNo" AllowPaging="true" OnItemDataBound="gvCategory1_ItemDataBound"
                                                        RepeatColumns="0" RepeatDirection="Horizontal">


                                                        <ItemTemplate>


                                                            <asp:ImageButton ID="imglocation2" runat="server" ImageUrl='<%#Eval("LocationPicture")%>'
                                                                Height="50px" Width="50px" CommandArgument='<%#Eval("RowSerialNo")%>'
                                                                CommandName="ShowRec" /><br />
                                                            <asp:LinkButton ID="lnkCategoryDelete" runat="server" Text="X" CommandArgument='<%#Eval("RowSerialNo")%>'
                                                                CommandName="DeleteRec" CausesValidation="false" OnClientClick='javascript:return confirm("Are you sure want to delete this entry?");'></asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <asp:HiddenField ID="hidCount" runat="server" />
                                                </ContentTemplate>

                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Special Instructions / Exemptions:
                                </label>
                                <asp:TextBox ID="txtspecialIns" runat="server" TextMode="MultiLine" TabIndex="7"></asp:TextBox>
                                <label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkPermit" runat="server" Text="Permit Required" />
                                <asp:CheckBox ID="chkHabitat" runat="server" Text="Habitat For Humanity Pick Up" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                </li>
            </ul>
            <div class="btn_sec">
                <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="save" OnClientClick="return ValidateImage()"
                    OnClick="btnsave_Click" TabIndex="8" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" ValidationGroup="exit" TabIndex="9"
                    OnClick="btnexit_Click" />
                <asp:HiddenField ID="hidProdId" runat="server" />
                <asp:HiddenField ID="hidProdType" runat="server" />
            </div>
            <div id="mask">
            </div>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="175px" Width="300px"
                Style="z-index: 111; background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                    <tr style="background-color: #b5494c">
                        <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                            align="center">Admin Verification <a id="closebtn" style="color: white; float: right; text-decoration: none"
                                class="btnClose" href="#">X</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 45%; text-align: center;">
                            <asp:Label ID="LabelValidate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Amount:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumericKey(event);"
                                MaxLength="20" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Admin Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtauthpass" runat="server" TextMode="Password" Text=""></asp:TextBox>
                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <input type="button" class="btnVerify" value="Verify" onclick="javascript: return focuslost();" />
                            &nbsp;&nbsp;
                            <input type="button" class="btnClose" value="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--<ajaxToolkit:ModalPopupExtender ID="myModalPopupExtender" runat="server" TargetControlID="txtProposalCost"
                    PopupControlID="pnlVerify" CancelControlID="btnCancel">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlVerify" runat="server" BackColor="Gray" height="200" width="450" Style="display: none;">
                    <iframe id="frameeditexpanse" runat="server" frameborder="0" src="CustomVerification.aspx" height="210" width="450">
                    </iframe>
                  <input id="btnCancel" value="Cancel" type="button" style="display:none;" />
                </asp:Panel>--%>
        </div>
    </div>
</asp:Content>
