using LanguageExt.TypeClasses;
using static LanguageExt.Prelude;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Equality enumerable
    /// </summary>
    public struct EqEnumerable<EQ, A> : Eq<IEnumerable<A>>
        where EQ : struct, Eq<A>
    {
        public static readonly EqEnumerable<EQ, A> Inst = default(EqEnumerable<EQ, A>);

        /// <summary>
        /// Equality check
        /// </summary>
        [Pure]
        public bool Equals(IEnumerable<A> x, IEnumerable<A> y)
        {
            if (x == null) return y == null;
            if (y == null) return false;

            using (var enumx = x.GetEnumerator())
            using (var enumy = y.GetEnumerator())
                while (true)
                {
                    bool a = enumx.MoveNext();
                    bool b = enumy.MoveNext();
                    if (a != b) return false;
                    if (!a && !b) return true;
                    if (!default(EQ).Equals(enumx.Current, enumy.Current)) return false;
                }
        }

        /// <summary>
        /// Get hash code of the value
        /// </summary>
        /// <param name="x">Value to get the hash code of</param>
        /// <returns>The hash code of x</returns>
        [Pure]
        public int GetHashCode(IEnumerable<A> x) =>
            hash(x);
    }
}
