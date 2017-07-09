using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;
using static LanguageExt.TypeClass;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using LanguageExt.TypeClasses;
using LanguageExt.ClassInstances;

namespace LanguageExt.Reactive
{
    public static partial class Prelude
    {
        /// <summary>
        /// Match the two states of the Either and return a stream of non-null R2s.
        /// </summary>
        [Pure]
        public static IObservable<R2> MatchObservable<L, R, R2>(this Either<L, IObservable<R>> self, Func<R, R2> Right, Func<L, R2> Left) =>
            self.IsRight
                ? self.RightValue.Select(Right).Select(Check.NullReturn)
                : Observable.Return(Check.NullReturn(Left(self.LeftValue)));

        /// <summary>
        /// Match the two states of the IObservable Either and return a stream of non-null R2s.
        /// </summary>
        [Pure]
        public static IObservable<R2> MatchObservable<L, R, R2>(this IObservable<Either<L, R>> self, Func<R, R2> Right, Func<L, R2> Left) =>
            self.Select(either => match(either, Right, Left));
    }
