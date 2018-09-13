using System;
using System.Collections.Generic;
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
    }
}
