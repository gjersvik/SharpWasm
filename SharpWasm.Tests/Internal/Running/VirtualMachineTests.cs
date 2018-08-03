using System;
using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Running;

namespace SharpWasm.Tests.Internal.Running
{
    [TestFixture]
    public class VirtualMachineTests
    {
        private static Local DefaultLocal => new Local(new Stack());

        [Test]
        public void InvalidInstruction()
        {
            var code = ImmutableArray.Create<IInstruction>(new Instruction((OpCode)0xFF));
            Assert.That(() => VirtualMachine.ExecuteCode(code, DefaultLocal), Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void Unreachable()
        {
            var code = ImmutableArray.Create<IInstruction>(Instruction.Unreachable);
            Assert.That(() => VirtualMachine.ExecuteCode(code, DefaultLocal), Throws.Exception.Message.Contain("Unreachable"));
        }

        [Test]
        public void Nop()
        {
            var code = ImmutableArray.Create<IInstruction>(Instruction.Nop);
            Assert.That(() => VirtualMachine.ExecuteCode(code, DefaultLocal), Throws.Nothing);
        }

        [Test]
        public void Drop()
        {
            var code = ImmutableArray.Create<IInstruction>(Instruction.Drop);
            var local = DefaultLocal;
            local.Stack.Push(1);
            VirtualMachine.ExecuteCode(code, local);
            Assert.That(local.Stack.Count, Is.EqualTo(0));
        }

        [TestCase(OpCode.Block)]
        [TestCase(OpCode.Loop)]
        [TestCase(OpCode.If)]
        [TestCase(OpCode.Else)]
        [TestCase(OpCode.End)]
        [TestCase(OpCode.Br)]
        [TestCase(OpCode.BrIf)]
        [TestCase(OpCode.BrTable)]
        [TestCase(OpCode.Return)]
        [TestCase(OpCode.Call)]
        [TestCase(OpCode.CallIndirect)]
        [TestCase(OpCode.Select)]
        [TestCase(OpCode.GetLocal)]
        [TestCase(OpCode.SetLocal)]
        [TestCase(OpCode.TeeLocal)]
        [TestCase(OpCode.GetGlobal)]
        [TestCase(OpCode.SetGlobal)]
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
        [TestCase(OpCode.CurrentMemory)]
        [TestCase(OpCode.GrowMemory)]
        [TestCase(OpCode.I32Const)]
        [TestCase(OpCode.I64Const)]
        [TestCase(OpCode.F32Const)]
        [TestCase(OpCode.F64Const)]
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
        public void NotImplemented(byte op)
        {
            var code = ImmutableArray.Create<IInstruction>(new Instruction((OpCode)op));
            Assert.That(() => VirtualMachine.ExecuteCode(code, DefaultLocal), Throws.TypeOf<NotImplementedException>());
        }
    }
}
