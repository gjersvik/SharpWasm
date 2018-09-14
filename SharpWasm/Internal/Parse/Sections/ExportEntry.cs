using System;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class ExportEntry: IEquatable<ExportEntry>
    {
        public readonly uint FieldLen;
        public readonly string FieldStr;
        public readonly ExternalKind ExternalKind;
        public readonly uint Index;

        public ExportEntry(string field, ExternalKind kind, uint index)
        {
            FieldLen = (uint) field.Length;
            FieldStr = field;
            ExternalKind = kind;
            Index = index;
        }

        public ExportEntry(BinaryReader reader)
        {
            FieldLen = Values.ToUInt(reader);
            FieldStr = ParseTools.ToUtf8(reader, FieldLen);
            ExternalKind = ParseTools.ToExternalKind(reader);
            Index = Values.ToUInt(reader);
        }

        public bool Equals(ExportEntry other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return FieldLen == other.FieldLen && string.Equals(FieldStr, other.FieldStr) && ExternalKind == other.ExternalKind && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ExportEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) FieldLen;
                hashCode = (hashCode * 397) ^ FieldStr.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) ExternalKind;
                hashCode = (hashCode * 397) ^ (int) Index;
                return hashCode;
            }
        }

        public static bool operator ==(ExportEntry left, ExportEntry right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExportEntry left, ExportEntry right)
        {
            return !Equals(left, right);
        }
    }
}
