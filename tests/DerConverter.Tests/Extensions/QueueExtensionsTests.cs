using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DerConverter.Tests.Extensions
{
    [TestFixture]
    public class QueueExtensionsTests
    {
        #region Dequeue

        [Test]
        public void Dequeue_ZeroCount_ShouldReturnEmptyEnumerable()
        {
            var data = new Queue<byte>(new byte[] { 0x01, 0x02, 0x03 });
            var result = data.Dequeue(0);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Dequeue_CountLessThanAvailable_ShouldDequeueRequestedNumberOfElements()
        {
            var data = new Queue<byte>(new byte[] { 0x01, 0x02, 0x03 });
            var result = data.Dequeue(2);
            Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x02 }));
            Assert.That(data, Has.Count.EqualTo(1));
        }

        [Test]
        public void Dequeue_TooMuch_ShouldThrowInvalidOperationException()
        {
            var data = new Queue<byte>(new byte[] { 0x01, 0x02, 0x03 });
            Assert.Throws<InvalidOperationException>(() => data.Dequeue(4).ToArray());
        }

        #endregion

        #region DequeueAll

        [Test]
        public void DequeueAll_EmptyQueue_ShouldReturnEmptyEnumerable()
        {
            var data = new Queue<byte>();
            var result = data.DequeueAll();
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void DequeueAll_NonEmptyQueue_ShouldDequeueAllElements()
        {
            var data = new Queue<byte>(new byte[] { 0x01, 0x02, 0x03 });
            var result = data.DequeueAll();
            Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03 }));
            Assert.That(data, Is.Empty);
        }

        #endregion
    }
}
