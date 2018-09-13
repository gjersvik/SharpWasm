using NUnit.Framework;
using SharpWasm.Core;
using SharpWasm.Core.Runtime;

namespace SharpWasm.Tests.Core
{
    [TestFixture]
    public class ModuleInstanceTests
    {
        [Test]
        public void GetExport()
        {
            var mi = new ModuleInstance();
            Assert.That(mi.GetExport(""), Is.EqualTo(new ExternalValue()));
        }
    }
}
