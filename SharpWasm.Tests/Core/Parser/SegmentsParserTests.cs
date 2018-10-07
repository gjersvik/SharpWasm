using System;
using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;
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

        [Test]
        public void ToExport()
        {
            const string hex = TestValues.TestStringHex + "0002";
            Export exportEntry;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                exportEntry = SegmentsParser.ToExport(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(exportEntry.Name, Is.EqualTo("test"), "Name");
                Assert.That(exportEntry.Type, Is.EqualTo(ExternalKind.Function), "Type");
                Assert.That(exportEntry.Index, Is.EqualTo(2), "Index");
            });
        }

        [Test]
        public void ToElement()
        {
            const string hex = "00" + TestValues.InitExprHex + "0301022A";
            Element elementSegment;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                elementSegment = SegmentsParser.ToElement(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(elementSegment.Table, Is.EqualTo(0), "Table");
                Assert.That(elementSegment.Offset, Is.EqualTo(TestValues.InitExpr).AsCollection, "Offset");
                Assert.That(elementSegment.Init, Is.EqualTo(new uint[] { 1, 2, 42 }), "Init");
            });
        }
    }
}
