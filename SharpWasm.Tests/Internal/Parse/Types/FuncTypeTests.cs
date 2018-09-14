using System;
using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class FuncTypeTests
    {
        [Test]
        public void Parse()
        {
            const string hex = "60047F7E7D7C017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var funcType = FuncType.Parse(reader);
                Assert.That(funcType.Parameters, Is.EqualTo(Values).AsCollection, "Parameters");
                Assert.That(funcType.Returns, Is.EqualTo(new []{ValueType.I32}).AsCollection, "ReturnType");
            }
        }

        [Test]
        public void ParseError()
        {
            const string hex = "40047F7E7D7C70017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => FuncType.Parse(reader), Throws.TypeOf<NotImplementedException>() );
            }
        }

        private static readonly ImmutableArray<ValueType> Values =
            ImmutableArray.Create(ValueType.I32, ValueType.I64, ValueType.F32, ValueType.F64);
    }
}