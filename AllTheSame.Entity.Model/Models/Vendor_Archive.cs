using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Vendor_Archive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrgId { get; set; }
        public byte[] ArchivedVersion { get; set; }
        public System.DateTime ArchivedDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
