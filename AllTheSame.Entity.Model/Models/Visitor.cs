using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            this.SignOuts = new List<SignOut>();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<SignOut> SignOuts { get; set; }
    }
}
