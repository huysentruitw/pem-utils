using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public class DerAsnContextSpecific : DerAsnType
    {
        private readonly List<DerAsnType> _items = new List<DerAsnType>();

        internal DerAsnContextSpecific(DerAsnTypeTag tag, Queue<byte> rawData)
            : base(tag)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            while (rawData.Any()) _items.Add(DerAsnType.Parse(rawData));
        }

        public DerAsnContextSpecific(DerAsnTypeTag tag, DerAsnType[] items)
            : base(tag)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            _items.AddRange(items);
        }

        public override object Value => _items.ToArray();

        public IList<DerAsnType> Items => _items;

        protected override byte[] InternalGetBytes()
        {
            return _items
                .Select(x => x.GetBytes())
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
