#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using System.Linq;
using FluentAssertions;
using Monad;
using NSpectator;

namespace Monad.Specs
{
    public class WriterSuite
    {
        private static Writer<string, int> LogNumber(int num) => () => Writer.Tell(num, "Got number: " + num);

        class BindingSpec : Spec
        {
            void When_multiply_and_memo()
            {
                it["should memo multiplication result"] = () =>
                {
                    var res = from a in LogNumber(3)
                        from b in LogNumber(5)
                        select a*b;

                    var memo = res.Memo();

                    (memo().Value == 15 && memo().Output.Count() == 2).Should().BeTrue();
                    (memo().Output.First() == "Got number: 3").Should().BeTrue();
                    (memo().Output.Skip(1).First() == "Got number: 5").Should().BeTrue();
                };
            }

            void When_multiply_and_tell_memo()
            {
                it["should memo multiplication result"] = () =>
                {
                    var res = from a in LogNumber(3)
                        from b in LogNumber(5)
                        from _ in Writer.Tell("Gonna multiply these two")
                        select a*b;

                    var memo = res.Memo();

                    (memo().Value == 15 && memo().Output.Count() == 3).Should().BeTrue();
                    (memo().Output.First() == "Got number: 3").Should().BeTrue();
                    (memo().Output.Skip(1).First() == "Got number: 5").Should().BeTrue();
                    (memo().Output.Skip(2).First() == "Gonna multiply these two").Should().BeTrue();
                };
            }
        }
    }
}