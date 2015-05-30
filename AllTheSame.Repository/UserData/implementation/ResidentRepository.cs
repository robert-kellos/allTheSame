using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     ResidentRepository
    /// </summary>
    public class ResidentRepository : SyncRepository<Resident>, IResidentRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResidentRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public ResidentRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}