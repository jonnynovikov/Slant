﻿using System;
using LanguageExt.TypeClasses;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Integer number 
    /// </summary>
    public struct TNumericChar : Eq<char>, Ord<char>, Monoid<char>, Arithmetic<char>
    {
        public static readonly TNumericChar Inst = default(TNumericChar);

        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(char x, char y) =>
            x == y;

        /// <summary>
        /// Compare two values
        /// </summary>
        /// <param name="x">Left hand side of the compare operation</param>
        /// <param name="y">Right hand side of the compare operation</param>
        /// <returns>
        /// if x greater than y : 1
        /// 
        /// if x less than y    : -1
        /// 
        /// if x equals y       : 0
        /// </returns>
        [Pure]
        public int Compare(char x, char y) =>
            CharToInt(x).CompareTo(CharToInt(y));

        /// <summary>
        /// Monoid empty value (0)
        /// </summary>
        /// <returns>0</returns>
        [Pure]
        public char Empty() => (char)0;

        /// <summary>
        /// Semigroup append (sum)
        /// </summary>
        /// <param name="x">left hand side of the append operation</param>
        /// <param name="y">right hand side of the append operation</param>
        /// <returns>x + y</returns>
        [Pure]
        public char Append(char x, char y) => 
            (char)(CharToInt(x) + CharToInt(y));

        /// <summary>
        /// Get the hash-code of the provided value
        /// </summary>
        /// <returns>Hash code of x</returns>
        [Pure]
        public int GetHashCode(char x) =>
            x.GetHashCode();

        [Pure]
        public char Plus(char x, char y) =>
            (char)(CharToInt(x) + CharToInt(y));

        [Pure]
        public char Subtract(char x, char y) =>
            (char)(CharToInt(x) - CharToInt(y));

        [Pure]
        public char Product(char x, char y) =>
            (char)(CharToInt(x) * CharToInt(y));

        [Pure]
        public char Negate(char x) =>
            (char)(-CharToInt(x));

        static int CharToInt(int x) =>
            x > 32768
                ? -(65536 - x)
                : x;
    }
}
