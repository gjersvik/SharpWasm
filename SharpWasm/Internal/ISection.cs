﻿namespace SharpWasm.Internal
{
    internal interface ISection
    {
        SectionId Id { get; }
        byte[] Payload { get; }
    }

    internal enum SectionId
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