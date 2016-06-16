<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ZipFile.aspx.cs"
    Inherits="JG_Prospect.Sr_App.CreateZip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>JG Prospect</title>
    <link href="/css/screen.css" rel="stylesheet" media="screen" type="text/css" />
    <link href="/css/screen.css" rel="stylesheet" media="screen" type="text/css" />
    <script src="../js/hover.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/smoothness/jquery-ui.css"
        type="text/css" />
<%--    <link rel="stylesheet" href="../js/jquery.jb.shortscroll.css" />
    <script type="text/javascript" src="../js/mousewheel.js"></script>
    <script type="text/javascript" src="../js/jquery.jb.shortscroll.js"></script>--%>
  <script type="text/javascript" src="../js/jquery.smooth-scroll.js"></script>
    <script type="text/javascript">
        this.imagePreview = function () {
            /* CONFIG */

            xOffset = 10;
            yOffset = 30;

            // these 2 variable determine popup's distance from the cursor
            // you might want to adjust to get the right result

            /* END CONFIG */
            $("a.preview").hover(function (e) {
                this.t = this.title;
                this.title = "";
                var c = (this.t != "") ? "<br/>" + this.t : "";
                $("body").append("<p id='preview'><img src='" + this.href + "' alt='Image preview' height='250px' width='350px' />" + c + "</p>");
                $("#preview")
			.css("top", (e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px")
			.fadeIn("slow");
            },
	function () {
	    this.title = this.t;
	    $("#preview").remove();
	});
            $("a.preview").mousemove(function (e) {
                $("#preview")
			.css("top", (e.pageY - xOffset) + "px")
			.css("left", (e.pageX + yOffset) + "px");
            });
        };
        $(document).ready(function () {
            imagePreview();
           // $('#target').shortscroll();
        });
    </script>
    <style>
        pre
        {
            display: block;
            font: 100% "Courier New" , Courier, monospace;
            padding: 10px;
            border: 1px solid #bae2f0;
            background: #e3f4f9;
            margin: .5em 0;
            overflow: auto;
            width: 800px;
        }
        
        img
        {
            border: none;
        }
        
        /*  */
        
        #preview
        {
            position: absolute;
            border: 1px solid #ccc;
            background: #333;
            padding: 5px;
            display: none;
            color: #fff;
        }
        
        /*  */
    </style>
    <style type="text/css">
   
    .scrollme {
      height: 550px;
      width: 550px;
      min-height:450px;
      overflow: auto;
      padding-left: 8px;
      border: 1px solid #999;
    }
   
  </style>
 
  <script type="text/javascript">
      $(document).ready(function () {

          $('ul.mainnav a').smoothScroll();

          $('p.subnav a').click(function (event) {
              event.preventDefault();
              var link = this;
              $.smoothScroll({
                  scrollTarget: link.hash
              });
          });

          $('button.scrollsomething').click(function () {
              $.smoothScroll({
                  scrollElement: $('div.scrollme'),
                  scrollTarget: '#findme'
              });
              return false;
          });
          $('button.scrollhorz').click(function () {
              $.smoothScroll({
                  direction: 'left',
                  scrollElement: $('div.scrollme'),
                  scrollTarget: '.horiz'
              });
              return false;
          });

      });

  </script>
    <!--accordion jquery-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <h1>
            Job Documents</h1>        
            <div class="form_panel">   
             <div class="scrollme">     
                <div class="btn_sec" style="float: right; padding-right:10px; padding-top: 35px;">
                    <asp:Button ID="btnGo" Text="Zip & Download" OnClick="btnGo_Click" runat="server" />
                </div><span id="ErrorMessage" style="color: Red" runat="server"></span>
              <div class="clr"></div>
                <div id="divmain" class="target">
                    <asp:GridView ID="Gridviewdocs" runat="server" AutoGenerateColumns="false" CssClass="grid"
                        Width="100%" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
                        OnRowDataBound="Gridviewdocs_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Files" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%-- <a href="~/CustomerDocs/maps/Chrysanthemum.jpg" class="preview"><img src="~/CustomerDocs/maps/Chrysanthemum.jpg" Height="30px" width="40px"  alt="" /></a>--%>
                                    <asp:HiddenField ID="Hiddenid" runat="server" Value='<%# Eval("srno")%>' />
                                    <a href='<%# Eval("DocumentName","../CustomerDocs/{0}") %>' class="preview">
                                        <asp:Image ID="Image1" runat="server" Width="60px" CssClass="preview" ImageUrl='<%# Eval("DocumentName","~/CustomerDocs/{0}") %>'
                                            Height="90px" /></a>
                                    <asp:Label ID="labelfile" ForeColor="Black" runat="server" Text='<%# Eval("DocumentName") %>' />
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="labeldesc" ForeColor="Black" runat="server" Text='<%# Eval("DocDescription") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select File to Archieve">
                                <ItemTemplate>
                                    <asp:CheckBox ID="checkbox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
   </div>
    </form>
</body>
</html>
