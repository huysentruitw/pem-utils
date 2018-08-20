using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    /// <summary>
    /// A default implementation of <see cref="IDerAsnDecoder"/> that registers known ASN.1 types.
    /// </summary>
    public class DefaultDerAsnDecoder : IDerAsnDecoder
    {
        private readonly Dictionary<TypeKey, TypeConstructor> _registeredTypes = new Dictionary<TypeKey, TypeConstructor>();
        private readonly Dictionary<DerAsnIdentifier, TypeConstructor> _registeredClassSpecificTypes = new Dictionary<DerAsnIdentifier, TypeConstructor>();

        /// <summary>
        /// Delegate that describes a function to construct a type during decoding.
        /// </summary>
        /// <param name="decoder">The decoder instance.</param>
        /// <param name="identifier">The tag identifier (contains tag class, encoding type and tag).</param>
        /// <param name="data">The data to decode for the specific type.</param>
        /// <returns>The decoded ASN.1 type (primitive or constructed set/sequence).</returns>
        public delegate DerAsnType TypeConstructor(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> data);

        /// <summary>
        /// Constructs a new <see cref="DefaultDerAsnDecoder"/> instance.
        /// </summary>
        public DefaultDerAsnDecoder()
        {
            RegisterKnownTypes();
        }

        /// <summary>
        /// Cleanup used resources.
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        /// Decode the ASN.1 structure encoded in the given byte array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The decoded ASN.1 type (primitive or constructed set/sequence).</returns>
        public DerAsnType Decode(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.Any()) throw new ArgumentException("Data cannot be empty", nameof(data));
            return Decode(new Queue<byte>(data));
        }

        /// <summary>
        /// Registers a generic, class independent type.
        /// </summary>
        /// <param name="encodingType">The encoding type.</param>
        /// <param name="tag">The tag number.</param>
        /// <param name="typeConstructor">The type constructor.</param>
        /// <param name="replace">True to allow replacing an existing registration, otherwise false</param>
        /// <returns>The <see cref="DefaultDerAsnDecoder"/> instance.</returns>
        public DefaultDerAsnDecoder RegisterGenericType(DerAsnEncodingType encodingType, long tag, TypeConstructor typeConstructor, bool replace = false)
        {
            if (typeConstructor == null) throw new ArgumentNullException(nameof(typeConstructor));

            var typeKey = new TypeKey { EncodingType = encodingType, Tag = tag };

            if (!replace && _registeredTypes.ContainsKey(typeKey))
                throw new InvalidOperationException($"Type with encoding type {encodingType} and tag number {tag} already registered");

            _registeredTypes[typeKey] = typeConstructor;

            return this;
        }

        /// <summary>
        /// Registers a class dependent type.
        /// </summary>
        /// <param name="identifier">The exact tag identifier of the type.</param>
        /// <param name="typeConstructor">The type constructor.</param>
        /// <param name="replace">True to allow replacing an existing registration, otherwise false</param>
        /// <returns>The <see cref="DefaultDerAsnDecoder"/> instance.</returns>
        public DefaultDerAsnDecoder RegisterType(DerAsnIdentifier identifier, TypeConstructor typeConstructor, bool replace = false)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier));
            if (typeConstructor == null) throw new ArgumentNullException(nameof(typeConstructor));

            if (!replace && _registeredClassSpecificTypes.ContainsKey(identifier))
                throw new InvalidOperationException($"Type with class {identifier.TagClass}, encoding type {identifier.EncodingType} and tag number {identifier.Tag} already registered");

            _registeredClassSpecificTypes[identifier] = typeConstructor;

            return this;
        }

        protected virtual void RegisterKnownTypes()
        {
            //RegisterGenericType(DerAsnIdentifiers.Universal.BitString, (decoder, identifier, data) => new DerAsnBitString(identifier, data));

        }

        protected virtual TypeConstructor FindTypeConstructor(DerAsnIdentifier identifier)
        {
            if (_registeredClassSpecificTypes.TryGetValue(identifier, out var classSpecificTypeConstructor))
                return classSpecificTypeConstructor;

            var typeKey = new TypeKey { EncodingType = identifier.EncodingType, Tag = identifier.Tag };
            if (_registeredTypes.TryGetValue(typeKey, out classSpecificTypeConstructor))
                return classSpecificTypeConstructor;

            return null;
        }

        private DerAsnType Decode(Queue<byte> data)
        {
            var identifier = DerAsnIdentifier.Decode(data);

            var typeConstructor = FindTypeConstructor(identifier)
                ?? throw new InvalidOperationException($"No type registered for identifier {identifier}");

            var length = DerAsnLength.Decode(data);
            if (data.Count != length)
                throw new InvalidOperationException($"Expected {length} bytes to decode type with identifier {identifier} but got {data.Count} bytes");

            return typeConstructor(this, identifier, data);
        }

        private class TypeKey
        {
            public DerAsnEncodingType EncodingType { get; set; }

            public long Tag { get; set; }

            public override bool Equals(object obj)
            {
                var key = obj as TypeKey;
                return key != null &&
                       EncodingType == key.EncodingType &&
                       Tag == key.Tag;
            }

            public override int GetHashCode()
            {
                var hashCode = -1011251413;
                hashCode = hashCode * -1521134295 + EncodingType.GetHashCode();
                hashCode = hashCode * -1521134295 + Tag.GetHashCode();
                return hashCode;
            }
        }
    }
}
