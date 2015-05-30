using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Entity.Model;
using AllTheSame.Entity.Model.Custom;
using AllTheSame.Service.Interfaces.Custom;

namespace AllTheSame.Service.Implementation.Custom
{
    /// <summary>
    /// VendorManagement
    /// </summary>
    public class VendorManagement : IVendorManagement   
    {
        /// <summary>
        /// Gets the vendor requirement list.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<Requirement> GetVendorRequirementList(int vendorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the vendor visit history.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<Visit> GetVendorVisitHistory(int vendorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the vendor visit status.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public KioskStatus GetVendorVisitStatus(int vendorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the vendor detail.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public VendorManagementData GetVendorDetail(int vendorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the vendor detail.
        /// </summary>
        /// <param name="vendorDetail">The vendor detail.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool UpdateVendorDetail(VendorManagementData vendorDetail)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the vendor visit details.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public VisitManagentData GetVendorVisitDetails(int vendorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the vendor appointment detail.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public AppointmentManagementData GetVendorAppointmentDetail(int vendorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the vendor appointment detail.
        /// </summary>
        /// <param name="appointmentManagementData">The appointment management data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool UpdateVendorAppointmentDetail(AppointmentManagementData appointmentManagementData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prints the badge.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void PrintBadge(long? userId)
        {
            throw new NotImplementedException();
        }
    }
}
