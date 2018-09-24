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

        [Test]
        public void Empty()
        {
            var module = new Module();
            Assert.Multiple(() =>
            {
                Assert.That(module.Custom, Is.Empty, "Custom");
                Assert.That(module.Types, Is.Empty, "Types");
                Assert.That(module.Funcs, Is.Empty, "Funcs");
                Assert.That(module.Tables, Is.Empty, "Tables");
                Assert.That(module.Mems, Is.Empty, "Mems");
                Assert.That(module.Globals, Is.Empty, "Globals");
                Assert.That(module.Elem, Is.Empty, "Elem");
                Assert.That(module.Data, Is.Empty, "Data");
                Assert.That(module.Start, Is.Null, "Start");
                Assert.That(module.ImportsArray, Is.Empty, "ImportsArray");
                Assert.That(module.ExportsArray, Is.Empty, "ExportsArray");
            });
        }
    }
}