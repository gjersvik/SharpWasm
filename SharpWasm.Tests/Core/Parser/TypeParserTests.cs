using System;
using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;
using SharpWasm.Tests.Helpers;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Tests.Core.Parser
{
    [TestFixture]
    public class TypeParserTests
    {
        [Test]
        public void ToFunctionType()
        {
            const string hex = "60047F7E7D7C017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var funcType = TypeParser.ToFunctionType(reader);
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
                Assert.That(() => TypeParser.ToFunctionType(reader), Throws.TypeOf<NotImplementedException>());
            }
        }

        private static readonly ImmutableArray<ValueType> Values =
            ImmutableArray.Create(ValueType.I32, ValueType.I64, ValueType.F32, ValueType.F64);


        [Test]
        public void ToLimits()
        {
            const string hex = "010102";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var limits = TypeParser.ToLimits(reader);
                Assert.That(limits.Min, Is.EqualTo(1));
                Assert.That(limits.Max, Is.EqualTo(2));
            }
        }

        [Test]
        public void ToMemoryType()
        {
            const string hex = "0001";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var memoryType = TypeParser.ToMemoryType(reader);
                Assert.That(memoryType.Limits, Is.EqualTo(new Limits(1)));
            }
        }

        [Test]
        public void ToElemType()
        {
            const string hex = "70";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var elemType = TypeParser.ToElemType(reader);
                Assert.That(elemType, Is.EqualTo(ElemType.AnyFunc));
            }
        }

        [Test]
        public void ToTableType()
        {
            const string hex = "700001";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var tableType = TypeParser.ToTableType(reader);
                Assert.That(tableType.ElemType, Is.EqualTo(ElemType.AnyFunc));
                Assert.That(tableType.Limits, Is.EqualTo(new Limits(1)));
            }
        }

        [Test]
        public void ToGlobalType()
        {
            const string hex = "7F00";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var globalType = TypeParser.ToGlobalType(reader);
                Assert.That(globalType.ValueType, Is.EqualTo(ValueType.I32));
                Assert.That(globalType.Mutable, Is.False);
            }
        }
    }
}
