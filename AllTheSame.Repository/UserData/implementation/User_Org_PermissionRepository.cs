using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     User_Org_PermissionRepository
    /// </summary>
    public class User_Org_PermissionRepository : GenericRepository<User_Org_Permission>, IUser_Org_PermissionRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="User_Org_PermissionRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public User_Org_PermissionRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}