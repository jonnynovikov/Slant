﻿using LanguageExt;
using LanguageExt.TypeClasses;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Compare the equality of any type in the NumType type-class
    /// </summary>
    public struct EqNumType<NUMTYPE, NUM, A> : Eq<NumType<NUMTYPE, NUM, A>>
        where NUM     : struct, Num<A>
        where NUMTYPE : NumType<NUMTYPE, NUM, A>
    {
        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(NumType<NUMTYPE, NUM, A> x, NumType<NUMTYPE, NUM, A> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return default(NUM).Equals((A)x, (A)y);
        }

        /// <summary>
        /// Get hash code of the value
        /// </summary>
        /// <param name="x">Value to get the hash code of</param>
        /// <returns>The hash code of x</returns>
        [Pure]
        public int GetHashCode(NumType<NUMTYPE, NUM, A> x) =>
            x.IsNull() ? 0 : x.GetHashCode();
    }

    /// <summary>
    /// Compare the equality of any type in the NumType type-class
    /// </summary>
    public struct EqNumType<NUMTYPE, NUM, A, PRED> : Eq<NumType<NUMTYPE, NUM, A, PRED>>
        where PRED    : struct, Pred<A>
        where NUM     : struct, Num<A>
        where NUMTYPE : NumType<NUMTYPE, NUM, A, PRED>
    {
        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(NumType<NUMTYPE, NUM, A, PRED> x, NumType<NUMTYPE, NUM, A, PRED> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return default(NUM).Equals((A)x, (A)y);
        }

        /// <summary>
        /// Get hash code of the value
        /// </summary>
        /// <param name="x">Value to get the hash code of</param>
        /// <returns>The hash code of x</returns>
        [Pure]
        public int GetHashCode(NumType<NUMTYPE, NUM, A, PRED> x) =>
            x.IsNull() ? 0 : x.GetHashCode();
    }
}
