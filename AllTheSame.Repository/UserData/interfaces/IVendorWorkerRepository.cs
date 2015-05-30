using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IVendorWorkerRepository
    /// </summary>
    public interface IVendorWorkerRepository : IGenericRepository<VendorWorker>, ISyncRepository<VendorWorker>
    {
    }
}