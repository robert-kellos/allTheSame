using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    ///     Provides methods for manipulating the DataSync Table
    /// </summary>
    public interface IDataSyncService : IEntityService<DataSync, IDataSyncRepository>
    {
    }
}