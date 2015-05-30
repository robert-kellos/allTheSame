using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AllTheSame.Service.Common
{
    /// <summary>
    ///     Provides Methods for restricting returned records based on the last DataSync.Id consumed by the client
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISyncService<T>
    {
        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(int lastSyncId);

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <param name="lastSyncId">Last DataSync Id from which to restrict updates</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        IList<T> GetAll(int lastSyncId, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        IList<T> GetList(int lastSyncId, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        ///     Finds the by.
        /// </summary>
        /// <param name="lastSyncId"></param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IEnumerable<T> FindBy(int lastSyncId, Expression<Func<T, bool>> predicate);
    }
}