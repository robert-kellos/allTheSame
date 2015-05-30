using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class User_Org_Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrgId { get; set; }
        public int RoleId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
