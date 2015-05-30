using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Resident
    {
        public Resident()
        {
            this.Appointments = new List<Appointment>();
            this.FamilyMembers = new List<FamilyMember>();
            this.SignOuts = new List<SignOut>();
            this.Visits = new List<Visit>();
        }

        public int Id { get; set; }
        public int CommunityId { get; set; }
        public int PersonId { get; set; }
        public string AssistantPhone { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Community Community { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<SignOut> SignOuts { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
