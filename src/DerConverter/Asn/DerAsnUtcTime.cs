using System;
using System.Collections.Generic;
using System.Text;

namespace DerConverter.Asn
{
    public class DerAsnUtcTime : DerAsnType
    {
        private readonly List<byte> _bytes = new List<byte>();

        internal DerAsnUtcTime(Queue<byte> rawData)
            : base(DerAsnTypeTag.UtcTime)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            _bytes.AddRange(rawData.DequeueAll());
        }

        public DerAsnUtcTime(DateTime time)
            : base(DerAsnTypeTag.UtcTime)
        {
            var timeAsString = $"{time.ToString("yyMMddHHmmss")}Z";
            var timeBytes = Encoding.ASCII.GetBytes(timeAsString);
            _bytes.AddRange(timeBytes);
        }

        public DerAsnUtcTime(byte[] bytes)
            : base(DerAsnTypeTag.UtcTime)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            _bytes.AddRange(bytes);
        }

        public override object Value => _bytes.ToArray();

        protected override byte[] InternalGetBytes()
        {
            return _bytes.ToArray();
        }
    }
}
