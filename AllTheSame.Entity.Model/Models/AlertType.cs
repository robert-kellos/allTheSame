using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class AlertType
    {
        public AlertType()
        {
            this.Alerts = new List<Alert>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string FormatText { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
