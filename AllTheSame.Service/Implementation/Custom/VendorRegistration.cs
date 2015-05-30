using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Service.Interfaces.Custom;

namespace AllTheSame.Service.Implementation.Custom
{
    /// <summary>
    /// VendorRegistration
    /// </summary>
    public class VendorRegistration : IVendorRegistration
    {
        /// <summary>
        /// Gets the vendor data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public VendorManagement GetVendorData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the register vendor.
        /// </summary>
        /// <param name="vendorManagement"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <value>
        /// The register vendor.
        /// </value>
        public bool RegisterVendor(VendorManagement vendorManagement)
        {
            throw new NotImplementedException();
        }
    }
}
