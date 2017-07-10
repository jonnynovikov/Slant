﻿using System;
using LanguageExt;
using LanguageExt.TypeClasses;
using static LanguageExt.TypeClass;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Compare the equality of any values bound by the Try monad
    /// </summary>
    public struct EqTry<EQ, A> : Eq<Try<A>>
        where EQ : struct, Eq<A>
    {
        public static readonly EqTry<EQ, A> Inst = default(EqTry<EQ, A>);

        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(Try<A> x, Try<A> y)
        {
            var a = x.Try();
            var b = y.Try();
            if (a.IsFaulted && b.IsFaulted) return true;
            if (a.IsFaulted || b.IsFaulted) return false;
            return equals<EQ, A>(a.Value, b.Value);
        }

        [Pure]
        public int GetHashCode(Try<A> x)
        {
            var res = x.Try();
            return res.IsFaulted || res.Value.IsNull() ? 0 : res.Value.GetHashCode();
        }
    }
}
