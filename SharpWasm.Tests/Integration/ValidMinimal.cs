using NUnit.Framework;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Integration
{
    [TestFixture]
    public class ValidMinimal
    {
        private static readonly byte[] MinimalValid = BinaryTools.HexToBytes("0061736D01000000");

        [Test]
        public void IsValid()
        {
            Assert.That(WebAssembly.Validate(MinimalValid), Is.True);
        }
    }

}
