﻿using System.Collections.Immutable;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;
using ValueType = SharpWasm.Internal.Parse.Types.ValueType;

namespace SharpWasm.Tests.Helpers
{
    internal static class TestValues
    {
        private const string ResizableLimitsHex = "010102";
        public static readonly ResizableLimits ResizableLimits = new ResizableLimits(1,2);
        public const string TableTypeHex = "70" + ResizableLimitsHex;
        public static readonly TableType TableType = new TableType(ResizableLimits);
        public const string GlobalTypeHex = "7f00";
        public static readonly GlobalType GlobalType = new GlobalType(ValueType.I32,false);
        public const string MemoryTypeHex = ResizableLimitsHex;
        public static readonly MemoryType MemoryType = new MemoryType(ResizableLimits);
        public static readonly InitExpr InitExpr = new InitExpr(new IInstruction[]{Instruction.I32Const(42), Instruction.End});
        public const string InitExprHex = "412A0B";

        public static readonly GlobalEntry GlobalEntry = new GlobalEntry(GlobalType, InitExpr);
        public const string GlobalEntryHex = GlobalTypeHex + InitExprHex;

        public const string TestStringHex = "0474657374";

        public static readonly ExportEntry ExportEntry = new ExportEntry("test", ExternalKind.Function, 02);
        public const string ExportEntryHex = TestStringHex + "0002";

        public static readonly ElementSegment ElementSegment = new ElementSegment(InitExpr, new uint[] { 1, 2, 42 });
        public const string ElementSegmentHex = "00" + InitExprHex + "0301022A";

        public static readonly LocalEntry LocalEntry = new LocalEntry(10, ValueType.I32);
        public const string LocalEntryHex = "0A7F";

        public static readonly FunctionBody FunctionBody = new FunctionBody(new[] { LocalEntry, LocalEntry },
            BinaryTools.HexToBytes(InitExprHex).ToImmutableArray());

        public static readonly DataSegment DataSegment = new DataSegment(InitExpr, new byte[] { 1, 2, 42 });
        public const string DataSegmentHex = "00" + InitExprHex + "0301022A";
    }
}
