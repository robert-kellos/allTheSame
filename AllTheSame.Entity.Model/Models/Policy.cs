using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Policy
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }
        public string Description { get; set; }
        public string DocumentURL { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Community Community { get; set; }
    }
}
