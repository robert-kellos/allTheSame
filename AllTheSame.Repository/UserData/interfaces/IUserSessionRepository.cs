using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IUserSessionRepository
    /// </summary>
    public interface IUserSessionRepository : IGenericRepository<UserSession>
    {
        //unique methods beyond generic
        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserSession GetById(long id);

        /// <summary>
        ///     Gets Session by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserSession GetSession(long userId);

        /// <summary>
        ///     Delete's Session
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        long DeleteSession(long userId);
    }
}