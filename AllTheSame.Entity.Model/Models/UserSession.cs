using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.Guid SessionId { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Expiration { get; set; }
        public bool IsValid { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual User User { get; set; }
    }
}
