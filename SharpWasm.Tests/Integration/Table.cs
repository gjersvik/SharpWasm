using NUnit.Framework;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class Table
    {
        private static readonly byte[] Wasm = BinaryTools.HexToBytes(
            "0061736D01000000010A026000017F60017F017F030403000001040401700002070801046D61696E00020908010041000B0200010A13030400412A0B0400410D0B070020001100000B");

        [TestCase(0, 42)]
        [TestCase(1, 13)]
        public void RunCode(int i, int output)
        {
            var module = WebAssembly.Compile(Wasm);
            var caller = module.Instantiate();

            Assert.That(caller.Run("main", i), Is.EqualTo(output));
        }
    }
}