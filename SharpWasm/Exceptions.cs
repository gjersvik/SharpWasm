using System;

namespace SharpWasm
{
    public class WebAssemblyStackOverflowException : Exception
    {
        public WebAssemblyStackOverflowException(): base("StackOverflow")
        {
        }
    }
    public class WebAssemblyRuntimeException : Exception
    {
        public WebAssemblyRuntimeException(string message) : base(message)
        {
        }
    }
}
