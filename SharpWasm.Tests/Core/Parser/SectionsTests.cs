using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class SectionsTests
    {
        [Test]
        public void Memory()
        {
            const string hex = "02" + TestValues.MemoryTypeHex + TestValues.MemoryTypeHex;
            var sections = new Sections();
            using (var reader = BinaryTools.HexToReader(hex))
            {
                sections.ParseMemory(reader);
            }

            var memory = sections.Memory;
            Assert.That(memory, Is.EqualTo(new[] {TestValues.MemoryType, TestValues.MemoryType}).AsCollection,
                "Entries");
        }
    }
}