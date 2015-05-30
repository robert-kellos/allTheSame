using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class CommunityType
    {
        public CommunityType()
        {
            this.Communities = new List<Community>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Community> Communities { get; set; }
    }
}
