using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Alert
    {
        public Alert()
        {
            this.CommunityWorker_Alert = new List<CommunityWorker_Alert>();
            this.VendorWorker_Alert = new List<VendorWorker_Alert>();
        }

        public int Id { get; set; }
        public int AlertTypeId { get; set; }
        public Nullable<int> AppointmentId { get; set; }
        public Nullable<int> KioskId { get; set; }
        public string Description { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<CommunityWorker_Alert> CommunityWorker_Alert { get; set; }
        public virtual ICollection<VendorWorker_Alert> VendorWorker_Alert { get; set; }
        public virtual AlertType AlertType { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Kiosk Kiosk { get; set; }
    }
}
