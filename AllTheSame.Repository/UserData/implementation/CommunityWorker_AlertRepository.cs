using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     CommunityWorker_AlertRepository
    /// </summary>
    public class CommunityWorker_AlertRepository : SyncRepository<CommunityWorker_Alert>,
        ICommunityWorker_AlertRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityWorker_AlertRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public CommunityWorker_AlertRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}