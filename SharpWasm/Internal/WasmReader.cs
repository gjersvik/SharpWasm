using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpWasm.Internal
{
    internal class WasmReader: IDisposable
    {
        private readonly BinaryReader _reader;
        private uint _lastCount;

        public WasmReader(byte[] buffer)
        {
            _reader = new BinaryReader(new MemoryStream(buffer), Encoding.UTF8);
        }

        public Module ReadModule()
        {
            var header = ReadHeader();
            if(!header.IsValid()) throw new WebAssemblyCompileError();
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
            var id = (SectionId)ReadVarUInt7();
            var len = ReadVarUInt32();
            var payload = ReadBytes(len);
            switch (id)
            {
                case SectionId.Custom:
                    return new CustomSection(payload);
                case SectionId.Type:
                    return new Types(payload);
                case SectionId.Import:
                    return new Section(id);
                case SectionId.Function:
                    return new FunctionSelection(payload);
                case SectionId.Table:
                    return new Section(id);
                case SectionId.Memory:
                    return new Section(id);
                case SectionId.Global:
                    return new Section(id);
                case SectionId.Export:
                    return new Exports(payload);
                case SectionId.Start:
                    return new Section(id);
                case SectionId.Element:
                    return new Section(id);
                case SectionId.Code:
                    return new Code(payload);
                case SectionId.Data:
                    return new Section(id);
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

        public Type ReadType()
        {
            ReadVarInt7();
            var count = ReadVarUInt32();
            var param = new DataTypes[count];
            for (var i = 0; i < count; i += 1)
            {
                param[i] = (DataTypes) ReadVarInt7();
            }

            return ReadVarUInt1() ? new Type(param, (DataTypes)ReadVarInt7()) : new Type(param);
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
            return new Export(name,kind,index);
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
            var localCount = ReadVarUInt32();
            bodySize -= _lastCount;

            if (localCount != 0) throw new Exception("Locals not supported");
            return new FunctionBody(ReadBytes(bodySize));
        }
        public byte ReadUInt8() => _reader.ReadByte();
        public ushort ReadUInt16() => _reader.ReadUInt16();
        public uint ReadUInt32() => _reader.ReadUInt32();
        public bool ReadVarUInt1() => Convert.ToBoolean(ReadVarUInt64());
        public byte ReadVarUInt7() => Convert.ToByte(ReadVarUInt64());
        public uint ReadVarUInt32() => Convert.ToUInt32(ReadVarUInt64());
        public sbyte ReadVarInt7() => Convert.ToSByte(ReadVarInt64());
        public int ReadVarInt32() => Convert.ToInt32(ReadVarInt64());
        private byte[] ReadBytes(uint len) => _reader.ReadBytes((int)len);
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
        
        private  long ReadVarInt64()
        {
            _lastCount = 0;
            long value = 0;
            var shift = 0;
            byte bt;
            do
            {
                var ibt = _reader.ReadByte();

                bt = ibt;

                value |= ((long)(bt & 0x7f) << shift);
                shift += 7;
                _lastCount += 1;
            }
            while (bt >= 128);

            // Sign extend negative numbers.
            if ((bt & 0x40) != 0)
                value |= (-1L) << shift;

            return value;
        }
        private ulong ReadVarUInt64()
        {
            _lastCount = 0;
            ulong value = 0;
            var shift = 0;

            while (true)
            {
                var bt = _reader.ReadByte();

                value += (ulong)(bt & 0x7f) << shift;
                _lastCount += 1;
                if (bt < 128) break;

                shift += 7;
            }
            return value;
        }
    }
}
