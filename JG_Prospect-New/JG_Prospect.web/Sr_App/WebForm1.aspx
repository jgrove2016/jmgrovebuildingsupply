<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="JG_Prospect.Sr_App.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
  <tr>
    <td align='center'><strong>INSTALLER WORK ORDER</strong></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td> <strong>Salesman:</strong> lbl_salesman</td>
        <td align='right'> <strong>Date Sold:</strong> lbl_date</td>
      </tr>
      <tr>
        <td> <strong>Cell #:</strong> lbl_cell</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td width='50%'> <u><strong>Installer Info</strong></u></td>
        <td width='50%'> <u><strong>Customer Info</strong></u></td>
      </tr>
      <tr>
        <td><br></td>
        <td><br></td>
      </tr>
      <tr>
        <td> Installer: lbl_installer</td>
        <td> Name: lbl_name</td>
      </tr>
      <tr>
        <td>Phone #: lbl_phone</td>
        <td> Job Location: lbl_joblocation</td>
      </tr>
      <tr>
        <td>Work Scheduled Date: lbl_workscheduled</td>
        <td> Phone #s: lbl_customerphone</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td width='50%' align='center'><u><strong>Special Instructions:</strong></u></td>
        <td width='50%' align='center'><u><strong>Work Area:</strong></u></td>
      </tr>
      <tr>
        <td align='center'><br></td>
        <td align='center'><br></td>
      </tr>
      <tr>
        <td align='center'>lbl_specialinstructions</td>
        <td align='center'>lbl_workarea</td>
      </tr>
      <tr>
        <td align='left'><u><strong>Materials Out</strong></u></td>
        <td align='left'><u><strong>Materials Returned</strong></u></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td>&nbsp;</td>
        <td colspan='5'>lbl_list</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td>Any additional work needed that is not listed on work order you must call salesman and homeowners approval to complete job. Ex rotted wood, metal etc. Poor workmanship or covering up shoddy work will not be tolerated!!! All excess material scrap aluminum, steel, &amp; copper is JMG property and must be returned. It is considered theft if not returned! </td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>Reschedule Date 1:        /          /</td>
        <td align='right'>&nbsp;</td>
      </tr>
      <tr>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'>&nbsp;</td>
        <td align='right'> Reschedule Date 2:       /           /</td>
        <td align='right'>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td align='center'>1.</td>
        <td colspan='5'>Is job done? Y - N</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align='center'>2.</td>
        <td colspan='5'> Leave sign: Y - N</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align='center'>3.</td>
        <td colspan='5'>Does customer need an EST? Y - N</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align='center'>4.</td>
        <td colspan='5'>Walk around / Phone Call &amp; Collection and any additional add on(s)? Y - N</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align='center'><br></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><br></td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
      <tr>
        <td colspan='2'>Sub-Contractor Signature:</td>
        <td colspan='3'>&nbsp;</td>
        <td align='right'>Date:</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td colspan='2'>&nbsp;</td>
        <td colspan='3'><hr color='#000000' width='100%' /></td>
        <td>&nbsp;</td>
        <td><hr color='#000000' width='100%' /></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
