using System;
using JetBrains.Annotations;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm
{
    public class ModuleExportDescriptor: IEquatable<ModuleExportDescriptor>
    {
        private readonly ExternalKind _kind;
        [NotNull] private readonly string _name;

        public ModuleExportDescriptor(ExternalKind kind, string name)
        {
            _kind = kind;
            _name = name;
        }

        internal ModuleExportDescriptor(ExportEntry export): this(export.ExternalKind, export.FieldStr)
        {

        }

        public bool Equals(ModuleExportDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _kind == other._kind && string.Equals(_name, other._name);
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
                return ((int) _kind * 397) ^ _name.GetHashCode();
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