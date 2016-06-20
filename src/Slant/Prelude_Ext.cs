using System;
using System.Diagnostics.Contracts;
// ReSharper disable InconsistentNaming

namespace LanguageExt
{
    public static partial class Prelude
    {
        /// <summary>
        /// Call ToString of an object
        /// </summary>
        /// <param name="x"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string toString<T>(T x) => x.ToString();

        /// <summary>
        /// Action which does nothing
        /// <remarks>noop = no operation</remarks>
        /// </summary>
        [Pure]
        public static Action noop => () => { };

        /// <summary>
        /// Converts value to a function returning captured value
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<T> ret<T>(T value) => () => value;

        /// <summary>
        /// Function which returns None
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Pure]
        public static Func<Option<T>> retNone<T>() => () => Option<T>.None;

        /// <summary>
        /// Convert value to a function which returns Option depends on value
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Pure]
        public static Func<Option<T>> retOptional<T>(T value) =>
            isnull(value)
                ? retNone<T>()
                : () => Option<T>.Some(value);
    }
}