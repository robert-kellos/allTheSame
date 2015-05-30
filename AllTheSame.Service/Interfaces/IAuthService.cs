using System;
using System.Collections.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Authentication;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    ///     IAuthService
    /// </summary>
    public interface IAuthService : IEntityService<UserSession, IAuthRepository>
    {
        /// <summary>
        ///     Gets the authentication.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserSession GetAuthentication(long id);

        /// <summary>
        ///     Creates a new UserSession with default paramaters and returns
        ///     a newly generated AuthToken
        /// </summary>
        /// s
        /// <param name="userId">The user identifier.</param>
        /// <param name="expiration">Expiration Date of Session in UTC Null for no expiration</param>
        /// <returns></returns>
        Guid CreateSession(int userId, DateTime? expiration);

        /// <summary>
        ///     Returns list of calcualted permissiosn for the user at the given orgId
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        IList<string> GetUserRoles(Guid sessionId, int orgId);
    }
}