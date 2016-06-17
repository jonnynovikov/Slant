#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion
using FluentAssertions;
using LanguageExt;
using NSpectator;
using static LanguageExt.Prelude;

namespace Slant.Monad.Specs
{
    public class OptionSpec : Spec
    {
        public void Specify_binding()
        {
            Option<int> option = Some(1000);
            Option<int> option2 = Some(2000);

            var result = from o in option
                         select o;

            (result.IsSome && result.IfSome(x => x.Should().Be(1000)).Return(true)).Should().BeTrue();
            result.Match(Some: _ => true, None: () => false).Should().BeTrue();
            result.Match(Some: _ => true, None: () => false).Should().BeTrue();

            result = from o in option
                     from o2 in option2
                     select o2;

            (result.IsSome && result.IfSome(x => x.Should().Be(2000)).Return(true)).Should().BeTrue();
            (result.Match(Some: _ => true, None: () => false)).Should().BeTrue();
            (result.Match(Some: _ => true, None: () => false)).Should().BeTrue();

            result = from o in option
                     from o2 in Nothing()
                     select o2;

            result.IsNone.Should().BeTrue();
        }

        public Option<int> Nothing()
        {
            return None;
        }
    }
}