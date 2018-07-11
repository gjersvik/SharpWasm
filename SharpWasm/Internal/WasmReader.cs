using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpWasm.Internal.Parse;

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
            var id = (SectionId) ReadVarUInt7();
            var len = ReadVarUInt32();
            var payload = ReadBytes(len);
            switch (id)
            {
                case SectionId.Custom:
                    return new CustomSection(payload);
                case SectionId.Type:
                    return new Types(payload);
                case SectionId.Import:
                    return new Imports(payload);
                case SectionId.Function:
                    return new FunctionSelection(payload);
                case SectionId.Table:
                    return new Table(payload);
                case SectionId.Memory:
                    return new Section(id);
                case SectionId.Global:
                    return new Section(id);
                case SectionId.Export:
                    return new Exports(payload);
                case SectionId.Start:
                    return new Section(id);
                case SectionId.Element:
                    return new Element(payload);
                case SectionId.Code:
                    return new Code(payload);
                case SectionId.Data:
                    return new Data(payload);
                default:
                    return new Section(id);
            }
        }

        public IEnumerable<Type> ReadTypes()
        {
            var count = ReadVarUInt32();
            var types = new Type[count];

            for (var i = 0; i < count; i += 1)
            {
                types[i] = ReadType();
            }

            return types;
        }

        public Type ReadType()
        {
            ReadVarInt7();
            var count = ReadVarUInt32();
            var param = new DataTypes[count];
            for (var i = 0; i < count; i += 1)
            {
                param[i] = (DataTypes) ReadVarInt7();
            }

            return ReadVarUInt1() ? new Type(param, (DataTypes) ReadVarInt7()) : new Type(param);
        }

        public IEnumerable<uint> ReadFunction()
        {
            var count = ReadVarUInt32();
            var types = new uint[count];

            for (var i = 0; i < count; i += 1)
            {
                types[i] = ReadVarUInt32();
            }

            return types;
        }

        public AImport ReadImport()
        {
            var module = ReadString();
            var field = ReadString();
            var kind = (ImportExportKind) ReadUInt8();

            switch (kind)
            {
                case ImportExportKind.Function:
                    var type = ReadVarUInt32();
                    return new FunctionImport(module, field, type);
                case ImportExportKind.Table:
                    throw new NotImplementedException();
                case ImportExportKind.Memory:
                    var limits = new ResizableLimits(_reader);
                    return new MemoryImport(module, field, limits.Initial, limits.Maximum ?? 0);
                case ImportExportKind.Global:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public IEnumerable<AImport> ReadImports()
        {
            var count = ReadVarUInt32();
            var types = new AImport[count];

            for (var i = 0; i < count; i += 1)
            {
                types[i] = ReadImport();
            }

            return types;
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
        public sbyte ReadVarInt7() => new VarIntSigned(_reader).SByte;
        public int ReadVarInt32() => new VarIntSigned(_reader).Int;
        private byte[] ReadBytes(uint len) => _reader.ReadBytes((int) len);

        public string ReadString()
        {
            var count = ReadVarUInt32();
            return Encoding.UTF8.GetString(ReadBytes(count));
        }

        public byte[] ReadRest()
        {
            using (var ms = new MemoryStream())
            {
                _reader.BaseStream.CopyTo(ms);
                return ms.ToArray();
            }
        }


        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}