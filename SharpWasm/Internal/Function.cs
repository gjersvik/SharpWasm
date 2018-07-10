using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal abstract class AFunction
    {
        public uint Id { get; }
        public uint TypeId { get; }
        public ImmutableArray<DataTypes> Param { get; }
        public DataTypes Return { get; }
        
        public bool Import { get; }

        public AFunction(uint id, Type type, bool import, uint typeId)
        {
            Id = id;
            Import = import;
            TypeId = typeId;
            Param = type.Params;
            Return = type.Return;
        }
    }
    internal class Function: AFunction
    {
        public readonly byte[] Body;

        public Function(uint id, byte[] body, Type type, uint typeId): base(id,type,false, typeId)
        {
            Body = body;
        }
    }
    internal class ImportFunction : AFunction
    {
        public readonly string Module;
        public readonly string Field;

        public ImportFunction(uint id, Type type, string module, string field, uint typeId) : base(id, type, true, typeId)
        {
            Module = module;
            Field = field;
        }
    }
}
