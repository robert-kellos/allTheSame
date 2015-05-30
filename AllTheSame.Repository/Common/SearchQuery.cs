using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     SearchQuery - handle and return the queried results in common list types
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class SearchQuery<TEntity>
    {
        //------------------------------------------------------------
        /// <summary>
        ///     Default constructor
        /// </summary>
        public SearchQuery()
        {
            Filters = new List<Expression<Func<TEntity, bool>>>();
            SortCriterias = new List<ISortCriteria<TEntity>>();
        }

        //-----------------------------------------------------------
        /// <summary>
        ///     Contains a list of filters to be applied to the query.
        /// </summary>
        /// <value>
        ///     The filters.
        /// </value>
        public List<Expression<Func<TEntity, bool>>> Filters { get; protected set; }

        //-----------------------------------------------------------
        /// <summary>
        ///     Contains a list of criterias that would be used for sorting.
        /// </summary>
        /// <value>
        ///     The sort criterias.
        /// </value>
        public List<ISortCriteria<TEntity>> SortCriterias { get; protected set; }

        //-------------------------------------------------------------
        /// <summary>
        ///     Contains a list of properties that would be eagerly loaded
        ///     with he query.
        /// </summary>
        /// <value>
        ///     The include properties.
        /// </value>
        public string IncludeProperties { get; set; }

        //-------------------------------------------------------------
        /// <summary>
        ///     Number of items to be skipped. Useful for paging.
        /// </summary>
        /// <value>
        ///     The skip.
        /// </value>
        public int Skip { get; set; }

        //-------------------------------------------------------------
        /// <summary>
        ///     Represents the number of items to be returned by the query.
        /// </summary>
        /// <value>
        ///     The take.
        /// </value>
        public int Take { get; set; }

        //-----------------------------------------------------------
        /// <summary>
        ///     Adds a new filter to the list
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void AddFilter(Expression<Func<TEntity, bool>> filter)
        {
            Filters.Add(filter);
        }

        //-------------------------------------------------------------
        /// <summary>
        ///     Adds a Sort Criteria to the list.
        /// </summary>
        /// <param name="sortCriteria">The sort criteria.</param>
        public void AddSortCriteria(ISortCriteria<TEntity> sortCriteria)
        {
            SortCriterias.Add(sortCriteria);
        }
    }
}