using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerConverter.Asn
{
    public class DerAsnUtf8String : DerAsnType
    {
        private readonly List<byte> _bytes;

        internal DerAsnUtf8String(Queue<byte> rawData)
            : base(DerAsnTypeTag.Utf8String)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            _bytes = rawData.DequeueAll().ToList();
        }

        public DerAsnUtf8String(byte[] bytes)
            : base(DerAsnTypeTag.Utf8String)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            _bytes = bytes.ToList();
        }

        public override object Value => Encoding.UTF8.GetString(_bytes.ToArray());
        

        protected override byte[] InternalGetBytes()
        {
            return _bytes.ToArray();
        }
    }
}
