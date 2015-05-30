using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     CommunityAdminRepository
    /// </summary>
    public class CommunityAdminRepository : SyncRepository<CommunityAdmin>, ICommunityAdminRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityAdminRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public CommunityAdminRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}