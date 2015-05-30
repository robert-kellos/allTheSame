using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Entity.Model;
using AllTheSame.Entity.Model.Custom;

namespace AllTheSame.Service.Interfaces.Custom
{
    /// <summary>
    /// IVendorManagement
    /// </summary>
    public interface IVendorManagement
    {
        /// <summary>
        /// Gets the vendor requirement list.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        IEnumerable<Requirement> GetVendorRequirementList(int vendorId);
        /// <summary>
        /// Gets the vendor visit history.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        IEnumerable<Visit> GetVendorVisitHistory(int vendorId);
        /// <summary>
        /// Gets the vendor visit status.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        KioskStatus GetVendorVisitStatus(int vendorId);

        /// <summary>
        /// Gets the vendor detail.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        VendorManagementData GetVendorDetail(int vendorId);
        /// <summary>
        /// Updates the vendor detail.
        /// </summary>
        /// <param name="vendorDetail">The vendor detail.</param>
        /// <returns></returns>
        bool UpdateVendorDetail(VendorManagementData vendorDetail);

        /// <summary>
        /// Gets the vendor visit details.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        VisitManagentData GetVendorVisitDetails(int vendorId);
        /// <summary>
        /// Gets the vendor appointment detail.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns></returns>
        AppointmentManagementData GetVendorAppointmentDetail(int vendorId);
        /// <summary>
        /// Updates the vendor appointment detail.
        /// </summary>
        /// <param name="appointmentManagementData">The appointment management data.</param>
        /// <returns></returns>
        bool UpdateVendorAppointmentDetail(AppointmentManagementData appointmentManagementData);

        /// <summary>
        /// Prints the badge.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        void PrintBadge(long? userId);
    }
}
