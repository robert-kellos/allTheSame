using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     SignOutRepository
    /// </summary>
    public class SignOutRepository : GenericRepository<SignOut>, ISignOutRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SignOutRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public SignOutRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}