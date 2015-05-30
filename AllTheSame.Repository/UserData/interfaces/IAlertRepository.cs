using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IAlertRepository
    /// </summary>
    public interface IAlertRepository : IGenericRepository<Alert>, ISyncRepository<Alert>
    {
    }
}