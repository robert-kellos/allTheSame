using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IResidentRepository
    /// </summary>
    public interface IResidentRepository : IGenericRepository<Resident>, ISyncRepository<Resident>
    {
    }
}