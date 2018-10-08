using System.Collections.Immutable;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

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

        public static readonly Export Export = new Export("test", ExternalKind.Function, 02);
        public const string ExportHex = TestStringHex + "0002";

        public static readonly Element Element = new Element(0,InitExpr, new uint[] { 1, 2, 42 });
        public const string ElementHex = "00" + InitExprHex + "0301022A";

        public static readonly ImmutableArray<ValueType> Local = ImmutableArray.Create(ValueType.I32, ValueType.I64,
            ValueType.I64, ValueType.F32, ValueType.F32, ValueType.F32, ValueType.F64, ValueType.F64, ValueType.F64,
            ValueType.F64);
        public const string LocalHex = "04017F027E037D047C";

        public static readonly Data Data = new Data(0,InitExpr, new byte[] { 1, 2, 42 });
        public const string DataHex = "00" + InitExprHex + "0301022A";
    }
}
