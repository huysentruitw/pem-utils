using System;
using System.Collections.Generic;
using System.Text;

namespace DerConverter.Asn
{
    public class DerAsnPrintableString : DerAsnType
    {
        private readonly List<byte> _bytes = new List<byte>();

        internal DerAsnPrintableString(Queue<byte> rawData)
            : base(DerAsnTypeTag.PrintableString)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            _bytes.AddRange(rawData.DequeueAll());
        }

        public DerAsnPrintableString(string value)
            : base(DerAsnTypeTag.PrintableString)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _bytes.AddRange(Encoding.ASCII.GetBytes(value));
        }

        public override object Value => Encoding.ASCII.GetString(_bytes.ToArray());

        protected override byte[] InternalGetBytes() => _bytes.ToArray();
    }
}
