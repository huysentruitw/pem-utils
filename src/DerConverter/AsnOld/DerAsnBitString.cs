//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;

//namespace DerConverter.AsnOld
//{
//    public class DerAsnBitString : DerAsnType<BitArray>
//    {
//        internal DerAsnBitString(DerAsnIdentifier identifier, Queue<byte> data)
//            : base(identifier, data)
//        {
//        }

//        protected override BitArray DecodeValue(Queue<byte> data)
//        {
//            var unusedLowerBitsInLastByte = data.Dequeue();
//            var bytes = data.DequeueAll().ToArray();

//            var result = new BitArray(bytes.Length * 8 - unusedLowerBitsInLastByte);
//            for (int i = 0; i < result.Length; i++)
//                result[i] = (bytes[i / 8] << (i % 8) & 0x80) != 0;

//            return result;
//        }

//        protected override byte[] EncodeValue(BitArray value)
//        {
//            throw new System.NotImplementedException();
//        }

//        //internal DerAsnBitString(Queue<byte> rawData)
//        //    : base(DerAsnTypeTag.BitString)
//        //{
//        //    if (rawData == null) throw new ArgumentNullException(nameof(rawData));
//        //    _unusedLowerBitsInLastByte = rawData.Dequeue();
//        //    _bytes.AddRange(rawData.DequeueAll());
//        //}

//        //public DerAsnBitString(byte[] bytes, int unusedLowerBitsInLastByte = 0)
//        //    : base(DerAsnTypeTag.BitString)
//        //{
//        //    if (bytes == null) throw new ArgumentNullException(nameof(bytes));
//        //    if (unusedLowerBitsInLastByte < 0 || unusedLowerBitsInLastByte > 7) throw new ArgumentOutOfRangeException(nameof(unusedLowerBitsInLastByte));
//        //    _bytes.AddRange(bytes);
//        //    _unusedLowerBitsInLastByte = unusedLowerBitsInLastByte;
//        //}

//        //public override object Value => _bytes.ToArray();

//        //public int UnusedLowerBitsInLastByte => _unusedLowerBitsInLastByte;

//        //protected override byte[] InternalGetBytes()
//        //{
//        //    var result = new List<byte>();
//        //    result.Add((byte)_unusedLowerBitsInLastByte);
//        //    result.AddRange(_bytes);
//        //    return result.ToArray();
//        //}
//    }
//}
