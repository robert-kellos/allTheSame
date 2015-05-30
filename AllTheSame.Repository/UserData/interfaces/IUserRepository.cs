using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IUserRepository
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>, ISyncRepository<User>
    {
        //unique methods beyond generic
        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        User GetById(long id);

        /// <summary>
        ///     Creates new user and Person Record and returns auto-generated UserId
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns></returns>
        int CreateNewUser(User newUser);
    }
}