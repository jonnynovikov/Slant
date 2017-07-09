﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace LanguageExt
{
    public static partial class Prelude
    {
        /// <summary>
        /// Reverse the order of the first two arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<B, Func<A, R>> flip<A, B, R>(Func<A, Func<B, R>> f) =>
            b => a => f(a)(b);

        /// <summary>
        /// Reverse the order of the first and last arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<C, Func<B, Func<A, R>>> flip<A, B, C, R>(Func<A, Func<B, Func<C, R>>> f) =>
            c => b => a => f(a)(b)(c);


        /// <summary>
        /// Reverse the order of the first two arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<B, A, C> flip<A, B, C>(Func<A, B, C> f) =>
            (arg2, arg1) => f(arg1, arg2);

        /// <summary>
        /// Reverse the order of the first and last arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<C, B, A, R> flip<A, B, C, R>(Func<A, B, C, R> f) =>
            (c, b, a) => f(a, b, c);


        /// <summary>
        /// Reverse the order of the first two arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<B, Func<A, C>> Flip<A, B, C>(this Func<A, Func<B, C>> f) =>
            arg2 => arg1 => f(arg1)(arg2);

        /// <summary>
        /// Reverse the order of the first and last arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<C, Func<B, Func<A, R>>> Flip<A, B, C, R>(this Func<A, Func<B, Func<C, R>>> f) =>
            c => b => a => f(a)(b)(c);


        /// <summary>
        /// Reverse the order of the first two arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<B, A, C> Flip<A, B, C>(this Func<A, B, C> f) =>
            (arg2, arg1) => f(arg1, arg2);

        /// <summary>
        /// Reverse the order of the first and last arguments of a curried function
        /// </summary>
        [Pure]
        public static Func<C, B, A, R> Flip<A, B, C, R>(this Func<A, B, C, R> f) =>
            (c, b, a) => f(a, b, c);
    }
}
