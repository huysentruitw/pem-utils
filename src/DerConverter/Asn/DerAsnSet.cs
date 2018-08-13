using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public sealed class DerAsnSet : DerAsnType
    {
        private readonly List<DerAsnType> _items = new List<DerAsnType>();

        internal DerAsnSet(Queue<byte> rawData)
            : base(DerAsnTypeTag.Set)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            while (rawData.Any()) _items.Add(DerAsnType.Parse(rawData));
        }

        public DerAsnSet(params DerAsnType[] items)
            : base(DerAsnTypeTag.Set)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            _items.AddRange(items);
        }

        public override object Value => _items.ToArray();

        public IReadOnlyList<DerAsnType> Items => _items;

        protected override byte[] InternalGetBytes()
        {
            return _items
                .Select(x => x.GetBytes())
                .SelectMany(x => x)
                .ToArray();
        }
    }
}
