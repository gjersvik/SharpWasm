using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace SharpWasm.Core.Types
{
    public class ExternalType: IEquatable<ExternalType>
    {
        public readonly ExternalKind Type;
        [CanBeNull] public readonly FunctionType FunctionType;
        [CanBeNull] public readonly TableType TableType;
        [CanBeNull] public readonly MemoryType MemoryType;
        [CanBeNull] public readonly GlobalType GlobalType;

        public ExternalType([NotNull]FunctionType functionType)
        {
            Type = ExternalKind.Function;
            FunctionType = functionType;
        }

        public ExternalType([NotNull]TableType tableType)
        {
            Type = ExternalKind.Table;
            TableType = tableType;
        }

        public ExternalType([NotNull]MemoryType memoryType)
        {
            Type = ExternalKind.Memory;
            MemoryType = memoryType;
        }

        public ExternalType([NotNull]GlobalType globalType)
        {
            Type = ExternalKind.Global;
            GlobalType = globalType;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public bool Equals(ExternalType other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Type != other.Type) return false;
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (Type == ExternalKind.Function)
                return FunctionType.Equals(other.FunctionType);
            if (Type == ExternalKind.Table)
                return TableType.Equals(other.TableType);
            return Type == ExternalKind.Memory ? MemoryType.Equals(other.MemoryType) : GlobalType.Equals(other.GlobalType);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ExternalType) obj);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (Type == ExternalKind.Function)
                    return ((int) Type * 397) ^ FunctionType.GetHashCode();
                if (Type == ExternalKind.Table)
                    return ((int) Type * 397) ^ TableType.GetHashCode();
                if (Type == ExternalKind.Memory)
                    return ((int) Type * 397) ^ MemoryType.GetHashCode();

                return ((int) Type * 397) ^ GlobalType.GetHashCode();
            }
        }

        public static bool operator ==(ExternalType left, ExternalType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExternalType left, ExternalType right)
        {
            return !Equals(left, right);
        }
    }

    public enum ExternalKind : byte
    {
        Function = 0,
        Table = 1,
        Memory = 2,
        Global = 3
    }
}