using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IVendorCredDocumentRepository
    /// </summary>
    public interface IVendorCredDocumentRepository : IGenericRepository<VendorCredDocument>,
        ISyncRepository<VendorCredDocument>
    {
    }
}