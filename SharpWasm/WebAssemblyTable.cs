using System;

namespace SharpWasm
{
    public enum TableKind
    {
        AnyFunc
    }

    public class WebAssemblyTable
    {
        public WebAssemblyTable(TableKind element, ulong initial, ulong maximum = 0)
        {
            throw new NotImplementedException();
        }

        public ulong Grow(ulong delta)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(ulong index)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(ulong index, T value)
        {
            throw new NotImplementedException();
        }
        public readonly ulong Length;
    }
}
