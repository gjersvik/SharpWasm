using System.Collections.Immutable;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Parse.Sections;
using GlobalType = SharpWasm.Core.Types.GlobalType;
using TableType = SharpWasm.Core.Types.TableType;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Tests.Helpers
{
    internal static class TestValues
    {
        private const string LimitsHex = "010102";
        public static readonly Limits Limits = new Limits(1,2);
        public const string TableTypeHex = "70" + LimitsHex;
        public static readonly TableType TableType = new TableType(Limits);
        public const string GlobalTypeHex = "7f00";
        public static readonly GlobalType GlobalType = new GlobalType(ValueType.I32,false);
        public const string MemoryTypeHex = LimitsHex;
        public static readonly MemoryType MemoryType = new MemoryType(Limits);
        public static readonly ImmutableArray<IInstruction> InitExpr =
            ImmutableArray.Create<IInstruction>(Instruction.I32Const(42), Instruction.End);
        public const string InitExprHex = "412A0B";

        public static readonly Global Global = new Global(GlobalType, InitExpr);
        public const string GlobalHex = GlobalTypeHex + InitExprHex;

        public const string TestStringHex = "0474657374";

        public static readonly ExportEntry ExportEntry = new ExportEntry("test", ExternalKind.Function, 02);
        public const string ExportEntryHex = TestStringHex + "0002";

        public static readonly ElementSegment ElementSegment = new ElementSegment(InitExpr, new uint[] { 1, 2, 42 });
        public const string ElementSegmentHex = "00" + InitExprHex + "0301022A";

        public static readonly LocalEntry LocalEntry = new LocalEntry(10, ValueType.I32);
        public const string LocalEntryHex = "0A7F";

        public static readonly FunctionBody FunctionBody = new FunctionBody(new[] { LocalEntry, LocalEntry },
            new IInstruction[] { Instruction.I32Const(42), Instruction.End });

        public static readonly DataSegment DataSegment = new DataSegment(InitExpr, new byte[] { 1, 2, 42 });
        public const string DataSegmentHex = "00" + InitExprHex + "0301022A";
    }
}
