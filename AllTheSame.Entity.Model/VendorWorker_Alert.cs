//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllTheSame.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class VendorWorker_Alert
    {
        public int Id { get; set; }
        public int AlertId { get; set; }
        public int VendorWorkerId { get; set; }
        public bool IsRead { get; set; }
        public byte[] Version { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Alert Alert { get; set; }
        public virtual VendorWorker VendorWorker { get; set; }
    }
}
