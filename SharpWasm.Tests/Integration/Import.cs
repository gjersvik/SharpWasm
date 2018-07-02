using NUnit.Framework;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class Import
    {
        private static readonly byte[] Wasm = BinaryTools.HexToBytes(
            "0061736D0100000001080260017F00600000020F0107636F6E736F6C65036C6F67000003020101070901056C6F67497400010A08010600410D10000B");

        [Test]
        public void RunCode()
        {
            var module = WebAssembly.Compile(Wasm);
            var value = 0;

            var import = new WebAssemblyImports();
            import.Add("console", "log", arg => value = arg[0]);

            var caller = module.Instantiate(import);
            caller.Run("main");

            Assert.That(value, Is.EqualTo(13));
        }
    }
}