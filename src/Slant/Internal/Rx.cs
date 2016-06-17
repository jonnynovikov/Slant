using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Reactive
{
    namespace Linq
    {
    }

    namespace Threading
    {
        namespace Tasks { }
    }

    namespace Subjects { }
}

namespace LanguageExt
{
    internal static class Observable
    {
        internal static IObservable<T> RxUnavailable<T>()
        {
            throw new NotImplementedException("Rx-Extensions are missed in Slant version of LanguageExt");
        }

        internal static IObservable<T> Return<T>(T input) => RxUnavailable<T>();

        internal static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source,
            Func<TSource, TResult> selector) => RxUnavailable<TResult>();

        internal static IObservable<TOther> SelectMany<TSource, TOther>(this IObservable<TSource> source,
            IObservable<TOther> other) => RxUnavailable<TOther>();

        internal static IObservable<TResult> SelectMany<TSource, TResult>(this IObservable<TSource> source,
            Func<TSource, IObservable<TResult>> selector) => RxUnavailable<TResult>();

        internal static IObservable<TResult> SelectMany<TSource, TResult>(this IObservable<TSource> source,
            Func<TSource, int, IObservable<TResult>> selector) => RxUnavailable<TResult>();

        internal static IObservable<TSource> Take<TSource>(this IObservable<TSource> source, int count) => RxUnavailable<TSource>();

        internal static IObservable<TSource> ToObservable<TSource>(this IEnumerable<TSource> source) => RxUnavailable<TSource>();

        internal static IObservable<TResult> ToObservable<TResult>(this Task<TResult> source) => RxUnavailable<TResult>();
    }
}