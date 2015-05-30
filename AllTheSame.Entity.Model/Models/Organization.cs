using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Organization
    {
        public Organization()
        {
            this.Communities = new List<Community>();
            this.User_Org_Permission = new List<User_Org_Permission>();
            this.Vendors = new List<Vendor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<short> Level { get; set; }
        public Nullable<int> OrgTypeId { get; set; }
        public Nullable<int> BillingAddressId { get; set; }
        public Nullable<int> ShippingAddressId { get; set; }
        public Nullable<int> IndustryId { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string GooglePlus { get; set; }
        public Nullable<int> AnnualRevenue { get; set; }
        public Nullable<int> NumEmployees { get; set; }
        public string WebURL { get; set; }
        public string OfficePhone { get; set; }
        public string AltPhone { get; set; }
        public string TickerSymbol { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        public virtual ICollection<Community> Communities { get; set; }
        public virtual Industry Industry { get; set; }
        public virtual ICollection<User_Org_Permission> User_Org_Permission { get; set; }
        public virtual OrgType OrgType { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
