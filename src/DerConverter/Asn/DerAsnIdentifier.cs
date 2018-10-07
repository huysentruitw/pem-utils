using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public class DerAsnIdentifier
    {
        public DerAsnTagClass TagClass { get; private set; }

        public DerAsnEncodingType EncodingType { get; private set; }

        public long Tag { get; private set; }

        public DerAsnIdentifier(DerAsnTagClass tagClass, DerAsnEncodingType encodingType, long tag)
        {
            TagClass = tagClass;
            EncodingType = encodingType;
            Tag = tag;
        }

        public override string ToString()
        {
            return $"[0x{Tag:X2}, {TagClass}, {EncodingType}]";
        }

        public override bool Equals(object obj)
        {
            var identifier = obj as DerAsnIdentifier;
            return identifier != null &&
                   TagClass == identifier.TagClass &&
                   EncodingType == identifier.EncodingType &&
                   Tag == identifier.Tag;
        }

        public override int GetHashCode()
        {
            var hashCode = -1994895936;
            hashCode = hashCode * -1521134295 + TagClass.GetHashCode();
            hashCode = hashCode * -1521134295 + EncodingType.GetHashCode();
            hashCode = hashCode * -1521134295 + Tag.GetHashCode();
            return hashCode;
        }

        internal IEnumerable<byte> Encode()
        {
            var firstByte = (byte)(((byte)TagClass << 6) + ((byte)EncodingType << 5));

            if (Tag > 30)
            {
                var tag = Tag;
                var data = new Stack<byte>();
                do
                {
                    data.Push((byte)(tag & 0x7F));
                    tag >>= 7;
                } while (tag != 0);

                yield return (byte)(firstByte | 0x1F);

                while (data.Count > 1)
                    yield return (byte)(data.Pop() | 0x80);

                yield return data.Pop();
            }
            else
            {
                yield return (byte)(firstByte + Tag);
            }
        }

        internal static DerAsnIdentifier Decode(Queue<byte> data)
        {
            var firstByte = data.Dequeue();
            var tagClass = (DerAsnTagClass)(firstByte >> 6);
            var encodingType = (DerAsnEncodingType)((firstByte >> 5) & 1);

            if ((firstByte & 0x1F) != 0x1F)
            {
                return new DerAsnIdentifier(
                    tagClass: tagClass,
                    encodingType: encodingType,
                    tag: firstByte & 0x1F);
            }

            // We can fit nine 7-bit values in a long (63-bit)
            int i = 0;
            long tag = 0;
            byte nextByte;
            do
            {
                if (++i > 9) throw new NotSupportedException("Tag number too large (more than 9 octets)");
                if (!data.Any()) throw new InvalidOperationException("Unexpected end of queue");
                nextByte = data.Dequeue();
                tag <<= 7;
                tag += nextByte & 0x7F;
            } while ((nextByte & 0x80) != 0); // Bit 8 == 0 indicates we're at the end of the tag

            return new DerAsnIdentifier(
                tagClass: tagClass,
                encodingType: encodingType,
                tag: tag);
        }
    }
}
