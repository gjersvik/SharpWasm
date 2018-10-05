using System;
using System.IO;
using SharpWasm.Core.Segments;

namespace SharpWasm.Core.Parser
{
    internal static class SegmentsParser
    {
        public static Import ToImport(BinaryReader reader)
        {
            var module = Values.ToName(reader);
            var name = Values.ToName(reader);
            var type = TypeParser.ToExternalKind(reader);
            switch (type)
            {
                case Types.ExternalKind.Function:
                    return new Import(module, name, Values.ToUInt(reader));
                case Types.ExternalKind.Table:
                    return new Import(module, name, TypeParser.ToTableType(reader));
                case Types.ExternalKind.Memory:
                    return new Import(module, name, TypeParser.ToMemoryType(reader));
                case Types.ExternalKind.Global:
                    return new Import(module, name, TypeParser.ToGlobalType(reader));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
