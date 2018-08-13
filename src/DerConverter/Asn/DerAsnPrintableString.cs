using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerConverter.Asn
{
    public class DerAsnPrintableString : DerAsnType
    {
        private readonly List<byte> _bytes;

        internal DerAsnPrintableString(Queue<byte> rawData)
            : base(DerAsnTypeTag.PrintableString)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            _bytes = rawData.DequeueAll().ToList();
        }

        public DerAsnPrintableString(byte[] bytes)
            : base(DerAsnTypeTag.PrintableString)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            _bytes = bytes.ToList();
        }

        public override object Value => Encoding.ASCII.GetString(_bytes.ToArray());

        protected override byte[] InternalGetBytes()
        {
            return _bytes.ToArray();
        }
    }
}
