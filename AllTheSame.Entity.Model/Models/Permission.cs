using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Permission
    {
        public Permission()
        {
            this.Role_Permission = new List<Role_Permission>();
            this.User_Org_Permission = new List<User_Org_Permission>();
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<Role_Permission> Role_Permission { get; set; }
        public virtual ICollection<User_Org_Permission> User_Org_Permission { get; set; }
    }
}
