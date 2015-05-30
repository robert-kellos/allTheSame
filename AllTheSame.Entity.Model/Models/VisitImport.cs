using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class VisitImport
    {
        public int Id { get; set; }
        public Nullable<int> VisitId { get; set; }
        public Nullable<System.Guid> ImportId { get; set; }
        public Nullable<System.DateTime> TimeIn { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public string VisitType { get; set; }
        public string CommunityName { get; set; }
        public string CommunityPhone { get; set; }
        public string ResidentFirstName { get; set; }
        public string ResidentLastName { get; set; }
        public string ResidentEmail { get; set; }
        public string ResidentPhone { get; set; }
        public string VendorType { get; set; }
        public string VendorCompanyName { get; set; }
        public string VendorCompanyPhone { get; set; }
        public string VendorWorkerFirstName { get; set; }
        public string VendorWorkerLastName { get; set; }
        public string VendorWorkerEmail { get; set; }
        public string VendorWorkerPhone { get; set; }
        public string VisitorFirstName { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorEmail { get; set; }
        public string VisitorPhone { get; set; }
        public Nullable<System.DateTime> ProcessedOn { get; set; }
    }
}
