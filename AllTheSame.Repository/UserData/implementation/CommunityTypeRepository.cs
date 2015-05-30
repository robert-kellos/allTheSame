using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     CommunityTypeRepository
    /// </summary>
    public class CommunityTypeRepository : GenericRepository<CommunityType>, ICommunityTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityTypeRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public CommunityTypeRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}