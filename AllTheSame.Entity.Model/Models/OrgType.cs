using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class OrgType
    {
        public OrgType()
        {
            this.Organizations = new List<Organization>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
