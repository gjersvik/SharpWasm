using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal class Function
    {
        public readonly uint Id;
        public readonly byte[] Body;
        public readonly ImmutableArray<DataTypes> Param;
        public readonly DataTypes Return;

        public Function(uint id, byte[] body, Type type)
        {
            Id = id;
            Body = body;
            Param = type.Params;
            Return = type.Return;
        }
    }
}
