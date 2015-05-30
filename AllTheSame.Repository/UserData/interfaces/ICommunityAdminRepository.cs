using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     ICommunityAdminRepository
    /// </summary>
    public interface ICommunityAdminRepository : IGenericRepository<CommunityAdmin>, ISyncRepository<CommunityAdmin>
    {
    }
}