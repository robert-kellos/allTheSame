using System.Collections.Generic;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     PagedListResult is a storage container for sorted, filtered results
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class PagedListResult<TEntity>
    {
        //-----------------------------------------------------------
        /// <summary>
        ///     Does the returned result contains more rows to be retrieved?
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has next; otherwise, <c>false</c>.
        /// </value>
        public bool HasNext { get; set; }

        //-----------------------------------------------------------
        /// <summary>
        ///     Does the returned result contains previous items ?
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has previous; otherwise, <c>false</c>.
        /// </value>
        public bool HasPrevious { get; set; }

        //-----------------------------------------------------------
        /// <summary>
        ///     Total number of rows that could be possibly be retrieved.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public long Count { get; set; }

        //-----------------------------------------------------------
        /// <summary>
        ///     Result of the query.
        /// </summary>
        /// <value>
        ///     The entities.
        /// </value>
        public IEnumerable<TEntity> Entities { get; set; }
    }
}