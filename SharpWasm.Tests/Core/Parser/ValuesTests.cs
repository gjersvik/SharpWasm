using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class ValuesTests
    {
        [Test]
        public void WikipediaSigned()
        {
            const string hex = "9BF159";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = Values.SignedVar(reader, out var count);
                Assert.That(number, Is.EqualTo(-624485));
                Assert.That(count, Is.EqualTo(3));
            }
        }

        [Test]
        public void WikipediaUnsigned()
        {
            const string hex = "E58E26";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = Values.UnsignedVar(reader, out var count);
                Assert.That(number, Is.EqualTo(624485));
                Assert.That(count, Is.EqualTo(3));
            }
        }

        [Test]
        public void OneSigned()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = Values.SignedVar(reader, out var count);
                Assert.That(number, Is.EqualTo(1));
                Assert.That(count, Is.EqualTo(1));
            }
        }

        [Test]
        public void OneUnsigned()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = Values.UnsignedVar(reader, out var count);
                Assert.That(number, Is.EqualTo(1));
                Assert.That(count, Is.EqualTo(1));
            }
        }

        [Test]
        public void ZeroUnsigned()
        {
            const string hex = "00";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = Values.UnsignedVar(reader, out var count);
                Assert.That(number, Is.EqualTo(0));
                Assert.That(count, Is.EqualTo(1));
            }
        }

        [Test]
        public void ToLong()
        {
            const string hex = "9BF159";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(Values.ToLong(reader), Is.EqualTo(-624485));
            }
        }

        [Test]
        public void ToInt()
        {
            const string hex = "9BF159";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(Values.ToInt(reader), Is.EqualTo(-624485));
            }
        }

        [Test]
        public void ToSByte()
        {
            const string hex = "0A";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(Values.ToSByte(reader), Is.EqualTo(10));
            }
        }

        [Test]
        public void ToUInt()
        {
            const string hex = "E58E26";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(Values.ToUInt(reader), Is.EqualTo(624485));
            }
        }

        [Test]
        public void ToByte()
        {
            const string hex = "0A";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(Values.ToByte(reader), Is.EqualTo(10));
            }
        }

        [Test]
        public void ToBool()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(Values.ToBool(reader), Is.True);
            }
        }
    }
}
