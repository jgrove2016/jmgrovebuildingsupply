using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class Customer
    {
        public int id;
        public string firstName;
        public string lastName;
        public string customerNm;
        public string CustomerAddress;
        public string state;
        public string City;
        public string Zipcode;
        public string JobLocation;
        public string EstDate;
        public string EstTime;
        public string CellPh;
        public string HousePh;
        public string AltPh;
        public string Email;
        public string Email2;
        public string Email3;
        public string CallTakenby;
        public int Productofinterest;
        public int SecondaryProductofinterest;
        public string ProjectManager;
        public string Notes;
        public string Addedby;
        public string Leadtype;
        public string BillingAddress;
        public string BestTimetocontact;
        public string PrimaryContact;
        public string ContactPreference;
        public int Prospectid;
        public int AssignToid;
        public string Map1;
        public string Map2;
        public string status;
        public bool Isrepeated;
        public int missingcontacts;
        public string followupdate;
        public string ContactType;
        public string PhoneType;
        public string AddressType;
        public string BillingAddressType;
        public string CompetitorsBids;

    }

    public class CustomerContract
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ZipCityState { get; set; }
        public string JobLocation { get; set; }
        public string Email { get; set; }
        public string CellPh { get; set; }
        public string HousePh { get; set; }
        public decimal AmountB { get; set; }
        public decimal AmountA { get; set; }
        public string ProductName { get; set; }
        public string WorkArea { get; set; }
        public string ProposalTerms { get; set; }
        public string SpecialInstruction { get; set; }
        public string ShutterTop { get; set; }
        public int Quantity { get; set; }
        public string Style { get; set; }
        public string ColorName { get; set; }
        public string JobNumber { get; set; }
        public bool IsCustomerSuppliedMaterial { get; set; }
        public bool IsMatStorageApplicable { get; set; }
        public bool IsPermitRequired { get; set; }
        public bool IsHabitate { get; set; }
        public string CustomerSuppliedMaterial { get; set; }
        public string MatStorageApplicable { get; set; }

    }

    public class AnnualEvent
    {
        public int id;
        public string EventName;
        public string Eventdate;
        public int EventAddedBy;

    }

    public class AddForemanName
    {
        public int ProductId;
        public int CustomerId;
        public int ID;
        public string SoldJob;
    }
   

}

public class PhoneDashboard
{
    public int intMode { get; set; }
    public int intScriptId { get; set; }
    public string strScriptName { get; set; }
    public string strScriptDescription { get; set; }
}