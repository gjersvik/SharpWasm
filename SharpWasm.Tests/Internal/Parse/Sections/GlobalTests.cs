using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class GlobalTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Global.Empty.Id, Is.EqualTo(SectionCode.Global), "Id");
                Assert.That(Global.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Global.Empty.Entries, Is.Empty, "Entries");
            });
        }

        [Test]
        public void Properties()
        {
            var global = new Global(new[] { TestValues.GlobalEntry, TestValues.GlobalEntry });
            Assert.Multiple(() =>
            {
                Assert.That(global.Count, Is.EqualTo(2), "Count");
                Assert.That(global.Entries,
                    Is.EqualTo(new[] { TestValues.GlobalEntry, TestValues.GlobalEntry }).AsCollection, "Entries");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "02" + TestValues.GlobalEntryHex + TestValues.GlobalEntryHex;
            Global global;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                global = new Global(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(global.Count, Is.EqualTo(2), "Count");
                Assert.That(global.Entries,
                    Is.EqualTo(new[] { TestValues.GlobalEntry, TestValues.GlobalEntry }).AsCollection, "Entries");
            });
        }
    }
}
