using System;
using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;
using ValueType = SharpWasm.Internal.Parse.ValueType;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class FuncTypeTests
    {
        [Test]
        public void Form()
        {
            Assert.That(FuncType.Form, Is.EqualTo(-0x20));
        }

        [Test]
        public void ParamCount()
        {
            var funcType = new FuncType(Values);
            Assert.That(funcType.ParamCount, Is.EqualTo(4));
        }

        [Test]
        public void ParamTypes()
        {
            var funcType = new FuncType(Values);
            Assert.That(funcType.ParamTypes, Is.EqualTo(Values));
        }

        [Test]
        public void ReturnNull()
        {
            var funcType = new FuncType(Values);
            Assert.That(funcType.ReturnCount, Is.False);
            Assert.That(funcType.ReturnType, Is.Null);
        }

        [Test]
        public void ReturnValue()
        {
            var funcType = new FuncType(Values, ValueType.I32);
            Assert.That(funcType.ReturnCount, Is.True);
            Assert.That(funcType.ReturnType, Is.EqualTo(ValueType.I32));
        }

        [Test]
        public void Parse()
        {
            const string hex = "60047F7E7D7C017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var funcType = new FuncType(reader);
                Assert.That(funcType.ParamCount, Is.EqualTo(4), "ParamCount");
                Assert.That(funcType.ParamTypes, Is.EqualTo(Values).AsCollection, "ParamTypes");
                Assert.That(funcType.ReturnCount, Is.True, "ReturnCount");
                Assert.That(funcType.ReturnType, Is.EqualTo(ValueType.I32), "ReturnType");
            }
        }

        [Test]
        public void ParseError()
        {
            const string hex = "40047F7E7D7C70017F";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => new FuncType(reader), Throws.TypeOf<NotImplementedException>() );
            }
        }

        private static readonly ImmutableArray<ValueType> Values =
            ImmutableArray.Create(ValueType.I32, ValueType.I64, ValueType.F32, ValueType.F64);
    }
}