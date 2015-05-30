using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     VendorRepository
    /// </summary>
    public class VendorRepository : SyncRepository<Vendor>, IVendorRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VendorRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public VendorRepository(DbContext context)
            : base(context)
        {
            //
        }

        /// <summary>
        ///     Gets the vendor community.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        public IEnumerable<Vendor> GetVendorWorkerCommunity(int orgId)
        {
            return CurrentDbSet.Include(vendor => vendor.VendorWorkers);
        }

        /// <summary>
        ///     GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vendor GetById(long id)
        {
            return CurrentDbSet.FirstOrDefault(x => x.Id == id);
        }
    }
}