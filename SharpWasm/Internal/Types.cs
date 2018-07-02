using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal class Types: ISection
    {
        public static Types Empty { get; } = new Types();

        public SectionId Id { get; } = SectionId.Type;

        public ImmutableArray<Type> TypeList;
        public Types(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                TypeList = reader.ReadTypes().ToImmutableArray();
            }
        }

        private Types()
        {
            TypeList = ImmutableArray<Type>.Empty;
        }

    }

    internal class Type
    {
        public readonly ImmutableArray<DataTypes> Params;
        public readonly DataTypes Return;

        public Type(IEnumerable<DataTypes> param, DataTypes returnType = DataTypes.EmptyBlock)
        {
            Params = param.ToImmutableArray();
            Return = returnType;
        }
    }
}
