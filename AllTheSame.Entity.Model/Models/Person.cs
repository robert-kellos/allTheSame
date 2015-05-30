using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Person
    {
        public Person()
        {
            this.CommunityWorkers = new List<CommunityWorker>();
            this.FamilyMembers = new List<FamilyMember>();
            this.Residents = new List<Resident>();
            this.Users = new List<User>();
            this.VendorWorkers = new List<VendorWorker>();
            this.VendorAdmins = new List<VendorAdmin>();
            this.Visitors = new List<Visitor>();
        }

        public int Id { get; set; }
        public Nullable<System.Guid> LookUpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> BillingAddressId { get; set; }
        public Nullable<int> ShippingAddressId { get; set; }
        public string Salutation { get; set; }
        public string Title { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        public virtual ICollection<CommunityWorker> CommunityWorkers { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<VendorWorker> VendorWorkers { get; set; }
        public virtual ICollection<VendorAdmin> VendorAdmins { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
    }
}
