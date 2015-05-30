using System.Linq;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     ISortCriteria
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISortCriteria<T>
    {
        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        SortDirection Direction { get; set; }

        /// <summary>
        ///     Applies the ordering.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="useThenBy">if set to <c>true</c> [use then by].</param>
        /// <returns></returns>
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, bool useThenBy);
    }
}