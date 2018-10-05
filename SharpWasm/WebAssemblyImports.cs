using System;
using System.Collections.Generic;
using SharpWasm.Core.Segments;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyImports
    {
        private readonly Dictionary<string, Func<WebAssemblyInstance, int[], int>> _functions = new Dictionary<string, Func<WebAssemblyInstance,int[], int>>();
        private readonly Dictionary<string, WebAssemblyMemory> _memories = new Dictionary<string, WebAssemblyMemory>();

        public void Add(string module, string function, Func<int[],int> func)
        {
            Add(module,function, (i,a) => func(a));
        }
        public void Add(string module, string function, Func<WebAssemblyInstance, int[], int> func)
        {
            _functions[module + "." + function] = func;
        }

        public void Add(string module, string function, WebAssemblyMemory memory)
        {
            _memories[module + "." + function] = memory;
        }

        internal int Call(WebAssemblyInstance instance, ImportFunction importFunc, int[] param)
        {
            return _functions[importFunc.Module + "." + importFunc.Field](instance,param);
        }

        internal WebAssemblyMemory GetMemory(Import import)
        {
            return _memories[import.Module + "." + import.Name];
        }
    }
}