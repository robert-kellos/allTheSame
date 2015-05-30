using AllTheSame.Common.Interfaces.Generic;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     ISearchRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISearchRepository<TEntity> : IEntity<int> where TEntity : class
    {
        /// <summary>
        ///     Searches the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery);

        /// <summary>
        ///     GetSortedFieldFilterResult
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="criteria"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        PagedListResult<TEntity> GetSortedFieldFilterResult(string searchField = null, string criteria = null,
            SortDirection sortDirection = SortDirection.Ascending);
    }
}