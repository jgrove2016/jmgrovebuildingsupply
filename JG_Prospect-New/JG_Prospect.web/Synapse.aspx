<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Synapse.aspx.cs" Inherits="JG_Prospect.Synapse" %>

<!DOCTYPE html>





<script src="https://d7630u3gvmjyq.cloudfront.net/static_assets/v2/iframes/express/synapse_express_v2.js"></script>


<html xmlns="http://www.w3.org/1999/xhtml">
    

<head runat="server">
   
    <title></title>
  <%--  <script type="text/javascript">

        var colors = {
            'trim': '#059db1 ',
            'unfocused': '#95a5a6 ',
            'text': '#212121 ',
            'background': 'white'
        };
        var userInfo = {
            'oauth_consumer_key': 'Yvrg9EXbfQmp1QEKmcLDxzPIvLSLFpsOUGby4GpS',
        };
        //you can also supply the user info like how you could in V1
        //var userInfo = {
        //    'client_id':'YOUR_SYNAPSE_EXPRESS_CLIENT_ID',
        //    'client_secret':'YOUR_SYNAPSE_EXPRESS_CLIENT_SECRET',
        //    'email':'test117@synapsepay.com',
        //    'full_name':'Test User',
        //    'phone_number':'(901)861-1010'
        //};
        var payload = {
            'colors': colors,
            'userInfo': userInfo,
            'seller_id': 1694,
            'amount': '1.00',
            'development_mode': true
        };

        setupSynapseSite(payload);
        $("#synapse_iframe").css("visibility", "visible");
        $("#synapse_iframe").css("height", "100%");
        $("#synapse_iframe").css("width", "100%");
        expressReciver = function (e) {
            try {
                var json = JSON.parse(e.data);
                if (json.success) {
                    //transaction went through, so do something
                    $("#synapse_iframe").css("visibility", "hidden");
                    $("#synapse_iframe").css("height", "0%");
                    $("#synapse_iframe").css("width", "0%");
                    $("#synapse_iframe").prop("src", "");
                } else if (json.cancel) {
                    //user hit go back, so close the iFrame
                    $("#synapse_iframe").css("visibility", "hidden");
                    $("#synapse_iframe").css("height", "0%");
                    $("#synapse_iframe").css("width", "0%");
                    $("#synapse_iframe").prop("src", "");
                } else {
                    //transaction failed.
                };
            } catch (e) {
                // console.log(e);
            };
        };
        window.addEventListener('message', expressReciver, false);


      </script>--%>
</head>

<body>
   
    <form id="form1" runat="server">
        
    <div>
      
   <%--   <iframe id="synapse_iframe" style="top:0; height:0%;width:0%;position:fixed;z-index:100000;visibility:hidden;"  frameborder="0" >
          <h1>Soham</h1>


      </iframe>--%>




    </div>
    </form>
</body>
</html>
