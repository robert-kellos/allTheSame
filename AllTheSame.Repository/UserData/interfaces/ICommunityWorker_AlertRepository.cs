using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     ICommunityWorker_AlertRepository
    /// </summary>
    public interface ICommunityWorker_AlertRepository : IGenericRepository<CommunityWorker_Alert>,
        ISyncRepository<CommunityWorker_Alert>
    {
    }
}