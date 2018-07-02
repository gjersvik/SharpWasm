using NUnit.Framework;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class Caller
    {
        private static readonly byte[] Wasm = BinaryTools.HexToBytes("0061736D010000000105016000017F0303020000070801046D61696E00010A0E020400412A0B0700100041016A0B");

        [Test]
        public void RunCode()
        {
            var module = WebAssembly.Compile(Wasm);
            var caller = module.Instantiate();

            Assert.That(caller.Run("main"), Is.EqualTo(43));
        }
    }
}
