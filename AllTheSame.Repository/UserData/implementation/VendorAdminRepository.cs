using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorAdminRepository
    /// </summary>
    public class VendorAdminRepository : SyncRepository<VendorAdmin>, IVendorAdminRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorAdminRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorAdminRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}