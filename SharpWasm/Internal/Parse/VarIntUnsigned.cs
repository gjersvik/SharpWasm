﻿using System;
using System.IO;

namespace SharpWasm.Internal.Parse
{
    internal class VarIntUnsigned
    {
        public static uint ToUInt(BinaryReader reader) => new VarIntUnsigned(reader).UInt;
        public static byte ToByte(BinaryReader reader) => new VarIntUnsigned(reader).Byte;
        public static bool ToBool(BinaryReader reader) => new VarIntUnsigned(reader).Bool;

        public readonly uint UInt;
        public byte Byte => Convert.ToByte(UInt);
        public bool Bool => Convert.ToBoolean(UInt);

        public readonly byte Count;

        public VarIntUnsigned(BinaryReader reader)
        {
            Count = 0;
            UInt = 0;
            var shift = 0;

            while (true)
            {
                var bt = reader.ReadByte();

                UInt += (uint)(bt & 0x7f) << shift;
                Count += 1;
                if (bt < 128) break;

                shift += 7;
            }
        }
    }
}
