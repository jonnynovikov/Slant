#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using NUnit.Framework;
using Monad.Parsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Monad.Specs
{
    public class TestExpr
    {
        [Test]
        public void ExpressionTests()
        {
            var ten = Eval("2*3+4");
            ten.Should().Be(10);

            var fourteen = Eval("2*(3+4)");
            fourteen.Should().Be(14);
        }


        public int Eval(string expr)
        {
            var r = New.Expr().Parse(expr);
            return !r.Value.Any() ? 999 : r.Value.First().Item1;
        }
    }


    public class New
    {
        public static Expr Expr()
        {
            return new Expr();
        }

        public static Term Term()
        {
            return new Term();
        }

        public static Factor Factor()
        {
            return new Factor();
        }
    }

    public class Expr : Parser<int>
    {
        public Expr()
            :
                base(
                inp => (from t in New.Term()
                    from e in
                        (from plus in Prim.Character('+')
                            from expr in New.Expr()
                            select expr)
                        | Prim.Return(0)
                    select t + e)
                    .Parse(inp)
                )
        {
        }
    }

    public class Term : Parser<int>
    {
        public Term()
            :
                base(
                inp => (from f in New.Factor()
                    from t in
                        (from mult in Prim.Character('*')
                            from term in New.Term()
                            select term)
                        | Prim.Return(1)
                    select f*t)
                    .Parse(inp)
                )
        {
        }
    }

    public class Factor : Parser<int>
    {
        public Factor()
            :
                base(
                inp => (from choice in
                    Prim.Digit().Select(d => int.Parse(d.Value.ToString()))
                    | (from open in Prim.Character('(')
                        from expr in New.Expr()
                        from close in Prim.Character(')')
                        select expr)
                    select choice).Parse(inp)
                )
        {
        }
    }
}