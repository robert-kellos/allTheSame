using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IVendorAdminRepository
    /// </summary>
    public interface IVendorAdminRepository : IGenericRepository<VendorAdmin>, ISyncRepository<VendorAdmin>
    {
    }
}