using System.Collections.Immutable;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal
{
    internal abstract class AFunction
    {
        public uint Id { get; }
        public uint TypeId { get; }
        public ImmutableArray<ValueType> Param { get; }
        public ValueType? Return { get; }
        
        public bool Import { get; }

        public AFunction(uint id, FuncType type, bool import, uint typeId)
        {
            Id = id;
            Import = import;
            TypeId = typeId;
            Param = type.ParamTypes;
            Return = type.ReturnType;
        }
    }
    internal class Function: AFunction
    {
        public readonly byte[] Body;

        public Function(uint id, byte[] body, FuncType type, uint typeId): base(id,type,false, typeId)
        {
            Body = body;
        }
    }
    internal class ImportFunction : AFunction
    {
        public readonly string Module;
        public readonly string Field;

        public ImportFunction(uint id, FuncType type, string module, string field, uint typeId) : base(id, type, true, typeId)
        {
            Module = module;
            Field = field;
        }
    }
}
