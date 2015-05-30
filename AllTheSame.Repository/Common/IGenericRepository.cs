using AllTheSame.Common.Interfaces.Generic;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     IGenericRepository TEntity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> : IGenericCrud<TEntity>, IEntity<int> // where TEntity : IEntity<int>
    {
    }
}