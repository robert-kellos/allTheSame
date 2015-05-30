using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class RequirementType
    {
        public RequirementType()
        {
            this.Requirements = new List<Requirement>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
