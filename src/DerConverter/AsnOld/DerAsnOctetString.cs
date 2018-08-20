//using System;
//using System.Collections.Generic;

//namespace DerConverter.AsnOld
//{
//    public class DerAsnOctetString : DerAsnType
//    {
//        private readonly List<byte> _bytes = new List<byte>();

//        internal DerAsnOctetString(Queue<byte> rawData)
//            : base(DerAsnTypeTag.OctetString)
//        {
//            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
//            _bytes.AddRange(rawData.DequeueAll());
//        }

//        public DerAsnOctetString(byte[] bytes)
//            : base(DerAsnTypeTag.OctetString)
//        {
//            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
//            _bytes.AddRange(bytes);
//        }

//        public override object Value => _bytes.ToArray();

//        protected override byte[] InternalGetBytes()
//        {
//            return _bytes.ToArray();
//        }
//    }
//}
