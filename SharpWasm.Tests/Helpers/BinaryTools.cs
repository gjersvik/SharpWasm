using System;
using System.IO;

namespace SharpWasm.Tests.Helpers
{
    internal static class BinaryTools
    {
        public static byte[] HexToBytes(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static BinaryReader HexToReader(string hex)
        {
            var buffer = HexToBytes(hex);
            return new BinaryReader(new MemoryStream(buffer));
        }
    }
}
