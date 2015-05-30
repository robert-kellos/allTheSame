using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorCredentialRepository
    /// </summary>
    public class VendorCredentialRepository : SyncRepository<VendorCredential>, IVendorCredentialRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorCredentialRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorCredentialRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}