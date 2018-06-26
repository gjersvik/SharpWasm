using System;
using JetBrains.Annotations;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class ModuleExportDescriptor: IEquatable<ModuleExportDescriptor>
    {
        public readonly ImportExportKind Kind;
        [NotNull] public readonly string Name;

        public ModuleExportDescriptor(ImportExportKind kind, string name)
        {
            Kind = kind;
            Name = name;
        }

        internal ModuleExportDescriptor(Export export): this(export.Kind, export.Name)
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