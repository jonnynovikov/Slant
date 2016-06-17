using System.Reflection;
using NUnit.Framework;

namespace Slant.Tests
{
    public class SpecRunner : DebuggerShim
    {
        static Assembly specsAssembly = typeof(DebuggerShim).Assembly;

        [Test]
        public void Spectate() => Debug(specsAssembly);
    }
}