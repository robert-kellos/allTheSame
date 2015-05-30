using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IOrganizationRepository
    /// </summary>
    public interface IOrganizationRepository : IGenericRepository<Organization>, ISyncRepository<Organization>
    {
    }
}