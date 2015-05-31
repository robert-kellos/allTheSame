using System.Linq;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     FieldSortCriteria
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldSortCriteria<T> : ISortCriteria<T>
    {
        /// <summary>
        /// Gets the search field.
        /// </summary>
        /// <value>
        /// The search field.
        /// </value>
        public string SearchField { get; }

        /// <summary>
        ///     Direction order of sorting
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        ///     FieldSortCriteria
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="sortDirection"></param>
        public FieldSortCriteria(string searchField, SortDirection sortDirection)
        {
            // TODO: Complete member initialization
            SearchField = searchField;
            Direction = sortDirection;
        }
        
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