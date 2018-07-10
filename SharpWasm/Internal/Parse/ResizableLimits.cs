namespace SharpWasm.Internal.Parse
{
    internal class ResizableLimits
    {
        public readonly bool Flags ;
        public readonly uint Initial;
        public readonly uint Maximum;

        public ResizableLimits(bool flags, uint initial, uint maximum)
        {
            Flags = flags;
            Initial = initial;
            Maximum = maximum;
        }
    }
}
