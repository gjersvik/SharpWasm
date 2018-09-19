using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using SharpWasm.Core.Types;

namespace SharpWasm.Core.Segments
{
    internal class Import: IEquatable<Import>
    {
        [NotNull]public readonly string Module;
        [NotNull] public readonly string Name;
        public readonly ExternalKind Type;
        [CanBeNull] public readonly uint? Function;
        [CanBeNull] public readonly TableType Table;
        [CanBeNull] public readonly MemoryType Memory;
        [CanBeNull] public readonly GlobalType Global;

        public Import(string module, string name, uint functionType)
        {
            Module = module;
            Name = name;
            Type = ExternalKind.Function;
            Function = functionType;
        }

        public Import(string module, string name, [NotNull]TableType tableType)
        {
            Module = module;
            Name = name;
            Type = ExternalKind.Table;
            Table = tableType;
        }

        public Import(string module, string name, [NotNull]MemoryType memoryType)
        {
            Module = module;
            Name = name;
            Type = ExternalKind.Memory;
            Memory = memoryType;
        }

        public Import(string module, string name, [NotNull]GlobalType globalType)
        {
            Module = module;
            Name = name;
            Type = ExternalKind.Global;
            Global = globalType;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public bool Equals(Import other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (!string.Equals(Module, other.Module)) return false;
            if (!string.Equals(Name, other.Name)) return false;
            if (Type != other.Type) return false;
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (Type == ExternalKind.Function)
                return Function.Equals(other.Function);
            if (Type == ExternalKind.Table)
                return Table.Equals(other.Table);
            return Type == ExternalKind.Memory ? Memory.Equals(other.Memory) : Global.Equals(other.Global);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Import) obj);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Module.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Type;
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (Type == ExternalKind.Function)
                    return (hashCode * 397) ^ Function.GetHashCode();
                if (Type == ExternalKind.Table)
                    return (hashCode * 397) ^ Table.GetHashCode();
                if (Type == ExternalKind.Memory)
                    return (hashCode * 397) ^ Memory.GetHashCode();

                return (hashCode * 397) ^ Global.GetHashCode();
            }
        }

        public static bool operator ==(Import left, Import right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Import left, Import right)
        {
            return !Equals(left, right);
        }
    }
}