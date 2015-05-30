using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IVendorWorker_AlertRepository
    /// </summary>
    public interface IVendorWorker_AlertRepository : IGenericRepository<VendorWorker_Alert>,
        ISyncRepository<VendorWorker_Alert>
    {
    }
}