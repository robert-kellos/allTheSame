using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllTheSame.Service.Interfaces.Custom;

namespace AllTheSame.Service.Implementation.Custom
{
    /// <summary>
    /// VisitManagement
    /// </summary>
    public class VisitManagement : IVisitManagement
    {
        /// <summary>
        /// Gets or sets the get visit data.
        /// </summary>
        /// <value>
        /// The get visit data.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public Entity.Model.Custom.VisitManagentData GetVisitData { get; set; }

        /// <summary>
        /// Downloads the sign outs.
        /// </summary>
        /// <param name="signOut">The sign out.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DownloadSignOuts(Entity.Model.SignOut signOut)
        {
            //TODO: dl code
            return false;
        }

        /// <summary>
        /// Downloads the visits.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DownloadVisits(Entity.Model.Visit visit)
        {
            //TODO: dl code
            return false;
        }


    }
}
