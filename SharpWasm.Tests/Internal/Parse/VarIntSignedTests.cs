using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class VarIntSignedTests
    {
        [Test]
        public void WikipediaExample()
        {
            const string hex = "9BF159";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntSigned(reader);
                Assert.That(number.Long, Is.EqualTo(-624485));
                Assert.That(number.Int, Is.EqualTo(-624485));
                Assert.That(number.Count, Is.EqualTo(3));
            }
        }
        [Test]
        public void One()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntSigned(reader);
                Assert.That(number.SByte, Is.EqualTo(1));
                Assert.That(number.Count, Is.EqualTo(1));
            }
        }
    }
}
