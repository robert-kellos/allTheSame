using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Role
    {
        public Role()
        {
            this.Role_Permission = new List<Role_Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Role_Permission> Role_Permission { get; set; }
    }
}
