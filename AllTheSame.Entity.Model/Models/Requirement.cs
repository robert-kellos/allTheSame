using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Requirement
    {
        public Requirement()
        {
            this.VendorCredentials = new List<VendorCredential>();
        }

        public int Id { get; set; }
        public int CommunityId { get; set; }
        public Nullable<int> RequirementTypeId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Community Community { get; set; }
        public virtual RequirementType RequirementType { get; set; }
        public virtual ICollection<VendorCredential> VendorCredentials { get; set; }
    }
}
