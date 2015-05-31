using System;
using System.Data.Entity;
using System.Linq;
using AllTheSame.Common.Core;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     SearchRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SearchRepository<TEntity> : Entity<int>, ISearchRepository<TEntity>, IDisposable where TEntity : class
    {
        /// <summary>
        ///     The context
        /// </summary>
        protected AllTheSameDbContext Context;

        /// <summary>
        ///     The database set
        /// </summary>
        protected IDbSet<TEntity> DbSet;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SearchRepository{TEntity}" /> class.
        /// </summary>
        public SearchRepository()
        {
            Context = new AllTheSameDbContext();
            DbSet = Context.Set<TEntity>();
        }

        /// <summary>
        ///     Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(Context);
        }

        //-----------------------------------------------------------
        /// <summary>
        ///     Implementation method of the IRepository interface.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        public virtual PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery)
        {
            IQueryable<TEntity> sequence = DbSet;

            sequence = ManageFilters(searchQuery, sequence);

            sequence = ManageIncludeProperties(searchQuery, sequence);

            sequence = ManageSortCriterias(searchQuery, sequence);

            return GetTheResult(searchQuery, sequence);
        }

        /// <summary>
        ///     GetSortedFieldFilterResult
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="criteria"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public virtual PagedListResult<TEntity> GetSortedFieldFilterResult(string searchField = null,
            string criteria = null, SortDirection sortDirection = SortDirection.Ascending)
        {
            var repository = new SearchRepository<TEntity>();

            searchField = !string.IsNullOrEmpty(searchField) ? searchField : "Id";
            //TODO: once UpdatedOn is implemented - this will be default
            criteria = !string.IsNullOrEmpty(criteria) ? criteria : "";

            var q = new SearchQuery<TEntity>();

            if (!string.IsNullOrEmpty(searchField))
                q.AddSortCriteria(
                    new FieldSortCriteria<TEntity>(searchField, sortDirection));

            if (!string.IsNullOrEmpty(criteria))
                q.AddSortCriteria(
                    new FieldSortCriteria<TEntity>(criteria, sortDirection));

            var result = repository.Search(q);

            return result;
        }

        //-----------------------------------------------------------
        /// <summary>
        ///     Executes the query against the repository (database).
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        public virtual PagedListResult<TEntity> GetTheResult(SearchQuery<TEntity> searchQuery,
            IQueryable<TEntity> sequence)
        {
            //Counting the total number of object.
            var resultCount = sequence.Count();

            var result = (searchQuery.Take > 0)
                ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
                : (sequence.ToList());

            //Debug info of what the query looks like
            Audit.Log.Debug(sequence.ToString());

            // Setting up the return object.
            var hasNext = (searchQuery.Skip > 0 || searchQuery.Take > 0) &&
                          (searchQuery.Skip + searchQuery.Take < resultCount);
            return new PagedListResult<TEntity>
            {
                Entities = result,
                HasNext = hasNext,
                HasPrevious = (searchQuery.Skip > 0),
                Count = resultCount
            };
        }

        //-----------------------------------------------------------
        /// <summary>
        ///     Resolves and applies the sorting criteria of the SearchQuery
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ManageSortCriterias(SearchQuery<TEntity> searchQuery,
            IQueryable<TEntity> sequence)
        {
            if (searchQuery.SortCriterias != null && searchQuery.SortCriterias.Count > 0)
            {
                var sortCriteria = searchQuery.SortCriterias[0];
                var orderedSequence = sortCriteria.ApplyOrdering(sequence, false);

                if (searchQuery.SortCriterias.Count > 1)
                {
                    for (var i = 1; i < searchQuery.SortCriterias.Count; i++)
                    {
                        var sc = searchQuery.SortCriterias[i];
                        orderedSequence = sc.ApplyOrdering(orderedSequence, true);
                    }
                }
                sequence = orderedSequence;
            }
            else
            {
                sequence = ((IOrderedQueryable<TEntity>) sequence).OrderBy(x => (true));
            }
            return sequence;
        }

        //-----------------------------------------------------------
        /// <summary>
        ///     Chains the where clause to the IQueriable instance.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ManageFilters(SearchQuery<TEntity> searchQuery,
            IQueryable<TEntity> sequence)
        {
            if (searchQuery.Filters == null || searchQuery.Filters.Count <= 0) return sequence;
            sequence = searchQuery.Filters.Aggregate(sequence, (current, filterClause) => current.Where(filterClause));
            return sequence;
        }

        //-----------------------------------------------------------
        /// <summary>
        ///     Implementation of eager-loading. Includes the properties sent as part of the SearchQuery.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ManageIncludeProperties(SearchQuery<TEntity> searchQuery,
            IQueryable<TEntity> sequence)
        {
            if (string.IsNullOrWhiteSpace(searchQuery.IncludeProperties)) return sequence;
            var properties = searchQuery.IncludeProperties.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

            sequence = properties.Aggregate(sequence, (current, includeProperty) => current.Include(includeProperty));
            return sequence;
        }

        /// <summary>
        ///     PersonFieldSortCriteriaTest
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="criteria"></param>
        public static void PersonFieldSortCriteriaTest(string searchField = "Bob", string criteria = "S")
        {
            var repository = new SearchRepository<Person>();

            searchField = !string.IsNullOrEmpty(searchField) ? searchField : "Bob";
            criteria = !string.IsNullOrEmpty(criteria) ? criteria : "S";

            var q = new SearchQuery<Person>();

            if (!string.IsNullOrEmpty(searchField))
                q.AddSortCriteria(
                    new FieldSortCriteria<Person>(searchField, SortDirection.Ascending));

            if (!string.IsNullOrEmpty(criteria))
                q.AddSortCriteria(
                    new FieldSortCriteria<Person>(criteria, SortDirection.Ascending));

            var result = repository.Search(q);

            foreach (var entity in result.Entities)
            {
                Audit.Log.Debug(searchField + "=" + entity.FirstName + ";" +
                                criteria + "=" + entity.LastName);
            }
        }

        /// <summary>
        ///     Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            Context?.Dispose();
        }
    }
}