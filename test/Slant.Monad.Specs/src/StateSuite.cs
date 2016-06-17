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
using Monad.Utility;

namespace Monad.Specs
{
    public class StateSuite
    {
        [Test]
        public void StateTest1()
        {
            var state = State.Return<string, int>();

            var sm = from w in state
                from x in DoSomething()
                from y in DoSomethingElse()
                select x + y;

            var res = sm("Hello");

            res.State.Should().Be("Hello, World");
            res.Value.Should().Be(3);
        }

        [Test]
        public void StateTest2()
        {
            var sm = from x in State.Get<string>()
                from y in State.Put("Hello" + x)
                select y;

            var res = sm(", World");
            res.State.Should().Be("Hello, World");
        }

        [Test]
        public void StateTest3()
        {
            var initial = State.Return<string, int>(10);

            var sm = from x in initial
                from t in State.Get<string>()
                from y in State.Put("Hello " + (x*10) + t)
                select y;

            var res = sm(", World");
            res.State.Should().Be("Hello 100, World");
        }

        [Test]
        public void StateTest4()
        {
            var first = State.Return<string, int>(10);
            var second = State.Return<string, int>(3);

            var sm = from x in first
                from t in State.Get<string>()
                from y in second
                from s in State.Put("Hello " + (x*y) + t)
                select s;

            var res = sm(", World");
            res.State.Should().Be("Hello 30, World");
        }

        [Test]
        public void StateTest5()
        {
            var first = State.Return<string, int>(10);
            var second = State.Return<string, int>(3);
            var third = State.Return<string, int>(5);
            var fourth = State.Return<string, int>(100);

            var sm = from x in first
                from t in State.Get<string>()
                from y in second
                from s in State.Put("Hello " + (x*y) + t)
                from z in third
                from w in fourth
                from s1 in State.Get<string>()
                from s2 in State.Put(s1 + " " + (z*w))
                select x*y*z*w;

            var res = sm(", World");
            res.State.Should().Be("Hello 30, World 500");
            res.Value.Should().Be(15000);
        }

        [Test]
        public void StateTest6()
        {
            var first = State.Return<string, int>(10);
            var second = State.Return<string, int>(3);
            var third = State.Return<string, int>(5);
            var fourth = State.Return<string, int>(100);

            var sm = from x in first
                from t in State.Get<string>(s => s + "yyy")
                from y in second
                from s in State.Put("Hello " + (x*y) + t)
                from z in third
                from w in fourth
                from s1 in State.Get<string>()
                from s2 in State.Put(s1 + " " + (z*w))
                select x*y*z*w;

            var res = sm(", World"); // Invoke with the initial state

            res.State.Should().Be("Hello 30, Worldyyy 500");
            res.Value.Should().Be(15000);
        }

        static State<Unit, S> Put<S>(S state)
        {
            return _ => StateResult.Create(Unit.Default, state);
        }

        static State<string, int> DoSomethingElse()
        {
            return state => StateResult.Create(state + "rld", 1);
        }

        static State<string, int> DoSomething()
        {
            return state => StateResult.Create(state + ", Wo", 2);
        }
    }
}