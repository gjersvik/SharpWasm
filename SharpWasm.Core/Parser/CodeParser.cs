
using System.IO;
using SharpWasm.Core.Code;

namespace SharpWasm.Core.Parser
{
    internal static class CodeParser
    {
        public static BrTable ToBrTable(BinaryReader reader)
        {
            var targetTable = Values.ToVector(reader, Values.ToUInt);
            var defaultTarget = Values.ToUInt(reader);
            return new BrTable(targetTable, defaultTarget);
        }
    }
}
