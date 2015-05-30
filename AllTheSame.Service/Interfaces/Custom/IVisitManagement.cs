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
    /// IVisitManagement
    /// </summary>
    public interface IVisitManagement
    {
        /// <summary>
        /// Gets or sets the get visit data.
        /// </summary>
        /// <value>
        /// The get visit data.
        /// </value>
        VisitManagentData GetVisitData { get; set; }

        /// <summary>
        /// Downloads the sign outs.
        /// </summary>
        /// <param name="signOut">The sign out.</param>
        bool DownloadSignOuts(SignOut signOut);

        /// <summary>
        /// Downloads the visits.
        /// </summary>
        /// <param name="visit">The visit.</param>
        bool DownloadVisits(Visit visit);
    }
}
