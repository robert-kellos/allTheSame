using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Service.Interfaces.Custom;

namespace AllTheSame.Service.Implementation.Custom
{
    /// <summary>
    /// AppointmentManagement
    /// </summary>
    public class AppointmentManagement : IAppointmentManagement
    {
        /// <summary>
        /// Gets the appointment management data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Entity.Model.Custom.AppointmentManagementData GetAppointmentManagementData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the appointment data.
        /// </summary>
        /// <param name="appointmentManagementData">The appointment management data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SetAppointmentData(Entity.Model.Custom.AppointmentManagementData appointmentManagementData)
        {
            throw new NotImplementedException();
        }
    }
}
