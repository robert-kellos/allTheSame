using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     AlertRepository
    /// </summary>
    public class AlertRepository : SyncRepository<Alert>, IAlertRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlertRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public AlertRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}