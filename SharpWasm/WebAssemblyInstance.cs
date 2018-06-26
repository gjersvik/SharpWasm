namespace SharpWasm
{
    public class WebAssemblyInstance
    {
        public readonly object Exports;

        internal WebAssemblyInstance(object exports)
        {
            Exports = exports;
        }
    }
}