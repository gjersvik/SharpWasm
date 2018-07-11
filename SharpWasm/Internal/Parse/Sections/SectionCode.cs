namespace SharpWasm.Internal.Parse.Sections
{
    internal enum SectionCode: byte
    {
        Custom = 0,
        Type,
        Import,
        Function,
        Table,
        Memory,
        Global,
        Export,
        Start,
        Element,
        Code,
        Data
    }
}
