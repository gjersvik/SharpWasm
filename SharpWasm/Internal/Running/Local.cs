namespace SharpWasm.Internal.Running
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
