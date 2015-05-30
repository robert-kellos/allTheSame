using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class CommunityWorker
    {
        public CommunityWorker()
        {
            this.CommunityWorker_Alert = new List<CommunityWorker_Alert>();
        }

        public int Id { get; set; }
        public int CommunityId { get; set; }
        public int PersonId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Community Community { get; set; }
        public virtual ICollection<CommunityWorker_Alert> CommunityWorker_Alert { get; set; }
        public virtual Person Person { get; set; }
    }
}
