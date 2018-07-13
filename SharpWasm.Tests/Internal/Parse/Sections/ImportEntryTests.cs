using System;
using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class ImportEntryTests
    {
        [Test]
        public void Properties()
        {
            var entry = new ImportEntryFunction("Test", "Function", 0);
            Assert.Multiple(() =>
            {
                Assert.That(entry.ModuleLen, Is.EqualTo(4), "ModuleLen");
                Assert.That(entry.ModuleStr, Is.EqualTo("Test"), "ModuleStr");
                Assert.That(entry.FieldLen, Is.EqualTo(8), "FieldLen");
                Assert.That(entry.FieldStr, Is.EqualTo("Function"), "FieldStr");
                Assert.That(entry.Kind, Is.EqualTo(ExternalKind.Function), "Kind");
            });
        }
        [Test]
        public void ParseWrongKind()
        {
            const string hex = "0161016204";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => ImportEntry.Parse(reader), Throws.TypeOf<NotImplementedException>());
            }
        }

        [Test]
        public void ParseTable()
        {
            const string hex = "0161016201" + TestValues.TableTypeHex;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var entry = ImportEntry.Parse(reader) as ImportEntryTable;
                Assert.That(entry?.Type, Is.EqualTo(TestValues.TableType));
            }
        }

        [Test]
        public void ParseGlobal()
        {
            const string hex = "0161016203" + TestValues.GlobalTypeHex;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var entry = ImportEntry.Parse(reader) as ImportEntryGlobal;
                Assert.That(entry?.Type, Is.EqualTo(TestValues.GlobalType));
            }
        }

        [Test]
        public void MemoryType()
        {
            var type = new MemoryType(new ResizableLimits(1));
            var memory = new ImportEntryMemory("Test", "Memory", type);
            Assert.That(memory.Type, Is.EqualTo(type));
        }
    }
}
