using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class VendorImport
    {
        public int Id { get; set; }
        public Nullable<System.Guid> ImportId { get; set; }
        public Nullable<int> VendorId { get; set; }
        public Nullable<int> OrgId { get; set; }
        public string VendorName { get; set; }
        public string OrgName { get; set; }
        public string BillingLine1 { get; set; }
        public string BillingLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public string ShippingLine1 { get; set; }
        public string ShippingLine2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingPostalCode { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string GooglePlus { get; set; }
        public Nullable<int> AnnualRevenue { get; set; }
        public Nullable<int> NumEmployees { get; set; }
        public string WebURL { get; set; }
        public string OfficePhone { get; set; }
        public string AltPhone { get; set; }
        public string TickerSymbol { get; set; }
        public Nullable<System.DateTime> ProcessedOn { get; set; }
        public string Industry { get; set; }
    }
}
