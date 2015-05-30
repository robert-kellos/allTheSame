using System.Collections.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IVendorRepository
    /// </summary>
    public interface IVendorRepository : IGenericRepository<Vendor>, ISyncRepository<Vendor>
    {
        /// <summary>
        ///     Gets the vendor community.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        IEnumerable<Vendor> GetVendorWorkerCommunity(int orgId);

        /// <summary>
        ///     GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Vendor GetById(long id);
    }
}