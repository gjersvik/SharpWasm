using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Types;
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

        [Test]
        public void ToLong()
        {
            const string hex = "9BF159";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntSigned.ToLong(reader), Is.EqualTo(-624485));
            }
        }

        [Test]
        public void ToInt()
        {
            const string hex = "9BF159";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntSigned.ToInt(reader), Is.EqualTo(-624485));
            }
        }

        [Test]
        public void ToSByte()
        {
            const string hex = "0A";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntSigned.ToSByte(reader), Is.EqualTo(10));
            }
        }

        [Test]
        public void ToValueType()
        {
            const string hex = "7f";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntSigned.ToValueType(reader), Is.EqualTo(ValueType.I32));
            }
        }

        [Test]
        public void ToBlockType()
        {
            const string hex = "40";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntSigned.ToBlockType(reader), Is.EqualTo(BlockType.EmptyBlock));
            }
        }

        [Test]
        public void ToElemType()
        {
            const string hex = "70";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                Assert.That(VarIntSigned.ToElemType(reader), Is.EqualTo(ElemType.AnyFunc));
            }
        }
    }
}
