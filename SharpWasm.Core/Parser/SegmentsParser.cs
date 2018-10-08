using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;
using ValueType = SharpWasm.Core.Types.ValueType;

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

        public static CodeSection ToCodeSection(BinaryReader reader)
        {
            var bodySize = Values.ToUInt(reader);
            var locals = ToLocals(reader, out var length);
            var codeLength = bodySize - length;

            var builder = ImmutableArray.CreateBuilder<IInstruction>();
            using (var codeReader = Tools.ToReader(reader, codeLength))
            {
                while (codeReader.BaseStream.Position != codeReader.BaseStream.Length)
                {
                    builder.Add(CodeParser.ToInstruction(codeReader));
                }
            }

            return new CodeSection(locals, builder.ToImmutable());
        }

        public static ImmutableArray<ValueType> ToLocals(BinaryReader reader, out uint length)
        {
            var locals = new List<ValueType>();
            var localsCount = Values.UnsignedVar(reader, out var subLength);
            length = subLength;
            for (var i = 0; i < localsCount; i += 1)
            {
                var count = Values.UnsignedVar(reader, out subLength);
                length += subLength;
                var type = TypeParser.ToValueType(reader, out subLength);
                length += subLength;
                locals.AddRange(Enumerable.Repeat(type, (int)count));
            }
            return locals.ToImmutableArray();
        }
    }
}
