using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     PermissionRepository
    /// </summary>
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PermissionRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public PermissionRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}