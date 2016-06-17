#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using FluentAssertions;
using Monad;
using NUnit.Framework;

namespace Monad.Specs
{
    public class EitherSuite
    {
        [Test]
        public void TestEitherBinding1()
        {
            var r = from lhs in Two()
                from rhs in Two()
                select lhs + rhs;

            (r.IsRight() && r.Right() == 4).Should().BeTrue();
        }

        [Test]
        public void TestEitherBinding2()
        {
            var r = (from lhs in Two()
                from rhs in Error()
                select lhs + rhs)
                .Memo();

            (r().IsLeft && r().Left == "Error!!").Should().BeTrue();
        }


        [Test]
        public void TestEitherBinding3()
        {
            var r =
                from lhs in Two()
                select lhs;

            (r.IsRight() && r.Right() == 2).Should().BeTrue();
        }

        [Test]
        public void TestEitherBinding4()
        {
            var r =
                from lhs in Error()
                select lhs;

            (r.IsLeft() && r.Left() == "Error!!").Should().BeTrue();
        }

        [Test]
        public void TestEitherBinding5()
        {
            var r =
                from one in Two()
                from two in Error()
                from thr in Two()
                select one + two + thr;

            (r.IsLeft() && r.Left() == "Error!!").Should().BeTrue();
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
                        Right: r => false.Should().BeTrue(),
                        Left: l => (l == "Error!!").Should().BeTrue()
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
                        right => true.Should().BeFalse(),
                        left => (left == "Error!!").Should().BeTrue()
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
                        Right: r => (r == 4).Should().BeTrue(),
                        Left: l => true.Should().BeFalse()
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
                        right => (right == 4).Should().BeTrue(),
                        left => true.Should().BeFalse()
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
                        Right: r => r*2,
                        Left: l => 0
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
                        Right: r => r*2,
                        Left: l => 0
                    );
            result().Should().Be(0);
        }

        public Either<string, int> Two()
        {
            return () => 2;
        }

        public Either<string, int> Error()
        {
            return () => "Error!!";
        }
    }
}