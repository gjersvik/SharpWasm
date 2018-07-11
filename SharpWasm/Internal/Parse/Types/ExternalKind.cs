namespace SharpWasm.Internal.Parse.Types
{
    internal enum ExternalKind: byte
    {
        Function = 0,
        Table = 1,
        Memory = 2,
        Global = 3
    }
}
