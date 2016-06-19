using System;
using FluentAssertions;
using LanguageExt;
using static LanguageExt.Prelude;
using NUnit.Framework;

namespace Slant.Tests
{
    [TestFixture]
    public class OptionTests
    {
        private static Option<int> SomeInteger() => Optional(42);

        public static Action FailHere => () => false.Should().Be(true, "Shouldn't get here");

        [Test]
        public void SomeGeneratorTestsObject()
        {
            var optional = Some(123);

            optional.Match(Some: i => i.Should().Be(123),
                           None: FailHere);

            int c = optional.Match(Some: i => i + 1,
                                   None: () => 0);

            Assert.True(c == 124);
        }

        [Test]
        public void SomeGeneratorTestsFunction()
        {
            var optional = Some(123);

            match(optional, Some: i => Assert.True(i == 123),
                            None: FailHere);

            int c = match(optional, Some: i => i + 1,
                                    None: () => 0);

            Assert.True(c == 124);
        }

        [Test]
        public void NoneGeneratorTestsObject()
        {
            Option<int> optional = None;

            optional.Match(Some: i => FailHere(),
                           None: () => Assert.True(true));

            int c = optional.Match(Some: i => i + 1,
                                   None: () => 0);

            Assert.True(c == 0);
        }

        [Test]
        public void NoneGeneratorTestsFunction()
        {
            Option<int> optional = None;

            match(optional, Some: i => FailHere(),
                            None: () => Assert.True(true));

            int c = match(optional, Some: i => i + 1,
                                    None: ret(0));

            Assert.True(c == 0);
        }

        [Test]
        public void SomeLinqTest()
        {
            var two = Some(2);
            var four = Some(4);
            var six = Some(6);

            match(from x in two
                  from y in four
                  from z in six
                  select x + y + z,
                   Some: v => Assert.True(v == 12),
                   None: failwith("Shouldn't get here"));
        }

        [Test]
        public void NoneLinqTest()
        {
            var two = Some(2);
            var four = Some(4);
            var six = Some(6);
            Option<int> none = None;

            match(from x in two
                  from y in four
                  from _ in none
                  from z in six
                  select x + y + z,
                   Some: v => failwith<int>("Shouldn't get here"),
                   None: () => Assert.True(true));
        }

        [Test]
        public void NullIsNotSomeTest()
        {
            Assert.Throws(
                typeof(ValueIsNullException),
                () =>
                {
                    GetStringNone();
                }
            );
        }

        [Test]
        public void NullIsNoneTest()
        {
            GetStringNone2().IsNone.Should().BeTrue();
        }

        [Test]
        public void OptionFluentSomeNoneTest()
        {
            int res1 = GetValue(true)
                        .Some(x => x + 10)
                        .None(0);

            int res2 = GetValue(false)
                        .Some(x => x + 10)
                        .None(ret(0));

            Assert.True(res1 == 1010);
            Assert.True(res2 == 0);
        }

        [Test]
        public void NullInSomeOrNoneTest()
        {
            Assert.Throws(
                typeof(ResultIsNullException),
                () =>
                {
                    GetValue(true)
                       .Some(x => (string)null)
                       .None((string)null);
                }
            );

            Assert.Throws(
                typeof(ResultIsNullException),
                () =>
                {
                    GetValue(false)
                       .Some(x => (string)null)
                       .None((string)null);
                }
            );
        }

        [Test]
        public void NullableTest()
        {
            var res = GetNullable(true)
                        .Some(identity)
                        .None(ret(0));

            res.Should().Be(1000);
        }

        [Test]
        public void NullableOrElseTest()
        {
            var res = GetNullable(true).GetOrElse(ret(0));
            res.Should().Be(1000);
            Some(res).Should().Be(res);
        }

        [Test]
        public void NullableDenySomeNullTest()
        {
            Assert.Throws(
                    typeof(ValueIsNullException),
                    () =>
                    {
                        var res = GetNullable(false)
                                    .Some(identity)
                                    .None(ret(0));
                    }
                );
        }

        [Test]
        public void NullableDenySomeNullOrElseTest()
        {
            fun(() =>
           {
               var res = GetNullable(false)
                           .Some(identity)
                           .None(ret(0));
           }).Should();

        }

        private Option<string> GetStringNone()
        {
            // This should fail
            string nullStr = null;
            return Some(nullStr);
        }

        private Option<string> GetStringNone2()
        {
            // This should be coerced to None
            string nullStr = null;
            return nullStr;
        }

        private Option<int> GetNullable(bool select) =>
            select
                ? Some((int?)1000)
                : Some((int?)null);

        private Option<int> GetValue(bool select) =>
            select
                ? Some(1000)
                : None;

        private Option<Option<int>> GetSomeOptionValue(bool select) =>
            select
                ? Some(Some(1000))
                : Some(Option<int>.None);

        private Option<int> ImplicitConversion() => 1000;

        private Option<int> ImplicitNoneConversion() => None;

        private void InferenceTest1()
        {
            Action<int> actionint = v => v = v * 2;
            Option<int> optional1 = Some(123);
            optional1.Some(actionint) // Compiler tries to call:  public static Option<T> Some(T value)
                     .None(noop);
        }

        [Test]
        public void ShouldMapNonNullToSome()
        {
            var option = Optional(new object());
            option.IsSome.Should().BeTrue();
        }

        [Test]
        public void ShouldReturnSelfOnOrElseIfValueIsPresent()
        {
            var opt = SomeInteger();
            opt.OrElse(Optional(0)).Should().Be(opt);
        }

        [Test]
        public void ShouldReturnSelfOnOrElseSupplierIfValueIsPresent()
        {
            var opt = SomeInteger();
            opt.OrElse(() => Optional(0)).Should().Be(opt);
            opt.OrElse(ret(Optional(0))).Should().Be(opt);
            opt.OrElse(retOptional(0)).Should().Be(opt);
        }

        [Test]
        public void ShouldReturnAlternativeOnOrElseIfValueIsNotDefined()
        {
            var opt = SomeInteger();
            Option<int>.None.OrElse(opt).Should().Be(opt);
        }

        [Test]
        public void ShouldMapSome()
        {
            Optional(1).Map(toString).Should().Be(Some("1"));
        }

        [Test]
        public void ShouldBeAwareOfPropertyThatNotHoldsForAllOfSome()
        {
            Some(1).AsEnumerable().ForAll(i => i == 2).Should().BeFalse();
        }
    }
}
