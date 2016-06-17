#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Monad;
using NUnit.Framework;

namespace Monad.Specs
{
    public static class TryAsyncExt
    {
        public static async Task<TryResult<T>> TryAsync<T>(this Task<T> t)
        {
            try
            {
                return await t;
            }
            catch (Exception e)
            {
                return new TryResult<T>(e);
            }
        }
    }

    public class TrySuite
    {
        [Test]
        public void TestTry()
        {
            var r = from lhs in Error()
                from rhs in Two().Select(_ => 999.0)
                select lhs + rhs;

            (r().IsFaulted && r().Exception.Message == "Error!!").Should().BeTrue();
        }

        [Test]
        public void TestEitherBinding2()
        {
            var r = (from lhs in Two()
                from rhs in Error()
                select lhs + rhs)
                .TryMemo();

            (r().IsFaulted && r().Exception.Message == "Error!!").Should().BeTrue();
        }


        [Test]
        public void TestEitherBinding3()
        {
            var r =
                from lhs in Two()
                select lhs;

            (r().Value == 2).Should().BeTrue();
        }

        [Test]
        public void TestEitherBinding4()
        {
            var r =
                from lhs in Error()
                select lhs;

            (r().IsFaulted && r().Exception.Message == "Error!!").Should().BeTrue();
        }

        [Test]
        public void TestEitherBinding5()
        {
            var r =
                from one in Two()
                from two in Error()
                from thr in Two()
                select one + two + thr;

            (r().IsFaulted && r().Exception.Message == "Error!!").Should().BeTrue();
        }

        [Test]
        public void TestEitherMatch1()
        {
            var unit =
                (from one in Two()
                    from two in Error()
                    from thr in Two()
                    select one + two + thr)
                    .Match(
                        Success: r => false.Should().BeTrue(),
                        Fail: l => l.Message.Should().Be("Error!!")
                    );

            Console.WriteLine(unit.ToString());
        }

        [Test]
        public void TestEitherMatch2()
        {
            var unit =
                (from one in Two()
                    from two in Error()
                    from thr in Two()
                    select one + two + thr)
                    .Match(
                        succ => true.Should().BeFalse(),
                        fail => fail.Message.Should().Be("Error!!")
                    );
            Console.WriteLine(unit.ToString());
        }

        [Test]
        public void TestEitherMatch3()
        {
            var unit =
                (from one in Two()
                    from two in Two()
                    select one + two)
                    .Match(
                        Success: r => r.Should().Be(4),
                        Fail: l => true.Should().BeFalse()
                    );
            Console.WriteLine(unit.ToString());
        }

        [Test]
        public void TestEitherMatch4()
        {
            var unit =
                (from one in Two()
                    from two in Two()
                    select one + two)
                    .Match(
                        succ => succ.Should().Be(4),
                        fail => true.Should().BeFalse()
                    );
            Console.WriteLine(unit.ToString());
        }

        [Test]
        public void TestEitherMatch5()
        {
            var result =
                (from one in Two()
                    from two in Two()
                    select one + two)
                    .Match(
                        Success: r => r*2,
                        Fail: l => 0
                    );

            result().Should().Be(8);
        }

        [Test]
        public void TestEitherMatch6()
        {
            var result =
                (from one in Two()
                    from err in Error()
                    from two in Two()
                    select one + two)
                    .Match(
                        Success: r => r*2,
                        Fail: l => 0
                    );

            result().Should().Be(0);
        }

        public Try<int> Two() => () => 2;

        public Try<int> Error() => () => { throw new Exception("Error!!"); };
    }
}