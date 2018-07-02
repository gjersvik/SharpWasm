using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace SharpWasm.Internal
{
    internal class Exports: ISection
    {
        public static readonly Exports Empty = new Exports();

        public SectionId Id { get; } = SectionId.Export;
        public ImmutableArray<Export> ExportList;

        public Exports(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                ExportList = reader.ReadExports().ToImmutableArray();
            }
        }

        private Exports()
        {
            ExportList = ImmutableArray<Export>.Empty;
        }

        public uint Func(string name)
        {
            var export = ExportList.Where(e => e.Kind == ImportExportKind.Function).First(e => e.Name == name);
            return export.Index;
        }
    }

    internal class Export
    {
        [NotNull] public readonly string Name;
        public readonly ImportExportKind Kind;
        public readonly uint Index;

        public Export(string name, ImportExportKind kind, uint index)
        {
            Name = name;
            Kind = kind;
            Index = index;
        }
    }
}
