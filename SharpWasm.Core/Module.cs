using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;
// ReSharper disable  MemberCanBeMadeStatic.Global
// ReSharper disable UnusedParameter.Global

namespace SharpWasm.Core
{
    public class Module
    {
        public static Module Decode(byte[] module)
        {
            return new Module();
        }
        
        public Error Validate()
        {
            return null;
        }

        public IEnumerable<Tuple<string, string, ExternalType>> Imports()
        {
           return new Tuple<string, string, ExternalType>[0];
        }

        public IEnumerable<Tuple<string, ExternalType>> Exports()
        {
            return new Tuple<string, ExternalType>[0];
        }

        internal readonly ImmutableArray<FunctionType> Types = ImmutableArray<FunctionType>.Empty;
        internal readonly ImmutableArray<Function> Funcs = ImmutableArray<Function>.Empty;
        internal readonly ImmutableArray<TableType> Tables = ImmutableArray<TableType>.Empty;
        internal readonly ImmutableArray<MemoryType> Mems = ImmutableArray<MemoryType>.Empty;
        internal readonly ImmutableArray<Global> Globals = ImmutableArray<Global>.Empty;
        internal readonly ImmutableArray<Element> Elem = ImmutableArray<Element>.Empty;
        internal readonly ImmutableArray<Data> Data = ImmutableArray<Data>.Empty;
        internal readonly uint? Start = null;
        internal readonly ImmutableArray<Import> ImportsArray = ImmutableArray<Import>.Empty;
        internal readonly ImmutableArray<Export> ExportsArray = ImmutableArray<Export>.Empty;
    }
}
