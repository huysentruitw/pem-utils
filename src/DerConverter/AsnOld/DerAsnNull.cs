//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DerConverter.Asn
//{
//    public class DerAsnNull : DerAsnType
//    {
//        internal DerAsnNull(Queue<byte> rawData)
//            : base(DerAsnTypeTag.Null)
//        {
//            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
//            if (rawData.Count != 0) throw new ArgumentException("Null-type can't contain any data", nameof(rawData));
//        }

//        public DerAsnNull()
//            : base(DerAsnTypeTag.Null)
//        {
//        }

//        public override object Value => null;

//        protected override byte[] InternalGetBytes() => new byte[0];
//    }
//}
