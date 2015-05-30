using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;

namespace AllTheSame.Repository.UserData.interfaces
{
    /// <summary>
    ///     IPersonRepository
    /// </summary>
    public interface IPersonRepository : IGenericRepository<Person>, ISyncRepository<Person>
    {
        //unique methods beyond generic
        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Person GetById(long id);

        /// <summary>
        ///     Updates the password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        bool UpdatePassword(int id, Person person);
    }
}