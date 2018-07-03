using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal abstract class AFunction
    {
        public uint Id { get; }
        public ImmutableArray<DataTypes> Param { get; }
        public DataTypes Return { get; }
        
        public bool Import { get; }

        public AFunction(uint id, Type type, bool import)
        {
            Id = id;
            Import = import;
            Param = type.Params;
            Return = type.Return;
        }
    }
    internal class Function: AFunction
    {
        public readonly byte[] Body;

        public Function(uint id, byte[] body, Type type): base(id,type,false)
        {
            Body = body;
        }
    }
    internal class ImportFunction : AFunction
    {
        public readonly string Module;
        public readonly string Field;

        public ImportFunction(uint id, Type type, string module, string field) : base(id, type, true)
        {
            Module = module;
            Field = field;
        }
    }
}
