using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Types
{
    [TestFixture]
    public class BlockTypeTests
    {
        [Test]
        public void I32()
        {
            const BlockType block = (BlockType)(-0x01);
            Assert.That(block, Is.EqualTo(BlockType.I32));
        }
        [Test]
        public void I64()
        {
            const BlockType block = (BlockType)(-0x02);
            Assert.That(block, Is.EqualTo(BlockType.I64));
        }
        [Test]
        public void F32()
        {
            const BlockType block = (BlockType)(-0x03);
            Assert.That(block, Is.EqualTo(BlockType.F32));
        }
        [Test]
        public void F64()
        {
            const BlockType block = (BlockType)(-0x04);
            Assert.That(block, Is.EqualTo(BlockType.F64));
        }
        [Test]
        public void EmptyBlock()
        {
            const BlockType block = (BlockType)(-0x40);
            Assert.That(block, Is.EqualTo(BlockType.EmptyBlock));
        }
    }
}
