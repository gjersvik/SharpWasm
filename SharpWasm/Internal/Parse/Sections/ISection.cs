﻿namespace SharpWasm.Internal.Parse.Sections
{
    internal interface ISection
    {
        SectionCode Id { get; }
    }
}