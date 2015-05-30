using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Visit
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public Nullable<int> VendorWorkerId { get; set; }
        public Nullable<int> VisitorId { get; set; }
        public System.DateTime TimeIn { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public string VisitType { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Resident Resident { get; set; }
    }
}
