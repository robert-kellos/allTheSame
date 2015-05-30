using System.Data.Entity;
using System.Linq;
using AllTheSame.Common.Extensions;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     UserRepository
    /// </summary>
    public class UserRepository : SyncRepository<User>, IUserRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(DbContext context)
            : base(context)
        {
            //
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User GetById(long id)
        {
            //--> need to define navigation property on type: return _dbset.Include(x => x.UserSessions.Where(us => us.UserId == id)).Where(u => u.Id == id).FirstOrDefault();
            return CurrentDbSet.SingleOrDefault(p => p.Id == id);
        }

        /// <summary>
        ///     Creates new user and Person Record and returns auto-generated UserId
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int CreateNewUser(User newUser)
        {
            var result = -1;

            var added = CurrentDbSet.Add(newUser);
            if (!added.IsNull())
                result = CurrentDbContext.SaveChanges();

            return result;
        }
    }
}