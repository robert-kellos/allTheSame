using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTheSame.Entity.Model.Custom
{
    /// <summary>
    /// 
    /// </summary>
    public class AlertManagementData
    {
        /// <summary>
        /// Gets or sets the alert data.
        /// </summary>
        /// <value>
        /// The alert data.
        /// </value>
        Alert AlertData { get; set; }
        /// <summary>
        /// Gets or sets the alert type data.
        /// </summary>
        /// <value>
        /// The alert type data.
        /// </value>
        AlertType AlertTypeData { get; set; }

        /// <summary>
        /// Gets or sets the community worker alert data.
        /// </summary>
        /// <value>
        /// The community worker alert data.
        /// </value>
        CommunityWorker_Alert CommunityWorkerAlertData { get; set; }
        /// <summary>
        /// Gets or sets the vendor worker alert data.
        /// </summary>
        /// <value>
        /// The vendor worker alert data.
        /// </value>
        VendorWorker_Alert VendorWorkerAlertData { get; set; }
    }
}
