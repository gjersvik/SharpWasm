using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpWasm.Core.Types
{
    public struct FunctionType
    {
        public readonly ImmutableArray<ValueType> Parameters;
        public readonly ImmutableArray<ValueType> Returns;

        public FunctionType(IEnumerable<ValueType> parameters, IEnumerable<ValueType> returns = null)
        {
            Parameters = parameters.ToImmutableArray();
            Returns = returns?.ToImmutableArray() ?? ImmutableArray<ValueType>.Empty;
        }
    }
}