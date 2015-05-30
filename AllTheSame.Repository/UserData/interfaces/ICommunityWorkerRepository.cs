using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     ICommunityWorkerRepository
    /// </summary>
    public interface ICommunityWorkerRepository : IGenericRepository<CommunityWorker>, ISyncRepository<CommunityWorker>
    {
    }
}