using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     ICommunityRepository
    /// </summary>
    public interface ICommunityRepository : IGenericRepository<Community>, ISyncRepository<Community>
    {
    }
}