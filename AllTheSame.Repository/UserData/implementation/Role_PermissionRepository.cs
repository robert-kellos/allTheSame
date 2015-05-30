using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     Role_PermissionRepository
    /// </summary>
    public class Role_PermissionRepository : GenericRepository<Role_Permission>, IRole_PermissionRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Role_PermissionRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public Role_PermissionRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}