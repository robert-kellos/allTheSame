using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AllTheSame.Common.Extensions
{
    /// <summary>
    ///     GlobalExtensions
    /// </summary>
    public static class GlobalExtensions
    {
        /// <summary>
        ///     Determines whether [is null or database null].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool IsNullOrDbNull<T>(this T obj) where T : class
        {
            var type = obj.GetType();
            return type == typeof (DBNull);
        }

        /// <summary>
        ///     Determines whether this instance is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool IsNull<T>(this T obj)
        {
            if (!IsNullableType(typeof (T)))
            {
                return false;
            }
            return (obj == null);
        }

        /// <summary>
        ///     Nots the null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        internal static bool NotNull<T>(T obj)
        {
            if (!IsNullableType(typeof (T)))
            {
                return true;
            }
            return (obj != null);
        }

        /// <summary>
        ///     Determines whether [is nullable type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsNullableType(Type type)
        {
            if (!type.IsValueType) // ref-type
            {
                return true;
            }
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        ///     Guard
        /// </summary>
        internal static class Guard
        {
            /// <summary>
            ///     Greaters the than.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="minimum">The minimum.</param>
            /// <param name="value">The value.</param>
            /// <param name="reference">The reference.</param>
            internal static void GreaterThan<T>(T minimum, T value, Expression<Func<T>> reference)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimum) > 0)
                {
                    return;
                }

                Throw.GuardFailureNotGreaterThan(GetName(reference), value, minimum);
            }

            /// <summary>
            ///     Throws an exception if the value is less than or equal to the minimum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="minimum">The minimum.</param>
            /// <param name="value">The value.</param>
            /// <param name="name">The name.</param>
            [DebuggerStepThrough]
            internal static void GreaterThan<T>(T minimum, T value, string name) where T : IComparable<T>
            {
                if (value.CompareTo(minimum) > 0)
                {
                    return;
                }

                Throw.GuardFailureNotGreaterThan(name, value, minimum);
            }

            /// <summary>
            ///     Throws an exception if the value is less than the minimum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="minimum">The minimum.</param>
            /// <param name="value">The value.</param>
            /// <param name="reference">The reference.</param>
            [DebuggerStepThrough]
            internal static void GreaterThanOrEqualTo<T>(T minimum, T value, Expression<Func<T>> reference)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimum) >= 0)
                {
                    return;
                }

                Throw.GuardFailureNotGreaterThanOrEqualTo(GetName(reference), value, minimum);
            }

            /// <summary>
            ///     Throws an exception if the value is less than the minimum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="minimum">The minimum.</param>
            /// <param name="value">The value.</param>
            /// <param name="name">The name.</param>
            [DebuggerStepThrough]
            internal static void GreaterThanOrEqualTo<T>(T minimum, T value, string name) where T : IComparable<T>
            {
                if (value.CompareTo(minimum) >= 0)
                {
                    return;
                }

                Throw.GuardFailureNotGreaterThanOrEqualTo(name, value, minimum);
            }

            /// <summary>
            ///     Throws an exception if the value is greater than or equal to the maximum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="maximum">The maximum.</param>
            /// <param name="value">The value.</param>
            /// <param name="reference">The reference.</param>
            [DebuggerStepThrough]
            internal static void LessThan<T>(T maximum, T value, Expression<Func<T>> reference) where T : IComparable<T>
            {
                if (value.CompareTo(maximum) < 0)
                {
                    return;
                }

                Throw.GuardFailureNotLessThan(GetName(reference), value, maximum);
            }

            /// <summary>
            ///     Throws an exception if the value is greater than or equal to the maximum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="maximum">The maximum.</param>
            /// <param name="value">The value.</param>
            /// <param name="name">The name.</param>
            [DebuggerStepThrough]
            internal static void LessThan<T>(T maximum, T value, string name) where T : IComparable<T>
            {
                if (value.CompareTo(maximum) < 0)
                {
                    return;
                }

                Throw.GuardFailureNotLessThan(name, value, maximum);
            }

            /// <summary>
            ///     Throws an exception if the value is greater than the maximum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="maximum">The maximum.</param>
            /// <param name="value">The value.</param>
            /// <param name="reference">The reference.</param>
            [DebuggerStepThrough]
            internal static void LessThanOrEqualTo<T>(T maximum, T value, Expression<Func<T>> reference)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximum) <= 0)
                {
                    return;
                }

                Throw.GuardFailureNotLessThanOrEqualTo(GetName(reference), value, maximum);
            }

            /// <summary>
            ///     Throws an exception if the value is greater than the maximum.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="maximum">The maximum.</param>
            /// <param name="value">The value.</param>
            /// <param name="name">The name.</param>
            [DebuggerStepThrough]
            internal static void LessThanOrEqualTo<T>(T maximum, T value, string name) where T : IComparable<T>
            {
                if (value.CompareTo(maximum) <= 0)
                {
                    return;
                }

                Throw.GuardFailureNotLessThanOrEqualTo(name, value, maximum);
            }

            /// <summary>
            ///     Throws an exception if the value is null.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value">The value.</param>
            /// <param name="reference">The reference.</param>
            [DebuggerStepThrough]
            internal static void NotNull<T>(T value, Expression<Func<T>> reference)
            {
                if (!IsNullableType(typeof (T)))
                {
                    return;
                }
                if (value == null)
                {
                    Throw.ArgumentNullException(GetName(reference));
                }
            }

            /// <summary>
            ///     Throws an exception if the value is null.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value">The value.</param>
            /// <param name="name">The name.</param>
            [DebuggerStepThrough]
            internal static void NotNull<T>(T value, string name)
            {
                if (!IsNullableType(typeof (T)))
                {
                    return;
                }
                if (value == null)
                {
                    Throw.ArgumentNullException(name);
                }
            }

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <param name="reference">The reference.</param>
            /// <returns></returns>
            private static string GetName(Expression reference)
            {
                var lambda = reference as LambdaExpression;
                if (lambda != null)
                {
                    var member = lambda.Body as MemberExpression;
                    if (member != null) return member.Member.Name;
                }
                return null;
            }
        }

        /// <summary>
        ///     Throw
        /// </summary>
        internal static class Throw
        {
            /// <summary>
            ///     Arguments the empty exception.
            /// </summary>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="System.ArgumentException"></exception>
            [DebuggerStepThrough]
            internal static void ArgumentEmptyException(string paramName)
            {
                throw new ArgumentException(string.Format("{0} cannot be empty.", paramName), paramName);
            }

            /// <summary>
            ///     Arguments the null exception.
            /// </summary>
            /// <param name="paramName">Name of the parameter.</param>
            /// <exception cref="System.ArgumentNullException"></exception>
            [DebuggerStepThrough]
            internal static void ArgumentNullException(string paramName)
            {
                throw new ArgumentNullException(paramName, string.Format("{0} cannot be null.", paramName));
            }

            /// <summary>
            ///     Exceptions the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void Exception(string message)
            {
                throw new Exception(message);
            }

            /// <summary>
            ///     Guards the against failure.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardAgainstFailure(string message)
            {
                throw new Exception(string.Format("Guard Against: {0}", message));
            }

            /// <summary>
            ///     Guards the assert failure.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardAssertFailure(string message)
            {
                throw new Exception(string.Format("Guard Assert: {0}", message));
            }

            /// <summary>
            ///     Guards the failure not greater than.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="name">The name.</param>
            /// <param name="value">The value.</param>
            /// <param name="minimum">The minimum.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardFailureNotGreaterThan<T>(string name, T value, T minimum)
            {
                var message = string.Format("Guard Greater: {0} is '{1}' but must be greater than '{2}'.", name, value,
                    minimum);
                throw new Exception(message);
            }

            /// <summary>
            ///     Guards the failure not greater than or equal to.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="name">The name.</param>
            /// <param name="value">The value.</param>
            /// <param name="minimum">The minimum.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardFailureNotGreaterThanOrEqualTo<T>(string name, T value, T minimum)
            {
                var message =
                    string.Format("Guard Greater or Equal: {0} is '{1}' but must be greater than or equal to '{2}'.",
                        name,
                        value,
                        minimum);
                throw new Exception(message);
            }

            /// <summary>
            ///     Guards the failure not less than.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="name">The name.</param>
            /// <param name="value">The value.</param>
            /// <param name="maximum">The maximum.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardFailureNotLessThan<T>(string name, T value, T maximum)
            {
                var message = string.Format("Guard Less: {0} is '{1}' but must be less than '{2}'.", name, value,
                    maximum);
                throw new Exception(message);
            }

            /// <summary>
            ///     Guards the failure not less than or equal to.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="name">The name.</param>
            /// <param name="value">The value.</param>
            /// <param name="maximum">The maximum.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardFailureNotLessThanOrEqualTo<T>(string name, T value, T maximum)
            {
                var message = string.Format(
                    "Guard Less or Equal: {0} is '{1}' but must be less than or equal to '{2}'.",
                    name,
                    value,
                    maximum);
                throw new Exception(message);
            }

            /// <summary>
            ///     Guards the type assignment failure.
            /// </summary>
            /// <param name="typeToAssign">The type to assign.</param>
            /// <param name="targetType">Type of the target.</param>
            /// <param name="name">The name.</param>
            /// <exception cref="System.Exception"></exception>
            [DebuggerStepThrough]
            internal static void GuardTypeAssignmentFailure(Type typeToAssign, Type targetType, string name)
            {
                var message = string.Format("Guard Type Assignment: {0} is '{1}' but does not {2} '{3}'.",
                    name,
                    typeToAssign,
                    targetType.IsInterface ? "implement required interface" : "convert to required type",
                    targetType);
                throw new Exception(message);
            }
        }
    }
}