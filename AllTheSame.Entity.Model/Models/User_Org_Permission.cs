using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class User_Org_Permission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrgId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAllowed { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
