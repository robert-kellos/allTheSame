using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Service.Implementation.Custom;

namespace AllTheSame.Service.Interfaces.Custom
{
    /// <summary>
    /// IVendorRegistration
    /// </summary>
    public interface IVendorRegistration
    {
        /// <summary>
        /// Gets the vendor data.
        /// </summary>
        /// <returns></returns>
        VendorManagement GetVendorData();
        /// <summary>
        /// Gets or sets the register vendor.
        /// </summary>
        /// <value>
        /// The register vendor.
        /// </value>
        bool RegisterVendor(VendorManagement vendorManagement);
    }
}
