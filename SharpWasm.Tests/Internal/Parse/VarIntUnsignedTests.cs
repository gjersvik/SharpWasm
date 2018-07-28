using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class VarIntUnsignedTests
    {
        [Test]
        public void WikipediaExample()
        {
            const string hex = "E58E26";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntUnsigned(reader);
                Assert.That(number.UInt, Is.EqualTo(624485));
                Assert.That(number.Count, Is.EqualTo(3));
            }
        }
        [Test]
        public void One()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntUnsigned(reader);
                Assert.That(number.Byte, Is.EqualTo(1));
                Assert.That(number.Bool, Is.True);
                Assert.That(number.SectionCode, Is.EqualTo(SectionCode.Type));
                Assert.That(number.Count, Is.EqualTo(1));
            }
        }
        [Test]
        public void Zero()
        {
            const string hex = "00";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntUnsigned(reader);
                Assert.That(number.Byte, Is.EqualTo(0));
                Assert.That(number.Bool, Is.False);
                Assert.That(number.SectionCode, Is.EqualTo(SectionCode.Custom));
                Assert.That(number.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void ToUInt()
        {
            const string hex = "E58E26";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntUnsigned.ToUInt(reader), Is.EqualTo(624485));
            }
        }

        [Test]
        public void ToByte()
        {
            const string hex = "0A";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntUnsigned.ToByte(reader), Is.EqualTo(10));
            }
        }

        [Test]
        public void ToBool()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntUnsigned.ToBool(reader), Is.True);
            }
        }

        [Test]
        public void ToSectionCode()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntUnsigned.ToSectionCode(reader), Is.EqualTo(SectionCode.Type));
            }
        }
    }
}
