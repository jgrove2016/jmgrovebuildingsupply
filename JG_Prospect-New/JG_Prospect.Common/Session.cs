using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common
{
    public static class SessionKey
    {
        public enum Key
        {
            CustomerName, CustomerId, PagedataTable, ProductType, Username, usertype, loginid, UserId, LocationPicture, JobId,PreviousPage
        }
        public const string SortExpression = "SortExpression";
        public const string SortDirection = "SortDirection";      
    }

    public static class QueryStringKey
    {
        public enum Key
        {
            ProductTypeId, CustomerId, CustomId, ProductId, Edit, Gutter_GuardId, GutterId, SoffitId, EstimateId, EmailStatus, Other,SoldJobId
        }
    }

    public static class ViewStateKey
    {
        public enum Key
        {
            ReferenceId, ProductTypeId, SoldJobNo, ProductId, CustomerId,JobSequenceId
        }
        public const string PageIndex = "PageIndex";
        public const string GridViewData = "GridViewData";
    }
}
