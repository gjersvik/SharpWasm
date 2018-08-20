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
        [Test]
        public void InvalidInstruction()
        {
            Assert.That(() => ExecuteInstruction(new Instruction((OpCode) 0xFF)),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void Unreachable()
        {
            Assert.That(() => ExecuteInstruction(Instruction.Unreachable),
                Throws.Exception.Message.Contain("Unreachable"));
        }

        [Test]
        public void Nop()
        {
            Assert.That(() => ExecuteInstruction(Instruction.Nop), Throws.Nothing);
        }

        [Test]
        public void Drop()
        {
            var stack = new Stack();
            stack.Push(1);
            ExecuteInstruction(Instruction.Drop, stack);
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void SelectFirst()
        {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(1);
            ExecuteInstruction(Instruction.Select, stack);
            Assert.That(stack.PopInt(), Is.EqualTo(1));
        }

        [Test]
        public void SelectSecond()
        {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(0);
            ExecuteInstruction(Instruction.Select, stack);
            Assert.That(stack.PopInt(), Is.EqualTo(2));
        }

        [Test]
        public void SelectWrongType()
        {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2.0);
            stack.Push(0);
            Assert.That(() => ExecuteInstruction(Instruction.Select, stack),
                Throws.InstanceOf<WebAssemblyRuntimeException>());
        }

        [Test]
        public void I32Const()
        {
            var stack = new Stack();
            ExecuteInstruction(Instruction.I32Const(24), stack);
            Assert.That(stack.PopInt(), Is.EqualTo(24));
        }

        [Test]
        public void I64Const()
        {
            var stack = new Stack();
            ExecuteInstruction(Instruction.I64Const(24), stack);
            Assert.That(stack.PopLong(), Is.EqualTo(24));
        }

        [Test]
        public void F32Const()
        {
            var stack = new Stack();
            ExecuteInstruction(Instruction.F32Const(24), stack);
            Assert.That(stack.PopFloat(), Is.EqualTo(24));
        }

        [Test]
        public void F64Const()
        {
            var stack = new Stack();
            ExecuteInstruction(Instruction.F64Const(24), stack);
            Assert.That(stack.PopDouble(), Is.EqualTo(24));
        }

        [TestCase(OpCode.I32Eqz, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I32Eqz, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32Eqz, -1, ExpectedResult = 0)]
        public int TestOp(byte op, int value)
        {
            var stack = new Stack();
            stack.Push(value);
            ExecuteInstruction(new Instruction((OpCode)op), stack);
            return stack.PopInt();
        }

        [TestCase(OpCode.I64Eqz, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I64Eqz, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64Eqz, -1, ExpectedResult = 0)]
        public int TestOp(byte op, long value)
        {
            var stack = new Stack();
            stack.Push(value);
            ExecuteInstruction(new Instruction((OpCode)op), stack);
            return stack.PopInt();
        }

        [TestCase(OpCode.I32Eq, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32Eq, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.I32Ne, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32Ne, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LtS, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32LtS, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LtS, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LtU, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32LtU, -42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32LtU, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32GtS, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32GtS, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.I32GtS, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I32GtU, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32GtU, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.I32GtU, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I32LeS, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LeS, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LeS, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LeU, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32LeU, -42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I32LeU, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32GeS, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32GeS, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.I32GeS, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I32GeU, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I32GeU, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.I32GeU, 42, 0, ExpectedResult = 1)]
        public int Relop(byte op, int a, int b)
        {
            var stack = new Stack();
            stack.Push(b);
            stack.Push(a);
            ExecuteInstruction(new Instruction((OpCode)op), stack);
            return stack.PopInt();
        }

        [TestCase(OpCode.I64Eq, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64Eq, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.I64Ne, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64Ne, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LtS, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64LtS, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LtS, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LtU, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64LtU, -42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64LtU, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64GtS, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64GtS, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.I64GtS, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I64GtU, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64GtU, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.I64GtU, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I64LeS, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LeS, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LeS, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LeU, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64LeU, -42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.I64LeU, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64GeS, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64GeS, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.I64GeS, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.I64GeU, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.I64GeU, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.I64GeU, 42, 0, ExpectedResult = 1)]
        public int Relop(byte op, long a, long b)
        {
            var stack = new Stack();
            stack.Push(b);
            stack.Push(a);
            ExecuteInstruction(new Instruction((OpCode)op), stack);
            return stack.PopInt();
        }

        [TestCase(OpCode.F32Eq, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Eq, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.F32Ne, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.F32Ne, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Lt, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.F32Lt, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Lt, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Gt, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.F32Gt, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Gt, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.F32Le, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Le, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Le, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Ge, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Ge, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.F32Ge, 42, 0, ExpectedResult = 1)]
        public int Relop(byte op, float a, float b)
        {
            var stack = new Stack();
            stack.Push(b);
            stack.Push(a);
            ExecuteInstruction(new Instruction((OpCode)op), stack);
            return stack.PopInt();
        }

        [TestCase(OpCode.F64Eq, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Eq, 42, -42, ExpectedResult = 0)]
        [TestCase(OpCode.F64Ne, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.F64Ne, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Lt, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.F64Lt, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Lt, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Gt, 42, 42, ExpectedResult = 0)]
        [TestCase(OpCode.F64Gt, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Gt, 42, 0, ExpectedResult = 1)]
        [TestCase(OpCode.F64Le, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Le, -42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Le, 0, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Ge, 42, 42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Ge, 42, -42, ExpectedResult = 1)]
        [TestCase(OpCode.F64Ge, 42, 0, ExpectedResult = 1)]
        public int Relop(byte op, double a, double b)
        {
            var stack = new Stack();
            stack.Push(b);
            stack.Push(a);
            ExecuteInstruction(new Instruction((OpCode)op), stack);
            return stack.PopInt();
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
            Assert.That(() => ExecuteInstruction(new Instruction((OpCode) op)),
                Throws.TypeOf<NotImplementedException>());
        }

        private static void ExecuteInstruction(IInstruction instruction, Stack stack = null)
        {
            if (stack is null) stack = new Stack();
            var local = new Local(stack);
            var code = ImmutableArray.Create(instruction);
            VirtualMachine.ExecuteCode(code, local);
        }
    }
}