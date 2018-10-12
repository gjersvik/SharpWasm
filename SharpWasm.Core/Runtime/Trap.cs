namespace SharpWasm.Core.Runtime
{
    public sealed class Trap
    {
        public static readonly Trap Value = new Trap();

        private Trap()
        {

        }
    }
}
