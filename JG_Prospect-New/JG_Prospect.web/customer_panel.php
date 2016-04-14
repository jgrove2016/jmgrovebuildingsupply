<?php 
ob_start();
session_start();
error_reporting(E_ALL & ~E_NOTICE);
//start of code to insert in sql server
 $serverName = "localhost\sql2008r2"; //serverName\instanceName

    $connectionInfo = array( "Database"=>"jgrove_JGP", "UID"=>"sa", "PWD"=>"sa@12345");
    $conn = sqlsrv_connect( $serverName, $connectionInfo);
    if( $conn === false ) {
         die( print_r( sqlsrv_errors(), true));
    }
	else
	{//echo "Success";
	}
function clean($string) {
 
							   $string = preg_replace('/[^A-Za-z0-9\ \-\~\!\@\#\$\%\^\&\*\(\)\/\]\[\'\;\/\.\,\}\{\:\?\>\<\+\_\`\=\\n]/', '', $string); // Removes special chars.
							    
							
							   return ( $string); // Replaces multiple hyphens with single one.
							}
 	//error_reporting(0);
$currentpage = "payment"; 
 date_default_timezone_set("Asia/Kolkata");
$now=date("YmdHis");
//echo $now;

if($_REQUEST['kyc_submit']!=''){

$data = file_get_contents($_FILES['name_of_control']['tmp_name']);
$data = base64_encode($data);
$name=basename( $_FILES['name_of_control']['name']);
//echo $name;
$pos = strrpos($name, ".");
$type=substr($name,$pos+1,strlen($name));
 // KYC Documentation
 
$payload = array(
    "login" => array(
        //Oauth_key of the user to add KYC doc
        "oauth_key" => $_REQUEST['oauth']
    ),
    "user" => array(
        //doc data
        "doc" => array(
            'attachment' => 'data:text/'.$type.';base64,'.$data
        ),
        "fingerprint" => "suasusau21324redakufejfjsf",
    )
);


$json=json_encode($payload);
//echo "<pre>";
//print_r($payload);
//exit;
//$payload=json_encode($json);



$sUrl= "https://sandbox.synapsepay.com/api/v3/user/doc/attachments/add";
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL,$sUrl);
    curl_setopt($ch, CURLOPT_VERBOSE, 1);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
    curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json; charset=utf-8'));
    curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
    $result = curl_exec($ch);
    curl_close($ch);
	$string=json_decode($result);
//	print_r($string);
//exit;
header("Location:customer_panel.php?status=review_doc");
}

if($_REQUEST['logout']!='')
{
session_destroy();
header("Location:login.aspx");

}


if($_REQUEST['Cust_Id']!='')
{
$_SESSION['Cust_Id']=$_REQUEST['Cust_Id'];
header("Location:customer_panel.php");

}

$customerID=$_SESSION['Cust_Id'];	



if($_REQUEST['userinfo_submit']!='')
{
//echo "<br>add".$_REQUEST['address'];
//echo "<br>phone".$_REQUEST['phone'];

	$i=2;
	$arr='';
	while($_REQUEST['email'.$i])
	{
	$arr.=$_REQUEST['email'.$i].",";
	$i++;
	if($i>20)
	{break;}
	}

$i=2;
	$phone_arr='';
	while($_REQUEST['phone'.$i])
	{
	$phone_arr.=$_REQUEST['phone'.$i].",";
	$i++;
	if($i>20)
	{break;}
	}
	
$i=2;
	$phone_type_arr='';
	while($_REQUEST['phone_type'.$i])
	{
	$phone_type_arr.=$_REQUEST['phone_type'.$i].",";
	$i++;
	if($i>20)
	{break;}
	}	


/*echo "update dbo.new_customer set CustomerName='".$_REQUEST['f_name']."',lastname='".$_REQUEST['l_name']."',Email='".$_REQUEST['email']."',CustomerAddress='".$_REQUEST['address']."',CellPh='".$_REQUEST['phone']."',City='".$_REQUEST['city']."',State='".$_REQUEST['state']."',BillingAddress='".$_REQUEST['address1']."',AdditionalEmail='".$arr."',AdditionalPhoneNo='".$phone_arr."',AdditionalPhoneType='".$phone_type_arr."',PhoneType='".$_REQUEST['phone_type']."',Zip='".$_REQUEST['zip']."' where id='".$customerID."'";
exit;*/
 $sql_user = "update dbo.new_customer set CustomerName='".$_REQUEST['f_name']."',lastname='".$_REQUEST['l_name']."',Email='".$_REQUEST['email']."',CustomerAddress='".$_REQUEST['address']."',CellPh='".$_REQUEST['phone']."',City='".$_REQUEST['city']."',State='".$_REQUEST['state']."',BillingAddress='".$_REQUEST['address1']."',AdditionalEmail='".$arr."',AdditionalPhoneNo='".$phone_arr."',AdditionalPhoneType='".$phone_type_arr."',PhoneType='".$_REQUEST['phone_type']."',ZipCode='".$_REQUEST['zip']."',AccType='".$_REQUEST['perbis']."',ConPref='".$_REQUEST['pref']."' where id='".$customerID."'";

 //echo "update dbo.new_customer set CustomerName='".$_REQUEST['f_name']."',lastname='".$_REQUEST['l_name']."',CustomerAddress='".$_REQUEST['address']."',CellPh='".$_REQUEST['phone']."',City='".$_REQUEST['city']."',State='".$_REQUEST['state']."',BillingAddress='".$_REQUEST['address1']."' where id='".$customerID."'";
						$query_user = sqlsrv_query($conn, $sql_user);
						//exit;
						  if ($query === false){  
						exit("<pre>".print_r(sqlsrv_errors(), true));
						}
						header("Location:customer_panel.php?msg=userinfo_succ");
}


if($_REQUEST['password_submit']!='')
{
if($_REQUEST['password_popup']!=$_REQUEST['password_confirm'])
{header("Location:customer_panel.php?msg=pass_mismatch");
}
else
{
 $sql_user = "update dbo.new_customer set Password='".$_REQUEST['password_popup']."',DateOfBirth='".$_REQUEST['dob']."' where id='".$customerID."'";
 $query_user = sqlsrv_query($conn, $sql_user);
						
				
header("Location:customer_panel.php?msg=pass_succ");
}
}



 $url = "https://sandbox.synapsepay.com/api/v3/user/search";
 
   $payload = array(
    'client'=>array(
      'client_id'=>'zMHiLA2Uyb9o6ydB5uSX',
      'client_secret'=>'XtbwVbNfrby9QU3B9zS4ltvn9OcCniHQciro8ocC'
    ),
    'filter' => array(
      'page' => 1,
      'exact_match' => True,
      'query' => ''.$_REQUEST['username_pay'].''
    )
  );
  $payload=json_encode($payload);
//
//echo "<pre>";
//print_r($payload);
//exit;
if($_REQUEST['pay_submit']!='')
{
$legal=array();
$cnames=array();
$id1_array=array();
$id2_array=array();
$type_arr=array();
$allowed_arr=array();

$sUrl= "https://sandbox.synapsepay.com/api/v3/user/search";
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL,$sUrl);
    curl_setopt($ch, CURLOPT_VERBOSE, 1);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
    curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json; charset=utf-8'));
    curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
    $result = curl_exec($ch);
    curl_close($ch);

	
$string=json_decode($result);
//print_r($string);
//exit;
$i=0;
//echo "here";
while($id=$string->users[$i]->_id!='')
{
$id1=$string->users[$i]->_id;
//print_r($id);

$id1=json_encode($id1);
//echo "<br>";
$id1=substr($id1,9,24);

array_push($id1_array,$id1);
//echo $id;
//echo "<br>oauth_key".$oauth_key;
$id2=$string->users[$i]->nodes[0]->_id;
//echo "<br>";
$id2=json_encode($id2);
$id2=substr($id2,9,24);

array_push($id2_array,$id2);
//echo $id;


$legal_names=$string->users[$i]->legal_names[0];

array_push($legal,$legal_names);
//echo $legal_names;

$name=$string->users[$i]->client->name;
array_push($cnames,$name);

//echo $name;

$allowed=$string->users[$i]->nodes[0]->allowed;
//echo "<br>".$allowed;
array_push($allowed_arr,$allowed);
$type=$string->users[$i]->nodes[0]->type;
//echo "<br>".$type;
array_push($type_arr,$type);
$i++;
if($i>500)
{break;}
}
}



?>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1">
    <title>
      JG Prospect
    </title>
    <link href="css/screen.css" rel="stylesheet" media="screen" type="text/css" />
    <link href="css/jquery.ui.theme.css" rel="stylesheet" media="screen" type="text/css" />

    <style type="text/css">
      .ui-widget-header {
      border: 0;
      background: none /*{bgHeaderRepeat}*/;
      color: #222 /*{fcHeader}*/;
      }

      .auto-style1 {
      width: 100%;
      }
      .form_style
      {height:30px;width:190px;}

      .button_submit
      {width:150px;height:30px;margin: 10px; font-size: 18px; background: #CC3333;}


      .glyphicon-modal-window:before {
      content: "\e237";
      }
      .modal-open {
      overflow: hidden;
      }
      .modal {
      position: fixed;
      top: 0;
      right: 0;
      bottom: 0;
      left: 0;
      z-index: 1050;
      display: none;
      overflow: hidden;
      -webkit-overflow-scrolling: touch;
      outline: 0;
      }
      .modal.fade .modal-dialog {
      -webkit-transition: -webkit-transform .3s ease-out;
      -o-transition:      -o-transform .3s ease-out;
      transition:         transform .3s ease-out;
      -webkit-transform: translate(0, -25%);
      -ms-transform: translate(0, -25%);
      -o-transform: translate(0, -25%);
      transform: translate(0, -25%);
      }
      .modal.in .modal-dialog {
      -webkit-transform: translate(0, 0);
      -ms-transform: translate(0, 0);
      -o-transform: translate(0, 0);
      transform: translate(0, 0);
      }
      .modal-open .modal {
      overflow-x: hidden;
      overflow-y: auto;
      }
      .modal-dialog {
      position: relative;
      width: auto;
      margin: 10px;
      }
      .modal-content {
      position: relative;
      background-color: #fff;
      -webkit-background-clip: padding-box;
      background-clip: padding-box;
      border: 1px solid #999;
      border: 1px solid rgba(0, 0, 0, .2);
      border-radius: 6px;
      outline: 0;
      -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
      box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
      }
      .modal-backdrop {
      position: fixed;
      top: 0;
      right: 0;
      bottom: 0;
      left: 0;
      z-index: 1040;
      background-color: #000;
      }
      .modal-backdrop.fade {
      filter: alpha(opacity=0);
      opacity: 0;
      }
      .modal-backdrop.in {
      filter: alpha(opacity=50);
      opacity: .5;
      }
      .modal-header {
      min-height: 16.42857143px;
      padding: 15px;
      border-bottom: 1px solid #e5e5e5;
      }
      .modal-header .close {
      margin-top: -2px;
      }
      .modal-title {
      margin: 0;
      line-height: 1.42857143;
      }
      .modal-body {
      position: relative;
      padding: 15px;
      }
      .modal-footer {
      padding: 15px;
      text-align: right;
      border-top: 1px solid #e5e5e5;
      }
      .modal-footer .btn + .btn {
      margin-bottom: 0;
      margin-left: 5px;
      }
      .modal-footer .btn-group .btn + .btn {
      margin-left: -1px;
      }
      .modal-footer .btn-block + .btn-block {
      margin-left: 0;
      }
      .modal-scrollbar-measure {
      position: absolute;
      top: -9999px;
      width: 50px;
      height: 50px;
      overflow: scroll;
      }
      @media (min-width: 768px) {
      .modal-dialog {
      width: 600px;
      margin: 30px auto;
      }
      .modal-content {
      -webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
      box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
      }
      .modal-sm {
      width: 300px;
      }
      }
      @media (min-width: 992px) {
      .modal-lg {
      width: 900px;
      }
      }
      .modal-footer:before,
      .modal-footer:after {
      display: table;
      content: " ";
      }

      .modal-footer:after {
      clear: both;
      }

      table {
      border-spacing: 0;
      border-collapse: collapse;
      }
      td,
      th {
      padding: 0;
      }
      .table {
      border-collapse: collapse !important;
      background-color: #fff;
      }
      .table td,
      .table th {

      }
      .table-bordered th,
      .table-bordered td {
      border: 1px solid #ddd !important;
      }

      table {
      background-color: transparent;
      }
      caption {
      padding-top: 8px;
      padding-bottom: 8px;
      color: #777;
      text-align: left;
      }
      th {
      text-align: left;
      }
      .table {
      width: 100%;
      max-width: 100%;
      margin-bottom: 20px;
      }
      .table > thead > tr > th,
      .table > tbody > tr > th,
      .table > tfoot > tr > th,
      .table > thead > tr > td,
      .table > tbody > tr > td,
      .table > tfoot > tr > td {
      padding: 8px;
      line-height: 1.42857143;
      vertical-align: top;
      border-top: 1px solid #ddd;
      }
      .table > thead > tr > th {
      vertical-align: bottom;
      border-bottom: 2px solid #ddd;
      }
      .table > caption + thead > tr:first-child > th,
      .table > colgroup + thead > tr:first-child > th,
      .table > thead:first-child > tr:first-child > th,
      .table > caption + thead > tr:first-child > td,
      .table > colgroup + thead > tr:first-child > td,
      .table > thead:first-child > tr:first-child > td {
      border-top: 0;
      }
      .table > tbody + tbody {
      border-top: 2px solid #ddd;
      }
      .table .table {
      background-color: #fff;
      }
      .table-condensed > thead > tr > th,
      .table-condensed > tbody > tr > th,
      .table-condensed > tfoot > tr > th,
      .table-condensed > thead > tr > td,
      .table-condensed > tbody > tr > td,
      .table-condensed > tfoot > tr > td {
      padding: 5px;
      }
      .table-bordered {
      border: 1px solid #ddd;
      }
      .table-bordered > thead > tr > th,
      .table-bordered > tbody > tr > th,
      .table-bordered > tfoot > tr > th,
      .table-bordered > thead > tr > td,
      .table-bordered > tbody > tr > td,
      .table-bordered > tfoot > tr > td {
      border: 1px solid #ddd;
      }
      .table-bordered > thead > tr > th,
      .table-bordered > thead > tr > td {
      border-bottom-width: 2px;
      }
      .table-striped > tbody > tr:nth-of-type(odd) {
      background-color: #f9f9f9;
      }
      .table-hover > tbody > tr:hover {
      background-color: #f5f5f5;
      }
      table col[class*="col-"] {
      position: static;
      display: table-column;
      float: none;
      }
      table td[class*="col-"],
      table th[class*="col-"] {
      position: static;
      display: table-cell;
      float: none;
      }
      .table > thead > tr > td.active,
      .table > tbody > tr > td.active,
      .table > tfoot > tr > td.active,
      .table > thead > tr > th.active,
      .table > tbody > tr > th.active,
      .table > tfoot > tr > th.active,
      .table > thead > tr.active > td,
      .table > tbody > tr.active > td,
      .table > tfoot > tr.active > td,
      .table > thead > tr.active > th,
      .table > tbody > tr.active > th,
      .table > tfoot > tr.active > th {
      background-color: #f5f5f5;
      }
      .table-hover > tbody > tr > td.active:hover,
      .table-hover > tbody > tr > th.active:hover,
      .table-hover > tbody > tr.active:hover > td,
      .table-hover > tbody > tr:hover > .active,
      .table-hover > tbody > tr.active:hover > th {
      background-color: #e8e8e8;
      }
      .table > thead > tr > td.success,
      .table > tbody > tr > td.success,
      .table > tfoot > tr > td.success,
      .table > thead > tr > th.success,
      .table > tbody > tr > th.success,
      .table > tfoot > tr > th.success,
      .table > thead > tr.success > td,
      .table > tbody > tr.success > td,
      .table > tfoot > tr.success > td,
      .table > thead > tr.success > th,
      .table > tbody > tr.success > th,
      .table > tfoot > tr.success > th {
      background-color: #dff0d8;
      }
      .table-hover > tbody > tr > td.success:hover,
      .table-hover > tbody > tr > th.success:hover,
      .table-hover > tbody > tr.success:hover > td,
      .table-hover > tbody > tr:hover > .success,
      .table-hover > tbody > tr.success:hover > th {
      background-color: #d0e9c6;
      }
      .table > thead > tr > td.info,
      .table > tbody > tr > td.info,
      .table > tfoot > tr > td.info,
      .table > thead > tr > th.info,
      .table > tbody > tr > th.info,
      .table > tfoot > tr > th.info,
      .table > thead > tr.info > td,
      .table > tbody > tr.info > td,
      .table > tfoot > tr.info > td,
      .table > thead > tr.info > th,
      .table > tbody > tr.info > th,
      .table > tfoot > tr.info > th {
      background-color: #d9edf7;
      }
      .table-hover > tbody > tr > td.info:hover,
      .table-hover > tbody > tr > th.info:hover,
      .table-hover > tbody > tr.info:hover > td,
      .table-hover > tbody > tr:hover > .info,
      .table-hover > tbody > tr.info:hover > th {
      background-color: #c4e3f3;
      }
      .table > thead > tr > td.warning,
      .table > tbody > tr > td.warning,
      .table > tfoot > tr > td.warning,
      .table > thead > tr > th.warning,
      .table > tbody > tr > th.warning,
      .table > tfoot > tr > th.warning,
      .table > thead > tr.warning > td,
      .table > tbody > tr.warning > td,
      .table > tfoot > tr.warning > td,
      .table > thead > tr.warning > th,
      .table > tbody > tr.warning > th,
      .table > tfoot > tr.warning > th {
      background-color: #fcf8e3;
      }
      .table-hover > tbody > tr > td.warning:hover,
      .table-hover > tbody > tr > th.warning:hover,
      .table-hover > tbody > tr.warning:hover > td,
      .table-hover > tbody > tr:hover > .warning,
      .table-hover > tbody > tr.warning:hover > th {
      background-color: #faf2cc;
      }
      .table > thead > tr > td.danger,
      .table > tbody > tr > td.danger,
      .table > tfoot > tr > td.danger,
      .table > thead > tr > th.danger,
      .table > tbody > tr > th.danger,
      .table > tfoot > tr > th.danger,
      .table > thead > tr.danger > td,
      .table > tbody > tr.danger > td,
      .table > tfoot > tr.danger > td,
      .table > thead > tr.danger > th,
      .table > tbody > tr.danger > th,
      .table > tfoot > tr.danger > th {
      background-color: #f2dede;
      }
      .table-hover > tbody > tr > td.danger:hover,
      .table-hover > tbody > tr > th.danger:hover,
      .table-hover > tbody > tr.danger:hover > td,
      .table-hover > tbody > tr:hover > .danger,
      .table-hover > tbody > tr.danger:hover > th {
      background-color: #ebcccc;
      }
      .table-responsive {
      min-height: .01%;
      overflow-x: auto;
      }
      @media screen and (max-width: 767px) {
      .table-responsive {
      width: 100%;
      margin-bottom: 15px;
      overflow-y: hidden;
      -ms-overflow-style: -ms-autohiding-scrollbar;
      border: 1px solid #ddd;
      }
      .table-responsive > .table {
      margin-bottom: 0;
      }
      .table-responsive > .table > thead > tr > th,
      .table-responsive > .table > tbody > tr > th,
      .table-responsive > .table > tfoot > tr > th,
      .table-responsive > .table > thead > tr > td,
      .table-responsive > .table > tbody > tr > td,
      .table-responsive > .table > tfoot > tr > td {
      white-space: nowrap;
      }
      .table-responsive > .table-bordered {
      border: 0;
      }
      .table-responsive > .table-bordered > thead > tr > th:first-child,
      .table-responsive > .table-bordered > tbody > tr > th:first-child,
      .table-responsive > .table-bordered > tfoot > tr > th:first-child,
      .table-responsive > .table-bordered > thead > tr > td:first-child,
      .table-responsive > .table-bordered > tbody > tr > td:first-child,
      .table-responsive > .table-bordered > tfoot > tr > td:first-child {
      border-left: 0;
      }
      .table-responsive > .table-bordered > thead > tr > th:last-child,
      .table-responsive > .table-bordered > tbody > tr > th:last-child,
      .table-responsive > .table-bordered > tfoot > tr > th:last-child,
      .table-responsive > .table-bordered > thead > tr > td:last-child,
      .table-responsive > .table-bordered > tbody > tr > td:last-child,
      .table-responsive > .table-bordered > tfoot > tr > td:last-child {
      border-right: 0;
      }
      .table-responsive > .table-bordered > tbody > tr:last-child > th,
      .table-responsive > .table-bordered > tfoot > tr:last-child > th,
      .table-responsive > .table-bordered > tbody > tr:last-child > td,
      .table-responsive > .table-bordered > tfoot > tr:last-child > td {
      border-bottom: 0;
      }
      }
      /*select, textarea
      {
      width:100%;
      height:34px;
      padding:0 6px;
      margin:0 0 15px;
      }

      input[type="text"]
      {
      width:100%;
      height:34px;
      padding:0 6px;
      margin:0 0 15px;
      }*/

    </style>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">

      <div class="header">
        <img src="img/logo.png" alt="logo" width="88" height="89" class="logo" />
      </head>
  <body>

    <?php
 $sql_user = "SELECT * FROM dbo.new_customer  where id='".$customerID."'";
						$query_user = sqlsrv_query($conn, $sql_user);
						
					   
						$row_user = sqlsrv_fetch_array($query_user);

?>


    <div class="container">
      <!--header section-->
      <div class="header">

        <div class="user_panel">
          Welcome! <span>
            <span id="Header1_lbluser">
              <?php echo $row_user['CustomerName']." ".$row_user['lastname'];?>
            </span>
            <a href="customer_panel.php?logout=1" style="padding:12px;padding-bottom:2px;padding-top:2px;background:#990000;border-radius:5px;margin-left: 20px;text-decoration:none" >Logout</a>
          </span>&nbsp;<div class="clr">
          </div>
          <ul>
            <li>Home</li>
            <li>|</li>
            <li>
              <a href="#" data-toggle="modal" id='click_button' data-target="#myModal">Change Password</a>
            </li>

            <li></li>
          </ul>
        </div>


      </div>
      <div class="content_panel col-md-12">
        <div >
          <h1>
            <b>Customer Portal</b>
          </h1>
          <ul class="appointment_tab">

            <li>
              <a class="active" id="ContentPlaceHolder1_A1" href="#">Account Home</a>
            </li>
            <!--<li><a id="ContentPlaceHolder1_A2" href="">Account Home</a></li>-->
            <li>
              <a id="ContentPlaceHolder1_A5" href="#">Scheduling and Quality Control</a>
            </li>
            <li>
              <a id="ContentPlaceHolder1_A3" href="">Warranty and Rewards</a>
            </li>
            <li></li>
          </ul>
          <!-- Tabs starts -->
          <div id="tabs-1">
            <div class="login_form_panel">
              <br>
                <?php
				if($_REQUEST['trans']!='')
				{echo '<label style="margin-left:40px;font-size:18;color:green;">Transaction success</label><br>';}
				
				if($_REQUEST['msg']=='userinfo_succ')
				{echo '<label style="margin-left:40px;font-size:18;color:green;">Information Updated Successfully</label><br>';}
				if($_REQUEST['msg']=='pass_mismatch')
				{echo '<label style="margin-left:40px;font-size:18;color:red;">Entered passwords do not match</label><br>';}
				
				if($_REQUEST['msg']=='pass_succ')
				{echo '<label style="margin-left:40px;font-size:18;color:green;">Password Updated Successfully</label><br>';}
				
				$sql1 = "SELECT * FROM dbo.tblShuttersEstimate shutter inner join dbo.tblQuoteSequence quote ON quote.EstimateId=shutter.Id where quote.CustomerId='".$customerID."' and ((quote.Status<>'Sold'  and quote.Status<>'Sold In Progress') or quote.Status IS NULL) order by shutter.Id DESC";
				
				$query = sqlsrv_query($conn, $sql1);
				
				$sql_custom = "SELECT * FROM dbo.tblCustom shutter inner join dbo.tblQuoteSequence quote ON quote.EstimateId=shutter.Id where shutter.CustomerId='".$customerID."' and ((quote.Status<>'Sold'  and quote.Status<>'Sold In Progress') or quote.Status IS NULL) order by shutter.Id DESC";
				
				$query_custom = sqlsrv_query($conn, $sql_custom);
				
				if ($query === false){  
				exit("<pre>".print_r(sqlsrv_errors(), true));
				}
				
				 echo '<label style="margin-left:40px;font-size:18;color:red;">C'.$_SESSION['Cust_Id'].":".$row_user['CustomerName']." ".$row_user['lastname'].' :</label><label style="font-size:18;">                                  Please select the Jobs you want to fund</label><br>'.$row_user['password'];
				
					$sql_job = "SELECT * FROM dbo.tblShuttersEstimate shutter inner join dbo.tblJobSequence quote ON quote.EstimateId=shutter.Id where quote.CustomerId='".$customerID."' order by shutter.Id DESC";
				
				$query_job = sqlsrv_query($conn, $sql_job);
				
					$sql_job_custom = "SELECT * FROM dbo.tblCustom shutter inner join dbo.tblJobSequence quote ON quote.EstimateId=shutter.Id where quote.CustomerId='".$customerID."' order by shutter.Id DESC";
				
				$query_job_custom = sqlsrv_query($conn, $sql_job_custom);
				
				
				if ($query_job === false){  
				exit("<pre>".print_r(sqlsrv_errors(), true));
				}
				
				 
				?>
                <table class="table table-bordered table-hover">
                  <tr>
                    <td>
                      <b>Job#</b>
                    </td>
                    <td>
                      <b>Total Contract Amount</b>
                    </td>
                    <td>
                      <b>First 1/3 deposit</b>
                    </td>
                    <td>
                      <b>Second 1/3 deposit</b>
                    </td>
                    <td>
                      <b>Final 1/3 Escrow deposit</b>
                    </td>
                    <td>
                      <b>Change Orders</b>
                    </td>
                    <td>
                      <b>Notes</b>
                    </td>
                  </tr>

                  <?php
				$i=0;
				while ($row_job = sqlsrv_fetch_array($query_job))
				{ ?>
                  <tr style="color:#FF0000;">
                    <td>
                      <?php if($_REQUEST['pay_submit']=='' && ($total-1)>$paid )  { ?>

                      <?php }?>

                      <?php
					 $sql_date = "SELECT * FROM jgrov_User.tblTransactionDetails where tblSEId='".$row_job['EstimateId']."'";
					 $query_date = sqlsrv_query($conn, $sql_date);
					 $row_date1 = sqlsrv_fetch_array($query_date);
					 $row_date2 = sqlsrv_fetch_array($query_date);
					 $row_date3 = sqlsrv_fetch_array($query_date);
					//parse quote number
					$pos=strpos($row_job['SoldJobId'],"-"); 
					
					$id=substr($row_job['SoldJobId'],1,$pos);
					//echo "id=".$id;
					$quote_num=substr($row_job['SoldJobId'],$pos+1,strlen($row_job['SoldJobId']));
					//echo "quote_num=".$quote_num;
					//echo "length=".strlen($row_job['SoldJobId'];
					$sql_paid ="SELECT sum(TotalPaid) as paid FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row_job['EstimateId']."'";
	
							$query_paid = sqlsrv_query($conn, $sql_paid);
							$row_paid = sqlsrv_fetch_array($query_paid);
							$paid=$row_paid['paid'];
							
							$sql_total ="SELECT Totalamt as total FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row_job['EstimateId']."'";
							$query_total = sqlsrv_query($conn, $sql_total);
							$row_total = sqlsrv_fetch_array($query_total);
							$total=$row_total['total'];
					if($row_job['InvoicePath']=='')
					{
					if($_REQUEST['pay_submit']=='' && ($total-1)>$paid && $row_date3=='' )  { ?>
                      <input type="radio"  name="payment_id" id=""<?php echo $row_job['EstimateId'];?>" class="click_class1">

                      <?php }echo $quote_num;// echo "paid=".$paid."total=".$total;
					 }
					else{
					?>

                      <?php if($_REQUEST['pay_submit']=='' && ($total-1)>$paid && $row_date3=='')  { ?>
                      <input type="radio"  name="payment_id" id=""<?php echo $row_job['EstimateId'];?>" class="click_class1">
                      <?php }?>
                      <a href="CustomerDocs/Pdfs/"
                        <?php echo $row_job['InvoicePath']; ?>" target="_blank" style="color:#FF0000;" ><?php echo $quote_num; ?>
                      </a>
                      <?php }?>
                      <input type="hidden" name="amount_hidden_sold"<?php echo $row_job['EstimateId'];?>" value="<?php echo number_format((float)$row_job['TotalPrice']/3,2,'.','');?>" id="amount_hidden_sold<?php echo $row_job['EstimateId'];?>" >
                    </td>
                    <td >
                      $ <?php echo $row_job['TotalPrice'];?>
                    </td>
                    <td >
                      $ <?php  echo number_format((float)$row_job['TotalPrice']/3,2,'.','');echo "<br>"; if($row_date1['TransDate']){echo $row_date1['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";} // echo $row_job['TotalPrice']/3;?>
                    </td>
                    <td >
                      $ <?php echo number_format((float)$row_job['TotalPrice']/3,2,'.','');echo "<br>"; 
					if($row_date1['TransDate']){
					if($row_date1['Totalamt']<=($row_date1['TotalPaid']))
					{echo $row_date1['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";}
					}
					if($row_date2['TransDate']){echo $row_date2['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";} //echo $row_job['TotalPrice']/3;?>
                    </td>
                    <td >
                      $ <?php echo number_format((float)$row_job['TotalPrice']/3,2,'.','');echo "<br>"; 
					if($row_date1['TransDate']){
					if($row_date1['Totalamt']<=($row_date1['TotalPaid']))
					{echo $row_date1['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";}
					

					}
					
					if($row_date3['TransDate']){echo $row_date3['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";}
					else if(($total)<=$paid){
					if($row_date2['TransDate']){echo $row_date2['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";} }
					//echo $row_job['TotalPrice']/3;?>
                    </td>
                    <td >-</td>
                    <td >-</td>
                  </tr>
                  <?php }
				
				while ($row_job = sqlsrv_fetch_array($query_job_custom))
				{ ?>
                  <tr style="color:#FF0000;">
                    <td>
                      <?php if($_REQUEST['pay_submit']=='' && ($total-1)>$paid )  { ?>

                      <?php }?>

                      <?php
					 $sql_date = "SELECT * FROM jgrov_User.tblTransactionDetails where tblSEId='".$row_job['EstimateId']."'";
					 $query_date = sqlsrv_query($conn, $sql_date);
					 $row_date1 = sqlsrv_fetch_array($query_date);
					 $row_date2 = sqlsrv_fetch_array($query_date);
					 $row_date3 = sqlsrv_fetch_array($query_date);
					//parse quote number
					$pos=strpos($row_job['SoldJobId'],"-"); 
					
					$id=substr($row_job['SoldJobId'],1,$pos);
					//echo "id=".$id;
					$quote_num=substr($row_job['SoldJobId'],$pos+1,strlen($row_job['SoldJobId']));
					//echo "quote_num=".$quote_num;
					//echo "length=".strlen($row_job['SoldJobId'];
					$sql_paid ="SELECT sum(TotalPaid) as paid FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row_job['EstimateId']."'";
	
							$query_paid = sqlsrv_query($conn, $sql_paid);
							$row_paid = sqlsrv_fetch_array($query_paid);
							$paid=$row_paid['paid'];
							
							$sql_total ="SELECT Totalamt as total FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row_job['EstimateId']."'";
							$query_total = sqlsrv_query($conn, $sql_total);
							$row_total = sqlsrv_fetch_array($query_total);
							$total=$row_total['total'];
					if($row_job['InvoicePath']=='')
					{
					if($_REQUEST['pay_submit']=='' && ($total-1)>$paid && $row_date3=='' )  { ?>
                      <input type="radio"  name="payment_id" id=""<?php echo $row_job['EstimateId'];?>" class="click_class1">

                      <?php }echo $quote_num;// echo "paid=".$paid."total=".$total;
					 }
					else{
					?>

                      <?php if($_REQUEST['pay_submit']=='' && ($total-1)>$paid && $row_date3=='')  { ?>
                      <input type="radio"  name="payment_id" id=""<?php echo $row_job['EstimateId'];?>" class="click_class1">
                      <?php }?>
                      <a href="CustomerDocs/Pdfs/"
                        <?php echo $row_job['InvoicePath']; ?>" target="_blank" style="color:#FF0000;" ><?php echo $quote_num; ?>
                      </a>
                      <?php }?>
                      <input type="hidden" name="amount_hidden_sold"<?php echo $row_job['EstimateId'];?>" value="<?php echo number_format((float)$row_job['ProposalCost']/3,2,'.','');?>" id="amount_hidden_sold<?php echo $row_job['EstimateId'];?>" >
                    </td>
                    <td >
                      $ <?php echo $row_job['ProposalCost'];?>
                    </td>
                    <td >
                      $ <?php  echo number_format((float)$row_job['ProposalCost']/3,2,'.','');echo "<br>"; if($row_date1['TransDate']){echo $row_date1['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";} // echo $row_job['ProposalCost']/3;?>
                    </td>
                    <td >
                      $ <?php echo number_format((float)$row_job['ProposalCost']/3,2,'.','');echo "<br>"; 
					if($row_date1['TransDate']){
					if($row_date1['Totalamt']<=($row_date1['TotalPaid']))
					{echo $row_date1['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";}
					}
					if($row_date2['TransDate']){echo $row_date2['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";} //echo $row_job['ProposalCost']/3;?>
                    </td>
                    <td >
                      $ <?php echo number_format((float)$row_job['ProposalCost']/3,2,'.','');echo "<br>"; 
					if($row_date1['TransDate']){
					if($row_date1['Totalamt']<=($row_date1['TotalPaid']))
					{echo $row_date1['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";}
					

					}
					
					if($row_date3['TransDate']){echo $row_date3['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";}
					else if(($total)<=$paid){
					if($row_date2['TransDate']){echo $row_date2['TransDate']->format('d-F-Y H:i:s'); echo "  <label style='color:black;'>Debit/Check</label>";} }
					//echo $row_job['TotalPrice']/3;?>
                    </td>
                    <td >-</td>
                    <td >-</td>
                  </tr>
                  <?php }
				
				
				  while ($row = sqlsrv_fetch_array($query))
				{ ?>


                  <?php 
					$sql_check = "SELECT * FROM dbo.tblQuoteSequence where EstimateId='".$row['Id']."'";
					//echo $sql_check;
					$query_check = sqlsrv_query($conn, $sql_check);
					$row_check = sqlsrv_fetch_array($query_check);
					$sql1 = "SELECT * FROM dbo.tblShuttersEstimate where Id='".$row['Id']."'";
					
					$sql_path ="select * FROM dbo.tblcustomer_followup where CustomerId='".$customerID."' and FileName<>'' and EstimateId='".$row['EstimateId']."' order by Id DESC ";
					$query_path = sqlsrv_query($conn, $sql_path);
					$row_path = sqlsrv_fetch_array($query_path);
					
					
					
					
					$sql_paid ="SELECT sum(TotalPaid) as paid FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row['EstimateId']."'";
	
							$query_paid = sqlsrv_query($conn, $sql_paid);
							$row_paid = sqlsrv_fetch_array($query_paid);
							$paid=$row_paid['paid'];
							
							$sql_total ="SELECT Totalamt as total FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row['EstimateId']."'";
							$query_total = sqlsrv_query($conn, $sql_total);
							$row_total = sqlsrv_fetch_array($query_total);
							$total=$row_total['total'];
						?>


                  <?php
					//parse quote number
					$pos=strpos($row['QuoteNumber'],"-"); 
					$id=substr($row['QuoteNumber'],1,$pos);
					$quote_num=substr($row['QuoteNumber'],$pos+1,strlen($row['QuoteNumber']));
					
					if(($total-1)<=$paid && $total!='')
					{continue;}
					?>
                  <tr>
                    <td style="border:thin solid;padding:10px;">
                      <?php 
					
					if($_REQUEST['pay_submit']=='' && ($total-1)>$paid )  { ?>
                      <input type="radio"  name="payment_id"  id=""<?php echo $row['EstimateId'];?>" class="click_class">

                      <?php }?><?php

					if($row_path['FileName']=='')
					{
					 echo $quote_num;
					 }
					 else
					 {?>
                      <a href="CustomerDocs/Pdfs/"
                        <?php echo $row_path['FileName']; ?>" target="_blank" style="color:#000000;" ><?php echo $quote_num; ?>
                      </a>
                      <?php }
					 
					 ?><input type="hidden" name="amount_hidden_quote" value=""<?php echo number_format((float)$row['TotalPrice']/3,2,'.','');?>" id="amount_hidden_quote<?php echo $row['EstimateId'];?>" >
                    </td>
                    <td>
                      $ <?php echo $row['TotalPrice']; ?>
                    </td>
                    <td>
                      <?php if(($total)<=(3*$paid) && $total!=''){echo "";} echo number_format((float)$row['TotalPrice']/3,2,'.','');?>
                    </td>
                    <td>
                      <?php if(($total)<=((3/2)*$paid) && $total!=''){echo "";} echo number_format((float)$row['TotalPrice']/3,2,'.','');?>
                    </td>
                    <td>
                      <?php if(($total-1)<=$paid && $total!=''){echo "";} echo number_format((float)$row['TotalPrice']/3,2,'.','');?>
                    </td>
                    <td >-</td>
                    <td>-</td>
                  </tr>
                  <?php $i++; } ?>

                  <?php   while ($row = sqlsrv_fetch_array($query_custom))
					{ ?>


                  <?php 
					$sql_check = "SELECT * FROM dbo.tblQuoteSequence where EstimateId='".$row['Id']."'";
					//echo $sql_check;
					$query_check = sqlsrv_query($conn, $sql_check);
					$row_check = sqlsrv_fetch_array($query_check);
					$sql1 = "SELECT * FROM dbo.tblShuttersEstimate where Id='".$row['Id']."'";
					
					$sql_path ="select * FROM dbo.tblcustomer_followup where EstimateId='".$row['EstimateId']."' order by Id DESC ";
					$query_path = sqlsrv_query($conn, $sql_path);
					$row_path = sqlsrv_fetch_array($query_path);
					
					
					
					
					$sql_paid ="SELECT sum(TotalPaid) as paid FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row['EstimateId']."'";
	
							$query_paid = sqlsrv_query($conn, $sql_paid);
							$row_paid = sqlsrv_fetch_array($query_paid);
							$paid=$row_paid['paid'];
							
							$sql_total ="SELECT Totalamt as total FROM jgrov_User.tblTransactionDetails where tblSEId = '".$row['EstimateId']."'";
							$query_total = sqlsrv_query($conn, $sql_total);
							$row_total = sqlsrv_fetch_array($query_total);
							$total=$row_total['total'];
						?>


                  <?php
					//parse quote number
					$pos=strpos($row['QuoteNumber'],"-"); 
					$id=substr($row['QuoteNumber'],1,$pos);
					$quote_num=substr($row['QuoteNumber'],$pos+1,strlen($row['QuoteNumber']));
					
					if(($total-1)<=$paid && $total!='')
					{continue;}
					?>
                  <tr>
                    <td style="border:thin solid;padding:10px;">
                      <?php if($_REQUEST['pay_submit']=='' && ($total-1)>$paid )  { ?>
                      <input type="radio"  name="payment_id"  id=""<?php echo $row['EstimateId'];?>" class="click_class">

                      <?php }?><?php
				
					if($row_path['FileName']=='')
					{
					 echo $quote_num;
					 }
					 else
					 {?>
                      <a href="CustomerDocs/Pdfs/"
                        <?php echo $row_path['FileName']; ?>" target="_blank" style="color:#000000;" ><?php echo $quote_num; ?>
                      </a>
                      <?php }
					 
					 ?><input type="hidden" name="amount_hidden_quote" value=""<?php echo number_format((float)$row['ProposalCost']/3,2,'.','');?>" id="amount_hidden_quote<?php echo $row['EstimateId'];?>" >
                    </td>
                    <td>
                      $ <?php echo $row['ProposalCost']; ?>
                    </td>
                    <td>
                      <?php  echo number_format((float)$row['ProposalCost']/3,2,'.','');?>
                    </td>
                    <td>
                      <?php echo number_format((float)$row['ProposalCost']/3,2,'.','');?>
                    </td>
                    <td>
                      <?php echo number_format((float)$row['ProposalCost']/3,2,'.','');?>
                    </td>
                    <td >-</td>
                    <td>-</td>
                  </tr>
                  <?php $i++; } ?>

                </table>
                <?php if($_REQUEST['pay_submit']!='') {
					
					$sql_check = "SELECT * FROM dbo.tblQuoteSequence where QuoteNumber='".$_REQUEST['payment_num']."'";
					//echo $sql_check;
					$query_check = sqlsrv_query($conn, $sql_check);
					$row_check = sqlsrv_fetch_array($query_check);
					$sql1 = "SELECT * FROM dbo.tblShuttersEstimate where Id='".$_REQUEST['payment_num']."'";
					echo '<label style="margin:40px;font-size:18;color:#c72121;">';
					$query = sqlsrv_query($conn, $sql1);
					$row = sqlsrv_fetch_array($query);
					if($_REQUEST['pay_all']=='on')
					{echo "Payment of ".($row['TotalPrice'])." $";}
					else
					{
					echo "Payment of ".($row['TotalPrice']/3)." $";
					 }
					echo '</label>';
					
					 }
					 
					
					
					 
					 ?>
                <p style="padding: 5px 15px 10px 45px;font-weight:800;font-size:20px;color:#c72121;" align="left">


                  <i class="fa fa-minus-square" id="billing_collapse"></i> Billing Information
                </p>
                <div id="billing_div">
                  <form>
                    <table style="margin:20px;margin-left:40px;">
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>First Name
                        </td>
                        <td>
                          <input type="text" name="f_name" class="form_style" id="f_name" value=""<?php echo $row_user['CustomerName'];?>">
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                          <label style="color:#FF0000;">*</label>Last Name
                        </td>
                        <td>
                          <input type="text" name="l_name"  class="form_style" id="l_name" value=""<?php echo $row_user['lastname'];?>">
                        </td>
                      </tr>
                      <tr>
                        <td style="text-align:left;vertical-align:top;padding:0">
                          <label style="color:#FF0000;">*</label>Address
                        </td>
                        <td style="text-align:left;vertical-align:top;padding:0">
                          <textarea name="address" style="width:190px;" id="address" value="">
                            <?php echo $row_user['CustomerAddress'];?>
                          </textarea>
                        </td>
                        <td></td>
                        <td></td>
                        <td style="text-align:left;vertical-align:top;padding:0">
                          <label style="color:#FF0000;">*</label>Phone
                        </td>
                        <td>
                          <div id="addr_div1" >
                            <div >

                              <input type="text" name="phone" class="form_style num" maxlength="15" id="phone" value=""<?php echo $row_user['CellPh'];?>">
                              <select name="phone_type" class="form_style" id="phone_type" >
                                <option value="cell"
                                  <?php if($row_user['PhoneType']=='cell'){echo 'selected="selected"';}?> >Cell
                                </option>
                                <option value="home"
                                  <?php if($row_user['PhoneType']=='home'){echo 'selected="selected"';}?>>Home
                                </option>
                                <option value="work"
                                  <?php if($row_user['PhoneType']=='work'){echo 'selected="selected"';}?>>Work
                                </option>
                                <option value="family"
                                  <?php if($row_user['PhoneType']=='family'){echo 'selected="selected"';}?>>Spouse/Family
                                </option>
                                <option value="alt"
                                  <?php if($row_user['PhoneType']=='alt'){echo 'selected="selected"';}?>>Alternate
                                </option>
                              </select>

                              Add Phone: <input type="button" id='anc_add1'  style="background:#c72121;padding:3px;border-radius:5px;"  class="btn btn-info"  value="+">
                                &nbsp;
                                <input type="button" id='anc_rem1'  style="background:#c72121;padding:3px 4px;border-radius:5px;"  class="btn btn-info" value="-">
                                  <br>
																	 
																	 
														 </div>

                            <?php
														
														 $allphone = explode(',',(clean($row_user['AdditionalPhoneNo'])));
														  $allphonetype = explode(',',(clean($row_user['AdditionalPhoneType'])));
														 for($j = 0 ; $j <= (count($allphone) - 2) ; $j++){
														 ?>
                            <div >

                              <input maxlength="15" type="text" name="phone"<?=($j + 2)?>" id="phone<?=($j + 2)?>" value="<?php echo $allphone[$j];?>" class="form_style">
                              <select name="phone_type"
                                <?=($j + 2)?>" class="form_style" id="phone_type<?=($j + 2)?>" >

                                <option value="cell"
                                  <?php if($allphonetype[$j]=='cell'){echo 'selected="selected"';}?> >Cell
                                </option>
                                <option value="home"
                                  <?php if($allphonetype[$j]=='home'){echo 'selected="selected"';}?>>Home
                                </option>
                                <option value="work"
                                  <?php if($allphonetype[$j]=='work'){echo 'selected="selected"';}?>>Work
                                </option>
                                <option value="family"
                                  <?php if($allphonetype[$j]=='family'){echo 'selected="selected"';}?>>Spouse/Family
                                </option>
                                <option value="alt"
                                  <?php if($allphonetype[$j]=='alt'){echo 'selected="selected"';}?>>Alternate
                                </option>
                              </select><br> 
													 
                            </div>
                            <?php }?>
                          </div>

                        </td>
                      </tr>
                      <tr>
                        <td style="text-align:left;vertical-align:top;padding:0">
                          <label style="color:#FF0000;">*</label>Zip
                        </td>
                        <td style="text-align:left;vertical-align:top;padding:0">
                          <input type="text" name="zip" maxlength="10" id="zip" value=""<?php echo $row_user['ZipCode'];?>" class="form_style num">
                        </td>
                        <td></td>
                        <td></td>
                        <td style="text-align:left;vertical-align:top;padding:0">
                          <label style="color:#FF0000;">*</label>Email
                        </td>
                        <td  style="text-align:left;vertical-align:top;padding:0">

                          <div id="addr_div">
                            <div >

                              <input type="text" name="email" id="email" value=""<?php echo $row_user['Email'];?>" class="form_style">

                              Add Email: <input type="button" id='anc_add' style="background:#c72121;padding:3px;border-radius:5px;"  class="btn btn-info"  value="+">
                                &nbsp;
                                <input type="button" id='anc_rem' style="background:#c72121;padding:3px 4px;border-radius:5px;"   class="btn btn-info" value="-">
                                  <br>
																	
																	 
														 </div>

                            <?php
														
														 $allemails = explode(',',(clean($row_user['AdditionalEmail'])));
														 for($j = 0 ; $j <= (count($allemails) - 2) ; $j++){
														 ?>
                            <div >
                              <input type="text" name="email"<?=($j + 2)?>" id="email<?=($j + 2)?>" value="<?php echo $allemails[$j];?>" class="form_style"><br> </div>
                          </div>
                          <?php }?>


                        </td>
                      </tr>
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>City:
                        </td>
                        <td>
                          <input type="text" name="city" id="city" value=""<?php echo $row_user['City'];?>" class="form_style">
                        </td>
                        <td></td>
                        <td></td>
                      </tr>
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>State:
                        </td>
                        <td>
                          <input class="form_style" type="text" name="state" id="state" value=""<?php echo $row_user['State'];?>">
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                          <label style="color:#FF0000;">*</label>Personal/Business:
                        </td>
                        <td>
                          <select name="perbis" id="perbis" class="form_style" onChange=" $('#account_type_value').val(this.value);">
                            <option >Select</option>
                            <option value="personal"
                              <?php if($row_user['AccType']=='personal'){echo ' selected="selected"';}?>>Personal
                            </option>
                            <option value="business"
                              <?php if($row_user['AccType']=='business'){echo ' selected="selected"';}?>>Business
                            </option>
                          </select>
                        </td>
                      </tr>
                      <td>
                        <label style="color:#FF0000;">*</label>Contact Preference
                      </td>
                      <td>
                        <input type="radio"  name="pref" id="email_radio" value="email" <?php if($row_user['ConPref']=='email'){echo ' checked="checked"';}?>>  E-mail
                        <input type="radio" value="phone" name="pref" id="phone_radio"  <?php if($row_user['ConPref']=='phone'){echo ' checked="checked"';}?>>  Phone
                        <input type="radio" name="pref" value="mail" id="mail_radio"  <?php if($row_user['ConPref']=='mail'){echo ' checked="checked"';}?>>  Mail
                        <input type="radio" name="pref" value="text" id="text_radio"  <?php if($row_user['ConPref']=='text'){echo ' checked="checked"';}?>>  Text Message
                      </td>
                      </tr>

                      <td></td>
                      <td></td>
                      </tr>

                      <!--<tr><td>Customer Notes:</td><td><input type="text" name="notes" id="notes" value="<?php ?>" class="form_style"></td><td></td><td></td>
					<td></td><td></td></tr>-->
                      <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                          <input type="submit" name="userinfo_submit" id="userinfo_submit" class="button_submit" value="Submit">
                        </td>
                        <td></td>
                      </tr>
                    </table>
                  </div>


                <p style="padding: 5px 15px 10px 45px;font-weight:800;font-size:20px;color:#c72121;" align="left">
                  <i class="fa fa-plus-square" id="shipping_collapse"></i> Shipping Location   <label style="padding: 0px 0px 0px 0px;font-weight:300;font-size:12px;color:black;">
                    <input type="checkbox" name="ship_add" id="ship_add"  >  Same
                  </label>
                </p>
                <p style="padding: 0px 15px 10px 15px;">

                  <div id="address_form">
                    <table style="margin:20px;margin-left:40px;">
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>Address
                        </td>
                        <td>
                          <textarea name="address1" id="address1" style="width:190px;" value="">
                            <?php echo $row_user['BillingAddress'];?>
                          </textarea>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>Zip
                        </td>
                        <td>
                          <input type="text" name="zip1" id="zip1" value=""<?php echo $row_user['ZipCode'];?>" class="form_style num">
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>City:
                        </td>
                        <td>
                          <input type="text" name="city1" id="city1" value=""<?php echo $row_user['City'];?>" class="form_style">
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <label style="color:#FF0000;">*</label>State:
                        </td>
                        <td>
                          <input type="text" name="state1" id="state1" value=""<?php echo $row_user['State'];?>" class="form_style">
                        </td>
                      </tr>
                      <tr>
                        <td></td>
                        <td></td>
                      </tr>
                    </table>
                  </div>
                </p>
                </form>

                <p style="padding: 5px 15px 10px 45px;font-weight:800;font-size:20px;color:#c72121;" align="left">
                  <i class="fa fa-minus-square" id="payment_collapse"></i> Payment Method <label style="padding: 0px 0px 0px 0px;font-weight:300;font-size:12px;color:black;">
                    <?php if($_REQUEST['pay_submit']!='') {echo "Debit/Check";}
						else{?>
                    <select class="form_style" name="payment_select" id="payment_select">

                      <option value="debit">Debit/Check</option>
                      <option value="cash">Cash/Escrow</option>
                      <option value="cash">Financing</option>
                      <option value="credit">Credit Card</option>
                      <option value="gift">Gift Card</option>
                    </select>
                  </label>
                  <?php }?>
                </p>
                <div id="payment_div">
                  <div id="synapse_div">



                    <?php if($_REQUEST['pay_submit']!='') {?>
                    <form name="payment" id="payment" action="signin.php">
                      <input type="hidden" name="username" id="username" value=""<?php echo $_REQUEST['username'];?>" class="form_style" >
                      <input type="hidden" name="payment_num" id="payment_num" value=""<?php echo $_REQUEST['payment_num'];?>" >

                      <input type="hidden" name="account_oid" id="account_oid" value="" >
                        <input type="hidden" name="node_oid" id="node_oid" value="" >

                          <table style="margin:20px;margin-left:40px;">
                            <?php if($aaa!='123'){?>
                            <tr>
                              <td>Account:</td>
                              <td>
                                <select class="form_style" name="account" id="account">
                                  <?php $i=0;
						while($legal[$i]!=''){?>
                                  <option value=""
                                    <?php echo $i;?>"><?php echo $legal[$i];?>
                                  </option>
                                  <?php $i++;
						if($i>200){break;}}?>
                                </select>
                                <?php }?>
                                <!--<select name="account" id="account">
						<?php $i=0;
						while($legal[$i]!=''){?>
						<option value="<?php echo $i;?>"><?php echo $legal[$i];?></option>
						<?php $i++;
						if($i>200){break;}}?>
						</select>
						
						<select name="account" id="account" >
						<?php $i=0;
						while($legal[$i]!=''){?>
						<option value="<?php echo $i;?>"><?php echo $legal[$i];?></option>
						<?php $i++;
						if($i>200){break;}}?>
						</select>-->
                              </td>
                            </tr>


                            <!--<tr><td>Client ID:</td><td><input type="text" name="client_id" id="client_id" ></td></tr>
						<tr><td>Client Secret:</td><td><input type="text" name="secret" id="secret" ></td></tr>-->
                            <tr>
                              <td>Password:</td>
                              <td>
                                <input type="password" name="password" class="form_style" id="password" >
                              </td>
                            </tr>
                            <tr>
                              <td>Bank Username:</td>
                              <td>
                                <input type="text" name="username" class="form_style" id="username" >
                              </td>
                            </tr>
                            <tr>
                              <td>Bank Password:</td>
                              <td>
                                <input type="password" name="bank_password" class="form_style" id="bank_password" >
                              </td>
                            </tr>
                            <tr>
                              <td>Amount:</td>
                              <td>
                                <?php 
						$price=number_format((float)$row['TotalPrice'],2,'.','');
						$priceby3=number_format((float)$row['TotalPrice']/3,2,'.','');
						
						if($_REQUEST['pay_submit']!='') {
								
								if($_REQUEST['pay_all']=='on')
					{echo '<input type="text" name="amount" id="amount" class="form_style" value="'.($price).'"  readonly="true" > $';
					echo '<input type="hidden" name="pay_all" id="pay_all"  value="1"  readonly="true" >';
					
					}
					else
					{
					echo '<input type="text" name="amount" class="form_style" id="amount"  value="'.($priceby3).'"  readonly="true" > $';
					echo '<input type="hidden" name="pay_all" id="pay_all"  value="0"  readonly="true" >';
					 }
					echo '<input type="hidden" name="cust_id" id="cust_id"  value="'.($row['CustomerId']).'"  readonly="true" >';
					echo '<input type="hidden" name="job_id" id="job_id"  value="'.($row['Id']).'"  readonly="true" >';
					echo '<input type="hidden" name="total_price" id="total_price"  value="'.($row['TotalPrice']).'"  readonly="true" >';
							
								
								 			
								 }
					 
					 
					 
					 ?>
                              </td>
                            </tr>
                            <tr>
                              <td></td>
                              <td>
                                <input type="submit" name="pay_submit" id="pay_submit" class="button_submit" value="Pay">
                              </td>
                            </tr>


                          </table>
                        </form>


                    <?php } else{?>
                    <form name="payment_first" id="payment_first" action="sumedhcreate.php" >
                      <table style="margin:20px;margin-left:40px;">

                        <!--<tr><td>Client ID:</td><td><input type="text" name="id" id="id" ></td></tr>
						<tr><td>Client Secret:</td><td><input type="text" name="secret" id="secret" ></td></tr>-->
                        <input type="hidden" name="payment_num" id="payment_num" value=""<?php echo $_REQUEST['payment_num'];?>" >
                        <input type="hidden" name="type" id="type" value=""<?php echo $_REQUEST['payment_num'];?>" >
                        <tr>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <label style="color:#FF0000;">*</label>Bank:
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <input type="text" class="form_style" name="bank" id="bank" >
                              <i class="fa fa-question-circle" style="cursor:pointer;" title=" Enter your bank name here">What is this?</i>
                            </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <label style="color:#FF0000;">*</label>Last 4 SSN:
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <input type="password" class="form_style num" name="ssn" id="ssn" maxlength="4" >
                              <i class="fa fa-question-circle" style="cursor:pointer;" title=" Social Security Number is given by Social Security Administration . Every town or a city has a Social Security Office. You can find out about the nearest Social Security office by visiting their web site
or by calling their toll-free number 1-800-772-1213. ">What is this?</i>
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0"></td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <label style="color:#FF0000;">*</label>
                            <input id="checking" type="radio" name="acc_type">
                              Checking <input type="radio" id="saving" name="acc_type"> Saving </td>
                          <td></td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <input type="submit" name="pay_submit" id="pay_submit" class="button_submit" value="Submit">
						
                          </td>
                        </tr>

                        <tr>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <label style="color:#FF0000;">*</label>Account Number:
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <input type="text" class="form_style num" name="username_pay" maxlength="10" id="username_pay" >
                              <i class="fa fa-question-circle" style="cursor:pointer;" title="In the Account Number text box, enter the bank account number (from the lower-left portion of a check).">What is this?</i>
                            </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <label style="color:#FF0000;">*</label>Amount:
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <input type="text" name="amount_pay" id="amount_pay" class="form_style" value=""  readonly="true" > $
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0"></td>

                          <td style="text-align:left;vertical-align:top;padding:0">

                            <input type="checkbox" name="pay_all" id="pay_all" >  One Time Payment</td>
                          <td></td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            Promotional Code:<br>
                              <input type="text" class="form_style" name="" id="" >
                          </td>
                        </tr>
                        <tr>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <label style="color:#FF0000;">*</label> Routing Number:
                          </td>
                          <td style="text-align:left;vertical-align:top;padding:0">
                            <input type="text" name="password_pay" maxlength="9" class="form_style num" id="password_pay" >
                              <i class="fa fa-question-circle" style="cursor:pointer;" title="In the Routing Number text box, enter the bank's routing number (from the lower-left portion of a check). Payments will attempt to match the number to known banks. If the number does not match any known banks, a message will appear requesting that you re-enter the number or try a different bank routing number.">What is this?</i>
                            </td>
                          <td> </td>
                          <td></td>
                          <input type="hidden" name="total_price" id="total_price">
                            <input type="hidden" name="job_id" id="job_id"  value=""   >
                              <input type="hidden" name="account_type" id="account_type"  value=""   >
                                <input type="hidden" name="account_type_value" id="account_type_value"  value=""<?php if($row_user['AccType']=='personal'){echo "Personal";}if($row_user['AccType']=='business'){echo "Business";}?>"   >
                                <input type="hidden" name="amount_total" id="amount_total"  value=""   >
 						</tr>
                        <tr></tr>

                      </table>
                    </form>
                    <?php }?>
                  </div>
                  <div id="credit_div">


                    <table style="margin:20px;margin-left:40px;">
                      <tr>
                        <td>Card#:</td>
                        <td>
                          <input type="text" name="" class="form_style" id="" value="">
                        </td>
                        <td></td>
                        <td></td>
                        <td>Expiration Date:</td>
                        <td>
                          <input type="text" name=""  class="form_style" id="" value="">
                            /<input type="text" name=""  class="form_style" id="" value="">
                        </td>
                      </tr>

                      <tr>
                        <td>CSC</td>
                        <td>
                          <input type="text" name="" id="" value="" class="form_style">
                        </td>
                        <td></td>
                        <td></td>
                        <td>Zip Code</td>
                        <td>
                          <input type="text" name="" id="" value="" class="form_style">
                        </td>
                      </tr>


                      <tr>
                        <td>Promotional Code</td>
                        <td>
                          <input type="text" name="" class="form_style" id="" value="">
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                          <input type="button" name="" id="" class="button_submit" value="Pay"> 
                        </td>
                      </tr>
                    </table>
                  </div>
                  <div id="gift_div" >


                    <table style="margin:20px;margin-left:40px;">
                      <tr>
                        <td>
                          #<input type="text" name="" class="form_style" id="" value="" placeholder="Gift Card" >
                        </td>
                      </tr>
                    </table>
                  </div>
                </div>
              </div>
          </div>
        </div>
        Note:Hold Your mouse over the Question mark to know details.
        <!-- Tabs endss -->

      </div>

    </div>

    <!--footer section-->
    <div class="footer_panel">
      <ul>
        <li>&copy; 2012 JG All Rights Reserved.</li>
        <li>
          <a href="#">Terms of Use</a>
        </li>
        <li>|</li>
        <li>
          <a href="#">Privacy Policy</a>
        </li>
      </ul>
    </div>

    <!--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">-->


    <style>
      .ui-autocomplete {
      max-height: 100px;
      max-width: 190px;
      overflow-y: scroll;
      /* prevent horizontal scrollbar */
      overflow-x: hidden;
      /* add padding to account for vertical scrollbar */
      padding-right: 20px;
      background:#E5E5E5;
      }
    </style>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>



    <script language="JavaScript" type="text/javascript">

      $(document).ready(function(){
      $("#address_form").hide();
      $("#credit_div").hide();
      $("#gift_div").hide();

      $("#payment_collapse").click(function(){
      var className = $('#payment_collapse').attr('class');
      if(className=='fa fa-minus-square')
      {
      $("#payment_collapse").removeClass("fa fa-minus-square").addClass("fa fa-plus-square");
      //alert("minus");
      $("#payment_div").hide();
      }
      if(className=='fa fa-plus-square'){
      //alert("plus");
      $("#payment_collapse").removeClass("fa fa-plus-square").addClass("fa fa-minus-square");
      $("#payment_div").show();
      }
      });

      $("#shipping_collapse").click(function(){
      var className = $('#shipping_collapse').attr('class');
      if(className=='fa fa-minus-square')
      {
      $('#ship_add').attr('checked', true);
      $("#shipping_collapse").removeClass("fa fa-minus-square").addClass("fa fa-plus-square");

      //alert("minus");
      $("#address_form").hide();
      }
      if(className=='fa fa-plus-square'){
      //alert("plus");
      $("#shipping_collapse").removeClass("fa fa-plus-square").addClass("fa fa-minus-square");
      $("#address_form").show();
      $('#ship_add').attr('checked', false);
      }
      });


      $("#billing_collapse").click(function(){
      var className = $('#billing_collapse').attr('class');
      if(className=='fa fa-minus-square')
      {
      $("#billing_collapse").removeClass("fa fa-minus-square").addClass("fa fa-plus-square");
      $("#billing_div").hide();

      //alert("hide");
      }
      if(className=='fa fa-plus-square'){
      $("#billing_collapse").removeClass("fa fa-plus-square").addClass("fa fa-minus-square");
      $("#billing_div").show();

      //alert("show");
      }
      });

      // Multiple Feilds add code

      $("#payment_select").click(function(){
      //alert($("#payment_select").val());
      var select_value =$("#payment_select").val();

      if(select_value=='debit')
      {
      $("#synapse_div").show();
      $("#credit_div").hide();
      $("#gift_div").hide();
      }
      if(select_value=='credit')
      {
      $("#synapse_div").hide();
      $("#credit_div").show();
      $("#gift_div").hide();
      }
      if(select_value=='gift')
      {
      $("#synapse_div").hide();
      $("#credit_div").hide();
      $("#gift_div").show();
      }

      });
      $('#ship_add').attr('checked', true);
      $("#anc_add").click(function(){
      //alert("add");
      var cnt;
      cnt = $('#addr_div div').size();
      cnt++;

      $('#addr_div div').last().after('<div style="margin-top:10px;">
        <input  name="email'+cnt+'" id="email'+cnt+'" class="form_style num">
      </div>');
      //handleMasks();
      });

      $("#anc_rem1").click(function(){
      var size1 = $('#addr_div1 div').size();
      if(size1 > 1)
      {
      $('#addr_div1 div:last-child').remove();
      }
      });


      $("#anc_add1").click(function(){
      //alert("add");
      var cnt;
      cnt = $('#addr_div1 div').size();
      cnt++;

      $('#addr_div1 div').last().after('<div style="margin-top:10px;">
        <input  name="phone'+cnt+'" maxlength="15" id="phone'+cnt+'"  class="form_style num">
          <select name="phone_type'+cnt+'" class="form_style" id="phone_type'+cnt+'" >
            <option value="cell">Cell</option>
            <option value="home">Home</option>
            <option value="work">Work</option>
            <option value="family">Spouse/Family</option>
            <option value="alt">Alternate</option>
          </select>
      </div>');
      //handleMasks();
      $(".num").keypress(function (e) {
      //if the letter is not digit then display error and don't type anything


      if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 59)) {
        //display error message
      
               return false;
    }
   });
							});
										 
							$("#anc_rem").click(function(){
							var size1 = $('#addr_div div').size();
							if(size1 > 1)
								{
									$('#addr_div div:last-child').remove();
								}
							});
							
//$('#ship_add').checked('false');
$("#account").change(function(){
			 //var address_form=$("#address_form").val();
			var account=$("#account").val();
			//alert(account);
			 
			 $("#account123").val('2');
			 });

$("#payment_first").submit(function(){



if($("#payment_num").val()=='')
{
alert("Please Select a Job to fund");
return false;
}
if($("#bank").val()=='')
{
alert("Please Select Bank");
return false;
}


if($("#username_pay").val()=='')
{
alert("Please Enter Account Number");
return false;
}
if($("#password_pay").val()=='')
{
alert("Please Enter Routing Number");
return false;
}
if($("#routing_pay").val()=='')
{
alert("Please Enter Routing Number");
return false;
}
if($("#ssn").val()=='')
{
alert("Please Enter Last 4 digits of SSN");
return false;
}

if($("#account_type_value").val()=='')
{
alert("Please Select if account is Personal or Business");
return false;
}


if(document.getElementById('checking').checked) {
$("#account_type").val('Checking');
}else if(document.getElementById('saving').checked) {
 $("#account_type").val('Saving');
}
else
{
alert("Please Select Checking/Saving");
return false;
}
<?php if($row_user['Email']=='')
{?>

      alert("Please Enter Your Email Address");
      return false;
      <?php }?>

      <?php if($row_user['DateOfBirth']=='')
{?>


      $("#myModal").modal("show");
      alert("Please Enter Date of Birth");
      return false;
      <?php }?>


      });


      $(".click_class").click(function(){
      //var address_form=$("#address_form").val();

      //alert(this.id);
      $("#payment_num").val(this.id);
      $("#type").val('quote');
      $("#amount_pay").val($("#amount_hidden_quote"+this.id).val());
      $("#total_price").val(($("#amount_hidden_quote"+this.id).val()*3));
      });

      $(".click_class1").click(function(){
      //var address_form=$("#address_form").val();

      //alert(this.id);
      $("#payment_num").val(this.id);
      $("#type").val('sold');
      $("#amount_pay").val($("#amount_hidden_sold"+this.id).val());
      $("#total_price").val(($("#amount_hidden_sold"+this.id).val()*3));

      });

      $("#ship_add").click(function(){
      //var address_form=$("#address_form").val();


      if($('#ship_add').is(':checked'))
      {
      $("#address_form").hide();
      // alert("herehide");
      $("#shipping_collapse").removeClass("fa fa-minus-square").addClass("fa fa-plus-square");
      }
      else
      {
      $("#address_form").show();
      $("#shipping_collapse").removeClass("fa fa-plus-square").addClass("fa fa-minus-square");
      // alert("hereshow");
      }

      });
      });
      function formValidation()
      {
      //alert('abc');
      var d=document.myForm;
      //alert(d);
      if(d.fname.value =="")
      {
      alert("Please Enter First Name");
      d.fname.focus();
      return false;
      }
      if(d.email.value=="")
      {
      alert("Please Enter Email Address");
      d.email.focus();
      return false;
      }
      if(d.lname.value=="")
      {
      alert("Please Enter Your Last name");
      d.lname.focus();
      return false;
      }
      if(d.zip.value=="")
      {
      alert("Please Enter Your zip");
      d.zip.focus();
      return false;
      }
      if(d.phone.value=="")
      {
      alert("Please Enter Your phone");
      d.phone.focus();
      return false;
      }
      if(d.position.value =="16" && d.otherposition.value =="")
      {
      alert("Please Enter Position Name");
      d.otherposition.focus();
      return false;
      }

      return true;
      }
      function formBooking(){
      var dob= ($("#dob")).val().trim();
      var name= ($("#password_popup")).val().trim();
      var email=($("#password_confirm")).val().trim();


      if(name=='')
      {
      alert("Please Enter Password");
      document.getElementById("password_popup").focus();
      return false;
      }
      else if(email=='')
      {

      alert("Please Enter Password Again");
      document.getElementById("password_confirm").focus();
      return false;
      }
      else if($("#dob").val()=='')
      {
      alert("Please Select your date of Birth");
      return false;
      }
      };

      $(function() {
      var availableTags = [

      <?php

		echo "\""; 		echo "Bank of America";		echo "\",";	
		echo "\""; 		echo "BB&T Bank";		echo "\",";	
		echo "\""; 		echo "Chase";		echo "\",";	
		echo "\""; 		echo "Citibank";		echo "\",";	
		echo "\""; 		echo "Charles Schwab";		echo "\",";	
		echo "\""; 		echo "Capital One 360";		echo "\",";	
		echo "\""; 		echo "Fidelity";		echo "\",";	
		echo "\""; 		echo "Fidelity";		echo "\",";	
		echo "\""; 		echo "First Tennessee";		echo "\",";	
		echo "\""; 		echo "TD Bank";		echo "\",";	
		echo "\""; 		echo "SunTrust";		echo "\",";	
		echo "\""; 		echo "PNC";		echo "\",";	
		echo "\""; 		echo "Wells Fargo";		echo "\",";	
		echo "\""; 		echo "USAA";		echo "\",";	
		echo "\""; 		echo "US Bank";		echo "\",";	
		echo "\""; 		echo "Ally";		echo "\",";	
		echo "\""; 		echo "";		echo "\",";	
		

?>

      ];
      $( "#bank" ).autocomplete({

      source: availableTags

      });

      });

      $(document).ready(function () {


      $(".num").keypress(function (e) {
      //if the letter is not digit then display error and don't type anything


      if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 59)) {
        //display error message
      
               return false;
    }
   });

});

    </script>
    <style>
      .close {
      float: right;
      font-size: 21px;
      font-weight: bold;
      line-height: 1;
      color: #000;
      text-shadow: 0 1px 0 #fff;
      filter: alpha(opacity=20);
      opacity: .2;
      }
    </style>
    <!-- Modal -->


    <script>
      $(document).ready(function() {
      <?php 
if($row_user['Password']=='jmgrove')
{?>
      $("#myModal").modal("show");

      <?php }?>



      <?php 
if($_REQUEST['add_kyc']=='1')
{?>
      $("#kyc_popup").modal("show");

      <?php }?>

      <?php 
if($_REQUEST['micro']=='1')
{?>
      $("#micro_popup").modal("show");

      <?php }?>

      });


      $(document).ready(function() {
      $(function(){

      $('#dob').datepicker({
      changeMonth: true,
      changeYear: true,
      dateFormat: 'yy-mm-dd',

      yearRange: '1950:2050' ,

      });

      });

      });
    </script>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
            <?php if($row_user['Password']=='jmgrove')
{
echo "<label style='color:red;'><b>Welcome ".$row_user['CustomerName']." ".$row_user['lastname'].".Please enter new passwork and enter date of birth for security reasons</b></label><br>";}?>
            <b>Enter Your Password</b>
          </div>
          <div class="modal-body">
            <form name="booking_form" id="booking_form" method="get" onSubmit="return formBooking()">




              <div class="col-md-6 padding_class">
                <b>
                  <label style="color:#FF0000;">*</label>Password:
                </b>
              </div>
              <div class="col-md-6 padding_class">
                <input type="password" name="password_popup" id="password_popup"  class="form_style" />
                <div id="pass_err" style="color:#FF0000;"></div>
              </div>

              <div class="col-md-6" style="margin-top:15px;">
                <b>
                  <label style="color:#FF0000;">*</label>Confirm Password:
                </b>
              </div>
              <div class="col-md-6" style="margin-top:15px;">
                <input type="password" name="password_confirm" id="password_confirm"  class="form_style" />
                <div id="confirm_pass_err" style="color:#FF0000;"></div>
              </div>

              <div class="modal-body col-md-6">
                <div class="col-md-6 padding_class">
                  <b>
                    <label style="color:#FF0000;">*</label>Date of Birth:
                  </b>
                </div>
                <div class="col-md-6 padding_class">
                  <input type="text"  name="dob" id="dob" value=''<?php echo $row_user['DateOfBirth'];?>' class="form_style"/>
                  <div id="pass_err" style="color:#FF0000;"></div>
                </div>
                <div class="col-md-12" style="margin-top:15px;">
                  <!--    <button type="submit" id="submit" name="submit" style="float:right;" value="submit" class="btn btn-primary">Submit</button>-->
                  <input type="submit" name="password_submit" id="password_submit" style="float:right;" class="button_submit" value="Submit"/>
                </div>

              </div>
            </form>
            <div class="modal-footer">

            </div>
          </div>
        </div>
      </div>
    </div>







    <div class="modal fade" id="micro_popup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
            <h3>
              <?php 
		if($_REQUEST['err']!='')
		echo "Enter micro deposits amount from your bank statements to verify this transaction";
		else
		{echo $_REQUEST['err'];}?>
            </h3>

          </div>
          <div class="modal-body">
            <form name="micro_form" action="verify_node.php">
              <div class="col-md-6 padding_class">
                <b>
                  <label style="color:#FF0000;">*</label>Amount 1:
                </b>
              </div>
              <div class="col-md-6 padding_class">
                <input type="text" name="micro1" id="micro1" class="form_style"/>
              </div>
              <div class="col-md-6 padding_class">
                <b>
                  <label style="color:#FF0000;">*</label>Amount 2:
                </b>
              </div>
              <div class="col-md-6 padding_class">
                <input type="text" name="micro2" id="micro2" class="form_style" />
              </div>
              <div class="col-md-12" style="margin-top:15px;">
                <!--    <button type="submit" id="submit" name="submit" style="float:right;" value="submit" class="btn btn-primary">Submit</button>-->
                <input type="submit" name="kyc_submit" id="kyc_submit" style="float:right;" class="button_submit" value="Submit"/>
              </div>

              <input type="hidden" name="id" id="id" value=""<?php echo $_REQUEST['id'];?>">
              <input type="hidden" name="oauth" id="oauth" value=""<?php echo $_REQUEST['oauth'];?>">
              <input  type="hidden"name="amount" id="amount" value=""<?php echo $_REQUEST['amount'];?>">
              <input  type="hidden"name="password" id="password" value=""<?php echo $_REQUEST['password'];?>">
              <input type="hidden" name="account" id="account" value=""<?php echo $_REQUEST['account'];?>">
              <input  type="hidden" name="payment_num" id="payment_num" value=""<?php echo $_REQUEST['payment_num'];?>">
              <input type="hidden" name="cust_id" id="cust_id" value=""<?php echo $_REQUEST['cust_id'];?>">
              <input type="hidden" name="job_id" id="job_id" value=""<?php echo $_REQUEST['job_id'];?>">
              <input  type="hidden" name="total_price" id="total_price" value=""<?php echo $_REQUEST['total_price'];?>">
              <input  type="hidden" name="pay_all" id="pay_all" value=""<?php echo $_REQUEST['pay_all'];?>">
              <input type="hidden" name="type" id="type" value=""<?php echo $_REQUEST['type'];?>">
              <input  type="hidden" name="bank" id="bank" value=""<?php echo $_REQUEST['bank'];?>" />


              <input  type="hidden" name="username_pay" id="username_pay" value=""<?php echo $_REQUEST['username_pay'];?>">
              <input  type="hidden" name="password_pay" id="password_pay" value=""<?php echo $_REQUEST['password_pay'];?>">
              <input type="hidden" name="ssn" id="ssn" value=""<?php echo $_REQUEST['ssn'];?>">

            </form>
            <div class="modal-footer">

            </div>
          </div>
        </div>
      </div>
    </div>




    <div class="modal fade" id="kyc_popup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
            <?php
if($_REQUEST['add_kyc']=='1')
{
 $url = "https://sandbox.synapsepay.com/api/v3/user/doc/add";

 // KYC Documentation
 
$payload = array(
    "login" => array(
	 "oauth_key" => $_REQUEST['oauth']
    ),
    "user" => array(
		"doc" => array(
             "birth_day" => date("d",strtotime($row_user['DateOfBirth'])),
            "birth_month" => date("m",strtotime($row_user['DateOfBirth'])),
            "birth_year" => date("Y",strtotime($row_user['DateOfBirth'])),
            "name_first" => $row_user['CustomerName'],
            "name_last" => $row_user['lastname'],
            "address_street1" => $row_user['CustomerAddress'],
            "address_postal_code" => $row_user['ZipCode'],
            "address_country_code" => "US",
            "document_value" => $_REQUEST['ssn'],
            "document_type" => "SSN"
        ),
        "fingerprint" => "suasusau21324redakufejfjsf",
    )
);

$json=json_encode($payload);
//echo "<pre>";
//print_r($payload);
//exit;
//$payload=json_encode($json);



$sUrl= "https://sandbox.synapsepay.com/api/v3/user/doc/add";
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL,$sUrl);
    curl_setopt($ch, CURLOPT_VERBOSE, 1);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 0);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
    curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Type: application/json; charset=utf-8'));
    curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
    $result = curl_exec($ch);
    curl_close($ch);
echo "<pre>";
$string=json_decode($result);
// print_r($string);

//echo "Location:verifykyc.php?email=".$_REQUEST['email']."&oauth=".$_REQUEST['oauth']."&amount=".$_REQUEST['amount']."&password=".$_REQUEST['password']."&amount=".$_REQUEST['amount']."&account=".$_REQUEST['account']."&payment_num=".$_REQUEST['payment_num']."&cust_id=".$_REQUEST['cust_id']."&job_id=".$_REQUEST['job_id']."&total_price=".$_REQUEST['total_price']."&pay_all=".$_REQUEST['pay_all']."&type=".$_REQUEST['type']."&bank=".$_REQUEST['bank']."&username_pay=".$_REQUEST['username_pay']."&password_pay=".$_REQUEST['password_pay'];
//exit; 
    $success=$string->success;
//echo "<br>success".$success;

$oauth_key=$string->oauth->oauth_key;
//echo "<br>oauth_key".$oauth_key;

$id=$string->user->_id;
$id=json_encode($id);
//echo "<br>id";
//echo substr($id,9,24);
echo " <b>";
 if($string->error_code=='10')
{
echo $string->message->en;
}

if($string->error->en)
{echo $string->error->en;}
echo "</b>";
}
		?>

          </div>
          <div class="modal-body">
            <?php  if($string->error_code=='10'){?>
            <form name="kyc_form" id="kyc_form" method="get" action="verifykyc.php">


              <input type="hidden" name="person_id" id="person_id" value=""<?php echo $string->question_set->person_id;?>">
              <input type="hidden" name="questions_id" id="questions_id" value=""<?php echo $string->question_set->id;?>">

              <?php  $i=0;
 while($string->question_set->questions[$i])
 {
	 ?>
              <div class="col-md-6 padding_class">
                <b>
                  <label style="color:#FF0000;">*</label>
                  <?php echo $string->question_set->questions[$i]->question;?>
                </b>
              </div>
              <div class="col-md-6 padding_class">
                <input type="hidden" name="question"<?php echo $i;?>" value="<?php echo $string->question_set->questions[$i]->id;?>">
                <?php
	   $j=1;
	    while($string->question_set->questions[$i]->answers[$j])
		 {?>
                <input type="radio" name="answer"<?php echo $i;?>" <?php if($j=='1'){?> checked="checked" <?php }?> value="<?php echo $string->question_set->questions[$i]->answers[$j]->id;?>"><?php echo $string->question_set->questions[$i]->answers[$j]->answer;?><br>

                  <?php $j++; }?>
                  <div id="asd_err" style="color:#FF0000;"></div>
                </div>

              <?php  $i++;
 }?>


              <div class="col-md-12" style="margin-top:15px;">
                <!--    <button type="submit" id="submit" name="submit" style="float:right;" value="submit" class="btn btn-primary">Submit</button>-->
                <input type="submit" name="kyc_submit" id="kyc_submit" style="float:right;" class="button_submit" value="Submit"/>
              </div>

            </div>

          <input type="hidden" name="id" id="id" value=""<?php echo $_REQUEST['id'];?>">
          <input type="hidden" name="oauth" id="oauth" value=""<?php echo $_REQUEST['oauth'];?>">
          <input  type="hidden"name="amount" id="amount" value=""<?php echo $_REQUEST['amount'];?>">
          <input  type="hidden"name="password" id="password" value=""<?php echo $_REQUEST['password'];?>">
          <input type="hidden" name="account" id="account" value=""<?php echo $_REQUEST['account'];?>">
          <input  type="hidden" name="payment_num" id="payment_num" value=""<?php echo $_REQUEST['payment_num'];?>">
          <input type="hidden" name="cust_id" id="cust_id" value=""<?php echo $_REQUEST['cust_id'];?>">
          <input type="hidden" name="job_id" id="job_id" value=""<?php echo $_REQUEST['job_id'];?>">
          <input  type="hidden" name="total_price" id="total_price" value=""<?php echo $_REQUEST['total_price'];?>">
          <input  type="hidden" name="pay_all" id="pay_all" value=""<?php echo $_REQUEST['pay_all'];?>">
          <input type="hidden" name="type" id="type" value=""<?php echo $_REQUEST['type'];?>">
          <input  type="hidden" name="bank" id="bank" value=""<?php echo $_REQUEST['bank'];?>" />


          <input  type="hidden" name="username_pay" id="username_pay" value=""<?php echo $_REQUEST['username_pay'];?>">
          <input  type="hidden" name="password_pay" id="password_pay" value=""<?php echo $_REQUEST['password_pay'];?>">
          <input type="hidden" name="ssn" id="ssn" value=""<?php echo $_REQUEST['ssn'];?>">

          </form>
          <?php }?>

          <?php if($string->error->en) {?>
          <form name="kyc_form" id="kyc_form" method="post" enctype="multipart/form-data" >
            <div class="col-md-6 padding_class">
              <b>
                <label style="color:#FF0000;">*</label>Browse File:
              </b>
            </div>
            <div class="col-md-6 padding_class">
              <input type="file" name="name_of_control" id="name_of_control" />
            </div>
            <div class="col-md-12" style="margin-top:15px;">
              <!--    <button type="submit" id="submit" name="submit" style="float:right;" value="submit" class="btn btn-primary">Submit</button>-->
              <input type="submit" name="kyc_submit" id="kyc_submit" style="float:right;" class="button_submit" value="Submit"/>
            </div>

          </div>

        <input type="hidden" name="id" id="id" value=""<?php echo $_REQUEST['id'];?>">
        <input type="hidden" name="oauth" id="oauth" value=""<?php echo $_REQUEST['oauth'];?>">
        <input  type="hidden"name="amount" id="amount" value=""<?php echo $_REQUEST['amount'];?>">
        <input  type="hidden"name="password" id="password" value=""<?php echo $_REQUEST['password'];?>">
        <input type="hidden" name="account" id="account" value=""<?php echo $_REQUEST['account'];?>">
        <input  type="hidden" name="payment_num" id="payment_num" value=""<?php echo $_REQUEST['payment_num'];?>">
        <input type="hidden" name="cust_id" id="cust_id" value=""<?php echo $_REQUEST['cust_id'];?>">
        <input type="hidden" name="job_id" id="job_id" value=""<?php echo $_REQUEST['job_id'];?>">
        <input  type="hidden" name="total_price" id="total_price" value=""<?php echo $_REQUEST['total_price'];?>">
        <input  type="hidden" name="pay_all" id="pay_all" value=""<?php echo $_REQUEST['pay_all'];?>">
        <input type="hidden" name="type" id="type" value=""<?php echo $_REQUEST['type'];?>">
        <input  type="hidden" name="bank" id="bank" value=""<?php echo $_REQUEST['bank'];?>" />


        <input  type="hidden" name="username_pay" id="username_pay" value=""<?php echo $_REQUEST['username_pay'];?>">
        <input  type="hidden" name="password_pay" id="password_pay" value=""<?php echo $_REQUEST['password_pay'];?>">
        <input type="hidden" name="ssn" id="ssn" value=""<?php echo $_REQUEST['ssn'];?>">

        </form>
        <?php }?>
        <div class="modal-footer">

        </div>
      </div>
    </div>
    </div>
    </div>

  </body>
</html>
