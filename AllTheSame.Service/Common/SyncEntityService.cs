using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Common.Logging;
using AllTheSame.Repository.Common;

namespace AllTheSame.Service.Common
{
    /// <summary>
    ///     Provides additonal methods to EntityService to allow restricting of data by the last DataSync
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    [Serializable]
    public class SyncEntityService<TEntity, TRepository> : EntityService<TEntity, TRepository>, ISyncService<TEntity>
        where TEntity : class, IEntity<int>
        where TRepository : class, ISyncRepository<TEntity>, IGenericRepository<TEntity>
    {
        /// <summary>
        ///     Creates Instance
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public SyncEntityService(IUnitOfWork unitOfWork, TRepository repository)
            : base(unitOfWork, repository)
        {
            Audit.Log.Info(string.Format("SyncEntityService contructor :: unitOfWork params"));
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(int lastSyncId)
        {
            return Repository.GetAll(lastSyncId);
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll(int lastSyncId,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return Repository.GetAll(lastSyncId, navigationProperties);
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual IList<TEntity> GetList(int lastSyncId, Func<TEntity, bool> @where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return Repository.GetList(lastSyncId, @where, navigationProperties);
        }

        /// <summary>
        ///     Finds the by.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> FindBy(int lastSyncId, Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FindBy(lastSyncId, predicate);
        }
    }
}