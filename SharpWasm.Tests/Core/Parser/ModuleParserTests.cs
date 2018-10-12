using System;
using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class ModuleParserTests
    {
        [Test]
        public void ToModuleWrongMagic()
        {
            Assert.That(() => ModuleParser.ToModule(BinaryTools.HexToBytes("0061736E01000000")), Throws.Exception);
        }

        [Test]
        public void ToModuleWrongVersion()
        {
            Assert.That(() => ModuleParser.ToModule(BinaryTools.HexToBytes("0061736D02000000")), Throws.Exception);
        }

        [Test]
        public void ToSegmentMemory()
        {
            const string hex = "050702" + TestValues.MemoryTypeHex + TestValues.MemoryTypeHex;
            var sections = RunSection(hex);
            var memory = sections.Memory;
            Assert.That(memory, Is.EqualTo(new[] {TestValues.MemoryType, TestValues.MemoryType}).AsCollection,
                "Entries");
        }

        [Test]
        public void ToSegmentGlobal()
        {
            const string hex = "060B02" + TestValues.GlobalHex + TestValues.GlobalHex;
            var sections = RunSection(hex);
            Assert.That(sections.Global,
                Is.EqualTo(new[] {TestValues.Global, TestValues.Global}).AsCollection, "Entries");
        }

        [Test]
        public void ToSegmentStart()
        {
            const string hex = "08012A";
            var sections = RunSection(hex);
            Assert.That(sections.Start, Is.EqualTo(42));
        }

        [Test]
        public void ToSegmentThrow()
        {
            Assert.That(() => RunSection("1000"), Throws.InstanceOf<NotImplementedException>());
        }

        private static Sections RunSection(string hex)
        {
            using (var reader = BinaryTools.HexToReader(hex))
            {
                return ModuleParser.ToSections(reader);
            }
        }
    }
}