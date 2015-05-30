using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     KioskStatusRepository
    /// </summary>
    public class KioskStatusRepository : GenericRepository<KioskStatus>, IKioskStatusRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KioskStatusRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public KioskStatusRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}