﻿using System;
using System.Collections.Immutable;
using System.IO;
using System.Text;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Internal.Parse
{
    internal static class ParseTools
    {
        public static ImmutableArray<T> ToArray<T>(BinaryReader reader, uint count, Func<BinaryReader, T> parser)
        {
            var builder = ImmutableArray.CreateBuilder<T>((int) count);

            for (var i = 0; i < count; i++)
            {
                builder.Add(parser(reader));
            }

            return builder.MoveToImmutable();
        }

        public static ImmutableArray<T> ToVector<T>(BinaryReader reader, Func<BinaryReader, T> parser)
        {
            var count = Values.ToUInt(reader);
            return ToArray(reader, count, parser);
        }

        public static BinaryReader FromBytes(byte[] bytes)
        {
            return new BinaryReader(new MemoryStream(bytes));
        }

        public static ExternalKind ToExternalKind(BinaryReader reader)
        {
            return (ExternalKind)reader.ReadByte();
        }

        public static string ToUtf8(BinaryReader reader, uint length)
        {
            return Encoding.UTF8.GetString(reader.ReadBytes((int)length));
        }

        public static ImmutableArray<byte> ToBytes(BinaryReader reader)
        {
            using (var ms = new MemoryStream())
            {
                reader.BaseStream.CopyTo(ms);
                return ms.ToArray().ToImmutableArray();
            }
        }

        public static ImmutableArray<byte> ToBytes(BinaryReader reader, uint count)
        {
            return reader.ReadBytes((int) count).ToImmutableArray();
        }

        public static BinaryReader ToReader(BinaryReader reader, uint length)
        {
            return FromBytes(reader.ReadBytes((int) length));
        }

        public static ValueType ToValueType(BinaryReader reader) => (ValueType)Values.ToSByte(reader);
        public static BlockType ToBlockType(BinaryReader reader) => (BlockType)Values.ToSByte(reader);
        public static ElemType ToElemType(BinaryReader reader) => (ElemType)Values.ToSByte(reader);
        public static SectionCode ToSectionCode(BinaryReader reader) => (SectionCode)Values.ToByte(reader);
    }
}