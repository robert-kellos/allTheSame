using System.Collections.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    ///     IVendorService
    /// </summary>
    public interface IVendorService : IEntityService<Vendor, IVendorRepository>, ISyncService<Vendor>
    {
        /// <summary>
        ///     Gets the vendor worker community.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        IEnumerable<Vendor> GetVendorWorkerCommunity(int orgId);

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Vendor GetById(long id);
    }
}