using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal
{
    internal class Imports : ISection
    {
        public static readonly Imports Empty = new Imports(ImmutableArray<AImport>.Empty);

        public SectionCode Id { get; } = SectionCode.Import;
        public readonly ImmutableArray<AImport> ImportList;

        public readonly ImmutableArray<FunctionImport> Functions;
        public readonly MemoryImport Memory;
        public int FunctionCount => Functions.Length;

        public Imports(byte[] payload) : this(FromPayload(payload))
        {
        }

        private Imports(IEnumerable<AImport> list)
        {
            ImportList = list.ToImmutableArray();
            Functions = ImportList.Where(i => i.Kind == ImportExportKind.Function).Cast<FunctionImport>()
                .ToImmutableArray();
            Memory = ImportList.FirstOrDefault(i => i.Kind == ImportExportKind.Memory) as MemoryImport;
        }

        private static IEnumerable<AImport> FromPayload(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                return reader.ReadImports();
            }
        }
    }

    internal abstract class AImport
    {
        public readonly string Module;
        public readonly string Field;
        public readonly ImportExportKind Kind;

        protected AImport(string module, string field, ImportExportKind kind)
        {
            Module = module;
            Field = field;
            Kind = kind;
        }
    }

    internal class FunctionImport : AImport
    {
        public readonly uint TypeIndex;

        public FunctionImport(string module, string field, uint typeIndex) : base(module, field,
            ImportExportKind.Function)
        {
            TypeIndex = typeIndex;
        }
    }

    internal class MemoryImport : AImport
    {
        public readonly uint Initial;
        public readonly uint Maximum;

        public MemoryImport(string module, string field, uint initial, uint maximum = 0) : base(module, field,
            ImportExportKind.Memory)
        {
            Initial = initial;
            Maximum = maximum;
        }
    }
}