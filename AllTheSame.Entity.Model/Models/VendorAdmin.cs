using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class VendorAdmin
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int VendorId { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Person Person { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
