using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpWasm.Internal.Parse;
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
                        return new Section(id);
                    case SectionCode.Global:
                        return new Section(id);
                    case SectionCode.Export:
                        return new Exports(payload);
                    case SectionCode.Start:
                        return new Section(id);
                    case SectionCode.Element:
                        return new Element(payload);
                    case SectionCode.Code:
                        return new Code(payload);
                    case SectionCode.Data:
                        return new Data(payload);
                    default:
                        return new Section(id);
                }
            }
        }

        public IEnumerable<Export> ReadExports()
        {
            var count = ReadVarUInt32();
            var exports = new Export[count];

            for (var i = 0; i < count; i += 1)
            {
                exports[i] = ReadExport();
            }

            return exports;
        }

        public Export ReadExport()
        {
            var name = ReadString();
            var kind = (ImportExportKind) ReadUInt8();
            var index = ReadVarUInt32();
            return new Export(name, kind, index);
        }

        public IEnumerable<ElementSegment> ReadElementSegments()
        {
            var count = ReadVarUInt32();
            var exports = new ElementSegment[count];

            for (var i = 0; i < count; i += 1)
            {
                exports[i] = ReadElementSegment();
            }

            return exports;
        }

        public ElementSegment ReadElementSegment()
        {
            var index = ReadVarUInt32();
            if (index != 0) throw new NotImplementedException();
            var offset = ReadInitExpr().Offset;
            var count = ReadVarUInt32();
            var elements = new uint[count];

            for (var i = 0; i < count; i += 1)
            {
                elements[i] = ReadVarUInt32();
            }

            return new ElementSegment(offset, elements);
        }

        public IEnumerable<FunctionBody> ReadFunctionBodies()
        {
            var count = ReadVarUInt32();
            var exports = new FunctionBody[count];

            for (var i = 0; i < count; i += 1)
            {
                exports[i] = ReadFunctionBody();
            }

            return exports;
        }

        public FunctionBody ReadFunctionBody()
        {
            var bodySize = ReadVarUInt32();
            var localCount = new VarIntUnsigned(_reader);
            bodySize -= localCount.Count;

            if (localCount.UInt != 0) throw new Exception("Locals not supported");
            return new FunctionBody(ReadBytes(bodySize));
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
            if (ReadUInt8() != (int)Instructions.I32Const) throw new NotImplementedException();
            var offset = ReadVarInt32();
            if (ReadUInt8() != (int)Instructions.End) throw new NotImplementedException();
            return new InitExpr(offset);
        }

        public byte ReadUInt8() => _reader.ReadByte();
        public uint ReadUInt32() => _reader.ReadUInt32();
        public bool ReadVarUInt1() => new VarIntUnsigned(_reader).Bool;
        public byte ReadVarUInt7() => new VarIntUnsigned(_reader).Byte;
        public uint ReadVarUInt32() => new VarIntUnsigned(_reader).UInt;
        public int ReadVarInt32() => new VarIntSigned(_reader).Int;
        private byte[] ReadBytes(uint len) => _reader.ReadBytes((int) len);

        public string ReadString()
        {
            var count = ReadVarUInt32();
            return Encoding.UTF8.GetString(ReadBytes(count));
        }


        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}