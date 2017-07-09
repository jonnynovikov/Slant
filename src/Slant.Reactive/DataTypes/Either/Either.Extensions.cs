using System;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Reactive.Linq;
using System.Diagnostics.Contracts;

/// <summary>
/// Extension methods for Either
/// </summary>
public static class EitherExtensionsReactive
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