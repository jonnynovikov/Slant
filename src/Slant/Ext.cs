﻿namespace Slant
{
    using System;
    using LanguageExt;
    using static LanguageExt.Prelude;

    /// <summary>
    /// LanguageExt extensions
    /// </summary>
    public static class Ext
    {
        /// <summary>
        /// Returns self if it is nonempty, otherwise return the alternative.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> OrElse<T, R>(this Option<T> self, Option<R> other) where R : T
        {
            return self.IsNone ? other.Map(x => (T)x) : self;
        }

        /// <summary>
        /// Returns self if it is nonempty, otherwise return the result of evaluating supplier.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="other"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Option<T> OrElse<T, R>(this Option<T> self, Func<Option<R>> other) where R : T
        {
            return self.IsNone ? other().Map(x => (T)x) : self;
        }

        /// <summary>
        /// Returns the value if this is a Just, otherwise the other value is returned, if this is a Nothing
        /// </summary>
        /// <param name="self"></param>
        /// <param name="supplier"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T GetOrElse<T>(this Option<T> self, Func<T> supplier) => self.MatchUnsafe(identity, supplier);

        /// <summary>
        /// Returns the value if this is a Just case, otherwise throws an exception.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="exceptionSupplier"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T GetOrElseThrow<T, TException>(this Option<T> self, Func<TException> exceptionSupplier)
            where TException : Exception
        {
            return self.MatchUnsafe(identity, () => { throw exceptionSupplier(); });
        }
    }
}