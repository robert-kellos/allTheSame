using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorTypeRepository
    /// </summary>
    public class VendorTypeRepository : GenericRepository<VendorType>, IVendorTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorTypeRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorTypeRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}