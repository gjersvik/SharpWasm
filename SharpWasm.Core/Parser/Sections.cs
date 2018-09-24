using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Core.Parser
{
    internal class Sections
    {
        public ImmutableDictionary<string, ImmutableArray<byte>> Custom { get; private set; } = ImmutableDictionary<string, ImmutableArray<byte>>.Empty;

        public void ParseCustom(BinaryReader reader)
        {
            var name = Values.ToName(reader);
            using (var ms = new MemoryStream())
            {
                reader.BaseStream.CopyTo(ms);
                Custom = Custom.Add(name, ms.ToArray().ToImmutableArray());
            }
        }
    }
}
