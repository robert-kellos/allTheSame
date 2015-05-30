using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTheSame.Entity.Model.Custom
{
    /// <summary>
    /// VendorManagementData
    /// </summary>
    public class VendorManagementData
    {
        /// <summary>
        /// Gets or sets the vendor data.
        /// </summary>
        /// <value>
        /// The vendor data.
        /// </value>
        public Vendor VendorData { get; set; }
        /// <summary>
        /// Gets or sets the vendor admin data.
        /// </summary>
        /// <value>
        /// The vendor admin data.
        /// </value>
        public VendorAdmin VendorAdminData { get; set; }
        /// <summary>
        /// Gets or sets the vendor cred document data.
        /// </summary>
        /// <value>
        /// The vendor cred document data.
        /// </value>
        public VendorCredDocument VendorCredDocumentData { get; set; }
        /// <summary>
        /// Gets or sets the vendor credential data.
        /// </summary>
        /// <value>
        /// The vendor credential data.
        /// </value>
        public VendorCredential VendorCredentialData { get; set; }
        /// <summary>
        /// Gets or sets the vendor type data.
        /// </summary>
        /// <value>
        /// The vendor type data.
        /// </value>
        public VendorType VendorTypeData { get; set; }
        /// <summary>
        /// Gets or sets the vendor worker data.
        /// </summary>
        /// <value>
        /// The vendor worker data.
        /// </value>
        public VendorWorker VendorWorkerData { get; set; }
        /// <summary>
        /// Gets or sets the vendor worker alert data.
        /// </summary>
        /// <value>
        /// The vendor worker alert data.
        /// </value>
        public VendorWorker_Alert VendorWorkerAlertData { get; set; }
        /// <summary>
        /// Gets or sets the vendor archive data.
        /// </summary>
        /// <value>
        /// The vendor archive data.
        /// </value>
        //public Vendor_Archive VendorArchiveData { get; set; }
    }
}
