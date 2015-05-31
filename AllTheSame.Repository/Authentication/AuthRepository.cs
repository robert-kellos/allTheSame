using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.Authentication
{
    /// <summary>
    /// </summary>
    public class AuthRepository : GenericRepository<UserSession>, IAuthRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AuthRepository(DbContext context)
            : base(context)
        {
            //
        }

        /// <summary>
        ///     Gets the authentication.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserSession GetAuthentication(long id)
        {
            //--> need to define navigation property on type: return _dbset.Include(x=>x.User).Where(u=>u.UserId == id).SingleOrDefault();
            return CurrentDbSet.SingleOrDefault(p => p.Id == id);
        }

        public IList<string> GetUserRoles(int sessionId, int orgId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     GetUserRoles - list of roles belonging to
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<string> GetUserRoles(int? userId, int? orgId)
        {
            var allTheSameContext = CurrentDbContext as AllTheSameDbContext;
            if (allTheSameContext == null)
            {
                throw new Exception("Context must be of type " + typeof (AllTheSameDbContext));
            }

            return allTheSameContext.spGetOrgPermissions(orgId, userId).Select(spResult => spResult.Code).ToList();
        }
    }
}