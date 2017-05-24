using System;
using System.Collections.Generic;

namespace DerConverter.Asn
{
    public class DerAsnBoolean : DerAsnType
    {
        private readonly bool _value;

        internal DerAsnBoolean(Queue<byte> rawData)
            : base(DerAsnTypeTag.Boolean)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            if (rawData.Count != 1) throw new ArgumentException("Boolean-type must contain one data byte", nameof(rawData));
            _value = rawData.Dequeue() != 0;
        }

        public DerAsnBoolean(bool value)
            : base(DerAsnTypeTag.Boolean)
        {
            _value = value;
        }

        public override object Value => _value;

        protected override byte[] InternalGetBytes()
        {
            return new[] { _value ? (byte)0xFF : (byte)0x00 };
        }
    }
}
