using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     KioskRepository
    /// </summary>
    public class KioskRepository : SyncRepository<Kiosk>, IKioskRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KioskRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public KioskRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}