using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AllTheSame.Common.Interfaces.Generic
{
    /// <summary>
    ///     IGenericCrud
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericCrud<TEntity> : IDisposable
    {
        /// <summary>
        ///     Count of current db table set
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        ///     Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<TEntity> Filter<TKey>(Expression<Func<TEntity, bool>> filter,
            out int total, int index = 0, int size = 50);

        /// <summary>
        ///     Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        IList<TEntity> GetList(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        ///     Gets the list asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        ///     Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Finds the by asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        ///     Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="include">The include.</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> include);

        /// <summary>
        ///     Finds the by asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="include">The include.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> include);

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        TEntity GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        ///     Gets the single asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        Task<TEntity> GetSingleAsync(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        ///     AddMany
        /// </summary>
        /// <param name="items">The items.</param>
        void AddMany(params TEntity[] items);

        /// <summary>
        ///     UpdateMany
        /// </summary>
        /// <param name="items">The items.</param>
        void UpdateMany(params TEntity[] items);

        /// <summary>
        ///     RemoveMany
        /// </summary>
        /// <param name="items">The items.</param>
        void RemoveMany(params TEntity[] items);

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        ///     Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Edit(TEntity entity);

        /// <summary>
        ///     Saves this instance.
        /// </summary>
        int Save();
    }
}