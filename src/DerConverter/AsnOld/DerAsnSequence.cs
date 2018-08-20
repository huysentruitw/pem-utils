//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DerConverter.AsnOld
//{
//    public sealed class DerAsnSequence : DerAsnType
//    {
//        private readonly List<DerAsnType> _items = new List<DerAsnType>();

//        internal DerAsnSequence(Queue<byte> rawData)
//            : base(DerAsnTypeTag.Sequence)
//        {
//            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
//            while (rawData.Any()) _items.Add(DerAsnType.Parse(rawData));
//        }

//        public DerAsnSequence(params DerAsnType[] items)
//            : base(DerAsnTypeTag.Sequence)
//        {
//            if (items == null) throw new ArgumentNullException(nameof(items));
//            _items.AddRange(items);
//        }

//        public override object Value => _items.ToArray();

//        public IReadOnlyList<DerAsnType> Items => _items;

//        protected override byte[] InternalGetBytes()
//        {
//            return _items
//                .Select(x => x.GetBytes())
//                .SelectMany(x => x)
//                .ToArray();
//        }
//    }
//}
