using System;
using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Tests.Helpers;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class TypesTests
    {
        [Test]
        public void ToFunctionType()
        {
            const string hex = "60047F7E7D7C017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var funcType = SharpWasm.Core.Parser.Types.ToFunctionType(reader);
                Assert.That(funcType.Parameters, Is.EqualTo(Values).AsCollection, "Parameters");
                Assert.That(funcType.Returns, Is.EqualTo(new[] { ValueType.I32 }).AsCollection, "ReturnType");
            }
        }

        [Test]
        public void ToFunctionTypeError()
        {
            const string hex = "40047F7E7D7C70017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => SharpWasm.Core.Parser.Types.ToFunctionType(reader), Throws.TypeOf<NotImplementedException>());
            }
        }

        private static readonly ImmutableArray<ValueType> Values =
            ImmutableArray.Create(ValueType.I32, ValueType.I64, ValueType.F32, ValueType.F64);
    }
}
