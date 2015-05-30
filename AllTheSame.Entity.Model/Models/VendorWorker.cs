using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class VendorWorker
    {
        public VendorWorker()
        {
            this.Appointments = new List<Appointment>();
            this.SignOuts = new List<SignOut>();
            this.VendorCredentials = new List<VendorCredential>();
            this.VendorWorker_Alert = new List<VendorWorker_Alert>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public int VendorId { get; set; }
        public Nullable<int> VendorTypeId { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<SignOut> SignOuts { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<VendorCredential> VendorCredentials { get; set; }
        public virtual VendorType VendorType { get; set; }
        public virtual ICollection<VendorWorker_Alert> VendorWorker_Alert { get; set; }
    }
}
