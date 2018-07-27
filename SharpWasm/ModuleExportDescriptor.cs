using System;
using JetBrains.Annotations;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm
{
    public class ModuleExportDescriptor: IEquatable<ModuleExportDescriptor>
    {
        public readonly ExternalKind Kind;
        [NotNull] public readonly string Name;

        public ModuleExportDescriptor(ExternalKind kind, string name)
        {
            Kind = kind;
            Name = name;
        }

        internal ModuleExportDescriptor(ExportEntry export): this(export.ExternalKind, export.FieldStr)
        {

        }

        public bool Equals(ModuleExportDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Kind == other.Kind && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ModuleExportDescriptor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Kind * 397) ^ Name.GetHashCode();
            }
        }

        public static bool operator ==(ModuleExportDescriptor left, ModuleExportDescriptor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ModuleExportDescriptor left, ModuleExportDescriptor right)
        {
            return !Equals(left, right);
        }
    }
}