using System;
using FluentAssertions;
using FluentAssertions.Specialized;
using LanguageExt;

namespace Slant.Assertions
{
    public class UnitAssertions : ActionAssertions
    {
        public Func<Unit> Subject { get; }

        public UnitAssertions(Func<Unit> func)
            : base(() => func())
        {
            Subject = func;
        }
    }
}