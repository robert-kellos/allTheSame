using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class SignOut
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int VisitorId { get; set; }
        public int VendorWorkerId { get; set; }
        public System.DateTime TimeOut { get; set; }
        public Nullable<System.DateTime> TimeBack { get; set; }
        public string SignOutType { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Resident Resident { get; set; }
        public virtual VendorWorker VendorWorker { get; set; }
        public virtual Visitor Visitor { get; set; }
    }
}
