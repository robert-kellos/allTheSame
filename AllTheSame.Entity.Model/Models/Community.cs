using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Community
    {
        public Community()
        {
            this.CommunityWorkers = new List<CommunityWorker>();
            this.Kiosks = new List<Kiosk>();
            this.Policies = new List<Policy>();
            this.Requirements = new List<Requirement>();
            this.Residents = new List<Resident>();
        }

        public int Id { get; set; }
        public int OrgId { get; set; }
        public Nullable<int> CommunityTypeId { get; set; }
        public Nullable<int> IndustryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Raiting { get; set; }
        public Nullable<int> NumBeds { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual CommunityType CommunityType { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<CommunityWorker> CommunityWorkers { get; set; }
        public virtual ICollection<Kiosk> Kiosks { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
    }
}
