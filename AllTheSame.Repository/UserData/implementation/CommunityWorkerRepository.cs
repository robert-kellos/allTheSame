using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     CommunityWorkerRepository
    /// </summary>
    public class CommunityWorkerRepository : SyncRepository<CommunityWorker>, ICommunityWorkerRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityWorkerRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public CommunityWorkerRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}