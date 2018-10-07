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

        [Test]
        public void Start()
        {
            const string hex = "2A";
            var sections = new Sections();
            using (var reader = BinaryTools.HexToReader(hex))
            {
                sections.ParseStart(reader);
            }
            Assert.That(sections.Start, Is.EqualTo(42));
        }

        [Test]
        public void Global()
        {
            const string hex = "02" + TestValues.GlobalHex + TestValues.GlobalHex;
            var sections = new Sections();
            using (var reader = BinaryTools.HexToReader(hex))
            {
                sections.ParseGlobal(reader);
            }
            Assert.That(sections.Global,
                Is.EqualTo(new[] { TestValues.Global, TestValues.Global }).AsCollection, "Entries");
        }


        [Test]
        public void Export()
        {
            const string hex = "02" + TestValues.ExportHex + TestValues.ExportHex;
            var sections = new Sections();
            using (var reader = BinaryTools.HexToReader(hex))
            {
                sections.ParseExport(reader);
            }
            Assert.That(sections.Export,
                Is.EqualTo(new[] { TestValues.Export, TestValues.Export }).AsCollection, "Entries");
        }
    }
}