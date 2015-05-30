using AllTheSame.Repository.Common;

namespace AllTheSame.WebAPI.Controllers.Custom
{
    /// <summary>
    ///     PagedList {TEntity}
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedList<TEntity> : SearchRepository<TEntity> where TEntity : class //, IEntity<int>
    {
        private static PagedList<TEntity> _instance;

        /// <summary>
        ///     Singleton - Instance of PagedList {TEntity}
        /// </summary>
        public static PagedList<TEntity> Instance
        {
            get { return _instance ?? (_instance = new PagedList<TEntity>()); }
        }
    }
}