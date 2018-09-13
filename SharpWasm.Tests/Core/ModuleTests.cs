using NUnit.Framework;
using SharpWasm.Core;

namespace SharpWasm.Tests.Core
{
    [TestFixture]
    public class ModuleTests
    {
        [Test]
        public void Decode()
        {
            Assert.That(Module.Decode(new byte[0]), Is.InstanceOf<Module>());
        }

        [Test]
        public void Validate()
        {
            var module = new Module();
            Assert.That(module.Validate(), Is.Null);
        }

        [Test]
        public void Imports()
        {
            var module = new Module();
            Assert.That(module.Imports(), Is.Empty);
        }

        [Test]
        public void Exports()
        {
            var module = new Module();
            Assert.That(module.Exports(), Is.Empty);
        }
    }
}
