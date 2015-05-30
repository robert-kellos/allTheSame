using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     RoleRepository
    /// </summary>
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoleRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public RoleRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}