using System;
using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Segments;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class SegmentsParserTests
    {
        [Test]
        public void ToImportWrongKind()
        {
            const string hex = "0161016204";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => SegmentsParser.ToImport(reader), Throws.TypeOf<NotImplementedException>());
            }
        }

        [Test]
        public void ToImportTable()
        {
            const string hex = "0161016201" + TestValues.TableTypeHex;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var import = SegmentsParser.ToImport(reader);
                Assert.That(import.Table, Is.EqualTo(TestValues.TableType));
            }
        }

        [Test]
        public void ToImportGlobal()
        {
            const string hex = "0161016203" + TestValues.GlobalTypeHex;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var import = SegmentsParser.ToImport(reader);
                Assert.That(import.Global, Is.EqualTo(TestValues.GlobalType));
            }
        }

        [Test]
        public void ToGlobal()
        {
            const string hex = TestValues.GlobalTypeHex + TestValues.InitExprHex;
            Global globalEntry;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                globalEntry = SegmentsParser.ToGlobal(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(globalEntry.Type, Is.EqualTo(TestValues.GlobalType), "Type");
                Assert.That(globalEntry.Init, Is.EqualTo(TestValues.InitExpr).AsCollection, "Init");
            });
        }
    }
}
