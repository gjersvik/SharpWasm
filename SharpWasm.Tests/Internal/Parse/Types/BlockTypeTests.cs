using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class BlockTypeTests
    {
        [Test]
        public void I32()
        {
            var block = new VarIntSigned(-0x01).BlockType;
            Assert.That(block, Is.EqualTo(BlockType.I32));
        }
        [Test]
        public void I64()
        {
            var block = new VarIntSigned(-0x02).BlockType;
            Assert.That(block, Is.EqualTo(BlockType.I64));
        }
        [Test]
        public void F32()
        {
            var block = new VarIntSigned(-0x03).BlockType;
            Assert.That(block, Is.EqualTo(BlockType.F32));
        }
        [Test]
        public void F64()
        {
            var block = new VarIntSigned(-0x04).BlockType;
            Assert.That(block, Is.EqualTo(BlockType.F64));
        }
        [Test]
        public void EmptyBlock()
        {
            var block = new VarIntSigned(-0x40).BlockType;
            Assert.That(block, Is.EqualTo(BlockType.EmptyBlock));
        }
    }
}
