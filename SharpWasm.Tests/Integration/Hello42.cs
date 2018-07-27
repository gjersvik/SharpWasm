using System.Linq;
using NUnit.Framework;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class Hello42
    {
        private static readonly byte[] Wasm = BinaryTools.HexToBytes("0061736D010000000105016000017F03020100070801046D61696E00000A06010400412A0B000A046E616D650203010000");

        [Test]
        public void GetCustomName()
        {
            var module = WebAssembly.Compile(Wasm);

            Assert.That(module.CustomSections("name"), Is.Not.Empty);
        }
        [Test]
        public void GetExports()
        {
            var module = WebAssembly.Compile(Wasm);
            var exports = module.Exports();
            var export = exports.First();

            Assert.That(export, Is.EqualTo(new ModuleExportDescriptor(ExternalKind.Function, "main")));
        }
        [Test]
        public void RunCode()
        {
            var module = WebAssembly.Compile(Wasm);
            var hello42 = module.Instantiate();

            Assert.That(hello42.Run("main"), Is.EqualTo(42));
        }
    }
}
