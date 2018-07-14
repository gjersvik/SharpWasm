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
    }
}
