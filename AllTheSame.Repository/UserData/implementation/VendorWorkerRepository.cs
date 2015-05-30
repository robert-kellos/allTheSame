using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorWorkerRepository
    /// </summary>
    public class VendorWorkerRepository : SyncRepository<VendorWorker>, IVendorWorkerRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorWorkerRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorWorkerRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}