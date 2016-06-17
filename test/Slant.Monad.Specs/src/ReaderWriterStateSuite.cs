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
    public class ReaderWriterStateSuite
    {
        [Test]
        public void ReaderWriterStateTest1()
        {
            var world = RWS.Return<Env, string, App, int>(0);

            var rws = (from _ in world
                from app in RWS.Get<Env, string, App>()
                from env in RWS.Ask<Env, string, App>()
                from x in Value(app.UsersLoggedIn, "Users logged in: " + app.UsersLoggedIn)
                from y in Value(100, "System folder: " + env.SystemFolder)
                from s in RWS.Put<Env, string, App>(new App {UsersLoggedIn = 35})
                from t in RWS.Tell<Env, string, App>("Process complete")
                select x*y)
                .Memo(new Env(), new App());

            var res = rws();

            res.Value.Should().Be(3400);
            res.State.UsersLoggedIn.Should().Be(35);
            res.Output.Should().HaveCount(3);
            res.Output.First().Should().Be("Users logged in: 34");
            res.Output.Skip(1).First().Should().Be("System folder: C:/Temp");
            res.Output.Skip(2).First().Should().Be("Process complete");
        }

        public static RWS<Env, string, App, int> Value(int val, string log)
        {
            return (r, s) => RWS.Tell<string, App, int>(val, log);
        }
    }

    public class App
    {
        public int UsersLoggedIn = 34;
    }

    public class Env
    {
        public string SystemFolder = "C:/Temp";
    }
}