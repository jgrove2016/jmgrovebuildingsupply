using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using JG_Prospect.DAL.Database;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using JG_Prospect.Common.modal;

namespace JG_Prospect.DAL
{
    public class pdf_DAL
    {
        private static pdf_DAL m_pdf_DAL = new pdf_DAL();
        private DataSet returndata;

        private pdf_DAL()
        {

        }

        public static pdf_DAL Instance
        {
            get { return m_pdf_DAL; }
            private set { ;}
        }


        //public DataSet FetchCustomerContractDetails(int CustomerId)
        //{
        //    try
        //    {
        //        SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
        //        {
        //            DataSet DS = new DataSet();
        //            DbCommand command = database.GetStoredProcCommand("UDP_FetchCustomerContractDetails");
        //            command.CommandType = CommandType.StoredProcedure;
        //            database.AddInParameter(command, "@CustomerId", DbType.Int32, CustomerId);
        //            DS = database.ExecuteDataSet(command);
        //            return DS;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        string url = ConfigurationManager.AppSettings["URL"].ToString();
        //        public string ContractPdfHeader()
        //        {
        //        string header = @"<table align='center' width='700' bordercolor='#666666' border='0' cellspacing='0' cellpadding='0' style='font-family:Verdana, Geneva, sans-serif; font-size:8pt;'>
        //  <tr>
        //    <td height='8' colspan='2' bgcolor='#990000' > </td>
        //  </tr>
        //  <tr>
        //    <td height='16' colspan='2' bgcolor='#000000'> </td>
        //  </tr>
        //  <tr>
        //    <td width='460' height='100' align='center'><img src=" + url + @"/img/LogoJG_PDF.png height='100' title='JM Grove construction Logo' /></td>
        //    <td>
        //    	<span><strong>Phone:(215) 274-5182</strong></span><br />
        //        <span>220 Krams Avenue</span><br />
        //        <span>Manayunk, PA 19127</span><br />
        //        <span>Service-Sales@JMGroveConstruction.com</span><br />
        //        <span>www.JMGroveconstruction.com</span><br />
        //        
        //    </td>
        //    
        //  </tr>
        //  <tr>
        //    <td height='16' colspan='2' bgcolor='#000000'>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td height='32' colspan='2' bgcolor='#990000' style='color:#ffffff; font-weight:bold; text-align:center; text-transform:uppercase; font-size:8pt;'>Doors • Windows • Roofing • siding • Skylights • Gutters  • Awnings • Decks • Soffit &amp; Fascia</td>
        //  </tr>
        //  <tr>
        //    <td height='34' colspan='2' align='center'><table width='100%' border='0' cellspacing='0' cellpadding='0'>
        //      <tr>
        //        <td width='25%' height='26' valign='bottom'><strong>Submitted To:</strong></td>
        //        <td width='25%' style='border-bottom:1px solid #000000'><label>lblsubmittedto</label></td>
        //        <td width='20%' valign='bottom'><p><strong>&nbsp;&nbsp;Home Phone:</strong></p></td>
        //        <td width='30%' style='border-bottom:1px solid #000000'><label>lblhousePhone</label></td>
        //        </tr>
        //      <tr>
        //        <td height='26' valign='bottom'><strong>Street Address:</strong></td>
        //        <td style='border-bottom:1px solid #000000'><label>lblAddress</label></td>
        //        <td valign='bottom'><strong>&nbsp;&nbsp;Cell Phone:</strong></td>
        //        <td style='border-bottom:1px solid #000000'>lblCell</td>
        //        </tr>
        //      <tr>
        //        <td height='26' valign='bottom'><strong>City, State &amp; Zip Code:</strong></td>
        //        <td style='border-bottom:1px solid #000000'>lblcitystatezip</td>
        //        <td valign='bottom'><strong>&nbsp;&nbsp;Email:</strong></td>
        //        <td style='border-bottom:1px solid #000000'><label>lblemail</label></td>
        //      </tr>
        //      <tr>
        //        <td height='26' valign='bottom'><strong>Date:</strong></td>
        //        <td style='border-bottom:1px solid #000000'>lblDate</td>
        //        <td valign='bottom'><strong>&nbsp;&nbsp;Job Location:</strong></td>
        //        <td style='border-bottom:1px solid #000000'>lblJobLocation</td>
        //      </tr>
        //      <tr>
        //        <td height='26' valign='bottom'><strong>Customer ID:</strong></td>
        //        <td style='border-bottom:1px solid #000000'>lblCustomerId</td>
        //        <td valign='bottom'>&nbsp;&nbsp;<strong>Job No.:</strong></td>
        //        <td style='border-bottom:1px solid #000000'>lblJobNo</td>
        //      </tr>
        //    </table>";
        //            return header;
        //        }
        public string ContractPdfHeader()
        {
            string header = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
  <tr>
    <td>
        <table width='100%' border='0' cellspacing='0' cellpadding='0'>      
        <tr>
            <td  valign='top'><img src=" + url + @"/img/ContractHeader.jpg width='550px' alt='' height='150px' /></td>
        </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
      <tr>                          
	<td> <strong>Submitted To:</strong>  </td>                          
	<td><label>lblsubmittedto</label> </td>              
        <td>  <strong>&nbsp;&nbsp;Home Phone:</strong>  </td> 
        <td> <label>lblhousePhone</label> </td>              
</tr> 
<tr>                         
	 <td> <strong>Street Address:</strong>  </td>                         
	 <td> <label> lblAddress</label> </td>    
         <td><strong>&nbsp;&nbsp;Cell Phone:</strong>   </td>                          
	<td>lblCell</td>                    
</tr>     
<tr>                          
	<td> <strong>City, State &amp; Zip Code:</strong> </td>     
        <td>lblcitystatezip  </td>                      
	<td><strong>&nbsp;&nbsp;Email:</strong> </td>  
        <td><label>lblemail</label> </td>           
</tr>
<tr>                         
 	<td> <strong>Date:</strong></td>   
        <td>lblDate </td>                         
	<td> <strong>&nbsp;&nbsp;Job Location:</strong> </td>                          
	<td>lblJobLocation </td> 
</tr>                      
<tr>                          
	<td><strong>Customer ID:</strong></td>                         
 	<td>lblCustomerId </td>  
        <td>  &nbsp;&nbsp;<strong>Job No.:</strong></td>                          
	<td>lblJobNo </td>                      
</tr> ";
            //            string header = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
            //  <tr>
            //    <td>
            //
            //<table width='100%' border='0' cellspacing='0' cellpadding='0'>      
            //      <tr>
            //        <td colspan='6' valign='top'><img src=" + url + @"/img/header001.png width='300px' /></td>
            //        <td colspan='4' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:6px;'>
            //          <tr>
            //            <td>PHONE: (215) 274-5182 <br />
            //          220 KRAMS AVENUE <br />
            //          MANAYUNK, PA 19127 <br />
            //          SERVICE-SALES@JMGROVECONSTRUCTION.COM <br />
            //          <span style='color:#e51b24'>WWW.JMGROVE CONSTRUCTION.COM</span></td>
            //          </tr>
            //          <tr>
            //            <td>
            //
            //<table width='100%' border='0' cellspacing='0' cellpadding='0'>
            //  <tr>
            //    <td>
            //
            //<table width='100%' border='0' cellspacing='0' cellpadding='0'>
            //      <tr>
            //        <td><a href='#'><img src=" + url + @"/img/facebook.png width='10' vspace='10' border='0' /></a></td>
            //        <td><a href='#'><img src=" + url + @"/img/twitter.png width='10' hspace='10' vspace='10' border='0' /></a></td>
            //        <td><a href='#'><img src=" + url + @"/img/googleplus.png width='10' vspace='10' border='0' /></a></td>
            //        <td><a href='#'><img src=" + url + @"/img/youtube.png width='30' hspace='10' vspace='10' border='0' /></a></td>
            //      </tr>
            //    </table>
            //
            //</td>
            //    <td>&nbsp;</td>
            //  </tr>
            //</table>
            //
            //</td>
            //          </tr>
            //        </table>
            //
            //
            //</td>
            //        </tr>
            //    </table>
            //
            //</td>
            //  </tr>
            //  <tr>
            //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>      
            //      <tr><td><img src=" + url + @"/img/header002.png width='520px' />
            //</td>
            //</tr>
            //</table>
            //
            //</td>
            //  </tr>
            //  <tr>
            //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
            //      
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' align='right' valign='top'><strong>Submitted To</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'><label>lblsubmittedto</label></td>
            //        <td valign='top'>&nbsp;</td>
            //        <td align='right' valign='top'><strong>Date</strong>:&nbsp;</td>
            //        <td valign='top'>lblDate</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='2' align='right' valign='top'><strong>Home Phone</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'><label>lblhousePhone</label></td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' align='right' valign='top'>&nbsp;</td>
            //        <td colspan='2' valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td align='right' valign='top'>&nbsp;</td>
            //        <td align='right' valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' align='right' valign='top'><strong>Street Address</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'><label>lblAddress</label></td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='2' align='right' valign='top'><strong>Cell Phone</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'>lblCell</td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' align='right' valign='top'>&nbsp;</td>
            //        <td colspan='2' valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td align='right' valign='top'>&nbsp;</td>
            //        <td align='right' valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' align='right' valign='top'><strong>City, State &amp; Zip Code</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'>lblcitystatezip</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='2' align='right' valign='top'><strong>Email</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'><label>lblemail</label></td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' valign='top'>&nbsp;</td>
            //        <td colspan='2' valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='3' valign='top'>&nbsp;</td>
            //        <td colspan='2' valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td valign='top'>&nbsp;</td>
            //        <td colspan='2' align='right' valign='top'><strong>Job Location</strong>:&nbsp;</td>
            //        <td colspan='2' valign='top'>lblJobLocation</td>
            //        <td valign='top'>&nbsp;</td>
            //      </tr>
            //      <tr>
            //        <td colspan='15' valign='top'><hr color='#000000' width='450' /></td>
            //      </tr>";

            return header;
        }

        public string ContractPdfBody()
        {
            string body = @"<tr>
        <td valign='top'>&nbsp;</td>
        <td colspan='13' align='justify'>
        <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
<tbody>
<tr>    
<td> 
<table width='100%' cellspacing='0' cellpadding='0' border='0'>      
<tbody>
<tr>  
<td colspan='5'><b><u>Proposal A:</u></b> To supply and install ( lblQuantity ) pair(s) of custom made Mid America (lblStyle) (lblColor)shutters. The shutters are to consist of a heavy duty vinyl. Remove and haul away old shutters and debris. Job location:(lblJobLocation)  </td></tr> </tbody> </table>
<table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'> 
<tbody>
<tr>
<td colspan='3'><img src=" + url + @"/img/ma.png width='100px' alt='' style='width: 243px; height: 46px;' /></td>
<td colspan='2'> <b>Lifetime manufacturer’s warranty</b><br />
<br />
<b> Two year labor warranty</b><br /><br />
<b> $  lblProposalAmtA</b><br />
<br />
<b> Per month:  6%</b><br />
</td></tr> </tbody> </table><br />
<br />
<table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
<tbody>
<tr>
<td colspan='5'><b><u>Proposal B:</u></b> To supply and install ( lblQuantity ) pair(s) of generic plastic (lblStyle) (lblColor)shutters. Remove and haul away old shutters and debris. Job location:(lblJobLocation)</td></tr> </tbody> </table><br />
<br />
<table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'> 
<tbody>
<tr>
<td colspan='3'></td>
<td colspan='2'> 
<b> $  lblProposalAmtB</b><br />
<br />
<b> Per month:  6%</b><br />
</td></tr> </tbody> </table> <br />
<br />
<table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
<tbody>
<tr>
<td valign='top'><span style='font-weight: bold;'>&nbsp;Special Instructions:</span> lblSpecialInstructions,</td>

<td valign='top'><span style='font-weight: bold;'>&nbsp;Work Area:</span> &nbsp;lblWorkArea, </td>

<td valign='top'>&nbsp;<span style='font-weight: bold;'>Shutter Tops:</span> lblShutterTops</td></tr> </tbody> 
</table> </td></tr></tbody></table><br />
        </td>
        <td valign='top'>&nbsp;</td>
      </tr>";

            return body;
        }


        public string ContractPdfFooter()
        {
            string Footer = @"</table><table align='center' width='100%' bordercolor='#666666' bgcolor='#FFFFFF' border='0' class='no_line' cellspacing='0' cellpadding='0' style='font-family:Verdana, Geneva, sans-serif; font-size:8pt;'>  
<tr>    
<td colspan='2'>
<table width='100%' border='0' cellspacing='10' cellpadding='0'>  
<tr>    
<td width='45%' height='30' valign='top' style='text-align:justify'>*Standard JM Grove projects start approximately: 2-8 weeks (depending on job type, size) from finalized material list and/or deposits. And are normally substantially completed by: (undeterminable)  Act of god, Weather, labor shortages, supplies availability, Custom orders, un-forseen labor/material and but not limited to change orders can all cause delays and cause undeterminable extended turn-around time. All following material & labor is furnished & installed by J.M. Grove Construction unless otherwise specified. J.M. Grove strongly recommends supplying and installing ALL material to prevent costly delays, “owner” is responsible forall delays caused by supplying incorrect material at Time & Material basis. The above prices, specifications and conditions are satisfactory and are hereby accepted. You are authorized to do the work as specified. Any equipment, material, or labor designated as “OWNERS” responsibility must be on the job site, uncrated and inspected and approved on the day the project begins. Client supplied material used will not be warrantied by J.M. Grove Construction. Payment will be made as outlined below. Please sign and return white copy if proposal is accepted. Review reverse side for terms and conditions</td>    
<td width='45%' valign='top' style='text-align:justify'>Any removal or correction of any concealed wall, ceiling or floor obstruction ,decay, pipe, ducting, additional wood or metal, additional un-forseen materials &labor needed to complete a job, Any required state or local permit ordinances,  Testing or Analysis or Remediation Removal or Repair of – Radon – Lead – Asbestos – Mold or any other like substances etc. revealed during construction, is not included in this contract. If this type of situation occurs, it will be reviewed with the client, and a separate price will be quoted on that part of the project or billed at a Time & Material rate of: (Mechanic Rate=$90/hr and/or Helper/Painter Rate =$70/hr). Payment for the extra work will be paid in full prior to the start of work or the original specifications will be performed. All changes will generally increase the time it takes to complete the project. Change Order Forms must be signed by both J.M. Grove Construction and client.      <p align='justify'>&nbsp;</p>      <p align='right' style='font-weight:bold'>Customer:X_____________________________</p></td>  
</tr>
</table>
</td>
</tr>
</table>

<table width='100%' border='0' cellspacing='10' cellpadding='0' style='font-family:Verdana, Geneva, sans-serif; font-size:8pt;>  
<tr>    
<td valign='top'>    <br /><br />	<strong>Acceptance of Proposal:</strong>       
Registration #:PA092750      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            Attorney General 717-787-3391<br />        
We  propose hereby to furnish material and labor – complete in accordance with abovespecifications, for the sum of:$ lblTotalAmount <br /> <br />      
<strong>Payment to be made as follows:</strong>   <br />    
1/3 Down Payment:$ lblamountpart1 ,1/3 Due upon scheduling:$&nbsp;lblamountpart2 ,1/3 Due upon majority completion:$&nbsp;lblamountpart3<br />
Date:&nbsp;lblFooterDate &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Authorized Signature:__________________________<br />		
Customer Name (Printed):&nbsp; lblCustomerName &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     Customer Signature:__________________________<br />
</td>
    </tr> 
</table>

<table width='100%' border='0' cellspacing='0' cellpadding='0'>      
        <tr>
            <td  valign='top'><img src=" + url + @"/img/ContractFooter.png width='550px' alt='' height='25px' /></td>
        </tr>
        </table>

<p>&nbsp;</p>      
<table align='center' width='100%' bgcolor='#FFFFFF' bordercolor='#666666' border='0'  cellspacing='0' cellpadding='0' 
style='font-family: Verdana, Geneva, sans-serif;          font-size: 8pt;'>          
<tr>              
<td align='center' style='text-align: justify;'>
                  <strong>Terms &amp; Conditions</strong>                  </td>
<td>&nbsp;</td>
</tr>
<tr>
<td>1.</td> 
<td>The Party executing as customer (“Buyer”) represents to the company that the Buyer is the owner of the “real property” described in the job address (“Property”). The buyer represents to the company that the buyer has the ability to authorize the specifications and work to be completed on the property. The Buyer is solely responsible to ensure that the specifications in this agreement do not violate (a) any building covenants to which buyer is subject, (b) any building code requirements, (c) any home owner/condominium association requirements, or (d) any other third party requirements affecting the property (all four collectively referred to as “Third Party Requirements”). If any third party attempts to impose third party requirements on the company, the company may notify such third party of the buyer’s representations herein. If the company is damaged by the third party requirements, including without limitation additional costs to complete the specifications, the buyer shall be responsible for reimbursing the company for such damage.</td></tr>
<tr>
<td>2.</td> 
<td>In the event that the buyer shall sell or otherwise dispose of the property, file or be subject to the authority of any bankruptcy court, allow a judgment and or lien to be registered against the property, the unpaid balance due or to be due here under shall immediately become due and payable, regardless of the terms of any other document executed between the company and buyer to the contrary.</td></tr>
<tr>
<td>3.</td> 
<td>The company shall not be responsible for any damage, or delay due to strike, fire, accident or other causes beyond their control. <strong></strong>Changes in field measurements and job site conditions, may necessitate on site adjustments, which JMG will use its best judgment to handle. Actual dimensions on site may vary from floor plan &amp; JM Grove reserves the right to cure by construction best standards. All surfaces of walls, ceilings, doors, windows, and woodwork (except those of factory made kitchen cabinetry) will be left unpainted and unfinished, unless specified otherwise. The owner recognizes that slight variations in finish, sheen, wood grain -knots etc, are inherent in wood and as such are considered acceptable in this project. Stone – granite – marble – limestone, etc, and any natural products have a wide range of color – movement – fissures - grain shade – crazing – indentations – etc, all of which are considered inherent in natural materials and are acceptable in this project. The client recognizes the above features and understands that J.M.Grove Construction will use its best judgment and industry standards to make your project as acceptable as possible. If specifications call for re-use of existing equipment, no responsibility on the part of JMG for appearance, function or service shall be implied. JMG calls your attention to limitations of matching materials such as drywall, plaster, paint, tile, roofing, wood trim, planes, etc., exact duplication is not promised and should not be expected. JMG shall have free access to work site for materials, trucks, men, dumpsters etc. Owner agrees to remove and protect any personal property in all rooms we will work in to include, window treatments, photos, etc. and outside including shrubs, flowers, etc. and JMG shall not be held responsible for damage to such items. </td>>/tr>
<tr>
<td>4.</td> 
<td>This agreement does not include any additional work other than expressly specified herein. Any alteration or deviation from the specifications herein, involving extra costs or material or labor will become an extra charge in addition to the total contract price stated in this agreement. All modifications or additional agreements with respect to this agreement in favor of the buyer must be in writing and signed by the company. No oral statements by an agent or employee of the company are binding upon the company unless in writing and executed by an authorized agent of the company..</td></tr>
<tr>
<td>5.</td> 
<td>To the extent required or allowed by law or regulation, buyer will apply for all applicable state and local building and construction permits necessary for the installation (“permits”) as required under state laws or local ordinances. The company is not obligated to commence work until receipt of such permits. Upon execution of this                          agreement and notification of receipt of the permits, the company will arrange,                          in the ordinary course of business. No representation, whether oral or in writing,                          regarding a projected starting and ending date for the installation will be enforceable                          against the company. The actual cost of the permits will be paid separately by the                          client, when obtained. It is the responsibility of the homeowner to have the home                          open and someone home on the days of inspections. Additional work, testing or inspection                          time required by the code official will be billed as extra under this contract,                          to be paid for prior to starting extra work.</td>                     </tr>
<tr>
<td>6.</td>
<td>If the buyer reschedules any installation, without at least twenty-four (24) hours                          prior notice, the buyer will be responsible to the company and its agents for any                          damages arising from such delay.</td>                     </tr>
<tr>
<td>7.</td>
<td>The buyer acknowledges that, by signing this agreement, the company will incur costs                          in anticipation of performance of their obligations hereunder. The buyer acknowledges                          that the exact amount of such costs is extremely difficult and impracticable to                          determine with any degree of certainty. Therefore, in the event the buyer breaches                          this agreement before the company commences their performance under this agreement,                          the buyer agrees to pay the company thirty-three percent (33%) of the total contract                          price as liquidated damages. The parties agree that this charge represents a fair.                          And reasonable estimate of the costs that the company will incur, before commencing                          their performance of their obligations under this agreement. After the company has                          commenced installation of the specifications, the buyer agrees to be responsible                          for total contract price and for all other damages incurred by the company resulting                          from the buyer’s breach of this agreement. The company does not waive any right                          to pursue any damages against the buyer, even if the company accepts partial payment.                          The company reserves all rights to pursue any legal rights and remedies available                          to the company. </td>                     </tr>
<tr>
<td>8.</td>
<td>This agreement shall become binding upon the company upon acceptance by the company                          either in writing or by commencing performance hereunder. </td>                      </tr>
<tr>
<td>9.</td>
<td>If any specification and/or any term and condition of this agreement is determined                          to be invalid or unenforceable, those unenforceable/invalid specifications and/or                          terms and conditions shall be deemed to be severable from the remainder of this                          agreement and shall not cause the invalidity or unenforceability of the remainder                          of this agreement.</td>                     </tr>
<tr>
<td>10.</td>
<td>The company may reject this agreement by providing written notice of termination                          to buyer within three (3) days of buyer’s execution of this agreement. </td>                     </tr>
<tr>
<td>11.</td>
<td>The buyer agrees to allow the company and its agents access to the property, as                          deemed necessary by the company, to complete the specifications, to inspect the                          completed job, and to investigate complaints regarding the installation and to address                          such allegations as the company deems appropriate. The buyer further agrees that                          before the buyer files any complaint with any governmental agency or business entity,                          the buyer shall allow the company every reasonable opportunity to remedy the buyer’s                          complaint. If the buyer fails to provide the company with access requested by the                          company, buyer waives and releases the company and its agents of any liabilities                          or damages arising from this agreement, whether arising from the complaint or otherwise</td>                     </tr>
<tr>
<td>12.</td>
<td>The buyer authorizes the company to obtain or exchange any personal and/or credit                          information with any agent towards establishing or verifying the buyer’s financial                          status. </td>                     </tr>
<tr>
<td>13.</td>
<td>The buyer fully understands that the total contract price is due and payable in                          full immediately upon majority completion of the specifications in this agreement.                          The only exception is if the total contract price has been financed through a third                          party lending institution or a separately agreed upon customer payment plan: in                          which case, the buyer agrees to sign all necessary papers and provide all stipulations                          required by such lending institution immediately on request. If the buyer refuses                          to sign the paper work or provide the stipulations then interest shall accrue, at                          the rate of 2 percent per month, upon the company completing the specifications,                          and will be added to the total contract price remaining due and unpaid.</td>                      </tr>
<tr>
<td>14.</td>
<td>Amounts past-due hereunder shall incur a late fee equal to 1.5 percent per month                          of the portion of the total contract price remaining due and unpaid each month thereafter                          until the total contract price is paid in full. The company shall be entitled to                          an award of its attorney’s fees in connection with any legal action brought to collect                          any amounts due and owing hereunder to cover the extra administrative and other                          costs.</td>                      </tr>
<tr>
<td>15.</td>
<td>Any controversy or claim arising out of or relating to this contract or the breach                          thereof, shall be settled by arbitration pursuant to the AAA rules and judgment                          on the award rendered by the arbitrators</td>                     </tr>
<tr>
<td>16.</td>
<td>The company has a policy of continued improvement and reserves the right to modify                          its products as displayed in any advertisements or sales material, from time to                          time as it deems necessary without notice or liability to the buyer. </td>                     </tr>
<tr>
<td>17.</td>
<td>The parties agree that the courts of the state of Pennsylvania or the courts of                          the United States located in the Montgomery County , shall have the sole and exclusive                          jurisdiction to entertain any action between the parties hereto and the parties                          hereto waive any and all objections to venue being in the state courts located in                          Montgomery County (and agree that the sole venue for such challenges shall be Montgomery                          County, Pa)</td>                      </ttr>
<tr>
<td>18.</td>
<td>Both the sales contractor executing this agreement on behalf of the company and                          the installations contractor performing the specifications under this agreement                          are independent contractors’ of the company and are not employees of the company                      </td>                      </tr>
<tr>
<td>19.</td>
<td>Buyer hereby indemnifies the company from any damages arising from buyer’s breach                          of any representation, covenants or obligation hereunder, or including without limitation                          reasonable attorneys fees and costs. Buyer represents to the company that buyer                          has no knowledge of any condition or fact regarding the property which might pose                          a danger to the company or its agents or that might make the installation more difficult                          or expensive.</td>                     </tr>
<tr>
<td>20.</td>
<td>The express warranties, if any, contained herein are in lieu of all other warranties,                          either expressed or implied, including without limitation any implied warranty of                          merchantability or fitness for a particular purpose, and of any other obligation                          on the part of manufacturer.</td>                      </tr>
<tr>
<td>21.</td>
<td>Upon completion of the installation, the buyer shall inspect the installation and                          either execute a reasonable acceptance and satisfaction, in a form satisfactory                          to the company, or notify the company of any objections or complaints. If buyer                          fails to execute acceptance and satisfaction and to notify the company of any objections                          or complaints within three (3) days of installation, the buyer will waive any claims                          against the company, and the company shall have no duty to address any objections                          or complaints. </td>                      </tr>
<tr>
<td>22.</td>
<td>This agreement constitutes the entire understanding of the parties with respect                          to the subject matter herein and supersedes any oral statements. The rights, obligations,                          and interests of the parties may not be changed, modified or amended except by written                          agreement of the parties hereto, including without limitation by oral statements                          of the sales consultant contractor or any other person or entity</td>                 </tr>
<tr>
<td colspan='2'>   <p align='center'><strong>Right of Rescission/Notice of Cancellation</strong></p>       </td> </tr>
<tr>
<td colspan='2'>You may cancel this transaction, without any obligation, within three (3) business                  days from the date this agreement was signed. However, you may not cancel if you                  have requested the seller to provide goods or services without delay because of                  an emergency and: The company in good faith makes a substantial beginning of performance                  of the Agreement before you give notice of cancellation; and. In the case of goods,                  the goods cannot be returned to the Company in substantially as good condition as                  when received by the Buyer. If you cancel, any property traded in, any payments                  made by you under the contract of sale, and any negotiable instrument executed by                  you will be returned within fifteen (15) business days following receipt by the                  company of your cancellation notice, and any security interest arising out of the                  transaction will be cancelled. If you cancel, you must make available to the company                  at your residence, in substantially as good condition as when received and goods                  delivered to you under this agreement. If you fail to make the goods available to                  the company, or if you agree to return the goods to the company and fail to do so,                  then you remain liable for performance of all obligations under this agreement.              </td></tr>

</table>


</td>
</tr>
</table>
";

            return Footer;
        }

        //        public string ContractPdfFooter()
        //        {
        //            string Footer = @"<tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top'><p>We propose hereby to furnish material and labor - complete in accordance with above specifications, for the sum of:</p></td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='5' valign='top'>Any building permits, additional wood, metal,  <br>
        //          mandated EPA lead containment requirements or  <br>
        //          additional un-forseen materials &amp; labor needed  <br>
        //          to complete a job will be at an additional  <br>
        //          charge. Review reverse side for terms and <br>
        //          conditions.</td>
        //        
        //        <td colspan='7' valign='top'><table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
        //          <tr>
        //            <td align='right'>$</td>
        //            <td><label>lblTotalAmount</label></td>
        //            </tr>
        //          <tr>
        //            <td></td>
        //            <td style='border-top:#000 1px solid;'><hr color='#000000' width='100%' /></td>
        //            </tr>
        //          <tr>
        //            <td></td>
        //            <td align='right'><strong>Payment to be made as follows</strong></td>
        //            </tr>
        //          <tr>
        //            <td align='right'>1/3 Down Payment:&nbsp;$</td>
        //            <td><label>lblamountpart1</label></td>
        //            </tr>
        //          <tr>
        //            <td></td>
        //            <td><hr color='#000000' width='100%' /></td>
        //            </tr>
        //          <tr>
        //            <td align='right'> 1/3 Due upon scheduling:&nbsp; </td>
        //            <td><label>$ lblamountpart2</label></td>
        //            </tr>
        //          <tr>
        //            <td></td>
        //            <td><hr color='#000000' width='100%' /></td>
        //            </tr>
        //          <tr>
        //            <td align='right'>1/3 Due upon majority completion:&nbsp; </td>
        //            <td><label>$ lblamountpart3</label></td>
        //            </tr>
        //          <tr>
        //            <td></td>
        //            <td><hr color='#000000' width='100%' /></td>
        //            </tr>
        //          </table></td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top'><strong>The Attorney General 717-787-3391</strong></td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top'>Registration #:PA092750</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top'>Acceptance of Proposal</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='11' valign='top'>The above prices, specifications and conditions are satisfactory and are hereby accepted. You are authorized to do the work as specified. Payment will be made as outlined above. Please sign and return white copy if proposal is accepted.</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='3' valign='top'>Authorized Signature:</td>
        //        <td colspan='4' valign='top'><label>lblAuthorizedSignature</label></td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='2' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'></td>
        //        <td valign='top'></td>
        //        <td colspan='3' valign='top'></td>
        //        <td colspan='4' valign='top'><hr color='#000000' width='100%' /></td>
        //        <td valign='top'></td>
        //        <td valign='top'></td>
        //        <td colspan='2' valign='top'></td>
        //        <td valign='top'></td>
        //        <td valign='top'></td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='3' valign='top'>Customer Name (Printed):</td>
        //        <td colspan='4' valign='top'><label>lblCustomerName</label></td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>Date:</td>
        //        <td colspan='2' valign='top'><label>lbldate</label></td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //     <tr>
        //       <td valign='top'></td>
        //       <td valign='top'></td>
        //       <td colspan='3' valign='top'></td>
        //       <td colspan='4' valign='top'><hr color='#000000' width='100%' /></td>
        //       <td valign='top'></td>
        //       <td valign='top'></td>
        //       <td colspan='2' valign='top'><hr color='#000000' width='100%' /></td>
        //       <td valign='top'></td>
        //       <td valign='top'></td>
        //     </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='3' valign='top'>Customer Signature:</td>
        //        <td colspan='4' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='2' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'></td>
        //        <td valign='top'></td>
        //        <td colspan='3' valign='top'></td>
        //        <td colspan='4' valign='top'><hr color='#000000' width='100%' /></td>
        //        <td valign='top'></td>
        //        <td valign='top'></td>
        //        <td colspan='2' valign='top'></td>
        //        <td valign='top'></td>
        //        <td valign='top'></td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><img src=" + url + @"/img/footer1.png width='500' /></td>
        //  </tr>
        //  <tr>
        //    <td>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:6pt;'>
        //      <tr>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //        <td width='6%' valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top'><strong>Terms &amp; Conditions</strong></td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='right' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='11' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>1.</td>
        //        <td colspan='12' valign='top' align='justify'>The party executing as customer (“Buyer”) represents to the company that the Buyer is the owner of the “real property” described in the job address (“Property”). The buyer represents to the company that the buyer has the ability to authorize the specifications and work to be completed on the property. The Buyer is solely responsible to ensure that the specifications in this agreement do not violate (a) any building covenants to which buyer is subject, (b) any building code requirements, (c) any home owner/condominium association requirements, or (d) any other third party requirements affecting the property (all four collectively referred to as “Third Party Requirements”). If any third party attempts to impose third party requirements on the company, the company may notify such third party of the buyer’s representations herein. If the company is damaged by the third party requirements, including without limitation additional costs to complete the specifications, the buyer shall be responsible for reimbursing the company for such damage.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>2.</td>
        //        <td colspan='12' valign='top' align='justify'>In the event that the buyer shall sell or otherwise dispose of the property, file or be subject to the authority of any bankruptcy court, allow a judgment and or lien to be registered against the property, the unpaid balance due or to be due here under shall immediately become due and payable, regardless of the terms of any other document executed between the company and buyer to the contrary.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>3.</td>
        //        <td colspan='12' valign='top'>The company shall not be responsible for any damage, or delay due to strike, fire, accident or other causes beyond their control.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>4.</td>
        //        <td colspan='12' valign='top' align='justify'>This agreement does not include any additional work other than expressly specified herein. Any alteration or deviation from the specifications herein, involving extra costs or material or labor will become an extra charge in addition to the total contract price stated in this agreement. All modifications or additional agreements with respect to this agreement in favor of the buyer must be in writing and signed by the company. No oral statements by an agent or employee of the company are binding upon the company unless in writing and executed by an authorized agent of the company.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>5.</td>
        //        <td colspan='12' valign='top' align='justify'>To the extent required or allowed by law or regulation, buyer will apply for all applicable state and local building and construction permits necessary for the installation (“permits”) as required under state laws or local ordinances. The company is not obligated to commence work until receipt of such permits. Upon execution of this agreement and notification of receipt of the permits, the company will arrange, in the ordinary course of business. No representation, whether oral or in writing, regarding a projected starting and ending date for the installation will be enforceable against the company.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>6.</td>
        //        <td colspan='12' valign='top' align='justify'>If the buyer reschedules any installation, without at least twenty-four (24) hours prior notice, the buyer will be responsible to the company and its agents for any damages arising from such delay.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>7.</td>
        //        <td colspan='12' valign='top' align='justify'>The buyer acknowledges that, by signing this agreement, the company will incur costs in anticipation of performance of their obligations hereunder. The buyer acknowledges that the exact amount of such costs is extremely difficult and impracticable to determine with any degree of certainty. Therefore, in the event the buyer breaches this agreement before the company commences their performance under this agreement, the buyer agrees to pay the company thirty-three percent (33%) of the total contract price as liquidated damages. The parties agree that this charge represents a fair. And reasonable estimate of the costs that the company will incur, before commencing their performance of their obligations under this agreement. After the company has commenced installation of the specifications, the buyer agrees to be responsible for total contract price and for all other damages incurred by the company resulting from the buyer’s breach of this agreement. The company does not waive any right to pursue any damages against the buyer, even if the company accepts partial payment. The company reserves all rights to pursue any legal rights and remedies available to the company.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>8.</td>
        //        <td colspan='12' valign='top' align='justify'>This agreement shall become binding upon the company upon acceptance by the company either in writing or by commencing performance hereunder.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>9.</td>
        //        <td colspan='12' valign='top' align='justify'>If any specification and/or any term and condition of this agreement is determined to be invalid or unenforceable, those unenforceable/invalid specifications and/or terms and conditions shall be deemed to be severable from the remainder of this agreement and shall not cause the invalidity or unenforceability of the remainder of this agreement.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>10.</td>
        //        <td colspan='12' valign='top' align='justify'>The company may reject this agreement by providing written notice of termination to buyer within three (3) days of buyer’s execution of this agreement.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>11.</td>
        //        <td colspan='12' valign='top' align='justify'>The buyer agrees to allow the company and its agents access to the property, as deemed necessary by the company, to complete the specifications, to inspect the completed job, and to investigate complaints regarding the installation and to address such allegations as the company deems appropriate. The buyer further agrees that before the buyer files any complaint with any governmental agency or business entity, the buyer shall allow the company every reasonable opportunity to remedy the buyer’s complaint. If the buyer fails to provide the company with access requested by the company, buyer waives and releases the company and its agents of any liabilities or damages arising from this agreement, whether arising from the complaint or otherwise</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>12.</td>
        //        <td colspan='12' valign='top' align='justify'>The buyer authorizes the company to obtain or exchange any personal and/or credit information with any agent towards establishing or verifying the buyer’s financial status.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>13.</td>
        //        <td colspan='12' valign='top' align='justify'>The buyer fully understands that the total contract price is due and payable in full immediately upon completion of the specifications in this agreement. The only exception is if the total contract price has been financed through a third party lending institution or a separately agreed upon customer payment plan: in which case, the buyer agrees to sign all necessary papers and provide all stipulations required by such lending institution immediately on request. If the buyer refuses to sign the paper work or provide the stipulations then interest shall accrue, at the rate of 2 percent per month, upon the company completing the specifications, and will be added to the total contract price remaining due and unpaid.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>14.</td>
        //        <td colspan='12' valign='top' align='justify'>Amounts past-due hereunder shall incur a late fee equal to 1.5 percent per month of the portion of the total contract price remaining due and unpaid each month thereafter until the total contract price is paid in full. The company shall be entitled to an award of its attorney’s fees in connection with any legal action brought to collect any amounts due and owing hereunder to cover the extra administrative and other costs.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>15.</td>
        //        <td colspan='12' valign='top' align='justify'>Any controversy or claim arising out of or relating to this contract or the breach thereof, shall be settled by arbitration pursuant to the AAA rules and judgment on the award rendered by the arbitrators</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>16.</td>
        //        <td colspan='12' valign='top' align='justify'>The company has a policy of continued improvement and reserves the right to modify its products as displayed in any advertisements or sales material, from time to time as it deems necessary without notice or liability to the buyer.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>17.</td>
        //        <td colspan='12' valign='top' align='justify'>The parties agree that the courts of the state of Pennsylvania or the courts of the United States located in the Montgomery County , shall have the sole and exclusive jurisdiction to entertain any action between the parties hereto and the parties hereto waive any and all objections to venue being in the state courts located in Montgomery County (and agree that the sole venue for such challenges shall be Montgomery County, Pa)</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>18.</td>
        //        <td colspan='12' valign='top' align='justify'>Both the sales contractor executing this agreement on behalf of the company and the installations contractor performing the specifications under this agreement are independent contractors’ of the company and are not employees of the company</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>19.</td>
        //        <td colspan='12' valign='top' align='justify'>Buyer hereby indemnifies the company from any damages arising from buyer’s breach of any representation, covenants or obligation hereunder, or including without limitation reasonable attorneys fees and costs. Buyer represents to the company that buyer has no knowledge of any condition or fact regarding the property which might pose a danger to the company or its agents or that might make the installation more difficult or expensive.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>20.</td>
        //        <td colspan='12' valign='top' align='justify'>The express warranties, if any, contained herein are in lieu of all other warranties, either expressed or implied, including without limitation any implied warranty of merchantability or fitness for a particular purpose, and of any other obligation on the part of manufacturer.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>21.</td>
        //        <td colspan='12' valign='top' align='justify'>Upon completion of the installation, the buyer shall inspect the installation and either execute a reasonable acceptance and satisfaction, in a form satisfactory to the company, or notify the company of any objections or complaints. If buyer fails to execute acceptance and satisfaction and to notify the company of any objections or complaints within three (3) days of installation, the buyer will waive any claims against the company, and the company shall have no duty to address any objections or complaints.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>&nbsp;</td>
        //        <td colspan='12' valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td align='center' valign='top'>22.</td>
        //        <td colspan='12' valign='top' align='justify'>This agreement constitutes the entire understanding of the parties with respect to the subject matter herein and supersedes any oral statements. The rights, obligations, and interests of the parties may not be changed, modified or amended except by written agreement of the parties hereto, including without limitation by oral statements of the sales consultant contractor or any other person or entity</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' align='center' valign='top'><strong>Right of Rescission/Notice of Cancellation</strong></td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td colspan='13' valign='top' align='justify'>You may cancel this transaction, without any obligation, within three (3) business days from the date this agreement was signed. However, you may not cancel if you have requested the seller to provide goods or services without delay because of an emergency and: The company in good faith makes a substantial beginning of performance of the Agreement before you give notice of cancellation; and. In the case of goods, the goods cannot be returned to the Company in substantially as good condition as when received by the Buyer. If you cancel, any property traded in, any payments made by you under the contract of sale, and any negotiable instrument executed by you will be returned within fifteen (15) business days following receipt by the company of your cancellation notice, and any security interest arising out of the transaction will be cancelled. If you cancel, you must make available to the company at your residence, in substantially as good condition as when received and goods delivered to you under this agreement. If you fail to make the goods available to the company, or if you agree to return the goods to the company and fail to do so, then you remain liable for performance of all obligations under this agreement.</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //        <td valign='top'>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td>&nbsp;</td>
        //  </tr>
        //</table>";

        //            return Footer;
        //        }
        public string InstallerInvoicePDF()
        {
            string invoice = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
              <tr>
                <td>
            
            <table width='100%' border='0' cellspacing='0' cellpadding='0'>      
                  <tr>
                    <td colspan='6' valign='top'><img src=" + url + @"/img/header001.png width='300px' alt='' /></td>
                    <td colspan='4' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:6px;'>
                      <tr>
                        <td>PHONE: (215) 274-5182 <br />
                      220 KRAMS AVENUE <br />
                      MANAYUNK, PA 19127 <br />
                      SERVICE-SALES@JMGROVECONSTRUCTION.COM <br />
                      <span style='color:#e51b24'>WWW.JMGROVE CONSTRUCTION.COM</span></td>
                      </tr>
                      <tr>
                        <td>
            
            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
              <tr>
                <td>
            
            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                  <tr>
                    <td><a href='#'><img src=" + url + @"/img/facebook.png width='10' vspace='10' border='0' alt='' /></a></td>
                    <td><a href='#'><img src=" + url + @"/img/twitter.png width='10' hspace='10' vspace='10' border='0' alt='' /></a></td>
                    <td><a href='#'><img src=" + url + @"/img/googleplus.png width='10' vspace='10' border='0' alt='' /></a></td>
                    <td><a href='#'><img src=" + url + @"/img/youtube.png width='30' hspace='10' vspace='10' alt='' border='0' /></a></td>
                  </tr>
                </table>
            
            </td>
                <td>&nbsp;</td>
              </tr>
            </table>
            
            </td>
                      </tr>
                    </table>
            
            
            </td>
                    </tr>
                </table>
            
            </td>
              </tr>
              <tr>
                <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>      
                  <tr><td><img src=" + url + @"/img/header002.png width='520px' alt='' />
            </td>
            </tr>
            </table>
            
            </td>
              </tr>
              <tr>
                <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
                  
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' align='right' valign='top'><strong>Submitted To</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'><label>Submitted To</label></td>
                    <td valign='top'>&nbsp;</td>
                    <td align='right' valign='top'><strong>Date</strong>:&nbsp;</td>
                    <td valign='top'>Date</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='2' align='right' valign='top'><strong>Home Phone</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'><label>Phone</label></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' align='right' valign='top'>&nbsp;</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td align='right' valign='top'>&nbsp;</td>
                    <td align='right' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' align='right' valign='top'><strong>Street Address</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'><label>Address</label></td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='2' align='right' valign='top'><strong>Cell Phone</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'>Cell</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' align='right' valign='top'>&nbsp;</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td align='right' valign='top'>&nbsp;</td>
                    <td align='right' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' align='right' valign='top'><strong>City, State &amp; Zip Code</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'>ascd</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='2' align='right' valign='top'><strong>Email</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'><label>email</label></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' valign='top'>&nbsp;</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' valign='top'>&nbsp;</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='2' align='right' valign='top'><strong>Job Description</strong>:&nbsp;</td>
                    <td colspan='2' valign='top'>Description</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td colspan='15' valign='top'><hr color='#000000' width='450' /></td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' align='justify'>
                    <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
            <tbody>
            <tr>    
            <td> 
            <table width='100%' cellspacing='0' cellpadding='0' border='0'>      
            <tbody>
            <tr>  
            <td colspan='5'><b><u>Proposal A:</u></b> To supply and install ( lblQuantity ) pair(s) of custom made Mid America (lblStyle) (lblColor)shutters. The shutters are to consist of a heavy duty vinyl. Remove and haul away old shutters and debris. Job location:(lblJobLocation)  </td></tr> </tbody> </table>
            <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'> 
            <tbody>
            <tr>
            <td colspan='3'><img src=" + url + @"/img/ma.png width='100px' style='width: 243px; height: 46px;' alt='' /></td>
            <td colspan='2'> <b>Lifetime manufacturer’s warranty</b><br />
            <br />
            <b> Two year labor warranty</b><br /><br />
            <b> $  lblProposalAmtA</b><br />
            <br />
            <b> Per month:  6%</b><br />
            </td></tr> </tbody> </table><br />
            <br />
            <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
            <tbody>
            <tr>
            <td colspan='5'><b><u>Proposal B:</u></b> To supply and install ( lblQuantity ) pair(s) of generic plastic (lblStyle) (lblColor)shutters. Remove and haul away old shutters and debris. Job location:(lblJobLocation)</td></tr> </tbody> </table><br />
            <br />
            <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'> 
            <tbody>
            <tr>
            <td colspan='3'></td>
            <td colspan='2'> 
            <b> $  lblProposalAmtB</b><br />
            <br />
            <b> Per month:  6%</b><br />
            </td></tr> </tbody> </table> <br />
            <br />
            <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
            <tbody>
            <tr>
            <td valign='top'><span style='font-weight: bold;'>&nbsp;Special Instructions:</span> lblSpecialInstructions,</td>
            
            <td valign='top'><span style='font-weight: bold;'>&nbsp;Work Area:</span> &nbsp;lblWorkArea, </td>
            
            <td valign='top'>&nbsp;<span style='font-weight: bold;'>Shutter Tops:</span> lblShutterTops</td></tr> </tbody> 
            </table> </td></tr></tbody></table><br />
                    </td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top'><p>We propose hereby to furnish material and labor - complete in accordance with above specifications, for the sum of:</p></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='5' valign='top'>Any building permits, additional wood, metal,  <br>
                      mandated EPA lead containment requirements or  <br>
                      additional un-forseen materials &amp; labor needed  <br>
                      to complete a job will be at an additional  <br>
                      charge. Review reverse side for terms and <br>
                      conditions.</td>
                    
                    <td colspan='7' valign='top'><table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif; font-size: 7pt;'>  
                      <tr>
                        <td align='right'>$</td>
                        <td></td>
                        </tr>
                      <tr>
                        <td></td>
                        <td style='border-top:#000 1px solid;'><hr color='#000000' width='100%' /></td>
                        </tr>
                      <tr>
                        <td></td>
                        <td align='right'><strong>Payment to be made as follows</strong></td>
                        </tr>
                      <tr>
                        <td align='right'>1/3 Down Payment:&nbsp;$</td>
                        <td></td>
                        </tr>
                      <tr>
                        <td></td>
                        <td><hr color='#000000' width='100%' /></td>
                        </tr>
                      <tr>
                        <td align='right'> 1/3 Due upon scheduling:&nbsp; </td>
                        <td>$</td>
                        </tr>
                      <tr>
                        <td></td>
                        <td><hr color='#000000' width='100%' /></td>
                        </tr>
                      <tr>
                        <td align='right'>1/3 Due upon majority completion:&nbsp; </td>
                        <td>$</td>
                        </tr>
                      <tr>
                        <td></td>
                        <td><hr color='#000000' width='100%' /></td>
                        </tr>
                      </table></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top'><strong>The Attorney General 717-787-3391</strong></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top'>Registration #:PA092750</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top'>Acceptance of Proposal</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='11' valign='top'>The above prices, specifications and conditions are satisfactory and are hereby accepted. You are authorized to do the work as specified. Payment will be made as outlined above. Please sign and return white copy if proposal is accepted.</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' valign='top'>Authorized Signature:</td>
                    <td colspan='4' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'></td>
                    <td valign='top'></td>
                    <td colspan='3' valign='top'></td>
                    <td colspan='4' valign='top'><hr color='#000000' width='100%' /></td>
                    <td valign='top'></td>
                    <td valign='top'></td>
                    <td colspan='2' valign='top'></td>
                    <td valign='top'></td>
                    <td valign='top'></td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' valign='top'>Customer Name (Printed):</td>
                    <td colspan='4' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>Date:</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                 <tr>
                   <td valign='top'></td>
                   <td valign='top'></td>
                   <td colspan='3' valign='top'></td>
                   <td colspan='4' valign='top'><hr color='#000000' width='100%' /></td>
                   <td valign='top'></td>
                   <td valign='top'></td>
                   <td colspan='2' valign='top'><hr color='#000000' width='100%' /></td>
                   <td valign='top'></td>
                   <td valign='top'></td>
                 </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='3' valign='top'>Customer Signature:</td>
                    <td colspan='4' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='2' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'></td>
                    <td valign='top'></td>
                    <td colspan='3' valign='top'></td>
                    <td colspan='4' valign='top'><hr color='#000000' width='100%' /></td>
                    <td valign='top'></td>
                    <td valign='top'></td>
                    <td colspan='2' valign='top'></td>
                    <td valign='top'></td>
                    <td valign='top'></td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td><img src=" + url + @"/img/footer1.png width='500' alt='' /></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:6pt;'>
                  <tr>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                    <td width='6%' valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top'><strong>Terms &amp; Conditions</strong></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='right' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='11' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>1.</td>
                    <td colspan='12' valign='top' align='justify'>The party executing as customer (“Buyer”) represents to the company that the Buyer is the owner of the “real property” described in the job address (“Property”). The buyer represents to the company that the buyer has the ability to authorize the specifications and work to be completed on the property. The Buyer is solely responsible to ensure that the specifications in this agreement do not violate (a) any building covenants to which buyer is subject, (b) any building code requirements, (c) any home owner/condominium association requirements, or (d) any other third party requirements affecting the property (all four collectively referred to as “Third Party Requirements”). If any third party attempts to impose third party requirements on the company, the company may notify such third party of the buyer’s representations herein. If the company is damaged by the third party requirements, including without limitation additional costs to complete the specifications, the buyer shall be responsible for reimbursing the company for such damage.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>2.</td>
                    <td colspan='12' valign='top' align='justify'>In the event that the buyer shall sell or otherwise dispose of the property, file or be subject to the authority of any bankruptcy court, allow a judgment and or lien to be registered against the property, the unpaid balance due or to be due here under shall immediately become due and payable, regardless of the terms of any other document executed between the company and buyer to the contrary.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>3.</td>
                    <td colspan='12' valign='top'>The company shall not be responsible for any damage, or delay due to strike, fire, accident or other causes beyond their control.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>4.</td>
                    <td colspan='12' valign='top' align='justify'>This agreement does not include any additional work other than expressly specified herein. Any alteration or deviation from the specifications herein, involving extra costs or material or labor will become an extra charge in addition to the total contract price stated in this agreement. All modifications or additional agreements with respect to this agreement in favor of the buyer must be in writing and signed by the company. No oral statements by an agent or employee of the company are binding upon the company unless in writing and executed by an authorized agent of the company.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>5.</td>
                    <td colspan='12' valign='top' align='justify'>To the extent required or allowed by law or regulation, buyer will apply for all applicable state and local building and construction permits necessary for the installation (“permits”) as required under state laws or local ordinances. The company is not obligated to commence work until receipt of such permits. Upon execution of this agreement and notification of receipt of the permits, the company will arrange, in the ordinary course of business. No representation, whether oral or in writing, regarding a projected starting and ending date for the installation will be enforceable against the company.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>6.</td>
                    <td colspan='12' valign='top' align='justify'>If the buyer reschedules any installation, without at least twenty-four (24) hours prior notice, the buyer will be responsible to the company and its agents for any damages arising from such delay.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>7.</td>
                    <td colspan='12' valign='top' align='justify'>The buyer acknowledges that, by signing this agreement, the company will incur costs in anticipation of performance of their obligations hereunder. The buyer acknowledges that the exact amount of such costs is extremely difficult and impracticable to determine with any degree of certainty. Therefore, in the event the buyer breaches this agreement before the company commences their performance under this agreement, the buyer agrees to pay the company thirty-three percent (33%) of the total contract price as liquidated damages. The parties agree that this charge represents a fair. And reasonable estimate of the costs that the company will incur, before commencing their performance of their obligations under this agreement. After the company has commenced installation of the specifications, the buyer agrees to be responsible for total contract price and for all other damages incurred by the company resulting from the buyer’s breach of this agreement. The company does not waive any right to pursue any damages against the buyer, even if the company accepts partial payment. The company reserves all rights to pursue any legal rights and remedies available to the company.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>8.</td>
                    <td colspan='12' valign='top' align='justify'>This agreement shall become binding upon the company upon acceptance by the company either in writing or by commencing performance hereunder.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>9.</td>
                    <td colspan='12' valign='top' align='justify'>If any specification and/or any term and condition of this agreement is determined to be invalid or unenforceable, those unenforceable/invalid specifications and/or terms and conditions shall be deemed to be severable from the remainder of this agreement and shall not cause the invalidity or unenforceability of the remainder of this agreement.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>10.</td>
                    <td colspan='12' valign='top' align='justify'>The company may reject this agreement by providing written notice of termination to buyer within three (3) days of buyer’s execution of this agreement.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>11.</td>
                    <td colspan='12' valign='top' align='justify'>The buyer agrees to allow the company and its agents access to the property, as deemed necessary by the company, to complete the specifications, to inspect the completed job, and to investigate complaints regarding the installation and to address such allegations as the company deems appropriate. The buyer further agrees that before the buyer files any complaint with any governmental agency or business entity, the buyer shall allow the company every reasonable opportunity to remedy the buyer’s complaint. If the buyer fails to provide the company with access requested by the company, buyer waives and releases the company and its agents of any liabilities or damages arising from this agreement, whether arising from the complaint or otherwise</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>12.</td>
                    <td colspan='12' valign='top' align='justify'>The buyer authorizes the company to obtain or exchange any personal and/or credit information with any agent towards establishing or verifying the buyer’s financial status.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>13.</td>
                    <td colspan='12' valign='top' align='justify'>The buyer fully understands that the total contract price is due and payable in full immediately upon completion of the specifications in this agreement. The only exception is if the total contract price has been financed through a third party lending institution or a separately agreed upon customer payment plan: in which case, the buyer agrees to sign all necessary papers and provide all stipulations required by such lending institution immediately on request. If the buyer refuses to sign the paper work or provide the stipulations then interest shall accrue, at the rate of 2 percent per month, upon the company completing the specifications, and will be added to the total contract price remaining due and unpaid.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>14.</td>
                    <td colspan='12' valign='top' align='justify'>Amounts past-due hereunder shall incur a late fee equal to 1.5 percent per month of the portion of the total contract price remaining due and unpaid each month thereafter until the total contract price is paid in full. The company shall be entitled to an award of its attorney’s fees in connection with any legal action brought to collect any amounts due and owing hereunder to cover the extra administrative and other costs.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>15.</td>
                    <td colspan='12' valign='top' align='justify'>Any controversy or claim arising out of or relating to this contract or the breach thereof, shall be settled by arbitration pursuant to the AAA rules and judgment on the award rendered by the arbitrators</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>16.</td>
                    <td colspan='12' valign='top' align='justify'>The company has a policy of continued improvement and reserves the right to modify its products as displayed in any advertisements or sales material, from time to time as it deems necessary without notice or liability to the buyer.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>17.</td>
                    <td colspan='12' valign='top' align='justify'>The parties agree that the courts of the state of Pennsylvania or the courts of the United States located in the Montgomery County , shall have the sole and exclusive jurisdiction to entertain any action between the parties hereto and the parties hereto waive any and all objections to venue being in the state courts located in Montgomery County (and agree that the sole venue for such challenges shall be Montgomery County, Pa)</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>18.</td>
                    <td colspan='12' valign='top' align='justify'>Both the sales contractor executing this agreement on behalf of the company and the installations contractor performing the specifications under this agreement are independent contractors’ of the company and are not employees of the company</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>19.</td>
                    <td colspan='12' valign='top' align='justify'>Buyer hereby indemnifies the company from any damages arising from buyer’s breach of any representation, covenants or obligation hereunder, or including without limitation reasonable attorneys fees and costs. Buyer represents to the company that buyer has no knowledge of any condition or fact regarding the property which might pose a danger to the company or its agents or that might make the installation more difficult or expensive.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>20.</td>
                    <td colspan='12' valign='top' align='justify'>The express warranties, if any, contained herein are in lieu of all other warranties, either expressed or implied, including without limitation any implied warranty of merchantability or fitness for a particular purpose, and of any other obligation on the part of manufacturer.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>21.</td>
                    <td colspan='12' valign='top' align='justify'>Upon completion of the installation, the buyer shall inspect the installation and either execute a reasonable acceptance and satisfaction, in a form satisfactory to the company, or notify the company of any objections or complaints. If buyer fails to execute acceptance and satisfaction and to notify the company of any objections or complaints within three (3) days of installation, the buyer will waive any claims against the company, and the company shall have no duty to address any objections or complaints.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>&nbsp;</td>
                    <td colspan='12' valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td align='center' valign='top'>22.</td>
                    <td colspan='12' valign='top' align='justify'>This agreement constitutes the entire understanding of the parties with respect to the subject matter herein and supersedes any oral statements. The rights, obligations, and interests of the parties may not be changed, modified or amended except by written agreement of the parties hereto, including without limitation by oral statements of the sales consultant contractor or any other person or entity</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
            
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' align='center' valign='top'><strong>Right of Rescission/Notice of Cancellation</strong></td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td colspan='13' valign='top' align='justify'>You may cancel this transaction, without any obligation, within three (3) business days from the date this agreement was signed. However, you may not cancel if you have requested the seller to provide goods or services without delay because of an emergency and: The company in good faith makes a substantial beginning of performance of the Agreement before you give notice of cancellation; and. In the case of goods, the goods cannot be returned to the Company in substantially as good condition as when received by the Buyer. If you cancel, any property traded in, any payments made by you under the contract of sale, and any negotiable instrument executed by you will be returned within fifteen (15) business days following receipt by the company of your cancellation notice, and any security interest arising out of the transaction will be cancelled. If you cancel, you must make available to the company at your residence, in substantially as good condition as when received and goods delivered to you under this agreement. If you fail to make the goods available to the company, or if you agree to return the goods to the company and fail to do so, then you remain liable for performance of all obligations under this agreement.</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                  <tr>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                    <td valign='top'>&nbsp;</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
            </table>";
            return invoice;
        }
        public string InstallerWorkorderPDF()
        {
            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
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
        <td> Phone #s: lbl_phone</td>
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

";
            return workorder;
        }

        public string WorkorderpdfStage3()
        {
            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:'Times New Roman', Times, serif; font-size:10pt;'>
<tr>
 <td width='100' align='right'><img src=" + url + @"/img/JG-Logo.gif width='40' height='40' alt='' /></td>
</tr>
  <tr>
    <td width='668' align='center'><strong>INSTALLER WORK ORDER</strong></td>
   
  </tr>

  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
      <tr>
        <td width='10%' align='left'> <strong>Salesman:</strong></td>
        <td width='20%' align='left'>lbl_salesman</td>
        <td width='40%'>&nbsp;</td>
        <td width='10%' align='left'> <strong>Date Sold:</strong></td>
        <td width='20%' align='left'> lbl_date</td>
      </tr>
      <tr>
        <td align='left'> <strong>Cell #:</strong></td>
        <td align='left'>lbl_cell</td>
        <td>&nbsp;</td>
        <td align='left'> <strong>Email:</strong></td>
        <td align='left'>lbl_email</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan='2'><br></td>
  </tr>
  <tr>
    <td colspan='2'> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
  </tr>
  <tr>
    <td colspan='2'><br></td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='5' cellpadding='0' >
      <tr>
        <td width='50%'> <u><strong>Installer Info</strong></u></td>
        <td width='50%'> <u><strong>Customer Info</strong></u></td>
      </tr>
      <tr>
        <td> <strong>Installer: lbl_installer</strong></td>
        <td><strong>Job #: lbl_JobId</strong></td>
      </tr>
       <tr>
        <td><strong> Phone #: lbl_installerPhone</strong></td>
        <td><strong>Customer Name: lbl_CustomerName</strong></td>
      </tr>
      <tr>
        <td><strong> Disposal: lbl_disposal</strong></td>
        <td><strong>Customer Phone #s: lbl_Phone</strong></td>
       </tr>
        <tr>
        <td> <strong> Work Start Date: lbl_workStartDate</strong></td>
        <td><strong>Job Address: lbl_JobAddress</strong></td>
      </tr>
      <tr>
        <td><strong>Reschedule Date 1: lbl_rescheduleDate1</strong></td>
        <td><strong>Reschedule Date 2: lbl_rescheduleDate2</strong></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
      <tr>
        <td width='50%' align='center'><u><strong>Special Instructions:</strong></u></td>
        <td width='50%' align='center'><u><strong>Work Area:</strong></u></td>
      </tr>
      <tr>
        <td height='100' align='left'>lbl_specialinstructions</td>
        <td align='left'>lbl_workarea</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan='2'>&nbsp;</td>
  </tr>
 <tr>
    <td><strong>Proposal Terms : </strong>lbl_ProposalTerms</td>
  </tr>
 <tr>
    <td colspan='2'>&nbsp;</td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
      <tr>
        <td align='left'><u><strong>Materials Out</strong></u></td>
        <td align='left'><u><strong>Actual Installed</strong></u></td>
        <td align='left'><u><strong>Materials Returned</strong></u></td>
      </tr>
      <tr>
        <td height='200' align='left'>lbl_list</td>
        <td align='left'>&nbsp;</td>
        <td align='left'>&nbsp;</td>
      </tr>
    </table></td>
  </tr> 
  
    <tr>
    <td colspan='2'>  <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                        <tr><td>
    *Any additional work/ material  needed not listed on work order you must call project manager and/or  homeowners  for approval! Ex) rotted wood, extra layers, metal, permits, testing or un-forseen work etc.
</td>
                        </tr>
  <tr>
    <td colspan='2' align='right'>&nbsp;</td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
      <tr>
        <td width='16%' height='25'>Installer Signature:</td>
        <td width='34%' valign='bottom'><hr color='#000000' width='100%' /></td>
        <td width='17%' align='right'>Completion Date:</td>
        <td width='33%' valign='bottom'><hr color='#000000' width='100%' /></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
</table>
</td>
  </tr>
</table>";

            return workorder;
        }
        public string Workorderpdf()
        {
            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:'Times New Roman', Times, serif; font-size:10pt;'>
<tr>
 <td width='100' align='right'><img src=" + url + @"/img/JG-Logo.gif width='40' height='40' alt='' /></td>
</tr>
  <tr>
    <td width='668' align='center'><strong>INSTALLER WORK ORDER</strong></td>
   
  </tr>

  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
      <tr>
        <td width='10%' align='left'> <strong>Salesman:</strong></td>
        <td width='20%' align='left'>lbl_salesman</td>
        <td width='40%'>&nbsp;</td>
        <td width='10%' align='left'> <strong>Date Sold:</strong></td>
        <td width='20%' align='left'> lbl_date</td>
      </tr>
      <tr>
        <td align='left'> <strong>Cell #:</strong></td>
        <td align='left'>lbl_cell</td>
        <td>&nbsp;</td>
        <td align='left'> <strong>Email:</strong></td>
        <td align='left'>lbl_email</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan='2'><br></td>
  </tr>
  <tr>
    <td colspan='2'> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
  </tr>
  <tr>
    <td colspan='2'><br></td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='5' cellpadding='0' >
      <tr>
        <td width='50%'> <u><strong>Installer Info</strong></u></td>
        <td width='50%'> <u><strong>Customer Info</strong></u></td>
      </tr>
      <tr>
        <td> <strong>Installer: lbl_installer</strong></td>
        <td><strong>Job #: lbl_JobId</strong></td>
      </tr>
       <tr>
        <td><strong> Phone #: lbl_installerPhone</strong></td>
        <td><strong>Customer Name: lbl_CustomerName</strong></td>
      </tr>
      <tr>
        <td><strong> Disposal: lbl_disposal</strong></td>
        <td><strong>Customer Phone #s: lbl_Phone</strong></td>
       </tr>
        <tr>
        <td> <strong> Work Start Date: lbl_workStartDate</strong></td>
        <td><strong>Job Address: lbl_JobAddress</strong></td>
      </tr>
      <tr>
        <td><strong>Reschedule Date 1: lbl_rescheduleDate1</strong></td>
        <td><strong>Reschedule Date 2: lbl_rescheduleDate2</strong></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
      <tr>
        <td width='50%' align='center'><u><strong>Special Instructions:</strong></u></td>
        <td width='50%' align='center'><u><strong>Work Area:</strong></u></td>
      </tr>
      <tr>
        <td height='100' align='left'>lbl_specialinstructions</td>
        <td align='left'>lbl_workarea</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan='2'>&nbsp;</td>
  </tr>
 <tr>
    <td><strong>Proposal Terms : </strong>lbl_ProposalTerms</td>
  </tr>
 <tr>
    <td colspan='2'>&nbsp;</td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
      <tr>
        <td align='left'><u><strong>Materials Out</strong></u></td>
        <td align='left'><u><strong>Actual Installed</strong></u></td>
        <td align='left'><u><strong>Materials Returned</strong></u></td>
      </tr>
      <tr>
        <td height='200' align='left'>lbl_list</td>
        <td align='left'>&nbsp;</td>
        <td align='left'>&nbsp;</td>
      </tr>
      <tr>
        <td colspan='3' align='center' >
            <table width='100%' border='0' cellspacing='0' cellpadding='5' style='font-family:Times New Roman, Times, serif; font-size:50pt; color:grey;'>
            <tr><td align='center'>Rough Draft</td></tr>
            <tr><td>&nbsp;</td></tr>
            </table>
        </td>
      </tr>
    </table></td>
  </tr> 
  
    <tr>
    <td colspan='2'>  <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                        <tr><td>
    *Any additional work/ material  needed not listed on work order you must call project manager and/or  homeowners  for approval! Ex) rotted wood, extra layers, metal, permits, testing or un-forseen work etc.
</td>
                        </tr>
  <tr>
    <td colspan='2' align='right'>&nbsp;</td>
  </tr>
  <tr>
    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
      <tr>
        <td width='16%' height='25'>Installer Signature:</td>
        <td width='34%' valign='bottom'><hr color='#000000' width='100%' /></td>
        <td width='17%' align='right'>Completion Date:</td>
        <td width='33%' valign='bottom'><hr color='#000000' width='100%' /></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
</table>
</td>
  </tr>
</table>";

            return workorder;
        }


//        public string Workorderpdf()
//        {
//            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:'Times New Roman', Times, serif; font-size:10pt;'>
//<tr>
// <td width='100' align='right'><img src=" + url + @"/img/JG-Logo.gif width='40' height='40' /></td>
//</tr>
//  <tr>
//    <td width='668' align='center'><strong>INSTALLER WORK ORDER</strong></td>
//   
//  </tr>
//
//  <tr>
//    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
//      <tr>
//        <td width='10%' align='right'> <strong>Salesman:</strong></td>
//        <td width='40%' align='left'>lbl_salesman</td>
//        <td width='32%' align='right'> <strong>Date Sold:</strong></td>
//        <td width='18%' align='left'> lbl_date</td>
//      </tr>
//      <tr>
//        <td align='right'> <strong>Cell #:</strong></td>
//        <td align='left'>lbl_cell</td>
//        <td align='right'> <strong>Email:</strong></td>
//        <td align='left'>lbl_email</td>
//      </tr>
//    </table></td>
//  </tr>
//  <tr>
//    <td colspan='2'><br></td>
//  </tr>
//  <tr>
//    <td colspan='2'> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
//  </tr>
//  <tr>
//    <td colspan='2'><br></td>
//  </tr>
//  <tr>
//    <td colspan='2'><table width='100%' border='0' cellspacing='5' cellpadding='0' >
//      <tr>
//        <td width='50%'> <u><strong>Installer Info</strong></u></td>
//        <td width='50%'> <u><strong>Customer Info</strong></u></td>
//      </tr>
//      <tr>
//        <td> <strong>Installer: lbl_installer</strong></td>
//        <td><strong>Job #: lbl_JobId</strong></td>
//      </tr>
//       <tr>
//        <td><strong> Phone #: lbl_installerPhone</strong></td>
//        <td><strong>Customer Name: lbl_CustomerName</strong></td>
//      </tr>
//      <tr>
//        <td><strong> Disposal: lbl_disposal</strong></td>
//        <td><strong>Customer Phone #s: lbl_Phone</strong></td>
//       </tr>
//        <tr>
//        <td> <strong> Work Start Date: lbl_workStartDate</strong></td>
//        <td><strong>Job Address: lbl_JobAddress</strong></td>
//      </tr>
//      <tr>
//        <td><strong>Reschedule Date 1: lbl_rescheduleDate1</strong></td>
//        <td><strong>Reschedule Date 2: lbl_rescheduleDate2</strong></td>
//      </tr>
//    </table></td>
//  </tr>
//  <tr>
//    <td colspan='2'><table width='100%' border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
//      <tr>
//        <td width='50%' align='center'><u><strong>Special Instructions:</strong></u></td>
//        <td width='50%' align='center'><u><strong>Work Area:</strong></u></td>
//      </tr>
//      <tr>
//        <td height='100' align='left'>lbl_specialinstructions</td>
//        <td align='left'>lbl_workarea</td>
//      </tr>
//    </table></td>
//  </tr>
//  <tr>
//    <td colspan='2'>&nbsp;</td>
//  </tr>
// <tr>
//    <td><strong>Proposal Terms : </strong>lbl_ProposalTerms</td>
//  </tr>
// <tr>
//    <td colspan='2'>&nbsp;</td>
//  </tr>
//  <tr>
//    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
//      <tr>
//        <td align='left'><u><strong>Materials Out</strong></u></td>
//        <td align='left'><u><strong>Actual Installed</strong></u></td>
//        <td align='left'><u><strong>Materials Returned</strong></u></td>
//      </tr>
//      <tr>
//        <td height='200' align='left'>lbl_list</td>
//        <td align='left'>&nbsp;</td>
//        <td align='left'>&nbsp;</td>
//      </tr>
//    </table></td>
//  </tr>
//  
//    <tr>
//    <td colspan='2'>  <table width='100%' border='0' cellspacing='0' cellpadding='0'>
//                        <tr><td>
//    *Any additional work/ material  needed not listed on work order you must call project manager and/or  homeowners  for approval! Ex) rotted wood, extra layers, metal, permits, testing or un-forseen work etc. <strong>(C.O) Additional Material & Labor:</strong>
//</td>
//                        </tr>
//  <tr>
//    <td colspan='2' align='right'>&nbsp;</td>
//  </tr>
//  <tr>
//    <td colspan='2'><table width='100%' border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
//      <tr>
//        <td width='30%' align='left'><table width='100%' border='0' cellspacing='0' cellpadding='0'>
//          <tr>
//            <td width='30%'><strong>L1</strong></td>
//            <td width='30%'>&nbsp;</td>
//            <td width='30%'>&nbsp;</td>
//          </tr>
//          <tr>
//            <td><strong>L2</strong></td>
//            <td>&nbsp;</td>
//            <td>&nbsp;</td>
//          </tr>
//          <tr>
//            <td><strong>L3</strong></td>
//            <td>&nbsp;</td>
//            <td>&nbsp;</td>
//          </tr>
//          <tr>
//            <td><strong>L4</strong></td>
//            <td>&nbsp;</td>
//            <td><strong>Total Hours:</strong></td>
//          </tr>
//        </table></td>
//        <td width='30%' align='left' style='border-top:1px solid #ffffff;border-bottom:1px solid #ffffff;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>
//          <tr>
//            <td width='30%'><strong>Travel Time:</strong></td>
//            <td width='30%'>&nbsp;</td>
//            </tr>
//          <tr>
//            <td>&nbsp;</td>
//            <td>&nbsp;</td>
//            </tr>
//          <tr>
//            <td>&nbsp;</td>
//            <td>&nbsp;</td>
//            </tr>
//          <tr>
//            <td><strong>Total Miles:</strong></td>
//            <td>&nbsp;</td>
//            </tr>
//        </table></td>
//        <td width='30%' align='left'><table width='100%' border='0' cellspacing='0' cellpadding='5'>
//          <tr>
//            <td width='80%'><strong>Total Additional Hours:$</strong></td>
//            <td width='20%' valign='bottom'><hr size='1' /></td>
//          </tr>
//          <tr>
//            <td><strong>Total Additional Material:$</strong></td>
//            <td valign='bottom'><hr size='1' /></td>
//          </tr>
//        </table></td>
//      </tr>
//    </table></td>
//  </tr>
//  <tr>
//    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
//      <tr>
//        <td width='50%'>1)Is job done?  Y – N</td>
//        <td width='50%'>2) Does customer need an EST? Y – N</td>
//        </tr>
//      <tr>
//        <td>3)Installation completion form &amp;/or  Phone Call  Y – N</td>
//        <td> 4) Leave sign?   Y - N</td>
//        </tr>
//      <tr>
//        <td>5) Paid in full    Y – N</td>
//        <td>6)Add-on work sign off &amp; payment?   N/A -Y - N</td>
//        </tr>
//    </table></td>
//  </tr>
//  <tr>
//    <td colspan='2'>&nbsp;</td>
//  </tr>
//  <tr>
//    <td colspan='2'>Fill out work order &amp; staple all receipts to  this form. On the back, break-down your hrs. &amp; description by day.<br></td>
//  </tr>
//  <tr>
//    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
//      <tr>
//        <td width='16%' height='25'>Installer Signature:</td>
//        <td width='34%' valign='bottom'><hr color='#000000' width='100%' /></td>
//        <td width='17%' align='right'>Completion Date:</td>
//        <td width='33%' valign='bottom'><hr color='#000000' width='100%' /></td>
//      </tr>
//      <tr>
//        <td>&nbsp;</td>
//        <td>&nbsp;</td>
//        <td>&nbsp;</td>
//        <td>&nbsp;</td>
//      </tr>
//    </table></td>
//</table>
//</td>
//  </tr>
//</table>";

//            return workorder;
//        }
        //        public string Workorderpdf()
        //        {
        //            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:'Times New Roman', Times, serif; font-size:10pt;'>
        //<tr>
        // <td width='100' align='right'><img src=" + url + @"/img/JG-Logo.gif width='40' height='40' /></td>
        //</tr>
        //  <tr>
        //    <td width='668' align='center'><strong>INSTALLER WORK ORDER</strong></td>
        //   
        //  </tr>
        //
        //  <tr>
        //    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
        //      <tr>
        //        <td width='10%' align='right'> <strong>Salesman:</strong></td>
        //        <td width='40%' align='left'>lbl_salesman</td>
        //        <td width='32%' align='right'> <strong>Date Sold:</strong></td>
        //        <td width='18%' align='left'> lbl_date</td>
        //      </tr>
        //      <tr>
        //        <td align='right'> <strong>Cell #:</strong></td>
        //        <td align='left'>lbl_cell</td>
        //        <td align='right'> <strong>Email:</strong></td>
        //        <td align='left'>lbl_email</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><br></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><br></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><table width='100%' border='0' cellspacing='5' cellpadding='0' >
        //      <tr>
        //        <td width='50%'> <u><strong>Installer Info</strong></u></td>
        //        <td width='50%'> <u><strong>Customer Info</strong></u></td>
        //      </tr>
        //      <tr>
        //        <td> <strong>Installer: lbl_installer</strong></td>
        //        <td><strong>Job #: lbl_JobId</strong></td>
        //      </tr>
        //       <tr>
        //        <td><strong> Phone #: lbl_installerPhone</strong></td>
        //        <td><strong>Customer Name: lbl_CustomerName</strong></td>
        //      </tr>
        //      <tr>
        //        <td><strong> Disposal: lbl_disposal</strong></td>
        //        <td><strong>Customer Phone #s: lbl_Phone</strong></td>
        //       </tr>
        //        <tr>
        //        <td> <strong> Work Start Date: lbl_workStartDate</strong></td>
        //        <td><strong>Job Address: lbl_JobAddress</strong></td>
        //      </tr>
        //      <tr>
        //        <td><strong>Reschedule Date 1: lbl_rescheduleDate1</strong></td>
        //        <td><strong>Reschedule Date 2: lbl_rescheduleDate2</strong></td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><table width='100%' border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
        //      <tr>
        //        <td width='50%' align='center'><u><strong>Special Instructions:</strong></u></td>
        //        <td width='50%' align='center'><u><strong>Work Area:</strong></u></td>
        //      </tr>
        //      <tr>
        //        <td height='100' align='left'>lbl_specialinstructions</td>
        //        <td align='left'>lbl_workarea</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'>&nbsp;</td>
        //  </tr>
        // <tr>
        //    <td><strong>Proposal Terms : </strong>lbl_ProposalTerms</td>
        //  </tr>
        // <tr>
        //    <td colspan='2'>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
        //      <tr>
        //        <td align='left'><u><strong>Materials Out</strong></u></td>
        //        <td align='left'><u><strong>Actual Installed</strong></u></td>
        //        <td align='left'><u><strong>Materials Returned</strong></u></td>
        //      </tr>
        //      <tr>
        //        <td height='200' align='left'>lbl_list</td>
        //        <td align='left'>&nbsp;</td>
        //        <td align='left'>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'>*Any additional work/ material  needed not listed on work order you must call project manager and/or  homeowners  for approval! Ex) rotted wood, extra layers, metal, permits, testing or un-forseen work etc. <strong>(C.O) Additional Material & Labor:</strong></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2' align='right'>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><table width='100%' border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse'>
        //      <tr>
        //        <td width='30%' align='left'><table width='100%' border='0' cellspacing='0' cellpadding='0'>
        //          <tr>
        //            <td width='30%'><strong>L1</strong></td>
        //            <td width='30%'>&nbsp;</td>
        //            <td width='30%'>&nbsp;</td>
        //          </tr>
        //          <tr>
        //            <td><strong>L2</strong></td>
        //            <td>&nbsp;</td>
        //            <td>&nbsp;</td>
        //          </tr>
        //          <tr>
        //            <td><strong>L3</strong></td>
        //            <td>&nbsp;</td>
        //            <td>&nbsp;</td>
        //          </tr>
        //          <tr>
        //            <td><strong>L4</strong></td>
        //            <td>&nbsp;</td>
        //            <td><strong>Total Hours:</strong></td>
        //          </tr>
        //        </table></td>
        //        <td width='30%' align='left' style='border-top:1px solid #ffffff;border-bottom:1px solid #ffffff;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>
        //          <tr>
        //            <td width='30%'><strong>Travel Time:</strong></td>
        //            <td width='30%'>&nbsp;</td>
        //            </tr>
        //          <tr>
        //            <td>&nbsp;</td>
        //            <td>&nbsp;</td>
        //            </tr>
        //          <tr>
        //            <td>&nbsp;</td>
        //            <td>&nbsp;</td>
        //            </tr>
        //          <tr>
        //            <td><strong>Total Miles:</strong></td>
        //            <td>&nbsp;</td>
        //            </tr>
        //        </table></td>
        //        <td width='30%' align='left'><table width='100%' border='0' cellspacing='0' cellpadding='5'>
        //          <tr>
        //            <td width='80%'><strong>Total Additional Hours:$</strong></td>
        //            <td width='20%' valign='bottom'><hr size='1' /></td>
        //          </tr>
        //          <tr>
        //            <td><strong>Total Additional Material:$</strong></td>
        //            <td valign='bottom'><hr size='1' /></td>
        //          </tr>
        //        </table></td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
        //      <tr>
        //        <td width='50%'>1)Is job done?  Y – N</td>
        //        <td width='50%'>2) Does customer need an EST? Y – N</td>
        //        </tr>
        //      <tr>
        //        <td>3)Installation completion form &amp;/or  Phone Call  Y – N</td>
        //        <td> 4) Leave sign?   Y - N</td>
        //        </tr>
        //      <tr>
        //        <td>5) Paid in full    Y – N</td>
        //        <td>6)Add-on work sign off &amp; payment?   N/A -Y - N</td>
        //        </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'>Fill out work order &amp; staple all receipts to  this form. On the back, break-down your hrs. &amp; description by day.<br></td>
        //  </tr>
        //  <tr>
        //    <td colspan='2'><table width='100%' border='0' cellspacing='0' cellpadding='0' >
        //      <tr>
        //        <td width='16%' height='25'>Installer Signature:</td>
        //        <td width='34%' valign='bottom'><hr color='#000000' width='100%' /></td>
        //        <td width='17%' align='right'>Completion Date:</td>
        //        <td width='33%' valign='bottom'><hr color='#000000' width='100%' /></td>
        //      </tr>
        //      <tr>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //</table>";

        //            return workorder;
        //        }

        //        public string Workorderpdf()
        //        {
        //            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //  <tr>
        //    <td align='center'><strong>INSTALLER WORK ORDER</strong></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td> <strong>Salesman:</strong> lbl_salesman</td>
        //        <td align='right'> <strong>Date Sold:</strong> lbl_date</td>
        //      </tr>
        //      <tr>
        //        <td> <strong>Cell #:</strong> lbl_cell</td>
        //        <td align='right'> <strong>Email:</strong> lbl_email</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td width='50%'> <u><strong>Installer Info</strong></u></td>
        //        <td width='50%'> <u><strong>Customer Info</strong></u></td>
        //      </tr>
        //      <tr>
        //        <td><br></td>
        //        <td><br></td>
        //      </tr>
        //      <tr>
        //        <td> <strong>Installer: lbl_installer</strong></td>
        //        <td><strong>Job #: lbl_JobId</strong></td>
        //      </tr>
        //       <tr>
        //        <td><strong> Phone #: lbl_phone</strong></td>
        //        <td><strong>Customer Name: lbl_CustomerName</strong></td>
        //      </tr>
        //      <tr>
        //        <td><strong> Disposal: lbl_disposal</strong></td>
        //        <td><strong>Customer Phone #s: lbl_Phone</strong></td>
        //       </tr>
        //        <tr>
        //        <td> <strong> Work Start Date: lbl_workStartDate</strong></td>
        //        <td><strong>Job Address: lbl_Jobaddress</strong></td>
        //      </tr>
        //      <tr>
        //        <td><strong>Reschedule Date 1: lbl_rescheduleDate1</strong></td>
        //        <td><strong>Reschedule Date 2: lbl_rescheduleDate2</strong></td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td width='50%' align='left'><u><strong>Special Instructions:</strong></u></td>
        //        <td width='50%' align='left'><u><strong>Work Area:</strong></u></td>
        //      </tr>
        //      <tr>
        //        <td align='left'><br></td>
        //        <td align='left'><br></td>
        //      </tr>
        //      <tr>
        //        <td align='left'>lbl_specialinstructions</td>
        //        <td align='left'>lbl_workarea</td>
        //      </tr>
        // </table>
        //    <table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td align='left'><u><strong>Materials Out</strong></u></td>
        //        <td align='left'><u><strong>Actual Installed</strong></u></td>
        //        <td align='left'><u><strong>Materials Returned</strong></u></td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' table-layout:fixed border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td width='45px' align='left'>lbl_list</td>
        //        <td width='45px' align='left'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td>*Any additional work/ material  needed not listed on work order you must call project manager and/or  homeowners  for approval! Ex) rotted wood, extra layers, metal, permits, testing or un-forseen work etc. </td>
        //  </tr>
        //  <tr>
        //    <td align='right'><strong>(C.O) Additional Material & Labor:</strong></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>Reschedule Date 1:        /          /</td>
        //        <td align='right'>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'>&nbsp;</td>
        //        <td align='right'> Reschedule Date 2:       /           /</td>
        //        <td align='right'>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td align='center'>1.</td>
        //        <td colspan='5'>Is job done? Y - N</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td align='center'>2.</td>
        //        <td colspan='5'> Leave sign: Y - N</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td align='center'>3.</td>
        //        <td colspan='5'>Does customer need an EST? Y - N</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td align='center'>4.</td>
        //        <td colspan='5'>Walk around / Phone Call &amp; Collection and any additional add on(s)? Y - N</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td align='center'><br></td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><br></td>
        //  </tr>
        //  <tr>
        //    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        //      <tr>
        //        <td colspan='2'>Sub-Contractor Signature:</td>
        //        <td colspan='3'>&nbsp;</td>
        //        <td align='right'>Date:</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //      <tr>
        //        <td colspan='2'>&nbsp;</td>
        //        <td colspan='3'><hr color='#000000' width='100%' /></td>
        //        <td>&nbsp;</td>
        //        <td><hr color='#000000' width='100%' /></td>
        //      </tr>
        //      <tr>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //        <td>&nbsp;</td>
        //      </tr>
        //    </table></td>
        //  </tr>
        //  <tr>
        //    <td>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td>&nbsp;</td>
        //  </tr>
        //  <tr>
        //    <td>&nbsp;</td>
        //  </tr>
        //</table>";
        ////            string workorder = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////  <tr>
        ////    <td align='center'><strong>INSTALLER WORK ORDER</strong></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td> <strong>Salesman:</strong> lbl_salesman</td>
        ////        <td align='right'> <strong>Date Sold:</strong> lbl_date</td>
        ////      </tr>
        ////      <tr>
        ////        <td> <strong>Cell #:</strong> lbl_cell</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td> Inspect all material and job site for estimate accuracy and material matches work order, Must call the customer if running late or can't make it at all, you CAN leave a message.(Rain or Shine)</td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td width='50%'> <u><strong>Installer Info</strong></u></td>
        ////        <td width='50%'> <u><strong>Customer Info</strong></u></td>
        ////      </tr>
        ////      <tr>
        ////        <td><br></td>
        ////        <td><br></td>
        ////      </tr>
        ////      <tr>
        ////        <td> Installer: lbl_installer</td>
        ////        <td> Name: lbl_name</td>
        ////      </tr>
        ////      <tr>
        ////        <td>Phone #: lbl_phone</td>
        ////        <td> Job Location: lbl_joblocation</td>
        ////      </tr>
        ////      <tr>
        ////        <td>Work Scheduled Date: lbl_workscheduled</td>
        ////        <td> Phone #s: lbl_customerphone</td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td width='50%' align='left'><u><strong>Special Instructions:</strong></u></td>
        ////        <td width='50%' align='left'><u><strong>Work Area:</strong></u></td>
        ////      </tr>
        ////      <tr>
        ////        <td align='left'><br></td>
        ////        <td align='left'><br></td>
        ////      </tr>
        ////      <tr>
        ////        <td align='left'>lbl_specialinstructions</td>
        ////        <td align='left'>lbl_workarea</td>
        ////      </tr>
        ////      <tr>
        ////        <td align='left'><u><strong>Materials Out</strong></u></td>
        ////        <td align='left'><u><strong>Materials Returned</strong></u></td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td>&nbsp;</td>
        ////        <td colspan='5'>lbl_list</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td>Any additional work needed that is not listed on work order you must call salesman and homeowners approval to complete job. Ex rotted wood, metal etc. Poor workmanship or covering up shoddy work will not be tolerated!!! All excess material scrap aluminum, steel, &amp; copper is JMG property and must be returned. It is considered theft if not returned! </td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>Reschedule Date 1:        /          /</td>
        ////        <td align='right'>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'>&nbsp;</td>
        ////        <td align='right'> Reschedule Date 2:       /           /</td>
        ////        <td align='right'>&nbsp;</td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td align='center'>1.</td>
        ////        <td colspan='5'>Is job done? Y - N</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td align='center'>2.</td>
        ////        <td colspan='5'> Leave sign: Y - N</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td align='center'>3.</td>
        ////        <td colspan='5'>Does customer need an EST? Y - N</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td align='center'>4.</td>
        ////        <td colspan='5'>Walk around / Phone Call &amp; Collection and any additional add on(s)? Y - N</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td align='center'><br></td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><br></td>
        ////  </tr>
        ////  <tr>
        ////    <td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Tahoma, Geneva, sans-serif; font-size:7pt;'>
        ////      <tr>
        ////        <td colspan='2'>Sub-Contractor Signature:</td>
        ////        <td colspan='3'>&nbsp;</td>
        ////        <td align='right'>Date:</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////      <tr>
        ////        <td colspan='2'>&nbsp;</td>
        ////        <td colspan='3'><hr color='#000000' width='100%' /></td>
        ////        <td>&nbsp;</td>
        ////        <td><hr color='#000000' width='100%' /></td>
        ////      </tr>
        ////      <tr>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////        <td>&nbsp;</td>
        ////      </tr>
        ////    </table></td>
        ////  </tr>
        ////  <tr>
        ////    <td>&nbsp;</td>
        ////  </tr>
        ////  <tr>
        ////    <td>&nbsp;</td>
        ////  </tr>
        ////  <tr>
        ////    <td>&nbsp;</td>
        ////  </tr>
        ////</table>";

        //            return workorder;
        //        }

        public string ProposalBodyHeaderShutter()
        {
            string proposalHeader = @"<tr>
            <td valign='top'>&nbsp;</td>
            <td colspan='13' align='justify'>
            <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
            font-size: 7pt;'>
              <tbody>
                <tr>
                    <td>";

            return proposalHeader;
        }

        public string ProposalBodyFooterShutter()
        {
            string proposalFooter = @"<table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
                            font-size: 7pt;'>
                            <tbody>
                                <tr>
                                    <td valign='top'>
                                        <span style='font-weight: bold;'>&nbsp;Special Instructions:</span> lblSpecialInstructions,
                                    </td>
                                    <td valign='top'>
                                        <span style='font-weight: bold;'>&nbsp;Work Area:</span> &nbsp;lblWorkArea,
                                    </td>
                                    <td valign='top'>
                                        &nbsp;<span style='font-weight: bold;'>Shutter Tops:</span> lblShutterTops
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
            </table>
            <br />
            </td>
            <td valign='top'>&nbsp;</td>
          </tr>";

            return proposalFooter;
        }

        public string ContractPdfBodyShutter(string proposalOption)
        {
            string proposal = string.Empty;

            if (proposalOption == "A")
            {
                proposal = @"  
                <table width='100%' cellspacing='0' cellpadding='0' border='0'>
                <tbody>
                    <tr>
                        <td colspan='5'>
                            <b><u>Proposal A:</u></b> To supply and install ( lblQuantity ) pair(s) of custom
                            made Mid America (lblStyle) (lblColor)shutters. The shutters are to consist of a
                            heavy duty vinyl. Remove and haul away old shutters and debris. Job location:(lblJobLocation)
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
                font-size: 7pt;'>
                <tbody>
                    <tr>
                        <td colspan='3'>
                            <img src=" + url + @"/img/ma.png width='100px' alt='' style='width: 243px; height: 46px;' />
                        </td>
                        <td colspan='2'>
                            <b>Lifetime manufacturer’s warranty</b><br />
                            <br />
                            <b>Two year labor warranty</b><br />
                            <br />
                            <b>$ lblProposalAmtA</b><br />
                            <br />
                            <b>Per month: 6%</b><br />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <br />";
            }
            else
            {
                proposal = @"  <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
                font-size: 7pt;'>
                <tbody>
                    <tr>
                        <td colspan='5'>
                            <b><u>Proposal B:</u></b> To supply and install ( lblQuantity ) pair(s) of generic
                            plastic (lblStyle) (lblColor)shutters. Remove and haul away old shutters and debris.
                            Job location:(lblJobLocation)
                        </td>
                    </tr>
                </tbody>
                </table>
                <br />
                <br />
                <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
                    font-size: 7pt;'>
                    <tbody>
                        <tr>
                            <td colspan='3'>
                            </td>
                            <td colspan='2'>
                                <b>$ lblProposalAmtB</b><br />
                                <br />
                                <b>Per month: 6%</b><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <br />";
            }

            return proposal;
        }

        public CustomerContract FetchCustomerContractDetailShutter(int CustomerId, int productType, int productId)
        {
            try
            {
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataSet DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchCustomerContractDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, CustomerId);
                    database.AddInParameter(command, "@ProductType", DbType.Int32, productType);
                    database.AddInParameter(command, "@ProductId", DbType.Int32, productId);
                    DS = database.ExecuteDataSet(command);
                    var contract = from row in DS.Tables[0].AsEnumerable()
                                   select new CustomerContract
                                   {
                                       CustomerName = row["CustomerName"].ToString(),
                                       AmountA = decimal.Parse(row["AmountA"].ToString()),
                                       AmountB = decimal.Parse(row["AmountB"].ToString()),
                                       CellPh = row["CellPh"].ToString(),
                                       CustomerAddress = row["CustomerAddress"].ToString(),
                                       Email = row["Email"].ToString(),
                                       HousePh = row["HousePh"].ToString(),
                                       JobLocation = row["JobLocation"].ToString(),
                                       ShutterTop = row["ShutterTop"].ToString(),
                                       SpecialInstruction = row["SpecialInstruction"].ToString(),
                                       WorkArea = row["WorkArea"].ToString(),
                                       ZipCityState = row["citystatezip"].ToString(),
                                       Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                                       Style = row["Productname"].ToString(),
                                       ColorName = row["ColorName"].ToString(),
                                       JobNumber = row["QuoteNumber"].ToString()
                                   };

                    return contract.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CustomPdfBody()
        {
            string body = @"<tr>        
                <td valign='top'>&nbsp;</td>
        <td colspan='13' align='justify'>     
                         <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
        font-size: 7pt;'>
        <tbody>
            <tr>
                <td valign='top'>
                    <span style='font-weight: bold;'>&nbsp;Proposal Terms:</span> lblProposalTerms
                </td>
            </tr>
        </tbody>
    </table><br />
    
    <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
        font-size: 7pt;'>
        <tbody>
            <tr>
                <td valign='top' align='right'>
                    Proposal Cost: <b>$ lblProposalAmtA</b>
                </td>
                <td>
                </td>
                <td valign='top'>
                    &nbsp;
                </td>
                <td valign='top'>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <br />
    <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
        font-size: 7pt;'>
        <tbody>
        
            <tr>
                <td valign='top'>
                    <span style='font-weight: bold;'>&nbsp;Special Instructions:</span> lblSpecialInstructions
                </td>
                <td valign='top'>
                    
                </td>
                <td valign='top'>
                    <span style='font-weight: bold;'>&nbsp;Work Area:</span> &nbsp;lblWorkArea
                </td>
                
            </tr>
        </tbody>
    </table> <br />
        </td>
        <td valign='top'>&nbsp;</td>    
                         </tr>  ";
            //            string body = @"<tr>        
            //                <td valign='top'>&nbsp;</td>
            //        <td colspan='13' align='justify'>     
            //                         <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
            //        font-size: 7pt;'>
            //        <tbody>
            //            <tr>
            //                <td valign='top'>
            //                    <span style='font-weight: bold;'>&nbsp;Proposal Terms:</span> lblProposalTerms
            //                </td>
            //            </tr>
            //        </tbody>
            //    </table><br />
            //    <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
            //        font-size: 7pt;'>
            //        <tbody>
            //            <tr>
            //                <td colspan='3'>
            //                    <img src='" + url + @"/img/ma.png' width='175'/>
            //                </td>
            //                <td colspan='2'>
            //                    <b>Lifetime manufacturer’s warranty</b>
            //                    <br />
            //                    <br />
            //                    <b>Two year labor warranty</b>
            //                    <br />
            //                </td>
            //            </tr>
            //        </tbody>
            //    </table>
            //    <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
            //        font-size: 7pt;'>
            //        <tbody>
            //            <tr>
            //                <td valign='top' align='right'>
            //                    Proposal Cost: <b>$ lblProposalAmtA</b>
            //                </td>
            //                <td>
            //                </td>
            //                <td valign='top'>
            //                    &nbsp;
            //                </td>
            //                <td valign='top'>
            //                    &nbsp;
            //                </td>
            //                <td>
            //                </td>
            //            </tr>
            //        </tbody>
            //    </table>
            //    <br />
            //    <br />
            //    <table width='100%' cellspacing='0' cellpadding='0' border='0' class='no_line' style='font-family: tahoma,geneva,sans-serif;
            //        font-size: 7pt;'>
            //        <tbody>
            //        
            //            <tr>
            //                <td valign='top'>
            //                    <span style='font-weight: bold;'>&nbsp;Special Instructions:</span> lblSpecialInstructions
            //                </td>
            //                <td valign='top'>
            //                    
            //                </td>
            //                <td valign='top'>
            //                    <span style='font-weight: bold;'>&nbsp;Work Area:</span> &nbsp;lblWorkArea
            //                </td>
            //                
            //            </tr>
            //        </tbody>
            //    </table> <br />
            //        </td>
            //        <td valign='top'>&nbsp;</td>    
            //                         </tr>  ";

            return body;
        }

        public CustomerContract FetchCustomerContractDetails(int CustomerId, int productType, int productId)
        {
            try
            {
                CustomerContract finalContract = new CustomerContract();
                SqlDatabase database = MSSQLDataBase.Instance.GetDefaultDatabase();
                {
                    DataSet DS = new DataSet();
                    DbCommand command = database.GetStoredProcCommand("UDP_FetchCustomerContractDetails");
                    command.CommandType = CommandType.StoredProcedure;
                    database.AddInParameter(command, "@CustomerId", DbType.Int32, CustomerId);
                    database.AddInParameter(command, "@ProductType", DbType.Int32, productType);
                    database.AddInParameter(command, "@ProductId", DbType.Int32, productId);
                    DS = database.ExecuteDataSet(command);
                    if (productType == 1)
                    {
                        var contract = from row in DS.Tables[0].AsEnumerable()
                                       select new CustomerContract
                                       {
                                           CustomerName = row["CustomerName"].ToString(),
                                           AmountA = decimal.Parse(row["AmountA"].ToString()),
                                           AmountB = decimal.Parse(row["AmountB"].ToString()),
                                           CellPh = row["CellPh"].ToString(),
                                           CustomerAddress = row["CustomerAddress"].ToString(),
                                           Email = row["Email"].ToString(),
                                           HousePh = row["HousePh"].ToString(),
                                           JobLocation = row["JobLocation"].ToString(),
                                           ShutterTop = row["ShutterTop"].ToString(),
                                           SpecialInstruction = row["SpecialInstruction"].ToString(),
                                           WorkArea = row["WorkArea"].ToString(),
                                           ZipCityState = row["citystatezip"].ToString(),
                                           Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                                           Style = row["Productname"].ToString(),
                                           ColorName = row["ColorName"].ToString(),
                                           JobNumber = row["QuoteNumber"].ToString()
                                          // IsCustomerSuppliedMaterial = (row["IsCustSupMatApplicable"].ToString() == "True" ? true : false),
                                          // IsMatStorageApplicable = (row["IsMatStorageApplicable"].ToString() == "True" ? true : false),
                                         //  IsPermitRequired = (row["IsPermitRequired"].ToString() == "True" ? true : false),
                                         //  IsHabitate = (row["IsHabitat"].ToString() == "True" ? true : false)
                                       };
                        finalContract = contract.FirstOrDefault();
                    }
                    else
                    {
                        var contract = from row in DS.Tables[0].AsEnumerable()
                                       select new CustomerContract
                                       {
                                           CustomerName = row["CustomerName"].ToString(),
                                           AmountA = decimal.Parse(row["ProposalCost"].ToString()),
                                           CellPh = row["CellPh"].ToString(),
                                           CustomerAddress = row["CustomerAddress"].ToString(),
                                           Email = row["Email"].ToString(),
                                           HousePh = row["HousePh"].ToString(),
                                           JobLocation = row["JobLocation"].ToString(),
                                           ProposalTerms = row["ProposalTerms"].ToString(),
                                           SpecialInstruction = row["SpecialInstruction"].ToString(),
                                           WorkArea = row["WorkArea"].ToString(),
                                           ZipCityState = row["citystatezip"].ToString(),
                                           JobNumber = row["QuoteNumber"].ToString(),
                                           IsCustomerSuppliedMaterial = (row["IsCustSupMatApplicable"].ToString() == "True" ? true : false),
                                           IsMatStorageApplicable = (row["IsMatStorageApplicable"].ToString() == "True" ? true : false),
                                           IsPermitRequired = (row["IsPermitRequired"].ToString() == "True" ? true : false),
                                           IsHabitate = (row["IsHabitat"].ToString() == "True" ? true : false),
                                           CustomerSuppliedMaterial = row["CustSuppliedMaterial"].ToString(),
                                           MatStorageApplicable = row["MaterialStorage"].ToString()
                                       };
                        finalContract = contract.FirstOrDefault();
                    }
                    return finalContract;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
