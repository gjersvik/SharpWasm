using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Sections;
using GlobalType = SharpWasm.Core.Types.GlobalType;
using TableType = SharpWasm.Core.Types.TableType;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class ImportTests
    {
        [Test]
        public void OneOfEach()
        {
            var func = new ImportEntryFunction("Test", "Func", 0);
            var table = new ImportEntryTable("Test", "Table", new TableType(new Limits(1)));
            var memory = new ImportEntryMemory("Test", "Memory", new MemoryType(new Limits(1)));
            var global = new ImportEntryGlobal("Test", "Global", new GlobalType(ValueType.I32,false));
            var each = ImmutableArray.Create<ImportEntry>(func, table, memory, global);

            var import = new Import(each);
            Assert.Multiple(() =>
            {
                Assert.That(import.Id, Is.EqualTo(SectionCode.Import), "Id");
                Assert.That(import.Count, Is.EqualTo(4), "Count");
                Assert.That(import.Entries, Is.EqualTo(each), "Entries");
                Assert.That(import.Functions.FirstOrDefault(), Is.EqualTo(func), "Functions");
                Assert.That(import.Table, Is.EqualTo(table), "Table");
                Assert.That(import.Memory, Is.EqualTo(memory), "Memory");
                Assert.That(import.Globals.FirstOrDefault(), Is.EqualTo(global), "Global");
            });
        }
    }
}
