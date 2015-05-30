using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     CommunityRepository
    /// </summary>
    public class CommunityRepository : SyncRepository<Community>, ICommunityRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public CommunityRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}