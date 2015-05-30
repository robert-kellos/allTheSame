using System;
using AllTheSame.Service.Interfaces.Custom;

namespace AllTheSame.Service.Implementation.Custom
{
    /// <summary>
    /// AlertManagement
    /// </summary>
    public class AlertManagement : IAlertManagement
    {
        /// <summary>
        /// Gets the alert data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Entity.Model.Custom.AlertManagementData GetAlertData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the alert data.
        /// </summary>
        /// <param name="alertManagementData">The alert management data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SetAlertData(Entity.Model.Custom.AlertManagementData alertManagementData)
        {
            throw new NotImplementedException();
        }
    }
}
