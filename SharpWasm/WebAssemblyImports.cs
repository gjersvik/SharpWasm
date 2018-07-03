using System;
using System.Collections.Generic;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyImports
    {
        private Dictionary<string, Func<int[], int>> _funcs = new Dictionary<string, Func<int[], int>>();

        public void Add(string module, string function, Func<int[],int> func)
        {
            _funcs[module + "." + function] = func;
        }

        internal int Call(ImportFunction importFunc, int[] param)
        {
            return _funcs[importFunc.Module + "." + importFunc.Field](param);
        }
    }
}