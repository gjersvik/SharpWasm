using System.Collections.Generic;
using System.Collections.Immutable;
using SharpWasm.Core.Code;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal
{
    internal abstract class AFunction
    {
        public uint Id { get; }
        public uint TypeId { get; }
        public ImmutableArray<ValueType> Param { get; }
        public ValueType? Return { get; }
        
        public bool Import { get; }

        protected AFunction(uint id, FunctionType type, bool import, uint typeId)
        {
            Id = id;
            Import = import;
            TypeId = typeId;
            Param = type.Parameters;
            if (type.Returns.IsEmpty)
            {
                Return = null;
            }
            else
            {
                Return = type.Returns[0];
            }
        }
    }
    internal class Function: AFunction
    {
        public readonly ImmutableArray<IInstruction> Body;

        public Function(uint id, IEnumerable<IInstruction> body, FunctionType type, uint typeId): base(id,type,false, typeId)
        {
            Body = body.ToImmutableArray();
        }
    }
    internal class ImportFunction : AFunction
    {
        public readonly string Module;
        public readonly string Field;

        public ImportFunction(uint id, FunctionType type, string module, string field, uint typeId) : base(id, type, true, typeId)
        {
            Module = module;
            Field = field;
        }
    }
}
