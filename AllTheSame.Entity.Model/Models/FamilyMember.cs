using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class FamilyMember
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ResidentId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Person Person { get; set; }
        public virtual Resident Resident { get; set; }
    }
}
