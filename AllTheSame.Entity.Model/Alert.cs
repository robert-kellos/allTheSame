//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllTheSame.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alert()
        {
            this.CommunityWorker_Alert = new HashSet<CommunityWorker_Alert>();
            this.VendorWorker_Alert = new HashSet<VendorWorker_Alert>();
        }
    
        public int Id { get; set; }
        public int AlertTypeId { get; set; }
        public Nullable<int> AppointmentId { get; set; }
        public Nullable<int> KioskId { get; set; }
        public string Description { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommunityWorker_Alert> CommunityWorker_Alert { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorWorker_Alert> VendorWorker_Alert { get; set; }
        public virtual AlertType AlertType { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Kiosk Kiosk { get; set; }
    }
}
