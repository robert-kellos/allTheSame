using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorWorker_AlertRepository
    /// </summary>
    public class VendorWorker_AlertRepository : SyncRepository<VendorWorker_Alert>, IVendorWorker_AlertRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorWorker_AlertRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorWorker_AlertRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}