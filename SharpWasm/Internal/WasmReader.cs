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

        public WasmReader(Stream stream)
        {
            _reader = new BinaryReader(stream, Encoding.UTF8);
        }
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
            if (id == SectionId.Custom)
            {
                return ReadCustomSection();
            }

            var len = ReadVarUInt32();
            return new Section(id, ReadBytes(len));
        }


        private CustomSection ReadCustomSection()
        {
            var payloadLen = ReadVarUInt32();
            var nameLen = ReadVarUInt32();
            payloadLen -= nameLen;
            payloadLen -= _lastCount;
            var name = ReadString(nameLen);
            return new CustomSection(name, ReadBytes(payloadLen));
        }

        public byte ReadUInt8() => _reader.ReadByte();
        public ushort ReadUInt16() => _reader.ReadUInt16();
        public uint ReadUInt32() => _reader.ReadUInt32();
        public byte ReadVarUInt7() => Convert.ToByte(ReadVarUInt64());
        public uint ReadVarUInt32() => Convert.ToUInt32(ReadVarUInt64());
        private byte[] ReadBytes(uint len) => _reader.ReadBytes((int)len);
        private string ReadString(uint count)
        {
            return Encoding.UTF8.GetString(ReadBytes(count));
        }


        public void Dispose()
        {
            _reader.Dispose();
        }
        
        private  long ReadVarInt64()
        {
            long value = 0;
            var shift = 0;
            _lastCount = 0;
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
            ulong value = 0;
            var shift = 0;

            _lastCount = 0;
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
