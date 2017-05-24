using System.Collections.Generic;
using System.Linq;

namespace DerConverter
{
    internal static class QueueExtensions
    {
        public static int DequeueDerLength(this Queue<byte> queue)
        {
            var count = queue.Dequeue();
            if (count < 0x80) return count;
            count -= 0x80;
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                result <<= 8;
                result += queue.Dequeue();
            }
            return result;
        }

        public static IEnumerable<T> Dequeue<T>(this Queue<T> queue, int count)
        {
            for (int i = 0; i < count; i++) yield return queue.Dequeue();
        }

        public static IEnumerable<T> DequeueAll<T>(this Queue<T> queue)
        {
            while (queue.Any()) yield return queue.Dequeue();
        }
    }
}
