using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IUserSessionService : IEntityService<UserSession, IUserSessionRepository>
    {
        //unique methods beyond generic
        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserSession GetById(long id);

        /// <summary>
        ///     Gets the session.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserSession GetSession(long userId);

        /// <summary>
        ///     Deletes the session.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        long DeleteSession(long userId);
    }
}