using System;
using System.Runtime.Serialization;

namespace SharpWasm
{
    [Serializable]
    internal class WebAssemblyCompileError : Exception
    {
        public WebAssemblyCompileError()
        {
        }

        public WebAssemblyCompileError(string message) : base(message)
        {
        }

        public WebAssemblyCompileError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebAssemblyCompileError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}