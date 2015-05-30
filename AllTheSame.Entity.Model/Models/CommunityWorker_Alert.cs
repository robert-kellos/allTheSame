using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class CommunityWorker_Alert
    {
        public int Id { get; set; }
        public int CommunityWorkerId { get; set; }
        public int AlertId { get; set; }
        public bool IsRead { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Alert Alert { get; set; }
        public virtual CommunityWorker CommunityWorker { get; set; }
    }
}
