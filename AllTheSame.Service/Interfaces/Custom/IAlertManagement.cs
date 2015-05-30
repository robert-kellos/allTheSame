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
    /// IAlertManagement
    /// </summary>
    public interface IAlertManagement
    {
        /// <summary>
        /// Gets the alert data.
        /// </summary>
        /// <returns></returns>
        AlertManagementData GetAlertData();
        /// <summary>
        /// Sets the alert data.
        /// </summary>
        /// <param name="alertManagementData">The alert management data.</param>
        /// <returns></returns>
        bool SetAlertData(AlertManagementData alertManagementData);
    }
}
