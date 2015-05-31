using System;
using System.Data.Entity;
using AllTheSame.Common.Logging;

namespace AllTheSame.Repository.Common
{
    /// <summary>
    ///     The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     The DbContext
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        ///     Saves all pending changes
        /// </summary>
        /// <returns>
        ///     The number of objects in an Added, Modified, or Deleted state
        /// </returns>
        public int Commit()
        {
            var result = _dbContext.SaveChanges();

            // Save changes with the default options
            Audit.Log.Info(string.Format("Commit :: UnitOfWork saved changes successfully: {0}", (result == 1)));

            return result;
        }

        /// <summary>
        ///     Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(_dbContext);

            Audit.Log.Info(string.Format("UnitOfWork Dispose :: _dbContext destroyed"));
        }

        /// <summary>
        ///     Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            _dbContext?.Dispose();
            //- _dbContext = null; Setting to nuttl breaks GC.SuppressFinalize in DisposeMethod
        }
    }
}