using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class DataSync
    {
        public int Id { get; set; }
        public Nullable<int> KioskId { get; set; }
        public System.DateTime SyncDateTime { get; set; }
        public byte[] RowVersion { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Kiosk Kiosk { get; set; }
    }
}
