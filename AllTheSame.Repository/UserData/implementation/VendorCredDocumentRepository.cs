using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorCredDocumentRepository
    /// </summary>
    public class VendorCredDocumentRepository : SyncRepository<VendorCredDocument>, IVendorCredDocumentRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorCredDocumentRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorCredDocumentRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}