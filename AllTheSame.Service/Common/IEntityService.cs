using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Repository.Common;

namespace AllTheSame.Service.Common
{
    /// <summary>
    ///     IEntityService
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public interface IEntityService<TEntity, out TRepository> : IGenericCrud<TEntity>, IService
        where TEntity : class
        where TRepository : class, IGenericRepository<TEntity>
    {
        /// <summary>
        /// </summary>
        TRepository Repository { get; }
    }
}