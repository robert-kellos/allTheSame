using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class VendorCredDocument
    {
        public int Id { get; set; }
        public int VendorCredId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }
        public string DocType { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual VendorCredential VendorCredential { get; set; }
    }
}
