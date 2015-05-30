using System;
using System.Collections.Generic;
using System.Data.Entity;
using AllTheSame.Common.Core;
using AllTheSame.Common.Extensions;
using AllTheSame.Common.Logging;

namespace AllTheSame.Common.Helpers
{
    /// <summary>
    ///     AppUtility
    /// </summary>
    public static class AppUtility
    {
        /// <summary>
        ///     Validates the context.
        /// </summary>
        /// <param name="context"></param>
        public static void ValidateContext(DbContext context)
        {
            if (context != null) return;

            var msg = string.Format(AppConstants.ErrorMessages.ArgumentNullExceptionMessage,
                "_context");
            var anex = new NullReferenceException(msg);

            Audit.Log.Error(msg, anex);

            throw anex;
        }

        /// <summary>
        ///     Validates the database set.
        /// </summary>
        /// <param name="dbSet">The database set.</param>
        public static void ValidateDbSet<TEntity>(IDbSet<TEntity> dbSet) where TEntity : class
        {
            if (!dbSet.IsNull()) return;

            var msg = string.Format(AppConstants.ErrorMessages.NullReferenceExceptionMessage,
                "_dbset");
            var nex = new NullReferenceException(msg);

            Audit.Log.Error(msg, nex);

            throw nex;
        }

        /// <summary>
        ///     Validates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void ValidateEntity<TEntity>(TEntity entity)
        {
            if (!entity.IsNull()) return;

            var msg = string.Format(AppConstants.ErrorMessages.ArgumentNullExceptionMessage,
                "entity");
            var anex = new NullReferenceException(msg);

            Audit.Log.Error(msg, anex);

            throw anex;
        }

        /// <summary>
        ///     Determines whether [is higher version] [the specified sample version].
        /// </summary>
        /// <param name="sampleVersion">The sample version.</param>
        /// <param name="baseVersion">The base version.</param>
        /// <returns></returns>
        public static bool IsHigherVersion(IReadOnlyList<byte> sampleVersion, IReadOnlyList<byte> baseVersion)
        {
            for (var i = 0; i < baseVersion.Count; i++)
            {
                if (sampleVersion[i] > baseVersion[i])
                {
                    return true;
                }
                if (sampleVersion[i] < baseVersion[i])
                {
                    return false;
                }
            }
            return false;
        }
    }
}