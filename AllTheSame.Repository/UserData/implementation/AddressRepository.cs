using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     AddressRepository
    /// </summary>
    public class AddressRepository : SyncRepository<Address>, IAddressRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AddressRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AddressRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}