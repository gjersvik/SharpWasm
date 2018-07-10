using System.Text;
using NUnit.Framework;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class Memory
    {
        private static readonly byte[] Wasm = BinaryTools.HexToBytes(
            "0061736D0100000001090260027F7F0060000002190207636F6E736F6C65036C6F670000026A73036D656D02000103020101070801046D61696E00010A0A0108004100410C10000B0B12010041000B0C48656C6C6F20576F726C6421");

        private string _output = "";

        [Test]
        public void RunCode()
        {
            var module = WebAssembly.Compile(Wasm);
            var memory = new WebAssemblyMemory(1);

            var import = new WebAssemblyImports();
            import.Add("console", "log", ConsoleLog);
            import.Add("js", "mem", memory);

            var caller = module.Instantiate(import);
            caller.Run("main");

            Assert.That(_output, Is.EqualTo("Hello World!"));
        }

        private int ConsoleLog(WebAssemblyInstance instance, params int[] args)
        {
            var bytes = instance.Memory.ReadBytes(args[0], args[1]);
            _output = Encoding.UTF8.GetString(bytes);
            return 0;
        }
    }
}