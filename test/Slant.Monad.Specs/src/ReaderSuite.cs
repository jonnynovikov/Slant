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
    public class ReaderSuite
    {
        [Test]
        public void ReaderBindTest1()
        {
            var person = new Person {Name = "Joe", Surname = "Bloggs"};

            var reader = from n in Name()
                from s in Surname()
                select n + " " + s;

            reader(person).Should().Be("Joe Bloggs");
        }

        [Test]
        public void ReaderAskTest1()
        {
            var person = new Person {Name = "Joe", Surname = "Bloggs"};

            var reader = from p in Reader.Ask<Person>()
                select p.Name + " " + p.Surname;

            reader(person).Should().Be("Joe Bloggs");
        }

        [Test]
        public void ReaderAskTest2()
        {
            var person = new Person {Name = "Joe", Surname = "Bloggs"};

            var reader = from p in Reader.Ask<Person>()
                let nl = p.Name.Length
                let sl = p.Surname.Length
                select nl*sl;

            reader(person).Should().Be(18);
        }

        [Test]
        public void ReaderAskReturnAndBindTest()
        {
            var person = new Person {Name = "Joe", Surname = "Bloggs"};

            var env = Reader.Return<Person, int>(10);

            var reader = from x in env
                from p in Reader.Ask<Person>()
                let nl = p.Name.Length
                let sl = p.Surname.Length
                select nl*sl*x;

            reader(person).Should().Be(180);
        }

        [Test]
        public void ReaderAskReturnAndBindTest2()
        {
            var person = new Person {Name = "Joe", Surname = "Bloggs"};

            var env = Reader.Return<Person, int>(10);

            var reader = from x in env
                from p in env.Ask()
                let nl = p.Name.Length
                let sl = p.Surname.Length
                select nl*sl*x;

            reader(person).Should().Be(180);
        }


        class Person
        {
            public string Name;

            public string Surname;
        }

        private static Reader<Person, string> Name()
        {
            return env => env.Name;
        }

        private static Reader<Person, string> Surname()
        {
            return env => env.Surname;
        }
    }
}