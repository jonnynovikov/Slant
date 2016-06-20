using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;
using LanguageExt;

namespace Slant.Assertions
{
    public static class ShouldExtensions
    {
        public static OptionAssertions<T> Should<T>(this Option<T> actualValue)
        {
            return new OptionAssertions<T>(actualValue);
        }

        public static UnitAssertions Should(this Func<Unit> func)
        {
            return new UnitAssertions(func);
        }
        
    }
}