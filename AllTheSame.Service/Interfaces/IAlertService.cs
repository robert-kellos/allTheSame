using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    ///     IAlertService
    /// </summary>
    public interface IAlertService : IEntityService<Alert, IAlertRepository>
    {
    }
}