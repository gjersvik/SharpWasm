using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SharpWasm.Core.Parser;
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
            return ModuleParser.ToModule(module);
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

        internal readonly ImmutableDictionary<string, ImmutableArray<byte>> Custom = ImmutableDictionary<string, ImmutableArray<byte>>.Empty;
        internal readonly ImmutableArray<FunctionType> Types = ImmutableArray<FunctionType>.Empty;
        internal readonly ImmutableArray<Function> Funcs = ImmutableArray<Function>.Empty;
        internal readonly ImmutableArray<TableType> Tables = ImmutableArray<TableType>.Empty;
        internal readonly ImmutableArray<MemoryType> Mems = ImmutableArray<MemoryType>.Empty;
        internal readonly ImmutableArray<Global> Globals = ImmutableArray<Global>.Empty;
        internal readonly ImmutableArray<Element> Elem = ImmutableArray<Element>.Empty;
        internal readonly ImmutableArray<Data> Data = ImmutableArray<Data>.Empty;
        internal readonly uint? Start;
        internal readonly ImmutableArray<Import> ImportsArray = ImmutableArray<Import>.Empty;
        internal readonly ImmutableArray<Export> ExportsArray = ImmutableArray<Export>.Empty;


        public Module()
        {

        }

        internal Module(ImmutableDictionary<string, ImmutableArray<byte>> custom, ImmutableArray<FunctionType> types, ImmutableArray<Function> funcs, ImmutableArray<TableType> tables, ImmutableArray<MemoryType> mems, ImmutableArray<Global> globals, ImmutableArray<Element> elem, ImmutableArray<Data> data, uint? start, ImmutableArray<Import> importsArray, ImmutableArray<Export> exportsArray)
        {
            Custom = custom;
            Types = types;
            Funcs = funcs;
            Tables = tables;
            Mems = mems;
            Globals = globals;
            Elem = elem;
            Data = data;
            Start = start;
            ImportsArray = importsArray;
            ExportsArray = exportsArray;
        }
    }
}
