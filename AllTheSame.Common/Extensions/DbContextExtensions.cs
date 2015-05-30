using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     DbContextExtensions
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        ///     To the object context.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <returns></returns>
        public static ObjectContext ToObjectContext(this DbContext dbContext)
        {
            return (dbContext as IObjectContextAdapter).ObjectContext;
        }
    }
}