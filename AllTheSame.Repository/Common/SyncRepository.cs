using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using AllTheSame.Common.Helpers;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Common.Logging;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     Provides methods for retreving data restricted by last DataSync operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class SyncRepository<T> : GenericRepository<T>, ISyncRepository<T> where T : class, IEntity<int>
    {
        /// <summary>
        ///     SyncRepository
        /// </summary>
        protected SyncRepository()
        {
            Audit.Log.Info("SyncRepository :: contructor initialized");
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        protected SyncRepository(DbContext context) : base(context)
        {
            Audit.Log.Info(string.Format("SyncRepository(DbContext) :: constructor initialized context: {0}", context));
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(int lastSyncId)
        {
            IEnumerable<T> result;
            result = GetAll(lastSyncId, new Expression<Func<T, object>>[0]);
            var list = (result as List<T>) != null ? (result as List<T>).Count : 0;

            Audit.Log.Info(string.Format("SyncRepository.GetAll(lastsyncId) :: lastSyncId : {0} - result count : {1}", lastSyncId, list));

            return result;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<T> GetAll(int lastSyncId, params Expression<Func<T, object>>[] navigationProperties)
        {
            var lastSync = GetLastSync(lastSyncId);
            if (lastSync == null)
            {
                return GetAll(navigationProperties);
            }

            var query = GetVersionPredicate(lastSync);

            Audit.Log.Info(string.Format("SyncRepository.GetAll(lastsyncId, params) :: lastSyncId : {0} - result Func : {1}", lastSyncId, query.Method.DeclaringType));

            return GetList(query, navigationProperties);
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int lastSyncId, Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            var andPredicate = AddSyncPredicate(@where, lastSyncId);

            return GetList(andPredicate, navigationProperties);
        }

        /// <summary>
        ///     Finds the by.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindBy(int lastSyncId, Expression<Func<T, bool>> predicate)
        {
            var andPred = AddSyncPredicate(predicate.Compile(), lastSyncId);
            Expression<Func<T, bool>> query = x => andPred(x);
            return FindBy(query);
        }

        /// <summary>
        ///     Gets the Last DataSync recorded for a specific Kiosk
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        public virtual DataSync GetLastDataSyncByKiosk(int kioskId)
        {
            var syncRepo = new DataSyncRepository(CurrentDbContext);

            return syncRepo.GetSingle(x => x.KioskId == kioskId);
        }

        /// <summary>
        ///     Retuns a linq expression to compare the specific Entity handled by the repositor
        ///     to the version in the last sync
        /// </summary>
        /// <param name="lastSync"></param>
        /// <returns></returns>
        public virtual Func<T, bool> GetVersionPredicate(DataSync lastSync)
        {
            var query =
                new Func<T, bool>(
                    v =>
                        AppUtility.IsHigherVersion(v.Version, lastSync.RowVersion));

            return query;
        }

        /// <summary>
        ///     Returns the DataSync Entity with provided id
        /// </summary>
        /// <param name="lastSyncId">The last synchronize identifier.</param>
        /// <returns></returns>
        protected virtual DataSync GetLastSync(int lastSyncId)
        {
            var dsRepo = new DataSyncRepository(CurrentDbContext);
            var lastSync = dsRepo.GetSingle(sync => sync.Id == lastSyncId);

            return lastSync;
        }

        /// <summary>
        ///     Appends the DataSync predicate to a passed in filter to restrict results of main filter
        ///     to those seen since the last sync
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="lastSyncId">The last synchronize identifier.</param>
        /// <returns></returns>
        protected virtual Func<T, bool> AddSyncPredicate(Func<T, bool> @where, int lastSyncId)
        {
            var lastSync = GetLastSync(lastSyncId);
            if (lastSync == null) return @where;

            var query = GetVersionPredicate(lastSync);
            var andPredicate = new Func<T, bool>(combined => @where(combined) && query(combined));

            return andPredicate;
        }
    }
}