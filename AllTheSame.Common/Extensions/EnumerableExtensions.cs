using System;
using System.Collections;
using System.Collections.Generic;
using AllTheSame.Common.Logging;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     IEnumerableExtensions
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        ///     Determines whether [is null or empty].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            try
            {
                if (items == null)
                {
                    return true;
                }

                var collectionOfT = items as ICollection<T>;
                if (collectionOfT != null)
                {
                    return collectionOfT.Count == 0;
                }

                var collection = items as ICollection;
                if (collection != null)
                {
                    return collection.Count == 0;
                }

                using (var e = items.GetEnumerator())
                {
                    return !e.MoveNext();
                }
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Checking if IEnumerable of type T is null or empty", e);
                return false;
            }
        }

        /// <summary>
        ///     Determines whether [is null or empty].
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this IEnumerable items)
        {
            try
            {
                if (items == null)
                {
                    return true;
                }

                var collection = items as ICollection;
                if (collection != null)
                {
                    return collection.Count == 0;
                }

                var enumerator = items.GetEnumerator();
                var isEmpty = !enumerator.MoveNext();

                var disposable = enumerator as IDisposable;
                disposable?.Dispose();

                return isEmpty;
            }
            catch (Exception e)
            {
                Audit.Log.Error("Exception :: Checking if IEnumerable of type T is null or empty", e);
                return false;
            }
        }
    }
}