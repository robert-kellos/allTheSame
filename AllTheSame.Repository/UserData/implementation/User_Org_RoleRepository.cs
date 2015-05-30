using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     User_Org_RoleRepository
    /// </summary>
    public class User_Org_RoleRepository : GenericRepository<User_Org_Role>, IUser_Org_RoleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="User_Org_RoleRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public User_Org_RoleRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}