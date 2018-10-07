using System;
using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Code;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    public class CodeParserTests
    {
        [Test]
        public void ToBrTable()
        {
            const string hex = "040102030405";
            BrTable brTable;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                brTable = CodeParser.ToBrTable(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(brTable.TargetTable, Is.EqualTo(new[] { 1, 2, 3, 4 }), "TargetTable");
                Assert.That(brTable.DefaultTarget, Is.EqualTo(5), "DefaultTarget");
            });
        }

        [Test]
        public void ToCallIndirect()
        {
            const string hex = "0401";
            CallIndirect callIndirect;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                callIndirect = CodeParser.ToCallIndirect(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(callIndirect.TypeIndex, Is.EqualTo(4), "TypeIndex");
                Assert.That(callIndirect.Reserved, Is.True, "Reserved");
            });
        }

        [Test]
        public void ToMemoryImmediate()
        {
            const string hex = "0102";
            MemoryImmediate memoryImmediate;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                memoryImmediate = CodeParser.ToMemoryImmediate(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(memoryImmediate.Flags, Is.EqualTo(1), "Flags");
                Assert.That(memoryImmediate.Offset, Is.EqualTo(2), "Offset");
            });
        }


        [Test]
        public void ToInitExpr()
        {
            const string hex = "412A0B";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var initExpr = CodeParser.ToInitExpr(reader);
                Assert.That(initExpr.Length, Is.EqualTo(2));
            }
        }

        [TestCase(OpCode.Unreachable)]
        [TestCase(OpCode.Nop)]
        [TestCase(OpCode.Else)]
        [TestCase(OpCode.End)]
        [TestCase(OpCode.Return)]
        [TestCase(OpCode.Drop)]
        [TestCase(OpCode.Select)]
        [TestCase(OpCode.I32Eqz)]
        [TestCase(OpCode.I32Eq)]
        [TestCase(OpCode.I32Ne)]
        [TestCase(OpCode.I32LtS)]
        [TestCase(OpCode.I32LtU)]
        [TestCase(OpCode.I32GtS)]
        [TestCase(OpCode.I32GtU)]
        [TestCase(OpCode.I32LeS)]
        [TestCase(OpCode.I32LeU)]
        [TestCase(OpCode.I32GeS)]
        [TestCase(OpCode.I32GeU)]
        [TestCase(OpCode.I64Eqz)]
        [TestCase(OpCode.I64Eq)]
        [TestCase(OpCode.I64Ne)]
        [TestCase(OpCode.I64LtS)]
        [TestCase(OpCode.I64LtU)]
        [TestCase(OpCode.I64GtS)]
        [TestCase(OpCode.I64GtU)]
        [TestCase(OpCode.I64LeS)]
        [TestCase(OpCode.I64LeU)]
        [TestCase(OpCode.I64GeS)]
        [TestCase(OpCode.I64GeU)]
        [TestCase(OpCode.F32Eq)]
        [TestCase(OpCode.F32Ne)]
        [TestCase(OpCode.F32Lt)]
        [TestCase(OpCode.F32Gt)]
        [TestCase(OpCode.F32Le)]
        [TestCase(OpCode.F32Ge)]
        [TestCase(OpCode.F64Eq)]
        [TestCase(OpCode.F64Ne)]
        [TestCase(OpCode.F64Lt)]
        [TestCase(OpCode.F64Gt)]
        [TestCase(OpCode.F64Le)]
        [TestCase(OpCode.F64Ge)]
        [TestCase(OpCode.I32Clz)]
        [TestCase(OpCode.I32Ctz)]
        [TestCase(OpCode.I32Popcnt)]
        [TestCase(OpCode.I32Add)]
        [TestCase(OpCode.I32Sub)]
        [TestCase(OpCode.I32Mul)]
        [TestCase(OpCode.I32DivS)]
        [TestCase(OpCode.I32DivU)]
        [TestCase(OpCode.I32RemS)]
        [TestCase(OpCode.I32RemU)]
        [TestCase(OpCode.I32And)]
        [TestCase(OpCode.I32Or)]
        [TestCase(OpCode.I32Xor)]
        [TestCase(OpCode.I32Shl)]
        [TestCase(OpCode.I32ShrS)]
        [TestCase(OpCode.I32ShrU)]
        [TestCase(OpCode.I32Rotl)]
        [TestCase(OpCode.I32Rotr)]
        [TestCase(OpCode.I64Clz)]
        [TestCase(OpCode.I64Ctz)]
        [TestCase(OpCode.I64Popcnt)]
        [TestCase(OpCode.I64Add)]
        [TestCase(OpCode.I64Sub)]
        [TestCase(OpCode.I64Mul)]
        [TestCase(OpCode.I64DivS)]
        [TestCase(OpCode.I64DivU)]
        [TestCase(OpCode.I64RemS)]
        [TestCase(OpCode.I64RemU)]
        [TestCase(OpCode.I64And)]
        [TestCase(OpCode.I64Or)]
        [TestCase(OpCode.I64Xor)]
        [TestCase(OpCode.I64Shl)]
        [TestCase(OpCode.I64ShrS)]
        [TestCase(OpCode.I64ShrU)]
        [TestCase(OpCode.I64Rotl)]
        [TestCase(OpCode.I64Rotr)]
        [TestCase(OpCode.F32Abs)]
        [TestCase(OpCode.F32Neg)]
        [TestCase(OpCode.F32Ceil)]
        [TestCase(OpCode.F32Floor)]
        [TestCase(OpCode.F32Trunc)]
        [TestCase(OpCode.F32Nearest)]
        [TestCase(OpCode.F32Sqrt)]
        [TestCase(OpCode.F32Add)]
        [TestCase(OpCode.F32Sub)]
        [TestCase(OpCode.F32Mul)]
        [TestCase(OpCode.F32Div)]
        [TestCase(OpCode.F32Min)]
        [TestCase(OpCode.F32Max)]
        [TestCase(OpCode.F32Copysign)]
        [TestCase(OpCode.F64Abs)]
        [TestCase(OpCode.F64Neg)]
        [TestCase(OpCode.F64Ceil)]
        [TestCase(OpCode.F64Floor)]
        [TestCase(OpCode.F64Trunc)]
        [TestCase(OpCode.F64Nearest)]
        [TestCase(OpCode.F64Sqrt)]
        [TestCase(OpCode.F64Add)]
        [TestCase(OpCode.F64Sub)]
        [TestCase(OpCode.F64Mul)]
        [TestCase(OpCode.F64Div)]
        [TestCase(OpCode.F64Min)]
        [TestCase(OpCode.F64Max)]
        [TestCase(OpCode.F64Copysign)]
        [TestCase(OpCode.I32WrapI64)]
        [TestCase(OpCode.I32TruncSf32)]
        [TestCase(OpCode.I32TruncUf32)]
        [TestCase(OpCode.I32TruncSf64)]
        [TestCase(OpCode.I32TruncUf64)]
        [TestCase(OpCode.I64ExtendSi32)]
        [TestCase(OpCode.I64ExtendUi32)]
        [TestCase(OpCode.I64TruncSf32)]
        [TestCase(OpCode.I64TruncUf32)]
        [TestCase(OpCode.I64TruncSf64)]
        [TestCase(OpCode.I64TruncUf64)]
        [TestCase(OpCode.F32ConvertSi32)]
        [TestCase(OpCode.F32ConvertUi32)]
        [TestCase(OpCode.F32ConvertSi64)]
        [TestCase(OpCode.F32ConvertUi64)]
        [TestCase(OpCode.F32DemoteF64)]
        [TestCase(OpCode.F64ConvertSi32)]
        [TestCase(OpCode.F64ConvertUi32)]
        [TestCase(OpCode.F64ConvertSi64)]
        [TestCase(OpCode.F64ConvertUi64)]
        [TestCase(OpCode.F64PromoteF32)]
        [TestCase(OpCode.I32ReinterpretF32)]
        [TestCase(OpCode.I64ReinterpretF64)]
        [TestCase(OpCode.F32ReinterpretI32)]
        [TestCase(OpCode.F64ReinterpretI64)]
        public void Parse(byte opCode)
        {
            IInstruction inst;
            using (var reader = BinaryTools.BytesToReader(opCode))
            {
                inst = CodeParser.ToInstruction(reader);
            }
            Assert.That(inst.OpCode, Is.EqualTo((OpCode)opCode));
            Assert.That(inst.HaveImmediate, Is.False);
        }

        [TestCase(OpCode.Block)]
        [TestCase(OpCode.Loop)]
        [TestCase(OpCode.If)]
        public void ParseBlockType(byte opCode)
        {
            Parse(new[] { opCode, (byte)0x40 }, (OpCode)opCode, BlockType.EmptyBlock);
        }

        [TestCase(OpCode.Br)]
        [TestCase(OpCode.BrIf)]
        [TestCase(OpCode.GetLocal)]
        [TestCase(OpCode.SetLocal)]
        [TestCase(OpCode.TeeLocal)]
        [TestCase(OpCode.GetGlobal)]
        [TestCase(OpCode.SetGlobal)]
        public void ParseUInt(byte opCode)
        {
            Parse(new[] { opCode, (byte)0x2A }, (OpCode)opCode, 42U);
        }

        [Test]
        public void ParseBrTable()
        {
            Parse(BinaryTools.HexToBytes("0E040102030405"), OpCode.BrTable, new BrTable(ImmutableArray.Create<uint>(1, 2, 3, 4), 5));
        }

        [TestCase(OpCode.I32Load)]
        [TestCase(OpCode.I64Load)]
        [TestCase(OpCode.F32Load)]
        [TestCase(OpCode.F64Load)]
        [TestCase(OpCode.I32Load8S)]
        [TestCase(OpCode.I32Load8U)]
        [TestCase(OpCode.I32Load16S)]
        [TestCase(OpCode.I32Load16U)]
        [TestCase(OpCode.I64Load8S)]
        [TestCase(OpCode.I64Load8U)]
        [TestCase(OpCode.I64Load16S)]
        [TestCase(OpCode.I64Load16U)]
        [TestCase(OpCode.I64Load32S)]
        [TestCase(OpCode.I64Load32U)]
        [TestCase(OpCode.I32Store)]
        [TestCase(OpCode.I64Store)]
        [TestCase(OpCode.F32Store)]
        [TestCase(OpCode.F64Store)]
        [TestCase(OpCode.I32Store8)]
        [TestCase(OpCode.I32Store16)]
        [TestCase(OpCode.I64Store8)]
        [TestCase(OpCode.I64Store16)]
        [TestCase(OpCode.I64Store32)]
        public void ParseMemoryImmediate(byte opCode)
        {
            Parse(new[] { opCode, (byte)0x01, (byte)0x02 }, (OpCode)opCode, new MemoryImmediate(1, 2));
        }

        [TestCase(OpCode.CurrentMemory)]
        [TestCase(OpCode.GrowMemory)]
        public void ParseBool(byte opCode)
        {
            Parse(new[] { opCode, (byte)0x01 }, (OpCode)opCode, true);
        }

        [Test]
        public void ParseI32Const() => Parse(BinaryTools.HexToBytes("412A"), OpCode.I32Const, 42);
        [Test]
        public void ParseI64Const() => Parse(BinaryTools.HexToBytes("422A"), OpCode.I64Const, 42L);
        [Test]
        public void ParseF32Const() => Parse(BinaryTools.HexToBytes("4300002842"), OpCode.F32Const, 42.0F);
        [Test]
        public void ParseF64Const() => Parse(BinaryTools.HexToBytes("440000000000004540"), OpCode.F64Const, 42.0);

        private static void Parse<T>(byte[] code, OpCode opCode, T immediate)
        {
            Instruction<T> inst;
            using (var reader = BinaryTools.BytesToReader(code))
            {
                inst = (Instruction<T>)CodeParser.ToInstruction(reader);
            }
            Assert.That(inst.OpCode, Is.EqualTo(opCode));
            Assert.That(inst.HaveImmediate, Is.True);
            Assert.That(inst.Immediate, Is.EqualTo(immediate));
        }


        [Test]
        public void ToInstructionParseInvalid()
        {
            using (var reader = BinaryTools.HexToReader("FF"))
            {
                // ReSharper disable once AccessToDisposedClosure
                Assert.That(() => CodeParser.ToInstruction(reader), Throws.InstanceOf<NotImplementedException>());
            }
        }
    }
}
