using System.Data.Entity;
using System.Linq;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     UserSessionRepository
    /// </summary>
    public class UserSessionRepository : GenericRepository<UserSession>, IUserSessionRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSessionRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public UserSessionRepository(DbContext context)
            : base(context)
        {
            //
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserSession GetById(long id)
        {
            //--> need to define navigation property on type: return _dbset.Include(us => us).Where(u => u.Id == id).FirstOrDefault();
            return CurrentDbSet.SingleOrDefault(p => p.Id == id);
        }

        /// <summary>
        ///     GetSession
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserSession GetSession(long userId)
        {
            return CurrentDbSet.SingleOrDefault(w => w.UserId == userId);
        }

        /// <summary>
        ///     DeleteSession
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public long DeleteSession(long userId)
        {
            var exists = GetSession(userId);
            if (exists == null) return -1;

            return CurrentDbSet.Remove(exists).Id;
        }

        /// <summary>
        ///     Gets the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserSession GetByUserId(long userId)
        {
            //--> need to define navigation property on type: return _dbset.Include(us => us).Where(u => u.UserId == userId).FirstOrDefault();
            return CurrentDbSet.SingleOrDefault(p => p.UserId == userId);
        }
    }
}