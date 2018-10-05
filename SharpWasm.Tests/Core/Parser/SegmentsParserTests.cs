using System;
using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class SegmentsParserTests
    {
        [Test]
        public void ParseWrongKind()
        {
            const string hex = "0161016204";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => SegmentsParser.ToImport(reader), Throws.TypeOf<NotImplementedException>());
            }
        }

        [Test]
        public void ParseTable()
        {
            const string hex = "0161016201" + TestValues.TableTypeHex;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var import = SegmentsParser.ToImport(reader);
                Assert.That(import.Table, Is.EqualTo(TestValues.TableType));
            }
        }

        [Test]
        public void ParseGlobal()
        {
            const string hex = "0161016203" + TestValues.GlobalTypeHex;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var import = SegmentsParser.ToImport(reader);
                Assert.That(import.Global, Is.EqualTo(TestValues.GlobalType));
            }
        }
    }
}
