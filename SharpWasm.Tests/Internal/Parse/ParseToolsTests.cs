using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class ParseToolsTests
    {
        [Test]
        public void ToArray()
        {
            const string hex = "0102030405";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(ParseTools.ToArray(reader, 5, VarIntUnsigned.ToByte),
                    Is.EqualTo(new byte[] {1, 2, 3, 4, 5}));
            }
        }
    }
}