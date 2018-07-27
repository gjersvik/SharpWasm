using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Parse.Sections;
using FunctionSection = SharpWasm.Internal.Parse.Sections.Function;

namespace SharpWasm.Internal
{
    internal class WasmReader : IDisposable
    {
        private readonly BinaryReader _reader;

        public WasmReader(byte[] buffer)
        {
            _reader = new BinaryReader(new MemoryStream(buffer), Encoding.UTF8);
        }

        public Module ReadModule()
        {
            var header = ReadHeader();
            if (!header.IsValid()) throw new WebAssemblyCompileError();
            var sections = new List<ISection>();
            while (_reader.BaseStream.Position != _reader.BaseStream.Length)
            {
                sections.Add(ReadSection());
            }

            return new Module(header, sections);
        }

        public Header ReadHeader()
        {
            var mn = ReadUInt32();
            var v = ReadUInt32();
            return new Header(mn, v);
        }

        public ISection ReadSection()
        {
            var id = (SectionCode) ReadVarUInt7();
            var len = ReadVarUInt32();
            var payload = ReadBytes(len);
            using (var reader = ParseTools.FromBytes(payload))
            {
                switch (id)
                {
                    case SectionCode.Custom:
                        return new Custom(reader);
                    case SectionCode.Type:
                        return new Parse.Sections.Type(reader);
                    case SectionCode.Import:
                        return new Import(reader);
                    case SectionCode.Function:
                        return new FunctionSection(reader);
                    case SectionCode.Table:
                        return new Table(reader);
                    case SectionCode.Memory:
                        return new Memory(reader);
                    case SectionCode.Global:
                        return new Global(reader);
                    case SectionCode.Export:
                        return new Export(reader);
                    case SectionCode.Start:
                        return new Start(reader);
                    case SectionCode.Element:
                        return new Element(reader);
                    case SectionCode.Code:
                        return new Code(reader);
                    case SectionCode.Data:
                        return new Data(payload);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public IEnumerable<DataSegment> ReadData()
        {
            var count = ReadVarUInt32();
            var exports = new DataSegment[count];

            for (var i = 0; i < count; i += 1)
            {
                exports[i] = ReadDataSegment();
            }

            return exports;
        }

        public DataSegment ReadDataSegment()
        {
            var index = ReadVarUInt32();
            if (index != 0) throw new NotImplementedException();
            var offset = ReadInitExpr().Offset;
            var length = ReadVarUInt32();
            var data = ReadBytes(length);

            return new DataSegment(offset, data);
        }

        public InitExpr ReadInitExpr()
        {
            if (ReadUInt8() != (int)OpCode.I32Const) throw new NotImplementedException();
            var offset = ReadVarInt32();
            if (ReadUInt8() != (int)OpCode.End) throw new NotImplementedException();
            return new InitExpr(offset);
        }

        public byte ReadUInt8() => _reader.ReadByte();
        public uint ReadUInt32() => _reader.ReadUInt32();
        public bool ReadVarUInt1() => new VarIntUnsigned(_reader).Bool;
        public byte ReadVarUInt7() => new VarIntUnsigned(_reader).Byte;
        public uint ReadVarUInt32() => new VarIntUnsigned(_reader).UInt;
        public int ReadVarInt32() => new VarIntSigned(_reader).Int;
        private byte[] ReadBytes(uint len) => _reader.ReadBytes((int) len);

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}