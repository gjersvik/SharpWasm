namespace SharpWasm.Internal.Runtime
{
    internal class Local
    {
        public Stack Stack { get; }

        public Local(Stack stack)
        {
            Stack = stack;
        }
    }
}
