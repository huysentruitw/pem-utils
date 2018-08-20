//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DerConverter.AsnOld
//{
//    public class DerAsnUtf8String : DerAsnType
//    {
//        private readonly List<byte> _bytes = new List<byte>();

//        internal DerAsnUtf8String(Queue<byte> rawData)
//            : base(DerAsnTypeTag.Utf8String)
//        {
//            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
//            _bytes.AddRange(rawData.DequeueAll());
//        }

//        public DerAsnUtf8String(string value)
//            : base(DerAsnTypeTag.Utf8String)
//        {
//            if (value == null) throw new ArgumentNullException(nameof(value));
//            _bytes.AddRange(Encoding.UTF8.GetBytes(value));
//        }

//        public override object Value => Encoding.UTF8.GetString(_bytes.ToArray());

//        protected override byte[] InternalGetBytes() => _bytes.ToArray();
//    }
//}
