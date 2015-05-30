using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IVendorCredentialRepository
    /// </summary>
    public interface IVendorCredentialRepository : IGenericRepository<VendorCredential>,
        ISyncRepository<VendorCredential>
    {
    }
}