using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class VendorCredential
    {
        public VendorCredential()
        {
            this.VendorCredDocuments = new List<VendorCredDocument>();
        }

        public int Id { get; set; }
        public int RequirementId { get; set; }
        public int VendorWorkerId { get; set; }
        public bool IsAttested { get; set; }
        public bool IsConfirmed { get; set; }
        public Nullable<System.DateTime> ConfirmedOn { get; set; }
        public Nullable<int> ConfirmedByUserId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<VendorCredDocument> VendorCredDocuments { get; set; }
        public virtual VendorWorker VendorWorker { get; set; }
    }
}
