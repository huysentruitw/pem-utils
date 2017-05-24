using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public class DerAsnInteger : DerAsnType
    {
        private readonly List<byte> _bytes = new List<byte>();
        private readonly bool _unsigned;

        internal DerAsnInteger(Queue<byte> rawData)
            : base(DerAsnTypeTag.Integer)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));

            if (rawData.Peek() == 0)
            {
                _unsigned = true;
                rawData.Dequeue();
                _bytes.AddRange(rawData.DequeueAll());
            }
            else
            {
                _unsigned = false;
                _bytes.AddRange(rawData.DequeueAll());
            }
        }

        public DerAsnInteger(byte[] bytes, bool unsigned)
            : base(DerAsnTypeTag.Integer)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            _bytes.AddRange(bytes);
            _unsigned = unsigned;
        }

        public override object Value => _bytes.ToArray();

        public bool Unsigned => _unsigned;

        protected override byte[] InternalGetBytes()
        {
            var result = new List<byte>();
            result.AddRange(_bytes.SkipWhile(x => x == 0));
            if (!result.Any() || (_unsigned && result.First() >= 0x80)) result.Insert(0, 0x00);
            return result.ToArray();
        }
    }
}
