using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class MemoryTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Memory.Empty.Id, Is.EqualTo(SectionCode.Memory), "Id");
                Assert.That(Memory.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Memory.Empty.Entries, Is.Empty, "Entries");
            });
        }

        [Test]
        public void Properties()
        {
            var memory = new Memory(new[] {TestValues.MemoryType, TestValues.MemoryType});
            Assert.Multiple(() =>
            {
                Assert.That(memory.Count, Is.EqualTo(2), "Count");
                Assert.That(memory.Entries,
                    Is.EqualTo(new[] {TestValues.MemoryType, TestValues.MemoryType}).AsCollection, "Entries");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "02" + TestValues.MemoryTypeHex + TestValues.MemoryTypeHex;
            Memory memory;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                memory = new Memory(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(memory.Count, Is.EqualTo(2), "Count");
                Assert.That(memory.Entries,
                    Is.EqualTo(new[] { TestValues.MemoryType, TestValues.MemoryType }).AsCollection, "Entries");
            });
        }
    }
}