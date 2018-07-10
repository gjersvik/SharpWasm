using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal class Data : ISection
    {
        public static readonly Data Empty = new Data(ImmutableArray<DataSegment>.Empty);

        public SectionId Id { get; } = SectionId.Data;
        public readonly ImmutableArray<DataSegment> DataSegments;

        public Data(byte[] payload) : this(FromPayload(payload))
        {
        }

        private Data(IEnumerable<DataSegment> list)
        {
            DataSegments = list.ToImmutableArray();
        }

        private static IEnumerable<DataSegment> FromPayload(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                return reader.ReadData();
            }
        }
    }

    internal class DataSegment
    {
        public readonly int Offset;
        public readonly byte[] Data;

        public DataSegment(int offset, byte[] data)
        {
            Offset = offset;
            Data = data;
        }
    }
}
