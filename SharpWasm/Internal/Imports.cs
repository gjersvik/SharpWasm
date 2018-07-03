using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal class Imports: ISection
    {
        public static readonly Imports Empty = new Imports(ImmutableArray<Import>.Empty);

        public SectionId Id { get; } = SectionId.Import;
        public readonly ImmutableArray<Import> ImportList;

        public readonly ImmutableArray<Import> Functions;
        public int FunctionCount => Functions.Length;

        public Imports(byte[] payload): this(FromPayload(payload))
        {
        }

        private Imports(IEnumerable<Import> list)
        {
            ImportList = list.ToImmutableArray();
            Functions = ImportList;
        }

        private static IEnumerable<Import> FromPayload(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                return reader.ReadImports().ToImmutableArray();
            }
        }
    }

    internal class Import
    {
        public readonly string Module;
        public readonly string Field;
        public readonly ImportExportKind Kind = ImportExportKind.Function;
        public uint TypeIndex;

        public Import(string module, string field, uint typeIndex)
        {
            Module = module;
            Field = field;
            TypeIndex = typeIndex;
        }
    }
}
