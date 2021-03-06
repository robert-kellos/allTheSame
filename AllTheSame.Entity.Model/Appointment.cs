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
    
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            this.Alerts = new HashSet<Alert>();
        }
    
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int VendorWorkerId { get; set; }
        public int AppointmentTypeId { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool RemindVendor { get; set; }
        public bool AlertOnVendorSignIn { get; set; }
        public bool AlertOnVendorSignOut { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual AppointmentType AppointmentType { get; set; }
        public virtual Resident Resident { get; set; }
        public virtual VendorWorker VendorWorker { get; set; }
    }
}
