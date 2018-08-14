using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public abstract class DerAsnType
    {
        public DerAsnTypeTag Tag { get; }

        public abstract object Value { get; }

        protected DerAsnType(DerAsnTypeTag tag)
        {
            Tag = tag;
        }

        internal byte[] GetBytes()
        {
            var rawData = InternalGetBytes();
            var result = new List<byte>();
            result.Add((byte)Tag);
            result.AddRange(rawData.Length.ToDerLengthBytes());
            result.AddRange(rawData);
            return result.ToArray();
        }

        protected abstract byte[] InternalGetBytes();

        internal static DerAsnType Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.Any()) throw new ArgumentException("Array is empty", nameof(data));
            return Parse(new Queue<byte>(data));
        }

        internal static DerAsnType Parse(Queue<byte> data)
        {
            var typeTag = (DerAsnTypeTag)data.Dequeue();
            var typeDataLength = data.DequeueDerLength();
            var typeData = new Queue<byte>(data.Dequeue(typeDataLength));

            switch (typeTag)
            {
                case DerAsnTypeTag.Boolean: return new DerAsnBoolean(typeData);
                case DerAsnTypeTag.Integer: return new DerAsnInteger(typeData);
                case DerAsnTypeTag.BitString: return new DerAsnBitString(typeData);
                case DerAsnTypeTag.OctetString: return new DerAsnOctetString(typeData);
                case DerAsnTypeTag.Null: return new DerAsnNull(typeData);
                case DerAsnTypeTag.ObjectIdentifier: return new DerAsnObjectIdentifier(typeData);
                case DerAsnTypeTag.Utf8String: return new DerAsnUtf8String(typeData);
                case DerAsnTypeTag.PrintableString: return new DerAsnPrintableString(typeData);
                case DerAsnTypeTag.Ia5tring: throw new NotImplementedException();
                case DerAsnTypeTag.UtcTime: return new DerAsnUtcTime(typeData);
                case DerAsnTypeTag.UnicodeString: throw new NotImplementedException();
                case DerAsnTypeTag.Sequence: return new DerAsnSequence(typeData);
                case DerAsnTypeTag.Set: return new DerAsnSet(typeData);
                default:
                    throw new NotImplementedException($"Type tag {typeTag} not implemented");
            }
        }
    }
}
