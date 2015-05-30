using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Kiosk
    {
        public Kiosk()
        {
            this.Alerts = new List<Alert>();
            this.DataSyncs = new List<DataSync>();
        }

        public int Id { get; set; }
        public Nullable<int> CommunityId { get; set; }
        public int KioskStatusId { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string IdentifierType { get; set; }
        public string OnSiteLocationDesc { get; set; }
        public int BadgesRemaining { get; set; }
        public int BadgeAlertCount { get; set; }
        public Nullable<System.DateTime> RestartTime { get; set; }
        public Nullable<int> SessionMaxHours { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual Community Community { get; set; }
        public virtual ICollection<DataSync> DataSyncs { get; set; }
        public virtual KioskStatu KioskStatu { get; set; }
    }
}
