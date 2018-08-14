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

        public DerAsnUtcTime(DateTimeOffset time, bool includeSeconds = true)
            : base(DerAsnTypeTag.UtcTime)
        {
            string timeZoneSuffix = "Z";
            string timeAsString = null;

            if (time.Offset != TimeSpan.Zero)
            {
                var offsetPrefix = time.Offset > TimeSpan.Zero ? '+' : '-';
                timeZoneSuffix = $"{offsetPrefix}{time.Offset:hhmm}";
            }

            if (includeSeconds)
            {
                timeAsString = $"{time:yyMMddHHmmss}{timeZoneSuffix}";
            }
            else
            {
                timeAsString = $"{time:yyMMddHHmm}{timeZoneSuffix}";
            }

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
