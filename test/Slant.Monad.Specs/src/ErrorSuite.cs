#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Monad;
using NUnit.Framework;

namespace Monad.Specs
{
    public class ErrorSuite
    {
        private static Try<int> DoSomething(int value)
        {
            return () => value + 1;
        }

        private static Try<int> DoSomethingElse(int value)
        {
            return () => value + 10;
        }

        private static Try<int> DoSomethingError(int value)
        {
            return () => { throw new Exception("Whoops"); };
        }

        private static int ThrowError(int val)
        {
            throw new Exception("Whoops");
        }

        private static Try<int> DoNotEverEnterThisFunction(int value)
        {
            return () => 10000;
        }

        [Test]
        public void TestWithoutRunTry()
        {
            var t = from v in DoSomethingError(10)
                select v;

            var e = t();

            t = from x in DoSomething(10)
                from u in DoSomethingElse(10)
                from v in DoSomethingElse(10)
                from w in DoSomethingError(10)
                select w;

            e = t();
        }


        [Test]
        public void TestErrorMonadLaws()
        {
            var value = 1000;

            // Return
            Try<int> errorM = () => value;

            errorM.Try().Value.Should().Be(1000);
            errorM.Try().IsFaulted.Should().BeFalse();

            errorM = DoSomethingError(0);

            errorM.Try().IsFaulted.Should().BeTrue();
            errorM.Try().Exception.Should().NotBeNull();

            // Bind
            var boundM = (from e in errorM
                from b in DoSomethingError(0)
                select b)
                .Try();

            // Value
            boundM.IsFaulted.Should().BeTrue();
            boundM.Exception.Should().NotBeNull();
        }

        [Test]
        public void TestErrorMonadSuccess()
        {
            var result = (from val1 in DoSomething(10)
                from val2 in DoSomethingElse(val1)
                select val2)
                .Try();

            (result.IsFaulted == false).Should().BeTrue("Should have succeeded");
            result.Value.Should().Be(21, "Value should be 21");
        }

        [Test]
        public void TestErrorMonadFail()
        {
            var result = (from val1 in DoSomething(10)
                from val2 in DoSomethingError(val1)
                from val3 in DoNotEverEnterThisFunction(val2)
                select val3)
                .Try();

            result.Value.Should().NotBe(10000, "Entered the function: DoNotEverEnterThisFunction()");
            result.IsFaulted.Should().BeTrue("Should throw an error");
        }

        [Test]
        public void TestErrorMonadSuccessFluent()
        {
            var result = DoSomething(10).Then(val2 => val2 + 10).Try();

            result.IsFaulted.Should().BeFalse("Should have succeeded");
            result.Value.Should().Be(21, "Value should be 21");
        }

        [Test]
        public void TestErrorMonadFailFluent()
        {
            var result = DoSomething(10)
                .Then(ThrowError)
                .Then(_ => 10000)
                .Try();

            result.Value.Should().NotBe(10000, "Entered the function: DoNotEverEnterThisFunction()");
            result.IsFaulted.Should().BeTrue("Should throw an error");
        }

        public Try<int> One()
        {
            return () => 1;
        }

        public Try<int> Two()
        {
            return () => 2;
        }

        public Try<int> Error()
        {
            return () => { throw new Exception("Error!!"); };
        }

        [Test]
        public void TestErrorMatch1()
        {
            (from one in One()
                from err in Error()
                from two in Two()
                select one + two + err)
                .Match(
                    Success: v => false.Should().BeTrue(),
                    Fail: e => (e.Message == "Error!!").Should().BeTrue()
                );
        }

        [Test]
        public void TestErrorMatch2()
        {
            var unit =
                (from one in One()
                    from err in Error()
                    from two in Two()
                    select one + two + err)
                    .Match(
                        val => false.Should().BeTrue(),
                        err => (err.Message == "Error!!").Should().BeTrue()
                    );

            Console.WriteLine(unit.ToString());
        }

        [Test]
        public void TestErrorMatch3()
        {
            (from one in One()
                from two in Two()
                select one + two)
                .Match(
                    Success: v => v.Should().Be(3),
                    Fail: e => false.Should().BeTrue()
                );
        }

        [Test]
        public void TestErrorMatch4()
        {
            var unit =
                (from one in One()
                    from two in Two()
                    select one + two)
                    .Match(
                        val => val.Should().Be(3),
                        err => false.Should().BeTrue()
                    );

            Console.WriteLine(unit.ToString());
        }
    }
}