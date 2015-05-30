using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            this.Alerts = new List<Alert>();
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
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual AppointmentType AppointmentType { get; set; }
        public virtual Resident Resident { get; set; }
        public virtual VendorWorker VendorWorker { get; set; }
    }
}
