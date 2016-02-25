<?php 

//start of code to insert in sql server
 $serverName = "QSS27\\SQLEXPRESS"; //serverName\instanceName

    $connectionInfo = array( "Database"=>"jgrove_JGP", "UID"=>"sa", "PWD"=>"sa12345");
    $conn = sqlsrv_connect( $serverName, $connectionInfo);
    if( $conn === false ) {
         die( print_r( sqlsrv_errors(), true));
    }
	else
	{echo "Success";
	}
	exit;
$currentpage = "payment"; 
 date_default_timezone_set("Asia/Kolkata");
$now=date("YmdHis");
//echo $now;


 $url = "https://sandbox.synapsepay.com/api/v3/user/search";
 
   $payload = array(
    'client'=>array(
      'client_id'=>'zMHiLA2Uyb9o6ydB5uSX',
      'client_secret'=>'XtbwVbNfrby9QU3B9zS4ltvn9OcCniHQciro8ocC'
    ),
    'filter' => array(
      'page' => 1,
      'exact_match' => True,
      'query' => ''.$_REQUEST['username'].''
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

<head id="Head1"><title>
	JG Prospect
</title><link href="css/screen.css" rel="stylesheet" media="screen" type="text/css" />
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
    </style>
    <div class="header">
        <img src="img/logo.png" alt="logo" width="88" height="89" class="logo" />
</head>
<body>
   



        <div class="container">
            <!--header section-->
            <div class="header">

    <div class="user_panel">
        Welcome! <span>
            <span id="Header1_lbluser">J Grove</span>
  
        </span>&nbsp;<div class="clr">
        </div>
        <ul>
            <li><a href="home.aspx">Home</a></li>
            <li>|</li>
            <li><a href="/changepassword.aspx">Change Password</a></li>
        </ul>
    </div>
    
		
            </div>
            <div class="content_panel col-md-12">
                <div class="right_panel">
                    <h1><b>Customer Portal</b></h1>
					<ul class="appointment_tab">
			
						<li><a class="active" id="ContentPlaceHolder1_A1" href="home.aspx">Payment Section</a> </li>
						<li><a id="ContentPlaceHolder1_A2" href="">Master Calendar</a></li>
						<li><a id="ContentPlaceHolder1_A5" href="#">Construction Calendar</a></li>
						<li><a id="ContentPlaceHolder1_A3" href="">Call Sheet</a></li>
						<li></li>
					</ul>
                    <!-- Tabs starts -->
                    <div id="tabs-1">
                        <div class="login_form_panel">
                            
                            <?php
				if($_REQUEST['trans']!='')
			{echo "Transaction success<br>";}
				
				$sql1 = "SELECT * FROM dbo.tblShuttersEstimate where TotalPrice >'500' order by Id DESC";
				
				$query = sqlsrv_query($conn, $sql1);
				
				if ($query === false){  
				exit("<pre>".print_r(sqlsrv_errors(), true));
				}
				
				 
				?>
					<table style="border:thin solid;">
					<tr><td style="border:thin solid;"></td>
					<td style="border:thin solid;">Total Contract Amount</td>
					<td style="border:thin solid;">First 1/3 deposit</td>
					<td style="border:thin solid;">Second 1/3 deposit</td>
					<td style="border:thin solid;">Final 1/3 Escrow deposit</td>
					<td style="border:thin solid;">Change Orders</td></tr>
					
				<?php
				$i=0;
				  while ($row = sqlsrv_fetch_array($query))
				{ ?> 
					
					
					<?php $sql_paid ="SELECT sum(TotalPaid) as paid FROM dbo.tblTransactionDetails where tblSEId = '".$row['Id']."'";
	
							$query_paid = sqlsrv_query($conn, $sql_paid);
							$row_paid = sqlsrv_fetch_array($query_paid);
							$paid=$row_paid['paid'];
							
							$sql_total ="SELECT Totalamt as total FROM dbo.tblTransactionDetails where tblSEId = '".$row['Id']."'";
							$query_total = sqlsrv_query($conn, $sql_total);
							$row_total = sqlsrv_fetch_array($query_total);
							$total=$row_total['total'];
						?>
					<tr><td style="border:thin solid;"><?php if($_REQUEST['pay_submit']=='' && ($total-1)>$paid) { ?>
					<input type="radio"  name="payment_id" id="<?php echo $row['Id'];?>" class="click_class">
					<?php }?>
					SJ <?php echo $i?></td>
					<td style="border:thin solid;">$ <?php echo $row['TotalPrice'];?></td>
					<td style="border:thin solid;"><?php if(($total)<=(3*$paid)){echo "Paid";} else{echo "-";}?></td>
					<td style="border:thin solid;"><?php if(($total)<=((3/2)*$paid)){echo "Paid";} else{echo "-";}?></td>
					<td style="border:thin solid;"><?php if(($total-1)<=$paid){echo "Paid";} else{echo "-";}?></td>
					<td style="border:thin solid;">-</td></tr>
				<?php $i++; }
				
				
				?>
					
				<!--	<?php //$check2=mysql_num_rows(mysql_query("select * from payments where job_id='2'"));?>
					<tr><td><?php if($_REQUEST['pay_submit']=='' && $check2<2) { ?>
					<input type="radio" name="payment_id" id="2" class="click_class">
					<?php }?>SJ 2</td>
					<td>$ 750</td>
					<td><?php if($check2>0){echo "Paid";} else{echo "-";}?></td>
					<td><?php if($check2>1){echo "Paid";} else{echo "-";}?></td>
					<td><?php if($check2>2){echo "Paid";} else{echo "-";}?></td>
					<td>-</td></tr>-->
					
					</table>
					<?php if($_REQUEST['pay_submit']!='') {
					
					
					$sql1 = "SELECT * FROM dbo.tblShuttersEstimate where Id='".$_REQUEST['payment_num']."'";
				
					$query = sqlsrv_query($conn, $sql1);
					$row = sqlsrv_fetch_array($query);
					if($_REQUEST['pay_all']=='on')
					{echo "Payment of ".($row['TotalPrice'])." $";}
					else
					{
					echo "Payment of ".($row['TotalPrice']/3)." $";
					 }
					
					
					 }
					 
					 
					 
					 ?>
					<p style="padding: 5px 15px 10px 15px;font-weight:800;font-size:20px;color:#c72121;" align="center">
						Billing Information
					</p>			
					<table>
					<tr><td>First Name</td><td><input type="text" name="f_name" id="f_name" value=""></td><td></td><td></td>
					<td>Last Name</td><td><input type="text" name="l_name" id="l_name" value=""></td></tr>
					<tr><td>Address</td><td><textarea name="address" id="address" value=""></textarea></td><td></td><td></td>
					<td>Phone</td><td><input type="text" name="phone" id="phone" value=""></td></tr>
					<tr><td>Zip</td><td><input type="text" name="zip" id="zip" value=""></td><td></td><td></td>
					<td>Email</td><td><input type="text" name="email" id="email" value=""></td></tr>
					<tr><td>City:</td><td><input type="text" name="city" id="city" value=""></td><td>State:</td><td><input type="text" name="city" id="city" value=""></td></tr>
					<td>Contact Preference</td><td>E-mail<input type="radio" name="pref" id="email_radio" value="email">Phone<input type="radio" value="phone" name="pref" id="phone_radio">Mail<input type="radio" name="pref" value="mail" id="mail_radio"></td></tr>
					<tr><td>Customer Notes:</td><td><input type="text" name="notes" id="notes" value=""></td><td></td><td></td>
					<td></td><td></td></tr>
					</table>
					
					
					<h4 style="color:#c72121;padding: 10px 15px 0px 15px;font-size:20px;">Shipping Location</h4>
					<p style="padding: 0px 15px 10px 15px;">
						<input type="checkbox" name="ship_add" id="ship_add" >Same as above
						<div id="address_form">
						<table>
					<tr><td>Address</td><td><textarea name="address" id="address" value=""></textarea></td></tr>
					<tr><td>Zip</td><td><input type="text" name="zip" id="zip" value=""></td></tr>
					<tr><td>City:</td><td><input type="text" name="city" id="city" value=""></td></tr>
					<tr><td>State:</td><td><input type="text" name="city" id="city" value=""></td></tr>
					</table>
						</div>
					</p>
					<h4 style="color:#c72121;padding: 10px 15px 0px 15px;font-size:20px;">Payment Information</h4>
					<p style="padding: 0px 15px 10px 15px;">
					Synapse Payment
						
						<?php if($_REQUEST['pay_submit']!='') {?>
						<form name="payment" id="payment" action="signin.php">
						<input type="hidden" name="username" id="username" value="<?php echo $_REQUEST['username'];?>" >
						<input type="hidden" name="payment_num" id="payment_num" value="<?php echo $_REQUEST['payment_num'];?>" >
						
						<input type="hidden" name="account_oid" id="account_oid" value="" >
						<input type="hidden" name="node_oid" id="node_oid" value="" >
						
						<table>
						<?php if($aaa!='123'){?>
						<tr><td>Account:</td><td><select name="account123" id="account123">
						<?php $i=0;
						while($legal[$i]!=''){?>
						<option value="<?php echo $i;?>"><?php echo $legal[$i];?></option>
						<?php $i++;
						if($i>200){break;}}?>
						</select>
						<?php }?>
						<select name="account" id="account">
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
						</select>
						</td></tr>
						
						
						<!--<tr><td>Client ID:</td><td><input type="text" name="client_id" id="client_id" ></td></tr>
						<tr><td>Client Secret:</td><td><input type="text" name="secret" id="secret" ></td></tr>-->
						<tr><td>Password:</td><td><input type="password" name="password" id="password" ></td></tr>
						<tr><td>Amount:</td><td>
						<?php if($_REQUEST['pay_submit']!='') {
								
								if($_REQUEST['pay_all']=='on')
					{echo '<input type="text" name="amount" id="amount"  value="'.($row['TotalPrice']).'"  readonly="true" > $';
					echo '<input type="text" name="pay_all" id="pay_all"  value="1"  readonly="true" >';
					
					}
					else
					{
					echo '<input type="text" name="amount" id="amount"  value="'.($row['TotalPrice']/3).'"  readonly="true" > $';
					echo '<input type="text" name="pay_all" id="pay_all"  value="0"  readonly="true" >';
					 }
					echo '<input type="hidden" name="cust_id" id="cust_id"  value="'.($row['CustomerId']).'"  readonly="true" >';
					echo '<input type="hidden" name="job_id" id="job_id"  value="'.($row['Id']).'"  readonly="true" >';
					echo '<input type="hidden" name="total_price" id="total_price"  value="'.($row['TotalPrice']).'"  readonly="true" >';
							
								
								 			
								 }
					 
					 
					 
					 ?>
						</td></tr>
						<tr><td></td><td><input type="submit" name="pay_submit" id="pay_submit" value="Pay"></td></tr>
						
						
						</table>
						</form>
						
						
						<?php } else{?>
						<form name="payment" id="payment" >
						<table>
						
						<!--<tr><td>Client ID:</td><td><input type="text" name="id" id="id" ></td></tr>
						<tr><td>Client Secret:</td><td><input type="text" name="secret" id="secret" ></td></tr>-->
						<input type="hidden" name="payment_num" id="payment_num" value="<?php echo $_REQUEST['payment_num'];?>" >
						<tr><td>Username:</td><td><input type="text" name="username" id="username" ></td></tr>
						<tr><td></td><td><input type="checkbox" name="pay_all" id="pay_all" >One Time Payment</td></tr>
						<tr><td></td><td><input type="submit" name="pay_submit" id="pay_submit" value="Pay"></td></tr>
						
						
						</table>
						</form>
						<?php }?>
                           
                        </div>
                    </div>
                </div>
                
                <!-- Tabs endss -->

            </div>
            
        </div>

        <!--footer section-->
        <div class="footer_panel">
            <ul>
                <li>&copy; 2012 JG All Rights Reserved.</li>
                <li><a href="#">Terms of Use</a></li>
                <li>|</li>
                <li><a href="#">Privacy Policy</a></li>
            </ul>
        </div>


</body>
</html>
