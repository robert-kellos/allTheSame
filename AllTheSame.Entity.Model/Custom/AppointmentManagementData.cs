using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTheSame.Entity.Model.Custom
{
    /// <summary>
    /// AppointmentManagementData
    /// </summary>
    public class AppointmentManagementData
    {
        /// <summary>
        /// Gets or sets the appointment data.
        /// </summary>
        /// <value>
        /// The appointment data.
        /// </value>
        public Appointment AppointmentData { get; set; }
        /// <summary>
        /// Gets or sets the appointment type data.
        /// </summary>
        /// <value>
        /// The appointment type data.
        /// </value>
        public AppointmentType AppointmentTypeData { get; set; }
    }
}
