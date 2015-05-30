using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Entity.Model.Custom;

namespace AllTheSame.Service.Interfaces.Custom
{
    /// <summary>
    /// IAppointmentManagement
    /// </summary>
    public interface IAppointmentManagement
    {
        /// <summary>
        /// Gets the appointment management data.
        /// </summary>
        /// <returns></returns>
        AppointmentManagementData GetAppointmentManagementData();
        /// <summary>
        /// Sets the appointment data.
        /// </summary>
        /// <param name="appointmentManagementData">The appointment management data.</param>
        /// <returns></returns>
        bool SetAppointmentData(AppointmentManagementData appointmentManagementData);
    }
}
