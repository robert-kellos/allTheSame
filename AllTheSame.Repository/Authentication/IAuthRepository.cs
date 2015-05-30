using System.Collections.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.Authentication
{
    /// <summary>
    /// </summary>
    public interface IAuthRepository : IGenericRepository<UserSession>
    {
        //unique methods beyond generic
        /// <summary>
        ///     Gets the authentication.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserSession GetAuthentication(long id);

        /// <summary>
        ///     Returns Roles granted to the user with sessionId
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<string> GetUserRoles(int sessionId, int orgId);
    }
}