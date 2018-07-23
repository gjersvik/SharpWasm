using NUnit.Framework;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class Adder
    {
        private static readonly byte[] Wasm = BinaryTools.HexToBytes("0061736D0100000001070160027F7F017F03020100070801046D61696E00000A09010700200020016A0B001C046E616D650106010003616464020D01000200036C68730103726873");

        [Test]
        public void RunCode()
        {
            var module = WebAssembly.Compile(Wasm);
            var adder = module.Instantiate();

            Assert.That(adder.Run("main", 123, 321), Is.EqualTo(444));
        }
    }
}
