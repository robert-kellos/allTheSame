using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Address
    {
        public Address()
        {
            this.People = new List<Person>();
            this.Organizations = new List<Organization>();
            this.Organizations1 = new List<Organization>();
            this.People1 = new List<Person>();
        }

        public int Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<Organization> Organizations1 { get; set; }
        public virtual ICollection<Person> People1 { get; set; }
    }
}
