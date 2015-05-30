using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            this.VendorAdmins = new List<VendorAdmin>();
            this.VendorWorkers = new List<VendorWorker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrgId { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<VendorAdmin> VendorAdmins { get; set; }
        public virtual ICollection<VendorWorker> VendorWorkers { get; set; }
    }
}
