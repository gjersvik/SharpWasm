using System;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class ImportEntry
    {
        internal static ImportEntry Parse(BinaryReader reader)
        {
            var entry = new ImportEntry(reader);
            switch (entry.Kind)
            {
                case ExternalKind.Function:
                    return new ImportEntryFunction(reader,entry);
                case ExternalKind.Table:
                    return new ImportEntryTable(reader, entry);
                case ExternalKind.Memory:
                    return new ImportEntryMemory(reader, entry);
                case ExternalKind.Global:
                    return new ImportEntryGlobal(reader, entry);
                default:
                    throw new NotImplementedException();
            }
        }

        public readonly uint ModuleLen;
        public readonly string ModuleStr;
        public readonly uint FieldLen;
        public readonly string FieldStr;
        public readonly ExternalKind Kind;

        protected ImportEntry(string module, string field, ExternalKind kind)
        {
            ModuleLen = (uint)module.Length;
            ModuleStr = module;
            FieldLen = (uint)field.Length;
            FieldStr = field;
            Kind = kind;
        }
        protected ImportEntry(ImportEntry other)
        {
            ModuleLen = other.ModuleLen;
            ModuleStr = other.ModuleStr;
            FieldLen = other.FieldLen;
            FieldStr = other.FieldStr;
            Kind = other.Kind;
        }

        private ImportEntry(BinaryReader reader)
        {
            ModuleLen = Values.ToUInt(reader);
            ModuleStr = ParseTools.ToUtf8(reader, ModuleLen);
            FieldLen = Values.ToUInt(reader);
            FieldStr = ParseTools.ToUtf8(reader, FieldLen);
            Kind = ParseTools.ToExternalKind(reader);
        }
    }

    internal class ImportEntryFunction : ImportEntry
    {
        public readonly uint Type;

        public ImportEntryFunction(BinaryReader reader, ImportEntry entry) : base(entry)
        {
            Type = Values.ToUInt(reader);
        }

        public ImportEntryFunction(string module, string field, uint type):base(module,field,ExternalKind.Function)
        {
            Type = type;
        }
    }

    internal class ImportEntryTable : ImportEntry
    {
        public readonly TableType Type;

        public ImportEntryTable(BinaryReader reader, ImportEntry entry) : base(entry)
        {
            Type = new TableType(reader);
        }

        public ImportEntryTable(string module, string field, TableType type) : base(module, field, ExternalKind.Table)
        {
            Type = type;
        }
    }

    internal class ImportEntryMemory : ImportEntry
    {
        public readonly MemoryType Type;

        public ImportEntryMemory(BinaryReader reader, ImportEntry entry) : base(entry)
        {
            Type = new MemoryType(reader);
        }

        public ImportEntryMemory(string module, string field, MemoryType type) : base(module, field, ExternalKind.Memory)
        {
            Type = type;
        }
    }

    internal class ImportEntryGlobal : ImportEntry
    {
        public readonly GlobalType Type;

        public ImportEntryGlobal(BinaryReader reader, ImportEntry entry) : base(entry)
        {
            Type = new GlobalType(reader);
        }

        public ImportEntryGlobal(string module, string field, GlobalType type) : base(module, field, ExternalKind.Global)
        {
            Type = type;
        }
    }
}
