using System;
using System.Collections.Generic;

namespace SharpWasm
{
    public class WebAssemblyImports
    {
        private Dictionary<string, Func<int[], int>> _funcs = new Dictionary<string, Func<int[], int>>();

        public void Add(string module, string function, Func<int[],int> func)
        {
            _funcs[module + "," + function] = func;
        }
    }
}