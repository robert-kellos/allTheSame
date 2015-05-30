using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTheSame.Entity.Model.Custom
{
    public class VisitManagentData
    {
        public Visit VisitData { get; set; }
        public Visitor VisitorData { get; set; }

        public Kiosk KioskData { get; set; }

        public Resident ResidentData { get; set; }
        public Person PersonData { get; set; }
        public Vendor VendorData { get; set; }
        public VendorWorker VendorWorkerData { get; set; }
        public FamilyMember FamilyMemberData { get; set; }
        public CommunityWorker CommunityWorkerData { get; set; }

        public SignOut SignOutData { get; set; }
    }
}
