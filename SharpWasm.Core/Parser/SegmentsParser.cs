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

        public static Global ToGlobal(BinaryReader reader)
        {
            var type = TypeParser.ToGlobalType(reader);
            var initExpr = CodeParser.ToInitExpr(reader);
            return new Global(type, initExpr);
        }

        public static Export ToExport(BinaryReader reader)
        {
            var name = Values.ToName(reader);
            var externalKind = TypeParser.ToExternalKind(reader);
            var index = Values.ToUInt(reader);
            return new Export(name, externalKind, index);
        }

        public static Element ToElement(BinaryReader reader)
        {
            var index = Values.ToUInt(reader);
            var offset = CodeParser.ToInitExpr(reader);
            var elements = Values.ToVector(reader, Values.ToUInt);
            return new Element(index, offset, elements);
        }
    }
}
