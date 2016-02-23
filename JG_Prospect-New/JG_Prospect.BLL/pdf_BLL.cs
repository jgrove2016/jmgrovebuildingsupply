using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using JG_Prospect.DAL;
using JG_Prospect.Common;
using JG_Prospect.Common.modal;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using JG_Prospect.Common.Logger;
using System.Configuration;

namespace JG_Prospect.BLL
{
    public class pdf_BLL
    {
        private static pdf_BLL m_pdf_BLL = new pdf_BLL();

        private pdf_BLL()
        {

        }

        public static pdf_BLL Instance
        {
            get { return m_pdf_BLL; }
            set { ;}
        }

        //public DataSet FetchCustomerContractDetails(int CustomerId)
        //{
        //    return pdf_DAL.Instance.FetchCustomerContractDetails(CustomerId);
        //}
        public string ContractPdfHeader()
        {
            return pdf_DAL.Instance.ContractPdfHeader();
        }

        public string ContractPdfBody()
        {
            return pdf_DAL.Instance.ContractPdfBody();
        }

        public string ContractPdfFooter()
        {
            return pdf_DAL.Instance.ContractPdfFooter();
        }

        public string Workorderpdf()
        {
            return pdf_DAL.Instance.Workorderpdf();
        }

        public string WorkorderpdfStage3()
        {
            return pdf_DAL.Instance.WorkorderpdfStage3();
        }

        public string CreateEstimate(string InvoiceNo, int CustomerId, int productType, string totalAmount,
                                     List<Tuple<int, string, int>> proposalOptionList, string downPayment, string Praposal)
        {
            string result = string.Empty;

            string url = ConfigurationManager.AppSettings["URL"].ToString();
            decimal amt2 = 0, amt3 = 0, amt1 = 0;
            string Customername = "", SpecialInstructions = string.Empty, WorkArea = string.Empty, ProposalTerms = string.Empty;
            string customercellphone = "";
            string customeraddress = "";
            string citystatezip = "";
            string customerhousePh = "";
            string customeremail = "";
            string Joblocation = "";

            decimal Amount = 0;
            string amountpart1 = "";
            string amountpart2 = "";
            string amountpart3 = "";
            string JobNo = "";
            //string result = "";
            string CustomerSuppliedMaterial = "";
            string MaterialDumpsterStorage = "";
            string PermitRequired = "";
            string HabitatForHumanityPickUp = "";

            DataSet ds = new DataSet();
            string ContractPdfBody = string.Empty;
            CustomerContract contract = null;

            ds = AdminBLL.Instance.FetchContractTemplate(proposalOptionList[0].Item3);
            string resultHeader="",resultBody="",resultBody2="",resultFooter="";
            if (ds.Tables[0].Rows.Count > 0)
            {
                resultHeader = ds.Tables[0].Rows[0][0].ToString();
                if (Praposal == "")
                    resultBody = ds.Tables[0].Rows[0][1].ToString();
                else
                    resultBody = Praposal;
                resultBody2 = ds.Tables[0].Rows[0][3].ToString();
                resultFooter = ds.Tables[0].Rows[0][2].ToString();
            }
            CustomerContract contractHeader = new CustomerContract();
            contractHeader = pdf_BLL.Instance.FetchCustomerContractDetails(CustomerId, proposalOptionList[0].Item3, proposalOptionList[0].Item1);

            if (contractHeader != null)
            {
                Customername = contractHeader.CustomerName;
                customercellphone = contractHeader.CellPh;
                customerhousePh = contractHeader.HousePh;
                customeraddress = contractHeader.CustomerAddress;
                citystatezip = contractHeader.ZipCityState;
                customeremail = contractHeader.Email;
                Joblocation = contractHeader.JobLocation;
            }

            resultHeader = resultHeader.Replace("lblsubmittedto", Customername);
            resultHeader = resultHeader.Replace("lblDate", DateTime.Now.Date.ToShortDateString());
            resultHeader = resultHeader.Replace("lblhousePhone", customerhousePh);
            resultHeader = resultHeader.Replace("lblAddress", customeraddress);
            resultHeader = resultHeader.Replace("lblCell", customercellphone);
            resultHeader = resultHeader.Replace("lblcitystatezip", citystatezip);
            resultHeader = resultHeader.Replace("lblemail", customeremail);
            resultHeader = resultHeader.Replace("lblJobLocation", Joblocation);
            resultHeader = resultHeader.Replace("lblCustomerId", "C" + CustomerId.ToString());

            resultHeader = resultHeader.Replace(@"""", @"'");
            resultHeader = resultHeader.Replace(@"src='../img", "src='" + url + @"/img");// @"src="" + url + @""/img");
            resultHeader = resultHeader.Replace("width='100%'", "width='700'");
            resultHeader = resultHeader.Replace("width='450'", "width='200'");
            resultHeader = resultHeader.Replace("url(../img", "url(" + url + "/img");
            resultHeader = resultHeader.Replace(@"id=""t1""",@"style=""style=""font-family: Verdana, Geneva, sans-serif;      font-size: 8pt;     background: url(http://localhost:49979/img/logo_bg.png) center no-repeat; background-size: 20%;");

            result = resultHeader;

            foreach (var prop in proposalOptionList)
            {
                contract = new CustomerContract();
                contract = pdf_BLL.Instance.FetchCustomerContractDetails(CustomerId, prop.Item3, prop.Item1);

                ds = AdminBLL.Instance.FetchContractTemplate(prop.Item3);
               // resultBody = ds.Tables[0].Rows[0][1].ToString();

                if (prop.Item3 == 1)
                {
                    if (prop.Item2 == "A")
                    {
                        resultBody = ds.Tables[0].Rows[0][4].ToString();
                    }
                    else
                    {
                        resultBody = ds.Tables[0].Rows[0][5].ToString();
                    }
                    string ShutterTop = string.Empty, Color = string.Empty, style = string.Empty;
                    int Quantity = 0;
                    decimal AmountA = 0, AmountB = 0;
                    if (contract != null)
                    {

                        ShutterTop = contract.ShutterTop;
                        Color = contract.ColorName;
                        style = contract.Style;
                        Quantity = contract.Quantity;
                        AmountA = contract.AmountA;
                        AmountB = contract.AmountB;
                        SpecialInstructions = contract.SpecialInstruction;
                        WorkArea = contract.WorkArea;
                        if (JobNo != "")
                        {
                            string s = contract.JobNumber;
                            string[] jno = s.Split('-');
                            JobNo = JobNo + "," + jno[1];
                        }
                        else
                        {
                            string s = contract.JobNumber;
                            string[] jno = s.Split('-');
                            JobNo = jno[1];
                        }
                    }

                    resultBody = resultBody.Replace("lblQuantity", Quantity.ToString());
                    resultBody = resultBody.Replace("lblStyle", style);
                    resultBody = resultBody.Replace("lblColor", Color);
                    resultBody = resultBody.Replace("lblJobLocation", Joblocation);

                    if (prop.Item2 == "A")
                    {
                        resultBody = resultBody.Replace("lblProposalAmtA", AmountA.ToString());
                    }
                    else
                    {
                        resultBody = resultBody.Replace("lblProposalAmtB", AmountB.ToString());
                        resultBody = resultBody.Replace("lblSpecialInstructions", SpecialInstructions);
                        resultBody = resultBody.Replace("lblWorkArea", WorkArea);
                        resultBody = resultBody.Replace("lblShutterTops", ShutterTop);
                    }

                    resultBody = resultBody.Replace(@"src=""../img", @"src=""" + url + @"/img");

                }
                else
                {
                    resultBody = ds.Tables[0].Rows[0][1].ToString();

                    if (contract != null)
                    {
                        SpecialInstructions = contract.SpecialInstruction;
                        WorkArea = contract.WorkArea;
                        Amount = contract.AmountA;
                        ProposalTerms = contract.ProposalTerms;
                        if (JobNo != "")
                        {
                            string s = contract.JobNumber;
                            string[] jno = s.Split('-');
                            JobNo = JobNo + "," + jno[1];
                        }
                        else
                        {
                            string s = contract.JobNumber;
                            string[] jno = s.Split('-');
                            JobNo = jno[1];
                        }
                        if (contract.IsCustomerSuppliedMaterial == false)
                        {
                            CustomerSuppliedMaterial = "<strong>Customer Supplied Material : </strong>" + contract.CustomerSuppliedMaterial;
                        }
                        if (contract.IsMatStorageApplicable == false)
                        {
                            MaterialDumpsterStorage = "<strong>Material/Dumpster Storage : </strong>" + contract.MatStorageApplicable;
                        }
                        if (contract.IsPermitRequired == true)
                        {
                            PermitRequired = "Permit Required";
                        }
                        if (contract.IsHabitate == true)
                        {
                            HabitatForHumanityPickUp = "Habitat For Humanity Pick Up";
                        }
                    }

                    resultBody = resultBody.Replace("lblProposalAmtA", Amount.ToString());

                    resultBody = resultBody.Replace("lblProposalTerms", ProposalTerms);
                    resultBody = resultBody.Replace("lblSpecialInstructions", SpecialInstructions);
                    resultBody = resultBody.Replace("lblWorkArea", WorkArea);
                    resultBody = resultBody.Replace("lblCustomerSuppliedMaterial", CustomerSuppliedMaterial);
                    resultBody = resultBody.Replace("lblMaterialDumpsterStorage", MaterialDumpsterStorage);
                    resultBody = resultBody.Replace("lblPermitRequired", PermitRequired);
                    resultBody = resultBody.Replace("lblHabitatForHumanityPickUp", HabitatForHumanityPickUp);
                   
                }

                result += resultBody;
                result = result + "<br />";
                if (proposalOptionList.Count > 1)
                {
                    //result += result + "<br /><hr color='#000000' width='450' />";
                }
            }

            result = result.Replace("lblJobNo", JobNo);

            if (totalAmount != "")
            {
                amt1 = Convert.ToDecimal(downPayment);
                amt2 = amt3 = (Convert.ToDecimal(totalAmount) - amt1) / 2;
                //ContractPdfBody = ContractPdfBody.Replace("lblProposalAmtA", totalAmount);
                amountpart1 = Math.Round(amt1, 2).ToString();
                amountpart2 = Math.Round(amt2, 2).ToString();
                amountpart3 = Math.Round(amt3, 2).ToString();
            }

            resultBody2 = resultBody2.Replace("lblTotalAmount", totalAmount.ToString());
            resultBody2 = resultBody2.Replace("lblamountpart1", amountpart1);
            resultBody2 = resultBody2.Replace("lblamountpart2", amountpart2);
            resultBody2 = resultBody2.Replace("lblamountpart3", amountpart3);
            resultBody2 = resultBody2.Replace("lblCustomerName", Customername);
            resultBody2 = resultBody2.Replace("lblFooterDate", DateTime.Now.Date.ToShortDateString());
            resultBody2 = resultBody2.Replace("url(../img", "url(" + url + "/img");
            resultBody2 = resultBody2.Replace(@"width=""100%""", @"width=""700""");
            resultBody2 = resultBody2.Replace(@"width=""50%""", @"width=""340""");
           // resultFooter = resultFooter.Replace(@"src=""../img", @"src=""" + url + @"/img");
            //resultFooter = resultFooter.Replace(@"<table align=""center"" width=""100%"" bordercolor=""#666666"" bgcolor=""#FFFFFF"" border=""0"" class=""no_line"" cellspacing=""0"" cellpadding=""0"" style=""font-family: verdana, geneva, sans-serif;     font-size: 8pt;"">        <tbody>    <tr>            <td>            <img src=""../img/bar3.png"" width=""100%"" height=""40"" />        </td>    </tr></tbody></table>", "");
            
            resultFooter = resultFooter.Replace(@"width=""100%""", @"width=""700""");
            resultFooter = resultFooter.Replace("url(../img", "url(" + url + "/img");
            
           // resultFooter = resultFooter.Replace(@"<img src=""../img/bar3.png"" width=""100%"" height=""40"" />   ", " &nbsp;");
            result += resultBody2 + resultFooter;
            //if (productType == 1)
            //{
            //    result = ShutterEstimate(InvoiceNo, CustomerId, productType, totalAmount, proposalOptionList);
            //}
            //else
            //{
            //    result = CustomEstimate(InvoiceNo, CustomerId, productType, totalAmount, proposalOptionList, downPayment);
            //}

            return result;
        }

        private string ShutterEstimate(string InvoiceNo, int CustomerId, int productType, string totalAmount, List<Tuple<int, string>> proposalOptionList)
        {
            string url = ConfigurationManager.AppSettings["URL"].ToString();
            CustomerContract contract = null;
            decimal amt1 = 0, amt2 = 0, amt3 = 0;
            int Quantity = 0;
            string Customername = "", SpecialInstructions = string.Empty, WorkArea = string.Empty, ShutterTop = string.Empty;
            string customercellphone = "", Color = string.Empty, style = string.Empty;
            string customeraddress = "";
            string citystatezip = "";
            string customerhousePh = "";
            string customeremail = "";
            string Joblocation = "";
            string JobNo = "";
            decimal TotalAmount = 0, AmountA = 0, AmountB = 0;
            string amountpart1 = "";
            string amountpart2 = "";
            string amountpart3 = "";
            string result;
            DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            string resultHeader = ds.Tables[0].Rows[0][0].ToString();
            string resultBody = ds.Tables[0].Rows[0][1].ToString();
            string resultBody2 = ds.Tables[0].Rows[0][3].ToString();
            string resultFooter = ds.Tables[0].Rows[0][2].ToString();

            //string proposalPdfBody = string.Empty;
            //string proposalPdfHeader = pdf_DAL.Instance.ProposalBodyHeaderShutter();
            //string proposalPdfFooter = pdf_DAL.Instance.ProposalBodyFooterShutter();

            //DataSet ds = new DataSet();

            //string ContractPdfHeader = pdf_BLL.Instance.ContractPdfHeader();
            //string ContractPdffooter = pdf_BLL.Instance.ContractPdfFooter();
            //string ContractPdfBody = pdf_BLL.Instance.ContractPdfBody(productType,proposalOption);

            // string ContractPdfBody = string.Empty;
            foreach (var prop in proposalOptionList)
            {
                string shutterPdfBody = pdf_DAL.Instance.ContractPdfBodyShutter(prop.Item2);
                contract = new CustomerContract();

                contract = pdf_DAL.Instance.FetchCustomerContractDetailShutter(CustomerId, productType, prop.Item1);

                if (contract != null)
                {
                    Customername = contract.CustomerName;
                    customercellphone = contract.CellPh;
                    customerhousePh = contract.HousePh;
                    customeraddress = contract.CustomerAddress;
                    citystatezip = contract.ZipCityState;
                    customeremail = contract.Email;
                    Joblocation = contract.JobLocation;
                    ShutterTop = contract.ShutterTop;
                    Color = contract.ColorName;
                    style = contract.Style;
                    Quantity = contract.Quantity;
                    AmountA = contract.AmountA;
                    AmountB = contract.AmountB;
                    SpecialInstructions = contract.SpecialInstruction;
                    WorkArea = contract.WorkArea;
                    JobNo = contract.JobNumber;
                }
                resultHeader = resultHeader.Replace("lblsubmittedto", Customername);
                resultHeader = resultHeader.Replace("lblDate", DateTime.Now.Date.ToShortDateString());
                resultHeader = resultHeader.Replace("lblhousePhone", customerhousePh);
                resultHeader = resultHeader.Replace("lblAddress", customeraddress);
                resultHeader = resultHeader.Replace("lblCell", customercellphone);
                resultHeader = resultHeader.Replace("lblcitystatezip", citystatezip);
                resultHeader = resultHeader.Replace("lblemail", customeremail);
                resultHeader = resultHeader.Replace("lblJobLocation", Joblocation);
                resultHeader = resultHeader.Replace("lblJobNo", JobNo);
                // ContractPdfHeader = ContractPdfHeader.Replace("lblCustomerId", CustomerId.ToString());

                resultBody = resultBody.Replace("lblQuantity", Quantity.ToString());
                resultBody = resultBody.Replace("lblStyle", style);
                resultBody = resultBody.Replace("lblColor", Color);
                resultBody = resultBody.Replace("lblJobLocation", Joblocation);

                if (prop.Item2 == "A")
                {
                    resultBody = resultBody.Replace("lblProposalAmtA", AmountA.ToString());
                }
                else
                {
                    resultBody = resultBody.Replace("lblProposalAmtB", AmountB.ToString());
                }

                //resultBody += resultBody;

            }
            resultBody = resultBody.Replace(@"src=""../img", @"src=""" + url + @"/img");
            //ContractPdfBody = ContractPdfBody.Replace("lblSpecialInstructions", SpecialInstructions);
            //ContractPdfBody = ContractPdfBody.Replace("lblWorkArea", WorkArea);
            //ContractPdfBody = ContractPdfBody.Replace("lblShutterTops", ShutterTop);

            //proposalPdfFooter = proposalPdfFooter.Replace("lblSpecialInstructions", SpecialInstructions);
            //proposalPdfFooter = proposalPdfFooter.Replace("lblWorkArea", WorkArea);
            //proposalPdfFooter = proposalPdfFooter.Replace("lblShutterTops", ShutterTop);

            if (totalAmount != "")
            {
                amt1 = amt2 = amt3 = Convert.ToDecimal(totalAmount) / 3;
                amountpart1 = Math.Round(amt1, 2).ToString();
                amountpart2 = Math.Round(amt2, 2).ToString();
                amountpart3 = Math.Round(amt3, 2).ToString();
            }
            //ContractPdffooter = ContractPdffooter.Replace("lblTotalAmount", totalAmount.ToString());
            //ContractPdffooter = ContractPdffooter.Replace("lblamountpart1", amountpart1);
            //ContractPdffooter = ContractPdffooter.Replace("lblamountpart2", amountpart2);
            //ContractPdffooter = ContractPdffooter.Replace("lblamountpart3", amountpart3);
            //ContractPdffooter = ContractPdffooter.Replace("lblCustomerName", Customername);
            //ContractPdffooter = ContractPdffooter.Replace("lbldate", DateTime.Now.Date.ToShortDateString());

            resultHeader = resultHeader.Replace(@"""", @"'");
            resultHeader = resultHeader.Replace(@"src='../img", "src='" + url + @"/img");// @"src="" + url + @""/img");
            resultHeader = resultHeader.Replace("width='100%'", "width='600'");
            resultHeader = resultHeader.Replace("width='450'", "width='200'");
            resultHeader = resultHeader.Replace("url(../img", "url(" + url + "/img");
            result = resultHeader + resultBody;

            resultBody2 = resultBody2.Replace("lblTotalAmount", totalAmount.ToString());
            resultBody2 = resultBody2.Replace("lblamountpart1", amountpart1);
            resultBody2 = resultBody2.Replace("lblamountpart2", amountpart2);
            resultBody2 = resultBody2.Replace("lblamountpart3", amountpart3);
            resultBody2 = resultBody2.Replace("lblCustomerName", Customername);
            resultBody2 = resultBody2.Replace("lblFooterDate", DateTime.Now.Date.ToShortDateString());
            resultBody2 = resultBody2.Replace("url(../img", "url(" + url + "/img");

            resultFooter = resultFooter.Replace(@"src=""../img", @"src=""" + url + @"/img");
            resultFooter = resultFooter.Replace(@"width=""100%""", @"width=""550""");
            resultFooter = resultFooter.Replace("url(../img", "url(" + url + "/img");

            // proposalPdfBody = proposalPdfHeader + resultBody + proposalPdfFooter;
            result += resultBody2 + resultFooter;
            //result = ContractPdfHeader + proposalPdfBody + ContractPdffooter;

            return result;
        }

        private string ShutterEstimateOLD(string InvoiceNo, int CustomerId, int productType, string totalAmount, List<Tuple<int, string>> proposalOptionList)
        {
            CustomerContract contract = null;
            decimal amt1 = 0, amt2 = 0, amt3 = 0;
            int Quantity = 0;
            string Customername = "", SpecialInstructions = string.Empty, WorkArea = string.Empty, ShutterTop = string.Empty;
            string customercellphone = "", Color = string.Empty, style = string.Empty;
            string customeraddress = "";
            string citystatezip = "";
            string customerhousePh = "";
            string customeremail = "";
            string Joblocation = "";

            decimal TotalAmount = 0, AmountA = 0, AmountB = 0;
            string amountpart1 = "";
            string amountpart2 = "";
            string amountpart3 = "";
            string result;

            string proposalPdfBody = string.Empty;
            string proposalPdfHeader = pdf_DAL.Instance.ProposalBodyHeaderShutter();
            string proposalPdfFooter = pdf_DAL.Instance.ProposalBodyFooterShutter();

            DataSet ds = new DataSet();

            string ContractPdfHeader = pdf_BLL.Instance.ContractPdfHeader();
            string ContractPdffooter = pdf_BLL.Instance.ContractPdfFooter();
           // string ContractPdfBody = pdf_BLL.Instance.ContractPdfBody(productType,proposalOption);

            string ContractPdfBody = string.Empty;
            foreach (var prop in proposalOptionList)
            {
                string shutterPdfBody = pdf_DAL.Instance.ContractPdfBodyShutter(prop.Item2);
                contract = new CustomerContract();

                contract = pdf_DAL.Instance.FetchCustomerContractDetailShutter(CustomerId, productType, prop.Item1);

                if (contract != null)
                {
                    Customername = contract.CustomerName;
                    customercellphone = contract.CellPh;
                    customerhousePh = contract.HousePh;
                    customeraddress = contract.CustomerAddress;
                    citystatezip = contract.ZipCityState;
                    customeremail = contract.Email;
                    Joblocation = contract.JobLocation;
                    ShutterTop = contract.ShutterTop;
                    Color = contract.ColorName;
                    style = contract.Style;
                    Quantity = contract.Quantity;
                    AmountA = contract.AmountA;
                    AmountB = contract.AmountB;
                    SpecialInstructions = contract.SpecialInstruction;
                    WorkArea = contract.WorkArea;
                }
                ContractPdfHeader = ContractPdfHeader.Replace("lblsubmittedto", Customername);
                ContractPdfHeader = ContractPdfHeader.Replace("lblDate", DateTime.Now.Date.ToShortDateString());
                ContractPdfHeader = ContractPdfHeader.Replace("lblhousePhone", customerhousePh);
                ContractPdfHeader = ContractPdfHeader.Replace("lblAddress", customeraddress);
                ContractPdfHeader = ContractPdfHeader.Replace("lblCell", customercellphone);
                ContractPdfHeader = ContractPdfHeader.Replace("lblcitystatezip", citystatezip);
                ContractPdfHeader = ContractPdfHeader.Replace("lblemail", customeremail);
                ContractPdfHeader = ContractPdfHeader.Replace("lblJobLocation", Joblocation);
                // ContractPdfHeader = ContractPdfHeader.Replace("lblCustomerId", CustomerId.ToString());

                shutterPdfBody = shutterPdfBody.Replace("lblQuantity", Quantity.ToString());
                shutterPdfBody = shutterPdfBody.Replace("lblStyle", style);
                shutterPdfBody = shutterPdfBody.Replace("lblColor", Color);
                shutterPdfBody = shutterPdfBody.Replace("lblJobLocation", Joblocation);

                if (prop.Item2 == "A")
                {
                    shutterPdfBody = shutterPdfBody.Replace("lblProposalAmtA", AmountA.ToString());
                }
                else
                {
                    shutterPdfBody = shutterPdfBody.Replace("lblProposalAmtB", AmountB.ToString());
                }

                ContractPdfBody += shutterPdfBody;

            }

            //ContractPdfBody = ContractPdfBody.Replace("lblSpecialInstructions", SpecialInstructions);
            //ContractPdfBody = ContractPdfBody.Replace("lblWorkArea", WorkArea);
            //ContractPdfBody = ContractPdfBody.Replace("lblShutterTops", ShutterTop);

            proposalPdfFooter = proposalPdfFooter.Replace("lblSpecialInstructions", SpecialInstructions);
            proposalPdfFooter = proposalPdfFooter.Replace("lblWorkArea", WorkArea);
            proposalPdfFooter = proposalPdfFooter.Replace("lblShutterTops", ShutterTop);

            if (totalAmount != "")
            {
                amt1 = amt2 = amt3 = Convert.ToDecimal(totalAmount) / 3;
                amountpart1 = Math.Round(amt1, 2).ToString();
                amountpart2 = Math.Round(amt2, 2).ToString();
                amountpart3 = Math.Round(amt3, 2).ToString();
            }
            ContractPdffooter = ContractPdffooter.Replace("lblTotalAmount", totalAmount.ToString());
            ContractPdffooter = ContractPdffooter.Replace("lblamountpart1", amountpart1);
            ContractPdffooter = ContractPdffooter.Replace("lblamountpart2", amountpart2);
            ContractPdffooter = ContractPdffooter.Replace("lblamountpart3", amountpart3);
            ContractPdffooter = ContractPdffooter.Replace("lblCustomerName", Customername);
            ContractPdffooter = ContractPdffooter.Replace("lbldate", DateTime.Now.Date.ToShortDateString());

            proposalPdfBody = proposalPdfHeader + ContractPdfBody + proposalPdfFooter;

            result = ContractPdfHeader + proposalPdfBody + ContractPdffooter;

            return result;
        }

        private string CustomEstimateOLD(string InvoiceNo, int CustomerId, int productType, string totalAmount,
                                        List<Tuple<int, string>> proposalOptionList, string downPayment)
        {
            decimal amt2 = 0, amt3 = 0, amt1 = 0;
            string Customername = "", SpecialInstructions = string.Empty, WorkArea = string.Empty, ProposalTerms = string.Empty;
            string customercellphone = "";
            string customeraddress = "";
            string citystatezip = "";
            string customerhousePh = "";
            string customeremail = "";
            string Joblocation = "";

            decimal Amount = 0;
            string amountpart1 = "";
            string amountpart2 = "";
            string amountpart3 = "";
            string JobNo = "";
            string result;
            DataSet ds = new DataSet();
            string ContractPdfBody = string.Empty;
            CustomerContract contract = null;

            string ContractPdfHeader = pdf_BLL.Instance.ContractPdfHeader();

            //string customPdfBody = pdf_DAL.Instance.CustomPdfBody();

            string ContractPdffooter = pdf_BLL.Instance.ContractPdfFooter();
            foreach (var prop in proposalOptionList)
            {
                string customPdfBody = pdf_DAL.Instance.CustomPdfBody();
                contract = new CustomerContract();
                contract = pdf_BLL.Instance.FetchCustomerContractDetails(CustomerId, productType, prop.Item1);

                if (contract != null)
                {
                    Customername = contract.CustomerName;
                    customercellphone = contract.CellPh;
                    customerhousePh = contract.HousePh;
                    customeraddress = contract.CustomerAddress;
                    citystatezip = contract.ZipCityState;
                    customeremail = contract.Email;
                    Joblocation = contract.JobLocation;

                    SpecialInstructions = contract.SpecialInstruction;
                    WorkArea = contract.WorkArea;
                    Amount = contract.AmountA;
                    ProposalTerms = contract.ProposalTerms;
                    JobNo = contract.JobNumber;
                }

                ContractPdfHeader = ContractPdfHeader.Replace("lblsubmittedto", Customername);
                ContractPdfHeader = ContractPdfHeader.Replace("lblDate", DateTime.Now.Date.ToShortDateString());
                ContractPdfHeader = ContractPdfHeader.Replace("lblhousePhone", customerhousePh);
                ContractPdfHeader = ContractPdfHeader.Replace("lblAddress", customeraddress);
                ContractPdfHeader = ContractPdfHeader.Replace("lblCell", customercellphone);
                ContractPdfHeader = ContractPdfHeader.Replace("lblcitystatezip", citystatezip);
                ContractPdfHeader = ContractPdfHeader.Replace("lblemail", customeremail);
                ContractPdfHeader = ContractPdfHeader.Replace("lblJobLocation", Joblocation);
                ContractPdfHeader = ContractPdfHeader.Replace("lblCustomerId", CustomerId.ToString());
                ContractPdfHeader = ContractPdfHeader.Replace("lblJobNo", JobNo);

                customPdfBody = customPdfBody.Replace("lblProposalAmtA", Amount.ToString());

                customPdfBody = customPdfBody.Replace("lblProposalTerms", ProposalTerms);
                customPdfBody = customPdfBody.Replace("lblSpecialInstructions", SpecialInstructions);
                customPdfBody = customPdfBody.Replace("lblWorkArea", WorkArea);

                ContractPdfBody += customPdfBody;
            }

            if (totalAmount != "")
            {
                amt1 = Convert.ToDecimal(downPayment);
                amt2 = amt3 = (Convert.ToDecimal(totalAmount) - amt1) / 2;
                //ContractPdfBody = ContractPdfBody.Replace("lblProposalAmtA", totalAmount);
                amountpart1 = Math.Round(amt1, 2).ToString();
                amountpart2 = Math.Round(amt2, 2).ToString();
                amountpart3 = Math.Round(amt3, 2).ToString();
            }

            ContractPdffooter = ContractPdffooter.Replace("lblTotalAmount", totalAmount.ToString());
            ContractPdffooter = ContractPdffooter.Replace("lblamountpart1", amountpart1);
            ContractPdffooter = ContractPdffooter.Replace("lblamountpart2", amountpart2);
            ContractPdffooter = ContractPdffooter.Replace("lblamountpart3", amountpart3);
            ContractPdffooter = ContractPdffooter.Replace("lblCustomerName", Customername);
            ContractPdffooter = ContractPdffooter.Replace("lbldate", DateTime.Now.Date.ToShortDateString());
            result = ContractPdfHeader + ContractPdfBody + ContractPdffooter;

            return result;
        }
        private string CustomEstimate(string InvoiceNo, int CustomerId, int productType, string totalAmount,
                                       List<Tuple<int, string>> proposalOptionList, string downPayment)
        {
            string url = ConfigurationManager.AppSettings["URL"].ToString();
            decimal amt2 = 0, amt3 = 0, amt1 = 0;
            string Customername = "", SpecialInstructions = string.Empty, WorkArea = string.Empty, ProposalTerms = string.Empty;
            string customercellphone = "";
            string customeraddress = "";
            string citystatezip = "";
            string customerhousePh = "";
            string customeremail = "";
            string Joblocation = "";

            decimal Amount = 0;
            string amountpart1 = "";
            string amountpart2 = "";
            string amountpart3 = "";
            string JobNo = "";
            string result = "";
            string CustomerSuppliedMaterial = "";
            string MaterialDumpsterStorage = "";
            string PermitRequired = "";
            string HabitatForHumanityPickUp = "";
            DataSet ds = new DataSet();
            string ContractPdfBody = string.Empty;
            CustomerContract contract = null;

            //  DataSet ds = new DataSet();
            ds = AdminBLL.Instance.FetchContractTemplate(productType);
            string resultHeader = ds.Tables[0].Rows[0][0].ToString();
            string resultBody = ds.Tables[0].Rows[0][1].ToString();
            string resultBody2 = ds.Tables[0].Rows[0][3].ToString();
            string resultFooter = ds.Tables[0].Rows[0][2].ToString();
            //  string ContractPdfHeader = pdf_BLL.Instance.ContractPdfHeader();

            //string customPdfBody = pdf_DAL.Instance.CustomPdfBody();

            //  string ContractPdffooter = pdf_BLL.Instance.ContractPdfFooter();
            foreach (var prop in proposalOptionList)
            {
                // string customPdfBody = pdf_DAL.Instance.CustomPdfBody();
                contract = new CustomerContract();
                contract = pdf_BLL.Instance.FetchCustomerContractDetails(CustomerId, productType, prop.Item1);

                if (contract != null)
                {
                    Customername = contract.CustomerName;
                    customercellphone = contract.CellPh;
                    customerhousePh = contract.HousePh;
                    customeraddress = contract.CustomerAddress;
                    citystatezip = contract.ZipCityState;
                    customeremail = contract.Email;
                    Joblocation = contract.JobLocation;

                    SpecialInstructions = contract.SpecialInstruction;
                    WorkArea = contract.WorkArea;
                    Amount = contract.AmountA;
                    ProposalTerms = contract.ProposalTerms;
                    JobNo = contract.JobNumber;
                    if (contract.IsCustomerSuppliedMaterial == true)
                    {
                        CustomerSuppliedMaterial = "Customer Supplied Material : " + contract.CustomerSuppliedMaterial;
                    }
                    if (contract.IsMatStorageApplicable == true)
                    {
                        MaterialDumpsterStorage = "Material/Dumpster Storage : " + contract.MatStorageApplicable;
                    }
                    if (contract.IsPermitRequired == true)
                    {
                        PermitRequired = "Permit Required";
                    }
                    if (contract.IsHabitate == true)
                    {
                        HabitatForHumanityPickUp = "Habitat For Humanity Pick Up";
                    }
                }

                resultHeader = resultHeader.Replace("lblsubmittedto", Customername);
                resultHeader = resultHeader.Replace("lblDate", DateTime.Now.Date.ToShortDateString());
                resultHeader = resultHeader.Replace("lblhousePhone", customerhousePh);
                resultHeader = resultHeader.Replace("lblAddress", customeraddress);
                resultHeader = resultHeader.Replace("lblCell", customercellphone);
                resultHeader = resultHeader.Replace("lblcitystatezip", citystatezip);
                resultHeader = resultHeader.Replace("lblemail", customeremail);
                resultHeader = resultHeader.Replace("lblJobLocation", Joblocation);
                resultHeader = resultHeader.Replace("lblCustomerId", CustomerId.ToString());
                resultHeader = resultHeader.Replace("lblJobNo", JobNo);

                resultBody = resultBody.Replace("lblProposalAmtA", Amount.ToString());

                resultBody = resultBody.Replace("lblProposalTerms", ProposalTerms);
                resultBody = resultBody.Replace("lblSpecialInstructions", SpecialInstructions);
                resultBody = resultBody.Replace("lblWorkArea", WorkArea);
                resultBody = resultBody.Replace("lblCustomerSuppliedMaterial", CustomerSuppliedMaterial);
                resultBody = resultBody.Replace("lblMaterialDumpsterStorage", MaterialDumpsterStorage);
                resultBody = resultBody.Replace("lblPermitRequired", PermitRequired);
                resultBody = resultBody.Replace("lblHabitatForHumanityPickUp", HabitatForHumanityPickUp);

                resultHeader = resultHeader.Replace(@"""", @"'");
                resultHeader = resultHeader.Replace(@"src='../img", "src='" + url + @"/img");// @"src="" + url + @""/img");
                resultHeader = resultHeader.Replace("width='100%'", "width='600'");
                resultHeader = resultHeader.Replace("width='450'", "width='200'");
                resultHeader = resultHeader.Replace("url(../img", "url(" + url + "/img");
                result = resultHeader + resultBody;
            }

            if (totalAmount != "")
            {
                amt1 = Convert.ToDecimal(downPayment);
                amt2 = amt3 = (Convert.ToDecimal(totalAmount) - amt1) / 2;
                //ContractPdfBody = ContractPdfBody.Replace("lblProposalAmtA", totalAmount);
                amountpart1 = Math.Round(amt1, 2).ToString();
                amountpart2 = Math.Round(amt2, 2).ToString();
                amountpart3 = Math.Round(amt3, 2).ToString();
            }

            resultBody2 = resultBody2.Replace("lblTotalAmount", totalAmount.ToString());
            resultBody2 = resultBody2.Replace("lblamountpart1", amountpart1);
            resultBody2 = resultBody2.Replace("lblamountpart2", amountpart2);
            resultBody2 = resultBody2.Replace("lblamountpart3", amountpart3);
            resultBody2 = resultBody2.Replace("lblCustomerName", Customername);
            resultBody2 = resultBody2.Replace("lblFooterDate", DateTime.Now.Date.ToShortDateString());
            resultBody2 = resultBody2.Replace("url(../img", "url(" + url + "/img");
            resultFooter = resultFooter.Replace(@"src=""../img", @"src=""" + url + @"/img");
            resultFooter = resultFooter.Replace(@"width=""100%""", @"width=""550""");
            resultFooter = resultFooter.Replace("url(../img", "url(" + url + "/img");
            result += resultBody2 + resultFooter; // ContractPdfHeader + ContractPdfBody + ContractPdffooter;

            return result;
        }

        public CustomerContract FetchCustomerContractDetails(int CustomerId, int productType, int productId)
        {
            return pdf_DAL.Instance.FetchCustomerContractDetails(CustomerId, productType, productId);
        }

        public string 
            CreateWorkOrder(string InvoiceNo, int estimateId, int productType, int customerId, string soldJobId,int stage)
        {
            string workOrder = string.Empty;
           
            workOrder = CustomWorkOrder(InvoiceNo, estimateId, productType, customerId, soldJobId,stage);

            return workOrder;
        }

        public string CustomWorkOrder(string InvoiceNo, int estimateId, int productType, int customerId, string soldJobId,int stage)
        {
            string result = string.Empty;
            try
            {
                string Salesman = "", email = "";
                string datesold = "";
                string SalesmanCell = "";
                string Customerinfo = "";
                string JobLocation = "";
                string CustomerPh = "";
                string SpclInstructions = "";
                string workArea = "";
               // int productid;
                string materiallist = "";
                //string category = "";
                string proposalTerms = "";
                bool IsPermit = false;
                bool IsHabitat = false;
                if (stage == 1)
                {
                    result = pdf_BLL.Instance.Workorderpdf();
                }
                else
                {
                    result = pdf_BLL.Instance.WorkorderpdfStage3();
                }

                DataSet DS1 = new DataSet();

                DS1 = shuttersBLL.Instance.FetchWorkOrderContractdetails(customerId, estimateId, productType);

                if (DS1 != null)
                {
                    Salesman = DS1.Tables[0].Rows[0]["Username"].ToString();
                    datesold = DateTime.Now.ToShortDateString();
                    email = DS1.Tables[0].Rows[0]["Email"].ToString();
                    SalesmanCell = DS1.Tables[0].Rows[0]["Phone"].ToString();
                    Customerinfo = DS1.Tables[0].Rows[0]["CustomerName"].ToString();
                    JobLocation = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                    CustomerPh = DS1.Tables[0].Rows[0]["phones"].ToString();
                    workArea = DS1.Tables[0].Rows[0]["WorkArea"].ToString();
                   
                    if (productType != (int)JGConstant.ProductType.shutter)
                    {
                        proposalTerms = DS1.Tables[0].Rows[0]["ProposalTerms"].ToString();

                        if (DS1.Tables[0].Rows[0]["IsPermitRequired"].ToString() != "")
                        {
                            IsPermit = Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsPermitRequired"].ToString());
                        }
                        if (DS1.Tables[0].Rows[0]["IsHabitat"].ToString() != "")
                        {
                            IsHabitat = Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsHabitat"].ToString());
                        }
                        //Style = DS1.Tables[0].Rows[0]["ProposalTerms"].ToString();
                        if (IsPermit == true)
                        {
                            SpclInstructions += "Building Permits required.";
                        }
                        if (IsHabitat == true)
                        {
                            SpclInstructions += "Salvageable material to be stored for Habitat for Humanity pick up.";
                        }
                    }
                    SpclInstructions += DS1.Tables[0].Rows[0]["SpecialInstruction"].ToString();
                    //productid = Convert.ToInt32(DS1.Tables[0].Rows[0]["ProductId"].ToString());
                    DataSet ds = new DataSet();

                    ds = shuttersBLL.Instance.GetMaterialList(customerId, soldJobId);//, estimateId, estimateId);

                    if (ds != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            materiallist += ds.Tables[0].Rows[i]["VendorCategoryNm"].ToString() + "    :    " + ds.Tables[0].Rows[i]["MaterialList"].ToString();
                            //materiallist += " </br> ";// System.Environment.NewLine;
                            materiallist += System.Environment.NewLine;
                        }
                    }

                }
                result = result.Replace("lbl_salesman", Salesman);
                result = result.Replace("lbl_date", datesold);
                result = result.Replace("lbl_email", email);
                result = result.Replace("lbl_cell", SalesmanCell);
                result = result.Replace("lbl_joblocation", JobLocation);
                result = result.Replace("lbl_Phone", CustomerPh);
                result = result.Replace("lbl_specialinstructions", SpclInstructions);
                result = result.Replace("lbl_workarea", workArea);
                result = result.Replace("lbl_CustomerName", Customerinfo);
                result = result.Replace("lbl_JobId", soldJobId);
                result = result.Replace("lbl_ProposalTerms", proposalTerms);

                result = result.Replace("lbl_list", materiallist);
                result = result.Replace("lbl_JobAddress", "");
                result = result.Replace("lbl_rescheduleDate2", "");
                result = result.Replace("lbl_rescheduleDate1", "");
                result = result.Replace("lbl_workStartDate", "");
                result = result.Replace("lbl_disposal", "");
                result = result.Replace("lbl_installerPhone", "");
                result = result.Replace("lbl_installer", "");
            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "WO stage1", "");
                //LogManager.Instance.WriteToFlatFile(ex.Message, "Custom",1);// Request.ServerVariables["remote_addr"].ToString());

            }
            return result;
        }

        public string ShutterWorkOrder(string InvoiceNo, int estimateId, int productType, int customerId, string soldJobId)
        {
            string Salesman = "", email = "", proposalTerms = "";
            string datesold = "";
            string SalesmanCell = "";
            //string Installer = "";
            string Customerinfo = "";
            //string InstallerPh = "";
            string JobLocation = "";
            //string workschd_date = "";
            string CustomerPh = "";
            //string disposal = "";
            string SpclInstructions = "";
            string workArea = "";
            //string MaterialsOut = "";
            //string MaterialReturned = "";
            //string RescheduleDate1 = "";
            //string RescheduleDate2 = "";
            //string productname = "", Quantity = "", Width = "", Color = "", Style = "", Length = "";
            //int productid;
            string materiallist = "";
            string result = pdf_BLL.Instance.Workorderpdf();

            DataSet DS1 = new DataSet();

            //DS1 = shuttersBLL.Instance.FetchWorkOrderContractdetails(int.Parse(Session["CustomerId"].ToString()), estimateId);
            DS1 = shuttersBLL.Instance.FetchWorkOrderContractdetails(customerId, estimateId, productType);

            if (DS1 != null)
            {
                Salesman = DS1.Tables[0].Rows[0]["Username"].ToString();
                datesold = DateTime.Now.ToShortDateString();
                email = DS1.Tables[0].Rows[0]["Email"].ToString();
                SalesmanCell = DS1.Tables[0].Rows[0]["Phone"].ToString();
                Customerinfo = DS1.Tables[0].Rows[0]["CustomerName"].ToString();
                JobLocation = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                CustomerPh = DS1.Tables[0].Rows[0]["phones"].ToString();
                workArea = DS1.Tables[0].Rows[0]["WorkArea"].ToString();
                proposalTerms = DS1.Tables[0].Rows[0]["ProposalTerms"].ToString();

                
                //SalesmanCell = DS1.Tables[0].Rows[0]["Email"].ToString();
                //Installer = DS1.Tables[0].Rows[0]["Installer"].ToString();
               // Customerinfo = DS1.Tables[0].Rows[0]["CustomerName"].ToString();
                //JobLocation = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                //workschd_date = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                //CustomerPh = DS1.Tables[0].Rows[0]["phones"].ToString();
                //disposal = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                SpclInstructions = DS1.Tables[0].Rows[0]["SpecialInstruction"].ToString();
                //workArea = DS1.Tables[0].Rows[0]["WorkArea"].ToString();
                //productname = DS1.Tables[0].Rows[0]["Productname"].ToString();
                //Quantity = DS1.Tables[0].Rows[0]["Quantity"].ToString();
                //Width = DS1.Tables[0].Rows[0]["Width"].ToString();
                //Color = DS1.Tables[0].Rows[0]["ColorName"].ToString();
                //Style = DS1.Tables[0].Rows[0]["Productname"].ToString();
                //Length = DS1.Tables[0].Rows[0]["ShutterLength"].ToString();
                //productid = Convert.ToInt32(DS1.Tables[0].Rows[0]["ShutterId"].ToString());
                DataSet ds = new DataSet();
                //ds = shuttersBLL.Instance.GetMaterialList(int.Parse(Session["CustomerId"].ToString()), productid);
                ds = shuttersBLL.Instance.GetMaterialList(customerId, soldJobId);//, productType, estimateId);
               // materiallist = ds.Tables[0].Rows[0]["MaterialList"].ToString();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        materiallist += ds.Tables[0].Rows[i]["VendorCategoryNm"].ToString() + "    :    " + ds.Tables[0].Rows[i]["MaterialList"].ToString();
                        //materiallist += " </br> ";// System.Environment.NewLine;
                        materiallist += System.Environment.NewLine;
                    }
                }
            }


            result = result.Replace("lbl_salesman", Salesman);
            result = result.Replace("lbl_date", datesold);
            result = result.Replace("lbl_email", email);
            result = result.Replace("lbl_cell", SalesmanCell);
            result = result.Replace("lbl_joblocation", JobLocation);
            result = result.Replace("lbl_Phone", CustomerPh);
            result = result.Replace("lbl_specialinstructions", SpclInstructions);
            result = result.Replace("lbl_workarea", workArea);
            result = result.Replace("lbl_CustomerName", Customerinfo);
            result = result.Replace("lbl_list", materiallist);
            result = result.Replace("lbl_JobId", soldJobId);
            result = result.Replace("lbl_ProposalTerms", proposalTerms);


           // result = result.Replace("lbl_salesman", Salesman);
           // result = result.Replace("lbl_date", datesold);
           // result = result.Replace("lbl_cell", SalesmanCell);
            //result = result.Replace("lbl_installer", Installer);
          //  result = result.Replace("lbl_name", Customerinfo);
            //result = result.Replace("lbl_phone", InstallerPh);
           // result = result.Replace("lbl_joblocation", JobLocation);
            //result = result.Replace("lbl_workscheduled", workschd_date);
           // result = result.Replace("lbl_customerphone", CustomerPh);
           // result = result.Replace("lbl_specialinstructions", SpclInstructions);
           // result = result.Replace("lbl_workarea", workArea);

            //result = result.Replace("lbl_list", materiallist);

            return result;

        }
        public string CreateWorkOrder(string InvoiceNo, int estimateId, int productType, int customerId, string soldJobId)
        {
            string workOrder = string.Empty;
            if (productType == 1)
            {
                workOrder = ShutterWorkOrder(InvoiceNo, estimateId, productType, customerId, soldJobId);
            }
            else //if (productType == Convert.ToInt16(JGConstant.ProductType.custom))
            {
                workOrder = CustomWorkOrder(InvoiceNo, estimateId, productType, customerId, soldJobId);
            }
            return workOrder;
        }
        public string CustomWorkOrder(string InvoiceNo, int estimateId, int productType, int customerId, string soldJobId)
        {
            string result = string.Empty;
            try
            {
                string Salesman = "", email = "";
                string datesold = "";
                string SalesmanCell = "";
                string Customerinfo = "";
                string JobLocation = "";
                string CustomerPh = "";
                string SpclInstructions = "";
                string workArea = "";
                int productid;
                string materiallist = "";
                string category = "";
                string proposalTerms = "";
                bool IsPermit = false;
                bool IsHabitat = false;
                result = pdf_BLL.Instance.Workorderpdf();

                DataSet DS1 = new DataSet();

                //DS1 = shuttersBLL.Instance.FetchWorkOrderContractdetails(int.Parse(Session["CustomerId"].ToString()), estimateId);
                DS1 = shuttersBLL.Instance.FetchWorkOrderContractdetails(customerId, estimateId, productType);

                if (DS1 != null)
                {
                    Salesman = DS1.Tables[0].Rows[0]["Username"].ToString();
                    datesold = DateTime.Now.ToShortDateString();
                    email = DS1.Tables[0].Rows[0]["Email"].ToString();
                    SalesmanCell = DS1.Tables[0].Rows[0]["Phone"].ToString();
                    Customerinfo = DS1.Tables[0].Rows[0]["CustomerName"].ToString();
                    JobLocation = DS1.Tables[0].Rows[0]["JobLocation"].ToString();
                    CustomerPh = DS1.Tables[0].Rows[0]["phones"].ToString();
                    workArea = DS1.Tables[0].Rows[0]["WorkArea"].ToString();
                    proposalTerms = DS1.Tables[0].Rows[0]["ProposalTerms"].ToString();
                    if (DS1.Tables[0].Rows[0]["IsPermitRequired"].ToString() != "")
                    {
                        IsPermit = Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsPermitRequired"].ToString());
                    }
                    if (DS1.Tables[0].Rows[0]["IsHabitat"].ToString() != "")
                    {
                        IsHabitat = Convert.ToBoolean(DS1.Tables[0].Rows[0]["IsHabitat"].ToString());
                    }
                    //Style = DS1.Tables[0].Rows[0]["ProposalTerms"].ToString();
                    if (IsPermit == true)
                    {
                        SpclInstructions += "Building Permits required.";
                    }
                    if (IsHabitat == true)
                    {
                        SpclInstructions += "Salvageable material to be stored for Habitat for Humanity pick up.";
                    }
                    SpclInstructions += DS1.Tables[0].Rows[0]["SpecialInstruction"].ToString();
                    productid = Convert.ToInt32(DS1.Tables[0].Rows[0]["ProductId"].ToString());
                    DataSet ds = new DataSet();
                    //ds = shuttersBLL.Instance.GetMaterialList(int.Parse(Session["CustomerId"].ToString()), productid);
                    ds = shuttersBLL.Instance.GetMaterialList(customerId, soldJobId);//, estimateId, estimateId);
                    // materiallist = ds.Tables[0].Rows[0]["ProposalTerms"].ToString();
                    if (ds != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            materiallist += ds.Tables[0].Rows[i]["VendorCategoryNm"].ToString() + "    :    " + ds.Tables[0].Rows[i]["MaterialList"].ToString();
                            //materiallist += " </br> ";// System.Environment.NewLine;
                            materiallist += System.Environment.NewLine;
                        }
                    }

                }
                result = result.Replace("lbl_salesman", Salesman);
                result = result.Replace("lbl_date", datesold);
                result = result.Replace("lbl_email", email);
                result = result.Replace("lbl_cell", SalesmanCell);
                result = result.Replace("lbl_joblocation", JobLocation);
                result = result.Replace("lbl_Phone", CustomerPh);
                result = result.Replace("lbl_specialinstructions", SpclInstructions);
                result = result.Replace("lbl_workarea", workArea);
                result = result.Replace("lbl_CustomerName", Customerinfo);
                result = result.Replace("lbl_list", materiallist);
                result = result.Replace("lbl_JobId", soldJobId);
                result = result.Replace("lbl_ProposalTerms", proposalTerms);


            }
            catch (Exception ex)
            {
                ErrorLog.Instance.writeToLog(ex, "Custom", soldJobId);
                //LogManager.Instance.WriteToFlatFile(ex.Message, "Custom",1);// Request.ServerVariables["remote_addr"].ToString());

            }
            return result;
        }
        public string InstallerWorkOrder()
        {
            string result = pdf_DAL.Instance.InstallerWorkorderPDF();
            return result;

        }

        public string InstallerInvoice()
        {
            string result = pdf_DAL.Instance.InstallerInvoicePDF();
            return result;

        }
    }
}
