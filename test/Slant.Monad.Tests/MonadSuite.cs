using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;

namespace Monad.Tests
{
    public class MonadSuite : DebuggerShim
    {
        static Assembly specsAssembly = typeof(DebuggerShim).Assembly;

        [Test]
        public void Spectate() => Debug(specsAssembly);
    }
}