#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;
using Monad;
using FluentAssertions;

namespace Monad.Specs
{
    public class CombinedSuite
    {
        private static Try<T> ErrIO<T>(IO<T> fn)
        {
            return () => fn();
        }

        private static Try<IO<T>> Trans<T>(IO<T> inner)
        {
            return () => inner;
        }

        private static Reader<E, Try<T>> Trans<E, T>(Try<T> inner)
        {
            return (env) => inner;
        }

        public static IO<string> Hello()
        {
            return () => "Hello,";
        }

        public static IO<string> World()
        {
            return () => " World";
        }

        public static Try<Option<IO<string>>> OpenFile(string fn)
        {
            return () => Option.Return(() => IO.Return(() => "Data" + fn));
        }

        [Test]
        public void Combined1()
        {
            var t1 = ErrIO(() => 1);
            var t2 = ErrIO(() => 2);
            var t3 = ErrIO(() => 3);

            var res = from one in t1
                from two in t2
                from thr in t3
                select one + two + thr;

            res.Value().Should().Be(6);
        }

        [Test]
        public void Combined2()
        {
            var t1 = ErrIO(() => 1);
            var t2 = ErrIO(() => 2);
            var t3 = ErrIO(() => 3);
            var fail = ErrIO<int>(() => { throw new Exception("Error"); });

            var res = from one in t1
                from two in t2
                from thr in t3
                from err in fail
                select one + two + thr + err;

            res().IsFaulted.Should().BeTrue();
        }

        // Messing
        [Test]
        public void TransTest()
        {
            var errT = Trans(from h in Hello()
                from w in World()
                select h + w);

            var rdrT = Trans<string, IO<string>>(errT);

            rdrT("environ")().Value().Should().Be("Hello, World");
        }

        [Test]
        public void TransTest2()
        {
            var mon = from ed1 in OpenFile("1")
                from ed2 in OpenFile("2")
                select Lift.M(ed1, ed2, (ioa, iob) =>
                    Lift.M(ioa, iob, (a, b) =>
                        a + b
                        ));

            var res = mon();
            res.Value().Value().Should().Be("Data1Data2");
        }
    }
}