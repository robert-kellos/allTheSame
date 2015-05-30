using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IModuleRepository
    /// </summary>
    public interface IModuleRepository : IGenericRepository<Module>, ISyncRepository<Module>
    {
    }
}