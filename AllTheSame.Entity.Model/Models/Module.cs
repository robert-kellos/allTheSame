using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Module
    {
        public Module()
        {
            this.Permissions = new List<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
