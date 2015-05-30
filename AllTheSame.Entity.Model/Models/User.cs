using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class User
    {
        public User()
        {
            this.User_Org_Permission = new List<User_Org_Permission>();
            this.UserSessions = new List<UserSession>();
            this.VendorCredentials = new List<VendorCredential>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string KioskPinHash { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<User_Org_Permission> User_Org_Permission { get; set; }
        public virtual ICollection<UserSession> UserSessions { get; set; }
        public virtual ICollection<VendorCredential> VendorCredentials { get; set; }
    }
}
