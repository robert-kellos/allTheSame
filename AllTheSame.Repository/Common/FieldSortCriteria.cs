using System.Linq;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     FieldSortCriteria
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldSortCriteria<T> : ISortCriteria<T>
    {
        private string _searchField;
        private SortDirection _sortDirection;

        /// <summary>
        ///     FieldSortCriteria
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="sortDirection"></param>
        public FieldSortCriteria(string searchField, SortDirection sortDirection)
        {
            // TODO: Complete member initialization
            _searchField = searchField;
            _sortDirection = sortDirection;
        }

        /// <summary>
        ///     Direction order of sorting
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        ///     ApplyOrdering
        /// </summary>
        /// <param name="query"></param>
        /// <param name="useThenBy"></param>
        /// <returns></returns>
        public IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, bool useThenBy)
        {
            return ((IOrderedQueryable<T>) query).OrderBy(x => (useThenBy));
        }
    }
}